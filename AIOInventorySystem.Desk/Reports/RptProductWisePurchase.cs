using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptProductWisePurchase : Form
    {
        DbClass db = new DbClass();
        ProductWisePurchaseRpt crpt = new ProductWisePurchaseRpt();
        CommonMethod cm = new CommonMethod();

        public RptProductWisePurchase()
        {
            InitializeComponent();
            getdata();
        }

        public RptProductWisePurchase(string proname, string suppname, string mfgcompany, int groupid, int code, bool Gst, string from1, string to1, int godownid)
        {
            InitializeComponent();
            getdata(proname, suppname, mfgcompany, groupid, code, Gst, from1, to1, godownid);
        }

        public void getdata(string proname, string suppname, string mfgcompany, int groupid, int code, bool Gst, string from1, string to1, int godownid)
        {
            string Proname1 = "", company = "", unit = "", Proname = "", suppliername = "", qty = "", toDate = "", fromDate = "";
            int porderno = 0;
            DateTime billdate;
            decimal rate = 0, amount = 0;
            try
            {
                db.connect();
                SqlDataAdapter da;
                DataTable dt;
                DSProductwisepurchase ds = new DSProductwisepurchase();
                if (from1 != "" && to1 != "")
                {
                    toDate = cm.ValidToDate(Convert.ToDateTime(to1));
                    fromDate = cm.ValidFromDate(Convert.ToDateTime(from1));
                }
                string strsql = "";

                if (proname != "")
                {
                    Proname1 = proname.Split(',')[0];
                    company = proname.Split(',')[1];
                    unit = proname.Split(',')[2];
                    Proname = "";

                    if ((Proname1).Contains("'"))
                        Proname = Proname1.Replace("'", "''");
                    else
                        Proname = Proname1;
                    if (godownid == 0)
                    {
                        if (strsql != "")
                            strsql = strsql + "and d.ProductId='" + proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + " ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null) ";
                        }
                    }
                    else
                    {
                        if (strsql != "")
                            strsql = strsql + "and d.ProductId='" + proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "'";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null) ";
                        }
                    }
                }
                if (suppname != "")
                {
                    if (godownid == 0)
                    {
                        if (strsql != "")
                            strsql = strsql + " and m.SupplierName='" + suppname + "' ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where m.SupplierName='" + suppname + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase') and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where m.SupplierName='" + suppname + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                    else
                    {
                        if (strsql != "")
                            strsql = strsql + "and m.SupplierName='" + suppname + "'";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where m.SupplierName='" + suppname + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where m.SupplierName='" + suppname + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                }
                if (mfgcompany != "")
                {
                    if (godownid == 0)
                    {
                        if (strsql != "")
                            strsql = strsql + "and d.CompanyName='" + mfgcompany + "'";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.CompanyName='" + mfgcompany + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.CompanyName='" + mfgcompany + "' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null) ";
                        }
                    }
                    else
                    {
                        if (strsql != "")
                            strsql = strsql + "and d.CompanyName='" + mfgcompany + "' ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.CompanyName='" + mfgcompany + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.CompanyName='" + mfgcompany + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                }
                if (groupid != 0)
                {
                    if (godownid == 0)
                    {
                        if (strsql != "")
                            strsql = strsql + "and p.GroupMasterId ='" + groupid + "' ";
                        else
                        {
                            if (Gst == true)
                            {
                                if (CommonMethod.commProduct == true)
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.GroupMasterId ='" + groupid + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                                else
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.compid=" + CommonMethod.CompId + " and p.GroupMasterId ='" + groupid + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                            }
                            else
                            {
                                if (CommonMethod.commProduct == true)
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.GroupMasterId ='" + groupid + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null) ";
                                else
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.compid=" + CommonMethod.CompId + " and p.GroupMasterId ='" + groupid + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null) ";
                            }
                        }
                    }
                    else
                    {
                        if (strsql != "")
                            strsql = strsql + "and p.GroupMasterId ='" + groupid + "' ";
                        else
                        {
                            if (Gst == true)
                            {
                                if (CommonMethod.commProduct == true)
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.GroupMasterId ='" + groupid + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                                else
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.compid=" + CommonMethod.CompId + " and p.GroupMasterId ='" + groupid + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                            }
                            else
                            {
                                if (CommonMethod.commProduct == true)
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.GroupMasterId ='" + groupid + "' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                                else
                                    strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where p.compid=" + CommonMethod.CompId + " and p.GroupMasterId ='" + groupid + "' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                            }
                        }
                    }
                }
                if (code != 0)
                {
                    if (godownid == 0)
                    {
                        if (strsql != "")
                            strsql = strsql + "and d.Code='" + code + "' ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.Code='" + code + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or  m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.Code='" + code + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                    else
                    {
                        if (strsql != "")
                            strsql = strsql + "and d.Code='" + code + "' ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.Code='" + code + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where d.Code='" + code + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                }
                if (fromDate != "" && toDate != "" && fromDate != null && toDate != null)
                {
                    if (godownid == 0)
                    {
                        if (strsql != "")
                            strsql = strsql + "and  m.PorderDate>='" + fromDate + "' and m.PorderDate<='" + toDate + "' ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where  m.PorderDate>='" + fromDate + "' and m.PorderDate<='" + toDate + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + godownid + "' or m.StockIn is null) ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where  m.PorderDate>='" + fromDate + "' and m.PorderDate<='" + toDate + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                    else
                    {
                        if (strsql != "")
                            strsql = strsql + "and  m.PorderDate>='" + fromDate + "' and m.PorderDate<='" + toDate + "' ";
                        else
                        {
                            if (Gst == true)
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where  m.PorderDate>='" + fromDate + "' and m.PorderDate<='" + toDate + "' and m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and  m.StockIn='" + godownid + "' ";
                            else
                                strsql = "Select m.PorderNo,CONVERT(VARCHAR(24),m.PorderDate,103) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount,d.Unit from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm Where  m.PorderDate>='" + fromDate + "' and m.PorderDate<='" + toDate + "' and m.SupplierName!='First Stock' and (m.TotalGst=0 or m.TotalGst=null) and m.TAXType is null and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (StockIn=0 or StockIn is null)";
                        }
                    }
                }
                strsql = strsql + " order by m.PorderDate";
                DataTable dtMaster;
                dtMaster = db.GetTable(strsql);
                for (int i = 0; i < dtMaster.Rows.Count; i++)
                {
                    porderno = Convert.ToInt32(dtMaster.Rows[i]["PorderNo"].ToString());
                    billdate = Convert.ToDateTime(dtMaster.Rows[i]["Porder Date"].ToString());
                    suppliername = (dtMaster.Rows[i]["Supplier Name"].ToString());
                    proname = dtMaster.Rows[i]["Product Name"].ToString();
                    qty = Convert.ToString(dtMaster.Rows[i]["Quantity"].ToString());
                    unit = Convert.ToString(dtMaster.Rows[i]["Unit"].ToString());
                    rate = Convert.ToDecimal(dtMaster.Rows[i]["Pur Price"].ToString());
                    amount = Convert.ToDecimal(dtMaster.Rows[i]["TotalAmount"].ToString());
                    dt = ds.Tables["DataTable1"];
                    dt.Rows.Add(new object[8] { porderno, billdate, suppliername, proname, qty, unit, rate, amount });
                }

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                crpt = new ProductWisePurchaseRpt();
                crpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = crpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose(); ds.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
        }

        public void getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                DSProductwisepurchase ds = new DSProductwisepurchase();
                da = new SqlDataAdapter("select m.PorderNo,m.PorderDate,m.SupplierName,d.ProductName,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.Unit,d.PurchasePrice,d.TotalAmount from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo Where  m.SupplierName!='First Stock' and (m.Vat=0 or m.Vat!=0 or m.TAXType='TaxPurchase' or m.TAXType=null) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DataTable1");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                crpt = new ProductWisePurchaseRpt();
                crpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = crpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose(); ds.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
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
    }
}
