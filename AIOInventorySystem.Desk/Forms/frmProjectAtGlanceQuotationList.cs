using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmProjectAtGlanceQuotationList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmProjectAtGlanceQuotationList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillsearchstaffcombo();
            fillCustomercombo();
            TodaysList();
        }

        public void fillCustomercombo()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> staffdata = new List<CustomerInformation>();
                staffdata.Clear();
                staffdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                staffdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                staffdata.Reverse();
                cmbCustomerName.DataSource = staffdata;
                cmbCustomerName.ValueMember = "Id";
                cmbCustomerName.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbCustomerName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillsearchstaffcombo()
        {
            try
            {
                StaffRepository staffrepo = new StaffRepository();
                List<StaffInfo> staffdata = new List<StaffInfo>();
                staffdata.Clear();
                staffdata = staffrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Name).ToList();
                staffdata.Add(new StaffInfo { Name = "Select", ID = 0 });
                staffdata.Reverse();
                cmbEmployeeName.DataSource = staffdata;
                cmbEmployeeName.ValueMember = "ID";
                cmbEmployeeName.DisplayMember = "Name";
                staffrepo.Dispose();
                cmbEmployeeName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void loadRefernceNo()
        {
            try
            {
                txtRefNo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand("SELECT RefernceNo FROM tblProjectAtGlanceMaster where compid=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtRefNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception) { }
        }

        private void frmProjectAtGlanceQuotationList_Load(object sender, EventArgs e)
        {
            try
            {
                loadRefernceNo();
            }
            catch (Exception)
            { }
        }

        public void TodaysList()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = db.GetTable("Select pm.Id, pm.QutNo as 'Qut No.',Convert(nvarchar(24),pm.Date,103) as 'Date',pm.RefernceNo as 'Ref No.',c.CustomerName as 'Customer Name',s.Name as 'Emplyoee Name',pm.TotalAmt as 'Total Amt',pm.GSTAmt as 'GST Amt',pm.NetAmt as 'Net Amt' from tblProjectAtGlanceMaster as pm inner join CustomerInformation as c on c.Id=pm.CustomerId left join StaffInfo as s on s.ID=pm.EmployeeId where pm.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and Convert(varchar(24),pm.Date,103)='" + DateTime.Now.Date.ToShortDateString() + "'");
                GVList.DataSource = null;
                GVList.DataSource = dt;
                if (GVList.Rows.Count > 0)
                {
                    GVList.Columns["Id"].Visible = false;
                    GVList.Columns[4].Width = 60;
                    GVList.Columns[4].Width = 80;
                    GVList.Columns[5].Width = 180;
                    lblTotal.Text = Convert.ToString(GVList.Rows.Count);
                }
                else
                    lblTotal.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                strsql = "Select pm.Id, pm.QutNo as 'Qut No.',Convert(nvarchar(24),pm.Date,103) as 'Date',pm.RefernceNo as 'Ref No.',c.CustomerName as 'Customer Name',s.Name as 'Emplyoee Name',pm.TotalAmt as 'Total Amt',pm.GSTAmt as 'GST Amt',pm.NetAmt as 'Net Amt' from tblProjectAtGlanceMaster as pm inner join CustomerInformation as c on c.Id=pm.CustomerId left join StaffInfo as s on s.ID=pm.EmployeeId where pm.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'";
                if (chkCustomerName.Checked == true)
                {
                    if (cmbCustomerName.SelectedIndex > 0)
                        strsql = strsql + "and pm.CustomerId=" + Convert.ToInt32(cmbCustomerName.SelectedValue) + "";
                    else
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbCustomerName;
                    }
                }
                if (chkDate.Checked == true)
                {
                    if (dtpFromDate.Value.Date <= dtpToDate.Value.Date)
                        strsql = strsql + "and Convert(varchar(24),pm.Date,103) between '" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "' and '" + dtpToDate.Value.ToString("dd/MM/yyyy") + "'";
                    else
                    {
                        MessageBox.Show("From Date Should be Grater Than To Date");
                        this.ActiveControl = dtpToDate;
                    }
                }
                if (chkEmployeeName.Checked == true)
                {
                    if (cmbEmployeeName.SelectedIndex > 0)
                        strsql = strsql + "and pm.EmployeeId=" + Convert.ToInt32(cmbEmployeeName.SelectedValue) + "";
                    else
                    {
                        MessageBox.Show("Select Employee Name");
                        this.ActiveControl = cmbEmployeeName;
                    }
                }
                if (chkRefNo.Checked == true)
                {
                    if (txtRefNo.Text != "")
                        strsql = strsql + "and pm.RefernceNo='" + txtRefNo.Text + "'";
                    else
                    {
                        MessageBox.Show("Enter Reference No");
                        this.ActiveControl = txtRefNo;
                    }
                }
                if (chkCustomerName.Checked == false && chkDate.Checked == false && chkEmployeeName.Checked == false && chkRefNo.Checked == false)
                {
                    MessageBox.Show("Select Cheack Box");
                    this.ActiveControl = chkCustomerName;
                }
                else
                {
                    DataTable dt = new DataTable();
                    if (strsql != "")
                        dt = db.GetTable(strsql);
                    GVList.DataSource = null;
                    GVList.DataSource = dt;
                    if (GVList.Rows.Count > 0)
                    {
                        GVList.Columns["Id"].Visible = false;
                        lblTotal.Text = Convert.ToString(GVList.Rows.Count);
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found");
                        lblTotal.Text = "0";
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                chkCustomerName.Checked = false; chkDate.Checked = false; chkEmployeeName.Checked = false; chkRefNo.Checked = false;
                fillCustomercombo(); fillsearchstaffcombo();
                dtpFromDate.Value = DateTime.Now.Date; dtpToDate.Value = DateTime.Now.Date;
                txtRefNo.Text = "";
                DataTable dt = new DataTable();
                dt = db.GetTable("Select pm.Id, pm.QutNo as 'Qut No.',Convert(nvarchar(24),pm.Date,103) as 'Date',pm.RefernceNo as 'Ref No.',c.CustomerName as 'Customer Name',s.Name as 'Emplyoee Name',pm.TotalAmt as 'Total Amt',pm.GSTAmt as 'GST Amt',pm.NetAmt as 'Net Amt' from tblProjectAtGlanceMaster as pm inner join CustomerInformation as c on c.Id=pm.CustomerId left join StaffInfo as s on s.ID=pm.EmployeeId where pm.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'");
                GVList.DataSource = null;
                GVList.DataSource = dt;
                if (GVList.Rows.Count > 0)
                {
                    GVList.Columns["Id"].Visible = false;
                    lblTotal.Text = Convert.ToString(GVList.Rows.Count);
                }
                else
                    lblTotal.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void GVList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (FrmProjectGlanceQuotation.projectQut == null)
                    {
                        FrmProjectGlanceQuotation qut = new FrmProjectGlanceQuotation(Convert.ToInt32(GVList.Rows[e.RowIndex].Cells[1].Value));
                        qut.ShowDialog();
                        FrmProjectGlanceQuotation.projectQut = null;
                    }
                    else
                    {
                        this.Close();
                        FrmProjectGlanceQuotation.projectQut.LoadData(Convert.ToInt32(GVList.Rows[e.RowIndex].Cells[1].Value));
                        FrmProjectGlanceQuotation.projectQut.Show();
                        FrmProjectGlanceQuotation.projectQut = null;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void chkCustomerName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCustomerName.Checked == true)
                {
                    if (chkEmployeeName.Checked == false)
                        fillsearchstaffcombo();
                    if (chkDate.Checked == false)
                    {
                        dtpFromDate.Value = DateTime.Now.Date;
                        dtpToDate.Value = DateTime.Now.Date;
                    }
                    if (chkRefNo.Checked == false)
                        txtRefNo.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDate.Checked == true)
                {
                    if (chkEmployeeName.Checked == false)
                        fillsearchstaffcombo();
                    if (chkCustomerName.Checked == false)
                    {
                        fillCustomercombo();
                    }
                    if (chkRefNo.Checked == false)
                        txtRefNo.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkEmployeeName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEmployeeName.Checked == true)
                {
                    if (chkCustomerName.Checked == false)
                        fillCustomercombo();
                    if (chkDate.Checked == false)
                    {
                        dtpFromDate.Value = DateTime.Now.Date;
                        dtpToDate.Value = DateTime.Now.Date;
                    }
                    if (chkRefNo.Checked == false)
                        txtRefNo.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkRefNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRefNo.Checked == true)
                {
                    if (chkEmployeeName.Checked == false)
                        fillsearchstaffcombo();
                    if (chkDate.Checked == false)
                    {
                        dtpFromDate.Value = DateTime.Now.Date;
                        dtpToDate.Value = DateTime.Now.Date;
                    }
                    if (chkCustomerName.Checked == false)
                        fillCustomercombo();
                }
            }
            catch (Exception)
            { }
        }
    }
}