using System;
using System.Data;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmGstReports : Form
    {
        public decimal Cgst5amt, Cgst12amt, Cgst18amt, Cgst28amt, Sgst5amt, Sgst12amt, Sgst18amt, Sgst28amt, Igst5amt, Igst12amt, Igst18amt, Igst28amt, TotSgstAmt, 
            TotCgstAmt, TotIgstAmt, gstpercent, cesssaleamt, cesspurchaseamt, taxable5amt, taxable12amt, taxable18amt, taxable28amt;
        DataTable dtPurchaseBill = new DataTable();
        DataTable dtSaleBill = new DataTable();
        DbClass db = new DbClass();

        public frmGstReports()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            defineDataTable();
        }

        private void defineDataTable()
        {
            dtPurchaseBill.Columns.Add("Gst%");
            dtPurchaseBill.Columns.Add("Taxable Amount");
            dtPurchaseBill.Columns.Add("Sgst Amount");
            dtPurchaseBill.Columns.Add("Cgst Amount");
            dtPurchaseBill.Columns.Add("Igst Amount");

            dtSaleBill.Columns.Add("Gst%");
            dtSaleBill.Columns.Add("Taxable Amount");
            dtSaleBill.Columns.Add("Sgst Amount");
            dtSaleBill.Columns.Add("Cgst Amount");
            dtSaleBill.Columns.Add("Igst Amount");
        }

        private void get3BData()
        {
            //Temporarily commented
            ////defineDataTable();

            ////string errormsg = "";
            ////Cgst5amt = 0;
            ////Cgst12amt = 0;
            ////Cgst18amt = 0;
            ////Cgst28amt = 0;
            ////Sgst5amt = 0;
            ////Sgst12amt = 0;
            ////Sgst18amt = 0;
            ////Sgst28amt = 0;
            ////Igst5amt = 0;
            ////Igst12amt = 0;
            ////Igst18amt = 0;
            ////Igst28amt = 0;
            ////TotSgstAmt = 0;
            ////TotCgstAmt = 0;
            ////TotIgstAmt = 0;
            ////taxable5amt = 0;
            ////taxable12amt = 0;
            ////taxable18amt = 0;
            ////taxable28amt = 0;
            ////cesssaleamt = 0;
            ////cesspurchaseamt = 0;

            ////decimal saleqty = 0;
            ////decimal VDis = 0;
            ////decimal VDetailtotamt = 0;
            ////SqlDataAdapter da;
            ////DataTable dtRecord;
            ////int billerror = 0;

            //////   //----------------------New code 2908----------Sale-//
            ////// //------------------Amount with unit=saleunit----------------//
            //////DataTable saledetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////DataTable saledetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (saledetail9.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < saledetail9.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(saledetail9.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail9.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail9.Rows[i][6].ToString());
            ////        taxable18amt += VDetailtotamt;
            ////        Sgst18amt += Convert.ToDecimal(saledetail9.Rows[i][0].ToString());
            ////        Cgst18amt += Convert.ToDecimal(saledetail9.Rows[i][1].ToString());
            ////        Igst18amt += Convert.ToDecimal(saledetail9.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(saledetail9.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 18 % rate from sale bill regular";
            ////    }
            ////}

            ////errormsg = "";

            ////DataTable saledetail6 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity * cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=6 or cd.igstpercent=12)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (saledetail6.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < saledetail6.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(saledetail6.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail6.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail6.Rows[i][6].ToString());
            ////        taxable12amt += VDetailtotamt;
            ////        Sgst12amt += Convert.ToDecimal(saledetail6.Rows[i][0].ToString());
            ////        Cgst12amt += Convert.ToDecimal(saledetail6.Rows[i][1].ToString());
            ////        Igst12amt += Convert.ToDecimal(saledetail6.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(saledetail6.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 18 % rate from sale bill regular";
            ////    }
            ////}

            ////errormsg = "";

            ////DataTable saledetail5 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=2.5 or cd.igstpercent=5)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (saledetail5.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < saledetail5.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(saledetail5.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail5.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail5.Rows[i][6].ToString());
            ////        taxable5amt += VDetailtotamt;
            ////        Sgst5amt += Convert.ToDecimal(saledetail5.Rows[i][0].ToString());
            ////        Cgst5amt += Convert.ToDecimal(saledetail5.Rows[i][1].ToString());
            ////        Igst5amt += Convert.ToDecimal(saledetail5.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(saledetail5.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 5 % rate from sale bill regular";
            ////    }
            ////}
            ////errormsg = "";
            ////DataTable saledetail14 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.quantity*cd.rate),sum(cd.quantity*cd.rate * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=14 or cd.igstpercent=28)) and cd.unit=cd.saleunit and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (saledetail14.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < saledetail14.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(saledetail14.Rows[i][3].ToString()) - Convert.ToDecimal(saledetail14.Rows[i][4].ToString()) - Convert.ToDecimal(saledetail14.Rows[i][6].ToString());
            ////        taxable28amt += VDetailtotamt;
            ////        Sgst28amt += Convert.ToDecimal(saledetail14.Rows[i][0].ToString());
            ////        Cgst28amt += Convert.ToDecimal(saledetail14.Rows[i][1].ToString());
            ////        Igst28amt += Convert.ToDecimal(saledetail14.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(saledetail14.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 28 % rate from sale bill regular";
            ////    }
            ////}
            ////errormsg = "";
            //////--------------------------amount with unit!=saleunit---- and rate!=salerate------------//
            ////DataTable salecompounddetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (salecompounddetail9.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < salecompounddetail9.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(salecompounddetail9.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][6].ToString());
            ////        Sgst18amt += Convert.ToDecimal(salecompounddetail9.Rows[i][0].ToString());
            ////        Cgst18amt += Convert.ToDecimal(salecompounddetail9.Rows[i][1].ToString());
            ////        Igst18amt += Convert.ToDecimal(salecompounddetail9.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(salecompounddetail9.Rows[i][5].ToString());
            ////        taxable18amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail9.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail9.Rows[i][2].ToString()));
            ////        errormsg = "Error on getting gst amount of 18 % rate from sale bill compound inclusive";
            ////    }
            ////}

            ////errormsg = "";

            ////DataTable salecompounddetail6 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=6 or cd.igstpercent=12)) and cd.unit!=cd.saleunit and (cd.salerate is null or cd.rate!=cd.salerate) and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (salecompounddetail6.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < salecompounddetail6.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(salecompounddetail6.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][6].ToString());
            ////        Sgst12amt += Convert.ToDecimal(salecompounddetail6.Rows[i][0].ToString());
            ////        Cgst12amt += Convert.ToDecimal(salecompounddetail6.Rows[i][1].ToString());
            ////        Igst12amt += Convert.ToDecimal(salecompounddetail6.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(salecompounddetail6.Rows[i][5].ToString());
            ////        taxable12amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail6.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail6.Rows[i][2].ToString()));
            ////        errormsg = "Error on getting gst amount of 18 % rate from sale bill compound inclusive";
            ////    }
            ////}

            ////errormsg = "";

            ////DataTable salecompounddetail5 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=2.5 or cd.igstpercent=5)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (salecompounddetail5.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < salecompounddetail5.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(salecompounddetail5.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][6].ToString());
            ////        Sgst5amt += Convert.ToDecimal(salecompounddetail5.Rows[i][0].ToString());
            ////        Cgst5amt += Convert.ToDecimal(salecompounddetail5.Rows[i][1].ToString());
            ////        Igst5amt += Convert.ToDecimal(salecompounddetail5.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(salecompounddetail5.Rows[i][5].ToString());
            ////        taxable5amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail5.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail5.Rows[i][2].ToString()));
            ////        errormsg = "Error on getting gst amount of 5 % rate from sale bill compound inclusive";
            ////    }
            ////}
            ////errormsg = "";
            ////DataTable salecompounddetail14 = db.Gettable("select sum(case when cd.sgstamt is null then 0 else cd.sgstamt end),sum(case when cd.cgstamt is null then 0 else cd.cgstamt end),sum(case when cd.igstamt is null then 0 else cd.igstamt end),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=14 or cd.igstpercent=28)) and cd.unit!=cd.saleunit and cd.rate!=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (salecompounddetail14.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < salecompounddetail14.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(salecompounddetail14.Rows[i][3].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][4].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][6].ToString());

            ////        Sgst28amt += Convert.ToDecimal(salecompounddetail14.Rows[i][0].ToString());
            ////        Cgst28amt += Convert.ToDecimal(salecompounddetail14.Rows[i][1].ToString());
            ////        Igst28amt += Convert.ToDecimal(salecompounddetail14.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(salecompounddetail14.Rows[i][5].ToString());
            ////        taxable28amt += (VDetailtotamt - Convert.ToDecimal(salecompounddetail14.Rows[i][0].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][1].ToString()) - Convert.ToDecimal(salecompounddetail14.Rows[i][2].ToString()));
            ////        errormsg = "Error on getting gst amount of 28 % rate from sale bill compound inclusive";
            ////    }
            ////}
            ////errormsg = "";
            //////--------------------------amount with unit!=saleunit---- and rate=salerate------------//
            ////DataTable sale1compounddetail9 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=9 or cd.igstpercent=18)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (sale1compounddetail9.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < sale1compounddetail9.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(sale1compounddetail9.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail9.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail9.Rows[i][6].ToString());
            ////        taxable18amt += VDetailtotamt;
            ////        Sgst18amt += Convert.ToDecimal(sale1compounddetail9.Rows[i][0].ToString());
            ////        Cgst18amt += Convert.ToDecimal(sale1compounddetail9.Rows[i][1].ToString());
            ////        Igst18amt += Convert.ToDecimal(sale1compounddetail9.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(sale1compounddetail9.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 18 % rate from sale bill compound regular";
            ////    }
            ////}

            ////errormsg = "";

            ////DataTable sale1compounddetail6 = db.Gettable("Select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=6 or cd.igstpercent=12)) and cd.unit!=cd.saleunit and (cd.rate=cd.salerate) and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (sale1compounddetail6.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < sale1compounddetail6.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(sale1compounddetail6.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail6.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail6.Rows[i][6].ToString());
            ////        taxable12amt += VDetailtotamt;
            ////        Sgst12amt += Convert.ToDecimal(sale1compounddetail6.Rows[i][0].ToString());
            ////        Cgst12amt += Convert.ToDecimal(sale1compounddetail6.Rows[i][1].ToString());
            ////        Igst12amt += Convert.ToDecimal(sale1compounddetail6.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(sale1compounddetail6.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 12 % rate from sale bill compound regular";
            ////    }
            ////}

            ////errormsg = "";

            ////DataTable sale1compounddetail5 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=2.5 or cd.igstpercent=5)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (sale1compounddetail5.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < sale1compounddetail5.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(sale1compounddetail5.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail5.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail5.Rows[i][6].ToString());
            ////        taxable5amt += VDetailtotamt;
            ////        Sgst5amt += Convert.ToDecimal(sale1compounddetail5.Rows[i][0].ToString());
            ////        Cgst5amt += Convert.ToDecimal(sale1compounddetail5.Rows[i][1].ToString());
            ////        Igst5amt += Convert.ToDecimal(sale1compounddetail5.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(sale1compounddetail5.Rows[i][5].ToString());
            ////        errormsg = "Error on getting gst amount of 5 % rate from sale bill compound regular";
            ////    }
            ////}
            ////errormsg = "";
            ////DataTable sale1compounddetail14 = db.Gettable("select sum(cd.sgstamt),sum(cd.cgstamt),sum(cd.igstamt),sum(cd.amount),sum(cd.amount * cd.discount/100),case when sum(cd.cessamt) is null then 0 else sum(cd.cessamt) end,case when sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) is null then 0 else sum(((cd.quantity*cd.rate)-(cd.quantity * cd.rate * cd.discount/100))*cd.SpcDisc/100) end from customerbillmaster as cm join customerbilldetail as cd on cm.Id=cd.billNo where cm.billdate>='" + frdate + "' and cm.billdate<='" + todate + "' and (cd.sgstpercent is not null and (cd.sgstpercent=14 or cd.igstpercent=28)) and cd.unit!=cd.saleunit and cd.rate=cd.salerate and cm.CompId='" + CommonMethod.CompId + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.BillNo like 'T%' group by cm.id");
            ////if (sale1compounddetail14.Rows.Count > 0)
            ////{
            ////    for (int i = 0; i < sale1compounddetail14.Rows.Count; i++)
            ////    {
            ////        VDetailtotamt = Convert.ToDecimal(sale1compounddetail14.Rows[i][3].ToString()) - Convert.ToDecimal(sale1compounddetail14.Rows[i][4].ToString()) - Convert.ToDecimal(sale1compounddetail14.Rows[i][6].ToString());
            ////        taxable28amt += VDetailtotamt;
            ////        Sgst28amt += Convert.ToDecimal(sale1compounddetail14.Rows[i][0].ToString());
            ////        Cgst28amt += Convert.ToDecimal(sale1compounddetail14.Rows[i][1].ToString());
            ////        Igst28amt += Convert.ToDecimal(sale1compounddetail14.Rows[i][2].ToString());
            ////        cesssaleamt += Convert.ToDecimal(sale1compounddetail14.Rows[i][5].ToString());

            ////        errormsg = "Error on getting gst amount of 28 % rate from sale bill compound regular";
            ////    }
            ////}
            ////errormsg = "";

            ////for (int j = 0; j < 4; j++)
            ////{
            ////    if (j == 0)
            ////        dtSaleBill.Rows.Add(new object[5] { 5, taxable5amt, Cgst5amt, Sgst5amt, Igst5amt});
            ////    else if (j == 1)
            ////        dtSaleBill.Rows.Add(new object[5] { 12, taxable12amt, Cgst12amt, Sgst12amt, Igst12amt });
            ////    else if (j == 2)
            ////        dtSaleBill.Rows.Add(new object[5] { 18, taxable18amt, Cgst18amt, Sgst18amt, Igst18amt });
            ////    else if (j == 3)
            ////        dtSaleBill.Rows.Add(new object[5] { 28, taxable28amt, Cgst28amt, Sgst28amt, Igst28amt});
            ////}
            ////////////////get purchase data
        }
    }
}
