using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;
using Excel1 = Microsoft.Office.Interop.Excel;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSummaryDetails : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public decimal Cgst5amt, Cgst12amt, Cgst18amt, Cgst28amt, Sgst5amt, Sgst12amt, Sgst18amt, Sgst28amt, Igst5amt, Igst12amt, Igst18amt, Igst28amt, cesssaleamt,
            cesspurchaseamt, taxable0amt, Sgst0amt, Cgst0amt, Igst0amt = 0, taxable5amt, taxable12amt, taxable18amt, taxable28amt, totalDiscount = 0,
            totalSaleDiscount = 0, totalspcDiscount = 0, totalTransport = 0, totalSaleTransport = 0, totalSaleHamali = 0, totalSaleCreditAmount = 0,
            totalSaleDebitAmount = 0, totalpurchaseCreditAmount = 0, totalpurchaseDebitAmount = 0;
        string errormsg = "";
        public DateTime fDate, tDate;

        public frmSummaryDetails()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            this.Text = "Sale Bill Reports";
            btnPrint.Text = "Print";
        }

        public frmSummaryDetails(int flag)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            this.Text = "All Reports";
            string input = "";
            input = Microsoft.VisualBasic.Interaction.InputBox("1 For CSV File and 2 For EXCEL File", "EXPORT", "", 250, 250);
            if (input == "1")
                btnPrint.Text = "Export CSV File";
            else if (input == "2")
                btnPrint.Text = "Export EXCEL File";
            else if (input == "")
                btnPrint.Text = "Export CSV File";
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

        private void Excel()
        {
            try
            {
                saveFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "Reports";
            }
            catch (Exception)
            { MessageBox.Show("Error occurred while converting into Excel file."); }
        }

        public DataTable B2B()
        {
            DataTable dt = new DataTable();
            try
            {
                string errormsg = "";
                SettingRepository setRepo = new SettingRepository();
                Setting setData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();

                try
                {
                    db.connect();
                    SqlDataAdapter da;
                    DSB2B ds = new DSB2B();
                    if (setData.ShowUserDefinedSeries == "Y")
                    {
                        da = new SqlDataAdapter("Select ci.BuyerTinNo as Gstin, cm.UserSeries as InvoiceNo,cm.billdate as InvoiceDate,cm.netamount as InvoiceValue,cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply," +
                                                "'N' as ReverseCharges,'Regular' as InvoiceType,'' as Ecommgstin,case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                                "case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                                " Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST "+
                                                " from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                                " inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID Where cm.billdate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and cm.billdate<='" + cm.ValidToDate(dtptomonth.Value.Date) + "' " +
                                                " and (convert(varchar(10),cm.billdate,120) >= (case when ((ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null)) then convert(varchar(10),ci.GSTINAddedDate,120) end) or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is null) " +
                                                "then convert(varchar(10),cm.billdate,120) end)) and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'  and ci.CompId='" + CommonMethod.CompId + "' group by ci.BuyerTinNo, cm.UserSeries, " +
                                                " cm.billdate,cm.netamount,cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode,ci.State order by cm.billdate ", db.Connection);
                    }
                    else
                    {
                        da = new SqlDataAdapter("Select ci.BuyerTinNo as Gstin, cm.billno as InvoiceNo,cm.billdate as InvoiceDate,cm.netamount as InvoiceValue,cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply," +
                                                "'N' as ReverseCharges,'Regular' as InvoiceType,'' as Ecommgstin,case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                                "case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                                " Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST "+
                                                " from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                                " inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID Where cm.billdate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and cm.billdate<='" + cm.ValidToDate(dtptomonth.Value.Date) + "' " +
                                                " and (convert(varchar(10),cm.billdate,120) >= (case when ((ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null)) then convert(varchar(10),ci.GSTINAddedDate,120) end) or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is null) " +
                                                "then convert(varchar(10),cm.billdate,120) end)) and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'  and ci.CompId='" + CommonMethod.CompId + "' group by ci.BuyerTinNo, cm.billNo, " +
                                                " cm.billdate,cm.netamount,cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode,ci.State order by cm.billdate ", db.Connection);
                    }
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "B2BTransaction");
                    dt = ds.Tables["B2BTransaction"];
                    db.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show(errormsg, "B2B Report");
                    db.CloseConnection();
                }
                return dt;
            }
            catch (Exception)
            { }
            return dt;
        }

        public DataTable B2CL()
        {
            DataTable dt = new DataTable();
            try
            {
                string errormsg = "";

                try
                {
                    db.connect();
                    SqlDataAdapter da;
                    DSB2B ds = new DSB2B();
                    SettingRepository setRepo = new SettingRepository();
                    Setting setData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (setData.ShowUserDefinedSeries == "Y")
                    {
                        da = new SqlDataAdapter("Select ci.BuyerTinNo as Gstin, cm.UserSeries as InvoiceNo,cm.billdate as InvoiceDate,cm.netamount as InvoiceValue," +
                                               "cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply,'N' as ReverseCharges,'Regular' as InvoiceType,'' as Ecommgstin," +
                                               "case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                               "case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                               " Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST " +
                                               " from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                               " inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID inner join CompanyInformation as com on ci.CompId=com.CompanyId " +
                                               "Where cm.billdate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and cm.billdate<='" + cm.ValidToDate(dtptomonth.Value.Date) +
                                               "' and (convert(varchar(10),cm.billdate,120) >= (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null) then convert(varchar(10),ci.GSTINAddedDate,120) end) " +
                                               " or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo is null or ci.BuyerTinNo='') then Convert(varchar(10),cm.billdate,120) end)) " +
                                               "and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' " +    ///and (ci.BuyerTinNo is null  or ci.BuyerTinNo='')
                                               "and ci.CompId='" + CommonMethod.CompId + "' and ci.StateCode!=com.StateCode and cm.netamount>250000 group by ci.BuyerTinNo, cm.UserSeries, cm.billdate,cm.netamount,cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode,ci.State order by cm.billdate", db.Connection);
                    }
                    else
                    {
                        da = new SqlDataAdapter("Select ci.BuyerTinNo as Gstin, cm.billno as InvoiceNo,cm.billdate as InvoiceDate,cm.netamount as InvoiceValue," +
                                               "cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply,'N' as ReverseCharges,'Regular' as InvoiceType,'' as Ecommgstin," +
                                               "case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                               "case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                               " Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST " +
                                                " from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                               " inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID inner join CompanyInformation as com on ci.CompId=com.CompanyId " +
                                               "Where cm.billdate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and cm.billdate<='" + cm.ValidToDate(dtptomonth.Value.Date) +
                                               "' and (convert(varchar(10),cm.billdate,120) >= (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null) then convert(varchar(10),ci.GSTINAddedDate,120) end) " +
                                               " or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo is null or ci.BuyerTinNo='') then Convert(varchar(10),cm.billdate,120) end)) " +
                                               "and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' " +    ///and (ci.BuyerTinNo is null  or ci.BuyerTinNo='')
                                               "and ci.CompId='" + CommonMethod.CompId + "' and ci.StateCode!=com.StateCode and cm.netamount>250000 group by ci.BuyerTinNo, cm.billno, cm.billdate,cm.netamount,cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode,ci.State order by cm.billdate", db.Connection);
                    }
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "B2BTransaction");
                    dt = ds.Tables["B2BTransaction"];
                    db.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show(errormsg, "B2CL Report");
                    db.CloseConnection();
                }
                return dt;
            }
            catch (Exception)
            { }
            return dt;
        }

        public DataTable B2CS()
        {
            DataTable dt = new DataTable();
            try
            {
                string errormsg = "";

                try
                {
                    db.connect();
                    SqlDataAdapter da;
                    DSB2B ds = new DSB2B();
                    da = new SqlDataAdapter("Select cast(ci.Statecode as varchar(5))+'-'+ci.State as Placeofsupply,'N' as ReverseCharges,'Regular' as InvoiceType," +
                                           "'' as Ecommgstin,case when (cd.sgstpercent is null or cd.sgstpercent=0) then cd.igstpercent else (cd.sgstpercent+cd.cgstpercent) end as GstRate," +
                                           " case when (cd.sgstpercent is null or cd.sgstpercent=0) then Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.IGstAmt,0)),0) else Isnull(sum(Isnull(cd.Totalamount,0)),0) - Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt,0)),0) end as TaxableValue," +
                                           " Isnull(sum(Isnull(cd.cessamt,0)),0) as CessAmt,'',Isnull(sum(Isnull(cd.IGstAmt,0)),0) as IGST,Isnull(sum(Isnull(cd.CGstAmt,0)),0) as CGST,Isnull(sum(Isnull(cd.SGstAmt,0)),0) as SGST " +
                                           " from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno " +
                                           " inner join CustomerInformation as ci on cm.CustomerId=ci.CustomerID inner join CompanyInformation as com on ci.CompId=com.CompanyId " +
                                           " Where cm.billdate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and cm.billdate<='" + cm.ValidToDate(dtptomonth.Value.Date) +
                                           "' and (convert(varchar(10),cm.billdate,120) < (case when (ci.BuyerTinNo!='' and ci.GSTINAddedDate is not null) then convert(varchar(10),ci.GSTINAddedDate,120) end) " +
                                           "or convert(varchar(10),cm.billdate,120) = (case when (ci.BuyerTinNo is null or ci.BuyerTinNo='') then convert(varchar(10),cm.billdate,120) end)) " +
                                           " and cm.TransactionYear='" + CommonMethod.TransactionYear +
                                           "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' and ci.CompId='" + CommonMethod.CompId +    //and (ci.BuyerTinNo is null or ci.BuyerTinNo='')
                                           "' and ((ci.StateCode!=com.StateCode and cm.netamount<250000) or ci.StateCode=com.StateCode) group by cd.sgstpercent,cd.cgstpercent,cd.igstpercent,ci.Statecode," +
                                           "ci.State order by ci.Statecode", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "B2BTransaction");
                    dt = ds.Tables["B2BTransaction"];
                    db.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show(errormsg, "B2CS Report");
                    db.CloseConnection();
                }
                return dt;
            }
            catch (Exception)
            { }
            return dt;
        }

        public DataTable HSN()
        {
            DataTable dt = new DataTable();
            try
            {
                string errormsg = "";

                try
                {
                    db.connect();
                    SqlDataAdapter da;
                    DSHSN ds = new DSHSN();
                    da = new SqlDataAdapter("Select pd.HSNCode as HSN,LEFT(pd.ProductName,30) As Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(cd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(cd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(cd.TotalAmount,0)),0)- Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt+cd.IGstAmt,0)),0) as TaxableValue,Isnull(Sum(Isnull(cd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(cd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(cd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(cd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno Where cm.billdate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and cm.billdate<='" + cm.ValidToDate(dtptomonth.Value.Date) + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' and cd.ProductId=pd.ProductName and cd.CompanyName=pd.ManufactureCompany and cd.Unit=pd.Uniti and pd.Uniti=un.UnitName group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "HSNData");
                    dt = ds.Tables["HSNData"];
                    db.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show(errormsg, "HSN Report");
                    db.CloseConnection();
                }
                return dt;
            }
            catch (Exception)
            { }
            return dt;
        }

        public DataTable CDNR()
        {
            DataTable dt = new DataTable();
            try
            {
                string errormsg = "";

                int billcustomerid = 0;
                try
                {
                    db.connect();
                    DSCDNR ds = new DSCDNR();
                    DataTable dtdetail = db.Gettable("SELECT  CustomerId,BillId,BillDate,Type FROM [RemainingPayment] where (Type='BillReturn' or Type='credit Note' or Type='Debit Note')" +
                                        " and CreateDate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and CreateDate<='" + cm.ValidToDate(dtptomonth.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and BillId like 'T%'");
                    if (dtdetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtdetail.Rows.Count; i++)
                        {
                            string gstinno, billno, VoucherNo, DType = "", Reason = "", Placeofsupply = "", PreGst = "";
                            DateTime billdate, Voucherdate;
                            decimal VoucherValue = 0, gstrate, taxableamt = 0, cessamt;
                            System.Windows.Forms.Application.DoEvents();
                            billcustomerid = Convert.ToInt32(dtdetail.Rows[i][0]);
                            DataTable dtcust = db.Gettable("select cust.state,cust.statecode,comp.statecode,cust.buyertinno from customerinformation as cust join companyinformation as comp on cust.compid=comp.companyid where comp.companyid='" + CommonMethod.CompId + "' and cust.buyertinno!='' and cust.customerid=" + billcustomerid);
                            if (dtcust.Rows.Count > 0)
                            {
                                errormsg = "Error on getting quantity details having bill no " + dtdetail.Rows[i][1].ToString();
                                DataTable dtBill = new DataTable();
                                if (dtdetail.Rows[i][3].ToString() == "BillReturn")
                                    dtBill = db.Gettable("Select cm.GoodReturnNo,cm.GoodReturnDate,cd.Amount,case when (cd.SGstPercent is null or cd.SGstPercent=0) then cd.IGstPercent else (cd.SGstPercent+cd.CGstPercent) end as gst,case when cd.CessAmt is null then 0 else cd.CessAmt end,cd.TotalAmount,cd.Reason from GoodReturnMaster as cm join GoodReturnDetail as cd on cm.Id=cd.GoodReturnNo where cm.BillNo='" + dtdetail.Rows[i][1].ToString() + "'");
                                else
                                    dtBill = db.Gettable("Select cm.CustCreditDebitNo,cm.CustCreditDebitDate,cd.Amount,case when (cd.SGstPercent is null or cd.SGstPercent=0) then cd.IGstPercent else (cd.SGstPercent+cd.CGstPercent) end as gst,case when cd.CessAmt is null then 0 else cd.CessAmt end,cd.TotalAmount,cd.Reason from CustCreditDebitMaster as cm join CustCreditDebitDetail as cd on cm.Id=cd.CustCreditDebitNo where cm.BillNo='" + dtdetail.Rows[i][1].ToString() + "' and cm.CDType='" + dtdetail.Rows[i][3].ToString() + "'");
                                if (dtBill.Rows.Count > 0)
                                {
                                    gstinno = dtcust.Rows[0][3].ToString();
                                    billno = dtdetail.Rows[i][1].ToString();
                                    billdate = Convert.ToDateTime(dtdetail.Rows[i][2].ToString());
                                    VoucherNo = dtBill.Rows[0][0].ToString();
                                    Voucherdate = Convert.ToDateTime(dtBill.Rows[0][1].ToString());
                                    if (dtdetail.Rows[i][3].ToString() == "Debit Note")
                                        DType = "D";
                                    else
                                        DType = "C";
                                    Reason = dtBill.Rows[0][6].ToString();
                                    Placeofsupply = dtcust.Rows[0][1].ToString() + '-' + dtcust.Rows[0][0].ToString();
                                    VoucherValue = Convert.ToDecimal(dtBill.Rows[0][5].ToString());
                                    gstrate = Convert.ToDecimal(dtBill.Rows[0][3].ToString());
                                    taxableamt = Convert.ToDecimal(dtBill.Rows[0][2].ToString());
                                    cessamt = Convert.ToDecimal(dtBill.Rows[0][4].ToString());
                                    PreGst = "N";

                                    dt = ds.Tables["CDNRTransaction"];
                                    dt.Rows.Add(new object[13] { gstinno, billno, billdate, VoucherNo, Voucherdate, DType, Reason, Placeofsupply, VoucherValue, gstrate, taxableamt, cessamt, PreGst });
                                }
                            }
                        }
                    }
                    db.CloseConnection();
                    ds.Dispose();
                    return dt;
                }
                catch (Exception)
                {
                    MessageBox.Show(errormsg, "CDNR Report");
                    db.CloseConnection();
                }
                return dt;
            }
            catch (Exception)
            { }
            return dt;
        }

        public DataTable CDNUR()
        {
            DataTable dt = new DataTable();
            try
            {
                string errormsg = "";
                int billcustomerid = 0;
                try
                {
                    db.connect();
                    DSCDNUR ds = new DSCDNUR();
                    DataTable dtdetail = db.Gettable("SELECT  CustomerId,BillId,BillDate,Type FROM [RemainingPayment] where (Type='BillReturn' or Type='credit Note' or Type='Debit Note')" +
                                        " and CreateDate>='" + cm.ValidFromDate(dtpMonth.Value.Date) + "' and CreateDate<='" + cm.ValidToDate(dtptomonth.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and BillId like 'T%'");
                    if (dtdetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtdetail.Rows.Count; i++)
                        {
                            string gstinno, billno, VoucherNo, DType = "", Reason = "", Placeofsupply = "", PreGst = "";
                            DateTime billdate, Voucherdate;
                            decimal VoucherValue = 0, gstrate, taxableamt = 0, cessamt;
                            System.Windows.Forms.Application.DoEvents();
                            billcustomerid = Convert.ToInt32(dtdetail.Rows[i][0]);
                            DataTable dtcust = db.Gettable("select cust.state,cust.statecode,comp.statecode,cust.buyertinno from customerinformation as cust join companyinformation as comp on cust.compid=comp.companyid where comp.companyid='" + CommonMethod.CompId + "' and cust.buyertinno='' and cust.customerid=" + billcustomerid);
                            if (dtcust.Rows.Count > 0)
                            {
                                errormsg = "Error on getting quantity details having bill no " + dtdetail.Rows[i][1].ToString();
                                DataTable dtBill = new DataTable();
                                if (dtdetail.Rows[i][3].ToString() == "BillReturn")
                                    dtBill = db.Gettable("Select cm.GoodReturnNo,cm.GoodReturnDate,cd.Amount,case when (cd.SGstPercent is null or cd.SGstPercent=0) then cd.IGstPercent else (cd.SGstPercent+cd.CGstPercent) end as gst,case when cd.CessAmt is null then 0 else cd.CessAmt end,cd.TotalAmount,cd.Reason from GoodReturnMaster as cm join GoodReturnDetail as cd on cm.Id=cd.GoodReturnNo where cm.BillNo='" + dtdetail.Rows[i][1].ToString() + "'");
                                else
                                    dtBill = db.Gettable("Select cm.CustCreditDebitNo,cm.CustCreditDebitDate,cd.Amount,case when (cd.SGstPercent is null or cd.SGstPercent=0) then cd.IGstPercent else (cd.SGstPercent+cd.CGstPercent) end as gst,case when cd.CessAmt is null then 0 else cd.CessAmt end,cd.TotalAmount,cd.Reason from CustCreditDebitMaster as cm join CustCreditDebitDetail as cd on cm.Id=cd.CustCreditDebitNo where cm.BillNo='" + dtdetail.Rows[i][1].ToString() + "' and cm.CDType='" + dtdetail.Rows[i][3].ToString() + "'");
                                if (dtBill.Rows.Count > 0)
                                {
                                    gstinno = "b2c";
                                    billno = dtdetail.Rows[i][1].ToString();
                                    billdate = Convert.ToDateTime(dtdetail.Rows[i][2].ToString());
                                    VoucherNo = dtBill.Rows[0][0].ToString();
                                    Voucherdate = Convert.ToDateTime(dtBill.Rows[0][1].ToString());
                                    if (dtdetail.Rows[i][3].ToString() == "Debit Note")
                                        DType = "D";
                                    else
                                        DType = "C";
                                    Reason = dtBill.Rows[0][6].ToString();
                                    Placeofsupply = dtcust.Rows[0][1].ToString() + '-' + dtcust.Rows[0][0].ToString();
                                    VoucherValue = Convert.ToDecimal(dtBill.Rows[0][5].ToString());
                                    gstrate = Convert.ToDecimal(dtBill.Rows[0][3].ToString());
                                    taxableamt = Convert.ToDecimal(dtBill.Rows[0][2].ToString());
                                    cessamt = Convert.ToDecimal(dtBill.Rows[0][4].ToString());
                                    PreGst = "N";
                                    dt = ds.Tables["CDNURTransaction"];
                                    dt.Rows.Add(new object[13] { gstinno, VoucherNo, Voucherdate, DType, billno, billdate, Reason, Placeofsupply, VoucherValue, gstrate, taxableamt, cessamt, PreGst });
                                }
                            }
                        }
                    }
                    db.CloseConnection();
                    ds.Dispose();
                    return dt;
                }
                catch (Exception)
                {
                    MessageBox.Show(errormsg, "CDNUR Report");
                    db.CloseConnection();
                }
                return dt;
            }
            catch (Exception)
            { }
            return dt;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnPrint.Text == "Print")
                {
                    RptSummaryDetails obj;
                    bool validreport = true;
                    obj = new RptSummaryDetails(dtpMonth.Value, dtptomonth.Value, ref validreport);
                    Cursor.Current = Cursors.Default;
                    if (validreport == true)
                    {
                        obj.ShowDialog();
                        obj.Dispose();
                    }
                }
                else if (btnPrint.Text == "Export CSV File")
                {
                    DataTable dt = new DataTable();
                    string[] drives = Environment.GetLogicalDrives();
                    string mnth = dtpMonth.Value.ToString("MMMM");
                    string year = dtpMonth.Value.ToString("yyyy");
                    string folderPath = @"" + drives[0] + "" + mnth + " " + year + "\\";
                    try
                    {
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                    }
                    catch (Exception)
                    { }
                    //Start b2b
                    string csv = string.Empty;
                    csv = "GSTIN/UIN of Recipient" + ',' + "Invoice Number" + ',' + "Invoice date" + ',' + "Invoice Value" + ',' + "Place Of Supply" + ',' + "Reverse Charge" + ',' + "Invoice Type" + ',' + "E-Commerce GSTIN" + ',' + "Rate" + ',' + "Taxable Value" + ',' + "Cess Amount" + ',';
                    csv += "\r\n";
                    dt = B2B();
                    string data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 10)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                csv += data.ToString().Replace(",", ";") + ',';
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "b2b.csv", csv);
                    //End b2b

                    //Start b2cl
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "Invoice Number" + ',' + "Invoice date" + ',' + "Invoice Value" + ',' + "Place Of Supply" + ',' + "Rate" + ',' + "Taxable Value" + ',' + "Cess Amount" + ',' + "E-Commerce GSTIN" + ',';
                    csv += "\r\n";
                    dt = B2CL();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if ((j >= 1 && j < 5) || j >= 8)
                                {
                                    if (j == 2)
                                    {
                                        DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                        data = dtb2b.ToString("dd-MMM-yy");
                                    }
                                    else if (j == 10)
                                    {
                                        if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                            data = "";
                                        else
                                            data = dt.Rows[i].ItemArray[j].ToString();
                                    }
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                    csv += data.ToString().Replace(",", ";") + ',';
                                }
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "b2cl.csv", csv);
                    //End b2cl

                    //Start b2cs
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "Type" + ',' + "Place Of Supply" + ',' + "Rate" + ',' + "Taxable Value" + ',' + "Cess Amount" + ',' + "E-Commerce GSTIN" + ',';
                    csv += "\r\n";
                    dt = B2CS();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 0)
                                    data = "OE";
                                else if (j == 10)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else if (j == 4 || (j >= 8 && j < 11))
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "b2cs.csv", csv);
                    //End b2cs

                    //Start cdnr
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "GSTIN/UIN of Recipient" + ',' + "Invoice/Advance Receipt Number" + ',' + "Invoice/Advance Receipt date" + ',' + "Note/Refund Voucher Number" + ',' + "Note/Refund Voucher date" + ',' + "Document Type" + ',' + "Reason For Issuing document" + ',' + "Place Of Supply" + ',' + "Note/Refund Voucher Value" + ',' + "Rate" + ',' + "Taxable Value" + ',' + "Cess Amount" + ',' + "Pre GST" + ',';
                    csv += "\r\n";
                    dt = CDNR();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 4)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();

                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "cdnr.csv", csv);
                    //End cdnr

                    //Start cdnur
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "UR Type" + ',' + "Note/Refund Voucher Number" + ',' + "Note/Refund Voucher date" + ',' + "Document Type" + ',' + "Invoice/Advance Receipt Number" + ',' + "Invoice/Advance Receipt date" + ',' + "Reason For Issuing document" + ',' + "Place Of Supply" + ',' + "Note/Refund Voucher Value" + ',' + "Rate" + ',' + "Taxable Value" + ',' + "Cess Amount" + ',' + "Pre GST" + ',';
                    csv += "\r\n";
                    dt = CDNUR();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 5)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "cdnur.csv", csv);
                    //End cdnur

                    //Start exp
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "Export Type" + ',' + "Invoice Number" + ',' + "Invoice date" + ',' + "Invoice Value" + ',' + "Port Code" + ',' + "Shipping Bill Number" + ',' + "Shipping Bill Date" + ',' + "Rate" + ',' + "Taxable Value" + ',';
                    csv += "\r\n";
                    //dt = exp();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 5)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();

                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "exp.csv", csv);
                    //End exp

                    //Start at
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "Place Of Supply" + ',' + "Rate" + ',' + "Gross Advance Received" + ',' + "Cess Amount" + ',';
                    csv += "\r\n";
                    //dt = at();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 5)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();

                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "at.csv", csv);
                    //End at

                    //Start atadj
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "Place Of Supply" + ',' + "Rate" + ',' + "Gross Advance Adjusted" + ',' + "Cess Amount" + ',';
                    csv += "\r\n";
                    //dt = atadj();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 5)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "atadj.csv", csv);
                    //End atadj

                    //Start hsn
                    dt = new DataTable();
                    csv = string.Empty;
                    csv = "HSN" + ',' + "Description" + ',' + "UQC" + ',' + "Total Quantity" + ',' + "Total Value" + ',' + "Taxable Value" + ',' + "Integrated Tax Amount" + ',' + "Central Tax Amount" + ',' + "State/UT Tax Amount" + ',' + "Cess Amount" + ',';
                    csv += "\r\n";
                    dt = HSN();
                    data = null;
                    if (dt.Rows.Count > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = dt.Rows.Count;
                        progressBar1.Minimum = 0;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 6 || j == 7 || j == 8 || j == 9)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                if (data != null)
                                    csv += data.ToString().Replace(",", ";") + ',';
                                data = null;
                            }
                            csv += "\r\n";
                        }
                        progressBar1.Visible = false;
                    }
                    File.WriteAllText(folderPath + "hsn.csv", csv);
                    //End hsn
                    MessageBox.Show("CSV files created Successfully At" + " " + folderPath);
                }
                else if (btnPrint.Text == "Export EXCEL File")
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application excel;
                        Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                        Excel1.Sheets xlSheets = null;
                        Excel1.Worksheet xlWorksheet = null;
                        object misValue = System.Reflection.Missing.Value;
                        excel = new Microsoft.Office.Interop.Excel.Application();
                        excel.Visible = false;
                        excel.DisplayAlerts = false;
                        excelworkBook = excel.Workbooks.Add(Type.Missing);
                        xlSheets = excelworkBook.Sheets;
                        DataTable dt = new DataTable();

                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "Help Instruction";
                        //Start B2B
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[2], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "b2b";
                        //xlWorksheet.Columns.AutoFit();
                        progressBar1.Visible = true;
                        dt = B2B();
                        int m = 5;
                        string data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Maximum = dt.Rows.Count;
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 10)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }
                        Excel1.Range RangeB2Bd5 = xlWorksheet.get_Range("D5:D" + m, misValue);
                        RangeB2Bd5.NumberFormat = "0.00";

                        Excel1.Range RangeB2Bi5 = xlWorksheet.get_Range("I5:I" + m, misValue);
                        RangeB2Bi5.NumberFormat = "0.00";

                        Excel1.Range RangeB2Bj5 = xlWorksheet.get_Range("J5:J" + m, misValue);
                        RangeB2Bj5.NumberFormat = "0.00";

                        Excel1.Range RangeB2Bk5 = xlWorksheet.get_Range("K5:K" + m, misValue);
                        RangeB2Bk5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeB2B1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeB2B1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For B2B(4)";

                        xlWorksheet.get_Range("A2", "K2").Font.Bold = true;
                        Excel1.Range RangeB2B2 = xlWorksheet.get_Range("A2:K2", misValue);
                        RangeB2B2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[2, 1] = "No. of Recipients";
                        xlWorksheet.Cells[2, 2] = "No. of Invoices";
                        xlWorksheet.Cells[2, 4] = "Total Invoice Value";
                        xlWorksheet.Cells[2, 10] = "Total Taxable Value";
                        xlWorksheet.Cells[2, 11] = "Total Cess";

                        xlWorksheet.get_Range("A3", "K3").Font.Bold = true;
                        Excel1.Range RangeB2B3 = xlWorksheet.get_Range("A3:K3", misValue);
                        RangeB2B3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeB2Bd3 = xlWorksheet.get_Range("D3:D3", misValue);
                        RangeB2Bd3.NumberFormat = "0.00";

                        Excel1.Range RangeB2Bj3 = xlWorksheet.get_Range("J3:J3", misValue);
                        RangeB2Bj3.NumberFormat = "0.00";

                        Excel1.Range RangeB2Bk3 = xlWorksheet.get_Range("K3:K3", misValue);
                        RangeB2Bk3.NumberFormat = "0.00";

                        if (dt.Rows.Count > 0)
                        {
                            string TGST = "";
                            DataTable UGST = dt.DefaultView.ToTable(true, "Gstin");
                            try
                            {
                                if (UGST != null)
                                    TGST = UGST.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                TGST = "0";
                            }
                            string TINVNo = "";
                            DataTable DINVNo = dt.DefaultView.ToTable(true, "InvoiceNo");
                            try
                            {
                                if (DINVNo != null)
                                    TINVNo = DINVNo.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                TINVNo = "0";
                            }
                            object TotalInvoiceValueB2B = "0"; ;
                            DataTable DINVNonVAlue = dt.DefaultView.ToTable(true, "InvoiceNo", "InvoiceValue");
                            try
                            {
                                if (DINVNonVAlue != null)
                                    TotalInvoiceValueB2B = DINVNonVAlue.Compute("Sum(InvoiceValue)", "");
                            }
                            catch (Exception)
                            {
                                TotalInvoiceValueB2B = "0";
                            }
                            object TotalTaxableValueB2B;
                            TotalTaxableValueB2B = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueB2B == null)
                                    TotalTaxableValueB2B = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueB2B = 0.00;
                            }
                            object TotalCessValueB2B;
                            TotalCessValueB2B = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueB2B == null || Convert.ToDecimal(TotalCessValueB2B) == 0)
                                    TotalCessValueB2B = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueB2B = 0.00;
                            }
                            xlWorksheet.Cells[3, 1] = UGST.Rows.Count.ToString();
                            xlWorksheet.Cells[3, 2] = TINVNo.ToString();
                            xlWorksheet.Cells[3, 4] = TotalInvoiceValueB2B.ToString();
                            xlWorksheet.Cells[3, 10] = TotalTaxableValueB2B.ToString();
                            xlWorksheet.Cells[3, 11] = TotalCessValueB2B.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 1] = 0;
                            xlWorksheet.Cells[3, 2] = 0;
                            xlWorksheet.Cells[3, 4] = 0.00;
                            xlWorksheet.Cells[3, 10] = 0.00;
                            xlWorksheet.Cells[3, 11] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "K4").Font.Bold = true;
                        Excel1.Range RangeB2B4 = xlWorksheet.get_Range("A4:K4", misValue);
                        RangeB2B4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;
                        xlWorksheet.Cells[4, 1] = "GSTIN/UIN of Recipient";
                        xlWorksheet.Cells[4, 2] = "Invoice Number";
                        xlWorksheet.Cells[4, 3] = "Invoice date";
                        xlWorksheet.Cells[4, 4] = "Invoice Value";
                        xlWorksheet.Cells[4, 5] = "Place Of Supply";
                        xlWorksheet.Cells[4, 6] = "Reverse Charge";
                        xlWorksheet.Cells[4, 7] = "Invoice Type";
                        xlWorksheet.Cells[4, 8] = "E-Commerce GSTIN";
                        xlWorksheet.Cells[4, 9] = "Rate";
                        xlWorksheet.Cells[4, 10] = "Taxable Value";
                        xlWorksheet.Cells[4, 11] = "Cess Amount";

                        xlWorksheet.get_Range("A1", "K1").EntireColumn.AutoFit();
                        dt.Dispose();
                        progressBar1.Visible = false;
                        //End B2B

                        //Start B2CL
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[3], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "b2cl";
                        dt = new DataTable();

                        progressBar1.Visible = true;
                        dt = B2CL();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if ((j >= 1 && j < 5) || j >= 8)
                                {
                                    if (j == 2)
                                    {
                                        DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                        data = dtb2b.ToString("dd-MMM-yy");
                                    }
                                    else if (j == 10)
                                    {
                                        if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                            data = "";
                                        else
                                            data = dt.Rows[i].ItemArray[j].ToString();
                                    }
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                    xlWorksheet.Cells[m, k] = data;
                                    k++;
                                }
                            }
                            m++;
                        }

                        Excel1.Range RangeB2CLc5 = xlWorksheet.get_Range("C5:C" + m, misValue);
                        RangeB2CLc5.NumberFormat = "0.00";

                        Excel1.Range RangeB2CLe5 = xlWorksheet.get_Range("E5:E" + m, misValue);
                        RangeB2CLe5.NumberFormat = "0.00";

                        Excel1.Range RangeB2CLf5 = xlWorksheet.get_Range("F5:F" + m, misValue);
                        RangeB2CLf5.NumberFormat = "0.00";

                        Excel1.Range RangeB2CLg5 = xlWorksheet.get_Range("G5:G" + m, misValue);
                        RangeB2CLg5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeB2CL1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeB2CL1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For B2CL(5)";

                        xlWorksheet.get_Range("A2", "H2").Font.Bold = true;
                        Excel1.Range RangeB2CL2 = xlWorksheet.get_Range("A2:H2", misValue);
                        RangeB2CL2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 1] = "No. of Invoices";
                        xlWorksheet.Cells[2, 3] = "Total Inv Value";
                        xlWorksheet.Cells[2, 6] = "Total Taxable Value";
                        xlWorksheet.Cells[2, 7] = "Total Cess";

                        xlWorksheet.get_Range("A3", "H3").Font.Bold = true;
                        Excel1.Range RangeB2CL3 = xlWorksheet.get_Range("A3:H3", misValue);
                        RangeB2CL3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeB2CLc3 = xlWorksheet.get_Range("C3:C3", misValue);
                        RangeB2CLc3.NumberFormat = "0.00";

                        Excel1.Range RangeB2CLf3 = xlWorksheet.get_Range("F3:F3", misValue);
                        RangeB2CLf3.NumberFormat = "0.00";

                        Excel1.Range RangeB2CLg3 = xlWorksheet.get_Range("G3:G3", misValue);
                        RangeB2CLg3.NumberFormat = "0.00";

                        if (dt.Rows.Count > 0)
                        {
                            string TINVNoB2CL = "";
                            DataTable DINVNoB2CL = dt.DefaultView.ToTable(true, "InvoiceNo");
                            try
                            {
                                if (DINVNoB2CL != null)
                                    TINVNoB2CL = DINVNoB2CL.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                TINVNoB2CL = "0";
                            }
                            object TotalInvoiceValueB2CL = "0";
                            DataTable DINVNonVAlue = dt.DefaultView.ToTable(true, "InvoiceNo", "InvoiceValue");
                            try
                            {
                                if (DINVNonVAlue != null)
                                    TotalInvoiceValueB2CL = DINVNonVAlue.Compute("Sum(InvoiceValue)", "");
                            }
                            catch (Exception)
                            {
                                TotalInvoiceValueB2CL = "0";
                            }
                            object TotalTaxableValueB2CL;
                            TotalTaxableValueB2CL = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueB2CL == null)
                                    TotalTaxableValueB2CL = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueB2CL = 0.00;
                            }
                            object TotalCessValueB2CL;
                            TotalCessValueB2CL = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueB2CL == null || Convert.ToDecimal(TotalCessValueB2CL) == 0)
                                    TotalCessValueB2CL = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueB2CL = 0.00;
                            }
                            xlWorksheet.Cells[3, 1] = TINVNoB2CL.ToString();
                            xlWorksheet.Cells[3, 3] = TotalInvoiceValueB2CL.ToString();
                            xlWorksheet.Cells[3, 6] = TotalTaxableValueB2CL.ToString();
                            xlWorksheet.Cells[3, 7] = TotalCessValueB2CL.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 1] = 0;
                            xlWorksheet.Cells[3, 3] = 0.00;
                            xlWorksheet.Cells[3, 6] = 0.00;
                            xlWorksheet.Cells[3, 7] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "H4").Font.Bold = true;
                        Excel1.Range RangeB2CL4 = xlWorksheet.get_Range("A4:H4", misValue);
                        RangeB2CL4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Invoice Number";
                        xlWorksheet.Cells[4, 2] = "Invoice date";
                        xlWorksheet.Cells[4, 3] = "Invoice Value";
                        xlWorksheet.Cells[4, 4] = "Place Of Supply";
                        xlWorksheet.Cells[4, 5] = "Rate";
                        xlWorksheet.Cells[4, 6] = "Taxable Value";
                        xlWorksheet.Cells[4, 7] = "Cess Amount";
                        xlWorksheet.Cells[4, 8] = "E-Commerce GSTIN";

                        xlWorksheet.get_Range("A1", "H1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End B2CL

                        //Start B2CS
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[4], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "b2cs";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        dt = B2CS();
                        m = 5;
                        data = null;

                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 0)
                                {
                                    data = "OE";
                                    xlWorksheet.Cells[m, k] = data;
                                    k++;
                                }
                                else if (j == 10)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();

                                    xlWorksheet.Cells[m, k] = data;
                                    k++;
                                }
                                else if (j == 4 || (j >= 8 && j < 11))
                                {
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                    xlWorksheet.Cells[m, k] = data;
                                    k++;
                                }
                            }
                            m++;
                        }

                        Excel1.Range RangeB2CSC5 = xlWorksheet.get_Range("C5:C" + m, misValue);
                        RangeB2CSC5.NumberFormat = "0.00";

                        Excel1.Range RangeB2CSD5 = xlWorksheet.get_Range("D5:D" + m, misValue);
                        RangeB2CSD5.NumberFormat = "0.00";

                        Excel1.Range RangeB2CSe5 = xlWorksheet.get_Range("E5:E" + m, misValue);
                        RangeB2CSe5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeB2CS1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeB2CS1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For B2CS(7)";

                        xlWorksheet.get_Range("A2", "F2").Font.Bold = true;
                        Excel1.Range RangeB2CS2 = xlWorksheet.get_Range("A2:F2", misValue);
                        RangeB2CS2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 4] = "Total Taxable Value";
                        xlWorksheet.Cells[2, 5] = "Total Cess";

                        xlWorksheet.get_Range("A3", "F3").Font.Bold = true;
                        Excel1.Range RangeB2CS3 = xlWorksheet.get_Range("A3:F3", misValue);
                        RangeB2CS3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeB2CSc3 = xlWorksheet.get_Range("D3:D3", misValue);
                        RangeB2CSc3.NumberFormat = "0.00";

                        Excel1.Range RangeB2CSf3 = xlWorksheet.get_Range("E3:E3", misValue);
                        RangeB2CSf3.NumberFormat = "0.00";

                        if (dt.Rows.Count > 0)
                        {
                            object TotalTaxableValueB2CS;
                            TotalTaxableValueB2CS = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueB2CS == null)
                                    TotalTaxableValueB2CS = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueB2CS = 0.00;
                            }
                            object TotalCessValueB2CS;
                            TotalCessValueB2CS = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueB2CS == null || Convert.ToDecimal(TotalCessValueB2CS) == 0)
                                    TotalCessValueB2CS = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueB2CS = 0.00;
                            }
                            xlWorksheet.Cells[3, 4] = TotalTaxableValueB2CS.ToString();
                            xlWorksheet.Cells[3, 5] = TotalCessValueB2CS.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 4] = 0.00;
                            xlWorksheet.Cells[3, 5] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "F4").Font.Bold = true;
                        Excel1.Range RangeB2CS4 = xlWorksheet.get_Range("A4:F4", misValue);
                        RangeB2CS4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Type";
                        xlWorksheet.Cells[4, 2] = "Place Of Supply";
                        xlWorksheet.Cells[4, 3] = "Rate";
                        xlWorksheet.Cells[4, 4] = "Taxable Value";
                        xlWorksheet.Cells[4, 5] = "Cess Amount";
                        xlWorksheet.Cells[4, 6] = "E-Commerce GSTIN";

                        xlWorksheet.get_Range("A1", "F1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End B2CS

                        //Start CDNR
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[5], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "cdnr";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        dt = CDNR();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 4)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());

                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, k] = data;
                                k++;
                            }
                            m++;
                        }

                        Excel1.Range RangeCDNRI5 = xlWorksheet.get_Range("I5:I" + m, misValue);
                        RangeCDNRI5.NumberFormat = "0.00";

                        Excel1.Range RangeCDNRJ5 = xlWorksheet.get_Range("J5:J" + m, misValue);
                        RangeCDNRJ5.NumberFormat = "0.00";

                        Excel1.Range RangeCDNRK5 = xlWorksheet.get_Range("K5:K" + m, misValue);
                        RangeCDNRK5.NumberFormat = "0.00";

                        Excel1.Range RangeCDNRL5 = xlWorksheet.get_Range("L5:L" + m, misValue);
                        RangeCDNRL5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeCDNR1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeCDNR1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For CDNR(9B)";

                        xlWorksheet.get_Range("A2", "M2").Font.Bold = true;
                        Excel1.Range RangeCDNR2 = xlWorksheet.get_Range("A2:M2", misValue);
                        RangeCDNR2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 1] = "No. of Recipients";
                        xlWorksheet.Cells[2, 2] = "No. of Invoices";
                        xlWorksheet.Cells[2, 4] = "No. of Notes/Vouchers";
                        xlWorksheet.Cells[2, 9] = "Total Note/Refund Voucher Value";
                        xlWorksheet.Cells[2, 11] = "Total Taxable Value";
                        xlWorksheet.Cells[2, 12] = "Total Cess";

                        xlWorksheet.get_Range("A3", "M3").Font.Bold = true;
                        Excel1.Range RangeCDNR3 = xlWorksheet.get_Range("A3:M3", misValue);
                        RangeCDNR3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeCDNRI3 = xlWorksheet.get_Range("I3:I3", misValue);
                        RangeCDNRI3.NumberFormat = "0.00";

                        Excel1.Range RangeCDNRK3 = xlWorksheet.get_Range("K3:K3", misValue);
                        RangeCDNRK3.NumberFormat = "0.00";

                        Excel1.Range RangeCDNRL3 = xlWorksheet.get_Range("L3:L3", misValue);
                        RangeCDNRL3.NumberFormat = "0.00";
                        if (dt.Rows.Count > 0)
                        {
                            string TGST = "";
                            DataTable UGST = dt.DefaultView.ToTable(true, "Gstin");
                            try
                            {
                                if (UGST != null)
                                    TGST = UGST.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                TGST = "0";
                            }
                            string TINVNo = "";
                            DataTable DINVNo = dt.DefaultView.ToTable(true, "InvoiceNo");
                            try
                            {
                                if (DINVNo != null)
                                    TINVNo = DINVNo.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                TINVNo = "0";
                            }
                            string VTINVNo = "";
                            DataTable VDINVNo = dt.DefaultView.ToTable(false, "VoucherNo");
                            try
                            {
                                if (VDINVNo != null)
                                    VTINVNo = VDINVNo.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                VTINVNo = "0";
                            }
                            object TotalVoucherVAlueCDNR = "0"; ;
                            DataTable DINVNonVAlue = dt.DefaultView.ToTable(true, "VoucherNo", "VoucherValue");
                            try
                            {
                                if (DINVNonVAlue != null)
                                    TotalVoucherVAlueCDNR = DINVNonVAlue.Compute("Sum(VoucherValue)", "");
                            }
                            catch (Exception)
                            {
                                TotalVoucherVAlueCDNR = "0";
                            }
                            object TotalTaxableValueCDNR;
                            TotalTaxableValueCDNR = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueCDNR == null)
                                    TotalTaxableValueCDNR = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueCDNR = 0.00;
                            }
                            object TotalCessValueCDNR;
                            TotalCessValueCDNR = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueCDNR == null || Convert.ToDecimal(TotalCessValueCDNR) == 0)
                                    TotalCessValueCDNR = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueCDNR = 0.00;
                            }
                            xlWorksheet.Cells[3, 1] = UGST.Rows.Count.ToString();
                            xlWorksheet.Cells[3, 2] = TINVNo.ToString();
                            xlWorksheet.Cells[3, 4] = VTINVNo.ToString();
                            xlWorksheet.Cells[3, 9] = TotalVoucherVAlueCDNR.ToString();
                            xlWorksheet.Cells[3, 11] = TotalTaxableValueCDNR.ToString();
                            xlWorksheet.Cells[3, 12] = TotalCessValueCDNR.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 1] = 0;
                            xlWorksheet.Cells[3, 2] = 0;
                            xlWorksheet.Cells[3, 4] = 0;
                            xlWorksheet.Cells[3, 9] = 0.00;
                            xlWorksheet.Cells[3, 11] = 0.00;
                            xlWorksheet.Cells[3, 12] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "M4").Font.Bold = true;
                        Excel1.Range RangeCDNR4 = xlWorksheet.get_Range("A4:M4", misValue);
                        RangeCDNR4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "GSTIN/UIN of Recipient";
                        xlWorksheet.Cells[4, 2] = "Invoice/Advance Receipt Number";
                        xlWorksheet.Cells[4, 3] = "Invoice/Advance Receipt date";
                        xlWorksheet.Cells[4, 4] = "Note/Refund Voucher Number";
                        xlWorksheet.Cells[4, 5] = "Note/Refund Voucher date";
                        xlWorksheet.Cells[4, 6] = "Document Type";
                        xlWorksheet.Cells[4, 7] = "Reason For Issuing document";
                        xlWorksheet.Cells[4, 8] = "Place Of Supply";
                        xlWorksheet.Cells[4, 9] = "Note/Refund Voucher Value";
                        xlWorksheet.Cells[4, 10] = "Rate";
                        xlWorksheet.Cells[4, 11] = "Taxable Value";
                        xlWorksheet.Cells[4, 12] = "Cess Amount";
                        xlWorksheet.Cells[4, 13] = "Pre GST";

                        xlWorksheet.get_Range("A1", "M1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End CDNR

                        //Start CDNUR
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[6], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "cdnur";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        dt = CDNUR();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 2 || j == 5)
                                {
                                    DateTime dtb2b = Convert.ToDateTime(dt.Rows[i].ItemArray[j].ToString());
                                    data = dtb2b.ToString("dd-MMM-yy");
                                }
                                else if (j == 11)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, k] = data;
                                k++;
                            }
                            m++;
                        }
                        Excel1.Range RangeCDNURI5 = xlWorksheet.get_Range("I5:I" + m, misValue);
                        RangeCDNURI5.NumberFormat = "0.00";

                        Excel1.Range RangeCDNURJ5 = xlWorksheet.get_Range("J5:J" + m, misValue);
                        RangeCDNURJ5.NumberFormat = "0.00";

                        Excel1.Range RangeCDNURK5 = xlWorksheet.get_Range("K5:K" + m, misValue);
                        RangeCDNURK5.NumberFormat = "0.00";

                        Excel1.Range RangeCDNURL5 = xlWorksheet.get_Range("L5:L" + m, misValue);
                        RangeCDNURL5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeCDNUR1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeCDNUR1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For CDNUR(9B)";

                        xlWorksheet.get_Range("A2", "M2").Font.Bold = true;
                        Excel1.Range RangeCDNUR2 = xlWorksheet.get_Range("A2:M2", misValue);
                        RangeCDNUR2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 2] = "No. of Notes/Vouchers";
                        xlWorksheet.Cells[2, 5] = "No. of Invoices";
                        xlWorksheet.Cells[2, 9] = "Total Note Value";
                        xlWorksheet.Cells[2, 11] = "Total Taxable Value";
                        xlWorksheet.Cells[2, 12] = "Total Cess";

                        xlWorksheet.get_Range("A3", "M3").Font.Bold = true;
                        Excel1.Range RangeCDNUR3 = xlWorksheet.get_Range("A3:M3", misValue);
                        RangeCDNUR3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeCDNURI3 = xlWorksheet.get_Range("I3:I3", misValue);
                        RangeCDNURI3.NumberFormat = "0.00";

                        Excel1.Range RangeCDNURK3 = xlWorksheet.get_Range("K3:K3", misValue);
                        RangeCDNURK3.NumberFormat = "0.00";

                        Excel1.Range RangeCDNURL3 = xlWorksheet.get_Range("L3:L3", misValue);
                        RangeCDNURL3.NumberFormat = "0.00";
                        if (dt.Rows.Count > 0)
                        {
                            string TINVNo = "";
                            DataTable DINVNo = dt.DefaultView.ToTable(true, "InvoiceNo");
                            try
                            {
                                if (DINVNo != null)
                                    TINVNo = DINVNo.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                TINVNo = "0";
                            }
                            string VTINVNo = "";
                            DataTable VDINVNo = dt.DefaultView.ToTable(false, "VoucherNo");
                            try
                            {
                                if (VDINVNo != null)
                                    VTINVNo = VDINVNo.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                VTINVNo = "0";
                            }
                            object TotalVoucherVAlueCDNR = "0"; ;
                            DataTable DINVNonVAlue = dt.DefaultView.ToTable(true, "VoucherNo", "VoucherValue");
                            try
                            {
                                if (DINVNonVAlue != null)
                                    TotalVoucherVAlueCDNR = DINVNonVAlue.Compute("Sum(VoucherValue)", "");
                            }
                            catch (Exception)
                            {
                                TotalVoucherVAlueCDNR = "0";
                            }
                            object TotalTaxableValueCDNR;
                            TotalTaxableValueCDNR = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueCDNR == null)
                                    TotalTaxableValueCDNR = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueCDNR = 0.00;
                            }
                            object TotalCessValueCDNR;
                            TotalCessValueCDNR = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueCDNR == null || Convert.ToDecimal(TotalCessValueCDNR) == 0)
                                    TotalCessValueCDNR = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueCDNR = 0.00;
                            }
                            xlWorksheet.Cells[3, 2] = TINVNo.ToString();
                            xlWorksheet.Cells[3, 5] = VTINVNo.ToString();
                            xlWorksheet.Cells[3, 9] = TotalVoucherVAlueCDNR.ToString();
                            xlWorksheet.Cells[3, 11] = TotalTaxableValueCDNR.ToString();
                            xlWorksheet.Cells[3, 12] = TotalCessValueCDNR.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 2] = 0;
                            xlWorksheet.Cells[3, 5] = 0;
                            xlWorksheet.Cells[3, 9] = 0.00;
                            xlWorksheet.Cells[3, 11] = 0.00;
                            xlWorksheet.Cells[3, 12] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "M4").Font.Bold = true;
                        Excel1.Range RangeCDNUR4 = xlWorksheet.get_Range("A4:M4", misValue);
                        RangeCDNUR4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "UR Type";
                        xlWorksheet.Cells[4, 2] = "Note/Refund Voucher Number";
                        xlWorksheet.Cells[4, 3] = "Note/Refund Voucher date";
                        xlWorksheet.Cells[4, 4] = "Document Type";
                        xlWorksheet.Cells[4, 5] = "Invoice/Advance Receipt Number";
                        xlWorksheet.Cells[4, 6] = "Invoice/Advance Receipt date";
                        xlWorksheet.Cells[4, 7] = "Reason For Issuing document";
                        xlWorksheet.Cells[4, 8] = "Place Of Supply";
                        xlWorksheet.Cells[4, 9] = "Note/Refund Voucher Value";
                        xlWorksheet.Cells[4, 10] = "Rate";
                        xlWorksheet.Cells[4, 11] = "Taxable Value";
                        xlWorksheet.Cells[4, 12] = "Cess Amount";
                        xlWorksheet.Cells[4, 13] = "Pre GST";

                        xlWorksheet.get_Range("A1", "M1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End CDNUR

                        //Start EXP
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[7], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "exp";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        //dt = B2CS();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }

                        Excel1.Range RangeEXPD5 = xlWorksheet.get_Range("D5:D" + m, misValue);
                        RangeEXPD5.NumberFormat = "0.00";

                        Excel1.Range RangeEXPH5 = xlWorksheet.get_Range("H5:H" + m, misValue);
                        RangeEXPH5.NumberFormat = "0.00";

                        Excel1.Range RangeEXPI5 = xlWorksheet.get_Range("I5:I" + m, misValue);
                        RangeEXPI5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeEXP1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeEXP1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For EXP(6)";

                        xlWorksheet.get_Range("A2", "I2").Font.Bold = true;
                        Excel1.Range RangeEXP2 = xlWorksheet.get_Range("A2:I2", misValue);
                        RangeEXP2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 2] = "No. of Invoices";
                        xlWorksheet.Cells[2, 4] = "Total Invoice Value";
                        xlWorksheet.Cells[2, 6] = "No. of Shipping Bill";
                        xlWorksheet.Cells[2, 9] = "Total Taxable Value";

                        xlWorksheet.get_Range("A3", "I3").Font.Bold = true;
                        Excel1.Range RangeEXP3 = xlWorksheet.get_Range("A3:I3", misValue);
                        RangeEXP3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeEXPD3 = xlWorksheet.get_Range("D3:D3", misValue);
                        RangeEXPD3.NumberFormat = "0.00";

                        Excel1.Range RangeEXPI3 = xlWorksheet.get_Range("I3:I3", misValue);
                        RangeEXPI3.NumberFormat = "0.00";
                        if (dt.Rows.Count > 0)
                        {
                            object TotalTaxableValueEXP;
                            TotalTaxableValueEXP = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueEXP == null)
                                    TotalTaxableValueEXP = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueEXP = 0.00;
                            }
                            object TotalCessValueEXP;
                            TotalCessValueEXP = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueEXP == null)
                                    TotalCessValueEXP = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueEXP = 0.00;
                            }
                            xlWorksheet.Cells[3, 2] = TotalTaxableValueEXP.ToString();
                            xlWorksheet.Cells[3, 4] = TotalTaxableValueEXP.ToString();
                            xlWorksheet.Cells[3, 6] = TotalTaxableValueEXP.ToString();
                            xlWorksheet.Cells[3, 9] = TotalCessValueEXP.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 2] = 0;
                            xlWorksheet.Cells[3, 4] = 0.00;
                            xlWorksheet.Cells[3, 6] = 0;
                            xlWorksheet.Cells[3, 9] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "I4").Font.Bold = true;
                        Excel1.Range RangeEXP4 = xlWorksheet.get_Range("A4:I4", misValue);
                        RangeEXP4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Export Type";
                        xlWorksheet.Cells[4, 2] = "Invoice Number";
                        xlWorksheet.Cells[4, 3] = "Invoice date";
                        xlWorksheet.Cells[4, 4] = "Invoice Value";
                        xlWorksheet.Cells[4, 5] = "Port Code";
                        xlWorksheet.Cells[4, 6] = "Shipping Bill Number";
                        xlWorksheet.Cells[4, 7] = "Shipping Bill Date";
                        xlWorksheet.Cells[4, 8] = "Rate";
                        xlWorksheet.Cells[4, 9] = "Taxable Value";

                        xlWorksheet.get_Range("A1", "I1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End EXP

                        //Start AT
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[8], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "at";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        //dt = B2CS();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            //int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }
                        Excel1.Range RangeATB5 = xlWorksheet.get_Range("B5:B" + m, misValue);
                        RangeATB5.NumberFormat = "0.00";

                        Excel1.Range RangeATC5 = xlWorksheet.get_Range("C5:C" + m, misValue);
                        RangeATC5.NumberFormat = "0.00";

                        Excel1.Range RangeATD5 = xlWorksheet.get_Range("D5:D" + m, misValue);
                        RangeATD5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeAT1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeAT1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For Advance Received (11B)";

                        xlWorksheet.get_Range("A2", "D2").Font.Bold = true;
                        Excel1.Range RangeAT2 = xlWorksheet.get_Range("A2:D2", misValue);
                        RangeAT2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 3] = "Total Advance Received";
                        xlWorksheet.Cells[2, 4] = "Total Cess";

                        xlWorksheet.get_Range("A3", "D3").Font.Bold = true;
                        Excel1.Range RangeAT3 = xlWorksheet.get_Range("A3:D3", misValue);
                        RangeAT3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeATC3 = xlWorksheet.get_Range("C3:C3", misValue);
                        RangeATC3.NumberFormat = "0.00";
                        Excel1.Range RangeATD3 = xlWorksheet.get_Range("D3:D3", misValue);
                        RangeATD3.NumberFormat = "0.00";
                        if (dt.Rows.Count > 0)
                        {
                            object TotalTaxableValueAT;
                            TotalTaxableValueAT = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueAT == null)
                                    TotalTaxableValueAT = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueAT = 0.00;
                            }
                            object TotalCessValueAT;
                            TotalCessValueAT = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueAT == null || Convert.ToDecimal(TotalCessValueAT) == 0)
                                    TotalCessValueAT = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueAT = 0.00;
                            }
                            xlWorksheet.Cells[3, 3] = TotalTaxableValueAT.ToString();
                            xlWorksheet.Cells[3, 4] = TotalTaxableValueAT.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 3] = 0.00;
                            xlWorksheet.Cells[3, 4] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "D4").Font.Bold = true;
                        Excel1.Range RangeAT4 = xlWorksheet.get_Range("A4:D4", misValue);
                        RangeAT4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Place Of Supply";
                        xlWorksheet.Cells[4, 2] = "Rate";
                        xlWorksheet.Cells[4, 3] = "Gross Advance Received";
                        xlWorksheet.Cells[4, 4] = "Cess Amount";

                        xlWorksheet.get_Range("A1", "D1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End AT

                        //Start ATADJ
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[9], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "atadj";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        //dt = B2CS();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            //int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }
                        Excel1.Range RangeATADJB5 = xlWorksheet.get_Range("B5:B" + m, misValue);
                        RangeATADJB5.NumberFormat = "0.00";

                        Excel1.Range RangeATADJC5 = xlWorksheet.get_Range("C5:C" + m, misValue);
                        RangeATADJC5.NumberFormat = "0.00";

                        Excel1.Range RangeATADJD5 = xlWorksheet.get_Range("D5:D" + m, misValue);
                        RangeATADJD5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeATADJ1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeATADJ1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For Advance Adjusted (11B)";

                        xlWorksheet.get_Range("A2", "D2").Font.Bold = true;
                        Excel1.Range RangeATADJ2 = xlWorksheet.get_Range("A2:D2", misValue);
                        RangeATADJ2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 3] = "Total Advance Adjusted";
                        xlWorksheet.Cells[2, 4] = "Total Cess";

                        xlWorksheet.get_Range("A3", "D3").Font.Bold = true;
                        Excel1.Range RangeATADJ3 = xlWorksheet.get_Range("A3:D3", misValue);
                        RangeATADJ3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeATADJC3 = xlWorksheet.get_Range("C3:C3", misValue);
                        RangeATADJC3.NumberFormat = "0.00";
                        Excel1.Range RangeATADJD3 = xlWorksheet.get_Range("D3:D3", misValue);
                        RangeATADJD3.NumberFormat = "0.00";
                        if (dt.Rows.Count > 0)
                        {
                            object TotalTaxableValueATADJ;
                            TotalTaxableValueATADJ = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueATADJ == null)
                                    TotalTaxableValueATADJ = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueATADJ = 0.00;
                            }
                            object TotalCessValueATADJ;
                            TotalCessValueATADJ = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueATADJ == null || Convert.ToDecimal(TotalCessValueATADJ) == 0)
                                    TotalCessValueATADJ = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueATADJ = 0.00;
                            }
                            xlWorksheet.Cells[3, 3] = TotalTaxableValueATADJ.ToString();
                            xlWorksheet.Cells[3, 4] = TotalTaxableValueATADJ.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 3] = 0.00;
                            xlWorksheet.Cells[3, 4] = 0.00;
                        }

                        xlWorksheet.get_Range("A4", "D4").Font.Bold = true;
                        Excel1.Range RangeATADJ4 = xlWorksheet.get_Range("A4:D4", misValue);
                        RangeATADJ4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Place Of Supply";
                        xlWorksheet.Cells[4, 2] = "Rate";
                        xlWorksheet.Cells[4, 3] = "Gross Advance Adjusted";
                        xlWorksheet.Cells[4, 4] = "Cess Amount";

                        xlWorksheet.get_Range("A1", "D1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End ATADJ

                        //Start EXEMP
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[10], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "exemp";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        //dt = B2CS();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeEXEMP1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeEXEMP1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For Nil rated, exempted and non GST outward supplies (8)";

                        xlWorksheet.get_Range("A2", "D2").Font.Bold = true;
                        Excel1.Range RangeEXEMP2 = xlWorksheet.get_Range("A2:D2", misValue);
                        RangeEXEMP2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 2] = "Total Nil Rated Supplies";
                        xlWorksheet.Cells[2, 3] = "Total Exempted Supplies";
                        xlWorksheet.Cells[2, 4] = "Total Non-GST Supplies";

                        xlWorksheet.get_Range("A3", "D3").Font.Bold = true;
                        Excel1.Range RangeEXEMP3 = xlWorksheet.get_Range("A3:D3", misValue);
                        RangeEXEMP3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeEXEMPB3 = xlWorksheet.get_Range("B3:B3", misValue);
                        RangeEXEMPB3.NumberFormat = "0.00";
                        Excel1.Range RangeEXEMPC3 = xlWorksheet.get_Range("C3:C3", misValue);
                        RangeEXEMPC3.NumberFormat = "0.00";
                        Excel1.Range RangeEXEMPD3 = xlWorksheet.get_Range("D3:D3", misValue);
                        RangeEXEMPD3.NumberFormat = "0.00";
                        if (dt.Rows.Count > 0)
                        {
                            object TotalTaxableValueEXEMP;
                            TotalTaxableValueEXEMP = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueEXEMP == null)
                                    TotalTaxableValueEXEMP = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueEXEMP = 0.00;
                            }
                            object TotalCessValueEXEMP;
                            TotalCessValueEXEMP = dt.Compute("Sum(CessAmt)", "");
                            try
                            {
                                if (TotalCessValueEXEMP == null)
                                    TotalCessValueEXEMP = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueEXEMP = 0.00;
                            }
                            xlWorksheet.Cells[3, 2] = TotalTaxableValueEXEMP.ToString();
                            xlWorksheet.Cells[3, 3] = TotalTaxableValueEXEMP.ToString();
                            xlWorksheet.Cells[3, 4] = TotalTaxableValueEXEMP.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 2] = 0.00;
                            xlWorksheet.Cells[3, 3] = 0.00;
                            xlWorksheet.Cells[3, 4] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "D4").Font.Bold = true;
                        Excel1.Range RangeEXEMP4 = xlWorksheet.get_Range("A4:D4", misValue);
                        RangeEXEMP4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Description";
                        xlWorksheet.Cells[4, 2] = "Nil Rated Supplies";
                        xlWorksheet.Cells[4, 3] = "Exempted (other than nil rated/non GST supply )";
                        xlWorksheet.Cells[4, 4] = "Non-GST supplies";

                        xlWorksheet.get_Range("A1", "D1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End EXEMP

                        //Start HSN
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[11], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "hsn";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        dt = HSN();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                if (j == 6 || j == 7 || j == 8 || j == 9)
                                {
                                    if (Convert.ToDecimal(dt.Rows[i].ItemArray[j]) == 0)
                                        data = "";
                                    else
                                        data = dt.Rows[i].ItemArray[j].ToString();
                                }
                                else
                                    data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }
                        Excel1.Range RangeHSNE5 = xlWorksheet.get_Range("E5:E" + m, misValue);
                        RangeHSNE5.NumberFormat = "0.00";

                        Excel1.Range RangeHSNF5 = xlWorksheet.get_Range("F5:F" + m, misValue);
                        RangeHSNF5.NumberFormat = "0.00";

                        Excel1.Range RangeHSNG5 = xlWorksheet.get_Range("G5:G" + m, misValue);
                        RangeHSNG5.NumberFormat = "0.00";

                        Excel1.Range RangeHSNH5 = xlWorksheet.get_Range("H5:H" + m, misValue);
                        RangeHSNH5.NumberFormat = "0.00";

                        Excel1.Range RangeHSNI5 = xlWorksheet.get_Range("I5:I" + m, misValue);
                        RangeHSNI5.NumberFormat = "0.00";

                        Excel1.Range RangeHSNJ5 = xlWorksheet.get_Range("J5:J" + m, misValue);
                        RangeHSNJ5.NumberFormat = "0.00";

                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range RangeHSN1 = xlWorksheet.get_Range("A1:A1", misValue);
                        RangeHSN1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary For HSN(12)";

                        xlWorksheet.get_Range("A2", "J2").Font.Bold = true;
                        Excel1.Range RangeHSN2 = xlWorksheet.get_Range("A2:J2", misValue);
                        RangeHSN2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 1] = "No. of HSN";
                        xlWorksheet.Cells[2, 5] = "Total Value";
                        xlWorksheet.Cells[2, 6] = "Total Taxable Value";
                        xlWorksheet.Cells[2, 7] = "Total Integrated Tax";
                        xlWorksheet.Cells[2, 8] = "Total Central Tax";
                        xlWorksheet.Cells[2, 9] = "Total State/UT Tax";
                        xlWorksheet.Cells[2, 10] = "Total Cess";

                        xlWorksheet.get_Range("A3", "J3").Font.Bold = true;
                        Excel1.Range RangeHSNS3 = xlWorksheet.get_Range("A3:J3", misValue);
                        RangeHSNS3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        Excel1.Range RangeHSNE3 = xlWorksheet.get_Range("E3:E3", misValue);
                        RangeHSNE3.NumberFormat = "0.00";

                        Excel1.Range RangeHSNF3 = xlWorksheet.get_Range("F3:F3", misValue);
                        RangeHSNF3.NumberFormat = "0.00";

                        Excel1.Range RangeHSNG3 = xlWorksheet.get_Range("G3:G3", misValue);
                        RangeHSNG3.NumberFormat = "0.00";

                        Excel1.Range RangeHSNH3 = xlWorksheet.get_Range("H3:H3", misValue);
                        RangeHSNH3.NumberFormat = "0.00";

                        Excel1.Range RangeHSNI3 = xlWorksheet.get_Range("I3:I3", misValue);
                        RangeHSNI3.NumberFormat = "0.00";

                        Excel1.Range RangeHSNJ3 = xlWorksheet.get_Range("J3:J3", misValue);
                        RangeHSNJ3.NumberFormat = "0.00";

                        if (dt.Rows.Count > 0)
                        {
                            string THSN = "";
                            DataTable DHSN = dt.DefaultView.ToTable(true, "HSN");
                            try
                            {
                                if (DHSN != null)
                                    THSN = DHSN.Rows.Count.ToString();
                            }
                            catch (Exception)
                            {
                                THSN = "0";
                            }
                            object TotalValueHSN;
                            TotalValueHSN = dt.Compute("Sum(TotalValue)", "");
                            try
                            {
                                if (TotalValueHSN == null)
                                    TotalValueHSN = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalValueHSN = 0.00;
                            }
                            object TotalTaxableValueHSN;
                            TotalTaxableValueHSN = dt.Compute("Sum(TaxableValue)", "");
                            try
                            {
                                if (TotalTaxableValueHSN == null)
                                    TotalTaxableValueHSN = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalTaxableValueHSN = 0.00;
                            }
                            object TotalITAmtHSN;
                            TotalITAmtHSN = dt.Compute("Sum(IntegratedTaxAmt)", "");
                            try
                            {
                                if (TotalITAmtHSN == null || Convert.ToDecimal(TotalITAmtHSN) == 0)
                                    TotalITAmtHSN = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalITAmtHSN = 0.00;
                            }
                            object TotalCTAmtHSN;
                            TotalCTAmtHSN = dt.Compute("Sum(CentralTaxAmt)", "");
                            try
                            {
                                if (TotalCTAmtHSN == null || Convert.ToDecimal(TotalCTAmtHSN) == 0)
                                    TotalCTAmtHSN = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCTAmtHSN = 0.00;
                            }
                            object TotalSTAmtHSN;
                            TotalSTAmtHSN = dt.Compute("Sum(StateTaxAmt)", "");
                            try
                            {
                                if (TotalSTAmtHSN == null || Convert.ToDecimal(TotalSTAmtHSN) == 0)
                                    TotalSTAmtHSN = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalSTAmtHSN = 0.00;
                            }
                            object TotalCessValueHSN;
                            TotalCessValueHSN = dt.Compute("Sum(TotalCess)", "");
                            try
                            {
                                if (TotalCessValueHSN == null || Convert.ToDecimal(TotalCessValueHSN) == 0)
                                    TotalCessValueHSN = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCessValueHSN = 0.00;
                            }
                            xlWorksheet.Cells[3, 1] = THSN.ToString();
                            xlWorksheet.Cells[3, 5] = TotalValueHSN.ToString();
                            xlWorksheet.Cells[3, 6] = TotalTaxableValueHSN.ToString();
                            xlWorksheet.Cells[3, 7] = TotalITAmtHSN.ToString();
                            xlWorksheet.Cells[3, 8] = TotalCTAmtHSN.ToString();
                            xlWorksheet.Cells[3, 9] = TotalSTAmtHSN.ToString();
                            xlWorksheet.Cells[3, 10] = TotalCessValueHSN.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 1] = 0;
                            xlWorksheet.Cells[3, 5] = 0.00;
                            xlWorksheet.Cells[3, 6] = 0.00;
                            xlWorksheet.Cells[3, 7] = 0.00;
                            xlWorksheet.Cells[3, 8] = 0.00;
                            xlWorksheet.Cells[3, 9] = 0.00;
                            xlWorksheet.Cells[3, 10] = 0.00;
                        }
                        xlWorksheet.get_Range("A4", "J4").Font.Bold = true;
                        Excel1.Range RangeHSN4 = xlWorksheet.get_Range("A4:J4", misValue);
                        RangeHSN4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "HSN";
                        xlWorksheet.Cells[4, 2] = "Description";
                        xlWorksheet.Cells[4, 3] = "UQC";
                        xlWorksheet.Cells[4, 4] = "Total Quantity";
                        xlWorksheet.Cells[4, 5] = "Total Value";
                        xlWorksheet.Cells[4, 6] = "Taxable Value";
                        xlWorksheet.Cells[4, 7] = "Integrated Tax Amount";
                        xlWorksheet.Cells[4, 8] = "Central Tax Amount";
                        xlWorksheet.Cells[4, 9] = "State/UT Tax Amount";
                        xlWorksheet.Cells[4, 10] = "Cess Amount";

                        xlWorksheet.get_Range("A1", "J1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //End HSN

                        //Start docs
                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[12], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "docs";
                        dt = new DataTable();
                        progressBar1.Visible = true;
                        //dt = B2CS();
                        m = 5;
                        data = null;
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            progressBar1.Maximum = dt.Rows.Count;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            //int k = 1;
                            for (int j = 0; j <= dt.Columns.Count - 1; j++)
                            {
                                data = dt.Rows[i].ItemArray[j].ToString();
                                xlWorksheet.Cells[m, j + 1] = data;
                            }
                            m++;
                        }
                        xlWorksheet.get_Range("A1", "A1").Font.Bold = true;
                        Excel1.Range Rangedocs1 = xlWorksheet.get_Range("A1:A1", misValue);
                        Rangedocs1.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;
                        xlWorksheet.Cells[1, 1] = "Summary of documents issued during the tax period (13)";

                        xlWorksheet.get_Range("A2", "E2").Font.Bold = true;
                        Excel1.Range Rangedocs2 = xlWorksheet.get_Range("A2:E2", misValue);
                        Rangedocs2.Interior.Color = Excel1.XlRgbColor.rgbLightSteelBlue;

                        xlWorksheet.Cells[2, 4] = "Total Number";
                        xlWorksheet.Cells[2, 5] = "Total Cancelled";

                        xlWorksheet.get_Range("A3", "E3").Font.Bold = true;
                        Excel1.Range Rangedocs3 = xlWorksheet.get_Range("A3:E3", misValue);
                        Rangedocs3.Interior.Color = Excel1.XlRgbColor.rgbLightSkyBlue;

                        if (dt.Rows.Count > 0)
                        {
                            object TotalNumberdocs;
                            TotalNumberdocs = dt.Compute("Sum(Total Number)", "");
                            try
                            {
                                if (TotalNumberdocs == null)
                                    TotalNumberdocs = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalNumberdocs = 0.00;
                            }
                            object TotalCancelleddocs;
                            TotalCancelleddocs = dt.Compute("Sum(Cancelled)", "");
                            try
                            {
                                if (TotalCancelleddocs == null)
                                    TotalCancelleddocs = 0.00;
                            }
                            catch (Exception)
                            {
                                TotalCancelleddocs = 0.00;
                            }
                            xlWorksheet.Cells[3, 4] = TotalNumberdocs.ToString();
                            xlWorksheet.Cells[3, 5] = TotalCancelleddocs.ToString();
                        }
                        else
                        {
                            xlWorksheet.Cells[3, 4] = 0;
                            xlWorksheet.Cells[3, 5] = 0;
                        }
                        xlWorksheet.get_Range("A4", "E4").Font.Bold = true;
                        Excel1.Range Rangedoc4 = xlWorksheet.get_Range("A4:E4", misValue);
                        Rangedoc4.Interior.Color = Excel1.XlRgbColor.rgbMistyRose;

                        xlWorksheet.Cells[4, 1] = "Nature  of Document";
                        xlWorksheet.Cells[4, 2] = "Sr. No. From";
                        xlWorksheet.Cells[4, 3] = "Sr. No. To";
                        xlWorksheet.Cells[4, 4] = "Total Number";
                        xlWorksheet.Cells[4, 5] = "Cancelled";

                        xlWorksheet.get_Range("A1", "E1").EntireColumn.AutoFit();
                        progressBar1.Visible = false;
                        //end docs 

                        xlWorksheet = (Excel1.Worksheet)xlSheets.Add(xlSheets[13], Type.Missing, Type.Missing, Type.Missing);
                        xlWorksheet.Name = "master";

                        excelworkBook.SaveAs(saveFileDialog.FileName.ToString(), Excel1.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel1.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        excelworkBook.Close(true, misValue, misValue);
                        excel.Quit();
                        releaseObject(xlWorksheet);
                        releaseObject(excelworkBook);
                        releaseObject(excel);
                        MessageBox.Show("Excel file created");
                    }
                }
                RptSummaryDetails.ds.Tables["B2BTransaction"].Clear();
            }
            catch (Exception)
            { }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public void BReportExportExcel(string frdate, string todate)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Cgst5amt = 0;
                Cgst12amt = 0;
                Cgst18amt = 0;
                Cgst28amt = 0;
                Sgst5amt = 0;
                Sgst12amt = 0;
                Sgst18amt = 0;
                Sgst28amt = 0;
                Igst5amt = 0;
                Igst12amt = 0;
                Igst18amt = 0;
                Igst28amt = 0;
                taxable5amt = 0;
                taxable12amt = 0;
                taxable18amt = 0;
                taxable28amt = 0;
                cesssaleamt = 0;
                cesspurchaseamt = 0;
                taxable0amt = 0;
                Sgst0amt = 0;
                Cgst0amt = 0;
                Igst0amt = 0;
                decimal VDetailtotamt = 0;
                DataTable dtRecord;
                DsSaleB2B ds = new DsSaleB2B();

                DataTable saledetail0 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and ((cd.sgstpercent is null or cd.sgstpercent=0) and (cd.igstpercent=0 or cd.igstpercent is null) and (cd.cgstpercent=0 or cd.cgstpercent is null)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (saledetail0.Rows.Count > 0)
                {
                    for (int i = 0; i < saledetail0.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(saledetail0.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail0.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail0.Rows[i][6].ToString());
                        taxable0amt += VDetailtotamt;
                        Sgst0amt += Convert.ToDecimal(saledetail0.Rows[i][0].ToString());
                        Cgst0amt += Convert.ToDecimal(saledetail0.Rows[i][1].ToString());
                        Igst0amt += Convert.ToDecimal(saledetail0.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(saledetail0.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 0 % rate from sale bill regular";
                    }
                }
                errormsg = "";

                DataTable saledetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (saledetail9.Rows.Count > 0)
                {
                    for (int i = 0; i < saledetail9.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(saledetail9.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail9.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail9.Rows[i][6].ToString());
                        taxable18amt += VDetailtotamt;
                        Sgst18amt += Convert.ToDecimal(saledetail9.Rows[i][0].ToString());
                        Cgst18amt += Convert.ToDecimal(saledetail9.Rows[i][1].ToString());
                        Igst18amt += Convert.ToDecimal(saledetail9.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(saledetail9.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 18 % rate from sale bill regular";
                    }
                }
                errormsg = "";

                DataTable saledetail6 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity * cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=6 or cd.igstpercent=12)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (saledetail6.Rows.Count > 0)
                {
                    for (int i = 0; i < saledetail6.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(saledetail6.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail6.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail6.Rows[i][6].ToString());
                        taxable12amt += VDetailtotamt;
                        Sgst12amt += Convert.ToDecimal(saledetail6.Rows[i][0].ToString());
                        Cgst12amt += Convert.ToDecimal(saledetail6.Rows[i][1].ToString());
                        Igst12amt += Convert.ToDecimal(saledetail6.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(saledetail6.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 12 % rate from sale bill regular";
                    }
                }
                errormsg = "";

                DataTable saledetail5 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=2.5 or cd.igstpercent=5)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (saledetail5.Rows.Count > 0)
                {
                    for (int i = 0; i < saledetail5.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(saledetail5.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail5.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail5.Rows[i][6].ToString());
                        taxable5amt += VDetailtotamt;
                        Sgst5amt += Convert.ToDecimal(saledetail5.Rows[i][0].ToString());
                        Cgst5amt += Convert.ToDecimal(saledetail5.Rows[i][1].ToString());
                        Igst5amt += Convert.ToDecimal(saledetail5.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(saledetail5.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 5 % rate from sale bill regular";
                    }
                }
                errormsg = "";

                DataTable saledetail14 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=14 or cd.igstpercent=28)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (saledetail14.Rows.Count > 0)
                {
                    for (int i = 0; i < saledetail14.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(saledetail14.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail14.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail14.Rows[i][6].ToString());
                        taxable28amt += VDetailtotamt;
                        Sgst28amt += Convert.ToDecimal(saledetail14.Rows[i][0].ToString());
                        Cgst28amt += Convert.ToDecimal(saledetail14.Rows[i][1].ToString());
                        Igst28amt += Convert.ToDecimal(saledetail14.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(saledetail14.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 28 % rate from sale bill regular";
                    }
                }
                errormsg = "";
                //--------------------------amount with unit!=saleunit---- and rate!=salerate------------//

                DataTable salecompounddetail0 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and ((cd.sgstpercent is null or cd.sgstpercent=0) and (cd.igstpercent=0 or cd.igstpercent is null) and (cd.cgstpercent=0 or cd.cgstpercent is null)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (salecompounddetail0.Rows.Count > 0)
                {
                    for (int i = 0; i < salecompounddetail0.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(salecompounddetail0.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail0.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail0.Rows[i][6].ToString());
                        Sgst0amt += Convert.ToDecimal(salecompounddetail0.Rows[i][0].ToString());
                        Cgst0amt += Convert.ToDecimal(salecompounddetail0.Rows[i][1].ToString());
                        Igst0amt += Convert.ToDecimal(salecompounddetail0.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(salecompounddetail0.Rows[i][5].ToString());
                        taxable0amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail0.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail0.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail0.Rows[i][2].ToString()));
                        errormsg = "Error on getting gst amount of 0 % rate from sale bill compound inclusive";
                    }
                }
                errormsg = "";

                DataTable salecompounddetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (salecompounddetail9.Rows.Count > 0)
                {
                    for (int i = 0; i < salecompounddetail9.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(salecompounddetail9.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][6].ToString());
                        Sgst18amt += Convert.ToDecimal(salecompounddetail9.Rows[i][0].ToString());
                        Cgst18amt += Convert.ToDecimal(salecompounddetail9.Rows[i][1].ToString());
                        Igst18amt += Convert.ToDecimal(salecompounddetail9.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(salecompounddetail9.Rows[i][5].ToString());
                        taxable18amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail9.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][2].ToString()));
                        errormsg = "Error on getting gst amount of 18 % rate from sale bill compound inclusive";
                    }
                }
                errormsg = "";

                DataTable salecompounddetail6 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=6 or cd.igstpercent=12)) and cd.unit!=cd.saleunit and (cd.salerate is null or cd.rate!=cd.salerate) and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (salecompounddetail6.Rows.Count > 0)
                {
                    for (int i = 0; i < salecompounddetail6.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(salecompounddetail6.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][6].ToString());
                        Sgst12amt += Convert.ToDecimal(salecompounddetail6.Rows[i][0].ToString());
                        Cgst12amt += Convert.ToDecimal(salecompounddetail6.Rows[i][1].ToString());
                        Igst12amt += Convert.ToDecimal(salecompounddetail6.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(salecompounddetail6.Rows[i][5].ToString());
                        taxable12amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail6.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][2].ToString()));
                        errormsg = "Error on getting gst amount of 18 % rate from sale bill compound inclusive";
                    }
                }
                errormsg = "";

                DataTable salecompounddetail5 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=2.5 or cd.igstpercent=5)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (salecompounddetail5.Rows.Count > 0)
                {
                    for (int i = 0; i < salecompounddetail5.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(salecompounddetail5.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][6].ToString());
                        Sgst5amt += Convert.ToDecimal(salecompounddetail5.Rows[i][0].ToString());
                        Cgst5amt += Convert.ToDecimal(salecompounddetail5.Rows[i][1].ToString());
                        Igst5amt += Convert.ToDecimal(salecompounddetail5.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(salecompounddetail5.Rows[i][5].ToString());
                        taxable5amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail5.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][2].ToString()));
                        errormsg = "Error on getting gst amount of 5 % rate from sale bill compound inclusive";
                    }
                }
                errormsg = "";

                DataTable salecompounddetail14 = db.Gettable("select sum(case when cd.sgstamt is null then 0 else cd.sgstamt end),sum(case when cd.cgstamt is null then 0 else cd.cgstamt end),sum(case when cd.igstamt is null then 0 else cd.igstamt end),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=14 or cd.igstpercent=28)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (salecompounddetail14.Rows.Count > 0)
                {
                    for (int i = 0; i < salecompounddetail14.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(salecompounddetail14.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][6].ToString());

                        Sgst28amt += Convert.ToDecimal(salecompounddetail14.Rows[i][0].ToString());
                        Cgst28amt += Convert.ToDecimal(salecompounddetail14.Rows[i][1].ToString());
                        Igst28amt += Convert.ToDecimal(salecompounddetail14.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(salecompounddetail14.Rows[i][5].ToString());
                        taxable28amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail14.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][2].ToString()));
                        errormsg = "Error on getting gst amount of 28 % rate from sale bill compound inclusive";
                    }
                }
                errormsg = "";
                //--------------------------amount with unit!=saleunit---- and rate=salerate------------//

                DataTable sale1compounddetail0 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and ((cd.sgstpercent is null or cd.sgstpercent=0) and (cd.igstpercent=0 or cd.igstpercent is null) and (cd.cgstpercent=0 or cd.cgstpercent is null)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (sale1compounddetail0.Rows.Count > 0)
                {
                    for (int i = 0; i < sale1compounddetail0.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(sale1compounddetail0.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail0.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail0.Rows[i][6].ToString());
                        taxable0amt += VDetailtotamt;
                        Sgst0amt += Convert.ToDecimal(sale1compounddetail0.Rows[i][0].ToString());
                        Cgst0amt += Convert.ToDecimal(sale1compounddetail0.Rows[i][1].ToString());
                        Igst0amt += Convert.ToDecimal(sale1compounddetail0.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(sale1compounddetail0.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 0 % rate from sale bill compound regular";
                    }
                }
                errormsg = "";

                DataTable sale1compounddetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (sale1compounddetail9.Rows.Count > 0)
                {
                    for (int i = 0; i < sale1compounddetail9.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(sale1compounddetail9.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail9.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail9.Rows[i][6].ToString());
                        taxable18amt += VDetailtotamt;
                        Sgst18amt += Convert.ToDecimal(sale1compounddetail9.Rows[i][0].ToString());
                        Cgst18amt += Convert.ToDecimal(sale1compounddetail9.Rows[i][1].ToString());
                        Igst18amt += Convert.ToDecimal(sale1compounddetail9.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(sale1compounddetail9.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 18 % rate from sale bill compound regular";
                    }
                }
                errormsg = "";

                DataTable sale1compounddetail6 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=6 or cd.igstpercent=12)) and cd.unit!=cd.saleunit and (cd.rate=cd.salerate) and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (sale1compounddetail6.Rows.Count > 0)
                {
                    for (int i = 0; i < sale1compounddetail6.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(sale1compounddetail6.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail6.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail6.Rows[i][6].ToString());
                        taxable12amt += VDetailtotamt;
                        Sgst12amt += Convert.ToDecimal(sale1compounddetail6.Rows[i][0].ToString());
                        Cgst12amt += Convert.ToDecimal(sale1compounddetail6.Rows[i][1].ToString());
                        Igst12amt += Convert.ToDecimal(sale1compounddetail6.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(sale1compounddetail6.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 12 % rate from sale bill compound regular";
                    }
                }
                errormsg = "";

                DataTable sale1compounddetail5 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=2.5 or cd.igstpercent=5)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (sale1compounddetail5.Rows.Count > 0)
                {
                    for (int i = 0; i < sale1compounddetail5.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(sale1compounddetail5.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail5.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail5.Rows[i][6].ToString());
                        taxable5amt += VDetailtotamt;
                        Sgst5amt += Convert.ToDecimal(sale1compounddetail5.Rows[i][0].ToString());
                        Cgst5amt += Convert.ToDecimal(sale1compounddetail5.Rows[i][1].ToString());
                        Igst5amt += Convert.ToDecimal(sale1compounddetail5.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(sale1compounddetail5.Rows[i][5].ToString());
                        errormsg = "Error on getting gst amount of 5 % rate from sale bill compound regular";
                    }
                }
                errormsg = "";

                DataTable sale1compounddetail14 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=14 or cd.igstpercent=28)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
                if (sale1compounddetail14.Rows.Count > 0)
                {
                    for (int i = 0; i < sale1compounddetail14.Rows.Count; i++)
                    {
                        VDetailtotamt = Convert.ToDecimal(sale1compounddetail14.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail14.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail14.Rows[i][6].ToString());
                        taxable28amt += VDetailtotamt;
                        Sgst28amt += Convert.ToDecimal(sale1compounddetail14.Rows[i][0].ToString());
                        Cgst28amt += Convert.ToDecimal(sale1compounddetail14.Rows[i][1].ToString());
                        Igst28amt += Convert.ToDecimal(sale1compounddetail14.Rows[i][2].ToString());
                        cesssaleamt += Convert.ToDecimal(sale1compounddetail14.Rows[i][5].ToString());

                        errormsg = "Error on getting gst amount of 28 % rate from sale bill compound regular";
                    }
                }
                errormsg = "";
                // //---------------new code 2908-----------------------------------//

                dtRecord = ds.Tables["B2BTransaction"];
                for (int j = 0; j < 5; j++)
                {
                    if (j == 0)
                        dtRecord.Rows.Add(new object[9] { 0, taxable0amt, Cgst0amt, Sgst0amt, Igst0amt, 0, 0, 0, 0 });
                    if (j == 1)
                        dtRecord.Rows.Add(new object[9] { 5, taxable5amt, Cgst5amt, Sgst5amt, Igst5amt, 0, 0, 0, 0 });
                    else if (j == 2)
                        dtRecord.Rows.Add(new object[9] { 12, taxable12amt, Cgst12amt, Sgst12amt, Igst12amt, 0, 0, 0, 0 });
                    else if (j == 3)
                        dtRecord.Rows.Add(new object[9] { 18, taxable18amt, Cgst18amt, Sgst18amt, Igst18amt, 0, 0, 0, 0 });
                    else if (j == 4)
                        dtRecord.Rows.Add(new object[9] { 28, taxable28amt, Cgst28amt, Sgst28amt, Igst28amt, 0, 0, 0, 0 });
                }

                DataTable salediscountdetail = db.Gettable("select case when sum(cm.TotalAmount*cm.discount/100) is null then 0 else sum(cm.TotalAmount*cm.discount/100) end from customerbillmaster as cm where cm.discount is not null and cm.discount!=0 and cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%'");
                if (salediscountdetail.Rows.Count > 0)
                {
                    if (salediscountdetail.Rows[0][0] != null)
                        totalSaleDiscount = Convert.ToDecimal(salediscountdetail.Rows[0][0]);
                }

                DataTable saleTransportdetail = db.Gettable("select case when sum(cm.TransportCharges) is null then 0 else sum(cm.TransportCharges) end from customerbillmaster as cm where cm.Transportcharges!=0 and cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%'");
                if (saleTransportdetail.Rows.Count > 0)
                {
                    if (saleTransportdetail.Rows[0][0] != null)
                        totalSaleTransport = Convert.ToDecimal(saleTransportdetail.Rows[0][0]);
                }

                DataTable saleHamalidetail = db.Gettable("select case when sum(cm.HamaliCharges) is null then 0 else sum(cm.HamaliCharges) end from customerbillmaster as cm where cm.HamaliCharges!=0 and cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%'");
                if (saleHamalidetail.Rows.Count > 0)
                {
                    if (saleHamalidetail.Rows[0][0] != null)
                        totalSaleHamali = Convert.ToDecimal(saleHamalidetail.Rows[0][0]);
                }
                DataTable SaleReturnAmount = db.Gettable("SELECT case when sum(NetAmt) is null then 0 else sum(NetAmt) end,case when sum(PaidAmt) is null then 0 else sum(PaidAmt) end  FROM [RemainingPayment] where (Type='BillReturn' or Type='credit Note' or Type='Debit Note') and CreateDate>='" + frdate + "' and CreateDate<='" + todate + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and BillId like 'T%'");
                if (SaleReturnAmount.Rows.Count > 0)
                {
                    if (SaleReturnAmount.Rows[0][0] != null)
                    {
                        totalSaleCreditAmount = Convert.ToDecimal(SaleReturnAmount.Rows[0][0]);
                        totalSaleDebitAmount = Convert.ToDecimal(SaleReturnAmount.Rows[0][1]);
                    }
                }

                //Purchase Records
                Cgst5amt = 0;
                Cgst12amt = 0;
                Cgst18amt = 0;
                Cgst28amt = 0;
                Sgst5amt = 0;
                Sgst12amt = 0;
                Sgst18amt = 0;
                Sgst28amt = 0;
                Igst5amt = 0;
                Igst12amt = 0;
                Igst18amt = 0;
                Igst28amt = 0;
                taxable5amt = 0;
                taxable18amt = 0;
                taxable12amt = 0;
                taxable28amt = 0;
                cesspurchaseamt = 0;
                taxable0amt = 0;
                Sgst0amt = 0;
                Cgst0amt = 0;
                Igst0amt = 0;
                decimal transportgstAmt = 0;
                decimal packagegstAmt = 0, packageAmt = 0;

                //------------------------ 26082017 new code --------//
                try
                {
                    // Add Transport Gst in taxable amount
                    DataTable purdetail_transport = db.Gettable("Select sum(isnull(pm.TransportGst,0)) from PurchaseMaster pm where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail_transport.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail_transport.Rows.Count; i++)
                        {
                            transportgstAmt = transportgstAmt + Convert.ToDecimal(purdetail_transport.Rows[i][0].ToString());
                        }
                    }

                    // Packaging gst
                    DataTable purdetail_package = db.Gettable("Select sum(isnull(pm.PackagingGstAmt,0)) from PurchaseMaster pm where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail_package.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail_package.Rows.Count; i++)
                        {
                            packagegstAmt = packagegstAmt + (Convert.ToDecimal(purdetail_package.Rows[i][0]));
                        }
                    }

                    // Packaging Amt
                    DataTable purdetail_packageAmt = db.Gettable("Select sum(isnull(pm.PackagingAndForwarding,0)) from PurchaseMaster pm where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail_packageAmt.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail_packageAmt.Rows.Count; i++)
                        {
                            packageAmt = packageAmt + (Convert.ToDecimal(purdetail_packageAmt.Rows[i][0]));
                        }
                    }
                    //0% GST
                    //                                                  0          1                2                3                            4                  5                                 
                    DataTable purdetail0 = db.Gettable("Select Isnull(sum(pd.sgstamt),0),Isnull(sum(pd.cgstamt),0),Isnull(sum(pd.igstamt),0),Isnull(sum(pd.amount),0),Isnull(sum(pd.amount * pd.discount/100),0),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else Isnull(sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100),0) end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and ((pd.SGstPercent is  NULL or pd.SGstPercent=0) and (pd.CGstPercent is NULL or pd.CGstPercent=0) and (pd.IGstPercent is  NULL or pd.IGstPercent=0)) and pm.id not in(Select case when pm.TotalGST=0 then pm.id else 0 end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and (pm.Taxtype is null or pm.Taxtype!='TaxPurchase') and ((pd.SGstPercent is  NULL or pd.SGstPercent=0) and (pd.CGstPercent is NULL or pd.CGstPercent=0) and (pd.IGstPercent is  NULL or pd.IGstPercent=0)) group by pm.id,pm.TotalGST) group by pm.id");

                    if (purdetail0.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail0.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail0.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail0.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail0.Rows[i][6].ToString());
                            taxable0amt += VDetailtotamt;
                            Sgst0amt += Convert.ToDecimal(purdetail0.Rows[i][0].ToString());
                            Cgst0amt += Convert.ToDecimal(purdetail0.Rows[i][1].ToString());
                            Igst0amt += Convert.ToDecimal(purdetail0.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail0.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 0 % rate";
                        }
                    }
                    errormsg = "";

                    //                                                  0          1                2                3                            4                  5                                 
                    DataTable purdetail9 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and (pd.sgstpercent is not null and (pd.sgstpercent=9 or pd.igstpercent=18)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail9.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail9.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail9.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail9.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail9.Rows[i][6].ToString());
                            taxable18amt += VDetailtotamt;
                            Sgst18amt += Convert.ToDecimal(purdetail9.Rows[i][0].ToString());
                            Cgst18amt += Convert.ToDecimal(purdetail9.Rows[i][1].ToString());
                            Igst18amt += Convert.ToDecimal(purdetail9.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail9.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 18 % rate";
                        }
                    }
                    errormsg = "";

                    DataTable purdetail6 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and (pd.sgstpercent is not null and (pd.sgstpercent=6 or pd.igstpercent=12)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail6.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail6.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail6.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail6.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail6.Rows[i][6].ToString());
                            taxable12amt += VDetailtotamt;
                            Sgst12amt += Convert.ToDecimal(purdetail6.Rows[i][0].ToString());
                            Cgst12amt += Convert.ToDecimal(purdetail6.Rows[i][1].ToString());
                            Igst12amt += Convert.ToDecimal(purdetail6.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail6.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 12 % rate";
                        }
                    }
                    errormsg = "";

                    DataTable purdetail5 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and (pd.sgstpercent is not null and (pd.sgstpercent=2.5 or pd.igstpercent=5)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail5.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail5.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail5.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail5.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail5.Rows[i][6].ToString());
                            taxable5amt += VDetailtotamt;
                            Sgst5amt += Convert.ToDecimal(purdetail5.Rows[i][0].ToString());
                            Cgst5amt += Convert.ToDecimal(purdetail5.Rows[i][1].ToString());
                            Igst5amt += Convert.ToDecimal(purdetail5.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail5.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 5 % rate";
                        }
                    }
                    errormsg = "";

                    DataTable purdetail14 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "'  and (pd.sgstpercent is not null and (pd.sgstpercent=14 or pd.igstpercent=28)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail14.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail14.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail14.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail14.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail14.Rows[i][6].ToString());
                            taxable28amt += VDetailtotamt;
                            Sgst28amt += Convert.ToDecimal(purdetail14.Rows[i][0].ToString());
                            Cgst28amt += Convert.ToDecimal(purdetail14.Rows[i][1].ToString());
                            Igst28amt += Convert.ToDecimal(purdetail14.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail14.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 28 % rate";
                        }
                    }
                    errormsg = "";

                    dtRecord = ds.Tables["B2BTransaction"];
                    for (int j = 0; j < 5; j++)
                    {
                        if (j == 0)
                        {
                            dtRecord.Rows[j][5] = taxable0amt;
                            dtRecord.Rows[j][6] = Cgst0amt;
                            dtRecord.Rows[j][7] = Sgst0amt;
                            dtRecord.Rows[j][8] = Igst0amt;
                        }
                        if (j == 1)
                        {
                            dtRecord.Rows[j][5] = taxable5amt;
                            dtRecord.Rows[j][6] = Cgst5amt;
                            dtRecord.Rows[j][7] = Sgst5amt;
                            dtRecord.Rows[j][8] = Igst5amt;
                        }
                        else if (j == 2)
                        {
                            dtRecord.Rows[j][5] = taxable12amt;
                            dtRecord.Rows[j][6] = Cgst12amt;
                            dtRecord.Rows[j][7] = Sgst12amt;
                            dtRecord.Rows[j][8] = Igst12amt;
                        }
                        else if (j == 3)
                        {
                            dtRecord.Rows[j][5] = taxable18amt;
                            dtRecord.Rows[j][6] = Cgst18amt;
                            dtRecord.Rows[j][7] = Sgst18amt;
                            dtRecord.Rows[j][8] = Igst18amt;
                        }
                        else if (j == 4)
                        {
                            dtRecord.Rows[j][5] = taxable28amt;
                            dtRecord.Rows[j][6] = Cgst28amt;
                            dtRecord.Rows[j][7] = Sgst28amt;
                            dtRecord.Rows[j][8] = Igst28amt;
                        }
                    }
                    //--------------------------------//

                    DataTable purdiscountdetail = db.Gettable("select case when sum(pm.TotalAmount*pm.discount/100) is null then 0 else sum(pm.TotalAmount*pm.discount/100) end from PurchaseMaster as pm where pm.discount is not null and pm.discount!=0 and pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.TotalGst>0");
                    if (purdiscountdetail.Rows.Count > 0)
                    {
                        if (purdiscountdetail.Rows[0][0] != null)
                            totalDiscount = Convert.ToDecimal(purdiscountdetail.Rows[0][0]);
                        errormsg = "Error on getting purchase discount";
                    }

                    DataTable purspcdiscountdetail = db.Gettable("select case when sum((pm.TotalAmount-pm.TotalAmount*pm.discount/100)*pm.SpcDiscount/100) is null then 0 else sum((pm.TotalAmount-pm.TotalAmount*pm.discount/100)*pm.SpcDiscount/100) end from PurchaseMaster as pm where pm.SpcDiscount is not null and pm.SpcDiscount!=0 and pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "'  and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.TotalGst>0");
                    if (purspcdiscountdetail.Rows.Count > 0)
                    {
                        if (purspcdiscountdetail.Rows[0][0] != null)
                            totalspcDiscount = Convert.ToDecimal(purspcdiscountdetail.Rows[0][0]);
                        errormsg = "Error on getting purchase special discount";
                    }

                    DataTable purTransportdetail = db.Gettable("select case when sum(pm.TransportCharges) is null then 0 else sum(pm.TransportCharges) end from PurchaseMaster as pm where pm.Transportcharges!=0 and pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.TotalGst>0");
                    if (purTransportdetail.Rows.Count > 0)
                    {
                        if (purTransportdetail.Rows[0][0] != null)
                            totalTransport = Convert.ToDecimal(purTransportdetail.Rows[0][0]);
                        errormsg = "Error on getting purchase transport detail";
                    }

                    DataTable purchaseReturnAmount = db.Gettable("SELECT case when sum(NetAmt) is null then 0 else sum(NetAmt) end,case when sum(PaidAmt) is null then 0 else sum(PaidAmt) end  FROM [SupplierRemainingPayment] where (Type='PurchaseReturn' or Type='credit Note' or Type='Debit Note') and CreateDate>='" + frdate + "' and CreateDate<='" + todate + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (purchaseReturnAmount.Rows.Count > 0)
                    {
                        if (purchaseReturnAmount.Rows[0][0] != null)
                        {
                            totalpurchaseCreditAmount = Convert.ToDecimal(purchaseReturnAmount.Rows[0][0]);
                            totalpurchaseDebitAmount = Convert.ToDecimal(purchaseReturnAmount.Rows[0][1]);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Purchase Bill: " + errormsg, "Warning");
                    this.Close();
                }
            }
            catch (Exception)
            { }
        }

        public void ExportToExcel(DataSet ds, string filename)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;

                object misValue = System.Reflection.Missing.Value;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                // excelSheet.Name = "Test work sheet";

                excelSheet.Application.ActiveWindow.ScrollRow = 1;
                excelSheet.Application.ActiveWindow.SplitRow = 1;
                excelSheet.Application.ActiveWindow.FreezePanes = true;

                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = ds.Tables["B2BTransaction"].Rows.Count;
                progressBar1.Visible = true;
                int m = 1;

                int k = 0;
                int i = 0;

                k = i;
                excelSheet.get_Range("A1", "AH1").Font.Bold = true;
                excelSheet.get_Range("A1", "AH1").Font.Color = System.Drawing.Color.Red;
                excelSheet.Cells[m, 5] = "3B";
                excelSheet.Cells[m, 8] = "From Date :";
                excelSheet.Cells[m, 9] = fDate;

                excelSheet.Cells[m, 4] = "Product Name";
                excelSheet.Cells[m, 5] = "BarCode Status";
                excelSheet.Cells[m, 6] = " BatchNo";
                excelSheet.Cells[m, 7] = " ExpiryDate";
                excelSheet.Cells[m, 8] = "Customer Name";
                excelSheet.Cells[m, 9] = " Customer Code";
                excelSheet.Cells[m, 10] = " Customer Type";
            }
            catch (Exception)
            { }
        }

        public void ExportToExcel(DsSaleB2B ds, string filename, RptSummaryDetails rptdetails)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;

                object misValue = System.Reflection.Missing.Value;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                // excelSheet.Name = "Test work sheet";

                excelSheet.Application.ActiveWindow.ScrollRow = 1;
                excelSheet.Application.ActiveWindow.SplitRow = 1;
                excelSheet.Application.ActiveWindow.FreezePanes = true;

                string data = null;

                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = ds.Tables["B2BTransaction"].Rows.Count;
                progressBar1.Visible = true;
                int m = 1;

                int k = 0;
                int i = 0;

                k = i;
                excelSheet.get_Range("A1", "AH1").Font.Bold = true;
                excelSheet.get_Range("A1", "AH1").Font.Color = System.Drawing.Color.Red;
                excelSheet.Cells[m, 7] = "3B Report Excel";
                excelSheet.Cells[++m, 6] = dtpMonth.Value.ToString();
                excelSheet.Cells[m, 7] = "-";
                excelSheet.Cells[m, 8] = dtptomonth.Value.ToString();
                excelSheet.Cells[++m, 1] = "GSTIN :";
                data = ds.Tables["CompanyInformation"].Rows[0].ItemArray[7].ToString();//hsn 1 billno
                excelSheet.Cells[m, 2] = data;
                excelSheet.Cells[++m, 1] = "Legal name of the registererd person:";
                data = ds.Tables["CompanyInformation"].Rows[0].ItemArray[2].ToString();//hsn 1 billno
                excelSheet.Cells[m, 2] = data;
                m++;
                excelSheet.Cells[++m, 1] = "GstPercent";
                excelSheet.Cells[m, 2] = "TaxableAmount";
                excelSheet.Cells[m, 3] = "SgstAmount";
                excelSheet.Cells[m, 4] = " CgstAmount";
                excelSheet.Cells[m, 5] = "IgstAmount";
                excelSheet.Cells[m, 6] = " PTaxableAmount";
                excelSheet.Cells[m, 7] = " PSgstAmount";
                excelSheet.Cells[m, 8] = " PCgstAmount";
                excelSheet.Cells[m, 9] = " PIgstAmount";
                m++;

                for (i = 0; i <= ds.Tables["B2BTransaction"].Rows.Count - 1; i++)
                {
                    progressBar1.Value = k;

                    int l = 0;

                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[0].ToString();//hsn 1 billno
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[1].ToString();//hsn 2 documentno
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[2].ToString();//qty 3 ducdate
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[3].ToString();//rate 4 productname
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[4].ToString();//hsn 1 billno
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[5].ToString();//hsn 2 documentno
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[6].ToString();//qty 3 ducdate
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[7].ToString();//rate 4 productname
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["B2BTransaction"].Rows[i].ItemArray[8].ToString();//rate 4 productname
                    excelSheet.Cells[m, ++l] = data;
                    m++;
                }

                int L = 0;
                data = "Total";
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(TaxableAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(SgstAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(CgstAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(IgstAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(PTaxableAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(PSgstAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(PCgstAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                data = ds.Tables["B2BTransaction"].Compute("Sum(PIgstAmount)", string.Empty).ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; m++;
                decimal TotCustIgstAmt, TotCgstAmt, TotCustSgstAmt, TotCustTaxable, TotPtaxable, TotPSgstamt, TotPCgstamt, TotPIgstamt;
                decimal totpayable, paidIgst, CollectedCgst, IgstRefundable, PaidCgst, purchaseNetAmt, TotalPurchaseGrossAmt, TotalSellSgst, TotalPurSgst, TotalIgstCollected, TotalSaleGrossAmt, TotalSellNetAmt;

                // decimal 
                TotCustIgstAmt = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(IgstAmount)", string.Empty).ToString());
                TotCgstAmt = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(CgstAmount)", string.Empty).ToString());
                TotCustSgstAmt = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(SgstAmount)", string.Empty).ToString());
                TotCustTaxable = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(TaxableAmount)", string.Empty).ToString());
                TotPtaxable = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(PTaxableAmount)", string.Empty).ToString());
                TotPSgstamt = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(PSgstAmount)", string.Empty).ToString());
                TotPCgstamt = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(PCgstAmount)", string.Empty).ToString());
                TotPIgstamt = Convert.ToDecimal(ds.Tables["B2BTransaction"].Compute("Sum(PIgstAmount)", string.Empty).ToString());
                totpayable = TotCgstAmt - TotPCgstamt;
                paidIgst = TotPIgstamt + TotPCgstamt;
                CollectedCgst = TotCgstAmt + TotCustIgstAmt;
                IgstRefundable = paidIgst - CollectedCgst;
                PaidCgst = TotPCgstamt;
                TotalPurchaseGrossAmt = (TotPtaxable + TotPSgstamt + TotPCgstamt + TotPIgstamt);
                purchaseNetAmt = (TotalPurchaseGrossAmt - rptdetails.totalDiscount - rptdetails.totalspcDiscount + rptdetails.totalTransport + rptdetails.totalpurchasewithnotax + rptdetails.cesspurchaseamt + rptdetails.pubtransportgstAmt + rptdetails.pubpackagegstAmt + rptdetails.pubpackageAmt + rptdetails.totalpurchaseCreditAmount - rptdetails.totalpurchaseDebitAmount);
                TotalSellSgst = TotCustSgstAmt;
                TotalPurSgst = TotPSgstamt;
                TotalIgstCollected = TotPIgstamt;
                TotalSaleGrossAmt = TotCustTaxable + TotCustSgstAmt + TotCustIgstAmt + TotCgstAmt;
                TotalSellNetAmt = TotalSaleGrossAmt - rptdetails.totalSaleDiscount + rptdetails.totalSaleTransport + rptdetails.totalSaleHamali + rptdetails.totalSalenotax + rptdetails.cesssaleamt + rptdetails.totalSaleCreditAmount - rptdetails.totalSaleDebitAmount;

                L = 0;//1
                data = "Total Collected SGST Amount    ";
                excelSheet.Cells[m, ++L] = data;
                data = TotalSellSgst.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Gst Amount On Transport";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.pubtransportgstAmt.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//2
                data = "Total Already Paid SGST Amount       ";
                excelSheet.Cells[m, ++L] = data;
                data = TotalPurSgst.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Gst Amount On Packaging and Forwarding";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.pubpackagegstAmt.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//3
                data = "SGST Payable Amount        ";
                excelSheet.Cells[m, ++L] = data;
                data = totpayable.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Total Paid Cgst Amount";
                excelSheet.Cells[m, ++L] = data;
                data = PaidCgst.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//4
                data = "Total Sale Amount    ";
                excelSheet.Cells[m, ++L] = data;
                data = TotalSaleGrossAmt.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Total Paid IGST Amount ";
                excelSheet.Cells[m, ++L] = data;
                data = TotalIgstCollected.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//5
                data = "Discount on Sale    ";
                excelSheet.Cells[m, ++L] = data;
                data = totalSaleDiscount.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Total Already Paid IGST+Cgst Amount    ";
                excelSheet.Cells[m, ++L] = data;
                data = paidIgst.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//6
                data = "Sale Transport Charges    ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalSaleTransport.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Total Collected Cgst+Igst Amount ";
                excelSheet.Cells[m, ++L] = data;
                data = CollectedCgst.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//7
                data = "Sale Hamali Charges     ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalSaleHamali.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Igst+Cgst Refundable Amount ";
                excelSheet.Cells[m, ++L] = data;
                data = IgstRefundable.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//8
                data = "Sale amount with no tax	     ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalSalenotax.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Total Purchase Amount ";
                excelSheet.Cells[m, ++L] = data;
                data = (Math.Round(TotalPurchaseGrossAmt).ToString()) + ".00";
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//9
                data = "Total Inward Cess   ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.cesssaleamt.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Discount on Purchase";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalDiscount.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//10
                data = "Total Credit Note Amount    ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalSaleCreditAmount.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Special Discount on Purchase";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalspcDiscount.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//11
                data = "Total Debit Note Amount   ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalSaleDebitAmount.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Purchase Transport Charges ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalTransport.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 0;//12
                data = "Total Inward Net Amount    ";
                excelSheet.Cells[m, ++L] = data;
                data = TotalSellNetAmt.ToString();
                excelSheet.Cells[m, ++L] = data;
                data = "Packaging & Forwarding Charges ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.pubpackageAmt.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 2;//13
                data = "Outward with no tax ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalpurchasewithnotax.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 2;//14
                data = "Total Outward Cess ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.cesspurchaseamt.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 2;//15
                data = "Total Credit Note Amount ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalpurchaseCreditAmount.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 2;//16
                data = "Total Debit Note Amount ";
                excelSheet.Cells[m, ++L] = data;
                data = rptdetails.totalpurchaseDebitAmount.ToString();
                excelSheet.Cells[m, ++L] = data;
                m++; L = 2;//17
                data = "Total Outward Net Amount";
                excelSheet.Cells[m, ++L] = data;
                data = (Math.Round(purchaseNetAmt).ToString()) + ".00";
                excelSheet.Cells[m, ++L] = data;
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs(filename, Excel1.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel1.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file " + filename);
                progressBar1.Visible = false;
                ds.Tables["B2BTransaction"].Clear();
            }
            catch (Exception)
            { }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            RptSummaryDetails rptdetails = new RptSummaryDetails();
            CommonMethod cm = new CommonMethod();
            bool bool1 = rptdetails.getReportData(cm.ValidFromDate(dtpMonth.Value), cm.ValidToDate(dtptomonth.Value));

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            { //consttructor code here
                try
                {
                    ExportToExcel(RptSummaryDetails.ds, saveFileDialog.FileName, rptdetails);
                }
                catch { }
            }
        }
    }
}
