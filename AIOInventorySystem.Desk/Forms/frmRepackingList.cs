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
    public partial class frmRepackingList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> billList = new List<int>();

        public frmRepackingList()
        {
            InitializeComponent();
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.DeleteSaleBill == "True")
                rbtnDeleteMultipleBill.Enabled = true;
            else
                rbtnDeleteMultipleBill.Enabled = false;
            Bindgrid();
        }

        public void Bindgrid()
        {
            try
            {
                DateTime date = DateTime.Now.Date;
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.RepackNo As 'Repack No',CONVERT(VARCHAR(24),pm.RepackDate,103) as 'Repack Date' FROM RepackMaster as pm inner join RepackDetail as pd on pm.ID=pd.RepackNo  where pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvPorderInfo.Columns[0].Visible = false;
                else
                    GvPorderInfo.Columns[0].Visible = true;
                GvPorderInfo.Columns["Repack No"].Width = 150;
                GvPorderInfo.Columns["Repack Date"].Width = 150;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkRepackNo.Checked == true)
                {
                    if (txtRepackNo.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.RepackNo As 'Repack No',CONVERT(VARCHAR(24),pm.RepackDate,103) as 'Repack Date' FROM RepackMaster as pm inner join RepackDetail as pd on pm.ID=pd.RepackNo  where pm.RepackNo='" + txtRepackNo.Text + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        GvPorderInfo.Refresh();
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvPorderInfo.Columns[0].Visible = false;
                            else
                                GvPorderInfo.Columns[0].Visible = true;
                            GvPorderInfo.Columns["Repack No"].Width = 150;
                            GvPorderInfo.Columns["Repack Date"].Width = 150;
                        }
                        else
                            MessageBox.Show("Record not found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select purchase Order Number.", "Warning");
                    this.ActiveControl = txtRepackNo;
                }
                else if (chkproductname.Checked == true)
                {
                    if (txtproductname.Text.Trim() != "")
                    {
                        string Proname1 = txtproductname.Text.Trim().Split(',')[0];
                        string company = txtproductname.Text.Trim().Split(',')[1];
                        string unit = txtproductname.Text.Trim().Split(',')[2];
                        ProductRepository productrepo = new ProductRepository();
                        var productdata = productrepo.GetAll().Where(t => t.ProductName == Proname1 && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.RepackNo As 'Repack No',CONVERT(VARCHAR(24),pm.RepackDate,103) as 'Repack Date' FROM RepackMaster as pm inner join RepackDetail as pd on pm.ID=pd.RepackNo  where pd.ProductNo='" + productdata.ID + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        GvPorderInfo.Refresh();
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvPorderInfo.Columns[0].Visible = false;
                            else
                                GvPorderInfo.Columns[0].Visible = true;
                            GvPorderInfo.Columns["Repack No"].Width = 150;
                            GvPorderInfo.Columns["Repack Date"].Width = 150;
                        }
                        else
                            MessageBox.Show("Record not found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Enter Product Name.", "Warning");
                    this.ActiveControl = txtproductname;
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (frmRepacking.repackinfo == null)
                    {
                        frmRepacking purord = new frmRepacking(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        purord.ShowDialog();
                        frmRepacking.repackinfo = null;
                    }
                    else
                    {
                        this.Close();
                        frmRepacking.repackinfo.LoadData(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        frmRepacking.repackinfo.Show();
                        frmRepacking.repackinfo = null;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmRepackingList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmRepacking.repackinfo = null;
            }
            catch (Exception)
            { }
        }

        private void frmRepackingList_Load(object sender, EventArgs e)
        {
            try
            {
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation Where RepackStatus='Repackage'", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation Where RepackStatus='Repackage' and compid=" + CommonMethod.CompId, con);
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
                    txtproductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void txtPOrderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chkproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void txtproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void txtproductname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtproductname.Text.Trim() != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtproductname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtproductname.Text.Trim().Split(',')[0];
                            company = txtproductname.Text.Trim().Split(',')[1];
                            unit = txtproductname.Text.Trim().Split(',')[2];
                        }
                        catch (Exception)
                        { }

                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.RepackStatus == "Repackage").FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.RepackStatus == "Repackage" && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtproductname.Text = "";
                            this.ActiveControl = txtproductname;
                        }
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtproductname.Text = "";
                        this.ActiveControl = txtproductname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                foreach (DataGridViewRow row in GvPorderInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(Convert.ToInt32(row.Cells[3].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[3].Value));
                        }
                    }
                }
                if (billList.Count != 0)
                {
                    DeleteMultiplebill(billList);
                    this.ActiveControl = txtRepackNo;
                    Bindgrid();
                }
                else
                {
                    MessageBox.Show("Please select at least one bill!!");
                    this.ActiveControl = GvPorderInfo;
                }
            }
            catch (Exception) { }
        }

        public void DeleteMultiplebill(List<int> billList)
        {
            try
            {
                for (int q = 0; q < billList.Count; q++)
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", BPname = "", BCname = "", BUname = "";
                    decimal NetAmount = 0; string Suppliername = "", DeletedBillNo = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, repackQty = 0;
                    int productId = 0, bulkProductId = 0;
                    RepackMasterRepository repackMRepo = new RepackMasterRepository();
                    RepackMaster repackdata = repackMRepo.GetAll().Where(t => t.RepackNo == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (repackdata != null)
                    {
                        int repackId = Convert.ToInt32(repackdata.ID);
                        var repackdata1 = repackMRepo.GetAll().Where(t => t.RepackNo == billList[q] && t.ID == repackId && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (repackdata1 != null)
                        {
                            // add details for history table
                            try
                            {
                                DeletedBillNo = Convert.ToString(repackdata1.RepackNo);
                                Suppliername = null;
                                NetAmount = 0;

                                DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                DeleteHistoryTable historydata = new DeleteHistoryTable();
                                historydata.DeleteType = "Repack";
                                historydata.BillNo = DeletedBillNo;
                                historydata.SupplierOrCustomerName = Suppliername;
                                historydata.Amount = Convert.ToDecimal(NetAmount);
                                historydata.CurrentDate = DateTime.Now.Date;
                                historydata.BillDate = Convert.ToDateTime(repackdata1.RepackDate);
                                historydata.CompId = CommonMethod.CompId;
                                historydata.TransactionYear = CommonMethod.TransactionYear;
                                historyRepo.Add(historydata);
                                historyRepo.Save();
                            }
                            catch (Exception) { }
                            RepackDetailRepository repackDRepo = new RepackDetailRepository();
                            List<RepackDetail> RList = repackDRepo.GetAll().Where(t => t.RepackNo == repackId && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                            if (RList.Count != 0)
                            {
                                foreach (var item in RList)
                                {
                                    decimal rate = 0; int updateStockId = 0;
                                    productId = Convert.ToInt32(item.ProductNo);
                                    bulkProductId = Convert.ToInt32(item.BulkProductNo);
                                    Quantity = Convert.ToDecimal(item.Qty);
                                    repackQty = Convert.ToDecimal(repackDRepo.GetAll().Where(t => t.ProductNo == productId && t.RepackNo == repackId && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Qty));
                                    ProductRepository productRepo = new ProductRepository();
                                    ProductInformation proddata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        proddata = productRepo.GetAll().Where(t => t.ID == productId).FirstOrDefault();
                                    else
                                        proddata = productRepo.GetAll().Where(t => t.ID == productId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (proddata != null)
                                    {
                                        productname = proddata.ProductName;
                                        company = proddata.ManufactureCompany;
                                        unit = proddata.Uniti;
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
                                            newsqty = (pqty) - Quantity;
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
                                            if (settData.StockOn == "Purchase Rate")
                                            {
                                                try
                                                {
                                                    db.connect();
                                                    SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
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
                                            }
                                            if (settData.StockOn == "Sale Rate")
                                            {
                                                try
                                                {
                                                    db.connect();
                                                    SqlCommand command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
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
                                            }
                                            sdata.ProductRate = Convert.ToDecimal(rate);
                                            sdata.CompId = CommonMethod.CompId;
                                            sdata.TransactionYear = CommonMethod.TransactionYear;
                                            sdata.Id = updateStockId;
                                            stockRepo.Edit(sdata);
                                            stockRepo.Save();
                                        }
                                        stockRepo.Dispose();
                                    }
                                    if (bulkProductId != 0)
                                    {
                                        ProductRepository prodRepo = new ProductRepository();
                                        ProductInformation pdata = new ProductInformation();
                                        if (CommonMethod.commProduct == true)
                                            pdata = prodRepo.GetAll().Where(t => t.ID == bulkProductId).FirstOrDefault();
                                        else
                                            pdata = prodRepo.GetAll().Where(t => t.ID == bulkProductId && t.CompId == CommonMethod.CompId).FirstOrDefault();

                                        BPname = pdata.ProductName;
                                        BCname = pdata.ManufactureCompany;
                                        BUname = pdata.Uniti;
                                        Quantity = Convert.ToDecimal(item.BulkQty);
                                        repackQty = Convert.ToDecimal(repackDRepo.GetAll().Where(t => t.BulkProductNo == bulkProductId && t.RepackNo == repackId && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.BulkQty));
                                        StockRepository stockRepo1 = new StockRepository();
                                        AIOInventorySystem.Data.Model.Stock sdata1 = new Data.Model.Stock();
                                        var stockdata1 = stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (stockdata1 != null)
                                        {
                                            newsqty = 0; sqty = 0; pqty = 0; prqty = 0; newrqty = 0; rqty = 0; srqty = 0; dqty = 0; oqty = 0;
                                            sqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleQty);
                                            pqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurchaseQty);
                                            rqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().RemQty);
                                            oqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().OpeningQty);
                                            dqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().DeadQty);
                                            prqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurReturnQty);
                                            srqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleReturnQty);
                                            newsqty = (sqty) - Quantity;
                                            newrqty = oqty + pqty - prqty - newsqty + srqty - dqty;
                                            updateStockId = stockdata1.Id;
                                        }
                                        sdata1 = stockRepo1.GetById(stockdata1.Id);
                                        if (sdata1 != null)
                                        {
                                            sdata1.ProductId = stockdata1.ProductId;
                                            sdata1.ProductName = BPname;
                                            sdata1.MfgCompany = BCname;
                                            sdata1.Unit = BUname;
                                            sdata1.OpeningQty = stockdata1.OpeningQty;
                                            sdata1.DeadQty = stockdata1.DeadQty;
                                            sdata1.PurchaseQty = Convert.ToDecimal(pqty);
                                            sdata1.SaleQty = Convert.ToDecimal(newsqty);
                                            sdata1.RemQty = Convert.ToDecimal(newrqty);
                                            sdata1.SaleReturnQty = stockdata1.SaleReturnQty;
                                            sdata1.PurReturnQty = stockdata1.PurReturnQty;

                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                                command.CommandType = CommandType.StoredProcedure;

                                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = BPname;
                                                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = BCname;
                                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = BUname;
                                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;

                                                SqlDataReader reader = command.ExecuteReader();
                                                DataTable dtRate = new DataTable();
                                                dtRate.Load(reader);
                                                rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                                dtRate.Dispose();
                                                db.CloseConnection();
                                            }
                                            catch (Exception)
                                            { db.CloseConnection(); }

                                            sdata1.ProductRate = Convert.ToDecimal(rate);
                                            sdata1.CompId = CommonMethod.CompId;
                                            sdata1.TransactionYear = CommonMethod.TransactionYear;
                                            sdata.Id = updateStockId;
                                            stockRepo1.Edit(sdata1);
                                            stockRepo1.Save();
                                        }
                                        stockRepo1.Dispose();
                                    }
                                }
                            }
                            db.connect();
                            DataTable dt1 = db.GetTable("delete from RepackDetail where RepackNo='" + repackId + "'");
                            DataTable dt2 = db.GetTable("delete from RepackMaster Where ID='" + repackId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                            MessageBox.Show("Repack " + repackId + " is delete successfully.", "Success");
                            dt1.Dispose();
                            dt2.Dispose();
                            db.CloseConnection();
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}