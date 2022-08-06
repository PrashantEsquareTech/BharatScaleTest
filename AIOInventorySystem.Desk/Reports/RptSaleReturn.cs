using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptSaleReturn : Form
    {
        DbClass db = new DbClass();
        SaleReturnCrystalReport salerrpt = new SaleReturnCrystalReport();
        CurrencyToWord CurrencytoWord = new CurrencyToWord();
        public bool userseriesSettings = false;

        public RptSaleReturn()
        {
            InitializeComponent();
        }

        public RptSaleReturn(int srno)
        {
            InitializeComponent();
            GetData(srno);
        }

        public void GetData(int Sreturnno)
        {
            try
            {
                int Sreturnid = 0, BillId = 0, custId = 0;
                string BillNo = "", Number = "", NetAmtInWord = "", customername = "", address = "", mobileno = "", billUserSeries = "";
                decimal Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, TotCgstAmt = 0,
                    TotIgstAmt = 0, igstamt = 0, sgstamt = 0, amt = 0, mvat = 0, mdvat = 0, gstpercent = 0;

                getSettingDetails();

                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from GoodReturnMaster gm Where gm.GoodReturnNo='" + Sreturnno + "' and gm.TransactionYear = '" + CommonMethod.TransactionYear + "' and gm.CompId=" + CommonMethod.CompId + "", db.Connection);
                DSSaleReturn ds = new DSSaleReturn();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "GoodReturnMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    customername = dt.Rows[0]["CustomerName"].ToString();
                    custId = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    Sreturnid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    BillNo = Convert.ToString(dt.Rows[0]["BillNo"].ToString());
                    billUserSeries = BillNo;
                    if (userseriesSettings == true)
                    {
                        CustomerBillMasterRepository custbillrepo = new CustomerBillMasterRepository();
                        CustomerBillMaster custbillmast = custbillrepo.GetAll().Where(t => t.BillNo == BillNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (custbillmast != null)
                            billUserSeries = custbillmast.UserSeries;
                        custbillrepo.Dispose();
                    }

                    Number = Math.Round(Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                }
                DataTable dtCustomer = db.GetTable("select * from CustomerInformation where customerId=" + custId + " and CustomerName='" + customername + "'");
                if (dtCustomer.Rows.Count > 0)
                {
                    address = Convert.ToString(dtCustomer.Rows[0]["CustomeAddress"].ToString());
                    mobileno = Convert.ToString(dtCustomer.Rows[0]["ContactNo"].ToString());
                }

                DataTable master = db.GetTable("Select * from CustomerBillMaster Where BillNo='" + BillNo + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "");
                if (master.Rows.Count > 0)
                {
                    BillId = Convert.ToInt32(master.Rows[0]["Id"].ToString());
                    mvat = Convert.ToDecimal(master.Rows[0]["Vat"].ToString());
                }
                if (mvat != 0)
                {
                    DataTable detail = db.GetTable("Select * from CustomerBillDetail Where BillNo='" + BillId + "'");
                    if (detail.Rows.Count > 0)
                    {
                        for (int i = 0; i < detail.Rows.Count; i++)
                        {
                            mdvat = mdvat + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString());
                        }
                    }
                    detail.Dispose();
                }
                da = new SqlDataAdapter("Select * from GoodReturnDetail d inner join GoodReturnMaster as m on m.Id=d.GoodReturnNo Where d.GoodReturnNo=" + Sreturnid + " and (d.AssemblyChildStatus!='Y' or d.AssemblyChildStatus is null or  d.AssemblyChildStatus='')", db.Connection);
                da.Fill(ds, "GoodReturnDetail");
                DataTable SRdetail = new DataTable();
                da.Fill(SRdetail);
                if (SRdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < SRdetail.Rows.Count; i++)
                    {
                        ProductRepository ProductRepo = new ProductRepository();
                        string taxtype = ProductRepo.GetAll().Where(t => t.ProductName == SRdetail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == SRdetail.Rows[i]["CompanyName"].ToString() && t.Uniti == SRdetail.Rows[i]["Unit"].ToString()).FirstOrDefault().TaxInclusive;
                        decimal discamt = 0, amount = Convert.ToDecimal(SRdetail.Rows[i]["Amount"].ToString()), disc = Convert.ToDecimal(SRdetail.Rows[i]["Discount"].ToString()), spdisc = Convert.ToDecimal(SRdetail.Rows[i]["SpcDisc"].ToString());
                        discamt = amount * disc / 100;
                        amt = Convert.ToDecimal(SRdetail.Rows[i]["Quantity"].ToString()) * Convert.ToDecimal(SRdetail.Rows[i]["Rate"].ToString());
                        amt = amt - (amt * disc) / 100;
                        amt = amt - (amt * spdisc) / 100;
                        gstpercent = Convert.ToDecimal(SRdetail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(SRdetail.Rows[i]["CGstPercent"].ToString());
                        sgstamt = amt * gstpercent / 100;
                        igstamt = amt * Convert.ToDecimal(SRdetail.Rows[i]["IGstPercent"].ToString()) / 100;
                        if (gstpercent == 5)
                        {
                            Cgst5amt = Cgst5amt + sgstamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 12)
                        {
                            Cgst12amt = Cgst12amt + sgstamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 18)
                        {
                            Cgst18amt = Cgst18amt + sgstamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 28)
                        {
                            Cgst28amt = Cgst28amt + sgstamt;
                            gstpercent = 0;
                        }

                        //igst amt
                        if (Convert.ToDecimal(SRdetail.Rows[i]["IGstPercent"].ToString()) == 5)
                            Igst5amt = Igst5amt + igstamt;
                        if (Convert.ToDecimal(SRdetail.Rows[i]["IGstPercent"].ToString()) == 12)
                            Igst12amt = Igst12amt + igstamt;
                        if (Convert.ToDecimal(SRdetail.Rows[i]["IGstPercent"].ToString()) == 18)
                            Igst18amt = Igst18amt + igstamt;
                        if (Convert.ToDecimal(SRdetail.Rows[i]["IGstPercent"].ToString()) == 28)
                            Igst28amt = Igst28amt + igstamt;
                        TotCgstAmt = Cgst5amt + Cgst12amt + Cgst18amt + Cgst28amt;
                        TotIgstAmt = Igst5amt + Igst12amt + Igst18amt + Igst28amt;
                    }
                }

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");
                salerrpt = new SaleReturnCrystalReport();
                salerrpt.SetDataSource(ds);
                salerrpt.SetParameterValue("mdvat", Convert.ToString(Math.Round(mdvat, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat1amt", Convert.ToString(Math.Round(0.0, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat2amt", Convert.ToString(Math.Round(0.0, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(mvat, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(0.0, 2, MidpointRounding.AwayFromZero)));
                salerrpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                salerrpt.SetParameterValue("customername", customername);
                salerrpt.SetParameterValue("Address", address);
                salerrpt.SetParameterValue("MobileNo", mobileno);
                salerrpt.SetParameterValue("SCgst5Amt", Cgst5amt);
                salerrpt.SetParameterValue("SCgst12Amt", Cgst12amt);
                salerrpt.SetParameterValue("SCgst18Amt", Cgst18amt);
                salerrpt.SetParameterValue("SCgst28Amt", Cgst28amt);
                salerrpt.SetParameterValue("TotalSCgstAmt", TotCgstAmt);
                salerrpt.SetParameterValue("Igst5Amt", Igst5amt);
                salerrpt.SetParameterValue("Igst12Amt", Igst12amt);
                salerrpt.SetParameterValue("Igst18Amt", Igst18amt);
                salerrpt.SetParameterValue("Igst28Amt", Igst28amt);
                salerrpt.SetParameterValue("TotalIgstAmt", TotIgstAmt);
                salerrpt.SetParameterValue("billUserSeries", billUserSeries);

                crystalReportViewer1.ReportSource = salerrpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                dt.Dispose();
                master.Dispose();
                SRdetail.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                GetData(Convert.ToInt32(txtorderno.Text));
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

        private void getSettingDetails()
        {
            SettingRepository setrepo = new SettingRepository();
            Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setData != null)
            {
                if (setData.ShowUserDefinedSeries == "Y")
                    userseriesSettings = true;
                else
                    userseriesSettings = false;
            }
            setrepo.Dispose();
        }
    }
}