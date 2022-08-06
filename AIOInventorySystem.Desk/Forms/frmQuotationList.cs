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
    public partial class frmQuotationList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> billList = new List<int>();

        public frmQuotationList()
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',CONVERT(VARCHAR(24),Quotationdate,103) as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where Status=1 and CONVERT(VARCHAR(24),Quotationdate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                GvBillInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvBillInfo.Columns[0].Visible = false;
                else
                    GvBillInfo.Columns[0].Visible = false;
                GvBillInfo.Columns["Selectg"].Width = 30;
                GvBillInfo.Columns["Updateg"].Width = 50;
                GvBillInfo.Columns["CustBill"].Width = 50;
                GvBillInfo.Columns["Duplicate"].Width = 70;
                GvBillInfo.Columns["Quotation No"].Width = 60;
                GvBillInfo.Columns["Quotation Date"].Width = 80;
                GvBillInfo.Columns["CustomerName"].Width = 120;
                GvBillInfo.Columns["TotalAmount"].Width = 90;
                GvBillInfo.Columns["Discount"].Width = 60;
                GvBillInfo.Columns["NetAmount"].Width = 90;
                GvBillInfo.Columns["TransactionYear"].Visible = false;
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
                QuotationMasterRepository qmasterrepo = new QuotationMasterRepository();
                List<QuotationMaster> customerdata = new List<QuotationMaster>();
                customerdata.Clear();
                customerdata = qmasterrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new QuotationMaster { CustomerName = "Select", CustomerId = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerId";
                cmbcustomername.DisplayMember = "CustomerName";
                qmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkbillno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbillno.Checked == true)
                {
                    chkQuoNo.Checked = false;
                    chkQuoNo.Checked = false;
                    txtquotationNo.Text = "";
                    this.ActiveControl = cmbcustomername;
                }
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbillno.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',[Quotationdate] as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where CustomerName='" + cmbcustomername.Text + "' and Status=1 and CompId=" + CommonMethod.CompId + "", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvBillInfo.DataSource = dt;
                        GvBillInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                        if (GvBillInfo.Rows.Count != 0)
                        {
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvBillInfo.Columns[0].Visible = false;
                            else
                                GvBillInfo.Columns[0].Visible = false;
                            GvBillInfo.Columns["ID"].Visible = false;
                            GvBillInfo.Columns["Selectg"].Width = 30;
                            GvBillInfo.Columns["Updateg"].Width = 50;
                            GvBillInfo.Columns["CustBill"].Width = 50;
                            GvBillInfo.Columns["Duplicate"].Width = 70;
                            GvBillInfo.Columns["Quotation No"].Width = 60;
                            GvBillInfo.Columns["Quotation Date"].Width = 80;
                            GvBillInfo.Columns["CustomerName"].Width = 120;
                            GvBillInfo.Columns["TotalAmount"].Width = 90;
                            GvBillInfo.Columns["Discount"].Width = 60;
                            GvBillInfo.Columns["NetAmount"].Width = 90;
                            GvBillInfo.Columns["TransactionYear"].Visible = false;
                            this.ActiveControl = cmbcustomername;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                if (chkfromdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',[Quotationdate] as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where Quotationdate >= '" + dtpfromdate.Value.ToString("MM/dd/yyyy") + "' and Quotationdate <= '" + dtptodate.Value.ToString("MM/dd/yyyy") + "' and Status=1 and CompId=" + CommonMethod.CompId + "", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvBillInfo.DataSource = dt;
                        GvBillInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                        if (GvBillInfo.Rows.Count != 0)
                        {
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvBillInfo.Columns[0].Visible = false;
                            else
                                GvBillInfo.Columns[0].Visible = false;
                            GvBillInfo.Columns["ID"].Visible = false;
                            GvBillInfo.Columns["Selectg"].Width = 30;
                            GvBillInfo.Columns["Updateg"].Width = 50;
                            GvBillInfo.Columns["CustBill"].Width = 50;
                            GvBillInfo.Columns["Duplicate"].Width = 70;
                            GvBillInfo.Columns["Quotation No"].Width = 60;
                            GvBillInfo.Columns["Quotation Date"].Width = 80;
                            GvBillInfo.Columns["CustomerName"].Width = 120;
                            GvBillInfo.Columns["TotalAmount"].Width = 90;
                            GvBillInfo.Columns["Discount"].Width = 60;
                            GvBillInfo.Columns["NetAmount"].Width = 90;
                            GvBillInfo.Columns["TransactionYear"].Visible = false;
                            this.ActiveControl = dtpfromdate;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = dtpfromdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                if (chkQuoNo.Checked == true)
                {
                    if (txtquotationNo.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',[Quotationdate] as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where  QuotationNo='" + txtquotationNo.Text + "' and Status=1 and CompId=" + CommonMethod.CompId + "", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvBillInfo.DataSource = dt;
                        GvBillInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                        if (GvBillInfo.Rows.Count != 0)
                        {
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvBillInfo.Columns[0].Visible = false;
                            else
                                GvBillInfo.Columns[0].Visible = false;
                            GvBillInfo.Columns["ID"].Visible = false;
                            GvBillInfo.Columns["Selectg"].Width = 30;
                            GvBillInfo.Columns["Updateg"].Width = 50;
                            GvBillInfo.Columns["CustBill"].Width = 50;
                            GvBillInfo.Columns["Duplicate"].Width = 70;
                            GvBillInfo.Columns["Quotation No"].Width = 60;
                            GvBillInfo.Columns["Quotation Date"].Width = 80;
                            GvBillInfo.Columns["CustomerName"].Width = 120;
                            GvBillInfo.Columns["TotalAmount"].Width = 90;
                            GvBillInfo.Columns["Discount"].Width = 60;
                            GvBillInfo.Columns["NetAmount"].Width = 90;
                            GvBillInfo.Columns["TransactionYear"].Visible = false;
                            this.ActiveControl = dtpfromdate;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = dtpfromdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Quotation Number.", "Warning");
                        this.ActiveControl = txtquotationNo;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                customerdata();
                txtquotationNo.Text = "";
                chkbillno.CheckState = 0;
                chkQuoNo.CheckState = 0;
                chkfromdate.CheckState = 0;
                chkcompletedc.CheckState = 0;
            }
            catch (Exception)
            { }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',[Quotationdate] as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where Status=1 and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                GvBillInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                if (rbtnDeleteMultipleBill.Enabled == false)
                    GvBillInfo.Columns[0].Visible = false;
                else
                    GvBillInfo.Columns[0].Visible = false;
                GvBillInfo.Columns["ID"].Visible = false;
                GvBillInfo.Columns["Selectg"].Width = 30;
                GvBillInfo.Columns["Updateg"].Width = 50;
                GvBillInfo.Columns["CustBill"].Width = 50;
                GvBillInfo.Columns["Duplicate"].Width = 70;
                GvBillInfo.Columns["Quotation No"].Width = 60;
                GvBillInfo.Columns["Quotation Date"].Width = 80;
                GvBillInfo.Columns["CustomerName"].Width = 120;
                GvBillInfo.Columns["TotalAmount"].Width = 90;
                GvBillInfo.Columns["Discount"].Width = 60;
                GvBillInfo.Columns["NetAmount"].Width = 90;
                GvBillInfo.Columns["TransactionYear"].Visible = false;
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
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',[Quotationdate] as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where  Status=0 and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvBillInfo.DataSource = dt;
                    GvBillInfo.Refresh();
                    lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                    if (GvBillInfo.Rows.Count != 0)
                    {
                        GvBillInfo.Columns["ID"].Visible = false;
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvBillInfo.Columns[0].Visible = false;
                        else
                            GvBillInfo.Columns[0].Visible = false;
                        GvBillInfo.Columns["Selectg"].Width = 30;
                        GvBillInfo.Columns["Updateg"].Width = 50;
                        GvBillInfo.Columns["CustBill"].Width = 50;
                        GvBillInfo.Columns["Duplicate"].Width = 70;
                        GvBillInfo.Columns["Quotation No"].Width = 60;
                        GvBillInfo.Columns["Quotation Date"].Width = 80;
                        GvBillInfo.Columns["CustomerName"].Width = 120;
                        GvBillInfo.Columns["TotalAmount"].Width = 90;
                        GvBillInfo.Columns["Discount"].Width = 60;
                        GvBillInfo.Columns["NetAmount"].Width = 90;
                        GvBillInfo.Columns["TransactionYear"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Record not found.", "Warning");
                        this.ActiveControl = chkcompletedc;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { }
            }
            if (chkcompletedc.Checked == false)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[QuotationNo] As 'Quotation No',[Quotationdate] as 'Quotation Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransactionYear] FROM [QuotationMaster] where  Status=1 and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvBillInfo.DataSource = dt;
                    GvBillInfo.Refresh();
                    lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                    if (GvBillInfo.Rows.Count != 0)
                    {
                        GvBillInfo.Columns["ID"].Visible = false;
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvBillInfo.Columns[0].Visible = false;
                        else
                            GvBillInfo.Columns[0].Visible = false;
                        GvBillInfo.Columns["Selectg"].Width = 30;
                        GvBillInfo.Columns["Updateg"].Width = 50;
                        GvBillInfo.Columns["CustBill"].Width = 50;
                        GvBillInfo.Columns["Duplicate"].Width = 70;
                        GvBillInfo.Columns["Quotation No"].Width = 60;
                        GvBillInfo.Columns["Quotation Date"].Width = 80;
                        GvBillInfo.Columns["CustomerName"].Width = 120;
                        GvBillInfo.Columns["TotalAmount"].Width = 90;
                        GvBillInfo.Columns["Discount"].Width = 60;
                        GvBillInfo.Columns["NetAmount"].Width = 90;
                        GvBillInfo.Columns["TransactionYear"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Record not found.", "Warning");
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

        private void GvBillInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (chkcompletedc.Checked == true)
                {
                    if (e.ColumnIndex == 1 && Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[11].Value) == CommonMethod.TransactionYear)
                    {
                        if (frmQuotation.quainfo == null)
                        {
                            frmQuotation qua = new frmQuotation(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value));
                            qua.ShowDialog();
                            frmQuotation.quainfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmQuotation.quainfo.QData(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value));
                            frmQuotation.quainfo.Show();
                            frmQuotation.quainfo = null;
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        if (frmQuotation.quainfo == null)
                        {
                            frmQuotation qua = new frmQuotation(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value), "md");
                            qua.ShowDialog();
                            frmQuotation.quainfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmQuotation.quainfo.QData(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value), "md");
                            frmQuotation.quainfo.Show();
                            frmQuotation.quainfo = null;
                        }
                    }
                }
                else
                {
                    if (e.ColumnIndex == 1 && Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[11].Value) == CommonMethod.TransactionYear)
                    {
                        if (frmQuotation.quainfo == null)
                        {
                            frmQuotation qua = new frmQuotation(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value));
                            qua.ShowDialog();
                            frmQuotation.quainfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmQuotation.quainfo.QData(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value));
                            frmQuotation.quainfo.Show();
                            frmQuotation.quainfo = null;
                        }
                    }
                    else if (e.ColumnIndex == 2)
                    {
                        try
                        {
                        a:
                            {
                                string input = "";
                                int type = 1;
                                input = Microsoft.VisualBasic.Interaction.InputBox("", "", "" + type + "", 500, 250);
                                if (input == "")
                                {
                                }
                                else if (input == "1" || input == "2" || input == "3")
                                {
                                    type = Convert.ToInt32(input);
                                    decimal billno = Convert.ToDecimal(GvBillInfo.Rows[e.RowIndex].Cells[5].Value);
                                    string qyear = Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[11].Value);
                                    QuotationMasterRepository BillMRepo = new QuotationMasterRepository();
                                    QuotationMaster BillM = new QuotationMaster();
                                    BillM = BillMRepo.GetAll().Where(t => t.Id == Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (BillM != null)
                                    {
                                        if (type == 1)
                                        {
                                            frmCustomerBill srpt = new frmCustomerBill(type, "a", billno, qyear);
                                            srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                            srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                            srpt.Show();
                                        }
                                        else
                                        {
                                            if (Convert.ToDecimal(BillM.Vat) == 0)
                                            {
                                                frmCustomerBill srpt = new frmCustomerBill(type, "a", billno, qyear);
                                                srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                                srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                                srpt.Show();
                                            }
                                            else
                                            {
                                                MessageBox.Show("This Order Having vat Record.", "Warning");
                                                goto a;
                                            }
                                        }
                                    }
                                    BillMRepo.Dispose();
                                }
                                else
                                {
                                    goto a;
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (frmQuotation.quainfo == null)
                        {
                            frmQuotation qua = new frmQuotation(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value), "md");
                            qua.ShowDialog();
                            frmQuotation.quainfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmQuotation.quainfo.QData(Convert.ToInt32(GvBillInfo.Rows[e.RowIndex].Cells["ID"].Value), "md");
                            frmQuotation.quainfo.Show();
                            frmQuotation.quainfo = null;
                        }
                    }
                }
                if (e.ColumnIndex != 0)
                    Bindgrid();
            }
            catch (Exception)
            { }
        }

        private void GvBillInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvBillInfo.CurrentCellAddress.X == 1)
                    {
                        frmQuotation custbillform = new frmQuotation(Convert.ToInt32(GvBillInfo.Rows[GvBillInfo.CurrentCellAddress.Y].Cells["ID"].Value));
                        custbillform.ShowDialog();
                    }
                    else if (GvBillInfo.CurrentCellAddress.X == 2)
                    {
                        decimal billno = Convert.ToDecimal(GvBillInfo.Rows[GvBillInfo.CurrentCellAddress.Y].Cells["ID"].Value);
                    }
                    else if (GvBillInfo.CurrentCellAddress.X == 3)
                    {
                        frmQuotation qua = new frmQuotation(Convert.ToInt32(GvBillInfo.Rows[GvBillInfo.CurrentCellAddress.Y].Cells["ID"].Value), "md");
                        qua.ShowDialog();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmQuotationList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmQuotation.quainfo = null;
            }
            catch (Exception)
            { }
        }

        private void btntodaylist_Click(object sender, EventArgs e)
        {
            try
            {
                txtquotationNo.Text = "";
                chkbillno.CheckState = 0;
                chkQuoNo.CheckState = 0;
                chkfromdate.CheckState = 0;
                chkcompletedc.CheckState = 0;
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
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtptodate;
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

        private void frmQuotationList_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void chkfromdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkfromdate.Checked == true)
                {
                    chkbillno.Checked = false;
                    chkQuoNo.Checked = false;
                    txtquotationNo.Text = "";
                    customerdata();
                    cmbcustomername.Text = "Select";
                }
            }
            catch (Exception)
            { }
        }

        private void chkQuoNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkQuoNo.Checked == true)
                {
                    chkbillno.Checked = false;
                    chkfromdate.Checked = false;
                    customerdata();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidCustomer(cmbcustomername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void txtquotationNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void chkfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        private void chkQuoNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtquotationNo;
            }
            catch (Exception)
            { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                foreach (DataGridViewRow row in GvBillInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(Convert.ToInt32(row.Cells[5].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[5].Value));
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
                    this.ActiveControl = GvBillInfo;
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
                    int QutMId = 0;
                    QuotationMasterRepository QutMastRepo = new QuotationMasterRepository();
                    var QuotationData = QutMastRepo.GetAll().Where(t => t.QuotationNo == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                    if (QuotationData != null)
                    {
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(QuotationData.QuotationNo);
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Quotation";
                            historydata.BillNo = DeletedBillNo;
                            historydata.SupplierOrCustomerName = Convert.ToString(QuotationData.CustomerName);
                            historydata.Amount = Convert.ToDecimal(QuotationData.NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(QuotationData.Quotationdate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();

                        }
                        catch (Exception) { }
                        QutMId = Convert.ToInt32(QuotationData.Id);
                        DataTable dt = db.GetTable("delete from QuotationDetail where QuotationNo='" + QutMId + "' ");
                        DataTable dt1 = db.GetTable("delete from QuotationMaster where Id='" + QutMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Quotation '" + DeletedBillNo + "' is deleted sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}