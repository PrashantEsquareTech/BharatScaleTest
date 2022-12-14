using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCashBook : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmCashBook()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getDetails();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                getDetails();
            }
            catch (Exception)
            { }
        }

        private void getDetails()
        {
            try
            {
                GVDr.Rows.Clear();
                GVCr.Rows.Clear();

                progressBar1.Visible = true;
                int minval = 0, maxval = 0;
                progressBar1.Minimum = minval;
                progressBar1.Maximum = maxval;

                string fromDate = cm.ValidFromDate(dtpfromdate.Value.Date), toDate = cm.ValidToDate(dtpToDate.Value.Date);
                //New Logic
                //Left Side Dr
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCashBook", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Dr";
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    SqlDataReader reader = command.ExecuteReader();
                    int l = 0;

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    maxval = dt.Rows.Count;
                    progressBar1.Maximum = maxval;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        progressBar1.Value = minval + 1;
                        GVDr.Rows.Add();
                        GVDr.Rows[l].Cells["DrParticulars"].Value = dt.Rows[i][1].ToString();
                        GVDr.Rows[l].Cells["DrAmount"].Value = Convert.ToString(dt.Rows[i][2].ToString());
                        l++;
                    }
                    db.CloseConnection();
                    dt.Dispose();
                    reader.Dispose();
                }
                catch (Exception)
                {
                    db.CloseConnection();
                }

                //Right Side Cr
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCashBook", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Cr";
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    SqlDataReader reader = command.ExecuteReader();
                    int l = 0;

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    progressBar1.Maximum = maxval + dt.Rows.Count;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        progressBar1.Value = minval + 1;
                        GVCr.Rows.Add();
                        GVCr.Rows[l].Cells["CrParticulars"].Value = dt.Rows[i][1].ToString();
                        GVCr.Rows[l].Cells["CrAmount"].Value = Convert.ToString(dt.Rows[i][2].ToString());
                        l++;
                    }

                    db.CloseConnection();
                    dt.Dispose();
                    reader.Dispose();
                }
                catch (Exception)
                {
                    db.CloseConnection();
                }
                //Total
                decimal DrTotal = 0;
                for (int i = 0; i < GVDr.Rows.Count; i++)
                {
                    DrTotal = DrTotal + Convert.ToDecimal(GVDr.Rows[i].Cells["DrAmount"].Value);
                }
                decimal CrTotal = 0;
                for (int i = 0; i < GVCr.Rows.Count; i++)
                {
                    CrTotal = CrTotal + Convert.ToDecimal(GVCr.Rows[i].Cells["CrAmount"].Value);
                }
                progressBar1.Visible = false;
                lbltotalDrAmount.Text = Convert.ToString(Math.Round(DrTotal, 2, MidpointRounding.AwayFromZero));
                lblTotalCrAmount.Text = Convert.ToString(Math.Round(CrTotal, 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            RptCashBook CashBookRpt = new RptCashBook(dtpfromdate.Value, dtpToDate.Value);
            CashBookRpt.ShowDialog();
            CashBookRpt.Dispose();
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

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpToDate;
            }
            catch (Exception)
            { }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void GVDr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() == "Cash Sales" && Convert.ToDecimal(GVDr.Rows[e.RowIndex].Cells["DrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmCustomerBillList BillList = new frmCustomerBillList(dtpfromdate.Value.Date, dtpToDate.Value.Date, "Cash");
                    BillList.ShowDialog();
                }
                else if (GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() == "Cash Receipt By Customer" && Convert.ToDecimal(GVDr.Rows[e.RowIndex].Cells["DrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    string booktype = "Cash";
                    frmCustomerLedger BillList = new frmCustomerLedger(dtpfromdate.Value.Date, dtpToDate.Value.Date, booktype);
                    BillList.ShowDialog();
                }
                else if (GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() == "Cash Receipt On Purchase Return" && Convert.ToDecimal(GVDr.Rows[e.RowIndex].Cells["DrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmPurchaseReturnList BillList = new frmPurchaseReturnList(dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    BillList.ShowDialog();
                }
                else if (GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() == "Cash Withdrawal" && Convert.ToDecimal(GVDr.Rows[e.RowIndex].Cells["DrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmBankStatement bank_state = new frmBankStatement(dtpfromdate.Value.Date, dtpToDate.Value.Date, "Withdrawals");
                    bank_state.ShowDialog();
                }
                else if (GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() != "By Purchase Return" && GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() != "To Customer Receipt" && GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString() != "To Sales" && Convert.ToDecimal(GVDr.Rows[e.RowIndex].Cells["DrAmount"].Value.ToString()) != 0)
                {
                    frmDayBook p = new frmDayBook();
                    p.Visible = false;
                    frmOtherIncome BillList = new frmOtherIncome(GVDr.Rows[e.RowIndex].Cells["DrParticulars"].Value.ToString(), dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    BillList.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void GVCr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() == "Purchase By Cash" && Convert.ToDecimal(GVCr.Rows[e.RowIndex].Cells["CrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmPurchaseOrderList BillList = new frmPurchaseOrderList(dtpfromdate.Value.Date, dtpToDate.Value.Date, "Cash");
                    BillList.ShowDialog();
                }
                else if (GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() == "Cash Paid To Supplier" && Convert.ToDecimal(GVCr.Rows[e.RowIndex].Cells["CrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    string booktype = "Cash";
                    frmSupplierLedger suppLedger = new frmSupplierLedger(dtpfromdate.Value.Date, dtpToDate.Value.Date, booktype);
                    suppLedger.ShowDialog();
                }
                else if (GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() == "Cash Deposited" && Convert.ToDecimal(GVCr.Rows[e.RowIndex].Cells["CrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmBankStatement bank_state = new frmBankStatement(dtpfromdate.Value.Date, dtpToDate.Value.Date, "Deposits");
                    bank_state.ShowDialog();
                }
                else if (GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() == "Cash Paid On Sale Return" && Convert.ToDecimal(GVCr.Rows[e.RowIndex].Cells["CrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmSaleReturnList BillList = new frmSaleReturnList(dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    BillList.ShowDialog();
                }
                else if (GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() != "Purchase By Cash" && GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() != "Cash Paid To Supplier" && GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() != "Cash Deposited" && GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString() != "Cash Paid On Sale Return" && Convert.ToDecimal(GVCr.Rows[e.RowIndex].Cells["CrAmount"].Value.ToString()) != 0)
                {
                    frmCashBook p = new frmCashBook();
                    p.Visible = false;
                    frmExpences Expenses = new frmExpences(GVCr.Rows[e.RowIndex].Cells["CrParticulars"].Value.ToString(), dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    Expenses.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }
    }
}