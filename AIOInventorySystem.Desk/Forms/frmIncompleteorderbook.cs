using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmIncompleteorderbook : Form
    {
        DbClass db = new DbClass();
        List<int> billList = new List<int>();
        CommonMethod cm = new CommonMethod();

        public frmIncompleteorderbook()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.DeleteSaleBill == "True")
                rbtnDeleteMultipleBill.Enabled = true;
            else
                rbtnDeleteMultipleBill.Enabled = false;
            Bindgrid();
            customerdata();
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Bindgrid()
        {
            try
            {
                string currentdate = DateTime.Now.ToShortDateString();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[Orderbookno] As 'Or.Book No',CONVERT(VARCHAR(24),Orderbookdate,103) as 'Or. Book Date',[Customername],[Delieverydate]  FROM [OrderBook] where Status=1 and CONVERT(VARCHAR(24),Orderbookdate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvorderInfo.DataSource = dt;
                GvorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvorderInfo.Rows.Count);
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvorderInfo.Columns[0].Visible = false;
                else
                    GvorderInfo.Columns[0].Visible = true;
                GvorderInfo.Columns["ID"].Visible = false;
                GvorderInfo.Columns["Or.Book No"].Width = 100;
                GvorderInfo.Columns["Or. Book Date"].Width = 100;
                GvorderInfo.Columns["Customername"].Width = 200;
                GvorderInfo.Columns["Delieverydate"].Width = 125;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkcustname_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcustname.Checked == true)
            {
                chkorderno.Checked = false;
                txtorderno.Text = "";
            }
        }

        private void chkorderno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkorderno.Checked == true)
            {
                chkcustname.Checked = false;
                cmbcustomername.Text = "Select";
            }
        }

        public void searchlist()
        {
            string strsql = "";
            try
            {
                strsql = "SELECT [ID],[Orderbookno] As 'Or.Book No',Convert(varchar(24),Orderbookdate,103) as 'Or. Book Date',[Customername],[Delieverydate]  FROM [OrderBook] where Status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "";
                if (chkcustname.Checked == true)
                {
                    if (cmbcustomername.Text != "")
                        strsql = strsql + "and CustomerId='" + cmbcustomername.SelectedValue + "'";
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                if (chkorderno.Checked == true)
                {
                    if (txtorderno.Text != "")
                            strsql = strsql + "and Orderbookno='" + txtorderno.Text + "'";                        
                    else
                    {
                        MessageBox.Show("Enter Order Booking Number.", "Warning");
                        this.ActiveControl = txtorderno;
                    }
                }
                if (chkDeliverydate.Checked == true)
                {
                    if (dtpdeliveryfromdate.Value.Date <= dtpdeliverytodate.Value.Date)
                    {
                        CommonMethod cm = new CommonMethod();
                        strsql = strsql + "and Orderbookdate>='" + cm.ValidFromDate(dtpdeliveryfromdate.Value.Date) + "' and Orderbookdate<='" + cm.ValidToDate(dtpdeliverytodate.Value.Date) + "'";
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                        this.ActiveControl = dtpdeliverytodate;
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvorderInfo.DataSource = dt;
                GvorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvorderInfo.Rows.Count);
                if (GvorderInfo.Rows.Count != 0)
                {
                    GvorderInfo.Columns["ID"].Visible = false;
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvorderInfo.Columns[0].Visible = false;
                    else
                        GvorderInfo.Columns[0].Visible = true;
                    GvorderInfo.Columns["Or.Book No"].Width = 100;
                    GvorderInfo.Columns["Or. Book Date"].Width = 100;
                    GvorderInfo.Columns["Customername"].Width = 200;
                    GvorderInfo.Columns["Delieverydate"].Width = 125;
                    this.ActiveControl = cmbcustomername;
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Warning");
                    this.ActiveControl = cmbcustomername;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkcompletedc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcompletedc.Checked == true)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[Orderbookno] As 'Or.Book No',Convert(varchar(24),Orderbookdate,103) as 'Or. Book Date',[Customername],[Delieverydate]  FROM [OrderBook] where Status=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvorderInfo.DataSource = dt;
                    GvorderInfo.Refresh();
                    lblTotalBill.Text = Convert.ToString(GvorderInfo.Rows.Count);
                    if (GvorderInfo.Rows.Count != 0)
                    {
                        GvorderInfo.Columns["ID"].Visible = false;
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvorderInfo.Columns[0].Visible = false;
                        else
                            GvorderInfo.Columns[0].Visible = true;
                        GvorderInfo.Columns["Or.Book No"].Width = 100;
                        GvorderInfo.Columns["Or. Book Date"].Width = 100;
                        GvorderInfo.Columns["Customername"].Width = 200;
                        GvorderInfo.Columns["Delieverydate"].Width = 125;
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found.", "Warning");
                        this.ActiveControl = chkcompletedc;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }
            }
            else if (chkcompletedc.Checked == false)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[Orderbookno] As 'Or.Book No',[Orderbookdate] as 'Or. Book Date',[Customername],[Delieverydate]  FROM [OrderBook] where Status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvorderInfo.DataSource = dt;
                    GvorderInfo.Refresh();
                    lblTotalBill.Text = Convert.ToString(GvorderInfo.Rows.Count);
                    if (GvorderInfo.Rows.Count != 0)
                    {
                        GvorderInfo.Columns["ID"].Visible = false;
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvorderInfo.Columns[0].Visible = false;
                        else
                            GvorderInfo.Columns[0].Visible = true;
                        GvorderInfo.Columns["Or.Book No"].Width = 100;
                        GvorderInfo.Columns["Or. Book Date"].Width = 150;
                        GvorderInfo.Columns["Customername"].Width = 200;
                        GvorderInfo.Columns["Delieverydate"].Width = 135;
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found.", "Warning");
                        this.ActiveControl = chkcompletedc;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }
            }
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

        private void GvorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvorderInfo.Columns[e.ColumnIndex].HeaderText == "Delete Booking No")
                {
                    try
                    {
                        if ((MessageBox.Show("Are you sure to Delete this Order Booking ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            OrderbookingRepository orderRepo = new OrderbookingRepository();
                            var orderdata = orderRepo.GetAll().Where(t => t.Id == Convert.ToInt32(GvorderInfo.Rows[e.RowIndex].Cells["ID"].Value) && t.Customername == Convert.ToString(GvorderInfo.Rows[e.RowIndex].Cells["Customername"].Value) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.status == "1").FirstOrDefault();
                            if (orderdata != null)
                            {
                                // add details for history table
                                try
                                {
                                    DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                    DeleteHistoryTable historydata = new DeleteHistoryTable();
                                    historydata.DeleteType = "Order Booking";
                                    historydata.BillNo = Convert.ToString(orderdata.Orderbookno);
                                    historydata.SupplierOrCustomerName = Convert.ToString(orderdata.Customername);
                                    historydata.Amount = Convert.ToDecimal(0);
                                    historydata.CurrentDate = DateTime.Now.Date;
                                    historydata.BillDate = Convert.ToDateTime(orderdata.Orderbookdate);
                                    historydata.CompId = CommonMethod.CompId;
                                    historydata.TransactionYear = CommonMethod.TransactionYear;
                                    historyRepo.Add(historydata);
                                    historyRepo.Save();
                                }
                                catch (Exception) { }
                                int OrderId = Convert.ToInt32(orderdata.Id);
                                db.connect();
                                DataTable dt = db.GetTable("delete from OrderBookdetail where OrderNo='" + OrderId + "'");
                                DataTable dt1 = db.GetTable("delete from OrderBook where Id='" + OrderId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                MessageBox.Show("Record Deleted Sucessfully.", "Success");
                                dt.Dispose();
                                dt1.Dispose();
                                db.CloseConnection();
                            }
                            else
                                MessageBox.Show("This order is convert in Invoice.", "Warning");
                            orderRepo.Dispose();
                        }
                    }
                    catch (Exception) { }
                }
                if (GvorderInfo.Columns[e.ColumnIndex].HeaderText == "Customer Bill")
                {
                    if (chkcompletedc.Checked == true)
                    { }
                    else
                    {
                        try
                        {
                        a:
                            {
                                string input = "";
                                input = Microsoft.VisualBasic.Interaction.InputBox("", "", "1", 500, 250);
                                if (input == "")
                                { }
                                else if (input == "1" || input == "2" || input == "3")
                                {
                                    frmCustomerBill pdata = new frmCustomerBill(Convert.ToInt32(GvorderInfo.Rows[e.RowIndex].Cells["ID"].Value), Convert.ToInt32(input));
                                    pdata.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                    pdata.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                    pdata.ShowDialog();
                                }
                                else
                                    goto a;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
                Bindgrid();
            }
            catch (Exception)
            { }
        }

        private void GvorderInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvorderInfo.CurrentCellAddress.X == 0)
                    {
                        if (chkcompletedc.Checked == true)
                        { }
                        else
                        {
                            try
                            {
                            a:
                                {
                                    string input = "";
                                    input = Microsoft.VisualBasic.Interaction.InputBox("", "", "1", 500, 250);
                                    if (input == "")
                                    { }
                                    else if (input == "1" || input == "2" || input == "3")
                                    {
                                        frmCustomerBill pdata = new frmCustomerBill(Convert.ToInt32(GvorderInfo.Rows[GvorderInfo.CurrentCellAddress.Y].Cells["ID"].Value), Convert.ToInt32(input));
                                        pdata.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                        pdata.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                        pdata.ShowDialog();
                                    }
                                    else
                                        goto a;
                                }
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void txtorderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void frmIncompleteorderbook_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (Convert.ToInt32(cm.ValidCustomer(cmbcustomername.Text)) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void btndelivery_Click(object sender, EventArgs e)
        {
            try
            {
                string currentdate = DateTime.Now.ToShortDateString();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[Orderbookno] As 'Or.Book No',CONVERT(VARCHAR(24),Orderbookdate,103) as 'Or. Book Date',[Customername],[Delieverydate]  FROM [OrderBook] where Status=1 and CONVERT(VARCHAR(24),Delieverydate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvorderInfo.DataSource = dt;
                GvorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvorderInfo.Rows.Count);
                GvorderInfo.Columns["ID"].Visible = false;
                GvorderInfo.Columns["Or.Book No"].Width = 100;
                GvorderInfo.Columns["Or. Book Date"].Width = 150;
                GvorderInfo.Columns["Customername"].Width = 200;
                GvorderInfo.Columns["Delieverydate"].Width = 135;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkcustname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void dtpdeliveryfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpdeliverytodate;
            }
            catch (Exception)
            { }
        }

        private void dtpdeliverytodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chkDeliverydate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpdeliveryfromdate;
            }
            catch (Exception)
            { }
        }

        private void chkorderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtorderno;
            }
            catch (Exception)
            { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                foreach (DataGridViewRow row in GvorderInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(Convert.ToInt32(row.Cells[3].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[3].Value));
                        }
                    }
                }
                if (billList.Count != 0)
                {
                    DeleteMultipleBill(billList);
                    this.ActiveControl = cmbcustomername;
                    Bindgrid();
                }
                else
                {
                    MessageBox.Show("Select at least one Order Booking");
                    this.ActiveControl = GvorderInfo;
                }
            }
            catch (Exception) { }
        }

        public void DeleteMultipleBill(List<int> billList)
        {
            try
            {
                for (int q = 0; q < billList.Count; q++)
                {
                    string DeletedBillNo = "";
                    OrderbookingRepository orderRepo = new OrderbookingRepository();
                    var orderdata = orderRepo.GetAll().Where(t => t.Id == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.status == "1").FirstOrDefault();
                    if (orderdata != null)
                    {
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(orderdata.Orderbookno);
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Order Booking";
                            historydata.BillNo = Convert.ToString(orderdata.Orderbookno);
                            historydata.SupplierOrCustomerName = Convert.ToString(orderdata.Customername);
                            historydata.Amount = Convert.ToDecimal(0);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(orderdata.Orderbookdate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }
                        int OrderId = Convert.ToInt32(orderdata.Id);
                        db.connect();
                        DataTable dt = db.GetTable("delete from OrderBookdetail where OrderNo='" + OrderId + "'");
                        DataTable dt1 = db.GetTable("delete from OrderBook where Id='" + OrderId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Order Booking No " + DeletedBillNo + " is deleted sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {
                searchlist();
            }
            catch (Exception)
            { }
        }

        private void btnTodaysList_Click(object sender, EventArgs e)
        {
            try
            {
                Bindgrid();
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                customerdata();
                chkcustname.Checked = false;
                chkorderno.Checked = false;
                chkDeliverydate.Checked = false;
                txtorderno.Text = "";
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[Orderbookno] As 'Or.Book No',[Orderbookdate] as 'Or. Book Date',[Customername],[Delieverydate]  FROM [OrderBook] where Status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvorderInfo.DataSource = dt;
                GvorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvorderInfo.Rows.Count);
                GvorderInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvorderInfo.Columns[0].Visible = false;
                else
                    GvorderInfo.Columns[0].Visible = true;
                GvorderInfo.Columns["Or.Book No"].Width = 100;
                GvorderInfo.Columns["Or. Book Date"].Width = 150;
                GvorderInfo.Columns["Customername"].Width = 200;
                GvorderInfo.Columns["Delieverydate"].Width = 135;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }
    }
}