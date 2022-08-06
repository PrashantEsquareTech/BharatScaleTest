using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptDeliveryChallan : Form
    {
        DbClass db = new DbClass();
        CrystalReportDeliveryChallan deliverychallanrpt = new CrystalReportDeliveryChallan();

        public int CustId, detailchallanid;
        public string custadd, mno, vehicalno, PanNO, GSTIn;

        public RptDeliveryChallan()
        {
            InitializeComponent();
        }

        public RptDeliveryChallan(int challanno)
        {
            InitializeComponent();
            GetData(challanno);
        }

        public void GetData(int DChallanNo)
        {
            try
            {
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from DeliveryChallanMaster Where DeliveryChallanNo='" + DChallanNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSDeliveryChallan ds = new DSDeliveryChallan();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DeliveryChallanMaster");
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    detailchallanid = Convert.ToInt32(dt1.Rows[0]["Id"].ToString());
                    CustId = Convert.ToInt32(dt1.Rows[0]["CustomerId"].ToString());
                    vehicalno = dt1.Rows[0]["VehicalNo"].ToString();
                }
                DataTable dt = db.GetTable("Select * from CustomerInformation Where CustomerId=" + CustId + "  and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    custadd = Convert.ToString(dt.Rows[0]["CustomeAddress"].ToString());
                    mno = Convert.ToString(dt.Rows[0]["Landlineno"].ToString());
                    PanNO = Convert.ToString(dt.Rows[0]["Panno"].ToString());
                    GSTIn = Convert.ToString(dt.Rows[0]["BuyerTinNo"].ToString());
                }

                da = new SqlDataAdapter("Select d.* from DeliveryChallanDetail d inner join DeliveryChallanMaster m on m.Id=d.DeliveryChallanNo Where d.DeliveryChallanNo=" + detailchallanid + "  and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DeliveryChallanDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
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

                deliverychallanrpt = new CrystalReportDeliveryChallan();
                deliverychallanrpt.SetDataSource(ds);
                deliverychallanrpt.SetParameterValue("custadd", custadd);
                deliverychallanrpt.SetParameterValue("custcontact", mno);
                deliverychallanrpt.SetParameterValue("vehicalno", vehicalno);
                deliverychallanrpt.SetParameterValue("PanNO", PanNO);
                deliverychallanrpt.SetParameterValue("GSTIn", GSTIn);
                crystalReportViewer1.ReportSource = deliverychallanrpt;
                crystalReportViewer1.Refresh();
                crystalReportViewer1.ParameterFieldInfo.Clear();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                GetData(Convert.ToInt32(txtbillno.Text));
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