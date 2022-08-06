using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using Microsoft.Win32;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class DemoVersion : Form
    {
        DbClass db = new DbClass();
        int Id;

        public DemoVersion()
        {
            InitializeComponent();
            GetData();
        }

        public void GetData()
        {
            try
            {
                DataTable dt = db.GetTable("Select top 1 * from TrailPeriod");
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["Version"].ToString().Trim() == "Demo")
                        rdbtnDemo.Checked = true;
                    else if (dt.Rows[0]["Version"].ToString().Trim() == "Rental")
                        rdbtnRental.Checked = true;
                    else if (dt.Rows[0]["Version"].ToString().Trim() == "Full")
                        rdbtnFull.Checked = true;
                    else
                        rdbtnFull.Checked = true;
                    if (dt.Rows[0]["Version"].ToString().Trim() == "Demo" || dt.Rows[0]["Version"].ToString().Trim() == "Rental")
                    {
                        dtpstartdate.Value = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString());
                        dtpenddate.Value = Convert.ToDateTime(dt.Rows[0]["EndDate"].ToString());
                    }
                    Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    btnSave.Text = "Update";
                }
                else
                {
                    rdbtnFull.Checked = true;
                    btnSave.Text = "Save";
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    TrialPeriodrepository trailRepo = new TrialPeriodrepository();
                    TrailPeriod traildata = new TrailPeriod();
                    if (rdbtnDemo.Checked == true)
                        traildata.Version = rdbtnDemo.Text.Trim();
                    else if (rdbtnRental.Checked == true)
                        traildata.Version = rdbtnRental.Text.Trim();
                    else if (rdbtnFull.Checked == true)
                        traildata.Version = rdbtnFull.Text.Trim();
                    if (rdbtnDemo.Checked == true || rdbtnRental.Checked == true)
                    {
                        traildata.StartDate = dtpstartdate.Value;
                        traildata.EndDate = dtpenddate.Value;
                    }
                    trailRepo.Add(traildata);
                    trailRepo.Save();
                    Id = traildata.Id;
                    if (rdbtnDemo.Checked == true || rdbtnRental.Checked == true)
                    {
                        try
                        {
                            RegistryKey soft_key = Registry.CurrentUser.OpenSubKey("Software", true);
                            DateTime expiredate;
                            expiredate = dtpenddate.Value.Date;
                            RegistryKey ExpiryDate = soft_key.CreateSubKey("ESquare_SoftKeyExpiry");
                            ExpiryDate.SetValue("ESquare_SoftKeyExpiry", Convert.ToString(expiredate));
                        }
                        catch (Exception)
                        { }
                    }
                    MessageBox.Show("Record Save Successfully.", "Success");
                    btnSave.Text = "Update";
                    CompanyInformationRepository compinforepo = new CompanyInformationRepository();
                    var compdata = compinforepo.GetAll().FirstOrDefault();
                    if (compdata == null)
                    {
                        this.Dispose();
                        frmCompanyInformation compinfo = new frmCompanyInformation(1);
                        compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        compinfo.ShowDialog();
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    TrialPeriodrepository trailRepo = new TrialPeriodrepository();
                    TrailPeriod traildata = new TrailPeriod();
                    traildata = trailRepo.GetById(Id);
                    if (rdbtnDemo.Checked == true)
                        traildata.Version = rdbtnDemo.Text.Trim();
                    else if (rdbtnRental.Checked == true)
                        traildata.Version = rdbtnRental.Text.Trim();
                    else if (rdbtnFull.Checked == true)
                        traildata.Version = rdbtnFull.Text.Trim();
                    if (rdbtnDemo.Checked == true || rdbtnRental.Checked == true)
                    {
                        traildata.StartDate = dtpstartdate.Value;
                        traildata.EndDate = dtpenddate.Value;
                    }
                    trailRepo.Edit(traildata);
                    trailRepo.Save();
                    if (rdbtnDemo.Checked == true || rdbtnRental.Checked == true)
                    {
                        try
                        {
                            RegistryKey soft_key = Registry.CurrentUser.OpenSubKey("Software", true);
                            DateTime expiredate;
                            expiredate = dtpenddate.Value.Date;
                            RegistryKey ExpiryDate = soft_key.CreateSubKey("ESquare_SoftKeyExpiry");
                            ExpiryDate.SetValue("ESquare_SoftKeyExpiry", Convert.ToString(expiredate));
                        }
                        catch (Exception)
                        { }
                    }
                    MessageBox.Show("Record Updated Successfully.", "Success");
                }
            }
            catch (Exception)
            { }
        }

        private void rdbtnFull_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnFull.Checked == true)
                {
                    dtpstartdate.Enabled = false;
                    dtpenddate.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        private void rdbtnDemo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnDemo.Checked == true)
                {
                    dtpstartdate.Enabled = true;
                    dtpenddate.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void rdbtnRental_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnRental.Checked == true)
                {
                    dtpstartdate.Enabled = true;
                    dtpenddate.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }
    }
}