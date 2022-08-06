using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSettings : Form
    {
        public int updateId;
        DbClass db = new DbClass();
        public List<string> lstPrinterList = new List<string>();
        byte[] @photologo;
        ToolTip tooltipbtn = new ToolTip();
        CommonMethod CM = new CommonMethod();

        public frmSettings()
        {
            InitializeComponent();
            this.ActiveControl = txtEmailId;
            chkDiscEstimate.Checked = true;
            chkDiscTax.Checked = true;
            btnUpdate.Enabled = false;
            chkAutoBill.Visible = false;
            chkAutoCredit.Visible = false;
            chkAutoFestivals.Visible = false;
            txtFormPassword.Enabled = false;
            getsettingdata(CommonMethod.CompId);
        }

        public void getsettingdata(int? compid)
        {
            try
            {
                SettingRepository SettingRepo = new SettingRepository();
                var data = SettingRepo.GetAll().Where(t => t.CompId == compid).FirstOrDefault();
                if (data != null)
                {
                    updateId = data.Id;
                    txtEmailId.Text = data.EmailId;
                    txtPassword.Text = data.Password;
                    txtSmsKey.Text = data.SMSKey;
                    txtSMSUserId.Text = data.SMSUserId;
                    txtSMSPassword.Text = data.SMSPassword;
                    txtMDI.Text = data.MDIName;
                    if (data.SendSMS == "True")
                        chkSendSMS.Checked = true;
                    else
                        chkSendSMS.Checked = false;
                    if (data.SendOption == "Auto")
                    {
                        rdbtnauto.Checked = true;
                        rdbtnmannual.Checked = false;
                        chkAutoBill.Visible = true;
                        chkAutoCredit.Visible = true;
                        chkAutoFestivals.Visible = true;
                        if (data.AutoForBill == "True")
                            chkAutoBill.Checked = true;
                        else
                            chkAutoBill.Checked = false;
                        if (data.AutoForCredit == "True")
                            chkAutoCredit.Checked = true;
                        else
                            chkAutoCredit.Checked = false;
                        if (data.AutoForFestivals == "True")
                            chkAutoFestivals.Checked = true;
                        else
                            chkAutoFestivals.Checked = false;
                    }
                    else if (data.SendOption == "Mannual")
                    {
                        rdbtnmannual.Checked = true;
                        rdbtnauto.Checked = false;
                        chkAutoBill.Visible = false;
                        chkAutoCredit.Visible = false;
                        chkAutoFestivals.Visible = false;
                    }
                    if (data.VatOption == "Exclusive")
                    {
                        //  rdbtnex.Checked = true;
                        // txtVat1.Text = Convert.ToString(data.Vat1);
                        // txtVat2.Text = Convert.ToString(data.Vat2);
                    }
                    if (data.StockOn == "Purchase Rate")
                        rdbtnpurchase.Checked = true;
                    else if (data.StockOn == "Sale Rate")
                        rdbtnsale.Checked = true;
                    if (data.AvgRateOn == "Basic")
                        rdbtnAvgRateBasic.Checked = true;
                    else if (data.AvgRateOn == "With GST")
                        rdbtnAvgRateGST.Checked = true;
                    if (data.RoundOff == 1)
                        chkRoundOff.Checked = true;
                    else
                        chkRoundOff.Checked = false;
                    txtPrinterName.Text = data.PrinterName;
                    if (data.TaxInvoiveLogo == "True")
                        chkTax.Checked = true;
                    else
                        chkTax.Checked = false;
                    if (data.EstimateInvoiceLogo == "True")
                        chkEstimate.Checked = true;
                    else
                        chkEstimate.Checked = false;
                    if (data.DiscInTax == "True")
                        chkDiscTax.Checked = true;
                    else
                        chkDiscTax.Checked = false;
                    if (data.DiscInEstimate == "True")
                        chkDiscEstimate.Checked = true;
                    else
                        chkDiscEstimate.Checked = false;
                    if (!string.IsNullOrEmpty(Convert.ToString(data.ShowUnit)))
                    {
                        if (data.ShowUnit == "Single")
                            rdbtnSingle.Checked = true;
                        else
                            rdbtnBoth.Checked = true;
                    }
                    if (data.ShowMfgComp == "True")
                        chkMfgComp.Checked = true;
                    else
                        chkMfgComp.Checked = false;
                    if (data.OneTimeReceiptAmountInReport == "True")
                        chkReceiptPayment.Checked = true;
                    else
                        chkReceiptPayment.Checked = false;
                    if (data.AutoTaxBillFromEstimate == "True")
                        chkAutoTaxBill.Checked = true;
                    else
                        chkAutoTaxBill.Checked = false;
                    if (data.TaxInvoicePrintOn == "Portrait")
                        rdbtnportrait.Checked = true;
                    else if (data.TaxInvoicePrintOn == "Both")
                        rdbtnbothprint.Checked = true;
                    else if (data.TaxInvoicePrintOn == "Marathi")
                        radMarathi.Checked = true;
                    else if (data.TaxInvoicePrintOn == "MarathiA6")
                        radAll.Checked = true;
                    else if (data.TaxInvoicePrintOn == "HoriZontal")
                        rdbtnhorizontal.Checked = true;
                    else if (data.TaxInvoicePrintOn == "Thermal")
                        radThermal.Checked = true;
                    else if (data.TaxInvoicePrintOn == "Slab")
                        radSlab.Checked = true;
                    else if (data.TaxInvoicePrintOn == "A5")
                        rdbtnA5.Checked = true;
                    else if (data.TaxInvoicePrintOn == "A6")
                        rdbtnA6.Checked = true;
                    else
                        rdbtnHorizontalA5.Checked = true;
                    if (data.EstimateInvoicePrintOn == "EA6")
                        rdbtnEstA6.Checked = true;
                    else if (data.EstimateInvoicePrintOn == "EMarathiA6")
                        rdbtnEStimateMarathiA6.Checked = true;
                    else
                        rdbtnEstA5.Checked = true;
                    if (data.CompanyFullNameOnEstimateBill == "True")
                        chkCompanyFullNameShowOnEstimate.Checked = true;
                    else
                        chkCompanyFullNameShowOnEstimate.Checked = false;
                    if (data.DeleteSaleBill == "True")
                        chkDeleteBill.Checked = true;
                    else
                        chkDeleteBill.Checked = false;
                    if (data.AdvanceAmtInLedger == "True")
                        chkadvanceinledger.Checked = true;
                    else
                        chkadvanceinledger.Checked = false;
                    if (data.CompositionScheme == "True")
                        chkCompositionscheme.Checked = true;
                    else
                        chkCompositionscheme.Checked = false;
                    if (data.AddressShowOnEstimateBill == "True")
                        chkshowAddrerssOnEstimateBill.Checked = true;
                    else
                        chkshowAddrerssOnEstimateBill.Checked = false;
                    if (data.BillinstedOfEstimateBill == "True")
                        chkShowBillinstedEstimalBill.Checked = true;
                    else
                        chkShowBillinstedEstimalBill.Checked = false;
                    if (data.AutoPaidAmount == "True")
                        chkAutoPaidAmount.Checked = true;
                    else
                        chkAutoPaidAmount.Checked = false;
                    if (data.BillWiseReceipt == "True")
                        chkBillWiseReceipt.Checked = true;
                    else
                        chkBillWiseReceipt.Checked = false;
                    if (data.PoweredByName != "")
                        txtPoweredByName.Text = data.PoweredByName;
                    picLogo.Image = null;
                    if (data.MDIImage != null)
                    {
                        @photologo = (byte[])data.MDIImage;
                        MemoryStream ms = new MemoryStream(@photologo);
                        picLogo.Image = Image.FromStream(ms);
                    }
                    txtproprietorname.Text = data.PropriterName;
                    if (data.MultipleCopies == "Y")
                        chkMulti.Checked = true;
                    if (data.MarathiSMS == "True")
                        chkMarathiSMS.Checked = true;
                    else
                        chkMarathiSMS.Checked = false;
                    if (data.ShowLicNum == "All")
                        chkshowlicenceno.Checked = true;
                    else
                        chkshowlicenceno.Checked = false;
                    if (data.ThreeUnit == "True")
                        chkThreeUnit.Checked = true;
                    else
                        chkThreeUnit.Checked = false;
                    if (!string.IsNullOrEmpty(data.DataBaseBackupPath))
                        txtdatabasebackupPath.Text = data.DataBaseBackupPath;
                    if (data.ShowTenderCash == "Y")
                        chkShowtendercash.Checked = true;
                    else
                        chkShowtendercash.Checked = false;
                    if (data.ShowQtyThreeDigit == "Y")
                        chkqtythreedigit.Checked = true;
                    else
                        chkqtythreedigit.Checked = false;
                    if (data.ShowUserDefinedSeries == "Y")
                        chkShowUserSeries.Checked = true;
                    else
                        chkShowUserSeries.Checked = false;
                    if (string.IsNullOrEmpty(data.CommonProduct))
                        chkCommProduct.Checked = true;
                    else
                    {
                        if (data.CommonProduct == "Y")
                        {
                            chkCommProduct.Checked = true;
                            CommonMethod.commProduct = true;
                        }
                        else
                        {
                            chkCommProduct.Checked = false;
                            CommonMethod.commProduct = false;
                        }
                    }
                    if (data.StockShowOnBill == "Y")
                        chkStockShow.Checked = true;
                    else
                        chkStockShow.Checked = false;
                    if (data.SeparationLedger == "Y")
                    {
                        chkSeparateLedger.Checked = true;
                        chkSeparateLedger.Enabled = false;
                    }
                    else
                        chkSeparateLedger.Checked = false;
                    if (data.ShowLoginWindowOnForm == "P")
                        rdbtnPurLogin.Checked = true;
                    else if (data.ShowLoginWindowOnForm == "D")
                        rdbtnDailySaleLogin.Checked = true;
                    else
                    {
                        rdbtnPurLogin.Checked = false;
                        rdbtnDailySaleLogin.Checked = false;
                    }
                    txtPassword.Text = data.FormPassword;
                    if (data.ShowBarcodePrintOn == "T")
                    {
                        rdbtnbarcodeThermal.Checked = true;
                    }
                    else if (data.ShowBarcodePrintOn == "A")
                    {
                        rdbtnBarcodeA4.Checked = true;
                    }
                    else
                    {
                        rdBtnPRN.Checked = true;
                    }
                    if (data.Applyonmarathifont == "Y")
                        chkApplyMarathiFont.Checked = true;
                    else
                        chkApplyMarathiFont.Checked = false;
                    txtSMSURLPath.Text = data.SMSURLPath;
                    if (data.PoultryQuotations == "Y")
                        chkPoultryQuotation.Checked = true;
                    else
                        chkPoultryQuotation.Checked = false;
                    if (data.HideOutStandingAmt == "Y")
                        chkHideOutStanding.Checked = true;
                    else
                        chkHideOutStanding.Checked = false;
                    if (data.RateDecimalPlaces == 2)
                        rdbtn2Decimal.Checked = true;
                    else
                        rdbtn3Decimal.Checked = true;
                    if (data.TCSEnabled == true)
                        chkTCS.Checked = true;
                    else
                        chkTCS.Checked = false;
                    if (data.UpdateMRP == true)
                        chkUpdateMRPfromPurchase.Checked = true;
                    else
                        chkUpdateMRPfromPurchase.Checked = false;
                    if (data.BarcodePrintTopMargin != null)
                        txtTopMargin.Text = data.BarcodePrintTopMargin.ToString();
                    else
                        txtTopMargin.Text = "0";
                    if (data.ShowMRPonPrint == true)
                        chkShowMRPonPrint.Checked = true;
                    else
                        chkShowMRPonPrint.Checked = false;
                    if (data.ContinueUserSeries == true)
                        chkContinueUserSeries.Checked = true;
                    else
                        chkContinueUserSeries.Checked = false;
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                SettingRepo.Dispose();
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
                if (btnSave.Enabled == true)
                    btnSave.PerformClick();
                else if (btnUpdate.Enabled == true)
                    btnUpdate.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void conv_photologo()
        {
            @photologo = null;

            if (picLogo.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                picLogo.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                @photologo = photo_aray;
            }
        }

        private void rdbtnauto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string result1 = null;
                foreach (Control control in this.grpSMSSetting.Controls)
                {
                    if (control is RadioButton)
                    {
                        RadioButton radio = control as RadioButton;
                        if (radio.Checked)
                            result1 = radio.Text;
                    }
                }
                chkAutoBill.Visible = true;
                chkAutoCredit.Visible = true;
                chkAutoFestivals.Visible = true;
                SettingRepository SettingRepo = new SettingRepository();
                var data = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (data.AutoForBill == "True")
                    chkAutoBill.Checked = true;
                else
                    chkAutoBill.Checked = false;
                if (data.AutoForCredit == "True")
                    chkAutoCredit.Checked = true;
                else
                    chkAutoCredit.Checked = false;
                if (data.AutoForFestivals == "True")
                    chkAutoFestivals.Checked = true;
                else
                    chkAutoFestivals.Checked = false;
                if (data.Applyonmarathifont == "Y")
                    chkApplyMarathiFont.Checked = true;
                else
                    chkApplyMarathiFont.Checked = false;
                SettingRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtEmailId_Leave(object sender, EventArgs e)
        {
            try
            {
                Regex mRegxExpression;
                if (txtEmailId.Text.Trim() != string.Empty)
                {
                    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                    if (!mRegxExpression.IsMatch(txtEmailId.Text.Trim()))
                    {
                        MessageBox.Show("Please enter valid Email-ID.", "Warning");
                        this.ActiveControl = txtEmailId;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == false)
                {
                    if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        this.Close();
                }
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SettingRepository SettingRepo = new SettingRepository();
                if (txtEmailId.Text.Trim() != "" && txtPassword.Text.Trim() != "" && txtMDI.Text.Trim() != "" && (rdbtnBoth.Checked != false || rdbtnSingle.Checked != false))
                {
                    progressBar1.Visible = true;
                    int minval = 0, maxval = 100;
                    progressBar1.Minimum = minval;
                    progressBar1.Maximum = maxval;
                    progressBar1.Value = minval + 1;
                    Setting sdata = new Setting();
                    sdata.EmailId = txtEmailId.Text.Trim();
                    sdata.Password = txtPassword.Text.Trim();
                    sdata.SMSKey = txtSmsKey.Text.Trim();
                    sdata.SMSUserId = txtSMSUserId.Text.Trim();
                    sdata.SMSPassword = txtSMSPassword.Text.Trim();
                    sdata.MDIName = txtMDI.Text.Trim();
                    progressBar1.Value = minval + 5;
                    if (rdbtnauto.Checked)
                        sdata.SendOption = rdbtnauto.Text;
                    else if (rdbtnmannual.Checked)
                        sdata.SendOption = rdbtnmannual.Text;
                    progressBar1.Value = minval + 10;
                    progressBar1.Value = minval + 15;
                    if (chkSendSMS.Checked == true)
                        sdata.SendSMS = "True";
                    else
                        sdata.SendSMS = "False";
                    progressBar1.Value = minval + 25;
                    if (chkAutoBill.Checked == true)
                        sdata.AutoForBill = "True";
                    else
                        sdata.AutoForBill = "False";
                    if (chkAutoCredit.Checked == true)
                        sdata.AutoForCredit = "True";
                    else
                        sdata.AutoForCredit = "False";
                    if (chkAutoFestivals.Checked == true)
                        sdata.AutoForFestivals = "True";
                    else
                        sdata.AutoForFestivals = "False";
                    if (chkMarathiSMS.Checked == true)
                        sdata.MarathiSMS = "True";
                    else
                        sdata.MarathiSMS = "False";
                    progressBar1.Value = minval + 30;
                    if (rdbtnpurchase.Checked)
                        sdata.StockOn = rdbtnpurchase.Text;
                    else if (rdbtnsale.Checked)
                        sdata.StockOn = rdbtnsale.Text;
                    if (rdbtnAvgRateBasic.Checked)
                        sdata.AvgRateOn = rdbtnAvgRateBasic.Text;
                    else if (rdbtnAvgRateGST.Checked)
                        sdata.AvgRateOn = rdbtnAvgRateGST.Text;
                    progressBar1.Value = minval + 40;
                    sdata.PrinterName = txtPrinterName.Text;
                    if (chkTax.Checked)
                        sdata.TaxInvoiveLogo = "True";
                    else
                        sdata.TaxInvoiveLogo = "False";
                    if (chkEstimate.Checked)
                        sdata.EstimateInvoiceLogo = "True";
                    else
                        sdata.EstimateInvoiceLogo = "False";
                    if (chkDiscTax.Checked)
                        sdata.DiscInTax = "True";
                    else
                        sdata.DiscInTax = "False";
                    if (chkDiscEstimate.Checked)
                        sdata.DiscInEstimate = "True";
                    else
                        sdata.DiscInEstimate = "False";
                    if (rdbtnSingle.Checked)
                        sdata.ShowUnit = rdbtnSingle.Text;
                    else if (rdbtnBoth.Checked)
                        sdata.ShowUnit = rdbtnBoth.Text;
                    if (chkRoundOff.Checked)
                        sdata.RoundOff = 1;
                    else
                        sdata.RoundOff = 0;
                    if (chkMfgComp.Checked)
                        sdata.ShowMfgComp = "True";
                    else
                        sdata.ShowMfgComp = "False";
                    if (chkReceiptPayment.Checked == true)
                        sdata.OneTimeReceiptAmountInReport = "True";
                    else
                        sdata.OneTimeReceiptAmountInReport = "False";
                    if (chkAutoTaxBill.Checked == true)
                        sdata.AutoTaxBillFromEstimate = "True";
                    else
                        sdata.AutoTaxBillFromEstimate = "False";
                    if (rdbtnportrait.Checked == true)
                        sdata.TaxInvoicePrintOn = "Portrait";
                    else if (rdbtnbothprint.Checked == true)
                        sdata.TaxInvoicePrintOn = "Both";
                    else if (radMarathi.Checked == true)
                        sdata.TaxInvoicePrintOn = "Marathi";
                    else if (radAll.Checked == true)
                        sdata.TaxInvoicePrintOn = "MarathiA6";
                    else if (rdbtnhorizontal.Checked == true)
                        sdata.TaxInvoicePrintOn = "HoriZontal";
                    else if (radThermal.Checked == true)
                        sdata.TaxInvoicePrintOn = "Thermal";
                    else if (radSlab.Checked == true)
                        sdata.TaxInvoicePrintOn = "Slab";
                    else if (rdbtnA5.Checked == true)
                        sdata.TaxInvoicePrintOn = "A5";
                    else if (rdbtnA6.Checked == true)
                        sdata.TaxInvoicePrintOn = "A6";
                    else
                        sdata.TaxInvoicePrintOn = "HorizontalA5";
                    if (rdbtnEstA5.Checked == true)
                        sdata.EstimateInvoicePrintOn = "EA5";
                    else if (rdbtnEstA6.Checked == true)
                        sdata.EstimateInvoicePrintOn = "EA6";
                    else
                        sdata.EstimateInvoicePrintOn = "EMarathiA6";
                    if (chkCompanyFullNameShowOnEstimate.Checked == true)
                        sdata.CompanyFullNameOnEstimateBill = "True";
                    else
                        sdata.CompanyFullNameOnEstimateBill = "False";
                    if (chkDeleteBill.Checked == true)
                        sdata.DeleteSaleBill = "True";
                    else
                        sdata.DeleteSaleBill = "False";
                    if (chkadvanceinledger.Checked == true)
                        sdata.AdvanceAmtInLedger = "True";
                    else
                        sdata.AdvanceAmtInLedger = "False";
                    if (chkCompositionscheme.Checked == true)
                        sdata.CompositionScheme = "True";
                    else
                        sdata.CompositionScheme = "False";
                    if (chkshowAddrerssOnEstimateBill.Checked == true)
                        sdata.AddressShowOnEstimateBill = "True";
                    else
                        sdata.AddressShowOnEstimateBill = "False";
                    if (chkShowBillinstedEstimalBill.Checked == true)
                        sdata.BillinstedOfEstimateBill = "True";
                    else
                        sdata.BillinstedOfEstimateBill = "False";
                    if (chkAutoPaidAmount.Checked == true)
                        sdata.AutoPaidAmount = "True";
                    else
                        sdata.AutoPaidAmount = "False";
                    if (chkBillWiseReceipt.Checked == true)
                        sdata.BillWiseReceipt = "True";
                    else
                        sdata.BillWiseReceipt = "False";
                    if (chkshowlicenceno.Checked == true)
                        sdata.ShowLicNum = "All";
                    else
                        sdata.ShowLicNum = "";
                    if (chkThreeUnit.Checked == true)
                        sdata.ThreeUnit = "True";
                    else
                        sdata.ThreeUnit = "False";
                    if (chkSeparateLedger.Checked == true)
                        sdata.SeparationLedger = "Y";
                    else
                        sdata.SeparationLedger = "N";
                    if (rdbtnbarcodeThermal.Checked == true)
                    {
                        sdata.ShowBarcodePrintOn = "T";
                    }
                    else if (rdbtnBarcodeA4.Checked == true)
                    {
                        sdata.ShowBarcodePrintOn = "A";
                    }
                    else
                    {
                        sdata.ShowBarcodePrintOn = "P";
                    }
                    sdata.PoweredByName = txtPoweredByName.Text;
                    sdata.CompId = CommonMethod.CompId;
                    conv_photologo();
                    sdata.MDIImage = @photologo;
                    sdata.PropriterName = txtproprietorname.Text;
                    if (chkMulti.Checked == true)
                        sdata.MultipleCopies = "Y";
                    if (!string.IsNullOrEmpty(txtdatabasebackupPath.Text))
                        sdata.DataBaseBackupPath = txtdatabasebackupPath.Text;
                    if (chkShowtendercash.Checked == true)
                        sdata.ShowTenderCash = "Y";
                    else
                        sdata.ShowTenderCash = "N";
                    if (chkqtythreedigit.Checked == true)
                        sdata.ShowQtyThreeDigit = "Y";
                    else
                        sdata.ShowQtyThreeDigit = "N";
                    if (chkShowUserSeries.Checked == true)
                        sdata.ShowUserDefinedSeries = "Y";
                    else
                        sdata.ShowUserDefinedSeries = "N";
                    if (chkCommProduct.Checked == true)
                    {
                        sdata.CommonProduct = "Y";
                        CommonMethod.commProduct = true;
                    }
                    else
                    {
                        sdata.CommonProduct = "N";
                        CommonMethod.commProduct = false;
                    }
                    if (chkStockShow.Checked == true)
                        sdata.StockShowOnBill = "Y";
                    else
                        sdata.StockShowOnBill = "N";
                    if (rdbtnPurLogin.Checked == true)
                        sdata.ShowLoginWindowOnForm = "P";
                    else if (rdbtnDailySaleLogin.Checked == true)
                        sdata.ShowLoginWindowOnForm = "D";
                    else
                        sdata.ShowLoginWindowOnForm = "";
                    if (txtFormPassword.Text != "")
                        sdata.FormPassword = txtFormPassword.Text;
                    if (chkApplyMarathiFont.Checked == true)
                        sdata.Applyonmarathifont = "Y";
                    else
                        sdata.Applyonmarathifont = "N";
                    sdata.SMSURLPath = txtSMSURLPath.Text.Trim();
                    if (chkPoultryQuotation.Checked == true)
                        sdata.PoultryQuotations = "Y";
                    else
                        sdata.PoultryQuotations = "N";
                    if (chkHideOutStanding.Checked == true)
                        sdata.HideOutStandingAmt = "Y";
                    else
                        sdata.HideOutStandingAmt = "N";
                    if (rdbtn2Decimal.Checked)
                        sdata.RateDecimalPlaces = 2;
                    else if (rdbtn3Decimal.Checked)
                        sdata.RateDecimalPlaces = 3;
                    else if (rdbtn2Decimal.Checked == false && rdbtn3Decimal.Checked == false)
                        sdata.RateDecimalPlaces = 3;
                    if (chkTCS.Checked)
                        sdata.TCSEnabled = true;
                    else
                        sdata.TCSEnabled = false;
                    if (chkUpdateMRPfromPurchase.Checked)
                        sdata.UpdateMRP = true;
                    else
                        sdata.UpdateMRP = false;
                    if (chkShowMRPonPrint.Checked)
                        sdata.ShowMRPonPrint = true;
                    else
                        sdata.ShowMRPonPrint = false;
                    if (chkContinueUserSeries.Checked)
                        sdata.ContinueUserSeries = true;
                    else
                        sdata.ContinueUserSeries = false;
                    sdata.BarcodePrintTopMargin = Convert.ToInt32(txtTopMargin.Text);
                    SettingRepo.Add(sdata);
                    SettingRepo.Save();
                    progressBar1.Value = minval + 50;
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPAdjustStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                        command.CommandTimeout = 1000;
                        progressBar1.Value = minval + 60;
                        command.ExecuteNonQuery();
                        db.CloseConnection();
                        progressBar1.Value = minval + 90;
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                    progressBar1.Value = maxval;
                    progressBar1.Visible = false;
                    MessageBox.Show("Settings are saved successfully.", "Success");
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    this.ActiveControl = txtEmailId;
                }
                else
                {
                    if (txtEmailId.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter Email-ID.", "Warning");
                        this.ActiveControl = txtEmailId;
                    }
                    else if (txtPassword.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter Password.", "Warning");
                        this.ActiveControl = txtPassword;
                    }
                    else if (txtMDI.Text.Trim() == "")
                    {
                        MessageBox.Show("Please enter MDI Name.", "Warning");
                        this.ActiveControl = txtMDI;
                    }
                    else if (rdbtnSingle.Checked == false && rdbtnBoth.Checked == false)
                        MessageBox.Show("Please select show unit option.", "Warning");
                }
                SettingRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Visible = true;
                int minval = 0, maxval = 100;
                progressBar1.Minimum = minval;
                progressBar1.Maximum = maxval;
                SettingRepository SettingRepo = new SettingRepository();
                progressBar1.Value = minval + 1;
                Setting sdata = new Setting();
                sdata = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.Id == updateId).FirstOrDefault();
                sdata.Id = updateId;
                sdata.EmailId = txtEmailId.Text.Trim();
                sdata.Password = txtPassword.Text.Trim();
                sdata.SMSKey = txtSmsKey.Text.Trim();
                sdata.SMSPassword = txtSMSPassword.Text.Trim();
                sdata.SMSUserId = txtSMSUserId.Text.Trim();
                sdata.MDIName = txtMDI.Text.Trim();
                if (rdbtnauto.Checked)
                    sdata.SendOption = rdbtnauto.Text;
                else if (rdbtnmannual.Checked)
                    sdata.SendOption = rdbtnmannual.Text;
                progressBar1.Value = minval + 5;
                progressBar1.Value = minval + 10;
                if (chkSendSMS.Checked == true)
                    sdata.SendSMS = "True";
                else
                    sdata.SendSMS = "False";
                if (chkAutoBill.Checked == true)
                    sdata.AutoForBill = "True";
                else
                    sdata.AutoForBill = "False";
                progressBar1.Value = minval + 15;
                if (chkAutoCredit.Checked == true)
                    sdata.AutoForCredit = "True";
                else
                    sdata.AutoForCredit = "False";
                if (chkAutoFestivals.Checked == true)
                    sdata.AutoForFestivals = "True";
                else
                    sdata.AutoForFestivals = "False";
                if (chkMarathiSMS.Checked == true)
                    sdata.MarathiSMS = "True";
                else
                    sdata.MarathiSMS = "False";
                progressBar1.Value = minval + 25;
                if (rdbtnpurchase.Checked)
                    sdata.StockOn = rdbtnpurchase.Text;
                else if (rdbtnsale.Checked)
                    sdata.StockOn = rdbtnsale.Text;
                if (rdbtnAvgRateBasic.Checked)
                    sdata.AvgRateOn = rdbtnAvgRateBasic.Text;
                else if (rdbtnAvgRateGST.Checked)
                    sdata.AvgRateOn = rdbtnAvgRateGST.Text;
                progressBar1.Value = minval + 35;
                sdata.PrinterName = txtPrinterName.Text;
                if (chkTax.Checked)
                    sdata.TaxInvoiveLogo = "True";
                else
                    sdata.TaxInvoiveLogo = "False";
                if (chkEstimate.Checked)
                    sdata.EstimateInvoiceLogo = "True";
                else
                    sdata.EstimateInvoiceLogo = "False";
                if (chkDiscTax.Checked)
                    sdata.DiscInTax = "True";
                else
                    sdata.DiscInTax = "False";
                if (chkDiscEstimate.Checked)
                    sdata.DiscInEstimate = "True";
                else
                    sdata.DiscInEstimate = "False";
                if (rdbtnSingle.Checked)
                    sdata.ShowUnit = rdbtnSingle.Text;
                else if (rdbtnBoth.Checked)
                    sdata.ShowUnit = rdbtnBoth.Text;
                if (chkRoundOff.Checked)
                    sdata.RoundOff = 1;
                else
                    sdata.RoundOff = 0;
                if (chkMfgComp.Checked)
                    sdata.ShowMfgComp = "True";
                else
                    sdata.ShowMfgComp = "False";
                if (chkReceiptPayment.Checked == true)
                    sdata.OneTimeReceiptAmountInReport = "True";
                else
                    sdata.OneTimeReceiptAmountInReport = "False";
                if (chkAutoTaxBill.Checked == true)
                    sdata.AutoTaxBillFromEstimate = "True";
                else
                    sdata.AutoTaxBillFromEstimate = "False";
                if (rdbtnportrait.Checked == true)
                    sdata.TaxInvoicePrintOn = "Portrait";
                else if (rdbtnbothprint.Checked == true)
                    sdata.TaxInvoicePrintOn = "Both";
                else if (radMarathi.Checked == true)
                    sdata.TaxInvoicePrintOn = "Marathi";
                else if (radAll.Checked == true)
                    sdata.TaxInvoicePrintOn = "MarathiA6";
                else if (rdbtnhorizontal.Checked == true)
                    sdata.TaxInvoicePrintOn = "HoriZontal";
                else if (radThermal.Checked == true)
                    sdata.TaxInvoicePrintOn = "Thermal";
                else if (radSlab.Checked == true)
                    sdata.TaxInvoicePrintOn = "Slab";
                else if (rdbtnA5.Checked == true)
                    sdata.TaxInvoicePrintOn = "A5";
                else if (rdbtnA6.Checked == true)
                    sdata.TaxInvoicePrintOn = "A6";
                else
                    sdata.TaxInvoicePrintOn = "HorizontalA5";
                if (rdbtnEstA5.Checked == true)
                    sdata.EstimateInvoicePrintOn = "EA5";
                else if (rdbtnEstA6.Checked == true)
                    sdata.EstimateInvoicePrintOn = "EA6";
                else
                    sdata.EstimateInvoicePrintOn = "EMarathiA6";
                if (chkCompanyFullNameShowOnEstimate.Checked == true)
                    sdata.CompanyFullNameOnEstimateBill = "True";
                else
                    sdata.CompanyFullNameOnEstimateBill = "False";
                if (chkDeleteBill.Checked == true)
                    sdata.DeleteSaleBill = "True";
                else
                    sdata.DeleteSaleBill = "False";
                if (chkadvanceinledger.Checked == true)
                    sdata.AdvanceAmtInLedger = "True";
                else
                    sdata.AdvanceAmtInLedger = "False";
                if (chkCompositionscheme.Checked == true)
                    sdata.CompositionScheme = "True";
                else
                    sdata.CompositionScheme = "False";
                if (chkshowAddrerssOnEstimateBill.Checked == true)
                    sdata.AddressShowOnEstimateBill = "True";
                else
                    sdata.AddressShowOnEstimateBill = "False";
                if (chkShowBillinstedEstimalBill.Checked == true)
                    sdata.BillinstedOfEstimateBill = "True";
                else
                    sdata.BillinstedOfEstimateBill = "False";
                if (chkAutoPaidAmount.Checked == true)
                    sdata.AutoPaidAmount = "True";
                else
                    sdata.AutoPaidAmount = "False";
                if (chkBillWiseReceipt.Checked == true)
                    sdata.BillWiseReceipt = "True";
                else
                    sdata.BillWiseReceipt = "False";
                if (chkshowlicenceno.Checked == true)
                    sdata.ShowLicNum = "All";
                else
                    sdata.ShowLicNum = "";
                if (chkThreeUnit.Checked == true)
                    sdata.ThreeUnit = "True";
                else
                    sdata.ThreeUnit = "False";
                if (rdbtnbarcodeThermal.Checked == true)
                {
                    sdata.ShowBarcodePrintOn = "T";
                }
                else if (rdbtnBarcodeA4.Checked == true)
                {
                    sdata.ShowBarcodePrintOn = "A";
                }
                else
                {
                    sdata.ShowBarcodePrintOn = "P";
                }
                sdata.PoweredByName = txtPoweredByName.Text;
                sdata.CompId = CommonMethod.CompId;
                conv_photologo();
                sdata.MDIImage = @photologo;
                sdata.PropriterName = txtproprietorname.Text;
                if (chkMulti.Checked == true)
                    sdata.MultipleCopies = "Y";
                if (!string.IsNullOrEmpty(txtdatabasebackupPath.Text))
                    sdata.DataBaseBackupPath = txtdatabasebackupPath.Text;

                if (chkShowtendercash.Checked == true)
                    sdata.ShowTenderCash = "Y";
                else
                    sdata.ShowTenderCash = "N";
                if (chkqtythreedigit.Checked == true)
                    sdata.ShowQtyThreeDigit = "Y";
                else
                    sdata.ShowQtyThreeDigit = "N";

                if (chkShowUserSeries.Checked == true)
                    sdata.ShowUserDefinedSeries = "Y";
                else
                    sdata.ShowUserDefinedSeries = "N";
                if (chkCommProduct.Checked == true)
                {
                    sdata.CommonProduct = "Y";
                    CommonMethod.commProduct = true;
                }
                else
                {
                    sdata.CommonProduct = "N";
                    CommonMethod.commProduct = false;
                }
                if (chkStockShow.Checked == true)
                    sdata.StockShowOnBill = "Y";
                else
                    sdata.StockShowOnBill = "N";
                if (chkSeparateLedger.Checked == true)
                    sdata.SeparationLedger = "Y";
                else
                    sdata.SeparationLedger = "N";
                if (rdbtnPurLogin.Checked == true)
                    sdata.ShowLoginWindowOnForm = "P";
                else if (rdbtnDailySaleLogin.Checked == true)
                    sdata.ShowLoginWindowOnForm = "D";
                else
                    sdata.ShowLoginWindowOnForm = "";
                if (txtFormPassword.Text != "")
                    sdata.FormPassword = txtFormPassword.Text;
                if (chkApplyMarathiFont.Checked == true)
                    sdata.Applyonmarathifont = "Y";
                else
                    sdata.Applyonmarathifont = "N";
                sdata.SMSURLPath = txtSMSURLPath.Text.Trim();
                if (chkPoultryQuotation.Checked == true)
                    sdata.PoultryQuotations = "Y";
                else
                    sdata.PoultryQuotations = "N";
                if (chkHideOutStanding.Checked == true)
                    sdata.HideOutStandingAmt = "Y";
                else
                    sdata.HideOutStandingAmt = "N";
                if (rdbtn2Decimal.Checked)
                    sdata.RateDecimalPlaces = 2;
                else if (rdbtn3Decimal.Checked)
                    sdata.RateDecimalPlaces = 3;
                else if (rdbtn2Decimal.Checked == false && rdbtn3Decimal.Checked == false)
                    sdata.RateDecimalPlaces = 3;
                if (chkTCS.Checked)
                    sdata.TCSEnabled = true;
                else
                    sdata.TCSEnabled = false;
                if (chkUpdateMRPfromPurchase.Checked)
                    sdata.UpdateMRP = true;
                else
                    sdata.UpdateMRP = false;
                if (chkShowMRPonPrint.Checked)
                    sdata.ShowMRPonPrint = true;
                else
                    sdata.ShowMRPonPrint = false;
                if (chkContinueUserSeries.Checked)
                    sdata.ContinueUserSeries = true;
                else
                    sdata.ContinueUserSeries = false;
                sdata.BarcodePrintTopMargin = Convert.ToInt32(txtTopMargin.Text);
                SettingRepo.Edit(sdata);
                SettingRepo.Save();
                progressBar1.Value = minval + 50;
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPAdjustStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                    command.CommandTimeout = 1000;
                    progressBar1.Value = minval + 60;
                    command.ExecuteNonQuery();
                    db.CloseConnection();
                    progressBar1.Value = minval + 90;
                }
                catch (Exception)
                { db.CloseConnection(); }
                progressBar1.Value = maxval;
                progressBar1.Visible = false;
                MessageBox.Show("Settings are updated successfully.", "Success");
                this.ActiveControl = txtEmailId;
                SettingRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtquantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i >= 46 && i <= 57 || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void txtvatvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i >= 46 && i <= 57 || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void chkSendSMS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SettingRepository SettingRepo = new SettingRepository();
                var data = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (data.SendOption == "Auto")
                    rdbtnauto.Checked = true;
                else
                    rdbtnmannual.Checked = true;
                SettingRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void rdbtnmannual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkAutoBill.Visible = false;
                chkAutoCredit.Visible = false;
                chkAutoFestivals.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmFestivals compinfo = new frmFestivals();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkbillinstruction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmBillInstruction billinstruction = new frmBillInstruction();
                billinstruction.WindowState = System.Windows.Forms.FormWindowState.Normal;
                billinstruction.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                billinstruction.ShowDialog();
            }
            catch (Exception)
            { }
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
                lstPrinterList.Add(mo["Name"].ToString());
                GVPrinterName.Rows.Add(mo["Name"].ToString());
            }
        }

        private void lnkprintername_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                GetAllPrinterList();
                GVPrinterName.Visible = true;
            }
            catch (Exception)
            { }
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                if (string.IsNullOrEmpty(txtPrinterName.Text))
                    lnkprintername.Text = "Add";
                else
                    lnkprintername.Text = "Change";
                tooltipbtn.SetToolTip(this.txtproprietorname, "Seperate Proprietors name by ','");
            }
            catch (Exception)
            { }
        }

        private void GVPrinterName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtPrinterName.Text = Convert.ToString(GVPrinterName.Rows[e.RowIndex].Cells["PrinterNameg"].Value);
                GVPrinterName.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void txtPrinterName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPrinterName.Text))
                    lnkprintername.Text = "Add";
                else
                    lnkprintername.Text = "Change";
            }
            catch (Exception)
            { }
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (btnSave.Enabled != false)
                    e.Cancel = true;
            }
            catch (Exception)
            { }
        }

        private void lnkStateMaster_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmStateMaster statemast = new frmStateMaster();
                statemast.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkGSTmaster_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmGstMaster statemast = new frmGstMaster();
                statemast.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void lnkACGroupMaster_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmAccountGroupMaster ACGMaster = new frmAccountGroupMaster();
                ACGMaster.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                    picLogo.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception)
            { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmBarcodeSetting barsetting = new frmBarcodeSetting();
                barsetting.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnDatabasepath_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string folderName = folderBrowserDialog1.SelectedPath;
                    txtdatabasebackupPath.Text = folderName;
                }
            }
            catch (Exception)
            { }
        }

        private void chkShowtendercash_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowtendercash.Checked == true)
                chkShowtendercash.Text = "Yes";
            else
                chkShowtendercash.Text = "No";
        }

        private void chkqtythreedigit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqtythreedigit.Checked == true)
                chkqtythreedigit.Text = "Yes";
            else
                chkqtythreedigit.Text = "No";
        }

        private void lnklblThermalbillsetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                frmThermalBillSettings thermalBill = new frmThermalBillSettings();
                thermalBill.ShowDialog();
            }
            catch (Exception) { }
        }

        private void lnkInvoiceSeries_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmUserSeries userSeriesWin = new frmUserSeries();
            userSeriesWin.StartPosition = FormStartPosition.CenterScreen;
            userSeriesWin.WindowState = FormWindowState.Normal;
            userSeriesWin.ShowDialog();
        }

        private void lnkMasterSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmMasterSettings masterSettings = new frmMasterSettings();
            masterSettings.StartPosition = FormStartPosition.CenterScreen;
            masterSettings.WindowState = FormWindowState.Normal;
            masterSettings.ShowDialog();
        }

        private void txtEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPassword;
            }
            catch (Exception) { }
        }

        private void txtSmsKey_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSMSUserId;
            }
            catch (Exception) { }
        }

        private void txtSMSUserId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSMSPassword;
            }
            catch (Exception) { }
        }

        private void lnkHistoryDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FrmHistoryDetails history = new FrmHistoryDetails();
                history.ShowDialog();
            }
            catch (Exception) { }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                pnlLogin.Visible = true;
                this.ActiveControl = txtusername;
            }
            catch (Exception) { }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtusername.Text != "" && txtdetailpassword.Text != "")
                {
                    if (txtusername.Text == "ESquareTech" && txtdetailpassword.Text == "admin@123")
                    {
                        pnlOtherSetting.Visible = true;
                        pnlLogin.Visible = false;
                        txtusername.Text = "";
                        txtdetailpassword.Text = "";
                        this.ActiveControl = tblltOtherSettingMain;
                    }
                    else if (txtusername.Text == "Admin" && txtdetailpassword.Text == "ESSUPPORT")
                    {
                        frmTransactionYearDates frmobj = new frmTransactionYearDates();
                        frmobj.ShowDialog();
                        pnlLogin.Visible = false;
                        txtusername.Text = "";
                        txtdetailpassword.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Enter Correct UserName and Password");
                        this.ActiveControl = txtusername;
                    }
                }
                else
                {
                    MessageBox.Show("Enter UserName and Password");
                    this.ActiveControl = txtusername;
                }
            }
            catch (Exception) { }
        }

        private void btnOtherClose_Click(object sender, EventArgs e)
        {
            pnlOtherSetting.Visible = false;
            this.ActiveControl = btnUpdate;
        }

        private void btnLoginClose_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = false;
        }

        private void txtdetailpassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = btnLogin;
            }
            catch (Exception) { }
        }

        private void txtusername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = txtusername;
            }
            catch (Exception) { }
        }

        private void lnkTransactionYearDates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                pnlLogin.Visible = true;
                this.ActiveControl = txtusername;
            }
            catch (Exception) { }
        }

        private void lnkVersionUpdation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DemoVersion demo = new DemoVersion();
                demo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void rdbtnPurLogin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnPurLogin.Checked == true)
                {
                    rdbtnDailySaleLogin.Checked = false;
                    txtFormPassword.Enabled = true;
                }
                else
                    txtFormPassword.Enabled = false;
            }
            catch (Exception)
            { }
        }

        private void rdbtnDailySaleLogin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnDailySaleLogin.Checked == true)
                {
                    rdbtnPurLogin.Checked = false;
                    txtFormPassword.Enabled = true;
                }
                else
                    txtFormPassword.Enabled = false;
            }
            catch (Exception)
            { }
        }
    }
}