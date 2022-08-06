using System;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmMasterSettings : Form
    {
        DbClass db = new DbClass();
        CommonMethod CM = new CommonMethod();

        public frmMasterSettings()
        {
            InitializeComponent();
            getmasterData();
        }

        private void getmasterData()
        {
            SettingRepository setRepo = new SettingRepository();
            Setting setData = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setData != null)
            {
                if (string.IsNullOrEmpty(setData.CommonProduct))
                {
                    chkProduct.Checked = false;
                    CommonMethod.commProduct = false;
                }
                else
                {
                    if (setData.CommonProduct == "Y")
                    {
                        chkProduct.Checked = true;
                        CommonMethod.commProduct = true;
                    }
                    else
                    {
                        chkProduct.Checked = false;
                        CommonMethod.commProduct = false;
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SettingRepository setRepo = new SettingRepository();
            Setting setdata = setRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setdata != null)
            {
                if (chkProduct.Checked == true)
                    setdata.CommonProduct = "Y";
                else
                    setdata.CommonProduct = "N";

                if (chkManuComp.Checked == true)
                    setdata.CommonManuComp = "Y";
                else
                    setdata.CommonManuComp = "N";

                if (chkUnit.Checked == true)
                    setdata.CommonUnit = "Y";
                else
                    setdata.CommonUnit = "N";

                setRepo.Edit(setdata);
                setRepo.Save();
            }
            else
            {
                if (chkProduct.Checked == true)
                    setdata.CommonProduct = "Y";
                else
                    setdata.CommonProduct = "N";

                if (chkManuComp.Checked == true)
                    setdata.CommonManuComp = "Y";
                else
                    setdata.CommonManuComp = "N";

                if (chkUnit.Checked == true)
                    setdata.CommonUnit = "Y";
                else
                    setdata.CommonUnit = "N";

                setdata.CompId = CommonMethod.CompId;
                setRepo.Add(setdata);
                setRepo.Save();
            }
            setRepo.Dispose();
            getmasterData();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }
    }
}
