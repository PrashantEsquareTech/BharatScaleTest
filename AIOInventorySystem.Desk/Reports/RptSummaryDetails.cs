using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptSummaryDetails : Form
    {
        DbClass db = new DbClass();
        public static DsSaleB2B ds = new DsSaleB2B();
        string errormsg = "";
        public DateTime fDate, tDate;
        public decimal Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0, Sgst5amt = 0, Sgst12amt = 0, Sgst18amt = 0, Sgst28amt = 0, Igst5amt = 0,
                    Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, cesssaleamt = 0, cesspurchaseamt = 0, taxable0amt = 0, Sgst0amt = 0, Cgst0amt = 0, Igst0amt = 0,
                    taxable5amt = 0, taxable12amt = 0, taxable18amt = 0, taxable28amt = 0, totalDiscount = 0, totalSaleDiscount = 0, totalspcDiscount = 0,
                    totalTransport = 0, totalSaleTransport = 0, totalSaleHamali = 0, totalSalenotax = 0, totalpurchasewithnotax = 0, totalSaleCreditAmount = 0,
                    totalSaleDebitAmount = 0, totalpurchaseCreditAmount = 0, totalpurchaseDebitAmount = 0, pubtransportgstAmt = 0, pubpackagegstAmt = 0,
                    pubpackageAmt = 0, VDetailtotamt = 0;

        public bool isvalid = true;

        public RptSummaryDetails()
        {
            InitializeComponent();
        }

        public RptSummaryDetails(DateTime fdate, DateTime tdate, ref bool validreport)
        {
            fDate = fdate;
            tDate = tdate;
            CommonMethod cm = new CommonMethod();
            InitializeComponent();
            validreport = getReportData(cm.ValidFromDate(fdate), cm.ValidToDate(tdate));
        }

        public bool getReportData(string frdate, string todate)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                SqlDataAdapter da;
                DataTable dtRecord;

                //   //----------------------New code 2908----------Sale-//
                // //------------------Amount with unit=saleunit----------------//
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
                decimal transportgstAmt = 0, packagegstAmt = 0, packageAmt = 0;

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
                    //                                                       0                         1                         2                           3                             4                                              5                                                                              6                                                                                                                                                               
                    DataTable purdetail0 = db.Gettable("Select Isnull(sum(pd.sgstamt),0),Isnull(sum(pd.cgstamt),0),Isnull(sum(pd.igstamt),0),Isnull(sum(pd.amount),0),Isnull(sum(pd.amount * pd.discount/100),0),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else Isnull(sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100),0) end,sum((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100)-((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100))*isnull(pd.SpcDisc,0)/100))*isnull(ThreeLevelDiscPer,0)/100) from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and ((pd.SGstPercent is  NULL or pd.SGstPercent=0) and (pd.CGstPercent is NULL or pd.CGstPercent=0) and (pd.IGstPercent is  NULL or pd.IGstPercent=0)) and pm.id not in(Select case when pm.TotalGST=0 then pm.id else 0 end from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and (pm.Taxtype is null or pm.Taxtype!='TaxPurchase') and ((pd.SGstPercent is  NULL or pd.SGstPercent=0) and (pd.CGstPercent is NULL or pd.CGstPercent=0) and (pd.IGstPercent is  NULL or pd.IGstPercent=0)) group by pm.id,pm.TotalGST) group by pm.id");

                    if (purdetail0.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail0.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail0.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail0.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail0.Rows[i][6].ToString()) - Convert.ToDecimal(purdetail0.Rows[i][7].ToString());
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
                    DataTable purdetail9 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end,sum((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100)-((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100))*isnull(pd.SpcDisc,0)/100))*isnull(ThreeLevelDiscPer,0)/100) from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and (pd.sgstpercent is not null and (pd.sgstpercent=9 or pd.igstpercent=18)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail9.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail9.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail9.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail9.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail9.Rows[i][6].ToString()) - Convert.ToDecimal(purdetail9.Rows[i][7].ToString());
                            taxable18amt += VDetailtotamt;
                            Sgst18amt += Convert.ToDecimal(purdetail9.Rows[i][0].ToString());
                            Cgst18amt += Convert.ToDecimal(purdetail9.Rows[i][1].ToString());
                            Igst18amt += Convert.ToDecimal(purdetail9.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail9.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 18 % rate";
                        }
                    }
                    errormsg = "";

                    DataTable purdetail6 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end,sum((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100)-((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100))*isnull(pd.SpcDisc,0)/100))*isnull(ThreeLevelDiscPer,0)/100) from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and (pd.sgstpercent is not null and (pd.sgstpercent=6 or pd.igstpercent=12)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail6.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail6.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail6.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail6.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail6.Rows[i][6].ToString()) - Convert.ToDecimal(purdetail6.Rows[i][7].ToString());
                            taxable12amt += VDetailtotamt;
                            Sgst12amt += Convert.ToDecimal(purdetail6.Rows[i][0].ToString());
                            Cgst12amt += Convert.ToDecimal(purdetail6.Rows[i][1].ToString());
                            Igst12amt += Convert.ToDecimal(purdetail6.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail6.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 12 % rate";
                        }
                    }
                    errormsg = "";

                    DataTable purdetail5 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end,sum((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100)-((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100))*isnull(pd.SpcDisc,0)/100))*isnull(ThreeLevelDiscPer,0)/100) from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "' and (pd.sgstpercent is not null and (pd.sgstpercent=2.5 or pd.igstpercent=5)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail5.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail5.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail5.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail5.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail5.Rows[i][6].ToString()) - Convert.ToDecimal(purdetail5.Rows[i][7].ToString());
                            taxable5amt += VDetailtotamt;
                            Sgst5amt += Convert.ToDecimal(purdetail5.Rows[i][0].ToString());
                            Cgst5amt += Convert.ToDecimal(purdetail5.Rows[i][1].ToString());
                            Igst5amt += Convert.ToDecimal(purdetail5.Rows[i][2].ToString());
                            cesspurchaseamt += Convert.ToDecimal(purdetail5.Rows[i][5].ToString());
                            errormsg = "Error on getting gst amount of 5 % rate";
                        }
                    }
                    errormsg = "";

                    DataTable purdetail14 = db.Gettable("Select sum(pd.sgstamt),sum(pd.cgstamt),sum(pd.igstamt),sum(pd.amount),sum(pd.amount * pd.discount/100),case when sum(pd.cessamt) is null then 0 else sum(pd.cessamt) end,case when sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) is null then 0 else sum((pd.amount-(pd.amount * pd.discount/100))*pd.SpcDisc/100) end,sum((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100)-((isnull(pd.amount,0)-(isnull(pd.amount,0) * isnull(pd.discount,0)/100))*isnull(pd.SpcDisc,0)/100))*isnull(ThreeLevelDiscPer,0)/100) from PurchaseMaster pm join PurchaseDetail pd on pm.Id=pd.PorderNo where pm.PorderDate>='" + frdate + "' and pm.PorderDate<='" + todate + "'  and (pd.sgstpercent is not null and (pd.sgstpercent=14 or pd.igstpercent=28)) and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' group by pm.id");
                    if (purdetail14.Rows.Count > 0)
                    {
                        for (int i = 0; i < purdetail14.Rows.Count; i++)
                        {
                            VDetailtotamt = Convert.ToDecimal(purdetail14.Rows[i][3].ToString()) - Convert.ToDecimal(purdetail14.Rows[i][4].ToString()) - Convert.ToDecimal(purdetail14.Rows[i][6].ToString()) - Convert.ToDecimal(purdetail14.Rows[i][7].ToString());
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
                    MessageBox.Show("Purchase Bill :" + errormsg, "Warning");
                    isvalid = false;
                    this.Close();
                    return false;
                }
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (isvalid == true)
                {
                    CrptB2BTransaction b2btransactionreport = new CrptB2BTransaction();
                    pubtransportgstAmt = transportgstAmt; pubpackagegstAmt = packagegstAmt; pubpackageAmt = packageAmt;

                    b2btransactionreport.SetDataSource(ds);
                    b2btransactionreport.SetParameterValue("fromdate", fDate);
                    b2btransactionreport.SetParameterValue("Todate", tDate);
                    b2btransactionreport.SetParameterValue("totalDiscount", totalDiscount);
                    b2btransactionreport.SetParameterValue("totalTransport", totalTransport);
                    b2btransactionreport.SetParameterValue("totalSaleDiscount", totalSaleDiscount);
                    b2btransactionreport.SetParameterValue("totalSaleTransport", totalSaleTransport);
                    b2btransactionreport.SetParameterValue("totalSaleHamali", totalSaleHamali);
                    b2btransactionreport.SetParameterValue("totalSalenotax", totalSalenotax);
                    b2btransactionreport.SetParameterValue("totalpurchasewithnotax", totalpurchasewithnotax);
                    b2btransactionreport.SetParameterValue("TotalSaleCess", cesssaleamt);
                    b2btransactionreport.SetParameterValue("TotalPurchaseCess", cesspurchaseamt);
                    b2btransactionreport.SetParameterValue("totalspcDiscount", totalspcDiscount);
                    b2btransactionreport.SetParameterValue("TransportGst", transportgstAmt);
                    b2btransactionreport.SetParameterValue("PackageGst", packagegstAmt);
                    b2btransactionreport.SetParameterValue("PackageAmt", packageAmt);
                    b2btransactionreport.SetParameterValue("TotalSaleCreditAmount", totalSaleCreditAmount);
                    b2btransactionreport.SetParameterValue("TotalSaleDebitAmount", totalSaleDebitAmount);
                    b2btransactionreport.SetParameterValue("TotalPurchaseCreditNoteAmount", totalpurchaseCreditAmount);
                    b2btransactionreport.SetParameterValue("TotalPurchasedebitNoteAmount", totalpurchaseDebitAmount);
                    crystalReportViewer1.ReportSource = b2btransactionreport;
                    crystalReportViewer1.Refresh();
                }
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                return isvalid;
            }
            catch (Exception)
            {
                this.Close();
                return false;
            }
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