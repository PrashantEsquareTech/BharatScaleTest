using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmshowdetailofstock : Form
    {
        DbClass db = new DbClass();

        public frmshowdetailofstock()
        {
            InitializeComponent();
        }

        public frmshowdetailofstock(string pname, string cname, string size, int compid)
        {
            InitializeComponent();
            loaddata(pname, cname, size, compid);
        }

        public frmshowdetailofstock(string pname, string cname, string size, string vat, int compid)
        {
            InitializeComponent();
            if (vat == "y")
                vatstock(pname, cname, size, vat, compid);
            else
                withoutvatstock(pname, cname, size, vat, compid);
        }

        public void loaddata(string pname, string cname, string size, int compid)
        {
            try
            {
                string Proname = "";
                if ((pname).Contains("'"))
                    Proname = pname.Replace("'", "''");
                else
                    Proname = pname;
                try
                {
                    //PurchaseDetail 
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PorderDate,103) as 'PO Date',PurchaseMaster.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur. Price'  FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and PurchaseMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and PurchaseMaster.CompId='" + CommonMethod.CompId + "' and (PurchaseMaster.StockIn=" + compid + " or PurchaseMaster.StockIn is null)", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PorderDate,103) as 'PO Date',PurchaseMaster.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur. Price' FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and PurchaseMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and PurchaseMaster.CompId='" + CommonMethod.CompId + "' and PurchaseMaster.StockIn=" + compid + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvPorderInfo.DataSource = dt;
                    GvPorderInfo.Refresh();
                    GvPorderInfo.Columns[0].Width = 65;
                    GvPorderInfo.Columns[1].Width = 55;
                    GvPorderInfo.Columns[2].Width = 50;
                    GvPorderInfo.Columns[3].Width = 50;
                    GvPorderInfo.Columns[4].Width = 60;

                    decimal totalpqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", "")) + Convert.ToDecimal(dt.Compute("Sum(FrQty)", ""));
                    if (dt.Rows.Count > 0)
                        lbltotalpqty.Text = Convert.ToString(totalpqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //PurchaseRepack
                    if (compid == 0)
                    {
                        ProductRepository prepo = new ProductRepository();
                        ProductInformation pdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            pdata = prepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == cname && t.Uniti == size).FirstOrDefault();
                        else
                            pdata = prepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == cname && t.Uniti == size && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (pdata != null)
                        {
                            SqlDataAdapter da = new SqlDataAdapter();
                            da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),RepackDate,103) as 'Repack Date',RepackMaster.RepackNo as 'Repack No',Qty as 'Qty',SaleRate as 'Salerate'  FROM [RepackMaster] inner join RepackDetail on RepackMaster.Id=RepackDetail.RepackNo where ProductNo='" + pdata.ID + "'  and RepackMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and RepackMaster.CompId='" + CommonMethod.CompId + "' ", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            decimal totalprqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", ""));
                            if (dt.Rows.Count > 0)
                                lblPurchaseRepackQty.Text = Convert.ToString(totalprqty);
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                }
                catch (Exception)
                { }
                try
                {
                    //Transfer To
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),TransferDate,103) as 'Transfer Date',Isnull(Quantity,0) as Quantity from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "' as varchar(10)) as varchar(15)) ", db.Connection);
                    else
                        da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),TransferDate,103) as 'Transfer Date',Isnull(Quantity,0) as Quantity from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + compid + "' as varchar(10)) as varchar(15)) ", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GVTransferToStock.DataSource = dt;
                    GVTransferToStock.Refresh();
                    GVTransferToStock.Columns[0].Width = 70;
                    GVTransferToStock.Columns[1].Width = 60;
                    decimal totalttqty = Convert.ToDecimal(dt.Compute("Sum(Quantity)", "")) + Convert.ToDecimal(lbltotalpqty.Text);
                    if (dt.Rows.Count > 0)
                        lbltotalpqty.Text = Convert.ToString(totalttqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //PurchaseReturnDetail 
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PurchaseReturnDate,103) as 'PR.Date',m.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur. Price'  FROM PurchaseReturnMaster m	inner join PurchaseReturnDetail d on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on m.PorderNo =pm.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (pm.StockIn=" + compid + " or pm.StockIn is null)	and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear=m.TransactionYear and pm.CompId=m.CompId", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PurchaseReturnDate,103) as 'PR.Date',m.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur. Price' FROM PurchaseReturnMaster m	inner join PurchaseReturnDetail d on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on m.PorderNo =pm.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and pm.StockIn=" + compid + "	and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear=m.TransactionYear and pm.CompId=m.CompId", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvPRInfo.DataSource = dt;
                    GvPRInfo.Refresh();
                    GvPRInfo.Columns[0].Width = 65;
                    GvPRInfo.Columns[1].Width = 55;
                    GvPRInfo.Columns[2].Width = 50;
                    GvPRInfo.Columns[3].Width = 50;
                    GvPRInfo.Columns[4].Width = 60;
                    decimal totalprtqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", "")) + Convert.ToDecimal(dt.Compute("Sum(FrQty)", ""));
                    if (dt.Rows.Count > 0)
                        lbltprqty.Text = Convert.ToString(totalprtqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //saledetails
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',CustomerBillMaster.BillNo,Quantity as 'Qty',Rate  FROM [CustomerBillMaster] inner join [CustomerBillDetail] on CustomerBillMaster.Id=CustomerBillDetail.BillNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (AssemblyChildStatus!='Y' or AssemblyChildStatus is null or AssemblyChildStatus='') and (CustomerBillMaster.BillNo like 'T%' or CustomerBillMaster.BillNo like 'E%') and CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvBillInfo.DataSource = dt1;
                        GvBillInfo.Refresh();
                        GvBillInfo.Columns[0].Width = 65;
                        GvBillInfo.Columns[1].Width = 55;
                        GvBillInfo.Columns[2].Width = 50;
                        GvBillInfo.Columns[3].Width = 65;
                        if (dt1.Rows.Count > 0)
                            lbltotalsqty.Text = Convert.ToString(dt1.Compute("Sum(Qty)", ""));
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltotalsqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //SaleRepack
                    if (compid == 0)
                    {
                        ProductRepository prepo = new ProductRepository();
                        ProductInformation pdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            pdata = prepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == cname && t.Uniti == size).FirstOrDefault();
                        else
                            pdata = prepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == cname && t.Uniti == size && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (pdata != null)
                        {
                            SqlDataAdapter da = new SqlDataAdapter();
                            da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),RepackDate,103) as 'Repack Date',RepackMaster.RepackNo as 'Repack No',BulkQty as 'Qty',SaleRate as 'Salerate'  FROM [RepackMaster] inner join RepackDetail on RepackMaster.Id=RepackDetail.RepackNo where BulkProductNo='" + pdata.ID + "'  and RepackMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and RepackMaster.CompId='" + CommonMethod.CompId + "' ", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            decimal totalsrqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", ""));
                            if (dt.Rows.Count > 0)
                                lblSaleRepackQty.Text = Convert.ToString(totalsrqty);
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                }
                catch (Exception)
                { }
                try
                {
                    // Changes for Assembly Product 16-12-2019
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',CustomerBillMaster.BillNo,Quantity as 'Qty',Rate  FROM [CustomerBillMaster] inner join [CustomerBillDetail] on CustomerBillMaster.Id=CustomerBillDetail.BillNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and AssemblyChildStatus='Y'  and (CustomerBillMaster.BillNo like 'T%' or CustomerBillMaster.BillNo like 'E%') and CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvTransferFromStock.DataSource = dt1;
                        GvTransferFromStock.Refresh();
                        GvTransferFromStock.Columns[0].Width = 70;
                        GvTransferFromStock.Columns[1].Width = 60;
                        decimal totaltfqty = Convert.ToDecimal(dt1.Compute("Sum(Qty)", "")) + Convert.ToDecimal(lbltotalsqty.Text);
                        if (dt1.Rows.Count > 0)
                            lbltotalsqty.Text = Convert.ToString(totaltfqty);
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltotalsqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //saleReturndetails
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),GoodReturnDate,103) as 'SR Date',BillNo,Quantity as 'Qty',Rate FROM GoodReturnMaster inner join GoodReturnDetail on GoodReturnMaster.Id=GoodReturnDetail.GoodReturnNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (GoodReturnMaster.BillNo like 'T%' or GoodReturnMaster.BillNo like 'E%') and GoodReturnMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and  GoodReturnMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvSRInfo.DataSource = dt1;
                        GvSRInfo.Refresh();
                        GvSRInfo.Columns[0].Width = 65;
                        GvSRInfo.Columns[1].Width = 55;
                        GvSRInfo.Columns[2].Width = 50;
                        GvSRInfo.Columns[3].Width = 60;
                        if (dt1.Rows.Count > 0)
                            lbltsrqty.Text = Convert.ToString(dt1.Compute("Sum(Qty)", ""));
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltsrqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //dead details
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    if (compid == 0)
                        da2 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date',DeadProductMaster.DeadNo,Quantity as 'Qty' FROM [DeadProductMaster] inner join [DeadProductDetail] on DeadProductMaster.Id=DeadProductDetail.DeadNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and DeadProductMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and  DeadProductMaster.CompId='" + CommonMethod.CompId + "'  and  (DeadProductMaster.StockIn=" + compid + " or DeadProductMaster.StockIn is null)", db.Connection);
                    else
                        da2 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date',DeadProductMaster.DeadNo,Quantity as 'Qty' FROM [DeadProductMaster] inner join [DeadProductDetail] on DeadProductMaster.Id=DeadProductDetail.DeadNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and DeadProductMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and  DeadProductMaster.CompId='" + CommonMethod.CompId + "' and  DeadProductMaster.StockIn='" + compid + "'", db.Connection);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    gvdeadinfo.DataSource = dt2;
                    gvdeadinfo.Refresh();
                    gvdeadinfo.Columns[0].Width = 65;
                    gvdeadinfo.Columns[1].Width = 55;
                    gvdeadinfo.Columns[2].Width = 50;
                    if (dt2.Rows.Count > 0)
                        lbltotaldqty.Text = Convert.ToString(dt2.Compute("Sum(Qty)", ""));
                    da2.Dispose();
                    dt2.Dispose();
                }
                catch (Exception)
                { }
                try
                {
                    //Opening Qty
                    if (compid == 0)
                    {
                        SqlDataAdapter da2 = new SqlDataAdapter("SELECT Isnull(Sum(Isnull(OpeningQty,0)),0) FROM [Stock] where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                            lblOpeningQty.Text = Convert.ToString(dt2.Rows[0][0].ToString());
                        da2.Dispose();
                        dt2.Dispose();
                    }
                    else
                        lblOpeningQty.Text = "0";
                }
                catch (Exception)
                { }
                decimal remqty = 0;
                remqty = Convert.ToDecimal(lblOpeningQty.Text) + Convert.ToDecimal(lbltotalpqty.Text) + Convert.ToDecimal(lblPurchaseRepackQty.Text) - Convert.ToDecimal(lbltprqty.Text) - Convert.ToDecimal(lbltotalsqty.Text) - Convert.ToDecimal(lblSaleRepackQty.Text) + Convert.ToDecimal(lbltsrqty.Text) - Convert.ToDecimal(lbltotaldqty.Text);
                lbltotalrqty.Text = Convert.ToString(remqty);
            }
            catch (Exception)
            { }
        }

        public void vatstock(string pname, string cname, string size, string vat, int compid)
        {
            try
            {
                string Proname = "";
                if ((pname).Contains("'"))
                    Proname = pname.Replace("'", "''");
                else
                    Proname = pname;
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PorderDate,103) as 'PO Date',PurchaseMaster.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price'  FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (PurchaseDetail.SGstPercent!=NULL or PurchaseDetail.SGstPercent!=0 or PurchaseDetail.CGstPercent!=NULL or PurchaseDetail.CGstPercent!=0 or PurchaseDetail.IGstPercent!=NULL or PurchaseDetail.IGstPercent!=0 or PurchaseMaster.TAXType='TaxPurchase') and PurchaseMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and PurchaseMaster.CompId='" + CommonMethod.CompId + "' and (PurchaseMaster.StockIn=" + compid + " or PurchaseMaster.StockIn is null)", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PorderDate,103) as 'PO Date',PurchaseMaster.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price' FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (PurchaseDetail.SGstPercent!=NULL or PurchaseDetail.SGstPercent!=0 or PurchaseDetail.CGstPercent!=NULL or PurchaseDetail.CGstPercent!=0 or PurchaseDetail.IGstPercent!=NULL or PurchaseDetail.IGstPercent!=0 or PurchaseMaster.TAXType='TaxPurchase') and PurchaseMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and PurchaseMaster.CompId='" + CommonMethod.CompId + "' and PurchaseMaster.StockIn=" + compid + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvPorderInfo.DataSource = dt;
                    GvPorderInfo.Refresh();
                    GvPorderInfo.Columns[0].Width = 65;
                    GvPorderInfo.Columns[1].Width = 55;
                    GvPorderInfo.Columns[2].Width = 50;
                    GvPorderInfo.Columns[3].Width = 50;
                    GvPorderInfo.Columns[4].Width = 60;
                    decimal totalqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", "")) + Convert.ToDecimal(dt.Compute("Sum(FrQty)", ""));
                    if (dt.Rows.Count > 0)
                        lbltotalpqty.Text = Convert.ToString(totalqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //Transfer To
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),TransferDate,103) as 'Transfer Date',Isnull(Quantity,0) as Quantity from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and Vat='True' and TransactionYear='" + CommonMethod.TransactionYear + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "' as varchar(10)) as varchar(15)) ", db.Connection);
                    else
                        da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),TransferDate,103) as 'Transfer Date',Isnull(Quantity,0) as Quantity from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and Vat='True' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + compid + "' as varchar(10)) as varchar(15)) ", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GVTransferToStock.DataSource = dt;
                    GVTransferToStock.Refresh();
                    GVTransferToStock.Columns[0].Width = 70;
                    GVTransferToStock.Columns[1].Width = 60;
                    decimal totalqty = Convert.ToDecimal(dt.Compute("Sum(Quantity)", "")) + Convert.ToDecimal(lbltotalpqty.Text);
                    if (dt.Rows.Count > 0)
                        lbltotalpqty.Text = Convert.ToString(totalqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PurchaseReturnDate,103) as 'PR Date',m.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price'  FROM PurchaseReturnMaster m	inner join PurchaseReturnDetail d on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on m.PorderNo =pm.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and (pm.StockIn=" + compid + " or pm.StockIn is null)	and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear=m.TransactionYear and pm.CompId=m.CompId", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PurchaseReturnDate,103) as 'PR Date',m.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price' FROM PurchaseReturnMaster m	inner join PurchaseReturnDetail d on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on m.PorderNo =pm.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.StockIn=" + compid + "	and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear=m.TransactionYear and pm.CompId=m.CompId", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvPRInfo.DataSource = dt;
                    GvPRInfo.Refresh();
                    GvPRInfo.Columns[0].Width = 65;
                    GvPRInfo.Columns[1].Width = 55;
                    GvPRInfo.Columns[2].Width = 50;
                    GvPRInfo.Columns[3].Width = 50;
                    GvPRInfo.Columns[4].Width = 60;
                    decimal totalqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", "")) + Convert.ToDecimal(dt.Compute("Sum(FrQty)", ""));
                    if (dt.Rows.Count > 0)
                        lbltprqty.Text = Convert.ToString(totalqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //saledetails
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',CustomerBillMaster.BillNo,Quantity as 'Qty',Rate  FROM [CustomerBillMaster] inner join [CustomerBillDetail] on CustomerBillMaster.Id=CustomerBillDetail.BillNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (AssemblyChildStatus!='Y' or AssemblyChildStatus is null or AssemblyChildStatus='') and (CustomerBillMaster.BillNo like 'T%') and CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvBillInfo.DataSource = dt1;
                        GvBillInfo.Refresh();
                        GvBillInfo.Columns[0].Width = 65;
                        GvBillInfo.Columns[1].Width = 55;
                        GvBillInfo.Columns[2].Width = 50;
                        GvBillInfo.Columns[3].Width = 65;
                        if (dt1.Rows.Count > 0)
                            lbltotalsqty.Text = Convert.ToString(dt1.Compute("Sum(Qty)", ""));
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltotalsqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',CustomerBillMaster.BillNo,Quantity as 'Qty',Rate  FROM [CustomerBillMaster] inner join [CustomerBillDetail] on CustomerBillMaster.Id=CustomerBillDetail.BillNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and AssemblyChildStatus='Y'  and (CustomerBillMaster.BillNo like 'T%') and CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvTransferFromStock.DataSource = dt1;
                        GvTransferFromStock.Refresh();
                        GvTransferFromStock.Columns[0].Width = 70;
                        GvTransferFromStock.Columns[1].Width = 60;
                        decimal totaltfqty = Convert.ToDecimal(dt1.Compute("Sum(Qty)", "")) + Convert.ToDecimal(lbltotalsqty.Text);
                        if (dt1.Rows.Count > 0)
                            lbltotalsqty.Text = Convert.ToString(totaltfqty);
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltotalsqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //salereturndetails
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),GoodReturnDate,103) as 'SR Date',BillNo,Quantity as 'Qty',Rate FROM GoodReturnMaster inner join GoodReturnDetail on GoodReturnMaster.Id=GoodReturnDetail.GoodReturnNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (GoodReturnMaster.BillNo like 'T%') and GoodReturnMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and GoodReturnMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvSRInfo.DataSource = dt1;
                        GvSRInfo.Refresh();
                        GvSRInfo.Columns[0].Width = 65;
                        GvSRInfo.Columns[1].Width = 55;
                        GvSRInfo.Columns[2].Width = 50;
                        GvSRInfo.Columns[3].Width = 60;
                        if (dt1.Rows.Count > 0)
                            lbltsrqty.Text = Convert.ToString(dt1.Compute("Sum(Qty)", ""));
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltsrqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //dead details
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    if (compid == 0)
                        da2 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date',DeadProductMaster.DeadNo,Quantity as 'Qty' FROM [DeadProductMaster] inner join [DeadProductDetail] on DeadProductMaster.Id=DeadProductDetail.DeadNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and Vat!=0.00 and DeadProductMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and DeadProductMaster.CompId='" + CommonMethod.CompId + "' and (DeadProductMaster.StockIn=" + compid + " or DeadProductMaster.StockIn is null)", db.Connection);
                    else
                        da2 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date',DeadProductMaster.DeadNo,Quantity as 'Qty' FROM [DeadProductMaster] inner join [DeadProductDetail] on DeadProductMaster.Id=DeadProductDetail.DeadNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and Vat!=0.00 and DeadProductMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and DeadProductMaster.CompId='" + CommonMethod.CompId + "' and DeadProductMaster.StockIn=" + compid + "", db.Connection);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    gvdeadinfo.DataSource = dt2;
                    gvdeadinfo.Refresh();
                    gvdeadinfo.Columns[0].Width = 65;
                    gvdeadinfo.Columns[1].Width = 55;
                    gvdeadinfo.Columns[2].Width = 50;
                    if (dt2.Rows.Count > 0)
                        lbltotaldqty.Text = Convert.ToString(dt2.Compute("Sum(Qty)", ""));
                    da2.Dispose();
                    dt2.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //Opening Qty
                    if (compid == 0)
                    {
                        SqlDataAdapter da2 = new SqlDataAdapter("SELECT Isnull(Sum(Isnull(OpeningQty,0)),0) FROM [StockOpening] where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and Vat='true' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                            lblOpeningQty.Text = Convert.ToString(dt2.Rows[0][0].ToString());
                        da2.Dispose();
                        dt2.Dispose();
                    }
                    else
                        lblOpeningQty.Text = "0";
                }
                catch (Exception)
                { }

                decimal remqty = 0;
                remqty = Convert.ToDecimal(lblOpeningQty.Text) + Convert.ToDecimal(lbltotalpqty.Text) - Convert.ToDecimal(lbltprqty.Text) - Convert.ToDecimal(lbltotalsqty.Text) + Convert.ToDecimal(lbltsrqty.Text) - Convert.ToDecimal(lbltotaldqty.Text);
                lbltotalrqty.Text = Convert.ToString(remqty);
            }
            catch (Exception)
            { }
        }

        public void withoutvatstock(string pname, string cname, string size, string vat, int compid)
        {
            try
            {
                string Proname = "";
                if ((pname).Contains("'"))
                    Proname = pname.Replace("'", "''");
                else
                    Proname = pname;
                try
                {
                    //PurchaseDetail 
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PorderDate,103) as 'PO Date',PurchaseMaster.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price'  FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (PurchaseDetail.SGstPercent=0 or PurchaseDetail.SGstPercent is NULL) and (PurchaseDetail.CGstPercent=0 or PurchaseDetail.CGstPercent is NULL) and (PurchaseDetail.IGstPercent=0 or PurchaseDetail.IGstPercent is NULL) and PurchaseMaster.TAXType is NULL and PurchaseMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and PurchaseMaster.CompId='" + CommonMethod.CompId + "' and (PurchaseMaster.StockIn=" + compid + " or PurchaseMaster.StockIn is null)", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PorderDate,103) as 'PO Date',PurchaseMaster.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price'  FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (PurchaseDetail.SGstPercent=0 or PurchaseDetail.SGstPercent is NULL) and (PurchaseDetail.CGstPercent=0 or PurchaseDetail.CGstPercent is NULL) and (PurchaseDetail.IGstPercent=0 or PurchaseDetail.IGstPercent is NULL) and PurchaseMaster.TAXType is NULL and PurchaseMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and PurchaseMaster.CompId='" + CommonMethod.CompId + "' and PurchaseMaster.StockIn=" + compid + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvPorderInfo.DataSource = dt;
                    GvPorderInfo.Refresh();
                    GvPorderInfo.Columns[0].Width = 65;
                    GvPorderInfo.Columns[1].Width = 55;
                    GvPorderInfo.Columns[2].Width = 50;
                    GvPorderInfo.Columns[3].Width = 50;
                    GvPorderInfo.Columns[4].Width = 60;
                    decimal totalqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", "")) + Convert.ToDecimal(dt.Compute("Sum(FrQty)", ""));
                    if (dt.Rows.Count > 0)
                        lbltotalpqty.Text = Convert.ToString(totalqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //Transfer To
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),TransferDate,103) as 'Transfer Date',Isnull(Quantity,0) as Quantity from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and Vat='False' and TransactionYear='" + CommonMethod.TransactionYear + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "' as varchar(10)) as varchar(15)) ", db.Connection);
                    else
                        da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),TransferDate,103) as 'Transfer Date',Isnull(Quantity,0) as Quantity from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and Vat='False' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + compid + "' as varchar(10)) as varchar(15)) ", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GVTransferToStock.DataSource = dt;
                    GVTransferToStock.Refresh();
                    GVTransferToStock.Columns[0].Width = 70;
                    GVTransferToStock.Columns[1].Width = 60;
                    decimal totalqty = Convert.ToDecimal(dt.Compute("Sum(Quantity)", "")) + Convert.ToDecimal(lbltotalpqty.Text);
                    if (dt.Rows.Count > 0)
                        lbltotalpqty.Text = Convert.ToString(totalqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //PurchasereturnDetail 
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (compid == 0)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PurchaseReturnDate,103) as 'PR Date',m.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price' FROM PurchaseReturnMaster m	inner join PurchaseReturnDetail d on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on m.PorderNo =pm.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (d.SGstPercent=0 or d.SGstPercent is NULL) and (d.CGstPercent=0 or d.CGstPercent is NULL) and (d.IGstPercent=0 or d.IGstPercent is NULL) and pm.TAXType is NULL  and (pm.StockIn=" + compid + " or pm.StockIn is null)	and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear=m.TransactionYear and pm.CompId=m.CompId", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),PurchaseReturnDate,103) as 'PR Date',m.PorderNo as 'PO No',Quantity as 'Qty',Isnull(FreeQty,0) as 'FrQty',PurchasePrice as 'Pur.Price' FROM PurchaseReturnMaster m	inner join PurchaseReturnDetail d on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on m.PorderNo =pm.PorderNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (d.SGstPercent=0 or d.SGstPercent is NULL) and (d.CGstPercent=0 or d.CGstPercent is NULL) and (d.IGstPercent=0 or d.IGstPercent is NULL) and pm.TAXType is NULL  and pm.StockIn=" + compid + "	and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear=m.TransactionYear and pm.CompId=m.CompId", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvPRInfo.DataSource = dt;
                    GvPRInfo.Refresh();
                    GvPRInfo.Columns[0].Width = 65;
                    GvPRInfo.Columns[1].Width = 55;
                    GvPRInfo.Columns[2].Width = 50;
                    GvPRInfo.Columns[3].Width = 50;
                    GvPRInfo.Columns[4].Width = 60;
                    decimal totalqty = Convert.ToDecimal(dt.Compute("Sum(Qty)", "")) + Convert.ToDecimal(dt.Compute("Sum(FrQty)", ""));
                    if (dt.Rows.Count > 0)
                        lbltprqty.Text = Convert.ToString(totalqty);
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //saledetails
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',CustomerBillMaster.BillNo,Quantity as 'Qty',Rate  FROM [CustomerBillMaster] inner join [CustomerBillDetail] on CustomerBillMaster.Id=CustomerBillDetail.BillNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (AssemblyChildStatus!='Y' or AssemblyChildStatus is null or AssemblyChildStatus='') and  (CustomerBillMaster.BillNo like 'E%') and CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvBillInfo.DataSource = dt1;
                        GvBillInfo.Refresh();
                        GvBillInfo.Columns[0].Width = 65;
                        GvBillInfo.Columns[1].Width = 55;
                        GvBillInfo.Columns[2].Width = 50;
                        GvBillInfo.Columns[3].Width = 65;
                        if (dt1.Rows.Count > 0)
                            lbltotalsqty.Text = Convert.ToString(dt1.Compute("Sum(Qty)", ""));
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltotalsqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //Transfer From                    
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',CustomerBillMaster.BillNo,Quantity as 'Qty',Rate  FROM [CustomerBillMaster] inner join [CustomerBillDetail] on CustomerBillMaster.Id=CustomerBillDetail.BillNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and AssemblyChildStatus='Y'  and (CustomerBillMaster.BillNo like 'E%') and CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvTransferFromStock.DataSource = dt1;
                        GvTransferFromStock.Refresh();
                        GvTransferFromStock.Columns[0].Width = 70;
                        GvTransferFromStock.Columns[1].Width = 60;
                        decimal totaltfqty = Convert.ToDecimal(dt1.Compute("Sum(Qty)", "")) + Convert.ToDecimal(lbltotalsqty.Text);
                        if (dt1.Rows.Count > 0)
                            lbltotalsqty.Text = Convert.ToString(totaltfqty);
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltotalsqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //salereturndetails
                    if (compid == 0)
                    {
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),GoodReturnDate,103) as 'SR Date',BillNo,Quantity As 'Qty',Rate FROM GoodReturnMaster inner join GoodReturnDetail on GoodReturnMaster.Id=GoodReturnDetail.GoodReturnNo where ProductId='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and (GoodReturnMaster.BillNo like 'E%') and GoodReturnMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and GoodReturnMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        GvSRInfo.DataSource = dt1;
                        GvSRInfo.Refresh();
                        GvSRInfo.Columns[0].Width = 65;
                        GvSRInfo.Columns[1].Width = 55;
                        GvSRInfo.Columns[2].Width = 50;
                        GvSRInfo.Columns[3].Width = 60;
                        if (dt1.Rows.Count > 0)
                            lbltsrqty.Text = Convert.ToString(dt1.Compute("Sum(Qty)", ""));
                        da1.Dispose();
                        dt1.Dispose();
                    }
                    else
                        lbltsrqty.Text = "0";
                }
                catch (Exception)
                { }

                try
                {
                    //dead details
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    if (compid == 0)
                        da2 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date',DeadProductMaster.DeadNo,Quantity as 'Qty' FROM [DeadProductMaster] inner join [DeadProductDetail] on DeadProductMaster.Id=DeadProductDetail.DeadNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and Vat=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "' and (DeadProductMaster.StockIn=" + compid + " or DeadProductMaster.StockIn is null)", db.Connection);
                    else
                        da2 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date',DeadProductMaster.DeadNo,Quantity As 'Qty' FROM [DeadProductMaster] inner join [DeadProductDetail] on DeadProductMaster.Id=DeadProductDetail.DeadNo where ProductName='" + Proname + "' and CompanyName='" + cname + "' and Unit='" + size + "' and Vat=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "' and DeadProductMaster.StockIn=" + compid + "", db.Connection);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    gvdeadinfo.DataSource = dt2;
                    gvdeadinfo.Refresh();
                    gvdeadinfo.Columns[0].Width = 65;
                    gvdeadinfo.Columns[1].Width = 55;
                    gvdeadinfo.Columns[2].Width = 50;
                    if (dt2.Rows.Count > 0)
                        lbltotaldqty.Text = Convert.ToString(dt2.Compute("Sum(Qty)", ""));
                    da2.Dispose();
                    dt2.Dispose();
                }
                catch (Exception)
                { }

                try
                {
                    //Opening Qty
                    if (compid == 0)
                    {
                        SqlDataAdapter da2 = new SqlDataAdapter("SELECT Isnull(Sum(Isnull(OpeningQty,0)),0) FROM [StockOpening] where ProductName='" + Proname + "' and MfgCompany='" + cname + "' and Unit='" + size + "' and Vat='false' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                            lblOpeningQty.Text = Convert.ToString(dt2.Rows[0][0].ToString());
                        da2.Dispose();
                        dt2.Dispose();
                    }
                    else
                        lblOpeningQty.Text = "0";
                }
                catch (Exception)
                { }

                decimal remqty = 0;
                remqty = Convert.ToDecimal(lblOpeningQty.Text) + Convert.ToDecimal(lbltotalpqty.Text) - Convert.ToDecimal(lbltprqty.Text) - Convert.ToDecimal(lbltotalsqty.Text) + Convert.ToDecimal(lbltsrqty.Text) - Convert.ToDecimal(lbltotaldqty.Text);
                lbltotalrqty.Text = Convert.ToString(remqty);
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

        private void frmshowdetailofstock_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
    }
}