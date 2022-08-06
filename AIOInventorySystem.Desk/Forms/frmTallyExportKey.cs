using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmTallyExportKey : Form
    {
        SettingRepository MSetRepo = new SettingRepository();
        Setting MSetData = new Setting();

        public frmTallyExportKey()
        {
            InitializeComponent();
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
                btnAccept_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmTallyExportKey_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtActivationKey.GotFocus += new EventHandler(txtActivationKey_GotFocus);
                txtActivationKey.LostFocus += new EventHandler(txtActivationKey_LostFocus);
            }
            catch (Exception)
            { } 
        }

        void txtActivationKey_GotFocus(object sender, EventArgs e)
        {
            txtActivationKey.BackColor = Color.Violet;
        }
        void txtActivationKey_LostFocus(object sender, EventArgs e)
        {
            txtActivationKey.BackColor = Color.White;
        }
        
        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtActivationKey.Text.Trim() != "")
                {
                    MSetData = new Setting();
                    MSetData = MSetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (txtActivationKey.Text.Trim() == "esquare@1".Trim())
                    {
                        MSetData.TallyKey = "esquare@1".Trim();
                        MSetRepo.Edit(MSetData);
                        MSetRepo.Save();
                        this.Close();
                        frmTallyExport tallyexport = new frmTallyExport();
                        tallyexport.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid ActivationKey.","Warning");
                        this.ActiveControl = txtActivationKey;
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter ActivationKey.","Warning");
                    this.ActiveControl = txtActivationKey;
                }
            }
            catch (Exception)
            { }
        }
    }
}
