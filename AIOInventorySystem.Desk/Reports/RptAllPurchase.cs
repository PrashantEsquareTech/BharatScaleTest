using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptAllPurchase : Form
    {
        DbClass db = new DbClass();
        AllPurchaseOrderCrystalReport purchaserpt = new AllPurchaseOrderCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptAllPurchase(int pono, string mode)
        {
            InitializeComponent();
            Getdata(pono, mode);
        }

        public RptAllPurchase()
        {
            InitializeComponent();
            Getdata();
        }

        public RptAllPurchase(string ssname)
        {
            InitializeComponent();
            getdata(ssname);
        }

        public RptAllPurchase(string from, string to)
        {
            InitializeComponent();
        }

        public RptAllPurchase(string supplier = "", string mode = "", string mfgcomp = "", int pono = 0, string from1 = "", string to1 = "", string productname = "", string transType = "", string PurchaseAcc1 = "")
        {
            InitializeComponent();
            getdata(supplier, mode, mfgcomp, pono, from1, to1, productname, transType, PurchaseAcc1);
        }

        public RptAllPurchase(decimal d, decimal t)
        {
            InitializeComponent();
            Getdata(d, t);
        }

        public void getdata(string ssname)
        {
            int porderno = 0, MPorderNo = 0;
            string porderdate = "", suppliername = "";
            decimal totalamount = 0, discount = 0, netamt = 0, taxableamt = 0, gst = 0, totalgst = 0, vdetailamt = 0, Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0,
                Cgst28amt = 0, Sgst5amt = 0, Sgst12amt = 0, Sgst18amt = 0, Sgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, gstpercent = 0, taxable5amt = 0,
                taxable12amt = 0, taxable18amt = 0, taxable28amt = 0, cessamt = 0;
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSAllPurchase ds = new DSAllPurchase();
                DataTable dtmaster, dt;
                //                               0          1               2              3            4            5             6            7
                dtmaster = db.GetTable("Select pm.Id,pm.PorderDate,pm.Suppliername,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount,pm.SupplierId,pm.PorderNo from PurchaseMaster as pm where pm.Suppliername='" + ssname + "' and CompId ='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                DataTable dtdetail;
                int j = 1;

                for (int i = 0; i < dtmaster.Rows.Count; i++)
                {
                    porderno = Convert.ToInt32(dtmaster.Rows[i]["Id"].ToString());
                    MPorderNo = Convert.ToInt32(dtmaster.Rows[i]["PorderNo"].ToString());
                    porderdate = Convert.ToString(dtmaster.Rows[i]["PorderDate"].ToString());
                    suppliername = Convert.ToString(dtmaster.Rows[i]["Suppliername"].ToString());
                    netamt = Convert.ToDecimal(dtmaster.Rows[i]["NetAmount"].ToString());
                    discount = 0;
                    totalamount = 0;
                    netamt = 0;
                    taxableamt = 0;
                    cessamt = 0;
                    //                                    0             1           2             3           4             5          6           7          8             9               10          11  
                    dtdetail = db.GetTable("select pd.SGstPercent,pd.SGstAmt,pd.CGstPercent,pd.CGstAmt,pd.IGstPercent,pd.IGstAmt,pd.Discount,pd.Quantity,pd.Amount,pd.PurchasePrice,pd.TotalAmount,pd.cessAmt from PurchaseDetail as pd where PorderNo='" + porderno + "'");
                    discount = 0;
                    totalgst = 0;
                    for (int K = 0; K < dtdetail.Rows.Count; K++)
                    {
                        decimal purqty = 0;
                        if (purqty == 0)
                            purqty = Convert.ToDecimal(dtdetail.Rows[K]["Quantity"].ToString());
                        discount = discount + Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString());
                        totalamount = totalamount + purqty * Convert.ToDecimal(dtdetail.Rows[K]["PurchasePrice"].ToString());
                        taxableamt = (totalamount - (totalamount * discount) / 100);
                        netamt = netamt + Convert.ToDecimal(dtdetail.Rows[K]["TotalAmount"].ToString());
                        vdetailamt = (Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) - Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) * Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString()) / 100);
                        if (dtdetail.Rows[K]["SGstPercent"].ToString() != null)
                        {
                            gst = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                            totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                        }
                        else
                        {
                            gst = Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString());
                            totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                        }
                        gstpercent = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                        if (gstpercent == 5)
                        {
                            Cgst5amt = Cgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                            Sgst5amt = Sgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                            taxable5amt = taxable5amt + vdetailamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 12)
                        {
                            Cgst12amt = Cgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                            Sgst12amt = Sgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                            taxable12amt = taxable12amt + vdetailamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 18)
                        {
                            Cgst18amt = Cgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                            Sgst18amt = Sgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                            taxable18amt = taxable18amt + vdetailamt;
                            gstpercent = 0;
                        }
                        if (gstpercent == 28)
                        {
                            Cgst28amt = Cgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                            Sgst28amt = Sgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                            taxable28amt = taxable28amt + vdetailamt;
                            gstpercent = 0;
                        }
                        vdetailamt = 0;
                        if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 5)
                        {
                            Igst5amt = Igst5amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                            taxable5amt = taxable5amt + vdetailamt;
                        }
                        if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 12)
                        {
                            Igst12amt = Igst12amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                            taxable12amt = taxable12amt + vdetailamt;
                        }
                        if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 18)
                        {
                            Igst18amt = Igst18amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                            taxable18amt = taxable18amt + vdetailamt;
                        }
                        if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 28)
                        {
                            Igst28amt = Igst28amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                            taxable28amt = taxable28amt + vdetailamt;
                        }
                        cessamt += Convert.ToDecimal(dtdetail.Rows[K]["cessAmt"].ToString());
                    }
                    dt = ds.Tables["DataTable1"];
                    dt.Rows.Add(new object[11] { j, MPorderNo, porderdate, suppliername, totalamount, taxableamt, gst, totalgst, netamt, discount, cessamt });
                    j++;

                }
                da = new SqlDataAdapter("select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ComapanyInformation");

                purchaserpt = new AllPurchaseOrderCrystalReport();
                purchaserpt.SetDataSource(ds);
                purchaserpt.SetParameterValue("Cgst5amt", Cgst5amt);
                purchaserpt.SetParameterValue("Cgst12amt", Cgst12amt);
                purchaserpt.SetParameterValue("Cgst18amt", Cgst18amt);
                purchaserpt.SetParameterValue("Cgst28amt", Cgst28amt);
                purchaserpt.SetParameterValue("Sgst5amt", Sgst5amt);
                purchaserpt.SetParameterValue("Sgst12amt", Sgst12amt);
                purchaserpt.SetParameterValue("Sgst18amt", Sgst18amt);
                purchaserpt.SetParameterValue("Sgst28amt", Sgst28amt);
                purchaserpt.SetParameterValue("Igst5amt", Igst5amt);
                purchaserpt.SetParameterValue("Igst12amt", Igst12amt);
                purchaserpt.SetParameterValue("Igst18amt", Igst18amt);
                purchaserpt.SetParameterValue("Igst28amt", Igst28amt);
                purchaserpt.SetParameterValue("taxable5amount", taxable5amt);
                purchaserpt.SetParameterValue("taxable12amount", taxable12amt);
                purchaserpt.SetParameterValue("taxable18amt", taxable18amt);
                purchaserpt.SetParameterValue("taxable28amt", taxable28amt);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception) { }
        }

        public void getdata(string supplier = "", string mode = "", string mfgcomp = "", int pono = 0, string from1 = "", string to1 = "", string itemname = "", string transType = "", string PurchaseAcc1 = "", double disc = 0.00, double trans = 0.00)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSAllPurchase ds = new DSAllPurchase();
                string toDate = "", fromDate = "";
                if (from1 != "" && to1 != "")
                {
                    toDate = cm.ValidToDate(Convert.ToDateTime(to1));
                    fromDate = cm.ValidFromDate(Convert.ToDateTime(from1));
                }
                da = new SqlDataAdapter("SPAllPurchaseInvoiceList", db.Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@Type", SqlDbType.VarChar).Value = "Purchase";
                da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                if (fromDate != "" && fromDate != null)
                    da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fromDate;
                if (toDate != "" && toDate != null)
                    da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = toDate;
                if (supplier != "" && supplier != null)
                    da.SelectCommand.Parameters.Add("@SupplierName", SqlDbType.VarChar).Value = supplier;
                if (mode != "" && mode != null)
                    da.SelectCommand.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode;
                if (mfgcomp != "" && mfgcomp != null)
                    da.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = mfgcomp;
                if (pono != 0 && pono != null)
                    da.SelectCommand.Parameters.Add("@PONo", SqlDbType.Int).Value = pono;
                if (itemname != "" && itemname != null)
                    da.SelectCommand.Parameters.Add("@ItemName", SqlDbType.VarChar).Value = itemname;
                if (transType != "" && transType != null)
                    da.SelectCommand.Parameters.Add("@TransType", SqlDbType.VarChar).Value = transType;
                if (PurchaseAcc1 != "" && PurchaseAcc1 != null)
                    da.SelectCommand.Parameters.Add("@PurchaseAcc1", SqlDbType.VarChar).Value = PurchaseAcc1;
                da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                da.Fill(ds, "tblAllPurchaseListReport");

                da = new SqlDataAdapter("select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                da = new SqlDataAdapter("select * from Settings where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                AllPurchaseOrderCrystalReport1 purchaserpt = new AllPurchaseOrderCrystalReport1();
                purchaserpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(decimal d, decimal t)
        {
            try
            {
                if (d != 0 && t == 0)
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select distinct pm.PorderNo,pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.NetAmount,pm.Discount,pm.SpcDiscount,pd.IGstAmt,pd.IGstPercent,pd.CGstAmt,pd.CGstPercent,pd.SGstAmt,pd.SGstPercent from PurchaseMaster pm inner join PurchaseDetail pd on pm.Id=pd.PorderNo where Discount>0.00 and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DSAllPurchase ds = new DSAllPurchase();
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "DataTable1");

                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    purchaserpt = new AllPurchaseOrderCrystalReport();
                    purchaserpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select distinct pm.PorderNo,pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.NetAmount,pm.Discount,pm.SpcDiscount,pd.IGstAmt,pd.IGstPercent,pd.CGstAmt,pd.CGstPercent,pd.SGstAmt,pd.SGstPercent from PurchaseMaster pm inner join PurchaseDetail pd on pm.Id=pd.PorderNo where TransportCharges>0.00 and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DSAllPurchase ds = new DSAllPurchase();
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "DataTable1");

                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    purchaserpt = new AllPurchaseOrderCrystalReport();
                    purchaserpt.SetDataSource(ds);
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

        public void Getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select distinct pm.PorderNo,pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.NetAmount,pm.Discount,pm.SpcDiscount,pd.IGstAmt,pd.IGstPercent,pd.CGstAmt,pd.CGstPercent,pd.SGstAmt,pd.SGstPercent from PurchaseMaster pm inner join PurchaseDetail pd on pm.Id=pd.PorderNo where   pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllPurchase ds = new DSAllPurchase();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DataTable1");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new AllPurchaseOrderCrystalReport();
                purchaserpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata1(string suppname)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select distinct pm.PorderNo,pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.NetAmount,pm.Discount,pm.SpcDiscount,pd.IGstAmt,pd.IGstPercent,pd.CGstAmt,pd.CGstPercent,pd.SGstAmt,pd.SGstPercent from PurchaseMaster pm inner join PurchaseDetail pd on pm.Id=pd.PorderNo  where Suppliername ='" + suppname + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllPurchase ds = new DSAllPurchase();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DataTable1");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new AllPurchaseOrderCrystalReport();
                purchaserpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(int pono, string mode)
        {
            int porderno = 0;
            string porderdate = "", suppliername = "";
            decimal totalamount = 0, discount = 0, netamt = 0, taxableamt = 0, gst = 0, totalgst = 0, vdetailamt = 0, Cgst5amt = 0, Cgst12amt = 0, Cgst18amt = 0,
                Cgst28amt = 0, Sgst5amt = 0, Sgst12amt = 0, Sgst18amt = 0, Sgst28amt = 0, Igst5amt = 0, Igst12amt = 0, Igst18amt = 0, Igst28amt = 0, gstpercent = 0,
                taxable5amt = 0, taxable12amt = 0, taxable18amt = 0, taxable28amt = 0;

            try
            {
                if (pono != 0 && mode == null)
                {
                    db.connect();
                    SqlDataAdapter da;
                    DSAllPurchase ds = new DSAllPurchase();
                    DataTable dtmaster, dt, dtdetail;

                    dtmaster = db.GetTable("Select pm.Id,pm.PorderNo,pm.PorderDate,pm.Suppliername,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm where pm.PorderNo='" + pono + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");
                    int j = 1;

                    for (int i = 0; i < dtmaster.Rows.Count; i++)
                    {
                        porderno = Convert.ToInt32(dtmaster.Rows[i]["Id"].ToString());
                        porderdate = Convert.ToString(dtmaster.Rows[i]["PorderDate"].ToString());
                        suppliername = Convert.ToString(dtmaster.Rows[i]["Suppliername"].ToString());
                        netamt = Convert.ToDecimal(dtmaster.Rows[i]["NetAmount"].ToString());
                        discount = 0;
                        totalamount = 0;
                        netamt = 0;
                        taxableamt = 0;
                        dtdetail = db.GetTable("select pd.SGstPercent,pd.SGstAmt,pd.CGstPercent,pd.CGstAmt,pd.IGstPercent,pd.IGstAmt,pd.Discount,pd.Quantity,pd.Amount,pd.PurchasePrice,pd.TotalAmount from PurchaseDetail as pd  where PorderNo='" + porderno + "'");
                        discount = 0;
                        totalgst = 0;
                        for (int K = 0; K < dtdetail.Rows.Count; K++)
                        {
                            decimal purqty = 0;
                            if (purqty == 0)
                                purqty = Convert.ToDecimal(dtdetail.Rows[K]["Quantity"].ToString());
                            discount = discount + Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString());
                            totalamount = totalamount + purqty * Convert.ToDecimal(dtdetail.Rows[K]["PurchasePrice"].ToString());
                            taxableamt = (totalamount - (totalamount * discount) / 100);
                            netamt = netamt + Convert.ToDecimal(dtdetail.Rows[K]["TotalAmount"].ToString());
                            vdetailamt = (Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) - Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) * Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString()) / 100);
                            if (dtdetail.Rows[K]["SGstPercent"].ToString() != null)
                            {
                                gst = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                                totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                            }
                            else
                            {
                                gst = Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString());
                                totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                            }
                            gstpercent = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                            if (gstpercent == 5)
                            {
                                Cgst5amt = Cgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                Sgst5amt = Sgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                taxable5amt = taxable5amt + vdetailamt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 12)
                            {
                                Cgst12amt = Cgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                Sgst12amt = Sgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                taxable12amt = taxable12amt + vdetailamt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 18)
                            {
                                Cgst18amt = Cgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                Sgst18amt = Sgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                taxable18amt = taxable18amt + vdetailamt;
                                gstpercent = 0;
                            }
                            if (gstpercent == 28)
                            {
                                Cgst28amt = Cgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                Sgst28amt = Sgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                taxable28amt = taxable28amt + vdetailamt;
                                gstpercent = 0;
                            }
                            vdetailamt = 0;
                            if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 5)
                            {
                                Igst5amt = Igst5amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                taxable5amt = taxable5amt + vdetailamt;
                            }
                            if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 12)
                            {
                                Igst12amt = Igst12amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                taxable12amt = taxable12amt + vdetailamt;
                            }
                            if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 18)
                            {
                                Igst18amt = Igst18amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                taxable18amt = taxable18amt + vdetailamt;
                            }
                            if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 28)
                            {
                                Igst28amt = Igst28amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                taxable28amt = taxable28amt + vdetailamt;
                            }
                        }
                        dt = ds.Tables["DataTable1"];
                        dt.Rows.Add(new object[10] { j, porderno, porderdate, suppliername, totalamount, taxableamt, gst, totalgst, netamt, discount });
                        j++;
                    }
                    da = new SqlDataAdapter("select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.Fill(ds, "ComapanyInformation");

                    purchaserpt = new AllPurchaseOrderCrystalReport();
                    purchaserpt.SetDataSource(ds);
                    purchaserpt.SetParameterValue("Cgst5amt", Cgst5amt);
                    purchaserpt.SetParameterValue("Cgst12amt", Cgst12amt);
                    purchaserpt.SetParameterValue("Cgst18amt", Cgst18amt);
                    purchaserpt.SetParameterValue("Cgst28amt", Cgst28amt);
                    purchaserpt.SetParameterValue("Sgst5amt", Sgst5amt);
                    purchaserpt.SetParameterValue("Sgst12amt", Sgst12amt);
                    purchaserpt.SetParameterValue("Sgst18amt", Sgst18amt);
                    purchaserpt.SetParameterValue("Sgst28amt", Sgst28amt);
                    purchaserpt.SetParameterValue("Igst5amt", Igst5amt);
                    purchaserpt.SetParameterValue("Igst12amt", Igst12amt);
                    purchaserpt.SetParameterValue("Igst18amt", Igst18amt);
                    purchaserpt.SetParameterValue("Igst28amt", Igst28amt);
                    purchaserpt.SetParameterValue("taxable5amount", taxable5amt);
                    purchaserpt.SetParameterValue("taxable12amount", taxable12amt);
                    purchaserpt.SetParameterValue("taxable18amt", taxable18amt);
                    purchaserpt.SetParameterValue("taxable28amt", taxable28amt);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    if (mode == "Cash")
                    {
                        db.connect();
                        SqlDataAdapter da;
                        DSAllPurchase ds = new DSAllPurchase();
                        DataTable dtmaster, dt, dtdetail;

                        dtmaster = db.GetTable("Select pm.Id,pm.PorderNo,pm.PorderDate,pm.Suppliername,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm where pm.RemainingAmount=0.00 and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");
                        int j = 1;

                        for (int i = 0; i < dtmaster.Rows.Count; i++)
                        {
                            porderno = Convert.ToInt32(dtmaster.Rows[i]["Id"].ToString());
                            porderdate = Convert.ToString(dtmaster.Rows[i]["PorderDate"].ToString());
                            suppliername = Convert.ToString(dtmaster.Rows[i]["Suppliername"].ToString());
                            netamt = Convert.ToDecimal(dtmaster.Rows[i]["NetAmount"].ToString());
                            discount = 0;
                            totalamount = 0;
                            netamt = 0;
                            taxableamt = 0;
                            dtdetail = db.GetTable("select pd.SGstPercent,pd.SGstAmt,pd.CGstPercent,pd.CGstAmt,pd.IGstPercent,pd.IGstAmt,pd.Discount,pd.Quantity,pd.Amount,pd.PurchasePrice,pd.TotalAmount from PurchaseDetail as pd  where PorderNo='" + porderno + "' ");
                            discount = 0;
                            totalgst = 0;
                            for (int K = 0; K < dtdetail.Rows.Count; K++)
                            {
                                decimal purqty = 0;
                                if (purqty == 0)
                                    purqty = Convert.ToDecimal(dtdetail.Rows[K]["Quantity"].ToString());
                                discount = discount + Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString());
                                totalamount = totalamount + purqty * Convert.ToDecimal(dtdetail.Rows[K]["PurchasePrice"].ToString());
                                taxableamt = (totalamount - (totalamount * discount) / 100);
                                netamt = netamt + Convert.ToDecimal(dtdetail.Rows[K]["TotalAmount"].ToString());
                                vdetailamt = (Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) - Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) * Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString()) / 100);
                                if (dtdetail.Rows[K]["SGstPercent"].ToString() != null)
                                {
                                    gst = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                                    totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                }
                                else
                                {
                                    gst = Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString());
                                    totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                }
                                gstpercent = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                                if (gstpercent == 5)
                                {
                                    Cgst5amt = Cgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst5amt = Sgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable5amt = taxable5amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                if (gstpercent == 12)
                                {
                                    Cgst12amt = Cgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst12amt = Sgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable12amt = taxable12amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                if (gstpercent == 18)
                                {
                                    Cgst18amt = Cgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst18amt = Sgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable18amt = taxable18amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                if (gstpercent == 28)
                                {
                                    Cgst28amt = Cgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst28amt = Sgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable28amt = taxable28amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                vdetailamt = 0;
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 5)
                                {
                                    Igst5amt = Igst5amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable5amt = taxable5amt + vdetailamt;
                                }
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 12)
                                {
                                    Igst12amt = Igst12amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable12amt = taxable12amt + vdetailamt;
                                }
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 18)
                                {
                                    Igst18amt = Igst18amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable18amt = taxable18amt + vdetailamt;
                                }
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 28)
                                {
                                    Igst28amt = Igst28amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable28amt = taxable28amt + vdetailamt;
                                }
                            }
                            dt = ds.Tables["DataTable1"];
                            dt.Rows.Add(new object[10] { j, porderno, porderdate, suppliername, totalamount, taxableamt, gst, totalgst, netamt, discount });
                            j++;
                        }
                        da = new SqlDataAdapter("select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "ComapanyInformation");

                        purchaserpt = new AllPurchaseOrderCrystalReport();
                        purchaserpt.SetDataSource(ds);
                        purchaserpt.SetParameterValue("Cgst5amt", Cgst5amt);
                        purchaserpt.SetParameterValue("Cgst12amt", Cgst12amt);
                        purchaserpt.SetParameterValue("Cgst18amt", Cgst18amt);
                        purchaserpt.SetParameterValue("Cgst28amt", Cgst28amt);
                        purchaserpt.SetParameterValue("Sgst5amt", Sgst5amt);
                        purchaserpt.SetParameterValue("Sgst12amt", Sgst12amt);
                        purchaserpt.SetParameterValue("Sgst18amt", Sgst18amt);
                        purchaserpt.SetParameterValue("Sgst28amt", Sgst28amt);
                        purchaserpt.SetParameterValue("Igst5amt", Igst5amt);
                        purchaserpt.SetParameterValue("Igst12amt", Igst12amt);
                        purchaserpt.SetParameterValue("Igst18amt", Igst18amt);
                        purchaserpt.SetParameterValue("Igst28amt", Igst28amt);
                        purchaserpt.SetParameterValue("taxable5amount", taxable5amt);
                        purchaserpt.SetParameterValue("taxable12amount", taxable12amt);
                        purchaserpt.SetParameterValue("taxable18amt", taxable18amt);
                        purchaserpt.SetParameterValue("taxable28amt", taxable28amt);
                        crystalReportViewer1.ReportSource = purchaserpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    else
                    {
                        db.connect();
                        SqlDataAdapter da;
                        DSAllPurchase ds = new DSAllPurchase();
                        DataTable dtmaster, dt, dtdetail;

                        dtmaster = db.GetTable("Select pm.Id,pm.PorderNo,pm.PorderDate,pm.Suppliername,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm where pm.RemainingAmount!=0.00 and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");
                        int j = 1;

                        for (int i = 0; i < dtmaster.Rows.Count; i++)
                        {
                            porderno = Convert.ToInt32(dtmaster.Rows[i]["Id"].ToString());
                            porderdate = Convert.ToString(dtmaster.Rows[i]["PorderDate"].ToString());
                            suppliername = Convert.ToString(dtmaster.Rows[i]["Suppliername"].ToString());
                            netamt = Convert.ToDecimal(dtmaster.Rows[i]["NetAmount"].ToString());
                            discount = 0;
                            totalamount = 0;
                            netamt = 0;
                            taxableamt = 0;
                            dtdetail = db.GetTable("select pd.SGstPercent,pd.SGstAmt,pd.CGstPercent,pd.CGstAmt,pd.IGstPercent,pd.IGstAmt,pd.Discount,pd.Quantity,pd.Amount,pd.PurchasePrice,pd.TotalAmount from PurchaseDetail as pd  where pd.PorderNo='" + porderno + "'");
                            discount = 0;
                            totalgst = 0;
                            for (int K = 0; K < dtdetail.Rows.Count; K++)
                            {
                                decimal purqty = 0;
                                if (purqty == 0)
                                    purqty = Convert.ToDecimal(dtdetail.Rows[K]["Quantity"].ToString());
                                discount = discount + Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString());
                                totalamount = totalamount + purqty * Convert.ToDecimal(dtdetail.Rows[K]["PurchasePrice"].ToString());
                                taxableamt = (totalamount - (totalamount * discount) / 100);
                                netamt = netamt + Convert.ToDecimal(dtdetail.Rows[K]["TotalAmount"].ToString());
                                vdetailamt = (Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) - Convert.ToDecimal(dtdetail.Rows[K]["Amount"].ToString()) * Convert.ToDecimal(dtdetail.Rows[K]["Discount"].ToString()) / 100);
                                if (dtdetail.Rows[K]["SGstPercent"].ToString() != null)
                                {
                                    gst = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                                    totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                }
                                else
                                {
                                    gst = Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString());
                                    totalgst = totalgst + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                }
                                gstpercent = Convert.ToDecimal(dtdetail.Rows[K]["SGstPercent"].ToString()) + Convert.ToDecimal(dtdetail.Rows[K]["CGstPercent"].ToString());
                                if (gstpercent == 5)
                                {
                                    Cgst5amt = Cgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst5amt = Sgst5amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable5amt = taxable5amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                if (gstpercent == 12)
                                {
                                    Cgst12amt = Cgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst12amt = Sgst12amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable12amt = taxable12amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                if (gstpercent == 18)
                                {
                                    Cgst18amt = Cgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst18amt = Sgst18amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable18amt = taxable18amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                if (gstpercent == 28)
                                {
                                    Cgst28amt = Cgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["SGstAmt"].ToString());
                                    Sgst28amt = Sgst28amt + Convert.ToDecimal(dtdetail.Rows[K]["CGstAmt"].ToString());
                                    taxable28amt = taxable28amt + vdetailamt;
                                    gstpercent = 0;
                                }
                                vdetailamt = 0;
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 5)
                                {
                                    Igst5amt = Igst5amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable5amt = taxable5amt + vdetailamt;
                                }
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 12)
                                {
                                    Igst12amt = Igst12amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable12amt = taxable12amt + vdetailamt;
                                }
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 18)
                                {
                                    Igst18amt = Igst18amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable18amt = taxable18amt + vdetailamt;
                                }
                                if (Convert.ToDecimal(dtdetail.Rows[K]["IGstPercent"].ToString()) == 28)
                                {
                                    Igst28amt = Igst28amt + Convert.ToDecimal(dtdetail.Rows[K]["IGstAmt"].ToString());
                                    taxable28amt = taxable28amt + vdetailamt;
                                }
                            }
                            dt = ds.Tables["DataTable1"];
                            dt.Rows.Add(new object[10] { j, porderno, porderdate, suppliername, totalamount, taxableamt, gst, totalgst, netamt, discount });
                            j++;
                        }
                        da = new SqlDataAdapter("select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "ComapanyInformation");

                        purchaserpt = new AllPurchaseOrderCrystalReport();
                        purchaserpt.SetDataSource(ds);
                        purchaserpt.SetParameterValue("Cgst5amt", Cgst5amt);
                        purchaserpt.SetParameterValue("Cgst12amt", Cgst12amt);
                        purchaserpt.SetParameterValue("Cgst18amt", Cgst18amt);
                        purchaserpt.SetParameterValue("Cgst28amt", Cgst28amt);
                        purchaserpt.SetParameterValue("Sgst5amt", Sgst5amt);
                        purchaserpt.SetParameterValue("Sgst12amt", Sgst12amt);
                        purchaserpt.SetParameterValue("Sgst18amt", Sgst18amt);
                        purchaserpt.SetParameterValue("Sgst28amt", Sgst28amt);
                        purchaserpt.SetParameterValue("Igst5amt", Igst5amt);
                        purchaserpt.SetParameterValue("Igst12amt", Igst12amt);
                        purchaserpt.SetParameterValue("Igst18amt", Igst18amt);
                        purchaserpt.SetParameterValue("Igst28amt", Igst28amt);
                        purchaserpt.SetParameterValue("taxable5amount", taxable5amt);
                        purchaserpt.SetParameterValue("taxable12amount", taxable12amt);
                        purchaserpt.SetParameterValue("taxable18amt", taxable18amt);
                        purchaserpt.SetParameterValue("taxable28amt", taxable28amt);
                        crystalReportViewer1.ReportSource = purchaserpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int cbn = Convert.ToInt32(txtorderno.Text);
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var custdata = pmasterrepo.GetAll().Where(t => t.PorderNo == cbn && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (custdata != null)
                {
                    getdata("", "", "", cbn, "", "", "");
                }
                else
                {
                    MessageBox.Show("This Purchase Order Number is not Present.", "Success");
                    txtorderno.Text = "";
                    this.ActiveControl = txtorderno;
                }
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

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            txtorderno.Text = "";
            getdata("", "", "", 0, "", "", "");
        }
    }
}