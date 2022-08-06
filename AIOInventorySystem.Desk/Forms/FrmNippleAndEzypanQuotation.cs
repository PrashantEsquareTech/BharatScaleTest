using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class FrmNippleAndEzypanQuotation : Form
    {
        DbClass db = new DbClass();
        public int colIndex = -1, updateId = 0;
        public decimal TotalPerBird = 0;
        List<int> deleteId = new List<int>();
        public static FrmNippleAndEzypanQuotation nipplefeederQta;
        CommonMethod cm = new CommonMethod();

        public FrmNippleAndEzypanQuotation()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            cmbSystemType.SelectedIndex = 0;
        }

        public FrmNippleAndEzypanQuotation(int UpdateID)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            LoadData(UpdateID);
        }

        public void LoadData(int UpdateId)
        {
            try
            {
                NippleFeederSystemMasterRepository nippleFeederRepo = new NippleFeederSystemMasterRepository();
                var nippleFeederDdata = nippleFeederRepo.GetAll().Where(t => t.Id == UpdateId && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (nippleFeederDdata != null)
                {
                    updateId = nippleFeederDdata.Id;
                    dtpQutDate.Value = Convert.ToDateTime(nippleFeederDdata.Date);
                    txtQutNo.Text = Convert.ToString(nippleFeederDdata.NippleEzypanQutNo);
                    txtRefChNo.Text = Convert.ToString(nippleFeederDdata.tblProjectAtGlanceMaster.RefernceNo);
                    lblCustomerName.Text = Convert.ToString(nippleFeederDdata.tblProjectAtGlanceMaster.CustomerInformation.CustomerName);
                    TotalPerBird = Convert.ToDecimal(nippleFeederDdata.tblProjectAtGlanceMaster.NoOfBirds);
                    cmbSystemType.Text = Convert.ToString(nippleFeederDdata.Type);
                    txtRecommendedLengthOfLines.Text = Convert.ToString(nippleFeederDdata.RecommendedLengthOfLines);
                    txtRecommendedNumberOfLines.Text = Convert.ToString(nippleFeederDdata.RecommendedNumberOfLines);
                    txtNumberOfSectionSegementPerLine.Text = Convert.ToString(nippleFeederDdata.NumberOfSectionSegementPerLine);
                    txtNumberOfSectionSegementPerLine1.Text = Convert.ToString(nippleFeederDdata.NumberOfSectionSegementPerLine1);
                    txtNumberOfNipplesFeederPerSegment.Text = Convert.ToString(nippleFeederDdata.NumberOfNipplesFeederPerSegment);
                    txtTotalNumberOfNipplesFeeders.Text = Convert.ToString(nippleFeederDdata.TotalNumberOfNipplesFeeders);
                    txtTotalNumberOfNipplesFeeders1.Text = Convert.ToString(nippleFeederDdata.TotalNumberOfNipplesFeeders1);
                    txtNumberOfBirdsPerNipplesFeeder.Text = Convert.ToString(nippleFeederDdata.NumberOfBirdsPerNipplesFeeder);
                    txtNumberOfBirdsPerNipplesFeederr1.Text = Convert.ToString(nippleFeederDdata.NumberOfBirdsPerNipplesFeederr1);
                    txtSuspensionAtEveryFTNipplesFeeder.Text = Convert.ToString(nippleFeederDdata.SuspensionAtEveryFTNipplesFeeder);
                    txtSuspensionAtEveryFTNipplesFeeder1.Text = Convert.ToString(nippleFeederDdata.SuspensionAtEveryFTNipplesFeeder1);
                    txtNumberOFPressureBreakers.Text = Convert.ToString(nippleFeederDdata.NumberOFPressureBreakers);
                    txtNippleFeederType.Text = Convert.ToString(nippleFeederDdata.NippleFeederType);
                    if (nippleFeederDdata.ShokerSystemNeededNipplesFeeder == "YES".Trim())
                        chkShokerSystemNeededNipplesFeeder.Checked = true;
                    else
                        chkShokerSystemNeededNipplesFeeder.Checked = false;
                    if (nippleFeederDdata.NippleTray == "YES".Trim())
                        chkNippleTray.Checked = true;
                    else
                        chkNippleTray.Checked = false;
                    if (nippleFeederDdata.ControlPanelNeededFeeder == "YES".Trim())
                        chkControlPanelNeededFeeder.Checked = true;
                    else
                        chkControlPanelNeededFeeder.Checked = false;
                    if (nippleFeederDdata.WaterMeter == "YES".Trim())
                        chkWaterMeter.Checked = true;
                    else
                        chkWaterMeter.Checked = false;
                    if (nippleFeederDdata.DiskFilter == "YES".Trim())
                        chkDiskFilter.Checked = true;
                    else
                        chkDiskFilter.Checked = false;
                    if (nippleFeederDdata.Medicator == "YES".Trim())
                        chkMedicator.Checked = true;
                    else
                        chkMedicator.Checked = false;
                    if (nippleFeederDdata.EzyCupDrinker == "YES".Trim())
                        chkEzyCupDrinker.Checked = true;
                    else
                        chkEzyCupDrinker.Checked = false;
                    txtTotalNippleFeederAmt.Text = Convert.ToString(nippleFeederDdata.NippleSystemFeederCost);
                    txtTotalSuspension.Text = Convert.ToString(nippleFeederDdata.SuspensionSystemCost);
                    txtTotalCost.Text = Convert.ToString(nippleFeederDdata.AllTotalCost);
                    txtTotalCostPerBird.Text = Convert.ToString(nippleFeederDdata.TotalCostPerHouse);
                    if (nippleFeederDdata.NippleSystemFeederCost1 == null)
                        txtTotalNippleFeederAmt1.Text = Convert.ToString(nippleFeederDdata.NippleSystemFeederCost);
                    else
                        txtTotalNippleFeederAmt1.Text = Convert.ToString(nippleFeederDdata.NippleSystemFeederCost1);
                    if (nippleFeederDdata.SuspensionSystemCost1 == null)
                        txtTotalSuspension1.Text = Convert.ToString(nippleFeederDdata.SuspensionSystemCost);
                    else
                        txtTotalSuspension1.Text = Convert.ToString(nippleFeederDdata.SuspensionSystemCost1);
                    if (nippleFeederDdata.SuspensionPercent == null)
                        txtSuspensionPercent.Text = "0";
                    else
                        txtSuspensionPercent.Text = Convert.ToString(nippleFeederDdata.SuspensionPercent);
                    if (nippleFeederDdata.NippleFeederPercent == null)
                        txtSuspensionPercent.Text = "0";
                    else
                        txtNipplePercent.Text = Convert.ToString(nippleFeederDdata.NippleFeederPercent);
                    if (nippleFeederDdata.MotorPh == null)
                        txtMotor1or3ph.Text = "0";
                    else
                        txtMotor1or3ph.Text = Convert.ToString(nippleFeederDdata.MotorPh);
                    if (nippleFeederDdata.HopperKG == null)
                        txtHopperKG.Text = "0";
                    else
                        txtHopperKG.Text = Convert.ToString(nippleFeederDdata.HopperKG);
                    txtEzypanV2V3.Text = Convert.ToString(nippleFeederDdata.EzypanV1V2);

                    NippleFeederSystemDetailRepository nippleFeederDRepo = new NippleFeederSystemDetailRepository();
                    List<tblNippleFeederSystemDetail> nippleFeederDData = nippleFeederDRepo.GetAll().Where(t => t.MasterId == updateId && t.tblNippleFeederSystemMaster.CompId == CommonMethod.CompId && t.tblNippleFeederSystemMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                    if (nippleFeederDData.Count > 0)
                    {
                        int i = 0;
                        foreach (var item in nippleFeederDData)
                        {
                            dtgvDetails.Rows.Add();
                            dtgvDetails.Rows[i].Cells[0].Value = item.Id;
                            dtgvDetails.Rows[i].Cells[1].Value = item.Code;
                            dtgvDetails.Rows[i].Cells[2].Value = item.Description;
                            dtgvDetails.Rows[i].Cells[3].Value = item.Quantity;
                            dtgvDetails.Rows[i].Cells[4].Value = item.SuspensionSystem.Trim();
                            dtgvDetails.Rows[i].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            i++;
                        }
                    }

                    btnUpdate.Enabled = true;
                    btnsave.Enabled = false;
                    btnDelete.Enabled = true;
                    this.ActiveControl = btnUpdate;
                    if (cmbSystemType.Text == "NIPPLE SYSTEM")
                    {
                        txtNumberOFPressureBreakers.Enabled = true;
                        chkControlPanelNeededFeeder.Enabled = false;
                        chkDiskFilter.Enabled = true;
                        chkEzyCupDrinker.Enabled = true;
                        chkNippleTray.Enabled = true;
                        chkWaterMeter.Enabled = true;
                        chkMedicator.Enabled = true;
                        chkSuspensionSystem.Enabled = true;
                        txtTotalNumberOfNipplesFeeders1.Enabled = false;
                        AutoCompleteDescription();
                        txtSuspensionPercent.Enabled = true; txtSuspensionPercentAmt.Enabled = true;
                        txtTotalSuspension.Enabled = true; txtTotalSuspension1.Enabled = true;
                        txtNumberOfBirdsPerNipplesFeederr1.Enabled = false; txtSuspensionAtEveryFTNipplesFeeder1.Enabled = false;
                        txtHopperKG.Enabled = false; txtMotor1or3ph.Enabled = false; txtEzypanV2V3.Enabled = false;
                    }
                    else if (cmbSystemType.Text == "EZYPAN FEEDING SYSTEM")
                    {
                        txtNumberOFPressureBreakers.Enabled = false;
                        chkControlPanelNeededFeeder.Enabled = true;
                        chkDiskFilter.Enabled = false;
                        chkEzyCupDrinker.Enabled = false;
                        chkNippleTray.Enabled = false;
                        chkWaterMeter.Enabled = false;
                        chkMedicator.Enabled = false;
                        chkSuspensionSystem.Enabled = false;
                        AutoCompleteDescription();
                        txtSuspensionPercent.Enabled = false; txtSuspensionPercentAmt.Enabled = false;
                        txtTotalSuspension.Enabled = false; txtTotalSuspension1.Enabled = false;
                        txtTotalNumberOfNipplesFeeders1.Enabled = true;
                        txtNumberOfBirdsPerNipplesFeederr1.Enabled = true; txtSuspensionAtEveryFTNipplesFeeder1.Enabled = true;
                        txtHopperKG.Enabled = true; txtMotor1or3ph.Enabled = true; txtEzypanV2V3.Enabled = true;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void maxproductid()
        {
            try
            {
                NippleFeederSystemMasterRepository productrepo = new NippleFeederSystemMasterRepository();
                var mid = Convert.ToInt32(productrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.NippleEzypanQutNo));
                if (mid == null)
                    txtQutNo.Text = "1";
                else
                    txtQutNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                productrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                maxproductid(); txtNumberOfSectionSegementPerLine1.Text = "";
                lblCustomerName.Text = ""; txtTotalNumberOfNipplesFeeders.Text = "";
                cmbSystemType.SelectedIndex = 0; txtCode.Text = "";
                TotalPerBird = 0; txtDescription.Text = "";
                txtNippleFeederType.Text = ""; txtNumberOfBirdsPerNipplesFeeder.Text = "";
                txtNumberOfBirdsPerNipplesFeederr1.Text = ""; txtNumberOfNipplesFeederPerSegment.Text = "";
                txtNumberOFPressureBreakers.Text = ""; txtNumberOfSectionSegementPerLine.Text = "";
                txtQty.Text = ""; txtRecommendedLengthOfLines.Text = "";
                txtRecommendedNumberOfLines.Text = ""; txtRefChNo.Text = "";
                txtSuspensionAtEveryFTNipplesFeeder.Text = ""; txtSuspensionAtEveryFTNipplesFeeder1.Text = "";
                txtTotalCost.Text = ""; txtTotalCostPerBird.Text = "";
                txtTotalNippleFeederAmt.Text = ""; txtTotalNumberOfNipplesFeeders1.Text = "";
                txtTotalSuspension.Text = "";
                chkSuspensionSystem.Checked = false; chkControlPanelNeededFeeder.Checked = false;
                chkDiskFilter.Checked = false; chkEzyCupDrinker.Checked = false;
                chkMedicator.Checked = false; chkNippleTray.Checked = false;
                chkShokerSystemNeededNipplesFeeder.Checked = false; chkSuspensionSystem.Checked = false;
                chkWaterMeter.Checked = false; btnsave.Enabled = true;
                btnUpdate.Enabled = false; btnDelete.Enabled = false;
                txtNumberOFPressureBreakers.Enabled = true; chkControlPanelNeededFeeder.Enabled = true;
                chkDiskFilter.Enabled = true; chkEzyCupDrinker.Enabled = true;
                chkNippleTray.Enabled = true; chkWaterMeter.Enabled = true;
                chkMedicator.Enabled = true; chkSuspensionSystem.Enabled = true;
                dtgvDetails.Rows.Clear();
                txtTotalNumberOfNipplesFeeders1.Enabled = true;
                txtHopperKG.Text = ""; txtEzypanV2V3.Text = "";
                txtMotor1or3ph.Text = ""; txtSuspensionPercentAmt.Text = "";
                txtSuspensionPercent.Text = "0"; txtNipplePercent.Text = "0";
                txtNipplePercentAmt.Text = ""; txtTotalNippleFeederAmt1.Text = "0";
                txtTotalSuspension1.Text = "0";
                txtHopperKG.Enabled = true; txtMotor1or3ph.Enabled = true;
                txtEzypanV2V3.Enabled = true; txtNumberOfBirdsPerNipplesFeederr1.Enabled = true;
                txtSuspensionAtEveryFTNipplesFeeder1.Enabled = true; this.ActiveControl = btnClose;
            }
            catch (Exception)
            { }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    this.Close();
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        public void AutoCompleteDescription()
        {
            try
            {
                txtDescription.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand("Select distinct nd.Description from tblNippleFeederSystemDetail as nd inner join tblNippleFeederSystemMaster as nm on nm.Id=nd.MasterId where nm.Type='" + cmbSystemType.Text + "' and compid=" + CommonMethod.CompId, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtDescription.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
            try
            {
                txtCode.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand("Select distinct nd.Code from tblNippleFeederSystemDetail as nd inner join tblNippleFeederSystemMaster as nm on nm.Id=nd.MasterId where nm.Type='" + cmbSystemType.Text + "' and compid=" + CommonMethod.CompId, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtCode.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception)
            { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbSystemType.Text != "" && txtNippleFeederType.Text != "" && dtgvDetails.Rows.Count > 0)
                {
                    if (txtTotalNippleFeederAmt.Text != "" && txtTotalCostPerBird.Text != "")
                    {
                        NippleFeederSystemMasterRepository nippleFeederMRepo = new NippleFeederSystemMasterRepository();
                        var nippleFeederData = nippleFeederMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Type.ToLower().Trim() == cmbSystemType.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (nippleFeederData != null && btnsave.Enabled != false)
                        {
                            MessageBox.Show("Already Saved this record");
                            this.ActiveControl = txtRefChNo;
                        }
                        else
                        {
                            ProjectGlanceMasterRepository projGMRepo = new ProjectGlanceMasterRepository();
                            var projMData = projGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (projMData != null)
                            {
                                tblNippleFeederSystemMaster nipplefeederMData = new tblNippleFeederSystemMaster();
                                nipplefeederMData.ReferenceId = projMData.Id;
                                nipplefeederMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                                nipplefeederMData.NippleEzypanQutNo = Convert.ToInt32(txtQutNo.Text);
                                nipplefeederMData.Type = Convert.ToString(cmbSystemType.Text.Trim());
                                if (txtRecommendedLengthOfLines.Text != "")
                                    nipplefeederMData.RecommendedLengthOfLines = Convert.ToDecimal(txtRecommendedLengthOfLines.Text);
                                else
                                    nipplefeederMData.RecommendedLengthOfLines = 0;
                                if (txtRecommendedNumberOfLines.Text != "")
                                    nipplefeederMData.RecommendedNumberOfLines = Convert.ToDecimal(txtRecommendedNumberOfLines.Text);
                                else
                                    nipplefeederMData.RecommendedNumberOfLines = 0;
                                if (txtNumberOfSectionSegementPerLine1.Text != "")
                                    nipplefeederMData.NumberOfSectionSegementPerLine1 = Convert.ToDecimal(txtNumberOfSectionSegementPerLine1.Text);
                                else
                                    nipplefeederMData.NumberOfSectionSegementPerLine1 = 0;
                                if (txtNumberOfSectionSegementPerLine.Text != "")
                                    nipplefeederMData.NumberOfSectionSegementPerLine = Convert.ToDecimal(txtNumberOfSectionSegementPerLine.Text);
                                else
                                    nipplefeederMData.NumberOfSectionSegementPerLine = 0;
                                if (txtNumberOfNipplesFeederPerSegment.Text != "")
                                    nipplefeederMData.NumberOfNipplesFeederPerSegment = Convert.ToDecimal(txtNumberOfNipplesFeederPerSegment.Text);
                                else
                                    nipplefeederMData.NumberOfNipplesFeederPerSegment = 0;
                                if (txtTotalNumberOfNipplesFeeders.Text != "")
                                    nipplefeederMData.TotalNumberOfNipplesFeeders = Convert.ToDecimal(txtTotalNumberOfNipplesFeeders.Text);
                                else
                                    nipplefeederMData.TotalNumberOfNipplesFeeders = 0;
                                if (txtTotalNumberOfNipplesFeeders1.Text != "")
                                    nipplefeederMData.TotalNumberOfNipplesFeeders1 = Convert.ToDecimal(txtTotalNumberOfNipplesFeeders1.Text);
                                else
                                    nipplefeederMData.TotalNumberOfNipplesFeeders1 = 0;
                                if (txtNumberOfBirdsPerNipplesFeeder.Text != "")
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeeder = Convert.ToDecimal(txtNumberOfBirdsPerNipplesFeeder.Text);
                                else
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeeder = 0;
                                if (txtNumberOfBirdsPerNipplesFeederr1.Text != "")
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeederr1 = Convert.ToDecimal(txtNumberOfBirdsPerNipplesFeederr1.Text);
                                else
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeederr1 = 0;
                                if (txtSuspensionAtEveryFTNipplesFeeder.Text != "")
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder = Convert.ToDecimal(txtSuspensionAtEveryFTNipplesFeeder.Text);
                                else
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder = 0;
                                if (txtSuspensionAtEveryFTNipplesFeeder1.Text != "")
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder1 = Convert.ToDecimal(txtSuspensionAtEveryFTNipplesFeeder1.Text);
                                else
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder1 = 0;
                                if (txtNumberOFPressureBreakers.Text != "")
                                    nipplefeederMData.NumberOFPressureBreakers = Convert.ToDecimal(txtNumberOFPressureBreakers.Text);
                                else
                                    nipplefeederMData.NumberOFPressureBreakers = 0;
                                if (chkShokerSystemNeededNipplesFeeder.Checked == true)
                                    nipplefeederMData.ShokerSystemNeededNipplesFeeder = "YES".Trim();
                                else
                                    nipplefeederMData.ShokerSystemNeededNipplesFeeder = "NO".Trim();
                                if (chkControlPanelNeededFeeder.Checked == true)
                                    nipplefeederMData.ControlPanelNeededFeeder = "YES".Trim();
                                else
                                    nipplefeederMData.ControlPanelNeededFeeder = "NO".Trim();
                                nipplefeederMData.NippleFeederType = txtNippleFeederType.Text.Trim();
                                if (chkNippleTray.Checked == true)
                                    nipplefeederMData.NippleTray = "YES".Trim();
                                else
                                    nipplefeederMData.NippleTray = "NO".Trim();
                                if (chkWaterMeter.Checked == true)
                                    nipplefeederMData.WaterMeter = "YES".Trim();
                                else
                                    nipplefeederMData.WaterMeter = "NO".Trim();
                                if (chkDiskFilter.Checked == true)
                                    nipplefeederMData.DiskFilter = "YES".Trim();
                                else
                                    nipplefeederMData.DiskFilter = "NO".Trim();
                                if (chkMedicator.Checked == true)
                                    nipplefeederMData.Medicator = "YES".Trim();
                                else
                                    nipplefeederMData.Medicator = "NO".Trim();
                                if (chkEzyCupDrinker.Checked == true)
                                    nipplefeederMData.EzyCupDrinker = "YES".Trim();
                                else
                                    nipplefeederMData.EzyCupDrinker = "NO".Trim();
                                if (txtTotalSuspension.Text != "")
                                    nipplefeederMData.SuspensionSystemCost = Convert.ToDecimal(txtTotalSuspension.Text);
                                else
                                    nipplefeederMData.SuspensionSystemCost = 0;
                                if (txtTotalNippleFeederAmt.Text != "")
                                    nipplefeederMData.NippleSystemFeederCost = Convert.ToDecimal(txtTotalNippleFeederAmt.Text);
                                else
                                    nipplefeederMData.NippleSystemFeederCost = 0;
                                if (txtTotalCostPerBird.Text != "")
                                    nipplefeederMData.TotalCostPerHouse = Convert.ToDecimal(txtTotalCostPerBird.Text);
                                else
                                    nipplefeederMData.TotalCostPerHouse = 0;
                                if (txtTotalCost.Text != "")
                                    nipplefeederMData.AllTotalCost = Convert.ToDecimal(txtTotalCost.Text);
                                else
                                    nipplefeederMData.AllTotalCost = 0;
                                nipplefeederMData.CompId = CommonMethod.CompId;
                                nipplefeederMData.TransactionYear = CommonMethod.TransactionYear;
                                if (txtHopperKG.Text != "")
                                    nipplefeederMData.HopperKG = Convert.ToDecimal(txtHopperKG.Text);
                                else
                                    nipplefeederMData.HopperKG = 0;
                                if (txtMotor1or3ph.Text != "")
                                    nipplefeederMData.MotorPh = Convert.ToDecimal(txtMotor1or3ph.Text);
                                else
                                    nipplefeederMData.MotorPh = 0;
                                if (txtNipplePercent.Text != "")
                                    nipplefeederMData.NippleFeederPercent = Convert.ToDecimal(txtNipplePercent.Text);
                                else
                                    nipplefeederMData.NippleFeederPercent = 0;
                                if (txtTotalSuspension1.Text != "")
                                    nipplefeederMData.SuspensionSystemCost1 = Convert.ToDecimal(txtTotalSuspension1.Text);
                                else
                                    nipplefeederMData.SuspensionSystemCost1 = 0;
                                if (txtTotalNippleFeederAmt1.Text != "")
                                    nipplefeederMData.NippleSystemFeederCost1 = Convert.ToDecimal(txtTotalNippleFeederAmt1.Text);
                                else
                                    nipplefeederMData.NippleSystemFeederCost1 = 0;
                                if (txtSuspensionPercent.Text != "")
                                    nipplefeederMData.SuspensionPercent = Convert.ToDecimal(txtSuspensionPercent.Text);
                                else
                                    nipplefeederMData.SuspensionPercent = 0;
                                nipplefeederMData.EzypanV1V2 = txtEzypanV2V3.Text.Trim();
                                nippleFeederMRepo.Add(nipplefeederMData);
                                nippleFeederMRepo.Save();

                                ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData = projDDRepo.GetAll().Where(t => t.MasterId == projMData.Id && t.Description.Trim() == cmbSystemType.Text.Trim()).FirstOrDefault();
                                if (projDData != null)
                                {
                                    projDDRepo = new ProjectGlanceDetailRepository();
                                    tblProjectGlanceDetail projDData1 = projDDRepo.GetById(projDData.Id);
                                    projDData1.InvestBird = Convert.ToDecimal(nipplefeederMData.TotalCostPerHouse);
                                    projDData1.InvestmentPerHouse = Convert.ToDecimal(nipplefeederMData.AllTotalCost);
                                    projDDRepo.Edit(projDData1);
                                    projDDRepo.Save();

                                    try
                                    {
                                        decimal totalGSTPercent = 0, totalInvestAmt = 0, totalSCgstPercent = 0, totalIgstPercent = 0;
                                        decimal netAmt = 0, GSTAmt = 0, totalGstAmt = 0;
                                        projDDRepo = new ProjectGlanceDetailRepository();
                                        totalInvestAmt = Convert.ToDecimal(projDDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId).Sum(t => t.InvestmentPerHouse));
                                        totalIgstPercent = Convert.ToDecimal(projDDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId && t.Id == projDData1.Id).Sum(t => t.IGstPercent));
                                        totalSCgstPercent = Convert.ToDecimal(projDDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId && t.Id == projDData1.Id).Sum(t => t.SGSTCGSTPercent));
                                        totalGSTPercent = totalIgstPercent + totalSCgstPercent;

                                        GSTAmt = Convert.ToDecimal(projDData1.InvestmentPerHouse) * totalGSTPercent / 100;
                                        ProjectGlanceDetailRepository pDRepo = new ProjectGlanceDetailRepository();
                                        List<tblProjectGlanceDetail> pdList = pDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId).ToList();
                                        if (pdList.Count > 0)
                                        {
                                            foreach (var item in pdList)
                                            {
                                                totalGSTPercent = Convert.ToDecimal(item.IGstPercent) + Convert.ToDecimal(item.SGSTCGSTPercent);
                                                totalGstAmt = totalGstAmt + Convert.ToDecimal(totalGSTPercent * item.InvestmentPerHouse / 100);
                                            }
                                        }
                                        netAmt = totalGstAmt + totalInvestAmt;
                                        ProjectGlanceMasterRepository projAGMRepo = new ProjectGlanceMasterRepository();
                                        tblProjectAtGlanceMaster projAGMData = projAGMRepo.GetById(Convert.ToInt32(projDData1.MasterId));
                                        projAGMData.TotalAmt = totalInvestAmt;
                                        projAGMData.GSTAmt = totalGstAmt;
                                        projAGMData.NetAmt = netAmt;
                                        projAGMRepo.Edit(projAGMData);
                                        projAGMRepo.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                updateId = Convert.ToInt32(nipplefeederMData.Id);
                                for (int i = 0; i < dtgvDetails.Rows.Count; i++)
                                {
                                    NippleFeederSystemDetailRepository nipplefeederDRepo = new NippleFeederSystemDetailRepository();
                                    tblNippleFeederSystemDetail nippleFeederDData = new tblNippleFeederSystemDetail();
                                    nippleFeederDData.MasterId = updateId;
                                    nippleFeederDData.Quantity = Convert.ToDecimal(dtgvDetails.Rows[i].Cells[3].Value);
                                    nippleFeederDData.Code = Convert.ToString(dtgvDetails.Rows[i].Cells[1].Value);
                                    nippleFeederDData.Description = Convert.ToString(dtgvDetails.Rows[i].Cells[2].Value);
                                    nippleFeederDData.SuspensionSystem = Convert.ToString(dtgvDetails.Rows[i].Cells[4].Value);
                                    nipplefeederDRepo.Add(nippleFeederDData);
                                    nipplefeederDRepo.Save();
                                    nipplefeederDRepo.Dispose();
                                }
                                MessageBox.Show("Record Save Successfully");
                                clear();
                                nippleFeederMRepo.Dispose();
                            }
                        }
                    }
                    else
                    {
                        if (txtTotalNippleFeederAmt.Text == "")
                        {
                            MessageBox.Show("Enter Total Nipple/Feeder Amount");
                            this.ActiveControl = txtTotalNippleFeederAmt;
                        }
                        else if (txtTotalCostPerBird.Text == "")
                        {
                            MessageBox.Show("Enter Total Cost Per Bird");
                            this.ActiveControl = txtTotalCostPerBird;
                        }
                    }
                }
                else
                {
                    if (txtRefChNo.Text == "")
                    {
                        MessageBox.Show("Enter Refernce Challan No");
                        this.ActiveControl = txtRefChNo;
                    }
                    else if (cmbSystemType.Text == "")
                    {
                        MessageBox.Show("Select System Type");
                        this.ActiveControl = cmbSystemType;
                    }
                    else if (txtNippleFeederType.Text == "")
                    {
                        MessageBox.Show("Select Nipple / Feeder System Type");
                        this.ActiveControl = txtNippleFeederType;
                    }
                    else if (dtgvDetails.Rows.Count > 0)
                    {
                        MessageBox.Show("Add at least one description");
                        this.ActiveControl = txtCode;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtNumeric_Print(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i >= 48 && i <= 57) || i == 8 || i == 45)
                { }
                else
                { e.Handled = true; }
            }
            catch (Exception)
            { }
        }

        private void txtRefChNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "")
                {
                    ProjectGlanceMasterRepository projGMRepo = new ProjectGlanceMasterRepository();
                    var projMData = projGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (projMData != null)
                    {
                        lblCustomerName.Text = Convert.ToString(projMData.CustomerInformation.CustomerName);
                        TotalPerBird = Convert.ToDecimal(projMData.NoOfBirds);
                    }
                    else
                    {
                        MessageBox.Show("This Reference is not valid");
                        this.ActiveControl = txtRefChNo;
                        txtRefChNo.Text = "";
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbSystemType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbSystemType.Text != "" && txtRefChNo.Text != "")
                {

                    ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                    var projeDData = projDDRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Description == cmbSystemType.Text).FirstOrDefault();
                    if (projeDData != null)
                    {
                        NippleFeederSystemMasterRepository nippleFeederMRepo = new NippleFeederSystemMasterRepository();
                        var nippleFeederMData = nippleFeederMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Type == cmbSystemType.Text).FirstOrDefault();
                        if (nippleFeederMData != null && btnsave.Enabled != false)
                        {
                            MessageBox.Show("This type of quotation save already");
                            this.ActiveControl = cmbSystemType;
                        }
                        else
                        {
                            if (cmbSystemType.Text == "NIPPLE SYSTEM")
                            {
                                txtNumberOFPressureBreakers.Enabled = true;
                                chkControlPanelNeededFeeder.Enabled = false;
                                chkDiskFilter.Enabled = true;
                                chkEzyCupDrinker.Enabled = true;
                                chkNippleTray.Enabled = true;
                                chkWaterMeter.Enabled = true;
                                chkMedicator.Enabled = true;
                                chkSuspensionSystem.Enabled = true;
                                txtTotalNumberOfNipplesFeeders1.Enabled = false;
                                AutoCompleteDescription();
                                txtSuspensionPercent.Enabled = true; txtSuspensionPercentAmt.Enabled = true;
                                txtTotalSuspension.Enabled = true; txtTotalSuspension1.Enabled = true;
                                txtNumberOfBirdsPerNipplesFeederr1.Enabled = false; txtSuspensionAtEveryFTNipplesFeeder1.Enabled = false;
                                txtHopperKG.Enabled = false; txtMotor1or3ph.Enabled = false; txtEzypanV2V3.Enabled = false;
                            }
                            else if (cmbSystemType.Text == "EZYPAN FEEDING SYSTEM")
                            {
                                txtNumberOFPressureBreakers.Enabled = false;
                                chkControlPanelNeededFeeder.Enabled = true;
                                chkDiskFilter.Enabled = false;
                                chkEzyCupDrinker.Enabled = false;
                                chkNippleTray.Enabled = false;
                                chkWaterMeter.Enabled = false;
                                chkMedicator.Enabled = false;
                                chkSuspensionSystem.Enabled = false;
                                AutoCompleteDescription();
                                txtTotalNumberOfNipplesFeeders1.Enabled = true;
                                txtSuspensionPercent.Enabled = false; txtSuspensionPercentAmt.Enabled = false;
                                txtTotalSuspension.Enabled = false; txtTotalSuspension1.Enabled = false;
                                txtNumberOfBirdsPerNipplesFeederr1.Enabled = true; txtSuspensionAtEveryFTNipplesFeeder1.Enabled = true;
                                txtHopperKG.Enabled = true; txtMotor1or3ph.Enabled = true; txtEzypanV2V3.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Reference can't use System");
                        this.ActiveControl = cmbSystemType;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text != "" && txtDescription.Text != "" && txtQty.Text != "")
                {
                    if (colIndex == -1)
                    {
                        for (int m = 0; m < dtgvDetails.Rows.Count; m++)
                        {
                            if (txtCode.Text == Convert.ToString(dtgvDetails.Rows[m].Cells[1].Value))
                            {
                                MessageBox.Show("Already Exists");
                                this.ActiveControl = txtCode;
                                goto a;
                            }
                        }
                    }
                    else
                    {
                        goto b;
                    }
                b:
                    {
                        if (colIndex == -1)
                        {
                            dtgvDetails.Rows.Add();
                            int i = dtgvDetails.RowCount;
                            dtgvDetails.Rows[i - 1].Cells[1].Value = txtCode.Text;
                            dtgvDetails.Rows[i - 1].Cells[2].Value = txtDescription.Text;
                            if (chkSuspensionSystem.Checked == true)
                                dtgvDetails.Rows[i - 1].Cells[4].Value = "YES".Trim();
                            else
                                dtgvDetails.Rows[i - 1].Cells[4].Value = "NO".Trim();
                            if (txtQty.Text == "")
                                dtgvDetails.Rows[i - 1].Cells[3].Value = "0";
                            else
                                dtgvDetails.Rows[i - 1].Cells[3].Value = txtQty.Text;
                            dtgvDetails.Rows[i - 1].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            txtCode.Text = "";
                            txtDescription.Text = "";
                            chkSuspensionSystem.Checked = false;
                            txtQty.Text = "";
                            this.ActiveControl = txtCode;
                            colIndex = -1;
                        }
                        else
                        {
                            dtgvDetails.Rows[colIndex].Cells[1].Value = txtCode.Text;
                            dtgvDetails.Rows[colIndex].Cells[2].Value = txtDescription.Text;
                            if (chkSuspensionSystem.Checked == true)
                                dtgvDetails.Rows[colIndex].Cells[4].Value = "YES".Trim();
                            else
                                dtgvDetails.Rows[colIndex].Cells[4].Value = "NO".Trim();
                            if (txtQty.Text == "")
                                dtgvDetails.Rows[colIndex].Cells[3].Value = "0";
                            else
                                dtgvDetails.Rows[colIndex].Cells[3].Value = txtQty.Text;
                            dtgvDetails.Rows[colIndex].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            txtCode.Text = "";
                            txtDescription.Text = "";
                            chkSuspensionSystem.Checked = false;
                            txtQty.Text = "";
                            this.ActiveControl = txtCode;
                            colIndex = -1;
                        }
                    }
                a:
                    { }
                }
                else
                {
                    if (txtCode.Text == "")
                    {
                        MessageBox.Show("Enter Code No");
                        this.ActiveControl = txtCode;
                    }
                    else if (txtDescription.Text == "")
                    {
                        MessageBox.Show("Enter Description");
                        this.ActiveControl = txtDescription;
                    }
                    else if (txtQty.Text == "")
                    {
                        MessageBox.Show("Enter Quantity");
                        this.ActiveControl = txtQty;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btndeliverychallenlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                nipplefeederQta = this;
                FrmNippleAndEzypanQuotation pq = new FrmNippleAndEzypanQuotation();
                pq.Visible = false;
                FrmNippleFeederSyytemQuotationList plist = new FrmNippleFeederSyytemQuotationList();
                plist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        public void loadRefernceNo()
        {
            try
            {
                txtRefChNo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand("SELECT RefernceNo FROM tblProjectAtGlanceMaster where compid=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtRefChNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception) { }
        }

        private void FrmNippleAndEzypanQuotation_Load(object sender, EventArgs e)
        {
            try
            {
                loadRefernceNo();
                AutoCompleteDescription();
            }
            catch (Exception)
            { }
        }

        public void calNetAmt()
        {
            try
            {
                if (txtTotalNippleFeederAmt1.Text != "" && txtNipplePercent.Text != "")
                {
                    decimal nippleTotalNippleFeederAmt = 0, suspensionAmt = 0, totalAmt = 0, calPerBird = 0, NippleInPer = 0, NippletoatlAmt = 0, supspenPer = 0, suspensiontotalAmt = 0, susperAmt = 0, nippleperAmt = 0;
                    nippleTotalNippleFeederAmt = Convert.ToDecimal(txtTotalNippleFeederAmt1.Text);
                    NippleInPer = Convert.ToDecimal(txtNipplePercent.Text);
                    NippletoatlAmt = nippleTotalNippleFeederAmt + (nippleTotalNippleFeederAmt * NippleInPer / 100);
                    nippleperAmt = Convert.ToDecimal(nippleTotalNippleFeederAmt * NippleInPer / 100);
                    suspensionAmt = Convert.ToDecimal(txtTotalSuspension1.Text);
                    supspenPer = Convert.ToDecimal(txtSuspensionPercent.Text);
                    suspensiontotalAmt = suspensionAmt + (suspensionAmt * supspenPer / 100);
                    susperAmt = Convert.ToDecimal(suspensionAmt * supspenPer / 100);
                    txtSuspensionPercentAmt.Text = Convert.ToString(susperAmt);
                    totalAmt = NippletoatlAmt + suspensiontotalAmt;
                    try
                    {
                        calPerBird = totalAmt / TotalPerBird;
                    }
                    catch (Exception)
                    {
                        calPerBird = 0;
                    }
                    txtNipplePercentAmt.Text = Convert.ToString(Math.Round(nippleperAmt, 2, MidpointRounding.AwayFromZero));
                    txtTotalNippleFeederAmt.Text = Convert.ToString(Math.Round(NippletoatlAmt, 2, MidpointRounding.AwayFromZero));
                    txtTotalSuspension.Text = Convert.ToString(Math.Round(suspensiontotalAmt, 2, MidpointRounding.AwayFromZero));
                    txtTotalCost.Text = Convert.ToString(Math.Round(totalAmt, 2, MidpointRounding.AwayFromZero));
                    txtTotalCostPerBird.Text = Convert.ToString(Math.Round(calPerBird, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        private void txtTotalNippleFeederAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalNippleFeederAmt.Text != "")
                    calNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtTotalSuspension_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalSuspension.Text != "")
                    calNetAmt();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                NippleFeederSystemMasterRepository nippleEzypanRepo = new NippleFeederSystemMasterRepository();
                var nippleEzypanData = nippleEzypanRepo.GetAll().Where(t => t.Id == updateId).FirstOrDefault();
                if (nippleEzypanData != null)
                {
                    RptProjectAtGlanceAndNippleFeederQuotation rpt = new RptProjectAtGlanceAndNippleFeederQuotation(updateId, "NippleFeederQut");
                    rpt.ShowDialog();
                    rpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtNipplePercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNipplePercent.Text != "")
                    calNetAmt();
                else
                {
                    txtNipplePercent.Text = "0";
                    this.ActiveControl = txtNipplePercent;
                }
            }
            catch (Exception)
            { }
        }

        private void txtTotalSuspension1_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalSuspension1.Text != "")
                calNetAmt();
        }

        private void txtSuspensionPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSuspensionPercent.Text != "")
                    calNetAmt();
                else
                {
                    txtSuspensionPercent.Text = "0";
                    this.ActiveControl = txtSuspensionPercent;
                }
            }
            catch (Exception)
            { }
        }

        private void txtNipplePercentAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtNipplePercentAmt.Text != "")
                {
                    if (Convert.ToDecimal(txtNipplePercentAmt.Text) <= Convert.ToDecimal(txtTotalNippleFeederAmt1.Text))
                    {
                        decimal discper = 0;
                        discper = Convert.ToDecimal(txtNipplePercentAmt.Text) * 100 / Convert.ToDecimal(txtTotalNippleFeederAmt1.Text);
                        txtNipplePercent.Text = Convert.ToString(Math.Round(discper, 5, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Nipple Feeder Amount.", "Warning");
                        txtNipplePercentAmt.Text = "0";
                        this.ActiveControl = txtNipplePercentAmt;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtSuspensionPercentAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSuspensionPercentAmt.Text != "")
                {
                    if (Convert.ToDecimal(txtSuspensionPercentAmt.Text) <= Convert.ToDecimal(txtTotalSuspension1.Text))
                    {
                        decimal discper = 0;
                        discper = Convert.ToDecimal(txtSuspensionPercentAmt.Text) * 100 / Convert.ToDecimal(txtTotalSuspension1.Text);
                        txtSuspensionPercent.Text = Convert.ToString(Math.Round(discper, 5, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Nipple Feeder Amount.", "Warning");
                        txtSuspensionPercentAmt.Text = "0";
                        this.ActiveControl = txtSuspensionPercentAmt;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtTotalNippleFeederAmt1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalNippleFeederAmt1.Text != "")
                    calNetAmt();
            }
            catch (Exception)
            { }
        }

        private void dtgvDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (dtgvDetails.Rows.Count > 0 && btnDelete.Enabled == true)
                    {
                        if (Convert.ToInt32(dtgvDetails.Rows[e.RowIndex].Cells[0].Value) != 0)
                            deleteId.Add(Convert.ToInt32(dtgvDetails.Rows[e.RowIndex].Cells[0].Value));
                        dtgvDetails.Rows.Remove(dtgvDetails.Rows[e.RowIndex]);
                    }
                    else if (btnsave.Enabled == true)
                        dtgvDetails.Rows.Remove(dtgvDetails.Rows[e.RowIndex]);
                    else
                        MessageBox.Show("This Quotation has only 1 product." + Environment.NewLine + "You have to delete full Order.", "Warning");
                }
                else
                {
                    txtCode.Text = Convert.ToString(dtgvDetails.Rows[e.RowIndex].Cells[1].Value);
                    txtDescription.Text = Convert.ToString(dtgvDetails.Rows[e.RowIndex].Cells[2].Value);
                    txtQty.Text = Convert.ToString(dtgvDetails.Rows[e.RowIndex].Cells[3].Value);
                    if (Convert.ToString(dtgvDetails.Rows[e.RowIndex].Cells[4].Value) == "YES")
                        chkSuspensionSystem.Checked = true;
                    else
                        chkSuspensionSystem.Checked = false;
                    colIndex = e.RowIndex;
                }
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbSystemType.Text != "" && txtNippleFeederType.Text != "" && dtgvDetails.Rows.Count > 0)
                {
                    if (txtTotalNippleFeederAmt.Text != "" && txtTotalCostPerBird.Text != "")
                    {
                        NippleFeederSystemMasterRepository nippleFeederMRepo = new NippleFeederSystemMasterRepository();
                        var nippleFeederData = nippleFeederMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Type.ToLower().Trim() == cmbSystemType.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Id != updateId).FirstOrDefault();
                        if (nippleFeederData != null)
                        {
                            MessageBox.Show("Already Saved this record");
                            this.ActiveControl = txtRefChNo;
                        }
                        else
                        {
                            ProjectGlanceMasterRepository projGMRepo = new ProjectGlanceMasterRepository();
                            var projMData = projGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (projMData != null)
                            {
                                nippleFeederMRepo = new NippleFeederSystemMasterRepository();
                                tblNippleFeederSystemMaster nipplefeederMData = nippleFeederMRepo.GetById(updateId);
                                nipplefeederMData.ReferenceId = projMData.Id;
                                nipplefeederMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                                nipplefeederMData.NippleEzypanQutNo = Convert.ToInt32(txtQutNo.Text);
                                nipplefeederMData.Type = Convert.ToString(cmbSystemType.Text.Trim());
                                if (txtRecommendedLengthOfLines.Text != "")
                                    nipplefeederMData.RecommendedLengthOfLines = Convert.ToDecimal(txtRecommendedLengthOfLines.Text);
                                else
                                    nipplefeederMData.RecommendedLengthOfLines = 0;
                                if (txtRecommendedNumberOfLines.Text != "")
                                    nipplefeederMData.RecommendedNumberOfLines = Convert.ToDecimal(txtRecommendedNumberOfLines.Text);
                                else
                                    nipplefeederMData.RecommendedNumberOfLines = 0;
                                if (txtNumberOfSectionSegementPerLine.Text != "")
                                    nipplefeederMData.NumberOfSectionSegementPerLine = Convert.ToDecimal(txtNumberOfSectionSegementPerLine.Text);
                                else
                                    nipplefeederMData.NumberOfSectionSegementPerLine = 0;
                                if (txtNumberOfSectionSegementPerLine1.Text != "")
                                    nipplefeederMData.NumberOfSectionSegementPerLine1 = Convert.ToDecimal(txtNumberOfSectionSegementPerLine1.Text);
                                else
                                    nipplefeederMData.NumberOfSectionSegementPerLine1 = 0;
                                if (txtNumberOfNipplesFeederPerSegment.Text != "")
                                    nipplefeederMData.NumberOfNipplesFeederPerSegment = Convert.ToDecimal(txtNumberOfNipplesFeederPerSegment.Text);
                                else
                                    nipplefeederMData.NumberOfNipplesFeederPerSegment = 0;
                                if (txtTotalNumberOfNipplesFeeders.Text != "")
                                    nipplefeederMData.TotalNumberOfNipplesFeeders = Convert.ToDecimal(txtTotalNumberOfNipplesFeeders.Text);
                                else
                                    nipplefeederMData.TotalNumberOfNipplesFeeders = 0;
                                if (txtTotalNumberOfNipplesFeeders1.Text != "")
                                    nipplefeederMData.TotalNumberOfNipplesFeeders1 = Convert.ToDecimal(txtTotalNumberOfNipplesFeeders1.Text);
                                else
                                    nipplefeederMData.TotalNumberOfNipplesFeeders1 = 0;
                                if (txtNumberOfBirdsPerNipplesFeeder.Text != "")
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeeder = Convert.ToDecimal(txtNumberOfBirdsPerNipplesFeeder.Text);
                                else
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeeder = 0;
                                if (txtNumberOfBirdsPerNipplesFeederr1.Text != "")
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeederr1 = Convert.ToDecimal(txtNumberOfBirdsPerNipplesFeederr1.Text);
                                else
                                    nipplefeederMData.NumberOfBirdsPerNipplesFeederr1 = 0;
                                if (txtSuspensionAtEveryFTNipplesFeeder.Text != "")
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder = Convert.ToDecimal(txtSuspensionAtEveryFTNipplesFeeder.Text);
                                else
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder = 0;
                                if (txtSuspensionAtEveryFTNipplesFeeder1.Text != "")
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder1 = Convert.ToDecimal(txtSuspensionAtEveryFTNipplesFeeder1.Text);
                                else
                                    nipplefeederMData.SuspensionAtEveryFTNipplesFeeder1 = 0;
                                if (txtNumberOFPressureBreakers.Text != "")
                                    nipplefeederMData.NumberOFPressureBreakers = Convert.ToDecimal(txtNumberOFPressureBreakers.Text);
                                else
                                    nipplefeederMData.NumberOFPressureBreakers = 0;
                                if (chkShokerSystemNeededNipplesFeeder.Checked == true)
                                    nipplefeederMData.ShokerSystemNeededNipplesFeeder = "YES".Trim();
                                else
                                    nipplefeederMData.ShokerSystemNeededNipplesFeeder = "NO".Trim();
                                if (chkControlPanelNeededFeeder.Checked == true)
                                    nipplefeederMData.ControlPanelNeededFeeder = "YES".Trim();
                                else
                                    nipplefeederMData.ControlPanelNeededFeeder = "NO".Trim();
                                nipplefeederMData.NippleFeederType = txtNippleFeederType.Text.Trim();
                                if (chkNippleTray.Checked == true)
                                    nipplefeederMData.NippleTray = "YES".Trim();
                                else
                                    nipplefeederMData.NippleTray = "NO".Trim();
                                if (chkWaterMeter.Checked == true)
                                    nipplefeederMData.WaterMeter = "YES".Trim();
                                else
                                    nipplefeederMData.WaterMeter = "NO".Trim();
                                if (chkDiskFilter.Checked == true)
                                    nipplefeederMData.DiskFilter = "YES".Trim();
                                else
                                    nipplefeederMData.DiskFilter = "NO".Trim();
                                if (chkMedicator.Checked == true)
                                    nipplefeederMData.Medicator = "YES".Trim();
                                else
                                    nipplefeederMData.Medicator = "NO".Trim();
                                if (chkEzyCupDrinker.Checked == true)
                                    nipplefeederMData.EzyCupDrinker = "YES".Trim();
                                else
                                    nipplefeederMData.EzyCupDrinker = "NO".Trim();
                                if (txtTotalSuspension.Text != "")
                                    nipplefeederMData.SuspensionSystemCost = Convert.ToDecimal(txtTotalSuspension.Text);
                                else
                                    nipplefeederMData.SuspensionSystemCost = 0;
                                if (txtTotalNippleFeederAmt.Text != "")
                                    nipplefeederMData.NippleSystemFeederCost = Convert.ToDecimal(txtTotalNippleFeederAmt.Text);
                                else
                                    nipplefeederMData.NippleSystemFeederCost = 0;
                                if (txtTotalCostPerBird.Text != "")
                                    nipplefeederMData.TotalCostPerHouse = Convert.ToDecimal(txtTotalCostPerBird.Text);
                                else
                                    nipplefeederMData.TotalCostPerHouse = 0;
                                if (txtTotalCost.Text != "")
                                    nipplefeederMData.AllTotalCost = Convert.ToDecimal(txtTotalCost.Text);
                                else
                                    nipplefeederMData.AllTotalCost = 0;
                                nipplefeederMData.CompId = CommonMethod.CompId;
                                nipplefeederMData.TransactionYear = CommonMethod.TransactionYear;
                                if (txtHopperKG.Text != "")
                                    nipplefeederMData.HopperKG = Convert.ToDecimal(txtHopperKG.Text);
                                else
                                    nipplefeederMData.HopperKG = 0;
                                if (txtMotor1or3ph.Text != "")
                                    nipplefeederMData.MotorPh = Convert.ToDecimal(txtMotor1or3ph.Text);
                                else
                                    nipplefeederMData.MotorPh = 0;
                                if (txtNipplePercent.Text != "")
                                    nipplefeederMData.NippleFeederPercent = Convert.ToDecimal(txtNipplePercent.Text);
                                else
                                    nipplefeederMData.NippleFeederPercent = 0;
                                if (txtTotalSuspension1.Text != "")
                                    nipplefeederMData.SuspensionSystemCost1 = Convert.ToDecimal(txtTotalSuspension1.Text);
                                else
                                    nipplefeederMData.SuspensionSystemCost1 = 0;
                                if (txtTotalNippleFeederAmt1.Text != "")
                                    nipplefeederMData.NippleSystemFeederCost1 = Convert.ToDecimal(txtTotalNippleFeederAmt1.Text);
                                else
                                    nipplefeederMData.NippleSystemFeederCost1 = 0;
                                if (txtSuspensionPercent.Text != "")
                                    nipplefeederMData.SuspensionPercent = Convert.ToDecimal(txtSuspensionPercent.Text);
                                else
                                    nipplefeederMData.SuspensionPercent = 0;
                                nipplefeederMData.EzypanV1V2 = txtEzypanV2V3.Text.Trim();
                                nippleFeederMRepo.Edit(nipplefeederMData);
                                nippleFeederMRepo.Save();

                                ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData = projDDRepo.GetAll().Where(t => t.MasterId == projMData.Id && t.Description.Trim() == cmbSystemType.Text.Trim()).FirstOrDefault();
                                if (projDData != null)
                                {
                                    projDDRepo = new ProjectGlanceDetailRepository();
                                    tblProjectGlanceDetail projDData1 = projDDRepo.GetById(projDData.Id);
                                    projDData1.InvestBird = Convert.ToDecimal(nipplefeederMData.TotalCostPerHouse);
                                    projDData1.InvestmentPerHouse = Convert.ToDecimal(nipplefeederMData.AllTotalCost);
                                    projDDRepo.Edit(projDData1);
                                    projDDRepo.Save();

                                    try
                                    {
                                        decimal totalGSTPercent = 0, totalInvestAmt = 0, totalSCgstPercent = 0, totalIgstPercent = 0;
                                        decimal netAmt = 0, GSTAmt = 0, totalGstAmt = 0;
                                        projDDRepo = new ProjectGlanceDetailRepository();
                                        totalInvestAmt = Convert.ToDecimal(projDDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId).Sum(t => t.InvestmentPerHouse));
                                        totalIgstPercent = Convert.ToDecimal(projDDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId && t.Id == projDData1.Id).Sum(t => t.IGstPercent));
                                        totalSCgstPercent = Convert.ToDecimal(projDDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId && t.Id == projDData1.Id).Sum(t => t.SGSTCGSTPercent));
                                        totalGSTPercent = totalIgstPercent + totalSCgstPercent;

                                        GSTAmt = Convert.ToDecimal(projDData1.InvestmentPerHouse) * totalGSTPercent / 100;
                                        ProjectGlanceDetailRepository pDRepo = new ProjectGlanceDetailRepository();
                                        List<tblProjectGlanceDetail> pdList = pDRepo.GetAll().Where(t => t.MasterId == projDData1.MasterId).ToList();
                                        if (pdList.Count > 0)
                                        {
                                            foreach (var item in pdList)
                                            {
                                                totalGSTPercent = Convert.ToDecimal(item.IGstPercent) + Convert.ToDecimal(item.SGSTCGSTPercent);
                                                totalGstAmt = totalGstAmt + Convert.ToDecimal(totalGSTPercent * item.InvestmentPerHouse / 100);
                                            }
                                        }
                                        netAmt = totalGstAmt + totalInvestAmt;
                                        ProjectGlanceMasterRepository projAGMRepo = new ProjectGlanceMasterRepository();
                                        tblProjectAtGlanceMaster projAGMData = projAGMRepo.GetById(Convert.ToInt32(projDData1.MasterId));
                                        projAGMData.TotalAmt = totalInvestAmt;
                                        projAGMData.GSTAmt = totalGstAmt;
                                        projAGMData.NetAmt = netAmt;
                                        projAGMRepo.Edit(projAGMData);
                                        projAGMRepo.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                updateId = Convert.ToInt32(nipplefeederMData.Id);
                                if (deleteId.Count > 0)
                                {
                                    for (int x = 0; x < deleteId.Count; x++)
                                    {
                                        NippleFeederSystemDetailRepository nipplefeederDRepo = new NippleFeederSystemDetailRepository();
                                        tblNippleFeederSystemDetail nippleFeederDData = nipplefeederDRepo.GetById(deleteId[x]);
                                        nipplefeederDRepo.Remove(nippleFeederDData);
                                        nipplefeederDRepo.Save();
                                    }
                                }
                                for (int i = 0; i < dtgvDetails.Rows.Count; i++)
                                {
                                    int RawId = Convert.ToInt32(dtgvDetails.Rows[i].Cells[0].Value);
                                    if (RawId != 0)
                                    {
                                        NippleFeederSystemDetailRepository nipplefeederDRepo = new NippleFeederSystemDetailRepository();
                                        tblNippleFeederSystemDetail nippleFeederDData = nipplefeederDRepo.GetById(RawId);
                                        nippleFeederDData.MasterId = updateId;
                                        nippleFeederDData.Quantity = Convert.ToDecimal(dtgvDetails.Rows[i].Cells[3].Value);
                                        nippleFeederDData.Code = Convert.ToString(dtgvDetails.Rows[i].Cells[1].Value);
                                        nippleFeederDData.Description = Convert.ToString(dtgvDetails.Rows[i].Cells[2].Value);
                                        nippleFeederDData.SuspensionSystem = Convert.ToString(dtgvDetails.Rows[i].Cells[4].Value);
                                        nipplefeederDRepo.Edit(nippleFeederDData);
                                        nipplefeederDRepo.Save();
                                        nipplefeederDRepo.Dispose();
                                    }
                                    else
                                    {
                                        NippleFeederSystemDetailRepository nipplefeederDRepo = new NippleFeederSystemDetailRepository();
                                        tblNippleFeederSystemDetail nippleFeederDData = new tblNippleFeederSystemDetail();
                                        nippleFeederDData.MasterId = updateId;
                                        nippleFeederDData.Quantity = Convert.ToDecimal(dtgvDetails.Rows[i].Cells[3].Value);
                                        nippleFeederDData.Code = Convert.ToString(dtgvDetails.Rows[i].Cells[1].Value);
                                        nippleFeederDData.Description = Convert.ToString(dtgvDetails.Rows[i].Cells[2].Value);
                                        nippleFeederDData.SuspensionSystem = Convert.ToString(dtgvDetails.Rows[i].Cells[4].Value);
                                        nipplefeederDRepo.Add(nippleFeederDData);
                                        nipplefeederDRepo.Save();
                                        nipplefeederDRepo.Dispose();
                                    }
                                }
                                MessageBox.Show("Record Update Successfully");
                                clear();
                                nippleFeederMRepo.Dispose();
                            }
                        }
                    }
                    else
                    {
                        if (txtTotalNippleFeederAmt.Text == "")
                        {
                            MessageBox.Show("Enter Total Nipple/Feeder Amount");
                            this.ActiveControl = txtTotalNippleFeederAmt;
                        }
                        else if (txtTotalCostPerBird.Text == "")
                        {
                            MessageBox.Show("Enter Total Cost Per Bird");
                            this.ActiveControl = txtTotalCostPerBird;
                        }
                    }
                }
                else
                {
                    if (txtRefChNo.Text == "")
                    {
                        MessageBox.Show("Enter Refernce Challan No");
                        this.ActiveControl = txtRefChNo;
                    }
                    else if (cmbSystemType.Text == "")
                    {
                        MessageBox.Show("Select System Type");
                        this.ActiveControl = cmbSystemType;
                    }
                    else if (txtNippleFeederType.Text == "")
                    {
                        MessageBox.Show("Select Nipple / Ezypan Feeder System Type");
                        this.ActiveControl = txtNippleFeederType;
                    }
                    else if (dtgvDetails.Rows.Count > 0)
                    {
                        MessageBox.Show("Add at least one description");
                        this.ActiveControl = txtCode;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (updateId != 0)
                {
                    NippleFeederSystemMasterRepository nippleFeederMRepo = new NippleFeederSystemMasterRepository();
                    var nipplefeederData = nippleFeederMRepo.GetAll().Where(t => t.Id == updateId).FirstOrDefault();
                    if (nipplefeederData != null)
                    {
                        DataTable dtDData = db.GetTable("delete from tblNippleFeederSystemDetail where MasterId=" + updateId + "");
                        DataTable dtMData = db.GetTable("delete from tblNippleFeederSystemMaster where Id=" + updateId + "");
                        MessageBox.Show("Record delete Successfully");
                        clear();
                        this.ActiveControl = txtRefChNo;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}
