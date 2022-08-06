using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmProductInMarathi : Form
    {
        DbClass db = new DbClass();
        private int maxPoID = 0;

        public frmProductInMarathi()
        {
            InitializeComponent();
        }

        private void frmProductInMarathi_Load(object sender, EventArgs e)
        {
            fillProducts();
        }

        private void fillProducts()
        {
            try
            {
                Cursor cur_sors = Cursors.WaitCursor;
                this.Cursor = cur_sors;
                dtgProductInfo.Rows.Clear();
                DataTable dtProduct = new DataTable();
                if (CommonMethod.commProduct == true)
                    dtProduct = db.GetTable("Select * from ProductInformation");
                else
                    dtProduct = db.GetTable("Select * from ProductInformation where CompId='" + CommonMethod.CompId + "'");
                if (dtProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProduct.Rows.Count; i++)
                    {
                        dtgProductInfo.Rows.Add();
                        dtgProductInfo.Rows[i].Cells["SrNo"].Value = i + 1;
                        dtgProductInfo.Rows[i].Cells["EnglishProduct"].Value = dtProduct.Rows[i][2].ToString();
                        dtgProductInfo.Rows[i].Cells["MarathiProduct"].Value = dtProduct.Rows[i][27].ToString();
                        dtgProductInfo.Rows[i].Cells["ProductId"].Value = dtProduct.Rows[i][0].ToString();
                        dtgProductInfo.Rows[i].Cells["Unit"].Value = dtProduct.Rows[i][3].ToString();

                        string productdesc = "", unitdesc = "", compdesc = "";
                        productdesc = dtProduct.Rows[i][2].ToString();
                        productdesc = productdesc.Replace("'", " ");
                        unitdesc = dtProduct.Rows[i][3].ToString();
                        compdesc = dtProduct.Rows[i][5].ToString();
                        DataTable dtStockQty = db.GetTable("Select d.Quantity,d.productname,d.id from PurchaseMaster m join PurchaseDetail d on m.Id=d.PorderNo where m.Suppliername='First stock' and d.productName='" + productdesc + "' and d.CompanyName='" + compdesc + "' and d.Unit='" + unitdesc + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "");
                        if (dtStockQty.Rows.Count > 0)
                        {
                            string AltUnitQty = dtProduct.Rows[i][15].ToString();
                            decimal calculateQty = 0, finalQty = 0;
                            if (AltUnitQty != null && AltUnitQty != "")
                                calculateQty = Convert.ToDecimal(dtProduct.Rows[i][16].ToString()) / Convert.ToDecimal(dtProduct.Rows[i][17].ToString());
                            if (dtStockQty.Rows[0][0].ToString() != null && dtStockQty.Rows[0][0].ToString() != "" && calculateQty != 0)
                                finalQty = Convert.ToDecimal(dtStockQty.Rows[0][0].ToString()) / calculateQty;
                            string stockqty = dtStockQty.Rows[0][0].ToString();
                            if (finalQty == 0)
                                dtgProductInfo.Rows[i].Cells["OStock"].Value = stockqty;
                            else
                                dtgProductInfo.Rows[i].Cells["OStock"].Value = finalQty;
                            dtgProductInfo.Rows[i].Cells["UStock"].Value = "Exist";
                            dtgProductInfo.Rows[i].Cells["DtlId"].Value = dtStockQty.Rows[0][2].ToString();  //detail id
                        }
                        else
                        {
                            dtgProductInfo.Rows[i].Cells["OStock"].Value = 0;
                            dtgProductInfo.Rows[i].Cells["DtlId"].Value = 0;
                            dtgProductInfo.Rows[i].Cells["UStock"].Value = "False";
                        }
                        dtgProductInfo.Rows[i].Cells["Updated"].Value = "False";
                        if ((i % 2) == 0)
                        {
                            dtgProductInfo.Rows[i].Cells["SrNo"].Style = new DataGridViewCellStyle { BackColor = Color.PowderBlue };
                            dtgProductInfo.Rows[i].Cells["EnglishProduct"].Style = new DataGridViewCellStyle { BackColor = Color.PowderBlue };
                            dtgProductInfo.Rows[i].Cells["MarathiProduct"].Style = new DataGridViewCellStyle { ForeColor = Color.Brown, BackColor = Color.PowderBlue };
                            dtgProductInfo.Rows[i].Cells["ProductId"].Style = new DataGridViewCellStyle { BackColor = Color.PowderBlue };
                            dtgProductInfo.Rows[i].Cells["Unit"].Style = new DataGridViewCellStyle { BackColor = Color.PowderBlue };
                            dtgProductInfo.Rows[i].Cells["OStock"].Style = new DataGridViewCellStyle { BackColor = Color.PowderBlue };
                        }
                    }
                    Cursor cur_sors1 = Cursors.Arrow;
                    this.Cursor = cur_sors1;
                }
            }
            catch (Exception)
            { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SaveUpdate();
        }

        private void getMaxID()
        {
            try
            {
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var mid = pmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.PorderNo);
                if (mid == null)
                    maxPoID = 1;
                else
                    maxPoID = Convert.ToInt32(mid) + 1;
                pmasterrepo.Dispose();
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
            if (keyData == Keys.F3)
            {
                SaveUpdate();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtgProductInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int currentrow = e.RowIndex;
            if (e.ColumnIndex == 6)
            {
                if (dtgProductInfo.Rows[currentrow].Cells["UStock"].Value.ToString() == "Exist")
                    dtgProductInfo.Rows[currentrow].Cells["UStock"].Value = "True_Exist";
                else
                    dtgProductInfo.Rows[currentrow].Cells["UStock"].Value = "True";
            }
            else if (e.ColumnIndex == 2)
                dtgProductInfo.Rows[currentrow].Cells["Updated"].Value = "True";
        }

        public void SaveUpdate()
        {
            int cnt = 0;
            int POID = 0;
            try
            {
                string isUpdated = "", compName = "", unitdesc = "", altunitdesc = "", updatedStock = "False";
                int productId = 0;
                getMaxID();

                for (int j = 0; j < dtgProductInfo.Rows.Count - 1; j++)
                {
                    cnt = j;
                    isUpdated = dtgProductInfo.Rows[j].Cells["Updated"].Value.ToString();
                    productId = Convert.ToInt32(dtgProductInfo.Rows[j].Cells["ProductId"].Value);
                    if (dtgProductInfo.Rows[j].Cells["UStock"].Value.ToString() == "True_Exist")
                        updatedStock = "True_Exist";  //already exist in purchase
                    else if (dtgProductInfo.Rows[j].Cells["UStock"].Value.ToString() == "True")
                        updatedStock = "True";  //if not exist and updated
                    else
                        updatedStock = "False";  // null value

                    ProductRepository prodRepo = new ProductRepository();
                    ProductInformation prodinfo = prodRepo.GetById(productId);
                    if (isUpdated == "True")
                    {
                        if (prodinfo != null)
                        {
                            prodinfo.MarathiName = dtgProductInfo.Rows[j].Cells["MarathiProduct"].Value.ToString();
                            compName = prodinfo.ManufactureCompany;
                            unitdesc = prodinfo.Uniti;
                            altunitdesc = prodinfo.AlternateUnit;
                            prodRepo.Edit(prodinfo);
                            prodRepo.Save();
                        }
                    }
                    else
                    {
                        if (prodinfo != null)
                        {
                            compName = prodinfo.ManufactureCompany;
                            unitdesc = prodinfo.Uniti;
                            altunitdesc = ((prodinfo.AlternateUnit == null || prodinfo.AlternateUnit == "") ? prodinfo.Uniti : prodinfo.AlternateUnit);
                        }
                    }

                    if (updatedStock == "True" || updatedStock == "True_Exist")  //only modified records
                    {
                        if (updatedStock == "True") // onlye if not exist in purchase and update stock quantity
                        {
                            PurchaseMasterRepository pmasterRepo = new PurchaseMasterRepository();
                            PurchaseMaster pmaster = new PurchaseMaster();
                            try
                            {
                                pmaster.PorderNo = Convert.ToInt32(maxPoID);
                                pmaster.PorderDate = DateTime.Now.Date;
                                pmaster.Suppliername = "First stock";
                                pmaster.Vat = 0;
                                pmaster.TransactionYear = CommonMethod.TransactionYear;
                                pmaster.CompId = CommonMethod.CompId;
                                pmaster.StockIn = 0;
                                pmasterRepo.Add(pmaster);
                                pmasterRepo.Save();
                                POID = pmaster.Id;
                            }
                            catch (Exception)
                            { }
                            pmasterRepo.Dispose();
                        }
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            if (updatedStock == "True_Exist")
                            {
                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'A';
                                command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(dtgProductInfo.Rows[j].Cells["DtlId"].Value);
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = 0;
                            }
                            else
                            {
                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = POID;
                            }
                            command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                            command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = dtgProductInfo.Rows[j].Cells["EnglishProduct"].Value.ToString();
                            command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = compName;
                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unitdesc;
                            command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = (altunitdesc == null ? unitdesc : altunitdesc);
                            command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgProductInfo.Rows[j].Cells["OStock"].Value);
                            command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                            command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "";
                            command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                            command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(0);
                            command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = "N";
                            command.ExecuteNonQuery();
                            db.CloseConnection();
                            command.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    prodRepo.Dispose();
                }
                db.CloseConnection();
                MessageBox.Show("Products updated successfully.");
                fillProducts();
            }
            catch (Exception)
            {
                MessageBox.Show("Error on updating product " + cnt);
                db.CloseConnection();
            }
        }

        private void btnUpdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnCancel;
            }
            catch (Exception) { }
        }
    }
}
