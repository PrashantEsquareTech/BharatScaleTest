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
    public partial class frmRecieptChallanList : Form
    {
        public static frmRecieptChallanList recchalleninfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        int productentrytype = 0;
        List<int> billList = new List<int>();

        public frmRecieptChallanList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.DeleteSaleBill == "True")
                rbtnDeleteMultiple.Enabled = true;
            else
                rbtnDeleteMultiple.Enabled = false;
            Bindgrid();
            customerdata();
        }

        public void Bindgrid()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id],[RecieptChallanNo] As 'RC.NO',CONVERT(VARCHAR(24),RecieptChallanDate,103) as 'RC.Date',[SupplierName]  FROM [RecieptChallanMaster] where status=1 and CONVERT(VARCHAR(24),RecieptChallanDate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                GvproductInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultiple.Enabled == false)
                    GvproductInfo.Columns["Id"].Visible = false;
                else
                    GvproductInfo.Columns["Id"].Visible = true;
                GvproductInfo.Columns["RC.NO"].Width = 100;
                GvproductInfo.Columns["RC.Date"].Width = 115;
                GvproductInfo.Columns["SupplierName"].Width = 210;
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
                SupplierRepository custrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = custrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "SupplierCode";
                cmbcustomername.DisplayMember = "SupplierName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[RecieptChallanNo] As 'RC.NO',[RecieptChallanDate] as 'RC.Date',[SupplierName]  FROM [RecieptChallanMaster]where status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvproductInfo.DataSource = dt;
                GvproductInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                GvproductInfo.Columns["ID"].Visible = false;
                if (rbtnDeleteMultiple.Enabled == false)
                    GvproductInfo.Columns["Id"].Visible = false;
                else
                    GvproductInfo.Columns["Id"].Visible = true;
                GvproductInfo.Columns["RC.NO"].Width = 100;
                GvproductInfo.Columns["RC.Date"].Width = 115;
                GvproductInfo.Columns["SupplierName"].Width = 210;
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
                if (chkporderno.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[RecieptChallanNo] As 'RC.NO',[RecieptChallanDate] as 'RC.Date',[SupplierName]  FROM [RecieptChallanMaster]where SupplierId='" + cmbcustomername.SelectedValue + "' and Status= 1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvproductInfo.DataSource = dt;
                        GvproductInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                        if (GvproductInfo.Rows.Count != 0)
                        {
                            GvproductInfo.Columns["ID"].Visible = false;
                            if (rbtnDeleteMultiple.Enabled == false)
                                GvproductInfo.Columns["Id"].Visible = false;
                            else
                                GvproductInfo.Columns["Id"].Visible = true;
                            GvproductInfo.Columns["RC.NO"].Width = 100;
                            GvproductInfo.Columns["RC.Date"].Width = 115;
                            GvproductInfo.Columns["SupplierName"].Width = 210;
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
                    else
                    {
                        MessageBox.Show("Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[RecieptChallanNo] As 'RC.NO',[RecieptChallanDate] as 'RC.Date',[SupplierName]  FROM [RecieptChallanMaster] where RecieptChallanDate >= '" + com.ValidFromDate(dtpFromPorderdate.Value.Date) + "' and RecieptChallanDate <='" + com.ValidToDate(dtpToPorderDate.Value.Date) + "' and Status = 1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvproductInfo.DataSource = dt;
                        GvproductInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                        if (GvproductInfo.Rows.Count != 0)
                        {
                            GvproductInfo.Columns["ID"].Visible = false;
                            if (rbtnDeleteMultiple.Enabled == false)
                                GvproductInfo.Columns["Id"].Visible = false;
                            else
                                GvproductInfo.Columns["Id"].Visible = true;
                            GvproductInfo.Columns["RC.NO"].Width = 100;
                            GvproductInfo.Columns["RC.Date"].Width = 115;
                            GvproductInfo.Columns["SupplierName"].Width = 210;
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        else
                        {
                            MessageBox.Show("Record Not Found.", "Warning");
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpToPorderDate;
                    }
                }
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
            if (chkcompletedc.Checked == true)
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[RecieptChallanNo] As 'RC.NO',[RecieptChallanDate] as 'RC.Date',[SupplierName]  FROM [RecieptChallanMaster] where Status=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvproductInfo.DataSource = dt;
                    GvproductInfo.Refresh();
                    lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                    GvproductInfo.Columns["ID"].Visible = false;
                    if (rbtnDeleteMultiple.Enabled == false)
                        GvproductInfo.Columns["Id"].Visible = false;
                    else
                        GvproductInfo.Columns["Id"].Visible = true;
                    GvproductInfo.Columns["RC.NO"].Width = 100;
                    GvproductInfo.Columns["RC.Date"].Width = 115;
                    GvproductInfo.Columns["SupplierName"].Width = 210;
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
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[RecieptChallanNo] As 'RC.NO',[RecieptChallanDate] as 'RC.Date',[SupplierName]  FROM [RecieptChallanMaster] where Status=1 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvproductInfo.DataSource = dt;
                    GvproductInfo.Refresh();
                    lblTotalBill.Text = Convert.ToString(GvproductInfo.Rows.Count);
                    GvproductInfo.Columns["ID"].Visible = false;
                    if (rbtnDeleteMultiple.Enabled == false)
                        GvproductInfo.Columns["Id"].Visible = false;
                    else
                        GvproductInfo.Columns["Id"].Visible = true;
                    GvproductInfo.Columns["RC.NO"].Width = 100;
                    GvproductInfo.Columns["RC.Date"].Width = 115;
                    GvproductInfo.Columns["SupplierName"].Width = 210;
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

        private void GvproductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (frmRecieptChallan.delichalleninfo == null)
                    {
                        recchalleninfo = this;
                        frmRecieptChallanList p = new frmRecieptChallanList();
                        p.Visible = false;
                        frmRecieptChallan frmDC = new frmRecieptChallan(Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        frmDC.ShowDialog();
                        frmRecieptChallan.delichalleninfo = null;
                    }
                    else
                    {
                        recchalleninfo = this;
                        this.Close();
                        frmRecieptChallan.delichalleninfo.GetChallanData(Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        frmRecieptChallan.delichalleninfo.Show();
                        frmRecieptChallan.delichalleninfo = null;
                    }
                    Bindgrid();
                }

                if (e.ColumnIndex == 2)
                {
                    int id = Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["RC.NO"].Value);
                    if (chkcompletedc.Checked == true)
                    { }
                    else
                    {
                        int i = 0;
                        List<int> list = new List<int>();
                        if (cmbcustomername.SelectedIndex > 0)
                        {
                            RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                            List<RecieptChallanMaster> MasterList = challanmasterrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Status == 1 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            foreach (var item in MasterList)
                            {
                                if (GvproductInfo.Rows[i].Cells[0].Value != null)
                                {
                                    id = item.Id;
                                    list.Add(Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                                }
                                i++;
                            }
                            try
                            {
                                RecieptChallanMasterRepository pmrrr = new RecieptChallanMasterRepository();
                                int poid = Convert.ToInt32(pmrrr.GetAll().Where(t => t.RecieptChallanNo == id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().POrderNo);
                                int rcid = Convert.ToInt32(pmrrr.GetAll().Where(t => t.RecieptChallanNo == id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id);
                                recchalleninfo = this;
                                frmRecieptChallanList p = new frmRecieptChallanList();
                                p.Visible = false;
                                frmPurchaseOrder srpt = new frmPurchaseOrder(poid, rcid);
                                srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                srpt.ShowDialog();
                                frmRecieptChallan.delichalleninfo = null;
                            }
                            catch (Exception)
                            { }
                            challanmasterrepo.Dispose();
                        }
                        else
                        {
                            try
                            {
                                RecieptChallanMasterRepository pmrrr = new RecieptChallanMasterRepository();
                                int poid = Convert.ToInt32(pmrrr.GetAll().Where(t => t.RecieptChallanNo == id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().POrderNo);
                                int rcid = Convert.ToInt32(pmrrr.GetAll().Where(t => t.RecieptChallanNo == id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id);
                                recchalleninfo = this;
                                frmRecieptChallanList p = new frmRecieptChallanList();
                                p.Visible = false;
                                frmPurchaseOrder srpt = new frmPurchaseOrder(poid, rcid);
                                srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                srpt.ShowDialog();
                                frmRecieptChallan.delichalleninfo = null;
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    chkcompletedc.Checked = false;
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
                        recchalleninfo = this;
                        frmRecieptChallanList p = new frmRecieptChallanList();
                        p.Visible = false;
                        frmRecieptChallan pdata = new frmRecieptChallan(Convert.ToInt32(GvproductInfo.Rows[GvproductInfo.CurrentCellAddress.Y].Cells["Id"].Value));
                        pdata.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        pdata.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        pdata.ShowDialog();
                    }
                    if (GvproductInfo.CurrentCellAddress.X == 1)
                    {
                        try
                        {
                            recchalleninfo = this;
                            frmRecieptChallanList p = new frmRecieptChallanList();
                            p.Visible = false;
                            frmPurchaseOrder srpt = new frmPurchaseOrder(Convert.ToInt32(GvproductInfo.Rows[GvproductInfo.CurrentCellAddress.Y].Cells["Id"].Value));
                            srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                            srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                            srpt.ShowDialog();
                            frmRecieptChallan.delichalleninfo = null;
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmRecieptChallanList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmRecieptChallan.delichalleninfo = null;
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

        private void frmRecieptChallanList_Load(object sender, EventArgs e)
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
                if (cm.ValidSupplier(cmbcustomername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
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
                else
                    productentrytype = 0;
            }
        }

        private void rbtnDeleteMultiple_Click(object sender, EventArgs e)
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
                            if (billList.Contains(Convert.ToInt32(row.Cells[3].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[3].Value));
                        }
                    }
                    if (billList.Count != 0)
                        DeleteMultiplebill(billList);
                    else
                        MessageBox.Show("Please select at least one bill!!");
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
                    string productname = "", unit = "", company = "", Suppliername = "", DeletedBillNo = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, freeqty = 0, NetAmount = 0, remRawQty = 0;
                    int cntProduct = 0, purcMId = 0, maxpoId = 0, rawqty = 0;
                    string proname = "", unit1 = "", company1 = "", batchno = "", expirydate = "";

                    RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                    var receiptMData = challanmasterrepo.GetAll().Where(t => t.Id == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                    if (receiptMData != null)
                    {
                        maxpoId = Convert.ToInt32(receiptMData.POrderNo);
                        Suppliername = Convert.ToString(receiptMData.SupplierName);
                        var receiptMData1 = challanmasterrepo.GetAll().Where(t => t.Id == billList[q] && t.POrderNo == Convert.ToInt32(maxpoId) && t.SupplierName == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                        if (receiptMData1 != null)
                        {
                            PurchaseMasterRepository purMRepo = new PurchaseMasterRepository();
                            var purMData = purMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(maxpoId) && t.Suppliername == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (purMData != null)
                            {
                                purcMId = Convert.ToInt32(purMData.Id);
                                PurchaseDetailRepository detailRepo1 = new PurchaseDetailRepository();
                                List<PurchaseDetail> detailData1 = detailRepo1.GetAll().Where(t => t.PorderNo == Convert.ToInt32(purcMId) && (t.BatchNo != null || t.ExpireDate != null) && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                                if (detailData1.Count != 0)
                                {
                                    foreach (var pdata in detailData1)
                                    {
                                        proname = pdata.ProductName;
                                        unit1 = pdata.Unit;
                                        company1 = pdata.CompanyName;
                                        batchno = pdata.BatchNo;
                                        if (pdata.ExpireDate != null)
                                            expirydate = Convert.ToString(pdata.ExpireDate.Value.ToShortDateString());
                                        else
                                            expirydate = "";
                                        getProductType(proname, company1, unit1);
                                        if (productentrytype == 1 || productentrytype == 2)
                                        {
                                            CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                            List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                            if (productentrytype == 1)
                                                custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno && t.Expiry == expirydate)).ToList();
                                            else
                                                custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno)).ToList();
                                            if (custdetailData.Count != 0)
                                            {
                                                foreach (var cdata in custdetailData)
                                                {
                                                    cntProduct++;
                                                }
                                            }
                                        }
                                        else if (productentrytype == 6)
                                        {
                                            CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                            List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                            if (productentrytype == 1)
                                                custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                            else
                                                custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                            if (custdetailData.Count != 0)
                                            {
                                                foreach (var cdata in custdetailData)
                                                {
                                                    cntProduct++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DataTable dtAssembly = db.GetTable("Select * from tblAssemblyProductDetails where ProductName='" + proname + "' and CompanyName='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                            if (dtAssembly.Rows.Count > 0)
                                            {
                                                //                                                  0                   1                 2                     3                          4                   
                                                DataTable dtStock = db.GetTable("Select isnull(PurchaseQty,0), isnull(SaleQty,0),isnull(PurReturnQty,0),isnull(SaleReturnQty,0),isnull(OpeningQty,0) from Stock where ProductName='" + proname + "' and MfgCompany='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                                if (dtStock.Rows.Count > 0)
                                                {
                                                    remRawQty = Convert.ToDecimal(dtStock.Rows[0][4].ToString()) + Convert.ToDecimal(dtStock.Rows[0][0].ToString()) - Convert.ToDecimal(dtStock.Rows[0][1].ToString()) - Convert.ToDecimal(dtStock.Rows[0][2].ToString()) + Convert.ToDecimal(dtStock.Rows[0][3].ToString()) - Convert.ToDecimal(pdata.Quantity);
                                                    if (remRawQty < 0)
                                                        rawqty = 1;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (cntProduct != 0 || rawqty == 1)
                                {
                                    if (cntProduct != 0)
                                        MessageBox.Show("You can't delete this Receipt Challan ," + Environment.NewLine + " because this bill is used in batch product in sale bill.", "Warning");
                                    else if (rawqty == 1)
                                        MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this raw products used for " + Environment.NewLine + "assembly product in sale bill.", "Warning");
                                }
                                else
                                {
                                    // add details for history table
                                    try
                                    {
                                        DeletedBillNo = Convert.ToString(receiptMData.RecieptChallanNo);
                                        Suppliername = Convert.ToString(receiptMData.SupplierName);
                                        NetAmount = 0;
                                        DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                        DeleteHistoryTable historydata = new DeleteHistoryTable();
                                        historydata.DeleteType = "Receipt Challan";
                                        historydata.BillNo = DeletedBillNo;
                                        historydata.SupplierOrCustomerName = Suppliername;
                                        historydata.Amount = Convert.ToDecimal(NetAmount);
                                        historydata.BillDate = Convert.ToDateTime(receiptMData.RecieptChallanDate);
                                        historydata.CurrentDate = DateTime.Now.Date;
                                        historydata.CompId = CommonMethod.CompId;
                                        historydata.TransactionYear = CommonMethod.TransactionYear;
                                        historyRepo.Add(historydata);
                                        historyRepo.Save();
                                    }
                                    catch (Exception) { }

                                    RecieptChalanDetailRepository RCDetailrepo = new RecieptChalanDetailRepository();
                                    List<RecieptChalanDetail> RCDetailList = RCDetailrepo.GetAll().Where(t => t.RecieptChallanNo == billList[q] && t.RecieptChallanMaster.CompId == CommonMethod.CompId && t.RecieptChallanMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                                    if (RCDetailList.Count != 0)
                                    {
                                        foreach (var item1 in RCDetailList)
                                        {
                                            int detailId = 0; decimal rate = 0; int updateStockId = 0;
                                            productname = item1.ProductName;
                                            company = item1.CompanyName;
                                            unit = item1.Unit;
                                            detailId = item1.Id;
                                            Quantity = Convert.ToDecimal(item1.Quantity);
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
                                                newsqty = pqty - Quantity - freeqty;
                                                newrqty = oqty + newsqty - prqty - sqty + srqty - dqty;
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
                                                sdata.PurchaseQty = Convert.ToDecimal(newsqty);
                                                sdata.SaleQty = Convert.ToDecimal(sqty);
                                                sdata.RemQty = Convert.ToDecimal(newrqty);
                                                sdata.SaleReturnQty = stockdata.SaleReturnQty;
                                                sdata.PurReturnQty = stockdata.PurReturnQty;

                                                SettingRepository settRepo = new SettingRepository();
                                                Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                try
                                                {
                                                    db.connect();
                                                    SqlCommand command = new SqlCommand();
                                                    if (settData.StockOn == "Purchase Rate")
                                                        command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                                    if (settData.StockOn == "Sale Rate")
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
                                            stockRepo.Dispose();
                                        }
                                    }
                                    db.connect();
                                    DataTable dtPurM = db.GetTable("delete from PurchaseDetail where PorderNo='" + purcMId + "' ");
                                    DataTable dtPurD = db.GetTable("delete from PurchaseMaster where Id='" + purcMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                    DataTable dt1 = db.GetTable("delete from RecieptChalanDetail where RecieptChallanNo='" + billList[q] + "'");
                                    DataTable dt = db.GetTable("delete from RecieptChallanMaster where Id='" + billList[q] + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                    MessageBox.Show("Receipt Challan No" + billList[q] + " is deleted sucessfully.", "Success");
                                    dt.Dispose();
                                    dt1.Dispose();
                                    db.CloseConnection();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}