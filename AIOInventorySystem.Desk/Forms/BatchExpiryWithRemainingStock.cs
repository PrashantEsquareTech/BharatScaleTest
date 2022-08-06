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
    public partial class BatchExpiryWithRemainingStock : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        //first push
        //Secound Push From colabrator
        // third push test
        // fourst push
        // fifth push
        public BatchExpiryWithRemainingStock()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
        }

        private void BatchExpiryWithRemainingStock_Load(object sender, EventArgs e)
        {
            try
            {
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
                    txtProductName1.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                }
            }
            catch (Exception) { }
        }

        private void txtProductName1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtProductName1.Text.Trim() != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtProductName1.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtProductName1.Text.Trim().Split(',')[0];
                            company = txtProductName1.Text.Trim().Split(',')[1];
                            unit = txtProductName1.Text.Trim().Split(',')[2];
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
                            txtProductName1.Text = "";
                            this.ActiveControl = txtProductName1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtProductName1.Text = "";
                        this.ActiveControl = txtProductName1;
                    }
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

        private void chkProductName1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProductName1;
            }
            catch (Exception) { }
        }

        private void txtProductName1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearchbtn;
            }
            catch (Exception) { }
        }

        private void ChkDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkDate.Checked == true)
                    chkExpireDate.Checked = false;
            }
            catch (Exception) { }
        }

        private void ChkDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtFromdate1;
            }
            catch (Exception) { }
        }

        private void dtFromdate1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpTodate1;
            }
            catch (Exception) { }
        }

        private void dtpTodate1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearchbtn;
            }
            catch (Exception) { }
        }

        private void chkExpireDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkExpireDate.Checked == true)
                    ChkDate.Checked = false;
            }
            catch (Exception) { }
        }

        private void dtpExpireDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearchbtn;
            }
            catch (Exception) { }
        }

        private void btnSearchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string ExpireDate1 = "";
                DateTime ExpireDate;
                decimal TotalRemQty = 0;
                int j = 0;
                DataTable dtPurchase = null, dtBatch = null;
                dtgvBatchNExpiryStock1.Rows.Clear();
                if (chkProductName1.Checked == true)
                {
                    if (txtProductName1.Text != "")
                    {
                        string Proname1 = txtProductName1.Text.Trim().Split(',')[0];
                        string company = txtProductName1.Text.Trim().Split(',')[1];
                        string unit = txtProductName1.Text.Trim().Split(',')[2];
                        string Proname = "";
                        if ((Proname1).Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        if (ChkDate.Checked == true)
                        {
                            if (dtFromdate1.Value.Date <= dtpTodate1.Value.Date)
                            {
                                string fromDate = Convert.ToString(cm.ValidFromDate(dtFromdate1.Value.Date));
                                string ToDate = Convert.ToString(cm.ValidToDate(dtpTodate1.Value.Date));
                                //                                          0           1               2         3                         4            5           6
                                dtPurchase = db.GetTable("SELECT distinct pd.Id,pd.ProductName,pd.CompanyName,pd.Unit,pd.Quantity as 'Purchase qty',pd.BatchNo,pd.ExpireDate from PurchaseDetail as pd inner join ProductInformation as p on pd.ProductName=p.ProductName inner join PurchaseMaster as pm on pm.Id=pd.PorderNo where pd.ProductName='" + Proname + "' and pd.CompanyName='" + company + "'and pd.Unit='" + unit + "' and pd.ExpireDate>='" + fromDate + "' and pd.ExpireDate<='" + ToDate + "'  and (p.WithBatch='Y' or p.WithBatchAndExpiry='Y') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' order by pd.ExpireDate asc");
                                label7.Text = "Total Remaining Qty:";
                            }
                            else
                            {
                                MessageBox.Show("From Date Should be greater than To Date.", "Warning");
                                this.ActiveControl = txtProductName1;
                            }
                        }
                        else if (chkExpireDate.Checked == true)
                        {
                            string fromDate = Convert.ToString(cm.ValidToDate(dtpExpireDate.Value.Date));
                            //                                          0           1               2         3                         4            5           6
                            dtPurchase = db.GetTable("SELECT distinct pd.Id,pd.ProductName,pd.CompanyName,pd.Unit,pd.Quantity as 'Purchase qty',pd.BatchNo,pd.ExpireDate from PurchaseDetail as pd inner join ProductInformation as p on pd.ProductName=p.ProductName inner join PurchaseMaster as pm on pm.Id=pd.PorderNo where pd.ProductName='" + Proname + "' and pd.CompanyName='" + company + "'and pd.Unit='" + unit + "' and pd.ExpireDate <= '" + fromDate + "' and (p.WithBatch='Y' or p.WithBatchAndExpiry='Y') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' order by pd.ExpireDate asc");
                            label7.Text = "Total Expired Qty:";
                        }
                        else
                        {
                            //                                          0           1               2         3                         4            5           6
                            dtPurchase = db.GetTable("SELECT distinct pd.Id,pd.ProductName,pd.CompanyName,pd.Unit,pd.Quantity as 'Purchase qty',pd.BatchNo,pd.ExpireDate from PurchaseDetail as pd inner join ProductInformation as p on pd.ProductName=p.ProductName inner join PurchaseMaster as pm on pm.Id=pd.PorderNo where pd.ProductName='" + Proname + "' and pd.CompanyName='" + company + "'and pd.Unit='" + unit + "' and (p.WithBatch='Y' or p.WithBatchAndExpiry='Y') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' order by pd.ExpireDate asc");
                            label7.Text = "Total Remaining Qty:";
                        }
                    }
                }
                else if (ChkDate.Checked == true)
                {
                    if (dtFromdate1.Value.Date <= dtpTodate1.Value.Date)
                    {
                        string fromDate = Convert.ToString(cm.ValidFromDate(dtFromdate1.Value.Date));
                        string ToDate = Convert.ToString(cm.ValidToDate(dtpTodate1.Value.Date));
                        //                                          0           1               2         3                         4            5           6
                        dtPurchase = db.GetTable("SELECT distinct pd.Id,pd.ProductName,pd.CompanyName,pd.Unit,pd.Quantity as 'Purchase qty',pd.BatchNo,pd.ExpireDate from PurchaseDetail as pd inner join ProductInformation as p on pd.ProductName=p.ProductName where pd.ExpireDate>='" + fromDate + "' and pd.ExpireDate<='" + ToDate + "'  and (p.WithBatch='Y' or p.WithBatchAndExpiry='Y') and p.compid=" + CommonMethod.CompId + " order by pd.ExpireDate asc");
                        label7.Text = "Total Remaining Qty:";
                    }
                    else
                    {
                        MessageBox.Show("From Date Should be greater than To Date.", "Warning");
                        this.ActiveControl = txtProductName1;
                    }
                }
                else if (chkExpireDate.Checked == true)
                {
                    string fromDate = Convert.ToString(cm.ValidToDate(dtpExpireDate.Value.Date));
                    //                                           0          1              2           3                        4              5           6
                    dtPurchase = db.GetTable("SELECT distinct pd.Id,pd.ProductName,pd.CompanyName,pd.Unit,pd.Quantity as 'Purchase qty',pd.BatchNo,pd.ExpireDate from PurchaseDetail as pd inner join ProductInformation as p on pd.ProductName=p.ProductName inner join PurchaseMaster as pm on pm.Id=pd.PorderNo where pd.ExpireDate <= '" + fromDate + "' and (p.WithBatch='Y' or p.WithBatchAndExpiry='Y') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and p.compid=" + CommonMethod.CompId + " order by pd.ExpireDate asc");
                    label7.Text = "Total Expired Qty:";
                }
                if (dtPurchase.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPurchase.Rows.Count; i++)
                    {
                        decimal UsedQty = 0, RemQty = 0;
                        ExpireDate = Convert.ToDateTime(dtPurchase.Rows[i]["ExpireDate"].ToString());
                        ExpireDate1 = Convert.ToString(cm.ValidFromDate(ExpireDate.Date));
                        dtBatch = db.GetTable("select IsNull(Sum(Isnull(UsedQty,0)),0) as UQty from Batch where ProductName='" + dtPurchase.Rows[i][1].ToString() + "' and BatchNo='" + dtPurchase.Rows[i][5].ToString() + "' and ExpiryDate='" + ExpireDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtBatch.Rows.Count != 0)
                            UsedQty = Convert.ToDecimal(dtBatch.Rows[0]["UQty"].ToString());
                        else
                            UsedQty = 0;
                        RemQty = Convert.ToDecimal(dtPurchase.Rows[i]["Purchase qty"].ToString()) - UsedQty;
                        TotalRemQty = TotalRemQty + RemQty;
                        dtgvBatchNExpiryStock1.Columns[7].HeaderText = "Expired Qty";
                        dtgvBatchNExpiryStock1.Rows.Add();
                        dtgvBatchNExpiryStock1.Rows[j].Cells["ProductNameg"].Value = dtPurchase.Rows[i]["ProductName"].ToString();
                        dtgvBatchNExpiryStock1.Rows[j].Cells["CompName"].Value = dtPurchase.Rows[i]["CompanyName"].ToString();
                        dtgvBatchNExpiryStock1.Rows[j].Cells["Unit"].Value = dtPurchase.Rows[i]["Unit"].ToString();
                        dtgvBatchNExpiryStock1.Rows[j].Cells["BatchNo"].Value = dtPurchase.Rows[i]["BatchNo"].ToString();
                        try
                        {
                            dtgvBatchNExpiryStock1.Rows[j].Cells["ExpireDate"].Value = ExpireDate.ToString("dd/MM/yyyy");
                        }
                        catch (Exception)
                        { }                        
                        dtgvBatchNExpiryStock1.Rows[j].Cells["PQty"].Value = dtPurchase.Rows[i]["Purchase qty"].ToString();
                        dtgvBatchNExpiryStock1.Rows[j].Cells["UsedQty"].Value = UsedQty;
                        dtgvBatchNExpiryStock1.Rows[j].Cells["RemQty"].Value = RemQty;
                        j++;
                    }
                }
                if (dtgvBatchNExpiryStock1.Rows.Count == 0)
                    MessageBox.Show("Record not found");
                label8.Text = Convert.ToString(TotalRemQty);
            }
            catch (Exception) { }
        }
    }
}