using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmTallyExport : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        ToolTip tooltipbtn = new ToolTip();
        public int MasterId = 0;
        SettingRepository MSetRepo = new SettingRepository();
        Setting MSetData = new Setting();

        class ProductNameWithBillNo
        {
            public string BillNo { get; set; }
            public string ProductName { get; set; }
        }

        class BillNoIdName
        {
            public string BillNo { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
        }

        //Master Error Array
        List<int> CustIDArray = new List<int>();
        List<int> SuppIDArray = new List<int>();
        List<int> UnitIDArray = new List<int>();
        List<int> MFGIDArray = new List<int>();
        List<int> ProductIDArray = new List<int>();
        List<int> ExpIDArray = new List<int>();
        List<int> IncomeIDArray = new List<int>();

        //Transaction Error Array
        List<BillNoIdName> PurchaseIDArray = new List<BillNoIdName>();
        List<ProductNameWithBillNo> PurProductIDArray = new List<ProductNameWithBillNo>();
        List<BillNoIdName> SaleIDArray = new List<BillNoIdName>();
        List<ProductNameWithBillNo> SaleProductIDArray = new List<ProductNameWithBillNo>();
        List<ProductNameWithBillNo> SaleGSTProductIDArray = new List<ProductNameWithBillNo>();
        List<ProductNameWithBillNo> PurchaseGSTProductIDArray = new List<ProductNameWithBillNo>();
        List<BillNoIdName> PurReturnIDArray = new List<BillNoIdName>();
        List<ProductNameWithBillNo> PurRProductIDArray = new List<ProductNameWithBillNo>();
        List<BillNoIdName> SaleReturnIDArray = new List<BillNoIdName>();
        List<ProductNameWithBillNo> SaleRProductIDArray = new List<ProductNameWithBillNo>();

        public int intPurCount = 0, intPurRCount = 0, intSaleCount = 0, intSaleRCount = 0, intGroupCount = 0, intProCnt = 0, intCustCount = 0, intSuppCount = 0, 
            intExpCount = 0, intIncome = 0, intUnitCount = 0, intMFGCount = 0, intProdCount = 0;
        public string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
        public static string TallyKey = "";
        
        public frmTallyExport()
        {
            MSetData = new Setting();
            MSetData = MSetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            tooltipbtn.SetToolTip(this.totMasterCount, "Click To See Master Details");
            tooltipbtn.SetToolTip(this.totTransactionCount, "Click To See Voucher Details");
        }

        private void btnMasterExport_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument XD = new XmlDocument();
                XmlNode ENVELOPE = XD.AppendChild(XD.CreateElement("ENVELOPE"));
                XmlNode HEADER = ENVELOPE.AppendChild(XD.CreateElement("HEADER"));
                XmlNode TALLYREQUEST = HEADER.AppendChild(XD.CreateElement("TALLYREQUEST"));
                XmlNode BODY = ENVELOPE.AppendChild(XD.CreateElement("BODY"));
                XmlNode IMPORTDATA = BODY.AppendChild(XD.CreateElement("IMPORTDATA"));
                XmlNode REQUESTDESC = IMPORTDATA.AppendChild(XD.CreateElement("REQUESTDESC"));
                XmlNode REPORTNAME = REQUESTDESC.AppendChild(XD.CreateElement("REPORTNAME"));
                XmlNode REQUESTDATA = IMPORTDATA.AppendChild(XD.CreateElement("REQUESTDATA"));
                TALLYREQUEST.InnerText = "Import Data";
                REPORTNAME.InnerText = "All Masters";

                //Purchase Accounts
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode VATAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("VATAPPLICABLE"));
                    XmlNode ISBILLWISEON = LEDGER.AppendChild(XD.CreateElement("ISBILLWISEON"));
                    XmlNode ISCOSTCENTRESON = LEDGER.AppendChild(XD.CreateElement("ISCOSTCENTRESON"));
                    XmlNode ASORIGINAL = LEDGER.AppendChild(XD.CreateElement("ASORIGINAL"));
                    XmlNode AFFECTSSTOCK = LEDGER.AppendChild(XD.CreateElement("AFFECTSSTOCK"));
                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "Purchase";
                    PARENT.InnerText = "Purchase Accounts";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "Others";
                    VATAPPLICABLE.InnerText = "  Applicable";
                    ISBILLWISEON.InnerText = "No";
                    ISCOSTCENTRESON.InnerText = "Yes";
                    ASORIGINAL.InnerText = "Yes";
                    AFFECTSSTOCK.InnerText = "Yes";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "Purchase";
                }

                //Sale Accounts
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode VATAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("VATAPPLICABLE"));
                    XmlNode ISBILLWISEON = LEDGER.AppendChild(XD.CreateElement("ISBILLWISEON"));
                    XmlNode ISCOSTCENTRESON = LEDGER.AppendChild(XD.CreateElement("ISCOSTCENTRESON"));
                    XmlNode ASORIGINAL = LEDGER.AppendChild(XD.CreateElement("ASORIGINAL"));
                    XmlNode AFFECTSSTOCK = LEDGER.AppendChild(XD.CreateElement("AFFECTSSTOCK"));
                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "Sales";
                    PARENT.InnerText = "Sales Accounts";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "Others";
                    VATAPPLICABLE.InnerText = "  Applicable";
                    ISBILLWISEON.InnerText = "No";
                    ISCOSTCENTRESON.InnerText = "Yes";
                    ASORIGINAL.InnerText = "Yes";
                    AFFECTSSTOCK.InnerText = "Yes";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "Sales";
                }

                //Discount
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));

                    XmlNode OLDAUDITENTRYIDSLIST = LEDGER.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                    XmlAttribute OLDAUDITENTRYIDSLISTTYPE = OLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode OLDAUDITENTRYIDS = OLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));

                    //XmlNode LEDGERGUID = LEDGER.AppendChild(XD.CreateElement("GUID"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXCLASSIFICATIONNAME = LEDGER.AppendChild(XD.CreateElement("TAXCLASSIFICATIONNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode LEDADDLALLOCTYPE = LEDGER.AppendChild(XD.CreateElement("LEDADDLALLOCTYPE"));
                    XmlNode GSTTYPE = LEDGER.AppendChild(XD.CreateElement("GSTTYPE"));
                    XmlNode SERVICECATEGORY = LEDGER.AppendChild(XD.CreateElement("SERVICECATEGORY"));
                    XmlNode EXCISELEDGERCLASSIFICATION = LEDGER.AppendChild(XD.CreateElement("EXCISELEDGERCLASSIFICATION"));
                    XmlNode EXCISEDUTYTYPE = LEDGER.AppendChild(XD.CreateElement("EXCISEDUTYTYPE"));
                    XmlNode EXCISENATUREOFPURCHASE = LEDGER.AppendChild(XD.CreateElement("EXCISENATUREOFPURCHASE"));
                    XmlNode LEDGERFBTCATEGORY = LEDGER.AppendChild(XD.CreateElement("LEDGERFBTCATEGORY"));
                    XmlNode VATAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("VATAPPLICABLE"));
                    XmlNode ISBILLWISEON = LEDGER.AppendChild(XD.CreateElement("ISBILLWISEON"));

                    XmlNode ISINTERESTON = LEDGER.AppendChild(XD.CreateElement("ISINTERESTON"));
                    XmlNode ALLOWINMOBILE = LEDGER.AppendChild(XD.CreateElement("ALLOWINMOBILE"));
                    XmlNode ISCOSTTRACKINGON = LEDGER.AppendChild(XD.CreateElement("ISCOSTTRACKINGON"));
                    XmlNode ISBENEFICIARYCODEON = LEDGER.AppendChild(XD.CreateElement("ISBENEFICIARYCODEON"));
                    XmlNode PLASINCOMEEXPENSE = LEDGER.AppendChild(XD.CreateElement("PLASINCOMEEXPENSE"));
                    XmlNode ISCOSTCENTRESON = LEDGER.AppendChild(XD.CreateElement("ISCOSTCENTRESON"));
                    XmlNode ISUPDATINGTARGETID = LEDGER.AppendChild(XD.CreateElement("ISUPDATINGTARGETID"));
                    XmlNode ASORIGINAL = LEDGER.AppendChild(XD.CreateElement("ASORIGINAL"));
                    XmlNode ISCONDENSED = LEDGER.AppendChild(XD.CreateElement("ISCONDENSED"));
                    XmlNode AFFECTSSTOCK = LEDGER.AppendChild(XD.CreateElement("AFFECTSSTOCK"));
                    XmlNode ISRATEINCLUSIVEVAT = LEDGER.AppendChild(XD.CreateElement("ISRATEINCLUSIVEVAT"));
                    XmlNode FORPAYROLL = LEDGER.AppendChild(XD.CreateElement("FORPAYROLL"));
                    XmlNode ISABCENABLED = LEDGER.AppendChild(XD.CreateElement("ISABCENABLED"));
                    XmlNode ISCREDITDAYSCHKON = LEDGER.AppendChild(XD.CreateElement("ISCREDITDAYSCHKON"));
                    XmlNode INTERESTONBILLWISE = LEDGER.AppendChild(XD.CreateElement("INTERESTONBILLWISE"));
                    XmlNode OVERRIDEINTEREST = LEDGER.AppendChild(XD.CreateElement("OVERRIDEINTEREST"));
                    XmlNode OVERRIDEADVINTEREST = LEDGER.AppendChild(XD.CreateElement("OVERRIDEADVINTEREST"));
                    XmlNode USEFORVAT = LEDGER.AppendChild(XD.CreateElement("USEFORVAT"));
                    XmlNode IGNORETDSEXEMPT = LEDGER.AppendChild(XD.CreateElement("IGNORETDSEXEMPT"));
                    XmlNode ISTCSAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISTCSAPPLICABLE"));
                    XmlNode ISTDSAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISTDSAPPLICABLE"));
                    XmlNode ISFBTAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISFBTAPPLICABLE"));
                    XmlNode ISGSTAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISGSTAPPLICABLE"));
                    XmlNode ISEXCISEAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISEXCISEAPPLICABLE"));
                    XmlNode ISTDSEXPENSE = LEDGER.AppendChild(XD.CreateElement("ISTDSEXPENSE"));
                    XmlNode ISEDLIAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISEDLIAPPLICABLE"));
                    XmlNode ISRELATEDPARTY = LEDGER.AppendChild(XD.CreateElement("ISRELATEDPARTY"));
                    XmlNode USEFORESIELIGIBILITY = LEDGER.AppendChild(XD.CreateElement("USEFORESIELIGIBILITY"));
                    XmlNode ISINTERESTINCLLASTDAY = LEDGER.AppendChild(XD.CreateElement("ISINTERESTINCLLASTDAY"));
                    XmlNode APPROPRIATETAXVALUE = LEDGER.AppendChild(XD.CreateElement("APPROPRIATETAXVALUE"));
                    XmlNode ISBEHAVEASDUTY = LEDGER.AppendChild(XD.CreateElement("ISBEHAVEASDUTY"));
                    XmlNode INTERESTINCLDAYOFADDITION = LEDGER.AppendChild(XD.CreateElement("INTERESTINCLDAYOFADDITION"));
                    XmlNode INTERESTINCLDAYOFDEDUCTION = LEDGER.AppendChild(XD.CreateElement("INTERESTINCLDAYOFDEDUCTION"));
                    XmlNode ISOTHTERRITORYASSESSEE = LEDGER.AppendChild(XD.CreateElement("ISOTHTERRITORYASSESSEE"));
                    XmlNode OVERRIDECREDITLIMIT = LEDGER.AppendChild(XD.CreateElement("OVERRIDECREDITLIMIT"));
                    XmlNode ISAGAINSTFORMC = LEDGER.AppendChild(XD.CreateElement("ISAGAINSTFORMC"));
                    XmlNode ISCHEQUEPRINTINGENABLED = LEDGER.AppendChild(XD.CreateElement("ISCHEQUEPRINTINGENABLED"));
                    XmlNode ISPAYUPLOAD = LEDGER.AppendChild(XD.CreateElement("ISPAYUPLOAD"));
                    XmlNode ISPAYBATCHONLYSAL = LEDGER.AppendChild(XD.CreateElement("ISPAYBATCHONLYSAL"));
                    XmlNode ISBNFCODESUPPORTED = LEDGER.AppendChild(XD.CreateElement("ISBNFCODESUPPORTED"));
                    XmlNode ALLOWEXPORTWITHERRORS = LEDGER.AppendChild(XD.CreateElement("ALLOWEXPORTWITHERRORS"));
                    XmlNode CONSIDERPURCHASEFOREXPORT = LEDGER.AppendChild(XD.CreateElement("CONSIDERPURCHASEFOREXPORT"));
                    XmlNode ISTRANSPORTER = LEDGER.AppendChild(XD.CreateElement("ISTRANSPORTER"));
                    XmlNode USEFORNOTIONALITC = LEDGER.AppendChild(XD.CreateElement("USEFORNOTIONALITC"));
                    XmlNode ISECOMMOPERATOR = LEDGER.AppendChild(XD.CreateElement("ISECOMMOPERATOR"));
                    XmlNode SHOWINPAYSLIP = LEDGER.AppendChild(XD.CreateElement("SHOWINPAYSLIP"));
                    XmlNode USEFORGRATUITY = LEDGER.AppendChild(XD.CreateElement("USEFORGRATUITY"));
                    XmlNode ISTDSPROJECTED = LEDGER.AppendChild(XD.CreateElement("ISTDSPROJECTED"));
                    XmlNode FORSERVICETAX = LEDGER.AppendChild(XD.CreateElement("FORSERVICETAX"));
                    XmlNode ISINPUTCREDIT = LEDGER.AppendChild(XD.CreateElement("ISINPUTCREDIT"));
                    XmlNode ISEXEMPTED = LEDGER.AppendChild(XD.CreateElement("ISEXEMPTED"));
                    XmlNode ISABATEMENTAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("ISABATEMENTAPPLICABLE"));
                    XmlNode ISSTXPARTY = LEDGER.AppendChild(XD.CreateElement("ISSTXPARTY"));
                    XmlNode ISSTXNONREALIZEDTYPE = LEDGER.AppendChild(XD.CreateElement("ISSTXNONREALIZEDTYPE"));
                    XmlNode ISUSEDFORCVD = LEDGER.AppendChild(XD.CreateElement("ISUSEDFORCVD"));
                    XmlNode LEDBELONGSTONONTAXABLE = LEDGER.AppendChild(XD.CreateElement("LEDBELONGSTONONTAXABLE"));
                    XmlNode ISEXCISEMERCHANTEXPORTER = LEDGER.AppendChild(XD.CreateElement("ISEXCISEMERCHANTEXPORTER"));
                    XmlNode ISPARTYEXEMPTED = LEDGER.AppendChild(XD.CreateElement("ISPARTYEXEMPTED"));
                    XmlNode ISSEZPARTY = LEDGER.AppendChild(XD.CreateElement("ISSEZPARTY"));
                    XmlNode TDSDEDUCTEEISSPECIALRATE = LEDGER.AppendChild(XD.CreateElement("TDSDEDUCTEEISSPECIALRATE"));
                    XmlNode ISECHEQUESUPPORTED = LEDGER.AppendChild(XD.CreateElement("ISECHEQUESUPPORTED"));
                    XmlNode ISEDDSUPPORTED = LEDGER.AppendChild(XD.CreateElement("ISEDDSUPPORTED"));
                    XmlNode HASECHEQUEDELIVERYMODE = LEDGER.AppendChild(XD.CreateElement("HASECHEQUEDELIVERYMODE"));
                    XmlNode HASECHEQUEDELIVERYTO = LEDGER.AppendChild(XD.CreateElement("HASECHEQUEDELIVERYTO"));
                    XmlNode HASECHEQUEPRINTLOCATION = LEDGER.AppendChild(XD.CreateElement("HASECHEQUEPRINTLOCATION"));
                    XmlNode HASECHEQUEPAYABLELOCATION = LEDGER.AppendChild(XD.CreateElement("HASECHEQUEPAYABLELOCATION"));
                    XmlNode HASECHEQUEBANKLOCATION = LEDGER.AppendChild(XD.CreateElement("HASECHEQUEBANKLOCATION"));
                    XmlNode HASEDDDELIVERYMODE = LEDGER.AppendChild(XD.CreateElement("HASEDDDELIVERYMODE"));
                    XmlNode HASEDDDELIVERYTO = LEDGER.AppendChild(XD.CreateElement("HASEDDDELIVERYTO"));
                    XmlNode HASEDDPRINTLOCATION = LEDGER.AppendChild(XD.CreateElement("HASEDDPRINTLOCATION"));
                    XmlNode HASEDDPAYABLELOCATION = LEDGER.AppendChild(XD.CreateElement("HASEDDPAYABLELOCATION"));
                    XmlNode HASEDDBANKLOCATION = LEDGER.AppendChild(XD.CreateElement("HASEDDBANKLOCATION"));
                    XmlNode ISEBANKINGENABLED = LEDGER.AppendChild(XD.CreateElement("ISEBANKINGENABLED"));
                    XmlNode ISEXPORTFILEENCRYPTED = LEDGER.AppendChild(XD.CreateElement("ISEXPORTFILEENCRYPTED"));
                    XmlNode ISBATCHENABLED = LEDGER.AppendChild(XD.CreateElement("ISBATCHENABLED"));
                    XmlNode ISPRODUCTCODEBASED = LEDGER.AppendChild(XD.CreateElement("ISPRODUCTCODEBASED"));
                    XmlNode HASEDDCITY = LEDGER.AppendChild(XD.CreateElement("HASEDDCITY"));
                    XmlNode HASECHEQUECITY = LEDGER.AppendChild(XD.CreateElement("HASECHEQUECITY"));
                    XmlNode ISFILENAMEFORMATSUPPORTED = LEDGER.AppendChild(XD.CreateElement("ISFILENAMEFORMATSUPPORTED"));
                    XmlNode HASCLIENTCODE = LEDGER.AppendChild(XD.CreateElement("HASCLIENTCODE"));
                    XmlNode PAYINSISBATCHAPPLICABLE = LEDGER.AppendChild(XD.CreateElement("PAYINSISBATCHAPPLICABLE"));
                    XmlNode PAYINSISFILENUMAPP = LEDGER.AppendChild(XD.CreateElement("PAYINSISFILENUMAPP"));
                    XmlNode ISSALARYTRANSGROUPEDFORBRS = LEDGER.AppendChild(XD.CreateElement("ISSALARYTRANSGROUPEDFORBRS"));
                    XmlNode ISEBANKINGSUPPORTED = LEDGER.AppendChild(XD.CreateElement("ISEBANKINGSUPPORTED"));
                    XmlNode ISSCBUAE = LEDGER.AppendChild(XD.CreateElement("ISSCBUAE"));
                    XmlNode ISBANKSTATUSAPP = LEDGER.AppendChild(XD.CreateElement("ISBANKSTATUSAPP"));
                    XmlNode ISSALARYGROUPED = LEDGER.AppendChild(XD.CreateElement("ISSALARYGROUPED"));
                    XmlNode USEFORPURCHASETAX = LEDGER.AppendChild(XD.CreateElement("USEFORPURCHASETAX"));
                    XmlNode AUDITED = LEDGER.AppendChild(XD.CreateElement("AUDITED"));
                    XmlNode SORTPOSITION = LEDGER.AppendChild(XD.CreateElement("SORTPOSITION"));
                    XmlNode ALTERID = LEDGER.AppendChild(XD.CreateElement("ALTERID"));
                    XmlNode SERVICETAXDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                    XmlNode LBTREGNDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("LBTREGNDETAILS.LIST"));
                    XmlNode VATDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("VATDETAILS.LIST"));
                    XmlNode SALESTAXCESSDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("SALESTAXCESSDETAILS.LIST"));
                    XmlNode GSTDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("GSTDETAILS.LIST"));

                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    XmlNode LANGUAGEID = LANGUAGENAMELIST.AppendChild(XD.CreateElement("LANGUAGEID"));

                    XmlNode XBRLDETAILLIST = LEDGER.AppendChild(XD.CreateElement("XBRLDETAIL.LIST"));
                    XmlNode AUDITDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("AUDITDETAILS.LIST"));
                    XmlNode SCHVIDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("SCHVIDETAILS.LIST"));
                    XmlNode EXCISETARIFFDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("EXCISETARIFFDETAILS.LIST"));
                    XmlNode TCSCATEGORYDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("TCSCATEGORYDETAILS.LIST"));
                    XmlNode TDSCATEGORYDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("TDSCATEGORYDETAILS.LIST"));
                    XmlNode SLABPERIODLIST = LEDGER.AppendChild(XD.CreateElement("SLABPERIOD.LIST"));
                    XmlNode GRATUITYPERIODLIST = LEDGER.AppendChild(XD.CreateElement("GRATUITYPERIOD.LIST"));
                    XmlNode ADDITIONALCOMPUTATIONSLIST = LEDGER.AppendChild(XD.CreateElement("ADDITIONALCOMPUTATIONS.LIST"));
                    XmlNode EXCISEJURISDICTIONDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("EXCISEJURISDICTIONDETAILS.LIST"));
                    XmlNode EXCLUDEDTAXATIONSLIST = LEDGER.AppendChild(XD.CreateElement("EXCLUDEDTAXATIONS.LIST"));
                    XmlNode BANKALLOCATIONSLIST = LEDGER.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                    XmlNode PAYMENTDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("PAYMENTDETAILS.LIST"));
                    XmlNode BANKEXPORTFORMATSLIST = LEDGER.AppendChild(XD.CreateElement("BANKEXPORTFORMATS.LIST"));
                    XmlNode BILLALLOCATIONSLIST = LEDGER.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                    XmlNode INTERESTCOLLECTIONLIST = LEDGER.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                    XmlNode LEDGERCLOSINGVALUESLIST = LEDGER.AppendChild(XD.CreateElement("LEDGERCLOSINGVALUES.LIST"));
                    XmlNode LEDGERAUDITCLASSLIST = LEDGER.AppendChild(XD.CreateElement("LEDGERAUDITCLASS.LIST"));
                    XmlNode OLDAUDITENTRIESLIST = LEDGER.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                    XmlNode TDSEXEMPTIONRULESLIST = LEDGER.AppendChild(XD.CreateElement("TDSEXEMPTIONRULES.LIST"));
                    XmlNode DEDUCTINSAMEVCHRULESLIST = LEDGER.AppendChild(XD.CreateElement("DEDUCTINSAMEVCHRULES.LIST"));
                    XmlNode LOWERDEDUCTIONLIST = LEDGER.AppendChild(XD.CreateElement("LOWERDEDUCTION.LIST"));
                    XmlNode STXABATEMENTDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("STXABATEMENTDETAILS.LIST"));
                    XmlNode LEDMULTIADDRESSLISTLIST = LEDGER.AppendChild(XD.CreateElement("LEDMULTIADDRESSLIST.LIST"));
                    XmlNode STXTAXDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("STXTAXDETAILS.LIST"));
                    XmlNode CHEQUERANGELIST = LEDGER.AppendChild(XD.CreateElement("CHEQUERANGE.LIST"));
                    XmlNode DEFAULTVCHCHEQUEDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("DEFAULTVCHCHEQUEDETAILS.LIST"));
                    XmlNode ACCOUNTAUDITENTRIESLIST = LEDGER.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                    XmlNode AUDITENTRIESLIST = LEDGER.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                    XmlNode BRSIMPORTEDINFOLIST = LEDGER.AppendChild(XD.CreateElement("BRSIMPORTEDINFO.LIST"));
                    XmlNode AUTOBRSCONFIGSLIST = LEDGER.AppendChild(XD.CreateElement("AUTOBRSCONFIGS.LIST"));
                    XmlNode BANKURENTRIESLIST = LEDGER.AppendChild(XD.CreateElement("BANKURENTRIES.LIST"));
                    XmlNode DEFAULTCHEQUEDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("DEFAULTCHEQUEDETAILS.LIST"));
                    XmlNode DEFAULTOPENINGCHEQUEDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("DEFAULTOPENINGCHEQUEDETAILS.LIST"));
                    XmlNode CANCELLEDPAYALLOCATIONSLIST = LEDGER.AppendChild(XD.CreateElement("CANCELLEDPAYALLOCATIONS.LIST"));
                    XmlNode ECHEQUEPRINTLOCATIONLIST = LEDGER.AppendChild(XD.CreateElement("ECHEQUEPRINTLOCATION.LIST"));
                    XmlNode ECHEQUEPAYABLELOCATIONLIST = LEDGER.AppendChild(XD.CreateElement("ECHEQUEPAYABLELOCATION.LIST"));
                    XmlNode EDDPRINTLOCATIONLIST = LEDGER.AppendChild(XD.CreateElement("EDDPRINTLOCATION.LIST"));
                    XmlNode EDDPAYABLELOCATIONLIST = LEDGER.AppendChild(XD.CreateElement("EDDPAYABLELOCATION.LIST"));
                    XmlNode AVAILABLETRANSACTIONTYPESLIST = LEDGER.AppendChild(XD.CreateElement("AVAILABLETRANSACTIONTYPES.LIST"));
                    XmlNode LEDPAYINSCONFIGSLIST = LEDGER.AppendChild(XD.CreateElement("LEDPAYINSCONFIGS.LIST"));
                    XmlNode TYPECODEDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("TYPECODEDETAILS.LIST"));
                    XmlNode FIELDVALIDATIONDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("FIELDVALIDATIONDETAILS.LIST"));
                    XmlNode INPUTCRALLOCSLIST = LEDGER.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                    XmlNode GSTCLASSFNIGSTRATESLIST = LEDGER.AppendChild(XD.CreateElement("GSTCLASSFNIGSTRATES.LIST"));
                    XmlNode EXTARIFFDUTYHEADDETAILSLIST = LEDGER.AppendChild(XD.CreateElement("EXTARIFFDUTYHEADDETAILS.LIST"));
                    XmlNode VOUCHERTYPEPRODUCTCODESLIST = LEDGER.AppendChild(XD.CreateElement("VOUCHERTYPEPRODUCTCODES.LIST"));

                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "Discount Given";
                    PARENT.InnerText = "Indirect Expenses";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "Others";
                    SERVICECATEGORY.InnerText = "&#4; Not Applicable";
                    VATAPPLICABLE.InnerText = "&#4; Not Applicable";
                    ISBILLWISEON.InnerText = "No";
                    ISINTERESTON.InnerText = "No";
                    ALLOWINMOBILE.InnerText = "No";
                    ISCOSTTRACKINGON.InnerText = "No";
                    ISBENEFICIARYCODEON.InnerText = "No";
                    PLASINCOMEEXPENSE.InnerText = "No";
                    ISCOSTCENTRESON.InnerText = "Yes";
                    ISUPDATINGTARGETID.InnerText = "No";
                    ASORIGINAL.InnerText = "Yes";
                    ISCONDENSED.InnerText = "No";
                    AFFECTSSTOCK.InnerText = "No";
                    ISRATEINCLUSIVEVAT.InnerText = "No";
                    FORPAYROLL.InnerText = "No";
                    ISABCENABLED.InnerText = "No";
                    ISCREDITDAYSCHKON.InnerText = "No";
                    INTERESTONBILLWISE.InnerText = "No";
                    OVERRIDEINTEREST.InnerText = "No";
                    OVERRIDEADVINTEREST.InnerText = "No";
                    USEFORVAT.InnerText = "No";
                    IGNORETDSEXEMPT.InnerText = "No";
                    ISTCSAPPLICABLE.InnerText = "No";
                    ISTDSAPPLICABLE.InnerText = "No";
                    ISFBTAPPLICABLE.InnerText = "No";
                    ISGSTAPPLICABLE.InnerText = "No";
                    ISEXCISEAPPLICABLE.InnerText = "No";
                    ISTDSEXPENSE.InnerText = "No";
                    ISEDLIAPPLICABLE.InnerText = "No";
                    ISRELATEDPARTY.InnerText = "No";
                    USEFORESIELIGIBILITY.InnerText = "No";
                    ISINTERESTINCLLASTDAY.InnerText = "No";
                    APPROPRIATETAXVALUE.InnerText = "No";
                    ISBEHAVEASDUTY.InnerText = "No";
                    INTERESTINCLDAYOFADDITION.InnerText = "No";
                    INTERESTINCLDAYOFDEDUCTION.InnerText = "No";
                    ISOTHTERRITORYASSESSEE.InnerText = "No";
                    OVERRIDECREDITLIMIT.InnerText = "No";
                    ISAGAINSTFORMC.InnerText = "No";
                    ISCHEQUEPRINTINGENABLED.InnerText = "Yes";
                    ISPAYUPLOAD.InnerText = "No";
                    ISPAYBATCHONLYSAL.InnerText = "No";
                    ISBNFCODESUPPORTED.InnerText = "No";
                    ALLOWEXPORTWITHERRORS.InnerText = "No";
                    CONSIDERPURCHASEFOREXPORT.InnerText = "No";
                    ISTRANSPORTER.InnerText = "No";
                    USEFORNOTIONALITC.InnerText = "No";
                    ISECOMMOPERATOR.InnerText = "No";
                    SHOWINPAYSLIP.InnerText = "No";
                    USEFORGRATUITY.InnerText = "No";
                    ISTDSPROJECTED.InnerText = "No";
                    FORSERVICETAX.InnerText = "No";
                    ISINPUTCREDIT.InnerText = "No";
                    ISEXEMPTED.InnerText = "No";
                    ISABATEMENTAPPLICABLE.InnerText = "No";
                    ISSTXPARTY.InnerText = "No";
                    ISSTXNONREALIZEDTYPE.InnerText = "No";
                    ISUSEDFORCVD.InnerText = "No";
                    LEDBELONGSTONONTAXABLE.InnerText = "No";
                    ISEXCISEMERCHANTEXPORTER.InnerText = "No";
                    ISPARTYEXEMPTED.InnerText = "No";
                    ISSEZPARTY.InnerText = "No";
                    TDSDEDUCTEEISSPECIALRATE.InnerText = "No";
                    ISECHEQUESUPPORTED.InnerText = "No";
                    ISEDDSUPPORTED.InnerText = "No";
                    HASECHEQUEDELIVERYMODE.InnerText = "No";
                    HASECHEQUEDELIVERYTO.InnerText = "No";
                    HASECHEQUEPRINTLOCATION.InnerText = "No";
                    HASECHEQUEPAYABLELOCATION.InnerText = "No";
                    HASECHEQUEBANKLOCATION.InnerText = "No";
                    HASEDDDELIVERYMODE.InnerText = "No";
                    HASEDDDELIVERYTO.InnerText = "No";
                    HASEDDPRINTLOCATION.InnerText = "No";
                    HASEDDPAYABLELOCATION.InnerText = "No";
                    HASEDDBANKLOCATION.InnerText = "No";
                    ISEBANKINGENABLED.InnerText = "No";
                    ISEXPORTFILEENCRYPTED.InnerText = "No";
                    ISBATCHENABLED.InnerText = "No";
                    ISPRODUCTCODEBASED.InnerText = "No";
                    HASEDDCITY.InnerText = "No";
                    HASECHEQUECITY.InnerText = "No";
                    ISFILENAMEFORMATSUPPORTED.InnerText = "No";
                    HASCLIENTCODE.InnerText = "No";
                    PAYINSISBATCHAPPLICABLE.InnerText = "No";
                    PAYINSISFILENUMAPP.InnerText = "No";
                    ISSALARYTRANSGROUPEDFORBRS.InnerText = "No";
                    ISEBANKINGSUPPORTED.InnerText = "No";
                    ISSCBUAE.InnerText = "No";
                    ISBANKSTATUSAPP.InnerText = "No";
                    ISSALARYGROUPED.InnerText = "No";
                    USEFORPURCHASETAX.InnerText = "No";
                    AUDITED.InnerText = "No";
                    SORTPOSITION.InnerText = "1000";
                    ALTERID.InnerText = "2259";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "Discount Given";
                    LANGUAGEID.InnerText = "1033";
                }

                //CGST
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode GSTDUTYHEAD = LEDGER.AppendChild(XD.CreateElement("GSTDUTYHEAD"));
                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "CGST";
                    PARENT.InnerText = "Duties & Taxes";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "GST";
                    GSTDUTYHEAD.InnerText = "Central Tax";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "CGST";
                }

                //SGST
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode GSTDUTYHEAD = LEDGER.AppendChild(XD.CreateElement("GSTDUTYHEAD"));
                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "SGST";
                    PARENT.InnerText = "Duties & Taxes";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "GST";
                    GSTDUTYHEAD.InnerText = "State Tax";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "SGST";
                }

                //IGST
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode GSTDUTYHEAD = LEDGER.AppendChild(XD.CreateElement("GSTDUTYHEAD"));
                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "IGST";
                    PARENT.InnerText = "Duties & Taxes";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "GST";
                    GSTDUTYHEAD.InnerText = "Integrated Tax";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "IGST";
                }

                //CESS
                {
                    XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                    XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                    XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                    XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                    XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                    XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                    XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                    XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                    XmlNode GSTDUTYHEAD = LEDGER.AppendChild(XD.CreateElement("GSTDUTYHEAD"));
                    XmlNode CESSVALUATIONMETHOD = LEDGER.AppendChild(XD.CreateElement("CESSVALUATIONMETHOD"));
                    XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                    XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                    XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                    XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                    TALLYMESSAGEUDF.InnerText = "TallyUDF";
                    LEDGERNAME.InnerText = "CESS";
                    PARENT.InnerText = "Duties & Taxes";
                    CURRENCYNAME.InnerText = "₹";
                    TAXTYPE.InnerText = "GST";
                    GSTDUTYHEAD.InnerText = "Cess";
                    CESSVALUATIONMETHOD.InnerText = "  Any";
                    NAMELISTTYPE.InnerText = "String";
                    NAME.InnerText = "CESS";
                }

                //Sundry Debtors (Customers)
                db.connect();
                //                                              0             1                                2                                                                                                                                                                                                                      3             4                                   
                DataTable dtCust = db.GetTable("SELECT  c.[CustomerName],c.[State],(select NetAmt from RemainingPayment where Type='OpeningBalance' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerId=c.CustomerId and CustomerName=c.CustomerName) as Opening,Isnull(c.BuyerTinNo,''), c.CustomerId  FROM [CustomerInformation] as c Where c.CompId='" + CommonMethod.CompId + "' and c.CustomerName!=''");
                for (int i = 0; i < dtCust.Rows.Count; i++)
                {
                    try
                    {
                        if (dtCust.Rows[i][1].ToString() == "" || dtCust.Rows[i][1] == null || dtCust.Rows[i][3] == null || dtCust.Rows[i][3].ToString() == "-1")
                        {
                            CustIDArray.Add(Convert.ToInt32(dtCust.Rows[i][4].ToString()));
                            if (CustIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else if (dtCust.Rows[i][0].ToString().Length == 1)
                        {
                            foreach (var item in specialChar)
                            {
                                if (dtCust.Rows[i][0].ToString().Contains(item))
                                    CustIDArray.Add(Convert.ToInt32(dtCust.Rows[i][4].ToString()));
                            }
                            if (CustIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else
                            goto a;
                    }
                    catch (Exception)
                    {
                        CustIDArray.Add(Convert.ToInt32(dtCust.Rows[i][4].ToString()));
                        if (CustIDArray.Count == 0)
                            goto a;
                        else
                            goto b;
                    }
                a:
                    {
                        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                        XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                        XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                        XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                        XmlNode MAILINGNAMELIST = LEDGER.AppendChild(XD.CreateElement("MAILINGNAME.LIST"));
                        XmlAttribute MAILINGNAMETYPE = MAILINGNAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                        XmlNode MAILINGNAME = MAILINGNAMELIST.AppendChild(XD.CreateElement("MAILINGNAME"));
                        XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                        XmlNode COUNTRYNAME = LEDGER.AppendChild(XD.CreateElement("COUNTRYNAME"));
                        XmlNode GSTREGISTRATIONTYPE = LEDGER.AppendChild(XD.CreateElement("GSTREGISTRATIONTYPE"));
                        XmlNode VATDEALERTYPE = LEDGER.AppendChild(XD.CreateElement("VATDEALERTYPE"));
                        XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                        XmlNode COUNTRYOFRESIDENCE = LEDGER.AppendChild(XD.CreateElement("COUNTRYOFRESIDENCE"));
                        XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                        XmlNode PARTYGSTIN = LEDGER.AppendChild(XD.CreateElement("PARTYGSTIN"));
                        XmlNode LEDSTATENAME = LEDGER.AppendChild(XD.CreateElement("LEDSTATENAME"));
                        XmlNode ISBILLWISEON = LEDGER.AppendChild(XD.CreateElement("ISBILLWISEON"));
                        XmlNode OPENINGBALANCE = LEDGER.AppendChild(XD.CreateElement("OPENINGBALANCE"));
                        XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                        XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                        XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                        XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                        LEDGERNAME.InnerText = Convert.ToString(dtCust.Rows[i][0].ToString());
                        MAILINGNAMETYPE.InnerText = "String";
                        MAILINGNAME.InnerText = Convert.ToString(dtCust.Rows[i][0].ToString());
                        CURRENCYNAME.InnerText = "₹";
                        COUNTRYNAME.InnerText = "India";
                        VATDEALERTYPE.InnerText = "Regular";
                        TAXTYPE.InnerText = "Others";
                        COUNTRYOFRESIDENCE.InnerText = "India";
                        if (Convert.ToString(dtCust.Rows[i][3].ToString()) != "" && Convert.ToString(dtCust.Rows[i][3].ToString()) != null)
                            GSTREGISTRATIONTYPE.InnerText = "Regular";
                        else
                            GSTREGISTRATIONTYPE.InnerText = "Unregistered";
                        PARENT.InnerText = "Sundry Debtors";
                        if (!string.IsNullOrEmpty(Convert.ToString(dtCust.Rows[i][3].ToString())))
                            PARTYGSTIN.InnerText = Convert.ToString(dtCust.Rows[i][3].ToString());
                        LEDSTATENAME.InnerText = Convert.ToString(dtCust.Rows[i][1].ToString());
                        ISBILLWISEON.InnerText = "Yes";
                        OPENINGBALANCE.InnerText = string.IsNullOrEmpty(Convert.ToString(dtCust.Rows[i][2].ToString())) ? "0" : Convert.ToString(dtCust.Rows[i][2].ToString());
                        NAMELISTTYPE.InnerText = "String";
                        NAME.InnerText = Convert.ToString(dtCust.Rows[i][0].ToString());
                    }
                b: { }
                }

                //Sundry Creditors  (Suppliers)
                //                                              0                   1                                                                           2                                                                                                                                                                                                                                               3           4
                DataTable dtSupp = db.GetTable("SELECT  S.[SupplierName],(Select StateName from  StateMaster where Id=S.[StateId]) as StateName ,(select NetAmt from SupplierRemainingPayment where Type='OpeningBalance' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and SupplierId=s.SupplierCode and SupplierName=s.SupplierName) as Opening,Isnull(s.VatNo,''),S.SupplierCode FROM [SupplierInformation] AS S where s.SupplierName!=''");
                for (int i = 0; i < dtSupp.Rows.Count; i++)
                {
                    try
                    {
                        if (dtSupp.Rows[i][1].ToString() == "" || dtSupp.Rows[i][1] == null || dtSupp.Rows[i][3] == null || dtSupp.Rows[i][3].ToString() == "-1")
                        {
                            SuppIDArray.Add(Convert.ToInt32(dtSupp.Rows[i][4].ToString()));
                            if (SuppIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else if (dtSupp.Rows[i][0].ToString().Length == 1)
                        {
                            foreach (var item in specialChar)
                            {
                                if (dtSupp.Rows[i][0].ToString().Contains(item))
                                    SuppIDArray.Add(Convert.ToInt32(dtSupp.Rows[i][4].ToString()));
                            }
                            if (SuppIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else
                            goto a;
                    }
                    catch (Exception)
                    {
                        SuppIDArray.Add(Convert.ToInt32(dtSupp.Rows[i][4].ToString()));
                        if (SuppIDArray.Count == 0)
                            goto a;
                        else
                            goto b;
                    }
                a:
                    {
                        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                        XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                        XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                        XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                        XmlNode MAILINGNAMELIST = LEDGER.AppendChild(XD.CreateElement("MAILINGNAME.LIST"));
                        XmlAttribute MAILINGNAMETYPE = MAILINGNAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                        XmlNode MAILINGNAME = MAILINGNAMELIST.AppendChild(XD.CreateElement("MAILINGNAME"));
                        XmlNode CURRENCYNAME = LEDGER.AppendChild(XD.CreateElement("CURRENCYNAME"));
                        XmlNode COUNTRYNAME = LEDGER.AppendChild(XD.CreateElement("COUNTRYNAME"));
                        XmlNode GSTREGISTRATIONTYPE = LEDGER.AppendChild(XD.CreateElement("GSTREGISTRATIONTYPE"));
                        XmlNode VATDEALERTYPE = LEDGER.AppendChild(XD.CreateElement("VATDEALERTYPE"));
                        XmlNode TAXTYPE = LEDGER.AppendChild(XD.CreateElement("TAXTYPE"));
                        XmlNode COUNTRYOFRESIDENCE = LEDGER.AppendChild(XD.CreateElement("COUNTRYOFRESIDENCE"));
                        XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                        XmlNode PARTYGSTIN = LEDGER.AppendChild(XD.CreateElement("PARTYGSTIN"));
                        XmlNode LEDSTATENAME = LEDGER.AppendChild(XD.CreateElement("LEDSTATENAME"));
                        XmlNode ISBILLWISEON = LEDGER.AppendChild(XD.CreateElement("ISBILLWISEON"));
                        XmlNode OPENINGBALANCE = LEDGER.AppendChild(XD.CreateElement("OPENINGBALANCE"));
                        XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                        XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                        XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                        XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                        LEDGERNAME.InnerText = Convert.ToString(dtSupp.Rows[i][0].ToString());
                        MAILINGNAMETYPE.InnerText = "String";
                        MAILINGNAME.InnerText = Convert.ToString(dtSupp.Rows[i][0].ToString());
                        CURRENCYNAME.InnerText = "₹";
                        COUNTRYNAME.InnerText = "India";
                        VATDEALERTYPE.InnerText = "Regular";
                        TAXTYPE.InnerText = "Others";
                        COUNTRYOFRESIDENCE.InnerText = "India";
                        if (Convert.ToString(dtSupp.Rows[i][3].ToString()) != "" && Convert.ToString(dtSupp.Rows[i][3].ToString()) != null)
                            GSTREGISTRATIONTYPE.InnerText = "Regular";
                        else
                            GSTREGISTRATIONTYPE.InnerText = "Unregistered";
                        PARENT.InnerText = "Sundry Creditors";
                        if (!string.IsNullOrEmpty(Convert.ToString(dtSupp.Rows[i][3].ToString())))
                            PARTYGSTIN.InnerText = Convert.ToString(dtSupp.Rows[i][3].ToString());
                        LEDSTATENAME.InnerText = Convert.ToString(dtSupp.Rows[i][1].ToString());
                        ISBILLWISEON.InnerText = "Yes";
                        if (!string.IsNullOrEmpty(Convert.ToString(dtSupp.Rows[i][2].ToString())))
                            OPENINGBALANCE.InnerText = "-" + Convert.ToString(dtSupp.Rows[i][2].ToString());
                        NAMELISTTYPE.InnerText = "String";
                        NAME.InnerText = Convert.ToString(dtSupp.Rows[i][0].ToString());
                    }
                b: { }
                }
                DataTable dtIncome = new DataTable();
                DataTable dtExpense = new DataTable();
                DataTable dtGroupLedgers = db.GetTable("Select Id, ACGName from AccountingGroupMaster where ACGName!=''");
                for (int m = 0; m < dtGroupLedgers.Rows.Count; m++)
                {
                    dtExpense = db.GetTable("SELECT ExpenceName, ExpenceId FROM ExpenceMaster where ACGId=" + Convert.ToInt32(dtGroupLedgers.Rows[m][0].ToString()) + " and ExpenceName!=''");
                    for (int i = 0; i < dtExpense.Rows.Count; i++)
                    {
                        if (dtExpense.Rows[i][0].ToString().Length == 1)
                        {
                            foreach (var item in specialChar)
                            {
                                if (dtExpense.Rows[i][0].ToString().Contains(item))
                                    ExpIDArray.Add(Convert.ToInt32(dtExpense.Rows[i][1].ToString()));
                            }
                            if (ExpIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else
                            goto a;
                    a:
                        {
                            XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                            XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                            XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                            XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                            XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                            XmlNode MAILINGNAMELIST = LEDGER.AppendChild(XD.CreateElement("MAILINGNAME.LIST"));
                            XmlAttribute MAILINGNAMETYPE = MAILINGNAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode MAILINGNAME = MAILINGNAMELIST.AppendChild(XD.CreateElement("MAILINGNAME"));
                            XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                            XmlNode OPENINGBALANCE = LEDGER.AppendChild(XD.CreateElement("OPENINGBALANCE"));
                            XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                            XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                            XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                            TALLYMESSAGEUDF.InnerText = "TallyUDF";
                            LEDGERNAME.InnerText = Convert.ToString(dtExpense.Rows[i][0].ToString());
                            MAILINGNAMETYPE.InnerText = "String";
                            MAILINGNAME.InnerText = Convert.ToString(dtExpense.Rows[i][0].ToString());
                            PARENT.InnerText = dtGroupLedgers.Rows[m][1].ToString();
                            OPENINGBALANCE.InnerText = "0.00";
                            NAMELISTTYPE.InnerText = "String";
                            NAME.InnerText = Convert.ToString(dtExpense.Rows[i][0].ToString());
                            intGroupCount++;
                        }
                    b: { }
                    }

                    dtIncome = db.GetTable("SELECT IncomeName,IncomeId FROM IncomeMaster where ACGId=" + Convert.ToInt32(dtGroupLedgers.Rows[m][0].ToString()) + " and IncomeName!=''");
                    for (int i = 0; i < dtIncome.Rows.Count; i++)
                    {
                        if (dtIncome.Rows[i][0].ToString().Length == 1)
                        {
                            foreach (var item in specialChar)
                            {
                                if (dtIncome.Rows[i][0].ToString().Contains(item))
                                    IncomeIDArray.Add(Convert.ToInt32(dtIncome.Rows[i][1].ToString()));
                            }
                            if (IncomeIDArray.Count == 0)
                                goto j;
                            else
                                goto k;
                        }
                        else
                            goto j;
                    j:
                        {
                            XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                            XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                            XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                            XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                            XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                            XmlNode MAILINGNAMELIST = LEDGER.AppendChild(XD.CreateElement("MAILINGNAME.LIST"));
                            XmlAttribute MAILINGNAMETYPE = MAILINGNAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode MAILINGNAME = MAILINGNAMELIST.AppendChild(XD.CreateElement("MAILINGNAME"));
                            XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                            XmlNode OPENINGBALANCE = LEDGER.AppendChild(XD.CreateElement("OPENINGBALANCE"));
                            XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                            XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                            XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                            TALLYMESSAGEUDF.InnerText = "TallyUDF";
                            LEDGERNAME.InnerText = Convert.ToString(dtIncome.Rows[i][0].ToString());
                            MAILINGNAMETYPE.InnerText = "String";
                            MAILINGNAME.InnerText = Convert.ToString(dtIncome.Rows[i][0].ToString());
                            PARENT.InnerText = dtGroupLedgers.Rows[m][1].ToString();
                            OPENINGBALANCE.InnerText = "0.00";
                            NAMELISTTYPE.InnerText = "String";
                            NAME.InnerText = Convert.ToString(dtIncome.Rows[i][0].ToString());
                            intGroupCount++;
                        }
                    k: { }
                    }
                }
                //Expense
                //DataTable dtExpense = db.GetTable("SELECT ExpenceName, ExpenceId FROM ExpenceMaster where ACGId=(SELECT Id FROM AccountingGroupMaster where ACGName='Indirect Expenses') and ExpenceName!=''");
                ////DataTable dtSupp = db.GetTable("SELECT top 10 S.[SupplierName],(Select StateName from  StateMaster where StateId=S.[StateId]) as StateName  FROM [SupplierInformation] AS S ");

                //for (int i = 0; i < dtExpense.Rows.Count; i++)
                //{
                //    if (dtExpense.Rows[i][0].ToString().Length == 1)
                //    {
                //        foreach (var item in specialChar)
                //        {
                //            //if (dtSupp.Rows[i][0].ToString() == specialChar)
                //            if (dtExpense.Rows[i][0].ToString().Contains(item))
                //                ExpIDArray.Add(Convert.ToInt32(dtExpense.Rows[i][1].ToString()));
                //        }
                //        if (ExpIDArray.Count == 0)
                //            goto a;
                //        else
                //            goto b;
                //    }
                //    else
                //        goto a;
                //a:
                //    {
                //        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                //        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                //        XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                //        XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                //        XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                //        XmlNode MAILINGNAMELIST = LEDGER.AppendChild(XD.CreateElement("MAILINGNAME.LIST"));
                //        XmlAttribute MAILINGNAMETYPE = MAILINGNAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                //        XmlNode MAILINGNAME = MAILINGNAMELIST.AppendChild(XD.CreateElement("MAILINGNAME"));
                //        XmlNode GSTREGISTRATIONTYPE = LEDGER.AppendChild(XD.CreateElement("GSTREGISTRATIONTYPE"));
                //        XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));                        
                //        XmlNode OPENINGBALANCE = LEDGER.AppendChild(XD.CreateElement("OPENINGBALANCE"));
                //        XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                //        XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                //        XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                //        XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                //        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                //        LEDGERNAME.InnerText = Convert.ToString(dtExpense.Rows[i][0].ToString());
                //        MAILINGNAMETYPE.InnerText = "String";
                //        MAILINGNAME.InnerText = Convert.ToString(dtExpense.Rows[i][0].ToString());
                //        GSTREGISTRATIONTYPE.InnerText = "Regular";
                //        PARENT.InnerText = "Indirect Expenses";
                //        OPENINGBALANCE.InnerText = "0.00";
                //        NAMELISTTYPE.InnerText = "String";
                //        NAME.InnerText = Convert.ToString(dtExpense.Rows[i][0].ToString());
                //    }
                //b: { }
                //}

                //Income
                //DataTable dtIncome = db.GetTable("SELECT IncomeName,IncomeId FROM IncomeMaster where ACGId=(SELECT Id FROM AccountingGroupMaster where ACGName='Indirect Incomes') and IncomeName!=''");
                //DataTable dtSupp = db.GetTable("SELECT top 10 S.[SupplierName],(Select StateName from  StateMaster where StateId=S.[StateId]) as StateName  FROM [SupplierInformation] AS S ");

                //for (int i = 0; i < dtIncome.Rows.Count; i++)
                //{
                //    if (dtIncome.Rows[i][0].ToString().Length == 1)
                //    {
                //        foreach (var item in specialChar)
                //        {
                //            //if (dtSupp.Rows[i][0].ToString() == specialChar)
                //            if (dtIncome.Rows[i][0].ToString().Contains(item))
                //                IncomeIDArray.Add(Convert.ToInt32(dtIncome.Rows[i][1].ToString()));
                //        }
                //        if (IncomeIDArray.Count == 0)
                //            goto a;
                //        else
                //            goto b;
                //    }
                //    else
                //        goto a;
                //a:
                //    {
                //        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                //        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                //        XmlNode LEDGER = TALLYMESSAGE.AppendChild(XD.CreateElement("LEDGER"));
                //        XmlAttribute LEDGERNAME = LEDGER.Attributes.Append(XD.CreateAttribute("NAME"));
                //        XmlAttribute LEDGERRESERVEDNAME = LEDGER.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                //        XmlNode MAILINGNAMELIST = LEDGER.AppendChild(XD.CreateElement("MAILINGNAME.LIST"));
                //        XmlAttribute MAILINGNAMETYPE = MAILINGNAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                //        XmlNode MAILINGNAME = MAILINGNAMELIST.AppendChild(XD.CreateElement("MAILINGNAME"));
                //        XmlNode GSTREGISTRATIONTYPE = LEDGER.AppendChild(XD.CreateElement("GSTREGISTRATIONTYPE"));
                //        XmlNode PARENT = LEDGER.AppendChild(XD.CreateElement("PARENT"));
                //        XmlNode OPENINGBALANCE = LEDGER.AppendChild(XD.CreateElement("OPENINGBALANCE"));
                //        XmlNode LANGUAGENAMELIST = LEDGER.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                //        XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                //        XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                //        XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                //        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                //        LEDGERNAME.InnerText = Convert.ToString(dtIncome.Rows[i][0].ToString());
                //        MAILINGNAMETYPE.InnerText = "String";
                //        MAILINGNAME.InnerText = Convert.ToString(dtIncome.Rows[i][0].ToString());
                //        GSTREGISTRATIONTYPE.InnerText = "Regular";
                //        PARENT.InnerText = "Indirect Incomes";
                //        OPENINGBALANCE.InnerText = "0.00";
                //        NAMELISTTYPE.InnerText = "String";
                //        NAME.InnerText = Convert.ToString(dtIncome.Rows[i][0].ToString());
                //    }
                //b: { }
                //}

                //Unit
                DataTable dtUnit = db.GetTable("SELECT [UnitName],Isnull(GSTUnitName,''),UnitID  FROM [Unit] where UnitName!=''");
                for (int i = 0; i < dtUnit.Rows.Count; i++)
                {
                    try
                    {
                        if (dtUnit.Rows[i][1].ToString() == "" || dtUnit.Rows[i][1] == null)
                        {
                            UnitIDArray.Add(Convert.ToInt32(dtUnit.Rows[i][2].ToString()));
                            if (UnitIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else if (dtUnit.Rows[i][0].ToString().Length == 1)
                        {
                            foreach (var item in specialChar)
                            {
                                if (dtUnit.Rows[i][0].ToString().Contains(item))
                                    UnitIDArray.Add(Convert.ToInt32(dtUnit.Rows[i][2].ToString()));
                            }
                            if (UnitIDArray.Count == 0)
                                goto a;
                            else
                                goto b;
                        }
                        else
                            goto a;
                    }
                    catch (Exception)
                    {
                        UnitIDArray.Add(Convert.ToInt32(dtUnit.Rows[i][2].ToString()));
                        if (UnitIDArray.Count == 0)
                            goto a;
                        else
                            goto b;
                    }
                a:
                    {
                        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                        XmlNode UNIT = TALLYMESSAGE.AppendChild(XD.CreateElement("UNIT"));
                        XmlAttribute UNITNAME = UNIT.Attributes.Append(XD.CreateAttribute("NAME"));
                        XmlAttribute UNITRESERVEDNAME = UNIT.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                        XmlNode NAME = UNIT.AppendChild(XD.CreateElement("NAME"));
                        XmlNode GSTREPUOM = UNIT.AppendChild(XD.CreateElement("GSTREPUOM"));
                        XmlNode ISUPDATINGTARGETID = UNIT.AppendChild(XD.CreateElement("ISUPDATINGTARGETID"));
                        XmlNode ASORIGINAL = UNIT.AppendChild(XD.CreateElement("ASORIGINAL"));
                        XmlNode ISGSTEXCLUDED = UNIT.AppendChild(XD.CreateElement("ISGSTEXCLUDED"));
                        XmlNode ISSIMPLEUNIT = UNIT.AppendChild(XD.CreateElement("ISSIMPLEUNIT"));
                        XmlNode DECIMALPLACES = UNIT.AppendChild(XD.CreateElement("DECIMALPLACES"));
                        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                        UNITNAME.InnerText = Convert.ToString(dtUnit.Rows[i][0].ToString()).Replace(" ", "");
                        NAME.InnerText = Convert.ToString(dtUnit.Rows[i][0].ToString()).Replace(" ", "");
                        GSTREPUOM.InnerText = Convert.ToString(dtUnit.Rows[i][1].ToString());
                        ISUPDATINGTARGETID.InnerText = "No";
                        ASORIGINAL.InnerText = "Yes";
                        ISGSTEXCLUDED.InnerText = "No";
                        ISSIMPLEUNIT.InnerText = "Yes";
                        DECIMALPLACES.InnerText = "2";
                    }
                b: { }
                }
                //                                                         0        1           2           3               4
                DataTable dtAlternateUnit = db.GetTable("Select distinct Uniti,AlternateUnit,UnitQty,AlternateUnitQty,ProductCode from ProductInformation where (AlternateUnit<>'' or AlternateUnit is not null) and (UnitQty is not null and AlternateUnitQty is not null)");
                for (int i = 0; i < dtAlternateUnit.Rows.Count; i++)
                {
                    UnitRepository UnitRepo = new UnitRepository();
                    Unit UnitData = new Unit();
                    UnitData = UnitRepo.GetAll().Where(t => t.UnitName.ToString() == dtAlternateUnit.Rows[i][0].ToString()).FirstOrDefault();
                    if (UnitData != null)
                    {
                        try
                        {
                            if (UnitData.GSTUnitName == null || UnitData.GSTUnitName.ToString() == "")
                            {
                                UnitIDArray.Add(Convert.ToInt32(UnitData.UnitID.ToString()));
                                if (UnitIDArray.Count == 0)
                                    goto a;
                                else
                                    goto b;
                            }
                            else
                            {
                                if (Convert.ToDecimal(dtAlternateUnit.Rows[i][2].ToString()) <= 1 || Convert.ToDecimal(dtAlternateUnit.Rows[i][3].ToString()) != 1)
                                {
                                    ProductIDArray.Add(Convert.ToInt32(dtAlternateUnit.Rows[i][4].ToString()));
                                    if (ProductIDArray.Count == 0)
                                        goto a;
                                    else
                                        goto b;
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }

                a:
                    {
                        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                        XmlNode UNIT = TALLYMESSAGE.AppendChild(XD.CreateElement("UNIT"));
                        XmlAttribute UNITNAME = UNIT.Attributes.Append(XD.CreateAttribute("NAME"));
                        XmlAttribute UNITRESERVEDNAME = UNIT.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                        XmlNode NAME = UNIT.AppendChild(XD.CreateElement("NAME"));
                        XmlNode BASEUNITS = UNIT.AppendChild(XD.CreateElement("BASEUNITS"));
                        XmlNode ADDITIONALUNITS = UNIT.AppendChild(XD.CreateElement("ADDITIONALUNITS"));
                        XmlNode ISSIMPLEUNIT = UNIT.AppendChild(XD.CreateElement("ISSIMPLEUNIT"));
                        XmlNode CONVERSION = UNIT.AppendChild(XD.CreateElement("CONVERSION"));
                        TALLYMESSAGEUDF.InnerText = "TallyUDF";

                        if (Convert.ToDecimal(dtAlternateUnit.Rows[i][2].ToString()) == Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtAlternateUnit.Rows[i][2].ToString()), 0).ToString()))
                        {
                            UNITNAME.InnerText = dtAlternateUnit.Rows[i][1].ToString() + " of " + Math.Round(Convert.ToDecimal(dtAlternateUnit.Rows[i][2].ToString()), 0).ToString() + " " + dtAlternateUnit.Rows[i][0].ToString();
                            NAME.InnerText = dtAlternateUnit.Rows[i][1].ToString() + " of " + Math.Round(Convert.ToDecimal(dtAlternateUnit.Rows[i][2].ToString()), 0).ToString() + " " + dtAlternateUnit.Rows[i][0].ToString();
                            CONVERSION.InnerText = Math.Round(Convert.ToDecimal(dtAlternateUnit.Rows[i][2].ToString()), 0).ToString();
                        }
                        else
                        {
                            UNITNAME.InnerText = dtAlternateUnit.Rows[i][1].ToString() + " of " + dtAlternateUnit.Rows[i][2].ToString() + " " + dtAlternateUnit.Rows[i][0].ToString();
                            NAME.InnerText = dtAlternateUnit.Rows[i][1].ToString() + " of " + dtAlternateUnit.Rows[i][2].ToString() + " " + dtAlternateUnit.Rows[i][0].ToString();
                            CONVERSION.InnerText = dtAlternateUnit.Rows[i][2].ToString();
                        }
                        BASEUNITS.InnerText = Convert.ToString(dtAlternateUnit.Rows[i][1].ToString());
                        ADDITIONALUNITS.InnerText = Convert.ToString(dtAlternateUnit.Rows[i][0].ToString());
                        ISSIMPLEUNIT.InnerText = "No";
                    }
                b: { }
                }

                //Manufacturing Company
                DataTable dtmfgCompany = db.GetTable("SELECT [CompanyName],CompanyId  FROM [ManuCompanyInfo] where [CompanyName]!=''");
                for (int i = 0; i < dtmfgCompany.Rows.Count; i++)
                {
                    if (dtmfgCompany.Rows[i][0].ToString().Length == 1)
                    {
                        foreach (var item in specialChar)
                        {
                            if (dtmfgCompany.Rows[i][0].ToString().Contains(item))
                                MFGIDArray.Add(Convert.ToInt32(dtmfgCompany.Rows[i][1].ToString()));
                        }
                        if (MFGIDArray.Count == 0)
                            goto a;
                        else
                            goto b;
                    }
                    else
                        goto a;
                a:
                    {
                        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                        XmlNode STOCKGROUP = TALLYMESSAGE.AppendChild(XD.CreateElement("STOCKGROUP"));
                        XmlAttribute STOCKGROUPNAME = STOCKGROUP.Attributes.Append(XD.CreateAttribute("NAME"));
                        XmlAttribute STOCKGROUPRESERVEDNAME = STOCKGROUP.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                        XmlNode PARENT = STOCKGROUP.AppendChild(XD.CreateElement("PARENT"));
                        XmlNode COSTINGMETHOD = STOCKGROUP.AppendChild(XD.CreateElement("COSTINGMETHOD"));
                        XmlNode VALUATIONMETHOD = STOCKGROUP.AppendChild(XD.CreateElement("VALUATIONMETHOD"));
                        XmlNode BASEUNITS = STOCKGROUP.AppendChild(XD.CreateElement("BASEUNITS"));
                        XmlNode ADDITIONALUNITS = STOCKGROUP.AppendChild(XD.CreateElement("ADDITIONALUNITS"));
                        XmlNode ISBATCHWISEON = STOCKGROUP.AppendChild(XD.CreateElement("ISBATCHWISEON"));
                        XmlNode ISPERISHABLEON = STOCKGROUP.AppendChild(XD.CreateElement("ISPERISHABLEON"));
                        XmlNode ISADDABLE = STOCKGROUP.AppendChild(XD.CreateElement("ISADDABLE"));
                        XmlNode ISUPDATINGTARGETID = STOCKGROUP.AppendChild(XD.CreateElement("ISUPDATINGTARGETID"));
                        XmlNode ASORIGINAL = STOCKGROUP.AppendChild(XD.CreateElement("ASORIGINAL"));
                        XmlNode IGNOREPHYSICALDIFFERENCE = STOCKGROUP.AppendChild(XD.CreateElement("IGNOREPHYSICALDIFFERENCE"));
                        XmlNode IGNORENEGATIVESTOCK = STOCKGROUP.AppendChild(XD.CreateElement("IGNORENEGATIVESTOCK"));
                        XmlNode TREATSALESASMANUFACTURED = STOCKGROUP.AppendChild(XD.CreateElement("TREATSALESASMANUFACTURED"));
                        XmlNode TREATPURCHASESASCONSUMED = STOCKGROUP.AppendChild(XD.CreateElement("TREATPURCHASESASCONSUMED"));
                        XmlNode TREATREJECTSASSCRAP = STOCKGROUP.AppendChild(XD.CreateElement("TREATREJECTSASSCRAP"));
                        XmlNode HASMFGDATE = STOCKGROUP.AppendChild(XD.CreateElement("HASMFGDATE"));
                        XmlNode ALLOWUSEOFEXPIREDITEMS = STOCKGROUP.AppendChild(XD.CreateElement("ALLOWUSEOFEXPIREDITEMS"));
                        XmlNode IGNOREBATCHES = STOCKGROUP.AppendChild(XD.CreateElement("IGNOREBATCHES"));
                        XmlNode IGNOREGODOWNS = STOCKGROUP.AppendChild(XD.CreateElement("IGNOREGODOWNS"));
                        XmlNode LANGUAGENAMELIST = STOCKGROUP.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                        XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                        XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                        XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                        STOCKGROUPNAME.InnerText = Convert.ToString(dtmfgCompany.Rows[i][0].ToString());
                        NAMELISTTYPE.InnerText = "String";
                        COSTINGMETHOD.InnerText = "Avg. Cost";
                        VALUATIONMETHOD.InnerText = "Avg. Price";
                        ISBATCHWISEON.InnerText = "No";
                        ISPERISHABLEON.InnerText = "No";
                        ISADDABLE.InnerText = "No";
                        ISUPDATINGTARGETID.InnerText = "No";
                        ASORIGINAL.InnerText = "Yes";
                        IGNOREPHYSICALDIFFERENCE.InnerText = "No";
                        IGNORENEGATIVESTOCK.InnerText = "No";
                        TREATSALESASMANUFACTURED.InnerText = "No";
                        TREATPURCHASESASCONSUMED.InnerText = "No";
                        TREATREJECTSASSCRAP.InnerText = "No";
                        HASMFGDATE.InnerText = "No";
                        ALLOWUSEOFEXPIREDITEMS.InnerText = "No";
                        IGNOREBATCHES.InnerText = "No";
                        IGNOREGODOWNS.InnerText = "No";
                        NAME.InnerText = Convert.ToString(dtmfgCompany.Rows[i][0].ToString());
                    }
                b: { }
                }

                //Products                                          0               1               2           3               4              5                    6                       7                8              9                       10                  11                          12                13       14         15         16           17         
                //                                                   0                 1             2           3                   4         5                 6                           7                8                                                                                                  9                   10                  11                                                                                      12      13      14          15          16          17             18         19          
                DataTable dtProductName = db.GetTable("SELECT [ProductName],[ManufactureCompany],[Uniti],0 as OpeningQty,0 as Openingvalue ,[Rate],' Applicable' as GSTApplicable,'Goods' as TypeOfSupply,HSNCode,case when IGstPercent=0 and SGstPercent+CGstPercent=0 then 'Nil Rated' else 'Taxable' end as Taxability,SGstPercent as SGST,CGstPercent as CGST,case when IGstPercent=0 then (SGstPercent+CGstPercent) else IGstPercent end as IGST ,Cess,decription,AlternateUnit,UnitQty,AlternateUnitQty,GSTUnitName,ProductCode FROM [ProductInformation] ,Unit where [ProductName]!='' and [ManufactureCompany]!='' and [Uniti]!='' and UnitName=Uniti");
                for (int i = 0; i < dtProductName.Rows.Count; i++)
                {
                    if (dtProductName.Rows[i][0].ToString().Length == 1)
                    {
                        foreach (var item in specialChar)
                        {
                            if (dtProductName.Rows[i][0].ToString().Contains(item))
                                ProductIDArray.Add(Convert.ToInt32(dtProductName.Rows[i][19].ToString()));
                        }
                        if (ProductIDArray.Count == 0)
                            goto a;
                        else
                            goto b;
                    }
                    else
                        goto a;
                a:
                    {
                        string pname = dtProductName.Rows[i][0].ToString().Replace("'", "''");
                        string cname = dtProductName.Rows[i][1].ToString();
                        string uname = dtProductName.Rows[i][2].ToString();
                        DataTable dtGSTProductWise = new DataTable();
                        if (MSetData.CommonProduct == "Y" || string.IsNullOrEmpty(MSetData.CommonProduct))
                            //0           1           2       3    4   5    6             7           8                                                                               9
                            dtGSTProductWise = db.GetTable("SELECT ProductCode,ProductName,MFGCompany,Unit,CGST,SGST,IGST,ApllicableFromDate,CompId,case when IGST=0 and CGST+SGST=0 then 'Nil Rated' else 'Taxable' end as Taxability FROM GSTRateTrackProductWise where ProductName='" + pname + "' and MFGCompany='" + cname + "' and Unit='" + uname + "'");
                        else
                            //0           1           2       3    4   5    6             7           8                                                                               9
                            dtGSTProductWise = db.GetTable("SELECT ProductCode,ProductName,MFGCompany,Unit,CGST,SGST,IGST,ApllicableFromDate,CompId,case when IGST=0 and CGST+SGST=0 then 'Nil Rated' else 'Taxable' end as Taxability FROM GSTRateTrackProductWise where ProductName='" + pname + "' and MFGCompany='" + cname + "' and Unit='" + uname + "' and CompId=" + CommonMethod.CompId + "");
                                           
                        XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                        XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                        XmlNode STOCKITEM = TALLYMESSAGE.AppendChild(XD.CreateElement("STOCKITEM"));
                        XmlAttribute STOCKITEMNAME = STOCKITEM.Attributes.Append(XD.CreateAttribute("NAME"));
                        XmlAttribute STOCKITEMRESERVEDNAME = STOCKITEM.Attributes.Append(XD.CreateAttribute("RESERVEDNAME"));
                        XmlNode PARENT = STOCKITEM.AppendChild(XD.CreateElement("PARENT"));
                        XmlNode CATEGORY = STOCKITEM.AppendChild(XD.CreateElement("CATEGORY"));
                        XmlNode GSTAPPLICABLE = STOCKITEM.AppendChild(XD.CreateElement("GSTAPPLICABLE"));
                        XmlNode TAXCLASSIFICATIONNAME = STOCKITEM.AppendChild(XD.CreateElement("TAXCLASSIFICATIONNAME"));
                        XmlNode GSTTYPEOFSUPPLY = STOCKITEM.AppendChild(XD.CreateElement("GSTTYPEOFSUPPLY"));
                        XmlNode EXCISEAPPLICABILITY = STOCKITEM.AppendChild(XD.CreateElement("EXCISEAPPLICABILITY"));
                        XmlNode SALESTAXCESSAPPLICABLE = STOCKITEM.AppendChild(XD.CreateElement("SALESTAXCESSAPPLICABLE"));
                        XmlNode VATAPPLICABLE = STOCKITEM.AppendChild(XD.CreateElement("VATAPPLICABLE"));
                        XmlNode COSTINGMETHOD = STOCKITEM.AppendChild(XD.CreateElement("COSTINGMETHOD"));
                        XmlNode VALUATIONMETHOD = STOCKITEM.AppendChild(XD.CreateElement("VALUATIONMETHOD"));
                        XmlNode BASEUNITS = STOCKITEM.AppendChild(XD.CreateElement("BASEUNITS"));
                        XmlNode ADDITIONALUNITS = STOCKITEM.AppendChild(XD.CreateElement("ADDITIONALUNITS"));
                        XmlNode VATBASEUNIT = STOCKITEM.AppendChild(XD.CreateElement("VATBASEUNIT"));
                        XmlNode GSTREPUOM = STOCKITEM.AppendChild(XD.CreateElement("GSTREPUOM"));
                        XmlNode ASORIGINAL = STOCKITEM.AppendChild(XD.CreateElement("ASORIGINAL"));
                        XmlNode DENOMINATOR = STOCKITEM.AppendChild(XD.CreateElement("DENOMINATOR"));
                        XmlNode RATEOFVAT = STOCKITEM.AppendChild(XD.CreateElement("RATEOFVAT"));
                        XmlNode VATBASENO = STOCKITEM.AppendChild(XD.CreateElement("VATBASENO"));
                        XmlNode VATTRAILNO = STOCKITEM.AppendChild(XD.CreateElement("VATTRAILNO"));
                        XmlNode VATACTUALRATIO = STOCKITEM.AppendChild(XD.CreateElement("VATACTUALRATIO"));

                        //LoopStart
                        if (dtGSTProductWise.Rows.Count > 0)
                        {
                            for (int a = 0; a < dtGSTProductWise.Rows.Count; a++)
                            {
                                XmlNode GSTDETAILSLIST = STOCKITEM.AppendChild(XD.CreateElement("GSTDETAILS.LIST"));
                                XmlNode APPLICABLEFROM = GSTDETAILSLIST.AppendChild(XD.CreateElement("APPLICABLEFROM"));
                                XmlNode CALCULATIONTYPE = GSTDETAILSLIST.AppendChild(XD.CreateElement("CALCULATIONTYPE"));
                                XmlNode HSNCODE = GSTDETAILSLIST.AppendChild(XD.CreateElement("HSNCODE"));
                                XmlNode HSN = GSTDETAILSLIST.AppendChild(XD.CreateElement("HSN"));
                                XmlNode TAXABILITY = GSTDETAILSLIST.AppendChild(XD.CreateElement("TAXABILITY"));
                                XmlNode ISREVERSECHARGEAPPLICABLE = GSTDETAILSLIST.AppendChild(XD.CreateElement("ISREVERSECHARGEAPPLICABLE"));
                                XmlNode ISNONGSTGOODS = GSTDETAILSLIST.AppendChild(XD.CreateElement("ISNONGSTGOODS"));
                                XmlNode GSTINELIGIBLEITC = GSTDETAILSLIST.AppendChild(XD.CreateElement("GSTINELIGIBLEITC"));
                                XmlNode INCLUDEEXPFORSLABCALC = GSTDETAILSLIST.AppendChild(XD.CreateElement("INCLUDEEXPFORSLABCALC"));
                                XmlNode STATEWISEDETAILSLIST = GSTDETAILSLIST.AppendChild(XD.CreateElement("STATEWISEDETAILS.LIST"));
                                XmlNode STATENAME = STATEWISEDETAILSLIST.AppendChild(XD.CreateElement("STATENAME"));
                                DateTime dt = Convert.ToDateTime(dtGSTProductWise.Rows[a][7].ToString());
                                APPLICABLEFROM.InnerText = String.Format("{0:yyyyMMdd}", dt); //"20180401";
                                CALCULATIONTYPE.InnerText = "On Value";
                                HSNCODE.InnerText = Convert.ToString(dtProductName.Rows[i][8].ToString());
                                HSN.InnerText = Convert.ToString(dtProductName.Rows[i][14].ToString());
                                TAXABILITY.InnerText = Convert.ToString(dtGSTProductWise.Rows[a][9].ToString());
                                ISREVERSECHARGEAPPLICABLE.InnerText = "No";
                                ISNONGSTGOODS.InnerText = "No";
                                GSTINELIGIBLEITC.InnerText = "No";
                                INCLUDEEXPFORSLABCALC.InnerText = "No";
                                STATENAME.InnerText = " Any";

                                XmlNode RATEDETAILSLISTCGST = STATEWISEDETAILSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                XmlNode GSTRATEDUTYHEADCGST = RATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                GSTRATEDUTYHEADCGST.InnerText = "Central Tax";
                                XmlNode GSTRATEVALUATIONTYPECGST = RATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                GSTRATEVALUATIONTYPECGST.InnerText = "Based on Value";
                                XmlNode GSTRATECGST = RATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATE"));
                                GSTRATECGST.InnerText = dtGSTProductWise.Rows[a][4].ToString();

                                XmlNode RATEDETAILSLISTSGST = STATEWISEDETAILSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                XmlNode GSTRATEDUTYHEADSGST = RATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                GSTRATEDUTYHEADSGST.InnerText = "State Tax";
                                XmlNode GSTRATEVALUATIONTYPESGST = RATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                GSTRATEVALUATIONTYPESGST.InnerText = "Based on Value";
                                XmlNode GSTRATESGST = RATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATE"));
                                GSTRATESGST.InnerText = dtGSTProductWise.Rows[a][5].ToString();

                                XmlNode RATEDETAILSLISTIGST = STATEWISEDETAILSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                XmlNode GSTRATEDUTYHEADIGST = RATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                GSTRATEDUTYHEADIGST.InnerText = "Integrated Tax";
                                XmlNode GSTRATEVALUATIONTYPEIGST = RATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                GSTRATEVALUATIONTYPEIGST.InnerText = "Based on Value";
                                XmlNode GSTRATEIGST = RATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATE"));
                                GSTRATEIGST.InnerText = dtGSTProductWise.Rows[a][6].ToString();

                                XmlNode RATEDETAILSLISTCESS = STATEWISEDETAILSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                XmlNode GSTRATEDUTYHEADCESS = RATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                GSTRATEDUTYHEADCESS.InnerText = "Cess";
                                XmlNode GSTRATEVALUATIONTYPECESS = RATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                GSTRATEVALUATIONTYPECESS.InnerText = "Based on Value";
                                XmlNode GSTRATECESS = RATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATE"));
                                GSTRATECESS.InnerText = Convert.ToString(dtProductName.Rows[i][13].ToString());

                                XmlNode RATEDETAILSLISTCESSONQTY = STATEWISEDETAILSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                XmlNode GSTRATEDUTYHEADCESSONQTY = RATEDETAILSLISTCESSONQTY.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                GSTRATEDUTYHEADCESSONQTY.InnerText = "Cess on Qty";
                                XmlNode GSTRATEVALUATIONTYPECESSONQTY = RATEDETAILSLISTCESSONQTY.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                GSTRATEVALUATIONTYPECESSONQTY.InnerText = "Based on Quantity";
                            }
                        }
                        //LoopEnd
                        XmlNode LANGUAGENAMELIST = STOCKITEM.AppendChild(XD.CreateElement("LANGUAGENAME.LIST"));
                        XmlNode NAMELIST = LANGUAGENAMELIST.AppendChild(XD.CreateElement("NAME.LIST"));
                        XmlAttribute NAMELISTTYPE = NAMELIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                        XmlNode NAME = NAMELIST.AppendChild(XD.CreateElement("NAME"));
                        TALLYMESSAGEUDF.InnerText = "TallyUDF";
                        STOCKITEMNAME.InnerText = Convert.ToString(dtProductName.Rows[i][0].ToString());
                        PARENT.InnerText = Convert.ToString(dtProductName.Rows[i][1].ToString());
                        GSTAPPLICABLE.InnerText = Convert.ToString(dtProductName.Rows[i][6].ToString());
                        GSTTYPEOFSUPPLY.InnerText = Convert.ToString(dtProductName.Rows[i][7].ToString());
                        EXCISEAPPLICABILITY.InnerText = Convert.ToString(dtProductName.Rows[i][6].ToString());
                        VATAPPLICABLE.InnerText = Convert.ToString(dtProductName.Rows[i][6].ToString());
                        COSTINGMETHOD.InnerText = "Avg. Cost";
                        VALUATIONMETHOD.InnerText = "Avg. Price";
                        if ((dtProductName.Rows[i][15].ToString() != "" || (!String.IsNullOrEmpty(dtProductName.Rows[i][15].ToString()))) && (dtProductName.Rows[i][16].ToString() != "" || (!String.IsNullOrEmpty(dtProductName.Rows[i][16].ToString()))) && (dtProductName.Rows[i][17].ToString() != "" || (!String.IsNullOrEmpty(dtProductName.Rows[i][17].ToString()))))
                        {
                            if (Convert.ToDecimal(dtProductName.Rows[i][16].ToString()) == Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtProductName.Rows[i][16].ToString()), 0).ToString()))
                                BASEUNITS.InnerText = Convert.ToString(dtProductName.Rows[i][15].ToString() + " of " + Math.Round(Convert.ToDecimal(dtProductName.Rows[i][16].ToString()), 0).ToString() + " " + dtProductName.Rows[i][2].ToString());
                            else
                                BASEUNITS.InnerText = Convert.ToString(dtProductName.Rows[i][15].ToString() + " of " + dtProductName.Rows[i][16].ToString() + " " + dtProductName.Rows[i][2].ToString());
                        }
                        else if (dtProductName.Rows[i][15].ToString() == "" || (!String.IsNullOrEmpty(dtProductName.Rows[i][15].ToString())))
                            BASEUNITS.InnerText = Convert.ToString(dtProductName.Rows[i][2].ToString());
                        else
                        {
                            intProCnt = intProCnt + 1;
                            goto b;
                        }
                        VATBASEUNIT.InnerText = dtProductName.Rows[i][2].ToString();
                        GSTREPUOM.InnerText = dtProductName.Rows[i][18].ToString();
                        ASORIGINAL.InnerText = "Yes";
                        DENOMINATOR.InnerText = "1";
                        RATEOFVAT.InnerText = "0";
                        VATBASENO.InnerText = "1";
                        VATTRAILNO.InnerText = "1";
                        VATACTUALRATIO.InnerText = "1";
                        NAMELISTTYPE.InnerText = "String";
                        NAME.InnerText = Convert.ToString(dtProductName.Rows[i][0].ToString());
                    }
                b: { }
                }
                {
                    if (dtCust.Rows.Count > 0)
                        intCustCount = Convert.ToInt32(dtCust.Rows.Count);
                    else
                        intCustCount = 0;
                    if (dtSupp.Rows.Count > 0)
                        intSuppCount = Convert.ToInt32(dtSupp.Rows.Count);
                    else
                        intSuppCount = 0;
                    if (dtUnit.Rows.Count > 0 || dtAlternateUnit.Rows.Count > 0)
                        intUnitCount = Convert.ToInt32(dtUnit.Rows.Count) + Convert.ToInt32(dtAlternateUnit.Rows.Count);
                    else
                        intUnitCount = 0;
                    if (dtmfgCompany.Rows.Count > 0)
                        intMFGCount = Convert.ToInt32(dtmfgCompany.Rows.Count);
                    else
                        intMFGCount = 0;
                    if (dtProductName.Rows.Count > 0)
                        intProdCount = Convert.ToInt32(dtProductName.Rows.Count) - intProCnt;
                    else
                        intProdCount = 0;
                    totMasterCount.Text = Convert.ToString(dtCust.Rows.Count + dtSupp.Rows.Count + intGroupCount + dtUnit.Rows.Count + dtAlternateUnit.Rows.Count + dtmfgCompany.Rows.Count + dtProductName.Rows.Count - intProCnt + 6);
                }
                if (CustIDArray.Count > 0 || SuppIDArray.Count > 0 || UnitIDArray.Count > 0 || MFGIDArray.Count > 0 || ProductIDArray.Count > 0 || ExpIDArray.Count > 0 || IncomeIDArray.Count > 0)
                {
                    totMasterCount.Text = "0";
                    this.ActiveControl= totMasterCount;
                    goto x;
                }
                else
                    this.ActiveControl = totMasterCount;
                string[] drives = Environment.GetLogicalDrives();
                string FolderName = "Tally Export";
                string folderPath = @"" + drives[0] + "" + FolderName + " \\";
                try
                {
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                }
                catch (Exception)
                { }

                XD.Save(folderPath + "Master.xml");
                string text = File.ReadAllText(folderPath + "\\Master.xml");
                string Newtext1 = text.Replace("&#x4; Applicable", "&#4; Applicable");
                string Newtext2 = Newtext1.Replace("&#x4; Any", "&#4; Any");
                File.WriteAllText(folderPath + "\\Master.xml", Newtext2);
                MessageBox.Show("XML files created Successfully At" + " " + folderPath + "Master.xml. \n For Detail Click on 'MasterCount'. ");
                db.CloseConnection();
                goto y;
            x: { MessageBox.Show("XML files not created. /n For Detail Error Click on 'MasterCount' and See Errors."); }
            y: { }
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

        private void btnTransactionExport_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDateExport.Visible = true;
                this.ActiveControl = dtpfromDate;
                pnlErrorPanel.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dtpfromDate.Value.Date <= dtpToDate.Value.Date)
            {
                pnlDateExport.Visible = false;
                string toDate = cm.ValidToDate(dtpToDate.Value.Date);
                string fromDate = cm.ValidFromDate(dtpfromDate.Value.Date);
                SupplierRepository SupplierRepo = new SupplierRepository();
                CustomerRepository CustomerRepo = new CustomerRepository();
                CompanyInformationRepository CompRepo = new CompanyInformationRepository();
                var CompanyData = CompRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                try
                {
                    decimal CGSTAMT = 0, SGSTAMT = 0, IGSTAMT = 0;
                    XmlDocument XD = new XmlDocument();
                    XmlNode ENVELOPE = XD.AppendChild(XD.CreateElement("ENVELOPE"));
                    XmlNode HEADER = ENVELOPE.AppendChild(XD.CreateElement("HEADER"));
                    XmlNode TALLYREQUEST = HEADER.AppendChild(XD.CreateElement("TALLYREQUEST"));
                    XmlNode BODY = ENVELOPE.AppendChild(XD.CreateElement("BODY"));
                    XmlNode IMPORTDATA = BODY.AppendChild(XD.CreateElement("IMPORTDATA"));
                    XmlNode REQUESTDESC = IMPORTDATA.AppendChild(XD.CreateElement("REQUESTDESC"));
                    XmlNode REPORTNAME = REQUESTDESC.AppendChild(XD.CreateElement("REPORTNAME"));
                    XmlNode REQUESTDATA = IMPORTDATA.AppendChild(XD.CreateElement("REQUESTDATA"));
                    TALLYREQUEST.InnerText = "Import Data";
                    REPORTNAME.InnerText = "Vouchers";
                    DataTable dtCustomerBillMaster = new DataTable(); DataTable dtCustomerBillReturnMaster = new DataTable();
                    DataTable dtPurchaseMaster = new DataTable(); DataTable dtPurchaseReturnMaster = new DataTable();

                    try
                    {
                        db.connect();
                        ////0        1               2               3           4             5
                        //dtPurchaseMaster = db.Gettable("Select distinct pm.Id,pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.PorderNo,pm.SupplierId from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  Where  pm.PorderDate>='" + fromDate + "' and pm.PorderDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and  pd.Quantity is not null and pm.TotalAmount!=0 order by pm.ID");
                        //                                                0          1             2                                                                                              3          4             5                6                  7
                        dtPurchaseMaster = db.Gettable("Select distinct pm.Id,pm.PorderDate,pm.SupplierName,cast(pm.TotalAmount-pm.TotalAmount*(Isnull(cast(pm.Discount as decimal(18,4)),0)/100) as decimal(18,2)) as Amount,pm.PorderNo,pm.SupplierId,Isnull(pm.Discount,0),pm.TotalAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  Where  pm.PorderDate>='" + fromDate + "' and pm.PorderDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and  pd.Quantity is not null and pm.TotalAmount!=0 order by pm.ID");

                        DataTable dtPurchaseMaster1 = db.Gettable("Select distinct pm.Id,pm.PorderNo,pm.SupplierId,pm.SupplierName from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where  pm.PorderDate>='" + fromDate + "' and pm.PorderDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and  pd.Quantity is not null and pm.TotalAmount!=0 and pm.SupplierId not in (Select distinct S.SupplierCode from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation S on pm.SupplierId=S.SupplierCode Where  pm.PorderDate>='" + fromDate + "' and pm.PorderDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and  pd.Quantity is not null and pm.TotalAmount!=0) order by pm.ID");
                        if (dtPurchaseMaster1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPurchaseMaster1.Rows.Count; i++)
                            {
                                PurchaseIDArray.Add(new BillNoIdName() { BillNo = dtPurchaseMaster1.Rows[i][1].ToString(), ID = dtPurchaseMaster1.Rows[i][2].ToString(), Name = dtPurchaseMaster1.Rows[i][3].ToString() });
                            }
                        }
                        barprgrsbar.Visible = true;
                        barprgrsbar.Minimum = 0;
                        barprgrsbar.Maximum = dtPurchaseMaster.Rows.Count;
                        for (int i = 0; i < dtPurchaseMaster.Rows.Count; i++)
                        {
                            barprgrsbar.Value = i;
                            CGSTAMT = 0; SGSTAMT = 0; IGSTAMT = 0;
                            SupplierRepo = new SupplierRepository();
                            int SupCode = Convert.ToInt32(dtPurchaseMaster.Rows[i][5].ToString());
                            string SupName = Convert.ToString(dtPurchaseMaster.Rows[i][2].ToString().Trim());
                            var SupplierData = SupplierRepo.GetAll().Where(t => t.SupplierCode == SupCode && t.SupplierName.Trim() == SupName).FirstOrDefault();

                            XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                            XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                            XmlNode VOUCHER = TALLYMESSAGE.AppendChild(XD.CreateElement("VOUCHER"));
                            XmlAttribute VOUCHERVCHTYPE = VOUCHER.Attributes.Append(XD.CreateAttribute("VCHTYPE"));
                            XmlAttribute VOUCHERACTION = VOUCHER.Attributes.Append(XD.CreateAttribute("ACTION"));
                            XmlAttribute VOUCHEROBJVIEW = VOUCHER.Attributes.Append(XD.CreateAttribute("OBJVIEW"));
                            XmlNode OLDAUDITENTRYIDSLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute OLDAUDITENTRYIDSLISTTYPE = OLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode OLDAUDITENTRYIDS = OLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode DATE = VOUCHER.AppendChild(XD.CreateElement("DATE"));
                            XmlNode REFERENCEDATE = VOUCHER.AppendChild(XD.CreateElement("REFERENCEDATE"));
                            XmlNode STATENAME = VOUCHER.AppendChild(XD.CreateElement("STATENAME"));
                            XmlNode COUNTRYOFRESIDENCE = VOUCHER.AppendChild(XD.CreateElement("COUNTRYOFRESIDENCE"));
                            XmlNode PARTYGSTIN = VOUCHER.AppendChild(XD.CreateElement("PARTYGSTIN"));
                            XmlNode NATUREOFSALES = VOUCHER.AppendChild(XD.CreateElement("NATUREOFSALES"));
                            XmlNode TAXUNITNAME = VOUCHER.AppendChild(XD.CreateElement("TAXUNITNAME"));
                            XmlNode PARTYNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYNAME"));
                            XmlNode VOUCHERTYPENAME = VOUCHER.AppendChild(XD.CreateElement("VOUCHERTYPENAME"));
                            XmlNode REFERENCE = VOUCHER.AppendChild(XD.CreateElement("REFERENCE"));
                            XmlNode VOUCHERNUMBER = VOUCHER.AppendChild(XD.CreateElement("VOUCHERNUMBER"));
                            XmlNode PARTYLEDGERNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYLEDGERNAME"));
                            XmlNode BASICBASEPARTYNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBASEPARTYNAME"));
                            XmlNode FBTPAYMENTTYPE = VOUCHER.AppendChild(XD.CreateElement("FBTPAYMENTTYPE"));
                            XmlNode PERSISTEDVIEW = VOUCHER.AppendChild(XD.CreateElement("PERSISTEDVIEW"));
                            XmlNode TYPEOFEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("TYPEOFEXCISEVOUCHER"));
                            XmlNode PLACEOFSUPPLY = VOUCHER.AppendChild(XD.CreateElement("PLACEOFSUPPLY"));
                            XmlNode CONSIGNEEGSTIN = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEEGSTIN"));
                            XmlNode BASICBUYERNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBUYERNAME"));
                            XmlNode BASICDATETIMEOFINVOICE = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFINVOICE"));
                            XmlNode BASICDATETIMEOFREMOVAL = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFREMOVAL"));
                            XmlNode CONSIGNEESTATENAME = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEESTATENAME"));
                            XmlNode DIFFACTUALQTY = VOUCHER.AppendChild(XD.CreateElement("DIFFACTUALQTY"));
                            XmlNode ISMSTFROMSYNC = VOUCHER.AppendChild(XD.CreateElement("ISMSTFROMSYNC"));
                            XmlNode ASORIGINAL = VOUCHER.AppendChild(XD.CreateElement("ASORIGINAL"));
                            XmlNode AUDITED = VOUCHER.AppendChild(XD.CreateElement("AUDITED"));
                            XmlNode FORJOBCOSTING = VOUCHER.AppendChild(XD.CreateElement("FORJOBCOSTING"));
                            XmlNode ISOPTIONAL = VOUCHER.AppendChild(XD.CreateElement("ISOPTIONAL"));
                            XmlNode EFFECTIVEDATE = VOUCHER.AppendChild(XD.CreateElement("EFFECTIVEDATE"));
                            XmlNode USEFOREXCISE = VOUCHER.AppendChild(XD.CreateElement("USEFOREXCISE"));
                            XmlNode ISFORJOBWORKIN = VOUCHER.AppendChild(XD.CreateElement("ISFORJOBWORKIN"));
                            XmlNode ALLOWCONSUMPTION = VOUCHER.AppendChild(XD.CreateElement("ALLOWCONSUMPTION"));
                            XmlNode USEFORINTEREST = VOUCHER.AppendChild(XD.CreateElement("USEFORINTEREST"));
                            XmlNode USEFORGAINLOSS = VOUCHER.AppendChild(XD.CreateElement("USEFORGAINLOSS"));
                            XmlNode USEFORGODOWNTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORGODOWNTRANSFER"));
                            XmlNode USEFORCOMPOUND = VOUCHER.AppendChild(XD.CreateElement("USEFORCOMPOUND"));
                            XmlNode USEFORSERVICETAX = VOUCHER.AppendChild(XD.CreateElement("USEFORSERVICETAX"));
                            XmlNode ISEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEVOUCHER"));
                            XmlNode EXCISETAXOVERRIDE = VOUCHER.AppendChild(XD.CreateElement("EXCISETAXOVERRIDE"));
                            XmlNode USEFORTAXUNITTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORTAXUNITTRANSFER"));
                            XmlNode EXCISEOPENING = VOUCHER.AppendChild(XD.CreateElement("EXCISEOPENING"));
                            XmlNode USEFORFINALPRODUCTION = VOUCHER.AppendChild(XD.CreateElement("USEFORFINALPRODUCTION"));
                            XmlNode ISTDSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTDSOVERRIDDEN"));
                            XmlNode ISTCSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTCSOVERRIDDEN"));
                            XmlNode ISTDSTCSCASHVCH = VOUCHER.AppendChild(XD.CreateElement("ISTDSTCSCASHVCH"));
                            XmlNode INCLUDEADVPYMTVCH = VOUCHER.AppendChild(XD.CreateElement("INCLUDEADVPYMTVCH"));
                            XmlNode ISSUBWORKSCONTRACT = VOUCHER.AppendChild(XD.CreateElement("ISSUBWORKSCONTRACT"));
                            XmlNode ISVATOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISVATOVERRIDDEN"));
                            XmlNode IGNOREORIGVCHDATE = VOUCHER.AppendChild(XD.CreateElement("IGNOREORIGVCHDATE"));
                            XmlNode ISVATPAIDATCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISVATPAIDATCUSTOMS"));
                            XmlNode ISDECLAREDTOCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISDECLAREDTOCUSTOMS"));
                            XmlNode ISSERVICETAXOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISSERVICETAXOVERRIDDEN"));
                            XmlNode ISISDVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISISDVOUCHER"));
                            XmlNode ISEXCISEOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEOVERRIDDEN"));
                            XmlNode ISEXCISESUPPLYVCH = VOUCHER.AppendChild(XD.CreateElement("ISEXCISESUPPLYVCH"));
                            XmlNode ISGSTOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISGSTOVERRIDDEN"));
                            XmlNode GSTNOTEXPORTED = VOUCHER.AppendChild(XD.CreateElement("GSTNOTEXPORTED"));
                            XmlNode ISVATPRINCIPALACCOUNT = VOUCHER.AppendChild(XD.CreateElement("ISVATPRINCIPALACCOUNT"));
                            XmlNode ISBOENOTAPPLICABLE = VOUCHER.AppendChild(XD.CreateElement("ISBOENOTAPPLICABLE"));
                            XmlNode ISSHIPPINGWITHINSTATE = VOUCHER.AppendChild(XD.CreateElement("ISSHIPPINGWITHINSTATE"));
                            XmlNode ISOVERSEASTOURISTTRANS = VOUCHER.AppendChild(XD.CreateElement("ISOVERSEASTOURISTTRANS"));
                            XmlNode ISDESIGNATEDZONEPARTY = VOUCHER.AppendChild(XD.CreateElement("ISDESIGNATEDZONEPARTY"));
                            XmlNode ISCANCELLED = VOUCHER.AppendChild(XD.CreateElement("ISCANCELLED"));
                            XmlNode HASCASHFLOW = VOUCHER.AppendChild(XD.CreateElement("HASCASHFLOW"));
                            XmlNode ISPOSTDATED = VOUCHER.AppendChild(XD.CreateElement("ISPOSTDATED"));
                            XmlNode USETRACKINGNUMBER = VOUCHER.AppendChild(XD.CreateElement("USETRACKINGNUMBER"));
                            XmlNode ISINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISINVOICE"));
                            XmlNode MFGJOURNAL = VOUCHER.AppendChild(XD.CreateElement("MFGJOURNAL"));
                            XmlNode HASDISCOUNTS = VOUCHER.AppendChild(XD.CreateElement("HASDISCOUNTS"));
                            XmlNode ASPAYSLIP = VOUCHER.AppendChild(XD.CreateElement("ASPAYSLIP"));
                            XmlNode ISCOSTCENTRE = VOUCHER.AppendChild(XD.CreateElement("ISCOSTCENTRE"));
                            XmlNode ISSTXNONREALIZEDVCH = VOUCHER.AppendChild(XD.CreateElement("ISSTXNONREALIZEDVCH"));
                            XmlNode ISEXCISEMANUFACTURERON = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEMANUFACTURERON"));
                            XmlNode ISBLANKCHEQUE = VOUCHER.AppendChild(XD.CreateElement("ISBLANKCHEQUE"));
                            XmlNode ISVOID = VOUCHER.AppendChild(XD.CreateElement("ISVOID"));
                            XmlNode ISONHOLD = VOUCHER.AppendChild(XD.CreateElement("ISONHOLD"));
                            XmlNode ORDERLINESTATUS = VOUCHER.AppendChild(XD.CreateElement("ORDERLINESTATUS"));
                            XmlNode VATISAGNSTCANCSALES = VOUCHER.AppendChild(XD.CreateElement("VATISAGNSTCANCSALES"));
                            XmlNode VATISPURCEXEMPTED = VOUCHER.AppendChild(XD.CreateElement("VATISPURCEXEMPTED"));
                            XmlNode ISVATRESTAXINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISVATRESTAXINVOICE"));
                            XmlNode VATISASSESABLECALCVCH = VOUCHER.AppendChild(XD.CreateElement("VATISASSESABLECALCVCH"));
                            XmlNode ISVATDUTYPAID = VOUCHER.AppendChild(XD.CreateElement("ISVATDUTYPAID"));
                            XmlNode ISDELIVERYSAMEASCONSIGNEE = VOUCHER.AppendChild(XD.CreateElement("ISDELIVERYSAMEASCONSIGNEE"));
                            XmlNode ISDISPATCHSAMEASCONSIGNOR = VOUCHER.AppendChild(XD.CreateElement("ISDISPATCHSAMEASCONSIGNOR"));
                            XmlNode ISDELETED = VOUCHER.AppendChild(XD.CreateElement("ISDELETED"));
                            XmlNode CHANGEVCHMODE = VOUCHER.AppendChild(XD.CreateElement("CHANGEVCHMODE"));
                            XmlNode MASTERID = VOUCHER.AppendChild(XD.CreateElement("MASTERID"));
                            XmlNode EXCLUDEDTAXATIONSLIST = VOUCHER.AppendChild(XD.CreateElement("EXCLUDEDTAXATIONS.LIST"));
                            XmlNode OLDAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode ACCOUNTAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode AUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode DUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode SUPPLEMENTARYDUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                            XmlNode EWAYBILLDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("EWAYBILLDETAILS.LIST"));
                            XmlNode INVOICEDELNOTESLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEDELNOTES.LIST"));
                            XmlNode INVOICEORDERLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEORDERLIST.LIST"));
                            XmlNode INVOICEINDENTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("DIFFAINVOICEINDENTLIST.LIST"));
                            XmlNode ATTENDANCEENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ATTENDANCEENTRIES.LIST"));
                            XmlNode ORIGINVOICEDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("ORIGINVOICEDETAILS.LIST"));
                            XmlNode INVOICEEXPORTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEEXPORTLIST.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTAL = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LDGROLDAUDITENTRYIDSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LDGROLDAUDITENTRYIDSLISTTYPE = LDGROLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LDGROLDAUDITENTRYIDS = LDGROLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVE = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEM = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGER = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVE = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTERED = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMED = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));

                            XmlNode LDGRBILLALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTNAME = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("NAME"));
                            XmlNode LDGRBILLALLOCATIONSLISTBILLTYPE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLTYPE"));
                            XmlNode LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSDEDUCTEEISSPECIALRATE"));
                            XmlNode LDGRBILLALLOCATIONSLISTAMOUNT = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LDGRBILLALLOCATIONSLISTINTERESTCOLLECTIONLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTSTBILLCATEGORIESLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("STBILLCATEGORIES.LIST"));

                            XmlNode LDGRINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGROLDAUDITENTRIESLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LDGRACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LDGRAUDITENTRIESLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LDGRINPUTCRALLOCSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LDGRDUTYHEADDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LDGREXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LDGRRATEDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LDGRSUMMARYALLOCSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LDGRSTPYMTDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LDGREXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LDGRTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LDGRTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LDGRTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LDGRVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LDGRCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LDGRREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LDGRINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LDGRVATITCDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LDGRADVANCETAXDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTALDisc = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LDGROLDAUDITENTRYIDSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LDGROLDAUDITENTRYIDSLISTTYPEDisc = LDGROLDAUDITENTRYIDSLISTDisc.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LDGROLDAUDITENTRYIDSDisc = LDGROLDAUDITENTRYIDSLISTDisc.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));

                            XmlNode BASICRATEOFINVOICETAXLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("BASICRATEOFINVOICETAX.LIST"));
                            XmlAttribute BASICRATEOFINVOICETAXTYPEDisc = BASICRATEOFINVOICETAXLISTDisc.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode BASICRATEOFINVOICETAXDisc = BASICRATEOFINVOICETAXLISTDisc.AppendChild(XD.CreateElement("BASICRATEOFINVOICETAX"));

                            XmlNode LEDGERENTRIESLISTLEDGERNAMETOTALDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVEDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTTOTALDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTVATEXPAMOUNTTOTALDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));

                            XmlNode LDGRBILLALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LDGRINTERESTCOLLECTIONLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGROLDAUDITENTRIESLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LDGRACCOUNTAUDITENTRIESLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LDGRAUDITENTRIESLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LDGRINPUTCRALLOCSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LDGRDUTYHEADDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LDGREXCISEDUTYHEADDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LDGRRATEDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LDGRSUMMARYALLOCSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LDGRSTPYMTDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LDGREXCISEPAYMENTALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LDGRTAXBILLALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LDGRTAXOBJECTALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LDGRTDSEXPENSEALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LDGRVATSTATUTORYDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LDGRCOSTTRACKALLOCATIONSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LDGRREFVOUCHERDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LDGRINVOICEWISEDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LDGRVATITCDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LDGRADVANCETAXDETAILSLISTDisc = LEDGERENTRIESLISTTOTALDisc.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTCGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTCGSTTYPE = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTCGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSCGST = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTCGSTROUNDTYPE = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTCGSTVATEXPAMOUNT = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLBILLALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLINTERESTCOLLECTIONLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLOLDAUDITENTRIESLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLACCOUNTAUDITENTRIESLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTAUDITENTRIESLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTINPUTCRALLOCSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTDUTYHEADDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTEXCISEDUTYHEADDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTRATEDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLSUMMARYALLOCSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLSTPYMTDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLEXCISEPAYMENTALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXBILLALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXOBJECTALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTDSEXPENSEALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTVATSTATUTORYDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLCOSTTRACKALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLREFVOUCHERDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLINVOICEWISEDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLVATITCDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLADVANCETAXDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTSGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTSGSTTYPE = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTSGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSSGST = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTSGSTROUNDTYPE = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSGSTVATEXPAMOUNT = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLBILLALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLINTERESTCOLLECTIONLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLOLDAUDITENTRIESLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLACCOUNTAUDITENTRIESLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTAUDITENTRIESLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTINPUTCRALLOCSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTDUTYHEADDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTEXCISEDUTYHEADDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTRATEDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLSUMMARYALLOCSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLSTPYMTDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLEXCISEPAYMENTALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXBILLALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXOBJECTALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTDSEXPENSEALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTVATSTATUTORYDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLCOSTTRACKALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLREFVOUCHERDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLINVOICEWISEDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLVATITCDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLADVANCETAXDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTIGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTOLDAUDITENTRYIDSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTIGSTLDGROLDAUDITENTRYIDSLISTIGSTTYPE = LEDGERENTRIESLISTIGSTOLDAUDITENTRYIDSLISTIGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSIGST = LEDGERENTRIESLISTIGSTOLDAUDITENTRYIDSLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTIGSTROUNDTYPE = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTIGSTVATEXPAMOUNT = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLBILLALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLINTERESTCOLLECTIONLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLOLDAUDITENTRIESLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLACCOUNTAUDITENTRIESLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTAUDITENTRIESLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTINPUTCRALLOCSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTDUTYHEADDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTEXCISEDUTYHEADDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTRATEDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLSUMMARYALLOCSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLSTPYMTDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLEXCISEPAYMENTALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXBILLALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXOBJECTALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTDSEXPENSEALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTVATSTATUTORYDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLCOSTTRACKALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLREFVOUCHERDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLINVOICEWISEDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLVATITCDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLADVANCETAXDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            MasterId = MasterId + 1;
                            DateTime dt = Convert.ToDateTime(dtPurchaseMaster.Rows[i][1]);
                            TALLYMESSAGEUDF.InnerText = "TallyUDF";
                            VOUCHERVCHTYPE.InnerText = "Purchase";
                            VOUCHERACTION.InnerText = "Create";
                            VOUCHEROBJVIEW.InnerText = "Invoice Voucher View";
                            OLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                            OLDAUDITENTRYIDS.InnerText = "-1";
                            DATE.InnerText = String.Format("{0:yyyyMMdd}", dt); //Convert.ToString(dtPurchaseMaster.Rows[i][1].ToString("yyyyMMdd")); //"20180401";
                            REFERENCEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt);
                            STATENAME.InnerText = SupplierData.StateMaster.StateName.ToString();
                            COUNTRYOFRESIDENCE.InnerText = "India";
                            PARTYGSTIN.InnerText = SupplierData.VatNo.ToString();
                            NATUREOFSALES.InnerText = "Manufacturer";
                            TAXUNITNAME.InnerText = "Default Tax Unit";
                            PARTYNAME.InnerText = SupplierData.SupplierName.ToString();

                            REFERENCE.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][4].ToString());
                            VOUCHERNUMBER.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][4].ToString());
                            VOUCHERTYPENAME.InnerText = "Purchase";
                            PARTYLEDGERNAME.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][2].ToString()); //"AABA JADHAV,ISLAMPUR";
                            BASICBASEPARTYNAME.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][2].ToString()); //"AABA JADHAV,ISLAMPUR";
                            FBTPAYMENTTYPE.InnerText = "Default";
                            PERSISTEDVIEW.InnerText = "Invoice Voucher View";
                            TYPEOFEXCISEVOUCHER.InnerText = "Non-Excise";
                            PLACEOFSUPPLY.InnerText = SupplierData.StateMaster.StateName.ToString();
                            CONSIGNEEGSTIN.InnerText = CompanyData.VatNo.ToString();
                            BASICBUYERNAME.InnerText = CompanyData.CompanyName;
                            BASICDATETIMEOFINVOICE.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            BASICDATETIMEOFREMOVAL.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            CONSIGNEESTATENAME.InnerText = CompanyData.State.ToString();
                            DIFFACTUALQTY.InnerText = "No";
                            ISMSTFROMSYNC.InnerText = "No";
                            ASORIGINAL.InnerText = "No";
                            AUDITED.InnerText = "No";
                            FORJOBCOSTING.InnerText = "No";
                            ISOPTIONAL.InnerText = "No";
                            EFFECTIVEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt);
                            USEFOREXCISE.InnerText = "No";
                            ISFORJOBWORKIN.InnerText = "No";
                            ALLOWCONSUMPTION.InnerText = "No";
                            USEFORINTEREST.InnerText = "No";
                            USEFORGAINLOSS.InnerText = "No";
                            USEFORGODOWNTRANSFER.InnerText = "No";
                            USEFORCOMPOUND.InnerText = "No";
                            USEFORSERVICETAX.InnerText = "No";
                            ISEXCISEVOUCHER.InnerText = "Yes";
                            EXCISETAXOVERRIDE.InnerText = "No";
                            USEFORTAXUNITTRANSFER.InnerText = "No";
                            EXCISEOPENING.InnerText = "No";
                            USEFORFINALPRODUCTION.InnerText = "No";
                            ISTDSOVERRIDDEN.InnerText = "No";
                            ISTCSOVERRIDDEN.InnerText = "No";
                            ISTDSTCSCASHVCH.InnerText = "No";
                            INCLUDEADVPYMTVCH.InnerText = "No";
                            ISSUBWORKSCONTRACT.InnerText = "No";
                            ISVATOVERRIDDEN.InnerText = "No";
                            IGNOREORIGVCHDATE.InnerText = "No";
                            ISVATPAIDATCUSTOMS.InnerText = "No";
                            ISDECLAREDTOCUSTOMS.InnerText = "No";
                            ISSERVICETAXOVERRIDDEN.InnerText = "No";
                            ISISDVOUCHER.InnerText = "No";
                            ISEXCISEOVERRIDDEN.InnerText = "No";
                            ISEXCISESUPPLYVCH.InnerText = "No";
                            ISGSTOVERRIDDEN.InnerText = "No";
                            GSTNOTEXPORTED.InnerText = "No";
                            ISVATPRINCIPALACCOUNT.InnerText = "No";
                            ISBOENOTAPPLICABLE.InnerText = "No";
                            ISSHIPPINGWITHINSTATE.InnerText = "No";
                            ISOVERSEASTOURISTTRANS.InnerText = "No";
                            ISDESIGNATEDZONEPARTY.InnerText = "No";
                            ISCANCELLED.InnerText = "No";
                            HASCASHFLOW.InnerText = "No";
                            ISPOSTDATED.InnerText = "No";
                            USETRACKINGNUMBER.InnerText = "No";
                            ISINVOICE.InnerText = "Yes";
                            MFGJOURNAL.InnerText = "No";
                            HASDISCOUNTS.InnerText = "No";
                            ASPAYSLIP.InnerText = "No";
                            ISCOSTCENTRE.InnerText = "No";
                            ISSTXNONREALIZEDVCH.InnerText = "No";
                            ISEXCISEMANUFACTURERON.InnerText = "No";
                            ISBLANKCHEQUE.InnerText = "No";
                            ISVOID.InnerText = "No";
                            ISONHOLD.InnerText = "No";
                            ORDERLINESTATUS.InnerText = "No";
                            VATISAGNSTCANCSALES.InnerText = "No";
                            VATISPURCEXEMPTED.InnerText = "No";
                            ISVATRESTAXINVOICE.InnerText = "No";
                            VATISASSESABLECALCVCH.InnerText = "No";
                            ISVATDUTYPAID.InnerText = "Yes";
                            ISDELIVERYSAMEASCONSIGNEE.InnerText = "No";
                            ISDISPATCHSAMEASCONSIGNOR.InnerText = "No";
                            ISDELETED.InnerText = "No";
                            CHANGEVCHMODE.InnerText = "No";
                            MASTERID.InnerText = Convert.ToString(MasterId);

                            LDGROLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                            LDGROLDAUDITENTRYIDS.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMETOTAL.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][2].ToString()); //"AABA JADHAV,ISLAMPUR";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVE.InnerText = "No";
                            LEDGERENTRIESLISTLEDGERFROMITEM.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIES.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGER.InnerText = "Yes";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVE.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATTAXALTERED.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMED.InnerText = "No";

                            LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][3].ToString()); //"100.00";
                            LDGRBILLALLOCATIONSLISTNAME.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][4].ToString());
                            LDGRBILLALLOCATIONSLISTBILLTYPE.InnerText = "New Ref";
                            LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE.InnerText = "No";
                            LDGRBILLALLOCATIONSLISTAMOUNT.InnerText = Convert.ToString(dtPurchaseMaster.Rows[i][3].ToString());
                            decimal discAmt = 0;
                            if (Convert.ToDecimal(dtPurchaseMaster.Rows[i][6].ToString()) > 0)
                            {
                                LDGROLDAUDITENTRYIDSLISTTYPEDisc.InnerText = "Number";
                                LDGROLDAUDITENTRYIDSDisc.InnerText = "-1";
                                BASICRATEOFINVOICETAXTYPEDisc.InnerText = "Number";
                                BASICRATEOFINVOICETAXDisc.InnerText = '-' + Convert.ToString(Math.Round(Convert.ToDecimal(dtPurchaseMaster.Rows[i][6].ToString()), 4, MidpointRounding.AwayFromZero));

                                LEDGERENTRIESLISTLEDGERNAMETOTALDisc.InnerText = "Discount Given";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVEDisc.InnerText = "Yes";
                                LEDGERENTRIESLISTLEDGERFROMITEMDisc.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESDisc.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERDisc.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEDisc.InnerText = "Yes";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDDisc.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDDisc.InnerText = "No";

                                discAmt = Convert.ToDecimal(dtPurchaseMaster.Rows[i][7].ToString()) * (Math.Round(Convert.ToDecimal(dtPurchaseMaster.Rows[i][6].ToString()), 4, MidpointRounding.AwayFromZero) / 100);
                                //LEDGERENTRIESLISTAMOUNTTOTALDisc.InnerText = Convert.ToString(Math.Round(Convert.ToDecimal(dtPurchaseMaster.Rows[i][6].ToString()), 4, MidpointRounding.AwayFromZero));
                                //LEDGERENTRIESLISTVATEXPAMOUNTTOTALDisc.InnerText = Convert.ToString(Math.Round(Convert.ToDecimal(dtPurchaseMaster.Rows[i][6].ToString()), 4, MidpointRounding.AwayFromZero));
                                LEDGERENTRIESLISTAMOUNTTOTALDisc.InnerText = Convert.ToString(Math.Round(discAmt, 2, MidpointRounding.AwayFromZero));
                                LEDGERENTRIESLISTVATEXPAMOUNTTOTALDisc.InnerText = Convert.ToString(Math.Round(discAmt, 2, MidpointRounding.AwayFromZero));
                            }

