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
    public partial class frmSupplierCreditDebitNoteList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public string crdrtype = "";

        public frmSupplierCreditDebitNoteList(string sType)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            crdrtype = sType;
            customerdata();
            TodaysList();
        }

        public void customerdata()
        {
            try
            {
                SupplierRepository custrepo1 = new SupplierRepository();
                List<SupplierInformation> custdata = new List<SupplierInformation>();
                custdata.Clear();
                custdata = custrepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                custdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                custdata.Reverse();
                cmbCustomerName.DataSource = custdata;
                cmbCustomerName.ValueMember = "SupplierCode";
                cmbCustomerName.DisplayMember = "SupplierName";
                custrepo1.Dispose();
                cmbCustomerName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void AutoCompleteCDNR()
        {
            try
            {
                txtCDNO.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT CustCreditDebitNo FROM SuppCreditDebitMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetInt32(0).ToString());
                        }
                        catch (Exception)
                        { }
                    }
                    txtCDNO.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void AutoCompletePorderNo()
        {
            try
            {
                txtBillNo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT PorderNo FROM PurchaseMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetInt32(0).ToString());
                        }
                        catch (Exception)
                        { }
                    }
                    txtBillNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void frmSupplierCreditDebitNoteList_Load(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteCDNR();
                AutoCompletePorderNo();
            }
            catch (Exception)
            { }
        }

        public void TodaysList()
        {
            try
            {
                string currdate = DateTime.Now.ToShortDateString();
                chkBillDate.Checked = false; chkBillNo.Checked = false; chkCreditDebitNo.Checked = false; chkCreditDebitNoteDate.Checked = false; chkCustomerName.Checked = false;
                customerdata(); dtpCDFrom.Value = DateTime.Now.Date; dtpCDTo.Value = DateTime.Now.Date; dtpFromDate.Value = DateTime.Now.Date; dtpToDate.Value = DateTime.Now.Date;
                txtBillNo.Text = "";
                txtCDNO.Text = "";
                DataTable dt = new DataTable();
                dt = db.GetTable("Select Id,SuppCreditDebitNo as 'CDN No',Convert(varchar(24),SuppCreditDebitDate,103) as 'Credit Debit Date',PorderNo as 'Porder No',Convert(varchar(24),PorderDate,103) as 'Porder Date',Suppliername as 'Supplier Name',CDType as 'Credit Debit Note Type',TotalAmount as 'Total Amount' from SuppCreditDebitMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'  and CDType='" + crdrtype + "' and Convert(varchar(24),SuppCreditDebitDate,103)='" + currdate + "'");
                GVCreditDebitList.DataSource = null;
                GVCreditDebitList.DataSource = dt;
                if (GVCreditDebitList.Rows.Count > 0)
                {
                    GVCreditDebitList.Columns["Id"].Visible = false;
                    GVCreditDebitList.Columns["Supplier Name"].Width = 200;
                    GVCreditDebitList.Columns["Credit Debit Note Type"].Width = 150;
                    lblCnt.Text = Convert.ToString(GVCreditDebitList.Rows.Count);
                }
                else
                    lblCnt.Text = Convert.ToString(0);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                strsql = "Select Id,SuppCreditDebitNo as 'CDN No',Convert(varchar(24),SuppCreditDebitDate,103) as 'Credit Debit Date',PorderNo as 'Porder No',Convert(varchar(24),PorderDate,103) as 'Porder Date',Suppliername as 'Supplier Name',CDType as 'Credit Debit Note Type',TotalAmount as 'Total Amount' from SuppCreditDebitMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'  and CDType='" + crdrtype + "' ";
                if (chkCustomerName.Checked == true)
                {
                    if (cmbCustomerName.SelectedIndex > 0)
                        strsql = strsql + "and Suppliername='" + Convert.ToString(cmbCustomerName.Text) + "' and SupplierId=" + Convert.ToInt32(cmbCustomerName.SelectedValue) + "";
                    else
                    {
                        MessageBox.Show("Select Supplier Name");
                        this.ActiveControl = cmbCustomerName;
                    }
                }
                if (chkBillNo.Checked == true)
                {
                    if (txtBillNo.Text != "")
                        strsql = strsql + "and PorderNo=" + Convert.ToInt32(txtBillNo.Text) + " ";
                    else
                    {
                        MessageBox.Show("Enter Bill No");
                        this.ActiveControl = txtBillNo;
                    }
                }
                if (chkCreditDebitNo.Checked == true)
                {
                    if (txtCDNO.Text != "")
                        strsql = strsql + "and SuppCreditDebitNo=" + Convert.ToInt32(txtCDNO.Text) + " ";
                    else
                    {
                        MessageBox.Show("Enter Credit Debit No");
                        this.ActiveControl = txtCDNO;
                    }
                }
                if (chkBillDate.Checked == true)
                {
                    if (dtpFromDate.Value.Date <= dtpToDate.Value.Date)
                        strsql = strsql + "and  Convert(varchar(24),PorderDate,103) between '" + dtpFromDate.Value.ToShortDateString() + "' and '" + dtpToDate.Value.ToShortDateString() + "'";
                    else
                    {
                        MessageBox.Show("To Date Should be Grater Than From Date");
                        this.ActiveControl = dtpToDate;
                    }
                }
                if (chkCreditDebitNoteDate.Checked == true)
                {
                    if (dtpCDFrom.Value.Date <= dtpCDTo.Value.Date)
                        strsql = strsql + "and  Convert(varchar(24),SuppCreditDebitDate,103) between '" + dtpCDFrom.Value.ToShortDateString() + "' and '" + dtpCDTo.Value.ToShortDateString() + "'";
                    else
                    {
                        MessageBox.Show("To Date Should be Greater Than From Date");
                        this.ActiveControl = dtpToDate;
                    }
                }
                DataTable dt = new DataTable();
                dt = db.GetTable(strsql);
                GVCreditDebitList.DataSource = null;
                GVCreditDebitList.DataSource = dt;
                if (chkBillDate.Checked == false && chkBillNo.Checked == false && chkCreditDebitNo.Checked == false && chkCreditDebitNoteDate.Checked == false && chkCustomerName.Checked == false)
                {
                    MessageBox.Show("Please Select Check Box");
                    this.ActiveControl = chkCustomerName;
                }
                else
                {
                    if (GVCreditDebitList.Rows.Count > 0)
                    {
                        GVCreditDebitList.Columns["Id"].Visible = false;
                        GVCreditDebitList.Columns["Supplier Name"].Width = 200;
                        GVCreditDebitList.Columns["Credit Debit Note Type"].Width = 150;
                        lblCnt.Text = Convert.ToString(GVCreditDebitList.Rows.Count);
                    }
                    else
                    {
                        MessageBox.Show("Reord Not Found");
                        lblCnt.Text = Convert.ToString(0);
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
                chkBillDate.Checked = false; chkBillNo.Checked = false; chkCreditDebitNo.Checked = false; chkCreditDebitNoteDate.Checked = false; chkCustomerName.Checked = false;
                customerdata(); dtpCDFrom.Value = DateTime.Now.Date; dtpCDTo.Value = DateTime.Now.Date; dtpFromDate.Value = DateTime.Now.Date; dtpToDate.Value = DateTime.Now.Date;
                txtBillNo.Text = ""; txtCDNO.Text = "";
                DataTable dt = new DataTable();
                dt = db.GetTable("Select Id,SuppCreditDebitNo as 'CDN No',Convert(varchar(24),SuppCreditDebitDate,103) as 'Credit Debit Date',PorderNo as 'Porder No',Convert(varchar(24),PorderDate,103) as 'Porder Date',Suppliername as 'Supplier Name',CDType as 'Credit Debit Note Type',TotalAmount as 'Total Amount' from SuppCreditDebitMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'  and CDType='" + crdrtype + "'");
                GVCreditDebitList.DataSource = null;
                GVCreditDebitList.DataSource = dt;
                if (GVCreditDebitList.Rows.Count > 0)
                {
                    GVCreditDebitList.Columns["Id"].Visible = false;
                    GVCreditDebitList.Columns["Supplier Name"].Width = 200;
                    GVCreditDebitList.Columns["Credit Debit Note Type"].Width = 150;
                    lblCnt.Text = Convert.ToString(GVCreditDebitList.Rows.Count);
                }
                else
                    lblCnt.Text = Convert.ToString(0);
            }
            catch (Exception)
            { }
        }

        private void GVCreditDebitList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (frmSupplierCreditDebitNotecs.suppcreditdebit == null)
                    {
                        frmSupplierCreditDebitNotecs custbill = new frmSupplierCreditDebitNotecs(Convert.ToInt32(GVCreditDebitList.Rows[e.RowIndex].Cells["Porder No"].Value));
                        custbill.ShowDialog();
                        frmSupplierCreditDebitNotecs.suppcreditdebit = null;
                    }
                    else
                    {
                        this.Close();
                        frmSupplierCreditDebitNotecs.suppcreditdebit.LoadData(Convert.ToInt32(GVCreditDebitList.Rows[e.RowIndex].Cells["Porder No"].Value));
                        frmSupplierCreditDebitNotecs.suppcreditdebit.Show();
                        frmSupplierCreditDebitNotecs.suppcreditdebit = null;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}