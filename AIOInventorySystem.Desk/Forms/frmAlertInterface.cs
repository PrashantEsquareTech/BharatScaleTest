using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmAlertInterface : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        private int CopyInput = 0;        

        public frmAlertInterface()
        {
            InitializeComponent();
            fillAlertDetails("TwoDays");
        }

        private void fillAlertDetails(string flag)
        {
            DateTime currdate = DateTime.Now.Date, currdate1 = DateTime.Now.Date, upcomingDate = currdate.AddDays(CopyInput), nextTwoDays = currdate.AddDays(2);
            string nexttwodays = cm.ValidToDate(nextTwoDays.Date), currdate2 = cm.ValidFromDate(currdate.Date);
            int rowcount = 0;

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                if (flag == "All")
                    da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                else if (flag == "Todays")
                    da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and CONVERT(VARCHAR(24),PaymentDate,103)='" + currdate.ToString("dd/MM/yyyy") + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                else if (flag == "UpComing")
                    da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and CONVERT(VARCHAR(24),PaymentDate,103)>='" + currdate.ToString("dd/MM/yyyy") + "' and CONVERT(VARCHAR(24),PaymentDate,103)<='" + upcomingDate.ToString("dd/MM/yyyy") + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                else if (flag == "TwoDays")
                    da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and PaymentDate>='" + currdate2 + "' and PaymentDate<='" + nexttwodays + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgCreditList.DataSource = dt;
                dtgCreditList.Refresh();
                if (dtgCreditList.Rows.Count != 0)
                {
                    dtgCreditList.DataSource = dt;
                    dtgCreditList.Columns["Id"].Visible = false;
                    dtgCreditList.Columns["BillNo"].Width = 45;
                    dtgCreditList.Columns["BillDate"].Width = 75;
                    dtgCreditList.Columns["CreditDate"].Width = 75;
                    dtgCreditList.Columns["Days"].Width = 40;
                    dtgCreditList.Columns["CustomerName"].Width = 200;
                    dtgCreditList.Columns["NetAmount"].Width = 95;
                    dtgCreditList.Columns["PaidAmount"].Width = 95;
                    dtgCreditList.Columns["RemAmount"].Width = 95;
                }
            }
            catch (Exception)
            { }

            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPStockAlert", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                DataTable dtStock = new DataTable();
                dtStock.Load(command.ExecuteReader());
                db.CloseConnection();
                if (dtStock.Rows.Count > 0)
                {
                    dtgStock.DataSource = dtStock;
                    dtgStock.Columns[1].Width = 250;
                    dtgStock.Columns["Id"].Visible = false;
                }
            }
            catch (Exception)
            { db.CloseConnection(); }

            try
            {
                DataTable dt1 = new DataTable();
                if (flag == "TwoDays")
                    currdate = nextTwoDays;
                db.connect();
                SqlCommand command = new SqlCommand("SPAlertForSuppliersCredits", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                if (flag == "UpComing")
                {
                    command.Parameters.Add("@CurrentDate", SqlDbType.VarChar).Value = upcomingDate.ToString("dd/MM/yyyy");
                    command.Parameters.Add("@CurrentDate1", SqlDbType.VarChar).Value = upcomingDate.ToString("MM/dd/yyyy");
                }
                else
                {
                    command.Parameters.Add("@CurrentDate", SqlDbType.VarChar).Value = currdate.ToString("dd/MM/yyyy");
                    command.Parameters.Add("@CurrentDate1", SqlDbType.VarChar).Value = currdate.ToString("MM/dd/yyyy");
                }
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.ExecuteNonQuery();
                SqlDataAdapter da1 = new SqlDataAdapter();

                if (flag == "All")
                    da1 = new SqlDataAdapter("Select POrderNo as 'Purchase NO',CONVERT(VARCHAR(24),POrderDate,103) as 'Purchase Date',CONVERT(VARCHAR(24),CreditDate,103) as 'Credit Date',Days,SupplierName,NetAmount,PaidAmount,RemainingAmount from tblAlertForSupplier  ", db.Connection);
                else if (flag == "Todays")
                    da1 = new SqlDataAdapter("Select POrderNo as 'Purchase NO',CONVERT(VARCHAR(24),POrderDate,103) as 'Purchase Date',CONVERT(VARCHAR(24),CreditDate,103) as 'Credit Date',Days,SupplierName,NetAmount,PaidAmount,RemainingAmount from tblAlertForSupplier where CONVERT(VARCHAR(24),CreditDate,103)='" + currdate.ToString("dd/MM/yyyy") + "'", db.Connection);
                else if (flag == "UpComing")
                    da1 = new SqlDataAdapter("Select POrderNo as 'Purchase NO',CONVERT(VARCHAR(24),POrderDate,103) as 'Purchase Date',CONVERT(VARCHAR(24),CreditDate,103) as 'Credit Date',Days,SupplierName,NetAmount,PaidAmount,RemainingAmount from tblAlertForSupplier where CONVERT(VARCHAR(24),CreditDate,103)>='" + currdate.ToString("dd/MM/yyyy") + "' and CONVERT(VARCHAR(24),CreditDate,103)<='" + upcomingDate.ToString("dd/MM/yyyy") + "'", db.Connection);
                else if (flag == "TwoDays")
                    da1 = new SqlDataAdapter("Select POrderNo as 'Purchase NO',CONVERT(VARCHAR(24),POrderDate,103) as 'Purchase Date',CONVERT(VARCHAR(24),CreditDate,103) as 'Credit Date',Days,SupplierName,NetAmount,PaidAmount,RemainingAmount from tblAlertForSupplier where CONVERT(VARCHAR(24),CreditDate,103)>='" + currdate1.ToString("dd/MM/yyyy") + "' and CONVERT(VARCHAR(24),CreditDate,103)<='" + nextTwoDays.ToString("dd/MM/yyyy") + "' ", db.Connection);

                da1.Fill(dt1);
                dtgvSupplierCreditListAlert.DataSource = dt1;
                dtgvSupplierCreditListAlert.Refresh();
                db.CloseConnection();
                if (dtgvSupplierCreditListAlert.Rows.Count != 0)
                {
                    dtgvSupplierCreditListAlert.DataSource = dt1;
                    dtgvSupplierCreditListAlert.Columns["SupplierName"].Width = 130;
                    dtgvSupplierCreditListAlert.Columns["Purchase NO"].Width = 50;
                    dtgvSupplierCreditListAlert.Columns["Purchase Date"].Width = 70;
                    dtgvSupplierCreditListAlert.Columns["Credit Date"].Width = 70;
                    dtgvSupplierCreditListAlert.Columns["Days"].Width = 40;
                    dtgvSupplierCreditListAlert.Columns["NetAmount"].Width = 90;
                    dtgvSupplierCreditListAlert.Columns["RemainigAmount"].Width = 90;
                    dtgvSupplierCreditListAlert.Columns["PaidAmount"].Width = 90;
                }
            }
            catch (Exception)
            { db.CloseConnection(); }

            try
            {
                bool flgExist = false;
                string Todays_Date = cm.ValidFromDate(DateTime.Now.Date);
                SqlDataAdapter da = new SqlDataAdapter("select customername,isnull(datepart(day,birthdate)-datepart(day,'" + Todays_Date + "'),0),isnull(datepart(month,birthdate)-datepart(month,'" + Todays_Date + "'),0),isnull(datepart(year,'" + Todays_Date + "')-datepart(year,birthdate),0),convert(varchar(24),birthdate,103) as BDate from customerinformation where compid=" + CommonMethod.CompId, db.Connection);
                DataTable dtBirthday = new DataTable();
                da.Fill(dtBirthday);
                if (dtBirthday.Rows.Count > 0)
                {
                    for (int i = 0; i < dtBirthday.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtBirthday.Rows[i][1]) == 0 && Convert.ToInt32(dtBirthday.Rows[i][2]) == 0 && Convert.ToInt32(dtBirthday.Rows[i][3]) > 0)
                            flgExist = true;
                        else
                            flgExist = false;
                        if (flgExist == true)
                        {
                            dtgBirthday.Rows.Add();
                            rowcount = Convert.ToInt32(dtgBirthday.Rows.Count - 1);
                            dtgBirthday.Rows[rowcount].Cells[0].Value = dtBirthday.Rows[i]["customername"];
                            dtgBirthday.Rows[rowcount].Cells[1].Value = dtBirthday.Rows[i]["BDate"];
                        }
                    }
                }
            }
            catch (Exception)
            { }

            try
            {
                string TodaysDate = cm.ValidToDate(DateTime.Now);
                string strsql = "select RemMessage,convert(varchar(24),Time,121) as Rem from reminder where Datediff(hh,convert(varchar(24),Time,121),'" + TodaysDate + "')>=0 and CompId='" + CommonMethod.CompId + "'";
                SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                DataTable dtReminder = new DataTable();
                da.Fill(dtReminder);
                if (dtReminder.Rows.Count > 0)
                {
                    for (int i = 0; i < dtReminder.Rows.Count; i++)
                    {
                        dtgReminder.Rows.Add();
                        rowcount = Convert.ToInt32(dtgReminder.Rows.Count - 1);
                        dtgReminder.Rows[rowcount].Cells[0].Value = dtReminder.Rows[i]["RemMessage"];
                        dtgReminder.Rows[rowcount].Cells[1].Value = dtReminder.Rows[i]["Rem"];
                    }
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

        private void btnall_Click(object sender, EventArgs e)
        {
            try
            {
                fillAlertDetails("All");
            }
            catch (Exception)
            { }
        }

        private void btntodayslist_Click(object sender, EventArgs e)
        {
            try
            {
                fillAlertDetails("Todays");
            }
            catch (Exception)
            { }
        }

        private void btnupComing_Click(object sender, EventArgs e)
        {
            try
            {
                string input_Copies = Microsoft.VisualBasic.Interaction.InputBox("How Many Days?", "Alert", "", 250, 250);
                CopyInput = Convert.ToInt32(input_Copies);
                fillAlertDetails("UpComing");
            }
            catch (Exception)
            { }
        }
    }
}