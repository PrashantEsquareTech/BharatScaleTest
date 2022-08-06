using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptB2CS : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public RptB2CS()
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
            string Placeofsupply = "", errormsg = "", gstinno, billno;
            decimal gstrate, taxableamt = 0, cessamt, saleqty = 0, netamt, IGSTAmt = 0, CGSTAmt = 0, SGSTAmt = 0;
            bool isvalid = true;
            int precustid = 0;

            try
            {
                db.connect();
                SqlDataAdapter da;
                DSB2B ds = new DSB2B();
                DataTable dataB2cs;
                bool userSetting = false;
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
                                   " Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear +
                                   "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' ");
                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        precustid = Convert.ToInt32(dtdetail.Rows[i]["customerid"]);
                        //                                          0                                                                           1                                                                               2               3
                        DataTable dtcust = db.Gettable("select cust.state,case when cust.statecode is null then 0 else cust.statecode end as statecode,case when comp.statecode is null then 0 else comp.statecode end as cstatecode,cust.buyertinno from customerinformation as cust join companyinformation as comp on cust.compid=comp.companyid where comp.companyid='" + CommonMethod.CompId + "' and cust.buyertinno='' and cust.customerid=" + precustid);
                        if (dtcust.Rows.Count > 0)
                        {
                            errormsg = "Error on getting quantity details having bill no " + dtdetail.Rows[i]["invoiceno"].ToString();
                            if (dtcust.Rows[0]["statecode"].ToString() == "0" || dtcust.Rows[0]["cstatecode"].ToString() == "0")
                            {
                                MessageBox.Show("Please Enter state and state code in company and customer information for all records", "Warning");
                                isvalid = false;
                                goto stateError;
                            }
                            else
                            {
                                if (Convert.ToInt32(dtcust.Rows[0]["statecode"]) != Convert.ToInt32(dtcust.Rows[0]["cstatecode"]) && Convert.ToDecimal(dtdetail.Rows[i]["netamount"].ToString()) > 250000)
                                    isvalid = false;
                                gstrate = Convert.ToDecimal(dtdetail.Rows[i]["gst"].ToString());
                                if (gstrate == 0 || (Convert.ToDecimal(dtdetail.Rows[i]["sgstamt"].ToString()) == 0 && Convert.ToDecimal(dtdetail.Rows[i]["cgstamt"].ToString()) == 0 && Convert.ToDecimal(dtdetail.Rows[i]["igstamt"].ToString()) == 0))
                                    isvalid = false;
                                if (isvalid == true)
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
                                    dataB2cs = ds.Tables["B2BTransaction"];
                                    dataB2cs.Rows.Add(new object[15] { gstinno, billno, billdate, netamt, Placeofsupply, 'N', "Regular", "", gstrate, taxableamt, cessamt, "", IGSTAmt, CGSTAmt, SGSTAmt });
                                }
                            }
                        }
                        isvalid = true;
                    }
                }

                da = new SqlDataAdapter("Select * from CustomerInformation as cust inner join CompanyInformation as comp on comp.CompanyId=cust.CompId where cust.CompId='" + CommonMethod.CompId + "' and cust.BuyerTinNo='' and cust.State=comp.State and cust.StateCode=comp.StateCode", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                B2CSSummaryReport salerpt = new B2CSSummaryReport();
                salerpt.SetDataSource(ds);

                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();

                da.Dispose();
                ds.Dispose();
            stateError: { };
                db.CloseConnection();
            }
            catch (Exception)
            {
                MessageBox.Show(errormsg, "B2Cs Report");
                db.CloseConnection();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string toDate = cm.ValidToDate(dtptomonth.Value.Date), fromDate = cm.ValidFromDate(dtpMonth.Value.Date);
                getallBill(fromDate, toDate);
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
                da = new SqlDataAdapter("Select cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply,'N' as ReverseCharges,'Regular' as InvoiceType," +
                                           "'' as Ecommgstin,case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                           " case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                           " Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt ,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST " +
                                           " from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                           " inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID inner join CompanyInformation as com on ci.CompId=com.CompanyId " +
                                           " Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate +
                                           "' and (convert(varchar(10),cm.billdate,120) < (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null) then convert(varchar(10),ci.GSTINAddedDate,120) end) " +
                                           "or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo is null or ci.BuyerTinNo='') then convert(varchar(10),cm.billdate,120) end)) " +
                                           " and cm.TransactionYear='" + CommonMethod.TransactionYear +
                                           "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' and ci.CompId='" + CommonMethod.CompId +    //and (ci.BuyerTinNo is null or ci.BuyerTinNo='')
                                           "' and ((ci.StateCode!=com.StateCode and cm.netamount<250000) or ci.StateCode=com.StateCode) group by cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode," +
                                           "ci.State order by ci.Statecode", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "B2BTransaction");
                System.Windows.Forms.Application.DoEvents();

                da = new SqlDataAdapter("Select * from CustomerInformation as cust inner join CompanyInformation as comp on comp.CompanyId=cust.CompId where cust.CompId='" + CommonMethod.CompId + "' and cust.BuyerTinNo='' and cust.State=comp.State and cust.StateCode=comp.StateCode", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");
                System.Windows.Forms.Application.DoEvents();

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                da = new SqlDataAdapter("Select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                B2CSSummaryReport salerpt = new B2CSSummaryReport();
                salerpt.SetDataSource(ds);

                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();
                da.Dispose();
                ds.Dispose();
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
    }
}
