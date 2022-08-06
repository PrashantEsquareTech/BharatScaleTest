using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBalanceSuppliers : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmBalanceSuppliers()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getremainingpayment("Load");
        }

        public void getremainingpayment(string flag)
        {
            try
            {
                decimal totAmount = 0, totalcount = 0;
                db.connect();

                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = flag;
                if (flag == "DateWise")
                {
                    CommonMethod com = new CommonMethod();
                    string toDate = com.ValidToDate(dtptodate.Value.Date);
                    string fromDate = com.ValidFromDate(dtpfromdate.Value.Date);
                    command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                    command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
                }
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                object sumtotAmount = new object();
                sumtotAmount = dt.Compute("Sum(TotalReaminingAmount)", "");

                GvRemainingpayment.DataSource = dt;
                GvRemainingpayment.Columns[0].HeaderText = "SupplierId";
                GvRemainingpayment.Columns[1].HeaderText = "SupplierName";
                GvRemainingpayment.Columns[2].HeaderText = "SupplierAddress";
                GvRemainingpayment.Columns[3].HeaderText = "MobileNo";
                if (flag != "DateWise")
                    GvRemainingpayment.Columns[4].HeaderText = "TotalRemAmount";
                else
                {
                    GvRemainingpayment.Columns[4].HeaderText = "OpeningAmount";
                    GvRemainingpayment.Columns[5].HeaderText = "RemAmt";
                    GvRemainingpayment.Columns[6].HeaderText = "TotalRemAmount";
                    GvRemainingpayment.Columns[4].Visible = false;
                    GvRemainingpayment.Columns[5].Visible = false;
                }
                GvRemainingpayment.Columns[0].Visible = false;

                db.CloseConnection();
                if (!string.IsNullOrEmpty(Convert.ToString(sumtotAmount)))
                    totAmount = Convert.ToDecimal(sumtotAmount);
                totalcount = dt.Rows.Count;
                lblTotalMount.Text = Convert.ToString(totAmount);
                lblSuppliersCount.Text = Convert.ToString(totalcount);
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
            a:
                {
                    string input = "";
                    input = Microsoft.VisualBasic.Interaction.InputBox("1 For Normal Print and 2 For Credit Day Wise print and 3 For Date Wise Print", "Remaining Payment", "1", 400, 300);
                    if (input != "")
                    {
                        RptSupplierCredits rempay = new RptSupplierCredits(input, dtpfromdate.Value, dtptodate.Value);
                        rempay.ShowDialog();
                        rempay.Dispose();
                    }
                    //if (input == "1" || input == "")
                    //{
                    //    RptSupplierCredits rempay = new RptSupplierCredits(input);
                    //    rempay.ShowDialog();
                    //    rempay.Dispose();
                    //}
                    //else if (input == "2")
                    //{
                    //    RptSupplierCredits rempay = new RptSupplierCredits(input);
                    //    rempay.ShowDialog();
                    //    rempay.Dispose();
                    //}
                    //else if (input == "3")
                    //{
                    //    RptSupplierCredits rempay = new RptSupplierCredits(dtpfromdate.Value, dtptodate.Value);
                    //    rempay.ShowDialog();
                    //    rempay.Dispose();
                    //}
                    else
                        goto a;
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

        private void frmBalanceSuppliers_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btncreditlist_Click(object sender, EventArgs e)
        {
            try
            {
                getremainingpayment("CreditList");
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                getremainingpayment("DateWise");
            }
            catch (Exception) { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                getremainingpayment("Load");
            }
            catch (Exception) { }
        }

        private void GvRemainingpayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvRemainingpayment.Columns[e.ColumnIndex].HeaderText == "SupplierId")
                {
                    frmSupplierLedger custdata = new frmSupplierLedger(Convert.ToInt32(GvRemainingpayment.Rows[e.RowIndex].Cells["SupplierId"].Value));
                    custdata.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }
    }
}