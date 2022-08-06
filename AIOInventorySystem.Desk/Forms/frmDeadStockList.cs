using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmDeadStockList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmDeadStockList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Bindgrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkporderno.Checked == true)
                {
                    if (txtPOrderno.Text != "")
                        da = new SqlDataAdapter("SELECT [Id],[DeadNo],CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date' FROM [DeadProductMaster] where DeadNo='" + txtPOrderno.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    else
                    {
                        MessageBox.Show("Enter Dead Stock Number.", "Warning");
                        this.ActiveControl = txtPOrderno;
                    }
                }
                else if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                        da = new SqlDataAdapter("SELECT [Id],[DeadNo],CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date' FROM [DeadProductMaster] where DeadDate>='" + cm.ValidFromDate(dtpFromPorderdate.Value.Date) + "' and DeadDate<='" + cm.ValidToDate(dtpToPorderDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    else
                    {
                        MessageBox.Show("To Date Should be greater than from date.", "Warning");
                        this.ActiveControl = dtpToPorderDate;
                    }
                }
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvPorderInfo.DataSource = dt;
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                if (GvPorderInfo.Rows.Count != 0)
                {
                    GvPorderInfo.Columns["Id"].Visible = false;
                    GvPorderInfo.Columns["DeadNo"].Width = 150;
                    GvPorderInfo.Columns["Dead Date"].Width = 200;
                    this.ActiveControl = txtPOrderno;
                }
                else
                {
                    MessageBox.Show("Record not found.", "Warning");
                    this.ActiveControl = txtPOrderno;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Bindgrid()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [DeadNo],CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date' FROM [DeadProductMaster] where CONVERT(VARCHAR(24),DeadDate,101)=CONVERT(VARCHAR(24),GETDATE(),101)  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvPorderInfo.DataSource = dt;
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();

                GvPorderInfo.Columns["Id"].Visible = false;
                GvPorderInfo.Columns["DeadNo"].Width = 150;
                GvPorderInfo.Columns["Dead Date"].Width = 200;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                chkporderno.Checked = false;
                chkbetweendate.Checked = false;
                try
                {
                    dtpFromPorderdate.Value = DateTime.Now.Date;
                    dtpToPorderDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtPOrderno.Text = "";
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [DeadNo],CONVERT(VARCHAR(24),DeadDate,103) as 'Dead Date' FROM [DeadProductMaster] where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvPorderInfo.DataSource = dt;
                GvPorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);

                GvPorderInfo.Columns["Id"].Visible = false;
                GvPorderInfo.Columns["DeadNo"].Width = 150;
                GvPorderInfo.Columns["Dead Date"].Width = 200;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkporderno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkporderno.Checked == true)
                    chkbetweendate.Checked = false;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbetweendate.Checked == true)
                    chkporderno.Checked = false;
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (frmDeadStock.deadstockinfo == null)
                    {
                        frmDeadStock deadstock = new frmDeadStock(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        deadstock.ShowDialog();
                        frmDeadStock.deadstockinfo = null;
                    }
                    else
                    {
                        this.Close();
                        frmDeadStock.deadstockinfo.LoadData(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        frmDeadStock.deadstockinfo.Show();
                        frmDeadStock.deadstockinfo = null;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmDeadStockList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmDeadStock.deadstockinfo = null;
            }
            catch (Exception)
            { }
        }

        private void btntodaylist_Click(object sender, EventArgs e)
        {
            try
            {
                chkporderno.Checked = false;
                chkbetweendate.Checked = false;
                try
                {
                    dtpFromPorderdate.Value = DateTime.Now.Date;
                    dtpToPorderDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtPOrderno.Text = "";
                Bindgrid();
            }
            catch (Exception)
            { }
        }

        private void txtPOrderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void dtpFromPorderdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpToPorderDate;
            }
            catch (Exception)
            { }
        }

        private void dtpToPorderDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void frmDeadStockList_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void chkporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPOrderno;
            }
            catch (Exception) { }
        }
    }
}