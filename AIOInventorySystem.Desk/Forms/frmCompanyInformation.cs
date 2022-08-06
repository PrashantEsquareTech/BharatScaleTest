using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCompanyInformation : Form
    {
        DbClass db = new DbClass();
        public int updateId = 0, flag = 0;
        byte[] @photologo;
        SettingRepository settRepo = new SettingRepository();
        Setting settData = new Setting();

        public frmCompanyInformation()
        {
            InitializeComponent();
            maxcompid();
            fillState();
            if (CommonMethod.CompId != null || CommonMethod.CompId != 0)
                getcompanydata(CommonMethod.CompId);
            this.ActiveControl = txtCompanyName;
            SettingRepository settingRepo = new SettingRepository();
            Setting settingdata = new Setting();
            settingdata = settingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settingdata != null)
            {
                if (settingdata.ShowLicNum == "All")
                {
                    txtSeedLicno.Enabled = true;
                    txtInscLicno.Enabled = true;
                    txtFertiLicno.Enabled = true;
                    txtRetailerLicNo.Enabled = true;
                    label19.Enabled = true;
                    label20.Enabled = true;
                    label21.Enabled = true;
                    label22.Enabled = true;
                }
                else
                {
                    txtSeedLicno.Enabled = false;
                    txtInscLicno.Enabled = false;
                    txtFertiLicno.Enabled = false;
                    txtRetailerLicNo.Enabled = true;
                    label19.Enabled = false;
                    label20.Enabled = false;
                    label21.Enabled = false;
                    label22.Enabled = false;
                }
                if (settData.Applyonmarathifont == "Y")
                    txtCompMarathi.Font = new Font("Shivaji01", 12);
                else
                    txtCompMarathi.Font = new Font("Microsoft Sans Serif", 10);
            }
        }

        public frmCompanyInformation(int flag1)
        {
            InitializeComponent();
            gstnstate();
            maxcompid();
            fillState();
            flag = flag1;
            if (CommonMethod.CompId != null || CommonMethod.CompId != 0)
                getcompanydata(CommonMethod.CompId);
            this.ActiveControl = txtCompanyName;
            SettingRepository settingRepo = new SettingRepository();
            Setting settingdata = new Setting();
            settingdata = settingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settingdata != null)
            {
                if (settingdata.ShowLicNum == "All")
                {
                    txtSeedLicno.Enabled = true;
                    txtInscLicno.Enabled = true;
                    txtFertiLicno.Enabled = true;
                    label19.Enabled = true;
                    label20.Enabled = true;
                    label21.Enabled = true;
                }
                else
                {
                    txtSeedLicno.Enabled = false;
                    txtInscLicno.Enabled = false;
                    txtFertiLicno.Enabled = false;
                    label19.Enabled = false;
                    label20.Enabled = false;
                    label21.Enabled = false;
                }
                if (settData.Applyonmarathifont == "Y")
                    txtCompMarathi.Font = new Font("Shivaji01", 12);
                else
                    txtCompMarathi.Font = new Font("Microsoft Sans Serif", 10);
            }
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

        public void AddMFGUnit()
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPUnitMFGDefault", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                command.Dispose();
            }
            catch (Exception)
            { }
        }


        public void maxcompid()
        {
            try
            {
                CompanyInformationRepository compinforepo = new CompanyInformationRepository();
                var mid = compinforepo.GetAll().Max(t => t.CompanyId);
                if (mid == null)
                    txtCompanyID.Text = "1";
                else
                    txtCompanyID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                compinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillState()
        {
            try
            {
                StateMasterRepository unitrepo = new StateMasterRepository();
                List<StateMaster> unitdata = new List<StateMaster>();
                unitdata.Clear();
                unitdata = unitrepo.GetAll().OrderByDescending(t => t.StateName).ToList();
                unitdata.Add(new StateMaster { StateName = "Select", StateId = 0 });
                unitdata.Reverse();
                cmbstate.DataSource = unitdata;
                cmbstate.ValueMember = "StateId";
                cmbstate.DisplayMember = "StateName";
                unitrepo.Dispose();
                cmbstate.SelectedValue = 27;
            }
            catch (Exception)
            { }
        }

        void conv_photologo()
        {
            @photologo = null;
            if (picLogo.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                picLogo.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                @photologo = photo_aray;
            }
        }

        public void Clear()
        {
            try
            {
                maxcompid();
                cmbstate.SelectedIndex = 0;
                txtCompanyName.Text = string.Empty;
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtComanyAddress.Text = string.Empty;
                txtContactNo.Text = string.Empty;
                txtEmailID.Text = string.Empty;
                txtWebsite.Text = string.Empty;
                txtVatNo.Text = string.Empty;
                txtTinNo.Text = string.Empty;
                txtIFSC.Text = "";
                txtBankAc.Text = "";
                txtRetailerLicNo.Text = "";
                txtFertiLicno.Text = "";
                txtSeedLicno.Text = "";
                txtInscLicno.Text = "";
                picLogo.Image = null;
                btnupdate.Text = "Save";
                updateId = 0;
                this.ActiveControl = txtCompanyName;
                txtBankName.Text = "";
                txtFssaiNo.Text = "";
            }
            catch (Exception)
            { }
        }

        public void getcompanydata(int? compid)
        {
            try
            {
                CompanyInformationRepository compinforepo = new CompanyInformationRepository();
                var data = compinforepo.GetAll().Where(t => t.CompanyId == compid).FirstOrDefault();
                if (data != null)
                {
                    LoginRepository LoginRepo = new LoginRepository();
                    Login LoginData = new Login();
                    LoginData = LoginRepo.GetAll().Where(t => t.CompId == compid).FirstOrDefault();
                    updateId = data.Id;
                    txtCompanyID.Text = data.CompanyId.ToString();
                    txtCompanyName.Text = data.CompanyName;
                    txtUsername.Text = LoginData.Username;
                    txtPassword.Text = LoginData.Password;
                    txtComanyAddress.Text = data.CompanyAddress;
                    txtContactNo.Text = data.ContactNo;
                    txtEmailID.Text = data.EmailID;
                    txtWebsite.Text = data.Website;
                    txtVatNo.Text = data.VatNo;
                    txtTinNo.Text = data.TinNo;
                    picLogo.Image = null;
                    txtBankName.Text = data.BankName;
                    txtBankAc.Text = data.BankAcNo;
                    txtIFSC.Text = data.IFSCCode;
                    txtSeedLicno.Text = data.SeedLicNo;
                    txtInscLicno.Text = data.InscLicNo;
                    txtFertiLicno.Text = data.FertLicNo;
                    txtBankName.Text = data.BankName;
                    cmbstate.Text = Convert.ToString(data.State);
                    txtMarathiAdd.Text = data.CompAddMarathi;
                    txtCompMarathi.Text = data.CompNameMarathi;
                    txtFssaiNo.Text = data.FSSAINo;
                    if (!string.IsNullOrEmpty(data.RetailerFertLicNo))
                        txtRetailerLicNo.Text = data.RetailerFertLicNo;
                    if (data.Logo != null)
                    {
                        @photologo = (byte[])data.Logo;
                        MemoryStream ms = new MemoryStream(@photologo);
                        picLogo.Image = Image.FromStream(ms);
                    }
                    LoginRepo.Dispose();
                    btnupdate.Text = "Update";
                }
                compinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
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
            if (keyData == Keys.F3)
            {
                btnupdate_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnupdate.Text == "Save")
                {
                    CompanyInformationRepository compinforepo = new CompanyInformationRepository();
                    CompanyInformation CompInfo = new CompanyInformation();
                    CompInfo = compinforepo.GetAll().Where(t => t.CompanyId == Convert.ToInt32(txtCompanyID.Text)).FirstOrDefault();
                    if (CompInfo == null)
                    {
                        if (!string.IsNullOrEmpty(txtCompanyName.Text) && !string.IsNullOrWhiteSpace(txtCompanyName.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text) && cmbstate.SelectedIndex > 0)
                        {
                            CompanyInformation cData = new CompanyInformation();
                            cData.CompanyId = Convert.ToInt32(txtCompanyID.Text);
                            cData.CompanyName = txtCompanyName.Text.Trim();
                            cData.CompanyAddress = txtComanyAddress.Text.Trim();
                            cData.ContactNo = txtContactNo.Text.Trim();
                            cData.EmailID = txtEmailID.Text.Trim();
                            cData.Website = txtWebsite.Text.Trim();
                            cData.VatNo = txtVatNo.Text.Trim();
                            cData.TinNo = txtTinNo.Text.Trim();
                            cData.BankName = txtBankName.Text;
                            cData.BankAcNo = txtBankAc.Text;
                            cData.IFSCCode = txtIFSC.Text;
                            cData.FertLicNo = txtFertiLicno.Text;
                            cData.InscLicNo = txtInscLicno.Text;
                            cData.SeedLicNo = txtSeedLicno.Text;
                            conv_photologo();
                            cData.Logo = @photologo;
                            cData.State = cmbstate.Text;
                            cData.StateCode = Convert.ToInt32(cmbstate.SelectedValue);
                            cData.CompNameMarathi = txtCompMarathi.Text;
                            cData.CompAddMarathi = txtMarathiAdd.Text;
                            cData.RetailerFertLicNo = txtRetailerLicNo.Text;
                            cData.FSSAINo = txtFssaiNo.Text;
                            //cData.FSSAINo = txtpad.Text;
                            compinforepo.Add(cData);
                            compinforepo.Save();

                            LoginRepository LoginRepo = new LoginRepository();
                            Login LoginInfo = new Login();
                            LoginInfo.Username = txtUsername.Text.Trim();
                            LoginInfo.Password = txtPassword.Text;
                            LoginInfo.Role = "Admin";
                            LoginInfo.CompId = Convert.ToInt32(cData.CompanyId);
                            LoginRepo.Add(LoginInfo);
                            LoginRepo.Save();

                            MessageBox.Show("Company Information Save Successfully.", "Success");
                            btnupdate.Text = "Update";
                            CommonMethod.CompId = Convert.ToInt32(cData.CompanyId);
                            AddMFGUnit();
                            getcompanydata(CommonMethod.CompId);
                            this.ActiveControl = btnClose;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtCompanyName.Text) && string.IsNullOrWhiteSpace(txtCompanyName.Text))
                            {
                                MessageBox.Show("Please Enter Company Name.", "Warning");
                                this.ActiveControl = txtCompanyName;
                            }
                            else if (string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrWhiteSpace(txtUsername.Text))
                            {
                                MessageBox.Show("Please Enter User Name.", "Warning");
                                this.ActiveControl = txtUsername;
                            }
                            else if (cmbstate.SelectedIndex == 0)
                            {
                                MessageBox.Show("Please Select State.", "Warning");
                                this.ActiveControl = cmbstate;
                            }
                            else
                            {
                                MessageBox.Show("Please Enter Password.", "Warning");
                                this.ActiveControl = txtPassword;
                            }
                        }
                    }
                }
                else
                {
                    if (updateId != 0)
                    {
                        CompanyInformationRepository compinforepo = new CompanyInformationRepository();
                        CompanyInformation cData = new CompanyInformation();

                        cData = compinforepo.GetAll().Where(t => t.CompanyId == Convert.ToInt32(txtCompanyID.Text)).FirstOrDefault();
                        if (cData != null)
                        {
                            if (!string.IsNullOrEmpty(txtCompanyName.Text) && !string.IsNullOrWhiteSpace(txtCompanyName.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text) && cmbstate.SelectedIndex > 0)
                            {
                                cData.CompanyId = Convert.ToInt32(txtCompanyID.Text);
                                cData.CompanyName = txtCompanyName.Text.Trim();
                                cData.CompanyAddress = txtComanyAddress.Text.Trim();
                                cData.ContactNo = txtContactNo.Text.Trim();
                                cData.EmailID = txtEmailID.Text.Trim();
                                cData.Website = txtWebsite.Text.Trim();
                                cData.VatNo = txtVatNo.Text.Trim();
                                cData.TinNo = txtTinNo.Text.Trim();
                                cData.BankName = txtBankName.Text;
                                cData.BankAcNo = txtBankAc.Text;
                                cData.IFSCCode = txtIFSC.Text;
                                cData.FertLicNo = txtFertiLicno.Text;
                                cData.InscLicNo = txtInscLicno.Text;
                                cData.SeedLicNo = txtSeedLicno.Text;
                                cData.State = cmbstate.Text;
                                cData.StateCode = Convert.ToInt32(cmbstate.SelectedValue);
                                conv_photologo();
                                cData.Logo = @photologo;
                                cData.CompNameMarathi = txtCompMarathi.Text;
                                cData.CompAddMarathi = txtMarathiAdd.Text;
                                cData.RetailerFertLicNo = txtRetailerLicNo.Text;
                                cData.FSSAINo = txtFssaiNo.Text;
                                compinforepo.Edit(cData);
                                compinforepo.Save();

                                LoginRepository LoginRepo = new LoginRepository();
                                Login LoginInfo = new Login();
                                LoginInfo = LoginRepo.GetAll().Where(t => t.CompId == cData.CompanyId).FirstOrDefault();
                                LoginInfo.Username = txtUsername.Text.Trim();
                                LoginInfo.Password = txtPassword.Text;
                                LoginInfo.Role = "Admin";
                                LoginInfo.CompId = Convert.ToInt32(cData.CompanyId);
                                LoginRepo.Edit(LoginInfo);
                                LoginRepo.Save();

                                MessageBox.Show("Company Information Update Successfully.", "Success");
                                btnupdate.Text = "Update";
                                getcompanydata(CommonMethod.CompId);
                                this.ActiveControl = btnClose;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtCompanyName.Text) && string.IsNullOrWhiteSpace(txtCompanyName.Text))
                                {
                                    MessageBox.Show("Please Enter Company Name.", "Warning");
                                    this.ActiveControl = txtCompanyName;
                                }
                                else if (string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrWhiteSpace(txtUsername.Text))
                                {
                                    MessageBox.Show("Please Enter User Name.", "Warning");
                                    this.ActiveControl = txtUsername;
                                }
                                else if (cmbstate.SelectedIndex == 0)
                                {
                                    MessageBox.Show("Please Select State.", "Warning");
                                    this.ActiveControl = cmbstate;
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Password.", "Warning");
                                    this.ActiveControl = txtPassword;
                                }
                            }
                        }
                    }
                }
                if (flag != 0)
                {
                    this.Dispose();
                    frmloginForm loginform = new frmloginForm();
                    loginform.WindowState = System.Windows.Forms.FormWindowState.Normal;
                    loginform.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    loginform.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if ((i >= 47 && i <= 57) || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void txtEmailID_Leave(object sender, EventArgs e)
        {
            try
            {
                Regex mRegxExpression;
                if (txtEmailID.Text.Trim() != string.Empty)
                {
                    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                    if (!mRegxExpression.IsMatch(txtEmailID.Text.Trim()))
                    {
                        MessageBox.Show("Please enter valid Email-ID.");
                        this.ActiveControl = txtEmailID;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                    picLogo.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception)
            { }
        }

        private void frmCompanyInformation_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            SettingRepository sRepo = new SettingRepository();
            Setting sdata = new Setting();
            sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata != null)
            {
                if (sdata.Applyonmarathifont == "Y")
                    txtCompMarathi.Font = new Font("Shivaji01", 12);
                else
                    txtCompMarathi.Font = new Font("Microsoft Sans Serif", 10);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (updateId != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Bank";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = updateId;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Check", SqlDbType.NVarChar);
                            command.Parameters["@Check"].Direction = ParameterDirection.Output;
                            command.Parameters["@Check"].Size = 5;
                            command.ExecuteReader();
                            strcheck = command.Parameters["@Check"].Value.ToString();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        if (strcheck == "T")
                            MessageBox.Show("This Company is Used. You Can Not Delete.", "Warning");
                        else
                        {
                            try
                            {
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
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "")
                {
                    LoginRepository LoginRepo = new LoginRepository();
                    Login LoginData = new Login();
                    LoginData = LoginRepo.GetAll().Where(t => t.Username.Trim() == txtUsername.Text.Trim() && t.CompId != CommonMethod.CompId).FirstOrDefault();
                    if (LoginData != null)
                    {
                        MessageBox.Show("This Username is Already used. Please Enter another username", "Warning");
                        txtUsername.Text = "";
                        this.ActiveControl = txtUsername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmCompanyInformation_MouseMove(object sender, MouseEventArgs e)
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

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCompMarathi;
            }
            catch (Exception)
            { }
        }

        private void txtCompMarathi_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                    frmCompanyInformation_Load(null, null);
                }
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtUsername;
            }
            catch (Exception)
            { }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPassword;
            }
            catch (Exception)
            { }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtComanyAddress;
            }
            catch (Exception)
            { }
        }

        private void txtComanyAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtMarathiAdd;
            }
            catch (Exception)
            { }
        }

        private void txtMarathiAdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnBrowseLogo;
            }
            catch (Exception)
            { }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtEmailID;
            }
            catch (Exception)
            { }
        }

        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtWebsite;
            }
            catch (Exception)
            { }
        }

        private void txtWebsite_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtVatNo;
            }
            catch (Exception)
            { }
        }

        private void txtVatNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankAc;
            }
            catch (Exception)
            { }
        }

        private void txtBankAc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtIFSC;
            }
            catch (Exception)
            { }
        }

        private void txtIFSC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSeedLicno;
            }
            catch (Exception)
            { }
        }

        private void btnBrowseLogo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtContactNo;
            }
            catch (Exception)
            { }
        }

        private void cmbstate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnupdate;
            }
            catch (Exception)
            { }
        }

        private void txtTinNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnupdate;
            }
            catch (Exception)
            { }
        }

        private void btnupdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnDelete;
            }
            catch (Exception)
            { }
        }

        private void rButton2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnClose;
            }
            catch (Exception)
            { }
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnNew;
            }
            catch (Exception)
            { }
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCompanyName;
            }
            catch (Exception)
            { }
        }

        private void txtSeedLicno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtInscLicno;
            }
            catch (Exception)
            { }
        }

        private void txtInscLicno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtFertiLicno;
            }
            catch (Exception)
            { }
        }

        private void txtFertiLicno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbstate;
            }
            catch (Exception)
            { }
        }
    }
}