using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmMessageSend : Form
    {
        DbClass db = new DbClass();

        public frmMessageSend()
        {
            InitializeComponent();
            getremainingpayment();
        }

        public void getremainingpayment()
        {
            try
            {
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                CustomerRepository custrepo = new CustomerRepository();
                List<RemainingPayment> remainingpay = new List<RemainingPayment>();
                DataTable dt = db.GetTable("Select * from RemainingPayment WHERE TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    int j = 0;
                    GvRemainingpayment.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.Type == "Bill" && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                        string remainigamt = Convert.ToString(netamt - Paidamt);
                        var paydate = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        DateTime paydate1 = Convert.ToDateTime(paydate.PaymentDate);
                        if (remainigamt != "0.00" && paydate1 <= DateTime.Now)
                        {
                            int custid = Convert.ToInt32(dt.Rows[i][0].ToString());
                            GvRemainingpayment.Rows.Add();
                            var customerdata = custrepo.GetAll().Where(t => t.CustomerID == custid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            GvRemainingpayment.Rows[j].Cells["CustomerName"].Value = customerdata.CustomerName;
                            GvRemainingpayment.Rows[j].Cells["Address"].Value = customerdata.CustomeAddress;
                            GvRemainingpayment.Rows[j].Cells["ContactNo"].Value = customerdata.Landlineno;
                            GvRemainingpayment.Rows[j].Cells["RemAmount"].Value = remainigamt;
                            GvRemainingpayment.Rows[j].Cells["PaymentDate"].Value = paydate.PaymentDate;
                            GvRemainingpayment.Rows[j].Cells["Send"].Value = AIOInventorySystem.Desk.Properties.Resources.sms1;
                            GvRemainingpayment.Rows[j].Cells["Id"].Value = customerdata.CustomerID;
                            j++;
                        }
                    }
                }
                remainingpayrepo.Dispose(); custrepo.Dispose();
                dt.Dispose();
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

        private void frmMessageSend_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
    }
}