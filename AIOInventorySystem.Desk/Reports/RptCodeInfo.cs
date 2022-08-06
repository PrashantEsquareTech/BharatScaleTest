using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptCodeInfo : Form
    {
        DbClass db = new DbClass();

        public RptCodeInfo()
        {
            InitializeComponent();
            getdata();
        }

        public void getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSCodeInfo ds = new DSCodeInfo();
                da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CodeInfo");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                CodeInfoRPT Cinforpt = new CodeInfoRPT();
                Cinforpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = Cinforpt;
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
