using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmTradingnPLAC : Form
    {
        DbClass db = new DbClass();
        string vatny = "A";
        CommonMethod cm = new CommonMethod();
        DataTable dt1 = new DataTable();

        public frmTradingnPLAC()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
        }

        public frmTradingnPLAC(DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            BalShtToTotalPLAC(fdate, tdate);
        }
        public frmTradingnPLAC(string vat)
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

        public void TPFAC(string vat)
        {
            try
            {
                GVProfitNLossDr.Rows.Clear();
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPTradingnProfitLossAC", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 't';
                    command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = vat;
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = cm.ValidFromDate(dtpfromdate.Value.Date);
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = cm.ValidToDate(dtpToDate.Value.Date);
                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.CommandTimeout = 1000;
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dt1 = dt.Copy();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GVProfitNLossDr.Rows.Add();
                        if (Convert.ToString(dt.Rows[i][2].ToString()) == "")
                            GVProfitNLossDr.Rows[i].Cells[1].Value = Convert.ToString(dt.Rows[i][1].ToString());
                        GVProfitNLossDr.Rows[i].Cells[2].Value = Convert.ToString(dt.Rows[i][2].ToString());
                        GVProfitNLossDr.Rows[i].Cells[3].Value = Convert.ToString(dt.Rows[i][3].ToString());
                        GVProfitNLossDr.Rows[i].Cells[4].Value = Convert.ToString(dt.Rows[i][4].ToString());
                        if (Convert.ToString(dt.Rows[i][6].ToString()) == "")
                            GVProfitNLossDr.Rows[i].Cells[5].Value = Convert.ToString(dt.Rows[i][5].ToString());
                        GVProfitNLossDr.Rows[i].Cells[6].Value = Convert.ToString(dt.Rows[i][6].ToString());
                        GVProfitNLossDr.Rows[i].Cells[7].Value = Convert.ToString(dt.Rows[i][7].ToString());
                        GVProfitNLossDr.Rows[i].Cells[8].Value = Convert.ToString(dt.Rows[i][8].ToString());
                    }
                    decimal NetPL = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 2][4].ToString());
                    if (NetPL < 0)
                    {
                        lblpl.Text = "Net Loss:";
                        lblpfls.Text = Convert.ToString(Math.Round(NetPL, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        lblpl.Text = "Net Profit:";
                        lblpfls.Text = Convert.ToString(Math.Round(NetPL, 2, MidpointRounding.AwayFromZero));
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
                    TPFAC(vatny);
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
                    RptTradingnPLAC TPLACrpt = new RptTradingnPLAC(vatny, dtpfromdate.Value, dtpToDate.Value);
                    TPLACrpt.ShowDialog();
                    TPLACrpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }
        public void BalShtToTotalPLAC(DateTime fdate, DateTime tdate)
        {
            try
            {
                dtpfromdate.Value = Convert.ToDateTime(fdate.Date);
                dtpToDate.Value = Convert.ToDateTime(tdate.Date);
                try
                {
                    TPFAC(vatny);
                }
                catch (Exception) { }

            }
            catch (Exception) { }
        }

        private void frmTradingnPLAC_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void GVProfitNLossDr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string cellValue = "", ACCName = "";
                if (e.ColumnIndex == 2)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        cellValue = GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString();
                        if (cellValue == dt1.Rows[i][2].ToString())
                            ACCName = dt1.Rows[i][1].ToString();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() != "" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0 && ACCName == "Direct Expenses")
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmExpences expences = new frmExpences(dtpfromdate.Value.Date, dtpToDate.Value.Date, cellValue, ACCName);
                        expences.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() != "" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0 && ACCName == "Indirect Expenses")
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmExpences expences = new frmExpences(dtpfromdate.Value.Date, dtpToDate.Value.Date, cellValue, ACCName);
                        expences.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Scrap Purchases Battery" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmPurchaseOrderList porderList = new frmPurchaseOrderList("Scrap Purchases Battery", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        porderList.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "OMS Purchase @" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmPurchaseOrderList porderList = new frmPurchaseOrderList("OMS Purchase @", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        porderList.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Purchase (URD)" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmPurchaseOrderList porderList = new frmPurchaseOrderList("Purchase (URD)", dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        porderList.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Purchase @ 0.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Purchase @ 5.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Purchase @ 12.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Purchase @ 18.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString() == "Purchase @ 28.00" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[3].Value.ToString()) != 0)
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmPurchaseOrderList porderList = new frmPurchaseOrderList(GVProfitNLossDr.Rows[e.RowIndex].Cells[2].Value.ToString(), dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        porderList.ShowDialog();
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
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0 && ACCName == "Indirect Incomes")
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmOtherIncome otherincome = new frmOtherIncome(cellValue, dtpfromdate.Value.Date, dtpToDate.Value.Date, ACCName);
                        otherincome.ShowDialog();
                    }
                    if (GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Sale @ 0.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Sale @ 5.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Sale @ 12.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Sale @ 18.00" || GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString() == "Sale @ 28.00" && Convert.ToDecimal(GVProfitNLossDr.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0)
                    {
                        frmTradingnPLAC TPLAC = new frmTradingnPLAC();
                        TPLAC.Visible = false;
                        frmCustomerBillList billlist = new frmCustomerBillList(GVProfitNLossDr.Rows[e.RowIndex].Cells[6].Value.ToString(), dtpfromdate.Value.Date, dtpToDate.Value.Date);
                        billlist.ShowDialog();
                    }
                }
            }
            catch (Exception) { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpToDate;
            }
            catch (Exception) { }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception) { }
        }
    }
}