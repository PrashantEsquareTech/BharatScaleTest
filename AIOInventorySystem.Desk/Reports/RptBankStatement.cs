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
    public partial class RptBankStatement : Form
    {
        DbClass db = new DbClass();
        BankStatementCrystalReport BankStRpt;
        CommonMethod cm = new CommonMethod();
        public DateTime sdate, edate;

        public RptBankStatement()
        {
            InitializeComponent();
            bankdata();
        }

        public RptBankStatement(int bankid, string bankname)
        {
            InitializeComponent();
            loaddata(bankid, bankname);
            bankdata();
        }

        public RptBankStatement(int bankid, string bankname, DateTime fromdate, DateTime todate)
        {
            InitializeComponent();
            loaddata(bankid, bankname, fromdate, todate);
            bankdata();
        }

        public RptBankStatement(string transType, DateTime fromdate, DateTime todate)
        {
            InitializeComponent();
            loaddata(transType, fromdate, todate);
            bankdata();
        }

        public RptBankStatement(DateTime fromdate, DateTime todate, string AccountType)
        {
            InitializeComponent();
            loaddata(fromdate, todate, AccountType);
            bankdata();
        }

        public void bankdata()
        {
            try
            {
                BankRepository BankRepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = BankRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t=>t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "", BankId = 0 });
                bankdata.Reverse();
                cmbbank.DataSource = bankdata;
                cmbbank.ValueMember = "BankId";
                cmbbank.DisplayMember = "Bankname";
                BankRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loaddata(int bankid, string bankname, DateTime fromdate, DateTime todate)
        {
            try
            {
                decimal balanceamt, sumObject2 = 0, sumObject3 = 0;
                db.connect();
                DateTime opdate = fromdate.AddDays(-1);
                string tilldate = cm.ValidToDate(opdate);
                SqlDataAdapter da1 = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where BankId='" + bankid + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate<='" + tilldate + "'", db.Connection);
                DataTable dt1 = new DataTable();
                da1.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da1.Fill(dt1);
                try
                {
                    sumObject2 = Convert.ToDecimal(dt1.Compute("Sum(Withdrawals)", ""));
                }
                catch (Exception)
                { }
                try
                {
                    sumObject3 = Convert.ToDecimal(dt1.Compute("Sum(Deposits)", ""));
                }
                catch (Exception)
                { }

                balanceamt = Convert.ToDecimal(Convert.ToDecimal(sumObject3) - Convert.ToDecimal(sumObject2));
                SqlDataAdapter da;

                string toDate = cm.ValidToDate(todate), fromDate = cm.ValidFromDate(fromdate);
                DSBankStatement ds = new DSBankStatement();
                da = new SqlDataAdapter("Select * from BankStatement where BankId='" + bankid + "'  and CompId='" + CommonMethod.CompId + "' and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BankStatement");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                BankStRpt = new BankStatementCrystalReport();
                BankStRpt.SetDataSource(ds);
                BankStRpt.SetParameterValue("OpBal", balanceamt);
                crystalReportViewer1.ReportSource = BankStRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loaddata(int bankid, string bankname)
        {
            try
            {
                sdate = CommonMethod.startdate;
                edate = CommonMethod.enddate;
                decimal balanceamt = 0;
                db.connect();
                SqlDataAdapter da;
                DSBankStatement ds = new DSBankStatement();
                da = new SqlDataAdapter("Select * from BankStatement where BankId='" + bankid + "' and TransactionDate>='" + cm.ValidFromDate(sdate) + "' and TransactionDate<='" + cm.ValidToDate(edate) + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BankStatement");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                BankStRpt = new BankStatementCrystalReport();
                BankStRpt.SetDataSource(ds);
                BankStRpt.SetParameterValue("OpBal", balanceamt);
                crystalReportViewer1.ReportSource = BankStRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
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

        public void loaddata(string transType, DateTime fromdate, DateTime todate)
        {
            try
            {
                decimal balanceamt, sumObject2 = 0, sumObject3 = 0;
                db.connect();
                DateTime opdate = fromdate.AddDays(-1);
                string tilldate = cm.ValidToDate(opdate), toDate = cm.ValidToDate(todate), fromDate = cm.ValidFromDate(fromdate);
                SqlDataAdapter da1 = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where  CompId='" + CommonMethod.CompId + "'   and Transactiontype='" + transType + "' and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "' ", db.Connection);
                DataTable dt1 = new DataTable();
                da1.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da1.Fill(dt1);
                try
                {
                    sumObject2 = Convert.ToDecimal(dt1.Compute("Sum(Withdrawals)", ""));
                }
                catch (Exception)
                { }
                try
                {
                    sumObject3 = Convert.ToDecimal(dt1.Compute("Sum(Deposits)", ""));
                }
                catch (Exception)
                { }

                balanceamt = Convert.ToDecimal(Convert.ToDecimal(sumObject3) - Convert.ToDecimal(sumObject2));
                SqlDataAdapter da;
                DSBankStatement ds = new DSBankStatement();
                da = new SqlDataAdapter("Select * from BankStatement where CompId='" + CommonMethod.CompId + "' and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "' and Transactiontype='" + transType + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BankStatement");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                BankStRpt = new BankStatementCrystalReport();
                BankStRpt.SetDataSource(ds);
                BankStRpt.SetParameterValue("OpBal", balanceamt);
                crystalReportViewer1.ReportSource = BankStRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
        }

        public void loaddata(DateTime fromdate, DateTime todate, string AccountType)
        {
            try
            {
                int bId, bstId, cId;
                string Tdate, particulars, TransactionType, ChequeNo, BankName1, bankname;
                decimal withdrawals = 0, deposits = 0, balance = 0, openingbalance = 0, balanceamt2 = 0, sumObject2 = 0, sumObject3 = 0;
                DSBankStatement ds = new DSBankStatement();
                if (AccountType == "Bank Accounts")
                {
                    db.connect();
                    DateTime opdate = fromdate.AddDays(-1);
                    string tilldate = cm.ValidToDate(opdate), toDate = cm.ValidToDate(todate.Date), fromDate = cm.ValidFromDate(fromdate.Date);
                    DataTable dtnew;
                    DataTable dtbank = db.GetTable("Select BankId,BankName from BankStatement b inner join AccountingGroupMaster a on b.ACGId=a.Id where TransactionDate between '" + fromDate + "' and '" + toDate + "' and CompId='" + CommonMethod.CompId + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='Bank Accounts') group by BankId,BankName ");
                    if (dtbank.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtbank.Rows.Count; i++)
                        {
                            int bankid = Convert.ToInt32(dtbank.Rows[i]["BankId"].ToString());
                            bankname = Convert.ToString(dtbank.Rows[i][1].ToString());
                            DataTable dtbankst = db.GetTable("Select Id,TransactionDate as 'Date',Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,isnull(Withdrawals,0) as 'Withdrawals',isnull(Deposits,0) as 'Deposits',BankId,BankName,isnull(Balance,0) as 'Balance',isnull(OpeningBalance,0) as 'OpeningBalance',CompId from BankStatement where BankId='" + bankid + "' and BankName='" + bankname + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "'");
                            if (dtbankst.Rows.Count > 0)
                            {
                                try
                                {
                                    sumObject2 = sumObject2 + Convert.ToDecimal(dtbankst.Compute("Sum(Withdrawals)", ""));
                                }
                                catch (Exception)
                                { }
                                try
                                {
                                    sumObject3 = sumObject3 + Convert.ToDecimal(dtbankst.Compute("Sum(Deposits)", ""));
                                }
                                catch (Exception)
                                { }

                                balanceamt2 = Convert.ToDecimal(Convert.ToDecimal(sumObject3) - Convert.ToDecimal(sumObject2));
                                for (int j = 0; j < dtbankst.Rows.Count; j++)
                                {
                                    bId = Convert.ToInt32(dtbankst.Rows[j][0].ToString());
                                    Tdate = Convert.ToString(dtbankst.Rows[j][1].ToString());
                                    particulars = Convert.ToString(dtbankst.Rows[j][2].ToString());
                                    ChequeNo = Convert.ToString(dtbankst.Rows[j][3].ToString());
                                    TransactionType = Convert.ToString(dtbankst.Rows[j][4].ToString());
                                    if (dtbankst.Rows[j][5].ToString() == "" || dtbankst.Rows[j][5].ToString() == null)
                                        withdrawals = 0;
                                    else
                                        withdrawals = Convert.ToDecimal(dtbankst.Rows[j][5].ToString());
                                    if (dtbankst.Rows[j][6].ToString() == "" || dtbankst.Rows[j][6].ToString() == null)
                                        deposits = 0;
                                    else
                                        deposits = Convert.ToDecimal(dtbankst.Rows[j][6].ToString());
                                    bstId = Convert.ToInt32(dtbankst.Rows[j][7].ToString());
                                    BankName1 = Convert.ToString(dtbankst.Rows[j][8].ToString());
                                    balance = Convert.ToDecimal(dtbankst.Rows[j][9].ToString());
                                    openingbalance = Convert.ToDecimal(dtbankst.Rows[j][10].ToString());
                                    cId = Convert.ToInt32(dtbankst.Rows[j][11].ToString());

                                    dtnew = ds.Tables["BankStatement"];
                                    dtnew.Rows.Add(new object[] { bId, bstId, BankName1, Tdate, particulars, ChequeNo, withdrawals, deposits, balance, openingbalance, cId, TransactionType });
                                }
                            }
                        }
                    }
                    SqlDataAdapter da;
                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    BankStRpt = new BankStatementCrystalReport();
                    BankStRpt.SetDataSource(ds);
                    BankStRpt.SetParameterValue("OpBal", balanceamt2);
                    BankStRpt.SetParameterValue("Hide", "Hide");
                    crystalReportViewer1.ReportSource = BankStRpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    ds.Dispose();
                    da.Dispose();
                }
                else
                {
                    db.connect();
                    bankname = AccountType;
                    DateTime opdate = fromdate.AddDays(-1);
                    string tilldate = cm.ValidToDate(opdate), toDate = cm.ValidToDate(todate.Date), fromDate = cm.ValidFromDate(fromdate.Date);
                    sumObject2 = 0;
                    sumObject3 = 0;

                    SqlDataAdapter da1 = new SqlDataAdapter("Select b.Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement b inner join AccountingGroupMaster a on b.ACGId=a.Id where BankName='" + bankname + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate<='" + tilldate + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='Bank OD A/C' or ACGName='Secured Loans') ", db.Connection);
                    DataTable dt1 = new DataTable();
                    da1.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da1.Fill(dt1);
                    try
                    {
                        sumObject2 = Convert.ToDecimal(dt1.Compute("Sum(Withdrawals)", ""));
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        sumObject3 = Convert.ToDecimal(dt1.Compute("Sum(Deposits)", ""));
                    }
                    catch (Exception)
                    { }

                    balanceamt2 = Convert.ToDecimal(Convert.ToDecimal(sumObject3) - Convert.ToDecimal(sumObject2));
                    SqlDataAdapter da;

                    toDate = cm.ValidToDate(todate.Date);
                    fromDate = cm.ValidFromDate(fromdate.Date);

                    da = new SqlDataAdapter("Select * from BankStatement b inner join AccountingGroupMaster a on b.ACGId=a.Id where BankName='" + bankname + "' and b.CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='Bank OD A/C' or ACGName='Secured Loans') ", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "BankStatement");
                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    BankStRpt = new BankStatementCrystalReport();
                    BankStRpt.SetDataSource(ds);
                    BankStRpt.SetParameterValue("OpBal", balanceamt2);
                    BankStRpt.SetParameterValue("Hide", "Show");
                    crystalReportViewer1.ReportSource = BankStRpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    ds.Dispose();
                    da.Dispose();
                }
            }
            catch (Exception) { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                sdate = CommonMethod.startdate;
                edate = CommonMethod.enddate;
                db.connect();
                SqlDataAdapter da;
                DSReceipt ds = new DSReceipt();
                da = new SqlDataAdapter("Select * from BankStatement where BankId='" + cmbbank.SelectedValue + "' and TransactionDate>='" + cm.ValidFromDate(sdate) + "' and TransactionDate<='" + cm.ValidToDate(edate) + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BankStatement");
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                BankStRpt = new BankStatementCrystalReport();
                BankStRpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = BankStRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
    }
}