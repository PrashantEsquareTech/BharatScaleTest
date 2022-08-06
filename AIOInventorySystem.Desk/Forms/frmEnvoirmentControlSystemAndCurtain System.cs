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
    public partial class frmEnvoirmentControlSystemAndCurtain_System : Form
    {
        DbClass db = new DbClass();
        public int colIndex = -1, updateId = 0;
        public decimal TotalPerBird = 0;
        List<int> deleteId = new List<int>();
        public static frmEnvoirmentControlSystemAndCurtain_System EnvAndCur;
        CommonMethod cm = new CommonMethod();

        public frmEnvoirmentControlSystemAndCurtain_System()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            cmbSystemType.SelectedIndex = 0;
        }

        public frmEnvoirmentControlSystemAndCurtain_System(int UpdateId)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            cmbSystemType.SelectedIndex = 0;
            loadData(UpdateId);
        }

        public void loadData(int UpdateId)
        {
            try
            {
                EnvironementAndCurtainCoolpadSystemMasterRepository envCurMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                var EnvCurMData = envCurMRepo.GetAll().Where(t => t.Id == UpdateId && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (EnvCurMData != null)
                {
                    updateId = EnvCurMData.Id;
                    txtQutNo.Text = Convert.ToString(EnvCurMData.VentilationQutNo);
                    dtpQutDate.Value = Convert.ToDateTime(EnvCurMData.Date);
                    txtRefChNo.Text = Convert.ToString(EnvCurMData.tblProjectAtGlanceMaster.RefernceNo);
                    lblCustomerName.Text = Convert.ToString(EnvCurMData.tblProjectAtGlanceMaster.CustomerInformation.CustomerName);
                    TotalPerBird = Convert.ToDecimal(EnvCurMData.tblProjectAtGlanceMaster.NoOfBirds);
                    cmbSystemType.Text = Convert.ToString(EnvCurMData.Type);
                    txtABCDSmart3g.Text = Convert.ToString(EnvCurMData.ABCDSmart3g);
                    txtPanelModelA6.Text = Convert.ToString(EnvCurMData.PanelModelA6);
                    txtNoOfHuminiditySensorRHS.Text = Convert.ToString(EnvCurMData.NoOfHuminiditySensorRHS);
                    txtCCommunicationMuxDCommunicationNNo.Text = Convert.ToString(EnvCurMData.CCommunicationMuxDCommunicationNNo);
                    txtCablesAndWiringLightsYESNO.Text = Convert.ToString(EnvCurMData.CablesAndWiringLightsYESNO);
                    txtLengthOfCurtainSideA1.Text = Convert.ToString(EnvCurMData.LengthOfCurtainSideA1);
                    txtSideA2.Text = Convert.ToString(EnvCurMData.SideA2);
                    ttxLengthOfCurtainOnOtherSideB1.Text = Convert.ToString(EnvCurMData.LengthOfCurtainOnOtherSideB1);
                    txtOnOtherSideB2.Text = Convert.ToString(EnvCurMData.OnOtherSideB2);
                    txtLengthOfCurtainOnBackCoolPad.Text = Convert.ToString(EnvCurMData.LengthOfCurtainOnBackCoolPad);
                    txtOnSidesCoolingPads.Text = Convert.ToString(EnvCurMData.OnSidesCoolingPads);
                    txtSuspensionAtEveryFeet.Text = Convert.ToString(EnvCurMData.SuspensionAtEveryFeet);
                    txtSideWallBottom.Text = Convert.ToString(EnvCurMData.SideWallBottom);
                    txtSideWallTop.Text = Convert.ToString(EnvCurMData.SideWallTop);
                    txtWidthOfCurtainNeeded.Text = Convert.ToString(EnvCurMData.WidthOfCurtainNeeded);
                    if (EnvCurMData.LouverSystem == "YES".Trim())
                        chkLouverSystem.Checked = true;
                    else
                        chkLouverSystem.Checked = false;
                    if (EnvCurMData.LectricWinch == "YES".Trim())
                        chkLectricWinch.Checked = true;
                    else
                        chkLectricWinch.Checked = false;
                    if (EnvCurMData.CurtainOnCoolPads == "YES".Trim())
                        chkCurtainOnCoolPads.Checked = true;
                    else
                        chkCurtainOnCoolPads.Checked = false;
                    txtCostOfTotalSystem.Text = Convert.ToString(EnvCurMData.CostOfTotalSystem);
                    txtCostPerHouse.Text = Convert.ToString(EnvCurMData.CostPerHouse);
                    txtCostPerBird.Text = Convert.ToString(EnvCurMData.CostPerBird);
                    if (EnvCurMData.CostOfTotalSystem1 == null)
                        txtCostOfTotalSystem1.Text = Convert.ToString(EnvCurMData.CostOfTotalSystem);
                    else
                        txtCostOfTotalSystem1.Text = Convert.ToString(EnvCurMData.CostOfTotalSystem1);
                    if (EnvCurMData.EnvCurPercent == null)
                        txtEnvCurPercent.Text = "0";
                    else
                        txtEnvCurPercent.Text = Convert.ToString(EnvCurMData.EnvCurPercent);
                    EnvironementAndCurtainCoolpadSystemDetailRepository ventDDRepo = new EnvironementAndCurtainCoolpadSystemDetailRepository();
                    List<tbEnvirnmentAndCurtainCoolpadSystemDetail> ventDDlist = new List<tbEnvirnmentAndCurtainCoolpadSystemDetail>();
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
                    if (cmbSystemType.Text == "POULTRY CONTROLLER SYSTEM")
                    {
                        groupBox2.Enabled = true;
                        groupBox3.Enabled = false;
                    }
                    else if (cmbSystemType.Text == "CURTAIN SYSTEM SIDE / COOL PAD")
                    {
                        groupBox3.Enabled = true;
                        groupBox2.Enabled = false;
                    }
                    btnsave.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
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

        public void maxproductid()
        {
            try
            {
                EnvironementAndCurtainCoolpadSystemMasterRepository productrepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
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

        public void calNetAmt()
        {
            try
            {
                if (txtCostOfTotalSystem1.Text != "")
                {
                    decimal nippleTotalNippleFeederAmt = 0, calPerBird = 0, VentInPer = 0, VenttoatlAmt = 0, VentInPerAmt = 0;
                    nippleTotalNippleFeederAmt = Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                    VentInPer = Convert.ToDecimal(txtEnvCurPercent.Text);
                    VenttoatlAmt = nippleTotalNippleFeederAmt + (nippleTotalNippleFeederAmt * VentInPer / 100);
                    VentInPerAmt = Convert.ToDecimal(nippleTotalNippleFeederAmt * VentInPer / 100);
                    txtEnvCurPercentAmt.Text = Convert.ToString(Math.Round(VentInPerAmt, 2, MidpointRounding.AwayFromZero));
                    txtCostOfTotalSystem.Text = Convert.ToString(Math.Round(VenttoatlAmt, 2, MidpointRounding.AwayFromZero));
                    txtCostPerHouse.Text = Convert.ToString(Math.Round(VenttoatlAmt, 2, MidpointRounding.AwayFromZero));
                    try
                    {
                        calPerBird = VenttoatlAmt / TotalPerBird;
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

        public void clear()
        {
            try
            {
                maxproductid();
                txtABCDSmart3g.Text = "";
                txtCablesAndWiringLightsYESNO.Text = "";
                txtCCommunicationMuxDCommunicationNNo.Text = "";
                txtCode.Text = "";
                txtCostOfTotalSystem.Text = "0";
                txtCostPerBird.Text = "0";
                txtCostPerHouse.Text = "0";
                txtDescription.Text = "";
                txtLengthOfCurtainOnBackCoolPad.Text = "";
                txtLengthOfCurtainSideA1.Text = "";
                txtNoOfHuminiditySensorRHS.Text = "";
                txtOnOtherSideB2.Text = "";
                txtOnSidesCoolingPads.Text = "";
                txtPanelModelA6.Text = "";
                txtQty.Text = "";
                txtRefChNo.Text = "";
                lblCustomerName.Text = "";
                ttxLengthOfCurtainOnOtherSideB1.Text = "";
                txtSideA2.Text = "";
                txtSideWallBottom.Text = "";
                txtSideWallTop.Text = "";
                txtSuspensionAtEveryFeet.Text = "";
                txtWidthOfCurtainNeeded.Text = "";
                chkCurtainOnCoolPads.Checked = false;
                chkLectricWinch.Checked = false;
                chkLouverSystem.Checked = false;
                GvProductInfo.Rows.Clear();
                groupBox3.Enabled = true;
                btnsave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                groupBox2.Enabled = true; TotalPerBird = 0;
                txtEnvCurPercent.Text = "0";
                txtEnvCurPercentAmt.Text = "0";
                txtCostOfTotalSystem1.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text != "" || txtDescription.Text != "" || txtQty.Text != "")
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

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
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
                if (txtRefChNo.Text != "" && cmbSystemType.Text != "" && GvProductInfo.Rows.Count > 0)
                {
                    if (txtCostOfTotalSystem.Text != "" && txtCostPerHouse.Text != "" && txtCostPerBird.Text != "")
                    {
                        EnvironementAndCurtainCoolpadSystemMasterRepository EnvCurMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                        var EnvCurSMdata = EnvCurMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Type == cmbSystemType.Text).FirstOrDefault();
                        if (EnvCurSMdata != null)
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
                                tblEnvirnmentAndCurtainCoolpadSystemMaster EnvCurMData = new tblEnvirnmentAndCurtainCoolpadSystemMaster();
                                EnvCurMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                                EnvCurMData.VentilationQutNo = Convert.ToInt32(txtQutNo.Text);
                                EnvCurMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                                EnvCurMData.ReferenceId = Convert.ToInt32(projMData.Id);
                                EnvCurMData.Type = cmbSystemType.Text.Trim();
                                EnvCurMData.ABCDSmart3g = Convert.ToString(txtABCDSmart3g.Text.Trim());
                                EnvCurMData.PanelModelA6 = txtPanelModelA6.Text.Trim();
                                EnvCurMData.NoOfHuminiditySensorRHS = txtNoOfHuminiditySensorRHS.Text.Trim();
                                EnvCurMData.CCommunicationMuxDCommunicationNNo = txtCCommunicationMuxDCommunicationNNo.Text.Trim();
                                EnvCurMData.CablesAndWiringLightsYESNO = txtCablesAndWiringLightsYESNO.Text.Trim();
                                if (txtLengthOfCurtainSideA1.Text != "")
                                    EnvCurMData.LengthOfCurtainSideA1 = Convert.ToDecimal(txtLengthOfCurtainSideA1.Text);
                                else
                                    EnvCurMData.LengthOfCurtainSideA1 = 0;
                                if (txtSideA2.Text != "")
                                    EnvCurMData.SideA2 = Convert.ToDecimal(txtSideA2.Text);
                                else
                                    EnvCurMData.SideA2 = 0;
                                if (ttxLengthOfCurtainOnOtherSideB1.Text != "")
                                    EnvCurMData.LengthOfCurtainOnOtherSideB1 = Convert.ToDecimal(ttxLengthOfCurtainOnOtherSideB1.Text);
                                else
                                    EnvCurMData.LengthOfCurtainOnOtherSideB1 = 0;
                                if (txtOnOtherSideB2.Text != "")
                                    EnvCurMData.OnOtherSideB2 = Convert.ToDecimal(txtOnOtherSideB2.Text);
                                else
                                    EnvCurMData.OnOtherSideB2 = 0;
                                if (txtLengthOfCurtainOnBackCoolPad.Text != "")
                                    EnvCurMData.LengthOfCurtainOnBackCoolPad = Convert.ToDecimal(txtLengthOfCurtainOnBackCoolPad.Text);
                                else
                                    EnvCurMData.LengthOfCurtainOnBackCoolPad = 0;
                                if (txtOnSidesCoolingPads.Text != "")
                                    EnvCurMData.OnSidesCoolingPads = Convert.ToDecimal(txtOnSidesCoolingPads.Text);
                                else
                                    EnvCurMData.OnSidesCoolingPads = 0;
                                if (txtSuspensionAtEveryFeet.Text != "")
                                    EnvCurMData.SuspensionAtEveryFeet = Convert.ToDecimal(txtSuspensionAtEveryFeet.Text);
                                else
                                    EnvCurMData.SuspensionAtEveryFeet = 0;
                                if (txtSideWallBottom.Text != "")
                                    EnvCurMData.SideWallBottom = Convert.ToDecimal(txtSideWallBottom.Text);
                                else
                                    EnvCurMData.SideWallBottom = 0;
                                if (txtSideWallTop.Text != "")
                                    EnvCurMData.SideWallTop = Convert.ToDecimal(txtSideWallTop.Text);
                                else
                                    EnvCurMData.SideWallTop = 0;
                                if (txtWidthOfCurtainNeeded.Text != "")
                                    EnvCurMData.WidthOfCurtainNeeded = Convert.ToDecimal(txtWidthOfCurtainNeeded.Text);
                                else
                                    EnvCurMData.WidthOfCurtainNeeded = 0;
                                if (chkLouverSystem.Checked == true)
                                    EnvCurMData.LouverSystem = "YES".Trim();
                                else
                                    EnvCurMData.LouverSystem = "NO".Trim();
                                if (chkLectricWinch.Checked == true)
                                    EnvCurMData.LectricWinch = "YES".Trim();
                                else
                                    EnvCurMData.LectricWinch = "NO".Trim();
                                if (chkCurtainOnCoolPads.Checked == true)
                                    EnvCurMData.CurtainOnCoolPads = "YES".Trim();
                                else
                                    EnvCurMData.CurtainOnCoolPads = "NO".Trim();
                                if (txtCostOfTotalSystem.Text != "")
                                    EnvCurMData.CostOfTotalSystem = Convert.ToDecimal(txtCostOfTotalSystem.Text);
                                else
                                    EnvCurMData.CostOfTotalSystem = 0;
                                if (txtCostPerHouse.Text != "")
                                    EnvCurMData.CostPerHouse = Convert.ToDecimal(txtCostPerHouse.Text);
                                else
                                    EnvCurMData.CostPerHouse = 0;
                                if (txtCostPerBird.Text != "")
                                    EnvCurMData.CostPerBird = Convert.ToDecimal(txtCostPerBird.Text);
                                else
                                    EnvCurMData.CostPerBird = 0;
                                if (txtCostOfTotalSystem1.Text != "")
                                    EnvCurMData.CostOfTotalSystem1 = Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                                else
                                    EnvCurMData.CostOfTotalSystem1 = 0;
                                if (txtEnvCurPercent.Text != "")
                                    EnvCurMData.EnvCurPercent = Convert.ToDecimal(txtEnvCurPercent.Text);
                                else
                                    EnvCurMData.EnvCurPercent = 0;
                                EnvCurMData.CompId = CommonMethod.CompId;
                                EnvCurMData.TransactionYear = CommonMethod.TransactionYear;
                                EnvCurMRepo.Add(EnvCurMData);
                                EnvCurMRepo.Save();

                                updateId = Convert.ToInt32(EnvCurMData.Id);
                                ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData = projDDRepo.GetAll().Where(t => t.MasterId == projMData.Id && t.Description.Trim() == cmbSystemType.Text).FirstOrDefault();
                                if (projDData != null)
                                {
                                    projDDRepo = new ProjectGlanceDetailRepository();
                                    tblProjectGlanceDetail projDData1 = projDDRepo.GetById(projDData.Id);
                                    projDData1.InvestBird = Convert.ToDecimal(EnvCurMData.CostPerBird);
                                    projDData1.InvestmentPerHouse = Convert.ToDecimal(EnvCurMData.CostOfTotalSystem);
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
                                    EnvironementAndCurtainCoolpadSystemDetailRepository ventilationDRepo = new EnvironementAndCurtainCoolpadSystemDetailRepository();
                                    tbEnvirnmentAndCurtainCoolpadSystemDetail ventilationDData = new tbEnvirnmentAndCurtainCoolpadSystemDetail();
                                    ventilationDData.MasterId = updateId;
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

        private void frmEnvoirmentControlSystemAndCurtain_System_Load(object sender, EventArgs e)
        {
            loadRefernceNo();
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
                        EnvironementAndCurtainCoolpadSystemMasterRepository envCurtainMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                        var envCurMData = envCurtainMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.Type == cmbSystemType.Text).FirstOrDefault();
                        if (envCurMData != null)
                        {
                            MessageBox.Show("This type of quotation save already");
                            this.ActiveControl = txtRefChNo;
                            txtRefChNo.Text = "";
                        }
                        else
                        {
                            if (cmbSystemType.Text == "POULTRY CONTROLLER SYSTEM")
                            {
                                groupBox2.Enabled = true;
                                groupBox3.Enabled = false;
                            }
                            else if (cmbSystemType.Text == "CURTAIN SYSTEM SIDE / COOL PAD")
                            {
                                groupBox3.Enabled = true;
                                groupBox2.Enabled = false;
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

        private void btndeliverychallenlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                EnvAndCur = this;
                frmEnvoirmentControlSystemAndCurtain_System pq = new frmEnvoirmentControlSystemAndCurtain_System();
                pq.Visible = false;
                frmEnvironmentAndCurtainCoolpadSystemList plist = new frmEnvironmentAndCurtainCoolpadSystemList();
                plist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                EnvironementAndCurtainCoolpadSystemMasterRepository envMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                var envMData = envMRepo.GetAll().Where(t => t.Id == updateId).FirstOrDefault();
                if (envMData != null)
                {
                    RptEnvironmentCurtainSystem rpt = new RptEnvironmentCurtainSystem(updateId, envMData.Type.Trim());
                    rpt.ShowDialog();
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

        private void txtEnvCurPercent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEnvCurPercent.Text != "")
                    calNetAmt();
                else
                {
                    txtEnvCurPercent.Text = "0";
                    this.ActiveControl = txtEnvCurPercent;
                }
            }
            catch (Exception)
            { }
        }

        private void txtEnvCurPercentAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtEnvCurPercentAmt.Text != "")
                {
                    if (Convert.ToDecimal(txtEnvCurPercentAmt.Text) <= Convert.ToDecimal(txtCostOfTotalSystem1.Text))
                    {
                        decimal discper = 0;
                        discper = Convert.ToDecimal(txtEnvCurPercentAmt.Text) * 100 / Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                        txtEnvCurPercent.Text = Convert.ToString(Math.Round(discper, 5, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Enviornmnet And Coolpad Amount.", "Warning");
                        txtEnvCurPercentAmt.Text = "0";
                        this.ActiveControl = txtEnvCurPercentAmt;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbSystemType.Text != "" && GvProductInfo.Rows.Count > 0)
                {
                    if (txtCostOfTotalSystem.Text != "" && txtCostPerHouse.Text != "" && txtCostPerBird.Text != "")
                    {
                        EnvironementAndCurtainCoolpadSystemMasterRepository EnvCurMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                        var EnvCurSMdata = EnvCurMRepo.GetAll().Where(t => t.tblProjectAtGlanceMaster.RefernceNo == txtRefChNo.Text && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Type.Trim() == cmbSystemType.Text.Trim() && t.Id != updateId).FirstOrDefault();
                        if (EnvCurSMdata != null)
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
                                EnvCurMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                                tblEnvirnmentAndCurtainCoolpadSystemMaster EnvCurMData = EnvCurMRepo.GetById(updateId);
                                EnvCurMData.VentilationQutNo = Convert.ToInt32(txtQutNo.Text);
                                EnvCurMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                                EnvCurMData.ReferenceId = Convert.ToInt32(projMData.Id);
                                EnvCurMData.Type = cmbSystemType.Text.Trim();
                                EnvCurMData.ABCDSmart3g = Convert.ToString(txtABCDSmart3g.Text.Trim());
                                EnvCurMData.PanelModelA6 = txtPanelModelA6.Text.Trim();
                                EnvCurMData.NoOfHuminiditySensorRHS = txtNoOfHuminiditySensorRHS.Text.Trim();
                                EnvCurMData.CCommunicationMuxDCommunicationNNo = txtCCommunicationMuxDCommunicationNNo.Text.Trim();
                                EnvCurMData.CablesAndWiringLightsYESNO = txtCablesAndWiringLightsYESNO.Text.Trim();
                                if (txtLengthOfCurtainSideA1.Text != "")
                                    EnvCurMData.LengthOfCurtainSideA1 = Convert.ToDecimal(txtLengthOfCurtainSideA1.Text);
                                else
                                    EnvCurMData.LengthOfCurtainSideA1 = 0;
                                if (txtSideA2.Text != "")
                                    EnvCurMData.SideA2 = Convert.ToDecimal(txtSideA2.Text);
                                else
                                    EnvCurMData.SideA2 = 0;
                                if (ttxLengthOfCurtainOnOtherSideB1.Text != "")
                                    EnvCurMData.LengthOfCurtainOnOtherSideB1 = Convert.ToDecimal(ttxLengthOfCurtainOnOtherSideB1.Text);
                                else
                                    EnvCurMData.LengthOfCurtainOnOtherSideB1 = 0;
                                if (txtOnOtherSideB2.Text != "")
                                    EnvCurMData.OnOtherSideB2 = Convert.ToDecimal(txtOnOtherSideB2.Text);
                                else
                                    EnvCurMData.OnOtherSideB2 = 0;
                                if (txtLengthOfCurtainOnBackCoolPad.Text != "")
                                    EnvCurMData.LengthOfCurtainOnBackCoolPad = Convert.ToDecimal(txtLengthOfCurtainOnBackCoolPad.Text);
                                else
                                    EnvCurMData.LengthOfCurtainOnBackCoolPad = 0;
                                if (txtOnSidesCoolingPads.Text != "")
                                    EnvCurMData.OnSidesCoolingPads = Convert.ToDecimal(txtOnSidesCoolingPads.Text);
                                else
                                    EnvCurMData.OnSidesCoolingPads = 0;
                                if (txtSuspensionAtEveryFeet.Text != "")
                                    EnvCurMData.SuspensionAtEveryFeet = Convert.ToDecimal(txtSuspensionAtEveryFeet.Text);
                                else
                                    EnvCurMData.SuspensionAtEveryFeet = 0;
                                if (txtSideWallBottom.Text != "")
                                    EnvCurMData.SideWallBottom = Convert.ToDecimal(txtSideWallBottom.Text);
                                else
                                    EnvCurMData.SideWallBottom = 0;
                                if (txtSideWallTop.Text != "")
                                    EnvCurMData.SideWallTop = Convert.ToDecimal(txtSideWallTop.Text);
                                else
                                    EnvCurMData.SideWallTop = 0;
                                if (txtWidthOfCurtainNeeded.Text != "")
                                    EnvCurMData.WidthOfCurtainNeeded = Convert.ToDecimal(txtWidthOfCurtainNeeded.Text);
                                else
                                    EnvCurMData.WidthOfCurtainNeeded = 0;
                                if (chkLouverSystem.Checked == true)
                                    EnvCurMData.LouverSystem = "YES".Trim();
                                else
                                    EnvCurMData.LouverSystem = "NO".Trim();
                                if (chkLectricWinch.Checked == true)
                                    EnvCurMData.LectricWinch = "YES".Trim();
                                else
                                    EnvCurMData.LectricWinch = "NO".Trim();
                                if (chkCurtainOnCoolPads.Checked == true)
                                    EnvCurMData.CurtainOnCoolPads = "YES".Trim();
                                else
                                    EnvCurMData.CurtainOnCoolPads = "NO".Trim();
                                if (txtCostOfTotalSystem.Text != "")
                                    EnvCurMData.CostOfTotalSystem = Convert.ToDecimal(txtCostOfTotalSystem.Text);
                                else
                                    EnvCurMData.CostOfTotalSystem = 0;
                                if (txtCostPerHouse.Text != "")
                                    EnvCurMData.CostPerHouse = Convert.ToDecimal(txtCostPerHouse.Text);
                                else
                                    EnvCurMData.CostPerHouse = 0;
                                if (txtCostPerBird.Text != "")
                                    EnvCurMData.CostPerBird = Convert.ToDecimal(txtCostPerBird.Text);
                                else
                                    EnvCurMData.CostPerBird = 0;
                                if (txtCostOfTotalSystem1.Text != "")
                                    EnvCurMData.CostOfTotalSystem1 = Convert.ToDecimal(txtCostOfTotalSystem1.Text);
                                else
                                    EnvCurMData.CostOfTotalSystem1 = 0;
                                if (txtEnvCurPercent.Text != "")
                                    EnvCurMData.EnvCurPercent = Convert.ToDecimal(txtEnvCurPercent.Text);
                                else
                                    EnvCurMData.EnvCurPercent = 0;
                                EnvCurMData.CompId = CommonMethod.CompId;
                                EnvCurMData.TransactionYear = CommonMethod.TransactionYear;
                                EnvCurMRepo.Edit(EnvCurMData);
                                EnvCurMRepo.Save();

                                int MasterId = Convert.ToInt32(EnvCurMData.Id);
                                ProjectGlanceDetailRepository projDDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData = projDDRepo.GetAll().Where(t => t.MasterId == projMData.Id && t.Description.Trim() == cmbSystemType.Text).FirstOrDefault();
                                if (projDData != null)
                                {
                                    projDDRepo = new ProjectGlanceDetailRepository();
                                    tblProjectGlanceDetail projDData1 = projDDRepo.GetById(projDData.Id);
                                    projDData1.InvestBird = Convert.ToDecimal(EnvCurMData.CostPerBird);
                                    projDData1.InvestmentPerHouse = Convert.ToDecimal(EnvCurMData.CostOfTotalSystem);
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
                                if (deleteId.Count > 0)
                                {
                                    for (int x = 0; x < deleteId.Count; x++)
                                    {
                                        EnvironementAndCurtainCoolpadSystemDetailRepository ventilationDRepo = new EnvironementAndCurtainCoolpadSystemDetailRepository();
                                        tbEnvirnmentAndCurtainCoolpadSystemDetail ventilationDData = ventilationDRepo.GetById(deleteId[x]);
                                        ventilationDRepo.Remove(ventilationDData);
                                        ventilationDRepo.Save();
                                    }
                                }

                                for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                                {
                                    int RowId = Convert.ToInt32(GvProductInfo.Rows[i].Cells[0].Value);
                                    if (RowId != 0)
                                    {
                                        EnvironementAndCurtainCoolpadSystemDetailRepository ventilationDRepo = new EnvironementAndCurtainCoolpadSystemDetailRepository();
                                        tbEnvirnmentAndCurtainCoolpadSystemDetail ventilationDData = ventilationDRepo.GetById(RowId);
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
                                        EnvironementAndCurtainCoolpadSystemDetailRepository ventilationDRepo = new EnvironementAndCurtainCoolpadSystemDetailRepository();
                                        tbEnvirnmentAndCurtainCoolpadSystemDetail ventilationDData = new tbEnvirnmentAndCurtainCoolpadSystemDetail();
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
                    EnvironementAndCurtainCoolpadSystemMasterRepository EnvCurMRepo = new EnvironementAndCurtainCoolpadSystemMasterRepository();
                    var nipplefeederData = EnvCurMRepo.GetAll().Where(t => t.Id == updateId).FirstOrDefault();
                    if (nipplefeederData != null)
                    {
                        DataTable dtDData = db.GetTable("delete from tbEnvirnmentAndCurtainCoolpadSystemDetail where MasterId=" + updateId + "");
                        DataTable dtMData = db.GetTable("delete from tblEnvirnmentAndCurtainCoolpadSystemMaster where Id=" + updateId + "");
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