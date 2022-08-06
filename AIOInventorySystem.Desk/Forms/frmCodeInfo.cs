using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCodeInfo : Form
    {
        DbClass db = new DbClass();

        public frmCodeInfo()
        {
            InitializeComponent();
            fillGodownName();
        }

        public void fillGodownName()
        {
            try
            {
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll().OrderByDescending(t=>t.GodownName).ToList();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GodownName).ToList();
                if (GDData.Count > 0)
                {
                    GDData.Add(new GodownMaster { GodownName = "Shop", ID = 0 });
                    GDData.Reverse();
                    cmbGodownName.DataSource = GDData;
                    cmbGodownName.ValueMember = "ID";
                    cmbGodownName.DisplayMember = "GodownName";
                    cmbGodownName.Enabled = true;
                    GDRepo.Dispose();
                }
                else
                {
                    cmbGodownName.DataSource = null;
                    cmbGodownName.Items.Clear();
                    cmbGodownName.DisplayMember = "Text";
                    cmbGodownName.ValueMember = "Value";
                    cmbGodownName.Items.Add(new { Text = "Shop", Value = 0 });
                    cmbGodownName.SelectedIndex = 0;
                    cmbGodownName.Enabled = false;
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

        public void rcode(string pname = "", string cname = "", string uname = "")
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkAll.Checked == false)
                {
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        if (cname != "" && pname == "" && uname == "")
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                        else if (cname != "" && pname != "" && uname != "")
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + pname + "' AND gd.CompanyName='" + cname + "' AND gd.Unit='" + uname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + pname + "' and MfgCompany='" + cname + "' and Unit='" + uname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + pname + "' AND gd.CompanyName='" + cname + "' AND gd.Unit='" + uname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                        else
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    }
                    else
                    {
                        if (cname != "" && pname == "" && uname == "")
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "') and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "'and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                        else
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + pname + "' AND gd.CompanyName='" + cname + "' AND gd.Unit='" + uname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + pname + "' and MfgCompany='" + cname + "' and Unit='" + uname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "') and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + pname + "' AND gd.CompanyName='" + cname + "' AND gd.Unit='" + uname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + pname + "' AND CompanyName='" + cname + "' AND Unit='" + uname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + pname + "' and m.MfgCompany='" + cname + "' and m.Unit='" + uname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    }
                }
                else
                {
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        if (cname != "" && pname == "" && uname == "")
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                        else
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    }
                    else
                    {
                        if (cname != "" && pname == "" && uname == "")
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where MfgCompany='" + cname + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "') and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where  CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.CompanyName='" + cname + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where CompanyName='" + cname + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.MfgCompany='" + cname + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                        else
                            da = new SqlDataAdapter("(Select m.ProductName,m.MfgCompany,m.Unit,m.Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select m.ProductName,m.MfgCompany,m.Unit,Quantity, d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.ProductName,d.CompanyName,d.Unit,d.Quantity, d.Code FROM PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "') and (d.code is not null and d.code != '') and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where m.CompId=" + CommonMethod.CompId + " and (m.StockIn='" + cmbGodownName.SelectedValue + "')) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    }
                }
                DataTable dt = new DataTable();
                da.Fill(dt);

                object sumqtyObject;
                sumqtyObject = dt.Compute("Sum(Quantity)", "");
                lbltotalqty.Text = Convert.ToString(sumqtyObject);
                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                if (GvproductInfo.Rows.Count != 0)
                {
                    GvproductInfo.Columns["ProductName"].Width = 120;
                    GvproductInfo.Columns["MfgCompany"].Width = 100;
                    GvproductInfo.Columns["Unit"].Width = 80;
                    GvproductInfo.Columns["Quantity"].Width = 80;
                    GvproductInfo.Columns["Code"].Width = 80;
                }
                else
                {
                    MessageBox.Show("Record Not found.", "Warning");
                    lbltotalqty.Text = "0";
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnrcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "")
                {
                    try
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];
                        chkPurchase.CheckState = 0;
                        chkPurReturn.CheckState = 0;
                        chkSale.CheckState = 0;
                        chkSaleReturn.CheckState = 0;
                        chkDead.CheckState = 0;
                        chkTransfer.CheckState = 0;
                        rcode(Proname, company, unit);
                    }
                    catch (Exception)
                    { }
                }
                else if (txtMfg.Text.Trim() != "")
                    rcode("", txtMfg.Text, "");
                else if (chkAll.Checked == true)
                    rcode();
                else
                {
                    MessageBox.Show("Please Enter Product Name.", "Warning");
                    this.ActiveControl = txtpname;
                }
            }
            catch (Exception)
            { }
        }

        private void frmCodeInfo_Load(object sender, EventArgs e)
        {
            try
            {
                txtpname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation ", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2));
                        }
                        catch (Exception)
                        { }
                    }
                    txtpname.AutoCompleteCustomSource = MyCollection;
                    reader.Dispose();
                    SqlCommand cmd1 = null;
                    if (CommonMethod.commProduct == true)
                        cmd1 = new SqlCommand("SELECT distinct ManufactureCompany FROM ProductInformation ", con);
                    else
                        cmd1 = new SqlCommand("SELECT distinct ManufactureCompany FROM ProductInformation where compid=" + CommonMethod.CompId, con);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection MyCollection1 = new AutoCompleteStringCollection();
                    while (reader1.Read())
                    {
                        try
                        {
                            MyCollection1.Add(reader1.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtMfg.AutoCompleteCustomSource = MyCollection1;
                    con.Close();
                    reader1.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkPurchase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPurchase.Checked)
                {
                    chkSale.Checked = false;
                    chkSaleReturn.Checked = false;
                    chkPurReturn.Checked = false;
                    chkDead.Checked = false;
                    chkTransfer.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkPurReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkPurReturn.Checked)
                {
                    chkSale.Checked = false;
                    chkSaleReturn.Checked = false;
                    chkPurchase.Checked = false;
                    chkDead.Checked = false;
                    chkTransfer.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkSale_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSale.Checked)
                {
                    chkPurReturn.Checked = false;
                    chkSaleReturn.Checked = false;
                    chkPurchase.Checked = false;
                    chkDead.Checked = false;
                    chkTransfer.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkSaleReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSaleReturn.Checked)
                {
                    chkPurReturn.Checked = false;
                    chkSale.Checked = false;
                    chkPurchase.Checked = false;
                    chkDead.Checked = false;
                    chkTransfer.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkDead_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDead.Checked)
                {
                    chkPurReturn.Checked = false;
                    chkSale.Checked = false;
                    chkPurchase.Checked = false;
                    chkSaleReturn.Checked = false;
                    chkTransfer.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkTransfer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTransfer.Checked)
                {
                    chkPurReturn.Checked = false;
                    chkSale.Checked = false;
                    chkPurchase.Checked = false;
                    chkSaleReturn.Checked = false;
                    chkDead.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "" || txtMfg.Text != "")
                {
                    string Proname = "", company = "", unit = "";
                    if (txtpname.Text != "")
                    {
                        Proname = txtpname.Text.Trim().Split(',')[0];
                        company = txtpname.Text.Trim().Split(',')[1];
                        unit = txtpname.Text.Trim().Split(',')[2];
                    }
                    if (chkPurchase.Checked)
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.PorderNo as 'Porder No',CONVERT(VARCHAR(24),m.PorderDate,101) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where (d.code is not null and d.code != '') and d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and  m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + cmbGodownName.SelectedValue + "' or m.StockIn is null) and (d.code is not null and d.code != '')", db.Connection);
                            else if (txtMfg.Text != "")
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.PorderNo as 'Porder No',CONVERT(VARCHAR(24),m.PorderDate,101) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where (d.code is not null and d.code != '') and d.CompanyName='" + company + "'and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + cmbGodownName.SelectedValue + "' or m.StockIn is null)", db.Connection);
                            }
                        }
                        else
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.PorderNo as 'Porder No',CONVERT(VARCHAR(24),m.PorderDate,101) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where (d.code is not null and d.code != '') and d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.CompId='" + CommonMethod.CompId + "' and m.StockIn='" + cmbGodownName.SelectedValue + "'", db.Connection);
                            else
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.PorderNo as 'Porder No',CONVERT(VARCHAR(24),m.PorderDate,101) as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where (d.code is not null and d.code != '') and d.CompanyName='" + company + "' and m.CompId='" + CommonMethod.CompId + "' and m.StockIn='" + cmbGodownName.SelectedValue + "'", db.Connection);
                            }
                        }
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        object sumqtyObject;
                        sumqtyObject = dt.Compute("Sum(Quantity)", "");
                        lbltotalqty.Text = Convert.ToString(sumqtyObject);
                        GvproductInfo.DataSource = dt;
                        GvproductInfo.Refresh();
                        if (GvproductInfo.Rows.Count != 0)
                            GvproductInfo.Columns["Product Name"].Width = 120;
                        else
                        {
                            MessageBox.Show("Record Not found.", "Warning");
                            lbltotalqty.Text = "0";
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else if (chkPurReturn.Checked)
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.PurchaseReturnNo as 'PR No',CONVERT(VARCHAR(24),m.PurchaseReturnDate,101) as 'PR Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from  PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseDetail as pd on d.POID=pd.Id inner join PurchaseMaster as pm on pm.Id=pd.PorderNo Where (d.code is not null and d.code != '') and d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and  m.CompId='" + CommonMethod.CompId + "' and (pm.StockIn='" + cmbGodownName.SelectedValue + "' or pm.StockIn is null)", db.Connection);
                            else
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.PurchaseReturnNo as 'PR No',CONVERT(VARCHAR(24),m.PurchaseReturnDate,101) as 'PR Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from  PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseDetail as pd on d.POID=pd.Id inner join PurchaseMaster as pm on pm.Id=pd.PorderNo Where (d.code is not null and d.code != '') and d.CompanyName='" + company + "' and  m.CompId='" + CommonMethod.CompId + "' and (pm.StockIn='" + cmbGodownName.SelectedValue + "' or pm.StockIn is null)", db.Connection);
                            }
                        }
                        else
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.PurchaseReturnNo as 'PR No',CONVERT(VARCHAR(24),m.PurchaseReturnDate,101) as 'PR Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from  PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseDetail as pd on d.POID=pd.Id inner join PurchaseMaster as pm on pm.Id=pd.PorderNo Where (d.code is not null and d.code != '') and d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.CompId='" + CommonMethod.CompId + "' and pm.StockIn='" + cmbGodownName.SelectedValue + "'", db.Connection);
                            else
                                da = new SqlDataAdapter("Select m.PurchaseReturnNo as 'PR No',CONVERT(VARCHAR(24),m.PurchaseReturnDate,101) as 'PR Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',d.Unit,(d.Quantity+isnull(d.FreeQty,0)) as Quantity,d.PurchasePrice,d.TotalAmount,d.Code from  PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseDetail as pd on d.POID=pd.Id inner join PurchaseMaster as pm on pm.Id=pd.PorderNo Where (d.code is not null and d.code != '') and d.CompanyName='" + company + "' and m.CompId='" + CommonMethod.CompId + "' and pm.StockIn='" + cmbGodownName.SelectedValue + "'", db.Connection);
                        }
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        object sumqtyObject;
                        sumqtyObject = dt.Compute("Sum(Quantity)", "");
                        lbltotalqty.Text = Convert.ToString(sumqtyObject);
                        GvproductInfo.DataSource = dt;
                        GvproductInfo.Refresh();
                        if (GvproductInfo.Rows.Count != 0)
                            GvproductInfo.Columns["Product Name"].Width = 120;
                        else
                        {
                            MessageBox.Show("Record Not found.", "Warning");
                            lbltotalqty.Text = "0";
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else if (chkSale.Checked)
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount,d.Code from CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where d.Code!=' ' and ProductId='" + Proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "", db.Connection);
                            else
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount,d.Code from CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where d.Code!=' ' and CompanyName='" + company + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "", db.Connection);
                            }
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            object sumqtyObject;
                            sumqtyObject = dt.Compute("Sum(Quantity)", "");
                            lbltotalqty.Text = Convert.ToString(sumqtyObject);
                            GvproductInfo.DataSource = dt;
                            GvproductInfo.Refresh();
                            if (GvproductInfo.Rows.Count != 0)
                                GvproductInfo.Columns["Product Name"].Width = 120;
                            else
                            {
                                MessageBox.Show("Record Not found.", "Warning");
                                lbltotalqty.Text = "0";
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Choice.", "Warning");
                            fillGodownName();
                        }
                    }
                    else if (chkSaleReturn.Checked)
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.GoodReturnNo as 'SR No',CONVERT(VARCHAR(24),m.GoodReturnDate,103) as 'SR Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount,d.Code from GoodReturnDetail as d inner join GoodReturnMaster as m on m.Id=d.GoodReturnNo Where d.Code!=' ' and ProductId='" + Proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "", db.Connection);
                            else
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.GoodReturnNo as 'SR No',CONVERT(VARCHAR(24),m.GoodReturnDate,103) as 'SR Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount,d.Code from GoodReturnDetail as d inner join GoodReturnMaster as m on m.Id=d.GoodReturnNo Where d.Code!=' ' and CompanyName='" + company + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "", db.Connection);
                            }
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            object sumqtyObject;
                            sumqtyObject = dt.Compute("Sum(Quantity)", "");
                            lbltotalqty.Text = Convert.ToString(sumqtyObject);
                            GvproductInfo.DataSource = dt;
                            GvproductInfo.Refresh();
                            if (GvproductInfo.Rows.Count != 0)
                                GvproductInfo.Columns["Product Name"].Width = 120;
                            else
                            {
                                MessageBox.Show("Record Not found.", "Warning");
                                lbltotalqty.Text = "0";
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Choice.", "Warning");
                            fillGodownName();
                        }
                    }
                    else if (chkDead.Checked)
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.DeadNo as 'Dead No',CONVERT(VARCHAR(24),m.DeadDate,101) as 'Dead Date',d.ProductName as 'Product Name',d.Unit,d.Quantity ,d.Code from  DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where d.Code!=' ' and d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and  m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + cmbGodownName.SelectedValue + "' or m.StockIn is null)", db.Connection);
                            else
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.DeadNo as 'Dead No',CONVERT(VARCHAR(24),m.DeadDate,101) as 'Dead Date',d.ProductName as 'Product Name',d.Unit,d.Quantity ,d.Code from  DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where d.Code!=' ' and d.CompanyName='" + company + "' and  m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + cmbGodownName.SelectedValue + "' or m.StockIn is null)", db.Connection);
                            }
                        }
                        else
                        {
                            if (txtpname.Text != "")
                                da = new SqlDataAdapter("Select m.DeadNo as 'Dead No',CONVERT(VARCHAR(24),m.DeadDate,101) as 'Dead Date',d.ProductName as 'Product Name',d.Unit,d.Quantity ,d.Code from  DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where d.Code !=' ' and d.ProductName='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "' and m.CompId='" + CommonMethod.CompId + "' and m.StockIn='" + cmbGodownName.SelectedValue + "'", db.Connection);
                            else
                            {
                                company = txtMfg.Text;
                                da = new SqlDataAdapter("Select m.DeadNo as 'Dead No',CONVERT(VARCHAR(24),m.DeadDate,101) as 'Dead Date',d.ProductName as 'Product Name',d.Unit,d.Quantity ,d.Code from  DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where d.Code !=' ' and d.CompanyName='" + company + "' and m.CompId='" + CommonMethod.CompId + "' and m.StockIn='" + cmbGodownName.SelectedValue + "'", db.Connection);
                            }
                        }
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        object sumqtyObject;
                        sumqtyObject = dt.Compute("Sum(Quantity)", "");
                        lbltotalqty.Text = Convert.ToString(sumqtyObject);
                        GvproductInfo.DataSource = dt;
                        GvproductInfo.Refresh();
                        if (GvproductInfo.Rows.Count != 0)
                        { }
                        else
                        {
                            MessageBox.Show("Record Not found.", "Warning");
                            lbltotalqty.Text = "0";
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else if (chkTransfer.Checked)
                    {
                        SqlDataAdapter da = new SqlDataAdapter();
                        if (txtpname.Text != "")
                            da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),d.TransferDate,101) as 'Transfer Date',d.TransferFrom,d.ProductName as 'Product Name',d.Unit,d.Quantity ,m.Code from  TransferStock as d inner join TransferStockDetail as m on d.Id=m.TransferId Where m.Code!=' ' and d.ProductName='" + Proname + "' and d.MfgCompany='" + company + "' and d.Unit='" + unit + "' and  d.CompId='" + CommonMethod.CompId + "' ", db.Connection);
                        else
                        {
                            company = txtMfg.Text;
                            da = new SqlDataAdapter("Select CONVERT(VARCHAR(24),d.TransferDate,101) as 'Transfer Date',d.TransferFrom,d.ProductName as 'Product Name',d.Unit,d.Quantity ,m.Code from  TransferStock as d inner join TransferStockDetail as m on d.Id=m.TransferId Where m.Code!=' ' and d.MfgCompany='" + company + "' and  d.CompId='" + CommonMethod.CompId + "' ", db.Connection);
                        }
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        object sumqtyObject;
                        sumqtyObject = dt.Compute("Sum(Quantity)", "");
                        lbltotalqty.Text = Convert.ToString(sumqtyObject);
                        GvproductInfo.DataSource = dt;
                        GvproductInfo.Refresh();
                        if (GvproductInfo.Rows.Count != 0)
                            GvproductInfo.Columns["Product Name"].Width = 120;
                        else
                        {
                            MessageBox.Show("Record Not found.", "Warning");
                            lbltotalqty.Text = "0";
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Check CheckBox First.", "Warning");
                        this.ActiveControl = chkPurchase;
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Product Name.", "Warning");
                    this.ActiveControl = txtpname;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked == true)
            {
                txtpname.Enabled = false;
                txtpname.Text = "";
            }
            else
                txtpname.Enabled = true;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked == true)
                {
                    RptCodeInfo codeinfo = new RptCodeInfo();
                    codeinfo.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please Check CheckBox First.", "Warning");
                    this.ActiveControl = chkAll;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkPurchase;
            }
            catch (Exception) { }
        }

        private void chkPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkPurchase.Checked == true)
                        this.ActiveControl = btnSearch;
                    else
                        this.ActiveControl = chkPurReturn;
                }
            }
            catch (Exception) { }
        }

        private void chkPurReturn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkPurReturn.Checked == true)
                        this.ActiveControl = btnSearch;
                    else
                        this.ActiveControl = chkSale;
                }
            }
            catch (Exception) { }
        }

        private void chkSale_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkSale.Checked == true)
                        this.ActiveControl = btnSearch;
                    else
                        this.ActiveControl = chkSaleReturn;
                }
            }
            catch (Exception) { }
        }

        private void chkSaleReturn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkSaleReturn.Checked == true)
                        this.ActiveControl = btnSearch;
                    else
                        this.ActiveControl = chkTransfer;
                }
            }
            catch (Exception) { }
        }

        private void chkTransfer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkTransfer.Checked == true)
                        this.ActiveControl = btnSearch;
                    else
                        this.ActiveControl = chkDead;
                }
            }
            catch (Exception) { }
        }

        private void chkDead_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkDead.Checked == true)
                        this.ActiveControl = btnSearch;
                    else
                        this.ActiveControl = btnrcode;
                }
            }
            catch (Exception) { }
        }

        private void chkAll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnprint;
            }
            catch (Exception) { }
        }

        private void txtMfg_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkPurchase;
            }
            catch (Exception) { }
        }

        private void cmbGodownName_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
            }
            catch (Exception) { }
        }
    }
}