using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptPurchaseReturn : Form
    {
        DbClass db = new DbClass();
        PurchaseReturn1CrystalReport purchaserpt = new PurchaseReturn1CrystalReport();

        public RptPurchaseReturn()
        {
            InitializeComponent();
        }

        public RptPurchaseReturn(int PrNo)
        {
            InitializeComponent();
            GetData(PrNo);
        }

        public void GetData(int preturnno)
        {
            try
            {
                CurrencyToWord CurrencytoWord = new CurrencyToWord();
                decimal Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, TotCgstAmt = 0,
                    TotIgstAmt = 0, cgstamt = 0, igstamt = 0, amt = 0, mdvat = 0, vat1 = 0, vat2 = 0, gstpercent = 0, igstpercent = 0;
                int preturnid = 0, POrderNo = 0, POID = 0;
                string Number = "", NetAmtInWord = "";

                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from PurchaseReturnMaster Where PurchaseReturnNo='" + preturnno + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSPurchaseReturn ds = new DSPurchaseReturn();
                da.Fill(ds, "PurchaseReturnMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    preturnid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    POrderNo = Convert.ToInt32(dt.Rows[0]["PorderNo"].ToString());
                    Number = Math.Round(Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                }

                DataTable master = db.GetTable("Select * from PurchaseMaster Where PorderNo='" + POrderNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (master.Rows.Count > 0)
                {
                    POID = Convert.ToInt32(master.Rows[0]["Id"].ToString());
                    vat1 = Convert.ToDecimal(master.Rows[0]["Vat"].ToString());
                }
                if (vat1 != 0)
                {
                    DataTable detail = db.GetTable("Select d.* from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo Where d.PorderNo='" + POID + "' and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    if (detail.Rows.Count > 0)
                    {
                        for (int i = 0; i < detail.Rows.Count; i++)
                        {
                            mdvat = mdvat + Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                        }
                    }
                    detail.Dispose();
                }

                da = new SqlDataAdapter("Select d.* from PurchaseReturnDetail d inner join PurchaseReturnMaster m on m.Id=d.PurchaseReturnNo Where d.PurchaseReturnNo=" + preturnid + " and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseReturnDetail");
                DataTable PRdetail = new DataTable();
                da.Fill(PRdetail);

                decimal vat1amt = 0, vat2amt = 0;
                if (PRdetail.Rows.Count > 0)
                {
                    for (int i = 0; i < PRdetail.Rows.Count; i++)
                    {
                        decimal discamt = 0, threeDisc, amount = Convert.ToDecimal(PRdetail.Rows[i]["Amount"].ToString()), disc = Convert.ToDecimal(PRdetail.Rows[i]["Discount"].ToString()), spdisc = Convert.ToDecimal(PRdetail.Rows[i]["SpcDisc"].ToString());
                        if (PRdetail.Rows[i]["ThreeLevelDiscount"].ToString() == null)
                            threeDisc = 0;
                        else
                            threeDisc = Convert.ToDecimal(PRdetail.Rows[i]["ThreeLevelDiscount"].ToString());
                        discamt = amount * disc / 100;
                        amt = Convert.ToDecimal(PRdetail.Rows[i]["Quantity"].ToString()) * Convert.ToDecimal(PRdetail.Rows[i]["PurchasePrice"].ToString());
                        amt = amt - (amt * disc) / 100;
                        amt = amt - (amt * spdisc) / 100;
                        amt = amt - (amt * threeDisc) / 100;
                        gstpercent = Convert.ToDecimal(PRdetail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(PRdetail.Rows[i]["CGstPercent"].ToString());
                        cgstamt = amt * gstpercent / 100;
                        if (gstpercent == 5)
                        {
                            Cgst5amt = Cgst5amt + cgstamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 12)
                        {
                            Cgst12amt = Cgst12amt + cgstamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 18)
                        {
                            Cgst18amt = Cgst18amt + cgstamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 28)
                        {
                            Cgst28amt = Cgst28amt + cgstamt;
                            gstpercent = 0;
                        }

                        //igst
                        igstpercent = Convert.ToDecimal(PRdetail.Rows[i]["IGstPercent"].ToString());
                        igstamt = amt * igstpercent / 100;
                        if (Convert.ToDecimal(PRdetail.Rows[i]["IGstPercent"].ToString()) == 5)
                            Igst5amt = Igst5amt + igstamt;
                        if (Convert.ToDecimal(PRdetail.Rows[i]["IGstPercent"].ToString()) == 12)
                            Igst12amt = Igst12amt + igstamt;
                        if (Convert.ToDecimal(PRdetail.Rows[i]["IGstPercent"].ToString()) == 18)
                            Igst18amt = Igst18amt + igstamt;
                        if (Convert.ToDecimal(PRdetail.Rows[i]["IGstPercent"].ToString()) == 28)
                            Igst28amt = Igst28amt + igstamt;
                        TotCgstAmt = Cgst5amt + Cgst12amt + Cgst18amt + Cgst28amt;
                        TotIgstAmt = Igst5amt + Igst12amt + Igst18amt + Igst28amt;
                    }
                }

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                purchaserpt = new PurchaseReturn1CrystalReport();
                purchaserpt.SetDataSource(ds);

                purchaserpt.SetParameterValue("mdvat", Convert.ToString(Math.Round(mdvat, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                purchaserpt.SetParameterValue("SCgst5Amt", Cgst5amt);
                purchaserpt.SetParameterValue("SCgst12Amt", Cgst12amt);
                purchaserpt.SetParameterValue("SCgst18Amt", Cgst18amt);
                purchaserpt.SetParameterValue("SCgst28Amt", Cgst28amt);
                purchaserpt.SetParameterValue("TotalSCgstAmt", TotCgstAmt);
                purchaserpt.SetParameterValue("Igst5Amt", Igst5amt);
                purchaserpt.SetParameterValue("Igst12Amt", Igst12amt);
                purchaserpt.SetParameterValue("Igst18Amt", Igst18amt);
                purchaserpt.SetParameterValue("Igst28Amt", Igst28amt);
                purchaserpt.SetParameterValue("TotalIgstAmt", TotIgstAmt);

                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
                master.Dispose();
                PRdetail.Dispose();
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
    }
}