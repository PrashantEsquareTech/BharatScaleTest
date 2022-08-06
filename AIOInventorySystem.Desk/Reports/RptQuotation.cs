using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptQuotation : Form
    {
        DbClass db = new DbClass();
        Quotation_Portrait quotationrpt1 = new Quotation_Portrait();
        CurrencyToWord CurrencytoWord = new CurrencyToWord();
        CompositionQuotationCrpt crpt = new CompositionQuotationCrpt();

        public RptQuotation()
        {
            InitializeComponent();
        }

        public RptQuotation(int QId)
        {
            InitializeComponent();
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.CompositionScheme == "False")
                GetData(QId);
            else
                CompostionData(QId);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    this.Close();
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        public void GetData(int QId)
        {
            try
            {
                int custid = 0, custvalue = 0;
                string taxtype = "", custadd = "", mobileno = "", landline = "", custname = "", tinno = "", quotationCustomerName = "", panno = "", custstatecode = "",
                    custstate = "", Number = "", NetAmtInWord = "", termcondition = "", declaration = "";
                decimal vatamt = 0, discountamt = 0, txtnetamt = 0, gstpercent = 0, Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0,
                    Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, TotCgstAmt = 0, TotIgstAmt = 0, vat1 = 0, vat2 = 0;

                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from QuotationMaster Where QuotationNo='" + QId + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DSQuotation ds = new DSQuotation();
                ds.Clear();
                da.Fill(ds, "QuotationMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    vat1 = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    custid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    quotationCustomerName = dt.Rows[0]["CustomerName"].ToString();
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    Number = Math.Round(Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                    decimal g = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        g = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountamt = Convert.ToDecimal((a * g) / 100);
                    txtnetamt = Convert.ToDecimal(a - discountamt);
                    decimal b = 0;
                    if (dt.Rows[0]["Vat"].ToString() != "")
                        b = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    vatamt = Convert.ToDecimal((txtnetamt * b) / 100);
                }
                if (custvalue != 0)
                {
                    DataTable dt1 = db.GetTable("Select * from CustomerInformation Where CustomerID=" + custvalue + " and CompId=" + CommonMethod.CompId + "");
                    if (dt1.Rows.Count > 0)
                    {
                        custname = dt1.Rows[0]["CustomerName"].ToString();
                        custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                        mobileno = dt1.Rows[0]["Mobileno"].ToString();
                        landline = dt1.Rows[0]["Landlineno"].ToString();
                        tinno = dt1.Rows[0]["BuyerTinNo"].ToString();
                        panno = dt1.Rows[0]["Panno"].ToString();
                        custstate = dt1.Rows[0]["State"].ToString();
                        custstatecode = dt1.Rows[0]["Statecode"].ToString();
                    }
                    dt1.Dispose();
                }
                else
                {
                    custname = quotationCustomerName;
                    custadd = "";
                    mobileno = "";
                    landline = "";
                    tinno = "";
                    panno = "";
                    custstate = "Maharashtra";
                    custstatecode = "27";
                }
                da = new SqlDataAdapter("Select d.* from QuotationDetail as d inner join QuotationMaster as m on m.Id=d.QuotationNo Where d.QuotationNo=" + custid + " and m.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "QuotationDetail");
                DataTable detail = new DataTable();
                da.Fill(detail);
                decimal vat1amt = 0, vat2amt = 0;
                if (detail.Rows.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < detail.Rows.Count; i++)
                        {
                            ProductRepository ProductRepo = new ProductRepository();
                            if (CommonMethod.commProduct == true)
                                taxtype = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString()).FirstOrDefault().TaxInclusive;
                            else
                                taxtype = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault().TaxInclusive;

                            decimal vatamt1 = 0, discamt = 0;
                            decimal amount = Convert.ToDecimal(detail.Rows[i]["Amount"].ToString());
                            decimal disc = Convert.ToDecimal(detail.Rows[i]["Discount"].ToString());
                            discamt = amount * disc / 100;
                            if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) == vat1)
                            {
                                vat1 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                                if (taxtype == "False" || taxtype == null)
                                    vatamt1 = (amount - discamt) * vat1 / 100;
                                else
                                    vatamt1 = (amount) * vat1 / 100;
                                vat1amt += vatamt1;
                            }
                            else if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != vat1 && Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != 0)
                            {
                                vat2 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                                if (taxtype == "False" || taxtype == null)
                                    vatamt1 = (amount - discamt) * vat2 / 100;
                                else
                                    vatamt1 = (amount) * vat2 / 100;
                                vat2amt += vatamt1;
                            }
                            gstpercent = Convert.ToDecimal(detail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(detail.Rows[i]["CGstPercent"].ToString());
                            if (gstpercent == 5)
                            {
                                Cgst5amt = Cgst5amt + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            if (gstpercent == 12)
                            {
                                Cgst12amt = Cgst12amt + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            if (gstpercent == 18)
                            {
                                Cgst18amt = Cgst18amt + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString());
                                gstpercent = 0;
                            }
                            if (gstpercent == 28)
                            {
                                Cgst28amt = Cgst28amt + Convert.ToDecimal(detail.Rows[i]["SGstAmt"].ToString()) + Convert.ToDecimal(detail.Rows[i]["CGstAmt"].ToString());
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
                        }
                    }
                    catch (Exception)
                    { }
                }
                DataTable dtBillInstru = db.GetTable("select * from BillInstruction where CompId=" + CommonMethod.CompId + "");
                if (dtBillInstru.Rows.Count > 0)
                {
                    termcondition = dtBillInstru.Rows[0]["TermsConditions"].ToString();
                    declaration = dtBillInstru.Rows[0]["Declaration"].ToString();
                }
                else
                {
                    termcondition = "";
                    declaration = "";
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

                da = new SqlDataAdapter("Select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                quotationrpt1 = new Quotation_Portrait();
                quotationrpt1.SetDataSource(ds);
                quotationrpt1.SetParameterValue("custname", custname);
                quotationrpt1.SetParameterValue("customeraddress", custadd);
                quotationrpt1.SetParameterValue("mobileno", mobileno);
                quotationrpt1.SetParameterValue("discamt", discountamt);
                quotationrpt1.SetParameterValue("buyertinno", tinno);
                quotationrpt1.SetParameterValue("NetAmtInWord", NetAmtInWord);
                quotationrpt1.SetParameterValue("custPanno", panno);
                quotationrpt1.SetParameterValue("Cgst5amt", Cgst5amt);
                quotationrpt1.SetParameterValue("Cgst12amt", Cgst12amt);
                quotationrpt1.SetParameterValue("Cgst18amt", Cgst18amt);
                quotationrpt1.SetParameterValue("Cgst28amt", Cgst28amt);
                quotationrpt1.SetParameterValue("Igst5amt", Igst5amt);
                quotationrpt1.SetParameterValue("Igst12amt", Igst12amt);
                quotationrpt1.SetParameterValue("Igst18amt", Igst18amt);
                quotationrpt1.SetParameterValue("Igst28amt", Igst28amt);
                quotationrpt1.SetParameterValue("TotCgstAmt", TotCgstAmt);
                quotationrpt1.SetParameterValue("TotIgstAmt", TotIgstAmt);
                quotationrpt1.SetParameterValue("termcondition", termcondition);
                quotationrpt1.SetParameterValue("declaration", declaration);
                quotationrpt1.SetParameterValue("custstate", custstate);
                quotationrpt1.SetParameterValue("custstatecode", custstatecode);
                crystalReportViewer1.ReportSource = quotationrpt1;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
                detail.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                QuotationMasterRepository Qmasterrepo = new QuotationMasterRepository();
                var custdata = Qmasterrepo.GetAll().Where(t => t.QuotationNo == Convert.ToInt32(txtbillno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                    GetData(Convert.ToInt32(txtbillno.Text));
                else
                {
                    MessageBox.Show("This Quotation Is Not Present.", "Warning");
                    txtbillno.Text = "";
                    this.ActiveControl = txtbillno;
                }
                Qmasterrepo.Dispose();
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

        public void CompostionData(int QId)
        {
            try
            {
                int custid = 0, custvalue = 0;
                string taxtype = "", custadd = "", mobileno = "", landline = "", custname = "", tinno = "", quotationCustomerName = "", panno = "", custstatecode = "",
                    custstate = "", Number = "", NetAmtInWord = "", termcondition = "", declaration = "";
                decimal vatamt = 0, discountamt = 0, txtnetamt = 0, vat1 = 0, vat2 = 0;
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from QuotationMaster Where QuotationNo='" + QId + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DSQuotation ds = new DSQuotation();
                ds.Clear();
                da.Fill(ds, "QuotationMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    vat1 = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    custid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["CustomerId"].ToString());
                    quotationCustomerName = dt.Rows[0]["CustomerName"].ToString();
                    decimal a = Convert.ToDecimal(dt.Rows[0]["TotalAmount"].ToString());
                    Number = Math.Round(Convert.ToDecimal(dt.Rows[0]["NetAmount"].ToString()), 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                    decimal g = 0;
                    if (dt.Rows[0]["Discount"].ToString() != "")
                        g = Convert.ToDecimal(dt.Rows[0]["Discount"].ToString());
                    discountamt = Convert.ToDecimal((a * g) / 100);
                    txtnetamt = Convert.ToDecimal(a - discountamt);
                    decimal b = 0;
                    if (dt.Rows[0]["Vat"].ToString() != "")
                        b = Convert.ToDecimal(dt.Rows[0]["Vat"].ToString());
                    vatamt = Convert.ToDecimal((txtnetamt * b) / 100);
                }
                if (custvalue != 0)
                {
                    DataTable dt1 = db.GetTable("Select * from CustomerInformation Where CustomerID=" + custvalue + " and CompId=" + CommonMethod.CompId + "");
                    if (dt1.Rows.Count > 0)
                    {
                        custname = dt1.Rows[0]["CustomerName"].ToString();
                        custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                        mobileno = dt1.Rows[0]["Mobileno"].ToString();
                        landline = dt1.Rows[0]["Landlineno"].ToString();
                        tinno = dt1.Rows[0]["BuyerTinNo"].ToString();
                        panno = dt1.Rows[0]["Panno"].ToString();
                        custstate = dt1.Rows[0]["State"].ToString();
                        custstatecode = dt1.Rows[0]["Statecode"].ToString();
                    }
                    dt1.Dispose();
                }
                else
                {
                    custname = quotationCustomerName;
                    custadd = "";
                    mobileno = "";
                    landline = "";
                    tinno = "";
                    panno = "";
                    custstate = "Maharashtra";
                    custstatecode = "27";
                }
                da = new SqlDataAdapter("Select d.* from QuotationDetail as d inner join QuotationMaster as m on m.Id=d.QuotationNo Where d.QuotationNo=" + custid + " and m.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "QuotationDetail");
                DataTable detail = new DataTable();
                da.Fill(detail);
                decimal vat1amt = 0, vat2amt = 0;
                if (detail.Rows.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < detail.Rows.Count; i++)
                        {
                            ProductRepository ProductRepo = new ProductRepository();
                            if (CommonMethod.commProduct == true)
                                taxtype = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString()).FirstOrDefault().TaxInclusive;
                            else
                                taxtype = ProductRepo.GetAll().Where(t => t.ProductName == detail.Rows[i]["ProductId"].ToString() && t.ManufactureCompany == detail.Rows[i]["CompanyName"].ToString() && t.Uniti == detail.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault().TaxInclusive;

                            decimal vatamt1 = 0, discamt = 0;
                            decimal amount = Convert.ToDecimal(detail.Rows[i]["Amount"].ToString());
                            decimal disc = Convert.ToDecimal(detail.Rows[i]["Discount"].ToString());
                            discamt = amount * disc / 100;
                            if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) == vat1)
                            {
                                vat1 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                                if (taxtype == "False" || taxtype == null)
                                    vatamt1 = (amount - discamt) * vat1 / 100;
                                else
                                    vatamt1 = (amount) * vat1 / 100;
                                vat1amt += vatamt1;
                            }
                            else if (Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != vat1 && Convert.ToDecimal(detail.Rows[i]["Vat"].ToString()) != 0)
                            {
                                vat2 = Convert.ToDecimal(detail.Rows[i]["Vat"].ToString());
                                if (taxtype == "False" || taxtype == null)
                                    vatamt1 = (amount - discamt) * vat2 / 100;
                                else
                                    vatamt1 = (amount) * vat2 / 100;
                                vat2amt += vatamt1;
                            }
                        }
                    }
                    catch (Exception) { }
                }
                DataTable dtBillInstru = db.GetTable("select * from BillInstruction where CompId=" + CommonMethod.CompId + "");
                if (dtBillInstru.Rows.Count > 0)
                {
                    termcondition = dtBillInstru.Rows[0]["TermsConditions"].ToString();
                    declaration = dtBillInstru.Rows[0]["Declaration"].ToString();
                }
                else
                {
                    termcondition = "";
                    declaration = "";
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

                da = new SqlDataAdapter("Select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                crpt = new CompositionQuotationCrpt();
                crpt.SetDataSource(ds);
                crpt.SetParameterValue("custname", custname);
                crpt.SetParameterValue("customeraddress", custadd);
                crpt.SetParameterValue("mobileno", mobileno);
                crpt.SetParameterValue("buyertinno", tinno);
                crpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                crpt.SetParameterValue("discamt", discountamt);
                crpt.SetParameterValue("termcondition", termcondition);
                crpt.SetParameterValue("declaration", declaration);
                crpt.SetParameterValue("custstate", custstate);
                crpt.SetParameterValue("custstatecode", custstatecode);
                crpt.SetParameterValue("custPanno", panno);
                crystalReportViewer1.ReportSource = crpt;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
                detail.Dispose();
            }
            catch (Exception)
            { }
        }
    }
}