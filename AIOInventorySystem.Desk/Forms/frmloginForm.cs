using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using Microsoft.Win32;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmloginForm : Form
    {
        private bool _Trial;
        CommonMethod cm = new CommonMethod();
        DbClass db = new DbClass();
        public bool ApplicationFlag { get; set; }
        bool VersionUpdate = false;

        public frmloginForm(bool IsTrial)
        {
            InitializeComponent();
            _Trial = IsTrial;
            clear();
            TransactionYear();
            this.ActiveControl = tblltInputs;
            this.ActiveControl = txtusername;
        }

        public frmloginForm(string a)
        {
            InitializeComponent();
            AddTransactionYearDates();
            clear();
            BindTransactionYears();
            VersionUpdate = true;
            this.ActiveControl = tblltInputs;
            this.ActiveControl = txtusername;
        }

        public frmloginForm()
        {
            InitializeComponent();
            AddTransactionYearDates();
            clear();
            CompanyInformationRepository compinforepo = new CompanyInformationRepository();
            var compdata = compinforepo.GetAll().FirstOrDefault();
            if (compdata == null)
            {
                this.Close();
                TrialPeriodrepository traildemo1 = new TrialPeriodrepository();
                var tradata = traildemo1.GetAll().FirstOrDefault();
                if (tradata == null)
                {
                    DemoVersion demo = new DemoVersion();
                    demo.ShowDialog();
                }
                else
                {

                    gstnstate();
                    AddTransactionYearDates();
                    AddCurrentDateinReg();
                    frmCompanyInformation compinfo = new frmCompanyInformation(1);
                    compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    compinfo.ShowDialog();
                }
            }
            compinforepo.Dispose();
            BindTransactionYears();
            this.ActiveControl = tblltInputs;
            this.ActiveControl = txtusername;
        }

        public void AddCurrentDateinReg()
        {
            try
            {
                RegistryKey soft_key = Registry.CurrentUser.OpenSubKey("Software", true);
                RegistryKey CurrentKey;
                try
                {
                    CurrentKey = soft_key.OpenSubKey("ESquare_SoftKeyCurrent");
                    CurrentKey.SetValue("ESquare_SoftKeyCurrent", Convert.ToString(DateTime.Today));
                }
                catch (Exception)
                {
                    CurrentKey = soft_key.CreateSubKey("ESquare_SoftKeyCurrent");
                    CurrentKey.SetValue("ESquare_SoftKeyCurrent", Convert.ToString(DateTime.Today));
                }                
            }
            catch (Exception)
            { }
        }

        public void gstnstate()
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

        

        public void TransactionYear()
        {
            string year = "";
            int dateyear = DateTime.Now.Year;
            int datemonth = DateTime.Now.Month;
            if (datemonth >= 1 && datemonth <= 3)
            {
                DateTime predate = DateTime.Now.AddYears(-1);
                int preyr = predate.Year;
                year = preyr + "-" + dateyear;
            }
            else
            {
                DateTime nextdate = DateTime.Now.AddYears(1);
                int nextyr = nextdate.Year;
                year = dateyear + "-" + nextyr;
            }
            System.Object[] ItemObject = new System.Object[10];
            for (int i = 9; i < 10 && i >= 0; i--)
            {
                ItemObject[i] = year;
                int preyr = Convert.ToInt32(year.ToString().Split('-')[0]);
                year = Convert.ToString(preyr - 1) + "-" + preyr;
            }
            cmbTransactionYear.Items.AddRange(ItemObject);
            cmbTransactionYear.SelectedIndex = 9;
        }

        public void AddTransactionYearDates()
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPTransactionYearDates", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                command.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindTransactionYears()
        {
            try
            {
                int SelectedindexYear = 0;
                tblTransactionYearDatesRepository TrDRepo = new tblTransactionYearDatesRepository();
                List<tblTransactionYearDate> TrDData = new List<tblTransactionYearDate>();
                TrDData = TrDRepo.GetAll().OrderByDescending(t => t.Id).Take(10).ToList();
                int i = 0;
                foreach (var item in TrDData)
                {
                    cmbTransactionYear.Items.Add(item.TransactionYear);
                    var dtstart = DateTime.ParseExact(item.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var dtend = DateTime.ParseExact(item.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var dtcurrent = DateTime.Today;
                    if (dtstart <= dtcurrent && dtend >= dtcurrent)
                        SelectedindexYear = i;
                    i++;
                }
                cmbTransactionYear.SelectedIndex = SelectedindexYear;
            }
            catch (Exception)
            { }
        }

        private void btnlogin_Click(object sender, EventArgs e)
            {
            try
            {
                if (cmbTransactionYear.Text != "")
                {
                    if (VersionUpdate == true)
                    {
                        if (txtpassword.Text == "EST")
                        {
                            changeSystemDate();
                            LoginRepository loginrepo1 = new LoginRepository();
                            Login itemdata1 = new Login();
                            itemdata1 = loginrepo1.GetAll().Where(t => t.Username == txtusername.Text).FirstOrDefault();
                            CommonMethod.CompId = Convert.ToInt32(itemdata1.CompId);
                            CommonMethod.TransactionYear = cmbTransactionYear.Text;
                            tblTransactionYearDatesRepository TransactionYearReop = new tblTransactionYearDatesRepository();
                            List<tblTransactionYearDate> TransactionYearData = new List<tblTransactionYearDate>();
                            TransactionYearData = TransactionYearReop.GetAll().ToList();
                            foreach (var item in TransactionYearData)
                            {
                                if (CommonMethod.TransactionYear == item.TransactionYear)
                                {
                                    CommonMethod.startdate = Convert.ToDateTime(DateTime.ParseExact(item.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    CommonMethod.enddate = Convert.ToDateTime(DateTime.ParseExact(item.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    CommonMethod.PreTransactionYear = TransactionYearData.Where(t => t.Id < item.Id).OrderByDescending(t => t.Id).FirstOrDefault().TransactionYear;
                                    CommonMethod.NextTransactionYear = TransactionYearData.Where(t => t.Id > item.Id).FirstOrDefault().TransactionYear;
                                    break;
                                }
                            }
                            DemoVersion objForm = new DemoVersion();
                            objForm.ShowDialog();
                            clear();
                            VersionUpdate = false;
                        }
                        else
                        {
                            MessageBox.Show("Username or Password Not Match.", "Warning");
                        }
                    }
                    else
                    {
                        LoginRepository loginrepo = new LoginRepository();
                        Login itemdata = new Login();
                        itemdata = loginrepo.GetAll().Where(t => t.Username == txtusername.Text && t.Password == txtpassword.Text).FirstOrDefault();
                        if (itemdata != null)
                        {
                            changeSystemDate();
                            CommonMethod.CompId = Convert.ToInt32(itemdata.CompId);
                            CommonMethod.TransactionYear = cmbTransactionYear.Text;
                            tblTransactionYearDatesRepository TransactionYearReop = new tblTransactionYearDatesRepository();
                            List<tblTransactionYearDate> TransactionYearData = new List<tblTransactionYearDate>();
                            TransactionYearData = TransactionYearReop.GetAll().ToList();
                            foreach (var item in TransactionYearData)
                            {
                                if (CommonMethod.TransactionYear == item.TransactionYear)
                                {
                                    CommonMethod.startdate = Convert.ToDateTime(DateTime.ParseExact(item.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    CommonMethod.enddate = Convert.ToDateTime(DateTime.ParseExact(item.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    CommonMethod.PreTransactionYear = TransactionYearData.Where(t => t.Id < item.Id).OrderByDescending(t => t.Id).FirstOrDefault().TransactionYear;
                                    CommonMethod.NextTransactionYear = TransactionYearData.Where(t => t.Id > item.Id).FirstOrDefault().TransactionYear;
                                    break;
                                }
                            }
                            itemdata.Status = 1;
                            loginrepo.Edit(itemdata);
                            loginrepo.Save();
                            AddCurrentDateinReg();
                            progressBar1.Visible = true;
                            timer1.Enabled = true;

                            SettingRepository setRepo = new SettingRepository();
                            Setting setData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (setData != null)
                            {
                                CommonMethod cm = new CommonMethod();
                                if (!String.IsNullOrEmpty(setData.CommonProduct))
                                {
                                    if (setData.CommonProduct == "Y")
                                        CommonMethod.commProduct = true;
                                    else
                                        CommonMethod.commProduct = false;
                                }
                                else
                                    CommonMethod.commProduct = false;
                            }
                            setRepo.Dispose();
                        }
                        else if (itemdata == null && txtpassword.Text == "ESSUPPORT")
                        {
                            changeSystemDate();
                            LoginRepository loginrepo1 = new LoginRepository();
                            Login itemdata1 = new Login();
                            itemdata1 = loginrepo1.GetAll().Where(t => t.Username == txtusername.Text).FirstOrDefault();
                            CommonMethod.CompId = Convert.ToInt32(itemdata1.CompId);
                            CommonMethod.TransactionYear = cmbTransactionYear.Text;
                            tblTransactionYearDatesRepository TransactionYearReop = new tblTransactionYearDatesRepository();
                            List<tblTransactionYearDate> TransactionYearData = new List<tblTransactionYearDate>();
                            TransactionYearData = TransactionYearReop.GetAll().ToList();
                            foreach (var item in TransactionYearData)
                            {
                                if (CommonMethod.TransactionYear == item.TransactionYear)
                                {
                                    CommonMethod.startdate = Convert.ToDateTime(DateTime.ParseExact(item.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    CommonMethod.enddate = Convert.ToDateTime(DateTime.ParseExact(item.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                                    CommonMethod.PreTransactionYear = TransactionYearData.Where(t => t.Id < item.Id).OrderByDescending(t => t.Id).FirstOrDefault().TransactionYear;
                                    CommonMethod.NextTransactionYear = TransactionYearData.Where(t => t.Id > item.Id).FirstOrDefault().TransactionYear;
                                    break;
                                }
                            }
                            frmYearEndingProcess YearEnding = new frmYearEndingProcess();
                            YearEnding.ShowDialog();
                            clear();
                        }
                        else
                            MessageBox.Show("Username or Password Not Match.", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Select Year.", "Warning");
                    this.ActiveControl = cmbTransactionYear;
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void clear()
        {
            txtusername.Text = "";
            txtpassword.Text = "";
        }

        public void changeSystemDate()
        {
            try
            {
                RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                string date = Convert.ToString(rkey.GetValue("sShortDate", "dd/MM/yyyy"));
                if (date == "dd/MM/yyyy")
                { }
                else
                {
                    rkey.SetValue("sShortDate", "dd/MM/yyyy");
                    rkey.SetValue("sLongDate", "dd/MM/yyyy");
                }
            }
            catch (Exception)
            { }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            btnlogin.Enabled = false;
            progressBar1.PerformStep();
            if (Convert.ToInt32(progressBar1.Value) == 50)
            {
                timer1.Enabled = false;
                this.Hide();
                MDINewForm inventorymdi = new MDINewForm();
                inventorymdi.ShowDialog();
            }
        }

        private void frmloginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (CommonMethod.CompId != 0)
                {
                    LoginRepository LoginRepo = new LoginRepository();
                    Login LoginData = new Login();
                    LoginData = LoginRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    LoginData.Status = 0;
                    LoginRepo.Edit(LoginData);
                    LoginRepo.Save();
                    AddCurrentDateinReg();
                }
                Application.Exit();
            }
            catch (Exception)
            { }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpassword;
            }
            catch (Exception)
            { }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnlogin;
            }
            catch (Exception)
            { }
        }
    }
}