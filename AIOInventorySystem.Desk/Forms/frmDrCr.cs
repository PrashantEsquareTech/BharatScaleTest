using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmDrCr : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmDrCr()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            try
            {
                getall();
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                {
                    //Sale Details
                    SqlDataAdapter da = new SqlDataAdapter("Select BillNo,CONVERT(VARCHAR(10),Billdate,103) as 'Bill Date',CustomerName,NetAmount From CustomerBillMaster Where BillDate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' AND BillDate<='" + cm.ValidToDate(dtptodate.Value.Date) + "' and BillNo like 'E%' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    GvCredit.Rows.Clear();
                    dt1.Rows.Add(null, null, "Total", dt1.Compute("Sum(NetAmount)", ""));
                    GvCredit.Refresh();
                    if (dt1.Rows.Count != 0)
                    {
                        int l = 0;
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            GvCredit.Rows.Add();
                            GvCredit.Rows[l].Cells[0].Value = Convert.ToString(dt1.Rows[i][0].ToString());
                            GvCredit.Rows[l].Cells[1].Value = Convert.ToString(dt1.Rows[i][1].ToString());
                            GvCredit.Rows[l].Cells[2].Value = Convert.ToString(dt1.Rows[i][2].ToString());
                            GvCredit.Rows[l].Cells[3].Value = Convert.ToString(dt1.Rows[i][3].ToString());
                            l++;
                        }
                        lblsaleamount.Text = Convert.ToString(dt1.Compute("Sum(NetAmount)", ""));
                    }
                    else
                        MessageBox.Show("Record not found.", "Warning");
                    //Purchase Details
                    SqlDataAdapter daDebit = new SqlDataAdapter("Select PorderNo,CONVERT(VARCHAR(10),PorderDate,103) as 'PO Date',Suppliername,NetAmount From PurchaseMaster Where PorderDate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' AND PorderDate<='" + cm.ValidToDate(dtptodate.Value.Date) + "'and vat=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DataTable dtDebit1 = new DataTable();
                    daDebit.Fill(dtDebit1);
                    GvDebit.Rows.Clear();
                    dtDebit1.Rows.Add(null, null, "Total", dtDebit1.Compute("Sum(NetAmount)", ""));
                    GvDebit.Refresh();
                    if (dtDebit1.Rows.Count != 0)
                    {
                        int k = 0;
                        for (int j = 0; j < dtDebit1.Rows.Count; j++)
                        {
                            GvDebit.Rows.Add();
                            GvDebit.Rows[k].Cells[0].Value = Convert.ToString(dtDebit1.Rows[j][0].ToString());
                            GvDebit.Rows[k].Cells[1].Value = Convert.ToString(dtDebit1.Rows[j][1].ToString());
                            GvDebit.Rows[k].Cells[2].Value = Convert.ToString(dtDebit1.Rows[j][2].ToString());
                            GvDebit.Rows[k].Cells[3].Value = Convert.ToString(dtDebit1.Rows[j][3].ToString());
                            k++;
                        }
                        lblpurchaseamount.Text = Convert.ToString(dtDebit1.Compute("Sum(NetAmount)", ""));
                    }
                    else
                        MessageBox.Show("Record not found.", "Warning");
                    da.Dispose(); dt1.Dispose();
                    daDebit.Dispose(); dtDebit1.Dispose();
                }
                else
                {
                    MessageBox.Show("To Date Should greater than from date.", "Warning");
                    return;
                }
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                {
                    RptDrCr drobject = new RptDrCr(dtpfromdate.Text, dtptodate.Text);
                    drobject.ShowDialog();
                    drobject.Dispose();
                }
                else
                {
                    RptDrCr drobject = new RptDrCr();
                    drobject.ShowDialog();
                    drobject.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void getall()
        {
            try
            {
                //Sale Details
                SqlDataAdapter da = new SqlDataAdapter("Select BillNo,CONVERT(VARCHAR(10),Billdate,103) as 'Date',CustomerName,NetAmount From CustomerBillMaster where vat=0 and BillNo like 'E%' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvCredit.Rows.Clear();
                dt.Rows.Add(null, null, "Total", dt.Compute("Sum(NetAmount)", ""));
                GvCredit.Refresh();
                if (dt.Rows.Count != 0)
                {
                    int l = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GvCredit.Rows.Add();
                        GvCredit.Rows[l].Cells[0].Value = Convert.ToString(dt.Rows[i][0].ToString());
                        GvCredit.Rows[l].Cells[1].Value = Convert.ToString(dt.Rows[i][1].ToString());
                        GvCredit.Rows[l].Cells[2].Value = Convert.ToString(dt.Rows[i][2].ToString());
                        GvCredit.Rows[l].Cells[3].Value = Convert.ToString(dt.Rows[i][3].ToString());
                        l++;
                    }
                    lblsaleamount.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                }
                else
                    MessageBox.Show("Record not found.", "Warning");

                //Purchase Details
                SqlDataAdapter daDebit = new SqlDataAdapter("Select PorderNo,CONVERT(VARCHAR(10),PorderDate,103),Suppliername,NetAmount From PurchaseMaster where (Vat=0 or TAXType is null) and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dtDebit = new DataTable();
                daDebit.Fill(dtDebit);
                GvDebit.Rows.Clear();
                dtDebit.Rows.Add(null, null, "Total", dtDebit.Compute("Sum(NetAmount)", ""));
                GvDebit.Refresh();
                if (dtDebit.Rows.Count != 0)
                {
                    int k = 0;
                    for (int j = 0; j < dtDebit.Rows.Count; j++)
                    {
                        GvDebit.Rows.Add();
                        GvDebit.Rows[k].Cells[0].Value = Convert.ToString(dtDebit.Rows[j][0].ToString());
                        GvDebit.Rows[k].Cells[1].Value = Convert.ToString(dtDebit.Rows[j][1].ToString());
                        GvDebit.Rows[k].Cells[2].Value = Convert.ToString(dtDebit.Rows[j][2].ToString());
                        GvDebit.Rows[k].Cells[3].Value = Convert.ToString(dtDebit.Rows[j][3].ToString());
                        k++;
                    }
                    lblpurchaseamount.Text = Convert.ToString(dtDebit.Compute("Sum(NetAmount)", ""));
                }
                else
                    MessageBox.Show("Record not found.", "Warning");
                da.Dispose(); dt.Dispose();
                daDebit.Dispose(); dtDebit.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            dtpfromdate.Value = DateTime.Now.Date;
            dtptodate.Value = DateTime.Now.Date;
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

        private void dtptodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void frmDrCr_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
    }
}