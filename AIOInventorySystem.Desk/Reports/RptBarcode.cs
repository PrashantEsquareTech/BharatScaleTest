using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using IDAutomation.NetAssembly;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptBarcode : Form
    {
        FontEncoder Encode = new FontEncoder();
        DbClass db = new DbClass();
        BarcodesmallRPT barcoderpt = new BarcodesmallRPT();
        ThermalPrintBarcodeCRPT barcoderpt1 = new ThermalPrintBarcodeCRPT();
        //ThermalBarcodePrintCrystalReport barcoderpt1 = new ThermalBarcodePrintCrystalReport();
        public string ProductNameP = "", Barcode, EncodeBarcode, Prefix = "", GroupName = "", MarathiProductName = "", MfgCompany = "", Unit = "", MRP = "",
            Warranty = "", HSNCode = "", ShopName = "", MarathiShopName = "", PurchaseRate = "", SaleRate = "", property1 = "", property2 = "", property3 = "", size = "";
        decimal qty;

        public RptBarcode()
        {
            InitializeComponent();
        }

        public RptBarcode(decimal ponumber, int rownum, int copynum, int colno, string flag)
        {
            InitializeComponent();
            GetData(ponumber, rownum, copynum, colno, flag);
        }

        public RptBarcode(int PId, decimal ponumber, int rownum, int copynum, int colno, string flag)
        {
            InitializeComponent();
            GetData(PId, ponumber, rownum, copynum, colno, flag);
        }

        public RptBarcode(List<int> plist, decimal ponumber, int rownum, int copynum, int colno, string flag)
        {
            InitializeComponent();
            getdata(plist, ponumber, rownum, copynum, colno, flag);
        }

        public void GetData(decimal ponumber, int Rownum, int copynum, int colno, string flag)
        {
            try
            {
                SqlDataAdapter da;
                DSBarcode ds = new DSBarcode();
                if (flag == "Purchase")
                {
                    //                                          0             1         2       3           4                                                 5          6              7
                    DataTable dt1 = db.GetTable(" Select d.ProductName,d.CompanyName,d.Unit,d.Barcode,d.Quantity,CONVERT(VARCHAR(24),m.PorderDate,103) as 'PurDate',d.SaleRate,d.PurchasePrice,d.Size from PurchaseMaster m inner join PurchaseDetail d on m.Id=d.PorderNo Where d.PorderNo='" + ponumber + "' and d.Barcode!='' and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        ProductNameP = Convert.ToString(dt1.Rows[j]["ProductName"].ToString());
                        MfgCompany = Convert.ToString(dt1.Rows[j]["CompanyName"].ToString());
                        Unit = Convert.ToString(dt1.Rows[j]["Unit"].ToString());
                        Barcode = Convert.ToString(dt1.Rows[j]["Barcode"].ToString());
                        EncodeBarcode = Encode.Code128(Convert.ToString(dt1.Rows[j]["Barcode"].ToString()), 0, false);
                        qty = Convert.ToDecimal(dt1.Rows[j]["Quantity"].ToString());
                        size = Convert.ToString(dt1.Rows[j]["Size"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["SaleRate"].ToString()))
                            SaleRate = Convert.ToString(dt1.Rows[j]["SaleRate"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["PurchasePrice"].ToString()))
                            PurchaseRate = Convert.ToString(dt1.Rows[j]["PurchasePrice"].ToString());
                        ProductRepository Prepo = new ProductRepository();
                        ProductInformation Pinfo = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit).FirstOrDefault();
                        else
                            Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (Pinfo != null)
                        {
                            if (!string.IsNullOrEmpty(Pinfo.Prefix))
                                Prefix = Pinfo.Prefix;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.GroupMasterId)))
                            {
                                GroupMasterRepository grepo = new GroupMasterRepository();
                                GroupMaster gdata = new GroupMaster();
                                if (CommonMethod.commProduct == true)
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId).FirstOrDefault();
                                else
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (gdata != null)
                                    GroupName = gdata.GroupMasterName;
                            }
                            if (!string.IsNullOrEmpty(Pinfo.MarathiName))
                                MarathiProductName = Pinfo.MarathiName;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.Rate)))
                                MRP = Convert.ToString(Pinfo.Rate);
                            if (!string.IsNullOrEmpty(Pinfo.Warranty))
                                Warranty = Pinfo.Warranty;
                            if (!string.IsNullOrEmpty(Pinfo.HSNCode))
                                HSNCode = Pinfo.HSNCode;
                        }
                        CompanyInformationRepository crepo = new CompanyInformationRepository();
                        var cdata = crepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                        if (cdata != null)
                        {
                            if (!string.IsNullOrEmpty(cdata.CompanyName))
                                ShopName = cdata.CompanyName;
                            if (!string.IsNullOrEmpty(cdata.CompNameMarathi))
                                MarathiShopName = cdata.CompNameMarathi;
                        }
                        for (int k = 0; k < copynum; k++)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                DataTable dt;
                                dt = ds.Tables["BarcodeTbl"];
                                dt.Rows.Add(new object[] { EncodeBarcode, Barcode, Prefix, GroupName, ProductNameP, MarathiProductName, MfgCompany, Unit, MRP, Warranty, HSNCode, ShopName, MarathiShopName, PurchaseRate, SaleRate, size });
                            }
                        }
                    }
                }
                else
                {
                    //                                          0         1       2                                               3          4    
                    DataTable dt1 = db.GetTable(" Select d.ProductNo,d.Barcode,d.Qty,CONVERT(VARCHAR(24),m.RepackDate,103) as 'PurDate',d.SaleRate from RepackMaster m inner join RepackDetail d on m.ID=d.RepackNo Where d.RepackNo='" + ponumber + "' and d.Barcode!=''");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        Barcode = Convert.ToString(dt1.Rows[j]["Barcode"].ToString());
                        EncodeBarcode = Encode.Code128(Convert.ToString(dt1.Rows[j]["Barcode"].ToString()), 0, false);
                        qty = Convert.ToDecimal(dt1.Rows[j]["Qty"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["SaleRate"].ToString()))
                            SaleRate = Convert.ToString(dt1.Rows[j]["SaleRate"].ToString());
                        ProductRepository Prepo = new ProductRepository();
                        ProductInformation Pinfo = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            Pinfo = Prepo.GetAll().Where(t => t.ID == Convert.ToInt32(dt1.Rows[j]["ProductNo"].ToString())).FirstOrDefault();
                        else
                            Pinfo = Prepo.GetAll().Where(t => t.ID == Convert.ToInt32(dt1.Rows[j]["ProductNo"].ToString()) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (Pinfo != null)
                        {
                            ProductNameP = Pinfo.ProductName;
                            MfgCompany = Pinfo.ManufactureCompany;
                            Unit = Pinfo.Uniti;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.PurchaseRate)))
                                PurchaseRate = Convert.ToString(Pinfo.PurchaseRate);
                            if (!string.IsNullOrEmpty(Pinfo.Prefix))
                                Prefix = Pinfo.Prefix;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.GroupMasterId)))
                            {
                                GroupMasterRepository grepo = new GroupMasterRepository();
                                GroupMaster gdata = new GroupMaster();
                                if (CommonMethod.commProduct == true)
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId).FirstOrDefault();
                                else
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (gdata != null)
                                    GroupName = gdata.GroupMasterName;
                            }
                            if (!string.IsNullOrEmpty(Pinfo.MarathiName))
                                MarathiProductName = Pinfo.MarathiName;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.Rate)))
                                MRP = Convert.ToString(Pinfo.Rate);
                            if (!string.IsNullOrEmpty(Pinfo.Warranty))
                                Warranty = Pinfo.Warranty;
                            if (!string.IsNullOrEmpty(Pinfo.HSNCode))
                                HSNCode = Pinfo.HSNCode;
                        }
                        CompanyInformationRepository crepo = new CompanyInformationRepository();
                        var cdata = crepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                        if (cdata != null)
                        {
                            if (!string.IsNullOrEmpty(cdata.CompanyName))
                                ShopName = cdata.CompanyName;
                            if (!string.IsNullOrEmpty(cdata.CompNameMarathi))
                                MarathiShopName = cdata.CompNameMarathi;
                        }
                        for (int k = 0; k < copynum; k++)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                DataTable dt;
                                dt = ds.Tables["BarcodeTbl"];
                                dt.Rows.Add(new object[] { EncodeBarcode, Barcode, Prefix, GroupName, ProductNameP, MarathiProductName, MfgCompany, Unit, MRP, Warranty, HSNCode, ShopName, MarathiShopName, PurchaseRate, SaleRate, size });
                            }
                        }
                    }
                }

                string marathi = "";

                da = new SqlDataAdapter("Select * from BarcodeSetting", db.Connection);
                da.Fill(ds, "BarcodeSetting");
                SettingRepository settRepo = new SettingRepository();
                Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.Applyonmarathifont == "Y")
                    marathi = "Y";
                else
                    marathi = "N";
                if (sdata.ShowBarcodePrintOn == "T")
                {
                    barcoderpt1 = new ThermalPrintBarcodeCRPT();
                    barcoderpt1.SetDataSource(ds);
                    barcoderpt1.SetParameterValue("flag", marathi);
                    crystalReportViewer1.ReportSource = barcoderpt1;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    int topmargin = 0;
                    if (sdata.BarcodePrintTopMargin != null)
                        topmargin = Convert.ToInt32(sdata.BarcodePrintTopMargin);
                    else
                    {
                        string input = Microsoft.VisualBasic.Interaction.InputBox("Enter top margin for Barcode", "Page Setup", "" + topmargin + "", 300, 250);
                        sdata.BarcodePrintTopMargin = Convert.ToInt32(input);
                        settRepo.Edit(sdata);
                        settRepo.Save();
                        topmargin = Convert.ToInt32(input);
                    }
                    barcoderpt = new BarcodesmallRPT();
                    int wdth = 300; //left margin
                    if (colno > 1)
                        wdth = (2664 + 300) * (colno - 1);
                    if (Rownum > 1)
                    {
                        int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                        hgt = (hgt + 80) * (Rownum - 1);
                        hgt = hgt + 300;
                        topmargin = topmargin + hgt;
                        barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                        barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, topmargin, 200, 0));
                    }
                    else
                    {
                        int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                        topmargin = topmargin + 300;
                        barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                        barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, topmargin, 200, 0));
                    }
                    barcoderpt.SetDataSource(ds);
                    barcoderpt.SetParameterValue("flag", marathi);
                    crystalReportViewer1.ReportSource = barcoderpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception)
            { }
        }

        public void GetData(int PId, decimal ponumber, int Rownum, int copynum, int colno, string flag)
        {
            try
            {
                SqlDataAdapter da;
                DSBarcode ds = new DSBarcode();

                if (flag == "Purchase")
                {
                    DataTable dt1 = db.GetTable(" Select d.ProductName,d.CompanyName,d.Unit,d.Barcode,d.Quantity,CONVERT(VARCHAR(24),m.PorderDate,103) as 'PurDate', " +
                        "d.SaleRate,d.PurchasePrice,d.Size from PurchaseMaster m inner join PurchaseDetail d on m.Id=d.PorderNo Where d.Id=" + PId + " and " +
                        "d.PorderNo='" + ponumber + "' and d.Barcode!='' and m.CompId='" + CommonMethod.CompId + "' " +
                        "and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        ProductNameP = Convert.ToString(dt1.Rows[j]["ProductName"].ToString());
                        MfgCompany = Convert.ToString(dt1.Rows[j]["CompanyName"].ToString());
                        Unit = Convert.ToString(dt1.Rows[j]["Unit"].ToString());
                        Barcode = Convert.ToString(dt1.Rows[j]["Barcode"].ToString());
                        EncodeBarcode = Encode.Code128(Convert.ToString(dt1.Rows[j]["Barcode"].ToString()), 0, false);
                        size = Convert.ToString(dt1.Rows[j]["Size"].ToString());
                        qty = Convert.ToDecimal(dt1.Rows[j]["Quantity"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["SaleRate"].ToString()))
                            SaleRate = Convert.ToString(dt1.Rows[j]["SaleRate"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["PurchasePrice"].ToString()))
                            PurchaseRate = Convert.ToString(dt1.Rows[j]["PurchasePrice"].ToString());

                        ProductRepository Prepo = new ProductRepository();
                        ProductInformation Pinfo = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit).FirstOrDefault();
                        else
                            Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (Pinfo != null)
                        {
                            if (!string.IsNullOrEmpty(Pinfo.Prefix))
                                Prefix = Pinfo.Prefix;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.GroupMasterId)))
                            {
                                GroupMasterRepository grepo = new GroupMasterRepository();
                                GroupMaster gdata = new GroupMaster();
                                if (CommonMethod.commProduct == true)
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId).FirstOrDefault();
                                else
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (gdata != null)
                                    GroupName = gdata.GroupMasterName;
                            }
                            if (!string.IsNullOrEmpty(Pinfo.MarathiName))
                                MarathiProductName = Pinfo.MarathiName;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.Rate)))
                                MRP = Convert.ToString(Pinfo.Rate);
                            if (!string.IsNullOrEmpty(Pinfo.Warranty))
                                Warranty = Pinfo.Warranty;
                            if (!string.IsNullOrEmpty(Pinfo.HSNCode))
                                HSNCode = Pinfo.HSNCode;
                        }
                        CompanyInformationRepository crepo = new CompanyInformationRepository();
                        var cdata = crepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                        if (cdata != null)
                        {
                            if (!string.IsNullOrEmpty(cdata.CompanyName))
                                ShopName = cdata.CompanyName;
                            if (!string.IsNullOrEmpty(cdata.CompNameMarathi))
                                MarathiShopName = cdata.CompNameMarathi;
                        }

                        for (int k = 0; k < copynum; k++)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                DataTable dt;
                                dt = ds.Tables["BarcodeTbl"];
                                dt.Rows.Add(new object[] { EncodeBarcode, Barcode, Prefix, GroupName, ProductNameP, MarathiProductName, MfgCompany, Unit, MRP, Warranty, HSNCode, ShopName, MarathiShopName, PurchaseRate, SaleRate, size });
                            }
                        }
                    }
                }

                string marathi = "";
                da = new SqlDataAdapter("Select * from BarcodeSetting", db.Connection);
                da.Fill(ds, "BarcodeSetting");
                SettingRepository settRepo = new SettingRepository();
                Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.Applyonmarathifont == "Y")
                    marathi = "Y";
                else
                    marathi = "N";
                if (sdata.ShowBarcodePrintOn == "T")
                {
                    barcoderpt1 = new ThermalPrintBarcodeCRPT();
                    barcoderpt1.SetDataSource(ds);
                    barcoderpt1.SetParameterValue("flag", marathi);
                    crystalReportViewer1.ReportSource = barcoderpt1;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    barcoderpt = new BarcodesmallRPT();
                    int wdth = 300; //left margin
                    if (colno > 1)
                        wdth = (2664 + 300) * (colno - 1);
                    if (Rownum > 1)
                    {
                        int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                        hgt = (hgt + 80) * (Rownum - 1);
                        hgt = hgt + 300;
                        barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                        barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, hgt, 200, 0));
                    }
                    else
                    {
                        int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                        barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                        barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, 300, 200, 0));
                    }
                    barcoderpt.SetDataSource(ds);
                    barcoderpt.SetParameterValue("flag", marathi);
                    crystalReportViewer1.ReportSource = barcoderpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception)
            { }
        }

        public void getdata(List<int> plist, decimal ponumber, int Rownum, int copynum, int colno, string flag)
        {
            try
            {
                SqlDataAdapter da;
                DSBarcode ds = new DSBarcode();
                for (int p = 0; p < plist.Count; p++)
                {
                    if (flag == "Purchase")
                    {
                        DataTable dtId = db.GetTable("Select d.PorderNo from PurchaseMaster m inner join PurchaseDetail d on m.Id=d.PorderNo Where  d.Id='" + plist[p] + "' and d.Barcode!='' and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                        if (dtId.Rows.Count != 0)
                            ponumber = Convert.ToInt32(dtId.Rows[0]["PorderNo"].ToString());
                        DataTable dt1 = db.GetTable("Select d.ProductName,d.CompanyName,d.Unit,d.Barcode,d.Quantity,CONVERT(VARCHAR(24),m.PorderDate,103) as 'PurDate',d.SaleRate,d.PurchasePrice,d.Size from PurchaseMaster m inner join PurchaseDetail d on m.Id=d.PorderNo Where d.PorderNo='" + ponumber + "' and d.Id='" + plist[p] + "' and d.Barcode!='' and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            ProductNameP = Convert.ToString(dt1.Rows[j]["ProductName"].ToString());
                            MfgCompany = Convert.ToString(dt1.Rows[j]["CompanyName"].ToString());
                            Unit = Convert.ToString(dt1.Rows[j]["Unit"].ToString());
                            Barcode = Convert.ToString(dt1.Rows[j]["Barcode"].ToString());
                            EncodeBarcode = Encode.Code128(Convert.ToString(dt1.Rows[j]["Barcode"].ToString()), 0, false);
                            qty = Convert.ToDecimal(dt1.Rows[j]["Quantity"].ToString());
                            size = Convert.ToString(dt1.Rows[j]["Size"].ToString());
                            if (!string.IsNullOrEmpty(dt1.Rows[j]["SaleRate"].ToString()))
                                SaleRate = Convert.ToString(dt1.Rows[j]["SaleRate"].ToString());
                            if (!string.IsNullOrEmpty(dt1.Rows[j]["PurchasePrice"].ToString()))
                                PurchaseRate = Convert.ToString(dt1.Rows[j]["PurchasePrice"].ToString());
                            ProductRepository Prepo = new ProductRepository();
                            ProductInformation Pinfo = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit).FirstOrDefault();
                            else
                                Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (Pinfo != null)
                            {
                                if (!string.IsNullOrEmpty(Pinfo.Prefix))
                                    Prefix = Pinfo.Prefix;
                                if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.GroupMasterId)))
                                {
                                    GroupMasterRepository grepo = new GroupMasterRepository();
                                    GroupMaster gdata = new GroupMaster();
                                    if (CommonMethod.commProduct == true)
                                        gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId).FirstOrDefault();
                                    else
                                        gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (gdata != null)
                                        GroupName = gdata.GroupMasterName;
                                }
                                if (!string.IsNullOrEmpty(Pinfo.MarathiName))
                                    MarathiProductName = Pinfo.MarathiName;
                                if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.Rate)))
                                    MRP = Convert.ToString(Pinfo.Rate);
                                if (!string.IsNullOrEmpty(Pinfo.Warranty))
                                    Warranty = Pinfo.Warranty;
                                if (!string.IsNullOrEmpty(Pinfo.HSNCode))
                                    HSNCode = Pinfo.HSNCode;
                            }
                            CompanyInformationRepository crepo = new CompanyInformationRepository();
                            var cdata = crepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                            if (cdata != null)
                            {
                                if (!string.IsNullOrEmpty(cdata.CompanyName))
                                    ShopName = cdata.CompanyName;
                                if (!string.IsNullOrEmpty(cdata.CompNameMarathi))
                                    MarathiShopName = cdata.CompNameMarathi;
                            }
                            for (int k = 0; k < copynum; k++)
                            {
                                for (int i = 0; i < qty; i++)
                                {
                                    DataTable dt;
                                    dt = ds.Tables["BarcodeTbl"];
                                    dt.Rows.Add(new object[] { EncodeBarcode, Barcode, Prefix, GroupName, ProductNameP, MarathiProductName, MfgCompany, Unit, MRP, Warranty, HSNCode, ShopName, MarathiShopName, PurchaseRate, SaleRate, size });
                                }
                            }
                        }
                    }
                }

                string marathi = "";
                da = new SqlDataAdapter("Select * from BarcodeSetting", db.Connection);
                da.Fill(ds, "BarcodeSetting");
                SettingRepository settRepo = new SettingRepository();
                Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.Applyonmarathifont == "Y")
                    marathi = "Y";
                else
                    marathi = "N";
                if (sdata.ShowBarcodePrintOn == "T")
                {
                    barcoderpt1 = new ThermalPrintBarcodeCRPT();
                    barcoderpt1.SetDataSource(ds);
                    barcoderpt1.SetParameterValue("flag", marathi);
                    crystalReportViewer1.ReportSource = barcoderpt1;
                    crystalReportViewer1.Refresh();
                }
                else
                {
                    int topmargin = 0;
                    if (sdata.BarcodePrintTopMargin != null)
                        topmargin = Convert.ToInt32(sdata.BarcodePrintTopMargin);
                    else
                    {
                        string input = Microsoft.VisualBasic.Interaction.InputBox("Enter top margin for Barcode", "Page Setup", "" + topmargin + "", 300, 250);
                        sdata.BarcodePrintTopMargin = topmargin;
                        settRepo.Edit(sdata);
                        settRepo.Save();
                        topmargin = Convert.ToInt32(input);
                    }
                    barcoderpt = new BarcodesmallRPT();
                    int wdth = 300; //left margin
                    if (colno > 1)
                        wdth = (2664 + 300) * (colno - 1);
                    if (Rownum > 1)
                    {
                        int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                        hgt = (hgt + 80) * (Rownum - 1);
                        hgt = hgt + 300;
                        topmargin = topmargin + hgt;
                        barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                        barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, topmargin, 200, topmargin));
                    }
                    else
                    {
                        int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                        topmargin = topmargin + 300;
                        barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                        barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, topmargin, 200, topmargin));
                    }
                    barcoderpt.SetDataSource(ds);
                    barcoderpt.SetParameterValue("flag", marathi);
                    crystalReportViewer1.ReportSource = barcoderpt;
                    crystalReportViewer1.Refresh();
                }
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

        public void customizeBracodePrint(decimal ponumber, int Rownum, int copynum, int colno, string flag)
        {
            try
            {
                SqlDataAdapter da;
                DSBarcode ds = new DSBarcode();
                if (flag == "Purchase")
                {
                    DataTable dt1 = db.GetTable(" Select d.ProductName,d.CompanyName,d.Unit,d.Barcode,d.Quantity,CONVERT(VARCHAR(24),m.PorderDate,103) as 'PurDate',d.SaleRate,d.PurchasePrice,d.Size from PurchaseMaster m inner join PurchaseDetail d on m.Id=d.PorderNo Where d.PorderNo='" + ponumber + "' and d.Barcode!='' and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        ProductNameP = Convert.ToString(dt1.Rows[j]["ProductName"].ToString());
                        MfgCompany = Convert.ToString(dt1.Rows[j]["CompanyName"].ToString());
                        Unit = Convert.ToString(dt1.Rows[j]["Unit"].ToString());
                        Barcode = Convert.ToString(dt1.Rows[j]["Barcode"].ToString());
                        EncodeBarcode = Encode.Code128(Convert.ToString(dt1.Rows[j]["Barcode"].ToString()), 0, false);
                        qty = Convert.ToDecimal(dt1.Rows[j]["Quantity"].ToString());
                        size = Convert.ToString(dt1.Rows[j]["Size"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["SaleRate"].ToString()))
                            SaleRate = Convert.ToString(dt1.Rows[j]["SaleRate"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["PurchasePrice"].ToString()))
                            PurchaseRate = Convert.ToString(dt1.Rows[j]["PurchasePrice"].ToString());
                        ProductRepository Prepo = new ProductRepository();
                        ProductInformation Pinfo = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit).FirstOrDefault();
                        else
                            Pinfo = Prepo.GetAll().Where(t => t.ProductName == ProductNameP && t.ManufactureCompany == MfgCompany && t.Uniti == Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (Pinfo != null)
                        {
                            if (!string.IsNullOrEmpty(Pinfo.Prefix))
                                Prefix = Pinfo.Prefix;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.GroupMasterId)))
                            {
                                GroupMasterRepository grepo = new GroupMasterRepository();
                                GroupMaster gdata = new GroupMaster();
                                if (CommonMethod.commProduct == true)
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId).FirstOrDefault();
                                else
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (gdata != null)
                                    GroupName = gdata.GroupMasterName;
                            }
                            if (!string.IsNullOrEmpty(Pinfo.MarathiName))
                                MarathiProductName = Pinfo.MarathiName;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.Rate)))
                                MRP = Convert.ToString(Pinfo.Rate);
                            if (!string.IsNullOrEmpty(Pinfo.Warranty))
                                Warranty = Pinfo.Warranty;
                            if (!string.IsNullOrEmpty(Pinfo.HSNCode))
                                HSNCode = Pinfo.HSNCode;
                        }
                        CompanyInformationRepository crepo = new CompanyInformationRepository();
                        var cdata = crepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                        if (cdata != null)
                        {
                            if (!string.IsNullOrEmpty(cdata.CompanyName))
                                ShopName = cdata.CompanyName;
                            if (!string.IsNullOrEmpty(cdata.CompNameMarathi))
                                MarathiShopName = cdata.CompNameMarathi;
                        }
                        for (int k = 0; k < copynum; k++)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                DataTable dt;
                                dt = ds.Tables["BarcodeTbl"];
                                dt.Rows.Add(new object[] { EncodeBarcode, Barcode, Prefix, GroupName, ProductNameP, MarathiProductName, MfgCompany, Unit, MRP, Warranty, HSNCode, ShopName, MarathiShopName, PurchaseRate, SaleRate, size });
                            }
                        }
                    }
                }
                else
                {
                    DataTable dt1 = db.GetTable(" Select d.ProductNo,d.Barcode,d.Qty,CONVERT(VARCHAR(24),m.RepackDate,103) as 'PurDate',d.SaleRate from RepackMaster m inner join RepackDetail d on m.ID=d.RepackNo Where d.RepackNo='" + ponumber + "' and d.Barcode!=''");
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        Barcode = Convert.ToString(dt1.Rows[j]["Barcode"].ToString());
                        EncodeBarcode = Encode.Code128(Convert.ToString(dt1.Rows[j]["Barcode"].ToString()), 0, false);
                        qty = Convert.ToDecimal(dt1.Rows[j]["Qty"].ToString());
                        if (!string.IsNullOrEmpty(dt1.Rows[j]["SaleRate"].ToString()))
                            SaleRate = Convert.ToString(dt1.Rows[j]["SaleRate"].ToString());
                        ProductRepository Prepo = new ProductRepository();
                        ProductInformation Pinfo = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            Pinfo = Prepo.GetAll().Where(t => t.ID == Convert.ToInt32(dt1.Rows[j]["ProductNo"].ToString())).FirstOrDefault();
                        else
                            Pinfo = Prepo.GetAll().Where(t => t.ID == Convert.ToInt32(dt1.Rows[j]["ProductNo"].ToString()) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (Pinfo != null)
                        {
                            ProductNameP = Pinfo.ProductName;
                            MfgCompany = Pinfo.ManufactureCompany;
                            Unit = Pinfo.Uniti;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.PurchaseRate)))
                                PurchaseRate = Convert.ToString(Pinfo.PurchaseRate);
                            if (!string.IsNullOrEmpty(Pinfo.Prefix))
                                Prefix = Pinfo.Prefix;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.GroupMasterId)))
                            {
                                GroupMasterRepository grepo = new GroupMasterRepository();
                                GroupMaster gdata = new GroupMaster();
                                if (CommonMethod.commProduct == true)
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId).FirstOrDefault();
                                else
                                    gdata = grepo.GetAll().Where(t => t.GroupMasterId == Pinfo.GroupMasterId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (gdata != null)
                                    GroupName = gdata.GroupMasterName;
                            }
                            if (!string.IsNullOrEmpty(Pinfo.MarathiName))
                                MarathiProductName = Pinfo.MarathiName;
                            if (!string.IsNullOrEmpty(Convert.ToString(Pinfo.Rate)))
                                MRP = Convert.ToString(Pinfo.Rate);
                            if (!string.IsNullOrEmpty(Pinfo.Warranty))
                                Warranty = Pinfo.Warranty;
                            if (!string.IsNullOrEmpty(Pinfo.HSNCode))
                                HSNCode = Pinfo.HSNCode;
                        }
                        CompanyInformationRepository crepo = new CompanyInformationRepository();
                        var cdata = crepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                        if (cdata != null)
                        {
                            if (!string.IsNullOrEmpty(cdata.CompanyName))
                                ShopName = cdata.CompanyName;
                            if (!string.IsNullOrEmpty(cdata.CompNameMarathi))
                                MarathiShopName = cdata.CompNameMarathi;
                        }
                        for (int k = 0; k < copynum; k++)
                        {
                            for (int i = 0; i < qty; i++)
                            {
                                DataTable dt;
                                dt = ds.Tables["BarcodeTbl"];
                                dt.Rows.Add(new object[] { EncodeBarcode, Barcode, Prefix, GroupName, ProductNameP, MarathiProductName, MfgCompany, Unit, MRP, Warranty, HSNCode, ShopName, MarathiShopName, PurchaseRate, SaleRate, size });
                            }
                        }
                    }
                }
                da = new SqlDataAdapter("Select * from BarcodeSetting", db.Connection);
                da.Fill(ds, "BarcodeSetting");
                BarcodesmallRPT barcoderpt = new BarcodesmallRPT();
                int wdth = 300; //left margin
                if (colno > 1)
                    wdth = (2664 + 300) * (colno - 1);
                if (Rownum > 1)
                {
                    int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                    hgt = (hgt + 80) * (Rownum - 1);
                    hgt = hgt + 300;
                    barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                    barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, hgt, 200, 0));
                }
                else
                {
                    int hgt = Convert.ToInt32(barcoderpt.ReportDefinition.Sections["Section3"].Height.ToString());
                    barcoderpt.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;
                    barcoderpt.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(wdth, 300, 200, 0));
                }
                barcoderpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = barcoderpt;
                crystalReportViewer1.Refresh();
            }
            catch (Exception)
            { }
        }
    }
}