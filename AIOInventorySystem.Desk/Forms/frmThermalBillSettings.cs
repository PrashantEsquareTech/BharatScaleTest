using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmThermalBillSettings : Form
    {
        DbClass db = new DbClass();
        private int updateId, stringwidthTitle = 0;
        SizeF stringSizeH1 = new SizeF();
        Font rptfontTitle = new Font("Mangal", 9);
        Image fakeImage = new Bitmap(1, 1);

        public frmThermalBillSettings()
        {
            InitializeComponent();
            this.ActiveControl = radDefault;
            this.ActiveControl = radbtnMarathi;
            btnUpdate.Enabled = false;
            GetFontFamilies();
            loadData();
        }

        public List<string> GetFontFamilies()
        {
            List<string> fontfamilies = new List<string>();
            foreach (FontFamily family in FontFamily.Families)
            {
                fontfamilies.Add(family.Name);
            }
            cmbFontName.DataSource = fontfamilies;
            cmbFontName.ValueMember = null;
            cmbFontName.DisplayMember = "Name";
            cmbFontName.SelectedItem = "Utsaah";
            return fontfamilies;
        }

        public void loadData()
        {
            try
            {
                ThermalBillSettingsRepository thermalRepo = new ThermalBillSettingsRepository();
                var detailData = thermalRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (detailData != null)
                {
                    updateId = detailData.Id;
                    if (detailData.Settings == "D")
                        radDefault.Checked = true;
                    else
                    {
                        radCustomize.Checked = true;
                        txtHeadings1.Text = detailData.Headings1;
                        txtHeadings2.Text = detailData.Headings2;
                        txtHeadings3.Text = detailData.Headings3;
                        txtHeadings4.Text = detailData.Headings4;
                        txtHeadings5.Text = detailData.Headings5;
                    }
                    if (detailData.DispalySavings == "Y")
                        chkSavings.Checked = true;
                    else
                        chkSavings.Checked = false;
                    if (detailData.DisplayDiscount == "Y")
                        chkdiscount.Checked = true;
                    else
                        chkdiscount.Checked = false;
                    if (detailData.CheckH3 == "N")
                        chkHeading3.Checked = false;
                    else
                        chkHeading3.Checked = true;
                    if (detailData.CheckH4 == "N")
                        chkHeading4.Checked = false;
                    else
                        chkHeading4.Checked = true;
                    if (detailData.CheckH5 == "N")
                        chkHeading5.Checked = false;
                    else
                        chkHeading5.Checked = true;
                    if (detailData.ShowMRP == "Y")
                        chkShowMRP.Checked = true;
                    else
                        chkShowMRP.Checked = false;
                    if (detailData.PrintOnMarathiOrEnglish == "E")
                        radbtnEnglish.Checked = true;
                    else
                        radbtnMarathi.Checked = true;
                    if (detailData.GSTDetail == true)
                        chkGST.Checked = true;
                    else
                        chkGST.Checked = false;
                    txtNote.Text = detailData.Note;
                    txtFontSize.Text = Convert.ToString(detailData.FontSize);
                    cmbFontName.Text = detailData.FontFamily.ToString();
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                thermalRepo.Dispose();
            }
            catch (Exception) { }
        }

        public void clear()
        {
            try
            {
                txtHeadings1.Text = "";
                txtHeadings2.Text = "";
                txtHeadings3.Text = "";
                txtHeadings4.Text = "";
                txtHeadings5.Text = "";
                radCustomize.Checked = false;
                radDefault.Checked = false;
                chkSavings.Checked = false;
                chkShowMRP.Checked = false;
                txtNote.Text = "";
            }
            catch (Exception) { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (radDefault.Checked == true || radCustomize.Checked == true)
                {
                    if (btnSave.Text == "Save")
                    {
                        ThermalBillSettingsRepository ThermalRepo = new ThermalBillSettingsRepository();
                        ThermalBillSetting thermalData = new ThermalBillSetting();
                        if (chkShowMRP.Checked == true)
                            thermalData.ShowMRP = "Y";
                        else
                            thermalData.ShowMRP = "N";
                        if (chkSavings.Checked == true)
                            thermalData.DispalySavings = "Y";
                        else
                            thermalData.DispalySavings = "N";
                        if (chkdiscount.Checked == true)
                            thermalData.DisplayDiscount = "Y";
                        else
                            thermalData.DisplayDiscount = "N";
                        if (radDefault.Checked == true)
                        {
                            thermalData.Settings = "D";
                            thermalData.Headings1 = lblproductname.Text;
                            thermalData.Headings2 = lblunit.Text;
                            thermalData.Headings3 = lblmrp.Text;
                            thermalData.Headings4 = lblrate.Text;
                            thermalData.Headings5 = lblamount.Text;
                        }
                        else
                        {
                            thermalData.Settings = "C";
                            thermalData.Headings1 = txtHeadings1.Text;
                            thermalData.Headings2 = txtHeadings2.Text;
                            thermalData.Headings3 = txtHeadings3.Text;
                            thermalData.Headings4 = txtHeadings4.Text;
                            thermalData.Headings5 = txtHeadings5.Text;
                        }

                        if (chkHeading3.Checked == true)
                            thermalData.CheckH3 = "Y";
                        else
                            thermalData.CheckH3 = "N";
                        if (chkHeading4.Checked == true)
                            thermalData.CheckH4 = "Y";
                        else
                            thermalData.CheckH4 = "N";
                        if (chkHeading5.Checked == true)
                            thermalData.CheckH5 = "Y";
                        else
                            thermalData.CheckH5 = "N";
                        if (radbtnEnglish.Checked == true)
                            thermalData.PrintOnMarathiOrEnglish = "E";
                        else
                            thermalData.PrintOnMarathiOrEnglish = "M";
                        thermalData.Note = txtNote.Text;
                        thermalData.FontFamily = cmbFontName.Text;
                        thermalData.FontSize = Convert.ToInt32(txtFontSize.Text);
                        if (chkGST.Checked == true)
                            thermalData.GSTDetail = true;
                        else
                            thermalData.GSTDetail = false;
                        thermalData.CompId = CommonMethod.CompId;
                        ThermalRepo.Add(thermalData);
                        ThermalRepo.Save();
                        MessageBox.Show("Thermal Setting Save Successfully.", "Success");
                        btnSave.Enabled = false;
                        btnUpdate.Enabled = true;
                        loadData();
                        ThermalRepo.Dispose();
                    }
                }
                else
                    MessageBox.Show("Choose Settings");
            }
            catch (Exception) { }
        }

        private void radDefault_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radDefault.Checked == true)
                {
                    txtHeadings1.Enabled = false;
                    txtHeadings2.Enabled = false;
                    txtHeadings3.Enabled = false;
                    txtHeadings4.Enabled = false;
                    txtHeadings5.Enabled = false;
                    txtHeadings1.Text = "";
                    txtHeadings2.Text = "";
                    txtHeadings3.Text = "";
                    txtHeadings4.Text = "";
                    txtHeadings5.Text = "";
                    chkShowMRP.Enabled = true;
                    chkShowMRP.Checked = true;
                    chkHeading3.Checked = true;
                    chkHeading4.Checked = true;
                    chkHeading5.Checked = true;
                }
                else
                {
                    chkShowMRP.Enabled = true;
                    chkHeading3.Checked = true;
                    chkHeading4.Checked = true;
                    chkHeading5.Checked = true;
                    txtHeadings1.Enabled = true;
                    txtHeadings2.Enabled = true;
                    txtHeadings3.Enabled = true;
                    txtHeadings4.Enabled = true;
                    txtHeadings5.Enabled = true;
                }
            }
            catch (Exception) { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ThermalBillSettingsRepository thermalRepo = new ThermalBillSettingsRepository();
                ThermalBillSetting thermalData = new ThermalBillSetting();
                thermalData.Id = updateId;
                if (chkShowMRP.Checked == true)
                    thermalData.ShowMRP = "Y";
                else
                    thermalData.ShowMRP = "N";
                if (chkSavings.Checked == true)
                    thermalData.DispalySavings = "Y";
                else
                    thermalData.DispalySavings = "N";
                if (chkdiscount.Checked == true)
                    thermalData.DisplayDiscount = "Y";
                else
                    thermalData.DisplayDiscount = "N";
                if (radDefault.Checked == true)
                {
                    thermalData.Settings = "D";
                    thermalData.Headings1 = lblproductname.Text;
                    thermalData.Headings2 = lblunit.Text;
                    thermalData.Headings3 = lblmrp.Text;
                    thermalData.Headings4 = lblrate.Text;
                    thermalData.Headings5 = lblamount.Text;
                }
                else
                {
                    thermalData.Settings = "C";
                    thermalData.Headings1 = txtHeadings1.Text;
                    thermalData.Headings2 = txtHeadings2.Text;
                    thermalData.Headings3 = txtHeadings3.Text;
                    thermalData.Headings4 = txtHeadings4.Text;
                    thermalData.Headings5 = txtHeadings5.Text;
                }

                if (chkHeading3.Checked == true)
                    thermalData.CheckH3 = "Y";
                else
                    thermalData.CheckH3 = "N";
                if (chkHeading4.Checked == true)
                    thermalData.CheckH4 = "Y";
                else
                    thermalData.CheckH4 = "N";
                if (chkHeading5.Checked == true)
                    thermalData.CheckH5 = "Y";
                else
                    thermalData.CheckH5 = "N";
                if (radbtnEnglish.Checked == true)
                    thermalData.PrintOnMarathiOrEnglish = "E";
                else
                    thermalData.PrintOnMarathiOrEnglish = "M";
                thermalData.Note = txtNote.Text;
                thermalData.FontFamily = cmbFontName.Text;
                thermalData.FontSize = Convert.ToInt32(txtFontSize.Text);
                if (chkGST.Checked == true)
                    thermalData.GSTDetail = true;
                else
                    thermalData.GSTDetail = false;
                thermalData.CompId = CommonMethod.CompId;
                thermalRepo.Edit(thermalData);
                thermalRepo.Save();
                MessageBox.Show("Thermal Setting Update Successfully.", "Success");
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                thermalRepo.Dispose();
            }
            catch (Exception) { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception) { }
        }

        private void frmThermalBillSettings_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        this.Close();
                }
            }
            catch (Exception) { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void chkShowMRP_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkShowMRP.Checked == true)
                {
                    chkSavings.Enabled = true;
                    chkHeading3.Checked = true;
                }
                else
                {
                    chkSavings.Enabled = false;
                    chkSavings.Checked = false;
                    chkHeading3.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void radCustomize_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radCustomize.Checked == true)
                {
                    chkHeading3.Enabled = true;
                    chkHeading4.Enabled = true;
                    chkHeading5.Enabled = true;
                    txtHeadings1.Text = "वस्तूचे नाव";
                    txtHeadings2.Text = "नग";
                    txtHeadings3.Text = "एम.आर.पी.";
                    txtHeadings4.Text = "दर";
                    txtHeadings5.Text = "रक्कम";
                    chkShowMRP.Checked = false;
                    chkSavings.Checked = false;
                }
            }
            catch (Exception) { }
        }

        private void txtHeadings1_Leave(object sender, EventArgs e)
        {
            if (txtHeadings1.Text != "")
            {
                Image fakeImage = new Bitmap(1, 1);
                Graphics graphicI = Graphics.FromImage(fakeImage);
                stringSizeH1 = graphicI.MeasureString(txtHeadings1.Text, rptfontTitle, stringwidthTitle);
                float str_wdth = stringSizeH1.Width;
                if (str_wdth > 70)
                {
                    MessageBox.Show("Heading 1 size exceeding, Please Enter another text", "Warning");
                    txtHeadings1.Text = "";
                    this.ActiveControl = txtHeadings1;
                }
            }
        }

        private void txtHeadings2_Leave(object sender, EventArgs e)
        {
            if (txtHeadings2.Text != "")
            {
                Graphics graphicI = Graphics.FromImage(fakeImage);
                stringSizeH1 = graphicI.MeasureString(txtHeadings2.Text, rptfontTitle, stringwidthTitle);
                float str_wdth = stringSizeH1.Width;
                if (str_wdth > 60)
                {
                    MessageBox.Show("Heading 2 size exceeding, Please Enter another text", "Warning");
                    txtHeadings2.Text = "";
                    this.ActiveControl = txtHeadings2;
                }
            }
        }

        private void txtHeadings3_Leave(object sender, EventArgs e)
        {
            if (txtHeadings3.Text != "")
            {
                Graphics graphicI = Graphics.FromImage(fakeImage);
                stringSizeH1 = graphicI.MeasureString(txtHeadings3.Text, rptfontTitle, stringwidthTitle);
                float str_wdth = stringSizeH1.Width;
                if (str_wdth > 64)
                {
                    MessageBox.Show("Heading 3 size exceeding, Please Enter another text", "Warning");
                    txtHeadings3.Text = "";
                    this.ActiveControl = txtHeadings3;
                }
            }
        }

        private void txtHeadings4_Leave(object sender, EventArgs e)
        {
            if (txtHeadings4.Text != "")
            {
                Graphics graphicI = Graphics.FromImage(fakeImage);
                stringSizeH1 = graphicI.MeasureString(txtHeadings4.Text, rptfontTitle, stringwidthTitle);
                float str_wdth = stringSizeH1.Width;
                if (str_wdth > 63)
                {
                    MessageBox.Show("Heading 4 size exceeding, Please Enter another text", "Warning");
                    txtHeadings4.Text = "";
                    this.ActiveControl = txtHeadings4;
                }
            }
        }

        private void txtHeadings5_Leave(object sender, EventArgs e)
        {
            if (txtHeadings5.Text != "")
            {
                Graphics graphicI = Graphics.FromImage(fakeImage);
                stringSizeH1 = graphicI.MeasureString(txtHeadings5.Text, rptfontTitle, stringwidthTitle);
                float str_wdth = stringSizeH1.Width;
                if (str_wdth > 40)
                {
                    MessageBox.Show("Heading 5 size exceeding, Please Enter another text", "Warning");
                    txtHeadings5.Text = "";
                    this.ActiveControl = txtHeadings5;
                }
            }
        }

        private void chkHeading3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkHeading3.Checked == true)
                    chkShowMRP.Checked = true;
                else
                    chkShowMRP.Checked = false;
            }
            catch (Exception)
            { }
        }
    }
}