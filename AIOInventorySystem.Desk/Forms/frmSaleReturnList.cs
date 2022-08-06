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
    public partial class frmSaleReturnList : Form
    {
        CommonMethod cm = new CommonMethod();
        List<int> billList = new List<int>();
        DbClass db = new DbClass();
        int productentrytype;

        public frmSaleReturnList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillsupplier();
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.DeleteSaleBill == "True")
                rbtnDeleteMultipleBill.Enabled = true;
            else
                rbtnDeleteMultipleBill.Enabled = false;
            Bindgrid();
        }

        public frmSaleReturnList(DateTime from, DateTime to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillsupplier();
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.DeleteSaleBill == "True")
                rbtnDeleteMultipleBill.Enabled = true;
            else
                rbtnDeleteMultipleBill.Enabled = false;
            Bindgrid(from, to);
        }

        public void Bindgrid(DateTime from, DateTime to)
        {
            try
            {
                GoodMasterRepository SRMRepo = new GoodMasterRepository();
                List<GoodReturnMaster> ItemList = SRMRepo.GetAll().Where(t => t.GoodReturnDate.Value.Date >= from.Date && t.GoodReturnDate.Value.Date <= to.Date && t.BillNo.StartsWith("T") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count > 0)
                {
                    dtgvList.Rows.Clear();
                    int i = 0;
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        dtgvList.Columns["Selectg"].Visible = false;
                    else
                        dtgvList.Columns["Selectg"].Visible = true;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["SaleReturnNo"].Value = item.GoodReturnNo;
                        dtgvList.Rows[i].Cells["SaleReturnDate"].Value = item.GoodReturnDate.Value.ToShortDateString();
                        dtgvList.Rows[i].Cells["BillNo"].Value = item.BillNo;
                        dtgvList.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                        dtgvList.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                        dtgvList.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                    this.ActiveControl = dtpFromPorderdate;
                }
                SRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Bindgrid()
        {
            try
            {
                GoodMasterRepository SRMRepo = new GoodMasterRepository();
                List<GoodReturnMaster> ItemList = SRMRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList != null)
                {
                    dtgvList.Rows.Clear();
                    int i = 0;
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        dtgvList.Columns["Selectg"].Visible = false;
                    else
                        dtgvList.Columns["Selectg"].Visible = true;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["SaleReturnNo"].Value = item.GoodReturnNo;
                        dtgvList.Rows[i].Cells["SaleReturnDate"].Value = item.GoodReturnDate.Value.ToShortDateString();
                        dtgvList.Rows[i].Cells["BillNo"].Value = item.BillNo;
                        dtgvList.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                        dtgvList.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                        dtgvList.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                SRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillsupplier()
        {
            try
            {
                CustomerRepository CustRepo = new CustomerRepository();
                List<CustomerInformation> custdata = new List<CustomerInformation>();
                custdata.Clear();
                custdata = CustRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                custdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                custdata.Reverse();
                cmbcustomername.DataSource = custdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                CustRepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void chkporderno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkporderno.Checked == true)
                {
                    chkbetweendate.Checked = false;
                    chksname.Checked = false;
                    fillsupplier();
                    checkboxbillno.Checked = false;
                }
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
                    chksname.Checked = false;
                    fillsupplier();
                    checkboxbillno.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dtpFromPorderdate.Value = DateTime.Now.Date;
                    dtpToPorderDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtsrno.Text = "";
                chkporderno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                Bindgrid();
                fillsupplier();
                chksname.CheckState = 0;
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkporderno.Checked == true)
                {
                    if (txtsrno.Text != "")
                    {
                        try
                        {
                            dtpFromPorderdate.Value = DateTime.Now.Date;
                            dtpToPorderDate.Value = DateTime.Now.Date;
                        }
                        catch (Exception)
                        { }
                        GoodMasterRepository SRMRepo = new GoodMasterRepository();
                        List<GoodReturnMaster> ItemList = SRMRepo.GetAll().Where(t => t.GoodReturnNo == Convert.ToInt32(txtsrno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            dtgvList.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                dtgvList.Columns["Selectg"].Visible = false;
                            else
                                dtgvList.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                dtgvList.Rows.Add();
                                dtgvList.Rows[i].Cells["SaleReturnNo"].Value = item.GoodReturnNo;
                                dtgvList.Rows[i].Cells["SaleReturnDate"].Value = item.GoodReturnDate.Value.ToShortDateString();
                                dtgvList.Rows[i].Cells["BillNo"].Value = item.BillNo;
                                dtgvList.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                                dtgvList.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                dtgvList.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = txtsrno;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            dtgvList.Rows.Clear();
                            this.ActiveControl = txtsrno;
                        }
                        SRMRepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Sale Return No.", "Warning");
                        this.ActiveControl = txtsrno;
                    }
                }
                else if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                    {
                        txtsrno.Text = "";
                        GoodMasterRepository SRMRepo = new GoodMasterRepository();
                        List<GoodReturnMaster> ItemList = SRMRepo.GetAll().Where(t => t.GoodReturnDate.Value.Date >= dtpFromPorderdate.Value.Date && t.GoodReturnDate.Value.Date <= dtpToPorderDate.Value.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            dtgvList.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                dtgvList.Columns["Selectg"].Visible = false;
                            else
                                dtgvList.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                dtgvList.Rows.Add();
                                dtgvList.Rows[i].Cells["SaleReturnNo"].Value = item.GoodReturnNo;
                                dtgvList.Rows[i].Cells["SaleReturnDate"].Value = item.GoodReturnDate.Value.ToShortDateString();
                                dtgvList.Rows[i].Cells["BillNo"].Value = item.BillNo;
                                dtgvList.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                                dtgvList.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                dtgvList.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            dtgvList.Rows.Clear();
                            this.ActiveControl = dtpFromPorderdate;

                        }
                        SRMRepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpFromPorderdate;
                    }
                }
                else if (chksname.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        try
                        {
                            dtpFromPorderdate.Value = DateTime.Now.Date;
                            dtpToPorderDate.Value = DateTime.Now.Date;
                        }
                        catch (Exception)
                        { }

                        GoodMasterRepository SRMRepo = new GoodMasterRepository();
                        List<GoodReturnMaster> ItemList = SRMRepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            dtgvList.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                dtgvList.Columns["Selectg"].Visible = false;
                            else
                                dtgvList.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                dtgvList.Rows.Add();
                                dtgvList.Rows[i].Cells["SaleReturnNo"].Value = item.GoodReturnNo;
                                dtgvList.Rows[i].Cells["SaleReturnDate"].Value = item.GoodReturnDate.Value.ToShortDateString();
                                dtgvList.Rows[i].Cells["BillNo"].Value = item.BillNo;
                                dtgvList.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                                dtgvList.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                dtgvList.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = chksname;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            dtgvList.Rows.Clear();
                            this.ActiveControl = chksname;
                        }
                        SRMRepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = chksname;
                        this.ActiveControl = cmbcustomername;
                    }
                }
                else if (checkboxbillno.Checked == true)
                {
                    if (txtbillno.Text != "")
                    {
                        try
                        {
                            dtpFromPorderdate.Value = DateTime.Now.Date;
                            dtpToPorderDate.Value = DateTime.Now.Date;
                        }
                        catch (Exception)
                        { }
                        GoodMasterRepository SRMRepo = new GoodMasterRepository();
                        List<GoodReturnMaster> ItemList = SRMRepo.GetAll().Where(t => t.BillNo == txtbillno.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            dtgvList.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                dtgvList.Columns["Selectg"].Visible = false;
                            else
                                dtgvList.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                dtgvList.Rows.Add();
                                dtgvList.Rows[i].Cells["SaleReturnNo"].Value = item.GoodReturnNo;
                                dtgvList.Rows[i].Cells["SaleReturnDate"].Value = item.GoodReturnDate.Value.ToShortDateString();
                                dtgvList.Rows[i].Cells["BillNo"].Value = item.BillNo;
                                dtgvList.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                                dtgvList.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                dtgvList.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = txtbillno;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            dtgvList.Rows.Clear();
                            this.ActiveControl = txtbillno;
                        }
                        SRMRepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Bill No.", "Warning");
                        this.ActiveControl = txtbillno;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void chksname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chksname.Checked == true)
                {
                    chkporderno.Checked = false;
                    chkbetweendate.Checked = false;
                    fillsupplier();
                    checkboxbillno.Checked = false;
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

        private void checkboxbillno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkboxbillno.Checked)
                {
                    chkporderno.Checked = false;
                    chkbetweendate.Checked = false;
                    chksname.Checked = false;
                    fillsupplier();
                }
            }
            catch (Exception)
            { }
        }

        private void frmSaleReturnList_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                //BillNo Binding to textbox
                txtbillno.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT BillNo FROM GoodReturnMaster where TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        { MyCollection.Add(reader.GetString(0)); }
                        catch (Exception)
                        { }
                    }
                    txtbillno.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }

                //SR No Binding to textbox
                txtsrno.Text = "";
                string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString2))
                {
                    SqlCommand cmd = new SqlCommand("SELECT GoodReturnNo FROM GoodReturnMaster where TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        { MyCollection.Add(reader.GetString(0)); }
                        catch (Exception)
                        { }
                    }
                    txtsrno.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtsrno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
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

        private void txtbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbuser_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = btnSearch;
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
                    fillsupplier();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void chkporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtsrno;
            }
            catch (Exception)
            { }
        }

        private void chksname_KeyDown(object sender, KeyEventArgs e)
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

        private void checkboxbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtbillno;
            }
            catch (Exception)
            { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                for (int i = 0; i < dtgvList.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtgvList.Rows[i].Cells["Selectg"].Value) == true)
                        billList.Add(Convert.ToInt32(dtgvList.Rows[i].Cells["SaleReturnNo"].Value));
                }
                if (billList.Count != 0)
                {
                    DeleteMultiplebill(billList);
                    this.ActiveControl = txtbillno;
                    Bindgrid();
                }
                else
                {
                    MessageBox.Show("Please select at least one bill!!");
                    this.ActiveControl = dtgvList;
                }

            }
            catch (Exception) { }
        }

        public void DeleteMultiplebill(List<int> billList)
        {
            try
            {
                for (int q = 0; q < billList.Count; q++)
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", alternateunit = "", custBillNo = "", Suppliername = "", DeletedBillNo = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, NetAmount = 0;
                    GoodMasterRepository salereturnRepo = new GoodMasterRepository();
                    var salereturndata = salereturnRepo.GetAll().Where(t => t.GoodReturnNo == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (salereturndata != null)
                    {
                        custBillNo = salereturndata.BillNo;
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(salereturndata.GoodReturnNo);
                            Suppliername = Convert.ToString(salereturndata.CustomerName);
                            NetAmount = Convert.ToDecimal(salereturndata.NetAmount);

                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Sale Return";
                            historydata.BillNo = DeletedBillNo;
                            historydata.SupplierOrCustomerName = Suppliername;
                            historydata.Amount = Convert.ToDecimal(NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(salereturndata.GoodReturnDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }
                        int SReturnId = salereturnRepo.GetAll().Where(t => t.BillNo == Convert.ToString(custBillNo) && t.CustomerName == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Billdate.Value.ToShortDateString() == salereturndata.Billdate.Value.ToShortDateString()).FirstOrDefault().Id;
                        RemainingPaymentRepository RemPaymentRepo = new RemainingPaymentRepository();
                        var remainingData = RemPaymentRepo.GetAll().Where(t => t.BillId == Convert.ToString(custBillNo) && t.Customername == Convert.ToString(Suppliername) && t.Type == "BillReturn" && t.PaidAmt != 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (remainingData != null)
                        {
                            int ReceiptId = RemPaymentRepo.GetAll().Where(t => t.BillId == Convert.ToString(custBillNo) && t.Customername == Convert.ToString(Suppliername) && t.Type == "BillReturn" && t.PaidAmt != 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;
                            RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                            RemainingPayment remdata = remRepo.GetById(ReceiptId);
                            remRepo.Remove(remdata);
                            remRepo.Save();

                            decimal decremamt = 0;
                            RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                            RemainingPayment remainingdata1 = new RemainingPayment();
                            remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id < remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remainingdata1 != null)
                                decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                            else
                                decremamt = 0;
                            RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                            List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id > remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            int i = 0;
                            foreach (var item in remainingdata2)
                            {
                                decimal decnetpaidamt = 0;
                                decnetpaidamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt);
                                item.RemainingAmt = decnetpaidamt + decremamt;
                                decremamt = decnetpaidamt + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                        }
                        GoodDetailRepository saledetailRepo = new GoodDetailRepository();
                        List<GoodReturnDetail> SDtdata = saledetailRepo.GetAll().Where(t => t.GoodReturnNo == SReturnId).ToList();
                        if (SDtdata.Count != 0)
                        {
                            foreach (var item in SDtdata)
                            {
                                int detailId = 0; decimal rate = 0; int updateStockId = 0;
                                string batchno = item.BatchNo;
                                DateTime expireDate;
                                decimal BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0, BRQty = 0, BPRQty = 0, netQty = 0;
                                if (item.Expiry == null || item.Expiry == "")
                                    expireDate = DateTime.Now.Date;
                                else
                                    expireDate = Convert.ToDateTime(item.Expiry);
                                productname = item.ProductId;
                                company = item.CompanyName;
                                unit = item.Unit;
                                alternateunit = item.SaleUnit;
                                detailId = item.Id;
                                Quantity = Convert.ToDecimal(item.Quantity);
                                getProductType(productname, company, unit);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    BatchRepository batchRepo = new BatchRepository();
                                    var batchdata = batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.MasterBillNo == custBillNo && (t.BatchNo == batchno) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (batchdata != null)
                                    {
                                        PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                                        var purData1 = mastRepo1.GetAll().Where(t => t.Id == batchdata.PorderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (purData1 != null)
                                        {
                                            PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                                            var purdetaildata1 = detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                            if (purdetaildata1 != null)
                                            {
                                                PReturnDetailRepository PRdetailRepo = new PReturnDetailRepository();
                                                var PRdetdata = PRdetailRepo.GetAll().Where(t => t.POID == purdetaildata1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseReturnMaster.CompId == CommonMethod.CompId && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                if (PRdetdata != null)
                                                    BPRQty = Convert.ToDecimal(PRdetdata.Quantity);
                                                else
                                                    BPRQty = 0;
                                                BPurQty = Convert.ToDecimal(detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Quantity));
                                                FreeQty = Convert.ToDecimal(detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.FreeQty));
                                                netQty = (BPurQty + FreeQty) - BPRQty;
                                                CustomerBillMasterRepository custMRepo = new CustomerBillMasterRepository();
                                                var custMData = custMRepo.GetAll().Where(t => t.BillNo == Convert.ToString(custBillNo) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                CustomerBillDetailRepository custDRepo = new CustomerBillDetailRepository();
                                                var custDetData = custDRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && (t.BatchNo == batchno) && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                if (custDetData != null)
                                                    BUQty = Convert.ToDecimal(custDRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && (t.BatchNo == batchno) && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Quantity));
                                                try
                                                {
                                                    GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                    GoodReturnDetail SRData = new GoodReturnDetail();
                                                    SRData = SRRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno).FirstOrDefault();
                                                    BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                                                }
                                                catch (Exception)
                                                { }
                                                BRQty = netQty - (BUQty);
                                                if (BRQty != 0)
                                                {
                                                    PurchaseDetail pdata1 = new PurchaseDetail();
                                                    pdata1 = detailrepo1.GetById(purdetaildata1.Id);
                                                    pdata1.Status = "Active";
                                                    detailrepo1.Edit(pdata1);
                                                    detailrepo1.Save();
                                                }
                                                else
                                                {
                                                    PurchaseDetail pdata1 = new PurchaseDetail();
                                                    pdata1 = detailrepo1.GetById(purdetaildata1.Id);
                                                    pdata1.Status = "Inactive";
                                                    detailrepo1.Edit(pdata1);
                                                    detailrepo1.Save();
                                                }
                                            }
                                        }
                                    }
                                    batchRepo.Dispose();
                                }
                                if (productentrytype == 9)
                                { }
                                else if (productentrytype == 6)
                                { }
                                else
                                {
                                    StockRepository stockRepo = new StockRepository();
                                    var stockdata = stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (stockdata != null)
                                    {
                                        newsqty = 0; sqty = 0; pqty = 0; prqty = 0; newrqty = 0; rqty = 0; srqty = 0; dqty = 0; oqty = 0;
                                        sqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleQty);
                                        pqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurchaseQty);
                                        rqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().RemQty);
                                        oqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().OpeningQty);
                                        dqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().DeadQty);
                                        prqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurReturnQty);
                                        srqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleReturnQty);
                                        newsqty = srqty - Quantity;//sale return qty
                                        newrqty = oqty + pqty - prqty - sqty + newsqty - dqty;
                                        updateStockId = stockdata.Id;
                                    }
                                    sdata = stockRepo.GetById(stockdata.Id);
                                    if (sdata != null)
                                    {
                                        sdata.ProductId = stockdata.ProductId;
                                        sdata.ProductName = productname;
                                        sdata.MfgCompany = company;
                                        sdata.Unit = unit;
                                        sdata.OpeningQty = stockdata.OpeningQty;
                                        sdata.DeadQty = stockdata.DeadQty;
                                        sdata.PurchaseQty = Convert.ToDecimal(pqty);
                                        sdata.SaleQty = Convert.ToDecimal(sqty);
                                        sdata.RemQty = Convert.ToDecimal(newrqty);
                                        sdata.SaleReturnQty = Convert.ToDecimal(newsqty);
                                        sdata.PurReturnQty = stockdata.PurReturnQty;

                                        SettingRepository settRepo = new SettingRepository();
                                        Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand();
                                            if (settData.StockOn == "Purchase Rate")
                                                command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                            else if (settData.StockOn == "Sale Rate")
                                                command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productname;
                                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            SqlDataReader reader = command.ExecuteReader();
                                            DataTable dtRate = new DataTable();
                                            dtRate.Load(reader);
                                            rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                            db.CloseConnection();
                                            dtRate.Dispose();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                        sdata.ProductRate = Convert.ToDecimal(rate);
                                        sdata.CompId = CommonMethod.CompId;
                                        sdata.TransactionYear = CommonMethod.TransactionYear;
                                        sdata.Id = updateStockId;
                                        stockRepo.Edit(sdata);
                                        stockRepo.Save();
                                    }
                                }
                            }
                        }
                        db.connect();
                        DataTable dt1 = db.GetTable("Delete from GoodReturnDetail where GoodReturnNo='" + SReturnId + "'");
                        DataTable dt = db.GetTable("Delete from GoodReturnMaster where Id='" + SReturnId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Sale Return " + DeletedBillNo + " is deleted sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                    }
                }
            }
            catch (Exception) { }
        }

        private void getProductType(string proname, string company, string unit)
        {
            ProductRepository prodrepo = new ProductRepository();
            ProductInformation productdata = new ProductInformation();
            if (CommonMethod.commProduct == true)
                productdata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
            else
                productdata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (productdata != null)
            {
                productentrytype = Convert.ToInt32(productdata.ProductType);  // allinone standard
                if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "N" && productdata.WithBatch == "N")
                    productentrytype = 0;
                else if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "Y")
                    productentrytype = 1;
                else if (productdata.ProductType == 0 && productdata.WithBatch == "Y") //krushi
                    productentrytype = 2;
                else if (productdata.ProductType == 3 && productdata.WithoutSize == "N")  //clothing
                    productentrytype = 3;
                else if (productdata.ProductType == 3 && productdata.WithoutSize == "Y")  //clothing
                    productentrytype = 4;
                else if (productdata.ProductType == 1) // serialized
                    productentrytype = 5;
                else if (productdata.ProductType == 2) // Assembly
                    productentrytype = 6;
                else if (productdata.ProductType == 4) //kit
                    productentrytype = 7;
                else if (productdata.ProductType == 5) //scrap
                    productentrytype = 8;
            }
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (frmSaleBillReturn.custbillinfo == null)
                        frmSaleBillReturn.custbillinfo = null;
                    else
                    {
                        this.Close();
                        frmSaleBillReturn.custbillinfo.Show();
                        frmSaleBillReturn.custbillinfo = null;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}