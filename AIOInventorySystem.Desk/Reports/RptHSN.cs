using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptHSN : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        static string strFlag = "";

        public RptHSN(string flag)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            strFlag = flag;
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
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                getNewHSNReport(cm.ValidFromDate(dtpMonth.Value.Date), cm.ValidToDate(dtptomonth.Value.Date));
            }
            catch (Exception)
            { }
        }

        public void getallHSN(string fdate, string tdate)
        {
            string HSN, Description, UQC;
            decimal Totalqty = 0, TotalValue = 0, taxableamt = 0, IntegratedTaxAmt = 0, CentralTaxAmt = 0, StateTaxAmt = 0, TotalCess = 0, disamt = 0, spdisamt = 0,
                tottaxable = 0;
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSHSN ds = new DSHSN();
                DataTable dataHSN;
                DataTable dtdetail = null;
                if (CommonMethod.commProduct == true)
                    dtdetail = db.GetTable("Select  distinct(HSNCode) from ProductInformation where HSNCode!='' and HSNCode is not null");
                else
                    dtdetail = db.GetTable("Select  distinct(HSNCode) from ProductInformation where HSNCode!='' and HSNCode is not null and compid=" + CommonMethod.CompId);
                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        DataTable dtdetail1 = db.Gettable("Select cd.billno as invoiceno," +  //0
                                              "cd.quantity,cd.rate,cd.unit,cd.saleunit,case when cd.cessamt is null then 0 else cd.cessamt end as cessamt,cd.amount,cd.sgstamt,cd.cgstamt,cd.igstamt," + //1,2,3,4,5,6,7,8,9
                                              "case when cd.discount is null then 0 else cd.discount end as discount,case when cd.spcdisc is null then 0 else cd.spcdisc end as spcdisc," + //10,11
                                              "case when cd.salerate is null then cd.rate else cd.salerate end as salerate,cd.companyname,cd.ProductId from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno" + //12,13,14
                                              " Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear +
                                              "' and cm.CompId=" + CommonMethod.CompId + " and cm.BillNo like 'T%' and cd.HSNCode='" + Convert.ToString(dtdetail.Rows[i]["HSNCode"]) + "'");
                        if (dtdetail1.Rows.Count > 0)
                        {
                            HSN = dtdetail.Rows[i]["HSNCode"].ToString();
                            Description = "Goods";
                            UQC = "";
                            Totalqty = 0;
                            TotalValue = 0;
                            taxableamt = 0;
                            IntegratedTaxAmt = 0;
                            CentralTaxAmt = 0;
                            StateTaxAmt = 0;
                            TotalCess = 0;
                            disamt = 0;
                            spdisamt = 0;
                            tottaxable = 0;
                            for (int j = 0; j < dtdetail1.Rows.Count; j++)
                            {
                                System.Windows.Forms.Application.DoEvents();
                                Totalqty += Convert.ToDecimal(dtdetail1.Rows[j]["quantity"].ToString());
                                if (dtdetail1.Rows[j]["unit"].ToString() != dtdetail1.Rows[j]["saleunit"].ToString())   //Alternate unit
                                {
                                    UQC = dtdetail1.Rows[j]["saleunit"].ToString();
                                    if (Convert.ToDecimal(dtdetail1.Rows[j]["rate"].ToString()) != Convert.ToDecimal(dtdetail1.Rows[j]["salerate"].ToString()))
                                    {
                                        ProductRepository ProductRepo = new ProductRepository();
                                        ProductInformation proddata = new ProductInformation();
                                        if (CommonMethod.commProduct == true)
                                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail1.Rows[j]["ProductId"].ToString() && t.ManufactureCompany == dtdetail1.Rows[j]["companyname"].ToString() && t.Uniti == dtdetail1.Rows[j]["unit"].ToString()).FirstOrDefault();
                                        else
                                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail1.Rows[j]["ProductId"].ToString() && t.ManufactureCompany == dtdetail1.Rows[j]["companyname"].ToString() && t.Uniti == dtdetail1.Rows[j]["unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (proddata != null)
                                        {
                                            if (dtdetail1.Rows[j]["saleunit"].ToString() == proddata.AlternateUnit)
                                            {
                                                Totalqty = Convert.ToDecimal(Convert.ToDecimal(dtdetail1.Rows[j]["quantity"].ToString()) / proddata.UnitQty);
                                                taxableamt = Totalqty * Convert.ToDecimal(dtdetail1.Rows[j]["rate"].ToString());
                                            }
                                        }
                                        ProductRepo.Dispose();
                                    }
                                    else
                                        taxableamt = Convert.ToDecimal(dtdetail1.Rows[j]["amount"].ToString());
                                }
                                else
                                    taxableamt = Convert.ToDecimal(dtdetail1.Rows[j]["quantity"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["rate"].ToString());
                                if (Convert.ToDecimal(dtdetail1.Rows[j]["discount"].ToString()) != 0)
                                {
                                    disamt = (Convert.ToDecimal(dtdetail1.Rows[j]["amount"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["discount"].ToString()) / 100);
                                    taxableamt = (taxableamt - disamt);
                                }
                                if (Convert.ToDecimal(dtdetail1.Rows[j]["spcdisc"].ToString()) != 0)
                                {
                                    spdisamt = (Convert.ToDecimal(dtdetail1.Rows[j]["amount"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["spcdisc"].ToString()) / 100);
                                    taxableamt = (taxableamt - spdisamt);
                                }
                                tottaxable = tottaxable + taxableamt;
                                TotalCess += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                                IntegratedTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["igstamt"].ToString());
                                CentralTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["cgstamt"].ToString());
                                StateTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["sgstamt"].ToString());
                                TotalValue = tottaxable + TotalCess + IntegratedTaxAmt + CentralTaxAmt + StateTaxAmt;
                            }
                            dataHSN = ds.Tables["HSNData"];
                            dataHSN.Rows.Add(new object[10] { HSN, Description, UQC, Totalqty, TotalValue, tottaxable, IntegratedTaxAmt, CentralTaxAmt, StateTaxAmt, TotalCess });
                        }
                    }
                }

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                HSNGroupWiseReport salerpt = new HSNGroupWiseReport();
                salerpt.SetDataSource(ds);
                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
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

        public void getall(string fdate, string tdate)
        {
            string HSN, Description, UQC;
            decimal Totalqty = 0, TotalValue = 0, taxableamt = 0, IntegratedTaxAmt = 0, CentralTaxAmt = 0, StateTaxAmt = 0, TotalCess = 0, disamt = 0, spdisamt = 0,
                tottaxable = 0;
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSHSN ds = new DSHSN();
                DataTable dataHSN, dtdetail = null;
                if (CommonMethod.commProduct == true)
                    dtdetail = db.GetTable("Select HSNCode,ProductName,ManufactureCompany,Uniti from ProductInformation where HSNCode!='' and HSNCode is not null order by HSNCode");
                else
                    dtdetail = db.GetTable("Select HSNCode,ProductName,ManufactureCompany,Uniti from ProductInformation where HSNCode!='' and HSNCode is not null and compid=" + CommonMethod.CompId + " order by HSNCode");
                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        string Proname1 = Convert.ToString(dtdetail.Rows[i]["ProductName"]);
                        string Proname = "";
                        if ((Proname1).Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        DataTable dtdetail1 = db.Gettable("Select cd.billno as invoiceno," +  //0
                                              "cd.quantity,cd.rate,cd.unit,cd.saleunit,case when cd.cessamt is null then 0 else cd.cessamt end as cessamt,cd.amount,cd.sgstamt,cd.cgstamt,cd.igstamt," + //1,2,3,4,5,6,7,8,9
                                              "case when cd.discount is null then 0 else cd.discount end as discount,case when cd.spcdisc is null then 0 else cd.spcdisc end as spcdisc," + //10,11
                                              "case when cd.salerate is null then cd.rate else cd.salerate end as salerate,cd.companyname,cd.ProductId from CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno" + //12,13,14
                                              "  Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear +
                                              "' and cm.CompId=" + CommonMethod.CompId + " and cm.BillNo like 'T%' and cd.ProductId='" + Proname + "' and cd.CompanyName='" + Convert.ToString(dtdetail.Rows[i]["ManufactureCompany"]) + "' and cd.Unit='" + Convert.ToString(dtdetail.Rows[i]["Uniti"]) + "'");
                        if (dtdetail1.Rows.Count > 0)
                        {
                            HSN = dtdetail.Rows[i]["HSNCode"].ToString();
                            Description = dtdetail.Rows[i]["ProductName"].ToString();
                            UQC = dtdetail.Rows[i]["Uniti"].ToString();
                            Totalqty = 0;
                            TotalValue = 0;
                            taxableamt = 0;
                            IntegratedTaxAmt = 0;
                            CentralTaxAmt = 0;
                            StateTaxAmt = 0;
                            TotalCess = 0;
                            disamt = 0;
                            spdisamt = 0;
                            tottaxable = 0;
                            for (int j = 0; j < dtdetail1.Rows.Count; j++)
                            {
                                System.Windows.Forms.Application.DoEvents();
                                Totalqty += Convert.ToDecimal(dtdetail1.Rows[j]["quantity"].ToString());
                                if (dtdetail1.Rows[j]["unit"].ToString() != dtdetail1.Rows[j]["saleunit"].ToString())   //Alternate unit
                                {
                                    UQC = dtdetail1.Rows[j]["saleunit"].ToString();
                                    if (Convert.ToDecimal(dtdetail1.Rows[j]["rate"].ToString()) != Convert.ToDecimal(dtdetail1.Rows[j]["salerate"].ToString()))
                                    {
                                        ProductRepository ProductRepo = new ProductRepository();
                                        ProductInformation proddata = new ProductInformation();
                                        if (CommonMethod.commProduct == true)
                                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail1.Rows[j]["ProductId"].ToString() && t.ManufactureCompany == dtdetail1.Rows[j]["companyname"].ToString() && t.Uniti == dtdetail1.Rows[j]["unit"].ToString()).FirstOrDefault();
                                        else
                                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail1.Rows[j]["ProductId"].ToString() && t.ManufactureCompany == dtdetail1.Rows[j]["companyname"].ToString() && t.Uniti == dtdetail1.Rows[j]["unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (proddata != null)
                                        {
                                            if (dtdetail1.Rows[j]["saleunit"].ToString() == proddata.AlternateUnit)
                                            {
                                                Totalqty = Convert.ToDecimal(Convert.ToDecimal(dtdetail1.Rows[j]["quantity"].ToString()) / proddata.UnitQty);
                                                taxableamt = Totalqty * Convert.ToDecimal(dtdetail1.Rows[j]["rate"].ToString());
                                            }
                                        }
                                        ProductRepo.Dispose();
                                    }
                                    else
                                        taxableamt = Convert.ToDecimal(dtdetail1.Rows[j]["amount"].ToString());
                                }
                                else
                                    taxableamt = Convert.ToDecimal(dtdetail1.Rows[j]["quantity"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["rate"].ToString());
                                if (Convert.ToDecimal(dtdetail1.Rows[j]["discount"].ToString()) != 0)
                                {
                                    disamt = (Convert.ToDecimal(dtdetail1.Rows[j]["amount"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["discount"].ToString()) / 100);
                                    taxableamt = (taxableamt - disamt);
                                }
                                if (Convert.ToDecimal(dtdetail1.Rows[j]["spcdisc"].ToString()) != 0)
                                {
                                    spdisamt = (Convert.ToDecimal(dtdetail1.Rows[j]["amount"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["spcdisc"].ToString()) / 100);
                                    taxableamt = (taxableamt - spdisamt);
                                }
                                tottaxable = tottaxable + taxableamt;
                                TotalCess += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                                IntegratedTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["igstamt"].ToString());
                                CentralTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["cgstamt"].ToString());
                                StateTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["sgstamt"].ToString());
                                TotalValue = tottaxable + TotalCess + IntegratedTaxAmt + CentralTaxAmt + StateTaxAmt;
                            }
                            dataHSN = ds.Tables["HSNData"];
                            dataHSN.Rows.Add(new object[10] { HSN, Description, UQC, Totalqty, TotalValue, tottaxable, IntegratedTaxAmt, CentralTaxAmt, StateTaxAmt, TotalCess });
                        }
                    }
                }

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                HSNSummaryReport salerpt = new HSNSummaryReport();
                salerpt.SetDataSource(ds);
                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
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

        public void getallnew(string fdate, string tdate)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSHSN ds = new DSHSN();
                System.Windows.Forms.Application.DoEvents();
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select pd.HSNCode as HSN, pd.ProductName as Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(cd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(cd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(cd.TotalAmount,0)),0)- Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt+cd.IGstAmt,0)),0) as TaxableValue,Isnull(Sum(Isnull(cd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(cd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(cd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(cd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno Where pd.HSNCode!='' and pd.HSNCode is not null and cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' and cd.ProductId=pd.ProductName and cd.CompanyName=pd.ManufactureCompany and cd.Unit=pd.Uniti and pd.Uniti=un.UnitName group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName", db.Connection);
                else
                    da = new SqlDataAdapter("Select pd.HSNCode as HSN, pd.ProductName as Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(cd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(cd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(cd.TotalAmount,0)),0)- Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt+cd.IGstAmt,0)),0) as TaxableValue,Isnull(Sum(Isnull(cd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(cd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(cd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(cd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno Where pd.HSNCode!='' and pd.HSNCode is not null and cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' and cd.ProductId=pd.ProductName and cd.CompanyName=pd.ManufactureCompany and cd.Unit=pd.Uniti and pd.Uniti=un.UnitName and cm.compid=pd.compid group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "HSNData");

                System.Windows.Forms.Application.DoEvents();
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                HSNSummaryReport salerpt = new HSNSummaryReport();
                salerpt.SetDataSource(ds);
                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
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

        public void getNewHSNReport(string fdate, string tdate)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSHSN ds = new DSHSN();
                System.Windows.Forms.Application.DoEvents();
                DataTable dtdetail = null, dataHSN;

                if (CommonMethod.commProduct == true)
                    dtdetail = db.GetTable("Select distinct(HSNCode) from ProductInformation where HSNCode!='' and HSNCode is not null order by HSNCode");
                else
                    dtdetail = db.GetTable("Select distinct(HSNCode) from ProductInformation where HSNCode!='' and HSNCode is not null and compid=" + CommonMethod.CompId + " order by HSNCode");
                DataTable dtdetail1 = null;
                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        if (CommonMethod.commProduct == true)
                        {
                            if (strFlag == "R1")
                                dtdetail1 = db.Gettable("Select pd.HSNCode as HSN, pd.ProductName as Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(cd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(cd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(cd.TotalAmount,0)),0)- Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt+cd.IGstAmt,0)),0) as TaxableValue,Isnull(Sum(Isnull(cd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(cd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(cd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(cd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId=" + CommonMethod.CompId + " and cm.BillNo like 'T%' and cd.ProductId=pd.ProductName and cd.CompanyName=pd.ManufactureCompany and cd.Unit=pd.Uniti and pd.Uniti=un.UnitName and cd.HSNCode='" + Convert.ToString(dtdetail.Rows[i]["HSNCode"]) + "' group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName");
                            else
                                dtdetail1 = db.Gettable("Select pd.HSNCode as HSN, pd.ProductName as Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(prd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(prd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(prd.TotalAmount,0)),0)- Isnull(sum(Isnull(prd.CGstAmt+prd.SGstAmt+prd.IGstAmt,0)),0) as TaxableValue, Isnull(Sum(Isnull(prd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(prd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(prd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(prd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,PurchaseMaster as pm join PurchaseDetail as prd on pm.id=prd.PorderNo Where pm.PorderDate>='" + fdate + "' and pm.PorderDate<='" + tdate + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + " and prd.ProductName=pd.ProductName and prd.CompanyName=pd.ManufactureCompany and prd.Unit=pd.Uniti and pd.Uniti=un.UnitName and pd.HSNCode='" + Convert.ToString(dtdetail.Rows[i]["HSNCode"]) + "' group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName");
                        }
                        else
                        {
                            if (strFlag == "R1")
                                dtdetail1 = db.Gettable("Select pd.HSNCode as HSN, pd.ProductName as Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(cd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(cd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(cd.TotalAmount,0)),0)- Isnull(sum(Isnull(cd.CGstAmt+cd.SGstAmt+cd.IGstAmt,0)),0) as TaxableValue,Isnull(Sum(Isnull(cd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(cd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(cd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(cd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,CustomerBillMaster as cm join customerbilldetail as cd on cm.id=cd.billno Where cm.billdate>='" + fdate + "' and cm.billdate<='" + tdate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId=" + CommonMethod.CompId + " and cm.BillNo like 'T%' and cd.ProductId=pd.ProductName and cd.CompanyName=pd.ManufactureCompany and cd.Unit=pd.Uniti and pd.Uniti=un.UnitName and cm.compid=pd.compid and cd.HSNCode='" + Convert.ToString(dtdetail.Rows[i]["HSNCode"]) + "' group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName");
                            else
                                dtdetail1 = db.Gettable("Select pd.HSNCode as HSN, pd.ProductName as Description, un.GSTUnitName as UQC,Isnull(sum(Isnull(prd.Quantity,0)),0) as TotalQuantity,Isnull(Sum(Isnull(prd.TotalAmount,0)),0) as TotalValue, Isnull(sum(Isnull(prd.TotalAmount,0)),0)- Isnull(sum(Isnull(prd.CGstAmt+prd.SGstAmt+prd.IGstAmt,0)),0) as TaxableValue, Isnull(Sum(Isnull(prd.IGstAmt,0)),0) as IntegratedTaxAmt, Isnull(Sum(Isnull(prd.CGstAmt,0)),0) as CentralTaxAmt, Isnull(Sum(Isnull(prd.SGstAmt,0)),0) as StateTaxAmt,Isnull(Sum(Isnull(prd.CessAmt,0)),0) as TotalCess from ProductInformation pd,Unit un ,PurchaseMaster as pm join PurchaseDetail as prd on pm.id=prd.PorderNo Where pm.PorderDate>='" + fdate + "' and pm.PorderDate<='" + tdate + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + " and prd.ProductName=pd.ProductName and prd.CompanyName=pd.ManufactureCompany and prd.Unit=pd.Uniti and pd.Uniti=un.UnitName and pm.compid=pd.compid and pd.HSNCode='" + Convert.ToString(dtdetail.Rows[i]["HSNCode"]) + "' group by pd.HSNCode,pd.ProductName,pd.ManufactureCompany,un.GSTUnitName");
                        }
                        if (dtdetail1.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtdetail1.Rows.Count; j++)
                            {
                                dataHSN = ds.Tables["HSNData"];
                                dataHSN.Rows.Add(new object[10] { dtdetail1.Rows[j]["HSN"].ToString(), dtdetail1.Rows[j]["Description"].ToString(), dtdetail1.Rows[j]["UQC"].ToString(), dtdetail1.Rows[j]["TotalQuantity"].ToString(), dtdetail1.Rows[j]["TotalValue"].ToString(), dtdetail1.Rows[j]["TaxableValue"].ToString(), dtdetail1.Rows[j]["IntegratedTaxAmt"].ToString(), dtdetail1.Rows[j]["CentralTaxAmt"].ToString(), dtdetail1.Rows[j]["StateTaxAmt"].ToString(), dtdetail1.Rows[j]["TotalCess"].ToString() });
                            }
                        }
                    }
                }

                System.Windows.Forms.Application.DoEvents();
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                HSNSummaryReport salerpt = new HSNSummaryReport();
                salerpt.SetDataSource(ds);
                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
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