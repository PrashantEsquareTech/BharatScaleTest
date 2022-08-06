using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptVatRemainingStock : Form
    {
        DbClass db = new DbClass();
        vatstockCrystalReport Stockorpt = new vatstockCrystalReport();

        public RptVatRemainingStock()
        {
            InitializeComponent();
            getallstock();
        }

        public RptVatRemainingStock(string companyname, string productname)
        {
            InitializeComponent();
            getcompanyproduct(companyname, productname);
        }

        public void getcompanyproduct(string companyname, string productname)
        {
            try
            {
                if (companyname != null && productname == null)
                {
                    DSVatStock ds = new DSVatStock();                    

                    db.connect();
                    SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = companyname;
                    command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'C';
                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'y';
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                    command.CommandTimeout = 300;
                    SqlDataAdapter adap = new SqlDataAdapter(command);
                    adap.Fill(ds, "VatRemainingStock");

                    SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    Stockorpt = new vatstockCrystalReport();
                    Stockorpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = Stockorpt;
                    db.CloseConnection();
                    ds.Dispose();
                    da.Dispose();
                    adap.Dispose();
                }
                else
                {
                    ProductRepository productrepo = new ProductRepository();
                    int i = 1;
                    List<ProductInformation> product = new List<ProductInformation>();
                    List<ProductInformation> allproduct = new List<ProductInformation>();
                    if (CommonMethod.commProduct == true)
                        allproduct = productrepo.GetAll().Where(t => t.ProductName == productname && t.Vat == Convert.ToDecimal("0.00")).ToList();
                    else
                        allproduct = productrepo.GetAll().Where(t => t.ProductName == productname && t.Vat == Convert.ToDecimal("0.00") && t.CompId == CommonMethod.CompId).ToList();
                    DSVatStock ds = new DSVatStock();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");
                    DataTable dt = new DataTable();
                    foreach (var item in allproduct)
                    {
                        string pname = item.ProductName;
                        string comname = item.ManufactureCompany;
                        string unit = item.Uniti;
                        db.connect();
                        SqlCommand command = new SqlCommand("SPStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = pname;
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = comname;
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = "y";
                        command.Parameters.Add("@pqty", SqlDbType.Decimal, 18);
                        command.Parameters.Add("@sqty", SqlDbType.Decimal, 18);
                        command.Parameters.Add("@dqty", SqlDbType.Decimal, 18);
                        command.Parameters.Add("@rqty", SqlDbType.Decimal, 18);
                        command.Parameters.Add("@prqty", SqlDbType.Decimal, 18);
                        command.Parameters.Add("@srqty", SqlDbType.Decimal, 18);
                        command.Parameters["@pqty"].Direction = ParameterDirection.Output;
                        command.Parameters["@sqty"].Direction = ParameterDirection.Output;
                        command.Parameters["@dqty"].Direction = ParameterDirection.Output;
                        command.Parameters["@rqty"].Direction = ParameterDirection.Output;
                        command.Parameters["@prqty"].Direction = ParameterDirection.Output;
                        command.Parameters["@srqty"].Direction = ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        db.CloseConnection();
                        dt = ds.Tables["VatRemainingStock"];
                        dt.Rows.Add(new object[] { i, pname, comname, unit, command.Parameters["@pqty"].Value.ToString(), command.Parameters["@sqty"].Value.ToString(), command.Parameters["@dqty"].Value.ToString(), command.Parameters["@rqty"].Value.ToString() });
                        i++;
                    }
                    Stockorpt = new vatstockCrystalReport();
                    Stockorpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = Stockorpt;
                    db.CloseConnection();
                    productrepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getallstock()
        {
            try
            {
                DSVatStock ds = new DSVatStock();
                
                db.connect();
                SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'y';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                command.CommandTimeout = 300;
                SqlDataAdapter adap = new SqlDataAdapter(command);
                adap.Fill(ds, "VatRemainingStock");

                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                Stockorpt = new vatstockCrystalReport();
                Stockorpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = Stockorpt;
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
                adap.Dispose();
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