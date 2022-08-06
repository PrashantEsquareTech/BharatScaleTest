using System;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmPersonalDetail : Form
    {
        public frmPersonalDetail()
        {
            InitializeComponent();
            lnkWOCustomerBillList.Visible = false; lnkWOPurchaseList.Visible = false; lnkWOCustomerBillReport.Visible = false; lnkWOPurchaseReport.Visible = false;
            lnkTotalPurSale.Visible = false; lnkTotalPurSaleReport.Visible = false; lnkOriginalStock.Visible = false; lnkWOProductWisePurchase.Visible = false;
            lnkFakeProductWiseSale.Visible = false; lnkWOProductWiseSale.Visible = false; lnkFakeBill.Visible = false; lnkFakeBiiList.Visible = false;
            lnkWOAddStock.Visible = false; lnkCashBook.Visible = false; lnkFakeBillLedger.Visible = false; lnkFakeBillRemPayment.Visible = false;
            lnkStockTransfer.Visible = false; lnkEstimateCustLedger.Visible = false; lnkOriginalCustomerLedger.Visible = false; lblSale.Visible = false;
            lblPurchase.Visible = false; lblPurchaseSale.Visible = false; lblStock.Visible = false; lblAccount.Visible = false;
            lnkOriginalSuppLedger.Visible = false; lnkEstimateSuppLedger.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                LoginRepository loginrepo = new LoginRepository();
                Login lpass = new Login();
                var Pass = loginrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                string Loginpass = Pass.Password;
                if (textpass.Text == Loginpass)
                {
                    lnkWOCustomerBillList.Visible = true; lnkWOPurchaseList.Visible = true;
                    lnkWOCustomerBillReport.Visible = true; lnkWOPurchaseReport.Visible = true;
                    lnkTotalPurSale.Visible = true; lnkWOStock.Visible = true;
                    lnkTotalPurSaleReport.Visible = true; lnkOriginalStock.Visible = true;
                    lnkWOProductWiseSale.Visible = true; lnkWOProductWisePurchase.Visible = true;
                    lnkFakeBill.Visible = true; lnkFakeProductWiseSale.Visible = true;
                    lnkFakeBiiList.Visible = true; lnkWOAddStock.Visible = true;
                    lnkCashBook.Visible = true;
                    lnkEstimateCustLedger.Visible = true;
                    lnkFakeBillLedger.Visible = true;
                    lnkFakeBillRemPayment.Visible = true;
                    lnkStockTransfer.Visible = true;
                    lblSale.Visible = true;
                    lblPurchase.Visible = true;
                    lblPurchaseSale.Visible = true;
                    lblStock.Visible = true;
                    lblAccount.Visible = true;
                    SettingRepository settRepo = new SettingRepository();
                    Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.SeparationLedger == "Y")
                    {
                        lnkEstimateCustLedger.Visible = true;
                        lnkOriginalCustomerLedger.Visible = true;
                        lnkOriginalSuppLedger.Visible = true;
                        lnkEstimateSuppLedger.Visible = true;
                    }
                    else
                    {
                        lnkEstimateCustLedger.Visible = false;
                        lnkOriginalCustomerLedger.Visible = false;
                        lnkOriginalSuppLedger.Visible = false;
                        lnkEstimateSuppLedger.Visible = false;
                    }
                    textpass.Text = "";
                }
                else
                {
                    MessageBox.Show("Password Not Matching", "Warning");
                    textpass.Text = "";
                    this.ActiveControl = textpass;
                }
                loginrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void frmlblnovatbill_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmNovatBillList compinfo = new frmNovatBillList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void rptlblnovatbill_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RptNoVatBillList compinfo = new RptNoVatBillList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                compinfo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void frmlblsale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmDrCr compinfo = new frmDrCr();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void rptlblsale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RptDrCr compinfo = new RptDrCr();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                compinfo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void lblfrmnovatporder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                WithoutVatPurchaseOrderList compinfo = new WithoutVatPurchaseOrderList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lblrptnovatporder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RptPurchaseorder compinfo = new RptPurchaseorder();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                compinfo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void lnkWOPurchaseList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                WithoutVatPurchaseOrderList compinfo = new WithoutVatPurchaseOrderList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkWOPurchaseReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RptWithoutVatAllPurchase compinfo = new RptWithoutVatAllPurchase();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                compinfo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void lnkWOCustomerBillList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmNovatBillList compinfo = new frmNovatBillList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkWOCustomerBillReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RptNoVatBillList compinfo = new RptNoVatBillList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                compinfo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void lnkTotalPurSale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmDrCr compinfo = new frmDrCr();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkTotalPurSaleReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                RptDrCr compinfo = new RptDrCr();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                compinfo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void lnkWOStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmVatRemainingStock compinfo = new frmVatRemainingStock();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkOriginalStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmOriginalStock a = new frmOriginalStock();
                a.WindowState = System.Windows.Forms.FormWindowState.Normal;
                a.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                a.Show();
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

        private void lnkWOProductWisePurchase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                NovatProductPurchaseInfo ppurchase = new NovatProductPurchaseInfo();
                ppurchase.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkFakeProductWiseSale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FakeProductwisesale fsale = new FakeProductwisesale();
                fsale.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkCashBook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmDayBook CashBook = new frmDayBook("N");
                CashBook.WindowState = FormWindowState.Normal;
                CashBook.StartPosition = FormStartPosition.CenterScreen;
                CashBook.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkFakeBillLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FrmFakeCustomerLedger1 FakeBillLedger = new FrmFakeCustomerLedger1();
                FakeBillLedger.WindowState = FormWindowState.Normal;
                FakeBillLedger.StartPosition = FormStartPosition.CenterScreen;
                FakeBillLedger.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkFakeBillRemPayment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmFakeRemainingpayment FakeBillRemPayment = new frmFakeRemainingpayment();
                FakeBillRemPayment.WindowState = FormWindowState.Normal;
                FakeBillRemPayment.StartPosition = FormStartPosition.CenterScreen;
                FakeBillRemPayment.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void frmPersonalDetail_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void lnkStockTransfer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmStockTransfer StockTransfer = new frmStockTransfer(false);
                StockTransfer.WindowState = FormWindowState.Normal;
                StockTransfer.StartPosition = FormStartPosition.CenterScreen;
                StockTransfer.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkWOAddStock_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmAddStock AddStock = new frmAddStock();
                AddStock.WindowState = FormWindowState.Normal;
                AddStock.StartPosition = FormStartPosition.CenterScreen;
                AddStock.Show();
            }
            catch (Exception)
            { }
        }

        private void lnkOriginalCustomerLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmCustomerLedger CustReceipt = new frmCustomerLedger("All");
                CustReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                CustReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                CustReceipt.Show();
            }
            catch (Exception)
            { }
        }

        private void lnkEstimateCustLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmCustomerLedger CustReceipt = new frmCustomerLedger("Estimate");
                CustReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                CustReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                CustReceipt.Show();
            }
            catch (Exception)
            { }
        }

        private void lnkOriginalSuppLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmSupplierLedger CustReceipt = new frmSupplierLedger();
                CustReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                CustReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                CustReceipt.Show();
            }
            catch (Exception)
            { }
        }

        private void lnkEstimateSuppLedger_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmSupplierLedger CustReceipt = new frmSupplierLedger("Estimate");
                CustReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                CustReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                CustReceipt.Show();
            }
            catch (Exception)
            { }
        }

        private void lnkWOProductWiseSale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                NovatProductsaleinfo psale = new NovatProductsaleinfo();
                psale.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkFakeBill_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmCustomerBill srpt = new frmCustomerBill(3, "a");
                srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                srpt.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkFakeBiiList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmFakeCustomerBillList fakebilllist = new frmFakeCustomerBillList();
                fakebilllist.ShowDialog();
            }
            catch (Exception)
            { }
        }
    }
}