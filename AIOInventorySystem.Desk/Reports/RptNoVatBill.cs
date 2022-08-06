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
    public partial class RptNoVatBill : Form
    {
        DbClass db = new DbClass();
        NoVatCustomerBillRpt custbillrpt = new NoVatCustomerBillRpt();
        CurrencyToWord CurrencytoWord = new CurrencyToWord();
        CustomerBillA61 custA6rrpt = new CustomerBillA61();
        public string Custbillno, BType;

        public RptNoVatBill()
        {
            InitializeComponent();
        }

        public RptNoVatBill(string billno)
        {
            Custbillno = billno;
            InitializeComponent();
            SettingRepository settingRepo = new SettingRepository();
            Setting sdata = settingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.EstimateInvoicePrintOn == "EA6")
                ShowBillA6(Custbillno);
            else if (sdata.EstimateInvoicePrintOn == "EMarathiA6")
                ShowMarathiA6Estimate(Custbillno);
            else
                ShowBill(Custbillno);
        }

        public RptNoVatBill(List<int> bList, string billType)
        {
            BType = billType;
            InitializeComponent();
            SettingRepository settingRepo = new SettingRepository();
            Setting sdata = settingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.EstimateInvoicePrintOn == "EA6")
                ShowMBillA6(bList);
            else if (sdata.EstimateInvoicePrintOn == "EMarathiA6")
                ShowMarathiA6Estimate(bList);
            else
                ShowMBill(bList);
        }

        public void ShowMBill(List<int> billList)
        {
            for (int i = 0; i < billList.Count; i++)
            {
                try
                {
                    int custvalue = 0;
                    string custid = "", custadd = "", mobileno = "", landline = "", day = "", custname = "", discamount = "", tinno = "", termscondition = "",
                        declaration = "", Number, NetAmtInWord = "", bonusStatus = "", bonusShow = "";
                    decimal bonusInRs = 0, bonusInPer = 0, prvBonus = 0, currbonus = 0, toatlBonus = 0, AlltotalAmount = 0, TotalNetAmt = 0, billRemaining = 0,
                        custremamt = 0;
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select * from CustomerBillMaster Where Id='" + billList[i] + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DSNoVatCustomerBill ds = new DSNoVatCustomerBill();
                    da.Fill(ds, "CustomerBillMaster");

                    DataTable dt = new DataTable();
                    da.Fill(dt);                        
                    if (dt.Rows.Count > 0)
                    {
                        custid = Convert.ToString(dt.Rows[0]["Id"].ToString());
                        custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                        decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                        decimal b = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                        discamount = Convert.ToString((a * b) / 100);
                        billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                        TotalNetAmt = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());
                        Custbillno = Convert.ToString(dt.Rows[0]["BillNo"].ToString());
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
                        landline = dt1.Rows[0]["Landlineno"].ToString();
                        day = dt1.Rows[0]["CreditDays"].ToString();
                        tinno = dt1.Rows[0]["BuyerTinNo"].ToString();
                        custremamt = 0;
                        RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                        List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        RemainingPayment remainingdata = new RemainingPayment();
                        remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == Custbillno).FirstOrDefault();
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
                            List<CustomerBillMaster> clist = CustBMrepo.GetAll().Where(t1 => t1.Id < Convert.ToInt32(custid) && t1.CustomerId == Convert.ToInt32(custvalue) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
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
                            prvBonus = AlltotalAmount;
                            toatlBonus = prvBonus + currbonus;
                        }
                        else
                            bonusShow = "Hide";
                    }
                    
                    da = new SqlDataAdapter("Select d.* from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerBillDetail");

                    da = new SqlDataAdapter("Select * from Settings  where CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "Settings");

                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "ProductInformation");

                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    DataTable dtInstructions = db.GetTable("Select * from BillInstructions where CompId='" + CommonMethod.CompId + "'");
                    if (dtInstructions.Rows.Count > 0)
                    {
                        termscondition = Convert.ToString(dtInstructions.Rows[0]["TermsConditions"].ToString());
                        declaration = Convert.ToString(dtInstructions.Rows[0]["Declaration"].ToString());
                    }

                    custbillrpt = new NoVatCustomerBillRpt();
                    custbillrpt.SetDataSource(ds);
                    custbillrpt.SetParameterValue("custname", custname);
                    custbillrpt.SetParameterValue("customeraddress", custadd);
                    custbillrpt.SetParameterValue("mobileno", mobileno);
                    custbillrpt.SetParameterValue("landlineno", landline);
                    custbillrpt.SetParameterValue("buyertinno", tinno);
                    custbillrpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    custbillrpt.SetParameterValue("bonusShow", bonusShow);
                    custbillrpt.SetParameterValue("PrvBonusAmt", prvBonus);
                    custbillrpt.SetParameterValue("CurrBonusAmt", currbonus);
                    custbillrpt.SetParameterValue("TotalBonusAmt", toatlBonus);
                    custbillrpt.SetParameterValue("Declaration", declaration);
                    custbillrpt.SetParameterValue("TermsAndCondition", termscondition);
                    crystalReportViewer1.ReportSource = custbillrpt;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    crystalReportViewer1.Visible = false;

                    SettingRepository SettingRepo = new SettingRepository();
                    AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                    SettingData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SettingData != null)
                    {
                        if (!string.IsNullOrEmpty(SettingData.PrinterName))
                        {
                            if (BType == "WithoutVat")
                                custbillrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                            custbillrpt.PrintToPrinter(1, true, 1, 5);
                        }
                    }
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    SettingRepo.Dispose();
                }
                catch (Exception)
                { }
            }
        }

        public void ShowMBillA6(List<int> billList)
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal billRemaining = 0, discountAmt = 0, netAmt = 0, Mrate = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0,
                    billcurrentAmt = 0, MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0, discounttotal = 0, TotCgstAmt = 0, TotIgstAmt = 0, discpercent = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", custrbillno = "", gstinno = "", instText1 = "", instText2 = "",
                    startingText = "", instText3 = "";
                DateTime billdate = DateTime.Today;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SqlDataAdapter da;
                DataTable dtRecord;
                for (int m = 0; m < billList.Count; m++)
                {
                    DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges,BillNo from CustomerBillMaster Where Id='" + billList[m] + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        billId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                        custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                        billdate = Convert.ToDateTime(dt.Rows[0]["Billdate"]);
                        custrbillno = Convert.ToString(dt.Rows[0]["BillNo"].ToString());
                        decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                        discpercent = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                        discounttotal = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) * Convert.ToDecimal(dt.Rows[0]["Discount"].ToString()) / 100;
                        discountAmt = Convert.ToDecimal((a * discpercent) / 100);
                        netAmt = Convert.ToDecimal(a - discountAmt);
                        if (dt.Rows[0]["TransportCharges"].ToString() != "" && dt.Rows[0]["TransportCharges"].ToString() != null)
                            transportcharges = Convert.ToDecimal(dt.Rows[0]["TransportCharges"].ToString());
                        else
                            transportcharges = 0;
                        if (dt.Rows[0]["HamaliCharges"].ToString() != "" && dt.Rows[0]["HamaliCharges"].ToString() != null)
                            hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());
                        else
                            hamalicharges = 0;

                        billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                        billcurrentAmt = netAmt;
                    }
                    DataTable dt1 = db.GetTable("Select custNameMarathi,CustAddMarathi,Customername,CustomeAddress from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        custname = dt1.Rows[0]["CustomerName"].ToString();
                        custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                    }
                    dtRecord = dsbill.Tables["BillMaster"];
                    dtRecord.Rows.Add(new object[8] { custrbillno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });

                    DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0) as Vat,d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,d.SaleRate from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId");
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
                                if (proddata.AlternateUnit == null)
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]);
                                    MUnit = detail.Rows[i]["Unit"].ToString();
                                    MRP = Convert.ToDecimal(proddata.Rate);
                                }
                                else
                                {
                                    if (detail.Rows[i]["SaleUnit"].ToString() == proddata.AlternateUnit && (proddata.UnitQty != null && proddata.UnitQty != 0))
                                    {
                                        MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.UnitQty);
                                        MUnit = detail.Rows[i]["SaleUnit"].ToString();
                                        MRP = Convert.ToDecimal(proddata.AlternateDealerRate);
                                    }
                                    else
                                    {
                                        MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.AlternateUnitQty);
                                        MUnit = detail.Rows[i]["Unit"].ToString();
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
                            }
                            TotalSavings = TotalMRP - Totalrate;
                            if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                                Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
                            int count = i + 1;
                            dtRecord = dsbill.Tables["BillDetail"];
                            dtRecord.Rows.Add(new object[8] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP });
                        }
                    }
                    DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress,VatNo from CompanyInformation where CompanyId=" + CommonMethod.CompId);
                    if (dtComp.Rows.Count > 0)
                    {
                        string compname = "", compadd = "";
                        compname = dtComp.Rows[0]["CompanyName"].ToString();
                        compadd = dtComp.Rows[0]["CompanyAddress"].ToString();
                        gstinno = Convert.ToString(dtComp.Rows[0][4].ToString());
                        dtRecord = dsbill.Tables["CompanyInfo"];
                        dtRecord.Rows.Add(new object[2] { compname, compadd });
                    }

                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dsbill, "ProductInformation");

                    da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dsbill, "Settings");

                    DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                    if (dtinstruct.Rows.Count > 0)
                    {
                        instText1 = dtinstruct.Rows[0]["InstructionText1"].ToString();
                        instText2 = dtinstruct.Rows[0]["InstructionText2"].ToString();
                        instText3 = dtinstruct.Rows[0]["InstructionText3"].ToString();
                        startingText = dtinstruct.Rows[0]["StartigText1"].ToString();
                    }
                    custA6rrpt = new CustomerBillA61();
                    custA6rrpt.SetDataSource(dsbill);
                    custA6rrpt.SetParameterValue("DiscAmt", discountAmt);
                    custA6rrpt.SetParameterValue("Flag", "Estimate Bill");
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
                    crystalReportViewer1.Visible = false;

                    SettingRepository SettingRepo = new SettingRepository();
                    AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                    SettingData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SettingData != null)
                    {
                        if (!string.IsNullOrEmpty(SettingData.PrinterName))
                        {
                            if (BType == "WithoutVat")
                                custA6rrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                            custA6rrpt.PrintToPrinter(1, true, 1, 5);
                        }
                    }
                    da.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                }
            }
            catch (Exception)
            { }
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

        public void ShowBill(string custrbillno)
        {
            try
            {
                int custvalue = 0;
                decimal bonusInRs = 0, bonusInPer = 0, prvBonus = 0, currbonus = 0, toatlBonus = 0, AlltotalAmount = 0, TotalNetAmt = 0, billRemaining = 0,
                    custremamt = 0;
                string custid = "", custadd = "", mobileno = "", landline = "", day = "", custname = "", discamount = "", tinno = "", termscondition = "",
                    declaration = "", Number, NetAmtInWord = "", bonusStatus, bonusShow = "";
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from CustomerBillMaster Where BillNo='" + custrbillno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DSNoVatCustomerBill ds = new DSNoVatCustomerBill();
                da.Fill(ds, "CustomerBillMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);                
                if (dt.Rows.Count > 0)
                {
                    custid = Convert.ToString(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    decimal b = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discamount = Convert.ToString((a * b) / 100);
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
                    landline = dt1.Rows[0]["Landlineno"].ToString();
                    day = dt1.Rows[0]["CreditDays"].ToString();
                    tinno = dt1.Rows[0]["BuyerTinNo"].ToString();

                    custremamt = 0;
                    RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                    List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    RemainingPayment remainingdata = new RemainingPayment();
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == custrbillno).FirstOrDefault();
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
                    bonusStatus = dt1.Rows[0][25].ToString();
                    if (bonusStatus == "Bonus In Rs" || bonusStatus == "Bonus In Per")
                    {
                        bonusShow = "Show";
                        if (bonusStatus == "Bonus In Rs")
                            bonusInRs = Convert.ToDecimal(dt1.Rows[0][26].ToString());
                        else if (bonusStatus == "Bonus In Per")
                            bonusInPer = Convert.ToDecimal(dt1.Rows[0][26].ToString());
                        CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                        List<CustomerBillMaster> clist = CustBMrepo.GetAll().Where(t1 => t1.Id < Convert.ToInt32(custid) && t1.CustomerId == Convert.ToInt32(custvalue) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
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
                        prvBonus = AlltotalAmount;
                        toatlBonus = prvBonus + currbonus;
                    }
                    else
                        bonusShow = "Hide";
                }
                
                da = new SqlDataAdapter("Select d.* from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerBillDetail");

                da = new SqlDataAdapter("Select * from Settings  where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dtInstructions = db.GetTable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                if (dtInstructions.Rows.Count > 0)
                {
                    termscondition = Convert.ToString(dtInstructions.Rows[0][10].ToString());
                    declaration = Convert.ToString(dtInstructions.Rows[0][9].ToString());
                }

                custbillrpt = new NoVatCustomerBillRpt();
                custbillrpt.SetDataSource(ds);
                custbillrpt.SetParameterValue("custname", custname);
                custbillrpt.SetParameterValue("customeraddress", custadd);
                custbillrpt.SetParameterValue("mobileno", mobileno);
                custbillrpt.SetParameterValue("landlineno", landline);
                custbillrpt.SetParameterValue("buyertinno", tinno);
                custbillrpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                custbillrpt.SetParameterValue("bonusShow", bonusShow);
                custbillrpt.SetParameterValue("PrvBonusAmt", prvBonus);
                custbillrpt.SetParameterValue("CurrBonusAmt", currbonus);
                custbillrpt.SetParameterValue("TotalBonusAmt", toatlBonus);
                custbillrpt.SetParameterValue("Declaration", declaration);
                custbillrpt.SetParameterValue("TermsAndCondition", termscondition);
                custbillrpt.SetParameterValue("custremamt", custremamt);
                custbillrpt.SetParameterValue("billRemaining", billRemaining);
                crystalReportViewer1.ReportSource = custbillrpt;

                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();

                da.Dispose();
                ds.Dispose();
                dt.Dispose();
                dt1.Dispose();
            }
            catch (Exception)
            { }
        }

        public void ShowBillA6(string custrbillno)
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal billRemaining = 0, discountAmt = 0, netAmt = 0, Mrate = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0,
                    billcurrentAmt = 0, MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0, discounttotal = 0, TotCgstAmt = 0, TotIgstAmt = 0, discpercent = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", gstinno = "", instText1 = "", instText2 = "", startingText = "", instText3 = "";
                DateTime billdate = DateTime.Today;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SqlDataAdapter da;
                DataTable dtRecord;
                DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges from CustomerBillMaster Where BillNo='" + custrbillno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    billId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    billdate = Convert.ToDateTime(dt.Rows[0]["Billdate"]);
                    discpercent = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discounttotal = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) * Convert.ToDecimal(dt.Rows[0]["Discount"].ToString()) / 100;
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    discountAmt = Convert.ToDecimal((a * discpercent) / 100);
                    netAmt = Convert.ToDecimal(a - discountAmt);
                    if (dt.Rows[0][6].ToString() != "" && dt.Rows[0][6].ToString() != null)
                        transportcharges = Convert.ToDecimal(dt.Rows[0][6].ToString());
                    else
                        transportcharges = 0;
                    if (dt.Rows[0]["HamaliCharges"].ToString() != "" && dt.Rows[0]["HamaliCharges"].ToString() != null)
                        hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());
                    else
                        hamalicharges = 0;

                    billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                    billcurrentAmt = netAmt;
                }
                DataTable dt1 = db.GetTable("Select custNameMarathi,CustAddMarathi,Customername,CustomeAddress from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    custname = dt1.Rows[0]["CustomerName"].ToString();
                    custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                }

                dtRecord = dsbill.Tables["BillMaster"];
                dtRecord.Rows.Add(new object[8] { custrbillno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0),d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,d.SaleRate from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId");
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
                            if (proddata.AlternateUnit == null)
                            {
                                MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]);
                                MUnit = detail.Rows[i]["Unit"].ToString();
                                MRP = Convert.ToDecimal(proddata.Rate);
                            }
                            else
                            {
                                if (detail.Rows[i]["SaleUnit"].ToString() == proddata.AlternateUnit && (proddata.UnitQty != null && proddata.UnitQty != 0))
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.UnitQty);
                                    MUnit = detail.Rows[i]["SaleUnit"].ToString();
                                    MRP = Convert.ToDecimal(proddata.AlternateDealerRate);
                                }
                                else
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]) / Convert.ToDecimal(proddata.AlternateUnitQty);
                                    MUnit = detail.Rows[i]["Unit"].ToString();
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
                        }
                        TotalSavings = TotalMRP - Totalrate;
                        if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                            Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
                        int count = i + 1;
                        dtRecord = dsbill.Tables["BillDetail"];
                        dtRecord.Rows.Add(new object[8] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP });
                    }
                }
                DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress,VatNo from CompanyInformation where CompanyId=" + CommonMethod.CompId);
                if (dtComp.Rows.Count > 0)
                {
                    string compname = "", compadd = "";
                    compname = dtComp.Rows[0]["CompanyName"].ToString();
                    compadd = dtComp.Rows[0]["CompanyAddress"].ToString();
                    gstinno = Convert.ToString(dtComp.Rows[0]["VatNo"].ToString());
                    dtRecord = dsbill.Tables["CompanyInfo"];
                    dtRecord.Rows.Add(new object[2] { compname, compadd });
                }

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "Settings");

                DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                if (dtinstruct.Rows.Count > 0)
                {
                    instText1 = dtinstruct.Rows[0]["InstructionText1"].ToString();
                    instText2 = dtinstruct.Rows[0]["InstructionText2"].ToString();
                    instText3 = dtinstruct.Rows[0]["InstructionText3"].ToString();
                    startingText = dtinstruct.Rows[0]["StartigText1"].ToString();
                }

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "ProductInformation");

                custA6rrpt = new CustomerBillA61();
                custA6rrpt.SetDataSource(dsbill);
                custA6rrpt.SetParameterValue("DiscAmt", TotalSavings);
                custA6rrpt.SetParameterValue("Flag", "Estimate Bill");
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
                da.Dispose();
                dt.Dispose();
                dt1.Dispose();
            }
            catch (Exception) { }
        }

        public void ShowMarathiA6Estimate(string custrbillno)
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal custremamt = 0, billRemaining = 0, discountAmt = 0, netAmt = 0, Mrate = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0,
                    billcurrentAmt = 0, MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", billType = "", MarathiUnit = "";
                DateTime billdate = DateTime.Today;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SqlDataAdapter da;
                DataTable dtRecord;
                DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges from CustomerBillMaster Where BillNo='" + custrbillno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    billId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    billdate = Convert.ToDateTime(dt.Rows[0]["Billdate"]);

                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    decimal e = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountAmt = Convert.ToDecimal((a * e) / 100);
                    netAmt = Convert.ToDecimal(a - discountAmt);
                    if (dt.Rows[0]["TransportCharges"].ToString() != "" && dt.Rows[0]["TransportCharges"].ToString() != null)
                        transportcharges = Convert.ToDecimal(dt.Rows[0]["TransportCharges"].ToString());
                    else
                        transportcharges = 0;
                    if (dt.Rows[0]["HamaliCharges"].ToString() != "" && dt.Rows[0]["HamaliCharges"].ToString() != null)
                        hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());
                    else
                        hamalicharges = 0;

                    billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                    if (Convert.ToDecimal(dt.Rows[0]["RemainingAmount"]) == 0)
                        billType = "रोख बिल";
                    else
                        billType = "उधार बिल";
                    billcurrentAmt = netAmt;
                }
                DataTable dt1 = db.GetTable("Select custNameMarathi,CustAddMarathi,Customername,CustomeAddress from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows[0]["custNameMarathi"].ToString() != "" && dt1.Rows[0]["custNameMarathi"].ToString() != null)
                        custname = dt1.Rows[0]["custNameMarathi"].ToString();
                    else
                        custname = dt1.Rows[0]["CustomerName"].ToString();

                    if (dt1.Rows[0]["CustAddMarathi"].ToString() != "" && dt1.Rows[0]["CustAddMarathi"].ToString() != null)
                        custadd = dt1.Rows[0]["CustAddMarathi"].ToString();
                    else
                        custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                    custremamt = 0;
                    RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                    List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    RemainingPayment remainingdata = new RemainingPayment();
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == custrbillno).FirstOrDefault();
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
                dtRecord.Rows.Add(new object[8] { custrbillno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0),d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,d.SaleRate from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId");
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
                        Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"]);
                        if (MRP != 0)
                        {
                            TotalMRP = TotalMRP + (MRP * MQty);
                            Totalrate = Totalrate + (Mrate * MQty);
                        }
                        TotalSavings = TotalMRP - Totalrate;

                        if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                            Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
                        int count = i + 1;
                        dtRecord = dsbill.Tables["BillDetail"];
                        dtRecord.Rows.Add(new object[8] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP });
                    }
                }
                DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress from CompanyInformation where CompanyId=" + CommonMethod.CompId);
                if (dtComp.Rows.Count > 0)
                {
                    string compname = "", compadd = "";
                    if (dtComp.Rows[0]["CompNameMarathi"].ToString() != "" && dtComp.Rows[0]["CompNameMarathi"].ToString() != null)
                        compname = dtComp.Rows[0]["CompNameMarathi"].ToString();
                    else
                        compname = dtComp.Rows[0]["CompanyName"].ToString();

                    if (dtComp.Rows[0]["CompAddMarathi"].ToString() != "" && dtComp.Rows[0]["CompAddMarathi"].ToString() != null)
                        compadd = dtComp.Rows[0]["CompAddMarathi"].ToString();
                    else
                        compadd = dtComp.Rows[0]["CompanyAddress"].ToString();
                    dtRecord = dsbill.Tables["CompanyInfo"];
                    dtRecord.Rows.Add(new object[2] { compname, compadd });
                }
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "ProductInformation");

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "Settings");

                string instText1 = "", instText2 = "";
                DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId=" + CommonMethod.CompId + "");
                if (dtinstruct.Rows.Count > 0)
                {
                    instText1 = dtinstruct.Rows[0]["StartigText1"].ToString();
                    instText2 = dtinstruct.Rows[0]["StartigText2"].ToString();
                }

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "Settings");

                CustomerbillMarathi_A6 custbillmarathiA6 = new CustomerbillMarathi_A6();
                custbillmarathiA6.SetDataSource(dsbill);

                custbillmarathiA6.SetParameterValue("instruction1", instText1);
                custbillmarathiA6.SetParameterValue("instruction2", instText2);
                custbillmarathiA6.SetParameterValue("custremamt", custremamt);
                custbillmarathiA6.SetParameterValue("BillType", billType);
                custbillmarathiA6.SetParameterValue("billRemaining", billRemaining);
                custbillmarathiA6.SetParameterValue("TotalSavings", TotalSavings);
                crystalReportViewer1.ReportSource = custbillmarathiA6;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();
            }
            catch (Exception)
            { }
        }

        public void ShowMarathiA6Estimate(List<int> billList)
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal billRemaining = 0, custremamt = 0, discountAmt = 0, netAmt = 0, Mrate = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0,
                    billcurrentAmt = 0, MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", billType = "", MarathiUnit = "", custrbillno = "", instText1 = "", instText2 = "";
                DateTime billdate = DateTime.Today;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SqlDataAdapter da;
                DataTable dtRecord;
                for (int m = 0; m < billList.Count; m++)
                {
                    DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges,BillNo from CustomerBillMaster Where Id='" + billList[m] + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        billId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                        custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                        billdate = Convert.ToDateTime(dt.Rows[0]["Billdate"]);
                        custrbillno = Convert.ToString(dt.Rows[0]["BillNo"]);
                        decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                        decimal e = 0;
                        if (dt.Rows[0]["Discount"].ToString() != "")
                            e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                        discountAmt = Convert.ToDecimal((a * e) / 100);
                        netAmt = Convert.ToDecimal(a - discountAmt);
                        if (dt.Rows[0]["TransportCharges"].ToString() != "" && dt.Rows[0]["TransportCharges"].ToString() != null)
                            transportcharges = Convert.ToDecimal(dt.Rows[0]["TransportCharges"].ToString());
                        else
                            transportcharges = 0;
                        if (dt.Rows[0]["HamaliCharges"].ToString() != "" && dt.Rows[0]["HamaliCharges"].ToString() != null)
                            hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());
                        else
                            hamalicharges = 0;
                        billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                        if (Convert.ToDecimal(dt.Rows[0]["RemainingAmount"]) == 0)
                            billType = "रोख बिल";
                        else
                            billType = "उधार बिल";
                        billcurrentAmt = netAmt;
                    }
                    DataTable dt1 = db.GetTable("Select custNameMarathi,CustAddMarathi,CustomerName,CustomeAddress from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                    if (dt1.Rows.Count > 0)
                    {
                        if (dt1.Rows[0]["custNameMarathi"].ToString() != "" && dt1.Rows[0]["custNameMarathi"].ToString() != null)
                            custname = dt1.Rows[0]["custNameMarathi"].ToString();
                        else
                            custname = dt1.Rows[0]["CustomerName"].ToString();
                        if (dt1.Rows[0]["CustAddMarathi"].ToString() != "" && dt1.Rows[0]["CustAddMarathi"].ToString() != null)
                            custadd = dt1.Rows[0]["CustAddMarathi"].ToString();
                        else
                            custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                        custremamt = 0;
                        RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                        List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        RemainingPayment remainingdata = new RemainingPayment();
                        remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == custrbillno).FirstOrDefault();
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
                    dtRecord.Rows.Add(new object[8] { custrbillno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                    DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0),d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,d.SaleRate from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId");
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
                            Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"]);
                            if (MRP != 0)
                            {
                                TotalMRP = TotalMRP + (MRP * MQty);
                                Totalrate = Totalrate + (Mrate * MQty);
                            }
                            TotalSavings = TotalMRP - Totalrate;
                            if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                                Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
                            int count = i + 1;
                            dtRecord = dsbill.Tables["BillDetail"];
                            dtRecord.Rows.Add(new object[8] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP });
                        }
                    }
                    DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress from CompanyInformation where CompanyId=" + CommonMethod.CompId);
                    if (dtComp.Rows.Count > 0)
                    {
                        string compname = "", compadd = "";
                        if (dtComp.Rows[0]["CompNameMarathi"].ToString() != "" && dtComp.Rows[0]["CompNameMarathi"].ToString() != null)
                            compname = dtComp.Rows[0]["CompNameMarathi"].ToString();
                        else
                            compname = dtComp.Rows[0]["CompanyName"].ToString();
                        if (dtComp.Rows[0]["CompAddMarathi"].ToString() != "" && dtComp.Rows[0]["CompAddMarathi"].ToString() != null)
                            compadd = dtComp.Rows[0]["CompAddMarathi"].ToString();
                        else
                            compadd = dtComp.Rows[0]["CompanyAddress"].ToString();
                        dtRecord = dsbill.Tables["CompanyInfo"];
                        dtRecord.Rows.Add(new object[2] { compname, compadd });
                    }
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dsbill, "ProductInformation");

                    da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dsbill, "Settings");

                    DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId=" + CommonMethod.CompId + "");
                    if (dtinstruct.Rows.Count > 0)
                    {
                        instText1 = dtinstruct.Rows[0]["StartigText1"].ToString();
                        instText2 = dtinstruct.Rows[0]["StartigText2"].ToString();
                    }

                    CustomerbillMarathi_A6 custbillmarathiA6 = new CustomerbillMarathi_A6();
                    custbillmarathiA6.SetDataSource(dsbill);

                    custbillmarathiA6.SetParameterValue("instruction1", instText1);
                    custbillmarathiA6.SetParameterValue("instruction2", instText2);
                    custbillmarathiA6.SetParameterValue("custremamt", custremamt);
                    custbillmarathiA6.SetParameterValue("BillType", billType);
                    custbillmarathiA6.SetParameterValue("billRemaining", billRemaining);
                    custbillmarathiA6.SetParameterValue("TotalSavings", TotalSavings);
                    crystalReportViewer1.ReportSource = custbillmarathiA6;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    crystalReportViewer1.Visible = false;

                    SettingRepository SettingRepo = new SettingRepository();
                    AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                    SettingData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SettingData != null)
                    {
                        if (!string.IsNullOrEmpty(SettingData.PrinterName))
                        {
                            if (BType == "WithoutVat")
                                custA6rrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                            custA6rrpt.PrintToPrinter(1, true, 1, 5);
                        }
                    }
                    da.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                var custdata = Custmasterrepo.GetAll().Where(t => t.BillNo == Convert.ToString(txtbillno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                {
                    if (Convert.ToDecimal(custdata.Vat) != 0)
                    { }
                    else
                        ShowBill(Convert.ToString(txtbillno.Text));
                }
                else
                {
                    MessageBox.Show("This Invoice Number is  Not Present.", "Warning");
                    txtbillno.Text = "";
                    this.ActiveControl = txtbillno;
                }
                Custmasterrepo.Dispose();
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