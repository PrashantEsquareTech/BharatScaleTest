using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptJournalEntry : Form
    {
        DbClass db = new DbClass();
        JournalEntryCrystalReport jerpt = new JournalEntryCrystalReport();

        public RptJournalEntry()
        {
            InitializeComponent();
        }

        public RptJournalEntry(string from, string to, int id, string flag)
        {
            InitializeComponent();
            getremainingpayment(from, to, id, flag);
        }

        public RptJournalEntry(string frmgrp, string frmac, int frmacid, string togrp, string toac, int toacid, string flag)
        {
            InitializeComponent();
            getremainingpayment(frmgrp, frmac, frmacid, togrp, toac, toacid, flag);
        }

        public void getremainingpayment(string frmgrp, string frmac, int frmacid, string togrp, string toac, int toacid, string flag)
        {
            try
            {
                SqlDataAdapter da;
                DSJournalEntry ds = new DSJournalEntry();
                if (flag == "dft")//From Date and To Date are different
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where JEDate>='" + frmgrp + "' and JEDate<='" + togrp + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "d")//From Date and To date are same or single date search
                {
                    string[] d = frmgrp.Split(' ');
                    da = new SqlDataAdapter("Select * from JournalEntry where CONVERT(VARCHAR(24),JEDate,101)=CONVERT(VARCHAR(24),'" + d[0].ToString() + "',101)   and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "fa")//From Group with From Account name
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where FromGroup='" + frmgrp + "' and FromAccountName='" + frmac + "' and FromAccountId=" + frmacid + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "f")//From group only
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where FromGroup='" + frmgrp + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "ta")//To group with To Account name
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where ToGroup='" + togrp + "' and ToAccountName='" + toac + "' and ToAccountId=" + toacid + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "t")//To Group only
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where ToGroup='" + togrp + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "ga")//GetAll
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "fata")//From Group with From Account name with To Group and To Account Name
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where FromGroup='" + frmgrp + "' and FromAccountName='" + frmac + "' and FromAccountId=" + frmacid + " and ToGroup='" + togrp + "' and ToAccountName='" + toac + "' and ToAccountId=" + toacid + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "fat")//From Group with From Account name with To Group with out To Account Name
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where FromGroup='" + frmgrp + "' and FromAccountName='" + frmac + "' and FromAccountId=" + frmacid + " and ToGroup='" + togrp + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception)
            { }
        }

        public void getremainingpayment(string from, string to, int id, string flag)
        {
            try
            {
                SqlDataAdapter da;
                DSJournalEntry ds = new DSJournalEntry();
                if (flag == "dft")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where JEDate>='" + from + "' and JEDate<='" + to + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "d")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where CONVERT(VARCHAR(24),JEDate,103)=CONVERT(VARCHAR(24),'" + from + "',103)   and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "f")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where FromGroup='" + from + "' and FromAccountName='" + to + "' and FromAccountId=" + id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "fg")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where FromGroup='" + from + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "t")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where ToGroup='" + from + "' and ToAccountName='" + to + "' and ToAccountId=" + id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "tg")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where ToGroup='" + from + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
                }
                else if (flag == "ga")
                {
                    da = new SqlDataAdapter("Select * from JournalEntry where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "JournalEntry");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    jerpt = new JournalEntryCrystalReport();
                    jerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = jerpt;
                    crystalReportViewer1.Refresh();
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
    }
}
