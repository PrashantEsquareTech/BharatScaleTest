using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmPurchaseOrderPlaceList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> billList = new List<int>();

        public frmPurchaseOrderPlaceList()
        {
            InitializeComponent();
            fillsupplier();
            cm.changedatetimepickerrange(this);
            getSettData();
            Bindgrid();
        }

        public void getSettData()
        {
            try
            {
                SettingRepository settRepo = new SettingRepository();
                Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.DeleteSaleBill == "True")
                    rbtnMultipleDeleteBill.Enabled = true;
                else
                    rbtnMultipleDeleteBill.Enabled = false;
            }
            catch (Exception) { }
        }

        public void Bindgrid()
        {
            try
            {
                getSettData();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[PorderNo] As 'Purchase Order Place No',CONVERT(VARCHAR(24),PorderDate,103) as 'Purchase Order Place Date',[Suppliername] as 'Supplier Name' FROM PurchaseOrderPlaceMaster where CONVERT(VARCHAR(24),PorderDate,101)= CONVERT(VARCHAR(24),GETDATE(),101) and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                if (rbtnMultipleDeleteBill.Enabled == false)
                    GvPorderInfo.Columns[0].Visible = false;
                else
                    GvPorderInfo.Columns[0].Visible = true;
                GvPorderInfo.Columns[1].Width = 80;
                GvPorderInfo.Columns[2].Width = 100;
                GvPorderInfo.Columns[3].Width = 100;
                GvPorderInfo.Columns[4].Width = 250;
                da.Dispose();
                dt.Dispose();
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
                txtPOrderno.Text = "";
                chkporderno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                chksname.CheckState = 0;
                fillsupplier();
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[PorderNo] As 'Purchase Order Place No',CONVERT(VARCHAR(24),PorderDate,103) as 'Purchase Order Place Date',[Suppliername] as 'Supplier Name' FROM PurchaseOrderPlaceMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                if (rbtnMultipleDeleteBill.Enabled == false)
                    GvPorderInfo.Columns[0].Visible = false;
                else
                    GvPorderInfo.Columns[0].Visible = true;
                GvPorderInfo.Columns[1].Width = 80;
                GvPorderInfo.Columns[2].Width = 150;
                GvPorderInfo.Columns[3].Width = 150;
                GvPorderInfo.Columns[4].Width = 250;
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
                getSettData();
                if (chkporderno.Checked == true)
                {
                    if (txtPOrderno.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[PorderNo] As 'Purchase Order Place No',CONVERT(VARCHAR(24),PorderDate,103) as 'Purchase Order Place Date',[Suppliername] as 'Supplier Name' FROM PurchaseOrderPlaceMaster where PorderNo='" + txtPOrderno.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        GvPorderInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            if (rbtnMultipleDeleteBill.Enabled == false)
                                GvPorderInfo.Columns[0].Visible = false;
                            else
                                GvPorderInfo.Columns[0].Visible = true;
                            GvPorderInfo.Columns[1].Width = 80;
                            GvPorderInfo.Columns[2].Width = 150;
                            GvPorderInfo.Columns[3].Width = 150;
                            GvPorderInfo.Columns[4].Width = 250;
                            this.ActiveControl = txtPOrderno;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = txtPOrderno;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select purchase Order Number.", "Warning");
                        this.ActiveControl = txtPOrderno;
                    }
                }
                if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Text != "" && dtpToPorderDate.Text != "")
                    {
                        if (dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                        {
                            CommonMethod com = new CommonMethod();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[PorderNo] As 'Purchase Order Place No',CONVERT(VARCHAR(24),PorderDate,103) as 'Purchase Order Place Date',[Suppliername] as 'Supplier Name' FROM PurchaseOrderPlaceMaster where PorderDate >= '" + com.ValidFromDate(dtpFromPorderdate.Value.Date) + "' and  PorderDate <= '" + com.ValidToDate(dtpToPorderDate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvPorderInfo.DataSource = dt;
                            GvPorderInfo.Refresh();
                            lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                            if (GvPorderInfo.Rows.Count != 0)
                            {
                                GvPorderInfo.Columns["ID"].Visible = false;
                                if (rbtnMultipleDeleteBill.Enabled == false)
                                    GvPorderInfo.Columns[0].Visible = false;
                                else
                                    GvPorderInfo.Columns[0].Visible = true;
                                GvPorderInfo.Columns[1].Width = 80;
                                GvPorderInfo.Columns[2].Width = 150;
                                GvPorderInfo.Columns[3].Width = 150;
                                GvPorderInfo.Columns[4].Width = 250;
                                this.ActiveControl = dtpFromPorderdate;
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
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
                    else
                    {
                        MessageBox.Show("Select purchase Order Dates.", "Warning");
                        this.ActiveControl = dtpFromPorderdate;
                    }
                }
                if (chksname.Checked == true)
                {
                    if (cmbsname.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],[PorderNo] As 'Purchase Order Place No',CONVERT(VARCHAR(24),PorderDate,103) as 'Purchase Order Place Date',[Suppliername] as 'Supplier Name' FROM PurchaseOrderPlaceMaster where SupplierId='" + cmbsname.SelectedValue + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        GvPorderInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            if (rbtnMultipleDeleteBill.Enabled == false)
                                GvPorderInfo.Columns[0].Visible = false;
                            else
                                GvPorderInfo.Columns[0].Visible = true;
                            GvPorderInfo.Columns[1].Width = 80;
                            GvPorderInfo.Columns[2].Width = 150;
                            GvPorderInfo.Columns[3].Width = 150;
                            GvPorderInfo.Columns[4].Width = 250;
                            this.ActiveControl = cmbsname;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = cmbsname;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsname;
                    }
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
                    txtPOrderno.Text = "";
                    cmbsname.Text = "Select";
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
                    txtPOrderno.Text = "";
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
                {
                    chkbetweendate.Checked = false;
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (Convert.ToString(GvPorderInfo.Rows[e.RowIndex].Cells[5].Value) != "First stock")
                    {
                        if (frmPurchaseOrderPlace.purorderinfo == null)
                        {
                            frmPurchaseOrderPlace purord = new frmPurchaseOrderPlace(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells[2].Value));
                            purord.ShowDialog();
                            frmPurchaseOrderPlace.purorderinfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmPurchaseOrderPlace.purorderinfo.LoadData(Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells[2].Value));
                            frmPurchaseOrderPlace.purorderinfo.Show();
                            frmPurchaseOrderPlace.purorderinfo = null;
                        }
                    }
                    else
                        MessageBox.Show("This Purchase Order User Can not Updated.", "Warning");
                }
            }
            catch (Exception)
            { }
        }

        private void GvPorderInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvPorderInfo.CurrentCellAddress.X == 0)
                    {
                        frmPurchaseOrderPlace pdata = new frmPurchaseOrderPlace(Convert.ToInt32(GvPorderInfo.Rows[GvPorderInfo.CurrentCellAddress.Y].Cells[2].Value));
                        pdata.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        pdata.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        pdata.ShowDialog();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkporderno.Checked == true && txtPOrderno.Text != "")
                {
                    RptAllPurchaseOrderPlace plist = new RptAllPurchaseOrderPlace(Convert.ToInt32(txtPOrderno.Text));
                    plist.ShowDialog();
                    plist.Dispose();
                }
                else if (chkbetweendate.Checked == true && dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                {
                    RptAllPurchaseOrderPlace plist = new RptAllPurchaseOrderPlace(cm.ValidFromDate(dtpFromPorderdate.Value), cm.ValidToDate(dtpToPorderDate.Value));
                    plist.ShowDialog();
                    plist.Dispose();
                }
                else if (chksname.Checked == true && cmbsname.Text != "Select")
                {
                    RptAllPurchaseOrderPlace plist = new RptAllPurchaseOrderPlace(cmbsname.Text);
                    plist.ShowDialog();
                    plist.Dispose();
                }
                else
                {
                    RptAllPurchaseOrderPlace plist = new RptAllPurchaseOrderPlace();
                    plist.ShowDialog();
                    plist.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void frmPurchaseOrderList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmPurchaseOrderPlace.purorderinfo = null;
            }
            catch (Exception)
            { }
        }

        private void btntodayslist_Click(object sender, EventArgs e)
        {
            try
            {
                Bindgrid();
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

        private void frmPurchaseOrderPlaceList_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
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

        private void rbtnMultipleDeleteBill_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                foreach (DataGridViewRow row in GvPorderInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(Convert.ToInt32(row.Cells[2].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[2].Value));
                        }
                    }
                }
                if (billList.Count != 0)
                    DeleteMultiplebill(billList);
                else
                    MessageBox.Show("Please select at least one bill!!");
                this.ActiveControl = chkporderno;
                Bindgrid();
            }
            catch (Exception) { }
        }

        public void DeleteMultiplebill(List<int> billList)
        {
            try
            {
                for (int q = 0; q < billList.Count; q++)
                {
                    int POPlaceId = 0;
                    PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                    var POPlaceData = pmasterrepo.GetAll().Where(t => t.Id == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == t.TransactionYear).FirstOrDefault();
                    if (POPlaceData != null)
                    {
                        try
                        {
                            POPlaceId = Convert.ToInt32(POPlaceData.Id);
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Purchase Order Place";
                            historydata.BillNo = Convert.ToString(POPlaceData.PorderNo);
                            historydata.SupplierOrCustomerName = Convert.ToString(POPlaceData.Suppliername);
                            historydata.Amount = Convert.ToDecimal(0);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(POPlaceData.PorderDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }
                        db.connect();
                        DataTable dt1 = db.GetTable("delete from PurchaseOrderPlaceDetail where PorderNo='" + POPlaceId + "' ");
                        DataTable dt2 = db.GetTable("delete from PurchaseOrderPlaceMaster where Id='" + POPlaceId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("This Purchase Order Palce No. " + POPlaceId + " Deleted Successfully");
                        dt1.Dispose();
                        dt2.Dispose();
                        db.CloseConnection();
                    }
                }
            }
            catch (Exception) { }
        }
    }
}