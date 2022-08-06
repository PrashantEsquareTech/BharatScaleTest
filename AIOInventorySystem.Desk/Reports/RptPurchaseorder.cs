using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptPurchaseorder : Form
    {
        DbClass db = new DbClass();

        public RptPurchaseorder()
        {
            InitializeComponent();
        }

        public RptPurchaseorder(int purchaseno)
        {
            InitializeComponent();
            GetData(purchaseno);
        }

        public void GetData(int porderno)
        {
            try
            {
                PurchaseorderCrystalReport purchaserpt = new PurchaseorderCrystalReport();
                CommonMethod cm = new CommonMethod();
                CurrencyToWord CurrencytoWord = new CurrencyToWord();
                int purchaseid = 0, suppcode = 0, stateId = 0;
                decimal vatamt = 0, discountamt = 0, spcdiscountamt = 0, mvat = 0, lblNetAmt = 0, txtnetamt = 0, lblSpecNetAmt = 0, Cgst5amt = 0, Cgst12amt = 0,
                    Cgst18amt = 0, Cgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, TotCgstAmt = 0, TotIgstAmt = 0, vat1 = 0, vat2 = 0,
                    gstpercent = 0;
                string custadd = "", mobileno = "", custname = "", state = "", GSTNo = "", statecode = "", Number = "", NetAmtInWord = "";

                db.connect();
                SqlDataAdapter da;
                DataTable dt = db.GetTable("Select Id,SupplierId,isnull(TotalAmount,0) as TotalAmount,isnull(Vat,0) as Vat,isnull(NetAmount,0) as NetAmount,isnull(Discount,0) as Discount,isnull(SpcDiscount,0) as SpcDiscount from PurchaseMaster Where PorderNo='" + porderno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    purchaseid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    suppcode = Convert.ToInt32(dt.Rows[0]["SupplierId"].ToString());
                    mvat = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    vat1 = mvat;
                    decimal d = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    Number = Math.Round(Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                    decimal e = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        e = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountamt = Convert.ToDecimal((d * e) / 100);
                    txtnetamt = Convert.ToDecimal(d - discountamt);
                    lblNetAmt = Math.Round(txtnetamt, 2, MidpointRounding.AwayFromZero);
                    decimal m = 0;
                    if (dt.Rows[0]["Vat"].ToString() != "")
                        m = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    decimal pcdiscountamt = Convert.ToDecimal((lblNetAmt * m) / 100);
                    txtnetamt = Convert.ToDecimal(lblNetAmt - spcdiscountamt);
                    lblSpecNetAmt = Math.Round(txtnetamt, 2, MidpointRounding.AwayFromZero);
                    vatamt = Convert.ToDecimal(Math.Round((lblSpecNetAmt * m) / 100, 2, MidpointRounding.AwayFromZero));
                }
                DataTable dt1;
                if (CommonMethod.commProduct == true)
                    dt1 = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + suppcode + "  ");
                else
                    dt1 = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + suppcode + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    custname = dt1.Rows[0]["SupplierName"].ToString();
                    custadd = dt1.Rows[0]["SupplierAddress"].ToString();
                    mobileno = dt1.Rows[0]["ContactNo"].ToString();
                    GSTNo = Convert.ToString(dt1.Rows[0]["VatNo"].ToString());
                    if (dt1.Rows[0]["StateId"].ToString() == "" || dt1.Rows[0]["StateId"].ToString() == null)
                        stateId = 0;
                    else
                        stateId = Convert.ToInt32(dt1.Rows[0]["StateId"].ToString());
                }
                if (stateId != 0)
                {
                    DataTable dtstate = db.GetTable("select * from StateMaster where Id=" + stateId + "");
                    if (dtstate.Rows.Count > 0)
                    {
                        state = Convert.ToString(dtstate.Rows[0]["StateName"].ToString());
                        statecode = Convert.ToString(dtstate.Rows[0]["StateId"].ToString());
                    }
                }
                else
                {
                    state = "";
                    statecode = "";
                }
                decimal vat1amt = 0, vat2amt = 0;
                DataTable detail = db.GetTable("Select isnull(d.amount,0) as Amount,isnull(d.Discount,0) as Discount,isnull(d.vat,0) as vat,isnull(d.sgstpercent,0) as sgstpercent,isnull(d.cgstpercent,0) as cgstpercent,isnull(sgstamt,0) as sgstamt,isnull(cgstamt,0) as cgstamt,isnull(igstpercent,0) as igstpercent,isnull(igstamt,0) as igstamt from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo Where d.PorderNo=" + purchaseid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' order by d.Id ");
                if (detail.Rows.Count > 0)
                {
                    for (int i = 0; i < detail.Rows.Count; i++)
                    {
                        decimal vatamt1 = 0, discamt = 0;
                        decimal amount = Convert.ToDecimal(detail.Rows[i]["Amount"].ToString()); //0
                        decimal disc = Convert.ToDecimal(detail.Rows[i]["Discount"].ToString()); //1
                        discamt = amount * disc / 100;
                        if (Convert.ToDecimal(detail.Rows[i]["vat"].ToString()) == mvat) //2
                        {
                            vat1 = Convert.ToDecimal(detail.Rows[i]["vat"].ToString());
                            vatamt1 = (amount - discamt) * vat1 / 100;
                            vat1amt += vatamt1;
                        }
                        else if (Convert.ToDecimal(detail.Rows[i]["vat"].ToString()) != mvat && Convert.ToDecimal(detail.Rows[i]["vat"].ToString()) != 0) //2
                        {
                            vat2 = Convert.ToDecimal(detail.Rows[i]["vat"].ToString());
                            vatamt1 = (amount - discamt) * vat2 / 100;
                            vat2amt += vatamt1;
                        }
                        gstpercent = Convert.ToDecimal(detail.Rows[i]["sgstpercent"].ToString()) + Convert.ToDecimal(detail.Rows[i]["cgstpercent"].ToString()); //3,4
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

                        //Igst
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
                da = new SqlDataAdapter("Select * from PurchaseMaster Where PorderNo='" + porderno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                PurchaseorderDS ds = new PurchaseorderDS();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseMaster");

                da = new SqlDataAdapter("Select d.* from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo Where d.PorderNo=" + purchaseid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus is null) order by d.Id", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation order by Productname", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " order by Productname", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                purchaserpt = new PurchaseorderCrystalReport();
                purchaserpt.SetDataSource(ds);

                purchaserpt.SetParameterValue("Suppliername", custname);
                purchaserpt.SetParameterValue("Supplieraddress", custadd);
                purchaserpt.SetParameterValue("mobileno", mobileno);
                purchaserpt.SetParameterValue("State", state);
                purchaserpt.SetParameterValue("Statecode", statecode);
                purchaserpt.SetParameterValue("vatamount", vatamt);
                purchaserpt.SetParameterValue("discount", discountamt);
                purchaserpt.SetParameterValue("spcdiscount", spcdiscountamt);
                purchaserpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                purchaserpt.SetParameterValue("GSTNo", GSTNo);
                purchaserpt.SetParameterValue("vat1amt", Convert.ToString(Math.Round(vat1amt, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat2amt", Convert.ToString(Math.Round(vat2amt, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat1", Convert.ToDecimal(Math.Round(vat1, 2, MidpointRounding.AwayFromZero)));
                purchaserpt.SetParameterValue("vat2", Convert.ToDecimal(Math.Round(vat2, 2, MidpointRounding.AwayFromZero)));
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
                crystalReportViewer1.ParameterFieldInfo.Clear();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
                dt.Dispose();
                dt1.Dispose();
                detail.Dispose();
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
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var custdata = pmasterrepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtorderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                    GetData(Convert.ToInt32(txtorderno.Text));
                else
                {
                    MessageBox.Show("This Purchase Order Number is not Present.", "Warning");
                    txtorderno.Text = "";
                    this.ActiveControl = txtorderno;
                }
                pmasterrepo.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
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