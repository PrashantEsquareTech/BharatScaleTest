using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmDbBackUp : Form
    {
        public SqlConnection Connection;

        public frmDbBackUp()
        {
            InitializeComponent();
        }

        private void btnBackupDB_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            try
            {
                CompanyInformationRepository compRepo = new CompanyInformationRepository();
                string compName = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault().CompanyName;
                string[] drives = Environment.GetLogicalDrives();
                saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                errormsg = "Error on Folder Create";
                string path = "";
                if (drives.Length > 1)
                    path = @"" + drives[1] + "" + compName + "\\" + " Database Backup/";
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
                errormsg = "Error on connection Create";
                var connectionString = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                Connection = new SqlConnection(connectionString);
                var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

                var backupFileName = String.Format("{0}{1}{2}.bak",
                   path, sqlConStrBuilder.InitialCatalog,
                   DateTime.Now.ToString("ddMMyyyy"));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                        sqlConStrBuilder.InitialCatalog, backupFileName);
                    errormsg = "Error on execute query";
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
            {
                MessageBox.Show(errormsg, "Error");
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

        private void frmDbBackUp_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }
    }
}