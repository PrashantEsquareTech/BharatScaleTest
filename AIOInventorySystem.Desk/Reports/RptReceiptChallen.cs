using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptReceiptChallen : Form
    {
        DbClass db = new DbClass();
        ReceiptChallen deliverychallanrpt = new ReceiptChallen();


        public RptReceiptChallen()
        {
            InitializeComponent();
        }

        public RptReceiptChallen(int challanno)
        {
            InitializeComponent();
            GetData(challanno);
        }

        public void GetData(int DChallanNo)
        {
            try
            {
                int CustId = 0, detailchallanid = 0;
                string custadd = "", mno = "";
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from RecieptChallanMaster Where RecieptChallanNo='" + DChallanNo + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSReceiptChallen ds = new DSReceiptChallen();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RecieptChallanMaster");
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                    detailchallanid = Convert.ToInt32(dt1.Rows[0]["Id"].ToString());
                    CustId = Convert.ToInt32(dt1.Rows[0]["SupplierId"].ToString());
                }
                DataTable dt;
                if (CommonMethod.commProduct == true)
                    dt = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + CustId + "  ");
                else
                    dt = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + CustId + " and CompId='" + CommonMethod.CompId + "'  ");
                if (dt.Rows.Count > 0)
                {
                    custadd = Convert.ToString(dt.Rows[0]["SupplierAddress"].ToString());
                    mno = Convert.ToString(dt.Rows[0]["ContactNo"].ToString());
                }

                da = new SqlDataAdapter("Select * from RecieptChalanDetail d inner join RecieptChallanMaster m on m.Id=d.RecieptChallanNo Where d.RecieptChallanNo=" + detailchallanid + "  and m.CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RecieptChalanDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                deliverychallanrpt = new ReceiptChallen();
                deliverychallanrpt.SetDataSource(ds);
                deliverychallanrpt.SetParameterValue("Suppadd", custadd);
                deliverychallanrpt.SetParameterValue("Suppcontact", mno);
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
