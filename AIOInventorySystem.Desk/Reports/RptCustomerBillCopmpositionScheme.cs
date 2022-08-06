using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptCustomerBillCopmpositionScheme : Form
    {
        DbClass db = new DbClass();
        CurrencyToWord CurrencytoWord = new CurrencyToWord();
        CustomerBillUnderCompositionScheme custbillrptHori = new CustomerBillUnderCompositionScheme();
        CustomerBillCompositionA5 csutbillcompoA5 = new CustomerBillCompositionA5();
        public int custvalue, ConsigneeId;
        public string custid, custadd, mobileno,   custname, tinno, custstatecode, custstate, custPanno, Number, NetAmtInWord, consigneeName, consigneeAdd, 
            consigneeState, consigneeStateCode, consigneeMob, consigneeGStIN, bonusStatus, bonusShow;
        public decimal  discountamt, Cgst5amt, Cgst12amt, Cgst18amt, Cgst28amt, Sgst5amt, Sgst12amt, Sgst18amt, Sgst28amt, Igst5amt,
            Igst12amt, Igst18amt, Igst28amt, TotCgstAmt, TotIgstAmt, billRemaining = 0, custremamt = 0, bonusInRs = 0, bonusInPer = 0, prvBonus = 0,
            currbonus = 0, toatlBonus = 0, AlltotalAmount = 0, TotalNetAmt = 0;
        
        public RptCustomerBillCopmpositionScheme()
        {
            InitializeComponent();
        }

        public RptCustomerBillCopmpositionScheme(string billno, string copyType = "")
        {
            InitializeComponent();
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.TaxInvoicePrintOn == "A6")
                ShowBillA6(billno);
            else
                ShowBill(billno);
        }

        public RptCustomerBillCopmpositionScheme(List<int> bList, string billType, string copyType = "")
        {
            InitializeComponent();
        }

        public Int32 GetPaperSize(String sPrinterName, String sPaperSizeName)
        {
            PrintDocument docPrintDoc = new PrintDocument();
            docPrintDoc.PrinterSettings.PrinterName = sPrinterName;
            for (int i = 0; i < docPrintDoc.PrinterSettings.PaperSizes.Count; i++)
            {
                int raw = docPrintDoc.PrinterSettings.PaperSizes[i].RawKind;
                if (docPrintDoc.PrinterSettings.PaperSizes[i].PaperName == sPaperSizeName)
                    return raw;
            }
            return 0;
        }

        public void ShowBill(string CustrBillNo, string CopyType = "")
        {
            try
            {
                decimal MRP = 0, TotalMRP = 0, TotalSavings = 0, Totalrate = 0, MQty = 0, Mrate = 0;
                SqlDataAdapter da;
                DataTable dt = db.GetTable("Select * from CustomerBillMaster Where BillNo='" + CustrBillNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    custid = Convert.ToString(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    decimal e = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountamt = Convert.ToDecimal((a * e) / 100);
                    if (dt.Rows[0]["ShipCustomerId"].ToString() == "" || dt.Rows[0]["ShipCustomerId"].ToString() == null)
                        ConsigneeId = 0;
                    else
                        ConsigneeId = Convert.ToInt32(dt.Rows[0]["ShipCustomerId"].ToString());
                    billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                    TotalNetAmt = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());
                    Number = Math.Round(TotalNetAmt, 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                }
                
                DataTable dt1 = db.GetTable("Select * from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    custname = dt1.Rows[0]["CustomerName"].ToString();
                    custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                    mobileno = dt1.Rows[0]["Mobileno"].ToString();
                    tinno = dt1.Rows[0]["BuyerTinNo"].ToString();
                    custstate = dt1.Rows[0]["State"].ToString();
                    custstatecode = dt1.Rows[0]["Statecode"].ToString();
                    custPanno = dt1.Rows[0]["Panno"].ToString();

                    custremamt = 0;
                    RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                    List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    RemainingPayment remainingdata = new RemainingPayment();
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == CustrBillNo).FirstOrDefault();
                    if (remainingdata != null)
                    {
                        foreach (var item in remainingamt)
                        {
                            if (item.Id < remainingdata.Id)
                            {
                                var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.Type != "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Id == item.Id).Sum(t => t.NetAmt);
                                var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Id == item.Id).Sum(t => t.PaidAmt);
                                if (remainingamt != null)
                                    custremamt = custremamt + Convert.ToDecimal(netamt - Paidamt);
                            }
                        }
                    }
                    else
                    {
                        var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.Type != "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                        if (remainingamt != null)
                            custremamt = custremamt + Convert.ToDecimal(netamt - Paidamt);
                    }
                    remainingpayrepo.Dispose();
                    bonusStatus = dt1.Rows[0]["BonusIn"].ToString();
                    if (bonusStatus == "Bonus In Rs" || bonusStatus == "Bonus In Per")
                    {
                        bonusShow = "Show";
                        if (bonusStatus == "Bonus In Rs")
                            bonusInRs = Convert.ToDecimal(dt1.Rows[0]["BonusInRsNPer"].ToString());
                        else if (bonusStatus == "Bonus In Per")
                            bonusInPer = Convert.ToDecimal(dt1.Rows[0]["BonusInRsNPer"].ToString());
                        CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                        List<CustomerBillMaster> clist = CustBMrepo.GetAll().Where(t1 => t1.Id <= Convert.ToInt32(custid) && t1.CustomerId == Convert.ToInt32(custvalue) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                        if (clist.Count > 0)
                        {
                            foreach (var item in clist)
                            {
                                if (bonusStatus == "Bonus In Rs")
                                {
                                    AlltotalAmount = AlltotalAmount + Convert.ToDecimal(bonusInRs);
                                    currbonus = bonusInPer;
                                }
                                else if (bonusStatus == "Bonus In Per")
                                {
                                    decimal calbonusInper = 0;
                                    calbonusInper = Convert.ToDecimal(item.NetAmount) * bonusInPer / 100;
                                    AlltotalAmount = AlltotalAmount + calbonusInper;
                                    currbonus = (Convert.ToDecimal(TotalNetAmt) * bonusInPer / 100);
                                }
                            }
                        }
                        else
                        {
                            if (bonusStatus == "Bonus In Rs")
                                currbonus = bonusInRs;
                            else if (bonusStatus == "Bonus In Per")
                                currbonus = (Convert.ToDecimal(TotalNetAmt) * bonusInPer / 100);
                        }
                        //This is the old logic for Sai Kirana
                        //prvBonus = AlltotalAmount;
                        //toatlBonus = prvBonus + currbonus;


                        //This is the new logic for SAi Kirana
                        BonusRepository bonusRepo = new BonusRepository();
                        currbonus = Convert.ToDecimal(bonusRepo.GetAll().Where(t => t.BillId == Convert.ToInt32(custid)).FirstOrDefault().TotalBonusAmount);
                        //var lastBonusData = bonusRepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custvalue)).LastOrDefault();
                        decimal lastBonusAmt = Convert.ToDecimal(bonusRepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custvalue)).LastOrDefault().RemainingAmt);
                        //decimal sumOfPaidAmtBonusData =Convert.ToDecimal(bonusRepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(lastBonusData.CustomerId)).Sum(t=>t.PaidAmt));
                        //prvBonus = AlltotalAmount;
                        //decimal finalPreviousAmt = Convert.ToDecimal(lastBonusData.RemainingAmt) - sumOfPaidAmtBonusData;
                        //toatlBonus = prvBonus + currbonus;
                        decimal totalBonusAmt = Convert.ToDecimal(bonusRepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custvalue) && t.Status == "Bonus").Sum(t => t.TotalBonusAmount));
                        decimal totalPaidAmt = Convert.ToDecimal(bonusRepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custvalue)).Sum(t => t.PaidAmt));


                        if (currbonus < lastBonusAmt)
                        {
                            currbonus = totalBonusAmt - totalPaidAmt;
                            prvBonus = 0;
                        }
                        else
                        {
                            prvBonus = currbonus - lastBonusAmt;

                        }
                        toatlBonus = currbonus + prvBonus;
                    }
                    else
                        bonusShow = "Hide";
                }
                DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0),d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,d.SaleRate from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                if (detail.Rows.Count > 0)
                {
                    for (int i = 0; i < detail.Rows.Count; i++)
                    {
                        ProductRepository ProductRepo = new ProductRepository();
                        ProductInformation proddata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString()).FirstOrDefault();
                        else
                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (proddata != null)
                        {
                            if (proddata.AlternateUnit == null)
                            {
                                MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]);
                                MRP = Convert.ToDecimal(proddata.Rate);
                            }
                            else
                            {
                                if (detail.Rows[i]["SaleUnit"].ToString() == proddata.AlternateUnit && (proddata.UnitQty != null && proddata.UnitQty != 0))
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.UnitQty);
                                    MRP = Convert.ToDecimal(proddata.AlternateDealerRate);
                                }
                                else
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.AlternateUnitQty);
                                    MRP = Convert.ToDecimal(proddata.Rate);
                                }
                            }
                        }
                        ProductRepo.Dispose();
                        Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"]);
                        if (MRP != 0)
                        {
                            TotalMRP = TotalMRP + (MRP * MQty);
                            Totalrate = Totalrate + (Mrate * MQty);
                            TotalSavings = TotalMRP - Totalrate;
                        }
                    }
                }
                if (ConsigneeId != 0)
                {
                    DataTable ConsigneeDetail = db.GetTable("Select c.CustomerName,c.CustomeAddress,c.MobileNo,c.State,c.StateCode,c.BuyerTinNo from CustomerInformation c where c.CustomerID=" + ConsigneeId + " and compid=" + CommonMethod.CompId);
                    if (ConsigneeDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < ConsigneeDetail.Rows.Count; i++)
                        {
                            consigneeName = ConsigneeDetail.Rows[0]["CustomerName"].ToString();
                            consigneeAdd = ConsigneeDetail.Rows[0]["CustomeAddress"].ToString();
                            consigneeMob = ConsigneeDetail.Rows[0]["MobileNo"].ToString();
                            consigneeState = ConsigneeDetail.Rows[0]["State"].ToString();
                            consigneeStateCode = ConsigneeDetail.Rows[0]["StateCode"].ToString();
                            if (ConsigneeDetail.Rows[0]["BuyerTinNo"].ToString() != null)
                                consigneeGStIN = ConsigneeDetail.Rows[0]["BuyerTinNo"].ToString();
                            else
                                consigneeGStIN = "";
                        }
                    }
                }
                else
                {
                    consigneeName = "";
                    consigneeAdd = "";
                    consigneeMob = "";
                    consigneeState = "";
                    consigneeStateCode = "";
                    consigneeGStIN = "";
                }
                da = new SqlDataAdapter("Select * from CustomerBillMaster Where BillNo='" + CustrBillNo + "'  and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                CustomerbillDS ds = new CustomerbillDS();

                da.Fill(ds, "CustomerBillMaster");

                da = new SqlDataAdapter("Select d.* from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerBillDetail");

                da = new SqlDataAdapter("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BillInstruction");

                da = new SqlDataAdapter("Select * from Settings  where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                SettingRepository settRepo = new SettingRepository();
                Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.CompositionScheme == "True")
                {
                    if (sdata.TaxInvoicePrintOn == "A5")
                    {
                        csutbillcompoA5 = new CustomerBillCompositionA5();
                        goto A5;
                    }
                    else
                    {
                        custbillrptHori = new CustomerBillUnderCompositionScheme();
                        goto A4;
                    }
                }
            A4:
                {
                    custbillrptHori.SetDataSource(ds);
                    custbillrptHori.SetParameterValue("custname", custname);
                    custbillrptHori.SetParameterValue("customeraddress", custadd);
                    custbillrptHori.SetParameterValue("mobileno", mobileno);
                    custbillrptHori.SetParameterValue("discamt", discountamt);
                    custbillrptHori.SetParameterValue("buyertinno", tinno);
                    custbillrptHori.SetParameterValue("custstate", custstate);
                    custbillrptHori.SetParameterValue("custstatecode", custstatecode);
                    custbillrptHori.SetParameterValue("custPanno", custPanno);
                    custbillrptHori.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    custbillrptHori.SetParameterValue("ConsigneeName", consigneeName);
                    custbillrptHori.SetParameterValue("consigneeAdd", consigneeAdd);
                    custbillrptHori.SetParameterValue("consigneeMob", consigneeMob);
                    custbillrptHori.SetParameterValue("consigneeState", consigneeState);
                    custbillrptHori.SetParameterValue("consigneeStateCode", consigneeStateCode);
                    custbillrptHori.SetParameterValue("consigneeGStIN", consigneeGStIN);
                    custbillrptHori.SetParameterValue("PrintCopy", CopyType);
                    custbillrptHori.SetParameterValue("custremamt", custremamt);
                    custbillrptHori.SetParameterValue("billRemaining", billRemaining);
                    custbillrptHori.SetParameterValue("bonusShow", bonusShow);
                    custbillrptHori.SetParameterValue("PrvBonusAmt", prvBonus);
                    custbillrptHori.SetParameterValue("CurrBonusAmt", currbonus);
                    custbillrptHori.SetParameterValue("TotalBonusAmt", toatlBonus);
                    custbillrptHori.SetParameterValue("TotalSavings", TotalSavings);
                    crystalReportViewer1.ReportSource = custbillrptHori;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    crystalReportViewer1.Show();

                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    goto M;
                }
            A5:
                {
                    csutbillcompoA5.SetDataSource(ds);
                    csutbillcompoA5.SetParameterValue("custname", custname);
                    csutbillcompoA5.SetParameterValue("customeraddress", custadd);
                    csutbillcompoA5.SetParameterValue("mobileno", mobileno);
                    csutbillcompoA5.SetParameterValue("buyertinno", tinno);
                    csutbillcompoA5.SetParameterValue("custstate", custstate);
                    custbillrptHori.SetParameterValue("discamt", discountamt);
                    csutbillcompoA5.SetParameterValue("custstatecode", custstatecode);
                    csutbillcompoA5.SetParameterValue("MRPTotalAmt", TotalMRP);
                    csutbillcompoA5.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    csutbillcompoA5.SetParameterValue("custremamt", custremamt);
                    csutbillcompoA5.SetParameterValue("billRemaining", billRemaining);
                    csutbillcompoA5.SetParameterValue("bonusShow", bonusShow);
                    csutbillcompoA5.SetParameterValue("PrvBonusAmt", prvBonus);
                    csutbillcompoA5.SetParameterValue("CurrBonusAmt", currbonus);
                    csutbillcompoA5.SetParameterValue("TotalBonusAmt", toatlBonus);
                    csutbillcompoA5.SetParameterValue("Savings", TotalSavings);
                    crystalReportViewer1.ReportSource = csutbillcompoA5;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    crystalReportViewer1.Show();

                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    goto M;
                }
            M:
                { }
            }
            catch (Exception)
            { }
        }

        public void ShowBillA6(string BillNo)
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal gstpercent = 0, discountAmt = 0, discpercent = 0, discounttotal = 0, Mrate = 0, custremamt = 0, hamalicharges = 0, transportcharges = 0, 
                    MQty = 0, Mdiscount = 0,  MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0;
                string MProduct = "", MUnit = "", gstinno = "", MarathiUnit = "";
                DateTime billdate = DateTime.Today;
                DataTable dtRecord;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SettingRepository SettingRepo = new SettingRepository();
                var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                SqlDataAdapter da;
                DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges from CustomerBillMaster Where BillNo='" + BillNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    billId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    billdate = Convert.ToDateTime(dt.Rows[0]["Billdate"]);
                    discpercent = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discounttotal = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) * Convert.ToDecimal(dt.Rows[0]["Discount"].ToString()) / 100;
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    decimal e = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountAmt = Convert.ToDecimal((a * e) / 100);
                    if (dt.Rows[0]["TransportCharges"].ToString() != "" && dt.Rows[0]["TransportCharges"].ToString() != null)
                        transportcharges = Convert.ToDecimal(dt.Rows[0]["TransportCharges"].ToString());
                    else
                        transportcharges = 0;
                    if (dt.Rows[0]["HamaliCharges"].ToString() != "" && dt.Rows[0]["HamaliCharges"].ToString() != null)
                        hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());
                    else
                        hamalicharges = 0;
                }

                DataTable dt1 = db.GetTable("Select custNameMarathi,CustAddMarathi,Customername,CustomeAddress from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["custNameMarathi"].ToString() != "" && dt1.Rows[0]["custNameMarathi"].ToString() != null)
                        custname = dt1.Rows[0]["custNameMarathi"].ToString();
                    else
                        custname = dt1.Rows[0]["Customername"].ToString();
                    if (dt1.Rows[0]["CustAddMarathi"].ToString() != "" && dt1.Rows[0]["CustAddMarathi"].ToString() != null)
                        custadd = dt1.Rows[0]["CustAddMarathi"].ToString();
                    else
                        custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                    custremamt = 0;
                    RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                    List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    RemainingPayment remainingdata = new RemainingPayment();
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == BillNo).FirstOrDefault();
                    if (remainingdata != null)
                    {
                        foreach (var item in remainingamt)
                        {
                            if (item.Id < remainingdata.Id)
                            {
                                var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.Type != "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Id == item.Id).Sum(t => t.NetAmt);
                                var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Id == item.Id).Sum(t => t.PaidAmt);
                                if (remainingamt != null)
                                    custremamt = custremamt + Convert.ToDecimal(netamt - Paidamt);
                            }
                        }
                    }
                    else
                    {
                        var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.Type != "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                        if (remainingamt != null)
                            custremamt = custremamt + Convert.ToDecimal(netamt - Paidamt);
                    }
                    remainingpayrepo.Dispose();
                }

                dtRecord = dsbill.Tables["BillMaster"];
                dtRecord.Rows.Add(new object[8] { BillNo, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                //                                             0        1           2        3       4                          5         6           7            8          9          10           11       12           13                              14                        15
                DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0) as Vat,d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,isnull(d.SaleRate,0) as SaleRate,isnull(d.Rate,0) as Rate from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                if (detail.Rows.Count > 0)
                {
                    for (int i = 0; i < detail.Rows.Count; i++)
                    {
                        ProductRepository ProductRepo = new ProductRepository();
                        ProductInformation proddata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString()).FirstOrDefault();
                        else
                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (proddata != null)
                        {
                            UnitRepository unitrepo = new UnitRepository();
                            Unit unitdata = new Unit();
                            if (CommonMethod.commProduct == true)
                                unitdata = unitrepo.GetAll().Where(t => t.UnitName == proddata.Uniti).FirstOrDefault();
                            else
                                unitdata = unitrepo.GetAll().Where(t => t.UnitName == proddata.Uniti && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            
                            MProduct = proddata.ProductName;
                            MUnit = proddata.Uniti;
                            if (proddata.MarathiName != "")
                                MProduct = proddata.MarathiName;
                            else
                                MProduct = proddata.ProductName;
                            if (unitdata != null)
                            {
                                MUnit = unitdata.UnitMarathi;
                                MarathiUnit = MUnit;
                            }
                            unitrepo.Dispose();
                            if (proddata.AlternateUnit == null)
                            {
                                MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]);
                                if (MarathiUnit == "")
                                    MUnit = detail.Rows[i]["Unit"].ToString();
                                MRP = Convert.ToDecimal(proddata.Rate);
                            }
                            else
                            {
                                if (detail.Rows[i]["SaleUnit"].ToString() == proddata.AlternateUnit && (proddata.UnitQty != null && proddata.UnitQty != 0))
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.UnitQty);
                                    if (MarathiUnit == "")
                                        MUnit = detail.Rows[i]["SaleUnit"].ToString();
                                    MRP = Convert.ToDecimal(proddata.AlternateDealerRate);
                                }
                                else
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.AlternateUnitQty);
                                    if (MarathiUnit == "")
                                        MUnit = detail.Rows[i]["Unit"].ToString();
                                    MRP = Convert.ToDecimal(proddata.Rate);
                                }
                            }
                        }
                        ProductRepo.Dispose();

                        if (Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString()) != Convert.ToDecimal(detail.Rows[i]["Rate"].ToString()))
                        {
                            if (Convert.ToDecimal(detail.Rows[i]["sgstpercent"].ToString()) != 0)
                                Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString()) * 100 / (100 + Convert.ToDecimal(detail.Rows[i]["sgstpercent"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstpercent"].ToString()));
                            else if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) != 0)
                                Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString()) * 100 / (100 + Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()));
                        }
                        else
                            Mrate = Convert.ToDecimal(detail.Rows[i]["Rate"]);
                        if (MRP != 0)
                        {
                            TotalMRP = TotalMRP + (MRP * MQty);
                            Totalrate = Totalrate + (Mrate * MQty);
                            TotalSavings = TotalMRP - Totalrate;
                        }
                        if (!string.IsNullOrEmpty(detail.Rows[i]["Amount"].ToString()))
                            Mdiscount = Convert.ToDecimal(detail.Rows[i]["Amount"]);
                        gstpercent = Convert.ToDecimal(detail.Rows[i]["sgstpercent"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstpercent"].ToString());
                        if (gstpercent == 5)
                        {
                            Cgst5amt = Cgst5amt + Convert.ToDecimal(detail.Rows[i]["sgstamt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstamt"].ToString());
                            gstpercent = 0;
                        }
                        if (gstpercent == 12)
                        {
                            Cgst12amt = Cgst12amt + Convert.ToDecimal(detail.Rows[i]["sgstamt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstamt"].ToString());
                            gstpercent = 0;
                        }
                        if (gstpercent == 18)
                        {
                            Cgst18amt = Cgst18amt + Convert.ToDecimal(detail.Rows[i]["sgstamt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstamt"].ToString());
                            gstpercent = 0;
                        }
                        if (gstpercent == 28)
                        {
                            Cgst28amt = Cgst28amt + Convert.ToDecimal(detail.Rows[i]["sgstamt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstamt"].ToString());
                            gstpercent = 0;
                        }
                        //-- IGst
                        if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) == 5)
                            Igst5amt = Igst5amt + Convert.ToDecimal(detail.Rows[i]["igstamt"].ToString());
                        if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) == 12)
                            Igst12amt = Igst12amt + Convert.ToDecimal(detail.Rows[i]["igstamt"].ToString());
                        if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) == 18)
                            Igst18amt = Igst18amt + Convert.ToDecimal(detail.Rows[i]["igstamt"].ToString());
                        if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) == 28)
                            Igst28amt = Igst28amt + Convert.ToDecimal(detail.Rows[i]["igstamt"].ToString());
                        TotCgstAmt = Cgst5amt + Cgst12amt + Cgst18amt + Cgst28amt;
                        TotIgstAmt = Igst5amt + Igst12amt + Igst18amt + Igst28amt;
                        int count = i + 1;
                        dtRecord = dsbill.Tables["BillDetail"];
                        dtRecord.Rows.Add(new object[8] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP });
                    }
                }

                DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress,VatNo from CompanyInformation where CompanyId=" + CommonMethod.CompId);
                if (dtComp.Rows.Count > 0)
                {
                    string compname = "";
                    string compadd = "";
                    if (dtComp.Rows[0]["CompNameMarathi"].ToString() != "" && dtComp.Rows[0]["CompNameMarathi"].ToString() != null)
                        compname = dtComp.Rows[0]["CompNameMarathi"].ToString();
                    else
                        compname = dtComp.Rows[0]["CompanyName"].ToString();
                    if (dtComp.Rows[0]["CompAddMarathi"].ToString() != "" && dtComp.Rows[0]["CompAddMarathi"].ToString() != null)
                        compadd = dtComp.Rows[0]["CompAddMarathi"].ToString();
                    else
                        compadd = dtComp.Rows[0]["CompanyAddress"].ToString();
                    gstinno = Convert.ToString(dtComp.Rows[0]["VatNo"].ToString());
                    dtRecord = dsbill.Tables["CompanyInfo"];
                    dtRecord.Rows.Add(new object[2] { compname, compadd });
                }

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "ProductInformation");
                string instText1 = "", instText2 = "", instText3 = "", startingText = "";
                DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                if (dtinstruct.Rows.Count > 0)
                {
                    instText1 = dtinstruct.Rows[0][4].ToString();
                    instText2 = dtinstruct.Rows[0][5].ToString();
                    instText3 = dtinstruct.Rows[0][6].ToString();
                    startingText = dtinstruct.Rows[0][1].ToString();
                }
                CustomerBillA61 custA6rrpt = new CustomerBillA61();
                custA6rrpt.SetDataSource(dsbill);
                custA6rrpt.SetParameterValue("DiscAmt", TotalSavings);
                custA6rrpt.SetParameterValue("Flag", "Bill Of Supply");
                custA6rrpt.SetParameterValue("discounttotal", discounttotal);
                custA6rrpt.SetParameterValue("instruction1", instText1);
                custA6rrpt.SetParameterValue("instruction2", instText2);
                custA6rrpt.SetParameterValue("Instruction3", instText3);
                custA6rrpt.SetParameterValue("StartingText", startingText);
                custA6rrpt.SetParameterValue("TotCgstAmt", TotCgstAmt);
                custA6rrpt.SetParameterValue("TotIgstAmt", TotIgstAmt);
                custA6rrpt.SetParameterValue("discpercent", discpercent);
                custA6rrpt.SetParameterValue("gstin", gstinno);
                crystalReportViewer1.ReportSource = custA6rrpt;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();
            }
            catch (Exception)
            { }
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