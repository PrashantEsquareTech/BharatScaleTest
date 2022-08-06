using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptProductInformation : Form
    {
        DbClass db = new DbClass();
        ProductInfoCrystalReport pinforpt = new ProductInfoCrystalReport();
        public static DataSet ds;
        public static ProductinfoDS pInfoDs;

        public RptProductInformation()
        {
            InitializeComponent();
            getdata();
        }

        public RptProductInformation(int pid)
        {
            InitializeComponent();
            getdata(pid);
        }

        public RptProductInformation(string company, string pname)
        {
            InitializeComponent();
            companyname(company, pname);
        }

        public RptProductInformation(string company, int GroupMasterId, string pname, ProductInfoCrystalReport pinforpt)
        {
            InitializeComponent();
            companyname(company, GroupMasterId, pname, pinforpt);
        }

        public RptProductInformation(string company, int GroupMasterId, string pname)
        {
            InitializeComponent();
            ExcelCompany(company, GroupMasterId, pname);
        }

        public void getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                ProductinfoDS ds = new ProductinfoDS();

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation order by ProductName", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.Fill(ds, "CompanyInformation");

                pinforpt = new ProductInfoCrystalReport();
                pinforpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = pinforpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void productname(int pid)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                ProductinfoDS ds = new ProductinfoDS();
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation where ProductCode='" + pid + "' order by ProductName", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where ProductCode='" + pid + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.Fill(ds, "CompanyInformation");

                pinforpt = new ProductInfoCrystalReport();
                pinforpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = pinforpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void companyname(string company, string pname)
        {
            try
            {
                if (company != null && pname == null)
                {
                    db.connect();
                    SqlDataAdapter da;
                    ProductinfoDS ds = new ProductinfoDS();
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation where ManufactureCompany='" + company + "' order by ProductName ", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where ManufactureCompany='" + company + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "ProductInformation");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.Fill(ds, "CompanyInformation");

                    pinforpt = new ProductInfoCrystalReport();
                    pinforpt.SetDataSource(ds);

                    crystalReportViewer1.ReportSource = pinforpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    string newpname = "";
                    if ((pname).Contains("'"))
                        newpname = pname.Replace("'", "''");
                    else
                        newpname = pname.Trim();

                    string Proname = newpname.Split(',')[0];
                    string compan = newpname.Split(',')[1];
                    string unit = newpname.Split(',')[2];
                    db.connect();
                    SqlDataAdapter da;
                    ProductinfoDS ds = new ProductinfoDS();
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' order by ProductName", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "ProductInformation");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.Fill(ds, "CompanyInformation");

                    pinforpt = new ProductInfoCrystalReport();
                    pinforpt.SetDataSource(ds);

                    crystalReportViewer1.ReportSource = pinforpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void companyname(string company, int GroupMasterId, string pname, ProductInfoCrystalReport pinforpt)
        {
            try
            {
                if (company != null && pname == null && GroupMasterId == -1)
                {
                    db.connect();
                    SqlDataAdapter da;
                    ProductinfoDS ds = new ProductinfoDS();
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation where ManufactureCompany='" + company + "' order by ProductName ", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where ManufactureCompany='" + company + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "ProductInformation");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.Fill(ds, "CompanyInformation");
                    pinforpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = pinforpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                    if (company == null && pname != null && GroupMasterId == -1)
                    {
                        string newpname = "";
                        if ((pname).Contains("'"))
                            newpname = pname.Replace("'", "''");
                        else
                            newpname = pname.Trim();

                        string Proname = newpname.Split(',')[0];
                        string compan = newpname.Split(',')[1];
                        string unit = newpname.Split(',')[2];
                        db.connect();
                        SqlDataAdapter da;
                        ProductinfoDS ds = new ProductinfoDS();
                        if (CommonMethod.commProduct == true)
                            da = new SqlDataAdapter("Select * from ProductInformation where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' order by ProductName", db.Connection);
                        else
                            da = new SqlDataAdapter("Select * from ProductInformation where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "ProductInformation");

                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        pinforpt.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = pinforpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    else if (company == null && pname == null && GroupMasterId != -1)
                    {
                        db.connect();
                        SqlDataAdapter da;
                        ProductinfoDS ds = new ProductinfoDS();
                        if (CommonMethod.commProduct == true)
                            da = new SqlDataAdapter("Select * from ProductInformation where GroupMasterId='" + GroupMasterId + "' order by ProductName ", db.Connection);
                        else
                            da = new SqlDataAdapter("Select * from ProductInformation where GroupMasterId='" + GroupMasterId + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "ProductInformation");

                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        pinforpt.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = pinforpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        void ExcelCompany(string company, int GroupMasterId, string pname)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (company != null && pname == null && GroupMasterId == -1)
                {
                    db.connect();
                    ds = new DataSet();
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where ManufactureCompany='" + company + "' order by ProductName", db.Connection);
                    else
                        da = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where ManufactureCompany='" + company + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds);
                    db.CloseConnection();
                }
                else
                {
                    if (company == null && pname != null && GroupMasterId == -1)
                    {
                        string newpname = "";
                        if ((pname).Contains("'"))
                            newpname = pname.Replace("'", "''");
                        else
                            newpname = pname.Trim();
                        string Proname = newpname.Split(',')[0];
                        string compan = newpname.Split(',')[1];
                        string unit = newpname.Split(',')[2];
                        db.connect();
                        if (CommonMethod.commProduct == true)
                            da = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' order by p.ProductName", db.Connection);
                        else
                            da = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' and p.compid=" + CommonMethod.CompId + " order by p.ProductName", db.Connection);
                        ds = new DataSet();
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds);
                        db.CloseConnection();
                    }
                    else if (company == null && pname == null && GroupMasterId != -1)
                    {
                        db.connect();
                        ProductinfoDS ds = new ProductinfoDS();
                        if (CommonMethod.commProduct == true)
                            da = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where GroupMasterId='" + GroupMasterId + "' order by ProductName", db.Connection);
                        else
                            da = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where GroupMasterId='" + GroupMasterId + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds);
                        db.CloseConnection();
                    }
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getdata(int groupId)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                ProductinfoDS ds = new ProductinfoDS();

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation where GroupMasterId='" + groupId + "' order by ProductName ", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where GroupMasterId='" + groupId + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.Fill(ds, "CompanyInformation");

                pinforpt = new ProductInfoCrystalReport();
                pinforpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = pinforpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
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