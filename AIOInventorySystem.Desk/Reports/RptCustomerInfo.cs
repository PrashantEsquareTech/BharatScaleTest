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
    public partial class RptCustomerInfo : Form
    {
        DbClass db = new DbClass();
        CustomerInfoCrystalReport1 custinforpt = new CustomerInfoCrystalReport1();
        public decimal bonusInRs = 0, bonusInPer = 0;

        public RptCustomerInfo()
        {
            InitializeComponent();
            getdata();
        }

        public RptCustomerInfo(string custname, string mechname)
        {
            InitializeComponent();
            getdata(custname, mechname);
        }

        public void getdata(string custname, string mechname)
        {
            try
            {
                DataTable dtRecord;
                CustomerRepository custrepo = new CustomerRepository();
                string cname = "", Address = "", bonusType = "", mobileno = "", pancardno = "", adharcardno = "", gstinno = "", panAdharcardno = "";
                int customerid = 0;
                if (custname != null && mechname == null)
                {
                    db.connect();
                    SqlDataAdapter da;
                    CustomerDS ds = new CustomerDS();
                    DataTable dtCustomer = new DataTable();
                    dtCustomer = db.GetTable("Select CustomerName,CustomeAddress,Mobileno,Panno,Adharcardno,BuyerTinNo,CustomerId,BonusIn,BonusInRsNPer from CustomerInformation  where CompId='" + CommonMethod.CompId + "' and CustomerName='" + custname + "'");
                    if (dtCustomer.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCustomer.Rows.Count; i++)
                        {
                            cname = Convert.ToString(dtCustomer.Rows[i]["CustomerName"].ToString());
                            Address = Convert.ToString(dtCustomer.Rows[i]["CustomeAddress"].ToString());
                            mobileno = Convert.ToString(dtCustomer.Rows[i]["Mobileno"].ToString());
                            pancardno = Convert.ToString(dtCustomer.Rows[i]["Panno"].ToString());
                            adharcardno = Convert.ToString(dtCustomer.Rows[i]["Adharcardno"].ToString());
                            if (pancardno != "" && adharcardno != "")
                                panAdharcardno = pancardno + " / " + adharcardno;
                            else if (pancardno != "")
                                panAdharcardno = pancardno;
                            else if (adharcardno != "")
                                panAdharcardno = adharcardno;
                            gstinno = Convert.ToString(dtCustomer.Rows[i]["BuyerTinNo"].ToString());
                            customerid = Convert.ToInt32(dtCustomer.Rows[i]["CustomerId"].ToString());
                            bonusType = Convert.ToString(dtCustomer.Rows[i]["BonusIn"].ToString());
                            bonusInPer = Convert.ToDecimal(dtCustomer.Rows[i]["BonusInRsNPer"].ToString());
                            List<CustomerInformation> bonusList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && (t.BonusIn == "Bonus In Per" || t.BonusIn == "Bonus In Rs")).ToList();
                            if (bonusList.Count != 0)
                            {
                                bonusInRs = 0;
                                CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                                List<CustomerBillMaster> clist = new List<CustomerBillMaster>();
                                clist = CustBMrepo.GetAll().Where(t1 => t1.CustomerId == Convert.ToInt32(customerid) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                                if (clist.Count > 0)
                                {
                                    foreach (var item1 in clist)
                                    {
                                        if (bonusType == "Bonus In Rs")
                                            bonusInRs = bonusInRs + Convert.ToDecimal(bonusInPer);
                                        else if (bonusType == "Bonus In Per")
                                        {
                                            decimal calbonusInper = 0;
                                            calbonusInper = Convert.ToDecimal(item1.NetAmount) * Convert.ToDecimal(bonusInPer) / 100;
                                            bonusInRs = bonusInRs + calbonusInper;
                                        }
                                    }
                                }
                            }
                            dtRecord = ds.Tables["DataTable1"];
                            dtRecord.Rows.Add(new object[6] { cname, Address, mobileno, panAdharcardno, gstinno, bonusInRs });
                        }
                    }
                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");
                    custinforpt = new CustomerInfoCrystalReport1();
                    custinforpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = custinforpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    db.connect();
                    SqlDataAdapter da;
                    CustomerDS ds = new CustomerDS();
                    DataTable dtCustomer = new DataTable();
                    dtCustomer = db.GetTable("Select CustomerName,CustomeAddress,Mobileno,Panno,Adharcardno,BuyerTinNo,CustomerId,BonusIn,BonusInRsNPer from CustomerInformation  where CompId='" + CommonMethod.CompId + "' and MistriName='" + mechname + "'");
                    if (dtCustomer.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCustomer.Rows.Count; i++)
                        {
                            cname = Convert.ToString(dtCustomer.Rows[i]["CustomerName"].ToString());
                            Address = Convert.ToString(dtCustomer.Rows[i]["CustomeAddress"].ToString());
                            mobileno = Convert.ToString(dtCustomer.Rows[i]["Mobileno"].ToString());
                            pancardno = Convert.ToString(dtCustomer.Rows[i]["Panno"].ToString());
                            adharcardno = Convert.ToString(dtCustomer.Rows[i]["Adharcardno"].ToString());
                            if (pancardno != "" && adharcardno != "")
                                panAdharcardno = pancardno + " / " + adharcardno;
                            else if (pancardno != "")
                                panAdharcardno = pancardno;
                            else if (adharcardno != "")
                                panAdharcardno = adharcardno;
                            gstinno = Convert.ToString(dtCustomer.Rows[i]["BuyerTinNo"].ToString());
                            customerid = Convert.ToInt32(dtCustomer.Rows[i]["CustomerId"].ToString());
                            bonusType = Convert.ToString(dtCustomer.Rows[i]["BonusIn"].ToString());
                            bonusInPer = Convert.ToDecimal(dtCustomer.Rows[i]["BonusInRsNPer"].ToString());
                            List<CustomerInformation> bonusList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && (t.BonusIn == "Bonus In Per" || t.BonusIn == "Bonus In Rs")).ToList();
                            if (bonusList.Count != 0)
                            {
                                bonusInRs = 0;
                                CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                                List<CustomerBillMaster> clist = new List<CustomerBillMaster>();
                                clist = CustBMrepo.GetAll().Where(t1 => t1.CustomerId == Convert.ToInt32(customerid) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                                if (clist.Count > 0)
                                {
                                    foreach (var item1 in clist)
                                    {
                                        if (bonusType == "Bonus In Rs")
                                            bonusInRs = bonusInRs + Convert.ToDecimal(bonusInPer);
                                        else if (bonusType == "Bonus In Per")
                                        {
                                            decimal calbonusInper = 0;
                                            calbonusInper = Convert.ToDecimal(item1.NetAmount) * Convert.ToDecimal(bonusInPer) / 100;
                                            bonusInRs = bonusInRs + calbonusInper;
                                        }
                                    }
                                }
                            }
                            dtRecord = ds.Tables["DataTable1"];
                            dtRecord.Rows.Add(new object[6] { cname, Address, mobileno, panAdharcardno, gstinno, bonusInRs });
                        }
                    }

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");
                    custinforpt = new CustomerInfoCrystalReport1();
                    custinforpt.SetDataSource(ds);

                    crystalReportViewer1.ReportSource = custinforpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getdata()
        {
            try
            {
                DataTable dtRecord;
                string cname = "", Address = "", bonusType = "", mobileno = "", pancardno = "", adharcardno = "", gstinno = "", panAdharcardno = "";
                int customerid = 0;
                CustomerRepository custrepo = new CustomerRepository();
                db.connect();
                SqlDataAdapter da;
                CustomerDS ds = new CustomerDS();
                DataTable dtCustomer = new DataTable();
                dtCustomer = db.GetTable("Select CustomerName,CustomeAddress,Mobileno,Isnull(Panno,'') as Panno,ISnull(Adharcardno,'') as Adharcardno,Isnull(BuyerTinNo,'') as BuyerTinNo,CustomerId,BonusIn,BonusInRsNPer from CustomerInformation  where CompId='" + CommonMethod.CompId + "'");
                if (dtCustomer.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCustomer.Rows.Count; i++)
                    {
                        cname = Convert.ToString(dtCustomer.Rows[i]["CustomerName"].ToString());
                        Address = Convert.ToString(dtCustomer.Rows[i]["CustomeAddress"].ToString());
                        mobileno = Convert.ToString(dtCustomer.Rows[i]["Mobileno"].ToString());
                        pancardno = Convert.ToString(dtCustomer.Rows[i]["Panno"].ToString());
                        adharcardno = Convert.ToString(dtCustomer.Rows[i]["Adharcardno"].ToString());
                        panAdharcardno = "";
                        if (pancardno != "" && adharcardno != "")
                            panAdharcardno = pancardno + " / " + adharcardno;
                        else if (pancardno != "")
                            panAdharcardno = pancardno;
                        else if (adharcardno != "")
                            panAdharcardno = adharcardno;
                        gstinno = Convert.ToString(dtCustomer.Rows[i]["BuyerTinNo"].ToString());
                        customerid = Convert.ToInt32(dtCustomer.Rows[i]["CustomerId"].ToString());
                        bonusType = Convert.ToString(dtCustomer.Rows[i]["BonusIn"].ToString());
                        try
                        {
                            bonusInPer = Convert.ToDecimal(dtCustomer.Rows[i]["BonusInRsNPer"].ToString());
                        }
                        catch (Exception)
                        { bonusInPer = 0; }

                        List<CustomerInformation> bonusList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && (t.BonusIn == "Bonus In Per" || t.BonusIn == "Bonus In Rs")).ToList();
                        if (bonusList.Count != 0)
                        {                            
                            CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                            List<CustomerBillMaster> clist = new List<CustomerBillMaster>();
                            clist = CustBMrepo.GetAll().Where(t1 => t1.CustomerId == Convert.ToInt32(customerid) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                            
                            if (clist.Count > 0)
                            {
                                bonusInRs = 0;
                                foreach (var item1 in clist)
                                {
                                    if (bonusType == "Bonus In Rs")
                                        bonusInRs = bonusInRs + Convert.ToDecimal(bonusInPer);
                                    else if (bonusType == "Bonus In Per")
                                    {
                                        decimal calbonusInper = 0;
                                        calbonusInper = Convert.ToDecimal(item1.NetAmount) * Convert.ToDecimal(bonusInPer) / 100;
                                        bonusInRs = bonusInRs + calbonusInper;
                                    }
                                }
                            }
                        }
                        dtRecord = ds.Tables["DataTable1"];
                        dtRecord.Rows.Add(new object[6] { cname, Address, mobileno, panAdharcardno, gstinno, bonusInRs });
                    }
                }
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                custinforpt = new CustomerInfoCrystalReport1();
                custinforpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = custinforpt;
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