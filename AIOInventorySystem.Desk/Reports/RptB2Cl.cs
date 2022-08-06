using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptB2Cl : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public RptB2Cl()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
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

        public void getall(string fdate, string tdate)
        {
            DateTime billdate;
            string gstinno, billno, Placeofsupply = "", errormsg = "", previousbill = "";
            decimal netamt, gstrate, taxableamt = 0, cessamt, TotalNetAmt = 0, saleqty = 0, IGSTAmt = 0, CGSTAmt = 0, SGSTAmt = 0;
            int Noofreceipients = 0, precustid = 0, billcustomerid = 0;
            bool hastaxes = true;

            try
            {
                db.connect();
                bool userSetting = false;
                SqlDataAdapter da;
                DSB2B ds = new DSB2B();
                DataTable datab2cl;
                SettingRepository setrepo = new SettingRepository();
                Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (setData != null)
                {
                    if (setData.ShowUserDefinedSeries == "Y")
                        userSetting = true;
                    else
                        userSetting = false;
                }
                setrepo.Dispose();
                DataTable dtdetail = db.Gettable("Select cm.billno as invoiceno,cm.billdate,cm.netamount as netamount," +  //0,1,2
                                    "case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as gst," +  //3
                                    "cd.quantity,cd.rate,cd.unit,cd.saleunit,case when cd.cessamt is null then 0 else cd.cessamt end as cessamt,cd.amount,cm.customerid, " +//4,5,6,7,8,9,10,
                                    "cd.sgstamt,cd.cgstamt,cd.igstamt, " + //11,12,13
                                    "case when cd.discount is null then 0 else cd.discount end as discount,case when cd.spcdisc is null then 0 else cd.spcdisc end as spcdisc, " + //14,15
                                    "case when cd.salerate is null then cd.rate else cd.salerate end as srate,cd.companyname,cd.ProductId,cm.userseries from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno" + //16,17,18,19
                                    " Where  cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "'  and cm.TransactionYear='" + CommonMethod.TransactionYear +
                                    "' and cm.CompId=" + CommonMethod.CompId + " and cm.netamount>250000 and cm.BillNo like 'T%'");
                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        billcustomerid = Convert.ToInt32(dtdetail.Rows[i]["customerid"]);
                        DataTable dtcust = db.Gettable("select cust.state,case when cust.statecode is null then 0 else cust.statecode end as statecode,case when comp.statecode is null then 0 else comp.statecode end as cstatecode,cust.buyertinno from customerinformation as cust join companyinformation as comp on cust.compid=comp.companyid where comp.companyid='" + CommonMethod.CompId + "' and cust.buyertinno='' and cust.customerid=" + billcustomerid);
                        if (dtcust.Rows.Count > 0)
                        {
                            errormsg = "Error on getting quantity details having bill no " + dtdetail.Rows[i]["invoiceno"].ToString();
                            if (Convert.ToInt32(dtcust.Rows[0]["statecode"]) == Convert.ToInt32(dtcust.Rows[0]["cstatecode"]))
                                hastaxes = false;
                            gstrate = Convert.ToDecimal(dtdetail.Rows[i]["gst"].ToString());
                            if (gstrate == 0 || (Convert.ToDecimal(dtdetail.Rows[i]["sgstamt"].ToString()) == 0 && Convert.ToDecimal(dtdetail.Rows[i]["cgstamt"].ToString()) == 0 && Convert.ToDecimal(dtdetail.Rows[i]["igstamt"].ToString()) == 0))
                                hastaxes = false;
                            if (hastaxes == true)
                            {
                                gstinno = dtcust.Rows[0]["buyertinno"].ToString();
                                if (userSetting == false)
                                    billno = dtdetail.Rows[i]["invoiceno"].ToString();
                                else
                                    billno = dtdetail.Rows[i]["userseries"].ToString();
                                billdate = Convert.ToDateTime(dtdetail.Rows[i]["billdate"].ToString());
                                netamt = Convert.ToDecimal(dtdetail.Rows[i]["netamount"].ToString());
                                Placeofsupply = dtcust.Rows[0]["statecode"].ToString() + '-' + dtcust.Rows[0]["state"].ToString();
                                saleqty = Convert.ToDecimal(dtdetail.Rows[i]["quantity"].ToString());
                                if (dtdetail.Rows[i]["unit"].ToString() != dtdetail.Rows[i]["saleunit"].ToString())   //Alternate unit
                                {
                                    if (Convert.ToDecimal(dtdetail.Rows[i]["rate"].ToString()) != Convert.ToDecimal(dtdetail.Rows[i]["srate"].ToString()))
                                    {
                                        ProductRepository ProductRepo = new ProductRepository();
                                        ProductInformation proddata = new ProductInformation();
                                        if (CommonMethod.commProduct == true)
                                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == dtdetail.Rows[i]["companyname"].ToString() && t.Uniti == dtdetail.Rows[i]["unit"].ToString()).FirstOrDefault();
                                        else
                                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == dtdetail.Rows[i]["companyname"].ToString() && t.Uniti == dtdetail.Rows[i]["unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (proddata != null)
                                        {
                                            if (dtdetail.Rows[i]["saleunit"].ToString() == proddata.AlternateUnit)
                                            {
                                                saleqty = Convert.ToDecimal(Convert.ToDecimal(dtdetail.Rows[i]["quantity"].ToString()) / proddata.UnitQty);
                                                taxableamt = saleqty * Convert.ToDecimal(dtdetail.Rows[i]["rate"].ToString());
                                            }
                                        }
                                        ProductRepo.Dispose();
                                    }
                                    else
                                        taxableamt = Convert.ToDecimal(dtdetail.Rows[i]["amount"].ToString());
                                }
                                else
                                    taxableamt = Convert.ToDecimal(dtdetail.Rows[i]["quantity"].ToString()) * Convert.ToDecimal(dtdetail.Rows[i]["rate"].ToString());

                                if (Convert.ToDecimal(dtdetail.Rows[i]["discount"].ToString()) != 0)
                                    taxableamt = taxableamt - (taxableamt * Convert.ToDecimal(dtdetail.Rows[i]["discount"].ToString()) / 100);
                                if (Convert.ToDecimal(dtdetail.Rows[i]["spcdisc"].ToString()) != 0)
                                    taxableamt = taxableamt - (taxableamt * Convert.ToDecimal(dtdetail.Rows[i]["spcdisc"].ToString()) / 100);

                                if (Convert.ToDecimal(dtdetail.Rows[i]["igstamt"].ToString()) != 0)
                                    IGSTAmt = Convert.ToDecimal(dtdetail.Rows[i]["igstamt"].ToString());
                                if (Convert.ToDecimal(dtdetail.Rows[i]["cgstamt"].ToString()) != 0)
                                    CGSTAmt = Convert.ToDecimal(dtdetail.Rows[i]["cgstamt"].ToString());
                                if (Convert.ToDecimal(dtdetail.Rows[i]["sgstamt"].ToString()) != 0)
                                    SGSTAmt = Convert.ToDecimal(dtdetail.Rows[i]["sgstamt"].ToString());

                                cessamt = Convert.ToDecimal(dtdetail.Rows[i]["cessamt"].ToString());
                                errormsg = "Error on getting sale details having bill no " + dtdetail.Rows[i]["invoiceno"].ToString();
                                if (previousbill == "" || previousbill != billno)
                                {
                                    previousbill = billno;
                                    TotalNetAmt += netamt;
                                }
                                if (precustid == 0 || precustid != Convert.ToInt32(dtdetail.Rows[i]["customerid"]))
                                {
                                    precustid = Convert.ToInt32(dtdetail.Rows[i]["customerid"]);
                                    Noofreceipients += 1;
                                }
                                datab2cl = ds.Tables["B2BTransaction"];
                                datab2cl.Rows.Add(new object[15] { "", billno, billdate, netamt, Placeofsupply, "", "", "", gstrate, taxableamt, cessamt, "", IGSTAmt, CGSTAmt, SGSTAmt });
                            }
                        }
                        hastaxes = true;
                    }
                }

                da = new SqlDataAdapter("Select * from CustomerInformation where CompId=" + CommonMethod.CompId + " and BuyerTinNo=''", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId=" + CommonMethod.CompId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                B2ClSummaryReport salerpt = new B2ClSummaryReport();
                salerpt.SetDataSource(ds);

                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
                salerpt.SetParameterValue("TotalInvoiceValue", TotalNetAmt);
                salerpt.SetParameterValue("NoofReceipients", Noofreceipients);

                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                getallBill(cm.ValidFromDate(dtpMonth.Value.Date), cm.ValidToDate(dtptomonth.Value.Date));
            }
            catch (Exception)
            { }
        }

        public void getallBill(string fdate, string tdate)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSB2B ds = new DSB2B();
                System.Windows.Forms.Application.DoEvents();
                SettingRepository setRepo = new SettingRepository();
                Setting setData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (setData.ShowUserDefinedSeries == "Y")
                    da = new SqlDataAdapter("Select ci.BuyerTinNo as Gstin, cm.UserSeries as InvoiceNo,cm.billdate as InvoiceDate,cm.netamount as InvoiceValue," +
                                               "cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply,'N' as ReverseCharges,'Regular' as InvoiceType,'' as Ecommgstin," +
                                               "case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                               "case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                               "Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt ,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST " +
                                               "from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                               "inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID inner join CompanyInformation as com on ci.CompId=com.CompanyId " +
                                               "Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate +
                                               "' and (convert(varchar(10),cm.billdate,120) >= (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null) then convert(varchar(10),ci.GSTINAddedDate,120) end) " +
                                               " or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo is null or ci.BuyerTinNo='') then Convert(varchar(10),cm.billdate,120) end)) " +
                                               "and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId=" + CommonMethod.CompId + " and cm.BillNo like 'T%' " +    ///and (ci.BuyerTinNo is null  or ci.BuyerTinNo='')
                                               "and ci.CompId=" + CommonMethod.CompId + " and ci.StateCode!=com.StateCode and cm.netamount>250000 group by ci.BuyerTinNo, cm.UserSeries, cm.billdate,cm.netamount,cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode,ci.State order by cm.billdate", db.Connection);
                else
                    da = new SqlDataAdapter("Select ci.BuyerTinNo as Gstin, cm.billno as InvoiceNo,cm.billdate as InvoiceDate,cm.netamount as InvoiceValue," +
                                                                  "cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply,'N' as ReverseCharges,'Regular' as InvoiceType,'' as Ecommgstin," +
                                                                  "case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                                                  "case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                                                  "Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt ,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST " +
                                                                  "from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                                                  "inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID inner join CompanyInformation as com on ci.CompId=com.CompanyId " +
                                                                  "Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate +
                                                                  "' and (convert(varchar(10),cm.billdate,120) >= (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null) then convert(varchar(10),ci.GSTINAddedDate,120) end) " +
                                                                  " or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo is null or ci.BuyerTinNo='') then Convert(varchar(10),cm.billdate,120) end)) " +
                                                                  "and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId=" + CommonMethod.CompId + " and cm.BillNo like 'T%' " +    ///and (ci.BuyerTinNo is null  or ci.BuyerTinNo='')
                                                                  "and ci.CompId=" + CommonMethod.CompId + " and ci.StateCode!=com.StateCode and cm.netamount>250000 group by ci.BuyerTinNo, cm.billno, cm.billdate,cm.netamount,cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode,ci.State order by cm.billdate", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "B2BTransaction");
                System.Windows.Forms.Application.DoEvents();

                da = new SqlDataAdapter("Select * from CustomerInformation where CompId=" + CommonMethod.CompId + " and BuyerTinNo=''", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");
                System.Windows.Forms.Application.DoEvents();

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId=" + CommonMethod.CompId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                System.Windows.Forms.Application.DoEvents();

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");
                System.Windows.Forms.Application.DoEvents();

                B2ClSummaryReport salerpt = new B2ClSummaryReport();
                salerpt.SetDataSource(ds);

                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
                salerpt.SetParameterValue("TotalInvoiceValue", "0");
                salerpt.SetParameterValue("NoofReceipients", 0);

                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }
    }
}