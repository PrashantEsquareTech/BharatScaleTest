using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBarcodeChoice : Form
    {
        DbClass db = new DbClass();
        public static string glcolorname, glsize;
        public static decimal BarcodePrice = 0;
        string pname, cname, uname, formname, barcode, barcodestatus, updateBarcode = "";
        decimal quantity = 0, remqty = 0;
        public int barcodefound = -1;

        public frmBarcodeChoice()
        {
            InitializeComponent();
        }

        public frmBarcodeChoice(string proname, string Company, string unit, string FormName, string BarcodeStatus, string UpdateBarcode = "", decimal qty = 0)
        {
            InitializeComponent();
            pname = proname;
            cname = Company;
            uname = unit;
            formname = FormName;
            barcodestatus = BarcodeStatus;
            quantity = qty;
            updateBarcode = UpdateBarcode;
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                ProductRepository prepo = new ProductRepository();
                ProductInformation pdata = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    pdata = prepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Uniti == uname).FirstOrDefault();
                else
                    pdata = prepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Uniti == uname && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (pdata != null)
                {
                    DataTable dtstock = new DataTable();
                    DataTable dtcolrstock = new DataTable();
                    SqlDataAdapter da;
                    DataTable dtPrQty, dtSrQty,dtDQty;
                    string psize = "", pcolor = "";
                    decimal prqty = 0, srqty = 0, pBarcodePrice = 0,dqty=0;
                    if (barcodestatus == "Our Barcode")
                    {
                        if (pdata.RepackStatus == "Repackage")
                            da = new SqlDataAdapter("select D.ProductName,D.CompanyName,D.Unit,D.Barcode,(IsNull(D.Quantity,0) +Isnull(D.FreeQty,0)) as 'PurchaseQty',0 as Remqty,D.SaleRate as SaleRate,M.Suppliername as SuppName from PurchaseDetail as D inner join PurchaseMaster as M on M.Id=D.PorderNo where D.ProductName='" + pname + "' and D.CompanyName='" + cname + "' and D.unit='" + uname + "'  and M.CompId='" + CommonMethod.CompId + "'  UNION ALL select pm.ProductName,pm.ManufactureCompany,pm.Uniti as 'Unit',RD.Barcode, (Isnull(RD.Qty,0)) as Qty,0 as remqty,RD.SaleRate as Salerate,'Repack Product' as SuppName from RepackMaster RM inner join RepackDetail RD on RD.RepackNo=RM.ID inner join ProductInformation as pm on pm.Id=RD.ProductNo where RD.ProductNo='" + pdata.ID + "'  and RM.CompId='" + CommonMethod.CompId + "' ", db.Connection);
                        else                                 // 0             1           2        3       4                                                        5               6          7                       8                        9
                            da = new SqlDataAdapter("select D.ProductName,D.CompanyName,D.Unit,D.Size,D.Barcode,(IsNull(D.Quantity,0) +Isnull(D.FreeQty,0)) as 'PurchaseQty',0 as Remqty,D.SaleRate as SaleRate,D.ColorName,M.Suppliername as SuppName from PurchaseDetail as D inner join PurchaseMaster as M on M.Id=D.PorderNo where D.ProductName='" + pname + "' and D.CompanyName='" + cname + "' and D.unit='" + uname + "'  and M.CompId='" + CommonMethod.CompId + "'  order by D.ProductName", db.Connection);
                    }
                    else if (barcodestatus == "Static Barcode")
                        da = new SqlDataAdapter("select Distinct D.ProductName,D.CompanyName,D.Unit,D.Size,D.StaticBarCode as Barcode,sum(IsNull(D.Quantity,0) +Isnull(D.FreeQty,0)) as 'PurchaseQty',0 as Remqty,D.SaleRate as SaleRate,D.ColorName  from PurchaseDetail as D inner join PurchaseMaster as M on M.Id=D.PorderNo where D.ProductName='" + pname + "' and D.CompanyName='" + cname + "' and D.unit='" + uname + "'  and M.CompId='" + CommonMethod.CompId + "'  group by D.ProductName,D.CompanyName,D.Unit,D.Size,D.StaticBarcode,D.SaleRate,D.ColorName order by D.ProductName", db.Connection);
                    else
                        da = new SqlDataAdapter("select Distinct D.ProductName,D.CompanyName,D.Unit,D.Size,D.StaticBarCode as Barcode,sum(IsNull(D.Quantity,0) +Isnull(D.FreeQty,0)) as 'PurchaseQty',0 as Remqty,D.SaleRate as SaleRate,D.ColorName  from PurchaseDetail as D inner join PurchaseMaster as M on M.Id=D.PorderNo where D.ProductName='" + pname + "' and D.CompanyName='" + cname + "' and D.unit='" + uname + "'  and M.CompId='" + CommonMethod.CompId + "' group by D.ProductName,D.CompanyName,D.Unit,D.Size,D.StaticBarcode,D.SaleRate,D.ColorName order by D.ProductName", db.Connection);
                                       
                    DataTable dtDetail = new DataTable();
                    da.Fill(dtDetail);

                    if (dtDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtDetail.Rows.Count - 1; i++)
                        {
                            try
                            {
                                try
                                {
                                    if (dtDetail.Rows[i]["Size"] == null || Convert.ToString(dtDetail.Rows[i]["Size"]) == "")
                                        psize = "";
                                    else
                                        psize = Convert.ToString(dtDetail.Rows[i]["Size"]);
                                }
                                catch (Exception)
                                { }

                                try
                                {
                                    if (dtDetail.Rows[i]["ColorName"] == null || Convert.ToString(dtDetail.Rows[i]["ColorName"]) == "")
                                        pcolor = "";
                                    else
                                        pcolor = Convert.ToString(dtDetail.Rows[i]["ColorName"]);
                                }
                                catch (Exception)
                                { }

                                try
                                {
                                    if (dtDetail.Rows[i]["SaleRate"] == null || Convert.ToString(dtDetail.Rows[i]["SaleRate"]) == "")
                                        pBarcodePrice = 0;
                                    else
                                        pBarcodePrice = Convert.ToDecimal(dtDetail.Rows[i]["SaleRate"].ToString());
                                }
                                catch (Exception)
                                { }                                

                                if (barcodestatus == "Our Barcode")
                                    dtstock = db.GetTable("select isnull(sum(D.Quantity),0) as Qty from CustomerBillDetail as d inner join CustomerBillMaster as m  on m.id=d.BillNo where d.Barcode='" + dtDetail.Rows[i]["Barcode"] + "' and d.Size='" + psize + "'  and d.ProductId='" + pname + "' and d.CompanyName='" + cname + "' and d.unit='" + uname + "' and m.CompId='" + CommonMethod.CompId + "'  UNION ALL select isnull(sum(RD.BulkQty),0) as Qty from RepackMaster RM inner join RepackDetail RD on RD.RepackNo=RM.ID where RD.BulkProductNo='" + pdata.ID + "'  and RM.CompId=" + CommonMethod.CompId + " and RM.TransactionYear='" + CommonMethod.TransactionYear + "'");
                                else
                                    dtstock = db.GetTable("select isnull(sum(D.Quantity),0) as Qty from CustomerBillDetail as d inner join CustomerBillMaster as m  on m.id=d.BillNo where d.Barcode='" + dtDetail.Rows[i]["Barcode"] + "'  and d.Size='" + psize + "'   and d.ProductId='" + pname + "' and d.CompanyName='" + cname + "' and d.unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " ");
                                dtDQty = db.GetTable("select isnull(sum(pd.Quantity),0) as Qty from DeadProductDetail as pd inner join DeadProductMaster as pm on pm.Id=pd.DeadNo where pd.Barcode='" + dtDetail.Rows[i]["Barcode"] + "'  and pd.Size='" + psize + "'   and pd.ProductName='" + pname + "' and pd.CompanyName='" + cname + "' and pd.unit='" + uname + "' and pm.CompId=" + CommonMethod.CompId + " ");
                                if (dtDQty.Rows.Count > 0)
                                    dqty = Convert.ToDecimal(dtDQty.Rows[0]["Qty"]);
                                else
                                    dqty = 0;

                                dtPrQty = db.GetTable("select isnull(sum(pd.Quantity),0) as Qty from PurchaseReturnDetail as pd inner join PurchaseReturnMaster as pm on pm.Id=pd.PurchaseReturnNo where pd.Barcode='" + dtDetail.Rows[i]["Barcode"] + "'  and pd.Size='" + psize + "'   and pd.ProductName='" + pname + "' and pd.CompanyName='" + cname + "' and pd.unit='" + uname + "' and pm.CompId=" + CommonMethod.CompId + " ");
                                if (dtPrQty.Rows.Count > 0)
                                    prqty = Convert.ToDecimal(dtPrQty.Rows[0]["Qty"]);
                                else
                                    prqty = 0;
                                dtSrQty = db.GetTable("select isnull(sum(gd.Quantity),0) as Qty from GoodReturnDetail as gd inner join GoodReturnMaster as gm on gm.Id=gd.GoodReturnNo where gd.Barcode='" + dtDetail.Rows[i]["Barcode"] + "'  and gd.Size='" + psize + "'   and gd.ProductId='" + pname + "' and gd.CompanyName='" + cname + "' and gd.Unit='" + uname + "' and gm.CompId='" + CommonMethod.CompId + "' ");
                                if (dtSrQty.Rows.Count > 0)
                                    srqty = Convert.ToDecimal(dtSrQty.Rows[0]["Qty"]);
                                else
                                    srqty = 0;
                                decimal sum = 0;
                                foreach (DataRow dr in dtstock.Rows)
                                {
                                    foreach (DataColumn dc in dtstock.Columns)
                                    {
                                        sum += (decimal)dr[dc];
                                    }
                                }
                                if (dtstock.Rows.Count > 0)
                                    dtDetail.Rows[i]["Remqty"] = Convert.ToDecimal(dtDetail.Rows[i]["PurchaseQty"]) - Convert.ToDecimal(sum) - dqty + srqty - prqty;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error on getting details", "Warning");
                                break;
                            }
                        }
                        foreach (DataRow dr in dtDetail.Rows)
                        {
                            if (dr["Remqty"].ToString() == "0")
                                dr.Delete();
                        }
                    }
                    dtgvBarcode.DataSource = dtDetail;
                    dtgvBarcode.CurrentCell = dtgvBarcode.Rows[0].Cells[0];
                    dtgvBarcode.CurrentCell.Selected = true;
                }
            }
            catch (Exception)
            { }

            if (updateBarcode != "")
            {
                try
                {
                    if (dtgvBarcode.RowCount > 0)
                    {
                        for (int i = 0; i < dtgvBarcode.RowCount; i++)
                        {
                            if (dtgvBarcode.Rows[i].Cells["Barcode"].Value.ToString() == updateBarcode)
                            {
                                barcodefound = 1;
                                barcode = updateBarcode;
                                remqty = Convert.ToDecimal(dtgvBarcode.Rows[i].Cells["Remqty"].Value.ToString());
                                break;
                            }
                            else
                                barcodefound = 0;
                        }
                    }
                    else
                        barcodefound = 0;
                    if (barcodefound == 0)
                    {
                        MessageBox.Show("Quantity for this barcode is not available please select another barcode");
                        barcode = updateBarcode;
                        remqty = 0;
                    }
                }
                catch { }
            }
        }

        private void dtgvBarcode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvBarcode.Columns[e.ColumnIndex].HeaderText == "")
                {
                    try
                    {
                        glcolorname = dtgvBarcode.Rows[e.RowIndex].Cells["ColorName"].Value.ToString();
                    }
                    catch (Exception)
                    { glcolorname = ""; }
                    try
                    {
                        glsize = dtgvBarcode.Rows[e.RowIndex].Cells["Size"].Value.ToString();
                    }
                    catch (Exception)
                    { glsize = ""; }
                    
                    BarcodePrice = Convert.ToDecimal(dtgvBarcode.Rows[e.RowIndex].Cells["SaleRate"].Value.ToString());
                    remqty = Convert.ToDecimal(dtgvBarcode.Rows[e.RowIndex].Cells["Remqty"].Value);
                    barcode = dtgvBarcode.Rows[e.RowIndex].Cells["Barcode"].Value.ToString();
                    if (barcode != "")
                        MessageBox.Show("You have selected item of barcode " + barcode, "Success");
                    else if (glsize != "")
                        MessageBox.Show("You have selected item of Size " + glsize, "Success");
                    this.Close();

                    if (formname == "frmBill")
                    {
                        if (frmCustomerBill.custbillinfo.btnUpdate.Enabled == true && frmCustomerBill.custbillinfo.colIndex != -1)
                        {
                            if (quantity >= 0)
                            {
                                if (glsize != "")
                                    frmCustomerBill.custbillinfo.frmBarcodeChoice(barcode, remqty, glsize, glcolorname, BarcodePrice, remqty - quantity);
                                else
                                    frmCustomerBill.custbillinfo.frmBarcodeChoice(barcode, remqty, remqty - quantity);
                            }
                        }
                        else
                        {
                            if (glsize != "")
                                frmCustomerBill.custbillinfo.frmBarcodeChoice(barcode, remqty, glsize, glcolorname, BarcodePrice);
                            else
                                frmCustomerBill.custbillinfo.frmBarcodeChoice(barcode, remqty);
                        }
                    }
                    if (formname == "frmDead")
                    {
                        if (frmDeadStock.deadstockinfo.btnupdate.Enabled == true && frmDeadStock.deadstockinfo.colindex != -1)
                        {
                            if (quantity >= 0)
                            {
                                if (glsize != "")
                                    frmDeadStock.deadstockinfo.frmBarcodeChoice(barcode, remqty, glsize, glcolorname, BarcodePrice, remqty - quantity);
                                else
                                    frmDeadStock.deadstockinfo.frmBarcodeChoice(barcode, remqty, remqty - quantity);
                            }
                        }
                        else
                        {
                            if (glsize != "")
                                frmDeadStock.deadstockinfo.frmBarcodeChoice(barcode, remqty, glsize, glcolorname, BarcodePrice);
                            else
                                frmDeadStock.deadstockinfo.frmBarcodeChoice(barcode, remqty);
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                if (formname == "frmBill")
                {
                    if (glsize != "")
                        frmCustomerBill.custbillinfo.frmBarcodeChoice(barcode, remqty, glsize, glcolorname, 0, (remqty - quantity));
                    else
                        frmCustomerBill.custbillinfo.frmBarcodeChoice(barcode, remqty, remqty - quantity);
                }
                if (formname == "frmDead")
                {
                    if (glsize != "")
                        frmDeadStock.deadstockinfo.frmBarcodeChoice(barcode, remqty, glsize, glcolorname, 0, (remqty - quantity));
                    else
                        frmDeadStock.deadstockinfo.frmBarcodeChoice(barcode, remqty, remqty - quantity);
                }
            }
            catch (Exception)
            { }
        }
    }
}