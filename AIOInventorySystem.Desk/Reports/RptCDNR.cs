using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptCDNR : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public RptCDNR()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
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

        public void getall(string fdate, string tdate)
        {

            string gstinno, billno, VoucherNo, DType = "", Reason = "", Placeofsupply = "", PreGst = "", errormsg = "", previousbill = "";
            DateTime billdate, Voucherdate;
            decimal VoucherValue = 0, gstrate, taxableamt = 0, cessamt, TotalNetAmt = 0;

            int billcustomerid = 0;
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSCDNR ds = new DSCDNR();
                DataTable datacdnr;
                bool userSetting = false;

                SettingRepository setrepo = new SettingRepository();
                Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (setData != null)
                {
                    if (setData.ShowUserDefinedSeries == "Y")
                        userSetting = true;
                    else
                        userSetting = false;
                }
                setrepo.Dispose();

                DataTable dtdetail = db.Gettable("SELECT  CustomerId,BillId,BillDate,Type FROM [RemainingPayment] where (Type='BillReturn' or Type='credit Note' or Type='Debit Note')" +
                                    " and CreateDate>='" + fdate + "' and CreateDate<='" + tdate + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and BillId like 'T%'");

                if (dtdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtdetail.Rows.Count; i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        billcustomerid = Convert.ToInt32(dtdetail.Rows[i]["CustomerId"]);
                        DataTable dtcust = db.Gettable("select cust.state,cust.statecode as cstatecode,comp.statecode as costatecode,cust.buyertinno from customerinformation as cust join companyinformation as comp on cust.compid=comp.companyid where comp.companyid='" + CommonMethod.CompId + "' and cust.buyertinno!='' and cust.customerid=" + billcustomerid);
                        if (dtcust.Rows.Count > 0)
                        {
                            errormsg = "Error on getting quantity details having bill no " + dtdetail.Rows[i]["BillId"].ToString();
                            DataTable dtBill = new DataTable();
                            if (dtdetail.Rows[i]["Type"].ToString() == "BillReturn")
                                dtBill = db.Gettable("Select cm.GoodReturnNo as No,cm.GoodReturnDate as Date,cd.Amount as Amt,case when (cd.SGstPercent is null or cd.SGstPercent=0) then cd.IGstPercent else (cd.SGstPercent+cd.CGstPercent) end as gst,case when cd.CessAmt is null then 0 else cd.CessAmt end as cessAmt,cd.TotalAmount,cd.Reason from GoodReturnMaster as cm join GoodReturnDetail as cd on cm.Id=cd.GoodReturnNo where cm.BillNo='" + dtdetail.Rows[i]["BillId"].ToString() + "'");
                            else
                                dtBill = db.Gettable("Select cm.CustCreditDebitNo as No,cm.CustCreditDebitDate as Date,cd.Amount as Amt,case when (cd.SGstPercent is null or cd.SGstPercent=0) then cd.IGstPercent else (cd.SGstPercent+cd.CGstPercent) end as gst,case when cd.CessAmt is null then 0 else cd.CessAmt end as cessAmt,cd.TotalAmount,cd.Reason from CustCreditDebitMaster as cm join CustCreditDebitDetail as cd on cm.Id=cd.CustCreditDebitNo where cm.BillNo='" + dtdetail.Rows[i]["BillId"].ToString() + "' and cm.CDType='" + dtdetail.Rows[i]["Type"].ToString() + "'");

                            if (dtBill.Rows.Count > 0)
                            {
                                string user_Series = "";
                                gstinno = dtcust.Rows[0]["buyertinno"].ToString();
                                CustomerBillMasterRepository custmastrepo = new CustomerBillMasterRepository();
                                CustomerBillMaster custbillData = custmastrepo.GetAll().Where(t => t.BillNo == dtdetail.Rows[i]["BillId"].ToString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                if (custbillData != null)
                                    user_Series = Convert.ToString(custbillData.UserSeries) == null ? "" : Convert.ToString(custbillData.UserSeries);
                                custmastrepo.Dispose();

                                if (userSetting == false)
                                    billno = dtdetail.Rows[i]["BillId"].ToString();
                                else
                                    billno = user_Series;

                                billdate = Convert.ToDateTime(dtdetail.Rows[i]["BillDate"].ToString());
                                VoucherNo = dtBill.Rows[0]["No"].ToString();
                                Voucherdate = Convert.ToDateTime(dtBill.Rows[0]["Date"].ToString());

                                if (dtdetail.Rows[i]["Type"].ToString() == "Debit Note")
                                    DType = "D";
                                else
                                    DType = "C";
                                Reason = dtBill.Rows[0]["Reason"].ToString();
                                Placeofsupply = dtcust.Rows[0]["cstatecode"].ToString() + '-' + dtcust.Rows[0]["state"].ToString();
                                VoucherValue = Convert.ToDecimal(dtBill.Rows[0]["TotalAmount"].ToString());
                                gstrate = Convert.ToDecimal(dtBill.Rows[0]["gst"].ToString());
                                taxableamt = Convert.ToDecimal(dtBill.Rows[0]["Amt"].ToString());
                                cessamt = Convert.ToDecimal(dtBill.Rows[0]["cessAmt"].ToString());
                                PreGst = "N";
                                if (previousbill == "" || previousbill != billno)
                                {
                                    previousbill = billno;
                                    TotalNetAmt += VoucherValue;
                                }
                                datacdnr = ds.Tables["CDNRTransaction"];
                                datacdnr.Rows.Add(new object[13] { gstinno, billno, billdate, VoucherNo, Voucherdate, DType, Reason, Placeofsupply, VoucherValue, gstrate, taxableamt, cessamt, PreGst });
                            }
                        }
                    }
                }

                da = new SqlDataAdapter("Select * from CustomerInformation  where CompId='" + CommonMethod.CompId + "' and BuyerTinNo!=''", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                CDNRSummaryReport salerpt = new CDNRSummaryReport();
                salerpt.SetDataSource(ds);
                salerpt.SetParameterValue("fromdate", dtpMonth.Value);
                salerpt.SetParameterValue("Todate", dtptomonth.Value);
                salerpt.SetParameterValue("TotalInvoiceValue", TotalNetAmt);
                crystalReportViewer1.ReportSource = salerpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show(errormsg, "CDNR Report");
                db.CloseConnection();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                getall(cm.ValidFromDate(dtpMonth.Value.Date), cm.ValidToDate(dtptomonth.Value.Date));
            }
            catch (Exception)
            { }
        }
    }
}