//                                                                                  0           1                                                                                                                                                                                           2                                                                                                                                                                                                                                                                                                                                        3                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         4       5       6       7            8                           9                            10               11            12              13                                            
                            DataTable dtPurchaseDetail = db.GetTable("Select pd.ProductName,pd.Unit,case when pd.Unit=pd.PurchaseUnit then Cast(Cast(pd.Amount as decimal(18,2))/cast(pd.Quantity as decimal(18,2)) as decimal(18,3)) else cast(pd.PurchasePrice as decimal(18,3)) end as Rate, case when pd.Unit=pd.PurchaseUnit then Cast(pd.Quantity as decimal(18,2)) else Cast(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit) as decimal(18,2)) end as Quantity, case when pd.Unit=pd.PurchaseUnit then cast((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100)-(((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) else case when cast(pd.PurchasePrice as decimal(18,2))/cast(pd.Quantity as decimal(18,2))<>cast(pd.Amount as decimal(18,2)) then cast((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100)-(((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) ELSE cast((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100)-(((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) End END as Amount,CGstAmt,SGstAmt,IGstAmt,pd.CompanyName,Isnull(pd.Discount,0)+Isnull(pd.SpcDisc,0),pd.PurchaseUnit,pd.CGstPercent,pd.SGstPercent,pd.IGstPercent from PurchaseMaster as pm inner join purchasedetail as pd on pm.Id=pd.PorderNo Where pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and pd.PorderNo='" + Convert.ToString(dtPurchaseMaster.Rows[i][0].ToString()) + "'");
                            for (int j = 0; j < dtPurchaseDetail.Rows.Count; j++)
                            {
                                ProductRepository ProductRepo = new ProductRepository();
                                ProductInformation ProdData = new ProductInformation();
                                ProdData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(dtPurchaseDetail.Rows[j][0].ToString()) && t.ManufactureCompany == Convert.ToString(dtPurchaseDetail.Rows[j][8].ToString()) && t.Uniti == Convert.ToString(dtPurchaseDetail.Rows[j][1].ToString())).FirstOrDefault();
                                if (ProdData == null)
                                {
                                    PurProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtPurchaseMaster.Rows[i][4].ToString(), ProductName = dtPurchaseDetail.Rows[j][0].ToString() + "," + dtPurchaseDetail.Rows[j][8].ToString() + "," + dtPurchaseDetail.Rows[j][1].ToString() });
                                }
                                if (SupplierData.StateId == CompanyData.StateCode && (Convert.ToDecimal(dtPurchaseDetail.Rows[j][13].ToString()) == 0))
                                    goto q;
                                else if (SupplierData.StateId != CompanyData.StateCode && (Convert.ToDecimal(dtPurchaseDetail.Rows[j][11].ToString()) == 0) && Convert.ToDecimal(dtPurchaseDetail.Rows[j][12].ToString()) == 0)
                                    goto q;
                                else
                                    PurchaseGSTProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtPurchaseMaster.Rows[i][4].ToString(), ProductName = dtPurchaseDetail.Rows[j][0].ToString() + "," + dtPurchaseDetail.Rows[j][8].ToString() + "," + dtPurchaseDetail.Rows[j][1].ToString() });
                            q:
                                {
                                    XmlNode ALLINVENTORYENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ALLINVENTORYENTRIES.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTSTOCKITEMNAME = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("STOCKITEMNAME"));
                                    XmlNode ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISAUTONEGATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISAUTONEGATE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISCUSTOMSCLEARANCE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISTRACKCOMPONENT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKCOMPONENT"));
                                    XmlNode ALLINVENTORYENTRIESLISTISTRACKPRODUCTION = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKPRODUCTION"));

                                    XmlNode ALLINVENTORYENTRIESLISTISPRIMARYITEM = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISPRIMARYITEM"));
                                    XmlNode ALLINVENTORYENTRIESLISSCRAP = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISSCRAP"));
                                    XmlNode ALLINVENTORYENTRIESLISTRATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("RATE"));
                                    XmlNode ALLINVENTORYENTRIESLISTAMOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                    XmlNode ALLINVENTORYENTRIESLISTACTUALQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                    XmlNode ALLINVENTORYENTRIESLISTBILLEDQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                    XmlNode ALLINVENTORYENTRIESLISTDISCOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DISCOUNT"));

                                    XmlNode AINVLISTBATCHALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BATCHALLOCATIONS.LIST"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTGODOWNNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("GODOWNNAME"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTBATCHNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BATCHNAME"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTINDENTNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("INDENTNO"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTORDERNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ORDERNO"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTTRACKINGNUMBER = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("TRACKINGNUMBER"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("DYNAMICCSTISCLEARED"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTAMOUNT = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTACTUALQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTBILLEDQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTADDITIONALDETAILSLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ADDITIONALDETAILS.LIST"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTVOUCHERCOMPONENTLISTLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("VOUCHERCOMPONENTLIST.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACCOUNTINGALLOCATIONS.LIST"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                                    XmlAttribute ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERNAME = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERNAME"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTGSTCLASS = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("GSTCLASS"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTGSTOVRDNNATURE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("GSTOVRDNNATURE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTAMOUNT = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTSERVICETAXDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTBANKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTINTERESTCOLLECTIONLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTACCOUNTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTINPUTCRALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESS = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQty = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQty.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQty.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTSUMMARYALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTSTPYMTDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEPAYMENTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTTAXBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTTAXOBJECTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTTDSEXPENSEALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTVATSTATUTORYDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTCOSTTRACKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTREFVOUCHERDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTINVOICEWISEDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTVATITCDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTADVANCETAXDETAILSSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                                    XmlNode ALLINVENTORYENTRIESLISTDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTSUPPLEMENTARYDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTTAXOBJECTALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTREFVOUCHERDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTEXCISEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXCISEALLOCATIONS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTEXPENSEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXPENSEALLOCATIONS.LIST"));

                                    ALLINVENTORYENTRIESLISTSTOCKITEMNAME.InnerText = dtPurchaseDetail.Rows[j][0].ToString(); //"ANGLE COCK ALCO";
                                    ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE.InnerText = "Yes";
                                    ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE.InnerText = "Yes";
                                    ALLINVENTORYENTRIESLISTISAUTONEGATE.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISTRACKCOMPONENT.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISTRACKPRODUCTION.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISPRIMARYITEM.InnerText = "No";
                                    ALLINVENTORYENTRIESLISSCRAP.InnerText = "No";

                                    ALLINVENTORYENTRIESLISTRATE.InnerText = dtPurchaseDetail.Rows[j][2].ToString() + "/" + dtPurchaseDetail.Rows[j][10].ToString(); //"100.00/NOS";
                                    ALLINVENTORYENTRIESLISTAMOUNT.InnerText = "-" + dtPurchaseDetail.Rows[j][4].ToString(); //"100.00";
                                    UnitRepository UnitRepo = new UnitRepository();
                                    Unit UnitData = new Unit();
                                    string unitname = "";
                                    try
                                    {
                                        unitname = dtPurchaseDetail.Rows[j][10].ToString();
                                    }
                                    catch (Exception)
                                    {
                                        unitname = dtPurchaseDetail.Rows[j][1].ToString();
                                    }

                                    if (dtPurchaseDetail.Rows[j][10].ToString() == dtPurchaseDetail.Rows[j][1].ToString())
                                    {
                                        ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                        ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                    }
                                    else
                                    {
                                        ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                        ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                    }
                                    ALLINVENTORYENTRIESLISTDISCOUNT.InnerText = dtPurchaseDetail.Rows[j][9].ToString();

                                    AINVLISTBATCHALLOCATIONSLISTGODOWNNAME.InnerText = "Main Location";
                                    AINVLISTBATCHALLOCATIONSLISTBATCHNAME.InnerText = "Primary Batch";
                                    AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED.InnerText = "No";
                                    AINVLISTBATCHALLOCATIONSLISTAMOUNT.InnerText = "-" + dtPurchaseDetail.Rows[j][4].ToString(); //"100.00";
                                    if (dtPurchaseDetail.Rows[j][10].ToString() == dtPurchaseDetail.Rows[j][1].ToString())
                                    {
                                        AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                        AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                    }
                                    else
                                    {
                                        AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                        AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = dtPurchaseDetail.Rows[j][3].ToString() + " " + dtPurchaseDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                    }

                                    ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                                    ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS.InnerText = "-1";
                                    ACCOUNTINGALLOCATIONSLISTLEDGERNAME.InnerText = "Purchase";
                                    ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE.InnerText = "Yes";
                                    ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE.InnerText = "Yes";
                                    ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTAMOUNT.InnerText = "-" + dtPurchaseDetail.Rows[j][4].ToString(); //"100.00";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEDUTYHEAD.InnerText = "Central Tax";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEDUTYHEAD.InnerText = "State Tax";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEDUTYHEAD.InnerText = "Integrated Tax";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEDUTYHEAD.InnerText = "Cess";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEDUTYHEAD.InnerText = "Cess on Qty";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEVALUATIONTYPE.InnerText = "Based on Quantity";

                                    if (Convert.ToDecimal(dtPurchaseDetail.Rows[j][5].ToString()) == 0 && Convert.ToDecimal(dtPurchaseDetail.Rows[j][6].ToString()) == 0 && Convert.ToDecimal(dtPurchaseDetail.Rows[j][7].ToString()) == 0)
                                        ACCOUNTINGALLOCATIONSLISTGSTOVRDNNATURE.InnerText = "Purchase Nil Rated";
                                    else
                                    {
                                        ACCOUNTINGALLOCATIONSLISTGSTOVRDNNATURE.InnerText = "Purchase Taxable";
                                        if ((Convert.ToDecimal(dtPurchaseDetail.Rows[j][11].ToString()) != 0 && Convert.ToDecimal(dtPurchaseDetail.Rows[j][12].ToString()) != 0) || Convert.ToDecimal(dtPurchaseDetail.Rows[j][13].ToString()) != 0)
                                        {
                                            if (Convert.ToDecimal(dtPurchaseDetail.Rows[j][11].ToString()) != 0 && Convert.ToDecimal(dtPurchaseDetail.Rows[j][12].ToString()) != 0 && Convert.ToDecimal(dtPurchaseDetail.Rows[j][13].ToString()) == 0)
                                            {
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATE.InnerText = dtPurchaseDetail.Rows[j][11].ToString();
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATE.InnerText = dtPurchaseDetail.Rows[j][12].ToString();
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATE.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Rows[j][11].ToString()) + Convert.ToDecimal(dtPurchaseDetail.Rows[j][12].ToString()));
                                            }
                                            else if (Convert.ToDecimal(dtPurchaseDetail.Rows[j][13].ToString()) != 0)
                                            {
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATE.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Rows[j][13].ToString()) / 2);
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATE.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Rows[j][13].ToString()) / 2);
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATE.InnerText = dtPurchaseDetail.Rows[j][13].ToString();
                                            }
                                        }
                                    }

                                    CGSTAMT = CGSTAMT + Convert.ToDecimal(dtPurchaseDetail.Rows[j][5].ToString());
                                    SGSTAMT = SGSTAMT + Convert.ToDecimal(dtPurchaseDetail.Rows[j][6].ToString());
                                    IGSTAMT = IGSTAMT + Convert.ToDecimal(dtPurchaseDetail.Rows[j][7].ToString());
                                }

                                LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTCGSTTYPE.InnerText = "Number";
                                LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSCGST.InnerText = "-1";
                                LEDGERENTRIESLISTLEDGERNAMECGST.InnerText = "CGST";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVECGST.InnerText = "Yes";
                                LEDGERENTRIESLISTLEDGERFROMITEMCGST.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESCGST.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERCGST.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST.InnerText = "Yes";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST.InnerText = "No";
                                LEDGERENTRIESLISTAMOUNTCGST.InnerText = "-" + Convert.ToString(CGSTAMT);
                                LEDGERENTRIESLISTCGSTVATEXPAMOUNT.InnerText = "-" + Convert.ToString(CGSTAMT);

                                LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTSGSTTYPE.InnerText = "Number";
                                LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSSGST.InnerText = "-1";
                                LEDGERENTRIESLISTLEDGERNAMESGST.InnerText = "SGST";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVESGST.InnerText = "Yes";
                                LEDGERENTRIESLISTLEDGERFROMITEMSGST.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESSGST.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERSGST.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST.InnerText = "Yes";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST.InnerText = "No";
                                LEDGERENTRIESLISTAMOUNTSGST.InnerText = "-" + Convert.ToString(SGSTAMT);
                                LEDGERENTRIESLISTSGSTVATEXPAMOUNT.InnerText = "-" + Convert.ToString(SGSTAMT);

                                LEDGERENTRIESLISTIGSTLDGROLDAUDITENTRYIDSLISTIGSTTYPE.InnerText = "Number";
                                LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSIGST.InnerText = "-1";
                                LEDGERENTRIESLISTLEDGERNAMEIGST.InnerText = "IGST";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST.InnerText = "Yes";
                                LEDGERENTRIESLISTLEDGERFROMITEMIGST.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESIGST.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERIGST.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST.InnerText = "Yes";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST.InnerText = "No";
                                LEDGERENTRIESLISTAMOUNTIGST.InnerText = "-" + Convert.ToString(IGSTAMT);
                                LEDGERENTRIESLISTIGSTVATEXPAMOUNT.InnerText = "-" + Convert.ToString(IGSTAMT);

                                if (Convert.ToDecimal(dtPurchaseMaster.Rows[i][6].ToString()) <= 0)
                                {
                                    if (Convert.ToDecimal(dtPurchaseMaster.Rows[i][3]) != Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(IGstAmt)", "")))
                                    {
                                        LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(IGstAmt)", "")));
                                        LDGRBILLALLOCATIONSLISTAMOUNT.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(IGstAmt)", "")));
                                    }
                                }
                                else
                                {
                                    if ((Convert.ToDecimal(dtPurchaseMaster.Rows[i][3]) + Math.Round(discAmt, 2, MidpointRounding.AwayFromZero)) != Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(IGstAmt)", "")))
                                    {
                                        LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(IGstAmt)", "")) - Math.Round(discAmt, 2, MidpointRounding.AwayFromZero));
                                        LDGRBILLALLOCATIONSLISTAMOUNT.InnerText = Convert.ToString(Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseDetail.Compute("Sum(IGstAmt)", "")) - Math.Round(discAmt, 2, MidpointRounding.AwayFromZero));
                                    }
                                }
                            }
                        }
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }

                    //CustomerBill
                    try
                    {
                        db.connect();
                        //                                                     0         1             2             3             4           5
                        dtCustomerBillMaster = db.GetTable("SELECT distinct cm.Id,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.BillNo,cm.CustomerId FROM CustomerBillMaster as cm inner join CustomerBillDetail as cd on cm.Id=cd.BillNo where cm.Billdate>='" + fromDate + "'  and cm.Billdate<='" + toDate + "' and  cm.BillNo like 'T%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cd.Quantity is not null and cm.TotalAmount!=0 order by cm.Id");
                        DataTable dtCustomerBillMaster1 = db.GetTable("SELECT distinct cm.Id,cm.BillNo,cm.CustomerId,cm.CustomerName FROM CustomerBillMaster as cm inner join CustomerBillDetail as cd on cm.Id=cd.BillNo where cm.Billdate>='" + fromDate + "'  and cm.Billdate<='" + toDate + "' and  cm.BillNo like 'T%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cd.Quantity is not null and cm.TotalAmount!=0 and cm.CustomerId not in (SELECT distinct cm.CustomerId FROM CustomerBillMaster as cm inner join CustomerBillDetail as cd on cm.Id=cd.BillNo inner join CustomerInformation c on cm.CustomerId=c.CustomerID where cm.Billdate>='" + fromDate + "'  and cm.Billdate<='" + toDate + "' and  cm.BillNo like 'T%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cd.Quantity is not null and cm.TotalAmount!=0) order by cm.Id");
                        if (dtCustomerBillMaster1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtCustomerBillMaster1.Rows.Count; i++)
                            {
                                SaleIDArray.Add(new BillNoIdName() { BillNo = dtCustomerBillMaster1.Rows[i][1].ToString(), ID = dtCustomerBillMaster1.Rows[i][2].ToString(), Name = dtCustomerBillMaster1.Rows[i][3].ToString() });
                            }
                        }
                        barprgrsbar.Visible = true;
                        barprgrsbar.Minimum = 0;
                        barprgrsbar.Maximum = dtCustomerBillMaster.Rows.Count;
                        for (int i = 0; i < dtCustomerBillMaster.Rows.Count; i++)
                        {
                            barprgrsbar.Value = i;
                            CGSTAMT = 0; SGSTAMT = 0; IGSTAMT = 0;
                            var CustomerData = CustomerRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(dtCustomerBillMaster.Rows[i][5].ToString()) && t.CustomerName == dtCustomerBillMaster.Rows[i][2].ToString()).FirstOrDefault();

                            XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                            XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                            XmlNode VOUCHER = TALLYMESSAGE.AppendChild(XD.CreateElement("VOUCHER"));
                            XmlAttribute VOUCHERVCHTYPE = VOUCHER.Attributes.Append(XD.CreateAttribute("VCHTYPE"));
                            XmlAttribute VOUCHERACTION = VOUCHER.Attributes.Append(XD.CreateAttribute("ACTION"));
                            XmlAttribute VOUCHEROBJVIEW = VOUCHER.Attributes.Append(XD.CreateAttribute("OBJVIEW"));

                            XmlNode OLDAUDITENTRYIDSLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute OLDAUDITENTRYIDSLISTTYPE = OLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode OLDAUDITENTRYIDS = OLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));

                            XmlNode DATE = VOUCHER.AppendChild(XD.CreateElement("DATE"));
                            XmlNode STATENAME = VOUCHER.AppendChild(XD.CreateElement("STATENAME"));
                            XmlNode COUNTRYOFRESIDENCE = VOUCHER.AppendChild(XD.CreateElement("COUNTRYOFRESIDENCE"));
                            XmlNode PARTYGSTIN = VOUCHER.AppendChild(XD.CreateElement("PARTYGSTIN"));
                            XmlNode TAXUNITNAME = VOUCHER.AppendChild(XD.CreateElement("TAXUNITNAME"));
                            XmlNode PARTYNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYNAME"));
                            XmlNode VOUCHERTYPENAME = VOUCHER.AppendChild(XD.CreateElement("VOUCHERTYPENAME"));
                            XmlNode REFERENCE = VOUCHER.AppendChild(XD.CreateElement("REFERENCE"));
                            XmlNode VOUCHERNUMBER = VOUCHER.AppendChild(XD.CreateElement("VOUCHERNUMBER"));
                            XmlNode PARTYLEDGERNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYLEDGERNAME"));
                            XmlNode BASICBASEPARTYNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBASEPARTYNAME"));
                            XmlNode CSTFORMISSUETYPE = VOUCHER.AppendChild(XD.CreateElement("CSTFORMISSUETYPE"));
                            XmlNode CSTFORMRECVTYPE = VOUCHER.AppendChild(XD.CreateElement("CSTFORMRECVTYPE"));
                            XmlNode FBTPAYMENTTYPE = VOUCHER.AppendChild(XD.CreateElement("FBTPAYMENTTYPE"));
                            XmlNode PERSISTEDVIEW = VOUCHER.AppendChild(XD.CreateElement("PERSISTEDVIEW"));
                            XmlNode TYPEOFEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("TYPEOFEXCISEVOUCHER"));
                            XmlNode PLACEOFSUPPLY = VOUCHER.AppendChild(XD.CreateElement("PLACEOFSUPPLY"));
                            XmlNode CONSIGNEEGSTIN = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEEGSTIN"));
                            XmlNode BASICBUYERNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBUYERNAME"));
                            XmlNode BASICDATETIMEOFINVOICE = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFINVOICE"));
                            XmlNode BASICDATETIMEOFREMOVAL = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFREMOVAL"));
                            XmlNode VCHGSTCLASS = VOUCHER.AppendChild(XD.CreateElement("VCHGSTCLASS"));
                            XmlNode CONSIGNEESTATENAME = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEESTATENAME"));
                            XmlNode DIFFACTUALQTY = VOUCHER.AppendChild(XD.CreateElement("DIFFACTUALQTY"));
                            XmlNode ISMSTFROMSYNC = VOUCHER.AppendChild(XD.CreateElement("ISMSTFROMSYNC"));
                            XmlNode ASORIGINAL = VOUCHER.AppendChild(XD.CreateElement("ASORIGINAL"));
                            XmlNode AUDITED = VOUCHER.AppendChild(XD.CreateElement("AUDITED"));
                            XmlNode FORJOBCOSTING = VOUCHER.AppendChild(XD.CreateElement("FORJOBCOSTING"));
                            XmlNode ISOPTIONAL = VOUCHER.AppendChild(XD.CreateElement("ISOPTIONAL"));
                            XmlNode EFFECTIVEDATE = VOUCHER.AppendChild(XD.CreateElement("EFFECTIVEDATE"));
                            XmlNode USEFOREXCISE = VOUCHER.AppendChild(XD.CreateElement("USEFOREXCISE"));
                            XmlNode ISFORJOBWORKIN = VOUCHER.AppendChild(XD.CreateElement("ISFORJOBWORKIN"));
                            XmlNode ALLOWCONSUMPTION = VOUCHER.AppendChild(XD.CreateElement("ALLOWCONSUMPTION"));
                            XmlNode USEFORINTEREST = VOUCHER.AppendChild(XD.CreateElement("USEFORINTEREST"));
                            XmlNode USEFORGAINLOSS = VOUCHER.AppendChild(XD.CreateElement("USEFORGAINLOSS"));
                            XmlNode USEFORGODOWNTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORGODOWNTRANSFER"));
                            XmlNode USEFORCOMPOUND = VOUCHER.AppendChild(XD.CreateElement("USEFORCOMPOUND"));
                            XmlNode USEFORSERVICETAX = VOUCHER.AppendChild(XD.CreateElement("USEFORSERVICETAX"));
                            XmlNode ISEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEVOUCHER"));
                            XmlNode EXCISETAXOVERRIDE = VOUCHER.AppendChild(XD.CreateElement("EXCISETAXOVERRIDE"));
                            XmlNode USEFORTAXUNITTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORTAXUNITTRANSFER"));
                            XmlNode EXCISEOPENING = VOUCHER.AppendChild(XD.CreateElement("EXCISEOPENING"));
                            XmlNode USEFORFINALPRODUCTION = VOUCHER.AppendChild(XD.CreateElement("USEFORFINALPRODUCTION"));
                            XmlNode ISTDSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTDSOVERRIDDEN"));
                            XmlNode ISTCSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTCSOVERRIDDEN"));
                            XmlNode ISTDSTCSCASHVCH = VOUCHER.AppendChild(XD.CreateElement("ISTDSTCSCASHVCH"));
                            XmlNode INCLUDEADVPYMTVCH = VOUCHER.AppendChild(XD.CreateElement("INCLUDEADVPYMTVCH"));
                            XmlNode ISSUBWORKSCONTRACT = VOUCHER.AppendChild(XD.CreateElement("ISSUBWORKSCONTRACT"));
                            XmlNode ISVATOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISVATOVERRIDDEN"));
                            XmlNode IGNOREORIGVCHDATE = VOUCHER.AppendChild(XD.CreateElement("IGNOREORIGVCHDATE"));
                            XmlNode ISVATPAIDATCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISVATPAIDATCUSTOMS"));
                            XmlNode ISDECLAREDTOCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISDECLAREDTOCUSTOMS"));
                            XmlNode ISSERVICETAXOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISSERVICETAXOVERRIDDEN"));
                            XmlNode ISISDVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISISDVOUCHER"));
                            XmlNode ISEXCISEOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEOVERRIDDEN"));
                            XmlNode ISEXCISESUPPLYVCH = VOUCHER.AppendChild(XD.CreateElement("ISEXCISESUPPLYVCH"));
                            XmlNode ISGSTOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISGSTOVERRIDDEN"));
                            XmlNode GSTNOTEXPORTED = VOUCHER.AppendChild(XD.CreateElement("GSTNOTEXPORTED"));
                            XmlNode ISVATPRINCIPALACCOUNT = VOUCHER.AppendChild(XD.CreateElement("ISVATPRINCIPALACCOUNT"));
                            XmlNode ISBOENOTAPPLICABLE = VOUCHER.AppendChild(XD.CreateElement("ISBOENOTAPPLICABLE"));
                            XmlNode ISSHIPPINGWITHINSTATE = VOUCHER.AppendChild(XD.CreateElement("ISSHIPPINGWITHINSTATE"));
                            XmlNode ISOVERSEASTOURISTTRANS = VOUCHER.AppendChild(XD.CreateElement("ISOVERSEASTOURISTTRANS"));
                            XmlNode ISDESIGNATEDZONEPARTY = VOUCHER.AppendChild(XD.CreateElement("ISDESIGNATEDZONEPARTY"));
                            XmlNode ISCANCELLED = VOUCHER.AppendChild(XD.CreateElement("ISCANCELLED"));
                            XmlNode HASCASHFLOW = VOUCHER.AppendChild(XD.CreateElement("HASCASHFLOW"));
                            XmlNode ISPOSTDATED = VOUCHER.AppendChild(XD.CreateElement("ISPOSTDATED"));
                            XmlNode USETRACKINGNUMBER = VOUCHER.AppendChild(XD.CreateElement("USETRACKINGNUMBER"));
                            XmlNode ISINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISINVOICE"));

                            XmlNode MFGJOURNAL = VOUCHER.AppendChild(XD.CreateElement("MFGJOURNAL"));
                            XmlNode HASDISCOUNTS = VOUCHER.AppendChild(XD.CreateElement("HASDISCOUNTS"));
                            XmlNode ASPAYSLIP = VOUCHER.AppendChild(XD.CreateElement("ASPAYSLIP"));
                            XmlNode ISCOSTCENTRE = VOUCHER.AppendChild(XD.CreateElement("ISCOSTCENTRE"));
                            XmlNode ISSTXNONREALIZEDVCH = VOUCHER.AppendChild(XD.CreateElement("ISSTXNONREALIZEDVCH"));
                            XmlNode ISEXCISEMANUFACTURERON = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEMANUFACTURERON"));
                            XmlNode ISBLANKCHEQUE = VOUCHER.AppendChild(XD.CreateElement("ISBLANKCHEQUE"));
                            XmlNode ISVOID = VOUCHER.AppendChild(XD.CreateElement("ISVOID"));
                            XmlNode ISONHOLD = VOUCHER.AppendChild(XD.CreateElement("ISONHOLD"));

                            XmlNode ORDERLINESTATUS = VOUCHER.AppendChild(XD.CreateElement("ORDERLINESTATUS"));
                            XmlNode VATISAGNSTCANCSALES = VOUCHER.AppendChild(XD.CreateElement("VATISAGNSTCANCSALES"));
                            XmlNode VATISPURCEXEMPTED = VOUCHER.AppendChild(XD.CreateElement("VATISPURCEXEMPTED"));
                            XmlNode ISVATRESTAXINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISVATRESTAXINVOICE"));
                            XmlNode VATISASSESABLECALCVCH = VOUCHER.AppendChild(XD.CreateElement("VATISASSESABLECALCVCH"));
                            XmlNode ISVATDUTYPAID = VOUCHER.AppendChild(XD.CreateElement("ISVATDUTYPAID"));
                            XmlNode ISDELIVERYSAMEASCONSIGNEE = VOUCHER.AppendChild(XD.CreateElement("ISDELIVERYSAMEASCONSIGNEE"));
                            XmlNode ISDISPATCHSAMEASCONSIGNOR = VOUCHER.AppendChild(XD.CreateElement("ISDISPATCHSAMEASCONSIGNOR"));
                            XmlNode ISDELETED = VOUCHER.AppendChild(XD.CreateElement("ISDELETED"));
                            XmlNode CHANGEVCHMODE = VOUCHER.AppendChild(XD.CreateElement("CHANGEVCHMODE"));
                            XmlNode MASTERID = VOUCHER.AppendChild(XD.CreateElement("MASTERID"));

                            XmlNode EXCLUDEDTAXATIONSLIST = VOUCHER.AppendChild(XD.CreateElement("EXCLUDEDTAXATIONS.LIST"));
                            XmlNode OLDAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode ACCOUNTAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode AUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode DUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode SUPPLEMENTARYDUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                            XmlNode EWAYBILLDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("EWAYBILLDETAILS.LIST"));
                            XmlNode INVOICEDELNOTESLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEDELNOTES.LIST"));
                            XmlNode INVOICEORDERLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEORDERLIST.LIST"));
                            XmlNode INVOICEINDENTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEINDENTLIST.LIST"));
                            XmlNode ATTENDANCEENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ATTENDANCEENTRIES.LIST"));
                            XmlNode ORIGINVOICEDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("ORIGINVOICEDETAILS.LIST"));
                            XmlNode INVOICEEXPORTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEEXPORTLIST.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTAL = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTTOTALTYPE = LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDS = LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));

                            XmlNode LDGRBILLALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTNAME = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("NAME"));
                            XmlNode LDGRBILLALLOCATIONSLISTBILLTYPE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLTYPE"));
                            XmlNode LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSDEDUCTEEISSPECIALRATE"));
                            XmlNode LDGRBILLALLOCATIONSLISTAMOUNT = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LDGRBILLALLOCATIONSLISTINTERESTCOLLECTIONLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTSTBILLCATEGORIESLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("STBILLCATEGORIES.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTALINTERESTCOLLECTION = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALOLDAUDITENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALACCOUNTAUDITENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALAUDITENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALINPUTCRALLOCS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALDUTYHEADDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALEXCISEDUTYHEADDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALRATEDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALSUMMARYALLOCS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALSTPYMTDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALEXCISEPAYMENTALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALTAXBILLALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALTAXOBJECTALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALTDSEXPENSEALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALVATSTATUTORYDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALCOSTTRACKALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALREFVOUCHERDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALINVOICEWISEDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALVATITCDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALADVANCETAXDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));
                            //CGST
                            XmlNode LEDGERENTRIESLISTCGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPECGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTCGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTCGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTROUNDTYPECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTCGSTVATEXPAMOUNT = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTCGSTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTBANKALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTBILLALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRIESLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTAUDITENTRIESLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTINPUTCRALLOCSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTDUTYHEADDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTEXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTRATEDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTSUMMARYALLOCSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTSTPYMTDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTEXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTVATITCDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTADVANCETAXDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            //SGST
                            XmlNode LEDGERENTRIESLISTSGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPESGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTSGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTSGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTROUNDTYPESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSGSTVATEXPAMOUNT = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSGSTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTBANKALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTBILLALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTOLDAUDITENTRIESLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTAUDITENTRIESLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTINPUTCRALLOCSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTDUTYHEADDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTEXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTRATEDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTSUMMARYALLOCSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTSTPYMTDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTEXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTVATITCDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTADVANCETAXDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            //IGST
                            XmlNode LEDGERENTRIESLISTIGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPEIGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTIGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTIGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTROUNDTYPEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTIGSTVATEXPAMOUNT = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTIGSTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTBANKALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTBILLALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTOLDAUDITENTRIESLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTAUDITENTRIESLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTINPUTCRALLOCSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTDUTYHEADDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTEXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTRATEDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTSUMMARYALLOCSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTSTPYMTDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTEXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTVATITCDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTADVANCETAXDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            MasterId = MasterId + 1;
                            DateTime dt = Convert.ToDateTime(dtCustomerBillMaster.Rows[i][1]);
                            TALLYMESSAGEUDF.InnerText = "TallyUDF";
                            VOUCHERVCHTYPE.InnerText = "Sales";
                            VOUCHERACTION.InnerText = "Create";
                            VOUCHEROBJVIEW.InnerText = "Invoice Voucher View";
                            OLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                            OLDAUDITENTRYIDS.InnerText = "-1";
                            DATE.InnerText = String.Format("{0:yyyyMMdd}", dt); //Convert.ToString(dtPurchaseMaster.Rows[i][1].ToString("yyyyMMdd")); //"20180401";
                            VOUCHERTYPENAME.InnerText = "Sales";
                            STATENAME.InnerText = CustomerData.State.ToString();
                            COUNTRYOFRESIDENCE.InnerText = "India";
                            try
                            {
                                PARTYGSTIN.InnerText = CustomerData.BuyerTinNo.ToString();
                            }
                            catch (Exception)
                            { PARTYGSTIN.InnerText = ""; }
                            TAXUNITNAME.InnerText = "Default Tax Unit";
                            PARTYNAME.InnerText = CustomerData.CustomerName.ToString();
                            REFERENCE.InnerText = dtCustomerBillMaster.Rows[i][4].ToString();
                            VOUCHERNUMBER.InnerText = dtCustomerBillMaster.Rows[i][4].ToString();
                            TYPEOFEXCISEVOUCHER.InnerText = "Non-Excise";
                            PLACEOFSUPPLY.InnerText = CustomerData.State.ToString();
                            CONSIGNEEGSTIN.InnerText = CompanyData.VatNo.ToString();
                            BASICBUYERNAME.InnerText = CustomerData.CustomerName.ToString();
                            BASICDATETIMEOFINVOICE.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            BASICDATETIMEOFREMOVAL.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            CONSIGNEESTATENAME.InnerText = CompanyData.State.ToString();
                            DIFFACTUALQTY.InnerText = "No";
                            ISMSTFROMSYNC.InnerText = "No";
                            ASORIGINAL.InnerText = "No";
                            AUDITED.InnerText = "No";
                            FORJOBCOSTING.InnerText = "No";
                            ISOPTIONAL.InnerText = "No";
                            EFFECTIVEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt);
                            USEFOREXCISE.InnerText = "No";
                            ISFORJOBWORKIN.InnerText = "No";
                            ALLOWCONSUMPTION.InnerText = "No";
                            USEFORINTEREST.InnerText = "No";
                            USEFORGAINLOSS.InnerText = "No";
                            USEFORGODOWNTRANSFER.InnerText = "No";
                            USEFORCOMPOUND.InnerText = "No";
                            USEFORSERVICETAX.InnerText = "No";
                            ISEXCISEVOUCHER.InnerText = "Yes";
                            EXCISETAXOVERRIDE.InnerText = "No";
                            USEFORTAXUNITTRANSFER.InnerText = "No";
                            EXCISEOPENING.InnerText = "No";
                            USEFORFINALPRODUCTION.InnerText = "No";
                            ISTDSOVERRIDDEN.InnerText = "No";
                            ISTCSOVERRIDDEN.InnerText = "No";
                            ISTDSTCSCASHVCH.InnerText = "No";
                            INCLUDEADVPYMTVCH.InnerText = "No";
                            ISSUBWORKSCONTRACT.InnerText = "No";
                            ISVATOVERRIDDEN.InnerText = "No";
                            IGNOREORIGVCHDATE.InnerText = "No";
                            ISVATPAIDATCUSTOMS.InnerText = "No";
                            ISDECLAREDTOCUSTOMS.InnerText = "No";
                            ISSERVICETAXOVERRIDDEN.InnerText = "No";
                            ISISDVOUCHER.InnerText = "No";
                            ISEXCISEOVERRIDDEN.InnerText = "No";
                            ISEXCISESUPPLYVCH.InnerText = "No";
                            ISGSTOVERRIDDEN.InnerText = "No";
                            GSTNOTEXPORTED.InnerText = "No";
                            ISVATPRINCIPALACCOUNT.InnerText = "No";
                            ISBOENOTAPPLICABLE.InnerText = "No";
                            ISSHIPPINGWITHINSTATE.InnerText = "No";
                            ISOVERSEASTOURISTTRANS.InnerText = "No";
                            ISDESIGNATEDZONEPARTY.InnerText = "No";
                            ISCANCELLED.InnerText = "No";
                            HASCASHFLOW.InnerText = "No";
                            ISPOSTDATED.InnerText = "No";
                            USETRACKINGNUMBER.InnerText = "No";
                            ISINVOICE.InnerText = "Yes";
                            MFGJOURNAL.InnerText = "No";
                            HASDISCOUNTS.InnerText = "No";
                            ASPAYSLIP.InnerText = "No";
                            ISCOSTCENTRE.InnerText = "No";
                            ISSTXNONREALIZEDVCH.InnerText = "No";
                            ISEXCISEMANUFACTURERON.InnerText = "No";
                            ISBLANKCHEQUE.InnerText = "No";
                            ISVOID.InnerText = "No";
                            ISONHOLD.InnerText = "No";
                            ORDERLINESTATUS.InnerText = "No";
                            VATISAGNSTCANCSALES.InnerText = "No";
                            VATISPURCEXEMPTED.InnerText = "No";
                            ISVATRESTAXINVOICE.InnerText = "No";
                            VATISASSESABLECALCVCH.InnerText = "Yes";
                            ISVATDUTYPAID.InnerText = "Yes";
                            ISDELIVERYSAMEASCONSIGNEE.InnerText = "No";
                            ISDISPATCHSAMEASCONSIGNOR.InnerText = "No";
                            ISDELETED.InnerText = "No";
                            CHANGEVCHMODE.InnerText = "No";
                            MASTERID.InnerText = MasterId.ToString();

                            PARTYLEDGERNAME.InnerText = dtCustomerBillMaster.Rows[i][2].ToString(); //"AABA JADHAV,ISLAMPUR";
                            BASICBASEPARTYNAME.InnerText = dtCustomerBillMaster.Rows[i][2].ToString(); //"AABA JADHAV,ISLAMPUR";
                            FBTPAYMENTTYPE.InnerText = "Default";
                            PERSISTEDVIEW.InnerText = "Invoice Voucher View";
                            LEDGERENTRIESLISTTOTALTYPE.InnerText = "Number";
                            LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDS.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMETOTAL.InnerText = dtCustomerBillMaster.Rows[i][2].ToString(); //"AABA JADHAV,ISLAMPUR";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVETOTAL.InnerText = "Yes";
                            LEDGERENTRIESLISTLEDGERFROMITEMTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERTOTAL.InnerText = "Yes";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVETOTAL.InnerText = "Yes";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = "-" + dtCustomerBillMaster.Rows[i][3].ToString(); //"100.00";
                            LDGRBILLALLOCATIONSLISTNAME.InnerText = dtCustomerBillMaster.Rows[i][4].ToString();
                            LDGRBILLALLOCATIONSLISTBILLTYPE.InnerText = "New Ref";
                            LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE.InnerText = "No";
                            LDGRBILLALLOCATIONSLISTAMOUNT.InnerText = "-" + dtCustomerBillMaster.Rows[i][3].ToString(); //"100.00";

                            //                                                          0        1       2                                                                                                                                                                                                                                                                                                                                    3                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 4      5       6       7           8                            9                           10          11              12             13
                            DataTable dtCustomerBillDetail = db.GetTable("Select pd.ProductId,pd.Unit,pd.Rate,case when pd.Unit=pd.SaleUnit then Cast(pd.Quantity as decimal(18,2)) else Cast(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit) as decimal(18,2)) end as Quantity, case when pd.Unit=pd.SaleUnit then cast((pd.Rate*pd.Quantity)-((pd.Rate*pd.Quantity)*Isnull(pd.Discount,0)/100)-(((pd.Rate*pd.Quantity)-((pd.Rate*pd.Quantity)*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) else cast((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100)-(((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) End as Amount,CGstAmt,SGstAmt,IGstAmt,pd.CompanyName,Isnull(pd.Discount,0)+Isnull(pd.SpcDisc,0),pd.SaleUnit,pd.CGstPercent,pd.SGstPercent,pd.IGstPercent  from CustomerBillMaster as pm inner join CustomerBillDetail as pd on pm.Id=pd.BillNo Where pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pd.BillNo='" + Convert.ToString(dtCustomerBillMaster.Rows[i][0].ToString()) + "'");
                            for (int j = 0; j < dtCustomerBillDetail.Rows.Count; j++)
                            {
                                ProductRepository ProductRepo = new ProductRepository();
                                ProductInformation ProdData = new ProductInformation();
                                ProdData = ProductRepo.GetAll().Where(t => t.ProductName == dtCustomerBillDetail.Rows[j][0].ToString() && t.ManufactureCompany == dtCustomerBillDetail.Rows[j][8].ToString() && t.Uniti == dtCustomerBillDetail.Rows[j][1].ToString()).FirstOrDefault();
                                if (ProdData == null)
                                {
                                    SaleProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtCustomerBillMaster.Rows[i][4].ToString(), ProductName = dtCustomerBillDetail.Rows[j][0].ToString() + "," + dtCustomerBillDetail.Rows[j][8].ToString() + "," + dtCustomerBillDetail.Rows[j][1].ToString() });
                                }
                                string MBillDate = String.Format("{0:MM/dd/yyyy}", dt), pdpname = dtCustomerBillDetail.Rows[j][0].ToString(), pdcompname = dtCustomerBillDetail.Rows[j][8].ToString(), pdunit = dtCustomerBillDetail.Rows[j][1].ToString();
                                DataTable dtGSTTrackData = db.GetTable("Select gp.CGST,gp.SGST,gp.IGST from GSTRateTrackProductWise gp,ProductInformation p, CustomerBillMaster cm , CustomerBillDetail cd where cd.BillNo=cm.Id and gp.ProductCode=p.ProductCode and cd.ProductId=p.ProductName and cd.CompanyName=p.ManufactureCompany and cd.Unit=p.Uniti and Convert(varchar(24),gp.ApllicableFromDate, 101)=Convert(varchar(24),'" + MBillDate + "', 101) and cm.BillNo='" + dtCustomerBillMaster.Rows[i][4].ToString() + "' and p.ProductName='" + pdpname + "' and p.ManufactureCompany='" + pdcompname + "' and p.Uniti='" + pdunit + "' and gp.CompId=" + CommonMethod.CompId + " and gp.CompId=cm.CompId");
                                if (dtGSTTrackData.Rows.Count > 0)
                                {
                                    if ((Convert.ToDecimal(dtCustomerBillDetail.Rows[j][11].ToString()) == Convert.ToDecimal(dtGSTTrackData.Rows[0][0].ToString()) && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][12].ToString()) == Convert.ToDecimal(dtGSTTrackData.Rows[0][1].ToString())) || Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) == Convert.ToDecimal(dtGSTTrackData.Rows[0][2].ToString()))
                                    {
                                        if (CustomerData.State.ToString() == CompanyData.State.ToString() && (Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) == 0))
                                            goto q;
                                        else if (CustomerData.State.ToString() != CompanyData.State.ToString() && (Convert.ToDecimal(dtCustomerBillDetail.Rows[j][11].ToString()) == 0) && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][12].ToString()) == 0)
                                            goto q;
                                        else
                                            SaleGSTProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtCustomerBillMaster.Rows[i][4].ToString(), ProductName = dtCustomerBillDetail.Rows[j][0].ToString() + "," + dtCustomerBillDetail.Rows[j][8].ToString() + "," + dtCustomerBillDetail.Rows[j][1].ToString() });
                                    }
                                    else
                                        SaleGSTProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtCustomerBillMaster.Rows[i][4].ToString(), ProductName = dtCustomerBillDetail.Rows[j][0].ToString() + "," + dtCustomerBillDetail.Rows[j][8].ToString() + "," + dtCustomerBillDetail.Rows[j][1].ToString() });
                                }
                            q:
                                {
                                    XmlNode ALLINVENTORYENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ALLINVENTORYENTRIES.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTSTOCKITEMNAME = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("STOCKITEMNAME"));
                                    XmlNode ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISAUTONEGATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISAUTONEGATE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISCUSTOMSCLEARANCE"));
                                    XmlNode ALLINVENTORYENTRIESLISTISTRACKCOMPONENT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKCOMPONENT"));
                                    XmlNode ALLINVENTORYENTRIESLISTISTRACKPRODUCTION = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKPRODUCTION"));
                                    XmlNode ALLINVENTORYENTRIESLISTISPRIMARYITEM = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISPRIMARYITEM"));
                                    XmlNode ALLINVENTORYENTRIESLISTISSCRAP = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISSCRAP"));
                                    XmlNode ALLINVENTORYENTRIESLISTRATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("RATE"));
                                    XmlNode ALLINVENTORYENTRIESLISTAMOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                    XmlNode ALLINVENTORYENTRIESLISTVATASSBLVALUE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("VATASSBLVALUE"));
                                    XmlNode ALLINVENTORYENTRIESLISTACTUALQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                    XmlNode ALLINVENTORYENTRIESLISTBILLEDQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                    XmlNode ALLINVENTORYENTRIESLISTDISCOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DISCOUNT"));

                                    XmlNode AINVLISTBATCHALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BATCHALLOCATIONS.LIST"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTGODOWNNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("GODOWNNAME"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTBATCHNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BATCHNAME"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTINDENTNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("INDENTNO"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTORDERNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ORDERNO"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTTRACKINGNUMBER = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("TRACKINGNUMBER"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("DYNAMICCSTISCLEARED"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTAMOUNT = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTACTUALQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTBILLEDQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTADDITIONALDETAILSLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ADDITIONALDETAILS.LIST"));
                                    XmlNode AINVLISTBATCHALLOCATIONSLISTVOUCHERCOMPONENTLISTLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("VOUCHERCOMPONENTLIST.LIST"));

                                    XmlNode ACCOUNTINGALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACCOUNTINGALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                                    XmlAttribute ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERNAME = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERNAME"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTGSTCLASS = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("GSTCLASS"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTGSTOVRDNNATURE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("GSTOVRDNNATURE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTAMOUNT = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTSERVICETAXDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTBANKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTINTERESTCOLLECTIONLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTACCOUNTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTINPUTCRALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGST.AppendChild(XD.CreateElement("GSTRATE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGST.AppendChild(XD.CreateElement("GSTRATE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGST.AppendChild(XD.CreateElement("GSTRATE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESS = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESS.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQty = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEDUTYHEAD = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQty.AppendChild(XD.CreateElement("GSTRATEDUTYHEAD"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEVALUATIONTYPE = ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQty.AppendChild(XD.CreateElement("GSTRATEVALUATIONTYPE"));

                                    XmlNode ACCOUNTINGALLOCATIONSLISTSUMMARYALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTSTPYMTDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEPAYMENTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTTAXBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTTAXOBJECTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTTDSEXPENSEALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTSVATSTATUTORYDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTCOSTTRACKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTREFVOUCHERDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTINVOICEWISEDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTVATITCDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                                    XmlNode ACCOUNTINGALLOCATIONSLISTADVANCETAXDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                                    XmlNode ALLINVENTORYENTRIESLISTDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTSUPPLEMENTARYDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTTAXOBJECTALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTREFVOUCHERDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTEXCISEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXCISEALLOCATIONS.LIST"));
                                    XmlNode ALLINVENTORYENTRIESLISTEXPENSEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXPENSEALLOCATIONS.LIST"));

                                    XmlNode PAYROLLMODEOFPAYMENTLIST = VOUCHER.AppendChild(XD.CreateElement("PAYROLLMODEOFPAYMENT.LIST"));
                                    XmlNode ATTDRECORDSLIST = VOUCHER.AppendChild(XD.CreateElement("ATTDRECORDS.LIST"));
                                    XmlNode GSTEWAYCONSIGNORADDRESSLIST = VOUCHER.AppendChild(XD.CreateElement("GSTEWAYCONSIGNORADDRESS.LIST"));
                                    XmlNode GSTEWAYCONSIGNEEADDRESSLIST = VOUCHER.AppendChild(XD.CreateElement("GSTEWAYCONSIGNEEADDRESS.LIST"));
                                    XmlNode TEMPGSTRATEDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("TEMPGSTRATEDETAILS.LIST"));

                                    ALLINVENTORYENTRIESLISTSTOCKITEMNAME.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][0].ToString()); //"ANGLE COCK ALCO";
                                    ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISAUTONEGATE.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISTRACKCOMPONENT.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISTRACKPRODUCTION.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISPRIMARYITEM.InnerText = "No";
                                    ALLINVENTORYENTRIESLISTISSCRAP.InnerText = "No";

                                    ALLINVENTORYENTRIESLISTRATE.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][2].ToString()) + "/" + Convert.ToString(dtCustomerBillDetail.Rows[j][10].ToString()); //"100.00/NOS";
                                    ALLINVENTORYENTRIESLISTAMOUNT.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                    ALLINVENTORYENTRIESLISTVATASSBLVALUE.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                    if (dtCustomerBillDetail.Rows[j][10].ToString() == dtCustomerBillDetail.Rows[j][1].ToString())
                                    {
                                        ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                        ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                    }
                                    else
                                    {
                                        ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + dtCustomerBillDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                        ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + dtCustomerBillDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                    }

                                    ALLINVENTORYENTRIESLISTDISCOUNT.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][9].ToString());

                                    AINVLISTBATCHALLOCATIONSLISTGODOWNNAME.InnerText = "Main Location";
                                    AINVLISTBATCHALLOCATIONSLISTBATCHNAME.InnerText = "Primary Batch";
                                    AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED.InnerText = "No";
                                    AINVLISTBATCHALLOCATIONSLISTAMOUNT.InnerText = Convert.ToString(Convert.ToDecimal(dtCustomerBillDetail.Rows[j][2]) * Convert.ToDecimal(dtCustomerBillDetail.Rows[j][3])); //Convert.ToString(dtdtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                    if (dtCustomerBillDetail.Rows[j][10].ToString() == dtCustomerBillDetail.Rows[j][1].ToString())
                                    {
                                        AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                        AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                    }
                                    else
                                    {
                                        AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + dtCustomerBillDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                        AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][3].ToString()) + " " + dtCustomerBillDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                    }
                                    ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                                    ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS.InnerText = "-1";
                                    ACCOUNTINGALLOCATIONSLISTLEDGERNAME.InnerText = "Sales";
                                    if (Convert.ToDecimal(dtCustomerBillDetail.Rows[j][5].ToString()) == 0 && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][6].ToString()) == 0 && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][7].ToString()) == 0)
                                        ACCOUNTINGALLOCATIONSLISTGSTOVRDNNATURE.InnerText = "Sales Nil Rated";
                                    else
                                    {
                                        ACCOUNTINGALLOCATIONSLISTGSTOVRDNNATURE.InnerText = "Sales Taxable";
                                        if ((Convert.ToDecimal(dtCustomerBillDetail.Rows[j][11].ToString()) != 0 && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][12].ToString()) != 0) || Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) != 0)
                                        {
                                            if (Convert.ToDecimal(dtCustomerBillDetail.Rows[j][11].ToString()) != 0 && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][12].ToString()) != 0 && Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) == 0)
                                            {
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATE.InnerText = dtCustomerBillDetail.Rows[j][11].ToString();
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATE.InnerText = dtCustomerBillDetail.Rows[j][12].ToString();
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATE.InnerText = Convert.ToString(Convert.ToDecimal(dtCustomerBillDetail.Rows[j][11].ToString()) + Convert.ToDecimal(dtCustomerBillDetail.Rows[j][12].ToString()));
                                            }
                                            else if (Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) != 0)
                                            {
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATE.InnerText = Convert.ToString(Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) / 2);
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATE.InnerText = Convert.ToString(Convert.ToDecimal(dtCustomerBillDetail.Rows[j][13].ToString()) / 2);
                                                ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATE.InnerText = dtCustomerBillDetail.Rows[j][13].ToString();
                                            }
                                        }
                                    }
                                    ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED.InnerText = "No";
                                    ACCOUNTINGALLOCATIONSLISTAMOUNT.InnerText = Convert.ToString(dtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEDUTYHEAD.InnerText = "Central Tax";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCGSTGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEDUTYHEAD.InnerText = "State Tax";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTSGSTGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEDUTYHEAD.InnerText = "Integrated Tax";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTIGSTGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEDUTYHEAD.InnerText = "Cess";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSGSTRATEVALUATIONTYPE.InnerText = "Based on Value";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEDUTYHEAD.InnerText = "Cess on Qty";
                                    ACCOUNTINGALLOCATIONSLISTRATEDETAILSLISTCESSQtyGSTRATEVALUATIONTYPE.InnerText = "Based on Quantity";
                                    CGSTAMT = CGSTAMT + Convert.ToDecimal(dtCustomerBillDetail.Rows[j][5].ToString());
                                    SGSTAMT = SGSTAMT + Convert.ToDecimal(dtCustomerBillDetail.Rows[j][6].ToString());
                                    IGSTAMT = IGSTAMT + Convert.ToDecimal(dtCustomerBillDetail.Rows[j][7].ToString());
                                }
                                LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPECGST.InnerText = "Number";
                                LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTCGST.InnerText = "-1";
                                LEDGERENTRIESLISTLEDGERNAMECGST.InnerText = "CGST";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVECGST.InnerText = "No";
                                LEDGERENTRIESLISTLEDGERFROMITEMCGST.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESCGST.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERCGST.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST.InnerText = "No";
                                LEDGERENTRIESLISTAMOUNTCGST.InnerText = Convert.ToString(CGSTAMT);
                                LEDGERENTRIESLISTCGSTVATEXPAMOUNT.InnerText = Convert.ToString(CGSTAMT);
                                LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPESGST.InnerText = "Number";
                                LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTSGST.InnerText = "-1";
                                LEDGERENTRIESLISTLEDGERNAMESGST.InnerText = "SGST";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVESGST.InnerText = "No";
                                LEDGERENTRIESLISTLEDGERFROMITEMSGST.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESSGST.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERSGST.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST.InnerText = "No";
                                LEDGERENTRIESLISTAMOUNTSGST.InnerText = Convert.ToString(SGSTAMT);
                                LEDGERENTRIESLISTSGSTVATEXPAMOUNT.InnerText = Convert.ToString(SGSTAMT);

                                LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPEIGST.InnerText = "Number";
                                LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTIGST.InnerText = "-1";
                                LEDGERENTRIESLISTLEDGERNAMEIGST.InnerText = "IGST";
                                LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST.InnerText = "No";
                                LEDGERENTRIESLISTLEDGERFROMITEMIGST.InnerText = "No";
                                LEDGERENTRIESLISTREMOVEZEROENTRIESIGST.InnerText = "No";
                                LEDGERENTRIESLISTISPARTYLEDGERIGST.InnerText = "No";
                                LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST.InnerText = "No";
                                LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST.InnerText = "No";
                                LEDGERENTRIESLISTAMOUNTIGST.InnerText = Convert.ToString(IGSTAMT);
                                LEDGERENTRIESLISTIGSTVATEXPAMOUNT.InnerText = Convert.ToString(IGSTAMT);

                                if (Convert.ToDecimal(dtCustomerBillMaster.Rows[i][3]) != Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(IGstAmt)", "")))
                                    LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = "-" + Convert.ToString(Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillDetail.Compute("Sum(IGstAmt)", "")));
                            }
                        }
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }

                    //Debit Note
                    try
                    {
                        db.connect();
                        //0           1                   2               3                 4               5
                        dtPurchaseReturnMaster = db.GetTable("Select pm.Id,pm.PurchaseReturnDate,pm.SupplierName,pm.TotalAmount,pm.PurchaseReturnNo,pm.SupplierId from PurchaseReturnMaster as pm  Where  pm.PurchaseReturnDate>='" + fromDate + "' and pm.PurchaseReturnDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and pm.TotalAmount!=0 order by CONVERT(VARCHAR(24),pm.PurchaseReturnDate,101),pm.ID");

                        DataTable dtPurchaseReturnMaster1 = db.Gettable("Select pm.Id,pm.PurchaseReturnNo,pm.SupplierId,pm.SupplierName from PurchaseReturnMaster as pm  Where  pm.PurchaseReturnDate>='" + fromDate + "' and pm.PurchaseReturnDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and pm.TotalAmount!=0 and pm.SupplierId not in (Select distinct S.SupplierCode from PurchaseReturnMaster as pm  inner join SupplierInformation S on pm.SupplierId=S.SupplierCode Where  pm.PurchaseReturnDate>='" + fromDate + "' and pm.PurchaseReturnDate<='" + toDate + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and pm.TotalAmount!=0) order by CONVERT(VARCHAR(24),pm.PurchaseReturnDate,101),pm.ID");
                        if (dtPurchaseReturnMaster1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtPurchaseReturnMaster1.Rows.Count; i++)
                            {
                                PurReturnIDArray.Add(new BillNoIdName() { BillNo = dtPurchaseReturnMaster1.Rows[i][1].ToString(), ID = dtPurchaseReturnMaster1.Rows[i][2].ToString(), Name = dtPurchaseReturnMaster1.Rows[i][3].ToString() });
                            }
                        }
                        barprgrsbar.Visible = true;
                        barprgrsbar.Minimum = 0;
                        barprgrsbar.Maximum = dtPurchaseReturnMaster.Rows.Count;
                        for (int i = 0; i < dtPurchaseReturnMaster.Rows.Count; i++)
                        {
                            barprgrsbar.Value = i;
                            CGSTAMT = 0; SGSTAMT = 0; IGSTAMT = 0;
                            var SupplierData = SupplierRepo.GetAll().Where(t => t.SupplierCode == Convert.ToInt32(dtPurchaseReturnMaster.Rows[i][5].ToString()) && t.SupplierName == Convert.ToString(dtPurchaseReturnMaster.Rows[i][2].ToString())).FirstOrDefault();
                            XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                            XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                            XmlNode VOUCHER = TALLYMESSAGE.AppendChild(XD.CreateElement("VOUCHER"));
                            XmlAttribute VOUCHERVCHTYPE = VOUCHER.Attributes.Append(XD.CreateAttribute("VCHTYPE"));
                            XmlAttribute VOUCHERACTION = VOUCHER.Attributes.Append(XD.CreateAttribute("ACTION"));
                            XmlAttribute VOUCHEROBJVIEW = VOUCHER.Attributes.Append(XD.CreateAttribute("OBJVIEW"));

                            XmlNode OLDAUDITENTRYIDSLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute OLDAUDITENTRYIDSLISTTYPE = OLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode OLDAUDITENTRYIDS = OLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode DATE = VOUCHER.AppendChild(XD.CreateElement("DATE"));
                            XmlNode REFERENCEDATE = VOUCHER.AppendChild(XD.CreateElement("REFERENCEDATE"));
                            XmlNode STATENAME = VOUCHER.AppendChild(XD.CreateElement("STATENAME"));
                            XmlNode COUNTRYOFRESIDENCE = VOUCHER.AppendChild(XD.CreateElement("COUNTRYOFRESIDENCE"));
                            XmlNode PARTYGSTIN = VOUCHER.AppendChild(XD.CreateElement("PARTYGSTIN"));
                            XmlNode TAXUNITNAME = VOUCHER.AppendChild(XD.CreateElement("TAXUNITNAME"));
                            XmlNode PARTYNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYNAME"));
                            XmlNode VOUCHERTYPENAME = VOUCHER.AppendChild(XD.CreateElement("VOUCHERTYPENAME"));
                            XmlNode REFERENCE = VOUCHER.AppendChild(XD.CreateElement("REFERENCE"));
                            XmlNode VOUCHERNUMBER = VOUCHER.AppendChild(XD.CreateElement("VOUCHERNUMBER"));
                            XmlNode PARTYLEDGERNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYLEDGERNAME"));
                            XmlNode BASICBASEPARTYNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBASEPARTYNAME"));
                            XmlNode FBTPAYMENTTYPE = VOUCHER.AppendChild(XD.CreateElement("FBTPAYMENTTYPE"));
                            XmlNode PERSISTEDVIEW = VOUCHER.AppendChild(XD.CreateElement("PERSISTEDVIEW"));
                            XmlNode TYPEOFEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("TYPEOFEXCISEVOUCHER"));
                            XmlNode CONSIGNEEGSTIN = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEEGSTIN"));
                            XmlNode BASICBUYERNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBUYERNAME"));
                            XmlNode BASICDATETIMEOFINVOICE = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFINVOICE"));
                            XmlNode BASICDATETIMEOFREMOVAL = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFREMOVAL"));
                            XmlNode CONSIGNEESTATENAME = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEESTATENAME"));
                            XmlNode DIFFACTUALQTY = VOUCHER.AppendChild(XD.CreateElement("DIFFACTUALQTY"));
                            XmlNode ISMSTFROMSYNC = VOUCHER.AppendChild(XD.CreateElement("ISMSTFROMSYNC"));
                            XmlNode ASORIGINAL = VOUCHER.AppendChild(XD.CreateElement("ASORIGINAL"));
                            XmlNode AUDITED = VOUCHER.AppendChild(XD.CreateElement("AUDITED"));
                            XmlNode FORJOBCOSTING = VOUCHER.AppendChild(XD.CreateElement("FORJOBCOSTING"));
                            XmlNode ISOPTIONAL = VOUCHER.AppendChild(XD.CreateElement("ISOPTIONAL"));
                            XmlNode EFFECTIVEDATE = VOUCHER.AppendChild(XD.CreateElement("EFFECTIVEDATE"));
                            XmlNode USEFOREXCISE = VOUCHER.AppendChild(XD.CreateElement("USEFOREXCISE"));
                            XmlNode ISFORJOBWORKIN = VOUCHER.AppendChild(XD.CreateElement("ISFORJOBWORKIN"));
                            XmlNode ALLOWCONSUMPTION = VOUCHER.AppendChild(XD.CreateElement("ALLOWCONSUMPTION"));
                            XmlNode USEFORINTEREST = VOUCHER.AppendChild(XD.CreateElement("USEFORINTEREST"));
                            XmlNode USEFORGAINLOSS = VOUCHER.AppendChild(XD.CreateElement("USEFORGAINLOSS"));
                            XmlNode USEFORGODOWNTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORGODOWNTRANSFER"));
                            XmlNode USEFORCOMPOUND = VOUCHER.AppendChild(XD.CreateElement("USEFORCOMPOUND"));
                            XmlNode USEFORSERVICETAX = VOUCHER.AppendChild(XD.CreateElement("USEFORSERVICETAX"));
                            XmlNode ISEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEVOUCHER"));
                            XmlNode EXCISETAXOVERRIDE = VOUCHER.AppendChild(XD.CreateElement("EXCISETAXOVERRIDE"));
                            XmlNode USEFORTAXUNITTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORTAXUNITTRANSFER"));
                            XmlNode EXCISEOPENING = VOUCHER.AppendChild(XD.CreateElement("EXCISEOPENING"));
                            XmlNode USEFORFINALPRODUCTION = VOUCHER.AppendChild(XD.CreateElement("USEFORFINALPRODUCTION"));
                            XmlNode ISTDSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTDSOVERRIDDEN"));
                            XmlNode ISTCSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTCSOVERRIDDEN"));
                            XmlNode ISTDSTCSCASHVCH = VOUCHER.AppendChild(XD.CreateElement("ISTDSTCSCASHVCH"));
                            XmlNode INCLUDEADVPYMTVCH = VOUCHER.AppendChild(XD.CreateElement("INCLUDEADVPYMTVCH"));
                            XmlNode ISSUBWORKSCONTRACT = VOUCHER.AppendChild(XD.CreateElement("ISSUBWORKSCONTRACT"));
                            XmlNode ISVATOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISVATOVERRIDDEN"));
                            XmlNode IGNOREORIGVCHDATE = VOUCHER.AppendChild(XD.CreateElement("IGNOREORIGVCHDATE"));
                            XmlNode ISVATPAIDATCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISVATPAIDATCUSTOMS"));
                            XmlNode ISDECLAREDTOCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISDECLAREDTOCUSTOMS"));
                            XmlNode ISSERVICETAXOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISSERVICETAXOVERRIDDEN"));
                            XmlNode ISISDVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISISDVOUCHER"));
                            XmlNode ISEXCISEOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEOVERRIDDEN"));
                            XmlNode ISEXCISESUPPLYVCH = VOUCHER.AppendChild(XD.CreateElement("ISEXCISESUPPLYVCH"));
                            XmlNode ISGSTOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISGSTOVERRIDDEN"));
                            XmlNode GSTNOTEXPORTED = VOUCHER.AppendChild(XD.CreateElement("GSTNOTEXPORTED"));
                            XmlNode ISVATPRINCIPALACCOUNT = VOUCHER.AppendChild(XD.CreateElement("ISVATPRINCIPALACCOUNT"));
                            XmlNode ISBOENOTAPPLICABLE = VOUCHER.AppendChild(XD.CreateElement("ISBOENOTAPPLICABLE"));
                            XmlNode ISSHIPPINGWITHINSTATE = VOUCHER.AppendChild(XD.CreateElement("ISSHIPPINGWITHINSTATE"));
                            XmlNode ISOVERSEASTOURISTTRANS = VOUCHER.AppendChild(XD.CreateElement("ISOVERSEASTOURISTTRANS"));
                            XmlNode ISDESIGNATEDZONEPARTY = VOUCHER.AppendChild(XD.CreateElement("ISDESIGNATEDZONEPARTY"));
                            XmlNode ISCANCELLED = VOUCHER.AppendChild(XD.CreateElement("ISCANCELLED"));
                            XmlNode HASCASHFLOW = VOUCHER.AppendChild(XD.CreateElement("HASCASHFLOW"));
                            XmlNode ISPOSTDATED = VOUCHER.AppendChild(XD.CreateElement("ISPOSTDATED"));
                            XmlNode USETRACKINGNUMBER = VOUCHER.AppendChild(XD.CreateElement("USETRACKINGNUMBER"));
                            XmlNode ISINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISINVOICE"));
                            XmlNode MFGJOURNAL = VOUCHER.AppendChild(XD.CreateElement("MFGJOURNAL"));
                            XmlNode HASDISCOUNTS = VOUCHER.AppendChild(XD.CreateElement("HASDISCOUNTS"));
                            XmlNode ASPAYSLIP = VOUCHER.AppendChild(XD.CreateElement("ASPAYSLIP"));
                            XmlNode ISCOSTCENTRE = VOUCHER.AppendChild(XD.CreateElement("ISCOSTCENTRE"));
                            XmlNode ISSTXNONREALIZEDVCH = VOUCHER.AppendChild(XD.CreateElement("ISSTXNONREALIZEDVCH"));
                            XmlNode ISEXCISEMANUFACTURERON = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEMANUFACTURERON"));
                            XmlNode ISBLANKCHEQUE = VOUCHER.AppendChild(XD.CreateElement("ISBLANKCHEQUE"));
                            XmlNode ISVOID = VOUCHER.AppendChild(XD.CreateElement("ISVOID"));
                            XmlNode ISONHOLD = VOUCHER.AppendChild(XD.CreateElement("ISONHOLD"));
                            XmlNode ORDERLINESTATUS = VOUCHER.AppendChild(XD.CreateElement("ORDERLINESTATUS"));
                            XmlNode VATISAGNSTCANCSALES = VOUCHER.AppendChild(XD.CreateElement("VATISAGNSTCANCSALES"));
                            XmlNode VATISPURCEXEMPTED = VOUCHER.AppendChild(XD.CreateElement("VATISPURCEXEMPTED"));
                            XmlNode ISVATRESTAXINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISVATRESTAXINVOICE"));
                            XmlNode VATISASSESABLECALCVCH = VOUCHER.AppendChild(XD.CreateElement("VATISASSESABLECALCVCH"));
                            XmlNode ISVATDUTYPAID = VOUCHER.AppendChild(XD.CreateElement("ISVATDUTYPAID"));
                            XmlNode ISDELIVERYSAMEASCONSIGNEE = VOUCHER.AppendChild(XD.CreateElement("ISDELIVERYSAMEASCONSIGNEE"));
                            XmlNode ISDISPATCHSAMEASCONSIGNOR = VOUCHER.AppendChild(XD.CreateElement("ISDISPATCHSAMEASCONSIGNOR"));
                            XmlNode ISDELETED = VOUCHER.AppendChild(XD.CreateElement("ISDELETED"));
                            XmlNode CHANGEVCHMODE = VOUCHER.AppendChild(XD.CreateElement("CHANGEVCHMODE"));
                            XmlNode MASTERID = VOUCHER.AppendChild(XD.CreateElement("MASTERID"));
                            XmlNode EXCLUDEDTAXATIONSLIST = VOUCHER.AppendChild(XD.CreateElement("EXCLUDEDTAXATIONS.LIST"));
                            XmlNode OLDAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode ACCOUNTAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode AUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode DUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode SUPPLEMENTARYDUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                            XmlNode EWAYBILLDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("EWAYBILLDETAILS.LIST"));
                            XmlNode INVOICEDELNOTESLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEDELNOTES.LIST"));
                            XmlNode INVOICEORDERLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEORDERLIST.LIST"));
                            XmlNode INVOICEINDENTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("DIFFAINVOICEINDENTLIST.LISTCTUALQTY"));
                            XmlNode ATTENDANCEENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ATTENDANCEENTRIES.LIST"));
                            XmlNode ORIGINVOICEDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("ORIGINVOICEDETAILS.LIST"));
                            XmlNode INVOICEEXPORTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEEXPORTLIST.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTAL = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LDGROLDAUDITENTRYIDSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LDGROLDAUDITENTRYIDSLISTTYPE = LDGROLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LDGROLDAUDITENTRYIDS = LDGROLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVE = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEM = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGER = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVE = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTERED = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMED = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));

                            XmlNode LDGRBILLALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTNAME = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("NAME"));
                            XmlNode LDGRBILLALLOCATIONSLISTBILLTYPE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLTYPE"));
                            XmlNode LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSDEDUCTEEISSPECIALRATE"));
                            XmlNode LDGRBILLALLOCATIONSLISTAMOUNT = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LDGRBILLALLOCATIONSLISTINTERESTCOLLECTIONLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTSTBILLCATEGORIESLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("STBILLCATEGORIES.LIST"));

                            XmlNode LDGRINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGROLDAUDITENTRIESLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LDGRACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LDGRAUDITENTRIESLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LDGRINPUTCRALLOCSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LDGRDUTYHEADDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LDGREXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LDGRRATEDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LDGRSUMMARYALLOCSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LDGRSTPYMTDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LDGREXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LDGRTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LDGRTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LDGRTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LDGRVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LDGRCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LDGRREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LDGRINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LDGRVATITCDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LDGRADVANCETAXDETAILSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTCGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTCGSTTYPE = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTCGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSCGST = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTCGSTROUNDTYPE = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTCGSTVATEXPAMOUNT = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLBILLALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLINTERESTCOLLECTIONLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLOLDAUDITENTRIESLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLACCOUNTAUDITENTRIESLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTAUDITENTRIESLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTINPUTCRALLOCSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTDUTYHEADDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTEXCISEDUTYHEADDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTRATEDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLSUMMARYALLOCSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLSTPYMTDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLEXCISEPAYMENTALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXBILLALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXOBJECTALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTDSEXPENSEALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTVATSTATUTORYDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLCOSTTRACKALLOCATIONSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLREFVOUCHERDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLINVOICEWISEDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLVATITCDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLADVANCETAXDETAILSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTSGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTSGSTTYPE = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTSGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSSGST = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTSGSTROUNDTYPE = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSGSTVATEXPAMOUNT = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLBILLALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLINTERESTCOLLECTIONLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLOLDAUDITENTRIESLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLACCOUNTAUDITENTRIESLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTAUDITENTRIESLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTINPUTCRALLOCSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTDUTYHEADDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTEXCISEDUTYHEADDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTRATEDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLSUMMARYALLOCSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLSTPYMTDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLEXCISEPAYMENTALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXBILLALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXOBJECTALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTDSEXPENSEALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTVATSTATUTORYDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLCOSTTRACKALLOCATIONSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLREFVOUCHERDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLINVOICEWISEDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLVATITCDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLADVANCETAXDETAILSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            XmlNode LEDGERENTRIESLISTIGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTIGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTIGSTTYPE = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTIGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSIGST = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTIGSTROUNDTYPE = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTIGSTVATEXPAMOUNT = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLBILLALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLINTERESTCOLLECTIONLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLOLDAUDITENTRIESLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLACCOUNTAUDITENTRIESLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTAUDITENTRIESLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTINPUTCRALLOCSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTDUTYHEADDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTEXCISEDUTYHEADDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTRATEDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLSUMMARYALLOCSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLSTPYMTDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLEXCISEPAYMENTALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXBILLALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTAXOBJECTALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLTDSEXPENSEALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTVATSTATUTORYDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLCOSTTRACKALLOCATIONSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLREFVOUCHERDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLINVOICEWISEDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLVATITCDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLADVANCETAXDETAILSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            MasterId = MasterId + 1;
                            DateTime dt = Convert.ToDateTime(dtPurchaseReturnMaster.Rows[i][1]);
                            TALLYMESSAGEUDF.InnerText = "TallyUDF";
                            VOUCHERVCHTYPE.InnerText = "Debit Note";
                            VOUCHERACTION.InnerText = "Create";
                            VOUCHEROBJVIEW.InnerText = "Invoice Voucher View";
                            OLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                            OLDAUDITENTRYIDS.InnerText = "-1";
                            DATE.InnerText = String.Format("{0:yyyyMMdd}", dt); //Convert.ToString(dtPurchaseMaster.Rows[i][1].ToString("yyyyMMdd")); //"20180401";
                            REFERENCEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt);
                            STATENAME.InnerText = SupplierData.StateMaster.StateName.ToString();
                            COUNTRYOFRESIDENCE.InnerText = "India";
                            PARTYGSTIN.InnerText = SupplierData.VatNo.ToString();
                            TAXUNITNAME.InnerText = "Default Tax Unit";
                            PARTYNAME.InnerText = SupplierData.SupplierName.ToString();

                            REFERENCE.InnerText = Convert.ToString(dtPurchaseReturnMaster.Rows[i][4].ToString());
                            VOUCHERNUMBER.InnerText = Convert.ToString(dtPurchaseReturnMaster.Rows[i][4].ToString());
                            VOUCHERTYPENAME.InnerText = "Debit Note";
                            PARTYLEDGERNAME.InnerText = Convert.ToString(dtPurchaseReturnMaster.Rows[i][2].ToString()); //"AABA JADHAV,ISLAMPUR";
                            BASICBASEPARTYNAME.InnerText = Convert.ToString(dtPurchaseReturnMaster.Rows[i][2].ToString()); //"AABA JADHAV,ISLAMPUR";
                            FBTPAYMENTTYPE.InnerText = "Default";
                            PERSISTEDVIEW.InnerText = "Invoice Voucher View";
                            TYPEOFEXCISEVOUCHER.InnerText = "Non-Excise";
                            CONSIGNEEGSTIN.InnerText = CompanyData.VatNo.ToString();
                            BASICBUYERNAME.InnerText = CompanyData.CompanyName;
                            BASICDATETIMEOFINVOICE.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            BASICDATETIMEOFREMOVAL.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            CONSIGNEESTATENAME.InnerText = CompanyData.State.ToString();
                            DIFFACTUALQTY.InnerText = "No";
                            ISMSTFROMSYNC.InnerText = "No";
                            ASORIGINAL.InnerText = "No";
                            AUDITED.InnerText = "No";
                            FORJOBCOSTING.InnerText = "No";
                            ISOPTIONAL.InnerText = "No";
                            EFFECTIVEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt);
                            USEFOREXCISE.InnerText = "No";
                            ISFORJOBWORKIN.InnerText = "No";
                            ALLOWCONSUMPTION.InnerText = "No";
                            USEFORINTEREST.InnerText = "No";
                            USEFORGAINLOSS.InnerText = "No";
                            USEFORGODOWNTRANSFER.InnerText = "No";
                            USEFORCOMPOUND.InnerText = "No";
                            USEFORSERVICETAX.InnerText = "No";
                            ISEXCISEVOUCHER.InnerText = "Yes";
                            EXCISETAXOVERRIDE.InnerText = "No";
                            USEFORTAXUNITTRANSFER.InnerText = "No";
                            EXCISEOPENING.InnerText = "No";
                            USEFORFINALPRODUCTION.InnerText = "No";
                            ISTDSOVERRIDDEN.InnerText = "No";
                            ISTCSOVERRIDDEN.InnerText = "No";
                            ISTDSTCSCASHVCH.InnerText = "No";
                            INCLUDEADVPYMTVCH.InnerText = "No";
                            ISSUBWORKSCONTRACT.InnerText = "No";
                            ISVATOVERRIDDEN.InnerText = "No";
                            IGNOREORIGVCHDATE.InnerText = "No";
                            ISVATPAIDATCUSTOMS.InnerText = "No";
                            ISDECLAREDTOCUSTOMS.InnerText = "No";
                            ISSERVICETAXOVERRIDDEN.InnerText = "No";
                            ISISDVOUCHER.InnerText = "No";
                            ISEXCISEOVERRIDDEN.InnerText = "No";
                            ISEXCISESUPPLYVCH.InnerText = "No";
                            ISGSTOVERRIDDEN.InnerText = "No";
                            GSTNOTEXPORTED.InnerText = "No";
                            ISVATPRINCIPALACCOUNT.InnerText = "No";
                            ISBOENOTAPPLICABLE.InnerText = "No";
                            ISSHIPPINGWITHINSTATE.InnerText = "No";
                            ISOVERSEASTOURISTTRANS.InnerText = "No";
                            ISDESIGNATEDZONEPARTY.InnerText = "No";
                            ISCANCELLED.InnerText = "No";
                            HASCASHFLOW.InnerText = "No";
                            ISPOSTDATED.InnerText = "No";
                            USETRACKINGNUMBER.InnerText = "No";
                            ISINVOICE.InnerText = "Yes";
                            MFGJOURNAL.InnerText = "No";
                            HASDISCOUNTS.InnerText = "No";
                            ASPAYSLIP.InnerText = "No";
                            ISCOSTCENTRE.InnerText = "No";
                            ISSTXNONREALIZEDVCH.InnerText = "No";
                            ISEXCISEMANUFACTURERON.InnerText = "No";
                            ISBLANKCHEQUE.InnerText = "No";
                            ISVOID.InnerText = "No";
                            ISONHOLD.InnerText = "No";
                            ORDERLINESTATUS.InnerText = "No";
                            VATISAGNSTCANCSALES.InnerText = "No";
                            VATISPURCEXEMPTED.InnerText = "No";
                            ISVATRESTAXINVOICE.InnerText = "No";
                            VATISASSESABLECALCVCH.InnerText = "Yes";
                            ISVATDUTYPAID.InnerText = "Yes";
                            ISDELIVERYSAMEASCONSIGNEE.InnerText = "No";
                            ISDISPATCHSAMEASCONSIGNOR.InnerText = "No";
                            ISDELETED.InnerText = "No";
                            CHANGEVCHMODE.InnerText = "No";
                            MASTERID.InnerText = Convert.ToString(MasterId);
                            LDGROLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                            LDGROLDAUDITENTRYIDS.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMETOTAL.InnerText = Convert.ToString(dtPurchaseReturnMaster.Rows[i][2].ToString()); //"AABA JADHAV,ISLAMPUR";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVE.InnerText = "Yes";
                            LEDGERENTRIESLISTLEDGERFROMITEM.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIES.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGER.InnerText = "Yes";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVE.InnerText = "Yes";
                            LEDGERENTRIESLISTISCAPVATTAXALTERED.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMED.InnerText = "No";

                            LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = "-" + Convert.ToString(dtPurchaseReturnMaster.Rows[i][3].ToString()); //"100.00";
                            LDGRBILLALLOCATIONSLISTNAME.InnerText = Convert.ToString(dtPurchaseReturnMaster.Rows[i][4].ToString());
                            LDGRBILLALLOCATIONSLISTBILLTYPE.InnerText = "Agst Ref";
                            LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE.InnerText = "No";
                            LDGRBILLALLOCATIONSLISTAMOUNT.InnerText = "-" + Convert.ToString(dtPurchaseReturnMaster.Rows[i][3].ToString());

                            //                                                             0           1                                                                                                                                                                                         2                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 3                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          4       5       6       7            8                          9                          10                  
                            DataTable dtPurchaseReturnDetail = db.GetTable("Select pd.ProductName,pd.Unit,case when pd.Unit=pd.PurchaseUnit then Cast(Cast(pd.Amount as decimal(18,2))/cast(pd.Quantity as decimal(18,2)) as decimal(18,3)) else cast(pd.PurchasePrice as decimal(18,3)) end as Rate, case when pd.Unit=pd.PurchaseUnit then Cast(pd.Quantity as decimal(18,2)) else case when cast(pd.PurchasePrice as decimal(18,2))/cast(pd.Quantity as decimal(18,2)) <>cast(pd.Amount as decimal(18,2)) then Cast(pd.Quantity as decimal(18,2)) else Cast(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit) as decimal(18,2)) end end as Quantity, case when pd.Unit=pd.PurchaseUnit then cast((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100)-(((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) else case when cast(pd.PurchasePrice as decimal(18,2))/cast(pd.Quantity as decimal(18,2))<>cast(pd.Amount as decimal(18,2)) then cast((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100)-(((pd.Amount)-((pd.Amount)*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) ELSE cast((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100)-(((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.PurchasePrice*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductName and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) End END as Amount,CGstAmt,SGstAmt,IGstAmt,pd.CompanyName,Isnull(pd.Discount,0)+Isnull(pd.SpcDisc,0),pd.PurchaseUnit from PurchaseReturnMaster as pm inner join PurchaseReturnDetail as pd on pm.Id=pd.PurchaseReturnNo Where pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.SupplierName!='First stock' and pd.PurchaseReturnNo='" + Convert.ToString(dtPurchaseReturnMaster.Rows[i][0].ToString()) + "'");
                            for (int j = 0; j < dtPurchaseReturnDetail.Rows.Count; j++)
                            {
                                ProductRepository ProductRepo = new ProductRepository();
                                ProductInformation ProdData = new ProductInformation();
                                ProdData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(dtPurchaseReturnDetail.Rows[j][0].ToString()) && t.ManufactureCompany == Convert.ToString(dtPurchaseReturnDetail.Rows[j][8].ToString()) && t.Uniti == Convert.ToString(dtPurchaseReturnDetail.Rows[j][1].ToString())).FirstOrDefault();
                                if (ProdData == null)
                                {
                                    PurRProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtPurchaseReturnMaster.Rows[i][4].ToString(), ProductName = dtPurchaseReturnDetail.Rows[j][0].ToString() + "," + dtPurchaseReturnDetail.Rows[j][8].ToString() + "," + dtPurchaseReturnDetail.Rows[j][1].ToString() });
                                }
                                XmlNode ALLINVENTORYENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ALLINVENTORYENTRIES.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTSTOCKITEMNAME = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("STOCKITEMNAME"));
                                XmlNode ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                XmlNode ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                XmlNode ALLINVENTORYENTRIESLISTISAUTONEGATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISAUTONEGATE"));
                                XmlNode ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISCUSTOMSCLEARANCE"));
                                XmlNode ALLINVENTORYENTRIESLISTISTRACKCOMPONENT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKCOMPONENT"));
                                XmlNode ALLINVENTORYENTRIESLISTISTRACKPRODUCTION = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKPRODUCTION"));

                                XmlNode ALLINVENTORYENTRIESLISTISPRIMARYITEM = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISPRIMARYITEM"));
                                XmlNode ALLINVENTORYENTRIESLISSCRAP = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISSCRAP"));
                                XmlNode ALLINVENTORYENTRIESLISTRATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("RATE"));
                                XmlNode ALLINVENTORYENTRIESLISTAMOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                XmlNode ALLINVENTORYENTRIESLISTVATASSBLVALUE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("VATASSBLVALUE"));
                                XmlNode ALLINVENTORYENTRIESLISTACTUALQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                XmlNode ALLINVENTORYENTRIESLISTBILLEDQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                XmlNode ALLINVENTORYENTRIESLISTDISCOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DISCOUNT"));

                                XmlNode AINVLISTBATCHALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BATCHALLOCATIONS.LIST"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTGODOWNNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("GODOWNNAME"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTBATCHNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BATCHNAME"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTINDENTNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("INDENTNO"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTORDERNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ORDERNO"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTTRACKINGNUMBER = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("TRACKINGNUMBER"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("DYNAMICCSTISCLEARED"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTAMOUNT = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTACTUALQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTBILLEDQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTADDITIONALDETAILSLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ADDITIONALDETAILS.LIST"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTVOUCHERCOMPONENTLISTLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("VOUCHERCOMPONENTLIST.LIST"));

                                XmlNode ACCOUNTINGALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACCOUNTINGALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                                XmlAttribute ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE = LEDGERENTRIESLISTCGSTOLDAUDITENTRYIDSLISTIGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERNAME = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERNAME"));

                                XmlNode ACCOUNTINGALLOCATIONSLISTGSTCLASS = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("GSTCLASS"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTAMOUNT = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTSERVICETAXDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTBANKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTINTERESTCOLLECTIONLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTACCOUNTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTINPUTCRALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));

                                XmlNode ACCOUNTINGALLOCATIONSLISTSUMMARYALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTSTPYMTDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEPAYMENTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTTAXBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTTAXOBJECTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTTDSEXPENSEALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTVATSTATUTORYDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTCOSTTRACKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTREFVOUCHERDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTINVOICEWISEDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTVATITCDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTADVANCETAXDETAILSSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                                XmlNode ALLINVENTORYENTRIESLISTDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTSUPPLEMENTARYDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTTAXOBJECTALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTREFVOUCHERDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTEXCISEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXCISEALLOCATIONS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTEXPENSEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXPENSEALLOCATIONS.LIST"));

                                ALLINVENTORYENTRIESLISTSTOCKITEMNAME.InnerText = dtPurchaseReturnDetail.Rows[j][0].ToString(); //"ANGLE COCK ALCO";
                                ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISAUTONEGATE.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISTRACKCOMPONENT.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISTRACKPRODUCTION.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISPRIMARYITEM.InnerText = "No";
                                ALLINVENTORYENTRIESLISSCRAP.InnerText = "No";

                                ALLINVENTORYENTRIESLISTRATE.InnerText = dtPurchaseReturnDetail.Rows[j][2].ToString() + "/" + dtPurchaseReturnDetail.Rows[j][10].ToString(); //"100.00/NOS";
                                ALLINVENTORYENTRIESLISTAMOUNT.InnerText = dtPurchaseReturnDetail.Rows[j][4].ToString(); //"100.00";
                                ALLINVENTORYENTRIESLISTVATASSBLVALUE.InnerText = dtPurchaseReturnDetail.Rows[j][4].ToString(); //"100.00";
                                UnitRepository UnitRepo = new UnitRepository();
                                Unit UnitData = new Unit();
                                string unitname = "";
                                try
                                {
                                    unitname = dtPurchaseReturnDetail.Rows[j][10].ToString();
                                }
                                catch (Exception)
                                {
                                    unitname = dtPurchaseReturnDetail.Rows[j][1].ToString();
                                }
                                if (dtPurchaseReturnDetail.Rows[j][10].ToString() == dtPurchaseReturnDetail.Rows[j][1].ToString())
                                {
                                    ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                    ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                }
                                else
                                {
                                    ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                    ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                }
                                ALLINVENTORYENTRIESLISTDISCOUNT.InnerText = dtPurchaseReturnDetail.Rows[j][9].ToString();

                                AINVLISTBATCHALLOCATIONSLISTGODOWNNAME.InnerText = "Main Location";
                                AINVLISTBATCHALLOCATIONSLISTBATCHNAME.InnerText = "Primary Batch";
                                AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED.InnerText = "No";
                                AINVLISTBATCHALLOCATIONSLISTAMOUNT.InnerText = dtPurchaseReturnDetail.Rows[j][4].ToString(); //"100.00";
                                if (dtPurchaseReturnDetail.Rows[j][10].ToString() == dtPurchaseReturnDetail.Rows[j][1].ToString())
                                {
                                    AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                    AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"1.00 NOS";
                                }
                                else
                                {
                                    AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                    AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = dtPurchaseReturnDetail.Rows[j][3].ToString() + " " + dtPurchaseReturnDetail.Rows[j][10].ToString() + " 0.00 " + dtPurchaseReturnDetail.Rows[j][1].ToString(); //"15 BUNDLE 0.00 NOS";
                                }

                                ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                                ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS.InnerText = "-1";
                                ACCOUNTINGALLOCATIONSLISTLEDGERNAME.InnerText = "Purchase";
                                ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTAMOUNT.InnerText = dtPurchaseReturnDetail.Rows[j][4].ToString(); //"100.00";

                                CGSTAMT = CGSTAMT + Convert.ToDecimal(dtPurchaseReturnDetail.Rows[j][5].ToString());
                                SGSTAMT = SGSTAMT + Convert.ToDecimal(dtPurchaseReturnDetail.Rows[j][6].ToString());
                                IGSTAMT = IGSTAMT + Convert.ToDecimal(dtPurchaseReturnDetail.Rows[j][7].ToString());
                            }

                            LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTCGSTTYPE.InnerText = "Number";
                            LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSCGST.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMECGST.InnerText = "CGST";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVECGST.InnerText = "No";
                            LEDGERENTRIESLISTLEDGERFROMITEMCGST.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESCGST.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERCGST.InnerText = "No";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTCGST.InnerText = Convert.ToString(CGSTAMT);
                            LEDGERENTRIESLISTCGSTVATEXPAMOUNT.InnerText = Convert.ToString(CGSTAMT);

                            LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTSGSTTYPE.InnerText = "Number";
                            LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSSGST.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMESGST.InnerText = "SGST";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVESGST.InnerText = "No";
                            LEDGERENTRIESLISTLEDGERFROMITEMSGST.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESSGST.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERSGST.InnerText = "No";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTSGST.InnerText = Convert.ToString(SGSTAMT);
                            LEDGERENTRIESLISTSGSTVATEXPAMOUNT.InnerText = Convert.ToString(SGSTAMT);

                            LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSLISTIGSTTYPE.InnerText = "Number";
                            LEDGERENTRIESLISTCGSTLDGROLDAUDITENTRYIDSIGST.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMEIGST.InnerText = "IGST";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST.InnerText = "No";
                            LEDGERENTRIESLISTLEDGERFROMITEMIGST.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESIGST.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERIGST.InnerText = "No";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTIGST.InnerText = Convert.ToString(IGSTAMT);
                            LEDGERENTRIESLISTIGSTVATEXPAMOUNT.InnerText = Convert.ToString(IGSTAMT);

                            if (Convert.ToDecimal(dtPurchaseMaster.Rows[i][3]) != Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(IGstAmt)", "")))
                            {
                                LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = "-" + Convert.ToString(Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtPurchaseReturnDetail.Compute("Sum(IGstAmt)", "")));
                            }
                        }
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                                        
                    //Sale Return
                    try
                    {
                        db.connect();
                        //0         1           2              3            4           5
                        dtCustomerBillReturnMaster = db.GetTable("SELECT Id,GoodReturnDate,CustomerName,[TotalAmount],GoodReturnNo,CustomerId FROM [GoodReturnMaster] where GoodReturnDate>='" + fromDate + "' and GoodReturnDate<='" + toDate + "' and  BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and TotalAmount!=0 order by CONVERT(VARCHAR(24),[GoodReturnDate],101),Id");
                        DataTable dtCustomerBillReturnMaster1 = db.GetTable("SELECT Id,GoodReturnNo,CustomerId,CustomerName,[TotalAmount] FROM [GoodReturnMaster] where GoodReturnDate>='" + fromDate + "' and GoodReturnDate<='" + toDate + "' and  BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and TotalAmount!=0 and CustomerId not in (SELECT grm.CustomerId FROM [GoodReturnMaster] grm inner join CustomerInformation c on grm.CustomerId=c.CustomerID where GoodReturnDate>='" + fromDate + "' and GoodReturnDate<='" + toDate + "' and  BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and grm.CompId='" + CommonMethod.CompId + "' and grm.TotalAmount!=0) order by CONVERT(VARCHAR(24),[GoodReturnDate],101),Id");
                        
                        if (dtCustomerBillReturnMaster1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtCustomerBillReturnMaster1.Rows.Count; i++)
                            {
                                SaleReturnIDArray.Add(new BillNoIdName() { BillNo = dtCustomerBillReturnMaster1.Rows[i][1].ToString(), ID = dtCustomerBillReturnMaster1.Rows[i][2].ToString(), Name = dtCustomerBillReturnMaster1.Rows[i][3].ToString() });
                            }
                        }
                        barprgrsbar.Visible = true;
                        barprgrsbar.Minimum = 0;
                        barprgrsbar.Maximum = dtCustomerBillReturnMaster.Rows.Count;
                        for (int i = 0; i < dtCustomerBillReturnMaster.Rows.Count; i++)
                        {
                            barprgrsbar.Value = i;
                            CGSTAMT = 0; SGSTAMT = 0; IGSTAMT = 0;
                            var CustomerData = CustomerRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(dtCustomerBillReturnMaster.Rows[i][5].ToString()) && t.CustomerName == dtCustomerBillReturnMaster.Rows[i][2].ToString()).FirstOrDefault();

                            XmlNode TALLYMESSAGE = REQUESTDATA.AppendChild(XD.CreateElement("TALLYMESSAGE"));
                            XmlAttribute TALLYMESSAGEUDF = TALLYMESSAGE.Attributes.Append(XD.CreateAttribute("xmlns:UDF"));
                            XmlNode VOUCHER = TALLYMESSAGE.AppendChild(XD.CreateElement("VOUCHER"));
                            XmlAttribute VOUCHERVCHTYPE = VOUCHER.Attributes.Append(XD.CreateAttribute("VCHTYPE"));
                            XmlAttribute VOUCHERACTION = VOUCHER.Attributes.Append(XD.CreateAttribute("ACTION"));
                            XmlAttribute VOUCHEROBJVIEW = VOUCHER.Attributes.Append(XD.CreateAttribute("OBJVIEW"));

                            XmlNode OLDAUDITENTRYIDSLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute OLDAUDITENTRYIDSLISTTYPE = OLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode OLDAUDITENTRYIDS = OLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));

                            XmlNode DATE = VOUCHER.AppendChild(XD.CreateElement("DATE"));
                            XmlNode REFERENCEDATE = VOUCHER.AppendChild(XD.CreateElement("REFERENCEDATE"));
                            XmlNode STATENAME = VOUCHER.AppendChild(XD.CreateElement("STATENAME"));
                            XmlNode COUNTRYOFRESIDENCE = VOUCHER.AppendChild(XD.CreateElement("COUNTRYOFRESIDENCE"));
                            XmlNode PARTYGSTIN = VOUCHER.AppendChild(XD.CreateElement("PARTYGSTIN"));
                            XmlNode TAXUNITNAME = VOUCHER.AppendChild(XD.CreateElement("TAXUNITNAME"));
                            XmlNode PARTYNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYNAME"));
                            XmlNode VOUCHERTYPENAME = VOUCHER.AppendChild(XD.CreateElement("VOUCHERTYPENAME"));
                            XmlNode REFERENCE = VOUCHER.AppendChild(XD.CreateElement("REFERENCE"));
                            XmlNode VOUCHERNUMBER = VOUCHER.AppendChild(XD.CreateElement("VOUCHERNUMBER"));
                            XmlNode PARTYLEDGERNAME = VOUCHER.AppendChild(XD.CreateElement("PARTYLEDGERNAME"));
                            XmlNode BASICBASEPARTYNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBASEPARTYNAME"));
                            XmlNode CSTFORMISSUETYPE = VOUCHER.AppendChild(XD.CreateElement("CSTFORMISSUETYPE"));
                            XmlNode CSTFORMRECVTYPE = VOUCHER.AppendChild(XD.CreateElement("CSTFORMRECVTYPE"));
                            XmlNode FBTPAYMENTTYPE = VOUCHER.AppendChild(XD.CreateElement("FBTPAYMENTTYPE"));
                            XmlNode PERSISTEDVIEW = VOUCHER.AppendChild(XD.CreateElement("PERSISTEDVIEW"));
                            XmlNode TYPEOFEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("TYPEOFEXCISEVOUCHER"));
                            XmlNode PLACEOFSUPPLY = VOUCHER.AppendChild(XD.CreateElement("PLACEOFSUPPLY"));
                            XmlNode CONSIGNEEGSTIN = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEEGSTIN"));
                            XmlNode BASICBUYERNAME = VOUCHER.AppendChild(XD.CreateElement("BASICBUYERNAME"));
                            XmlNode BASICDATETIMEOFREMOVAL = VOUCHER.AppendChild(XD.CreateElement("BASICDATETIMEOFREMOVAL"));
                            XmlNode VCHGSTCLASS = VOUCHER.AppendChild(XD.CreateElement("VCHGSTCLASS"));
                            XmlNode CONSIGNEESTATENAME = VOUCHER.AppendChild(XD.CreateElement("CONSIGNEESTATENAME"));
                            XmlNode DIFFACTUALQTY = VOUCHER.AppendChild(XD.CreateElement("DIFFACTUALQTY"));
                            XmlNode ISMSTFROMSYNC = VOUCHER.AppendChild(XD.CreateElement("ISMSTFROMSYNC"));
                            XmlNode ASORIGINAL = VOUCHER.AppendChild(XD.CreateElement("ASORIGINAL"));
                            XmlNode AUDITED = VOUCHER.AppendChild(XD.CreateElement("AUDITED"));
                            XmlNode FORJOBCOSTING = VOUCHER.AppendChild(XD.CreateElement("FORJOBCOSTING"));
                            XmlNode ISOPTIONAL = VOUCHER.AppendChild(XD.CreateElement("ISOPTIONAL"));
                            XmlNode EFFECTIVEDATE = VOUCHER.AppendChild(XD.CreateElement("EFFECTIVEDATE"));
                            XmlNode USEFOREXCISE = VOUCHER.AppendChild(XD.CreateElement("USEFOREXCISE"));
                            XmlNode ISFORJOBWORKIN = VOUCHER.AppendChild(XD.CreateElement("ISFORJOBWORKIN"));
                            XmlNode ALLOWCONSUMPTION = VOUCHER.AppendChild(XD.CreateElement("ALLOWCONSUMPTION"));
                            XmlNode USEFORINTEREST = VOUCHER.AppendChild(XD.CreateElement("USEFORINTEREST"));
                            XmlNode USEFORGAINLOSS = VOUCHER.AppendChild(XD.CreateElement("USEFORGAINLOSS"));
                            XmlNode USEFORGODOWNTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORGODOWNTRANSFER"));
                            XmlNode USEFORCOMPOUND = VOUCHER.AppendChild(XD.CreateElement("USEFORCOMPOUND"));
                            XmlNode USEFORSERVICETAX = VOUCHER.AppendChild(XD.CreateElement("USEFORSERVICETAX"));
                            XmlNode ISEXCISEVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEVOUCHER"));
                            XmlNode EXCISETAXOVERRIDE = VOUCHER.AppendChild(XD.CreateElement("EXCISETAXOVERRIDE"));
                            XmlNode USEFORTAXUNITTRANSFER = VOUCHER.AppendChild(XD.CreateElement("USEFORTAXUNITTRANSFER"));
                            XmlNode EXCISEOPENING = VOUCHER.AppendChild(XD.CreateElement("EXCISEOPENING"));
                            XmlNode USEFORFINALPRODUCTION = VOUCHER.AppendChild(XD.CreateElement("USEFORFINALPRODUCTION"));
                            XmlNode ISTDSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTDSOVERRIDDEN"));
                            XmlNode ISTCSOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISTCSOVERRIDDEN"));
                            XmlNode ISTDSTCSCASHVCH = VOUCHER.AppendChild(XD.CreateElement("ISTDSTCSCASHVCH"));
                            XmlNode INCLUDEADVPYMTVCH = VOUCHER.AppendChild(XD.CreateElement("INCLUDEADVPYMTVCH"));
                            XmlNode ISSUBWORKSCONTRACT = VOUCHER.AppendChild(XD.CreateElement("ISSUBWORKSCONTRACT"));
                            XmlNode ISVATOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISVATOVERRIDDEN"));
                            XmlNode IGNOREORIGVCHDATE = VOUCHER.AppendChild(XD.CreateElement("IGNOREORIGVCHDATE"));
                            XmlNode ISVATPAIDATCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISVATPAIDATCUSTOMS"));
                            XmlNode ISDECLAREDTOCUSTOMS = VOUCHER.AppendChild(XD.CreateElement("ISDECLAREDTOCUSTOMS"));
                            XmlNode ISSERVICETAXOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISSERVICETAXOVERRIDDEN"));
                            XmlNode ISISDVOUCHER = VOUCHER.AppendChild(XD.CreateElement("ISISDVOUCHER"));
                            XmlNode ISEXCISEOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEOVERRIDDEN"));
                            XmlNode ISEXCISESUPPLYVCH = VOUCHER.AppendChild(XD.CreateElement("ISEXCISESUPPLYVCH"));
                            XmlNode ISGSTOVERRIDDEN = VOUCHER.AppendChild(XD.CreateElement("ISGSTOVERRIDDEN"));
                            XmlNode GSTNOTEXPORTED = VOUCHER.AppendChild(XD.CreateElement("GSTNOTEXPORTED"));
                            XmlNode ISVATPRINCIPALACCOUNT = VOUCHER.AppendChild(XD.CreateElement("ISVATPRINCIPALACCOUNT"));
                            XmlNode ISBOENOTAPPLICABLE = VOUCHER.AppendChild(XD.CreateElement("ISBOENOTAPPLICABLE"));
                            XmlNode ISSHIPPINGWITHINSTATE = VOUCHER.AppendChild(XD.CreateElement("ISSHIPPINGWITHINSTATE"));
                            XmlNode ISOVERSEASTOURISTTRANS = VOUCHER.AppendChild(XD.CreateElement("ISOVERSEASTOURISTTRANS"));
                            XmlNode ISDESIGNATEDZONEPARTY = VOUCHER.AppendChild(XD.CreateElement("ISDESIGNATEDZONEPARTY"));
                            XmlNode ISCANCELLED = VOUCHER.AppendChild(XD.CreateElement("ISCANCELLED"));
                            XmlNode HASCASHFLOW = VOUCHER.AppendChild(XD.CreateElement("HASCASHFLOW"));
                            XmlNode ISPOSTDATED = VOUCHER.AppendChild(XD.CreateElement("ISPOSTDATED"));
                            XmlNode USETRACKINGNUMBER = VOUCHER.AppendChild(XD.CreateElement("USETRACKINGNUMBER"));
                            XmlNode ISINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISINVOICE"));

                            XmlNode MFGJOURNAL = VOUCHER.AppendChild(XD.CreateElement("MFGJOURNAL"));
                            XmlNode HASDISCOUNTS = VOUCHER.AppendChild(XD.CreateElement("HASDISCOUNTS"));
                            XmlNode ASPAYSLIP = VOUCHER.AppendChild(XD.CreateElement("ASPAYSLIP"));
                            XmlNode ISCOSTCENTRE = VOUCHER.AppendChild(XD.CreateElement("ISCOSTCENTRE"));
                            XmlNode ISSTXNONREALIZEDVCH = VOUCHER.AppendChild(XD.CreateElement("ISSTXNONREALIZEDVCH"));
                            XmlNode ISEXCISEMANUFACTURERON = VOUCHER.AppendChild(XD.CreateElement("ISEXCISEMANUFACTURERON"));
                            XmlNode ISBLANKCHEQUE = VOUCHER.AppendChild(XD.CreateElement("ISBLANKCHEQUE"));
                            XmlNode ISVOID = VOUCHER.AppendChild(XD.CreateElement("ISVOID"));
                            XmlNode ISONHOLD = VOUCHER.AppendChild(XD.CreateElement("ISONHOLD"));

                            XmlNode ORDERLINESTATUS = VOUCHER.AppendChild(XD.CreateElement("ORDERLINESTATUS"));
                            XmlNode VATISAGNSTCANCSALES = VOUCHER.AppendChild(XD.CreateElement("VATISAGNSTCANCSALES"));
                            XmlNode VATISPURCEXEMPTED = VOUCHER.AppendChild(XD.CreateElement("VATISPURCEXEMPTED"));
                            XmlNode ISVATRESTAXINVOICE = VOUCHER.AppendChild(XD.CreateElement("ISVATRESTAXINVOICE"));
                            XmlNode VATISASSESABLECALCVCH = VOUCHER.AppendChild(XD.CreateElement("VATISASSESABLECALCVCH"));
                            XmlNode ISVATDUTYPAID = VOUCHER.AppendChild(XD.CreateElement("ISVATDUTYPAID"));
                            XmlNode ISDELIVERYSAMEASCONSIGNEE = VOUCHER.AppendChild(XD.CreateElement("ISDELIVERYSAMEASCONSIGNEE"));
                            XmlNode ISDISPATCHSAMEASCONSIGNOR = VOUCHER.AppendChild(XD.CreateElement("ISDISPATCHSAMEASCONSIGNOR"));
                            XmlNode ISDELETED = VOUCHER.AppendChild(XD.CreateElement("ISDELETED"));
                            XmlNode CHANGEVCHMODE = VOUCHER.AppendChild(XD.CreateElement("CHANGEVCHMODE"));
                            XmlNode MASTERID = VOUCHER.AppendChild(XD.CreateElement("MASTERID"));

                            XmlNode EXCLUDEDTAXATIONSLIST = VOUCHER.AppendChild(XD.CreateElement("EXCLUDEDTAXATIONS.LIST"));
                            XmlNode OLDAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode ACCOUNTAUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode AUDITENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode DUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode SUPPLEMENTARYDUTYHEADDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                            XmlNode EWAYBILLDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("EWAYBILLDETAILS.LIST"));
                            XmlNode INVOICEDELNOTESLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEDELNOTES.LIST"));
                            XmlNode INVOICEORDERLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEORDERLIST.LIST"));
                            XmlNode INVOICEINDENTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEINDENTLIST.LIST"));
                            XmlNode ATTENDANCEENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ATTENDANCEENTRIES.LIST"));
                            XmlNode ORIGINVOICEDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("ORIGINVOICEDETAILS.LIST"));
                            XmlNode INVOICEEXPORTLISTLIST = VOUCHER.AppendChild(XD.CreateElement("INVOICEEXPORTLIST.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTAL = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTTOTALTYPE = LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDS = LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVETOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSERVICETAXDETAILSLISTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTBANKALLOCATIONSLISTTOTAL = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));

                            XmlNode LDGRBILLALLOCATIONSLIST = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTNAME = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("NAME"));
                            XmlNode LDGRBILLALLOCATIONSLISTBILLTYPE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLTYPE"));
                            XmlNode LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSDEDUCTEEISSPECIALRATE"));
                            XmlNode LDGRBILLALLOCATIONSLISTAMOUNT = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LDGRBILLALLOCATIONSLISTINTERESTCOLLECTIONLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LDGRBILLALLOCATIONSLISTSTBILLCATEGORIESLIST = LDGRBILLALLOCATIONSLIST.AppendChild(XD.CreateElement("STBILLCATEGORIES.LIST"));

                            XmlNode LEDGERENTRIESLISTTOTALINTERESTCOLLECTION = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALOLDAUDITENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALACCOUNTAUDITENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALAUDITENTRIES = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALINPUTCRALLOCS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALDUTYHEADDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALEXCISEDUTYHEADDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALRATEDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALSUMMARYALLOCS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALSTPYMTDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALEXCISEPAYMENTALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALTAXBILLALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALTAXOBJECTALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALTDSEXPENSEALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALVATSTATUTORYDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALCOSTTRACKALLOCATIONS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALREFVOUCHERDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALINVOICEWISEDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALVATITCDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTTOTALADVANCETAXDETAILS = LEDGERENTRIESLISTTOTAL.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));
                            //CGST
                            XmlNode LEDGERENTRIESLISTCGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPECGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTCGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTCGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTROUNDTYPECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTCGST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTCGSTVATEXPAMOUNT = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTCGSTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTBANKALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTBILLALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTOLDAUDITENTRIESLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTAUDITENTRIESLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTINPUTCRALLOCSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTDUTYHEADDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTEXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTRATEDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTSUMMARYALLOCSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTSTPYMTDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTEXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTVATITCDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTCGSTADVANCETAXDETAILSLIST = LEDGERENTRIESLISTCGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            //SGST
                            XmlNode LEDGERENTRIESLISTSGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPESGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTSGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTSGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTROUNDTYPESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTSGST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTSGSTVATEXPAMOUNT = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTSGSTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTBANKALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTBILLALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTOLDAUDITENTRIESLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTAUDITENTRIESLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTINPUTCRALLOCSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTDUTYHEADDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTEXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTRATEDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTSUMMARYALLOCSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTSTPYMTDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTEXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTVATITCDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTSGSTADVANCETAXDETAILSLIST = LEDGERENTRIESLISTSGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            //IGST
                            XmlNode LEDGERENTRIESLISTIGST = VOUCHER.AppendChild(XD.CreateElement("LEDGERENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                            XmlAttribute LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPEIGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTIGST.Attributes.Append(XD.CreateAttribute("TYPE"));
                            XmlNode LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTIGST = LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                            XmlNode LEDGERENTRIESLISTROUNDTYPEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ROUNDTYPE"));
                            XmlNode LEDGERENTRIESLISTLEDGERNAMEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERNAME"));
                            XmlNode LEDGERENTRIESLISTGSTCLASSIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("GSTCLASS"));
                            XmlNode LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTLEDGERFROMITEMIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                            XmlNode LEDGERENTRIESLISTREMOVEZEROENTRIESIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                            XmlNode LEDGERENTRIESLISTISPARTYLEDGERIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                            XmlNode LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                            XmlNode LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                            XmlNode LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                            XmlNode LEDGERENTRIESLISTAMOUNTIGST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AMOUNT"));
                            XmlNode LEDGERENTRIESLISTIGSTVATEXPAMOUNT = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATEXPAMOUNT"));
                            XmlNode LEDGERENTRIESLISTIGSTSERVICETAXDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTBANKALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTBILLALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTINTERESTCOLLECTIONLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTOLDAUDITENTRIESLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTACCOUNTAUDITENTRIESLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTAUDITENTRIESLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTINPUTCRALLOCSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTDUTYHEADDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTEXCISEDUTYHEADDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTRATEDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTSUMMARYALLOCSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTSTPYMTDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTEXCISEPAYMENTALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTTAXBILLALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTTAXOBJECTALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTTDSEXPENSEALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTVATSTATUTORYDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTCOSTTRACKALLOCATIONSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTREFVOUCHERDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTINVOICEWISEDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTVATITCDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                            XmlNode LEDGERENTRIESLISTIGSTADVANCETAXDETAILSLIST = LEDGERENTRIESLISTIGST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                            MasterId = MasterId + 1;
                            DateTime dt = Convert.ToDateTime(dtCustomerBillReturnMaster.Rows[i][1]);
                            TALLYMESSAGEUDF.InnerText = "TallyUDF";
                            VOUCHERVCHTYPE.InnerText = "Credit Note";
                            VOUCHERACTION.InnerText = "Create";
                            VOUCHEROBJVIEW.InnerText = "Invoice Voucher View";
                            OLDAUDITENTRYIDSLISTTYPE.InnerText = "Number";
                            OLDAUDITENTRYIDS.InnerText = "-1";
                            DATE.InnerText = String.Format("{0:yyyyMMdd}", dt); //Convert.ToString(dtPurchaseMaster.Rows[i][1].ToString("yyyyMMdd")); //"20180401";
                            REFERENCEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt); //Convert.ToString(dtPurchaseMaster.Rows[i][1].ToString("yyyyMMdd")); //"20180401";
                            VOUCHERTYPENAME.InnerText = "Credit Note";

                            STATENAME.InnerText = CustomerData.State.ToString();
                            COUNTRYOFRESIDENCE.InnerText = "India";
                            PARTYGSTIN.InnerText = CustomerData.BuyerTinNo.ToString();
                            TAXUNITNAME.InnerText = "Default Tax Unit";
                            PARTYNAME.InnerText = CustomerData.CustomerName.ToString();

                            REFERENCE.InnerText = dtCustomerBillReturnMaster.Rows[i][4].ToString();
                            VOUCHERNUMBER.InnerText = dtCustomerBillReturnMaster.Rows[i][4].ToString();
                            TYPEOFEXCISEVOUCHER.InnerText = "Non-Excise";
                            PLACEOFSUPPLY.InnerText = CustomerData.State.ToString();
                            CONSIGNEEGSTIN.InnerText = CompanyData.VatNo.ToString();
                            BASICBUYERNAME.InnerText = CustomerData.CustomerName.ToString();
                            BASICDATETIMEOFREMOVAL.InnerText = String.Format("{0:dd-MMM-yyyy}", dt) + " at " + String.Format("{0:h:m}", dt);
                            CONSIGNEESTATENAME.InnerText = CompanyData.State.ToString();
                            DIFFACTUALQTY.InnerText = "No";
                            ISMSTFROMSYNC.InnerText = "No";
                            ASORIGINAL.InnerText = "No";
                            AUDITED.InnerText = "No";
                            FORJOBCOSTING.InnerText = "No";
                            ISOPTIONAL.InnerText = "No";
                            EFFECTIVEDATE.InnerText = String.Format("{0:yyyyMMdd}", dt);
                            USEFOREXCISE.InnerText = "No";
                            ISFORJOBWORKIN.InnerText = "No";
                            ALLOWCONSUMPTION.InnerText = "No";
                            USEFORINTEREST.InnerText = "No";
                            USEFORGAINLOSS.InnerText = "No";
                            USEFORGODOWNTRANSFER.InnerText = "No";
                            USEFORCOMPOUND.InnerText = "No";
                            USEFORSERVICETAX.InnerText = "No";
                            ISEXCISEVOUCHER.InnerText = "Yes";
                            EXCISETAXOVERRIDE.InnerText = "No";
                            USEFORTAXUNITTRANSFER.InnerText = "No";
                            EXCISEOPENING.InnerText = "No";
                            USEFORFINALPRODUCTION.InnerText = "No";
                            ISTDSOVERRIDDEN.InnerText = "No";
                            ISTCSOVERRIDDEN.InnerText = "No";
                            ISTDSTCSCASHVCH.InnerText = "No";
                            INCLUDEADVPYMTVCH.InnerText = "No";
                            ISSUBWORKSCONTRACT.InnerText = "No";
                            ISVATOVERRIDDEN.InnerText = "No";
                            IGNOREORIGVCHDATE.InnerText = "No";
                            ISVATPAIDATCUSTOMS.InnerText = "No";
                            ISDECLAREDTOCUSTOMS.InnerText = "No";
                            ISSERVICETAXOVERRIDDEN.InnerText = "No";
                            ISISDVOUCHER.InnerText = "No";
                            ISEXCISEOVERRIDDEN.InnerText = "No";
                            ISEXCISESUPPLYVCH.InnerText = "No";
                            ISGSTOVERRIDDEN.InnerText = "No";
                            GSTNOTEXPORTED.InnerText = "No";
                            ISVATPRINCIPALACCOUNT.InnerText = "No";
                            ISBOENOTAPPLICABLE.InnerText = "No";
                            ISSHIPPINGWITHINSTATE.InnerText = "No";
                            ISOVERSEASTOURISTTRANS.InnerText = "No";
                            ISDESIGNATEDZONEPARTY.InnerText = "No";
                            ISCANCELLED.InnerText = "No";
                            HASCASHFLOW.InnerText = "No";
                            ISPOSTDATED.InnerText = "No";
                            USETRACKINGNUMBER.InnerText = "No";
                            ISINVOICE.InnerText = "Yes";
                            MFGJOURNAL.InnerText = "No";
                            HASDISCOUNTS.InnerText = "No";
                            ASPAYSLIP.InnerText = "No";
                            ISCOSTCENTRE.InnerText = "No";
                            ISSTXNONREALIZEDVCH.InnerText = "No";
                            ISEXCISEMANUFACTURERON.InnerText = "No";
                            ISBLANKCHEQUE.InnerText = "No";
                            ISVOID.InnerText = "No";
                            ISONHOLD.InnerText = "No";
                            ORDERLINESTATUS.InnerText = "No";
                            VATISAGNSTCANCSALES.InnerText = "No";
                            VATISPURCEXEMPTED.InnerText = "No";
                            ISVATRESTAXINVOICE.InnerText = "No";
                            VATISASSESABLECALCVCH.InnerText = "Yes";
                            ISVATDUTYPAID.InnerText = "Yes";
                            ISDELIVERYSAMEASCONSIGNEE.InnerText = "No";
                            ISDISPATCHSAMEASCONSIGNOR.InnerText = "No";
                            ISDELETED.InnerText = "No";
                            CHANGEVCHMODE.InnerText = "No";
                            MASTERID.InnerText = MasterId.ToString();

                            PARTYLEDGERNAME.InnerText = dtCustomerBillReturnMaster.Rows[i][2].ToString(); //"AABA JADHAV,ISLAMPUR";
                            BASICBASEPARTYNAME.InnerText = dtCustomerBillReturnMaster.Rows[i][2].ToString(); //"AABA JADHAV,ISLAMPUR";
                            FBTPAYMENTTYPE.InnerText = "Default";
                            PERSISTEDVIEW.InnerText = "Invoice Voucher View";

                            LEDGERENTRIESLISTTOTALTYPE.InnerText = "Number";
                            LEDGERENTRIESLISTTOTALOLDAUDITENTRYIDS.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMETOTAL.InnerText = dtCustomerBillReturnMaster.Rows[i][2].ToString(); //"AABA JADHAV,ISLAMPUR";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVETOTAL.InnerText = "No";
                            LEDGERENTRIESLISTLEDGERFROMITEMTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERTOTAL.InnerText = "Yes";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVETOTAL.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDTOTAL.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = dtCustomerBillReturnMaster.Rows[i][3].ToString(); //"100.00";
                            LDGRBILLALLOCATIONSLISTNAME.InnerText = dtCustomerBillReturnMaster.Rows[i][4].ToString();
                            LDGRBILLALLOCATIONSLISTBILLTYPE.InnerText = "Agst Ref";
                            LDGRBILLALLOCATIONSLISTTDSDEDUCTEEISSPECIALRATE.InnerText = "No";
                            LDGRBILLALLOCATIONSLISTAMOUNT.InnerText = dtCustomerBillReturnMaster.Rows[i][3].ToString(); //"100.00";

                            DataTable dtCustomerBillReturnDetail = db.GetTable("Select pd.ProductId,pd.Unit,pd.Rate,case when pd.Unit=pd.SaleUnit then Cast(pd.Quantity as decimal(18,2)) else Cast(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit) as decimal(18,2)) end as Quantity, case when pd.Unit=pd.SaleUnit then cast((pd.Rate*pd.Quantity)-((pd.Rate*pd.Quantity)*Isnull(pd.Discount,0)/100)-(((pd.Rate*pd.Quantity)-((pd.Rate*pd.Quantity)*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) else cast((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100)-(((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))-((pd.Rate*(pd.Quantity/(Select cast (Isnull(Isnull(UnitQty,1)/Isnull(AlternateUnitQty,1),1) as decimal(18,2)) from ProductInformation where ProductName=pd.ProductId and ManufactureCompany=pd.CompanyName and Uniti=pd.Unit)))*Isnull(pd.Discount,0)/100))*Isnull(pd.SpcDisc,0)/100)as Decimal(18,2)) End as Amount,CGstAmt,SGstAmt,IGstAmt,pd.CompanyName,Isnull(pd.Discount,0)+Isnull(pd.SpcDisc,0),pd.SaleUnit  from GoodReturnMaster as pm inner join GoodReturnDetail as pd on pm.Id=pd.GoodReturnNo Where pm.CompId='" + CommonMethod.CompId + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pd.GoodReturnNo='" + Convert.ToString(dtCustomerBillReturnMaster.Rows[i][0].ToString()) + "'");
                            for (int j = 0; j < dtCustomerBillReturnDetail.Rows.Count; j++)
                            {
                                ProductRepository ProductRepo = new ProductRepository();
                                ProductInformation ProdData = new ProductInformation();
                                ProdData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(dtCustomerBillReturnDetail.Rows[j][0].ToString()) && t.ManufactureCompany == Convert.ToString(dtCustomerBillReturnDetail.Rows[j][8].ToString()) && t.Uniti == Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString())).FirstOrDefault();
                                if (ProdData == null)
                                {
                                    SaleRProductIDArray.Add(new ProductNameWithBillNo() { BillNo = dtCustomerBillReturnMaster.Rows[i][4].ToString(), ProductName = dtCustomerBillReturnDetail.Rows[j][0].ToString() + "," + dtCustomerBillReturnDetail.Rows[j][8].ToString() + "," + dtCustomerBillReturnDetail.Rows[j][1].ToString() });
                                }
                                XmlNode ALLINVENTORYENTRIESLIST = VOUCHER.AppendChild(XD.CreateElement("ALLINVENTORYENTRIES.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTSTOCKITEMNAME = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("STOCKITEMNAME"));
                                XmlNode ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                XmlNode ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                XmlNode ALLINVENTORYENTRIESLISTISAUTONEGATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISAUTONEGATE"));
                                XmlNode ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISCUSTOMSCLEARANCE"));
                                XmlNode ALLINVENTORYENTRIESLISTISTRACKCOMPONENT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKCOMPONENT"));
                                XmlNode ALLINVENTORYENTRIESLISTISTRACKPRODUCTION = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISTRACKPRODUCTION"));
                                XmlNode ALLINVENTORYENTRIESLISTISPRIMARYITEM = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISPRIMARYITEM"));
                                XmlNode ALLINVENTORYENTRIESLISTISSCRAP = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ISSCRAP"));
                                XmlNode ALLINVENTORYENTRIESLISTRATE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("RATE"));
                                XmlNode ALLINVENTORYENTRIESLISTAMOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                XmlNode ALLINVENTORYENTRIESLISTVATASSBLVALUE = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("VATASSBLVALUE"));
                                XmlNode ALLINVENTORYENTRIESLISTACTUALQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                XmlNode ALLINVENTORYENTRIESLISTBILLEDQTY = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                XmlNode ALLINVENTORYENTRIESLISTDISCOUNT = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DISCOUNT"));

                                XmlNode AINVLISTBATCHALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("BATCHALLOCATIONS.LIST"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTGODOWNNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("GODOWNNAME"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTBATCHNAME = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BATCHNAME"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTINDENTNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("INDENTNO"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTORDERNO = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ORDERNO"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTTRACKINGNUMBER = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("TRACKINGNUMBER"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("DYNAMICCSTISCLEARED"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTAMOUNT = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTACTUALQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ACTUALQTY"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTBILLEDQTY = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLEDQTY"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTADDITIONALDETAILSLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("ADDITIONALDETAILS.LIST"));
                                XmlNode AINVLISTBATCHALLOCATIONSLISTVOUCHERCOMPONENTLISTLIST = AINVLISTBATCHALLOCATIONSLIST.AppendChild(XD.CreateElement("VOUCHERCOMPONENTLIST.LIST"));

                                XmlNode ACCOUNTINGALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("ACCOUNTINGALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS.LIST"));
                                XmlAttribute ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTTYPE = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.Attributes.Append(XD.CreateAttribute("TYPE"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLISTOLDAUDITENTRYIDS = ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRYIDSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRYIDS"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERNAME = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERNAME"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTGSTCLASS = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("GSTCLASS"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISDEEMEDPOSITIVE"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("LEDGERFROMITEM"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REMOVEZEROENTRIES"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISPARTYLEDGER"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISLASTDEEMEDPOSITIVE"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATTAXALTERED"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ISCAPVATNOTCLAIMED"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTAMOUNT = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AMOUNT"));

                                XmlNode ACCOUNTINGALLOCATIONSLISTSERVICETAXDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SERVICETAXDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTBANKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BANKALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("BILLALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTINTERESTCOLLECTIONLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INTERESTCOLLECTION.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTOLDAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("OLDAUDITENTRIES.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTACCOUNTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ACCOUNTAUDITENTRIES.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTAUDITENTRIESLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("AUDITENTRIES.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTINPUTCRALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INPUTCRALLOCS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEDUTYHEADDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEDUTYHEADDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTRATEDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("RATEDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTSUMMARYALLOCSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("SUMMARYALLOCS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTSTPYMTDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("STPYMTDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTEXCISEPAYMENTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("EXCISEPAYMENTALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTTAXBILLALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXBILLALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTTAXOBJECTALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTTDSEXPENSEALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("TDSEXPENSEALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTSVATSTATUTORYDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATSTATUTORYDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTCOSTTRACKALLOCATIONSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("COSTTRACKALLOCATIONS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTREFVOUCHERDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTINVOICEWISEDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("INVOICEWISEDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTVATITCDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("VATITCDETAILS.LIST"));
                                XmlNode ACCOUNTINGALLOCATIONSLISTADVANCETAXDETAILSLIST = ACCOUNTINGALLOCATIONSLIST.AppendChild(XD.CreateElement("ADVANCETAXDETAILS.LIST"));

                                XmlNode ALLINVENTORYENTRIESLISTDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("DUTYHEADDETAILS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTSUPPLEMENTARYDUTYHEADDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("SUPPLEMENTARYDUTYHEADDETAILS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTTAXOBJECTALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("TAXOBJECTALLOCATIONS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTREFVOUCHERDETAILSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("REFVOUCHERDETAILS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTEXCISEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXCISEALLOCATIONS.LIST"));
                                XmlNode ALLINVENTORYENTRIESLISTEXPENSEALLOCATIONSLIST = ALLINVENTORYENTRIESLIST.AppendChild(XD.CreateElement("EXPENSEALLOCATIONS.LIST"));

                                XmlNode PAYROLLMODEOFPAYMENTLIST = VOUCHER.AppendChild(XD.CreateElement("PAYROLLMODEOFPAYMENT.LIST"));
                                XmlNode ATTDRECORDSLIST = VOUCHER.AppendChild(XD.CreateElement("ATTDRECORDS.LIST"));
                                XmlNode GSTEWAYCONSIGNORADDRESSLIST = VOUCHER.AppendChild(XD.CreateElement("GSTEWAYCONSIGNORADDRESS.LIST"));
                                XmlNode GSTEWAYCONSIGNEEADDRESSLIST = VOUCHER.AppendChild(XD.CreateElement("GSTEWAYCONSIGNEEADDRESS.LIST"));
                                XmlNode TEMPGSTRATEDETAILSLIST = VOUCHER.AppendChild(XD.CreateElement("TEMPGSTRATEDETAILS.LIST"));

                                ALLINVENTORYENTRIESLISTSTOCKITEMNAME.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][0].ToString()); //"ANGLE COCK ALCO";
                                ALLINVENTORYENTRIESLISTISDEEMEDPOSITIVE.InnerText = "Yes";
                                ALLINVENTORYENTRIESLISTISLASTDEEMEDPOSITIVE.InnerText = "Yes";
                                ALLINVENTORYENTRIESLISTISAUTONEGATE.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISCUSTOMSCLEARANCE.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISTRACKCOMPONENT.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISTRACKPRODUCTION.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISPRIMARYITEM.InnerText = "No";
                                ALLINVENTORYENTRIESLISTISSCRAP.InnerText = "No";

                                ALLINVENTORYENTRIESLISTRATE.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][2].ToString()) + "/" + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][10].ToString()); //"100.00/NOS";
                                ALLINVENTORYENTRIESLISTAMOUNT.InnerText = "-" + Convert.ToString(Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][2]) * Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][3])); //Convert.ToString(dtdtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                ALLINVENTORYENTRIESLISTVATASSBLVALUE.InnerText = "-" + Convert.ToString(Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][2]) * Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][3])); //Convert.ToString(dtdtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                if (dtCustomerBillReturnDetail.Rows[j][10].ToString() == dtCustomerBillReturnDetail.Rows[j][1].ToString())
                                {
                                    ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                    ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                }
                                else
                                {
                                    ALLINVENTORYENTRIESLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + dtCustomerBillReturnDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                    ALLINVENTORYENTRIESLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + dtCustomerBillReturnDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                }

                                ALLINVENTORYENTRIESLISTDISCOUNT.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][9].ToString());
                                AINVLISTBATCHALLOCATIONSLISTGODOWNNAME.InnerText = "Main Location";
                                AINVLISTBATCHALLOCATIONSLISTBATCHNAME.InnerText = "Primary Batch";
                                AINVLISTBATCHALLOCATIONSLISTDYNAMICCSTISCLEARED.InnerText = "No";
                                AINVLISTBATCHALLOCATIONSLISTAMOUNT.InnerText = "-" + Convert.ToString(Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][2]) * Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][3])); //Convert.ToString(dtdtCustomerBillDetail.Rows[j][4].ToString()); //"100.00";
                                if (dtCustomerBillReturnDetail.Rows[j][10].ToString() == dtCustomerBillReturnDetail.Rows[j][1].ToString())
                                {
                                    AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                    AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"1.00 NOS";
                                }
                                else
                                {
                                    AINVLISTBATCHALLOCATIONSLISTACTUALQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + dtCustomerBillReturnDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                    AINVLISTBATCHALLOCATIONSLISTBILLEDQTY.InnerText = Convert.ToString(dtCustomerBillReturnDetail.Rows[j][3].ToString()) + " " + dtCustomerBillReturnDetail.Rows[j][10].ToString() + " 0.00 " + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][1].ToString()); //"15 BUNDLE 0.00 NOS";
                                }
                                ACCOUNTINGALLOCATIONSLISTLEDGERNAME.InnerText = "Sales";
                                ACCOUNTINGALLOCATIONSLISTISDEEMEDPOSITIVE.InnerText = "Yes";
                                ACCOUNTINGALLOCATIONSLISTLEDGERFROMITEM.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTREMOVEZEROENTRIES.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISPARTYLEDGER.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISLASTDEEMEDPOSITIVE.InnerText = "Yes";
                                ACCOUNTINGALLOCATIONSLISTISCAPVATTAXALTERED.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTISCAPVATNOTCLAIMED.InnerText = "No";
                                ACCOUNTINGALLOCATIONSLISTAMOUNT.InnerText = "-" + Convert.ToString(dtCustomerBillReturnDetail.Rows[j][4].ToString()); //"100.00";

                                CGSTAMT = CGSTAMT + Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][5].ToString());
                                SGSTAMT = SGSTAMT + Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][6].ToString());
                                IGSTAMT = IGSTAMT + Convert.ToDecimal(dtCustomerBillReturnDetail.Rows[j][7].ToString());
                            }
                            LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPECGST.InnerText = "Number";
                            LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTCGST.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMECGST.InnerText = "CGST";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVECGST.InnerText = "Yes";
                            LEDGERENTRIESLISTLEDGERFROMITEMCGST.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESCGST.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERCGST.InnerText = "No";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVECGST.InnerText = "Yes";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDCGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDCGST.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTCGST.InnerText = "-" + Convert.ToString(CGSTAMT);
                            LEDGERENTRIESLISTCGSTVATEXPAMOUNT.InnerText = "-" + Convert.ToString(CGSTAMT);
                            LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPESGST.InnerText = "Number";
                            LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTSGST.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMESGST.InnerText = "SGST";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVESGST.InnerText = "Yes";
                            LEDGERENTRIESLISTLEDGERFROMITEMSGST.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESSGST.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERSGST.InnerText = "No";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVESGST.InnerText = "Yes";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDSGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDSGST.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTSGST.InnerText = "-" + Convert.ToString(SGSTAMT);
                            LEDGERENTRIESLISTSGSTVATEXPAMOUNT.InnerText = "-" + Convert.ToString(SGSTAMT);
                            LEDGERENTRIESLISTOLDAUDITENTRYIDSLISTTYPEIGST.InnerText = "Number";
                            LEDGERENTRIESLISTOLDAUDITENTRYIDSLISOLDAUDITENTRYIDSTIGST.InnerText = "-1";
                            LEDGERENTRIESLISTLEDGERNAMEIGST.InnerText = "IGST";
                            LEDGERENTRIESLISTISDEEMEDPOSITIVEIGST.InnerText = "Yes";
                            LEDGERENTRIESLISTLEDGERFROMITEMIGST.InnerText = "No";
                            LEDGERENTRIESLISTREMOVEZEROENTRIESIGST.InnerText = "No";
                            LEDGERENTRIESLISTISPARTYLEDGERIGST.InnerText = "No";
                            LEDGERENTRIESLISTISLASTDEEMEDPOSITIVEIGST.InnerText = "Yes";
                            LEDGERENTRIESLISTISCAPVATTAXALTEREDIGST.InnerText = "No";
                            LEDGERENTRIESLISTISCAPVATNOTCLAIMEDIGST.InnerText = "No";
                            LEDGERENTRIESLISTAMOUNTIGST.InnerText = "-" + Convert.ToString(IGSTAMT);
                            LEDGERENTRIESLISTIGSTVATEXPAMOUNT.InnerText = "-" + Convert.ToString(IGSTAMT);

                            if (Convert.ToDecimal(dtCustomerBillReturnMaster.Rows[i][3]) != Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(IGstAmt)", "")))
                                LEDGERENTRIESLISTAMOUNTTOTAL.InnerText = Convert.ToString(Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(Amount)", "")) + Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(CGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(SGstAmt)", "")) + Convert.ToDecimal(dtCustomerBillReturnDetail.Compute("Sum(IGstAmt)", "")));
                        }
                        db.CloseConnection();
                    }
                    catch (Exception)
                    {
                        db.CloseConnection();
                    }
                    
                    {
                        label15.Visible = true;
                        totTransactionCount.Visible = true;

                        if (dtCustomerBillMaster.Rows.Count > 0)
                            intSaleCount = Convert.ToInt32(dtCustomerBillMaster.Rows.Count);
                        else
                            intSaleCount = 0;

                        if (dtPurchaseMaster.Rows.Count > 0)
                            intPurCount = Convert.ToInt32(dtPurchaseMaster.Rows.Count);
                        else
                            intPurCount = 0;

                        if (dtCustomerBillReturnMaster.Rows.Count > 0)
                            intSaleRCount = Convert.ToInt32(dtCustomerBillReturnMaster.Rows.Count);
                        else
                            intSaleRCount = 0;
                        if (dtPurchaseReturnMaster.Rows.Count > 0)
                            intPurRCount = Convert.ToInt32(dtPurchaseReturnMaster.Rows.Count);
                        else
                            intPurRCount = 0;
                        totTransactionCount.Text = Convert.ToString(dtCustomerBillMaster.Rows.Count + dtPurchaseMaster.Rows.Count + dtCustomerBillReturnMaster.Rows.Count + dtPurchaseReturnMaster.Rows.Count);
                    }
                    if (PurchaseIDArray.Count > 0 || PurProductIDArray.Count > 0 || PurchaseGSTProductIDArray.Count > 0 || SaleIDArray.Count > 0 || SaleProductIDArray.Count > 0 || SaleGSTProductIDArray.Count > 0 || PurReturnIDArray.Count > 0 || PurRProductIDArray.Count > 0 || SaleReturnIDArray.Count > 0 || SaleRProductIDArray.Count > 0)
                    {
                        totTransactionCount.Text = "0";
                        this.ActiveControl = totTransactionCount;
                        goto x;
                    }
                    else
                        this.ActiveControl = totTransactionCount;
                    string[] drives = Environment.GetLogicalDrives();
                    string FolderName = "Tally Export";
                    string folderPath = @"" + drives[0] + "" + FolderName + " \\";
                    try
                    {
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);
                    }
                    catch (Exception)
                    { }

                    XD.Save(folderPath + "Accounting Voucher.xml");
                    MessageBox.Show("XML files created Successfully At" + " " + folderPath + "Accounting Voucher.xml /n For Detail count Click on TransactionCount");
                    db.CloseConnection();
                    barprgrsbar.Visible = false;
                    goto y;
                x: { MessageBox.Show("XML files not created. /n For Detail Error Click on 'TransactionCount' and See Errors."); }
                y: { barprgrsbar.Visible = false; }
                }
                catch (Exception)
                { barprgrsbar.Visible = false; }
            }
            else
            {
                MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                pnlDateExport.Visible = true;
                this.ActiveControl = dtpToDate;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlDateExport.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                pnlErrorPanel.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void pnlErrorPanel_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (pnlErrorPanel.Visible == true)
                    btnOK.Visible = true;
                else
                    btnOK.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void totMasterCount_Click(object sender, EventArgs e)
        {
            try
            {
                pnlErrorPanel.Visible = true;
                if (CustIDArray.Count > 0)
                {
                    lblLabel1.Text = "Errored Customer IDs :";
                    string CustIds = "";
                    for (int i = 0; i < CustIDArray.Count; i++)
                    {
                        if (i == 0)
                            CustIds = CustIDArray[i].ToString();
                        else
                            CustIds = CustIds + "," + CustIDArray[i].ToString();
                    }
                    lbltot1.Text = CustIds.ToString();
                }
                else
                {
                    lblLabel1.Text = "Total Customer Names :";
                    lbltot1.Text = intCustCount.ToString();
                }
                if (SuppIDArray.Count > 0)
                {
                    lblLabel2.Text = "Errored Supplier IDs :";
                    string SuppIds = "";
                    for (int i = 0; i < SuppIDArray.Count; i++)
                    {
                        if (i == 0)
                            SuppIds = SuppIDArray[i].ToString();
                        else
                            SuppIds = SuppIds + "," + SuppIDArray[i].ToString();
                    }
                    lbltot2.Text = SuppIds.ToString();
                }
                else
                {
                    lblLabel2.Text = "Total Supplier Names :";
                    lbltot2.Text = intSuppCount.ToString();
                }
                if (UnitIDArray.Count > 0)
                {
                    lblLabel3.Text = "Errored Unit IDs :";
                    string UnitIds = "";
                    for (int i = 0; i < UnitIDArray.Count; i++)
                    {
                        if (i == 0)
                            UnitIds = UnitIDArray[i].ToString();
                        else
                            UnitIds = UnitIds + "," + UnitIDArray[i].ToString();
                    }
                    lbltot3.Text = UnitIds.ToString();
                }
                else
                {
                    lblLabel3.Text = "Total Unit Names :";
                    lbltot3.Text = intUnitCount.ToString();
                }
                if (MFGIDArray.Count > 0)
                {
                    lblLabel4.Text = "Errored Mfg Comapny IDs :";
                    string MFGIds = "";
                    for (int i = 0; i < MFGIDArray.Count; i++)
                    {
                        if (i == 0)
                            MFGIds = MFGIDArray[i].ToString();
                        else
                            MFGIds = MFGIds + "," + MFGIDArray[i].ToString();
                    }
                    lbltot4.Text = MFGIds.ToString();
                }
                else
                {
                    lblLabel4.Text = "Total MFG Company Names :";
                    lbltot4.Text = intMFGCount.ToString();
                }
                if (ProductIDArray.Count > 0)
                {
                    lblLabel5.Text = "Errored Product IDs :";
                    string ProductIds = "";
                    for (int i = 0; i < ProductIDArray.Count; i++)
                    {
                        if (i == 0)
                            ProductIds = ProductIDArray[i].ToString();
                        else
                            ProductIds = ProductIds + "," + ProductIDArray[i].ToString();
                    }
                    lbltot5.Text = ProductIds.ToString();
                }
                else
                {
                    lblLabel5.Text = "Total Product Names :";
                    lbltot5.Text = intProdCount.ToString();
                }
                if (ExpIDArray.Count > 0)
                {
                    lblLabel6.Text = "Errored Expense IDs :";
                    string ExpIds = "";
                    for (int i = 0; i < ExpIDArray.Count; i++)
                    {
                        if (i == 0)
                            ExpIds = ExpIDArray[i].ToString();
                        else
                            ExpIds = ExpIds + "," + ExpIDArray[i].ToString();
                    }
                    lbltot6.Text = ExpIds.ToString();
                }
                else
                {
                    lblLabel6.Text = "Total Group Ledger Names :";
                    lbltot6.Text = intGroupCount.ToString();
                }
                if (IncomeIDArray.Count > 0)
                {
                    lblLabel7.Text = "Errored Income IDs :";
                    string ExpIds = "";
                    for (int i = 0; i < IncomeIDArray.Count; i++)
                    {
                        if (i == 0)
                            ExpIds = IncomeIDArray[i].ToString();
                        else
                            ExpIds = ExpIds + "," + IncomeIDArray[i].ToString();
                    }
                    lbltot7.Text = ExpIds.ToString();
                    lblLabel8.Text = "Total Ledgers :";
                    lbltot8.Text = "6";
                }
                else
                {
                    lblLabel7.Text = "Total Ledgers :";
                    lbltot7.Text = "6";
                    lblLabel8.Text = "";
                    lbltot8.Text = "";
                    lblLabel9.Text = "";
                    lbltot9.Text = "";
                    lblLabel10.Text = "";
                    lbltot10.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void totTransactionCount_Click(object sender, EventArgs e)
        {
            try
            {
                pnlErrorPanel.Visible = true;
                if (SaleIDArray.Count > 0)
                {
                    lblLabel1.Text = "Errored CustomerBill IDs :";
                    lbltot1.Text = SaleIDArray.Count.ToString();
                }
                else
                {
                    lblLabel1.Text = "Total Customer Bills :";
                    lbltot1.Text = intSaleCount.ToString();
                }
                if (SaleProductIDArray.Count > 0)
                {
                    lblLabel2.Text = "Errored SaleProducts :";
                    lbltot2.Text = SaleProductIDArray.Count.ToString();
                }
                else
                {
                    lblLabel2.Text = "Total Supplier Bills :";
                    lbltot2.Text = intPurCount.ToString();
                }
                if (PurchaseIDArray.Count > 0)
                {
                    lblLabel3.Text = "Errored Purchase IDs :";
                    lbltot3.Text = PurchaseIDArray.Count.ToString();
                }
                else
                {
                    lblLabel3.Text = "Total Sale Return :";
                    lbltot3.Text = intSaleRCount.ToString();
                }
                if (PurProductIDArray.Count > 0)
                {
                    lblLabel4.Text = "Errored PurchaseProducts :";
                    lbltot4.Text = PurProductIDArray.Count.ToString();
                }
                else
                {
                    lblLabel4.Text = "Total Purchase Return :";
                    lbltot4.Text = intPurRCount.ToString();
                }
                if (SaleReturnIDArray.Count > 0)
                {
                    lblLabel5.Text = "Errored SaleReturn IDs :";
                    lbltot5.Text = SaleReturnIDArray.Count.ToString();
                }
                else
                {
                    lblLabel5.Text = "";
                    lbltot5.Text = "";
                }
                if (SaleRProductIDArray.Count > 0)
                {
                    lblLabel6.Text = "Errored SaleReturned Products :";
                    lbltot6.Text = SaleRProductIDArray.Count.ToString();
                }
                else
                {
                    lblLabel6.Text = "";
                    lbltot6.Text = "";
                }
                if (PurReturnIDArray.Count > 0)
                {
                    lblLabel7.Text = "Errored PurchaseReturn IDs :";
                    lbltot7.Text = PurReturnIDArray.Count.ToString();
                }
                else
                {
                    lblLabel7.Text = "";
                    lbltot7.Text = "";
                }
                if (PurRProductIDArray.Count > 0)
                {
                    lblLabel8.Text = "Errored PurchaseReturn Products :";
                    lbltot8.Text = PurRProductIDArray.Count.ToString();
                }
                else
                {
                    lblLabel8.Text = "";
                    lbltot8.Text = "";
                }
                if (SaleGSTProductIDArray.Count > 0)
                {
                    lblLabel9.Text = "Errored SaleGST Products :";
                    lbltot9.Text = SaleGSTProductIDArray.Count.ToString();
                }
                else
                {
                    lblLabel9.Text = "";
                    lbltot9.Text = "";
                }
                if (PurchaseGSTProductIDArray.Count > 0)
                {
                    lblLabel10.Text = "Errored PurchaseGST Products :";
                    lbltot10.Text = PurchaseGSTProductIDArray.Count.ToString();
                }
                else
                {
                    lblLabel10.Text = "";
                    lbltot10.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot1_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel1.Text == "Errored CustomerBill IDs :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored CustomerBill IDs";
                    dtgvErrorList.DataSource = SaleIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void btnErrorOK_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDetailedErrorPanel.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void lbltot2_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel2.Text == "Errored SaleProducts :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored SaleProducts";
                    dtgvErrorList.DataSource = SaleProductIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot3_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel3.Text == "Errored Purchase IDs :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored Purchase IDs";
                    dtgvErrorList.DataSource = PurchaseIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot4_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel4.Text == "Errored PurchaseProducts :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored PurchaseProducts";
                    dtgvErrorList.DataSource = PurProductIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot5_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel5.Text == "Errored SaleReturn IDs :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored SaleReturn IDs";
                    dtgvErrorList.DataSource = SaleReturnIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot6_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel6.Text == "Errored SaleReturned Products :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored SaleReturned Products";
                    dtgvErrorList.DataSource = SaleRProductIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot7_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel7.Text == "Errored PurchaseReturn IDs :")
                {

                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored PurchaseReturn IDs";
                    dtgvErrorList.DataSource = PurReturnIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot8_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel8.Text == "Errored PurchaseReturn Products :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored PurchaseReturn Products";
                    dtgvErrorList.DataSource = PurRProductIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot9_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel9.Text == "Errored SaleGST Products :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored SaleGST Products";
                    dtgvErrorList.DataSource = SaleGSTProductIDArray;
                }
            }
            catch (Exception)
            { }
        }

        private void lbltot10_Click(object sender, EventArgs e)
        {
            try
            {
                dtgvErrorList.DataSource = null;
                if (lblLabel10.Text == "Errored PurchaseGST Products :")
                {
                    pnlDetailedErrorPanel.Visible = true;
                    lblErrorLabel.Text = "Errored PurchaseGST Products";
                    dtgvErrorList.DataSource = PurchaseGSTProductIDArray;
                }
            }
            catch (Exception)
            { }
        }
    }
}





