using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptOrderbook : Form
    {
        DbClass db = new DbClass();
        OrderbookRpt orderpt = new OrderbookRpt();

        public RptOrderbook()
        {
            InitializeComponent();
        }

        public RptOrderbook(int orderno)
        {
            InitializeComponent();
            GetData(orderno);
        }

        public void GetData(int orderbookno)
        {
            try
            {
                int orderid = 0, custvalue = 0;
                string custadd = "", mobileno = "", landline = "", day = "", custname = "";
                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from OrderBook Where Orderbookno='" + orderbookno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                OrderbookDS ds = new OrderbookDS();
                da.Fill(ds, "OrderBook");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    orderid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    custvalue = Convert.ToInt32(dt.Rows[0]["Customerid"].ToString());
                }
                DataTable dt1 = db.GetTable("Select * from CustomerInformation Where CustomerID=" + custvalue + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    custname = dt1.Rows[0]["CustomerName"].ToString();
                    custadd = dt1.Rows[0]["CustomeAddress"].ToString();
                    mobileno = dt1.Rows[0]["Mobileno"].ToString();
                    landline = dt1.Rows[0]["Landlineno"].ToString();
                    day = dt1.Rows[0]["CreditDays"].ToString();
                }                

                da = new SqlDataAdapter("Select * from OrderBookdetail d inner join OrderBook m on m.Id=d.Orderno Where d.Orderno=" + orderid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "OrderBookdetail");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
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

                orderpt = new OrderbookRpt();
                orderpt.SetDataSource(ds);
                orderpt.SetParameterValue("custname", custname);
                orderpt.SetParameterValue("custaddress", custadd);
                orderpt.SetParameterValue("mobileno", mobileno);
                orderpt.SetParameterValue("landlineno", landline);
                orderpt.SetParameterValue("day", day);

                crystalReportViewer1.ReportSource = orderpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
                dt1.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                OrderbookingRepository orderbookrepo = new OrderbookingRepository();
                var custdata = orderbookrepo.GetAll().Where(t => t.Orderbookno == Convert.ToInt32(txtorderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                    GetData(Convert.ToInt32(txtorderno.Text));
                else
                {
                    MessageBox.Show("This Order Number is not Present.", "Warning");
                    txtorderno.Text = "";
                    this.ActiveControl = txtorderno;
                }
                orderbookrepo.Dispose();
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