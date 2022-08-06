using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptSupplierCreditNote : Form
    {
        DbClass db = new DbClass();
        SupplierCreditNoteReport suppCDNRpt = new SupplierCreditNoteReport();
        

        public RptSupplierCreditNote()
        {
            InitializeComponent();
        }

        public RptSupplierCreditNote(int SuppCDNO)
        {
            InitializeComponent();
            getData(SuppCDNO);
        }

        public void getData(int creditDebitNo)
        {
            try
            {
                decimal Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0, Cgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, TotCgstAmt = 0,
                    TotIgstAmt = 0, amount = 0, sgstAmt = 0, igstAmt = 0, discper = 0, disamt = 0, spcdicper = 0, spcdiscamt = 0, othdisc = 0, othdiscamt = 0,
                    gstpercent = 0;
                string CDType = "", suppliername = "", address = "", suppMobileno = "", GSTIN = "", state = "", statecode = "";
                int CDNoteId = 0, stateId = 0, suppId = 0;

                db.connect();
                SqlDataAdapter da;
                DataTable dtState;
                DSSuppCreditNote ds = new DSSuppCreditNote();
                da = new SqlDataAdapter("Select * from SuppCreditDebitMaster where SuppCreditDebitNo=" + creditDebitNo + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "SuppCreditDebitMaster");
                DataTable dtMaster = new DataTable();
                da.Fill(dtMaster);
                if (dtMaster.Rows.Count > 0)
                {
                    CDType = Convert.ToString(dtMaster.Rows[0]["CDType"].ToString());
                    CDNoteId = Convert.ToInt32(dtMaster.Rows[0]["Id"].ToString());
                    suppId = Convert.ToInt32(dtMaster.Rows[0]["SupplierId"].ToString());
                    suppliername = Convert.ToString(dtMaster.Rows[0]["Suppliername"].ToString());
                }
                DataTable dtSupplier;
                if (CommonMethod.commProduct == true)
                    dtSupplier = db.GetTable("select * from SupplierInformation where SupplierCode=" + suppId + " and SupplierName='" + suppliername + "'");
                else
                    dtSupplier = db.GetTable("select * from SupplierInformation where SupplierCode=" + suppId + " and SupplierName='" + suppliername + "' and CompId='" + CommonMethod.CompId + "'");
                if (dtSupplier.Rows.Count > 0)
                {
                    if (dtSupplier.Rows[0]["SupplierAddress"].ToString() == "" || dtSupplier.Rows[0]["SupplierAddress"].ToString() == null)
                        address = "";
                    else
                        address = Convert.ToString(dtSupplier.Rows[0]["SupplierAddress"].ToString());
                    if (dtSupplier.Rows[0]["ContactNo"].ToString() == "" || dtSupplier.Rows[0]["ContactNo"].ToString() == null)
                        suppMobileno = "";
                    else
                        suppMobileno = Convert.ToString(dtSupplier.Rows[0]["ContactNo"].ToString());
                    if (dtSupplier.Rows[0]["VatNo"].ToString() == "" || dtSupplier.Rows[0]["VatNo"].ToString() == null)
                        GSTIN = "";
                    else
                        GSTIN = Convert.ToString(dtSupplier.Rows[0]["VatNo"].ToString());
                    if (dtSupplier.Rows[0]["StateId"].ToString() == "" || dtSupplier.Rows[0]["StateId"].ToString() == null)
                        stateId = 0;
                    else
                        stateId = Convert.ToInt32(dtSupplier.Rows[0]["StateId"].ToString());
                }
                if (stateId != 0)
                {
                    dtState = db.GetTable("select * from StateMaster where Id='" + stateId + "'");
                    if (dtState.Rows.Count > 0)
                    {
                        statecode = (dtState.Rows[0]["StateId"].ToString());
                        state = Convert.ToString(dtState.Rows[0]["StateName"].ToString());
                    }
                }
                else
                {
                    state = "";
                    statecode = "";
                }
                if (CDType == "Credit Note")
                {
                    DataTable dtDetail = db.GetTable("select * from SuppCreditDebitDetail as d inner join SuppCreditDebitMaster as m on m.Id=d.SuppCreditDebitNo where d.SuppCreditDebitNo=" + CDNoteId + " and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    if (dtDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            amount = Convert.ToDecimal(dtDetail.Rows[i]["Amount"].ToString());
                            if (dtDetail.Rows[i]["Discount"].ToString() == null)
                            {
                                discper = 0;
                                disamt = discper * amount / 100;
                            }
                            else
                            {
                                discper = Convert.ToDecimal(dtDetail.Rows[i]["Discount"].ToString());
                                disamt = discper * amount / 100;
                            }
                            if (dtDetail.Rows[i]["SpcDiscount"].ToString() == null)
                            {
                                spcdicper = 0;
                                spcdiscamt = (amount - disamt) * spcdicper / 100;
                            }
                            else
                            {
                                spcdicper = Convert.ToDecimal(dtDetail.Rows[i]["SpcDiscount"].ToString());
                                spcdiscamt = (amount - disamt) * spcdicper / 100;
                            }
                            if (dtDetail.Rows[i]["ThreeLevelDicount"].ToString() == null)
                            {
                                othdisc = 0;
                                othdisc = (amount - disamt - spcdiscamt) * spcdicper / 100;
                            }
                            else
                            {
                                othdisc = Convert.ToDecimal(dtDetail.Rows[i]["ThreeLevelDicount"].ToString());
                                othdiscamt = (amount - disamt - spcdiscamt) * othdisc / 100;
                            }
                            amount = amount - disamt - spcdiscamt - othdiscamt;
                            gstpercent = Convert.ToDecimal(dtDetail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(dtDetail.Rows[i]["CGstPercent"].ToString());
                            sgstAmt = amount * gstpercent / 100;
                            igstAmt = amount * Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString()) / 100;
                            // for cgst and sgst
                            if (gstpercent == 5)
                            {
                                Cgst5amt = Cgst5amt + sgstAmt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 12)
                            {
                                Cgst12amt = Cgst12amt + sgstAmt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 18)
                            {
                                Cgst18amt = Cgst18amt + sgstAmt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 28)
                            {
                                Cgst28amt = Cgst28amt + sgstAmt;
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
                }
                else
                {
                    DataTable dtDetail = db.GetTable("select * from SuppCreditDebitDetail as d inner join SuppCreditDebitMaster as m on m.Id=d.SuppCreditDebitNo where d.SuppCreditDebitNo=" + CDNoteId + " and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    if (dtDetail.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtDetail.Rows.Count; i++)
                        {
                            // for cgst and sgst
                            amount = Convert.ToDecimal(dtDetail.Rows[i]["Amount"].ToString());
                            if (dtDetail.Rows[i]["Discount"].ToString() == null)
                            {
                                discper = 0;
                                disamt = discper * amount / 100;
                            }
                            else
                            {
                                discper = Convert.ToDecimal(dtDetail.Rows[i]["Discount"].ToString());
                                disamt = discper * amount / 100;
                            }
                            if (dtDetail.Rows[i]["SpcDiscount"].ToString() == null)
                            {
                                spcdicper = 0;
                                spcdiscamt = (amount - disamt) * spcdicper / 100;
                            }
                            else
                            {
                                spcdicper = Convert.ToDecimal(dtDetail.Rows[i]["SpcDiscount"].ToString());
                                spcdiscamt = (amount - disamt) * spcdicper / 100;
                            }
                            if (dtDetail.Rows[i]["ThreeLevelDicount"].ToString() == null)
                            {
                                othdisc = 0;
                                othdisc = (amount - disamt - spcdiscamt) * spcdicper / 100;
                            }
                            else
                            {
                                othdisc = Convert.ToDecimal(dtDetail.Rows[i]["ThreeLevelDicount"].ToString());
                                othdiscamt = (amount - disamt - spcdiscamt) * othdisc / 100;
                            }
                            amount = amount - disamt - spcdiscamt - othdiscamt;
                            gstpercent = Convert.ToDecimal(dtDetail.Rows[i]["SGstPercent"].ToString()) + Convert.ToDecimal(dtDetail.Rows[i]["CGstPercent"].ToString());
                            sgstAmt = amount * gstpercent / 100;
                            igstAmt = amount * Convert.ToDecimal(dtDetail.Rows[i]["IGstPercent"].ToString()) / 100;
                            // for cgst and sgst
                            if (gstpercent == 5)
                            {
                                Cgst5amt = Cgst5amt + sgstAmt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 12)
                            {
                                Cgst12amt = Cgst12amt + sgstAmt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 18)
                            {
                                Cgst18amt = Cgst18amt + sgstAmt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 28)
                            {
                                Cgst28amt = Cgst28amt + sgstAmt;
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
                }
                

                da = new SqlDataAdapter("select * from SuppCreditDebitDetail as d inner join SuppCreditDebitMaster as m on m.Id=d.SuppCreditDebitNo where d.SuppCreditDebitNo=" + CDNoteId + " and (d.AssemblyChildStatus='N' or d.AssemblyChildStatus='' or d.AssemblyChildStatus is null) and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "SuppCreditDebitDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                suppCDNRpt = new SupplierCreditNoteReport();
                suppCDNRpt.SetDataSource(ds);

                suppCDNRpt.SetParameterValue("suppliername", suppliername);
                suppCDNRpt.SetParameterValue("Address", address);
                suppCDNRpt.SetParameterValue("MobileNo", suppMobileno);
                suppCDNRpt.SetParameterValue("State", state);
                suppCDNRpt.SetParameterValue("GSTIN", GSTIN);
                suppCDNRpt.SetParameterValue("Statecode", statecode);
                suppCDNRpt.SetParameterValue("SCgst5Amt", Cgst5amt);
                suppCDNRpt.SetParameterValue("SCgst12Amt", Cgst12amt);
                suppCDNRpt.SetParameterValue("SCgst18Amt", Cgst18amt);
                suppCDNRpt.SetParameterValue("SCgst28Amt", Cgst28amt);
                suppCDNRpt.SetParameterValue("TotalSCgstAmt", TotCgstAmt);
                suppCDNRpt.SetParameterValue("Igst5Amt", Igst5amt);
                suppCDNRpt.SetParameterValue("Igst12Amt", Igst12amt);
                suppCDNRpt.SetParameterValue("Igst18Amt", Igst18amt);
                suppCDNRpt.SetParameterValue("Igst28Amt", Igst28amt);
                suppCDNRpt.SetParameterValue("TotalIgstAmt", TotIgstAmt);
                suppCDNRpt.SetParameterValue("CreditDebitType", CDType);
                crystalReportViewer1.ReportSource = suppCDNRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                dtMaster.Dispose();
                dtSupplier.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                SuppCreditDebitNoteMasterRepository suppCDmaster = new SuppCreditDebitNoteMasterRepository();
                var suppCDdata = suppCDmaster.GetAll().Where(t => t.Id == Convert.ToInt32(txtCDNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (suppCDdata != null)
                    getData(Convert.ToInt32(txtCDNo.Text));
                else
                {
                    MessageBox.Show("This credit debit number is not present.", "Warning");
                    txtCDNo.Text = "";
                    this.ActiveControl = txtCDNo;
                }
                suppCDmaster.Dispose();
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
