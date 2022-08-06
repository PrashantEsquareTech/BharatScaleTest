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
    public partial class RptProprietorLedger : Form
    {
        DbClass db = new DbClass();
        ProprietorLedgderRpt receiptrpt;
        public string totalremainingamount;
        public decimal TNetAmt = 0, TPaidAmt = 0;

        public RptProprietorLedger()
        {
            InitializeComponent();
            customerdata();
        }

        public RptProprietorLedger(int custid, string customername)
        {
            InitializeComponent();
            customerdata();
            loaddata(custid, customername);
        }

        public RptProprietorLedger(string proLedName, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            customerdata();
            loaddata(proLedName, fdate, tdate);
        }

        public void customerdata()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ProprietorName).ToList();
                customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                customerdata.Reverse();
                cmbProprietor.DataSource = customerdata;
                cmbProprietor.ValueMember = "ProprietorId";
                cmbProprietor.DisplayMember = "ProprietorName";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loaddata(int custid, string customername)
        {
            try
            {
                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(custid) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                TNetAmt = Convert.ToDecimal(remainingamt.Where(t => t.Type != "Receipt").Sum(t => t.NetAmount));
                TPaidAmt = Convert.ToDecimal(remainingamt.Sum(t => t.PaidAmount));
                if (remainingamt != null)
                    totalremainingamount = Convert.ToString(TNetAmt - TPaidAmt);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da;
                DSProprietorLedger ds = new DSProprietorLedger();
                da = new SqlDataAdapter("Select * from ProprietorLedger where ProprietorId='" + custid + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProprietorLedger");

                receiptrpt = new ProprietorLedgderRpt();
                receiptrpt.SetDataSource(ds);
                receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                crystalReportViewer1.ReportSource = receiptrpt;
                db.CloseConnection();
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                loaddata(Convert.ToInt32(cmbProprietor.SelectedValue), cmbProprietor.Text);
            }
            catch (Exception)
            { }
        }

        public void loaddata(string proLedName, DateTime fdate, DateTime tdate)
        {
            try
            {
                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorName == proLedName && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                TNetAmt = Convert.ToDecimal(remainingamt.Where(t => t.Type != "Receipt").Sum(t => t.NetAmount));
                TPaidAmt = Convert.ToDecimal(remainingamt.Sum(t => t.PaidAmount));
                if (remainingamt != null)
                    totalremainingamount = Convert.ToString(TNetAmt - TPaidAmt);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da;
                CommonMethod com = new CommonMethod();
                DSProprietorLedger ds = new DSProprietorLedger();

                da = new SqlDataAdapter("Select * from ProprietorLedger where ProprietorName='" + proLedName + "' and CreateDate >= '" + com.ValidFromDate(fdate.Date) + "' and CreateDate <= '" + com.ValidToDate(tdate.Date) + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProprietorLedger");
                receiptrpt = new ProprietorLedgderRpt();
                receiptrpt.SetDataSource(ds);
                receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                crystalReportViewer1.ReportSource = receiptrpt;
                db.CloseConnection();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
    }
}