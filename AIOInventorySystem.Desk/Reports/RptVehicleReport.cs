using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptVehicleReport : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        VehicleCrystalReport VCRpt = new VehicleCrystalReport();

        public RptVehicleReport()
        {
            InitializeComponent();
        }

        public RptVehicleReport(string vehicleno = "", string fromdate = "", string todate = "", string customername = "")
        {
            InitializeComponent();
            getdata(vehicleno, fromdate, todate, customername);
        }

        public void getdata(string vehicleno, string fromdate, string todate, string customername)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSVehicleReport ds = new DSVehicleReport();
                string strsql = "", fromDate = "", ToDate = "", custname = "", vehicleNo = "";
                custname = customername;
                vehicleNo = vehicleno;
                if (fromdate != "" && todate != "")
                {
                    fromDate = Convert.ToString(cm.ValidFromDate(Convert.ToDateTime(fromdate)));
                    ToDate = Convert.ToString(cm.ValidToDate(Convert.ToDateTime(todate)));
                }
                if (vehicleNo != "")
                {
                    if (strsql != "")
                        strsql = strsql + "and dc.VehicalNo='" + vehicleNo + "'";
                    else
                        strsql = "select cm.BillNo,cm.Billdate,cm.DcDate ,cm.CustomerName,cm.Narration as 'Village',cm.NetAmount,cm.DCNo,dc.VehicalNo as 'VehicleNo' from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on cm.DCNo=dc.DeliveryChallanNo where dc.VehicalNo='" + vehicleNo + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' ";
                }
                if (fromDate != "" && ToDate != "" && fromDate != null && ToDate != null)
                {
                    if (strsql != "")
                        strsql = strsql + "and Billdate>='" + fromDate + "' and Billdate<='" + ToDate + "'";
                    else
                        strsql = "select cm.BillNo,cm.Billdate,cm.DcDate ,cm.CustomerName,cm.Narration as 'Village',cm.NetAmount,cm.DCNo,dc.VehicalNo as 'VehicleNo' from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on cm.DCNo=dc.DeliveryChallanNo where Billdate>='" + fromDate + "' and Billdate<='" + ToDate + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' ";
                }
                if (custname != "")
                {
                    if (strsql != "")
                        strsql = strsql + "and cm.CustomerName='" + custname + "'";
                    else
                        strsql = "select cm.BillNo,cm.Billdate,cm.DcDate ,cm.CustomerName,cm.Narration as 'Village',cm.NetAmount,cm.DCNo,dc.VehicalNo as 'VehicleNo' from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on cm.DCNo=dc.DeliveryChallanNo where cm.CustomerName='" + custname + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "'";
                }
                if (strsql == "")
                    strsql = "select cm.BillNo,cm.Billdate,cm.DcDate ,cm.CustomerName,cm.Narration as 'Village',cm.NetAmount,cm.DCNo,dc.VehicalNo as 'VehicleNo' from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on cm.DCNo=dc.DeliveryChallanNo where cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo";
                else
                    strsql = strsql + "order by Billdate,BillNo";

                da = new SqlDataAdapter(strsql, db.Connection);
                da.Fill(ds, "VehicleTable");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.Fill(ds, "CompanyInformation");

                VCRpt = new VehicleCrystalReport();
                VCRpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = VCRpt;
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
        }
    }
}