using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptRemainingtock : Form
    {
        DbClass db = new DbClass();
        RemainingtockCrystalReport Stockorpt = new RemainingtockCrystalReport();

        public RptRemainingtock()
        {
            InitializeComponent();
            getvatstock();
        }

        public RptRemainingtock(char i)
        {
            InitializeComponent();
            getallstock(i);
        }

        public RptRemainingtock(string companyname, string productname, char Type)
        {
            InitializeComponent();
            getcompanyproduct(companyname, productname, Type);
        }

        public RptRemainingtock(string date,char Type)
        {
            InitializeComponent();
            getDateWiseStock(date, Type);
        }

        public void getcompanyproduct(string companyname, string productname, char Type)
        {
            try
            {
                if (companyname != null && productname == null)
                {
                    try
                    {
                        db.connect();
                        SqlDataAdapter da;
                        DSRemainingstock ds = new DSRemainingstock();

                        da = new SqlDataAdapter("SPVatnWithoutVatStock", db.Connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = companyname;
                        da.SelectCommand.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.Parameters.Add("@stock", SqlDbType.VarChar).Value = "C";
                        da.SelectCommand.Parameters.Add("@vat", SqlDbType.VarChar).Value = Type;
                        da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        da.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.CommandTimeout = 1000;
                        da.Fill(ds, "Stock");

                        DataTable dtStock = ds.Tables["Stock"];
                        object AmtSum;
                        AmtSum = dtStock.Compute("Sum(Amount)", "RemQty>=0");
                        if (string.IsNullOrEmpty(AmtSum.ToString()))
                            AmtSum = 0;
                        object QtySum;
                        QtySum = dtStock.Compute("Sum(RemQty)", "RemQty>=0");
                        if (string.IsNullOrEmpty(QtySum.ToString()))
                            QtySum = 0;
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "CompanyInformation");

                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        Stockorpt.SetParameterValue("TotalAmt", Convert.ToDecimal(AmtSum));
                        Stockorpt.SetParameterValue("TotalQty", Convert.ToDecimal(QtySum));
                        crystalReportViewer1.ReportSource = Stockorpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
                else if (companyname == null && productname != null)
                {
                    try
                    {
                        string Proname1 = productname.Split(',')[0];
                        string company = productname.Split(',')[1];
                        string unit = productname.Split(',')[2];
                        string Proname = "";
                        if (Proname1.Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        db.connect();
                        SqlDataAdapter da;
                        DSRemainingstock ds = new DSRemainingstock();

                        da = new SqlDataAdapter("SPVatnWithoutVatStock", db.Connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname;
                        da.SelectCommand.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                        da.SelectCommand.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                        da.SelectCommand.Parameters.Add("@stock", SqlDbType.VarChar).Value = "P";
                        da.SelectCommand.Parameters.Add("@vat", SqlDbType.VarChar).Value = Type;
                        da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        da.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.CommandTimeout = 1000;
                        da.Fill(ds, "Stock");

                        DataTable dtStock = ds.Tables["Stock"];
                        object AmtSum;
                        AmtSum = dtStock.Compute("Sum(Amount)", "RemQty>=0");
                        if (string.IsNullOrEmpty(AmtSum.ToString()))
                            AmtSum = 0;
                        object QtySum;
                        QtySum = dtStock.Compute("Sum(RemQty)", "RemQty>=0");
                        if (string.IsNullOrEmpty(QtySum.ToString()))
                            QtySum = 0;
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "CompanyInformation");

                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        Stockorpt.SetParameterValue("TotalAmt", Convert.ToDecimal(AmtSum));
                        Stockorpt.SetParameterValue("TotalQty", Convert.ToDecimal(QtySum));
                        crystalReportViewer1.ReportSource = Stockorpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
                else
                {
                    try
                    {
                        db.connect();
                        SqlDataAdapter da;
                        DSRemainingstock ds = new DSRemainingstock();

                        da = new SqlDataAdapter("SPVatnWithoutVatStock", db.Connection);
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.Parameters.Add("@vat", SqlDbType.VarChar).Value = Type;
                        da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        da.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        da.SelectCommand.CommandTimeout = 1000;
                        da.Fill(ds, "Stock");

                        DataTable dtStock = ds.Tables["Stock"];
                        object AmtSum;
                        AmtSum = dtStock.Compute("Sum(Amount)", "RemQty>=0");
                        if (string.IsNullOrEmpty(AmtSum.ToString()))
                            AmtSum = 0;
                        object QtySum;
                        QtySum = dtStock.Compute("Sum(RemQty)", "RemQty>=0");
                        if (string.IsNullOrEmpty(QtySum.ToString()))
                            QtySum = 0;
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "CompanyInformation");

                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        Stockorpt.SetParameterValue("TotalAmt", Convert.ToDecimal(AmtSum));
                        Stockorpt.SetParameterValue("TotalQty", Convert.ToDecimal(QtySum));
                        crystalReportViewer1.ReportSource = Stockorpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
            }
            catch (Exception)
            { }
        }

        public void getallstock(char Type)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSRemainingstock ds = new DSRemainingstock();

                da = new SqlDataAdapter("SPVatnWithoutVatStock", db.Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@vat", SqlDbType.VarChar).Value = Type;
                da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                da.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.CommandTimeout = 1000;
                da.Fill(ds, "Stock");

                DataTable dtStock = ds.Tables["Stock"];
                object AmtSum;
                AmtSum = dtStock.Compute("Sum(Amount)", "RemQty>=0");
                if (string.IsNullOrEmpty(AmtSum.ToString()))
                    AmtSum = 0;
                object QtySum;
                QtySum = dtStock.Compute("Sum(RemQty)", "RemQty>=0");
                if (string.IsNullOrEmpty(QtySum.ToString()))
                    QtySum = 0;
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                Stockorpt = new RemainingtockCrystalReport();
                Stockorpt.SetDataSource(ds);
                Stockorpt.SetParameterValue("TotalAmt", Convert.ToDecimal(AmtSum));
                Stockorpt.SetParameterValue("TotalQty", Convert.ToDecimal(QtySum));
                crystalReportViewer1.ReportSource = Stockorpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getDateWiseStock(string tdate,char Type)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSRemainingstock ds = new DSRemainingstock();               

                da = new SqlDataAdapter("SPVatnWithoutVatStock", db.Connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                da.SelectCommand.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'D';
                da.SelectCommand.Parameters.Add("@vat", SqlDbType.VarChar).Value = Type;
                da.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                da.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                da.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = tdate;
                da.SelectCommand.CommandTimeout = 1000;
                da.Fill(ds, "Stock");

                DataTable dtStock = ds.Tables["Stock"];
                object AmtSum;
                AmtSum = dtStock.Compute("Sum(Amount)", "RemQty>=0");
                if (string.IsNullOrEmpty(AmtSum.ToString()))
                    AmtSum = 0;
                object QtySum;
                QtySum = dtStock.Compute("Sum(RemQty)", "RemQty>=0");
                if (string.IsNullOrEmpty(QtySum.ToString()))
                    QtySum = 0;
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                Stockorpt = new RemainingtockCrystalReport();
                Stockorpt.SetDataSource(ds);
                Stockorpt.SetParameterValue("TotalAmt", Convert.ToDecimal(AmtSum));
                Stockorpt.SetParameterValue("TotalQty", Convert.ToDecimal(QtySum));
                crystalReportViewer1.ReportSource = Stockorpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { }
        }

        public void getvatstock()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("SELECT *  FROM [PurchaseMaster] inner join [PurchaseDetail] on PurchaseMaster.Id=PurchaseDetail.PorderNo where  (PurchaseMaster.Vat!=0.00 or PurchaseMaster.TAXType='TaxPurchase') and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DSRemainingstock ds = new DSRemainingstock();
                da.Fill(ds, "PurchaseDetail");
                da.Fill(ds, "PurchaseMaster");
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                Stockorpt = new RemainingtockCrystalReport();
                Stockorpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Stockorpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
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