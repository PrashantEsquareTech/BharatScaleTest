using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptCustomerbill : Form
    {
        DbClass db = new DbClass();
        CustomerBillA61 custA6rrpt = new CustomerBillA61();
        CustomerbillRpt_A5 custbillA5 = new CustomerbillRpt_A5();
        CustomerBillSlabCRpt slabRpt = new CustomerBillSlabCRpt();
        CustomerbillRpt_New custbillrptHori = new CustomerbillRpt_New();
        CrptCustomerBillMarathi salereport = new CrptCustomerBillMarathi();
        CustomerbillMarathi_A6 custbillmarathiA6 = new CustomerbillMarathi_A6();
        CustomerbillRpt_HoriZontalNEW custbillrptHori1 = new CustomerbillRpt_HoriZontalNEW();
        CustomerbillRpt_PortraitDifer custbillrptPortrait = new CustomerbillRpt_PortraitDifer();
        CustomerBillUnderCompositionScheme custbillrptcomposition = new CustomerBillUnderCompositionScheme();
        CustomerBillA5HorizontalCrystalReport custbillA5Horizontal = new CustomerBillA5HorizontalCrystalReport();
        CurrencyToWord CurrencytoWord = new CurrencyToWord();
        DsSmallBill dsBill = new DsSmallBill();
        DataTable dataBill;
        public int custvalue, ConsigneeId;
        public string Custbillno, custid, custadd, mobileno, landline, custname, tinno, custstatecode, custstate, custPanno, Number, NetAmtInWord, BType,
            showtender, bonusStatus, bonusShow, gstinno = "", consigneeName, consigneeAdd, consigneeState, consigneeStateCode, consigneeMob, consigneeGStIN;
        public decimal vatamt, discountamt, txtnetamt, tendercash, returncash, Cgst5amt, Cgst12amt, Cgst18amt, Cgst28amt, Sgst5amt, Sgst12amt, Sgst18amt,
            Sgst28amt, Igst5amt, Igst12amt, Igst18amt, Igst28amt, TotCgstAmt, TotIgstAmt, bonusInRs = 0, bonusInPer = 0, prvBonus = 0, currbonus = 0,
            toatlBonus = 0, AlltotalAmount = 0, TotalNetAmt = 0, discounttotal = 0, discpercent = 0;

        public RptCustomerbill()
        {
            InitializeComponent();
        }

        public RptCustomerbill(string billno, string input, string copyType = "")
        {
            Custbillno = billno;
            InitializeComponent();
            SettingRepository SettingRepo = new SettingRepository();
            var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settingdata.TaxInvoicePrintOn == "Marathi" || input == "4")
                showMarathiBill(Custbillno, input, copyType);
            else if (settingdata.TaxInvoicePrintOn == "MarathiA6" || input == "5")
                showMarathiBill(Custbillno, input);
            else if (input == "6")
                showSmallBill_print(Custbillno, input);
            else if (input == "7") //Slab Report
                showCustomerbillSlab(Custbillno, input);
            else if (input == "8")
                showMarathiBill(Custbillno, input);
            else
                ShowBill(Custbillno, input, copyType);
            SettingRepo.Dispose();
        }
       

        public RptCustomerbill(List<int> bList, string billType, string input, string copyType = "")
        {
            InitializeComponent();
            BType = billType;
            SettingRepository SettingRepo = new SettingRepository();
            var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settingdata.TaxInvoicePrintOn == "Marathi" || input == "4")
                showMarathiBill(bList, input);
            else if (settingdata.TaxInvoicePrintOn == "MarathiA6" || input == "5")
            {
            getInput:
                {
                    if (input == "1")
                        ShowMBill(bList, input, copyType);
                    else if (input == "4")
                        showMarathiBill(Custbillno);
                    else if (input == "5")
                        showMarathiBill(bList, input);
                    else if (input == "8")
                        showMarathiBill(bList, input);
                    else if (input == "")
                        ShowMBill(bList, input, copyType);
                    else
                        goto getInput;
                }
            }
            else if (settingdata.TaxInvoicePrintOn == "A6" || input == "8")
                showMarathiBill(bList, input);

            else
                ShowMBill(bList, input, copyType);
            SettingRepo.Dispose();
        }
        
        //This Constructor Only for A6 When print preview dose not want see
        public RptCustomerbill(string billNo, string billType, string input, string copyType = "")
        {
            InitializeComponent();

            PrintA6Invoice(billNo, input, copyType);
         
        }
        public void PrintA6Invoice(string billno, string input = "", string copyType = "")
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal gstpercent = 0, discountAmt = 0, netAmt = 0, Mrate = 0, custremamt = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0, billcurrentAmt = 0, billRemaining = 0, MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", billType = "", MarathiUnit = "", prtbarcode = "", colorname = "", Msize = "0";
                DateTime billdate = DateTime.Today;
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
                TotCgstAmt = 0;
                TotIgstAmt = 0;
                DataTable dtRecord;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SettingRepository SettingRepo = new SettingRepository();
                var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                SqlDataAdapter da;
                DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges from CustomerBillMaster Where BillNo='" + billno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
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
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == billno).FirstOrDefault();
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
                dtRecord.Rows.Add(new object[8] { billno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                //                                           0            1         2        3        4            		5          6          7            8          9          10           11        12         13      			    14        		       15
                DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0) as Vat,d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,isnull(d.SaleRate,0) as SaleRate,isnull(d.Rate,0) as Rate,d.Size,d.ColorName,d.Barcode from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "'  Order By ProductId");
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
                            if (settingdata.ShowMfgComp == "True")
                                MProduct = MProduct + ", " + proddata.ManufactureCompany;
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
                            if (input == "8")
                                Mrate = Convert.ToDecimal(detail.Rows[i]["Rate"]);
                            else
                            {
                                if (Convert.ToDecimal(detail.Rows[i]["sgstpercent"].ToString()) != 0)
                                    Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString());
                                else if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) != 0)
                                    Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString());
                            }
                        }
                        else
                            Mrate = Convert.ToDecimal(detail.Rows[i]["Rate"]);
                        if (MRP != 0)
                        {
                            TotalMRP = TotalMRP + (MRP * MQty);
                            Totalrate = Totalrate + (Mrate * MQty);
                            TotalSavings = TotalMRP - Totalrate;
                        }
                        if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                            Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
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
                        if (detail.Rows[i]["Barcode"].ToString() == null || detail.Rows[i]["Barcode"].ToString() == "")
                            prtbarcode = "";
                        else
                            prtbarcode = Convert.ToString(detail.Rows[i]["Barcode"].ToString());
                        if (detail.Rows[i]["ColorName"].ToString() == null || detail.Rows[i]["ColorName"].ToString() == "")
                            colorname = "";
                        else
                            colorname = Convert.ToString(detail.Rows[i]["ColorName"].ToString());
                        if (detail.Rows[i]["Size"].ToString() == null || detail.Rows[i]["Size"].ToString() == "")
                            Msize = "";
                        else
                            Msize = detail.Rows[i]["Size"].ToString();
                        int count = i + 1;
                        dtRecord = dsbill.Tables["BillDetail"];
                        dtRecord.Rows.Add(new object[12] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP, prtbarcode, colorname, Msize, 0 });
                    }
                }

                DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress,VatNo from CompanyInformation where CompanyId=" + CommonMethod.CompId);
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
                    gstinno = Convert.ToString(dtComp.Rows[0]["VatNo"].ToString());
                    dtRecord = dsbill.Tables["CompanyInfo"];
                    dtRecord.Rows.Add(new object[2] { compname, compadd });
                }

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation Order By ProductName", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " Order By ProductName", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "ProductInformation");

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "Settings");
                string instText1 = "", instText2 = "", instText3 = "", startingText = "";
                DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                if (dtinstruct.Rows.Count > 0)
                {
                    instText1 = dtinstruct.Rows[0]["InstructionText1"].ToString();
                    instText2 = dtinstruct.Rows[0]["InstructionText2"].ToString();
                    instText3 = dtinstruct.Rows[0]["InstructionText3"].ToString();
                    startingText = dtinstruct.Rows[0]["StartigText1"].ToString();
                }
                if (settingdata.TaxInvoicePrintOn == "Both")
                {
                    if (input == "4")
                    {
                        salereport = new CrptCustomerBillMarathi();
                        goto M;
                    }
                    else if (input == "5")
                    {
                        custbillmarathiA6 = new CustomerbillMarathi_A6();
                        goto MA6;
                    }
                    else
                    {
                        custA6rrpt = new CustomerBillA61();
                        goto A6;
                    }
                }
                else if (settingdata.TaxInvoicePrintOn == "Marathi")
                {
                    salereport = new CrptCustomerBillMarathi();
                    goto M;
                }
                else if (settingdata.TaxInvoicePrintOn == "MarathiA6")
                {
                    custbillmarathiA6 = new CustomerbillMarathi_A6();
                    goto MA6;
                }
                else
                {
                    custA6rrpt = new CustomerBillA61();
                    goto A6;
                }
            M:
                {
                    salereport.SetDataSource(dsbill);
                    salereport.SetParameterValue("instruction1", instText1);
                    salereport.SetParameterValue("instruction2", instText2);
                    salereport.SetParameterValue("custremamt", custremamt);
                    salereport.SetParameterValue("BillType", billType);
                    salereport.SetParameterValue("billRemaining", billRemaining);
                    crystalReportViewer1.ReportSource = salereport;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    goto MA;
                }
            MA6:
                {
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
                    goto MA;
                }
            A6:
                {
                    custA6rrpt.SetDataSource(dsbill);
                    custA6rrpt.SetParameterValue("DiscAmt", TotalSavings);
                    custA6rrpt.SetParameterValue("Flag", "Customer Bill");
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
                    SettingRepository SettingRepo2 = new SettingRepository();
                    AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                    SettingData = SettingRepo2.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SettingData != null)
                    {
                        if (!string.IsNullOrEmpty(SettingData.PrinterName))
                        {
                            custA6rrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A6");
                            custA6rrpt.PrintToPrinter(1, true, 1, 5);
                        }
                    }
                    goto MA;
                }
            MA:
                { }
            }
            catch (Exception)
            { }
        }
        public void ShowMBill(List<int> billList, string input, string copyType = "")
        {
            decimal gstpercent = 0, custremamt = 0, billRemaining = 0;
            string custbillno1 = "";
            for (int q = 0; q < billList.Count; q++)
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
                TotCgstAmt = 0;
                TotIgstAmt = 0;
                try
                {
                    decimal mvat = 0, vat1 = 0, vat2 = 0;
                    string taxtype = "";
                    SqlDataAdapter da;
                    DataTable dt = db.GetTable("Select * from CustomerBillMaster Where Id='" + billList[q] + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        custid = Convert.ToString(dt.Rows[0]["Id"].ToString());
                        custbillno1 = Convert.ToString(dt.Rows[0]["BillNo"].ToString());
                        custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                        decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                        discounttotal = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()) * Convert.ToDecimal(dt.Rows[0]["Discount"].ToString()) / 100;
                        decimal e = 0;
                        if (dt.Rows[0]["Discount"].ToString() != "")
                            e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                        discountamt = (a * e) / 100;
                        txtnetamt = Convert.ToDecimal(a - discountamt);
                        decimal b = 0;
                        if (dt.Rows[0]["Vat"].ToString() != "")
                            b = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                        if (dt.Rows[0]["Vat"].ToString() == null || Convert.ToString(dt.Rows[0]["Vat"].ToString()) == "")
                            mvat = 0;
                        else
                            mvat = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                        vat1 = mvat;

                        vatamt = (txtnetamt * b) / 100;
                        if (dt.Rows[0]["ShipCustomerId"].ToString() == null || dt.Rows[0]["ShipCustomerId"].ToString() == "")
                            ConsigneeId = 0;
                        else
                            ConsigneeId = Convert.ToInt32(dt.Rows[0]["ShipCustomerId"].ToString());

                        if (dt.Rows[0]["TenderCash"].ToString() == null || dt.Rows[0]["TenderCash"].ToString() == "")
                            tendercash = 0;
                        else
                            tendercash = Convert.ToDecimal(dt.Rows[0]["TenderCash"].ToString());
                        if (dt.Rows[0]["ReturnCash"].ToString() == null || dt.Rows[0]["ReturnCash"].ToString() == "")
                            returncash = 0;
                        else
                            returncash = Convert.ToDecimal(dt.Rows[0]["ReturnCash"].ToString());
                        billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                        TotalNetAmt = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());

                        string amountnet = Math.Round(TotalNetAmt, 0, MidpointRounding.AwayFromZero).ToString();
                        Number = amountnet.Split('.')[0];
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
                        tinno = dt1.Rows[0]["BuyerTinNo"].ToString();
                        custstate = dt1.Rows[0]["State"].ToString();
                        custstatecode = dt1.Rows[0]["Statecode"].ToString();
                        custPanno = dt1.Rows[0]["Panno"].ToString();

                        custremamt = 0;
                        RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                        List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        RemainingPayment remainingdata = new RemainingPayment();
                        remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == Convert.ToString(custbillno1)).FirstOrDefault();
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

                    decimal vat1amt = 0, vat2amt = 0;
                    //                                          0              1         2      3          4                	   5          6           7           8            9         10          11 
                    DataTable detail = db.GetTable("Select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0) as Vat,d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo  Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) Order By ProductId");
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
                                taxtype = proddata.TaxInclusive;

                            decimal vatamt1 = 0, discamt = 0;
                            decimal amount = Convert.ToDecimal(detail.Rows[i]["Amount"].ToString());
                            decimal disc = Convert.ToDecimal(detail.Rows[i]["Discount"].ToString());
                            discamt = amount * disc / 100;
                            if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) == mvat)
                            {
                                vat1 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                                if (taxtype == "False" || taxtype == null)
                                    vatamt1 = (amount - discamt) * vat1 / 100;
                                else
                                    vatamt1 = (amount) * vat1 / 100;
                                vat1amt += vatamt1;
                            }
                            else if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != mvat && Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != 0)
                            {
                                vat2 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                                if (taxtype == "False" || taxtype == null)
                                    vatamt1 = (amount - discamt) * vat2 / 100;
                                else
                                    vatamt1 = (amount) * vat2 / 100;
                                vat2amt += vatamt1;
                            }

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

                    da = new SqlDataAdapter("Select * from CustomerBillMaster Where Id='" + billList[q] + "'  and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    CustomerbillDS ds = new CustomerbillDS();
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerBillMaster");

                    da = new SqlDataAdapter("Select d.* from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo  Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) Order By ProductId", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerBillDetail");

                    da = new SqlDataAdapter("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "BillInstruction");

                    da = new SqlDataAdapter("Select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "Settings");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    da = new SqlDataAdapter("Select * from CustomerInformation  where CustomerId='" + custvalue + "' and Customername='" + custname + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerInformation");

                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation Order By ProductName", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " Order By ProductName", db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "ProductInformation");

                    SettingRepository SettingRepo = new SettingRepository();
                    var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (settingdata.ShowTenderCash == "N" || settingdata.ShowTenderCash == null)
                        showtender = "No";
                    else
                        showtender = "Yes";
                    if (settingdata.CompositionScheme == "True")
                    {
                        custbillrptcomposition = new CustomerBillUnderCompositionScheme();
                        goto C;
                    }
                    else
                    {
                        if (input == "1")
                        {
                            custbillrptPortrait = new CustomerbillRpt_PortraitDifer();
                            goto P;
                        }
                        else if (input == "2")
                        {
                            custbillrptHori1 = new CustomerbillRpt_HoriZontalNEW();
                            goto H;
                        }
                        else if (input == "3")
                        {
                            custbillA5 = new CustomerbillRpt_A5();
                            goto A5;
                        }
                        else if (input == "9")
                        {

                            custbillA5Horizontal = new CustomerBillA5HorizontalCrystalReport();
                            goto A5H;
                        }
                        else if (input == "")
                        {
                            custbillrptPortrait = new CustomerbillRpt_PortraitDifer();
                            goto P;
                        }
                    }
                C:
                    {
                        custbillrptcomposition.SetDataSource(ds);
                        custbillrptcomposition.SetParameterValue("custname", custname);
                        custbillrptcomposition.SetParameterValue("customeraddress", custadd);
                        custbillrptcomposition.SetParameterValue("mobileno", mobileno);
                        custbillrptcomposition.SetParameterValue("discamt", discountamt);
                        custbillrptcomposition.SetParameterValue("buyertinno", tinno);
                        custbillrptcomposition.SetParameterValue("custstate", custstate);
                        custbillrptcomposition.SetParameterValue("custstatecode", custstatecode);
                        custbillrptcomposition.SetParameterValue("NetAmtInWord", NetAmtInWord);
                        custbillrptcomposition.SetParameterValue("ConsigneeName", consigneeName);
                        custbillrptcomposition.SetParameterValue("consigneeAdd", consigneeAdd);
                        custbillrptcomposition.SetParameterValue("consigneeMob", consigneeMob);
                        custbillrptcomposition.SetParameterValue("consigneeState", consigneeState);
                        custbillrptcomposition.SetParameterValue("consigneeStateCode", consigneeStateCode);
                        custbillrptcomposition.SetParameterValue("consigneeGStIN", consigneeGStIN);
                        custbillrptcomposition.SetParameterValue("custPanno", custPanno);
                        custbillrptcomposition.SetParameterValue("PrintCopy", copyType);

                        crystalReportViewer1.ReportSource = custbillrptcomposition;
                        crystalReportViewer1.Refresh();
                        crystalReportViewer1.ParameterFieldInfo.Clear();
                        crystalReportViewer1.Visible = false;

                        SettingRepository SettingRepo1 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                if (BType == "Fake")
                                    custbillrptcomposition.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                                else if (BType == "WithVat")
                                    custbillrptcomposition.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                                custbillrptcomposition.PrintToPrinter(1, true, 1, 5);
                            }
                        }
                        SettingRepo.Dispose();
                        da.Dispose();
                        ds.Dispose();
                        dt.Dispose();
                        dt1.Dispose();
                        detail.Dispose();
                        goto S;
                    }
                P:
                    {
                        custbillrptPortrait.SetDataSource(ds);
                        custbillrptPortrait.SetParameterValue("custname", custname);
                        custbillrptPortrait.SetParameterValue("customeraddress", custadd);
                        custbillrptPortrait.SetParameterValue("mobileno", mobileno);
                        custbillrptPortrait.SetParameterValue("landlineno", landline);
                        custbillrptPortrait.SetParameterValue("discamt", discountamt);
                        custbillrptPortrait.SetParameterValue("Vatamt", vatamt);
                        custbillrptPortrait.SetParameterValue("buyertinno", tinno);
                        custbillrptPortrait.SetParameterValue("custstate", custstate);
                        custbillrptPortrait.SetParameterValue("custstatecode", custstatecode);
                        custbillrptPortrait.SetParameterValue("NetAmtInWord", NetAmtInWord);
                        custbillrptPortrait.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                        custbillrptPortrait.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                        custbillrptPortrait.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                        custbillrptPortrait.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));
                        custbillrptPortrait.SetParameterValue("ConsigneeName", consigneeName);
                        custbillrptPortrait.SetParameterValue("consigneeAdd", consigneeAdd);
                        custbillrptPortrait.SetParameterValue("consigneeMob", consigneeMob);
                        custbillrptPortrait.SetParameterValue("consigneeState", consigneeState);
                        custbillrptPortrait.SetParameterValue("consigneeStateCode", consigneeStateCode);
                        custbillrptPortrait.SetParameterValue("consigneeGStIN", consigneeGStIN);
                        custbillrptPortrait.SetParameterValue("custPanno", custPanno);
                        custbillrptPortrait.SetParameterValue("Cgst5amt", Cgst5amt);
                        custbillrptPortrait.SetParameterValue("Cgst12amt", Cgst12amt);
                        custbillrptPortrait.SetParameterValue("Cgst18amt", Cgst18amt);
                        custbillrptPortrait.SetParameterValue("Cgst28amt", Cgst28amt);
                        custbillrptPortrait.SetParameterValue("Igst5amt", Igst5amt);
                        custbillrptPortrait.SetParameterValue("Igst12amt", Igst12amt);
                        custbillrptPortrait.SetParameterValue("Igst18amt", Igst18amt);
                        custbillrptPortrait.SetParameterValue("Igst28amt", Igst28amt);
                        custbillrptPortrait.SetParameterValue("TotCgstAmt", TotCgstAmt);
                        custbillrptPortrait.SetParameterValue("TotIgstAmt", TotIgstAmt);
                        custbillrptPortrait.SetParameterValue("PrintCopy", copyType);
                        custbillrptPortrait.SetParameterValue("ShowTender", showtender);
                        custbillrptPortrait.SetParameterValue("TenderCash", tendercash);
                        custbillrptPortrait.SetParameterValue("ReturnCash", returncash);
                        custbillrptPortrait.SetParameterValue("custremamt", custremamt);
                        custbillrptPortrait.SetParameterValue("billRemaining", billRemaining);
                        custbillrptPortrait.SetParameterValue("bonusShow", bonusShow);
                        custbillrptPortrait.SetParameterValue("PrvBonusAmt", prvBonus);
                        custbillrptPortrait.SetParameterValue("CurrBonusAmt", currbonus);
                        custbillrptPortrait.SetParameterValue("TotalBonusAmt", toatlBonus);
                        crystalReportViewer1.ReportSource = custbillrptPortrait;
                        crystalReportViewer1.Refresh();
                        crystalReportViewer1.ParameterFieldInfo.Clear();
                        crystalReportViewer1.Visible = false;

                        SettingRepository SettingRepo1 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                if (BType == "Fake")
                                    custbillrptPortrait.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                                else if (BType == "WithVat")
                                    custbillrptPortrait.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                                custbillrptPortrait.PrintToPrinter(1, true, 1, 5);
                            }
                        }
                        SettingRepo.Dispose();
                        da.Dispose();
                        ds.Dispose();
                        dt.Dispose();
                        dt1.Dispose();
                        detail.Dispose();
                        goto S;
                    }
                H:
                    {
                        custbillrptHori1.SetDataSource(ds);
                        custbillrptHori1.SetParameterValue("custname", custname);
                        custbillrptHori1.SetParameterValue("customeraddress", custadd);
                        custbillrptHori1.SetParameterValue("mobileno", mobileno);
                        custbillrptHori1.SetParameterValue("landlineno", landline);
                        custbillrptHori1.SetParameterValue("discamt", discountamt);
                        custbillrptHori1.SetParameterValue("Vatamt", vatamt);
                        custbillrptHori1.SetParameterValue("buyertinno", tinno);
                        custbillrptHori1.SetParameterValue("custstate", custstate);
                        custbillrptHori1.SetParameterValue("custstatecode", custstatecode);
                        custbillrptHori1.SetParameterValue("NetAmtInWord", NetAmtInWord);
                        custbillrptHori1.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                        custbillrptHori1.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                        custbillrptHori1.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                        custbillrptHori1.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));
                        custbillrptHori1.SetParameterValue("ConsigneeName", consigneeName);
                        custbillrptHori1.SetParameterValue("consigneeAdd", consigneeAdd);
                        custbillrptHori1.SetParameterValue("consigneeMob", consigneeMob);
                        custbillrptHori1.SetParameterValue("consigneeState", consigneeState);
                        custbillrptHori1.SetParameterValue("consigneeStateCode", consigneeStateCode);
                        custbillrptHori1.SetParameterValue("consigneeGStIN", consigneeGStIN);
                        custbillrptHori1.SetParameterValue("custPanno", custPanno);
                        custbillrptHori1.SetParameterValue("Cgst5amt", Cgst5amt);
                        custbillrptHori1.SetParameterValue("Cgst12amt", Cgst12amt);
                        custbillrptHori1.SetParameterValue("Cgst18amt", Cgst18amt);
                        custbillrptHori1.SetParameterValue("Cgst28amt", Cgst28amt);
                        custbillrptHori1.SetParameterValue("Igst5amt", Igst5amt);
                        custbillrptHori1.SetParameterValue("Igst12amt", Igst12amt);
                        custbillrptHori1.SetParameterValue("Igst18amt", Igst18amt);
                        custbillrptHori1.SetParameterValue("Igst28amt", Igst28amt);
                        custbillrptHori1.SetParameterValue("TotCgstAmt", TotCgstAmt);
                        custbillrptHori1.SetParameterValue("TotIgstAmt", TotIgstAmt);
                        custbillrptHori1.SetParameterValue("TotIgstAmt", TotIgstAmt);
                        custbillrptHori1.SetParameterValue("PrintCopy", copyType);
                        custbillrptHori1.SetParameterValue("ShowTender", showtender);
                        custbillrptHori1.SetParameterValue("TenderCash", tendercash);
                        custbillrptHori1.SetParameterValue("ReturnCash", returncash);
                        custbillrptHori1.SetParameterValue("custremamt", custremamt);
                        custbillrptHori1.SetParameterValue("billRemaining", billRemaining);
                        crystalReportViewer1.ReportSource = custbillrptHori1;
                        crystalReportViewer1.Refresh();
                        crystalReportViewer1.ParameterFieldInfo.Clear();
                        crystalReportViewer1.Visible = false;

                        SettingRepository SettingRepo1 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                if (BType == "Fake")
                                    custbillrptHori1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                                else if (BType == "WithVat")
                                    custbillrptHori1.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                                custbillrptHori1.PrintToPrinter(1, true, 1, 5);
                            }
                        }
                        SettingRepo.Dispose();
                        da.Dispose();
                        ds.Dispose();
                        dt.Dispose();
                        dt1.Dispose();
                        detail.Dispose();
                        goto S;
                    }
                A5:
                    {
                        custbillA5.SetDataSource(ds);
                        custbillA5.SetParameterValue("custname", custname);
                        custbillA5.SetParameterValue("customeraddress", custadd);
                        custbillA5.SetParameterValue("mobileno", mobileno);
                        custbillA5.SetParameterValue("buyertinno", tinno);
                        custbillA5.SetParameterValue("discamt", discountamt);
                        custbillA5.SetParameterValue("custstate", custstate);
                        custbillA5.SetParameterValue("custstatecode", custstatecode);
                        custbillA5.SetParameterValue("custPanno", custPanno);
                        custbillA5.SetParameterValue("NetAmtInWord", NetAmtInWord);
                        custbillA5.SetParameterValue("Cgst5amt", Cgst5amt);
                        custbillA5.SetParameterValue("Cgst12amt", Cgst12amt);
                        custbillA5.SetParameterValue("Cgst18amt", Cgst18amt);
                        custbillA5.SetParameterValue("Cgst28amt", Cgst28amt);
                        custbillA5.SetParameterValue("Igst5amt", Igst5amt);
                        custbillA5.SetParameterValue("Igst12amt", Igst12amt);
                        custbillA5.SetParameterValue("Igst18amt", Igst18amt);
                        custbillA5.SetParameterValue("Igst28amt", Igst28amt);
                        custbillA5.SetParameterValue("TotCgstAmt", TotCgstAmt);
                        custbillA5.SetParameterValue("TotIgstAmt", TotIgstAmt);
                        custbillA5.SetParameterValue("PrintCopy", copyType);
                        custbillA5.SetParameterValue("ShowTender", showtender);
                        custbillA5.SetParameterValue("TenderCash", tendercash);
                        custbillA5.SetParameterValue("ReturnCash", returncash);
                        custbillA5.SetParameterValue("custremamt", custremamt);
                        custbillA5.SetParameterValue("billRemaining", billRemaining);

                        crystalReportViewer1.ReportSource = custbillA5;
                        crystalReportViewer1.Refresh();
                        crystalReportViewer1.ParameterFieldInfo.Clear();
                        crystalReportViewer1.Visible = false;
                        SettingRepository SettingRepo1 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                if (BType == "Fake")
                                    custbillA5.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                                else if (BType == "WithVat")
                                    custbillA5.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                                custbillA5.PrintToPrinter(1, true, 1, 99);
                            }
                        }
                        da.Dispose();
                        ds.Dispose();
                        dt.Dispose();
                        dt1.Dispose();
                        detail.Dispose();
                        goto S;

                    }
                A5H:
                    {
                        custbillA5Horizontal.SetDataSource(ds);
                        custbillA5Horizontal.SetParameterValue("custname", custname);
                        custbillA5Horizontal.SetParameterValue("customeraddress", custadd);
                        custbillA5Horizontal.SetParameterValue("mobileno", mobileno);
                        custbillA5Horizontal.SetParameterValue("buyertinno", tinno);
                        custbillA5Horizontal.SetParameterValue("discamt", discountamt);
                        custbillA5Horizontal.SetParameterValue("custstate", custstate);
                        custbillA5Horizontal.SetParameterValue("custstatecode", custstatecode);
                        custbillA5Horizontal.SetParameterValue("custPanno", custPanno);
                        custbillA5Horizontal.SetParameterValue("NetAmtInWord", NetAmtInWord);
                        custbillA5Horizontal.SetParameterValue("Cgst5amt", Cgst5amt);
                        custbillA5Horizontal.SetParameterValue("Cgst12amt", Cgst12amt);
                        custbillA5Horizontal.SetParameterValue("Cgst18amt", Cgst18amt);
                        custbillA5Horizontal.SetParameterValue("Cgst28amt", Cgst28amt);
                        custbillA5Horizontal.SetParameterValue("Igst5amt", Igst5amt);
                        custbillA5Horizontal.SetParameterValue("Igst12amt", Igst12amt);
                        custbillA5Horizontal.SetParameterValue("Igst18amt", Igst18amt);
                        custbillA5Horizontal.SetParameterValue("Igst28amt", Igst28amt);
                        custbillA5Horizontal.SetParameterValue("TotCgstAmt", TotCgstAmt);
                        custbillA5Horizontal.SetParameterValue("TotIgstAmt", TotIgstAmt);
                        custbillA5Horizontal.SetParameterValue("PrintCopy", copyType);
                        custbillA5Horizontal.SetParameterValue("ShowTender", showtender);
                        custbillA5Horizontal.SetParameterValue("TenderCash", tendercash);
                        custbillA5Horizontal.SetParameterValue("ReturnCash", returncash);
                        custbillA5Horizontal.SetParameterValue("custremamt", custremamt);
                        custbillA5Horizontal.SetParameterValue("billRemaining", billRemaining);

                        crystalReportViewer1.ReportSource = custbillA5Horizontal;
                        crystalReportViewer1.Refresh();
                        crystalReportViewer1.ParameterFieldInfo.Clear();

                        SettingRepository SettingRepo1 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                if (BType == "Fake")
                                    custbillA5Horizontal.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                                else if (BType == "WithVat")
                                    custbillA5Horizontal.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                                custbillA5Horizontal.PrintToPrinter(1, true, 1, 99);
                            }
                        }
                        da.Dispose();
                        ds.Dispose();
                        dt.Dispose();
                        dt1.Dispose();
                        detail.Dispose();
                        goto S;
                    }
                S:
                    { }
                }
                catch (Exception)
                { }
            }
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

        public void ShowBill(string CustrBillNo, string input, string copyType = "")
        {
            try
            {
                decimal mvat = 0, vat1 = 0, vat2 = 0, gstpercent = 0, custremamt = 0, billRemaining = 0;
                string taxtype = "";
                int custId = 0, CID = 0;
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
                TotCgstAmt = 0;
                TotIgstAmt = 0;
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                GoodReturnMaster SaleRMData = new GoodReturnMaster();
                SaleRMData = SaleRMRepo.GetAll().Where(t => t.BillNo == CustrBillNo && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (SaleRMData != null)
                    btnPrintReturn.Visible = true;
                else
                    btnPrintReturn.Visible = false;
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
                    txtnetamt = Convert.ToDecimal(a - discountamt);
                    decimal b = 0;
                    if (dt.Rows[0]["Vat"].ToString() != "")
                        b = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    if (dt.Rows[0]["Vat"].ToString() != null && dt.Rows[0]["Vat"].ToString() != "")
                        mvat = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    vat1 = mvat;
                    vatamt = Convert.ToDecimal((txtnetamt * b) / 100);
                    if (dt.Rows[0]["ShipCustomerId"].ToString() == "" || dt.Rows[0]["ShipCustomerId"].ToString() == null)
                        ConsigneeId = 0;
                    else
                        ConsigneeId = Convert.ToInt32(dt.Rows[0]["ShipCustomerId"].ToString());
                    if (dt.Rows[0]["TenderCash"].ToString() == null || dt.Rows[0]["TenderCash"].ToString() == "")
                        tendercash = 0;
                    else
                        tendercash = Convert.ToDecimal(dt.Rows[0]["TenderCash"].ToString());
                    if (dt.Rows[0]["ReturnCash"].ToString() == null || dt.Rows[0]["ReturnCash"].ToString() == "")
                        returncash = 0;
                    else
                        returncash = Convert.ToDecimal(dt.Rows[0]["ReturnCash"].ToString());
                    if (dt.Rows[0]["RemainingAmount"].ToString() == "" || dt.Rows[0]["RemainingAmount"].ToString() == null)
                        billRemaining = 0;
                    else
                        billRemaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                    if (dt.Rows[0]["NetAmount"].ToString() == "" || dt.Rows[0]["NetAmount"].ToString() == null)
                        TotalNetAmt = 0;
                    else
                        TotalNetAmt = Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString());
                    Number = Math.Round(TotalNetAmt, 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                }

                DataTable dt1 = db.GetTable("Select CustomerName,CustomeAddress,MobileNo,LandlineNo,CreditDays,BuyerTinNo,State,StateCode,Panno,customerid,BonusIn,BonusInRsNPer,Id from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    custname = dt1.Rows[0]["CustomerName"].ToString();
                    custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                    mobileno = dt1.Rows[0]["MobileNo"].ToString();
                    landline = dt1.Rows[0]["LandlineNo"].ToString();
                    tinno = dt1.Rows[0]["BuyerTinNo"].ToString();
                    custstate = dt1.Rows[0]["State"].ToString();
                    custstatecode = dt1.Rows[0]["StateCode"].ToString();
                    custPanno = dt1.Rows[0]["Panno"].ToString();
                    custId = Convert.ToInt32(dt1.Rows[0]["customerid"].ToString());
                    CID = Convert.ToInt32(dt1.Rows[0]["Id"].ToString());

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
                decimal vat1amt = 0, vat2amt = 0;
                //                                           0           1          2        3        4                         5         6           7            8          9          10          11          12
                DataTable detail = db.GetTable("Select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0) as Vat,d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Prodsubtitle from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId");
                if (detail.Rows.Count > 0)
                {
                    for (int i = 0; i < detail.Rows.Count; i++)
                    {
                        decimal vatamt1 = 0, discamt = 0;
                        decimal amount = Convert.ToDecimal(detail.Rows[i]["Amount"].ToString());
                        decimal disc = Convert.ToDecimal(detail.Rows[i]["Discount"].ToString());
                        discamt = amount * disc / 100;
                        if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) == mvat)
                        {
                            vat1 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                            if (taxtype == "False" || taxtype == null)
                                vatamt1 = (amount - discamt) * vat1 / 100;
                            else
                                vatamt1 = (amount) * vat1 / 100;
                            vat1amt += vatamt1;
                        }
                        else if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != mvat && Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != 0)
                        {
                            vat2 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                            if (taxtype == "False" || taxtype == null)
                                vatamt1 = (amount - discamt) * vat2 / 100;
                            else
                                vatamt1 = (amount) * vat2 / 100;
                            vat2amt += vatamt1;
                        }
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
                CustomerbillDS ds = new CustomerbillDS();

                da.Fill(ds, "CustomerBillMaster");

                da = new SqlDataAdapter("Select d.* from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by ProductId", db.Connection);
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

                da = new SqlDataAdapter("Select * from CustomerInformation  where CustomerId=" + custvalue + " and Id=" + CID + " and CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation order by Productname", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " order by Productname", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                SettingRepository SettingRepo = new SettingRepository();
                var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (settingdata.ShowTenderCash == "N" || settingdata.ShowTenderCash == null)
                    showtender = "No";
                else
                    showtender = "Yes";
                if (settingdata.TaxInvoicePrintOn == "Portrait")
                {
                    custbillrptPortrait = new CustomerbillRpt_PortraitDifer();
                    goto P;
                }
                else if (settingdata.TaxInvoicePrintOn == "Both")
                {
                a:
                    {
                        if (input == "1")
                        {
                            custbillrptPortrait = new CustomerbillRpt_PortraitDifer();
                            goto P;
                        }
                        else if (input == "2")
                        {
                            custbillrptHori = new CustomerbillRpt_New();
                            goto H;
                        }
                        else if (input == "3")
                        {
                            custbillA5 = new CustomerbillRpt_A5();
                            goto A5;
                        }
                        else if (input == "9")
                        {
                            custbillA5Horizontal = new CustomerBillA5HorizontalCrystalReport();
                            goto A5H;
                        }
                        else if (input == "")
                        {
                            custbillrptPortrait = new CustomerbillRpt_PortraitDifer();
                            goto P;
                        }
                        else
                            goto a;
                    }
                }
                else if (settingdata.TaxInvoicePrintOn == "HoriZontal")
                {
                    custbillrptHori = new CustomerbillRpt_New();
                    goto H;
                }
                else if (settingdata.TaxInvoicePrintOn == "A5")
                {
                    custbillA5 = new CustomerbillRpt_A5();
                    goto A5;
                }
                else
                {
                    custbillA5Horizontal = new CustomerBillA5HorizontalCrystalReport();
                    goto A5H;
                }
            P:
                {
                    custbillrptPortrait.SetDataSource(ds);
                    custbillrptPortrait.SetParameterValue("custname", custname);
                    custbillrptPortrait.SetParameterValue("customeraddress", custadd);
                    custbillrptPortrait.SetParameterValue("mobileno", mobileno);
                    custbillrptPortrait.SetParameterValue("landlineno", landline);
                    custbillrptPortrait.SetParameterValue("discamt", discountamt);
                    custbillrptPortrait.SetParameterValue("Vatamt", vatamt);
                    custbillrptPortrait.SetParameterValue("buyertinno", tinno);
                    custbillrptPortrait.SetParameterValue("custstate", custstate);
                    custbillrptPortrait.SetParameterValue("custstatecode", custstatecode);
                    custbillrptPortrait.SetParameterValue("custPanno", custPanno);
                    custbillrptPortrait.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    custbillrptPortrait.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                    custbillrptPortrait.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                    custbillrptPortrait.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                    custbillrptPortrait.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));
                    custbillrptPortrait.SetParameterValue("ConsigneeName", consigneeName);
                    custbillrptPortrait.SetParameterValue("consigneeAdd", consigneeAdd);
                    custbillrptPortrait.SetParameterValue("consigneeMob", consigneeMob);
                    custbillrptPortrait.SetParameterValue("consigneeState", consigneeState);
                    custbillrptPortrait.SetParameterValue("consigneeStateCode", consigneeStateCode);
                    custbillrptPortrait.SetParameterValue("consigneeGStIN", consigneeGStIN);
                    custbillrptPortrait.SetParameterValue("Cgst5amt", Cgst5amt);
                    custbillrptPortrait.SetParameterValue("Cgst12amt", Cgst12amt);
                    custbillrptPortrait.SetParameterValue("Cgst18amt", Cgst18amt);
                    custbillrptPortrait.SetParameterValue("Cgst28amt", Cgst28amt);
                    custbillrptPortrait.SetParameterValue("Igst5amt", Igst5amt);
                    custbillrptPortrait.SetParameterValue("Igst12amt", Igst12amt);
                    custbillrptPortrait.SetParameterValue("Igst18amt", Igst18amt);
                    custbillrptPortrait.SetParameterValue("Igst28amt", Igst28amt);
                    custbillrptPortrait.SetParameterValue("TotCgstAmt", TotCgstAmt);
                    custbillrptPortrait.SetParameterValue("TotIgstAmt", TotIgstAmt);
                    custbillrptPortrait.SetParameterValue("PrintCopy", copyType);
                    custbillrptPortrait.SetParameterValue("ShowTender", showtender);
                    custbillrptPortrait.SetParameterValue("TenderCash", tendercash);
                    custbillrptPortrait.SetParameterValue("ReturnCash", returncash);
                    custbillrptPortrait.SetParameterValue("custremamt", custremamt);
                    custbillrptPortrait.SetParameterValue("billRemaining", billRemaining);
                    custbillrptPortrait.SetParameterValue("bonusShow", bonusShow);
                    custbillrptPortrait.SetParameterValue("PrvBonusAmt", prvBonus);
                    custbillrptPortrait.SetParameterValue("CurrBonusAmt", currbonus);
                    custbillrptPortrait.SetParameterValue("TotalBonusAmt", toatlBonus);
                    crystalReportViewer1.ReportSource = custbillrptPortrait;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    SaleRMRepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    detail.Dispose();
                    goto S;
                }
            H:
                {
                    custbillrptHori.SetDataSource(ds);
                    custbillrptHori.SetParameterValue("custname", custname);
                    custbillrptHori.SetParameterValue("customeraddress", custadd);
                    custbillrptHori.SetParameterValue("mobileno", mobileno);
                    custbillrptHori.SetParameterValue("landlineno", landline);
                    custbillrptHori.SetParameterValue("discamt", discountamt);
                    custbillrptHori.SetParameterValue("Vatamt", vatamt);
                    custbillrptHori.SetParameterValue("buyertinno", tinno);
                    custbillrptHori.SetParameterValue("custstate", custstate);
                    custbillrptHori.SetParameterValue("custstatecode", custstatecode);
                    custbillrptHori.SetParameterValue("custPanno", custPanno);
                    custbillrptHori.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    custbillrptHori.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                    custbillrptHori.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                    custbillrptHori.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                    custbillrptHori.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));
                    custbillrptHori.SetParameterValue("ConsigneeName", consigneeName);
                    custbillrptHori.SetParameterValue("consigneeAdd", consigneeAdd);
                    custbillrptHori.SetParameterValue("consigneeMob", consigneeMob);
                    custbillrptHori.SetParameterValue("consigneeState", consigneeState);
                    custbillrptHori.SetParameterValue("consigneeStateCode", consigneeStateCode);
                    custbillrptHori.SetParameterValue("consigneeGStIN", consigneeGStIN);
                    custbillrptHori.SetParameterValue("Cgst5amt", Cgst5amt);
                    custbillrptHori.SetParameterValue("Cgst12amt", Cgst12amt);
                    custbillrptHori.SetParameterValue("Cgst18amt", Cgst18amt);
                    custbillrptHori.SetParameterValue("Cgst28amt", Cgst28amt);
                    custbillrptHori.SetParameterValue("Igst5amt", Igst5amt);
                    custbillrptHori.SetParameterValue("Igst12amt", Igst12amt);
                    custbillrptHori.SetParameterValue("Igst18amt", Igst18amt);
                    custbillrptHori.SetParameterValue("Igst28amt", Igst28amt);
                    custbillrptHori.SetParameterValue("TotCgstAmt", TotCgstAmt);
                    custbillrptHori.SetParameterValue("TotIgstAmt", TotIgstAmt);
                    custbillrptHori.SetParameterValue("PrintCopy", copyType);
                    custbillrptHori.SetParameterValue("ShowTender", showtender);
                    custbillrptHori.SetParameterValue("TenderCash", tendercash);
                    custbillrptHori.SetParameterValue("ReturnCash", returncash);
                    custbillrptHori.SetParameterValue("custremamt", custremamt);
                    custbillrptHori.SetParameterValue("billRemaining", billRemaining);
                    crystalReportViewer1.ReportSource = custbillrptHori;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    SaleRMRepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    detail.Dispose();
                    goto S;
                }
            A5:
                {
                    custbillA5.SetDataSource(ds);
                    custbillA5.SetParameterValue("custname", custname);
                    custbillA5.SetParameterValue("customeraddress", custadd);
                    custbillA5.SetParameterValue("mobileno", mobileno);
                    custbillA5.SetParameterValue("buyertinno", tinno);
                    custbillA5.SetParameterValue("discamt", discountamt);
                    custbillA5.SetParameterValue("custstate", custstate);
                    custbillA5.SetParameterValue("custstatecode", custstatecode);
                    custbillA5.SetParameterValue("custPanno", custPanno);
                    custbillA5.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    custbillA5.SetParameterValue("Cgst5amt", Cgst5amt);
                    custbillA5.SetParameterValue("Cgst12amt", Cgst12amt);
                    custbillA5.SetParameterValue("Cgst18amt", Cgst18amt);
                    custbillA5.SetParameterValue("Cgst28amt", Cgst28amt);
                    custbillA5.SetParameterValue("Igst5amt", Igst5amt);
                    custbillA5.SetParameterValue("Igst12amt", Igst12amt);
                    custbillA5.SetParameterValue("Igst18amt", Igst18amt);
                    custbillA5.SetParameterValue("Igst28amt", Igst28amt);
                    custbillA5.SetParameterValue("TotCgstAmt", TotCgstAmt);
                    custbillA5.SetParameterValue("TotIgstAmt", TotIgstAmt);
                    custbillA5.SetParameterValue("PrintCopy", copyType);
                    custbillA5.SetParameterValue("ShowTender", showtender);
                    custbillA5.SetParameterValue("TenderCash", tendercash);
                    custbillA5.SetParameterValue("ReturnCash", returncash);
                    custbillA5.SetParameterValue("custremamt", custremamt);
                    custbillA5.SetParameterValue("billRemaining", billRemaining);

                    crystalReportViewer1.ReportSource = custbillA5;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    SaleRMRepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    detail.Dispose();
                    goto S;
                }
            A5H:
                {
                    custbillA5Horizontal.SetDataSource(ds);
                    custbillA5Horizontal.SetParameterValue("custname", custname);
                    custbillA5Horizontal.SetParameterValue("customeraddress", custadd);
                    custbillA5Horizontal.SetParameterValue("mobileno", mobileno);
                    custbillA5Horizontal.SetParameterValue("buyertinno", tinno);
                    custbillA5Horizontal.SetParameterValue("discamt", discountamt);
                    custbillA5Horizontal.SetParameterValue("custstate", custstate);
                    custbillA5Horizontal.SetParameterValue("custstatecode", custstatecode);
                    custbillA5Horizontal.SetParameterValue("custPanno", custPanno);
                    custbillA5Horizontal.SetParameterValue("NetAmtInWord", NetAmtInWord);
                    custbillA5Horizontal.SetParameterValue("Cgst5amt", Cgst5amt);
                    custbillA5Horizontal.SetParameterValue("Cgst12amt", Cgst12amt);
                    custbillA5Horizontal.SetParameterValue("Cgst18amt", Cgst18amt);
                    custbillA5Horizontal.SetParameterValue("Cgst28amt", Cgst28amt);
                    custbillA5Horizontal.SetParameterValue("Igst5amt", Igst5amt);
                    custbillA5Horizontal.SetParameterValue("Igst12amt", Igst12amt);
                    custbillA5Horizontal.SetParameterValue("Igst18amt", Igst18amt);
                    custbillA5Horizontal.SetParameterValue("Igst28amt", Igst28amt);
                    custbillA5Horizontal.SetParameterValue("TotCgstAmt", TotCgstAmt);
                    custbillA5Horizontal.SetParameterValue("TotIgstAmt", TotIgstAmt);
                    custbillA5Horizontal.SetParameterValue("PrintCopy", copyType);
                    custbillA5Horizontal.SetParameterValue("ShowTender", showtender);
                    custbillA5Horizontal.SetParameterValue("TenderCash", tendercash);
                    custbillA5Horizontal.SetParameterValue("ReturnCash", returncash);
                    custbillA5Horizontal.SetParameterValue("custremamt", custremamt);
                    custbillA5Horizontal.SetParameterValue("billRemaining", billRemaining);

                    crystalReportViewer1.ReportSource = custbillA5Horizontal;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    SaleRMRepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    detail.Dispose();
                    goto S;
                }
            S:
                { }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string cbn = Convert.ToString(txtbillno.Text);
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                var custdata = Custmasterrepo.GetAll().Where(t => t.BillNo == cbn && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                {
                    char c = cbn.FirstOrDefault();
                    if (c == 'T' || c == 'F')
                    {
                        string input = "";
                        SettingRepository SettingRepo = new SettingRepository();
                        var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (settingdata.TaxInvoicePrintOn == "Marathi" || input == "4")
                            showMarathiBill(cbn, input);
                        else if (settingdata.TaxInvoicePrintOn == "MarathiA6" || input == "5")
                            showMarathiBill(cbn);
                        else if (settingdata.TaxInvoicePrintOn == "Both")
                        {
                            input = Microsoft.VisualBasic.Interaction.InputBox("1 For Portrait and 2 For LandScape and 3 For A5 and 4 For Marathi Bill and 5 For Marathi A6 Bill", "Tax Invoice", "", 250, 250);
                            if (input == "1")
                                input = "1";
                            else if (input == "2")
                                input = "2";
                            else if (input == "3")
                                input = "3";
                            else if (input == "4")
                            {
                                input = "4";
                                showMarathiBill(cbn, input);
                            }
                            else if (input == "5")
                            {
                                input = "5";
                                showMarathiBill(cbn, input);
                            }
                            else if (input == "6")
                            {
                                input = "6";
                                showSmallBill_print(cbn, input);
                            }
                            else if (input == "7")
                            {
                                input = "7";
                                showCustomerbillSlab(cbn, input);
                            }
                            ShowBill(cbn, input);
                        }
                        else if (settingdata.TaxInvoicePrintOn == "Thermal" || input == "6")
                            showSmallBill_print(cbn, input);
                        else if (settingdata.TaxInvoicePrintOn == "Slab" || input == "7") //Slab Report
                            showCustomerbillSlab(cbn, input);
                        else
                            ShowBill(cbn, input);
                    }
                }
                else
                {
                    MessageBox.Show("This Invoice Number is Not Present.", "Warning");
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

        private void btnPrintReturn_Click(object sender, EventArgs e)
        {
            try
            {
                GetData(Custbillno);
            }
            catch (Exception)
            { }
        }

        public void GetData(string BillNo)
        {
            try
            {
                decimal vat1 = 0, vat2 = 0, mvat = 0, mdvat = 0;
                int Sreturnno = 0, Sreturnid = 0, BillId = 0;
                SaleReturnCrystalReport salerrpt = new SaleReturnCrystalReport();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from GoodReturnMaster Where BillNo='" + BillNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSSaleReturn ds = new DSSaleReturn();
                da.Fill(ds, "GoodReturnMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Sreturnid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    Sreturnno = Convert.ToInt32(dt.Rows[0]["GoodReturnNo"].ToString());
                    BillNo = Convert.ToString(dt.Rows[0]["BillNo"].ToString());
                }
                DataTable master = db.GetTable("Select * from CustomerBillMaster Where BillNo='" + BillNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (master.Rows.Count > 0)
                {
                    BillId = Convert.ToInt32(master.Rows[0]["Id"].ToString());
                    mvat = Convert.ToDecimal(master.Rows[0]["Vat"].ToString());
                    vat1 = mvat;
                }
                if (mvat != 0)
                {
                    DataTable detail = db.GetTable("Select * from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo='" + BillId + "'  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    if (detail.Rows.Count > 0)
                    {
                        for (int i = 0; i < detail.Rows.Count; i++)
                        {
                            mdvat = mdvat + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());
                        }
                    }
                    detail.Dispose();
                }

                decimal vat1amt = 0, vat2amt = 0;
                DataTable SRdetail = db.GetTable("Select * from GoodReturnDetail d inner join GoodReturnMaster m on m.Id=d.GoodReturnMaster Where d.GoodReturnNo=" + Sreturnid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                if (SRdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < SRdetail.Rows.Count; i++)
                    {
                        ProductRepository ProductRepo = new ProductRepository();
                        string taxtype = "";
                        if (CommonMethod.commProduct == true)
                            taxtype = ProductRepo.GetAll().Where(t => t.ProductName == SRdetail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == SRdetail.Rows[i]["CompanyName"].ToString() && t.Uniti == SRdetail.Rows[i]["Unit"].ToString()).FirstOrDefault().TaxInclusive;
                        else
                            taxtype = ProductRepo.GetAll().Where(t => t.ProductName == SRdetail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == SRdetail.Rows[i]["CompanyName"].ToString() && t.Uniti == SRdetail.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault().TaxInclusive;
                        decimal vatamt1 = 0, discamt = 0;
                        decimal amount = Convert.ToDecimal(SRdetail.Rows[i]["Amount"].ToString());
                        discamt = amount * Convert.ToDecimal(SRdetail.Rows[i]["Discount"].ToString()) / 100;
                        if (Convert.ToDecimal(SRdetail.Rows[i]["Vat"].ToString()) == mvat)
                        {
                            vat1 = Convert.ToDecimal(SRdetail.Rows[i]["Vat"].ToString());
                            if (taxtype == "False" || taxtype == null)
                                vatamt1 = (amount - discamt) * vat1 / 100;
                            else
                                vatamt1 = (amount) * vat1 / 100;
                            vat1amt += vatamt1;
                        }
                        else if (Convert.ToDecimal(SRdetail.Rows[i]["Vat"].ToString()) != mvat && Convert.ToDecimal(SRdetail.Rows[i]["Vat"].ToString()) != 0)
                        {
                            vat2 = Convert.ToDecimal(SRdetail.Rows[i]["Vat"].ToString());
                            if (taxtype == "False" || taxtype == null)
                                vatamt1 = (amount - discamt) * vat2 / 100;
                            else
                                vatamt1 = (amount) * vat2 / 100;
                            vat2amt += vatamt1;
                        }
                    }
                }

                da = new SqlDataAdapter("Select * from GoodReturnDetail d inner join GoodReturnMaster m on m.Id=d.GoodReturnNo Where d.GoodReturnNo=" + Sreturnid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "GoodReturnDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                salerrpt = new SaleReturnCrystalReport();
                salerrpt.SetDataSource(ds);
                salerrpt.SetParameterValue("mdvat", Convert.ToString(Math.Round(mdvat, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));

                SettingRepository SettingRepo = new SettingRepository();
                AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                SettingData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (SettingData != null)
                {
                    if (!string.IsNullOrEmpty(SettingData.PrinterName))
                    {
                        salerrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A4");
                        salerrpt.PrintToPrinter(1, true, 1, 5);
                    }
                }
                SettingRepo.Dispose();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
                master.Dispose();
                SRdetail.Dispose();
            }
            catch (Exception)
            { }
        }

        private void showMarathiBill(string billno, string input = "", string copyType = "")
        {
            try
            {
                int billId = 0, custvalue = 0;
                decimal gstpercent = 0, discountAmt = 0, netAmt = 0, Mrate = 0, custremamt = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0, billcurrentAmt = 0, billRemaining = 0, MRP = 0, TotalMRP = 0, Totalrate = 0, TotalSavings = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", billType = "", MarathiUnit = "", prtbarcode = "", colorname = "", Msize = "0";
                DateTime billdate = DateTime.Today;
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
                TotCgstAmt = 0;
                TotIgstAmt = 0;
                DataTable dtRecord;
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                SettingRepository SettingRepo = new SettingRepository();
                var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                SqlDataAdapter da;
                DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges from CustomerBillMaster Where BillNo='" + billno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
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
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == billno).FirstOrDefault();
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
                dtRecord.Rows.Add(new object[8] { billno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                //                                           0            1         2        3        4            		5          6          7            8          9          10           11        12         13      			    14        		       15
                DataTable detail = db.GetTable("select d.ProductId,d.CompanyName,d.Unit,d.Amount,d.Discount,isnull(d.Vat,0) as Vat,d.sgstpercent,d.sgstamt,d.cgstpercent,d.cgstamt,d.igstpercent,d.igstamt,d.Quantity,d.SaleUnit,isnull(d.SaleRate,0) as SaleRate,isnull(d.Rate,0) as Rate,d.Size,d.ColorName,d.Barcode from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and  m.CompId='" + CommonMethod.CompId + "' and  m.TransactionYear='" + CommonMethod.TransactionYear + "'  Order By ProductId");
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
                            if (settingdata.ShowMfgComp == "True")
                                MProduct = MProduct + ", " + proddata.ManufactureCompany;
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
                            if (input == "8")
                                Mrate = Convert.ToDecimal(detail.Rows[i]["Rate"]);
                            else
                            {
                                if (Convert.ToDecimal(detail.Rows[i]["sgstpercent"].ToString()) != 0)
                                    Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString());
                                else if (Convert.ToDecimal(detail.Rows[i]["igstpercent"].ToString()) != 0)
                                    Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString());
                            }
                        }
                        else
                            Mrate = Convert.ToDecimal(detail.Rows[i]["Rate"]);
                        if (MRP != 0)
                        {
                            TotalMRP = TotalMRP + (MRP * MQty);
                            Totalrate = Totalrate + (Mrate * MQty);
                            TotalSavings = TotalMRP - Totalrate;
                        }
                        if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                            Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
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
                        if (detail.Rows[i]["Barcode"].ToString() == null || detail.Rows[i]["Barcode"].ToString() == "")
                            prtbarcode = "";
                        else
                            prtbarcode = Convert.ToString(detail.Rows[i]["Barcode"].ToString());
                        if (detail.Rows[i]["ColorName"].ToString() == null || detail.Rows[i]["ColorName"].ToString() == "")
                            colorname = "";
                        else
                            colorname = Convert.ToString(detail.Rows[i]["ColorName"].ToString());
                        if (detail.Rows[i]["Size"].ToString() == null || detail.Rows[i]["Size"].ToString() == "")
                            Msize = "";
                        else
                            Msize = detail.Rows[i]["Size"].ToString();
                        int count = i + 1;
                        dtRecord = dsbill.Tables["BillDetail"];
                        dtRecord.Rows.Add(new object[12] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount, MRP, prtbarcode, colorname, Msize, 0 });
                    }
                }

                DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress,VatNo from CompanyInformation where CompanyId=" + CommonMethod.CompId);
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
                    gstinno = Convert.ToString(dtComp.Rows[0]["VatNo"].ToString());
                    dtRecord = dsbill.Tables["CompanyInfo"];
                    dtRecord.Rows.Add(new object[2] { compname, compadd });
                }

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation Order By ProductName", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " Order By ProductName", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "ProductInformation");

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(dsbill, "Settings");
                string instText1 = "", instText2 = "", instText3 = "", startingText = "";
                DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                if (dtinstruct.Rows.Count > 0)
                {
                    instText1 = dtinstruct.Rows[0]["InstructionText1"].ToString();
                    instText2 = dtinstruct.Rows[0]["InstructionText2"].ToString();
                    instText3 = dtinstruct.Rows[0]["InstructionText3"].ToString();
                    startingText = dtinstruct.Rows[0]["StartigText1"].ToString();
                }
                if (settingdata.TaxInvoicePrintOn == "Both")
                {
                    if (input == "4")
                    {
                        salereport = new CrptCustomerBillMarathi();
                        goto M;
                    }
                    else if (input == "5")
                    {
                        custbillmarathiA6 = new CustomerbillMarathi_A6();
                        goto MA6;
                    }
                    else
                    {
                        custA6rrpt = new CustomerBillA61();
                        goto A6;
                    }
                }
                else if (settingdata.TaxInvoicePrintOn == "Marathi")
                {
                    salereport = new CrptCustomerBillMarathi();
                    goto M;
                }
                else if (settingdata.TaxInvoicePrintOn == "MarathiA6")
                {
                    custbillmarathiA6 = new CustomerbillMarathi_A6();
                    goto MA6;
                }
                else
                {
                    custA6rrpt = new CustomerBillA61();
                    goto A6;
                }
            M:
                {
                    salereport.SetDataSource(dsbill);
                    salereport.SetParameterValue("instruction1", instText1);
                    salereport.SetParameterValue("instruction2", instText2);
                    salereport.SetParameterValue("custremamt", custremamt);
                    salereport.SetParameterValue("BillType", billType);
                    salereport.SetParameterValue("billRemaining", billRemaining);
                    crystalReportViewer1.ReportSource = salereport;
                    crystalReportViewer1.Refresh();
                    crystalReportViewer1.ParameterFieldInfo.Clear();
                    goto MA;
                }
            MA6:
                {
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
                    goto MA;
                }
            A6:
                {
                    custA6rrpt.SetDataSource(dsbill);
                    custA6rrpt.SetParameterValue("DiscAmt", TotalSavings);
                    custA6rrpt.SetParameterValue("Flag", "Customer Bill");
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
                   
                    goto MA;
                }
            MA:
                { }
            }
            catch (Exception)
            { }
        }


       
        private void showMarathiBill(List<int> billlist, string input)
        {
            try
            {
                DsCustBillMarathi dsbill = new DsCustBillMarathi();
                string instText1 = "", instText2 = "", billType = "", billno = "", MarathiUnit = "";
                decimal custremamt = 0, billremaining = 0, TotalSavings = 0, MRP = 0, TotalMRP = 0, Totalrate = 0;

                for (int q = 0; q < billlist.Count; q++)
                {
                    int billId = 0, custvalue = 0;
                    decimal gstpercent = 0, discountAmt = 0, netAmt = 0, Mrate = 0, hamalicharges = 0, transportcharges = 0, MQty = 0, Mdiscount = 0, billRemaining = 0;
                    string custname = "", custadd = "", MProduct = "", MUnit = "";
                    DateTime billdate = DateTime.Today;
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
                    TotCgstAmt = 0;
                    TotIgstAmt = 0;
                    DataTable dtRecord;

                    SettingRepository SettingRepo = new SettingRepository();
                    var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    SqlDataAdapter da;



                    DataTable dt = db.GetTable("Select Id,CustomerId,Billdate,TotalAmount,Discount,RemainingAmount,TransportCharges,HamaliCharges,BillNo from CustomerBillMaster Where Id='" + billlist[q] + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        billno = Convert.ToString(dt.Rows[0]["BillNo"].ToString());
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
                        netAmt = Convert.ToDecimal(a - discountAmt);
                        if (dt.Rows[0]["TransportCharges"].ToString() != "" && dt.Rows[0]["TransportCharges"].ToString() != null)
                            transportcharges = Convert.ToDecimal(dt.Rows[0]["TransportCharges"].ToString());
                        else
                            transportcharges = 0;
                        if (dt.Rows[0]["HamaliCharges"].ToString() != "" && dt.Rows[0]["HamaliCharges"].ToString() != null)
                            hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());
                        else
                            hamalicharges = 0;
                        billremaining = Convert.ToDecimal(dt.Rows[0]["RemainingAmount"].ToString());
                        if (Convert.ToDecimal(dt.Rows[0]["RemainingAmount"]) == 0)
                            billType = "रोख बिल";
                        else
                            billType = "उधार बिल";
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
                        remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == custvalue && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.BillId == billno).FirstOrDefault();
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
                    dtRecord.Rows.Add(new object[8] { billno, custname, custadd, billdate, 0, hamalicharges, transportcharges, 0 });
                    DataTable detail = db.GetTable("Select d.* from CustomerBillDetail d join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' Order By ProductId");
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
                                if (settingdata.TaxInvoicePrintOn == "A6" || input == "8")
                                {
                                    MProduct = proddata.ProductName;
                                    MUnit = unitdata.UnitName;
                                }
                                else
                                {
                                    if (proddata.MarathiName != "" && proddata.MarathiName != null)
                                        MProduct = proddata.MarathiName;
                                    else
                                        MProduct = proddata.ProductName;
                                    if (unitdata != null)
                                        MUnit = unitdata.UnitMarathi;
                                    unitrepo.Dispose();
                                }
                                if (proddata.AlternateUnit == null)
                                {
                                    MQty = Convert.ToDecimal(detail.Rows[i]["Quantity"]);
                                    if (MarathiUnit == "")
                                        MUnit = detail.Rows[i]["Unit"].ToString();
                                    MRP = Convert.ToDecimal(proddata.Rate);
                                }
                                else
                                {
                                    if (detail.Rows[i]["SaleUnit"].ToString() == proddata.AlternateUnit)
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
                                MRP = Convert.ToDecimal(proddata.Rate);
                            }
                            ProductRepo.Dispose();

                            Mrate = Convert.ToDecimal(detail.Rows[i]["SaleRate"]);
                            if (!string.IsNullOrEmpty(detail.Rows[i]["Discount"].ToString()))
                                Mdiscount = Convert.ToDecimal(detail.Rows[i]["Discount"]);
                            if (MRP != 0)
                            {
                                TotalMRP = TotalMRP + (MRP * MQty);
                                Totalrate = Totalrate + (Mrate * MQty);

                                TotalSavings = TotalMRP - Totalrate;
                            }
                            gstpercent = Convert.ToDecimal(detail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(detail.Rows[i]["CGstPercent"].ToString());
                            if (gstpercent == 5)
                            {
                                Cgst5amt = Cgst5amt + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            if (gstpercent == 12)
                            {
                                Cgst12amt = Cgst12amt + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            if (gstpercent == 18)
                            {
                                Cgst18amt = Cgst18amt + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            if (gstpercent == 28)
                            {
                                Cgst28amt = Cgst28amt + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            //-- IGst
                            if (Convert.ToDecimal(detail.Rows[i]["IGstPercent"].ToString()) == 5)
                                Igst5amt = Igst5amt + Convert.ToDecimal(detail.Rows[i]["IGstAmt"].ToString());
                            if (Convert.ToDecimal(detail.Rows[i]["IGstPercent"].ToString()) == 12)
                                Igst12amt = Igst12amt + Convert.ToDecimal(detail.Rows[i]["IGstAmt"].ToString());
                            if (Convert.ToDecimal(detail.Rows[i]["IGstPercent"].ToString()) == 18)
                                Igst18amt = Igst18amt + Convert.ToDecimal(detail.Rows[i]["IGstAmt"].ToString());
                            if (Convert.ToDecimal(detail.Rows[i]["IGstPercent"].ToString()) == 28)
                                Igst28amt = Igst28amt + Convert.ToDecimal(detail.Rows[i]["IGstAmt"].ToString());
                            TotCgstAmt = Cgst5amt + Cgst12amt + Cgst18amt + Cgst28amt;
                            TotIgstAmt = Igst5amt + Igst12amt + Igst18amt + Igst28amt;
                            int count = i + 1;
                            dtRecord = dsbill.Tables["BillDetail"];
                            dtRecord.Rows.Add(new object[7] { count, MProduct, MQty, MUnit, Mrate, 0, Mdiscount });
                        }
                    }

                    DataTable dtComp = db.Gettable("Select CompNameMarathi,CompAddMarathi,CompanyName,CompanyAddress,VatNo from CompanyInformation where CompanyId=" + CommonMethod.CompId);
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
                        gstinno = Convert.ToString(dtComp.Rows[0]["VatNo"].ToString());
                        dtRecord = dsbill.Tables["CompanyInfo"];
                        dtRecord.Rows.Add(new object[2] { compname, compadd });
                    }

                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation Order By ProductName", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " Order By ProductName", db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dsbill, "ProductInformation");

                    da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(dsbill, "Settings");

                    string instText3 = "", startingText = "";
                    DataTable dtinstruct = db.Gettable("Select * from BillInstruction where CompId='" + CommonMethod.CompId + "'");
                    if (dtinstruct.Rows.Count > 0)
                    {
                        instText1 = dtinstruct.Rows[0]["InstructionText1"].ToString();
                        instText2 = dtinstruct.Rows[0]["InstructionText2"].ToString();
                        instText3 = dtinstruct.Rows[0]["InstructionText3"].ToString();
                        startingText = dtinstruct.Rows[0]["StartigText1"].ToString();
                    }



                    if (settingdata.TaxInvoicePrintOn == "Both")
                    {
                        if (input == "4")
                        {
                            salereport = new CrptCustomerBillMarathi();
                            goto M;
                        }
                        else if (input == "5")
                        {
                            custbillmarathiA6 = new CustomerbillMarathi_A6();
                            goto MA6;
                        }
                        else
                        {
                            custA6rrpt = new CustomerBillA61();
                            goto A6;
                        }
                    }
                    else if (settingdata.TaxInvoicePrintOn == "Marathi")
                    {
                        salereport = new CrptCustomerBillMarathi();
                        goto M;
                    }
                    else if (settingdata.TaxInvoicePrintOn == "MarathiA6")
                    {
                        custbillmarathiA6 = new CustomerbillMarathi_A6();
                        goto MA6;
                    }
                    else
                    {
                        custA6rrpt = new CustomerBillA61();
                        goto A6;
                    }
                M:
                    {
                        salereport.SetDataSource(dsbill);

                        salereport.SetParameterValue("instruction1", instText1);
                        salereport.SetParameterValue("instruction2", instText2);
                        salereport.SetParameterValue("custremamt", custremamt);
                        salereport.SetParameterValue("BillType", billType);
                        salereport.SetParameterValue("billRemaining", billRemaining);

                        crystalReportViewer1.ReportSource = salereport;
                        crystalReportViewer1.Refresh();
                        crystalReportViewer1.ParameterFieldInfo.Clear();
                        crystalReportViewer1.Visible = false;
                        SettingRepository SettingRepo1 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                salereport.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                                salereport.PrintToPrinter(1, true, 1, 5);
                            }
                        }
                        goto MA;
                    }
                MA6:
                    {
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
                        SettingRepository SettingRepo2 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo2.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                custbillmarathiA6.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A5");
                                custbillmarathiA6.PrintToPrinter(1, true, 1, 5);
                            }
                        }
                        goto MA;
                    }
                A6:
                    {
                        //custA6rrpt.SetDataSource(dsbill);
                        //custA6rrpt.SetParameterValue("DiscAmt", discountAmt);
                        //custA6rrpt.SetParameterValue("Flag", "Customer Bill");
                        //custA6rrpt.SetParameterValue("discounttotal", discounttotal);
                        //custA6rrpt.SetParameterValue("instruction1", instText1);
                        //custA6rrpt.SetParameterValue("instruction2", instText2);
                        //custA6rrpt.SetParameterValue("Instruction3", instText3);
                        //custA6rrpt.SetParameterValue("StartingText", startingText);
                        //custA6rrpt.SetParameterValue("TotCgstAmt", TotCgstAmt);
                        //custA6rrpt.SetParameterValue("TotIgstAmt", TotIgstAmt);
                        //custA6rrpt.SetParameterValue("discpercent", discpercent);
                        //custA6rrpt.SetParameterValue("gstin", gstinno);
                        //crystalReportViewer1.ReportSource = custA6rrpt;
                        //crystalReportViewer1.Refresh();
                        //crystalReportViewer1.ParameterFieldInfo.Clear();
                        custA6rrpt.SetDataSource(dsbill);
                        custA6rrpt.SetParameterValue("DiscAmt", TotalSavings);
                        custA6rrpt.SetParameterValue("Flag", "Customer Bill");
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
                        SettingRepository SettingRepo2 = new SettingRepository();
                        AIOInventorySystem.Data.Model.Setting SettingData = new Data.Model.Setting();
                        SettingData = SettingRepo2.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SettingData != null)
                        {
                            if (!string.IsNullOrEmpty(SettingData.PrinterName))
                            {
                                custA6rrpt.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)GetPaperSize(SettingData.PrinterName, "A6");
                                custA6rrpt.PrintToPrinter(1, true, 1, 5);
                            }
                        }
                        goto MA;
                    }
                MA:
                    { }
                }
            }
            catch (Exception)
            { }
        }

        private void showSmallBill_print(string billno, string input)
        {
            try
            {
                int rowcount = 0;
                string custname = "";
                SettingRepository settingrepo = new SettingRepository();
                Setting settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                DataTable dt = db.GetTable("Select * from CustomerBillMaster Where BillNo='" + billno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    custid = Convert.ToString(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    decimal e = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountamt = Convert.ToDecimal((a * e) / 100);
                    string customerGstin = "", CustContactNo = "";
                    CustomerRepository custrepo = new CustomerRepository();
                    CustomerInformation custdata = custrepo.GetAll().Where(t => t.CustomerID == custvalue).FirstOrDefault();
                    if (custdata != null)
                    {
                        customerGstin = custdata.BuyerTinNo;
                        CustContactNo = custdata.Mobileno;
                        if (custdata.CustNameMarathi == null || custdata.CustNameMarathi == "")
                            custname = custdata.CustomerName;
                        else
                            custname = custdata.CustNameMarathi;
                    }
                    custrepo.Dispose();
                    string companyName = "", CompAdd = "";
                    CompanyInformationRepository comprepo = new CompanyInformationRepository();
                    CompanyInformation compdata = comprepo.GetAll().Where(t => t.CompanyId == Convert.ToInt32(dt.Rows[0][23].ToString())).FirstOrDefault();
                    if (compdata != null)
                    {
                        if (!string.IsNullOrEmpty(compdata.CompNameMarathi))
                            companyName = compdata.CompNameMarathi;
                        else
                            companyName = compdata.CompanyName;
                        if (!string.IsNullOrEmpty(compdata.CompAddMarathi))
                            CompAdd = compdata.CompAddMarathi;
                        else
                            CompAdd = compdata.CompanyAddress;
                        customerGstin = compdata.VatNo;
                        CustContactNo = compdata.ContactNo;
                    }
                    comprepo.Dispose();
                    string bill_instruct = "";
                    BillInstructionRepository billInstrepo = new BillInstructionRepository();
                    BillInstruction billInstData = billInstrepo.GetAll().FirstOrDefault();
                    if (billInstData != null)
                        bill_instruct = billInstData.InstructionText1;
                    dataBill = dsBill.Tables["BillMaster"];
                    dataBill.Rows.Add(new object[10] { billno, Convert.ToDateTime(dt.Rows[0]["Billdate"].ToString()), custname, Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString()), 0, customerGstin, CustContactNo, bill_instruct, companyName, CompAdd });
                }

                decimal MrpRate = 0;
                DataTable detail = db.GetTable("Select * from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + custid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' Order By ProductId");
                if (detail.Rows.Count > 0)
                {
                    string prodinMarathi = "";
                    for (int i = 0; i < detail.Rows.Count; i++)
                    {
                        rowcount++;
                        ProductRepository ProductRepo = new ProductRepository();
                        ProductInformation proddata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString()).FirstOrDefault();
                        else
                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();

                        if (proddata != null)
                        {
                            MrpRate = Convert.ToDecimal(proddata.Rate);
                            if (!string.IsNullOrEmpty(proddata.MarathiName))
                                prodinMarathi = proddata.MarathiName;
                            else
                                prodinMarathi = detail.Rows[i]["ProductId"].ToString();
                        }
                        ProductRepo.Dispose();

                        decimal discamt = 0, BillQty = 0, BillMrp = 0, BillRate = 0, BillAmount = 0, CGSTRate = 0, SGSTRate = 0, CGSTAmt = 0, SGSTAmt = 0;
                        decimal amount = Convert.ToDecimal(detail.Rows[i]["Amount"].ToString());
                        discamt = amount * Convert.ToDecimal(detail.Rows[i]["Discount"].ToString()) / 100;

                        BillQty = Convert.ToDecimal(detail.Rows[i]["Quantity"].ToString());
                        BillMrp = MrpRate;
                        BillRate = Convert.ToDecimal(detail.Rows[i]["SaleRate"].ToString());//Convert.ToDecimal(detail.Rows[i]["Rate"].ToString());
                        BillAmount = Convert.ToDecimal(detail.Rows[i]["TotalAmount"].ToString());

                        SGSTRate = Convert.ToDecimal(detail.Rows[i]["SGstPercent"].ToString());
                        SGSTAmt = Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());

                        CGSTRate = Convert.ToDecimal(detail.Rows[i]["CGstPercent"].ToString());
                        CGSTAmt = Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString());

                        dataBill = dsBill.Tables["BillDetail"];
                        dataBill.Rows.Add(new object[10] { billno, prodinMarathi, BillQty, BillRate, BillAmount, SGSTAmt, CGSTAmt, MrpRate, SGSTRate, CGSTRate });
                    }
                }
                PrintDialog printDialog = new PrintDialog();
                PrintDocument printdoc = new PrintDocument();
                printDialog.Document = printdoc;
                if (settingdata != null)
                    printdoc.PrinterSettings.PrinterName = settingdata.PrinterName;
                printdoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("CustomPaper", 312, 1000);
                printdoc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printdoc.Print();
            }
            catch (Exception)
            { }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                string MarathiOrEnglish = "", FontName = "Utsaah";
                int FontSize = 9;
                Font rptfont = new Font("Utsaah", 9);
                ThermalBillSettingsRepository thermalRepo = new ThermalBillSettingsRepository();
                ThermalBillSetting tdata = thermalRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (tdata != null)
                {
                    MarathiOrEnglish = tdata.PrintOnMarathiOrEnglish;
                    FontName = tdata.FontFamily;
                    FontSize = Convert.ToInt32(tdata.FontSize);
                }
                float docwidth;
                decimal TotalAmount = 0, TotQty = 0, TotSave = 0;
                Graphics gra_phic = e.Graphics;
                rptfont = new Font(FontName, FontSize);
                //System.OperatingSystem osInfo = System.Environment.OSVersion;
                //string osversion = System.Environment.OSVersion.Version.Major.ToString() + "." + System.Environment.OSVersion.Version.Minor.ToString();
                //if (Convert.ToDecimal(osversion) >= 10)
                //    rptfont = new Font("Utsaah", 9);
                //else if (Convert.ToDecimal(osversion) > 6 && Convert.ToDecimal(osversion) < 10)
                //    rptfont = new Font("Utsaah", 11);
                float fontHeight = rptfont.GetHeight();
                docwidth = e.PageBounds.Width;
                float halfwidth = docwidth / 2 - 20;
                float thriforthwidth = docwidth / 3 - 20;
                float XPos = (docwidth / 2) - 20; // pagemargin reduced
                int startY = 5, offset = 5, startX = 12;
                int intxpointcol1 = 0, intxpointcol2 = 0, intxpointcol3 = 0, intxpointcol4 = 0, intxpointcol5 = 0, totcol = 0;
                decimal actualdocwidth = Convert.ToDecimal(docwidth) - 20;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;

                // Header
                try
                {
                    for (int i = 0; i <= dsBill.Tables["BillMaster"].Rows.Count - 1; i++)
                    {
                        //if (Convert.ToDecimal(osversion) > 6 && Convert.ToDecimal(osversion) < 10)
                        //    gra_phic.DrawString(dsBill.Tables["BillMaster"].Rows[i]["CompName"].ToString(), new Font("Utsaah", 18, FontStyle.Bold), new SolidBrush(Color.Black), XPos, startY, sf);
                        //else
                        gra_phic.DrawString(dsBill.Tables["BillMaster"].Rows[i]["CompName"].ToString(), new Font(FontName, FontSize + 5, FontStyle.Bold), new SolidBrush(Color.Black), XPos + 5, startY, sf);
                        offset = offset + (int)fontHeight + 3;
                        string[] addStr = new string[] { "" };
                        float lengthstr = (float)dsBill.Tables["BillMaster"].Rows[i]["CompAdd"].ToString().Length;
                        if (lengthstr > (XPos / 3))
                        {
                            addStr = dsBill.Tables["BillMaster"].Rows[i]["CompAdd"].ToString().Split(',');
                            for (int jj = 0; jj < addStr.Length; jj++)
                            {
                                //if (Convert.ToDecimal(osversion) > 6 && Convert.ToDecimal(osversion) < 10)
                                //    gra_phic.DrawString(addStr[jj], new Font("Utsaah", 10), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                                //else
                                gra_phic.DrawString(addStr[jj], new Font(FontName, FontSize - 1), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                                offset = offset + (int)fontHeight + 2;
                            }
                        }
                        else
                        {
                            //if (Convert.ToDecimal(osversion) > 6 && Convert.ToDecimal(osversion) < 10)
                            //    gra_phic.DrawString(dsBill.Tables["BillMaster"].Rows[i]["CompAdd"].ToString(), new Font("Utsaah", 10), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                            //else
                            gra_phic.DrawString(dsBill.Tables["BillMaster"].Rows[i]["CompAdd"].ToString(), new Font(FontName, FontSize - 1), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                            offset = offset + (int)fontHeight + 4;
                        }
                        //if (Convert.ToDecimal(osversion) > 6 && Convert.ToDecimal(osversion) < 10)
                        //    gra_phic.DrawString("GSTIN :".PadRight(10) + dsBill.Tables["BillMaster"].Rows[i]["CustGstNo"].ToString(), new Font("Utsaah", 10), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                        //else
                        gra_phic.DrawString("GSTIN :".PadRight(10) + dsBill.Tables["BillMaster"].Rows[i]["CustGstNo"].ToString(), new Font(FontName, FontSize - 1), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                        offset = offset + (int)fontHeight + 1;
                        string contactnodesc = "";
                        if (MarathiOrEnglish == "E")
                            contactnodesc = "Mob No.:";
                        else
                            contactnodesc = "फोन नं.:";
                        contactnodesc = contactnodesc.PadRight(10) + dsBill.Tables["BillMaster"].Rows[i]["CustPhNo"].ToString();
                        gra_phic.DrawString(contactnodesc, new Font(rptfont, FontStyle.Regular), new SolidBrush(Color.Black), XPos, startY + offset, sf);
                        offset = offset + (int)fontHeight + 10;
                        string billdesc = new string(' ', 270);
                        if (MarathiOrEnglish == "E")
                            billdesc = "Bill No.:".PadRight(10) + Convert.ToString(dsBill.Tables["BillMaster"].Rows[i]["BillNo"]);
                        else
                            billdesc = "बिल नं.:".PadRight(10) + Convert.ToString(dsBill.Tables["BillMaster"].Rows[i]["BillNo"]);
                        gra_phic.DrawString(billdesc, new Font(rptfont, FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + offset);
                        DateTime billdate = Convert.ToDateTime(dsBill.Tables["BillMaster"].Rows[i]["BillDate"]);
                        string Sbilldate;
                        Sbilldate = billdate.ToShortDateString();
                        string StmBillDate = billdate.ToLongTimeString();
                        if (MarathiOrEnglish == "E")
                            billdesc = "Date: " + Sbilldate + ' ' + StmBillDate;
                        else
                            billdesc = "तारीख: " + Sbilldate + ' ' + StmBillDate;
                        gra_phic.DrawString(billdesc, new Font(rptfont, FontStyle.Regular), new SolidBrush(Color.Black), startX + (Convert.ToInt32(actualdocwidth) / 3), startY + offset);
                        offset = offset + (int)fontHeight + 5;
                        string countdesc = new string(' ', 270);
                        if (MarathiOrEnglish == "E")
                            countdesc = "Name:".PadRight(10) + dsBill.Tables["BillMaster"].Rows[i]["CounterNo"];
                        else
                            countdesc = "नाव:".PadRight(10) + dsBill.Tables["BillMaster"].Rows[i]["CounterNo"];
                        gra_phic.DrawString(countdesc, new Font(rptfont, FontStyle.Regular), new SolidBrush(Color.Black), startX, startY + offset);
                        offset = offset + (int)fontHeight + 5;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error on getting master details");
                }
                offset += 6;
                gra_phic.DrawLine(new Pen(Color.Black), startX - 5, offset + 1, docwidth - 3, offset);
                //Detail
                ThermalBillSettingsRepository ThmRepo = new ThermalBillSettingsRepository();
                ThermalBillSetting ThmData = new ThermalBillSetting();
                ThmData = ThmRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (ThmData != null)
                {
                    if (ThmData.Settings == "D")
                    {
                        int k = 2;
                        intxpointcol1 = 12;
                        int intcol1wdth = Convert.ToInt32(actualdocwidth) / 3;
                        int int23width = intcol1wdth * 2;
                        if (ThmData.CheckH3 == "Y" || string.IsNullOrEmpty(ThmData.CheckH3))
                            k++;
                        if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                            k++;
                        if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                            k++;
                        int intcol2wdth = int23width / (k - 1);
                        totcol = k;
                        intxpointcol2 = intxpointcol1 + intcol1wdth;
                        intxpointcol3 = intxpointcol2 + intcol2wdth;
                        intxpointcol4 = intxpointcol3 + intcol2wdth;
                        intxpointcol5 = intxpointcol4 + intcol2wdth;
                        if (MarathiOrEnglish == "E")
                        {
                            gra_phic.DrawString("Product Name", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            gra_phic.DrawString("Qty", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                        }
                        else
                        {
                            gra_phic.DrawString("वस्तूचे नाव", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            gra_phic.DrawString("नग", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                        }
                        if (ThmData.CheckH3 == "Y" || string.IsNullOrEmpty(ThmData.CheckH3))
                        {
                            if (MarathiOrEnglish == "E")
                                gra_phic.DrawString("M.R.P.", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            else
                                gra_phic.DrawString("एम.आर.पी.", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                            {
                                if (MarathiOrEnglish == "E")
                                    gra_phic.DrawString("Rate", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                else
                                    gra_phic.DrawString("दर", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                {
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Amount", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                                    else
                                        gra_phic.DrawString("रक्कम", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                                }
                            }
                            else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                            {
                                if (MarathiOrEnglish == "E")
                                    gra_phic.DrawString("Amount", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                else
                                    gra_phic.DrawString("रक्कम", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                            }
                        }
                        else if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                        {
                            if (MarathiOrEnglish == "E")
                                gra_phic.DrawString("Rate", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            else
                                gra_phic.DrawString("दर", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                            {
                                if (MarathiOrEnglish == "E")
                                    gra_phic.DrawString("Amount", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                else
                                    gra_phic.DrawString("रक्कम", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                            }
                        }
                        else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                        {
                            if (MarathiOrEnglish == "E")
                                gra_phic.DrawString("Amount", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            else
                                gra_phic.DrawString("रक्कम", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        }
                    }
                    else
                    {
                        int k = 2;
                        if (ThmData.CheckH3 == "Y" || string.IsNullOrEmpty(ThmData.CheckH3))
                            k++;
                        if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                            k++;
                        if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                            k++;
                        intxpointcol1 = 12;
                        int intcol1wdth = Convert.ToInt32(actualdocwidth) / 3;
                        int int23width = intcol1wdth * 2;
                        int intcol2wdth = int23width / (k - 1);
                        totcol = k;
                        intxpointcol2 = intxpointcol1 + intcol1wdth;
                        intxpointcol3 = intxpointcol2 + intcol2wdth;
                        intxpointcol4 = intxpointcol3 + intcol2wdth;
                        intxpointcol5 = intxpointcol4 + intcol2wdth;
                        gra_phic.DrawString(ThmData.Headings1.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        gra_phic.DrawString(ThmData.Headings2.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol2, startY + offset);

                        if (ThmData.CheckH3 == "Y" || string.IsNullOrEmpty(ThmData.CheckH3))
                        {
                            gra_phic.DrawString(ThmData.Headings3.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                            {
                                gra_phic.DrawString(ThmData.Headings4.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                    gra_phic.DrawString(ThmData.Headings5.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                            }
                            else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                gra_phic.DrawString(ThmData.Headings5.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                        }
                        else if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                        {
                            gra_phic.DrawString(ThmData.Headings4.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                gra_phic.DrawString(ThmData.Headings5.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                        }
                        else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                            gra_phic.DrawString(ThmData.Headings5.ToString().Trim(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                    }
                }
                else
                {
                    //5 Column bydefault                    
                    intxpointcol1 = 12;
                    int intcol1wdth = Convert.ToInt32(actualdocwidth) / 3;
                    int int23width = intcol1wdth * 2;
                    int intcol2wdth = int23width / 4;
                    intxpointcol2 = intxpointcol1 + intcol1wdth;
                    intxpointcol3 = intxpointcol2 + intcol2wdth;
                    intxpointcol4 = intxpointcol3 + intcol2wdth;
                    intxpointcol5 = intxpointcol4 + intcol2wdth;
                    totcol = 5;
                    if (MarathiOrEnglish == "E")
                    {
                        gra_phic.DrawString("Product Name", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        gra_phic.DrawString("Qty", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                        gra_phic.DrawString("M.R.P.", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        gra_phic.DrawString("Rate", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                        gra_phic.DrawString("Amount", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                    }
                    else
                    {
                        gra_phic.DrawString("वस्तूचे नाव", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        gra_phic.DrawString("नग", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                        gra_phic.DrawString("एम.आर.पी.", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        gra_phic.DrawString("दर", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                        gra_phic.DrawString("रक्कम", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                    }
                }
                offset += 20;
                gra_phic.DrawLine(new Pen(Color.Black), startX - 5, offset, docwidth - 5, offset);

                string productdesc = new string(' ', 50);
                string pQty = new string(' ', 10);
                string pMrp = new string(' ', 60);
                string pRate = new string(' ', 60);
                string pAmount = new string(' ', 90);
                //decimal sgst25 = 0, cgst25 = 0, sgst6 = 0, cgst6 = 0, sgst9 = 0, cgst9 = 0, sgst14 = 0, cgst14 = 0;
                decimal psgst25amt = 0, pcgst25amt = 0, psgst6amt = 0, pcgst6amt = 0, psgst9amt = 0, pcgst9amt = 0, psgst14amt = 0, pcgst14amt = 0;
                decimal ptaxableamt5 = 0, ptaxableamt12 = 0, ptaxableamt18 = 0, ptaxableamt28 = 0;
                TotalAmount = 0;
                offset += 2;
                try
                {
                    TotSave = 0;
                    int stringwidth = 0;
                    for (int j = 0; j < dsBill.Tables["BillDetail"].Rows.Count; j++)
                    {
                        startX = 15;
                        string[] prod_desc = new string[] { "" };
                        productdesc = dsBill.Tables["BillDetail"].Rows[j]["Particular"].ToString();
                        productdesc = productdesc.ToUpper();
                        stringwidth = 0;
                        SizeF stringSize = new SizeF();
                        stringSize = gra_phic.MeasureString(productdesc, rptfont, stringwidth);
                        float strwdth = stringSize.Width;

                        float desclength = productdesc.Length;
                        if (strwdth > 89)  //static width 
                        {
                            string extractStr = "";
                            if (desclength >= 15)
                            {
                                SizeF stringSize1 = new SizeF();
                                extractStr = productdesc.Substring(0, 15);
                                stringSize1 = gra_phic.MeasureString(productdesc, rptfont, stringwidth);
                                float strwdth_N = stringSize1.Width;
                                if (strwdth_N > 89)
                                    extractStr = productdesc.Substring(0, 13);
                            }
                            else
                                extractStr = productdesc;
                            productdesc = extractStr.Trim().PadRight(2);
                        }
                        else
                            productdesc = productdesc.Trim().PadRight(2);
                        pQty = dsBill.Tables["BillDetail"].Rows[j]["Quantity"].ToString();
                        pMrp = dsBill.Tables["BillDetail"].Rows[j]["Mrp"].ToString();
                        pRate = dsBill.Tables["BillDetail"].Rows[j]["Rate"].ToString();
                        pAmount = dsBill.Tables["BillDetail"].Rows[j]["Amount"].ToString();
                        //2.5% GST Calculation
                        if (Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTRate"].ToString()) == Convert.ToDecimal(2.50))
                        {
                            psgst25amt = psgst25amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTAmt"].ToString());
                            pcgst25amt = pcgst25amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["CGSTAmt"].ToString());
                            ptaxableamt5 = ptaxableamt5 + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Amount"].ToString());
                        }
                        //6% GST Calculation
                        if (Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTRate"].ToString()) == Convert.ToDecimal(6))
                        {
                            psgst6amt = psgst6amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTAmt"].ToString());
                            pcgst6amt = pcgst6amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["CGSTAmt"].ToString());
                            ptaxableamt12 = ptaxableamt12 + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Amount"].ToString());
                        }
                        //9% GST Calculation
                        if (Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTRate"].ToString()) == Convert.ToDecimal(9))
                        {
                            psgst9amt = psgst9amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTAmt"].ToString());
                            pcgst9amt = pcgst9amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["CGSTAmt"].ToString());
                            ptaxableamt18 = ptaxableamt18 + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Amount"].ToString());
                        }
                        //14% GST Calculation
                        if (Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTRate"].ToString()) == Convert.ToDecimal(14))
                        {
                            psgst14amt = psgst14amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["SGSTAmt"].ToString());
                            pcgst14amt = pcgst14amt + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["CGSTAmt"].ToString());
                            ptaxableamt28 = ptaxableamt28 + Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Amount"].ToString());
                        }

                        gra_phic.DrawString(productdesc, rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        gra_phic.DrawString(pQty, rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                        if (ThmData != null)
                        {
                            if (ThmData.CheckH3 == "Y" || string.IsNullOrEmpty(ThmData.CheckH3))
                            {
                                gra_phic.DrawString(pMrp, rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                                {
                                    gra_phic.DrawString(pRate, rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                        gra_phic.DrawString(pAmount, rptfont, new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                                }
                                else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                    gra_phic.DrawString(pQty, rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                            }
                            else if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4))
                            {
                                gra_phic.DrawString(pRate, rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                    gra_phic.DrawString(pAmount, rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                            }
                            else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                gra_phic.DrawString(pAmount, rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        }
                        else
                        {
                            gra_phic.DrawString(productdesc, rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            gra_phic.DrawString(pQty, rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                            gra_phic.DrawString(pMrp, rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                            gra_phic.DrawString(pRate, rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                            gra_phic.DrawString(pAmount, rptfont, new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                        }
                        offset = offset + (int)fontHeight + 1;
                        if (Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Mrp"]) != 0)
                            TotSave += Math.Round((decimal)((Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Mrp"]) - Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Rate"])) * Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Quantity"])), 2, MidpointRounding.AwayFromZero);
                        TotQty += Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Quantity"]);
                        TotalAmount += Convert.ToDecimal(dsBill.Tables["BillDetail"].Rows[j]["Amount"]);
                    }
                    TotalAmount = Math.Round((decimal)(TotalAmount), 0, MidpointRounding.AwayFromZero);
                    TotalAmount = Math.Round((decimal)(TotalAmount), 2, MidpointRounding.AwayFromZero);
                    startX = 12;
                    gra_phic.DrawLine(new Pen(Color.Black), startX - 5, offset + 7, docwidth - 3, offset + 7);
                    offset += 10;
                    //Footer
                    if (MarathiOrEnglish == "E")
                        gra_phic.DrawString("Total:", rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                    else
                        gra_phic.DrawString("एकुण:", rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                    gra_phic.DrawString(TotQty.ToString(), rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                    if (totcol == 5)
                        gra_phic.DrawString(TotalAmount.ToString(), rptfont, new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                    else if (totcol == 4)
                        gra_phic.DrawString(TotalAmount.ToString(), rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                    else if (totcol == 3)
                        gra_phic.DrawString(TotalAmount.ToString(), rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                    else if (totcol == 2)
                        gra_phic.DrawString(TotalAmount.ToString(), rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                    offset += 15;
                    decimal discountamt1 = Math.Round((decimal)discountamt, 2, MidpointRounding.AwayFromZero);
                    startX = 12;
                    if (ThmData != null)
                    {
                        if (ThmData.ShowMRP == "Y" && (ThmData.DispalySavings != "N" && !string.IsNullOrEmpty(ThmData.DispalySavings)))
                        {
                            if (MarathiOrEnglish == "E")
                                gra_phic.DrawString("Save In Rs.: " + TotSave.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            else
                                gra_phic.DrawString("बचत रु.: " + TotSave.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            if (ThmData.DisplayDiscount != "N" && !string.IsNullOrEmpty(ThmData.DisplayDiscount))
                            {
                                if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4) && ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                {
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Disc Rs.: ", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    else
                                        gra_phic.DrawString("डिस्का रु.: ", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    gra_phic.DrawString(discountamt1.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                                    offset = offset + (int)fontHeight + 3;
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Net Amt: ", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    else
                                        gra_phic.DrawString("निव्वळ: ", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    gra_phic.DrawString(Convert.ToString(Math.Round((decimal)Convert.ToDecimal(TotalAmount - discountamt1), 2, MidpointRounding.AwayFromZero)) + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol5, startY + offset);
                                }
                                else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                {
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Disc Rs.: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    else
                                        gra_phic.DrawString("डिस्का रु.: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    gra_phic.DrawString(discountamt1.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    offset = offset + (int)fontHeight + 3;
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Net Amt: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    else
                                        gra_phic.DrawString("निव्वळ: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    gra_phic.DrawString(Convert.ToString(Math.Round((decimal)Convert.ToDecimal(TotalAmount - discountamt1), 2, MidpointRounding.AwayFromZero)) + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                }
                            }
                        }
                        else
                        {
                            if (ThmData.DisplayDiscount != "N" && !string.IsNullOrEmpty(ThmData.DisplayDiscount))
                            {
                                if (ThmData.CheckH4 == "Y" || string.IsNullOrEmpty(ThmData.CheckH4) && ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                {
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Disc Rs.: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    else
                                        gra_phic.DrawString("डिस्का रु.: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    gra_phic.DrawString(discountamt1.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                    offset = offset + (int)fontHeight + 3;
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Net Amt: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    else
                                        gra_phic.DrawString("निव्वळ: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    gra_phic.DrawString(Convert.ToString(Math.Round((decimal)Convert.ToDecimal(TotalAmount - discountamt1), 2, MidpointRounding.AwayFromZero)) + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                                }
                                else if (ThmData.CheckH5 == "Y" || string.IsNullOrEmpty(ThmData.CheckH5))
                                {
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Disc Rs.: ", rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                                    else
                                        gra_phic.DrawString("डिस्का रु.: ", rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                                    gra_phic.DrawString(discountamt1.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                    offset = offset + (int)fontHeight + 3;
                                    if (MarathiOrEnglish == "E")
                                        gra_phic.DrawString("Net Amt: ", rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                                    else
                                        gra_phic.DrawString("निव्वळ: ", rptfont, new SolidBrush(Color.Black), intxpointcol2, startY + offset);
                                    gra_phic.DrawString(Convert.ToString(Math.Round((decimal)Convert.ToDecimal(TotalAmount - discountamt1), 2, MidpointRounding.AwayFromZero)) + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                                }
                            }
                        }
                        offset += 10;
                        offset = offset + (int)fontHeight + 2;
                        if (!string.IsNullOrEmpty(Convert.ToString(dsBill.Tables["BillMaster"].Rows[0]["BillInstruction"])))
                        {
                            gra_phic.DrawString(Convert.ToString(dsBill.Tables["BillMaster"].Rows[0]["BillInstruction"]), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            offset = offset + (int)fontHeight + 3;
                        }
                        if (ThmData.GSTDetail == true)
                        {
                            decimal wogstamt5 = 0, wogstamt12 = 0, wogstamt18 = 0, wogstamt28 = 0, pgstamt5 = 0, pgstamt12 = 0, pgstamt18 = 0, pgstamt28 = 0;
                            gra_phic.DrawString("CGST Summary:", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            offset = offset + (int)fontHeight + 3;
                            if (ptaxableamt5 != 0)
                            {
                                wogstamt5 = ptaxableamt5 - psgst25amt - pcgst25amt;
                                pgstamt5 = wogstamt5 * Convert.ToDecimal(2.5) / 100;
                                gra_phic.DrawString("2.5% * " + Convert.ToString(Math.Round(wogstamt5, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt5, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            if (ptaxableamt12 != 0)
                            {
                                wogstamt12 = ptaxableamt12 - psgst6amt - pcgst6amt;
                                pgstamt12 = wogstamt12 * Convert.ToDecimal(6) / 100;
                                gra_phic.DrawString("6% * " + Convert.ToString(Math.Round(wogstamt12, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt12, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            if (ptaxableamt18 != 0)
                            {
                                wogstamt18 = ptaxableamt18 - psgst9amt - pcgst9amt;
                                pgstamt18 = wogstamt18 * Convert.ToDecimal(9) / 100;
                                gra_phic.DrawString("9% * " + Convert.ToString(Math.Round(wogstamt18, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt18, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            if (ptaxableamt28 != 0)
                            {
                                wogstamt28 = ptaxableamt28 - psgst14amt - pcgst14amt;
                                pgstamt28 = wogstamt28 * Convert.ToDecimal(14) / 100;
                                gra_phic.DrawString("14% * " + Convert.ToString(Math.Round(wogstamt28, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt28, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            gra_phic.DrawString("SGST Summary:", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            offset = offset + (int)fontHeight + 3;
                            if (wogstamt5 != 0)
                            {
                                gra_phic.DrawString("2.5% * " + Convert.ToString(Math.Round(wogstamt5, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt5, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            if (wogstamt12 != 0)
                            {
                                gra_phic.DrawString("6% * " + Convert.ToString(Math.Round(wogstamt12, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt12, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            if (wogstamt18 != 0)
                            {
                                gra_phic.DrawString("9% * " + Convert.ToString(Math.Round(wogstamt18, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt18, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                            if (wogstamt28 != 0)
                            {
                                gra_phic.DrawString("14% * " + Convert.ToString(Math.Round(wogstamt28, 2, MidpointRounding.AwayFromZero)) + " = " + Convert.ToString(Math.Round(pgstamt28, 2, MidpointRounding.AwayFromZero)), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                                offset = offset + (int)fontHeight + 3;
                            }
                        }
                        if (!string.IsNullOrEmpty(ThmData.Note))
                            gra_phic.DrawString(ThmData.Note.ToString(), new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        else
                        {
                            if (MarathiOrEnglish == "E")
                                gra_phic.DrawString("Thank You!!".PadRight(30) + "Visit Again", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            else
                                gra_phic.DrawString("धन्यवाद!!".PadRight(30) + " पुन्हा भेटूया ", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        }
                    }
                    else
                    {
                        if (MarathiOrEnglish == "E")
                            gra_phic.DrawString("Save In Rs.: " + TotSave.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        else
                            gra_phic.DrawString("बचत रु.: " + TotSave.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        if (MarathiOrEnglish == "E")
                            gra_phic.DrawString("Disc Rs.: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        else
                            gra_phic.DrawString("डिस्का रु.: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        gra_phic.DrawString(discountamt1.ToString() + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);
                        offset = offset + (int)fontHeight + 3;
                        gra_phic.DrawString("Net Amt: ", rptfont, new SolidBrush(Color.Black), intxpointcol3, startY + offset);
                        gra_phic.DrawString(Convert.ToString(Math.Round((decimal)Convert.ToDecimal(TotalAmount - discountamt1), 2, MidpointRounding.AwayFromZero)) + "/-", rptfont, new SolidBrush(Color.Black), intxpointcol4, startY + offset);

                        offset += 10;
                        offset = offset + (int)fontHeight + 2;
                        if (!string.IsNullOrEmpty(Convert.ToString(dsBill.Tables["BillMaster"].Rows[0]["BillInstruction"])))
                        {
                            gra_phic.DrawString(Convert.ToString(dsBill.Tables["BillMaster"].Rows[0]["BillInstruction"]), rptfont, new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                            offset = offset + (int)fontHeight + 3;
                        }

                        if (MarathiOrEnglish == "E")
                            gra_phic.DrawString("Thank You!!".PadRight(30) + "Visit Again", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                        else
                            gra_phic.DrawString("धन्यवाद!!".PadRight(30) + " पुन्हा भेटूया ", new Font(rptfont, FontStyle.Bold), new SolidBrush(Color.Black), intxpointcol1, startY + offset);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error on getting details");
                }
            }
            catch (Exception)
            { }
        }

        private void showCustomerbillSlab(string billno, string input = "", string copyType = "")
        {
            try
            {
                db.connect();
                int billId = 0, custvalue = 0, detailId = 0;
                decimal TotSgstAmt = 0, discountAmt = 0, netAmt = 0, Mrate = 0, hamalicharges = 0, transportcharges = 0, OthCharges = 0, roundoff = 0, qty = 0, SQFQty = 0, prodamount = 0, totalAmount = 0;
                string custname = "", custadd = "", MProduct = "", MUnit = "", mobileno = "", companyname = "", creditmemo = "", ProdSize = "";
                DateTime billdate = DateTime.Today;
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

                TotCgstAmt = 0;
                TotIgstAmt = 0;
                DataTable dtRecord;
                DSCustomerBill_Slab ds = new DSCustomerBill_Slab();

                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from CustomerBillMaster Where BillNo='" + billno + "'  and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.Fill(ds, "CustomerBillMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    billId = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    billdate = Convert.ToDateTime(dt.Rows[0]["Billdate"]);

                    totalAmount = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    decimal e = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountAmt = Convert.ToDecimal((totalAmount * e) / 100);
                    netAmt = Convert.ToDecimal(totalAmount - discountAmt);
                    if (dt.Rows[0]["HamaliCharges"].ToString() == "" || dt.Rows[0]["HamaliCharges"].ToString() == null)
                        hamalicharges = 0;
                    else
                        hamalicharges = Convert.ToDecimal(dt.Rows[0]["HamaliCharges"].ToString());

                    if (dt.Rows[0]["TransportCharges"].ToString() == "" || dt.Rows[0]["TransportCharges"].ToString() == null)
                        transportcharges = 0;
                    else
                        transportcharges = Convert.ToDecimal(dt.Rows[0]["TransportCharges"].ToString());
                    OthCharges = hamalicharges + transportcharges;
                    roundoff = Convert.ToDecimal(dt.Rows[0]["RoundOff"].ToString());
                    if (Convert.ToDecimal(dt.Rows[0][10].ToString()) == 0)
                        creditmemo = "CASH MEMO";
                    else
                        creditmemo = "CREDIT MEMO";
                }
                DataTable dt1 = db.GetTable("select CustomerName,CustomeAddress,MobileNo from CustomerInformation where CustomerID='" + custvalue + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    custname = Convert.ToString(dt1.Rows[0]["CustomerName"].ToString());
                    custadd = Convert.ToString(dt1.Rows[0]["CustomeAddress"].ToString());
                    mobileno = Convert.ToString(dt1.Rows[0]["MobileNo"].ToString());
                }
                dtRecord = ds.Tables["BillMaster"];
                dtRecord.Rows.Add(new object[4] { billno, billdate, custname, custadd });

                da = new SqlDataAdapter("Select d.* from CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo Where d.BillNo=" + billId + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' Order By ProductId", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerBillDetail");
                DataTable dtdetail = new DataTable();
                da.Fill(dtdetail);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        detailId = Convert.ToInt32(dtdetail.Rows[i]["Id"].ToString());
                        MProduct = Convert.ToString(dtdetail.Rows[i]["ProductId"].ToString());
                        Mrate = Convert.ToDecimal(dtdetail.Rows[i]["Rate"].ToString());
                        MUnit = Convert.ToString(dtdetail.Rows[i]["Unit"].ToString());
                        companyname = Convert.ToString(dtdetail.Rows[i]["CompanyName"].ToString());
                        DataTable dtMUnitInfo = db.GetTable("select * from ManufacturingUnitInfo where BilldetailId='" + detailId + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtMUnitInfo.Rows.Count > 0)
                        {
                            ProdSize = Convert.ToString(dtMUnitInfo.Rows[0]["Size"].ToString());
                            qty = Convert.ToDecimal(dtMUnitInfo.Rows[0]["BillQty"].ToString());
                            SQFQty = Convert.ToDecimal(dtMUnitInfo.Rows[0]["SQF"].ToString());
                            prodamount = Mrate * SQFQty;
                        }
                        else
                        {
                            qty = Convert.ToDecimal(dtdetail.Rows[i]["Quantity"].ToString());
                            ProdSize = "";
                            SQFQty = 0;
                            prodamount = Mrate * qty;
                        }

                        TotSgstAmt = TotSgstAmt + Convert.ToDecimal(dtdetail.Rows[i]["SGstAmt"].ToString());
                        TotCgstAmt = TotCgstAmt + Convert.ToDecimal(dtdetail.Rows[i]["CGstAmt"].ToString());
                        TotIgstAmt = TotIgstAmt + Convert.ToDecimal(dtdetail.Rows[i]["IGstAmt"].ToString());
                        dtRecord = ds.Tables["BillDetail"];
                        dtRecord.Rows.Add(new object[9] { MProduct, ProdSize, qty, SQFQty, Mrate, prodamount, detailId, companyname, MUnit });
                    }
                }

                da = new SqlDataAdapter("Select * from Settings  where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation order by ProductName", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                SettingRepository SettingRepo = new SettingRepository();
                var settingdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                slabRpt = new CustomerBillSlabCRpt();
                slabRpt.SetDataSource(ds);
                slabRpt.SetParameterValue("MobileNo", mobileno);
                slabRpt.SetParameterValue("totalAmount", totalAmount);
                slabRpt.SetParameterValue("roundoff", roundoff);
                slabRpt.SetParameterValue("netamount", netAmt);
                slabRpt.SetParameterValue("credittype", creditmemo);
                slabRpt.SetParameterValue("othercharges", OthCharges);
                slabRpt.SetParameterValue("InscSgst", TotSgstAmt);
                slabRpt.SetParameterValue("InscCgst", TotCgstAmt);
                slabRpt.SetParameterValue("InscIgst", TotIgstAmt);

                crystalReportViewer1.ReportSource = slabRpt;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();
            }
            catch (Exception) { }
        }
    }
}