using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptAllCustomerBill : Form
    {
        DbClass db = new DbClass();
        AllSaleCrystalReport salerpt = new AllSaleCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptAllCustomerBill()
        {
            InitializeComponent();
            Getdata();
        }

        public RptAllCustomerBill(int custid)
        {
            InitializeComponent();
            Getdata(custid);
        }

        public RptAllCustomerBill(string mode)
        {
            InitializeComponent();
            Getdata(mode);
        }

        public RptAllCustomerBill(int billtype, string from = "", string to = "", string mode = "", int custid = 0, string rcptno = "", string itemname = "", string companyname = "", string custbillno = "", string transType = "", string userBillSeries = "", string orderby = "", int SalesmanId = 0,int rootid=0)
        {
            InitializeComponent();
            Getdata(billtype, from, to, mode, custid, rcptno, itemname, companyname, custbillno, transType, userBillSeries, orderby, SalesmanId,rootid);
        }

        //public void Getdata(int billtype, string from = "", string to = "", string mode = "", int custid = 0, string rcptno = "", string itemname = "", string companyname = "", String custbillno = "", string transType = "", string user_billSeries = "", string orderby = "", int SalesmanId = 0)
        //{
        //    int Vbillno = 0, customerid = 0, customercompid = 0;
        //    string Billnoforprint = "", VBilldate = "", VCname = "", Proname1 = "", company = "", unit = "", Proname = "", custgstin = "", custpanno = "";
        //    decimal gst5amt = 0, gst12amt = 0, gst18amt = 0, gst28amt = 0, gst5discAmt = 0, gst12discAmt = 0, gst18discAmt = 0, gst28discAmt = 0, nontaxDiscAmt = 0,
        //        VDis = 0, VSpDis = 0, VBDiscount = 0, Totalnetamtfor0tax = 0, netamtfor0tax1 = 0, VGst = 0, VGstAmt = 0, VNamt = 0, VDetailtotamt = 0,
        //        pregst = 0, net5amt = 0, net12amt = 0, net18amt = 0, net28amt = 0, cess5amt = 0, cess12amt = 0, cess18amt = 0, cess28amt = 0, netamtfor0tax = 0,
        //        Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0, Sgst5amt = 0, Sgst12amt = 0, Sgst18amt = 0, Sgst28amt = 0, Igst5amt = 0, Igst12amt = 0,
        //        Igst18amt = 0, Igst28amt = 0, gstpercent = 0, cessamt = 0, taxable5amt = 0, taxable12amt = 0, taxable18amt = 0, taxable28amt = 0, taxable5amt1 = 0,
        //        taxable12amt1 = 0, taxable18amt1 = 0, taxable28amt1 = 0, SumDiscAmt = 0, SumSpDiscAmt = 0;
        //    bool flgtaxable = false, flgnotax = false;

        //    try
        //    {
        //        db.connect();
        //        SqlDataAdapter da;
        //        string toDate = "", fromDate = "", previousbill = "", user_Series = "";
        //        DataTable dtmaster;
        //        DSAllSale ds = new DSAllSale();
        //        bool userSettings = false;

        //        SettingRepository setRepo = new SettingRepository();
        //        Setting setData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
        //        if (setData != null)
        //        {
        //            if (setData.ShowUserDefinedSeries == "Y")
        //                userSettings = true;
        //        }
        //        if (from != "" && to != "")
        //        {
        //            toDate = cm.ValidToDate(Convert.ToDateTime(to));
        //            fromDate = cm.ValidFromDate(Convert.ToDateTime(from));
        //        }
        //        string strsql = "";
        //        if (fromDate != "" && toDate != "" && fromDate != null && toDate != null)
        //        {
        //            if (billtype == 1)
        //                strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.BillNo Where cm.Billdate>='" + fromDate + "' and cm.Billdate<='" + toDate + "'and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //            else if (billtype == 3)
        //                strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.BillNo Where cm.Billdate>='" + fromDate + "' and cm.Billdate<='" + toDate + "'and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //        }
        //        if (mode != "" && mode != null)
        //        {
        //            if (strsql != "")
        //            {
        //                if (mode == "Cash")
        //                    strsql = strsql + " and cm.RemainingAmount=0.00";
        //                else
        //                    strsql = strsql + " and cm.RemainingAmount!=0.00";
        //            }
        //            else
        //            {
        //                if (mode == "Cash")
        //                {
        //                    if (billtype == 1)
        //                        strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.RemainingAmount=0.00 and cm.BillNo like 'T%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "'";
        //                    else if (billtype == 3)
        //                        strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.RemainingAmount=0.00 and cm.BillNo like 'F%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "'";
        //                }
        //                else
        //                {
        //                    if (billtype == 1)
        //                        strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.RemainingAmount!=0.00 and cm.BillNo like 'T%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "'";
        //                    else if (billtype == 3)
        //                        strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.RemainingAmount!=0.00 and cm.BillNo like 'F%' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "'";
        //                }
        //            }
        //        }
        //        if (custid != 0)
        //        {
        //            if (strsql != "")
        //                strsql = strsql + " and cm.CustomerId='" + custid + "'";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.CustomerId='" + custid + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.CustomerId='" + custid + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //            }
        //        }
        //        if (rcptno != "")
        //        {
        //            if (strsql != "")
        //                strsql = strsql + " and cm.BillRcptNo='" + rcptno + "'";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.BillRcptNo='" + rcptno + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.BillRcptNo='" + rcptno + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //            }
        //        }
        //        if (itemname != "")
        //        {
        //            Proname1 = itemname.Split(',')[0];
        //            company = itemname.Split(',')[1];
        //            unit = itemname.Split(',')[2];
        //            Proname = "";

        //            if ((Proname1).Contains("'"))
        //                Proname = Proname1.Replace("'", "''");
        //            else
        //                Proname = Proname1;

        //            if (strsql != "")
        //                strsql = strsql + " and cd.ProductId='" + Proname + "' and cd.CompanyName='" + company + "' and cd.Unit='" + unit + "'";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cd.ProductId='" + Proname + "' and cd.CompanyName='" + company + "' and cd.Unit='" + unit + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cd.ProductId='" + Proname + "' and cd.CompanyName='" + company + "' and cd.Unit='" + unit + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //            }
        //        }
        //        if (companyname != "")
        //        {
        //            if (strsql != "")
        //                strsql = strsql + " and cd.CompanyName='" + companyname + "'";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cd.CompanyName='" + companyname + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cd.CompanyName='" + companyname + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //            }
        //        }
        //        if (custbillno != "")
        //        {
        //            if (strsql != "")
        //                strsql = strsql + " and cm.BillNo='" + custbillno + "'";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.BillNo='" + custbillno + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.BillNo='" + custbillno + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //            }
        //        }
        //        if (transType != "")
        //        {
        //            if (transType == "Cash")
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.BillNo Where cm.Billdate>='" + fromDate + "' and cm.Billdate<='" + toDate + "' and  cm.PaidAmount!=0 and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' ";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.BillNo Where cm.Billdate>='" + fromDate + "' and cm.Billdate<='" + toDate + "' and  cm.PaidAmount!=0 and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%' ";
        //            }
        //            else if (transType == "Day")
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.BillNo Where cm.Billdate>='" + fromDate + "' and cm.Billdate<='" + toDate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' ";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.BillNo Where cm.Billdate>='" + fromDate + "' and cm.Billdate<='" + toDate + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%' ";
        //            }
        //        }
        //        if (user_billSeries != "")
        //        {
        //            if (strsql != "")
        //                strsql = strsql + " and cm.userseries='" + user_billSeries + "'";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' and cm.userseries='" + user_billSeries + "'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%' and cm.userseries='" + user_billSeries + "'";
        //            }
        //        }
        //        if (SalesmanId != 0)
        //        {
        //            if (strsql != "")
        //                strsql = strsql + " and cm.StaffId=" + SalesmanId + "";
        //            else
        //            {
        //                if (billtype == 1)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.StaffId=" + SalesmanId + " and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%'";
        //                else if (billtype == 3)
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.StaffId=" + SalesmanId + " and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%'";
        //            }
        //        }
        //        if (strsql == "")
        //        {
        //            if (billtype == 1)
        //            {
        //                if (orderby == "BillNo")
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by len(cm.BillNo),cm.BillNo";
        //                else if (orderby == "CustomerName")
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by cm.CustomerName";
        //                else
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'T%' group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by cm.BillDate";
        //            }
        //            else if (billtype == 3)
        //            {
        //                if (orderby == "BillNo")
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%' group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by len(cm.BillNo),cm.BillNo";
        //                else if (orderby == "CustomerName")
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%' group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by cm.CustomerName";
        //                else
        //                    strsql = "Select  cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.Id=cd.Billno Where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' and cm.BillNo like 'F%' group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by cm.BillDate";
        //            }
        //        }
        //        else
        //        {
        //            if (orderby == "BillNo")
        //                strsql = strsql + " group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by len(cm.BillNo),cm.BillNo asc";
        //            else if (orderby == "CustomerName")
        //                strsql = strsql + " group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by cm.CustomerName asc";
        //            else
        //                strsql = strsql + " group by cm.ID,cm.Billdate,cm.CustomerName,cm.TotalAmount,cm.Discount,cm.NetAmount,cm.BillNo,cm.CustomerId,cm.compid,cm.Userseries order by cm.BillDate asc";
        //        }

        //        DataTable dtDetail, dtRecord;
        //        dtmaster = db.GetTable(strsql);
        //        try
        //        {
        //            for (int i = 0; i < dtmaster.Rows.Count; i++)
        //            {
        //                Vbillno = Convert.ToInt32(dtmaster.Rows[i]["ID"].ToString());
        //                Billnoforprint = dtmaster.Rows[i]["BillNo"].ToString();
        //                VBilldate = dtmaster.Rows[i]["Billdate"].ToString();
        //                VCname = dtmaster.Rows[i]["CustomerName"].ToString();
        //                customerid = Convert.ToInt32(dtmaster.Rows[i]["CustomerId"]);
        //                customercompid = Convert.ToInt32(dtmaster.Rows[i]["compid"]);
        //                VBDiscount = Convert.ToDecimal(dtmaster.Rows[i]["Discount"].ToString());
        //                VNamt = Convert.ToDecimal(dtmaster.Rows[i]["NetAmount"].ToString());
        //                VDis = 0; VSpDis = 0;
        //                cessamt = 0;
        //                user_Series = dtmaster.Rows[i]["Userseries"].ToString();

        //                if (itemname == "")
        //                {
        //                    //                                   0               1               2                   3           4       5                       6           7       8               9                                                                10        11              12          13          14          15
        //                    dtDetail = db.GetTable("select cd.SGstPercent,cd.Cgstpercent,cd.IGstpercent as Igst,cd.SGstAmt,cd.CgstAmt,cd.IGstAmt as IgstAmt,cd.Amount,cd.Discount,cd.Rate,case when cd.SaleRate is null then 0 else cd.SaleRate end as salerate,cd.Quantity,cd.TotalAmount,cd.ProductId,cd.Companyname,cd.unit,cd.SaleUnit, " +
        //                        //              16                                      17
        //                                  "cd.BillNo,case when cd.cessamt is null then 0 else cd.cessamt end as cessamt,Isnull(cd.SpcDisc,0) as SpcDisc from CustomerBillDetail as cd where cd.BillNo =" + Vbillno + " order by cd.billno,cd.Sgstpercent");
        //                }
        //                else
        //                {
        //                    //                                   0               1               2                   3           4       5                       6           7       8               9                                                                10        11              12          13          14          15
        //                    dtDetail = db.GetTable("select cd.SGstPercent,cd.Cgstpercent,cd.IGstpercent as Igst,cd.SGstAmt,cd.CgstAmt,cd.IGstAmt as IgstAmt,cd.Amount,cd.Discount,cd.Rate,case when cd.SaleRate is null then 0 else cd.SaleRate end as salerate,cd.Quantity,cd.TotalAmount,cd.ProductId,cd.Companyname,cd.unit,cd.SaleUnit," +
        //                        //              16                                      17
        //                                  "cd.BillNo,case when cd.cessamt is null then 0 else cd.cessamt end as cessamt,Isnull(cd.SpcDisc,0) as SpcDisc from CustomerBillDetail as cd where cd.BillNo =" + Vbillno + " and cd.ProductId='" + Proname + "' and cd.CompanyName='" + company + "' and cd.Unit='" + unit + "' order by cd.billno,cd.Sgstpercent");
        //                }

        //                VDis = 0; VSpDis = 0;
        //                VGstAmt = 0;
        //                flgnotax = false;
        //                flgtaxable = false;
        //                netamtfor0tax = 0;
        //                for (int K = 0; K < dtDetail.Rows.Count; K++)
        //                {
        //                    decimal saleqty = 0;
        //                    VGstAmt = 0;
        //                    VGst = 0;
        //                    if (dtDetail.Rows[K]["unit"].ToString() != dtDetail.Rows[K]["SaleUnit"].ToString())
        //                    {
        //                        ProductRepository ProductRepo = new ProductRepository();
        //                        ProductInformation proddata = new ProductInformation();
        //                        if (CommonMethod.commProduct == true)
        //                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtDetail.Rows[K]["ProductId"].ToString() && t.ManufactureCompany == dtDetail.Rows[K]["Companyname"].ToString() && t.Uniti == dtDetail.Rows[K]["unit"].ToString()).FirstOrDefault();
        //                        else
        //                            proddata = ProductRepo.GetAll().Where(t => t.ProductName == dtDetail.Rows[K]["ProductId"].ToString() && t.ManufactureCompany == dtDetail.Rows[K]["Companyname"].ToString() && t.Uniti == dtDetail.Rows[K]["unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
        //                        if (proddata != null)
        //                        {
        //                            if ((proddata.AlternateUnit == null) && (proddata.UnitQty == null))
        //                                saleqty = Convert.ToDecimal(dtDetail.Rows[K]["Quantity"].ToString());
        //                            else
        //                            {
        //                                if (dtDetail.Rows[K]["SaleUnit"].ToString() == proddata.AlternateUnit)
        //                                    saleqty = Convert.ToDecimal(Convert.ToDecimal(dtDetail.Rows[K]["Quantity"].ToString()) / proddata.UnitQty);
        //                                else
        //                                    saleqty = Convert.ToDecimal(Convert.ToDecimal(dtDetail.Rows[K]["Quantity"].ToString()) / proddata.AlternateUnitQty);
        //                            }
        //                        }
        //                        ProductRepo.Dispose();
        //                    }

        //                    CustomerRepository custrepo = new CustomerRepository();
        //                    CustomerInformation custinfo = custrepo.GetAll().Where(t => t.CustomerID == customerid && t.CompId == customercompid).FirstOrDefault();
        //                    if (custinfo != null)
        //                    {
        //                        custgstin = custinfo.BuyerTinNo;
        //                        custpanno = custinfo.Panno;
        //                    }

        //                    if (saleqty == 0)
        //                        saleqty = Convert.ToDecimal(dtDetail.Rows[K]["Quantity"].ToString());

        //                    VDis = Convert.ToDecimal(dtDetail.Rows[K]["Discount"].ToString());
        //                    VSpDis = Convert.ToDecimal(dtDetail.Rows[K]["SpcDisc"].ToString());
        //                    //if (Convert.ToDecimal(dtDetail.Rows[K]["Rate"].ToString()) != Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString()) && Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString()) != 0)   //if taxinclusive
        //                    //{
        //                    //    decimal inclusiveamt = 0;
        //                    //    inclusiveamt = (saleqty * Convert.ToDecimal(dtDetail.Rows[K]["Rate"].ToString()));
        //                    //    VTamt = VTamt + inclusiveamt;
        //                    //}
        //                    //else
        //                    //    VTamt = VTamt + (saleqty * Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString()));
        //                    decimal vamt = 0, discamt = 0, spdiscamt = 0;
        //                    if (Convert.ToDecimal(dtDetail.Rows[K]["Rate"].ToString()) != Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString()) && Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString()) != 0)  //if taxinclusive
        //                    {
        //                        //decimal vamt = 0, discamt = 0, spdiscamt = 0;
        //                        vamt = (Convert.ToDecimal(dtDetail.Rows[K]["Rate"].ToString()) * saleqty);
        //                        if (VDis != 0)
        //                        {
        //                            discamt = vamt * VDis / 100;
        //                            vamt = vamt - discamt;
        //                            SumDiscAmt = SumDiscAmt + discamt;
        //                        }
        //                        if (VSpDis != 0)
        //                        {
        //                            spdiscamt = vamt * VSpDis / 100;
        //                            vamt = vamt - spdiscamt;
        //                            SumSpDiscAmt = SumSpDiscAmt + spdiscamt;
        //                        }
        //                        VDetailtotamt = vamt;
        //                    }
        //                    else
        //                    {
        //                        if (Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString()) == 0)
        //                            VDetailtotamt = saleqty * Convert.ToDecimal(dtDetail.Rows[K]["Rate"].ToString());
        //                        else
        //                            VDetailtotamt = saleqty * Convert.ToDecimal(dtDetail.Rows[K]["salerate"].ToString());
        //                        if (VDis != 0)
        //                        {
        //                            discamt = VDetailtotamt * VDis / 100;
        //                            VDetailtotamt = VDetailtotamt - discamt;
        //                            SumDiscAmt = SumDiscAmt + discamt;
        //                        }
        //                        if (VSpDis != 0)
        //                        {
        //                            spdiscamt = VDetailtotamt * VSpDis / 100;
        //                            VDetailtotamt = VDetailtotamt - spdiscamt;
        //                            SumSpDiscAmt = SumSpDiscAmt + spdiscamt;
        //                        }
        //                        //VDetailtotamt = VDetailtotamt - (VDetailtotamt * VDis / 100);
        //                    }
        //                    if (dtDetail.Rows[K]["SGstPercent"].ToString() != null && dtDetail.Rows[K]["SGstPercent"].ToString() != "0.00")
        //                    {
        //                        pregst = Convert.ToDecimal(dtDetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["Cgstpercent"].ToString());
        //                        if (pregst != VGst)
        //                            VGstAmt = 0;
        //                        VGst = Convert.ToDecimal(dtDetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["Cgstpercent"].ToString());
        //                        VGstAmt = VGstAmt + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString());
        //                    }
        //                    else
        //                    {
        //                        pregst = Convert.ToDecimal(dtDetail.Rows[K]["Igst"].ToString());
        //                        if (pregst != VGst)
        //                            VGstAmt = 0;
        //                        VGst = Convert.ToDecimal(dtDetail.Rows[K]["Igst"].ToString());
        //                        VGstAmt = VGstAmt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString());
        //                    }
        //                    if (userSettings == true)
        //                        Billnoforprint = user_Series;
        //                    /*-----------------------Gst Calculation */
        //                    if (previousbill == "")
        //                        previousbill = Billnoforprint;
        //                    else if (previousbill != Billnoforprint)
        //                    {
        //                        taxable5amt1 = 0;
        //                        taxable12amt1 = 0;
        //                        taxable18amt1 = 0;
        //                        taxable28amt1 = 0;
        //                        netamtfor0tax = 0;
        //                        previousbill = Billnoforprint;
        //                    }
        //                    gstpercent = Convert.ToDecimal(dtDetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["Cgstpercent"].ToString());
        //                    if ((Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString()) != 0))
        //                    {
        //                        if (gstpercent == 5)
        //                        {
        //                            Cgst5amt = Cgst5amt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString());
        //                            Sgst5amt = Sgst5amt + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString());
        //                            taxable5amt = taxable5amt + VDetailtotamt;   // this amount is required to show sum in tax detail table
        //                            taxable5amt1 = taxable5amt1 + VDetailtotamt; // this amount is required to show in report under group section
        //                            net5amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess5amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                        if (gstpercent == 12)
        //                        {
        //                            Cgst12amt = Cgst12amt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString());
        //                            Sgst12amt = Sgst12amt + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString());
        //                            taxable12amt = taxable12amt + VDetailtotamt;
        //                            taxable12amt1 = taxable12amt1 + VDetailtotamt;
        //                            net12amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess12amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                        if (gstpercent == 18)
        //                        {
        //                            Cgst18amt = Cgst18amt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString());
        //                            Sgst18amt = Sgst18amt + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString());
        //                            taxable18amt = taxable18amt + VDetailtotamt;
        //                            taxable18amt1 = taxable18amt1 + VDetailtotamt;
        //                            net18amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess18amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                        if (gstpercent == 28)
        //                        {
        //                            Cgst28amt = Cgst28amt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString());
        //                            Sgst28amt = Sgst28amt + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString());
        //                            taxable28amt = taxable28amt + VDetailtotamt;
        //                            taxable28amt1 = taxable28amt1 + VDetailtotamt;
        //                            net28amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["CgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess28amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                    }

        //                    if ((Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString()) != 0))
        //                    {
        //                        if (Convert.ToDecimal(dtDetail.Rows[K]["Igst"].ToString()) == 5)
        //                        {
        //                            Igst5amt = Igst5amt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString());
        //                            taxable5amt = taxable5amt + VDetailtotamt;
        //                            taxable5amt1 = taxable5amt1 + VDetailtotamt;
        //                            net5amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess5amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                        if (Convert.ToDecimal(dtDetail.Rows[K]["Igst"].ToString()) == 12)
        //                        {
        //                            Igst12amt = Igst12amt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString());
        //                            taxable12amt = taxable12amt + VDetailtotamt;
        //                            taxable12amt1 = taxable12amt1 + VDetailtotamt;
        //                            net12amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess12amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                        if (Convert.ToDecimal(dtDetail.Rows[K]["Igst"].ToString()) == 18)
        //                        {
        //                            Igst18amt = Igst18amt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString());
        //                            taxable18amt = taxable18amt + VDetailtotamt;
        //                            taxable18amt1 = taxable18amt1 + VDetailtotamt;
        //                            net18amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess18amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                        if (Convert.ToDecimal(dtDetail.Rows[K]["Igst"].ToString()) == 28)
        //                        {
        //                            Igst28amt = Igst28amt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString());
        //                            taxable28amt = taxable28amt + VDetailtotamt;
        //                            taxable28amt1 = taxable28amt1 + VDetailtotamt;
        //                            net28amt += VDetailtotamt + Convert.ToDecimal(dtDetail.Rows[K]["IgstAmt"].ToString()) + Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                            cess28amt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                        }
        //                    }

        //                    if (VGst == 5 && VGstAmt != 0)
        //                        gst5amt = gst5amt + VGstAmt;
        //                    if (VGst == 12 && VGstAmt != 0)
        //                        gst12amt = gst12amt + VGstAmt;
        //                    if (VGst == 18 && VGstAmt != 0)
        //                        gst18amt = gst18amt + VGstAmt;
        //                    if (VGst == 28 && VGstAmt != 0)
        //                        gst28amt = gst28amt + VGstAmt;

        //                    if (gst5amt == 0 && gst12amt == 0 && gst18amt == 0 && gst28amt == 0 && flgtaxable == false)
        //                        cessamt += Convert.ToDecimal(dtDetail.Rows[K]["cessamt"].ToString());
        //                    else
        //                        flgtaxable = true;
        //                    if (VGstAmt == 0)
        //                    {
        //                        flgnotax = true;
        //                        netamtfor0tax += VDetailtotamt;
        //                    }
        //                }
        //                if (VBDiscount != 0)
        //                {
        //                    gst5discAmt = gst5discAmt + (net5amt * VBDiscount / 100);
        //                    net5amt = net5amt - (net5amt * VBDiscount / 100);
        //                    decimal b = Math.Round(net5amt), c = 0;
        //                    if (net5amt > b)
        //                    {
        //                        c = net5amt - b;
        //                        net5amt = Convert.ToDecimal(net5amt) - Convert.ToDecimal(c);
        //                    }
        //                    else
        //                    {
        //                        c = b - net5amt;
        //                        net5amt = Convert.ToDecimal(net5amt) + Convert.ToDecimal(c);
        //                    }

        //                    gst12discAmt = gst12discAmt + (net12amt * VBDiscount / 100);
        //                    net12amt = net12amt - (net12amt * VBDiscount / 100);
        //                    decimal b1 = Math.Round(net12amt), c1 = 0;
        //                    if (net12amt > b1)
        //                    {
        //                        c1 = net12amt - b1;
        //                        net12amt = Convert.ToDecimal(net12amt) - Convert.ToDecimal(c1);
        //                    }
        //                    else
        //                    {
        //                        c1 = b1 - net12amt;
        //                        net12amt = Convert.ToDecimal(net12amt) + Convert.ToDecimal(c1);
        //                    }

        //                    gst18discAmt = gst18discAmt + (net18amt * VBDiscount / 100);
        //                    net18amt = net18amt - (net18amt * VBDiscount / 100);
        //                    decimal b2 = Math.Round(net18amt), c2 = 0;
        //                    if (net18amt > b2)
        //                    {
        //                        c2 = net18amt - b2;
        //                        net18amt = Convert.ToDecimal(net18amt) - Convert.ToDecimal(c2);
        //                    }
        //                    else
        //                    {
        //                        c2 = b2 - net18amt;
        //                        net18amt = Convert.ToDecimal(net18amt) + Convert.ToDecimal(c2);
        //                    }

        //                    gst28discAmt = gst28discAmt + (net28amt * VBDiscount / 100);
        //                    net28amt = net28amt - (net28amt * VBDiscount / 100);
        //                    decimal b3 = Math.Round(net28amt), c3 = 0;
        //                    if (net28amt > b3)
        //                    {
        //                        c3 = net28amt - b3;
        //                        net28amt = Convert.ToDecimal(net28amt) - Convert.ToDecimal(c3);
        //                    }
        //                    else
        //                    {
        //                        c3 = b3 - net28amt;
        //                        net28amt = Convert.ToDecimal(net28amt) + Convert.ToDecimal(c3);
        //                    }

        //                    Totalnetamtfor0tax = Totalnetamtfor0tax + netamtfor0tax;
        //                    nontaxDiscAmt = nontaxDiscAmt + (netamtfor0tax * VBDiscount / 100);
        //                    netamtfor0tax1 = netamtfor0tax - (netamtfor0tax * VBDiscount / 100);

        //                    decimal b4 = Math.Round(netamtfor0tax), c4 = 0;
        //                    if (netamtfor0tax > b4)
        //                    {
        //                        c4 = netamtfor0tax - b4;
        //                        netamtfor0tax = Convert.ToDecimal(netamtfor0tax) - Convert.ToDecimal(c4);
        //                    }
        //                    else
        //                    {
        //                        c4 = b4 - netamtfor0tax;
        //                        netamtfor0tax = Convert.ToDecimal(netamtfor0tax) + Convert.ToDecimal(c4);
        //                    }
        //                }
        //                else
        //                {
        //                    netamtfor0tax1 = netamtfor0tax;
        //                    Totalnetamtfor0tax = Totalnetamtfor0tax + netamtfor0tax;
        //                }
        //                dtRecord = ds.Tables["AllSaleDetails"];
        //                for (int j = 0; j < 6; j++)
        //                {
        //                    if (j == 0 && gst5amt != 0)
        //                        dtRecord.Rows.Add(new object[11] { Billnoforprint, VBilldate, VCname, taxable5amt1, 5, gst5amt, net5amt, custgstin, custpanno, cess5amt, VBDiscount });
        //                    else if (j == 1 && gst12amt != 0)
        //                        dtRecord.Rows.Add(new object[11] { Billnoforprint, VBilldate, VCname, taxable12amt1, 12, gst12amt, net12amt, custgstin, custpanno, cess12amt, VBDiscount });
        //                    else if (j == 2 && gst18amt != 0)
        //                        dtRecord.Rows.Add(new object[11] { Billnoforprint, VBilldate, VCname, taxable18amt1, 18, gst18amt, net18amt, custgstin, custpanno, cess18amt, VBDiscount });
        //                    else if (j == 3 && gst28amt != 0)
        //                        dtRecord.Rows.Add(new object[11] { Billnoforprint, VBilldate, VCname, taxable28amt1, 28, gst28amt, net28amt, custgstin, custpanno, cess28amt, VBDiscount });
        //                    else if (j == 4 && gst5amt == 0 && gst12amt == 0 && gst18amt == 0 && gst28amt == 0 && flgtaxable == false)  //if bill has no tax
        //                        dtRecord.Rows.Add(new object[11] { Billnoforprint, VBilldate, VCname, netamtfor0tax, 0, 0, VNamt, custgstin, custpanno, cessamt, VBDiscount });
        //                    else if (j == 5 && flgnotax == true && flgtaxable == true) // if same bill has two types of product withtax and withouttax
        //                        dtRecord.Rows.Add(new object[11] { Billnoforprint, VBilldate, VCname, netamtfor0tax, 0, 0, netamtfor0tax1, custgstin, custpanno, cessamt, VBDiscount });
        //                }

        //                gst5amt = 0;
        //                gst12amt = 0;
        //                gst18amt = 0;
        //                gst28amt = 0;
        //                net5amt = 0;
        //                net12amt = 0;
        //                net18amt = 0;
        //                net28amt = 0;
        //                cessamt = 0;
        //                cess5amt = 0;
        //                cess12amt = 0;
        //                cess18amt = 0;
        //                cess28amt = 0;
        //            }
        //        }
        //        catch (Exception)
        //        { }
        //        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
        //        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        //        da.Fill(ds, "CompanyInformation");

        //        salerpt = new AllSaleCrystalReport();
        //        salerpt.SetDataSource(ds);

        //        salerpt.SetParameterValue("Cgst5amt", Cgst5amt);
        //        salerpt.SetParameterValue("Cgst12amt", Cgst12amt);
        //        salerpt.SetParameterValue("Cgst18amt", Cgst18amt);
        //        salerpt.SetParameterValue("Cgst28amt", Cgst28amt);
        //        salerpt.SetParameterValue("Sgst5amt", Sgst5amt);
        //        salerpt.SetParameterValue("Sgst12amt", Sgst12amt);
        //        salerpt.SetParameterValue("Sgst18amt", Sgst18amt);
        //        salerpt.SetParameterValue("Sgst28amt", Sgst28amt);
        //        salerpt.SetParameterValue("Igst5amt", Igst5amt);
        //        salerpt.SetParameterValue("Igst12amt", Igst12amt);
        //        salerpt.SetParameterValue("Igst18amt", Igst18amt);
        //        salerpt.SetParameterValue("Igst28amt", Igst28amt);
        //        salerpt.SetParameterValue("taxable5amt", taxable5amt);
        //        salerpt.SetParameterValue("taxable12amt", taxable12amt);
        //        salerpt.SetParameterValue("taxable18amt", taxable18amt);
        //        salerpt.SetParameterValue("taxable28amt", taxable28amt);
        //        salerpt.SetParameterValue("GST5DiscAmt", gst5discAmt);
        //        salerpt.SetParameterValue("GST12DiscAmt", gst12discAmt);
        //        salerpt.SetParameterValue("GST18DiscAmt", gst18discAmt);
        //        salerpt.SetParameterValue("GST28DiscAmt", gst28discAmt);
        //        salerpt.SetParameterValue("GST0DiscAmt", nontaxDiscAmt);
        //        salerpt.SetParameterValue("netamt0tax", Totalnetamtfor0tax);
        //        salerpt.SetParameterValue("SumDiscAmt", SumDiscAmt);
        //        salerpt.SetParameterValue("SumSpDiscAmt", SumSpDiscAmt);
        //        crystalReportViewer1.ReportSource = salerpt;
        //        crystalReportViewer1.Refresh();
        //        db.CloseConnection();
        //        da.Dispose();
        //        ds.Dispose();
        //    }
        //    catch (Exception)
        //    { db.CloseConnection(); }
        //}

        public void Getdata(int billtype, string from = "", string to = "", string mode = "", int custid = 0, string rcptno = "", string itemname = "", string companyname = "", string custbillno = "", string transType = "", string user_billSeries = "", string orderby = "", int SalesmanId = 0,int rootid=0)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSAllPurchase ds = new DSAllPurchase();
                string toDate = "", fromDate = "";
                if (from != "" && to != "")
                {
                    toDate = cm.ValidToDate(Convert.ToDateTime(to));
                    fromDate = cm.ValidFromDate(Convert.ToDateTime(from));
                }
                if (rootid==0)
                {
                   
                da = new SqlDataAdapter("SPAllPurchaseInvoiceList", db.Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = "Invoice";
                da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                if (fromDate != "" && fromDate != null)
                    da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fromDate;
                if (toDate != "" && toDate != null)
                    da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = toDate;
                if (billtype != 0 && billtype != null)
                    da.SelectCommand.Parameters.Add("@Billtype", SqlDbType.Int).Value = billtype;
                if (rcptno != "" && rcptno != null)
                    da.SelectCommand.Parameters.Add("@BillRcptNo", SqlDbType.VarChar).Value = rcptno;
                if (custbillno != "" && custbillno != null)
                    da.SelectCommand.Parameters.Add("@BillNoM", SqlDbType.VarChar).Value = custbillno;
                if (user_billSeries != "" && user_billSeries != null)
                    da.SelectCommand.Parameters.Add("@UserSeries", SqlDbType.VarChar).Value = user_billSeries;
                if (mode != "" && mode != null)
                    da.SelectCommand.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode;
                if (companyname != "" && companyname != null)
                    da.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = companyname;
                if (custid != 0 && custid != null)
                    da.SelectCommand.Parameters.Add("@CustId", SqlDbType.Int).Value = custid;
                if (SalesmanId != 0 && SalesmanId != null)
                    da.SelectCommand.Parameters.Add("@StaffId", SqlDbType.Int).Value = SalesmanId;
                if (itemname != "" && itemname != null)
                    da.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = itemname;
                if (transType != "" && transType != null)
                    da.SelectCommand.Parameters.Add("@TransType", SqlDbType.VarChar).Value = transType;
                da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                da.Fill(ds, "tblAllPurchaseListReport");

                }
                else
                {
                    da = new SqlDataAdapter("SELECT     dbo.CustomerBillMaster.Billdate,dbo.CustomerBillMaster.BillNo, dbo.CustomerInformation.CustomerName, dbo.CustomerBillMaster.NetAmount FROM dbo.CustomerBillMaster INNER JOIN dbo.CustomerInformation ON dbo.CustomerBillMaster.CustomerId = dbo.CustomerInformation.Id where dbo.CustomerInformation.RouteId='"+rootid+"' and dbo.CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "RootWiseDataSale");
                }

                da = new SqlDataAdapter("select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                da = new SqlDataAdapter("select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                if (rootid==0)
                {
                    AllInvoiceCrystalReport purchaserpt = new AllInvoiceCrystalReport();
                    purchaserpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    RootWiseSaleReport purchaserpt = new RootWiseSaleReport();
                    purchaserpt.SetDataSource(ds);
                    string result = "0";
                    string myQuery = "SELECT    SUM(dbo.CustomerBillMaster.NetAmount) FROM dbo.CustomerBillMaster INNER JOIN dbo.CustomerInformation ON dbo.CustomerBillMaster.CustomerId = dbo.CustomerInformation.Id where dbo.CustomerInformation.RouteId='5' and dbo.CustomerBillMaster.CompId='1'";
                 
                    SqlCommand cmd = new SqlCommand(myQuery,  db.Connection);
                  
                    //System.NullReferenceException occurs when their is no data/result
                    string getValue = cmd.ExecuteScalar().ToString();
                    if (getValue != null)
                    {
                        result = getValue.ToString();
                    }
                    db.CloseConnection();
                    purchaserpt.SetParameterValue("NetAmt", result);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
        
        public void Getdata(string mode)
        {
            try
            {
                if (mode == "Cash")
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select * from CustomerBillMaster Where RemainingAmount=0.00 and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DSAllSale ds = new DSAllSale();
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerBillMaster");

                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    salerpt = new AllSaleCrystalReport();
                    salerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = salerpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select * from CustomerBillMaster Where RemainingAmount!=0.00 and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DSAllSale ds = new DSAllSale();
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerBillMaster");

                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    salerpt = new AllSaleCrystalReport();
                    salerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = salerpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(int custid)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from CustomerBillMaster Where CustomerId='" + custid + "' and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllSale ds = new DSAllSale();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerBillMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                salerpt = new AllSaleCrystalReport();
                salerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from CustomerBillMaster Where BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllSale ds = new DSAllSale();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerBillMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                salerpt = new AllSaleCrystalReport();
                salerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string cbn = Convert.ToString(txtbillno.Text);
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                var custdata = Custmasterrepo.GetAll().Where(t => t.BillNo == cbn && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                    Getdata(1, "", "", "", 0, "", "", "", cbn);
                else
                {
                    MessageBox.Show("This Invoice Number is Not Present.", "Success");
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

        private void btnexport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog.Filter = "Document (*.pdf)|*.PDF";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "Export PDF File To";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    salerpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, saveFileDialog.FileName);
                    MessageBox.Show("Exported Successful");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while converting into PDF file.");
            }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            txtbillno.Text = "";
            string from = "", to = "", mode = "", rcptno = "", itemname = "", companyname = "", cbn = "";
            Getdata(1, from, to, mode, 0, rcptno, itemname, companyname, cbn);
        }
    }
}