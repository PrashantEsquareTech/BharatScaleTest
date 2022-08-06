using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class MDINewForm : Form
    {
        public SqlConnection Connection;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        decimal DrT = 0, CrT = 0;
        private static System.Timers.Timer aTimer;
        public string separateLedger = "";
        byte[] @photologo;

        public MDINewForm()
        {
            InitializeComponent();
            SettingRepository setRepo = new SettingRepository();
            Setting SettingData = new Setting();
            SettingData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SettingData == null)
            {
                try
                {
                    frmSettings Setting = new frmSettings();
                    Setting.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    Setting.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    Setting.ShowDialog();
                }
                catch (Exception)
                { }
            }
            else
            {
                if (SettingData.PoultryQuotations == "Y")
                {
                   
                }
                else
                {
                  
                }
            }
            try
            {
                separateLedger = SettingData.SeparationLedger;
            }
            catch (Exception)
            { }

            GodownStockRepository GSRepo = new GodownStockRepository();
            GodownStock GSData = new GodownStock();
            GSData = GSRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (GSData != null)
            {
            
            }

            GSTMasterRepository GSTrepo = new GSTMasterRepository();
            List<GSTMaster> gstdata = new List<GSTMaster>();
            gstdata = GSTrepo.GetAll().ToList();
            StateMasterRepository srepo = new StateMasterRepository();
            List<StateMaster> statedata = new List<StateMaster>();
            statedata = srepo.GetAll().ToList();
            AccountingGroupMasterRepository arepo = new AccountingGroupMasterRepository();
            List<AccountingGroupMaster> accdata = new List<AccountingGroupMaster>();
            accdata = arepo.GetAll().ToList();
            FestivalRepository festivalRepo = new FestivalRepository();
            List<Festival> festivalData = new List<Festival>();
            festivalData = festivalRepo.GetAll().ToList();
            FestivalTemplateRepository festTempRepo = new FestivalTemplateRepository();
            List<FestivalTemplate> festTempData = new List<FestivalTemplate>();
            festTempData = festTempRepo.GetAll().ToList();

            if (gstdata.Count < 8 || statedata.Count < 37 || accdata.Count < 34 || festivalData.Count < 25 || festTempData.Count < 207)
            {
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPGSTnState", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 1000;
                    command.ExecuteNonQuery();
                    db.CloseConnection();
                    command.Dispose();
                }
                catch (Exception)
                { }
            }
            ChangeNameAndImage();

            // Create a timer and set a two second interval.
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
        }

        private void OnTimedEvent1(object souurce, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    this.notifyIcon1.BalloonTipText = "Some Credit Alert Occured Please Check Credit List.";
                    this.notifyIcon1.BalloonTipTitle = "Credit Alert!";
                    this.notifyIcon1.Icon = SystemIcons.Exclamation;
                    this.notifyIcon1.ShowBalloonTip(1000);
                }
            }
            catch (Exception)
            { }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPStockAlert", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                db.CloseConnection();
                if (dt.Rows.Count > 0)
                {
                    this.notifyIcon1.BalloonTipText = "Some Products Stock is minimum or below the alert value please check.";
                    this.notifyIcon1.BalloonTipTitle = "Stock Alert!";
                    this.notifyIcon1.Icon = SystemIcons.Exclamation;
                    this.notifyIcon1.ShowBalloonTip(1000);
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void ChangeNameAndImage()
        {
            try
            {
                SettingRepository setRepo = new SettingRepository();
                string mdiname = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault().MDIName;
                LoginRepository logRepo = new LoginRepository();
                string username = logRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault().Username;
                this.Text = mdiname + " ( " + CommonMethod.TransactionYear + " )" + "  " + "User Name : " + username;
                string poweredbyname = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault().PoweredByName;
                if (poweredbyname != "")
                    toolStripStatusLabel.Text = poweredbyname;
            }
            catch (Exception)
            { }
            try
            {
                SettingRepository setRepo = new SettingRepository();
                var data = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (data.MDIImage != null)
                {
                    @photologo = (byte[])data.MDIImage;
                    MemoryStream ms = new MemoryStream(@photologo);
                    this.BackgroundImage = Image.FromStream(ms);
                }
            }
            catch (Exception)
            { }
        }

        public void PFAC(string vat)
        {
            try
            {
                List<decimal> Dramt = new List<decimal>();
                List<decimal> Cramt = new List<decimal>();
                decimal DrTotal = 0, CrTotal = 0;

                string fromDate = cm.ValidFromDate(DateTime.Now.Date);
                string toDate = cm.ValidToDate(DateTime.Now.Date);
                //Left Side Dr
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCashBook", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Dr";
                    command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = vat;
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DrTotal = DrTotal + Convert.ToDecimal(dt.Rows[i][2].ToString());
                    }
                    db.CloseConnection();
                    reader.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }

                //Right Side Cr
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCashBook", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Cr";
                    command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = vat;
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CrTotal = CrTotal + Convert.ToDecimal(dt.Rows[i][2].ToString());
                    }
                    db.CloseConnection();
                    reader.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
                DrT = DrTotal;
                CrT = CrTotal;
            }
            catch (Exception)
            { }
        }

        public void dailySP()
        {
            try
            {
                int flag = 0;
                decimal openigcash = 0, closingcash = 0, openingbalance = 0;
                string input = "";
                CounterCashRepository CounterCashRepo3 = new CounterCashRepository();
                CounterCash CounterCashDataFirst = new CounterCash();
                CounterCashDataFirst = CounterCashRepo3.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashDataFirst == null)
                {
                    input = Microsoft.VisualBasic.Interaction.InputBox("Input Counter Cash of Today", "Opening Balance", "" + openingbalance + "", 300, 250);
                    CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                    CounterCash CounterCashData1 = new CounterCash();
                    CounterCashData1.Date = DateTime.Now.Date;
                    CounterCashData1.OpeningCash = Convert.ToDecimal(input);
                    CounterCashData1.ClosingCash = Convert.ToDecimal(0);
                    CounterCashData1.CompId = CommonMethod.CompId;
                    CounterCashRepo1.Add(CounterCashData1);
                    CounterCashRepo1.Save();
                    CounterCashRepo1.Dispose();
                }
                CounterCashRepository CounterCashRepo = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo.GetAll().Where(t => t.Date.Value.Date.ToString("dd/MM/yyyy") == DateTime.Now.Date.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (CounterCashData != null)
                {
                    openigcash = Convert.ToDecimal(CounterCashData.OpeningCash);
                    closingcash = Convert.ToDecimal(CounterCashData.ClosingCash);
                    flag = 1;
                }
                else
                {
                    CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                    CounterCash CounterCashDataPre = new CounterCash();
                    CounterCashDataPre = CounterCashRepo1.GetAll().Where(t => t.Date.Value.Date < DateTime.Now.Date && t.CompId == CommonMethod.CompId).LastOrDefault();
                    if (CounterCashDataPre.Date.Value.ToString("dd/MM/yyyy") != DateTime.Now.Date.ToString("dd/MM/yyyy"))
                        openigcash = Convert.ToDecimal(CounterCashDataPre.ClosingCash);
                    else
                    {
                        openigcash = 0;
                        closingcash = 0;
                    }
                    flag = 0;
                    CounterCashRepo1.Dispose();
                }
                try
                {
                    PFAC("N");
                }
                catch (Exception)
                { }

                closingcash = Convert.ToDecimal(openigcash) - Convert.ToDecimal(CrT) + Convert.ToDecimal(DrT);
                if (flag == 0)
                {
                    CounterCashRepository CounterCashRepo1 = new CounterCashRepository();
                    CounterCash CounterCashData1 = new CounterCash();
                    CounterCashData1.Date = DateTime.Now.Date;
                    CounterCashData1.OpeningCash = Convert.ToDecimal(openigcash);
                    CounterCashData1.ClosingCash = Convert.ToDecimal(closingcash);
                    CounterCashData1.CompId = CommonMethod.CompId;
                    CounterCashRepo1.Add(CounterCashData1);
                    CounterCashRepo1.Save();
                    CounterCashRepo1.Dispose();
                }
                else if (flag == 1)
                {
                    CounterCashRepository CounterCashRepo2 = new CounterCashRepository();
                    CounterCash CounterCashData1 = new CounterCash();
                    CounterCashData1 = CounterCashRepo2.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    CounterCashData1.OpeningCash = Convert.ToDecimal(openigcash);
                    CounterCashData1.ClosingCash = Convert.ToDecimal(closingcash);
                    CounterCashData1.CompId = CommonMethod.CompId;
                    CounterCashRepo2.Edit(CounterCashData1);
                    CounterCashRepo2.Save();
                    CounterCashRepo2.Dispose();
                }
                CounterCashRepo3.Dispose();
                CounterCashRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void CompanyInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCompanyInformation CompInfo = new frmCompanyInformation();
                CompInfo.WindowState = FormWindowState.Normal;
                CompInfo.StartPosition = FormStartPosition.CenterScreen;
                CompInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void UnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUnitInformation UnitInfo = new frmUnitInformation();
                UnitInfo.WindowState = FormWindowState.Normal;
                UnitInfo.StartPosition = FormStartPosition.CenterScreen;
                UnitInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void MFGCompToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmManufactureCompanyInfo MfgInfo = new frmManufactureCompanyInfo();
                MfgInfo.WindowState = FormWindowState.Normal;
                MfgInfo.StartPosition = FormStartPosition.CenterScreen;
                MfgInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void ProductInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmProductInformation ProductInfo = new frmProductInformation();
                ProductInfo.WindowState = FormWindowState.Normal;
                ProductInfo.StartPosition = FormStartPosition.CenterScreen;
                ProductInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void CustomerInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmcustomer CustomerInfo = new frmcustomer();
                CustomerInfo.WindowState = FormWindowState.Normal;
                CustomerInfo.StartPosition = FormStartPosition.CenterScreen;
                CustomerInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void SupplierInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplierInformation SupplierInfo = new frmSupplierInformation();
                SupplierInfo.WindowState = FormWindowState.Normal;
                SupplierInfo.StartPosition = FormStartPosition.CenterScreen;
                SupplierInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void BankInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBankInformation BankInfo = new frmBankInformation();
                BankInfo.WindowState = FormWindowState.Normal;
                BankInfo.StartPosition = FormStartPosition.CenterScreen;
                BankInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void ChangePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePassword ChangePass = new ChangePassword();
                ChangePass.WindowState = FormWindowState.Normal;
                ChangePass.StartPosition = FormStartPosition.CenterScreen;
                ChangePass.Show();
            }
            catch (Exception)
            { }
        }

        private void ExittoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoginRepository LoginRepo = new LoginRepository();
                Login LoginData = new Login();
                LoginData = LoginRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                LoginData.Status = 0;
                LoginRepo.Edit(LoginData);
                LoginRepo.Save();
                foreach (Form f in Application.OpenForms)
                {
                    if (!f.IsDisposed)
                        f.Dispose();
                }
                Application.Exit();
            }
            catch (Exception)
            { }
        }

        private void PurOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingRepository sRepo = new SettingRepository();
                Setting sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.ShowLoginWindowOnForm == "P")
                {
                    pnl2.Visible = true;
                    this.ActiveControl = txtusername;
                }
                else
                {
                    frmPurchaseOrder Purchaseorder = new frmPurchaseOrder();
                    Purchaseorder.WindowState = FormWindowState.Normal;
                    Purchaseorder.StartPosition = FormStartPosition.CenterScreen;
                    Purchaseorder.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void PurOrderListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPurchaseOrderList PurchaseorderList = new frmPurchaseOrderList();
                PurchaseorderList.WindowState = FormWindowState.Normal;
                PurchaseorderList.StartPosition = FormStartPosition.CenterScreen;
                PurchaseorderList.Show();
            }
            catch (Exception)
            { }
        }

        private void ProductPurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProductPurchaseinfo ProductPurchaseInfo = new ProductPurchaseinfo();
                ProductPurchaseInfo.WindowState = FormWindowState.Normal;
                ProductPurchaseInfo.StartPosition = FormStartPosition.CenterScreen;
                ProductPurchaseInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void QuotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuotation Quotation = new frmQuotation();
                Quotation.WindowState = FormWindowState.Normal;
                Quotation.StartPosition = FormStartPosition.CenterScreen;
                Quotation.Show();
            }
            catch (Exception)
            { }
        }

        private void DCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDelieveryChallan DeliveryChallan = new frmDelieveryChallan();
                DeliveryChallan.WindowState = FormWindowState.Normal;
                DeliveryChallan.StartPosition = FormStartPosition.CenterScreen;
                DeliveryChallan.Show();
            }
            catch (Exception)
            { }
        }

        private void CustBilltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmServiceInvoice CustomerBill = new frmServiceInvoice();
                CustomerBill.WindowState = System.Windows.Forms.FormWindowState.Normal;
                CustomerBill.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                CustomerBill.Show();
            }
            catch (Exception)
            { }
        }

        private void OrderBooktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmorderbooking OrderBooking = new frmorderbooking();
                OrderBooking.WindowState = FormWindowState.Normal;
                OrderBooking.StartPosition = FormStartPosition.CenterScreen;
                OrderBooking.Show();
            }
            catch (Exception)
            { }
        }

        private void QTListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuotationList QuotationList = new frmQuotationList();
                QuotationList.WindowState = FormWindowState.Normal;
                QuotationList.StartPosition = FormStartPosition.CenterScreen;
                QuotationList.Show();
            }
            catch (Exception)
            { }
        }

        private void DCListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDeliveryChallanList DCList = new frmDeliveryChallanList();
                DCList.WindowState = FormWindowState.Normal;
                DCList.StartPosition = FormStartPosition.CenterScreen;
                DCList.Show();
            }
            catch (Exception)
            { }
        }

        private void CustBillListtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmServiceInvoiceList CustBillList = new frmServiceInvoiceList();
                CustBillList.WindowState = FormWindowState.Normal;
                CustBillList.StartPosition = FormStartPosition.CenterScreen;
                CustBillList.Show();
            }
            catch (Exception)
            { }
        }

        private void OrderBookListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmIncompleteorderbook OrderBookingList = new frmIncompleteorderbook();
                OrderBookingList.WindowState = FormWindowState.Normal;
                OrderBookingList.StartPosition = FormStartPosition.CenterScreen;
                OrderBookingList.Show();
            }
            catch (Exception)
            { }
        }

        private void ProductWiseSaletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProductSaleinfo ProductSale = new ProductSaleinfo();
                ProductSale.WindowState = FormWindowState.Normal;
                ProductSale.StartPosition = FormStartPosition.CenterScreen;
                ProductSale.Show();
            }
            catch (Exception)
            { }
        }

        private void CreditListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCreditList CreditList = new frmCreditList();
                CreditList.WindowState = FormWindowState.Normal;
                CreditList.StartPosition = FormStartPosition.CenterScreen;
                CreditList.Show();
            }
            catch (Exception)
            { }
        }

        private void AddStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAddStockGST AddStock = new frmAddStockGST();
                AddStock.WindowState = FormWindowState.Normal;
                AddStock.StartPosition = FormStartPosition.CenterScreen;
                AddStock.Show();
            }
            catch (Exception)
            { }
        }

        private void RemStocktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Stock RemStock = new Stock();
                RemStock.WindowState = FormWindowState.Normal;
                RemStock.StartPosition = FormStartPosition.CenterScreen;
                RemStock.Show();
            }
            catch (Exception)
            { }
        }

        private void DeadStocktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDeadStock DeadStock = new frmDeadStock();
                DeadStock.WindowState = FormWindowState.Normal;
                DeadStock.StartPosition = FormStartPosition.CenterScreen;
                DeadStock.Show();
            }
            catch (Exception)
            { }
        }

        private void DeadStockListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDeadStockList DeadStockList = new frmDeadStockList();
                DeadStockList.WindowState = FormWindowState.Normal;
                DeadStockList.StartPosition = FormStartPosition.CenterScreen;
                DeadStockList.Show();
            }
            catch (Exception)
            { }
        }

        private void StockAlerttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StockAlert StockAlert = new StockAlert();
                StockAlert.WindowState = FormWindowState.Normal;
                StockAlert.StartPosition = FormStartPosition.CenterScreen;
                StockAlert.Show();
            }
            catch (Exception)
            { }
        }

        private void ProductInfoRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptProductInformation ProductInfoReport = new RptProductInformation();
                ProductInfoReport.Show();
            }
            catch (Exception)
            { }
        }

        private void SupplierInfoRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptSupplierinfo SupplierInfoReport = new RptSupplierinfo();
                SupplierInfoReport.Show();
            }
            catch (Exception)
            { }
        }

        private void BankInfoRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptBankinfo BankInfoReport = new RptBankinfo();
                BankInfoReport.Show();
            }
            catch (Exception)
            { }
        }

        private void CustInfoRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptCustomerInfo CustInfoReport = new RptCustomerInfo();
                CustInfoReport.Show();
            }
            catch (Exception)
            { }
        }

        private void PORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptPurchaseorder PurOrderReport = new RptPurchaseorder();
                PurOrderReport.Show();
            }
            catch (Exception)
            { }
        }

        private void CustBillRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptCustomerbill CustBillReport = new RptCustomerbill();
                CustBillReport.Show();
            }
            catch (Exception)
            { }
        }

        private void DCRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptDeliveryChallan DCReport = new RptDeliveryChallan();
                DCReport.Show();
            }
            catch (Exception)
            { }
        }

        private void ExpencesRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptExpences ExpencesReport = new RptExpences();
                ExpencesReport.Show();
            }
            catch (Exception)
            { }
        }

        private void RemStockRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptRemainingtock remainingqty = new RptRemainingtock('y');
                remainingqty.Show();
                //RptVatRemainingStock RemStockReport = new RptVatRemainingStock();
                //RemStockReport.Show();
            }
            catch (Exception)
            { }
        }

        private void RemPayCustRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptRemainingPayment RemPaymentReport = new RptRemainingPayment();
                RemPaymentReport.Show();
            }
            catch (Exception)
            { }
        }

        private void AllPurRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptAllPurchase AllPurReport = new RptAllPurchase("", null, "", 0, "", "", "");
                AllPurReport.Show();
            }
            catch (Exception)
            { }
        }

        private void AllCustBillRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptAllCustomerBill AllCustBillReport = new RptAllCustomerBill(1, "", "", null, 0, "", "", "", "");
                AllCustBillReport.Show();
            }
            catch (Exception)
            { }
        }

        private void CustReceiptRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptReceipt CustReceiptReport = new RptReceipt();
                CustReceiptReport.Show();
            }
            catch (Exception)
            { }
        }

        private void SuppReceiptRtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptSupplierReceipt SuppReceiptReport = new RptSupplierReceipt();
                SuppReceiptReport.Show();
            }
            catch (Exception)
            { }
        }

        private void ExpencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmExpences Expences = new frmExpences();
                Expences.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Expences.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                Expences.Show();
            }
            catch (Exception)
            { }
        }

        private void SuppLedACToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (separateLedger == "Y")
                {
                    frmSupplierLedger SuppReceipt = new frmSupplierLedger("Tax");
                    SuppReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    SuppReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    SuppReceipt.Show();
                }
                else
                {
                    frmSupplierLedger SuppReceipt = new frmSupplierLedger();
                    SuppReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    SuppReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    SuppReceipt.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void CustLedACToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (separateLedger == "Y")
                {
                    frmCustomerLedger CustReceipt = new frmCustomerLedger("Tax");
                    CustReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    CustReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    CustReceipt.Show();
                }
                else
                {
                    frmCustomerLedger CustReceipt = new frmCustomerLedger();
                    CustReceipt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    CustReceipt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    CustReceipt.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void BankStatementtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBankStatement BankStatements = new frmBankStatement();
                BankStatements.WindowState = System.Windows.Forms.FormWindowState.Normal;
                BankStatements.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                BankStatements.Show();
            }
            catch (Exception)
            { }
        }

        private void AllProductInfotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAllProductInformation AllProductInfo = new frmAllProductInformation();
                AllProductInfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                AllProductInfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                AllProductInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void RemPayCusttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRemainingpayment RemainingPayment = new frmRemainingpayment();
                RemainingPayment.WindowState = System.Windows.Forms.FormWindowState.Normal;
                RemainingPayment.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                RemainingPayment.Show();
            }
            catch (Exception)
            { }
        }

        private void DBBackuptoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDbBackUp DBBAckup = new frmDbBackUp();
                DBBAckup.WindowState = System.Windows.Forms.FormWindowState.Normal;
                DBBAckup.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                DBBAckup.Show();
            }
            catch (Exception)
            { }
        }

        private void MsgAlerttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMessageSend MessageSend = new frmMessageSend();
                MessageSend.WindowState = System.Windows.Forms.FormWindowState.Normal;
                MessageSend.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                MessageSend.Show();
            }
            catch (Exception)
            { }
        }

        private void SendEMailtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSendMail SendMail = new frmSendMail();
                SendMail.WindowState = System.Windows.Forms.FormWindowState.Normal;
                SendMail.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                SendMail.Show();
            }
            catch (Exception)
            { }
        }

        private void SendMailListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSendMailList SendMailList = new frmSendMailList();
                SendMailList.WindowState = System.Windows.Forms.FormWindowState.Normal;
                SendMailList.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                SendMailList.Show();
            }
            catch (Exception)
            { }
        }

        private void SettingtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSettings Setting = new frmSettings();
                Setting.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Setting.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                Setting.Show();
            }
            catch (Exception)
            { }
        }

        private void RemindertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmReminder Reminder = new frmReminder();
                Reminder.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Reminder.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                Reminder.Show();
            }
            catch (Exception)
            { }
        }

        private void CalculatortoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("calc.exe");
            }
            catch (Exception)
            { }
        }

        private void PainttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("Mspaint.exe");
            }
            catch (Exception)
            { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F12))
            {
                frmPersonalDetail PersonalDetail = new frmPersonalDetail();
                PersonalDetail.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PersonalDetail.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PersonalDetail.Show();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void FestivaltoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSendFestivalMessage Reminder = new frmSendFestivalMessage();
                Reminder.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Reminder.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                Reminder.Show();
            }
            catch (Exception)
            { }
        }

        private void MDINewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CompanyInformationRepository compRepo = new CompanyInformationRepository();
                string compName = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault().CompanyName;
                string[] drives = Environment.GetLogicalDrives();
                saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                string path = "";
                if (drives.Length > 1)
                    path = @"" + drives[1] + "" + compName + "\\" + " Database Backup/";  // Give the specific path
                else
                    path = @"" + drives[0] + "" + compName + "\\" + " Database Backup/";
                SettingRepository Srepo = new SettingRepository();
                var sdata = Srepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata != null)
                {
                    if (!string.IsNullOrEmpty(sdata.DataBaseBackupPath))
                        path = sdata.DataBaseBackupPath.Replace("//", "/") + compName + "/" + " Database Backup/";
                }
                if (!(Directory.Exists(path)))
                    Directory.CreateDirectory(path);
                var connectionString = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                Connection = new SqlConnection(connectionString);
                string[] drvname = drives[1].Split(':');
                var backupFolder1 = drvname[0] + ":/" + compName + " Database Backup/";
                var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
                var backupFileName = String.Format("{0}{1}{2}.bak",
                     path, sqlConStrBuilder.InitialCatalog,
                     DateTime.Now.ToString("ddMMyyyy"));
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                        sqlConStrBuilder.InitialCatalog, backupFileName);
                    using (var command = new SqlCommand(query, Connection))
                    {
                        Connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Backup Created Sucessfully.", "Success");
                        Connection.Close();
                    }
                }
                compRepo.Dispose();
            }
            catch (Exception)
            { }
            try
            {
                LoginRepository LoginRepo = new LoginRepository();
                Login LoginData = new Login();
                LoginData = LoginRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                LoginData.Status = 0;
                LoginRepo.Edit(LoginData);
                LoginRepo.Save();
                frmloginForm l = new frmloginForm();
                l.Close();
                foreach (Form f in Application.OpenForms)
                {
                    if (!f.IsDisposed)
                        f.Dispose();
                }
                Application.Exit();
            }
            catch (Exception)
            { }
        }

        private void DailySalePurchaseMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCounterCash Reminder = new frmCounterCash();
                Reminder.WindowState = System.Windows.Forms.FormWindowState.Normal;
                Reminder.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                Reminder.Show();
            }
            catch (Exception)
            { }
        }

        private void DailySaleMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingRepository sRepo = new SettingRepository();
                Setting sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.ShowLoginWindowOnForm == "D")
                {
                    pnl2.Visible = true;
                    this.ActiveControl = txtusername;
                }
                else
                {
                    frmDailySaleDetail Reminder = new frmDailySaleDetail();
                    Reminder.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    Reminder.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    Reminder.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void PurRetListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPurchaseReturnList PurReturnList = new frmPurchaseReturnList();
                PurReturnList.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturnList.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturnList.Show();
            }
            catch (Exception)
            { }
        }

        private void SaleRetListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSaleReturnList SaleReturnList = new frmSaleReturnList();
                SaleReturnList.WindowState = System.Windows.Forms.FormWindowState.Normal;
                SaleReturnList.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                SaleReturnList.Show();
            }
            catch (Exception)
            { }
        }

        private void totalProfitLosstoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmTradingnPLAC totalProfit = new frmTradingnPLAC("A");
                totalProfit.WindowState = System.Windows.Forms.FormWindowState.Normal;
                totalProfit.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                totalProfit.Show();
            }
            catch (Exception)
            { }
        }

        private void CreditSupplierstoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBalanceSuppliers totalProfit = new frmBalanceSuppliers();
                totalProfit.WindowState = System.Windows.Forms.FormWindowState.Normal;
                totalProfit.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                totalProfit.Show();
            }
            catch (Exception)
            { }
        }

        private void CashBooktoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmDayBook DayBook = new frmDayBook("Y");
                DayBook.WindowState = FormWindowState.Normal;
                DayBook.StartPosition = FormStartPosition.CenterScreen;
                DayBook.Show();
            }
            catch (Exception)
            { }
        }

        private void OtherincometoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmOtherIncome Income = new frmOtherIncome();
                Income.WindowState = FormWindowState.Normal;
                Income.StartPosition = FormStartPosition.CenterScreen;
                Income.Show();
            }
            catch (Exception)
            { }
        }

        private void AllCounterCashtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptDailySP rptcountercash = new RptDailySP(CommonMethod.startdate, CommonMethod.enddate);
                rptcountercash.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void microSoftPowerPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("powerpnt.exe");
            }
            catch (Exception)
            { }
        }

        private void microSoftOffice2007ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("WINWORD.EXE");
            }
            catch (Exception)
            { }
        }

        private void microSoftExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("excel.exe");
            }
            catch (Exception)
            { }
        }

        private void notePadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("notepad.exe");
            }
            catch (Exception)
            { }
        }

        private void POPtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPurchaseOrderPlace POP = new frmPurchaseOrderPlace();
                POP.WindowState = FormWindowState.Normal;
                POP.StartPosition = FormStartPosition.CenterScreen;
                POP.Show();
            }
            catch (Exception)
            { }
        }

        private void compareLedgersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCompareLedger comledger = new frmCompareLedger();
                comledger.WindowState = FormWindowState.Normal;
                comledger.StartPosition = FormStartPosition.CenterScreen;
                comledger.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void SRCustWisetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSaleBillReturnCustomerWise SRC = new frmSaleBillReturnCustomerWise();
                SRC.WindowState = FormWindowState.Normal;
                SRC.StartPosition = FormStartPosition.CenterScreen;
                SRC.Show();
            }
            catch (Exception)
            { }
        }

        private void MDINewForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;

                frmAlertInterface alertwin = new frmAlertInterface();
                alertwin.MdiParent = this.MdiParent;
                alertwin.StartPosition = FormStartPosition.CenterScreen;
                alertwin.WindowState = FormWindowState.Normal;
                alertwin.Show();
            }
            catch (Exception)
            { }
        }

        private void GodownInfotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmGodownMaster GodownInfo = new frmGodownMaster();
                GodownInfo.WindowState = FormWindowState.Normal;
                GodownInfo.StartPosition = FormStartPosition.CenterScreen;
                GodownInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void ReceiptChallantoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRecieptChallan RCInfo = new frmRecieptChallan();
                RCInfo.WindowState = FormWindowState.Normal;
                RCInfo.StartPosition = FormStartPosition.CenterScreen;
                RCInfo.Show();
            }
            catch (Exception)
            { }
        }

        private void RCListtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRecieptChallanList RCList = new frmRecieptChallanList();
                RCList.WindowState = FormWindowState.Normal;
                RCList.StartPosition = FormStartPosition.CenterScreen;
                RCList.Show();
            }
            catch (Exception)
            { }
        }

        private void MDINewForm_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                LoginRepository LoginRepo = new LoginRepository();
                Login LoginData = new Login();
                LoginData = LoginRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (LoginData.Status == 0)
                {
                    MessageBox.Show("This User is Forcefully Logged out from another device.", "Error");
                    foreach (Form f in Application.OpenForms)
                    {
                        if (!f.IsDisposed)
                            f.Dispose();
                    }
                    Application.Exit();
                }
            }
            catch (Exception)
            { }
        }

        private void StTsfrtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GodownStockRepository GSRepo = new GodownStockRepository();
                GodownStock GSData = new GodownStock();
                GSData = GSRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (GSData != null)
                {
                    frmStockTransfer StockTransfer = new frmStockTransfer(true);
                    StockTransfer.WindowState = FormWindowState.Normal;
                    StockTransfer.StartPosition = FormStartPosition.CenterScreen;
                    StockTransfer.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                StockAlert StockAlert = new StockAlert("a");
                StockAlert.WindowState = FormWindowState.Normal;
                StockAlert.StartPosition = FormStartPosition.CenterScreen;
                StockAlert.Show();
            }
            catch (Exception)
            { }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            try
            {
                StockAlert StockAlert = new StockAlert("a");
                StockAlert.WindowState = FormWindowState.Normal;
                StockAlert.StartPosition = FormStartPosition.CenterScreen;
                StockAlert.Show();
            }
            catch (Exception)
            { }
        }

        private void productCodeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCodeInfo codeinf = new frmCodeInfo();
                codeinf.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void speedBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustSpeedBill speedbill = new frmCustSpeedBill();
                speedbill.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void b2BTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSummaryDetails summarydetails = new frmSummaryDetails();
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void b2BReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptB2B summarydetails = new RptB2B("R1");
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void b2CLReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptB2Cl summarydetails = new RptB2Cl();
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void b2cstoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                RptB2CS summarydetails = new RptB2CS();
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void CDNRtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                RptCDNR summarydetails = new RptCDNR();
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void CDNURtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                RptCDNUR summarydetails = new RptCDNUR();
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void HSNReporttoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                RptHSN summarydetails = new RptHSN("R1");
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCashBook cashBook = new frmCashBook();
                cashBook.WindowState = FormWindowState.Normal;
                cashBook.StartPosition = FormStartPosition.CenterScreen;
                cashBook.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void printerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrintSetting printsetting = new frmPrintSetting();
                printsetting.WindowState = FormWindowState.Normal;
                printsetting.StartPosition = FormStartPosition.CenterScreen;
                printsetting.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmProductInMarathi productmarathi = new frmProductInMarathi();
                productmarathi.WindowState = FormWindowState.Normal;
                productmarathi.StartPosition = FormStartPosition.CenterScreen;
                productmarathi.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void BalSheettoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmFinalBalanceSheet BalanceSheet = new frmFinalBalanceSheet();
                BalanceSheet.WindowState = FormWindowState.Normal;
                BalanceSheet.StartPosition = FormStartPosition.CenterScreen;
                BalanceSheet.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void AllReporttoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSummaryDetails AllReport = new frmSummaryDetails(1);
                AllReport.WindowState = FormWindowState.Normal;
                AllReport.StartPosition = FormStartPosition.CenterScreen;
                AllReport.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void saleReturnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSaleBillReturn SaleReturn = new frmSaleBillReturn();
                SaleReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                SaleReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                SaleReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPurchaseReturn PurReturn = new frmPurchaseReturn();
                PurReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void rateDifferenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerCreditDebitNote PurReturn = new frmCustomerCreditDebitNote("CN");
                PurReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void rateDifferenceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplierCreditDebitNotecs PurReturn = new frmSupplierCreditDebitNotecs("DN");
                PurReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void debitNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomerCreditDebitNote PurReturn = new frmCustomerCreditDebitNote("DN");
                PurReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void creditNoteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplierCreditDebitNotecs PurReturn = new frmSupplierCreditDebitNotecs("CN");
                PurReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void journalEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmJournalEntry PurReturn = new frmJournalEntry();
                PurReturn.WindowState = System.Windows.Forms.FormWindowState.Normal;
                PurReturn.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                PurReturn.Show();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                frmTaxSlab taxslab = new frmTaxSlab();
                taxslab.WindowState = System.Windows.Forms.FormWindowState.Normal;
                taxslab.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                taxslab.Show();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            try
            {
                frmDeptartement dept = new frmDeptartement();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                frmcolor color_form = new frmcolor();
                color_form.WindowState = System.Windows.Forms.FormWindowState.Normal;
                color_form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                color_form.Show();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                frmCategory category = new frmCategory();
                category.WindowState = System.Windows.Forms.FormWindowState.Normal;
                category.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                category.Show();
            }
            catch (Exception)
            { }
        }

        private void repackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRepacking repack = new frmRepacking();
                repack.WindowState = System.Windows.Forms.FormWindowState.Normal;
                repack.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                repack.Show();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                frmSubGroupMaster subgroup = new frmSubGroupMaster();
                subgroup.WindowState = System.Windows.Forms.FormWindowState.Normal;
                subgroup.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                subgroup.Show();
            }
            catch (Exception) { }
        }

        private void stockRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmStockRegister stockregister = new frmStockRegister();
                stockregister.WindowState = System.Windows.Forms.FormWindowState.Normal;
                stockregister.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                stockregister.Show();
            }
            catch (Exception) { }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                frmSlabMaster slab = new frmSlabMaster();
                slab.WindowState = System.Windows.Forms.FormWindowState.Normal;
                slab.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                slab.ShowDialog();
            }
            catch (Exception) { }
        }

        private void propritermastertoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProprietorMaster pmast = new FrmProprietorMaster();
                pmast.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void propriterLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmProprietorLedger pledger = new frmProprietorLedger();
                pledger.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void RateSlabtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRateSlab rateSlab = new frmRateSlab();
                rateSlab.WindowState = System.Windows.Forms.FormWindowState.Normal;
                rateSlab.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                rateSlab.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void dataExportInTallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SettingRepository setRepo = new SettingRepository();
                Setting SettingData = new Setting();
                SettingData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (SettingData != null)
                {
                    if (SettingData.TallyKey == null || SettingData.TallyKey.Trim() == "".Trim())
                    {
                        frmTallyExportKey tallyKey = new frmTallyExportKey();
                        tallyKey.ShowDialog();
                    }
                    else
                    {
                        frmTallyExport tallyexport = new frmTallyExport();
                        tallyexport.ShowDialog();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void batchNExpiryStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BatchExpiryWithRemainingStock bstock = new BatchExpiryWithRemainingStock();
                bstock.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void VehiclereporttoolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                frmVehicleReport vehicleReport = new frmVehicleReport();
                vehicleReport.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void VehicletoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmVehicleInformation vehicleinfo = new frmVehicleInformation();
                vehicleinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtusername.Text != "" && txtpassword.Text != "")
                {
                    LoginRepository loginrepo = new LoginRepository();
                    AIOInventorySystem.Data.Model.Login itemdata = new Data.Model.Login();
                    itemdata = loginrepo.GetAll().Where(t => t.Username == txtusername.Text).FirstOrDefault();
                    if (itemdata != null)
                    {
                        SettingRepository setrepo = new SettingRepository();
                        Setting setdata = setrepo.GetAll().Where(t => t.FormPassword == txtpassword.Text).FirstOrDefault();
                        if (setdata != null)
                        {
                            pnl2.Visible = false;
                            txtpassword.Text = "";
                            txtusername.Text = "";
                            frmPurchaseOrder purchase = new frmPurchaseOrder();
                            purchase.WindowState = FormWindowState.Normal;
                            purchase.StartPosition = FormStartPosition.CenterScreen;
                            purchase.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Password is not correct.", "Warning");
                            this.ActiveControl = txtpassword;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username is not correct.", "Warning");
                        this.ActiveControl = txtusername;
                    }
                }
                else
                    MessageBox.Show("Enter UserName And Password.", "Warning");
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                pnl2.Visible = false;
                txtusername.Text = "";
                txtpassword.Text = "";
            }
            catch (Exception)
            { }
        }

        private void bonusLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBonus vehicleinfo = new frmBonus();
                vehicleinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void unPaidBonusListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUnpaidBonusList vehicleinfo = new frmUnpaidBonusList();
                vehicleinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                frmStaffInfo dept = new frmStaffInfo();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void projectAtGlanceQuotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProjectGlanceQuotation dept = new FrmProjectGlanceQuotation();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void nippleWaterSystemEzypanFeederSysytemQuotationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmNippleAndEzypanQuotation dept = new FrmNippleAndEzypanQuotation();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                frmQuotationImage dept = new frmQuotationImage();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void ventilationAndCoolingSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmVentilationAndCoolingSystem dept = new frmVentilationAndCoolingSystem();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void environmentControlSystemCurtainSystemSideCoolPadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmEnvoirmentControlSystemAndCurtain_System dept = new frmEnvoirmentControlSystemAndCurtain_System();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void ProfittoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCashProfit objfrm = new frmCashProfit();
                objfrm.WindowState = System.Windows.Forms.FormWindowState.Normal;
                objfrm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                objfrm.Show();
            }
            catch (Exception)
            { }
        }

        private void SaleRegistertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RptGSTR2 gstr2 = new RptGSTR2("R1");
                gstr2.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItemR2B2B_Click(object sender, EventArgs e)
        {
            try
            {
                RptB2B summarydetails = new RptB2B("R2");
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItemR2B2C_Click(object sender, EventArgs e)
        {
            try
            {
                RptB2B summarydetails = new RptB2B("B2CR2");
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItemR2Nill_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItemR2HSN_Click(object sender, EventArgs e)
        {
            try
            {
                RptHSN summarydetails = new RptHSN("R2");
                summarydetails.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItemPurregister_Click(object sender, EventArgs e)
        {
            try
            {
                RptGSTR2 gstr2 = new RptGSTR2("R2");
                gstr2.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void toolStripMenuItemSiteInfo_Click(object sender, EventArgs e)
        {
            try
            {
                frmSiteInfo dept = new frmSiteInfo();
                dept.WindowState = System.Windows.Forms.FormWindowState.Normal;
                dept.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                dept.Show();
            }
            catch (Exception)
            { }
        }

        private void RouteMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmRouteInfo route = new frmRouteInfo();
                route.WindowState = System.Windows.Forms.FormWindowState.Normal;
                route.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                route.Show();
            }
            catch (Exception)
            { }
        }
    }
}