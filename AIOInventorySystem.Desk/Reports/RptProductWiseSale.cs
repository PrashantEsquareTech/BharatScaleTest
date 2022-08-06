using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptProductWiseSale : Form
    {
        DbClass db = new DbClass();
        ProductWiseSaleCrystalReport crpt = new ProductWiseSaleCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptProductWiseSale()
        {
            InitializeComponent();
            Getdata();
        }

        public RptProductWiseSale(string proname, string cname, string mfgcompany, int groupid, int code, bool Gst, string from1, string to1, int ShipPatyId)
        {
            InitializeComponent();
            getdata(proname, cname, mfgcompany, groupid, code, Gst, from1, to1, ShipPatyId);
        }

        public void getdata(string proname, string cname, string mfgcompany, int groupid, int code, bool Gst, string from1, string to1,int ShipPatyId)
        {
            string Proname1 = "", company = "", unit = "", Proname = "", billno = "", billdate = "", customername = "", qty = "", toDate = "", fromDate = "";
            decimal rate = 0, amount = 0;

            try
            {
                db.connect();
                SqlDataAdapter da;
                DataTable dt;
                DSProductWisesale ds = new DSProductWisesale();
                if (from1 != "" && to1 != "")
                {
                    toDate = cm.ValidToDate(Convert.ToDateTime(to1));
                    fromDate = cm.ValidFromDate(Convert.ToDateTime(from1));
                }
                string strsql = "";
                if (Gst == true)
                {
                    if (CommonMethod.commProduct == true)
                        strsql = "Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.UserSeries as 'UserSeries',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,cast(fnm.SSS as decimal(18,3)) as 'Rate',d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName and m.compid=p.compid left join GroupMaster as g on  p.GroupMasterId=g.GroupMasterId  cross apply (Select (Isnull(d.Rate,0)-(Isnull(d.Rate,0)*isnull(d.Discount,0)/100)) as 'SDiscAmt') as c cross apply(Select isnull(c.SDiscAmt,0)-(isnull(c.SDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp Cross apply (Select isnull(sp.spdamt,0)+(isnull(sp.spdamt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and (m.BillNo like 'T%')";
                    else
                        strsql = "Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.UserSeries as 'UserSeries',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,cast(fnm.SSS as decimal(18,3)) as 'Rate',d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName left join GroupMaster as g on  p.GroupMasterId=g.GroupMasterId  cross apply (Select (Isnull(d.Rate,0)-(Isnull(d.Rate,0)*isnull(d.Discount,0)/100)) as 'SDiscAmt') as c cross apply(Select isnull(c.SDiscAmt,0)-(isnull(c.SDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp Cross apply (Select isnull(sp.spdamt,0)+(isnull(sp.spdamt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and (m.BillNo like 'T%')";
                }
                else
                {
                    if (CommonMethod.commProduct == true)
                        strsql = "Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.UserSeries as 'UserSeries',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,cast(fnm.SSS as decimal(18,3)) as 'Rate',d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName and m.compid=p.compid left join GroupMaster as g on  p.GroupMasterId=g.GroupMasterId  cross apply (Select (Isnull(d.Rate,0)-(Isnull(d.Rate,0)*isnull(d.Discount,0)/100)) as 'SDiscAmt') as c cross apply(Select isnull(c.SDiscAmt,0)-(isnull(c.SDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp Cross apply (Select isnull(sp.spdamt,0)+(isnull(sp.spdamt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and (m.BillNo like 'E%')";
                    else
                        strsql = "Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.UserSeries as 'UserSeries',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,cast(fnm.SSS as decimal(18,3)) as 'Rate',d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName left join GroupMaster as g on  p.GroupMasterId=g.GroupMasterId  cross apply (Select (Isnull(d.Rate,0)-(Isnull(d.Rate,0)*isnull(d.Discount,0)/100)) as 'SDiscAmt') as c cross apply(Select isnull(c.SDiscAmt,0)-(isnull(c.SDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp Cross apply (Select isnull(sp.spdamt,0)+(isnull(sp.spdamt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and (m.BillNo like 'E%')";
                }

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
                    if (strsql != "")
                        strsql = strsql + "and d.ProductId='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "'";
                }
                if (cname != "")
                {
                    if (strsql != "")
                        strsql = strsql + "and m.CustomerName='" + cname + "'";
                }
                if (mfgcompany != "")
                {
                    if (strsql != "")
                        strsql = strsql + "and d.CompanyName='" + mfgcompany + "'";
                }
                if (groupid != 0)
                {
                    if (strsql != "")
                        strsql = strsql + "and p.GroupMasterId=" + groupid + "";
                }
                if (code != 0)
                {
                    if (strsql != "")
                        strsql = strsql + "and d.Code='" + code + "'";
                }
                if (fromDate != "" && toDate != "" && fromDate != null && toDate != null)
                {
                    if (strsql != "")
                        strsql = strsql + "and m.BillDate>='" + fromDate + "' and m.BillDate<='" + toDate + "'";
                }
                if (ShipPatyId != 0)
                {
                    if (strsql != "")
                        strsql = strsql + "and p.ShipCustomerId=" + ShipPatyId + "";
                }
                strsql = strsql + " order by m.Billdate";
                DataTable dtMaster;
                dtMaster = db.GetTable(strsql);
                for (int i = 0; i < dtMaster.Rows.Count; i++)
                {
                    billno = Convert.ToString(dtMaster.Rows[i]["Bill No"].ToString());
                    billdate = Convert.ToString(dtMaster.Rows[i]["Bill Date"].ToString());
                    customername = (dtMaster.Rows[i]["Customer Name"].ToString());
                    proname = dtMaster.Rows[i]["Product Name"].ToString();
                    qty = Convert.ToString(dtMaster.Rows[i]["Quantity"].ToString());
                    unit = Convert.ToString(dtMaster.Rows[i]["Unit"].ToString());
                    rate = Convert.ToDecimal(dtMaster.Rows[i]["Rate"].ToString());
                    amount = Convert.ToDecimal(dtMaster.Rows[i]["TotalAmount"].ToString());
                    dt = ds.Tables["DataTable1"];
                    dt.Rows.Add(new object[8] { billno, billdate, customername, proname, qty, unit, rate, amount });
                }

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                crpt = new ProductWiseSaleCrystalReport();
                crpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = crpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose(); ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSProductWisesale ds = new DSProductWisesale();

                da = new SqlDataAdapter("select m.BillNo,m.Billdate,m.CustomerName,d.ProductId,d.Quantity,d.Unit,d.Rate,d.TotalAmount from CustomerBillDetail d inner join  CustomerBillMaster m on m.Id=d.BillNo where d.ProductId and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DataTable1");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                crpt = new ProductWiseSaleCrystalReport();
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