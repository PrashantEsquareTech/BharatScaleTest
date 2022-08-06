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
    public partial class frmCustomerCerditDebitNoteList : Form
    {
        DbClass db = new DbClass();
        public string crdrtype = "";
        CommonMethod cm = new CommonMethod();

        public frmCustomerCerditDebitNoteList(string sType)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            customerdata();
            crdrtype = sType;
            TodaysList();
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo1 = new CustomerRepository();
                List<CustomerInformation> custdata = new List<CustomerInformation>();
                custdata.Clear();
                custdata = custrepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                custdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                custdata.Reverse();
                cmbCustomerName.DataSource = custdata;
                cmbCustomerName.ValueMember = "CustomerID";
                cmbCustomerName.DisplayMember = "CustomerName";
                custrepo1.Dispose();
                cmbCustomerName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void AutoCompleteBillNo()
        {
            try
            {
                txtBillNo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT BillNo FROM CustomerBillMaster where BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", con);
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
                    txtBillNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
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
                    SqlCommand cmd = new SqlCommand("SELECT CustCreditDebitNo FROM CustCreditDebitMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", con);
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

        private void frmCustomerCerditDebitNoteList_Load(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteBillNo();
                AutoCompleteCDNR();
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strsql = "";
                strsql = "Select Id,CustCreditDebitNo as 'CDN No',Convert(varchar(24),CustCreditDebitDate,103) as 'Credit Debit Date',BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',CustomerName as 'Customer Name',CDType as 'Credit Debit Note Type',TotalAmount as 'Total Amount' from CustCreditDebitMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CDType='" + crdrtype + "'";
                if (chkCustomerName.Checked == true)
                {
                    if (cmbCustomerName.SelectedIndex > 0)
                        strsql = strsql + "and CustomerName='" + Convert.ToString(cmbCustomerName.Text) + "' and CustomerId=" + Convert.ToInt32(cmbCustomerName.SelectedValue) + "";
                    else
                    {
                        MessageBox.Show("Select Customer Name.","Warning");
                        this.ActiveControl = cmbCustomerName;
                    }
                }
                if (chkBillNo.Checked == true)
                {
                    if (txtBillNo.Text != "")
                        strsql = strsql + "and BillNo='" + Convert.ToString(txtBillNo.Text) + "' ";
                    else
                    {
                        MessageBox.Show("Enter Bill No.","Warning");
                        this.ActiveControl = txtBillNo;
                    }
                }
                if (chkCreditDebitNo.Checked == true)
                {
                    if (txtCDNO.Text != "")
                        strsql = strsql + "and CustCreditDebitNo=" + Convert.ToInt32(txtCDNO.Text) + " ";
                    else
                    {
                        MessageBox.Show("Enter Credit Debit No.","Warning");
                        this.ActiveControl = txtCDNO;
                    }
                }
                if (chkBillDate.Checked == true)
                {
                    if (dtpFromDate.Value.Date <= dtpToDate.Value.Date)
                        strsql = strsql + "and  Convert(varchar(24),Billdate,103) between '" + dtpFromDate.Value.ToShortDateString() + "' and '" + dtpToDate.Value.ToShortDateString() + "'";
                    else
                    {
                        MessageBox.Show("To Date Should be Grater Than From Date.","Warning");
                        this.ActiveControl = dtpToDate;
                    }
                }
                if (chkCreditDebitNoteDate.Checked == true)
                {
                    if (dtpCDFrom.Value.Date <= dtpCDTo.Value.Date)
                        strsql = strsql + "and  Convert(varchar(24),CustCreditDebitDate,103) between '" + dtpCDFrom.Value.ToShortDateString() + "' and '" + dtpCDTo.Value.ToShortDateString() + "'";
                    else
                    {
                        MessageBox.Show("To Date Should be Greater Than From Date.","Warning");
                        this.ActiveControl = dtpToDate;
                    }
                }
                DataTable dt = new DataTable();
                dt = db.GetTable(strsql);
                GVCreditDebitList.DataSource = null;
                GVCreditDebitList.DataSource = dt;
                if (chkBillDate.Checked == false && chkBillNo.Checked == false && chkCreditDebitNo.Checked == false && chkCreditDebitNoteDate.Checked == false && chkCustomerName.Checked == false)
                {
                    MessageBox.Show("Please Select Check Box.","Warning");
                    this.ActiveControl = chkCustomerName;
                }
                else
                {
                    if (GVCreditDebitList.Rows.Count > 0)
                    {
                        GVCreditDebitList.Columns["Id"].Visible = false;
                        GVCreditDebitList.Columns["Customer Name"].Width = 200;
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

        public void TodaysList()
        {
            try
            {
                chkBillDate.Checked = false;                            chkBillNo.Checked = false;                  chkCreditDebitNo.Checked = false; 
                chkCreditDebitNoteDate.Checked = false;                 chkCustomerName.Checked = false;
                customerdata(); 
                dtpCDFrom.Value = DateTime.Now.Date;                    dtpCDTo.Value = DateTime.Now.Date; 
                dtpFromDate.Value = DateTime.Now.Date;                  dtpToDate.Value = DateTime.Now.Date;
                txtBillNo.Text = "";                                    txtCDNO.Text = "";                
                DataTable dt = new DataTable();
                dt = db.GetTable("Select Id,CustCreditDebitNo as 'CDN No',Convert(varchar(24),CustCreditDebitDate,103) as 'Credit Debit Date',BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',CustomerName as 'Customer Name',CDType as 'Credit Debit Note Type',TotalAmount as 'Total Amount' from CustCreditDebitMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'  and CDType='" + crdrtype + "' and Convert(varchar(24),CustCreditDebitDate,103)='" + DateTime.Now.ToShortDateString() + "'");
                GVCreditDebitList.DataSource = null;
                GVCreditDebitList.DataSource = dt;
                if (GVCreditDebitList.Rows.Count > 0)
                {
                    GVCreditDebitList.Columns["Id"].Visible = false;
                    GVCreditDebitList.Columns["Customer Name"].Width = 200;
                    GVCreditDebitList.Columns["Credit Debit Note Type"].Width = 150;
                    lblCnt.Text = Convert.ToString(GVCreditDebitList.Rows.Count);
                }
                else
                    lblCnt.Text = Convert.ToString(0);
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                chkBillDate.Checked = false;                            chkBillNo.Checked = false;                  chkCreditDebitNo.Checked = false; 
                chkCreditDebitNoteDate.Checked = false;                 chkCustomerName.Checked = false;
                customerdata(); dtpCDFrom.Value = DateTime.Now.Date;    dtpCDTo.Value = DateTime.Now.Date; 
                dtpFromDate.Value = DateTime.Now.Date;                  dtpToDate.Value = DateTime.Now.Date;
                txtBillNo.Text = "";                                    txtCDNO.Text = "";
                DataTable dt = new DataTable();
                dt = db.GetTable("Select Id,CustCreditDebitNo as 'CDN No',Convert(varchar(24),CustCreditDebitDate,103) as 'Credit Debit Date',BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',CustomerName as 'Customer Name',CDType as 'Credit Debit Type',TotalAmount as 'Total Amount' from CustCreditDebitMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'  and CDType='" + crdrtype + "'");
                GVCreditDebitList.DataSource = null;
                GVCreditDebitList.DataSource = dt;
                if (GVCreditDebitList.Rows.Count > 0)
                {
                    GVCreditDebitList.Columns["Id"].Visible = false;
                    GVCreditDebitList.Columns["Customer Name"].Width = 200;
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

        private void GVCreditDebitList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (frmCustomerCreditDebitNote.creditdebitnote == null)
                    {
                        frmCustomerCreditDebitNote custbill = new frmCustomerCreditDebitNote(Convert.ToString(GVCreditDebitList.Rows[e.RowIndex].Cells["Bill No"].Value), Convert.ToInt32(GVCreditDebitList.Rows[e.RowIndex].Cells["CDN No"].Value));
                        custbill.ShowDialog();
                        frmCustomerCreditDebitNote.creditdebitnote = null;
                    }
                    else
                    {
                        this.Close();
                        frmCustomerCreditDebitNote.creditdebitnote.billdata(Convert.ToString(GVCreditDebitList.Rows[e.RowIndex].Cells["Bill No"].Value));
                        frmCustomerCreditDebitNote.creditdebitnote.Show();
                        frmCustomerCreditDebitNote.creditdebitnote = null;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}