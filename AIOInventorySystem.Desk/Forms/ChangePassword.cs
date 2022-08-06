using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class ChangePassword : Form
    {
        DbClass db = new DbClass();

        public ChangePassword()
        {
            InitializeComponent();
            txtNewPassword.Enabled = false;
            this.ActiveControl = txtOldPassword;
            System.Reflection.FieldInfo receivedObject = typeof(DbClass).GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)[0];
            var obj = receivedObject.GetValue(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPassword.Text.Trim() != "")
                {
                    LoginRepository loginRepo = new LoginRepository();
                    LoginRepository loginrepo = new LoginRepository();
                    Login allData = new Login();
                    var loginData = loginrepo.GetAll().Where(t => t.Password == txtOldPassword.Text.Trim() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    allData.Username = Convert.ToString(loginData.Username);
                    allData.Password = Convert.ToString(txtNewPassword.Text.Trim());
                    allData.Role = loginData.Role;
                    allData.CompId = loginData.CompId;
                    allData.Id = loginData.Id;
                    loginRepo.Edit(allData);
                    loginRepo.Save();
                    loginRepo.Dispose();
                    loginrepo.Dispose();
                    MessageBox.Show("Password Changed Successfully.", "Success");
                    this.ActiveControl = txtOldPassword;
                }
                else
                {
                    MessageBox.Show("Please Enter New Password.", "Warning");
                    this.ActiveControl = txtNewPassword;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Password not Changed Successfully.", "Warning");
            }
        }

        private void txtOldPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                LoginRepository loginRepo = new LoginRepository();
                Login loginData = new Login();
                loginData = loginRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (txtOldPassword.Text == loginData.Password)
                {
                    txtNewPassword.Enabled = true;
                    this.ActiveControl = txtNewPassword;
                }
                else
                {
                    txtNewPassword.Enabled = false;
                    MessageBox.Show("Please enter correct password.", "Warning");
                    this.ActiveControl = txtOldPassword;
                }
                loginRepo.Dispose();
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
            if (keyData == Keys.F3)
            {
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtOldPassword.GotFocus += new EventHandler(txtOldPassword_GotFocus);
                txtOldPassword.LostFocus += new EventHandler(txtOldPassword_LostFocus);
                txtNewPassword.GotFocus += new EventHandler(txtNewPassword_GotFocus);
                txtNewPassword.LostFocus += new EventHandler(txtNewPassword_LostFocus);
                btnSave.GotFocus += new EventHandler(btnSave_GotFocus);
                btnSave.LostFocus += new EventHandler(btnSave_LostFocus);
            }
            catch (Exception)
            { }
        }

        void txtOldPassword_GotFocus(object sender, EventArgs e)
        {
            txtOldPassword.BackColor = Color.LightYellow;
        }
        void txtOldPassword_LostFocus(object sender, EventArgs e)
        {
            txtOldPassword.BackColor = Color.White;
        }
        void txtNewPassword_GotFocus(object sender, EventArgs e)
        {
            txtNewPassword.BackColor = Color.LightYellow;
        }
        void txtNewPassword_LostFocus(object sender, EventArgs e)
        {
            txtNewPassword.BackColor = Color.White;
        }
        void btnSave_GotFocus(object sender, EventArgs e)
        {
            btnSave.BackColor = Color.White;
            btnSave.ForeColor = Color.SteelBlue;
        }
        void btnSave_LostFocus(object sender, EventArgs e)
        {
            btnSave.ForeColor = Color.White;
            btnSave.BackColor = Color.SteelBlue;
        }        
    }
}
