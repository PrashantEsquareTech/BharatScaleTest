using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class Rpt_StockRegister : Form
    {
        DbClass db = new DbClass();
        StockRegisterReport stockReport = new StockRegisterReport();
        MonthwiseStockRegisterReport month_stockReport = new MonthwiseStockRegisterReport();

        public Rpt_StockRegister()
        {
            InitializeComponent();
        }

        public Rpt_StockRegister(string ProductDesc, DateTime FromDate, DateTime ToDate, string Displaymonthwise, int categoryid)
        {
            InitializeComponent();
            getStockReport(ProductDesc, FromDate, ToDate, Displaymonthwise, categoryid);
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

        private void getStockReport(string ProductDesc, DateTime FromDate, DateTime ToDate, string DisplayMonthwise, int categoryid)
        {
            if (ProductDesc != "")
            {
                DataTable dtstock, datastock;

                string Proname = ProductDesc.Split(',')[0];
                string company = ProductDesc.Split(',')[1];
                string unit = ProductDesc.Split(',')[2];

                db.connect();
                SqlCommand cmd = new SqlCommand("sp_Stockregister", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ffromdate", SqlDbType.DateTime).Value = CommonMethod.startdate;
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(FromDate.ToShortDateString());
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(ToDate.ToShortDateString());
                cmd.Parameters.Add("@Productdesc", SqlDbType.NVarChar).Value = Proname;
                cmd.Parameters.Add("@MfgCompany", SqlDbType.NVarChar).Value = company;
                cmd.Parameters.Add("@Unit", SqlDbType.NVarChar).Value = unit;
                cmd.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtstock = new DataTable();
                da.Fill(dtstock);

                DsStockRegister ds = new DsStockRegister();

                DateTime transactionDate;
                decimal OpQty = 0, purQty = 0, purRetQty = 0, saleQty = 0, saleRetQty = 0, CloseQty = 0, DeadQty = 0;

                for (int j = 0; j < dtstock.Rows.Count; j++)
                {
                    transactionDate = Convert.ToDateTime(dtstock.Rows[j]["Transaction Date"]);
                    OpQty = Convert.ToDecimal(dtstock.Rows[j]["Opening Qty"]);
                    purQty = Convert.ToDecimal(dtstock.Rows[j]["Purchase Qty"]);
                    purRetQty = Convert.ToDecimal(dtstock.Rows[j]["Purchase Return Qty"]);
                    saleQty = Convert.ToDecimal(dtstock.Rows[j]["Sale Qty"]);
                    saleRetQty = Convert.ToDecimal(dtstock.Rows[j]["Sale Return Qty"]);
                    CloseQty = Convert.ToDecimal(dtstock.Rows[j]["Closing Qty"]);
                    DeadQty = Convert.ToDecimal(dtstock.Rows[j]["Dead Qty"]);

                    datastock = ds.Tables["StockRegisterData"];
                    datastock.Rows.Add(new object[10] { j + 1, transactionDate, OpQty, purQty, purRetQty, saleQty, saleRetQty, CloseQty, DeadQty, "" });
                }

                DataTable dtcomp = db.Gettable("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'");
                if (dtcomp.Rows.Count > 0)
                {
                    datastock = ds.Tables["CompanyInfo"];
                    datastock.Rows.Add(new object[2] { dtcomp.Rows[0][2].ToString(), dtcomp.Rows[0][3].ToString() });
                }

                stockReport = new StockRegisterReport();
                stockReport.SetDataSource(ds);
                stockReport.SetParameterValue("ProductDesc", ProductDesc);
                try
                {
                    crystalReportViewer1.ReportSource = stockReport;
                }
                catch (Exception)
                { }
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            else if (DisplayMonthwise == "Y")
            {
                DataTable dtstock, datastock;

                DateTime toDate = Convert.ToDateTime(ToDate.ToShortDateString());
                DateTime fromDate = Convert.ToDateTime(FromDate.ToShortDateString());

                db.connect();
                SqlCommand cmd = new SqlCommand("sp_Stockregister", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ffromdate", SqlDbType.DateTime).Value = CommonMethod.startdate;
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                cmd.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                cmd.Parameters.Add("@DisplayMonthwise", SqlDbType.Char).Value = "Y";
                cmd.Parameters.Add("@CatId", SqlDbType.Int).Value = categoryid;
                cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtstock = new DataTable();
                da.Fill(dtstock);

                DsStockRegister ds = new DsStockRegister();

                decimal OpQty = 0, purQty = 0, purRetQty = 0, saleQty = 0, saleRetQty = 0, CloseQty = 0, DeadQty = 0;
                string productdesc = "", categorydesc = "";
                for (int j = 0; j < dtstock.Rows.Count; j++)
                {
                    productdesc = Convert.ToString(dtstock.Rows[j]["Product"]);
                    categorydesc = Convert.ToString(dtstock.Rows[j]["Category"]);
                    OpQty = Convert.ToDecimal(dtstock.Rows[j]["Opening Qty"]);
                    purQty = Convert.ToDecimal(dtstock.Rows[j]["Purchase Qty"]);
                    purRetQty = Convert.ToDecimal(dtstock.Rows[j]["Purchase Return Qty"]);
                    saleQty = Convert.ToDecimal(dtstock.Rows[j]["Sale Qty"]);
                    saleRetQty = Convert.ToDecimal(dtstock.Rows[j]["Sale Return Qty"]);
                    CloseQty = Convert.ToDecimal(dtstock.Rows[j]["Closing Qty"]);
                    DeadQty = Convert.ToDecimal(dtstock.Rows[j]["Dead Qty"]);

                    datastock = ds.Tables["StockRegisterData"];
                    datastock.Rows.Add(new object[11] { j + 1, DateTime.Today.Date, OpQty, purQty, purRetQty, saleQty, saleRetQty, CloseQty, DeadQty, productdesc, 
                        categorydesc });
                }

                DataTable dtcomp = db.Gettable("Select * from CompanyInformation Where CompanyId='" + CommonMethod.CompId + "'");
                if (dtcomp.Rows.Count > 0)
                {
                    datastock = ds.Tables["CompanyInfo"];
                    datastock.Rows.Add(new object[2] { dtcomp.Rows[0][2].ToString(), dtcomp.Rows[0][3].ToString() });
                }

                month_stockReport = new MonthwiseStockRegisterReport();
                month_stockReport.SetDataSource(ds);
                month_stockReport.SetParameterValue("FromDate", fromDate);
                month_stockReport.SetParameterValue("ToDate", toDate);
                try
                {
                    crystalReportViewer1.ReportSource = month_stockReport;
                }
                catch (Exception)
                { }
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                dtstock.Dispose();
            }
        }
    }
}