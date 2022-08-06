using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptCustomerCreditDebitNote : Form
    {
        DbClass db = new DbClass();
        CustomerCreditDebitReport custRpt = new CustomerCreditDebitReport();

        public RptCustomerCreditDebitNote()
        {
            InitializeComponent();
        }

        public RptCustomerCreditDebitNote(int custCDNo)
        {
            InitializeComponent();
            getdata(custCDNo);
        }

        public void getdata(int creditDebitNo)
        {
            try
            {
                bool userSetting = false;
                decimal gstpercent = 0, Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0,
                    TotCgstAmt = 0, TotIgstAmt = 0, amount = 0, cgstAmt = 0, igstAmt = 0;
                string noteuserSeries = "", CDType = "", customername = "", address = "", custMobileno = "", GSTIN = "", state = "", statecode = "";
                int CDNoteId = 0, CustId = 0;

                db.connect();

                SettingRepository setrepo = new SettingRepository();
                Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (setData != null)
                {
                    if (setData.ShowUserDefinedSeries == "Y")
                        userSetting = true;
                }
                setrepo.Dispose();

                SqlDataAdapter da;
                DataTable dtMaster = db.GetTable("select * from CustCreditDebitMaster where CustCreditDebitNo=" + creditDebitNo + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dtMaster.Rows.Count > 0)
                {
                    CDType = dtMaster.Rows[0]["CDType"].ToString();
                    CustId = Convert.ToInt32(dtMaster.Rows[0]["CustomerId"].ToString());
                    customername = Convert.ToString(dtMaster.Rows[0]["CustomerName"].ToString());
                    CDNoteId = Convert.ToInt32(dtMaster.Rows[0]["Id"].ToString());
                    noteuserSeries = dtMaster.Rows[0]["BillNo"].ToString();
                    if (userSetting == true)
                    {
                        CustomerBillMasterRepository custbillrepo = new CustomerBillMasterRepository();
                        CustomerBillMaster custbillmast = custbillrepo.GetAll().Where(t => t.BillNo == noteuserSeries && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (custbillmast != null)
                            noteuserSeries = custbillmast.UserSeries;
                        custbillrepo.Dispose();
                    }
                }
                DataTable dtCustomer = db.GetTable("select * from CustomerInformation where customerId=" + CustId + " and CustomerName='" + customername + "' and CompId='" + CommonMethod.CompId + "'");
                if (dtCustomer.Rows.Count > 0)
                {
                    address = Convert.ToString(dtCustomer.Rows[0]["CustomeAddress"].ToString());
                    custMobileno = Convert.ToString(dtCustomer.Rows[0]["ContactNo"].ToString());
                    state = Convert.ToString(dtCustomer.Rows[0]["State"].ToString());
                    GSTIN = Convert.ToString(dtCustomer.Rows[0]["BuyerTinNo"].ToString());
                    if (dtCustomer.Rows[0]["Statecode"].ToString() == "" || dtCustomer.Rows[0]["Statecode"].ToString() == null)
                        statecode = "";
                    else
                        statecode = dtCustomer.Rows[0]["Statecode"].ToString();
                }
                DataTable dtDetail = db.GetTable("select * from CustCreditDebitDetail as d inner join CustCreditDebitmaster as m on m.Id=d.CustCreditDebitNo where d.CustCreditDebitNo=" + CDNoteId + " and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' ");
                if (dtDetail.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDetail.Rows.Count; i++)
                    {
                        amount = Convert.ToDecimal(dtDetail.Rows[i]["Amount"].ToString());
                        gstpercent = Convert.ToDecimal(dtDetail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(dtDetail.Rows[i]["CGstPercent"].ToString());
                        cgstAmt = (amount * gstpercent) / 100;
                        igstAmt = (amount * Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString())) / 100;

                        //for cgst and sgst
                        if (gstpercent == 5)
                        {
                            Cgst5amt = Cgst5amt + cgstAmt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 12)
                        {
                            Cgst12amt = Cgst12amt + cgstAmt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 18)
                        {
                            Cgst18amt = Cgst18amt + cgstAmt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 28)
                        {
                            Cgst28amt = Cgst28amt + cgstAmt;
                            gstpercent = 0;
                        }

                        //for igst
                        if (Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString()) == 5)
                            Igst5amt = Igst5amt + igstAmt;
                        if (Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString()) == 12)
                            Igst12amt = Igst12amt + igstAmt;
                        if (Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString()) == 18)
                            Igst18amt = Igst18amt + igstAmt;
                        if (Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString()) == 28)
                            Igst28amt = Igst28amt + igstAmt;
                        TotCgstAmt = Cgst5amt + Cgst12amt + Cgst18amt + Cgst28amt;
                        TotIgstAmt = Igst5amt + Igst12amt + Igst18amt + Igst28amt;
                    }
                }

                DSCustomerCreditDebitNote ds = new DSCustomerCreditDebitNote();
                da = new SqlDataAdapter("select * from CustCreditDebitMaster where CustCreditDebitNo=" + creditDebitNo + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustCreditDebitMaster");

                da = new SqlDataAdapter("select d.* from CustCreditDebitDetail as d inner join CustCreditDebitmaster as m on m.Id=d.CustCreditDebitNo where d.CustCreditDebitNo=" + CDNoteId + " and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus='' or d.AssemblyChildStatus is null)  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustCreditDebitDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                custRpt = new CustomerCreditDebitReport();
                custRpt.SetDataSource(ds);
                custRpt.SetParameterValue("customername", customername);
                custRpt.SetParameterValue("address", address);
                custRpt.SetParameterValue("mobileno", custMobileno);
                custRpt.SetParameterValue("GSTIN", GSTIN);
                custRpt.SetParameterValue("state", state);
                custRpt.SetParameterValue("statecode", statecode);
                custRpt.SetParameterValue("SCgst5Amt", Cgst5amt);
                custRpt.SetParameterValue("SCgst12Amt", Cgst12amt);
                custRpt.SetParameterValue("SCgst18Amt", Cgst18amt);
                custRpt.SetParameterValue("SCgst28Amt", Cgst28amt);
                custRpt.SetParameterValue("TotalSCgstAmt", TotCgstAmt);
                custRpt.SetParameterValue("Igst5Amt", Igst5amt);
                custRpt.SetParameterValue("Igst12Amt", Igst12amt);
                custRpt.SetParameterValue("Igst18Amt", Igst18amt);
                custRpt.SetParameterValue("Igst28Amt", Igst28amt);
                custRpt.SetParameterValue("TotalIgstAmt", TotIgstAmt);
                custRpt.SetParameterValue("CreditDebitType", CDType);
                custRpt.SetParameterValue("noteuserSeries", noteuserSeries);
                crystalReportViewer1.ReportSource = custRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                dtCustomer.Dispose();
                dtDetail.Dispose();
                dtMaster.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int cdnNo = Convert.ToInt32(txtCDNo.Text);
                CustCreditDebitNoteMasterRepository custCDRepo = new CustCreditDebitNoteMasterRepository();
                var custData = custCDRepo.GetAll().Where(t => t.CustCreditDebitNo == cdnNo && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custData != null)
                    getdata(cdnNo);
                else
                {
                    MessageBox.Show("This credit debit number is not present.", "Warning");
                    txtCDNo.Text = "";
                    this.ActiveControl = txtCDNo;
                }
                custCDRepo.Dispose();
            }
            catch (Exception)
            { }
        }
    }
}