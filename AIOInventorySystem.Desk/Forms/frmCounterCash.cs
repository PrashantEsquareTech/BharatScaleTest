using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCounterCash : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmCounterCash()
        {
            InitializeComponent();
            try
            {
                cm.changedatetimepickerrange(this);
                dtpfromdate.MaxDate = DateTime.Now.Date;
                if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                {
                    btnAddOpeningCash.Enabled = false;
                    btnAdjustment.Enabled = false;
                }
                CounterCashRepository CounterCashRepo = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashData == null)
                    btnUpdate.Enabled = false;
                else
                    btnUpdate.Enabled = true;
            }
            catch (Exception)
            { }
        }

        public frmCounterCash(string CounterCash, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            try
            {
                if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                {
                    btnAddOpeningCash.Enabled = false;
                    btnAdjustment.Enabled = false;
                }
                CounterCashRepository CounterCashRepo = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashData == null)
                    btnUpdate.Enabled = false;
                else
                    btnUpdate.Enabled = true;
                BalShtToCounterCash(CounterCash, fdate, tdate);
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value.Date != null)
                {
                    int flag = 0;
                    CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                    CounterCash CounterCashData = new CounterCash();
                    CounterCashData = CounterCashRepo1.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == dtpfromdate.Value.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (CounterCashData != null)
                    {
                        txtOpeningCash.Text = CounterCashData.OpeningCash.ToString();
                        txtClosingCash.Text = CounterCashData.ClosingCash.ToString();
                        flag = 1;
                    }
                    else
                    {
                        CounterCashRepository CounterCashRepo = new CounterCashRepository();
                        CounterCash CounterCashDataPre = new CounterCash();
                        CounterCashDataPre = CounterCashRepo.GetAll().Where(t => t.Date.Value.Date < dtpfromdate.Value.Date && t.CompId == CommonMethod.CompId).LastOrDefault();
                        if (CounterCashDataPre.Date.Value.ToString("dd/MM/yyyy") != dtpfromdate.Value.Date.ToString("dd/MM/yyyy"))
                            txtOpeningCash.Text = CounterCashDataPre.ClosingCash.ToString();
                        else
                        {
                            txtOpeningCash.Text = "0.00";
                            txtClosingCash.Text = "0.00";
                        }
                        flag = 0;
                        CounterCashRepo.Dispose();
                    }

                    try
                    {
                        PFAC();
                    }
                    catch (Exception)
                    { }

                    decimal closingcash = Convert.ToDecimal(txtOpeningCash.Text) - Convert.ToDecimal(lblTotalCrAmount.Text) + Convert.ToDecimal(lbltotalDrAmount.Text);
                    txtClosingCash.Text = closingcash.ToString();
                    if (flag == 0)
                    {
                        CounterCashRepository CounterCashRepo = new CounterCashRepository();
                        CounterCash CounterCashData1 = new CounterCash();
                        CounterCashData1.Date = dtpfromdate.Value.Date;
                        CounterCashData1.OpeningCash = Convert.ToDecimal(txtOpeningCash.Text);
                        CounterCashData1.ClosingCash = Convert.ToDecimal(txtClosingCash.Text);
                        CounterCashData1.CompId = CommonMethod.CompId;
                        CounterCashRepo.Add(CounterCashData1);
                        CounterCashRepo.Save();
                        CounterCashRepo.Dispose();
                    }
                    else if (flag == 1)
                    {
                        CounterCashRepository CounterCashRepo = new CounterCashRepository();
                        CounterCash CounterCashData1 = new CounterCash();
                        CounterCashData1 = CounterCashRepo.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == dtpfromdate.Value.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        CounterCashData1.OpeningCash = Convert.ToDecimal(txtOpeningCash.Text);
                        CounterCashData1.ClosingCash = Convert.ToDecimal(txtClosingCash.Text);
                        CounterCashData1.CompId = CommonMethod.CompId;
                        CounterCashRepo.Edit(CounterCashData1);
                        CounterCashRepo.Save();
                        CounterCashRepo.Dispose();
                    }
                    CounterCashRepo1.Dispose();
                }
                else
                {
                    MessageBox.Show("Select Date.", "Warning");
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
                RptDailySP rptdaily = new RptDailySP(dtpfromdate.Value);
                rptdaily.ShowDialog();
            }
            catch (Exception)
            { }
        }

        public void PFAC()
        {
            try
            {
                GVDr.Rows.Clear();
                GVCr.Rows.Clear();

                progressBar1.Visible = true;
                int minval = 0, maxval = 0;
                progressBar1.Minimum = minval;
                progressBar1.Maximum = maxval;

                string fromDate = cm.ValidFromDate(dtpfromdate.Value.Date), toDate = cm.ValidToDate(dtpfromdate.Value.Date);
                //New Logic
                //Left Side Dr
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCounterCashBook", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Dr";
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
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
                        GVDr.Rows[l].Cells[0].Value = dt.Rows[i][1].ToString();
                        GVDr.Rows[l].Cells[1].Value = Convert.ToString(dt.Rows[i][2].ToString());
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
                    SqlCommand command = new SqlCommand("SPCounterCashBook", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Cr";
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    SqlDataReader reader = command.ExecuteReader();
                    int l = 0;
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    progressBar1.Maximum = maxval + dt.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        progressBar1.Value = minval + 1;
                        GVCr.Rows.Add();
                        GVCr.Rows[l].Cells[0].Value = dt.Rows[i][1].ToString();
                        GVCr.Rows[l].Cells[1].Value = Convert.ToString(dt.Rows[i][2].ToString());
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
                    DrTotal = DrTotal + Convert.ToDecimal(GVDr.Rows[i].Cells[1].Value);
                }
                decimal CrTotal = 0;
                for (int i = 0; i < GVCr.Rows.Count; i++)
                {
                    CrTotal = CrTotal + Convert.ToDecimal(GVCr.Rows[i].Cells[1].Value);
                }
                progressBar1.Visible = false;

                lbltotalDrAmount.Text = Convert.ToString(Math.Round(DrTotal, 2, MidpointRounding.AwayFromZero));
                lblTotalCrAmount.Text = Convert.ToString(Math.Round(CrTotal, 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        public void getall()
        {
            try
            {
                int flag = 0;
                CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo1.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashData != null)
                {
                    txtOpeningCash.Text = CounterCashData.OpeningCash.ToString();
                    txtClosingCash.Text = CounterCashData.ClosingCash.ToString();
                    flag = 1;
                }
                else
                {
                    CounterCashRepository CounterCashRepo = new CounterCashRepository();
                    CounterCash CounterCashDataPre = new CounterCash();
                    CounterCashDataPre = CounterCashRepo.GetAll().Where(t => t.Date.Value.Date < dtpfromdate.Value.Date && t.CompId == CommonMethod.CompId).LastOrDefault();
                    if (CounterCashDataPre.Date.Value.ToString("dd/MM/yyyy") != DateTime.Now.ToString("dd/MM/yyyy"))
                        txtOpeningCash.Text = CounterCashDataPre.ClosingCash.ToString();
                    else
                    {
                        txtOpeningCash.Text = "0.00";
                        txtClosingCash.Text = "0.00";
                    }
                    flag = 0;
                    CounterCashRepo.Dispose();
                }

                try
                {
                    PFAC();
                }
                catch (Exception)
                { }
                decimal closingcash = Convert.ToDecimal(txtOpeningCash.Text) - Convert.ToDecimal(lblTotalCrAmount.Text) + Convert.ToDecimal(lbltotalDrAmount.Text);
                txtClosingCash.Text = closingcash.ToString();
                if (flag == 0)
                {
                    CounterCashRepository CounterCashRepo = new CounterCashRepository();
                    CounterCash CounterCashData1 = new CounterCash();
                    CounterCashData1.Date = DateTime.Now.Date;
                    CounterCashData1.OpeningCash = Convert.ToDecimal(txtOpeningCash.Text);
                    CounterCashData1.ClosingCash = Convert.ToDecimal(txtClosingCash.Text);
                    CounterCashData1.CompId = CommonMethod.CompId;
                    CounterCashRepo.Add(CounterCashData1);
                    CounterCashRepo.Save();
                    CounterCashRepo.Dispose();
                }
                else if (flag == 1)
                {
                    CounterCashRepository CounterCashRepo = new CounterCashRepository();
                    CounterCash CounterCashData1 = new CounterCash();
                    CounterCashData1 = CounterCashRepo.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    CounterCashData1.OpeningCash = Convert.ToDecimal(txtOpeningCash.Text);
                    CounterCashData1.ClosingCash = Convert.ToDecimal(txtClosingCash.Text);
                    CounterCashData1.CompId = CommonMethod.CompId;
                    CounterCashRepo.Edit(CounterCashData1);
                    CounterCashRepo.Save();
                    CounterCashRepo.Dispose();
                }
                CounterCashRepo1.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            dtpfromdate.Value = DateTime.Now.Date;
            getall();
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

        private void txtOpeningCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i >= 48 && i <= 57) || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void dtpfromdate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                CounterCashRepository CounterCashRepo = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashData != null)
                {
                    if (CounterCashData.Date.Value.Date > dtpfromdate.Value.Date)
                    {
                        MessageBox.Show("Date must be greater than Opening Balance Date.","Warning");
                        dtpfromdate.Value = DateTime.Now.Date;
                        this.ActiveControl = dtpfromdate;
                        return;
                    }
                }
                CounterCashRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void btnAddOpeningCash_Click(object sender, EventArgs e)
        {
            CounterCashRepository CounterCashRepo = new CounterCashRepository();
            CounterCash CounterCashData = new CounterCash();
            CounterCashData = CounterCashRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (CounterCashData == null)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Input Opening Balance of Today", "Opening Balance", "0", 300, 250);
                if (input != "" && input!="0")
                {
                    CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                    CounterCash CounterCashData1 = new CounterCash();
                    CounterCashData1.Date = dtpfromdate.Value.Date;
                    CounterCashData1.OpeningCash = Convert.ToDecimal(input);
                    CounterCashData1.ClosingCash = Convert.ToDecimal(input);
                    CounterCashData1.CompId = CommonMethod.CompId;
                    CounterCashRepo1.Add(CounterCashData1);
                    CounterCashRepo1.Save();
                    CounterCashRepo1.Dispose();
                    getall();
                    btnUpdate.Enabled = true;
                }
            }
            else
                MessageBox.Show("Opening Cash Already Added.","Warning");
            CounterCashRepo.Dispose();
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            try
            {
                RptDailySP rptdaily = new RptDailySP(CommonMethod.startdate, CommonMethod.enddate);
                rptdaily.ShowDialog();
                rptdaily.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                //decimal adjustmentamt = 0, openingcash = 0; ;
                //string adjustvalue = Microsoft.VisualBasic.Interaction.InputBox("Input Adjustment Amount for Opening Cash of Today", "Adjustment", "" + adjustmentamt + "", 300, 250);
                //if (adjustvalue != "")
                //{
                //    adjustmentamt = Convert.ToDecimal(adjustvalue);
                //    try
                //    {
                //        CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                //        CounterCash CounterCashData = new CounterCash();
                //        CounterCashData = CounterCashRepo1.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == dtpfromdate.Value.Date.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                //        if (CounterCashData != null)
                //        {
                //            openingcash = Convert.ToDecimal(CounterCashData.OpeningCash.ToString()) + adjustmentamt;
                //            txtOpeningCash.Text = openingcash.ToString();
                //            txtClosingCash.Text = CounterCashData.ClosingCash.ToString();
                //        }
                //        try
                //        {
                //            PFAC();
                //        }
                //        catch (Exception)
                //        { }
                //        decimal closingcash = Convert.ToDecimal(txtOpeningCash.Text) - Convert.ToDecimal(lblTotalCrAmount.Text) + Convert.ToDecimal(lbltotalDrAmount.Text);
                //        txtClosingCash.Text = closingcash.ToString();

                //        CounterCashRepository CounterCashRepo = new CounterCashRepository();
                //        CounterCash CounterCashData1 = new CounterCash();
                //        CounterCashData1 = CounterCashRepo.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == dtpfromdate.Value.Date.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                //        CounterCashData1.OpeningCash = Convert.ToDecimal(txtOpeningCash.Text);
                //        CounterCashData1.ClosingCash = Convert.ToDecimal(txtClosingCash.Text);
                //        CounterCashData1.CompId = CommonMethod.CompId;
                //        CounterCashRepo.Edit(CounterCashData1);
                //        CounterCashRepo.Save();

                //        printdate = DateTime.Now.ToString("dd/MM/yyyy");
                //        CounterCashRepo1.Dispose();
                //        CounterCashRepo.Dispose();
                //    }
                //    catch (Exception)
                //    { }
                //}
            }
            catch (Exception)
            { }
        }

        private void frmCounterCash_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPCalculateCounterCash", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = (cm.ValidToDate(dtpfromdate.Value.Date)).ToString();
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                SqlDataReader reader = command.ExecuteReader();
                db.CloseConnection();
                reader.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void BalShtToCounterCash(string CounterCash, DateTime fdate, DateTime tdate)
        {
            try
            {
                DataTable dt = db.GetTable("Select MAX(Date) as 'Date' From CounterCash where Date between '" + (cm.ValidFromDate(Convert.ToDateTime(fdate.Date))).ToString() + "' and '" + (cm.ValidToDate(Convert.ToDateTime(tdate.Date))).ToString() + "' and CompId='" + CommonMethod.CompId + "'");
                dtpfromdate.Value = Convert.ToDateTime(dt.Rows[0]["Date"].ToString());

                CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo1.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == dtpfromdate.Value.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashData != null)
                {
                    txtOpeningCash.Text = CounterCashData.OpeningCash.ToString();
                    txtClosingCash.Text = CounterCashData.ClosingCash.ToString();
                }
                try
                {
                    PFAC();
                }
                catch (Exception)
                { }
            }
            catch (Exception) { }
        }
    }
}