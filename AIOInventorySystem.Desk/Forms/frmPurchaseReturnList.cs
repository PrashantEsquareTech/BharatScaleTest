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
    public partial class frmPurchaseReturnList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        int productentrytype = 0;
        List<int> billList = new List<int>();

        public frmPurchaseReturnList()
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

        public frmPurchaseReturnList(DateTime from, DateTime to)
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
                PReturnMasterRepository preturnmasterrepo = new PReturnMasterRepository();
                List<PurchaseReturnMaster> ItemList = preturnmasterrepo.GetAll().Where(t => t.PurchaseReturnDate.Value.Date >= from.Date && t.PurchaseReturnDate.Value.Date <= to.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList != null)
                {
                    GvPorderInfo.Rows.Clear();
                    int i = 0;
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvPorderInfo.Columns["Selectg"].Visible = false;
                    else
                        GvPorderInfo.Columns["Selectg"].Visible = true;
                    foreach (var item in ItemList)
                    {
                        GvPorderInfo.Rows.Add();
                        GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value = item.PurchaseReturnNo;
                        GvPorderInfo.Rows[i].Cells["PurchaseReturnDate"].Value = item.PurchaseReturnDate.Value.ToShortDateString();
                        GvPorderInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                        GvPorderInfo.Rows[i].Cells["SupplierName"].Value = item.Suppliername;
                        GvPorderInfo.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                        GvPorderInfo.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                        GvPorderInfo.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                preturnmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Bindgrid()
        {
            try
            {
                PReturnMasterRepository preturnmasterrepo = new PReturnMasterRepository();
                List<PurchaseReturnMaster> ItemList = preturnmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList != null)
                {
                    GvPorderInfo.Rows.Clear();
                    int i = 0;
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvPorderInfo.Columns["Selectg"].Visible = false;
                    else
                        GvPorderInfo.Columns["Selectg"].Visible = true;
                    foreach (var item in ItemList)
                    {
                        GvPorderInfo.Rows.Add();
                        GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value = item.PurchaseReturnNo;
                        GvPorderInfo.Rows[i].Cells["PurchaseReturnDate"].Value = item.PurchaseReturnDate.Value.ToShortDateString();
                        GvPorderInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                        GvPorderInfo.Rows[i].Cells["SupplierName"].Value = item.Suppliername;
                        GvPorderInfo.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                        GvPorderInfo.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                        GvPorderInfo.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                preturnmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillsupplier()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> sdata = new List<SupplierInformation>();
                sdata.Clear();
                if (CommonMethod.commProduct == true)
                    sdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    sdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                sdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                sdata.Reverse();
                cmbsname.DataSource = sdata;
                cmbsname.ValueMember = "SupplierCode";
                cmbsname.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
                cmbsname.SelectedIndex = 0;
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
                    chkpono.CheckState = 0;
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
                    chkpono.CheckState = 0;
                }
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                dtpFromPorderdate.Value = DateTime.Now.Date;
                dtpToPorderDate.Value = DateTime.Now.Date;
                txtPOrderno.Text = "";
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
                    if (txtPOrderno.Text != "")
                    {
                        dtpFromPorderdate.Value = DateTime.Now.Date;
                        dtpToPorderDate.Value = DateTime.Now.Date;
                        PReturnMasterRepository preturnmasterrepo = new PReturnMasterRepository();
                        List<PurchaseReturnMaster> ItemList = preturnmasterrepo.GetAll().Where(t => t.PurchaseReturnNo == Convert.ToInt32(txtPOrderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            GvPorderInfo.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvPorderInfo.Columns["Selectg"].Visible = false;
                            else
                                GvPorderInfo.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                GvPorderInfo.Rows.Add();
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value = item.PurchaseReturnNo;
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnDate"].Value = item.PurchaseReturnDate.Value.ToShortDateString();
                                GvPorderInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                                GvPorderInfo.Rows[i].Cells["SupplierName"].Value = item.Suppliername;
                                GvPorderInfo.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                GvPorderInfo.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                GvPorderInfo.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = txtPOrderno;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = txtPOrderno;
                        }
                        preturnmasterrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Purchase Return Order No.", "Warning");
                        this.ActiveControl = txtPOrderno;
                    }
                }
                else if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Value.Date <= dtpToPorderDate.Value.Date)
                    {
                        txtPOrderno.Text = "";
                        PReturnMasterRepository preturnmasterrepo = new PReturnMasterRepository();
                        List<PurchaseReturnMaster> ItemList = preturnmasterrepo.GetAll().Where(t => t.PurchaseReturnDate.Value.Date >= dtpFromPorderdate.Value.Date && t.PurchaseReturnDate.Value.Date <= dtpToPorderDate.Value.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            GvPorderInfo.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvPorderInfo.Columns["Selectg"].Visible = false;
                            else
                                GvPorderInfo.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                GvPorderInfo.Rows.Add();
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value = item.PurchaseReturnNo;
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnDate"].Value = item.PurchaseReturnDate.Value.ToShortDateString();
                                GvPorderInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                                GvPorderInfo.Rows[i].Cells["SupplierName"].Value = item.Suppliername;
                                GvPorderInfo.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                GvPorderInfo.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                GvPorderInfo.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        preturnmasterrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpFromPorderdate;
                    }
                }
                else if (chksname.Checked == true)
                {
                    if (cmbsname.Text != "Select")
                    {
                        dtpFromPorderdate.Value = DateTime.Now.Date;
                        dtpToPorderDate.Value = DateTime.Now.Date;
                        PReturnMasterRepository preturnmasterrepo = new PReturnMasterRepository();
                        List<PurchaseReturnMaster> ItemList = preturnmasterrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsname.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            GvPorderInfo.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvPorderInfo.Columns["Selectg"].Visible = false;
                            else
                                GvPorderInfo.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                GvPorderInfo.Rows.Add();
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value = item.PurchaseReturnNo;
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnDate"].Value = item.PurchaseReturnDate.Value.ToShortDateString();
                                GvPorderInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                                GvPorderInfo.Rows[i].Cells["SupplierName"].Value = item.Suppliername;
                                GvPorderInfo.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                GvPorderInfo.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                GvPorderInfo.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = cmbsname;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = cmbsname;
                        }
                        preturnmasterrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsname;
                    }
                }
                else if (chkpono.Checked)
                {
                    if (txtpono.Text != "")
                    {
                        dtpFromPorderdate.Value = DateTime.Now.Date;
                        dtpToPorderDate.Value = DateTime.Now.Date;
                        PReturnMasterRepository preturnmasterrepo = new PReturnMasterRepository();
                        List<PurchaseReturnMaster> ItemList = preturnmasterrepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtpono.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (ItemList.Count > 0)
                        {
                            GvPorderInfo.Rows.Clear();
                            int i = 0;
                            if (rbtnDeleteMultipleBill.Enabled == false)
                                GvPorderInfo.Columns["Selectg"].Visible = false;
                            else
                                GvPorderInfo.Columns["Selectg"].Visible = true;
                            foreach (var item in ItemList)
                            {
                                GvPorderInfo.Rows.Add();
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value = item.PurchaseReturnNo;
                                GvPorderInfo.Rows[i].Cells["PurchaseReturnDate"].Value = item.PurchaseReturnDate.Value.ToShortDateString();
                                GvPorderInfo.Rows[i].Cells["POrderNo"].Value = item.PorderNo;
                                GvPorderInfo.Rows[i].Cells["SupplierName"].Value = item.Suppliername;
                                GvPorderInfo.Rows[i].Cells["TotalAmt"].Value = item.NetAmount;
                                GvPorderInfo.Rows[i].Cells["Updateg"].Value = AIOInventorySystem.Desk.Properties.Resources.Update;
                                GvPorderInfo.Rows[i].Cells["Id"].Value = item.Id;
                                i++;
                            }
                            this.ActiveControl = txtpono;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = txtpono;
                        }
                        preturnmasterrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Purchase Order No.", "Warning");
                        this.ActiveControl = txtpono;
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
                    chkpono.CheckState = 0;
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

        private void chkpono_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtpFromPorderdate.Value = DateTime.Now.Date;
                dtpToPorderDate.Value = DateTime.Now.Date;
                txtPOrderno.Text = "";
                chkporderno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                Bindgrid();
                fillsupplier();
                chksname.CheckState = 0;
            }
            catch (Exception)
            { }
        }

        private void frmPurchaseReturnList_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                //Purchase Return No Binding to textbox
                txtPOrderno.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT PurchaseReturnNo FROM PurchaseReturnMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", con);
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
                    txtPOrderno.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }

                //Purchase Order No Binding to textbox
                txtpono.Text = "";
                string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString2))
                {
                    SqlCommand cmd = new SqlCommand("SELECT PorderNo FROM PurchaseReturnMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", con);
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
                    txtpono.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtPOrderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbsname_KeyDown(object sender, KeyEventArgs e)
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

        private void txtpono_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbsname_Leave(object sender, EventArgs e)
        {
            if (cmbsname.Text != "" && cmbsname.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbsname.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    fillsupplier();
                    this.ActiveControl = cmbsname;
                }
            }
        }

        private void chkpono_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpono;
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

        private void chksname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsname;
            }
            catch (Exception)
            { }
        }

        private void chkporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPOrderno;
            }
            catch (Exception)
            { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                for (int i = 0; i < GvPorderInfo.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(GvPorderInfo.Rows[i].Cells["Select"].Value) == true)
                        billList.Add(Convert.ToInt32(GvPorderInfo.Rows[i].Cells["PurchaseReturnNo"].Value));
                }
                if (billList.Count != 0)
                {
                    DeleteMultiplebill(billList);
                    this.ActiveControl = txtpono;
                    Bindgrid();
                }
                else
                    MessageBox.Show("Please select at least one bill!!");
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
                    string productname = "", unit = "", company = "", alternateunit = "", Suppliername = "", DeletedBillNo = "";
                    int PMID = 0, POrderNo = 0;
                    decimal NetAmount = 0, Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0;
                    PReturnMasterRepository PRmastRepo = new PReturnMasterRepository();
                    var PRMData = PRmastRepo.GetAll().Where(t => t.PurchaseReturnNo == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (PRMData != null)
                    {
                        Suppliername = Convert.ToString(PRMData.Suppliername);
                        POrderNo = Convert.ToInt32(PRMData.PorderNo);
                        var PRMData1 = PRmastRepo.GetAll().Where(t => t.PurchaseReturnNo == billList[q] && t.Suppliername == Suppliername && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (PRMData1 != null)
                        {
                            // add details for history table
                            try
                            {
                                DeletedBillNo = Convert.ToString(PRMData1.PurchaseReturnNo);
                                Suppliername = Convert.ToString(PRMData1.Suppliername);
                                NetAmount = Convert.ToDecimal(PRMData1.NetAmount);

                                DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                DeleteHistoryTable historydata = new DeleteHistoryTable();
                                historydata.DeleteType = "Purchase Return";
                                historydata.BillNo = DeletedBillNo;
                                historydata.SupplierOrCustomerName = Suppliername;
                                historydata.Amount = Convert.ToDecimal(NetAmount);
                                historydata.CurrentDate = DateTime.Now.Date;
                                historydata.BillDate = Convert.ToDateTime(PRMData1.PurchaseReturnDate);
                                historydata.CompId = CommonMethod.CompId;
                                historydata.TransactionYear = CommonMethod.TransactionYear;
                                historyRepo.Add(historydata);
                                historyRepo.Save();
                            }
                            catch (Exception) { }
                            decimal decremamt = 0;
                            int PReturnId = PRmastRepo.GetAll().Where(t => t.PurchaseReturnNo == billList[q] && t.PorderNo == POrderNo && t.Suppliername == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                            SupplierRemainingPaymentRepository suppremRepo = new SupplierRemainingPaymentRepository();
                            var suppremdata = suppremRepo.GetAll().Where(t => t.BillId == POrderNo && t.Suppliername == Convert.ToString(Suppliername) && t.Type == "PurchaseReturn" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (suppremdata != null)
                            {
                                int ReceiptId = suppremRepo.GetAll().Where(t => t.BillId == POrderNo && t.Suppliername == Convert.ToString(Suppliername) && t.Type == "PurchaseReturn" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                                SupplierRemainingPaymentRepository SupplierRemRepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment SupplierRemdata = SupplierRemRepo.GetById(ReceiptId);
                                SupplierRemRepo.Remove(SupplierRemdata);
                                SupplierRemRepo.Save();

                                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remdata1 = SupplierRemRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id < ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                if (remdata1 != null)
                                    decremamt = Convert.ToDecimal(remdata1.RemainingAmt);
                                else
                                    decremamt = 0;
                                SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id > ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                remainingpayrepo1.Dispose();
                                remainingpayrepo2.Dispose();
                            }
                            PReturnDetailRepository PRDetailRepo = new PReturnDetailRepository();
                            List<PurchaseReturnDetail> detailList = PRDetailRepo.GetAll().Where(t => t.PurchaseReturnNo == PReturnId).ToList();
                            if (detailList.Count != 0)
                            {
                                foreach (var item1 in detailList)
                                {
                                    int detailId = 0, updateStockId = 0;
                                    decimal rate = 0, BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0, BRQty = 0, BPRQty = 0;
                                    string batchNo = "";
                                    DateTime expirydate;
                                    productname = item1.ProductName;
                                    company = item1.CompanyName;
                                    unit = item1.Unit;
                                    alternateunit = item1.PurchaseUnit;
                                    detailId = item1.Id;
                                    batchNo = item1.BatchNo;
                                    expirydate = Convert.ToDateTime(item1.ExpireDate);
                                    Quantity = Convert.ToDecimal(item1.Quantity);

                                    //for batch product 27/6/2018 
                                    getProductType(productname, company, unit);
                                    if (productentrytype == 1 || productentrytype == 2)
                                    {
                                        PurchaseMasterRepository pmrepo = new PurchaseMasterRepository();
                                        var pmdata = pmrepo.GetAll().Where(t => t.PorderNo == POrderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (pmdata != null)
                                            PMID = pmdata.Id;
                                        BatchRepository batchRepo = new BatchRepository();
                                        var batchdata = batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.PorderNo == PMID && t.BatchNo == batchNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (batchdata != null)
                                        {
                                            BUQty = Convert.ToDecimal(batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.PorderNo == PMID && t.BatchNo == batchNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.UsedQty));
                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                                            GoodReturnDetail SRData = new GoodReturnDetail();
                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && t.BNID == Convert.ToInt32(batchdata.BillId) && t.BatchNo == batchNo).FirstOrDefault();
                                            if (SRData != null)
                                                BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                                        }
                                        else
                                            BUQty = 0;

                                        PurchaseDetailRepository purchaseRepo = new PurchaseDetailRepository();
                                        var purchaseData = purchaseRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).FirstOrDefault();
                                        if (purchaseData != null)
                                        {
                                            BPurQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).Sum(t => t.Quantity));
                                            FreeQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).Sum(t => t.FreeQty));
                                        }

                                        PReturnDetailRepository PRdetailRepo = new PReturnDetailRepository();
                                        var PRdetdata = PRdetailRepo.GetAll().Where(t => t.POID == purchaseData.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (PRdetdata != null)
                                            BPRQty = Convert.ToDecimal(PRdetdata.Quantity);
                                        else
                                            BPRQty = 0;

                                        BRQty = (BPurQty + FreeQty) - (BUQty - BSRQty);
                                        if (BRQty == 0)
                                        {
                                            PurchaseMasterRepository mastRepo = new PurchaseMasterRepository();
                                            var purData = mastRepo.GetAll().Where(t => t.PorderNo == POrderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                            if (purData != null)
                                            {
                                                PurchaseDetailRepository detailrepo = new PurchaseDetailRepository();
                                                var purdetaildata = detailrepo.GetAll().Where(t => t.Id == purchaseData.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).FirstOrDefault();
                                                if (purdetaildata != null)
                                                {
                                                    int pdID = purdetaildata.Id;
                                                    PurchaseDetail pdata = new PurchaseDetail();
                                                    pdata = detailrepo.GetById(pdID);
                                                    pdata.Status = "Inactive";
                                                    detailrepo.Edit(pdata);
                                                    detailrepo.Save();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                                            var purData1 = mastRepo1.GetAll().Where(t => t.PorderNo == POrderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                            if (purData1 != null)
                                            {
                                                PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                                                var purdetaildata1 = detailrepo1.GetAll().Where(t => t.Id == purchaseData.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).FirstOrDefault();
                                                if (purdetaildata1 != null)
                                                {
                                                    int pdID = purdetaildata1.Id;
                                                    PurchaseDetail pdata1 = new PurchaseDetail();
                                                    pdata1 = detailrepo1.GetById(pdID);
                                                    pdata1.Status = "Active";
                                                    detailrepo1.Edit(pdata1);
                                                    detailrepo1.Save();
                                                }
                                            }
                                        }
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
                                            newsqty = prqty - Quantity;// purchase return qty
                                            newrqty = oqty + pqty - newsqty - sqty + srqty - dqty;
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
                                            sdata.SaleReturnQty = stockdata.SaleReturnQty;
                                            sdata.PurReturnQty = Convert.ToDecimal(newsqty);

                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
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
                                                dtRate.Dispose();
                                                db.CloseConnection();
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
                                        stockRepo.Dispose();
                                    }
                                }
                            }
                            db.connect();
                            DataTable dt = db.GetTable("Delete from PurchaseReturnDetail where PurchaseReturnNo='" + PReturnId + "'");
                            DataTable dt1 = db.GetTable("Delete from PurchaseReturnMaster where Id='" + PReturnId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                            MessageBox.Show("Purchase Return " + DeletedBillNo + " is deleted successfully.", "Success");
                            dt.Dispose();
                            dt1.Dispose();
                            db.CloseConnection();
                        }
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (frmPurchaseReturn.custbillinfo == null)
                    {
                        frmPurchaseReturn purord = new frmPurchaseReturn(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        purord.ShowDialog();
                        frmPurchaseReturn.custbillinfo = null;
                    }
                    else
                    {
                        this.Close();
                        frmPurchaseReturn.custbillinfo.billdata(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        frmPurchaseReturn.custbillinfo.Show();
                        frmPurchaseReturn.custbillinfo = null;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}