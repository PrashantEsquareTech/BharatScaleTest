using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmPrintSetting : Form
    {
        DbClass db = new DbClass();

        public frmPrintSetting()
        {
            InitializeComponent();
            getSetting();
        }

        private void getSetting()
        {
            CustomerReportSettingRepository reportsetting = new CustomerReportSettingRepository();
            CustomerReportSetting settingdata = reportsetting.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settingdata != null)
            {
                txtPrinter.Text = settingdata.PrinterName;
                txtHeight.Text = Convert.ToString(settingdata.PaperSizeHeight);
                txtWidth.Text = Convert.ToString(settingdata.PaperSizeWidth);
                btnSave.Text = "Update";
            }
            else
                btnSave.Text = "Save";
            reportsetting.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPrinter.Text != "" && txtHeight.Text != "" && txtWidth.Text != "")
            {
                CustomerReportSettingRepository settingrepo = new CustomerReportSettingRepository();
                var idcount = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count();
                if (Convert.ToInt32(idcount) == 0)
                {
                    CustomerReportSetting repodata = new CustomerReportSetting();
                    repodata.PrinterName = txtPrinter.Text;
                    repodata.PaperSizeHeight = Convert.ToInt32(txtHeight.Text);
                    repodata.PaperSizeWidth = Convert.ToInt32(txtWidth.Text);
                    repodata.CompId = Convert.ToInt32(CommonMethod.CompId);
                    settingrepo.Add(repodata);
                    settingrepo.Save();
                    settingrepo.Dispose();
                    repodata = null;
                    MessageBox.Show("Settings added successfully.", "Save Record");
                    this.Close();
                }
                else
                {
                    CustomerReportSetting repodata = settingrepo.GetAll().Where(t => t.Id == 1 && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (repodata != null)
                    {
                        repodata.PrinterName = txtPrinter.Text;
                        repodata.PaperSizeHeight = Convert.ToInt32(txtHeight.Text);
                        repodata.PaperSizeWidth = Convert.ToInt32(txtWidth.Text);
                        repodata.CompId = Convert.ToInt32(CommonMethod.CompId);
                        settingrepo.Edit(repodata);
                        settingrepo.Save();
                        settingrepo.Dispose();
                        repodata = null;
                        MessageBox.Show("Settings updated successfully.", "Update Record");
                        this.Close();
                    }
                }
            }
            else
            {
                if (txtPrinter.Text == "")
                {
                    MessageBox.Show("Set Printer!", "Warning");
                    this.ActiveControl = txtPrinter;
                }
                else if (txtHeight.Text == "")
                {
                    MessageBox.Show("Set paper height!", "Warning");
                    this.ActiveControl = txtHeight;
                }
                else
                {
                    MessageBox.Show("Set paper width!", "Warning");
                    this.ActiveControl = txtWidth;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void GetAllPrinterList()
        {
            GVPrinterName.Rows.Clear();
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();
            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                GVPrinterName.Rows.Add(mo["Name"].ToString());
            }
        }

        private void btnAddPrinter_Click(object sender, EventArgs e)
        {
            try
            {
                GVPrinterName.Visible = true;
                GetAllPrinterList();
                GVPrinterName.Visible = true;
            }
            catch (Exception)
            { }
        }

        private void frmPrintSetting_Load(object sender, EventArgs e)
        {
            GVPrinterName.Visible = false;
        }

        private void GVPrinterName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtPrinter.Text = Convert.ToString(GVPrinterName.Rows[e.RowIndex].Cells[0].Value);
                GVPrinterName.Visible = false;
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

        private void txtPrinter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtHeight;
            }
            catch (Exception) { }
        }

        private void txtHeight_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtWidth;
            }
            catch (Exception) { }
        }

        private void txtWidth_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }
    }
}