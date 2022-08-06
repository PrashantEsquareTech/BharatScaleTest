using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmFinalBalanceSheet : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        DataTable dt1 = new DataTable();
        public int CustomerId, SupplierId;
        public string Customername, Suppliername, vatny = "A";
        public decimal SNetAmt = 0, CNetAmt = 0, SPaidAmt = 0, CPaidAmt = 0, SRemAmt = 0, CRemAmt = 0, CActAmt = 0, SActAmt = 0;

        public frmFinalBalanceSheet()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
        }

        public frmFinalBalanceSheet(string vat)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            vatny = vat;
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

        public void BalanceSheet(string vat)
        {
            try
            {
                GVProfitNLossDr.Rows.Clear();
                progressBar1.Visible = true;
                int minval = 0, maxval = 0;
                progressBar1.Minimum = minval;
                progressBar1.Maximum = maxval;
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPNewBalanceSheet", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = vat;
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = cm.ValidFromDate(dtpfromdate.Value.Date);
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = cm.ValidToDate(dtpToDate.Value.Date);
                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.CommandTimeout = 1000;
                    SqlDataReader reader = command.ExecuteReader();
                    int l = 0;

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dt1 = dt.Copy();
                    maxval = dt.Rows.Count;
                    progressBar1.Maximum = maxval;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        progressBar1.Value = i;
                        GVProfitNLossDr.Rows.Add();
                        if (Convert.ToString(dt.Rows[i][2].ToString()) == "")
                            GVProfitNLossDr.Rows[l].Cells[1].Value = Convert.ToString(dt.Rows[i][1].ToString());
                        GVProfitNLossDr.Rows[l].Cells[2].Value = Convert.ToString(dt.Rows[i][2].ToString());
                        GVProfitNLossDr.Rows[l].Cells[3].Value = Convert.ToString(dt.Rows[i][3].ToString());
                        GVProfitNLossDr.Rows[l].Cells[4].Value = Convert.ToString(dt.Rows[i][4].ToString());
                        if (Convert.ToString(dt.Rows[i][6].ToString()) == "")
                            GVProfitNLossDr.Rows[l].Cells[5].Value = Convert.ToString(dt.Rows[i][5].ToString());
                        GVProfitNLossDr.Rows[l].Cells[6].Value = Convert.ToString(dt.Rows[i][6].ToString());
                        GVProfitNLossDr.Rows[l].Cells[7].Value = Convert.ToString(dt.Rows[i][7].ToString());
                        GVProfitNLossDr.Rows[l].Cells[8].Value = Convert.ToString(dt.Rows[i][8].ToString());

                        l++;
                    }
                    db.CloseConnection();
                    dt.Dispose();
                    reader.Dispose();

                    progressBar1.Visible = false;
                }
                catch (Exception)
                {
                    db.CloseConnection();
                }
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value < dtpToDate.Value)
                {
                    GVProfitNLossDr.Columns[1].Width = 120;
                    GVProfitNLossDr.Columns[2].Width = 150;
                    GVProfitNLossDr.Columns[3].Width = 100;
                    GVProfitNLossDr.Columns[4].Width = 100;
                    GVProfitNLossDr.Columns[5].Width = 120;
                    GVProfitNLossDr.Columns[6].Width = 150;
                    GVProfitNLossDr.Columns[7].Width = 100;
                    GVProfitNLossDr.Columns[8].Width = 100;
                    BalanceSheet(vatny);
                }
                else
                {
                    MessageBox.Show("To Date Should greater than from date.", "Warning");
                    this.ActiveControl = dtpfromdate;
                }
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value < dtpToDate.Value)
                {
                    RptFinalBalanceSheet BalSheetrpt = new RptFinalBalanceSheet(vatny, dtpfromdate.Value, dtpToDate.Value);
                    BalSheetrpt.ShowDialog();
                    BalSheetrpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void frmFinalBalanceSheet_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
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

        private void GVProfitNLossDr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string balShtString = "", newProLedName = "", ACCName = "", cellValue = "";
                if (e.ColumnIndex == 1)
                {
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[1].Value.ToString() == "Profit & Loss A/C" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[4].Value.ToString()) != 0)
                    {
                        frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                        f.Visible = false;
                        frmTradingnPLAC TotalPLAC = new frmTradingnPLAC(dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        TotalPLAC.ShowDialog();
                    }
                }
                if (e.ColumnIndex == 2)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        cellValue = GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString();
                        if (cellValue == dt1.Rows[i][2].ToString())
                            ACCName = dt1.Rows[i][1].ToString();
                    }
                    string proLedName = GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if (proLedName.Contains("."))
                    {
                        string pName = proLedName;
                        balShtString = pName.Split('.')[0];
                        newProLedName = pName.Split('.')[1];
                        newProLedName = newProLedName.Trim();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() != "" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0 && balShtString == "Capital A/C To Mr")
                    {
                        frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                        f.Visible = false;
                        frmProprietorLedger proledger = new frmProprietorLedger(newProLedName, dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        proledger.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() != "" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
                    {
                        cellValue = GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString();
                        try
                        {
                            ExpenceRepository expencerepo = new ExpenceRepository();
                            var ExpenceData = expencerepo.GetAll().Where(t => t.ExpenceName == cellValue && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (ExpenceData != null)
                            {
                                frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                                f.Visible = false;
                                frmExpences expences = new frmExpences(dtpfromdate.Value.Date, dtpToDate.Value.Date, cellValue, ACCName);
                                expences.ShowDialog();
                            }
                            else
                            {
                                BankStatementRepository bankstRepo = new BankStatementRepository();
                                var bankstData = bankstRepo.GetAll().Where(t => t.Bankname == cellValue && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (bankstData != null)
                                {
                                    frmBankStatement bankst = new frmBankStatement(cellValue, dtpfromdate.Value.Date, dtpToDate.Value.Date);
                                    bankst.ShowDialog();
                                }
                            }
                        }
                        catch (Exception) { }
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Sundry Creditors" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
                    {
                        panel1.Visible = true;
                        SuppNCustCreditors();
                    }
                }
                if (e.ColumnIndex == 6)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        cellValue = GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString();
                        if (cellValue == dt1.Rows[i][6].ToString())
                            ACCName = dt1.Rows[i][5].ToString();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Bank Accounts" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0)
                    {
                        frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                        f.Visible = false;
                        frmBankStatement bankst = new frmBankStatement("Bank Accounts", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        bankst.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0)
                    {
                        ExpenceRepository expencerepo = new ExpenceRepository();
                        var ExpenceData = expencerepo.GetAll().Where(t => t.ExpenceName == cellValue && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (ExpenceData != null)
                        {
                            frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                            f.Visible = false;
                            frmExpences expences = new frmExpences(dtpfromdate.Value.Date, dtpToDate.Value.Date, cellValue, ACCName);
                            expences.ShowDialog();
                        }
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Cash-in-hand" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0)
                    {
                        frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                        f.Visible = false;
                        frmCounterCash counterCash = new frmCounterCash("Cash-in-hand", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        counterCash.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Sundry Debtors" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0)
                    {
                        panel2.Visible = true;
                        SuppNCustDebitors();
                    }
                }
                dt1.Dispose();
            }
            catch (Exception) { }
        }

        public void SuppNCustCreditors()
        {
            try
            {
                DataTable dtNetAmt1; DataTable dtNetAmt; DataTable dtPaidAmt; DataTable dtPaidAmt1;
                SActAmt = 0; CActAmt = 0; SRemAmt = 0; CRemAmt = 0; SNetAmt = 0; CNetAmt = 0; SPaidAmt = 0; CPaidAmt = 0;
                DataTable dt1 = db.GetTable("select distinct SupplierId,SupplierName from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        SupplierId = Convert.ToInt32(dt1.Rows[j][0].ToString());
                        Suppliername = Convert.ToString(dt1.Rows[j][1].ToString());
                        dtNetAmt = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) from SupplierRemainingPayment where SupplierId='" + SupplierId + "' and Suppliername='" + Suppliername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                        if (dtNetAmt.Rows.Count > 0)
                            SNetAmt = Convert.ToDecimal(dtNetAmt.Rows[0][0].ToString());
                        dtPaidAmt = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) from SupplierRemainingPayment where SupplierId='" + SupplierId + "' and Suppliername='" + Suppliername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtPaidAmt.Rows.Count > 0)
                            SPaidAmt = Convert.ToDecimal(dtPaidAmt.Rows[0][0].ToString());
                        SRemAmt = SNetAmt - SPaidAmt;
                        if (SRemAmt > 0)
                            SActAmt = SActAmt + SRemAmt;
                    }
                }
                DataTable dt = db.GetTable("select distinct CustomerId,CustomerName from RemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CustomerId = Convert.ToInt32(dt.Rows[i][0].ToString());
                        Customername = Convert.ToString(dt.Rows[i][1].ToString());
                        dtNetAmt1 = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                        if (dtNetAmt1.Rows.Count > 0)
                            CNetAmt = Convert.ToDecimal(dtNetAmt1.Rows[0][0].ToString());
                        dtPaidAmt1 = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtPaidAmt1.Rows.Count > 0)
                            CPaidAmt = Convert.ToDecimal(dtPaidAmt1.Rows[0][0].ToString());
                        CRemAmt = CNetAmt - CPaidAmt;
                        if (CRemAmt < 0)
                            CActAmt = CActAmt + CRemAmt;
                    }
                }
                GVCustNSuppCreditors.Rows.Clear();
                GVCustNSuppCreditors.Rows.Add();
                GVCustNSuppCreditors.Rows[0].Cells[0].Value = "Supplier Creditors";
                GVCustNSuppCreditors.Rows[0].Cells[1].Value = SActAmt;
                GVCustNSuppCreditors.Rows.Add();
                GVCustNSuppCreditors.Rows[1].Cells[0].Value = "Customer Creditors";
                GVCustNSuppCreditors.Rows[1].Cells[1].Value = CActAmt;
                dt.Dispose();
                dt1.Dispose();
            }
            catch (Exception) { }
        }

        public void SuppNCustDebitors()
        {
            try
            {
                DataTable dtNetAmt1; DataTable dtNetAmt; DataTable dtPaidAmt; DataTable dtPaidAmt1;
                SActAmt = 0; CActAmt = 0; SRemAmt = 0; CRemAmt = 0; SNetAmt = 0; CNetAmt = 0; SPaidAmt = 0; CPaidAmt = 0;
                DataTable dt1 = db.GetTable("select distinct SupplierId,SupplierName from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        SupplierId = Convert.ToInt32(dt1.Rows[j][0].ToString());
                        Suppliername = Convert.ToString(dt1.Rows[j][1].ToString());
                        dtNetAmt = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) from SupplierRemainingPayment where SupplierId='" + SupplierId + "' and Suppliername='" + Suppliername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                        if (dtNetAmt.Rows.Count > 0)
                            SNetAmt = Convert.ToDecimal(dtNetAmt.Rows[0][0].ToString());
                        dtPaidAmt = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) from SupplierRemainingPayment where SupplierId='" + SupplierId + "' and Suppliername='" + Suppliername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtPaidAmt.Rows.Count > 0)
                            SPaidAmt = Convert.ToDecimal(dtPaidAmt.Rows[0][0].ToString());
                        SRemAmt = SNetAmt - SPaidAmt;
                        if (SRemAmt > 0)
                            SActAmt = SActAmt + SRemAmt;
                    }
                }
                DataTable dt = db.GetTable("select distinct CustomerId,CustomerName from RemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CustomerId = Convert.ToInt32(dt.Rows[i][0].ToString());
                        Customername = Convert.ToString(dt.Rows[i][1].ToString());
                        dtNetAmt1 = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                        if (dtNetAmt1.Rows.Count > 0)
                            CNetAmt = Convert.ToDecimal(dtNetAmt1.Rows[0][0].ToString());
                        dtPaidAmt1 = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtpToDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtPaidAmt1.Rows.Count > 0)
                            CPaidAmt = Convert.ToDecimal(dtPaidAmt1.Rows[0][0].ToString());
                        CRemAmt = CNetAmt - CPaidAmt;
                        if (CRemAmt > 0)
                            CActAmt = CActAmt + CRemAmt;
                    }
                }
                GVSuppNCustDebitors.Rows.Clear();
                GVSuppNCustDebitors.Rows.Add();
                GVSuppNCustDebitors.Rows[0].Cells[0].Value = "Customer Debtors";
                GVSuppNCustDebitors.Rows[0].Cells[1].Value = CActAmt;
                GVSuppNCustDebitors.Rows.Add();
                GVSuppNCustDebitors.Rows[1].Cells[0].Value = "Supplier Debtors";
                GVSuppNCustDebitors.Rows[1].Cells[1].Value = SActAmt;
                dt.Dispose();
                dt1.Dispose();
            }
            catch (Exception) { }
        }

        private void GVCustNSuppCreditors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GVCustNSuppCreditors.Rows[e.RowIndex].Cells[0].Value.ToString() == "Supplier Creditors" && Convert.ToDecimal(GVCustNSuppCreditors.Rows[e.RowIndex].Cells[1].Value.ToString()) != 0)
                {
                    frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                    f.Visible = false;
                    frmSupplierLedger suppLedger = new frmSupplierLedger("Supplier Creditors", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    suppLedger.ShowDialog();
                }
                if (GVCustNSuppCreditors.Rows[e.RowIndex].Cells[0].Value.ToString() == "Customer Creditors" && Convert.ToDecimal(GVCustNSuppCreditors.Rows[e.RowIndex].Cells[1].Value.ToString()) != 0)
                {
                    frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                    f.Visible = false;
                    frmCustomerLedger custLedger = new frmCustomerLedger("Customer Creditors", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    custLedger.ShowDialog();
                }
                panel1.Visible = false;
            }
            catch (Exception) { }
        }

        private void GVSuppNCustDebitors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GVSuppNCustDebitors.Rows[e.RowIndex].Cells[0].Value.ToString() == "Supplier Debtors" && Convert.ToDecimal(GVSuppNCustDebitors.Rows[e.RowIndex].Cells[1].Value.ToString()) != 0)
                {
                    frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                    f.Visible = false;
                    frmSupplierLedger suppLedger = new frmSupplierLedger("Supplier Debtors", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    suppLedger.ShowDialog();
                }
                if (GVSuppNCustDebitors.Rows[e.RowIndex].Cells[0].Value.ToString() == "Customer Debtors" && Convert.ToDecimal(GVSuppNCustDebitors.Rows[e.RowIndex].Cells[1].Value.ToString()) != 0)
                {
                    frmFinalBalanceSheet f = new frmFinalBalanceSheet();
                    f.Visible = false;
                    frmCustomerLedger custLedger = new frmCustomerLedger("Customer Debtors", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                    custLedger.ShowDialog();
                }
                panel2.Visible = false;
            }
            catch (Exception) { }
        }
    }
}