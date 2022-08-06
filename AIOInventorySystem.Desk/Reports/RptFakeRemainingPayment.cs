using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptFakeRemainingPayment : Form
    {
        DbClass db = new DbClass();
        RemainingPaymentCrystalReport1 rempayrpt = new RemainingPaymentCrystalReport1();

        public RptFakeRemainingPayment()
        {
            InitializeComponent();
            getremainingpayment();
        }

        public void getremainingpayment()
        {
            try
            {
                DSRemainingPayment ds = new DSRemainingPayment();
                DataTable dt = new DataTable();
                CustomerRepository custrepo = new CustomerRepository();
                DataTable dt1 = db.GetTable("Select Distinct(CustomerId) from FakeRemainingPayment where TransactionYear = '" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    int j = 1;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                        int custid = Convert.ToInt32(dt1.Rows[i]["CustomerId"].ToString());
                        var customerdata = custrepo.GetAll().Where(t => t.CustomerID == custid && t.CompId == CommonMethod.CompId).FirstOrDefault();

                        List<FakeRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt1.Rows[i]["CustomerId"].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        decimal netamt = Convert.ToDecimal(remainingamt.Where(t => t.Type != "Receipt").Sum(t => t.NetAmt));
                        decimal Paidamt = Convert.ToDecimal(remainingamt.Sum(t => t.PaidAmt));
                        string remainigamt = Convert.ToString(netamt - Paidamt);
                        if (remainigamt != "0.00")
                        {
                            string cusatname = customerdata.CustomerName;
                            string cusataddress = customerdata.CustomeAddress;
                            string cno = customerdata.Mobileno;
                            dt = ds.Tables["RemainingPayment"];
                            dt.Rows.Add(new object[] { j, cusatname, cusataddress, cno, remainigamt });
                            j++;
                        }
                        netamt = 0; Paidamt = 0; remainigamt = "0";
                        remainingpayrepo.Dispose();
                    }
                }

                rempayrpt = new RemainingPaymentCrystalReport1();
                rempayrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rempayrpt;
                db.CloseConnection();
                custrepo.Dispose();
                ds.Dispose();
                dt.Dispose();
                dt1.Dispose();
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