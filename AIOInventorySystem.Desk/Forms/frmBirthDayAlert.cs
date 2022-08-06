using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBirthDayAlert : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmBirthDayAlert()
        {
            InitializeComponent();
        }

        public frmBirthDayAlert(string docType)
        {
            InitializeComponent();
            getDetails(docType);
        }

        private void getDetails(string doc_type)
        {
            try
            {
                int rowcount = 0;
                dtgDetails.Rows.Clear();
                if (doc_type == "CB")  //Customers birthdate
                {
                    this.Text = "Customers Birthday";
                    lblTitle.Text = "Customers Birthday";
                    dtgDetails.Columns[0].HeaderText = "Customer";
                    dtgDetails.Columns[1].HeaderText = "Birth Date";
                    try
                    {
                        bool flgExist = false;
                        string Todays_Date = cm.ValidFromDate(DateTime.Now.Date);
                        SqlDataAdapter da = new SqlDataAdapter("select customername, isnull(datepart(day,birthdate)-datepart(day,'" + Todays_Date + "'),0),isnull(datepart(month,birthdate)-datepart(month,'" + Todays_Date + "'),0),isnull(datepart(year,'" + Todays_Date + "')-datepart(year,birthdate),0),convert(varchar(24),birthdate,103) as BDate from customerinformation where compid=" + CommonMethod.CompId, db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(dt.Rows[i][1]) == 0 && Convert.ToInt32(dt.Rows[i][2]) == 0 && Convert.ToInt32(dt.Rows[i][3]) > 0)
                                    flgExist = true;
                                else
                                    flgExist = false;
                                if (flgExist == true)
                                {
                                    dtgDetails.Rows.Add();
                                    rowcount = Convert.ToInt32(dtgDetails.Rows.Count - 1);
                                    dtgDetails.Rows[rowcount].Cells["Customer"].Value = dt.Rows[i]["customername"];
                                    dtgDetails.Rows[rowcount].Cells["BirthDate"].Value = dt.Rows[i]["BDate"];
                                }
                            }
                        }
                    }
                    catch (Exception)
                    { }
                }
                else if (doc_type == "R")  //Reminder
                {
                    try
                    {
                        this.Text = "Reminders";
                        lblTitle.Text = "Reminders";
                        string TodaysDate = cm.ValidToDate(DateTime.Now), Todays_Date = cm.ValidFromDate(DateTime.Now.Date);
                        dtgDetails.Columns[0].HeaderText = "Reminder Message";
                        dtgDetails.Columns[1].HeaderText = "Date and Time";
                        SqlDataAdapter da = new SqlDataAdapter("select RemMessage,convert(varchar(24),Time,103) as Rem from reminder where Datediff(day,[Date],'" + Todays_Date + "')=0 and Datediff(month,[Date],'" + Todays_Date + "')=0 and Datediff(year,[Date],'" + Todays_Date + "')=0 and Datediff(hh,convert(varchar(24),Time,121),'" + TodaysDate + "')>=0 and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dtgDetails.Rows.Add();
                                rowcount = Convert.ToInt32(dtgDetails.Rows.Count - 1);
                                dtgDetails.Rows[rowcount].Cells["Customer"].Value = dt.Rows[i]["RemMessage"];
                                dtgDetails.Rows[rowcount].Cells["BirthDate"].Value = dt.Rows[i]["Rem"];
                            }
                        }
                    }
                    catch (Exception)
                    { }
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
    }
}
