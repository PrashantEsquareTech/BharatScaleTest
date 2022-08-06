using System;
using System.Data;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Forms;
using System.Data.SqlClient;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptDailySaleDetail : Form
    {
        DbClass db = new DbClass();
        DailySaleDetail DSDcrpt = new DailySaleDetail();
        frmDailySaleDetail fm = new frmDailySaleDetail();
        CommonMethod cm = new CommonMethod();

        public RptDailySaleDetail()
        {
            InitializeComponent();
        }

        public RptDailySaleDetail(string productname, string mfgcompany)
        {
            InitializeComponent();
            Getdata(productname, mfgcompany);
        }

        public RptDailySaleDetail(string fdate, string tdate,string flag)
        {
            InitializeComponent();
            Getdata(fdate, tdate, flag);
        }

        public void Getdata(string fdate, string tdate, string flag)
        {
            try
            {
                DSDailySaleDetail ds = new DSDailySaleDetail();
                DataTable dt = new DataTable(), dt1 = new DataTable();

                try
                {
                    decimal profit1 = 0, totalamount = 0;
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCashProfit", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fdate; //dtpfromdate.Value.Date.ToShortDateString();
                    command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = tdate;// dtpfromdate.Value.Date.ToShortDateString();
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Dispose();
                    command.Dispose();
                    db.CloseConnection();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt1 = ds.Tables["DailySale"];
                            dt1.Rows.Add(new object[10] { "", dt.Rows[i]["ProductName"].ToString(), dt.Rows[i]["CompanyName"].ToString(), dt.Rows[i]["Unit"].ToString(), dt.Rows[i]["Quantity"].ToString(), dt.Rows[i]["SRate"].ToString(), dt.Rows[i]["PRate"].ToString(), dt.Rows[i]["SaleAmt"].ToString(), dt.Rows[i]["Profit"].ToString(), dt.Rows[i]["MarathiProductName"].ToString() });
                        }
                        if (Convert.ToString(dt.Compute("Sum(Profit)", "")) != "")
                            profit1 = Convert.ToDecimal(dt.Compute("Sum(Profit)", ""));
                        else
                            profit1 = 0;

                        if (Convert.ToString(dt.Compute("Sum(SaleAmt)", "")) != "")
                            totalamount = Convert.ToDecimal(dt.Compute("Sum(SaleAmt)", ""));
                        else
                            totalamount = 0;
                    }
                    dt = null;
                    dt = db.GetTable("Select CompanyName,CompanyAddress,ContactNo,Website from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'");
                    DSDcrpt = new DailySaleDetail();
                    DSDcrpt.SetDataSource(ds);
                    DSDcrpt.SetParameterValue("profit", profit1);
                    DSDcrpt.SetParameterValue("totalamt", totalamount);
                    DSDcrpt.SetParameterValue("companyname", dt.Rows[0]["CompanyName"].ToString());
                    DSDcrpt.SetParameterValue("address", dt.Rows[0]["CompanyAddress"].ToString());
                    DSDcrpt.SetParameterValue("conatactno", dt.Rows[0]["ContactNo"].ToString());
                    DSDcrpt.SetParameterValue("website", dt.Rows[0]["Website"].ToString());
                    crystalReportViewer1.ReportSource = DSDcrpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    ds.Dispose();

                }
                catch (Exception)
                { db.CloseConnection(); }


                //db.connect();
                //SqlDataAdapter sda;
                //sda = new SqlDataAdapter("SPCashProfit", db.Connection);
                //sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sda.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fdate; //dtpfromdate.Value.Date.ToShortDateString();
                //sda.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = tdate;// dtpfromdate.Value.Date.ToShortDateString();
                //sda.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                //sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                //sda.Fill(ds, "DailySale");
                //DSDcrpt = new DailySaleDetail();
                //DSDcrpt.SetDataSource(ds);
                //DSDcrpt.SetParameterValue("Fdate", fdate);
                //DSDcrpt.SetParameterValue("TDate", tdate);
                //crystalReportViewer1.ReportSource = DSDcrpt;
                //db.CloseConnection();
                //sda.Dispose();
                //ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(string productname, string mfgcompany)
        {
            string billno = "", Proname1 = "", company = "", unit = "", Proname = "", companyname = "", address = "", contactno = "", website = "";
            decimal qty = 0, rate = 0, amount = 0, samt = 0, sramt = 0;

            try
            {
                db.connect();
                DSDailySaleDetail ds = new DSDailySaleDetail();
                string strsql = "";
                decimal profit1 = 0, totalamount = 0, purchaseamt = 0, charges = 0;
                DateTime date = DateTime.Now.Date;
                if (productname != "")
                {
                    Proname1 = productname.Split(',')[0];
                    company = productname.Split(',')[1];
                    unit = productname.Split(',')[2];
                    Proname = "";
                    if ((Proname1).Contains("'"))
                        Proname = Proname1.Replace("'", "''");
                    else
                        Proname = Proname1;
                    if (strsql != "")
                        strsql = strsql + "and d.ProductId='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' ";
                    else
                        //                    0                          1               2          3                               4        5                              6           7        8              9                 10                                    11                      12
                        strsql = "(Select m.BillNo,d.ProductId as 'Product Name', d.CompanyName,d.Unit, Isnull(gd.Quantity,0) as Quantity,d.Rate, Isnull(gd.Amount ,0) as Amount, 0 as SAmt,m.Discount,m.TransportCharges,m.HamaliCharges,Isnull(gd.TotalAmount ,0) as SRAmt ,'Sale Return' as Type From CustomerBillMaster m, CustomerBillDetail d , GoodReturnMaster gm, GoodReturnDetail gd where  CONVERT(VARCHAR(24),m.Billdate,103)='" + date.ToString("dd/MM/yyyy") + "' and d.ProductId='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and (d.Status!='Inactive' or d.Status is null) and m.Id=d.BillNo and m.BillNo=gm.BillNo and gm.Id=gd.GoodReturnNo and gd.ProductId='" + Proname + "' and gd.CompanyName='" + company + "' and gd.Unit='" + unit + "' and d.ProductId=gd.ProductId and d.CompanyName=gd.CompanyName and d.Unit=gd.Unit and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + " and gm.TransactionYear='" + CommonMethod.TransactionYear + "' and gm.CompId=" + CommonMethod.CompId + ") union (Select m.BillNo,d.ProductId as 'Product Name', d.CompanyName,d.Unit, Isnull(d.Quantity,0) as Quantity,d.Rate, Isnull(d.Amount ,0) as Amount, Isnull(d.TotalAmount ,0) as SAmt,m.Discount,m.TransportCharges,m.HamaliCharges,0 as SRAmt,'Sale' as Type From CustomerBillMaster m, CustomerBillDetail d where  CONVERT(VARCHAR(24),m.Billdate,103)='" + date.ToString("dd/MM/yyyy") + "' and d.ProductId='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and (d.Status!='Inactive' or d.Status is null) and m.Id=d.BillNo and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + ")  order by m.BillNo";
                }
                else if (mfgcompany != "")
                {
                    if (strsql != "")
                        strsql = strsql + "and d.CompanyName='" + mfgcompany + "'";
                    else
                        strsql = "(Select m.BillNo,d.ProductId as 'Product Name', d.CompanyName,d.Unit, Isnull(gd.Quantity,0) as Quantity,d.Rate, Isnull(gd.Amount ,0) as Amount, 0 as SAmt,m.Discount,m.TransportCharges,m.HamaliCharges,Isnull(gd.TotalAmount ,0) as SRAmt ,'Sale Return' as Type From CustomerBillMaster m, CustomerBillDetail d , GoodReturnMaster gm, GoodReturnDetail gd where  CONVERT(VARCHAR(24),m.Billdate,103)='" + date.ToString("dd/MM/yyyy") + "' and  d.CompanyName='" + mfgcompany + "' and (d.Status!='Inactive' or d.Status is null) and m.Id=d.BillNo and m.BillNo=gm.BillNo and gm.Id=gd.GoodReturnNo  and gd.CompanyName='" + mfgcompany + "'  and d.ProductId=gd.ProductId and d.CompanyName=gd.CompanyName and d.Unit=gd.Unit and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + " and gm.TransactionYear='" + CommonMethod.TransactionYear + "' and gm.CompId=" + CommonMethod.CompId + ") union (Select m.BillNo,d.ProductId as 'Product Name', d.CompanyName,d.Unit, Isnull(d.Quantity,0) as Quantity,d.Rate, Isnull(d.Amount ,0) as Amount, Isnull(d.TotalAmount ,0) as SAmt,m.Discount,m.TransportCharges,m.HamaliCharges,0 as SRAmt,'Sale' as Type From CustomerBillMaster m, CustomerBillDetail d where CONVERT(VARCHAR(24),m.Billdate,103)='" + date.ToString("dd/MM/yyyy") + "'  and d.CompanyName='" + mfgcompany + "'  and (d.Status!='Inactive' or d.Status is null) and m.Id=d.BillNo and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + ")  order by m.BillNo";
                }
                else
                {
                    if (strsql != "")
                    {                        //strsql = strsql;
                    }
                    else
                        strsql = "(Select m.BillNo,d.ProductId as 'Product Name', d.CompanyName,d.Unit, Isnull(gd.Quantity,0) as Quantity,d.Rate, Isnull(gd.Amount ,0) as Amount, 0 as SAmt,m.Discount,m.TransportCharges,m.HamaliCharges,Isnull(gd.TotalAmount ,0) as SRAmt ,'Sale Return' as Type From CustomerBillMaster m, CustomerBillDetail d , GoodReturnMaster gm, GoodReturnDetail gd where  CONVERT(VARCHAR(24),m.Billdate,103)='" + date.ToString("dd/MM/yyyy") + "'  and (d.Status!='Inactive' or d.Status is null) and m.Id=d.BillNo and m.BillNo=gm.BillNo and gm.Id=gd.GoodReturnNo    and d.ProductId=gd.ProductId and d.CompanyName=gd.CompanyName and d.Unit=gd.Unit and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + " and gm.TransactionYear='" + CommonMethod.TransactionYear + "' and gm.CompId=" + CommonMethod.CompId + ") union (Select m.BillNo,d.ProductId as 'Product Name', d.CompanyName,d.Unit, Isnull(d.Quantity,0) as Quantity,d.Rate, Isnull(d.Amount ,0) as Amount, Isnull(d.TotalAmount ,0) as SAmt,m.Discount,m.TransportCharges,m.HamaliCharges,0 as SRAmt,'Sale' as Type From CustomerBillMaster m, CustomerBillDetail d where CONVERT(VARCHAR(24),m.Billdate,103)='" + date.ToString("dd/MM/yyyy") + "'   and (d.Status!='Inactive' or d.Status is null) and m.Id=d.BillNo and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + ")  order by m.BillNo";
                }
                DataTable dt1, dt, dtcompany;
                dt1 = db.GetTable(strsql);

                dtcompany = db.GetTable("select CompanyName,CompanyAddress,ContactNo,Website from CompanyInformation where Id=" + CommonMethod.CompId + " ");
                companyname = dtcompany.Rows[0]["CompanyName"].ToString();
                address = dtcompany.Rows[0]["CompanyAddress"].ToString();
                contactno = dtcompany.Rows[0]["ContactNo"].ToString();
                website = dtcompany.Rows[0]["Website"].ToString();

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    billno = Convert.ToString(dt1.Rows[i]["BillNo"].ToString());
                    Proname = dt1.Rows[i]["Product Name"].ToString();
                    company = dt1.Rows[i]["CompanyName"].ToString();
                    unit = dt1.Rows[i]["Unit"].ToString();
                    qty = Convert.ToDecimal(dt1.Rows[i]["Quantity"].ToString());
                    rate = Convert.ToDecimal(dt1.Rows[i]["Rate"].ToString());
                    amount = Convert.ToDecimal(dt1.Rows[i]["Amount"].ToString());
                    samt = Convert.ToDecimal(dt1.Rows[i]["SAmt"].ToString());
                    sramt = Convert.ToDecimal(dt1.Rows[i]["SRAmt"].ToString());
                    charges = (Convert.ToDecimal(dt1.Rows[i]["TransportCharges"].ToString()) + Convert.ToDecimal(dt1.Rows[i]["HamaliCharges"].ToString()));
                    samt = (samt - (samt * Convert.ToDecimal(dt1.Rows[i]["Discount"].ToString()) / 100));

                    DataTable dtMaster;
                    decimal totalamt = 0, taxableamt = 0, amt = 0, spcdisc = 0;
                    //                                        0             1             2         3        4         5                                6                             7                  8           9           10
                    dtMaster = db.GetTable("select top 1 d.Quantity,d.PurchasePrice,d.Discount,d.SGstAmt,d.CGstAmt,d.IGstAmt,IsNull(d.SpcDisc,0) as SpcDisc,IsNull(d.CessAmt,0) as Cess,m.Discount as MDisc,m.SpcDiscount,m.NetAmount from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo where d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + " order by m.Id desc ");
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        if (sramt == 0)
                        {
                            amt = (Convert.ToDecimal(dtMaster.Rows[k]["Quantity"].ToString()) * Convert.ToDecimal(dtMaster.Rows[k]["PurchasePrice"].ToString()));
                            taxableamt = amt - (amt * Convert.ToDecimal(dtMaster.Rows[k]["Discount"].ToString()) / 100);
                            taxableamt = taxableamt - (taxableamt * Convert.ToDecimal(dtMaster.Rows[k]["SpcDisc"].ToString()) / 100);
                            totalamt = taxableamt + Convert.ToDecimal(dtMaster.Rows[k]["SGstAmt"].ToString()) + Convert.ToDecimal(dtMaster.Rows[k]["CGstAmt"].ToString()) + Convert.ToDecimal(dtMaster.Rows[k]["IGstAmt"].ToString()) + Convert.ToDecimal(dtMaster.Rows[k]["Cess"].ToString());
                            spcdisc = Convert.ToDecimal(dtMaster.Rows[k]["MDisc"].ToString()) + Convert.ToDecimal(dtMaster.Rows[k]["SpcDiscount"].ToString());
                            purchaseamt = totalamt - (totalamt * spcdisc / 100);
                            purchaseamt = purchaseamt / (Convert.ToDecimal(dtMaster.Rows[k]["Quantity"].ToString()));
                            purchaseamt = purchaseamt * qty;
                            profit1 = profit1 + (samt - purchaseamt);
                        }
                        totalamount = totalamount + (samt - sramt);
                    }
                    dt = ds.Tables["DailySale"];
                    dt.Rows.Add(new object[10] { billno, Proname, company, unit, qty, rate, amount, samt, sramt,"" });
                }
                totalamount = totalamount + charges;
                DSDcrpt = new DailySaleDetail();
                DSDcrpt.SetDataSource(ds);
                DSDcrpt.SetParameterValue("profit", profit1);
                DSDcrpt.SetParameterValue("totalamt", totalamount);
                DSDcrpt.SetParameterValue("companyname", companyname);
                DSDcrpt.SetParameterValue("address", address);
                DSDcrpt.SetParameterValue("conatactno", contactno);
                DSDcrpt.SetParameterValue("website", website);
                crystalReportViewer1.ReportSource = DSDcrpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
    }
}
