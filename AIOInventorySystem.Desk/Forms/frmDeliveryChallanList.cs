using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using System.Data.SqlClient;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmDeliveryChallanList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> billList = new List<int>();

        public frmDeliveryChallanList()
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

        public void Bindgrid()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[DeliveryChallanNo] As 'DC. NO',CONVERT(VARCHAR(24),DeliveryChallanDate,103) as 'DC. Date',[CustomerName]  FROM [DeliveryChallanMaster]where status=1 and CONVERT(VARCHAR(24),DeliveryChallanDate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                GvproductInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvproductInfo.Columns[0].Visible = false;
                else
                    GvproductInfo.Columns[0].Visible = true;
                GvproductInfo.Columns["DC. NO"].Width = 100;
                GvproductInfo.Columns["DC. Date"].Width = 115;
                GvproductInfo.Columns["CustomerName"].Width = 210;

                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                customerdata();
                dtpToPorderDate.Value = DateTime.Now.Date;
                dtpFromPorderdate.Value = DateTime.Now.Date;
                chkporderno.Checked = false;
                chkbetweendate.Checked = false;
                chkcompletedc.Checked = false;
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[DeliveryChallanNo] As 'DC. NO',[DeliveryChallanDate] as 'DC. Date',[CustomerName]  FROM [DeliveryChallanMaster]where status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                GvproductInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvproductInfo.Columns[0].Visible = false;
                else
                    GvproductInfo.Columns[0].Visible = true;
                GvproductInfo.Columns["DC. NO"].Width = 100;
                GvproductInfo.Columns["DC. Date"].Width = 115;
                GvproductInfo.Columns["CustomerName"].Width = 210;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkporderno.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                        da = new SqlDataAdapter("SELECT [ID],[DeliveryChallanNo] As 'DC. NO',[DeliveryChallanDate] as 'DC. Date',[CustomerName]  FROM [DeliveryChallanMaster] where CustomerId='" + cmbcustomername.SelectedValue + "' and Status= 1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        da = new SqlDataAdapter("SELECT [ID],[DeliveryChallanNo] As 'DC. NO',[DeliveryChallanDate] as 'DC. Date',[CustomerName]  FROM [DeliveryChallanMaster] where DeliveryChallanDate >= '" + com.ValidFromDate(dtpFromPorderdate.Value.Date) + "' and DeliveryChallanDate <='" + com.ValidToDate(dtpToPorderDate.Value.Date) + "' and Status = 1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpToPorderDate;
                    }
                }
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                if (GvproductInfo.Rows.Count != 0)
                {
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvproductInfo.Columns[0].Visible = false;
                    else
                        GvproductInfo.Columns[0].Visible = true;
                    GvproductInfo.Columns["ID"].Visible = false;
                    GvproductInfo.Columns["DC. NO"].Width = 100;
                    GvproductInfo.Columns["DC. Date"].Width = 115;
                    GvproductInfo.Columns["CustomerName"].Width = 210;
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
                {
                    chkporderno.Checked = false;
                    cmbcustomername.Text = "Select";
                }
            }
            catch (Exception)
            { }
        }

        private void chkcompletedc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkcompletedc.Checked == true)
                {
                    try
                    {
                        da = new SqlDataAdapter("SELECT [ID],[DeliveryChallanNo] As 'DC. NO',[DeliveryChallanDate] as 'DC. Date',[CustomerName]  FROM [DeliveryChallanMaster] where Status=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    }
                    catch (Exception)
                    { }
                }
                else if (chkcompletedc.Checked == false)
                {
                    try
                    {
                        da = new SqlDataAdapter("SELECT [ID],[DeliveryChallanNo] As 'DC. NO',[DeliveryChallanDate] as 'DC. Date',[CustomerName]  FROM [DeliveryChallanMaster] where Status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    }
                    catch (Exception)
                    { }
                }
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvproductInfo.Columns[0].Visible = false;
                else
                    GvproductInfo.Columns[0].Visible = true;
                GvproductInfo.Columns["ID"].Visible = false;
                GvproductInfo.Columns["DC. NO"].Width = 100;
                GvproductInfo.Columns["DC. Date"].Width = 115;
                GvproductInfo.Columns["CustomerName"].Width = 210;
                da.Dispose();
                dt.Dispose();
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

        private void GvproductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (frmDelieveryChallan.delichalleninfo == null)
                    {
                        frmDelieveryChallan frmDC = new frmDelieveryChallan(Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["ID"].Value));
                        frmDC.ShowDialog();
                        frmDelieveryChallan.delichalleninfo = null;
                    }
                    else
                    {
                        this.Close();
                        frmDelieveryChallan.delichalleninfo.GetChallanData(Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["ID"].Value));
                        frmDelieveryChallan.delichalleninfo.Show();
                        frmDelieveryChallan.delichalleninfo = null;
                    }
                    Bindgrid();
                }
                if (e.ColumnIndex == 2)
                {
                    if (chkcompletedc.Checked == true)
                    { }
                    else
                    {
                        int i = 0;
                        List<int> list = new List<int>();
                        if (cmbcustomername.SelectedIndex > 0)
                        {
                            DeliveryChallanRepository challanmasterrepo = new DeliveryChallanRepository();
                            List<DeliveryChallanMaster> MasterList = challanmasterrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Status == 1 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            foreach (var item in MasterList)
                            {
                                if (GvproductInfo.Rows[i].Cells[0].Value != null)
                                    list.Add(Convert.ToInt32(GvproductInfo.Rows[i].Cells["ID"].Value));
                                i++;
                            }
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
                                        frmCustomerBill srpt = new frmCustomerBill(Convert.ToInt32(input), "a", list);
                                        srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                        srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                        srpt.Show();
                                    }
                                    else
                                        goto a;
                                }
                            }
                            catch (Exception)
                            { }
                            challanmasterrepo.Dispose();
                        }
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
                                        frmCustomerBill srpt = new frmCustomerBill(Convert.ToInt32(input), "a", Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["ID"].Value));
                                        srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                        srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                        srpt.Show();
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
            }
            catch (Exception)
            { }
        }

        private void GvproductInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvproductInfo.CurrentCellAddress.X == 0)
                    {
                        frmDelieveryChallan pdata = new frmDelieveryChallan(Convert.ToInt32(GvproductInfo.Rows[GvproductInfo.CurrentCellAddress.Y].Cells["ID"].Value));
                        pdata.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        pdata.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        pdata.ShowDialog();
                    }
                    if (GvproductInfo.CurrentCellAddress.X == 1)
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
                                    frmCustomerBill srpt = new frmCustomerBill(Convert.ToInt32(input), "a", Convert.ToInt32(GvproductInfo.Rows[GvproductInfo.CurrentCellAddress.Y].Cells[2].Value));
                                    srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                    srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                    srpt.Show();
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
            catch (Exception)
            { }
        }

        private void frmDeliveryChallanList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmDelieveryChallan.delichalleninfo = null;
            }
            catch (Exception)
            { }
        }

        private void btntodaylist_Click(object sender, EventArgs e)
        {
            try
            {
                Bindgrid();
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

        private void frmDeliveryChallanList_Load(object sender, EventArgs e)
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

        private void chkporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpFromPorderdate;
            }
            catch (Exception)
            { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                foreach (DataGridViewRow row in GvproductInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(Convert.ToInt32(row.Cells[4].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[4].Value));
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
                    MessageBox.Show("Select at least one Delivery Challan");
                    this.ActiveControl = GvproductInfo;
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
                    int DCMId = 0;
                    decimal NetAmount = 0;
                    string DeletedBillNo = "";
                    DeliveryChallanRepository DCMRepo = new DeliveryChallanRepository();
                    var DCData = DCMRepo.GetAll().Where(t => t.DeliveryChallanNo == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                    if (DCData != null)
                    {
                        var DCData1 = DCMRepo.GetAll().Where(t => t.DeliveryChallanNo == billList[q] && t.CustomerName == Convert.ToString(DCData.CustomerName) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                        if (DCData1 != null)
                        {
                            DCMId = Convert.ToInt32(DCData1.Id);
                            // add details for history table
                            try
                            {
                                DeletedBillNo = Convert.ToString(DCData1.DeliveryChallanNo);
                                NetAmount = Convert.ToDecimal(0);
                                DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                DeleteHistoryTable historydata = new DeleteHistoryTable();
                                historydata.DeleteType = "Delivery Challan";
                                historydata.BillNo = DeletedBillNo;
                                historydata.SupplierOrCustomerName = Convert.ToString(DCData1.CustomerName);
                                historydata.Amount = Convert.ToDecimal(NetAmount);
                                historydata.CurrentDate = DateTime.Now.Date;
                                historydata.BillDate = Convert.ToDateTime(DCData.DeliveryChallanDate);
                                historydata.CompId = CommonMethod.CompId;
                                historydata.TransactionYear = CommonMethod.TransactionYear;
                                historyRepo.Add(historydata);
                                historyRepo.Save();
                            }
                            catch (Exception) { }
                            db.connect();
                            DataTable dt = db.GetTable("delete from DeliveryChallanDetail where DeliveryChallanNo='" + DCMId + "'");
                            DataTable dt1 = db.GetTable("delete from DeliveryChallanMaster where Id='" + DCMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                            MessageBox.Show("Delivery Challan " + DeletedBillNo + " is  deleted sucessfully.", "Success");
                            dt.Dispose();
                            dt1.Dispose();
                            db.CloseConnection();
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}