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
    public partial class frmVentilationAndCoolingSystem : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int colIndex = -1, updateId = 0;
        public decimal TotalPerBird = 0;
        List<int> deleteId = new List<int>();
        public static frmVentilationAndCoolingSystem ventilationQta;

        public frmVentilationAndCoolingSystem()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            cmbSystemType.SelectedIndex = 0;
        }

        public frmVentilationAndCoolingSystem(int UpdateId)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            cmbSystemType.SelectedIndex = 0;
            loadData(UpdateId);
        }

        public void maxproductid()
        {
            try
            {
                VentilationCoolingAndSystemMasterRepository productrepo = new VentilationCoolingAndSystemMasterRepository();
                var mid = Convert.ToInt32(productrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.VentilationQutNo));
                if (mid == null)
                    txtQutNo.Text = "1";
                else
                    txtQutNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                productrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loadData(int UpdateId)
        {
            try
            {
                VentilationCoolingAndSystemMasterRepository ventilationMRepo = new VentilationCoolingAndSystemMasterRepository();
                tblVentilationAndCoolingSystem ventilationMData = ventilationMRepo.GetAll().Where(t => t.Id == UpdateId && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (ventilationMData != null)
                {
                    updateId = ventilationMData.Id;
                    txtQutNo.Text = Convert.ToString(ventilationMData.VentilationQutNo);
                    dtpQutDate.Value = Convert.ToDateTime(ventilationMData.Date);
                    txtRefChNo.Text = Convert.ToString(ventilationMData.tblProjectAtGlanceMaster.RefernceNo);
                    lblCustomerName.Text = Convert.ToString(ventilationMData.tblProjectAtGlanceMaster.CustomerInformation.CustomerName);
                    TotalPerBird = Convert.ToDecimal(ventilationMData.tblProjectAtGlanceMaster.NoOfBirds);
                    cmbSystemType.Text = Convert.ToString(ventilationMData.Type);
                    txtTARGETTEMPF.Text = Convert.ToString(ventilationMData.TARGETTEMPF);
                    txtTARGETTEMPF1.Text = Convert.ToString(ventilationMData.TARGETTEMPF1);
                    txtDIFFFANTPAD.Text = Convert.ToString(ventilationMData.DIFFFANTPAD);
                    txtTOTALHEATLOAD.Text = Convert.ToString(ventilationMData.TOTALHEATLOAD);
                    txtPADSIZEHxWxThid.Text = Convert.ToString(ventilationMData.PADSIZEHxWxThid1);
                    txtPADSIZEHxWxThid1.Text = Convert.ToString(ventilationMData.PADSIZEHxWxThid);
                    txtAirSpeedPossibleinShedFtMi.Text = Convert.ToString(ventilationMData.AirSpeedPossibleinShedFtMi);
                    txtFANSRequiredForHeatRemoval.Text = Convert.ToString(ventilationMData.FANSRequiredForHeatRemoval);
                    txtPadAreaSQFTReuiredFinal.Text = Convert.ToString(ventilationMData.PadAreaSQFTReuiredFinal);
                    txtCoolingPadLengthSuggest.Text = Convert.ToString(ventilationMData.CoolingPadLengthSuggest);
                    txtCoolingPadLengthSuggest1.Text = Convert.ToString(ventilationMData.CoolingPadLengthSuggest1);
                    txtCoolPadDesigned.Text = Convert.ToString(ventilationMData.CoolPadDesigned);
                    txtBackWall.Text = Convert.ToString(ventilationMData.BackWall);
                    txtBackWall1.Text = Convert.ToString(ventilationMData.BackWall1);
                    txtCelcius.Text = Convert.ToString(ventilationMData.Celcius);
                    if (ventilationMData.LightTrap == "YES".Trim())
                        chkLightTrap.Checked = true;
                    else
                        chkLightTrap.Checked = false;
                    txtSingleStory.Text = Convert.ToString(ventilationMData.SingleStory);
                    txtOutMaxTempF.Text = Convert.ToString(ventilationMData.OutMaxTempF);
                    txtMaxBirdWtKg.Text = Convert.ToString(ventilationMData.MaxBirdWtKg);
                    txtInsulationR.Text = Convert.ToString(ventilationMData.InsulationR);
                    txtEffectiveHtBaffle.Text = Convert.ToString(ventilationMData.EffectiveHtBaffle);
                    txtRequiredAirSpeed.Text = Convert.ToString(ventilationMData.RequiredAirSpeed);
                    txtFanRequired.Text = Convert.ToString(ventilationMData.FanRequired);
                    txtFanRequired1.Text = Convert.ToString(ventilationMData.FanRequired1);
                    txtFinalFanQt.Text = Convert.ToString(ventilationMData.FinalFanQt);
                    txtMaxAirFlow.Text = Convert.ToString(ventilationMData.MaxAirFlow);
                    txtAirExchangemsec.Text = Convert.ToString(ventilationMData.AirExchangemsec);
                    txtAirExchangemsec1.Text = Convert.ToString(ventilationMData.AirExchangemsec1);
                    txtAirSpeedmsec.Text = Convert.ToString(ventilationMData.AirSpeedmsec);
                    if (ventilationMData.AirInlets1 == "YES".Trim())
                        chkAirInlets.Checked = true;
                    else
                        chkAirInlets.Checked = false;
                    txtAirInlets.Text = Convert.ToString(ventilationMData.AirInlets);
                    txtMinVentFans.Text = Convert.ToString(ventilationMData.MinVentFans);
                    txtMinVentFans1.Text = Convert.ToString(ventilationMData.MinVentFans1);
                    txtCostOfTotalSystem.Text = Convert.ToString(ventilationMData.CostOfTotalSystem);
                    txtCostPerHouse.Text = Convert.ToString(ventilationMData.CostPerHouse);
                    txtCostPerBird.Text = Convert.ToString(ventilationMData.CostPerBird);
                    if (ventilationMData.CostOfTotalSystem1 == null)
                        txtCostOfTotalSystem1.Text = Convert.ToString(ventilationMData.CostOfTotalSystem);
                    else
                        txtCostOfTotalSystem1.Text = Convert.ToString(ventilationMData.CostOfTotalSystem1);
                    if (ventilationMData.VentPercent == null)
                        txtVentilationPercent.Text = "0";
                    else
                        txtVentilationPercent.Text = Convert.ToString(ventilationMData.VentPercent);
                    VentilationCoolingAndSystemDetailRepository ventDDRepo = new VentilationCoolingAndSystemDetailRepository();
                    List<tblVentilationAndCoolingSystemDetail> ventDDlist = new List<tblVentilationAndCoolingSystemDetail>();
                    ventDDlist = ventDDRepo.GetAll().Where(t => t.MasterId == updateId).ToList();
                    if (ventDDlist.Count > 0)
                    {
                        int i = 0;
                        GvProductInfo.Rows.Clear();
                        foreach (var item in ventDDlist)
                        {
                            GvProductInfo.Rows.Add();
                            GvProductInfo.Rows[i].Cells[0].Value = item.Id;
                            GvProductInfo.Rows[i].Cells[1].Value = item.Code;
                            GvProductInfo.Rows[i].Cells[2].Value = item.Description;
                            GvProductInfo.Rows[i].Cells[3].Value = item.Quantity;
                            GvProductInfo.Rows[i].Cells[4].Value = "";
                            GvProductInfo.Rows[i].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            i++;
                        }
                    }
                    btnsave.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                GvProductInfo.Rows.Clear();
                txtAirExchangemsec.Text = "";
                txtAirExchangemsec1.Text = "";
                txtAirInlets.Text = "";
                chkAirInlets.Checked = false;
                chkLightTrap.Checked = false;
                txtAirSpeedmsec.Text = "";
                txtAirSpeedPossibleinShedFtMi.Text = "";
                txtBackWall.Text = "";
                txtBackWall1.Text = "";
                txtCoolingPadLengthSuggest1.Text = "";
                txtCode.Text = "";
                txtCelcius.Text = "";
                txtCoolingPadLengthSuggest.Text = "";
                txtCoolPadDesigned.Text = "";
                txtCostOfTotalSystem.Text = "0";
                txtCostPerBird.Text = "0";
                txtCostPerHouse.Text = "0";
                txtDescription.Text = "";
                txtDIFFFANTPAD.Text = "";
                txtMinVentFans1.Text = "";
                txtEffectiveHtBaffle.Text = "";
                txtFanRequired.Text = "";
                txtFanRequired1.Text = "";
                txtFANSRequiredForHeatRemoval.Text = "";
                txtFinalFanQt.Text = "";
                txtInsulationR.Text = "";
                txtMaxAirFlow.Text = "";
                txtMaxBirdWtKg.Text = "";
                txtMinVentFans.Text = "";
                txtOutMaxTempF.Text = "";
                txtPadAreaSQFTReuiredFinal.Text = "";
                txtPADSIZEHxWxThid.Text = "";
                txtPADSIZEHxWxThid1.Text = "";
                txtQty.Text = "";
                maxproductid();
                txtRefChNo.Text = "";
                txtRequiredAirSpeed.Text = "";
                txtSingleStory.Text = "";
                txtTARGETTEMPF.Text = "";
                txtTARGETTEMPF1.Text = "";
                lblCustomerName.Text = "";
                txtTOTALHEATLOAD.Text = "";
                dtpQutDate.Value = DateTime.Now.Date;
                btnUpdate.Enabled = false;
                btnsave.Enabled = true;
                txtVentilationPercent.Text = "0";
                txtVentilationPercentAmt.Text = "";
                txtCostOfTotalSystem1.Text = "0";
                txtCostOfTotalSystem.Text = "0";
                btnDelete.Enabled = false;
                TotalPerBird = 0;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
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

        public void calNetAmt()
        {
            try
            {
                if (txtCostOfTotalSystem.Text != "")
                {
                    decimal nippleTotalNippleFeederAmt = 0, totalAmt = 0, calPerBird = 0, VentInPer = 0, VenttoatlAmt = 0, VentInPerAmt = 0;
                    nippleTotalNippleFeederAmt = Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                    VentInPer = Convert.ToDecimal(txtVentilationPercent.Text);
                    VenttoatlAmt = nippleTotalNippleFeederAmt + (nippleTotalNippleFeederAmt * VentInPer / 100);
                    VentInPerAmt = Convert.ToDecimal(nippleTotalNippleFeederAmt * VentInPer / 100);
                    txtVentilationPercentAmt.Text = Convert.ToString(Math.Round(VentInPerAmt, 2, MidpointRounding.AwayFromZero));
                    txtCostOfTotalSystem.Text = Convert.ToString(Math.Round(VenttoatlAmt, 2, MidpointRounding.AwayFromZero));
                    txtCostPerHouse.Text = Convert.ToString(Math.Round(VenttoatlAmt, 2, MidpointRounding.AwayFromZero));
                    totalAmt = VenttoatlAmt;
                    try
                    {
                        calPerBird = totalAmt / TotalPerBird;
                    }
                    catch (Exception)
                    {
                        calPerBird = 0;
                    }
                    txtCostPerBird.Text = Convert.ToString(Math.Round(calPerBird, 2, MidpointRounding.AwayFromZero));
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
                        for (int m = 0; m < GvProductInfo.Rows.Count; m++)
                        {
                            if (txtCode.Text == Convert.ToString(GvProductInfo.Rows[m].Cells[1].Value))
                            {
                                MessageBox.Show("Already Exists");
                                this.ActiveControl = txtCode;
                                goto a;
                            }
                        }
                    }
                    else
                        goto b;
                b:
                    {
                        if (colIndex == -1)
                        {
                            GvProductInfo.Rows.Add();
                            int i = GvProductInfo.RowCount;
                            GvProductInfo.Rows[i - 1].Cells[1].Value = txtCode.Text;
                            GvProductInfo.Rows[i - 1].Cells[2].Value = txtDescription.Text;
                            GvProductInfo.Rows[i - 1].Cells[4].Value = "";
                            if (txtQty.Text == "")
                                GvProductInfo.Rows[i - 1].Cells[3].Value = "0";
                            else
                                GvProductInfo.Rows[i - 1].Cells[3].Value = txtQty.Text;
                            GvProductInfo.Rows[i - 1].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            txtCode.Text = "";
                            txtDescription.Text = "";
                            txtQty.Text = "";
                            this.ActiveControl = txtCode;
                            colIndex = -1;
                        }
                        else
                        {
                            GvProductInfo.Rows[colIndex].Cells[1].Value = txtCode.Text;
                            GvProductInfo.Rows[colIndex].Cells[2].Value = txtDescription.Text;
                            GvProductInfo.Rows[colIndex].Cells[4].Value = "";
                            if (txtQty.Text == "")
                                GvProductInfo.Rows[colIndex].Cells[3].Value = "0";
                            else
                                GvProductInfo.Rows[colIndex].Cells[3].Value = txtQty.Text;
                            GvProductInfo.Rows[colIndex].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            txtCode.Text = "";
                            txtDescription.Text = "";
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbSystemType.Text != "" && GvProductInfo.Rows.Count > 0)
                {
                    if (txtCostOfTotalSystem.Text != "" && txtCostPerHouse.Text != "" && txtCostPerBird.Text != "")
                    {
                        VentilationCoolingAndSystemMasterRepository venMRepo = new VentilationCoolingAndSystemMasterRepository();
                        var venSMdata = venMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Type == cmbSystemType.Text).FirstOrDefault();
                        if (venSMdata != null)
                        {
                            MessageBox.Show("Already Save This Record");
                            this.ActiveControl = txtRefChNo;
                        }
                        else
                        {
                            ProjectGlanceMasterRepository projGMRepo = new ProjectGlanceMasterRepository();
                            var projMData = projGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (projMData != null)
                            {
                                tblVentilationAndCoolingSystem venMData = new tblVentilationAndCoolingSystem();
                                venMRepo = new VentilationCoolingAndSystemMasterRepository();
                                venMData.VentilationQutNo = Convert.ToInt32(txtQutNo.Text);
                                venMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                                venMData.ReferenceId = Convert.ToInt32(projMData.Id);
                                venMData.Type = cmbSystemType.Text.Trim();
                                if (txtTARGETTEMPF.Text != "")
                                    venMData.TARGETTEMPF = Convert.ToDecimal(txtTARGETTEMPF.Text);
                                else
                                    venMData.TARGETTEMPF = 0;
                                if (txtTARGETTEMPF1.Text != "")
                                    venMData.TARGETTEMPF1 = Convert.ToDecimal(txtTARGETTEMPF1.Text);
                                else
                                    venMData.TARGETTEMPF1 = 0;
                                if (txtDIFFFANTPAD.Text != "")
                                    venMData.DIFFFANTPAD = Convert.ToDecimal(txtDIFFFANTPAD.Text);
                                else
                                    venMData.DIFFFANTPAD = 0;
                                if (txtTOTALHEATLOAD.Text != "")
                                    venMData.TOTALHEATLOAD = Convert.ToDecimal(txtTOTALHEATLOAD.Text);
                                else
                                    venMData.TOTALHEATLOAD = 0;
                                if (txtPADSIZEHxWxThid1.Text != "")
                                    venMData.PADSIZEHxWxThid = Convert.ToString(txtPADSIZEHxWxThid1.Text.Trim());
                                else
                                    venMData.PADSIZEHxWxThid = "";
                                if (txtPADSIZEHxWxThid.Text != "")
                                    venMData.PADSIZEHxWxThid1 = Convert.ToString(txtPADSIZEHxWxThid.Text);
                                else
                                    venMData.PADSIZEHxWxThid1 = "";
                                if (txtAirSpeedPossibleinShedFtMi.Text != "")
                                    venMData.AirSpeedPossibleinShedFtMi = Convert.ToDecimal(txtAirSpeedPossibleinShedFtMi.Text);
                                else
                                    venMData.AirSpeedPossibleinShedFtMi = 0;
                                if (txtFANSRequiredForHeatRemoval.Text != "")
                                    venMData.FANSRequiredForHeatRemoval = Convert.ToDecimal(txtFANSRequiredForHeatRemoval.Text);
                                else
                                    venMData.FANSRequiredForHeatRemoval = 0;
                                if (txtPadAreaSQFTReuiredFinal.Text != "")
                                    venMData.PadAreaSQFTReuiredFinal = Convert.ToDecimal(txtPadAreaSQFTReuiredFinal.Text);
                                else
                                    venMData.PadAreaSQFTReuiredFinal = 0;
                                if (txtCelcius.Text != "")
                                    venMData.Celcius = Convert.ToDecimal(txtCelcius.Text);
                                else
                                    venMData.Celcius = 0;
                                if (txtOutMaxTempF.Text == "")
                                    venMData.OutMaxTempF = 0;
                                else
                                    venMData.OutMaxTempF = Convert.ToDecimal(txtOutMaxTempF.Text);
                                if (txtCoolPadDesigned.Text != "")
                                    venMData.CoolPadDesigned = Convert.ToDecimal(txtCoolPadDesigned.Text);
                                else
                                    venMData.CoolPadDesigned = 0;
                                if (txtCoolingPadLengthSuggest.Text != "")
                                    venMData.CoolingPadLengthSuggest = Convert.ToDecimal(txtCoolingPadLengthSuggest.Text);
                                else
                                    venMData.CoolingPadLengthSuggest = 0;
                                if (txtCoolingPadLengthSuggest1.Text != "")
                                    venMData.CoolingPadLengthSuggest1 = Convert.ToDecimal(txtCoolingPadLengthSuggest1.Text);
                                else
                                    venMData.CoolingPadLengthSuggest1 = 0;
                                if (txtBackWall.Text != "")
                                    venMData.BackWall = Convert.ToDecimal(txtBackWall.Text);
                                else
                                    venMData.BackWall = 0;
                                if (txtBackWall1.Text != "")
                                    venMData.BackWall1 = Convert.ToDecimal(txtBackWall1.Text);
                                else
                                    venMData.BackWall1 = 0;
                                if (chkLightTrap.Checked == true)
                                    venMData.LightTrap = "YES".Trim();
                                else
                                    venMData.LightTrap = "NO".Trim();
                                if (txtSingleStory.Text != "")
                                    venMData.SingleStory = Convert.ToDecimal(txtSingleStory.Text);
                                else
                                    venMData.SingleStory = 0;
                                if (txtMaxBirdWtKg.Text != "")
                                    venMData.MaxBirdWtKg = Convert.ToDecimal(txtMaxBirdWtKg.Text);
                                else
                                    venMData.MaxBirdWtKg = 0;
                                if (txtInsulationR.Text != "")
                                    venMData.InsulationR = Convert.ToDecimal(txtInsulationR.Text);
                                else
                                    venMData.InsulationR = 0;
                                if (txtEffectiveHtBaffle.Text != "")
                                    venMData.EffectiveHtBaffle = Convert.ToDecimal(txtEffectiveHtBaffle.Text);
                                else
                                    venMData.EffectiveHtBaffle = 0;
                                if (txtRequiredAirSpeed.Text != "")
                                    venMData.RequiredAirSpeed = Convert.ToDecimal(txtRequiredAirSpeed.Text);
                                else
                                    venMData.RequiredAirSpeed = 0;
                                if (txtFanRequired.Text != "")
                                    venMData.FanRequired = Convert.ToDecimal(txtFanRequired.Text);
                                else
                                    venMData.FanRequired = 0;
                                if (txtFanRequired1.Text != "")
                                    venMData.FanRequired1 = Convert.ToDecimal(txtFanRequired1.Text);
                                else
                                    venMData.FanRequired1 = 0;
                                if (txtFinalFanQt.Text != "")
                                    venMData.FinalFanQt = Convert.ToDecimal(txtFinalFanQt.Text);
                                else
                                    venMData.FinalFanQt = 0;
                                if (txtMaxAirFlow.Text != "")
                                    venMData.MaxAirFlow = Convert.ToDecimal(txtMaxAirFlow.Text);
                                else
                                    venMData.MaxAirFlow = 0;
                                if (txtAirExchangemsec.Text != "")
                                    venMData.AirExchangemsec = Convert.ToDecimal(txtAirExchangemsec.Text);
                                else
                                    venMData.AirExchangemsec = 0;
                                if (txtAirExchangemsec1.Text != "")
                                    venMData.AirExchangemsec1 = Convert.ToDecimal(txtAirExchangemsec1.Text);
                                else
                                    venMData.AirExchangemsec1 = 0;
                                if (txtAirSpeedmsec.Text != "")
                                    venMData.AirSpeedmsec = Convert.ToDecimal(txtAirSpeedmsec.Text);
                                else
                                    venMData.AirSpeedmsec = 0;
                                if (chkAirInlets.Checked == true)
                                    venMData.AirInlets1 = "YES".Trim();
                                else
                                    venMData.AirInlets1 = "NO".Trim();
                                if (txtAirInlets.Text != "")
                                    venMData.AirInlets = Convert.ToDecimal(txtAirInlets.Text);
                                else
                                    venMData.AirInlets = 0;
                                if (txtMinVentFans.Text != "")
                                    venMData.MinVentFans = Convert.ToDecimal(txtMinVentFans.Text);
                                else
                                    venMData.MinVentFans = 0;
                                if (txtMinVentFans1.Text != "")
                                    venMData.MinVentFans1 = Convert.ToDecimal(txtMinVentFans1.Text);
                                else
                                    venMData.MinVentFans1 = 0;
                                if (txtCostOfTotalSystem.Text != "")
                                    venMData.CostOfTotalSystem = Convert.ToDecimal(txtCostOfTotalSystem.Text);
                                else
                                    venMData.CostOfTotalSystem = 0;
                                if (txtCostPerHouse.Text != "")
                                    venMData.CostPerHouse = Convert.ToDecimal(txtCostPerHouse.Text);
                                else
                                    venMData.CostPerHouse = 0;
                                if (txtCostPerBird.Text != "")
                                    venMData.CostPerBird = Convert.ToDecimal(txtCostPerBird.Text);
                                else
                                    venMData.CostPerBird = 0;
                                if (txtCostOfTotalSystem1.Text != "")
                                    venMData.CostOfTotalSystem1 = Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                                else
                                    venMData.CostOfTotalSystem1 = 0;
                                if (txtVentilationPercent.Text != "")
                                    venMData.VentPercent = Convert.ToDecimal(txtVentilationPercent.Text);
                                else
                                    venMData.VentPercent = 0;
                                venMData.CompId = CommonMethod.CompId;
                                venMData.TransactionYear = CommonMethod.TransactionYear;
                                venMRepo.Add(venMData);
                                venMRepo.Save();

                                int MasterId = Convert.ToInt32(venMData.Id);
                                updateId = Convert.ToInt32(venMData.Id);
                                ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData = projDDRepo.GetAll().Where(t => t.MasterId == projMData.Id && t.Description.Trim() == "COOLING & VENT SYSTEM").FirstOrDefault();
                                if (projDData != null)
                                {
                                    projDDRepo = new ProjectGlanceDetailRepository();
                                    tblProjectGlanceDetail projDData1 = projDDRepo.GetById(projDData.Id);
                                    projDData1.InvestBird = Convert.ToDecimal(venMData.CostPerBird);
                                    projDData1.InvestmentPerHouse = Convert.ToDecimal(venMData.CostOfTotalSystem);
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

                                for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                                {
                                    VentilationCoolingAndSystemDetailRepository ventilationDRepo = new VentilationCoolingAndSystemDetailRepository();
                                    tblVentilationAndCoolingSystemDetail ventilationDData = new tblVentilationAndCoolingSystemDetail();
                                    ventilationDData.MasterId = MasterId;
                                    ventilationDData.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells[3].Value);
                                    ventilationDData.Code = Convert.ToString(GvProductInfo.Rows[i].Cells[1].Value);
                                    ventilationDData.Description = Convert.ToString(GvProductInfo.Rows[i].Cells[2].Value);
                                    ventilationDRepo.Add(ventilationDData);
                                    ventilationDRepo.Save();
                                    ventilationDRepo.Dispose();
                                }
                                MessageBox.Show("Record Save Successfully");
                                clear();
                            }
                        }
                    }
                    else
                    {
                        if (txtCostOfTotalSystem.Text == "")
                        {
                            MessageBox.Show("Enter Cost Of Total System");
                            this.ActiveControl = txtCostOfTotalSystem;
                        }
                        else if (txtCostPerHouse.Text == "")
                        {
                            MessageBox.Show("Enter Cost Of Per House System");
                            this.ActiveControl = txtCostPerHouse;
                        }
                        else if (txtCostPerBird.Text == "")
                        {
                            MessageBox.Show("Enter Cost Of Per Bird");
                            this.ActiveControl = txtCostPerBird;
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
                    else if (GvProductInfo.Rows.Count > 0)
                    {
                        MessageBox.Show("Add at least one description");
                        this.ActiveControl = txtCode;
                    }
                }
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

        private void btndeliverychallenlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                ventilationQta = this;
                frmVentilationAndCoolingSystem pq = new frmVentilationAndCoolingSystem();
                pq.Visible = false;
                VentilationAndCoolingSystemList plist = new VentilationAndCoolingSystemList();
                plist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void frmVentilationAndCoolingSystem_Load(object sender, EventArgs e)
        {
            try
            {
                loadRefernceNo();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptEnvironmentCurtainSystem rpt = new RptEnvironmentCurtainSystem(updateId);
                rpt.ShowDialog();
                rpt.Dispose();
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
                    var projeDData = projDDRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Description == "COOLING & VENT SYSTEM").FirstOrDefault();
                    if (projeDData != null)
                    {
                        VentilationCoolingAndSystemMasterRepository ventMRepo = new VentilationCoolingAndSystemMasterRepository();
                        var venMData = ventMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Type == cmbSystemType.Text).FirstOrDefault();
                        if (venMData != null)
                        {
                            MessageBox.Show("This type of quotation save already");
                            this.ActiveControl = cmbSystemType;
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

        private void txtCostOfTotalSystem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCostOfTotalSystem.Text != "")
                    calNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtVentilationPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtVentilationPercent.Text != "")
                    calNetAmt();
                else
                {
                    txtVentilationPercent.Text = "0";
                    this.ActiveControl = txtVentilationPercent;
                }
            }
            catch (Exception)
            { }
        }

        private void txtVentilationPercentAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtVentilationPercentAmt.Text != "")
                {
                    if (Convert.ToDecimal(txtVentilationPercentAmt.Text) <= Convert.ToDecimal(txtCostOfTotalSystem1.Text))
                    {
                        decimal discper = 0;
                        discper = Convert.ToDecimal(txtVentilationPercentAmt.Text) * 100 / Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                        txtVentilationPercent.Text = Convert.ToString(Math.Round(discper, 5, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Ventilation Amount.", "Warning");
                        txtCostOfTotalSystem1.Text = "0";
                        this.ActiveControl = txtCostOfTotalSystem1;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtCostOfTotalSystem1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCostOfTotalSystem1.Text != "")
                    calNetAmt();
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbSystemType.Text != "" && GvProductInfo.Rows.Count > 0)
                {
                    if (txtCostOfTotalSystem.Text != "" && txtCostPerHouse.Text != "" && txtCostPerBird.Text != "")
                    {
                        VentilationCoolingAndSystemMasterRepository venMRepo = new VentilationCoolingAndSystemMasterRepository();
                        var venSMdata = venMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Type == cmbSystemType.Text && t.Id != updateId).FirstOrDefault();
                        if (venSMdata != null)
                        {
                            MessageBox.Show("Already Save This Record");
                            this.ActiveControl = txtRefChNo;
                        }
                        else
                        {
                            ProjectGlanceMasterRepository projGMRepo = new ProjectGlanceMasterRepository();
                            var projMData = projGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (projMData != null)
                            {
                                venMRepo = new VentilationCoolingAndSystemMasterRepository();
                                tblVentilationAndCoolingSystem venMData = venMRepo.GetById(updateId);
                                venMData.VentilationQutNo = Convert.ToInt32(txtQutNo.Text);
                                venMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                                venMData.ReferenceId = Convert.ToInt32(projMData.Id);
                                venMData.Type = cmbSystemType.Text.Trim();
                                if (txtTARGETTEMPF.Text != "")
                                    venMData.TARGETTEMPF = Convert.ToDecimal(txtTARGETTEMPF.Text);
                                else
                                    venMData.TARGETTEMPF = 0;
                                if (txtTARGETTEMPF1.Text != "")
                                    venMData.TARGETTEMPF1 = Convert.ToDecimal(txtTARGETTEMPF1.Text);
                                else
                                    venMData.TARGETTEMPF1 = 0;
                                if (txtDIFFFANTPAD.Text != "")
                                    venMData.DIFFFANTPAD = Convert.ToDecimal(txtDIFFFANTPAD.Text);
                                else
                                    venMData.DIFFFANTPAD = 0;
                                if (txtTOTALHEATLOAD.Text != "")
                                    venMData.TOTALHEATLOAD = Convert.ToDecimal(txtTOTALHEATLOAD.Text);
                                else
                                    venMData.TOTALHEATLOAD = 0;
                                if (txtPADSIZEHxWxThid1.Text != "")
                                    venMData.PADSIZEHxWxThid = Convert.ToString(txtPADSIZEHxWxThid1.Text.Trim());
                                else
                                    venMData.PADSIZEHxWxThid = "";
                                if (txtPADSIZEHxWxThid.Text != "")
                                    venMData.PADSIZEHxWxThid1 = Convert.ToString(txtPADSIZEHxWxThid.Text);
                                else
                                    venMData.PADSIZEHxWxThid1 = "";
                                if (txtAirSpeedPossibleinShedFtMi.Text != "")
                                    venMData.AirSpeedPossibleinShedFtMi = Convert.ToDecimal(txtAirSpeedPossibleinShedFtMi.Text);
                                else
                                    venMData.AirSpeedPossibleinShedFtMi = 0;
                                if (txtFANSRequiredForHeatRemoval.Text != "")
                                    venMData.FANSRequiredForHeatRemoval = Convert.ToDecimal(txtFANSRequiredForHeatRemoval.Text);
                                else
                                    venMData.FANSRequiredForHeatRemoval = 0;
                                if (txtPadAreaSQFTReuiredFinal.Text != "")
                                    venMData.PadAreaSQFTReuiredFinal = Convert.ToDecimal(txtPadAreaSQFTReuiredFinal.Text);
                                else
                                    venMData.PadAreaSQFTReuiredFinal = 0;
                                if (txtCoolingPadLengthSuggest.Text != "")
                                    venMData.CoolingPadLengthSuggest = Convert.ToDecimal(txtCoolingPadLengthSuggest.Text);
                                else
                                    venMData.CoolingPadLengthSuggest = 0;
                                if (txtCoolingPadLengthSuggest1.Text != "")
                                    venMData.CoolingPadLengthSuggest1 = Convert.ToDecimal(txtCoolingPadLengthSuggest1.Text);
                                else
                                    venMData.CoolingPadLengthSuggest1 = 0;
                                if (txtBackWall.Text != "")
                                    venMData.BackWall = Convert.ToDecimal(txtBackWall.Text);
                                else
                                    venMData.BackWall = 0;
                                if (txtCoolPadDesigned.Text != "")
                                    venMData.CoolPadDesigned = Convert.ToDecimal(txtCoolPadDesigned.Text);
                                else
                                    venMData.CoolPadDesigned = 0;
                                if (txtBackWall1.Text != "")
                                    venMData.BackWall1 = Convert.ToDecimal(txtBackWall1.Text);
                                else
                                    venMData.BackWall1 = 0;
                                if (txtCelcius.Text != "")
                                    venMData.Celcius = Convert.ToDecimal(txtCelcius.Text);
                                else
                                    venMData.Celcius = 0;
                                if (chkLightTrap.Checked == true)
                                    venMData.LightTrap = "YES".Trim();
                                else
                                    venMData.LightTrap = "NO".Trim();
                                if (txtSingleStory.Text != "")
                                    venMData.SingleStory = Convert.ToDecimal(txtSingleStory.Text);
                                else
                                    venMData.SingleStory = 0;
                                if (txtOutMaxTempF.Text == "")
                                    venMData.OutMaxTempF = 0;
                                else
                                    venMData.OutMaxTempF = Convert.ToDecimal(txtOutMaxTempF.Text);
                                if (txtMaxBirdWtKg.Text != "")
                                    venMData.MaxBirdWtKg = Convert.ToDecimal(txtMaxBirdWtKg.Text);
                                else
                                    venMData.MaxBirdWtKg = 0;
                                if (txtInsulationR.Text != "")
                                    venMData.InsulationR = Convert.ToDecimal(txtInsulationR.Text);
                                else
                                    venMData.InsulationR = 0;
                                if (txtEffectiveHtBaffle.Text != "")
                                    venMData.EffectiveHtBaffle = Convert.ToDecimal(txtEffectiveHtBaffle.Text);
                                else
                                    venMData.EffectiveHtBaffle = 0;
                                if (txtRequiredAirSpeed.Text != "")
                                    venMData.RequiredAirSpeed = Convert.ToDecimal(txtRequiredAirSpeed.Text);
                                else
                                    venMData.RequiredAirSpeed = 0;
                                if (txtFanRequired.Text != "")
                                    venMData.FanRequired = Convert.ToDecimal(txtFanRequired.Text);
                                else
                                    venMData.FanRequired = 0;
                                if (txtFanRequired1.Text != "")
                                    venMData.FanRequired1 = Convert.ToDecimal(txtFanRequired1.Text);
                                else
                                    venMData.FanRequired1 = 0;
                                if (txtFinalFanQt.Text != "")
                                    venMData.FinalFanQt = Convert.ToDecimal(txtFinalFanQt.Text);
                                else
                                    venMData.FinalFanQt = 0;
                                if (txtMaxAirFlow.Text != "")
                                    venMData.MaxAirFlow = Convert.ToDecimal(txtMaxAirFlow.Text);
                                else
                                    venMData.MaxAirFlow = 0;
                                if (txtAirExchangemsec.Text != "")
                                    venMData.AirExchangemsec = Convert.ToDecimal(txtAirExchangemsec.Text);
                                else
                                    venMData.AirExchangemsec = 0;
                                if (txtAirExchangemsec1.Text != "")
                                    venMData.AirExchangemsec1 = Convert.ToDecimal(txtAirExchangemsec1.Text);
                                else
                                    venMData.AirExchangemsec1 = 0;
                                if (txtAirSpeedmsec.Text != "")
                                    venMData.AirSpeedmsec = Convert.ToDecimal(txtAirSpeedmsec.Text);
                                else
                                    venMData.AirSpeedmsec = 0;
                                if (chkAirInlets.Checked == true)
                                    venMData.AirInlets1 = "YES".Trim();
                                else
                                    venMData.AirInlets1 = "NO".Trim();
                                if (txtAirInlets.Text != "")
                                    venMData.AirInlets = Convert.ToDecimal(txtAirInlets.Text);
                                else
                                    venMData.AirInlets = 0;
                                if (txtMinVentFans.Text != "")
                                    venMData.MinVentFans = Convert.ToDecimal(txtMinVentFans.Text);
                                else
                                    venMData.MinVentFans = 0;
                                if (txtCostOfTotalSystem.Text != "")
                                    venMData.CostOfTotalSystem = Convert.ToDecimal(txtCostOfTotalSystem.Text);
                                else
                                    venMData.CostOfTotalSystem = 0;
                                if (txtCostPerHouse.Text != "")
                                    venMData.CostPerHouse = Convert.ToDecimal(txtCostPerHouse.Text);
                                else
                                    venMData.CostPerHouse = 0;
                                if (txtCostPerBird.Text != "")
                                    venMData.CostPerBird = Convert.ToDecimal(txtCostPerBird.Text);
                                else
                                    venMData.CostPerBird = 0;
                                if (txtMinVentFans1.Text != "")
                                    venMData.MinVentFans1 = Convert.ToDecimal(txtMinVentFans1.Text);
                                else
                                    venMData.MinVentFans1 = 0;
                                if (txtCostOfTotalSystem1.Text != "")
                                    venMData.CostOfTotalSystem1 = Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                                else
                                    venMData.CostOfTotalSystem1 = 0;
                                if (txtVentilationPercent.Text != "")
                                    venMData.VentPercent = Convert.ToDecimal(txtVentilationPercent.Text);
                                else
                                    venMData.VentPercent = 0;
                                venMData.CompId = CommonMethod.CompId;
                                venMData.TransactionYear = CommonMethod.TransactionYear;
                                venMRepo.Edit(venMData);
                                venMRepo.Save();

                                int MasterId = Convert.ToInt32(venMData.Id);
                                ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData = projDDRepo.GetAll().Where(t => t.MasterId == projMData.Id && t.Description.Trim() == "COOLING & VENT SYSTEM").FirstOrDefault();
                                if (projDData != null)
                                {
                                    projDDRepo = new ProjectGlanceDetailRepository();
                                    tblProjectGlanceDetail projDData1 = projDDRepo.GetById(projDData.Id);
                                    projDData1.InvestBird = Convert.ToDecimal(venMData.CostPerBird);
                                    projDData1.InvestmentPerHouse = Convert.ToDecimal(venMData.CostOfTotalSystem);
                                    projDDRepo.Edit(projDData1);
                                    projDDRepo.Save();

                                    try
                                    {
                                        decimal totalGSTPercent = 0, totalInvestAmt = 0, totalSCgstPercent = 0, totalIgstPercent = 0, netAmt = 0, GSTAmt = 0, totalGstAmt = 0;
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
                                if (deleteId.Count > 0)
                                {
                                    for (int x = 0; x < deleteId.Count; x++)
                                    {
                                        VentilationCoolingAndSystemDetailRepository ventilationDRepo = new VentilationCoolingAndSystemDetailRepository();
                                        tblVentilationAndCoolingSystemDetail ventilationDData = ventilationDRepo.GetById(deleteId[x]);
                                        ventilationDRepo.Remove(ventilationDData);
                                        ventilationDRepo.Save();
                                    }
                                }
                                for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                                {
                                    int RowId = Convert.ToInt32(GvProductInfo.Rows[i].Cells[0].Value);
                                    if (RowId != 0)
                                    {
                                        VentilationCoolingAndSystemDetailRepository ventilationDRepo = new VentilationCoolingAndSystemDetailRepository();
                                        tblVentilationAndCoolingSystemDetail ventilationDData = ventilationDRepo.GetById(RowId);
                                        ventilationDData.MasterId = MasterId;
                                        ventilationDData.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells[3].Value);
                                        ventilationDData.Code = Convert.ToString(GvProductInfo.Rows[i].Cells[1].Value);
                                        ventilationDData.Description = Convert.ToString(GvProductInfo.Rows[i].Cells[2].Value);

                                        ventilationDRepo.Edit(ventilationDData);
                                        ventilationDRepo.Save();
                                        ventilationDRepo.Dispose();
                                    }
                                    else
                                    {
                                        VentilationCoolingAndSystemDetailRepository ventilationDRepo = new VentilationCoolingAndSystemDetailRepository();
                                        tblVentilationAndCoolingSystemDetail ventilationDData = new tblVentilationAndCoolingSystemDetail();
                                        ventilationDData.MasterId = MasterId;
                                        ventilationDData.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells[3].Value);
                                        ventilationDData.Code = Convert.ToString(GvProductInfo.Rows[i].Cells[1].Value);
                                        ventilationDData.Description = Convert.ToString(GvProductInfo.Rows[i].Cells[2].Value);

                                        ventilationDRepo.Add(ventilationDData);
                                        ventilationDRepo.Save();
                                        ventilationDRepo.Dispose();
                                    }
                                }
                                MessageBox.Show("Record Update Successfully");
                                clear();
                            }
                        }
                    }
                    else
                    {
                        if (txtCostOfTotalSystem.Text == "")
                        {
                            MessageBox.Show("Enter Cost Of Total System");
                            this.ActiveControl = txtCostOfTotalSystem;
                        }
                        else if (txtCostPerHouse.Text == "")
                        {
                            MessageBox.Show("Enter Cost Of Per House System");
                            this.ActiveControl = txtCostPerHouse;
                        }
                        else if (txtCostPerBird.Text == "")
                        {
                            MessageBox.Show("Enter Cost Of Per Bird");
                            this.ActiveControl = txtCostPerBird;
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
                    else if (GvProductInfo.Rows.Count > 0)
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
                    VentilationCoolingAndSystemMasterRepository nippleFeederMRepo = new VentilationCoolingAndSystemMasterRepository();
                    var nipplefeederData = nippleFeederMRepo.GetAll().Where(t => t.Id == updateId).FirstOrDefault();
                    if (nipplefeederData != null)
                    {
                        DataTable dtDData = db.GetTable("delete from tblVentilationAndCoolingSystemDetail where MasterId=" + updateId + "");
                        DataTable dtMData = db.GetTable("delete from tblVentilationAndCoolingSystem where Id=" + updateId + "");
                        MessageBox.Show("Record delete Successfully");
                        clear();
                        this.ActiveControl = txtRefChNo;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (GvProductInfo.Rows.Count > 0 && btnDelete.Enabled == true)
                    {
                        if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells[0].Value) != 0)
                            deleteId.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells[0].Value));
                        GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                    }
                    else if (btnsave.Enabled == true)
                        GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                    else
                        MessageBox.Show("This Quotation has only 1 product." + Environment.NewLine + "You have to delete full Order.", "Warning");
                }
                else
                {
                    txtCode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells[1].Value);
                    txtDescription.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells[2].Value);
                    txtQty.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells[3].Value);
                    colIndex = e.RowIndex;
                }
            }
            catch (Exception)
            { }
        }
    }
}