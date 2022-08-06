using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptGSTR2 : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        string Flag;

        public RptGSTR2()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
        }

        public RptGSTR2(string flag)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Flag = flag;
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

        public void getall(string fdate, string tdate,string Flag)
        {
            //string GSTIn, InvNo, supppono, SupplierName;
            //DateTime InvDate;
            //decimal TotalValue = 0, taxableamt = 0, IntegratedTaxAmt = 0, CentralTaxAmt = 0, StateTaxAmt = 0, TotalCess = 0, disamt = 0, 
            //        spdisamt = 0, tottaxable = 0, Totalqty = 0, othDiscAmt = 0, 
            //        taxableamt5 = 0, IntegratedTaxAmt5 = 0, CentralTaxAmt5 = 0, StateTaxAmt5 = 0, TotalCess5 = 0,
            //        taxableamt12 = 0, IntegratedTaxAmt12 = 0, CentralTaxAmt12 = 0, StateTaxAmt12 = 0, TotalCess12 = 0,
            //        taxableamt18 = 0, IntegratedTaxAmt18 = 0, CentralTaxAmt18 = 0, StateTaxAmt18 = 0, TotalCess18 = 0,
            //        taxableamt28 = 0, IntegratedTaxAmt28 = 0, CentralTaxAmt28 = 0, StateTaxAmt28 = 0, TotalCess28 = 0;
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSGSTR2 ds = new DSGSTR2();
                //DataTable dataHSN;
                ////                                                  0          1               2              3            4           5               6                                 7       
                //DataTable dtdetail = db.GetTable("Select distinct pm.Id,pm.PorderDate,pm.Suppliername,pm.TotalAmount,pm.Discount,pm.NetAmount,pm.SupplierpoNo,Isnull(pm.TAXType,0) as 'TAXType' from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo where pm.Suppliername!='First stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and pm.PorderDate>='" + fdate + "' and pm.PorderDate<='" + tdate + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");

                //if (dtdetail.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtdetail.Rows.Count; i++)
                //    {
                //        InvNo = dtdetail.Rows[i]["Id"].ToString();
                //        InvDate = Convert.ToDateTime(dtdetail.Rows[i]["PorderDate"].ToString());
                //        SupplierName = dtdetail.Rows[i]["Suppliername"].ToString();
                //        SupplierRepository sRepo = new SupplierRepository();
                //        SupplierInformation sdata = new SupplierInformation();
                //        if (CommonMethod.commProduct == true)
                //            sdata = sRepo.GetAll().Where(t => t.SupplierName == SupplierName).FirstOrDefault();
                //        else
                //            sdata = sRepo.GetAll().Where(t => t.SupplierName == SupplierName && t.CompId == CommonMethod.CompId).FirstOrDefault();
                //        if (sdata != null)
                //            GSTIn = sdata.VatNo;
                //        else
                //            GSTIn = "";
                //        supppono = dtdetail.Rows[i]["SupplierpoNo"].ToString();
                //        //                                              0               1           2
                //        DataTable dtdetail1 = db.GetTable("select pd.ProductName,pd.CompanyName,pd.Unit," +
                //            //                                                                              3                                       4
                //            "case when pd.PurchaseUnit is null then pd.Unit else pd.PurchaseUnit end as PurchaseUnit,isnull(pd.SGstPercent,0) as SGstPercent," +
                //            //                          5                                   6                                   7
                //            "isnull(pd.SGstAmt,0) as SGstAmt,isnull(pd.CGstPercent,0) as CGstPercent,isnull(pd.CGstAmt,0) as CGstAmt," +
                //            //                              8                                   9                               10
                //            "isnull(pd.IGstPercent,0) as IGstPercent,isnull(pd.IGstAmt,0) as IGstAmt,isnull(pd.Discount,0) as Discount," +
                //            //                          11                              12                           13
                //            "isnull(pd.SpcDisc,0) as SpcDisc,isnull(pd.Quantity,0) as Quantity,isnull(pd.Amount,0) Amount," +
                //            //                                  14                                      15                                  16                            
                //            "isnull(pd.PurchasePrice,0) as PurchasePrice,isnull(pd.TotalAmount,0) as TotalAmount,isnull(pd.cessamt,0) as cessamt," +
                //            //                                  17
                //            "isnull(ThreeLevelDiscPer,0) as 'OthDisc' " +
                //            "from PurchaseDetail as pd  where pd.PorderNo='" + InvNo + "' order by pd.ID");

                //        if (dtdetail1.Rows.Count > 0)
                //        {
                //            TotalValue = 0;
                //            taxableamt = 0;
                //            IntegratedTaxAmt = 0;
                //            CentralTaxAmt = 0;
                //            StateTaxAmt = 0;
                //            TotalCess = 0; IntegratedTaxAmt5 = 0;
                //            CentralTaxAmt5 = 0;
                //            StateTaxAmt5 = 0;
                //            TotalCess5 = 0; IntegratedTaxAmt12 = 0;
                //            CentralTaxAmt12 = 0;
                //            StateTaxAmt12 = 0;
                //            TotalCess12 = 0; IntegratedTaxAmt18 = 0;
                //            CentralTaxAmt18 = 0;
                //            StateTaxAmt18 = 0;
                //            TotalCess18 = 0; IntegratedTaxAmt28 = 0;
                //            CentralTaxAmt28 = 0;
                //            StateTaxAmt28 = 0;
                //            TotalCess28 = 0;
                //            disamt = 0;
                //            spdisamt = 0;
                //            tottaxable = 0;
                //            Totalqty = 0; taxableamt5 = 0; taxableamt12 = 0; taxableamt18 = 0; taxableamt28 = 0;

                //            for (int j = 0; j < dtdetail1.Rows.Count; j++)
                //            {
                //                taxableamt = 0;
                //                Totalqty += Convert.ToDecimal(dtdetail1.Rows[j]["Quantity"].ToString());
                //                if (dtdetail1.Rows[j]["Unit"].ToString() != dtdetail1.Rows[j]["PurchaseUnit"].ToString())   //Alternate unit
                //                {
                //                    ProductRepository ProductRepo = new ProductRepository();
                //                    ProductInformation proddata = new ProductInformation();
                //                    if (CommonMethod.commProduct == true)
                //                        proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail1.Rows[j]["ProductName"].ToString() && t.ManufactureCompany == dtdetail1.Rows[j]["CompanyName"].ToString() && t.Uniti == dtdetail1.Rows[j]["Unit"].ToString()).FirstOrDefault();
                //                    else
                //                        proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtdetail1.Rows[j]["ProductName"].ToString() && t.ManufactureCompany == dtdetail1.Rows[j]["CompanyName"].ToString() && t.Uniti == dtdetail1.Rows[j]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();

                //                    if (proddata != null)
                //                    {
                //                        if (dtdetail1.Rows[j]["PurchaseUnit"].ToString() == proddata.AlternateUnit)
                //                        {
                //                            Totalqty = Convert.ToDecimal(Convert.ToDecimal(dtdetail1.Rows[j]["Quantity"].ToString()) / proddata.UnitQty);
                //                            taxableamt = Totalqty * Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtdetail1.Rows[j]["PurchasePrice"].ToString()), 3, MidpointRounding.AwayFromZero));
                //                        }
                //                    }
                //                    ProductRepo.Dispose();
                //                }
                //                else
                //                    taxableamt = Convert.ToDecimal(Math.Round((Convert.ToDecimal(dtdetail1.Rows[j]["Quantity"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["PurchasePrice"].ToString())), 3, MidpointRounding.AwayFromZero));
                //                if (Convert.ToDecimal(dtdetail1.Rows[j]["Discount"].ToString()) != 0)
                //                {
                //                    disamt = (Convert.ToDecimal(dtdetail1.Rows[j]["Amount"].ToString()) * Convert.ToDecimal(dtdetail1.Rows[j]["Discount"].ToString()) / 100);
                //                    taxableamt = Convert.ToDecimal(Math.Round((taxableamt - disamt), 3, MidpointRounding.AwayFromZero));
                //                }
                //                if (Convert.ToDecimal(dtdetail1.Rows[j]["SpcDisc"].ToString()) != 0)
                //                {
                //                    spdisamt = (Convert.ToDecimal(taxableamt) * Convert.ToDecimal(dtdetail1.Rows[j]["SpcDisc"].ToString()) / 100);
                //                    taxableamt = Convert.ToDecimal(Math.Round((taxableamt - spdisamt), 3, MidpointRounding.AwayFromZero));
                //                }
                //                if (Convert.ToDecimal(dtdetail1.Rows[j]["OthDisc"].ToString()) != 0)
                //                {
                //                    othDiscAmt = (Convert.ToDecimal(taxableamt) * Convert.ToDecimal(dtdetail1.Rows[j]["OthDisc"].ToString()) / 100);
                //                    taxableamt = Convert.ToDecimal(Math.Round((taxableamt - othDiscAmt), 3, MidpointRounding.AwayFromZero));
                //                }
                //                if ((Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) == Convert.ToDecimal(2.5))
                //                    || (Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) == Convert.ToDecimal(5)))
                //                {
                //                    taxableamt5 = taxableamt5 + taxableamt;
                //                    TotalCess5 += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                //                    CentralTaxAmt5 += Convert.ToDecimal(dtdetail1.Rows[j]["CGstAmt"].ToString());
                //                    StateTaxAmt5 += Convert.ToDecimal(dtdetail1.Rows[j]["SGstAmt"].ToString());
                //                    IntegratedTaxAmt5 += Convert.ToDecimal(dtdetail1.Rows[j]["IGstAmt"].ToString());
                //                }
                //                else if ((Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) == Convert.ToDecimal(6))
                //                    || (Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) == Convert.ToDecimal(12)))
                //                {
                //                    taxableamt12 = taxableamt12 + taxableamt;
                //                    TotalCess12 += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                //                    CentralTaxAmt12 += Convert.ToDecimal(dtdetail1.Rows[j]["CGstAmt"].ToString());
                //                    StateTaxAmt12 += Convert.ToDecimal(dtdetail1.Rows[j]["SGstAmt"].ToString());
                //                    IntegratedTaxAmt12 += Convert.ToDecimal(dtdetail1.Rows[j]["IGstAmt"].ToString());
                //                }
                //                else if ((Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) == Convert.ToDecimal(9))
                //                    || (Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) == Convert.ToDecimal(18)))
                //                {
                //                    taxableamt18 = taxableamt18 + taxableamt;
                //                    TotalCess18 += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                //                    CentralTaxAmt18 += Convert.ToDecimal(dtdetail1.Rows[j]["CGstAmt"].ToString());
                //                    StateTaxAmt18 += Convert.ToDecimal(dtdetail1.Rows[j]["SGstAmt"].ToString());
                //                    IntegratedTaxAmt18 += Convert.ToDecimal(dtdetail1.Rows[j]["IGstAmt"].ToString());
                //                }
                //                else if ((Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["SGstPercent"].ToString()) == Convert.ToDecimal(14))
                //                    || (Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) != 0 && Convert.ToDecimal(dtdetail1.Rows[j]["IGstPercent"].ToString()) == Convert.ToDecimal(28)))
                //                {
                //                    taxableamt28 = taxableamt28 + taxableamt;
                //                    TotalCess28 += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                //                    CentralTaxAmt28 += Convert.ToDecimal(dtdetail1.Rows[j]["CGstAmt"].ToString());
                //                    StateTaxAmt28 += Convert.ToDecimal(dtdetail1.Rows[j]["SGstAmt"].ToString());
                //                    IntegratedTaxAmt28 += Convert.ToDecimal(dtdetail1.Rows[j]["IGstAmt"].ToString());
                //                }
                //                //tottaxable = tottaxable + taxableamt;
                //                //TotalCess += Convert.ToDecimal(dtdetail1.Rows[j]["cessamt"].ToString());
                //                //IntegratedTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["IGstAmt"].ToString());
                //                //CentralTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["CGstAmt"].ToString());
                //                //StateTaxAmt += Convert.ToDecimal(dtdetail1.Rows[j]["SGstAmt"].ToString());
                //                //TotalValue = tottaxable + TotalCess + IntegratedTaxAmt + CentralTaxAmt + StateTaxAmt;
                //            }

                //            dataHSN = ds.Tables["GSTR2"];
                //            if (taxableamt5 != 0)
                //                dataHSN.Rows.Add(new object[11] { SupplierName, GSTIn, InvDate, supppono, Convert.ToDecimal(Math.Round(taxableamt5, 3, MidpointRounding.AwayFromZero)), IntegratedTaxAmt5, CentralTaxAmt5, StateTaxAmt5, TotalCess5, 5, Convert.ToDecimal(Math.Round(taxableamt5, 3, MidpointRounding.AwayFromZero)) + IntegratedTaxAmt5 + CentralTaxAmt5 + StateTaxAmt5 + TotalCess5 });
                //            if (taxableamt12 != 0)
                //                dataHSN.Rows.Add(new object[11] { SupplierName, GSTIn, InvDate, supppono, Convert.ToDecimal(Math.Round(taxableamt12, 3, MidpointRounding.AwayFromZero)), IntegratedTaxAmt12, CentralTaxAmt12, StateTaxAmt12, TotalCess12, 12, Convert.ToDecimal(Math.Round(taxableamt12, 3, MidpointRounding.AwayFromZero)) + IntegratedTaxAmt12 + CentralTaxAmt12 + StateTaxAmt12 + TotalCess12 });
                //            if (taxableamt18 != 0)
                //                dataHSN.Rows.Add(new object[11] { SupplierName, GSTIn, InvDate, supppono, Convert.ToDecimal(Math.Round(taxableamt18, 3, MidpointRounding.AwayFromZero)), IntegratedTaxAmt18, CentralTaxAmt18, StateTaxAmt18, TotalCess18, 18, Convert.ToDecimal(Math.Round(taxableamt18, 3, MidpointRounding.AwayFromZero)) + IntegratedTaxAmt18 + CentralTaxAmt18 + StateTaxAmt18 + TotalCess18 });
                //            if (taxableamt28 != 0)
                //                dataHSN.Rows.Add(new object[11] { SupplierName, GSTIn, InvDate, supppono, Convert.ToDecimal(Math.Round(taxableamt28, 3, MidpointRounding.AwayFromZero)), IntegratedTaxAmt28, CentralTaxAmt28, StateTaxAmt28, TotalCess28, 28, Convert.ToDecimal(Math.Round(taxableamt28, 3, MidpointRounding.AwayFromZero)) + IntegratedTaxAmt28 + CentralTaxAmt28 + StateTaxAmt28 + TotalCess28 });
                //        }
                //    }
                //}

                da = new SqlDataAdapter("SPGSTR2", db.Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = Flag;
                da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fdate;
                da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = tdate;
                da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                da.Fill(ds, "tblGSTR2");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                GSTR2ReportNew salerpt = new GSTR2ReportNew();
                salerpt.SetDataSource(ds);
                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
                salerpt.SetParameterValue("Flag", Flag);
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
                getall(cm.ValidFromDate(dtpMonth.Value.Date), cm.ValidToDate(dtptomonth.Value.Date),Flag);
            }
            catch (Exception)
            { }
        }
    }
}
