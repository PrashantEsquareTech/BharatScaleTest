using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;
using Excel1 = Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using System.Diagnostics;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmPurchaseOrder : Form
    {
        public static SaveFileDialog saveFileDialog = new SaveFileDialog();
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        ToolTip tooltipbtn = new ToolTip();
        List<int> deleteid = new List<int>();
        List<int> DIdListBarcodePrint = new List<int>();
        public static frmPurchaseOrder purorderinfo;
        public static int maxProductCode;
        public decimal totalamt = 0, vat5amt = 0, vat12amt = 0, Pamt = 0, remamt = 0, rdoff = 1, tempPaidamt, totalSGst = 0, totalCGst = 0, totalIGst = 0,
            totalCess = 0, totalqty = 0, TaxableTotal = 0, totalGST = 0, glbTaxableTotal = 0;
        public int flgBatchQty = 0, PurchaeId, printId, colIndex = -1, flag = 0, pno, purmasterid, rcmid, productentrytype = 0, StateId = 0;  // productentrytype -- this variable's type change for krushi,clothing and allinone
        public string ProductCategory = "", barcodestatus, updateBarcode, product_prefix = "", VisibleBatchPanel = "", AdjustPanelString = "", separateLedger = "",
            currentdate = Convert.ToString(DateTime.Now.ToString("M/d/yyyy"));
        public static decimal setvat1 = 0, setvat2 = 0;
        private static bool ismandatory = false, flgmandatory = false, prefixSelected = false, refreshFlag = false, QtySubtitle = false, flgTCSchange = false,
            matrixwithbarcode = false, HideSaleRate = false, showsize = false;
        ProductRepository glbproductrepo = new ProductRepository();
        ProductInformation glbproductdata = new ProductInformation();

        public frmPurchaseOrder()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData.VatOption == "Exclusive")
            {
                setvat1 = Convert.ToDecimal(SetData.Vat1);
                setvat2 = Convert.ToDecimal(SetData.Vat2);
            }
            separateLedger = SetData.SeparationLedger;
            if (SetData.TCSEnabled == true)
            {
                txtTCSPer.Enabled = true;
                txtTCSAmt.Enabled = true;
            }
            else
            {
                txtTCSPer.Enabled = false;
                txtTCSAmt.Enabled = false;
            }
            if (SetData.PuchaseMRP!=null || SetData.PuchaseMRP!=0)
            {
                pnlOther.Visible = true;
            }
            fillsupplier();
            fillGodownName();
            fillbankname();
            fillmfgcomp();
            fillgst();
            maxpurchaseorderid();
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            deleteid.Clear();
            this.ActiveControl = tblltRow1;
            this.ActiveControl = txtsupplierpono;
            
        }

        public frmPurchaseOrder(int PorderId, int rcid)
        {
            PurchaeId = PorderId;
            rcmid = rcid;
            InitializeComponent();
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData != null)
            {
                if (SetData.VatOption == "Exclusive")
                {
                    setvat1 = Convert.ToDecimal(SetData.Vat1);
                    setvat2 = Convert.ToDecimal(SetData.Vat2);
                }
                separateLedger = SetData.SeparationLedger;
                if (SetData.TCSEnabled == true)
                {
                    txtTCSPer.Enabled = true;
                    txtTCSAmt.Enabled = true;
                }
                else
                {
                    txtTCSPer.Enabled = false;
                    txtTCSAmt.Enabled = false;
                }
                
            }
            fillsupplier();
            fillGodownName();
            fillbankname();
            fillmfgcomp();
            fillgst();
            deleteid.Clear();
            maxpurchaseorderid();
            cm.changedatetimepickerrange(this);
            LoadData(PorderId, rcid);
            this.ActiveControl = tblltRow1;
            this.ActiveControl = txtsupplierpono;
        }

        public frmPurchaseOrder(int PorderId)
        {
            PurchaeId = PorderId;
            InitializeComponent();
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData.VatOption == "Exclusive")
            {
                setvat1 = Convert.ToDecimal(SetData.Vat1);
                setvat2 = Convert.ToDecimal(SetData.Vat2);
            }
            if (SetData.TCSEnabled == true)
            {
                txtTCSPer.Enabled = true;
                txtTCSAmt.Enabled = true;
            }
            else
            {
                txtTCSPer.Enabled = false;
                txtTCSAmt.Enabled = false;
            }
            separateLedger = SetData.SeparationLedger;
            if (SetData.DeleteSaleBill == "True")
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
            fillsupplier();
            fillGodownName();
            fillbankname();
            fillmfgcomp();
            fillgst();
            deleteid.Clear();
            maxpurchaseorderid();
            cm.changedatetimepickerrange(this);
            LoadData(PurchaeId);
            this.ActiveControl = tblltRow1;
            this.ActiveControl = txtsupplierpono;
        }

        public void LoadData(int PorderId, int rcid)
        {
            try
            {
                decimal vat_1 = setvat1;
                vat5amt = 0; vat12amt = 0;
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaeId = PorderId;
                var PurchaeData = pmasterrepo.GetAll().Where(t => t.PorderNo == PorderId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtporderno.Text = Convert.ToString(PurchaeData.PorderNo);
                PReturnDetailRepository PRDetailRepo = new PReturnDetailRepository();
                List<PurchaseReturnDetail> PRDetailData = new List<PurchaseReturnDetail>();
                PRDetailData = PRDetailRepo.GetAll().Where(t => t.PurchaseReturnMaster.PorderNo == Convert.ToInt32(PurchaeData.Id) && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).ToList();
                if (PRDetailData.Count > 0)
                    btnreturn.Visible = true;
                else
                    btnreturn.Visible = false;
                txtsupplierpono.Text = Convert.ToString(PurchaeData.SupplierpoNo);
                dtpPorderdate.Text = Convert.ToString(PurchaeData.PorderDate.Value);
                cmbsuppliername.Text = PurchaeData.Suppliername;
                if (PurchaeData.StockIn != null)
                {
                    cmbGodownName.SelectedValue = PurchaeData.StockIn;
                    cmbGodownName.Enabled = false;
                }

                if (PurchaeData.TAXType == "TaxPurchase")
                    chkTPurchase.Checked = true;
                SettingRepository SetRepo = new SettingRepository();
                Setting SetData = new Setting();
                SetData = SetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (SetData != null)
                {
                    if (SetData.RoundOff.ToString() == "0")
                        rdoff = 0;
                    else
                        rdoff = 1;
                }
                else
                    rdoff = 0;
                txtTotalAmt.Text = Convert.ToString(PurchaeData.TotalAmount);
                txtDiscount.Text = Convert.ToString(PurchaeData.Discount);
                txtSpcDisc.Text = Convert.ToString(PurchaeData.SpcDiscount);
                txtMRefNo.Text = Convert.ToString(PurchaeData.ManualPurRefNo);
                ttvat.Text = Convert.ToString(PurchaeData.Vat);
                vat_1 = Convert.ToDecimal(PurchaeData.Vat);
                if (!string.IsNullOrEmpty(Convert.ToString(PurchaeData.TransportGst)))
                {
                    txtGstTransport.Text = Convert.ToString(PurchaeData.TransportGst);
                    txttranscharges.Text = Convert.ToString(PurchaeData.TransportCharges);
                }
                else
                    txtGstPackpercent.Text = "0";
                if (!string.IsNullOrEmpty(Convert.ToString(PurchaeData.PackagingGstAmt)))
                {
                    txtPackaging.Text = Convert.ToString(PurchaeData.PackagingAndForwarding);
                    txtGstPackpercent.Text = Convert.ToString(PurchaeData.PackagingGstPercent);
                    lblpackGST.Text = Convert.ToString(PurchaeData.PackagingGstAmt);
                }
                else
                {
                    txtPackaging.Text = "0";
                    txtGstPackpercent.Text = "0";
                }
                txttranscharges.Text = Convert.ToString(PurchaeData.TransportCharges);
                txtfreightchrgs.Text = Convert.ToString(PurchaeData.Freightchrges);
                txtexciseduty.Text = Convert.ToString(PurchaeData.ExciseDuty);
                PurchaeId = PurchaeData.Id;
                printId = Convert.ToInt32(PurchaeData.PorderNo);
                pno = Convert.ToInt32(txtporderno.Text);

                PurchaseDetailRepository pdetailrepo = new PurchaseDetailRepository();
                var PurchaseDetailData = PurchaeData.PurchaseDetails;
                int i = 0;
                TaxableTotal = 0;
                foreach (var item in PurchaseDetailData)
                {
                    if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                    {
                        GvProductInfo.Rows.Add();
                        GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
                        GvProductInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                        GvProductInfo.Rows[i].Cells["Unit"].Value = item.Unit;
                        if (item.PurchaseUnit != null)
                            GvProductInfo.Rows[i].Cells["PurUnit"].Value = item.PurchaseUnit;
                        else
                            GvProductInfo.Rows[i].Cells["PurUnit"].Value = item.Unit;
                        decimal rate = 1;
                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (item.Unit != item.PurchaseUnit && item.PurchaseUnit != null)
                        {
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            decimal qty, frqty;
                            if (item.PurchaseUnit == productdata.AlternateUnit)
                            {
                                qty = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                frqty = Convert.ToDecimal(item.FreeQty) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                GvProductInfo.Rows[i].Cells["Quantityg"].Value = qty;
                                GvProductInfo.Rows[i].Cells["FrQty"].Value = frqty;
                                rate = Convert.ToDecimal(productdata.AlternateUnitRate);
                                GvProductInfo.Rows[i].Cells["SaleRate"].Value = Convert.ToDecimal(item.SaleRate) * Convert.ToDecimal(productdata.UnitQty);
                            }
                            else if (item.PurchaseUnit == productdata.TripleUnit)
                            {
                                qty = Convert.ToDecimal(item.Quantity) / ((Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)) * (Convert.ToDecimal(productdata.SecondUnitQty) / Convert.ToDecimal(productdata.TripleUnitQty)));
                                frqty = Convert.ToDecimal(item.FreeQty) / ((Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)) * (Convert.ToDecimal(productdata.SecondUnitQty) / Convert.ToDecimal(productdata.TripleUnitQty)));
                                GvProductInfo.Rows[i].Cells["Quantityg"].Value = qty;
                                GvProductInfo.Rows[i].Cells["FrQty"].Value = frqty;
                                rate = Convert.ToDecimal(productdata.AlternateUnitRate) * Convert.ToDecimal(productdata.SecondUnitQty);
                                GvProductInfo.Rows[i].Cells["SaleRate"].Value = Convert.ToDecimal(item.SaleRate) * ((Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)) * (Convert.ToDecimal(productdata.SecondUnitQty) / Convert.ToDecimal(productdata.TripleUnitQty)));
                            }
                        }
                        else
                        {
                            GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDouble(item.Quantity);
                            GvProductInfo.Rows[i].Cells["FrQty"].Value = Convert.ToDouble(item.FreeQty);
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            rate = Convert.ToDecimal(productdata.PurchaseRate);
                            GvProductInfo.Rows[i].Cells["SaleRate"].Value = item.SaleRate;
                        }
                        decimal pdiscamt = 0, pspdscamt = 0, sgstamt = 0, cgstamt = 0, igstamt = 0, cessamt = 0;
                        decimal amount = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value) * rate;
                        GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                        GvProductInfo.Rows[i].Cells["Price"].Value = Convert.ToString(Math.Round(rate, 2, MidpointRounding.AwayFromZero));
                        GvProductInfo.Rows[i].Cells["Amount"].Value = Convert.ToString(Math.Round(amount, 2, MidpointRounding.AwayFromZero));
                        GvProductInfo.Rows[i].Cells["Disc"].Value = productdata.ProductDiscount;
                        pdiscamt = amount * (Convert.ToDecimal(productdata.ProductDiscount) / 100);
                        if (item.SpcDisc.ToString() != "")
                        {
                            GvProductInfo.Rows[i].Cells["SpDisc"].Value = item.SpcDisc;
                            pspdscamt = (amount - pdiscamt) * (Convert.ToDecimal(item.SpcDisc) / 100);
                        }
                        else
                        {
                            GvProductInfo.Rows[i].Cells["SpDisc"].Value = 0;
                            pspdscamt = 0;
                        }
                        amount = amount - pdiscamt - pspdscamt;
                        GvProductInfo.Rows[i].Cells["SGST"].Value = productdata.SGstPercent;
                        sgstamt = amount * (Convert.ToDecimal(productdata.SGstPercent) / 100);
                        GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = sgstamt;
                        GvProductInfo.Rows[i].Cells["CGST"].Value = productdata.CGstPercent;
                        cgstamt = amount * (Convert.ToDecimal(productdata.CGstPercent) / 100);
                        GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = cgstamt;
                        GvProductInfo.Rows[i].Cells["IGST"].Value = productdata.IGstPercent;
                        igstamt = amount * (Convert.ToDecimal(productdata.IGstPercent) / 100);
                        GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = igstamt;
                        GvProductInfo.Rows[i].Cells["Cess"].Value = productdata.Cess;
                        GvProductInfo.Rows[i].Cells["TCSPer"].Value = 0;
                        GvProductInfo.Rows[i].Cells["TCSAmt"].Value = 0;
                        cessamt = amount * (Convert.ToDecimal(productdata.IGstPercent) / 100);
                        amount = amount + sgstamt + cgstamt + igstamt + cessamt;
                        GvProductInfo.Rows[i].Cells["TotalAmount"].Value = Convert.ToString(Math.Round(amount, 2, MidpointRounding.AwayFromZero));
                        GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value = item.HasMandatoryCodeSeries;
                        GvProductInfo.Rows[i].Cells["CessAmt"].Value = cessamt;
                        if (item.Barcode != null)
                            GvProductInfo.Rows[i].Cells["Barcode"].Value = item.Barcode;
                        else
                            GvProductInfo.Rows[i].Cells["Barcode"].Value = "";
                        GvProductInfo.Rows[i].Cells["StaticBarcode"].Value = item.StaticBarcode;
                        if (!string.IsNullOrEmpty(item.BatchNo))
                            GvProductInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                        if (item.ExpireDate != null)
                            GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = item.ExpireDate;
                        if (item.ThreeLevelDiscPer != null)
                            GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value = item.ThreeLevelDiscPer;
                        else
                            GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value = "0";
                        if (item.TotalQuantitySubtitle != null)
                            GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value = item.TotalQuantitySubtitle;
                        else
                            GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value = "0";
                        GvProductInfo.Rows[i].Cells["Sizep"].Value = item.Size;
                        GvProductInfo.Rows[i].Cells["Department"].Value = item.DeptName;
                        GvProductInfo.Rows[i].Cells["Category"].Value = item.CategoryName;
                        //GvProductInfo.Rows[i].Cells[31].Value = item.Commission;
                        //GvProductInfo.Rows[i].Cells[32].Value = item.CommRs;
                        GvProductInfo.Rows[i].Cells["color"].Value = item.ColorName;

                        GvProductInfo.Rows[i].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                        productrepo.Dispose();
                        i++;
                    }
                }
                totalamt = 0;
                totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                totalSGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SGSTAmt"].Value));
                totalCGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CGSTAmt"].Value));
                totalIGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["IGSTAmt"].Value));
                totalCess = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CessAmt"].Value));
                totalGST = totalCGst + totalIGst + totalSGst;
                txtTotalGST.Text = Convert.ToString(totalGST);
                txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                TaxableTotal = (totalamt - (totalSGst + totalCGst + totalIGst + totalCess));
                ttnetamt.Text = txtTotalAmt.Text;
                txtPaidAmt.Text = Convert.ToString(0);
                txtRemainingAmt.Text = Convert.ToString(ttnetamt.Text);
                SupplierRemainingPaymentRepository suppremRepo = new SupplierRemainingPaymentRepository();
                var remdata = suppremRepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (remdata != null)
                {
                    if (remdata.PaymentMode == "Cheque")
                    {
                        cmbbank.Enabled = true;
                        txtchequeno.Enabled = true;
                        dtpchequedate.Enabled = true;
                        label42.Enabled = true;
                        label43.Enabled = true;
                        label44.Enabled = true;
                        chkcheque.Checked = true;
                        cmbbank.Text = remdata.Bankname;
                        txtchequeno.Text = remdata.Chequeno;
                        dtpchequedate.Value = remdata.CreateDate.Value;
                    }
                    else
                    {
                        cmbbank.Enabled = false;
                        txtchequeno.Enabled = false;
                        dtpchequedate.Enabled = false;
                        label42.Enabled = false;
                        label43.Enabled = false;
                        label44.Enabled = false;
                    }
                }
                txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
                if (txtGstTransport.Text != "" && Convert.ToDecimal(txtGstTransport.Text) != 0)
                    chkGstTransport.Checked = true;
                if (lblpackGST.Text != "" && Convert.ToDecimal(lblpackGST.Text) != 0)
                    chkGstPack.Checked = true;
                else
                    chkGstPack.Checked = false;
                pmasterrepo.Dispose(); PRDetailRepo.Dispose(); suppremRepo.Dispose();
                pdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void LoadData(int PorderId)
        {
            try
            {
                decimal vat_1 = 0;
                vat5amt = 0; vat12amt = 0;
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaeId = PorderId;
                var PurchaeData = pmasterrepo.GetAll().Where(t => t.PorderNo == PorderId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtporderno.Text = Convert.ToString(PurchaeData.PorderNo);
                PReturnDetailRepository PRDetailRepo = new PReturnDetailRepository();
                List<PurchaseReturnDetail> PRDetailData = new List<PurchaseReturnDetail>();
                PRDetailData = PRDetailRepo.GetAll().Where(t => t.PurchaseReturnMaster.PorderNo == Convert.ToInt32(txtporderno.Text) && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).OrderBy(t => t.Id).ToList();
                if (PRDetailData.Count > 0)
                    btnreturn.Visible = true;
                else
                    btnreturn.Visible = false;
                txtsupplierpono.Text = Convert.ToString(PurchaeData.SupplierpoNo);
                dtpPorderdate.Text = Convert.ToString(PurchaeData.PorderDate.Value);
                cmbsuppliername.Text = PurchaeData.Suppliername;
                if (PurchaeData.StockIn != null)
                {
                    cmbGodownName.SelectedValue = PurchaeData.StockIn;
                    cmbGodownName.Enabled = false;
                }
                if (PurchaeData.TAXType == "TaxPurchase")
                    chkTPurchase.Checked = true;
                if (PurchaeData.RoundOff != null)
                {
                    txtRoundOff.Text = Convert.ToString(PurchaeData.RoundOff);
                    rdoff = 1;
                }
                else
                    rdoff = 0;
                txtTotalAmt.Text = Convert.ToString(PurchaeData.TotalAmount);
                txtDiscount.Text = Convert.ToString(PurchaeData.Discount);
                txtSpcDisc.Text = Convert.ToString(PurchaeData.SpcDiscount);
                ttvat.Text = Convert.ToString(PurchaeData.Vat);
                vat_1 = Convert.ToDecimal(PurchaeData.Vat);
                txttranscharges.Text = Convert.ToString(PurchaeData.TransportCharges);
                PurchaeId = PurchaeData.Id;
                printId = Convert.ToInt32(PurchaeData.PorderNo);
                pno = Convert.ToInt32(txtporderno.Text);
                tempPaidamt = Convert.ToDecimal(PurchaeData.PaidAmount);
                txtMRefNo.Text = Convert.ToString(PurchaeData.ManualPurRefNo);
                if (PurchaeData.TransportGst != null)
                {
                    txtGstTransport.Text = Convert.ToString(PurchaeData.TransportGst);
                    txttranscharges.Text = Convert.ToString(PurchaeData.TransportCharges);
                }
                else
                    txtGstTransport.Text = "0";
                if (PurchaeData.PackagingGstAmt != null)
                {
                    txtPackaging.Text = Convert.ToString(PurchaeData.PackagingAndForwarding);
                    txtGstPackpercent.Text = Convert.ToString(PurchaeData.PackagingGstPercent);
                    lblpackGST.Text = Convert.ToString(PurchaeData.PackagingGstAmt);
                }
                else
                {
                    txtPackaging.Text = "0";
                    txtGstPackpercent.Text = "0";
                }
                txtfreightchrgs.Text = Convert.ToString(PurchaeData.Freightchrges);
                txtexciseduty.Text = Convert.ToString(PurchaeData.ExciseDuty);
                if (PurchaeData.Narration == null || PurchaeData.Narration == "")
                    txtNarration.Text = "";
                else
                    txtNarration.Text = Convert.ToString(PurchaeData.Narration);
                PurchaseDetailRepository pdetailrepo = new PurchaseDetailRepository();
                var PurchaseDetailData = PurchaeData.PurchaseDetails;
                int i = 0;
                TaxableTotal = 0;
                foreach (var item in PurchaseDetailData)
                {
                    try
                    {
                        if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                        {
                            GvProductInfo.Rows.Add();
                            GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                            GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
                            GvProductInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                            GvProductInfo.Rows[i].Cells["Unit"].Value = item.Unit;
                            if (item.PurchaseUnit != null)
                                GvProductInfo.Rows[i].Cells["PurUnit"].Value = item.PurchaseUnit;
                            else
                                GvProductInfo.Rows[i].Cells["PurUnit"].Value = item.Unit;
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (item.Unit != item.PurchaseUnit && item.PurchaseUnit != null)
                            {
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                else
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                                decimal qty, frqty;
                                if (item.PurchaseUnit == productdata.AlternateUnit)
                                {
                                    qty = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                    frqty = Convert.ToDecimal(item.FreeQty) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                    GvProductInfo.Rows[i].Cells["Quantityg"].Value = qty;
                                    GvProductInfo.Rows[i].Cells["FrQty"].Value = frqty;
                                    GvProductInfo.Rows[i].Cells["SaleRate"].Value = Convert.ToDecimal(item.SaleRate) * Convert.ToDecimal(productdata.UnitQty);
                                }
                                else if (item.PurchaseUnit == productdata.TripleUnit)
                                {
                                    qty = Convert.ToDecimal(item.Quantity) / ((Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)) * (Convert.ToDecimal(productdata.SecondUnitQty) / Convert.ToDecimal(productdata.TripleUnitQty)));
                                    frqty = Convert.ToDecimal(item.FreeQty) / ((Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)) * (Convert.ToDecimal(productdata.SecondUnitQty) / Convert.ToDecimal(productdata.TripleUnitQty)));
                                    GvProductInfo.Rows[i].Cells["Quantityg"].Value = qty;
                                    GvProductInfo.Rows[i].Cells["FrQty"].Value = frqty;
                                    GvProductInfo.Rows[i].Cells["SaleRate"].Value = Convert.ToDecimal(item.SaleRate) * ((Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)) * (Convert.ToDecimal(productdata.SecondUnitQty) / Convert.ToDecimal(productdata.TripleUnitQty)));
                                }
                            }
                            else
                            {
                                GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDouble(item.Quantity);
                                GvProductInfo.Rows[i].Cells["FrQty"].Value = Convert.ToDouble(item.FreeQty);
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                else
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                GvProductInfo.Rows[i].Cells["SaleRate"].Value = item.SaleRate;
                            }
                            GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                            GvProductInfo.Rows[i].Cells["Price"].Value = item.PurchasePrice;
                            GvProductInfo.Rows[i].Cells["Amount"].Value = item.Amount;
                            GvProductInfo.Rows[i].Cells["Disc"].Value = item.Discount;
                            GvProductInfo.Rows[i].Cells["SpDisc"].Value = item.SpcDisc;
                            GvProductInfo.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                            GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                            GvProductInfo.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                            GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                            GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                            GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                            GvProductInfo.Rows[i].Cells["Cess"].Value = item.Cess;
                            GvProductInfo.Rows[i].Cells["TCSPer"].Value = item.TCSPer;
                            GvProductInfo.Rows[i].Cells["TCSAmt"].Value = item.TCSAmt;
                            GvProductInfo.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;
                            GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            if (item.HasMandatoryCodeSeries == "Y")
                                GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value = "Y";
                            else
                                GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value = "N";
                            GvProductInfo.Rows[i].Cells["CessAmt"].Value = item.CessAmt;
                            if (item.Barcode != null)
                                GvProductInfo.Rows[i].Cells["Barcode"].Value = item.Barcode;
                            else
                                GvProductInfo.Rows[i].Cells["Barcode"].Value = "";
                            GvProductInfo.Rows[i].Cells["StaticBarcode"].Value = item.StaticBarcode;
                            if (!string.IsNullOrEmpty(item.BatchNo))
                                GvProductInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                            if (item.ExpireDate != null)
                                GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = item.ExpireDate;
                            if (item.ThreeLevelDiscPer != null)
                                GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value = item.ThreeLevelDiscPer;
                            else
                                GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value = "0";
                            if (item.TotalQuantitySubtitle != null)
                                GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value = item.TotalQuantitySubtitle;
                            else
                                GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value = "0";
                            GvProductInfo.Rows[i].Cells["Sizep"].Value = item.Size;
                            GvProductInfo.Rows[i].Cells["Department"].Value = item.DeptName;
                            GvProductInfo.Rows[i].Cells["Category"].Value = item.CategoryName;
                            //GvProductInfo.Rows[i].Cells[31].Value = item.Commission;
                            //GvProductInfo.Rows[i].Cells[32].Value = item.CommRs;
                            GvProductInfo.Rows[i].Cells["Color"].Value = item.ColorName;
                            GvProductInfo.Rows[i].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                            TaxableTotal = TaxableTotal + Convert.ToDecimal(item.Amount);
                            if (Convert.ToDecimal(item.Vat) != 0)
                            {
                                decimal discamt = 0, vatamt = 0;
                                if (Convert.ToDecimal(item.Discount) != 0)
                                    discamt = Convert.ToDecimal(item.Amount) * Convert.ToDecimal(item.Discount) / 100;
                                if (Convert.ToDecimal(item.Vat) == vat_1)
                                {
                                    setvat1 = Convert.ToDecimal(item.Vat);
                                    vatamt = (Convert.ToDecimal(item.Amount) - discamt) * Convert.ToDecimal(item.Vat) / 100;
                                    vat5amt += vatamt;
                                }
                                else if (Convert.ToDecimal(item.Vat) != vat_1 && Convert.ToDecimal(item.Vat) != 0)
                                {
                                    setvat2 = Convert.ToDecimal(item.Vat);
                                    vatamt = (Convert.ToDecimal(item.Amount) - discamt) * Convert.ToDecimal(item.Vat) / 100;
                                    vat12amt += vatamt;
                                }
                            }
                            i++;
                        }
                    }
                    catch (Exception)
                    { }
                    totalamt = 0;
                    totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                    totalSGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SGSTAmt"].Value));
                    totalCGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CGSTAmt"].Value));
                    totalIGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["IGSTAmt"].Value));
                    totalCess = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CessAmt"].Value));

                    totalGST = totalCGst + totalIGst + totalSGst;
                    txtTotalGST.Text = Convert.ToString(totalGST);
                    TaxableTotal = (totalamt - (totalSGst + totalCGst + totalIGst + totalCess));
                }
                if (vat5amt != 0)
                {
                    if (vat12amt != 0)
                    {
                        lbl2vatamt.Text = Convert.ToString(Math.Round(vat12amt, 2, MidpointRounding.AwayFromZero));
                    }
                    lblvat.Text = Convert.ToString(Math.Round(vat5amt, 2, MidpointRounding.AwayFromZero));
                    ttvat.Text = setvat1.ToString();
                    ttvat.Enabled = false;
                }
                else if (vat12amt != 0)
                {
                    lblvat.Text = Convert.ToString(Math.Round(vat12amt, 2, MidpointRounding.AwayFromZero));
                    ttvat.Text = setvat2.ToString();
                    ttvat.Enabled = false;
                }
                ttnetamt.Text = Convert.ToString(PurchaeData.NetAmount);
                txtPaidAmt.Text = Convert.ToString(PurchaeData.PaidAmount);
                txtRemainingAmt.Text = Convert.ToString(PurchaeData.RemainingAmount);
                SupplierRemainingPaymentRepository suppremRepo = new SupplierRemainingPaymentRepository();
                var remdata = suppremRepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.PaymentMode == "Cheque" && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (remdata != null)
                {
                    if (remdata.PaymentMode == "Cheque")
                    {
                        label42.Enabled = true;
                        label43.Enabled = true;
                        label44.Enabled = true;
                        chkcheque.Checked = true;
                        chkcheque.Enabled = false;
                        cmbbank.Enabled = false;
                        cmbbank.Text = remdata.Bankname;
                        txtchequeno.Text = remdata.Chequeno;
                        dtpchequedate.Value = remdata.CreateDate.Value;
                    }
                    else
                    {
                        cmbbank.Enabled = false;
                        txtchequeno.Enabled = false;
                        dtpchequedate.Enabled = false;
                        label42.Enabled = false;
                        label43.Enabled = false;
                        label44.Enabled = false;
                    }
                }
                if (txtGstTransport.Text != "" && Convert.ToDecimal(txtGstTransport.Text) != 0)
                    chkGstTransport.Checked = true;
                else
                    chkGstTransport.Checked = false;

                if (lblpackGST.Text != "" && Convert.ToDecimal(lblpackGST.Text) != 0)
                    chkGstPack.Checked = true;
                else
                    chkGstPack.Checked = false;
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                if (btnSave.Enabled == false)
                {
                    SettingRepository settRepo = new SettingRepository();
                    Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.DeleteSaleBill == "True")
                        btnDelete.Enabled = true;
                    else
                        btnDelete.Enabled = false;
                }
                pmasterrepo.Dispose(); PRDetailRepo.Dispose(); suppremRepo.Dispose(); //PRMRepo.Dispose();
                pdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxpurchaseorderid()
        {
            try
            {
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var mid = pmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.PorderNo);
                if (mid == null)
                    txtporderno.Text = "1";
                else
                    txtporderno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillsupplier()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> sdata = new List<SupplierInformation>();
                sdata.Clear();
                if (CommonMethod.commProduct == true)
                    sdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    sdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                sdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                sdata.Reverse();
                cmbsuppliername.DataSource = sdata;
                cmbsuppliername.ValueMember = "SupplierCode";
                cmbsuppliername.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillgst()
        {
            try
            {
                DataTable dtsgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtsgst.Rows.Count > 0)
                {
                    cmbsgst.ValueMember = "GSTNo";
                    cmbsgst.DisplayMember = "GSTPercent";
                    cmbsgst.DataSource = dtsgst;
                }
                DataTable dtcgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtcgst.Rows.Count > 0)
                {
                    cmbcgst.ValueMember = "GSTNo";
                    cmbcgst.DisplayMember = "GSTPercent";
                    cmbcgst.DataSource = dtcgst;
                }
                DataTable dtigst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst.Rows.Count > 0)
                {
                    cmbigst.ValueMember = "GSTNo";
                    cmbigst.DisplayMember = "GSTPercent";
                    cmbigst.DataSource = dtigst;
                }
            }
            catch (Exception) { }
        }

        public void fillGodownName()
        {
            try
            {
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll().OrderByDescending(t => t.GodownName).ToList();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GodownName).ToList();
                if (GDData.Count > 0)
                {
                    GDData.Add(new GodownMaster { GodownName = "Shop", ID = 0 });
                    GDData.Reverse();
                    cmbGodownName.DataSource = GDData;
                    cmbGodownName.ValueMember = "ID";
                    cmbGodownName.DisplayMember = "GodownName";
                    cmbGodownName.Enabled = true;
                    GDRepo.Dispose();
                }
                else
                {
                    cmbGodownName.DataSource = null;
                    cmbGodownName.Items.Clear();
                    cmbGodownName.DisplayMember = "Text";
                    cmbGodownName.ValueMember = "Value";
                    cmbGodownName.Items.Add(new { Text = "Shop", Value = 0 });
                    cmbGodownName.SelectedIndex = 0;
                    cmbGodownName.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        public void fillmfgcomp()
        {
            try
            {
                ProductRepository custrepo = new ProductRepository();
                List<ProductInformation> customerdata = new List<ProductInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = custrepo.GetAll().Where(t => t.Prefix != null && t.Prefix != "").GroupBy(t => t.Prefix).Select(t => t.FirstOrDefault()).OrderByDescending(t => t.Prefix).ToList();
                else
                    customerdata = custrepo.GetAll().Where(t => t.Prefix != null && t.Prefix != "" && t.CompId == CommonMethod.CompId).GroupBy(t => t.Prefix).Select(t => t.FirstOrDefault()).OrderByDescending(t => t.Prefix).ToList();
                customerdata.Add(new ProductInformation { Prefix = "Select", ProductCode = 0 });
                customerdata.Reverse();
                cmbcomanyname.DataSource = customerdata;
                cmbcomanyname.ValueMember = "ProductCode";
                cmbcomanyname.DisplayMember = "Prefix";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillbankname()
        {
            try
            {
                BankRepository BankRepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = BankRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank.DataSource = bankdata;
                cmbbank.ValueMember = "BankId";
                cmbbank.DisplayMember = "Bankname";
                BankRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        public void clearAdd()
        {
            txtpname.Text = "";
            txtQuantity.Text = "0";
            txtamount.Text = "";
            txtpurchaseprice.Text = "";
            txtFreeQty.Text = "0";
            txtpdiscount.Text = "0";
            txtpTotalAmt.Text = "";
            txt3LevelDisvPer.Text = "0";
            txt3LevelDiscAmt.Text = "";
            txtpSpcDisc.Text = "0";
            txtpCess.Text = "0";
            txtHSN.Text = "";
            fillgst();
            txtTCSAmt.Text = "0";
            txtTCSPer.Text = "0";
            totalamt = 0;
            showsize = false;
            txtstaticBarcode.Text = "";
            txtstaticBarcode.Enabled = false;
            txtSaleRate.Text = "0";
            txtBatchNo.Text = "";
            HideSaleRate = false;
            matrixwithbarcode = false;
            dtpExpiry.Value = DateTime.Now.Date;
            dtgvSize.Rows.Clear();
            txtsizefrom.Text = "0";
            txtsizeto.Text = "0";
            txtpcommRs.Text = "0";
            txtpcomm.Text = "0";
            txtsizegap.Text = "0";
            txtSaleRate.Enabled = false;
            txtQuantity.Enabled = true;
            updateBarcode = string.Empty;
            barcodestatus = string.Empty;
            txtQuantitySubtitle.Text = "0";
            pnlQtySubtitle.Visible = false;
            dtgvcode.Visible = false;
            cmbUnit.Items.Clear();
            cmbUnit.DisplayMember = "Text";
            cmbUnit.ValueMember = "Value";
            if (cmbcomanyname.Items.Count > 2)
                this.ActiveControl = cmbcomanyname;
            else
                this.ActiveControl = txtpname;
            colIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            try
            {
                if (cmbsuppliername.Text != "Select" && txtpname.Text.Trim() != "" && txtQuantity.Text != "" && txtpurchaseprice.Text != "")
                {
                    //------------- integration on 05032018
                    if (productentrytype == 1) //krushi project with batch and expiry 
                    {
                        if (txtBatchNo.Text.Trim() == "")
                        {
                            isValid = false;
                            MessageBox.Show("Enter Batch Number.", "Warning");
                            AdjustPanelDetails(productentrytype);
                            this.ActiveControl = txtBatchNo;
                        }
                        if (dtpExpiry.Value.Date <= dtpPorderdate.Value.Date)
                        {
                            isValid = false;
                            MessageBox.Show("Expiry Date must be greater than Todays Date.", "Warning");
                            AdjustPanelDetails(productentrytype);
                            this.ActiveControl = dtpExpiry;
                        }
                    }
                    else if (productentrytype == 2) //krushi project with batch
                    {
                        if (txtBatchNo.Text.Trim() == "")
                        {
                            isValid = false;
                            MessageBox.Show("Enter Batch Number.", "Warning");
                            AdjustPanelDetails(productentrytype);
                            this.ActiveControl = txtBatchNo;
                        }
                    }
                    else if (productentrytype == 3)
                    {
                        if (colIndex == -1)
                        {
                            decimal totalqty = dtgvSize.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells[1].Value));
                            if (Convert.ToDecimal(txtQuantity.Text) != totalqty)
                            {
                                MessageBox.Show("Size are not distributed properly", "Warning");
                                dtgvSize.Visible = true;
                                dtgvSize.Focus();
                                goto a;
                            }
                        }
                    }
                    //---------------------------
                    if (ismandatory == true)
                    {
                        for (int i = 0; i < dtgvcode.RowCount; i++)
                        {
                            if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                            {
                                MessageBox.Show("All Quantity must have Serial No or Code.", "Warning");
                                dtgvcode.Visible = true;
                                this.ActiveControl = dtgvcode;
                                dtgvcode.CurrentCell = dtgvcode[0, i];
                                dtgvcode.CurrentCell.Selected = true;
                                goto a;
                            }
                        }
                    }
                    if (matrixwithbarcode == false && (barcodestatus == "Our Barcode" || barcodestatus == "Static Barcode"))
                    {
                        if (txtSaleRate.Text == "0")
                        {
                            MessageBox.Show("Product must have SaleRate.", "Warning");
                            this.ActiveControl = txtSaleRate;
                            goto a;
                        }
                        else if (barcodestatus == "Static Barcode" && txtstaticBarcode.Text == "")
                        {
                            MessageBox.Show("Product must have Static Barcode.", "Warning");
                            this.ActiveControl = txtstaticBarcode;
                            goto a;
                        }
                    }
                    if (colIndex != -1 && isValid == true)
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];
                        int i = GvProductInfo.RowCount;
                        GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value = Proname;
                        GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                        GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[colIndex].Cells["PurUnit"].Value = cmbUnit.Text;
                        GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value = txtQuantity.Text;
                        if (txtFreeQty.Text == " ")
                            GvProductInfo.Rows[colIndex].Cells["FrQty"].Value = 0;
                        else
                            GvProductInfo.Rows[colIndex].Cells["FrQty"].Value = txtFreeQty.Text;
                        if (ProductCategory == "Motor")
                        {
                            if (GvProductInfo.RowCount > 0)
                            {
                                for (int k = 0; k < GvProductInfo.RowCount; k++)
                                {
                                    if (colIndex != k && GvProductInfo.Rows[k].Cells["Code"].Value.ToString() != "")
                                    {
                                        if (GvProductInfo.Rows[k].Cells["Code"].Value.ToString() == dtgvcode.Rows[0].Cells[0].Value.ToString() && GvProductInfo.Rows[k].Cells["ProductNameg"].Value.ToString() == GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value.ToString() && GvProductInfo.Rows[k].Cells["Company"].Value.ToString() == GvProductInfo.Rows[colIndex].Cells["Company"].Value.ToString() && GvProductInfo.Rows[k].Cells["Unit"].Value.ToString() == GvProductInfo.Rows[colIndex].Cells["Unit"].Value.ToString())
                                        {
                                            MessageBox.Show("You already entered this product.", "Warning");
                                            goto s;
                                        }
                                    }
                                }
                            }
                            if (ismandatory == true)
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = dtgvcode.Rows[0].Cells[0].Value.ToString();
                            else
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = "";
                        }
                        else
                            GvProductInfo.Rows[colIndex].Cells["Code"].Value = "";
                        GvProductInfo.Rows[colIndex].Cells["Price"].Value = Convert.ToDecimal(txtpurchaseprice.Text);
                        GvProductInfo.Rows[colIndex].Cells["Amount"].Value = Convert.ToDecimal(txtamount.Text);
                        if (txtpdiscount.Text == "")
                            GvProductInfo.Rows[colIndex].Cells["Disc"].Value = 0;
                        else
                            GvProductInfo.Rows[colIndex].Cells["Disc"].Value = txtpdiscount.Text;
                        if (txtpSpcDisc.Text == "")
                            GvProductInfo.Rows[colIndex].Cells["SpDisc"].Value = 0;
                        else
                            GvProductInfo.Rows[colIndex].Cells["SpDisc"].Value = txtpSpcDisc.Text;
                        if (cmbsgst.Text == "")
                            GvProductInfo.Rows[colIndex].Cells["SGST"].Value = 0;
                        else
                            GvProductInfo.Rows[colIndex].Cells["SGST"].Value = cmbsgst.Text;
                        GvProductInfo.Rows[colIndex].Cells["SGSTAmt"].Value = lblpvat.Text;
                        GvProductInfo.Rows[colIndex].Cells["CGST"].Value = cmbcgst.Text;
                        GvProductInfo.Rows[colIndex].Cells["CGSTAmt"].Value = lblCGst.Text;
                        GvProductInfo.Rows[colIndex].Cells["IGST"].Value = cmbigst.Text;
                        GvProductInfo.Rows[colIndex].Cells["IGSTAmt"].Value = lblIGst.Text;
                        GvProductInfo.Rows[colIndex].Cells["Cess"].Value = txtpCess.Text;
                        GvProductInfo.Rows[colIndex].Cells["TotalAmount"].Value = txtpTotalAmt.Text;
                        GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        if (flgmandatory == true)
                            GvProductInfo.Rows[colIndex].Cells["MandatoryCodeSeries"].Value = "Y";
                        else
                            GvProductInfo.Rows[colIndex].Cells["MandatoryCodeSeries"].Value = "N";
                        GvProductInfo.Rows[colIndex].Cells["CessAmt"].Value = lblpCessAmt.Text;
                        if (barcodestatus == "Our Barcode")
                        {
                            if (updateBarcode == "")
                                GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = generateBarcode();
                            else
                                GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = updateBarcode;
                        }
                        else
                            GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = string.Empty;
                        GvProductInfo.Rows[colIndex].Cells["SaleRate"].Value = txtSaleRate.Text;
                        GvProductInfo.Rows[colIndex].Cells["StaticBarcode"].Value = txtstaticBarcode.Text;
                        if (txt3LevelDisvPer.Text != "")
                            GvProductInfo.Rows[colIndex].Cells["ThreeLevelDisc"].Value = txt3LevelDisvPer.Text;
                        else
                            GvProductInfo.Rows[colIndex].Cells["ThreeLevelDisc"].Value = "0";
                        if (txt3LevelDiscAmt.Text != "")
                            GvProductInfo.Rows[colIndex].Cells["ThreeLevelDiscAmt"].Value = txt3LevelDiscAmt.Text;
                        else
                            GvProductInfo.Rows[colIndex].Cells["ThreeLevelDiscAmt"].Value = "0";
                        if (txtQuantitySubtitle.Text != "")
                            GvProductInfo.Rows[colIndex].Cells["QuantitySubtitle"].Value = txtQuantitySubtitle.Text;
                        else
                            GvProductInfo.Rows[colIndex].Cells["QuantitySubtitle"].Value = "0";
                        if (productentrytype == 1 || productentrytype == 2) //1- with batch and expiry
                        {
                            GvProductInfo.Rows[colIndex].Cells["BatchNo"].Value = txtBatchNo.Text;
                            if (productentrytype != 2)  // without expiry
                                GvProductInfo.Rows[colIndex].Cells["ExpiryDate"].Value = dtpExpiry.Value.ToString("dd/MM/yyyy");
                        }
                        GvProductInfo.Rows[colIndex].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                        if (txtTCSPer.Text == "")
                            GvProductInfo.Rows[colIndex].Cells["TCSPer"].Value = 0;
                        else
                            GvProductInfo.Rows[colIndex].Cells["TCSPer"].Value = txtTCSPer.Text;
                        if (txtTCSAmt.Text == "")
                            GvProductInfo.Rows[colIndex].Cells["TCSAmt"].Value = 0;
                        else
                            GvProductInfo.Rows[colIndex].Cells["TCSAmt"].Value = txtTCSAmt.Text;
                    s:
                        {
                            totalamt = 0;
                            totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                            totalSGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SGSTAmt"].Value));
                            totalCGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CGSTAmt"].Value));
                            totalIGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["IGSTAmt"].Value));
                            totalCess = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CessAmt"].Value));
                            totalqty = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["Quantityg"].Value));
                            totalGST = totalSGst + totalCGst + totalIGst;
                            txtTotalGST.Text = Convert.ToString(totalGST);
                            TaxableTotal = totalamt - (totalSGst + totalCGst + totalIGst + totalCess);
                            txtTotalAmt.Text = Convert.ToString(totalamt);
                            UpdateMRP(Proname, company, unit);
                            CalculateNetAmt();
                            clearAdd();
                        }
                    }
                    else if (isValid == true)
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];

                        if (ProductCategory == "Motor" && ismandatory == true)
                        {
                            for (int l = 0; l < dtgvcode.RowCount; l++)
                            {
                                GvProductInfo.Rows.Add();
                                int i = GvProductInfo.RowCount;
                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = "1";
                                if (txtFreeQty.Text == " ")
                                    GvProductInfo.Rows[i - 1].Cells["FrQty"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["FrQty"].Value = txtFreeQty.Text;
                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                GvProductInfo.Rows[i - 1].Cells["Price"].Value = Convert.ToDecimal(txtpurchaseprice.Text);
                                GvProductInfo.Rows[i - 1].Cells["Amount"].Value = Convert.ToDecimal(txtpurchaseprice.Text);
                                if (txtpdiscount.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["Disc"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["Disc"].Value = txtpdiscount.Text;
                                if (txtpSpcDisc.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["SpDisc"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["SpDisc"].Value = txtpSpcDisc.Text;
                                if (cmbsgst.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["SGST"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["SGST"].Value = cmbsgst.Text;
                                GvProductInfo.Rows[i - 1].Cells["SGSTAmt"].Value = Convert.ToDecimal(lblpvat.Text) / Convert.ToDecimal(txtQuantity.Text);
                                GvProductInfo.Rows[i - 1].Cells["CGST"].Value = cmbcgst.Text;
                                GvProductInfo.Rows[i - 1].Cells["CGSTAmt"].Value = Convert.ToDecimal(lblCGst.Text) / Convert.ToDecimal(txtQuantity.Text);
                                GvProductInfo.Rows[i - 1].Cells["IGST"].Value = cmbigst.Text;
                                GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = Convert.ToDecimal(lblIGst.Text) / Convert.ToDecimal(txtQuantity.Text);
                                GvProductInfo.Rows[i - 1].Cells["Cess"].Value = txtpCess.Text;
                                GvProductInfo.Rows[i - 1].Cells["TotalAmount"].Value = Convert.ToDecimal(txtpTotalAmt.Text) / Convert.ToDecimal(txtQuantity.Text);
                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                if (ismandatory == true)
                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                else
                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                GvProductInfo.Rows[i - 1].Cells["CessAmt"].Value = Convert.ToDecimal(lblpCessAmt.Text) / Convert.ToDecimal(txtQuantity.Text);

                                GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                                GvProductInfo.Rows[i - 1].Cells["SaleRate"].Value = txtSaleRate.Text;
                                GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = txtstaticBarcode.Text;
                                if (txt3LevelDisvPer.Text != "")
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDisc"].Value = txt3LevelDisvPer.Text;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDisc"].Value = "0";
                                if (txt3LevelDiscAmt.Text != "")
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDiscAmt"].Value = txt3LevelDiscAmt.Text;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDiscAmt"].Value = "0";
                                if (txtQuantitySubtitle.Text != "")
                                    GvProductInfo.Rows[i - 1].Cells["QuantitySubtitle"].Value = txtQuantitySubtitle.Text;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["QuantitySubtitle"].Value = "0";
                                GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                if (txtTCSPer.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["TCSPer"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["TCSPer"].Value = txtTCSPer.Text;
                                if (txtTCSAmt.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["TCSAmt"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["TCSAmt"].Value = txtTCSAmt.Text;
                               
                            }
                        }
                        else if (productentrytype == 3 && matrixwithbarcode == true)
                        {
                            for (int m = 0; m < dtgvSize.RowCount; m++)
                            {
                                if (Convert.ToDecimal(dtgvSize.Rows[m].Cells["Qtyg"].Value) == 0 || Convert.ToString(dtgvSize.Rows[m].Cells["Qtyg"].Value) == "")
                                {
                                    goto A;
                                }
                                GvProductInfo.Rows.Add();
                                int i = GvProductInfo.RowCount;
                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = Convert.ToDecimal(dtgvSize.Rows[m].Cells["Qtyg"].Value);
                                if (txtFreeQty.Text == " ")
                                    GvProductInfo.Rows[i - 1].Cells["FrQty"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["FrQty"].Value = Convert.ToDecimal(dtgvSize.Rows[m].Cells["FrQtyg"].Value);
                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                GvProductInfo.Rows[i - 1].Cells["Price"].Value = Convert.ToDecimal(txtpurchaseprice.Text);
                                GvProductInfo.Rows[i - 1].Cells["Amount"].Value = Convert.ToDecimal(txtpurchaseprice.Text) * Convert.ToDecimal(dtgvSize.Rows[m].Cells["Qtyg"].Value);
                                if (txtpdiscount.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["Disc"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["Disc"].Value = txtpdiscount.Text;
                                if (txtpSpcDisc.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["SpDisc"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["SpDisc"].Value = txtpSpcDisc.Text;

                                decimal dSAmt = 0, dSDiscAmt = 0, dSSpDiscAmt = 0, dStaxableAmt = 0, dSCgstAmt = 0, dSICgstAmt = 0, dSTotAmt = 0;
                                dSAmt = Convert.ToDecimal(txtpurchaseprice.Text) * Convert.ToDecimal(dtgvSize.Rows[m].Cells["Qtyg"].Value);
                                dSDiscAmt = dSAmt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                                dSSpDiscAmt = ((dSAmt - dSDiscAmt) * Convert.ToDecimal(txtpSpcDisc.Text)) / 100;
                                dStaxableAmt = dSAmt - dSDiscAmt - dSSpDiscAmt;
                                dSCgstAmt = dStaxableAmt * Convert.ToDecimal(cmbcgst.Text) / 100;
                                dSICgstAmt = dStaxableAmt * Convert.ToDecimal(cmbigst.Text) / 100;
                                dSTotAmt = dStaxableAmt + dSCgstAmt + dSICgstAmt + dSCgstAmt;
                                //GvProductInfo.Rows[i - 1].Cells[13].Value = Convert.ToDecimal(dSCgstAmt);
                                //GvProductInfo.Rows[i - 1].Cells[14].Value = cmbcgst.Text;
                                //GvProductInfo.Rows[i - 1].Cells[15].Value = Convert.ToDecimal(dSCgstAmt);
                                //GvProductInfo.Rows[i - 1].Cells[16].Value = cmbigst.Text;
                                //GvProductInfo.Rows[i - 1].Cells[17].Value = Convert.ToDecimal(dSICgstAmt);
                                //GvProductInfo.Rows[i - 1].Cells[18].Value = txtpCess.Text;
                                //GvProductInfo.Rows[i - 1].Cells[19].Value = Convert.ToDecimal(dSTotAmt);

                                if (cmbsgst.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["SGST"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["SGST"].Value = cmbsgst.Text;
                                GvProductInfo.Rows[i - 1].Cells["SGSTAmt"].Value = Convert.ToDecimal(dSCgstAmt);
                                GvProductInfo.Rows[i - 1].Cells["CGST"].Value = cmbcgst.Text;
                                GvProductInfo.Rows[i - 1].Cells["CGSTAmt"].Value = Convert.ToDecimal(dSCgstAmt);
                                GvProductInfo.Rows[i - 1].Cells["IGST"].Value = cmbigst.Text;
                                GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = Convert.ToDecimal(dSICgstAmt);
                                GvProductInfo.Rows[i - 1].Cells["Cess"].Value = txtpCess.Text;
                                GvProductInfo.Rows[i - 1].Cells["TotalAmount"].Value = Convert.ToDecimal(dSTotAmt);
                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                if (ismandatory == true)
                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                else
                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                GvProductInfo.Rows[i - 1].Cells["CessAmt"].Value = Convert.ToDecimal(lblpCessAmt.Text) / Convert.ToDecimal(dtgvSize.Rows[m].Cells["Qtyg"].Value);

                                GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                                GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = txtstaticBarcode.Text;
                                if (txt3LevelDisvPer.Text != "")
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDisc"].Value = txt3LevelDisvPer.Text;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDisc"].Value = "0";
                                if (txt3LevelDiscAmt.Text != "")
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDiscAmt"].Value = txt3LevelDiscAmt.Text;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["ThreeLevelDiscAmt"].Value = "0";
                                if (txtQuantitySubtitle.Text != "")
                                    GvProductInfo.Rows[i - 1].Cells["QuantitySubtitle"].Value = txtQuantitySubtitle.Text;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["QuantitySubtitle"].Value = "0";
                                GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                if (txtTCSPer.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["TCSPer"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["TCSPer"].Value = txtTCSPer.Text;
                                if (txtTCSAmt.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["TCSAmt"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["TCSAmt"].Value = txtTCSAmt.Text;
                                GvProductInfo.Rows[i - 1].Cells["Sizep"].Value = Convert.ToString(dtgvSize.Rows[m].Cells["Sizeg"].Value);
                                GvProductInfo.Rows[i - 1].Cells["SaleRate"].Value = Convert.ToDecimal(dtgvSize.Rows[m].Cells["SRateg"].Value);
                                GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = Convert.ToString(dtgvSize.Rows[m].Cells["SBarcodeg"].Value);
                                GvProductInfo.Rows[i - 1].Cells["Department"].Value = glbproductdata.DeptName;
                                GvProductInfo.Rows[i - 1].Cells["Category"].Value = glbproductdata.Category;
                                GvProductInfo.Rows[i - 1].Cells["Color"].Value = glbproductdata.ColorName;
                                if (Convert.ToString(dtgvSize.Rows[m].Cells["Colorg"].FormattedValue) != "Select")
                                    GvProductInfo.Rows[i - 1].Cells["Color"].Value = Convert.ToString(dtgvSize.Rows[m].Cells["Colorg"].FormattedValue);
                                else
                                    GvProductInfo.Rows[i - 1].Cells["Color"].Value = "";
                            A: { }
                            }
                        }
                        else
                        {
                            GvProductInfo.Rows.Add();
                            int i = GvProductInfo.RowCount;
                            GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                            GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                            GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                            GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                            GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = txtQuantity.Text;
                            if (txtFreeQty.Text == " ")
                                GvProductInfo.Rows[i - 1].Cells["FrQty"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["FrQty"].Value = txtFreeQty.Text;
                            GvProductInfo.Rows[i - 1].Cells["Price"].Value = Convert.ToDecimal(txtpurchaseprice.Text);
                            GvProductInfo.Rows[i - 1].Cells["Amount"].Value = Convert.ToDecimal(txtamount.Text);
                            if (txtpdiscount.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["Disc"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["Disc"].Value = txtpdiscount.Text;
                            if (txtpSpcDisc.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["SpDisc"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["SpDisc"].Value = txtpSpcDisc.Text;
                            if (cmbsgst.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["SGST"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["SGST"].Value = cmbsgst.Text;
                            GvProductInfo.Rows[i - 1].Cells["SGSTAmt"].Value = lblpvat.Text;
                            GvProductInfo.Rows[i - 1].Cells["CGST"].Value = cmbcgst.Text;
                            GvProductInfo.Rows[i - 1].Cells["CGSTAmt"].Value = lblCGst.Text;
                            GvProductInfo.Rows[i - 1].Cells["IGST"].Value = cmbigst.Text;
                            GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = lblIGst.Text;
                            GvProductInfo.Rows[i - 1].Cells["Cess"].Value = txtpCess.Text;
                            GvProductInfo.Rows[i - 1].Cells["TotalAmount"].Value = txtpTotalAmt.Text;
                            GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            if (ismandatory == true)
                                GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                            else
                                GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                            GvProductInfo.Rows[i - 1].Cells["CessAmt"].Value = lblpCessAmt.Text;
                            GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                            GvProductInfo.Rows[i - 1].Cells["SaleRate"].Value = txtSaleRate.Text;
                            GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = txtstaticBarcode.Text;
                            if (txt3LevelDisvPer.Text != "")
                                GvProductInfo.Rows[i - 1].Cells["ThreeLevelDisc"].Value = txt3LevelDisvPer.Text;
                            else
                                GvProductInfo.Rows[i - 1].Cells["ThreeLevelDisc"].Value = "0";
                            if (txt3LevelDiscAmt.Text != "")
                                GvProductInfo.Rows[i - 1].Cells["ThreeLevelDiscAmt"].Value = txt3LevelDiscAmt.Text;
                            else
                                GvProductInfo.Rows[i - 1].Cells["ThreeLevelDiscAmt"].Value = "0";
                            if (txtQuantitySubtitle.Text != "")
                                GvProductInfo.Rows[i - 1].Cells["QuantitySubtitle"].Value = txtQuantitySubtitle.Text;
                            else
                                GvProductInfo.Rows[i - 1].Cells["QuantitySubtitle"].Value = "0";
                            if (productentrytype == 1 || productentrytype == 2) //1- with batch and expiry
                            {
                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = txtBatchNo.Text;
                                if (productentrytype != 2)  // without expiry
                                    GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = dtpExpiry.Value.ToString("dd/MM/yyyy");
                            }
                            GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                            if (txtTCSPer.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["TCSPer"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["TCSPer"].Value = txtTCSPer.Text;
                            if (txtTCSAmt.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["TCSAmt"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["TCSAmt"].Value = txtTCSAmt.Text;
                            if (txtMRPRate.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["MRP"].Value = 0;
                            else
                                GvProductInfo.Rows[i - 1].Cells["MRP"].Value = txtMRPRate.Text;
                        }
                        totalamt = 0;
                        totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                        totalSGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SGSTAmt"].Value));
                        totalCGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CGSTAmt"].Value));
                        totalIGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["IGSTAmt"].Value));
                        totalCess = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CessAmt"].Value));
                        totalqty = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["Quantityg"].Value));
                        txtTotalAmt.Text = Convert.ToString(totalamt);
                        totalGST = totalSGst + totalCGst + totalIGst;
                        txtTotalGST.Text = Convert.ToString(totalGST);
                        TaxableTotal = totalamt - (totalSGst + totalCGst + totalIGst + totalCess);
                        UpdateMRP(Proname, company, unit);
                        CalculateNetAmt();
                        clearAdd();
                    }
                a: { }
                    refreshFlag = false;
                }
                else
                {
                    if (cmbsuppliername.Text == "Select")
                    {
                        MessageBox.Show("Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsuppliername;
                    }
                    else if (txtpname.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtpname;
                    }
                    else if (txtQuantity.Text == "")
                    {
                        MessageBox.Show("Enter Quantity.", "Warning");
                        this.ActiveControl = txtQuantity;
                    }
                    else if (txtBatchNo.Text == "" && (productentrytype == 1 || productentrytype == 2))
                    {
                        MessageBox.Show("Enter Batch Number.", "Warning");
                        AdjustPanelDetails(productentrytype);
                        this.ActiveControl = txtBatchNo;
                    }
                    else if (dtpExpiry.Value.Date < DateTime.Now && productentrytype == 1)
                    {
                        MessageBox.Show("Expiry Date must be greater than Todays Date.", "Warning");
                        AdjustPanelDetails(productentrytype);
                        this.ActiveControl = dtpExpiry;
                    }
                    else
                    {
                        MessageBox.Show("Enter Purchase Price.", "Warning");
                        this.ActiveControl = txtpurchaseprice;
                    }
                }
                flgmandatory = false;
                ismandatory = false;
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
            VisibleBatchPanel = "";
            pnlIntegratedDtls.Visible = false;
            this.ActiveControl = txtporderno;
        }

        public void clear()
        {
            try
            {
                clearAdd();
                maxpurchaseorderid(); fillsupplier(); fillGodownName();
                txtpname.Text = ""; txtQuantity.Text = "0"; txtamount.Text = "";
                txtpurchaseprice.Text = ""; txtHSN.Text = ""; txtFreeQty.Text = "0";
                lbl2vatamt.Text = "0"; ttvat.Text = "0"; txtpdiscount.Text = "0";
                txtpTotalAmt.Text = ""; txtDiscount.Text = "0"; txtSpcDisc.Text = "0";
                lbldiscount.Text = "0"; lblSpcDisc.Text = "0"; lblvat.Text = "0";
                Pamt = 0; ttnetamt.Text = "0"; colIndex = -1;
                txtTotalAmt.Text = "0"; txtTotalGST.Text = "0"; GvProductInfo.Rows.Clear();
                btnSave.Enabled = true; btnDelete.Enabled = false; btnUpdate.Enabled = false;
                fillgst(); showsize = false;
                txttranscharges.Text = "0"; txtPaidAmt.Text = "0"; txtRemainingAmt.Text = "0";
                txtchequeno.Text = ""; txtNarration.Text = ""; chkcheque.Checked = false;
                fillmfgcomp(); cmbUnit.Items.Clear(); txtsupplierpono.Text = "";
                fillbankname(); gvProductRemStock.Visible = false; ttvat.Enabled = true;
                chkcheque.Enabled = true; pnlQtySubtitle.Visible = false;
                try
                {
                    dtpPorderdate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtstaticBarcode.Text = ""; txtstaticBarcode.Enabled = false; txtSaleRate.Text = "0";
                txtSaleRate.Enabled = false; updateBarcode = string.Empty; barcodestatus = string.Empty;
                txtGstTransport.Text = "0"; txtPackaging.Text = "0"; txtGstPackpercent.Text = "0";
                lblpackGST.Text = "0"; flgmandatory = true; ismandatory = false;
                txtBatchNo.Text = "0"; dtpExpiry.Value = DateTime.Now.Date; txtfreightchrgs.Text = "0";
                txtexciseduty.Text = "0"; pnlBank.Visible = false; txt3LevelDisvPer.Text = "0";
                txt3LevelDiscAmt.Text = ""; pnl3LevelDisc.Visible = false; txtMRefNo.Text = "";
                QtySubtitle = false; chkTPurchase.Checked = false;
            }
            catch (Exception)
            { }
        }

        public void savepomaster()
        {
            try
            {
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaseMaster purchasedata = new PurchaseMaster();
                maxpurchaseorderid();
                purchasedata.PorderNo = Convert.ToInt32(txtporderno.Text);
                purchasedata.PorderDate = dtpPorderdate.Value;
                purchasedata.SupplierpoNo = Convert.ToString(txtsupplierpono.Text.Trim());
                purchasedata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                purchasedata.Suppliername = cmbsuppliername.Text;
                purchasedata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                if (ttvat.Text == "0")
                    purchasedata.Vat = Convert.ToDecimal(0);
                else
                    purchasedata.Vat = Convert.ToDecimal(ttvat.Text);
                if (txtDiscount.Text == "0")
                    purchasedata.Discount = Convert.ToDecimal(0);
                else
                    purchasedata.Discount = Convert.ToDecimal(txtDiscount.Text);
                if (txtSpcDisc.Text == "0")
                    purchasedata.SpcDiscount = Convert.ToDecimal(0);
                else
                    purchasedata.SpcDiscount = Convert.ToDecimal(txtSpcDisc.Text);
                if (txtPaidAmt.Text != "")
                    purchasedata.PaidAmount = Convert.ToDecimal(txtPaidAmt.Text);
                else
                    purchasedata.PaidAmount = 0;
                if (txtRemainingAmt.Text != "")
                    purchasedata.RemainingAmount = Convert.ToDecimal(txtRemainingAmt.Text);
                else
                    purchasedata.RemainingAmount = 0;
                if (txttranscharges.Text == "")
                    purchasedata.TransportCharges = 0;
                else
                    purchasedata.TransportCharges = Convert.ToDecimal(txttranscharges.Text);
                purchasedata.NetAmount = Convert.ToDecimal(ttnetamt.Text);
                if (txtRoundOff.Text != "")
                    purchasedata.RoundOff = Convert.ToDecimal(txtRoundOff.Text);
                if (chkTPurchase.Checked)
                    purchasedata.TAXType = "TaxPurchase";
                purchasedata.TransactionYear = CommonMethod.TransactionYear;
                purchasedata.CompId = CommonMethod.CompId;
                purchasedata.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                purchasedata.TotalGst = totalSGst + totalCGst + totalIGst;
                if (chkGstTransport.Checked == true)
                    purchasedata.TransportGst = Convert.ToDecimal(txtGstTransport.Text);
                else
                    purchasedata.TransportGst = 0;
                purchasedata.PackagingAndForwarding = Convert.ToDecimal(txtPackaging.Text);
                purchasedata.PackagingGstPercent = Convert.ToDecimal(txtGstPackpercent.Text);
                purchasedata.PackagingGstAmt = Convert.ToDecimal(lblpackGST.Text);
                if (txtfreightchrgs.Text == "") { txtfreightchrgs.Text = "0"; }
                if (txtexciseduty.Text == "") { txtexciseduty.Text = "0"; }
                purchasedata.Freightchrges = Convert.ToDecimal(txtfreightchrgs.Text);
                purchasedata.ExciseDuty = Convert.ToDecimal(txtexciseduty.Text);
                if (txtNarration.Text != "")
                    purchasedata.Narration = Convert.ToString(txtNarration.Text);
                else
                    purchasedata.Narration = "";
                purchasedata.ManualPurRefNo = txtMRefNo.Text.Trim();
                pmasterrepo.Add(purchasedata);
                pmasterrepo.Save();
                printId = Convert.ToInt32(purchasedata.PorderNo);
                purmasterid = Convert.ToInt32(purchasedata.Id);
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void saveremainingpayment(string mode)
        {
            //Save Purchase OrderNo with total balance calculation
            try
            {
                SupplierRemainingPaymentRepository sremainingpaymentrepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                remainingdata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                remainingdata.Suppliername = Convert.ToString(cmbsuppliername.Text);
                if (ttnetamt.Text != "")
                    remainingdata.NetAmt = Convert.ToDecimal(ttnetamt.Text);
                else
                    remainingdata.NetAmt = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtPaidAmt.Text != "")
                    remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                else
                    remainingdata.PaidAmt = 0;
                SupplierRemainingPaymentRepository sremainingpaymentrepo1 = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                remainingdata1 = sremainingpaymentrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                decimal decremamt = 0;
                if (remainingdata1 != null)
                {
                    decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                    if (txtRemainingAmt.Text != "")
                        remainingdata.RemainingAmt = decremamt + Convert.ToDecimal(txtRemainingAmt.Text);
                    else
                        remainingdata.RemainingAmt = decremamt;
                }
                else
                {
                    if (txtRemainingAmt.Text != "")
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainingAmt.Text);
                    else
                        remainingdata.RemainingAmt = 0;
                }
                remainingdata.CreateDate = Convert.ToDateTime(dtpPorderdate.Value);
                remainingdata.PaymentMode = mode;
                if (mode == "Cheque")
                {
                    remainingdata.Bankname = cmbbank.Text;
                    remainingdata.Chequedate = dtpchequedate.Value;
                    remainingdata.Chequeno = txtchequeno.Text;
                    try
                    {
                        BankStatementRepository bankrepo = new BankStatementRepository();
                        BankStatement bankstdata = new BankStatement();
                        bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                        bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                        bankstdata.TransactionDate = dtpPorderdate.Value;
                        bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbsuppliername.Text.Trim());
                        bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                        bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                        bankstdata.TransactionType = "Purchase";
                        bankstdata.Balance = 0;
                        bankstdata.CompId = CommonMethod.CompId;
                        bankrepo.Add(bankstdata);
                        bankrepo.Save();
                        bankrepo.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                remainingdata.BillId = Convert.ToInt32(txtporderno.Text);
                remainingdata.BillDate = Convert.ToDateTime(dtpPorderdate.Value);
                if (separateLedger == "Y")
                {
                    DataTable dt = db.GetTable("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.PorderNo='" + txtporderno.Text + "' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count != 0)
                        remainingdata.Status = "Tax";
                    else
                        remainingdata.Status = "Estimate";
                }
                remainingdata.Type = "Purchase";
                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                remainingdata.CompId = CommonMethod.CompId;
                sremainingpaymentrepo.Add(remainingdata);
                sremainingpaymentrepo.Save();
                sremainingpaymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void SavePurchaseBill()
        {
            try
            {
                maxpurchaseorderid();
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var BillId = pmasterrepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnNew;
                }
                else
                {
                    if (cmbsuppliername.Text != "Select")
                    {
                        if (GvProductInfo.Rows.Count != 0 && txtTotalAmt.Text != "" && txtPaidAmt.Text != "")
                        {
                            if (chkcheque.Checked == true)
                            {
                                if (cmbbank.Text != "Select" && txtchequeno.Text != "" && dtpchequedate.Text != currentdate)
                                {
                                    //CustomerMaster table entry code
                                    savepomaster();
                                    for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                            command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@BillNo", SqlDbType.Int).Value = purmasterid;
                                            command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                            command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                            command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                            command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                            command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["FrQty"].Value);
                                            command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                            command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Price"].Value);
                                            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                                            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                                            command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                            command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                                            command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                                            command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                                            command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                                            command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                            command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                                            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                            command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value);
                                            command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                            command.Parameters.Add("@SpcDisc", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SpDisc"].Value);
                                            command.Parameters.Add("@Cess", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Cess"].Value);
                                            command.Parameters.Add("@CessAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CessAmt"].Value);
                                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                            command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["StaticBarcode"].Value);
                                            command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                            if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value == null)
                                                command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                            else if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Contains(' '))
                                                command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Split(' ')[0].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                            else
                                                command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                            if (GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value == null)
                                                command.Parameters.Add("@ThreeLevelDisc", SqlDbType.Decimal).Value = 0;
                                            else
                                                command.Parameters.Add("@ThreeLevelDisc", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value);
                                            if (GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value == null)
                                                command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = 0;
                                            else
                                                command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value);
                                            command.Parameters.Add("@TCSPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSPer"].Value);
                                            command.Parameters.Add("@TCSAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSAmt"].Value);
                                            command.Parameters.Add("@Size", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizep"].Value);
                                            command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Department"].Value);
                                            command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Category"].Value);
                                            command.Parameters.Add("@ColorName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Color"].Value);
                                            //command.Parameters.Add("@Commission", SqlDbType.Decimal).Value = Convert.ToString(GvProductInfo.Rows[i].Cells[32].Value);
                                            //command.Parameters.Add("@CommRs", SqlDbType.Decimal).Value = Convert.ToString(GvProductInfo.Rows[i].Cells[31].Value);
                                            command.ExecuteNonQuery();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                    }
                                    //SupplierRemainingPayment table entry
                                    string mode = "Cheque";
                                    saveremainingpayment(mode);
                                    GvProductInfo.Rows.Clear();
                                    MessageBox.Show("Purchase Order saved.", "Success");
                                    chkcheque.Checked = false;
                                    clear();
                                    fillsupplier();
                                    fillGodownName();
                                    this.ActiveControl = btnPrint;
                                }
                                else
                                {
                                    if (cmbbank.Text == "Select")
                                    {
                                        MessageBox.Show("Select Bank Name.", "Warning");
                                        this.ActiveControl = cmbbank;
                                    }
                                    else if (dtpchequedate.Text == currentdate)
                                    {
                                        MessageBox.Show("Enter cheque Date.", "Warning");
                                        this.ActiveControl = dtpchequedate;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter cheque Number.", "Warning");
                                        this.ActiveControl = txtchequeno;
                                    }
                                }
                            }
                            else
                            {
                                //CustomerMaster table entry code
                                savepomaster();
                                for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                        command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@BillNo", SqlDbType.Int).Value = purmasterid;
                                        command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                        command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                        command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                        command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                        command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["FrQty"].Value);
                                        command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                        command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Price"].Value);
                                        command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                                        command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                                        command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                        command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                                        command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                                        command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                                        command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                                        command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                        command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                                        command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                        command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value);
                                        command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                        command.Parameters.Add("@SpcDisc", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SpDisc"].Value);
                                        command.Parameters.Add("@Cess", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Cess"].Value);
                                        command.Parameters.Add("@CessAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CessAmt"].Value);
                                        command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                        command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["StaticBarcode"].Value);
                                        command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                        if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value == null)
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                        else if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Contains(' '))
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Split(' ')[0].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                        else
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        if (GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value == null)
                                            command.Parameters.Add("@ThreeLevelDiscPer", SqlDbType.Decimal).Value = 0;
                                        else
                                            command.Parameters.Add("@ThreeLevelDiscPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value);
                                        if (GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value == null)
                                            command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = 0;
                                        else
                                            command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value);
                                        command.Parameters.Add("@TCSPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSPer"].Value);
                                        command.Parameters.Add("@TCSAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSAmt"].Value);
                                        command.Parameters.Add("@Size", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizep"].Value);
                                        command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Department"].Value);
                                        command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Category"].Value);
                                        command.Parameters.Add("@ColorName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Color"].Value);
                                        command.Parameters.Add("@PMRP", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MRP"].Value);
                                        command.ExecuteNonQuery();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                if (Convert.ToDecimal(txtRemainingAmt.Text) != 0)
                                {
                                    //SupplierRemainingPayment table entry
                                    string mode = "Cash";
                                    saveremainingpayment(mode);
                                }
                                GvProductInfo.Rows.Clear();
                                MessageBox.Show("Purchase Order saved.", "Success");
                                chkcheque.Checked = false;
                                clear();
                                fillsupplier();
                                fillGodownName();
                                this.ActiveControl = btnPrint;
                            }
                        }
                        else
                        {
                            if (GvProductInfo.Rows.Count == 0)
                            {
                                MessageBox.Show("Please Add Record first.", "Warning");
                                this.ActiveControl = btnAdd;
                            }
                            else if (txtPaidAmt.Text == "")
                            {
                                MessageBox.Show("Please fill paid amount.", "Warning");
                                this.ActiveControl = txtPaidAmt;
                            }
                            else
                            {
                                MessageBox.Show("Fill Total Amount.", "Warning");
                                this.ActiveControl = txtTotalAmt;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Supplier Name.", "Warning");
                        this.ActiveControl = cmbsuppliername;
                    }
                }
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void UpdatePurchaseBill()
        {
            try
            {
                if (cmbsuppliername.Text != "Select")
                {
                    if (GvProductInfo.Rows.Count != 0 && txtTotalAmt.Text != "" && txtPaidAmt.Text != "")
                    {
                        if (chkcheque.Checked == true)
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "" && dtpchequedate.Text != currentdate)
                            {
                                updatemaster();
                                if (deleteid.Count != 0)
                                {
                                    for (int x = 0; x < deleteid.Count; x++)
                                    {
                                        //deleteitemwithstockupdation(deleteid[x]);
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPPOnBillRemoveDetailWithStockUpdate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@id", SqlDbType.Int).Value = deleteid[x];
                                            command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);

                                            command.ExecuteNonQuery();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                    }
                                }

                                for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                                {
                                    int id = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value);
                                    if (id != 0)
                                    {
                                        //updatestockifnameupdate(id, i);
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPPOnBillUpdateStockIfNameReplace", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                            command.ExecuteNonQuery();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                    }


                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                        if (id != 0)
                                        {

                                            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                            command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                        }
                                        else
                                        {
                                            command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                        }
                                        command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@BillNo", SqlDbType.Int).Value = PurchaeId;
                                        command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                        command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                        command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                        command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                        command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["FrQty"].Value);
                                        command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                        command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Price"].Value);
                                        command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                                        command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                                        command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                        command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                                        command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                                        command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                                        command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                                        command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                        command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                                        command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                        command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value);
                                        command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                        command.Parameters.Add("@SpcDisc", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SpDisc"].Value);
                                        command.Parameters.Add("@Cess", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Cess"].Value);
                                        command.Parameters.Add("@CessAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CessAmt"].Value);
                                        command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                        command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["StaticBarcode"].Value);
                                        command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                        if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value == null)
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                        else if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Contains(' '))
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Split(' ')[0].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                        else
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        if (GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value == null)
                                            command.Parameters.Add("@ThreeLevelDiscPer", SqlDbType.Decimal).Value = 0;
                                        else
                                            command.Parameters.Add("@ThreeLevelDiscPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value);
                                        if (GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value == null)
                                            command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = 0;
                                        else
                                            command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value);
                                        command.Parameters.Add("@TCSPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSPer"].Value);
                                        command.Parameters.Add("@TCSAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSAmt"].Value);
                                        command.Parameters.Add("@Size", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizep"].Value);
                                        command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Department"].Value);
                                        command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Category"].Value);
                                        command.Parameters.Add("@ColorName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Color"].Value);
                                        command.ExecuteNonQuery();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                    //updatedetail(i, id, "update");
                                    updatebatchtable(i);
                                }
                                updateremainingpayment("Cheque");
                                clear();
                                GvProductInfo.Rows.Clear();
                                MessageBox.Show("Purchase Order Updated.", "Success");
                                try
                                {
                                    RecieptChallanMasterRepository rcmr = new RecieptChallanMasterRepository();
                                    int chkstatus = Convert.ToInt32(rcmr.GetAll().Where(t => t.Id == rcmid && t.CompId == CommonMethod.CompId).FirstOrDefault().Status);
                                    if (chkstatus == 1)
                                    {
                                        RecieptChallanMasterRepository rcmr1 = new RecieptChallanMasterRepository();
                                        RecieptChallanMaster rcmdata = new RecieptChallanMaster();
                                        rcmdata = rcmr1.GetById(rcmid);
                                        rcmdata.Status = 0;
                                        rcmr1.Edit(rcmdata);
                                        rcmr1.Save();
                                        MessageBox.Show("Reciept challan Successfully Converted to Purchase bill.", "Success");
                                    }
                                }
                                catch (Exception)
                                { }
                                this.ActiveControl = btnPrint;
                                chkcheque.Checked = false;
                                deleteid.Clear();
                                this.ActiveControl = txtporderno;
                            }
                            else
                            {
                                if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else if (dtpchequedate.Text == currentdate)
                                {
                                    MessageBox.Show("Enter cheque Date.", "Warning");
                                    this.ActiveControl = dtpchequedate;
                                }
                                else
                                {
                                    MessageBox.Show("Enter cheque Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        else
                        {
                            updatemaster();
                            if (deleteid.Count != 0)
                            {
                                for (int x = 0; x < deleteid.Count; x++)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPPOnBillRemoveDetailWithStockUpdate", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = deleteid[x];
                                        command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                        command.ExecuteNonQuery();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                            }

                            for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                            {
                                int id = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value);
                                if (id != 0)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPPOnBillUpdateStockIfNameReplace", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                        command.ExecuteNonQuery();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                    if (id != 0)
                                    {
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                        command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                    }
                                    else
                                    {
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                    }
                                    command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = PurchaeId;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                    command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["FrQty"].Value);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Price"].Value);
                                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                                    command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                                    command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                                    command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                                    command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                                    command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                                    command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                    command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                                    command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                    command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value);
                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                    command.Parameters.Add("@SpcDisc", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SpDisc"].Value);
                                    command.Parameters.Add("@Cess", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Cess"].Value);
                                    command.Parameters.Add("@CessAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CessAmt"].Value);
                                    command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                    command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["StaticBarcode"].Value);
                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                    if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value == null)
                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                    else if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Contains(' '))
                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Split(' ')[0].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                    else
                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                    //{

                                    //    try
                                    //    {
                                    //        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = DateTime.ParseExact(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString().Split(' ')[0].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                                    //    }
                                    //    catch (Exception)
                                    //    { }
                                    //}
                                    if (GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value == null)
                                        command.Parameters.Add("@ThreeLevelDiscPer", SqlDbType.Decimal).Value = 0;
                                    else
                                        command.Parameters.Add("@ThreeLevelDiscPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value);
                                    if (GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value == null)
                                        command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = 0;
                                    else
                                        command.Parameters.Add("@QtySubtitle", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["QuantitySubtitle"].Value);
                                    command.Parameters.Add("@TCSPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSPer"].Value);
                                    command.Parameters.Add("@TCSAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSAmt"].Value);
                                    command.Parameters.Add("@Size", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizep"].Value);
                                    command.Parameters.Add("@DeptName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Department"].Value);
                                    command.Parameters.Add("@CategoryName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Category"].Value);
                                    command.Parameters.Add("@ColorName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Color"].Value);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                updatebatchtable(i);
                            }
                            updateremainingpayment("Cash");
                            clear();
                            GvProductInfo.Rows.Clear();
                            MessageBox.Show("Purchase Order Updated.", "Success");
                            try
                            {
                                RecieptChallanMasterRepository rcmr = new RecieptChallanMasterRepository();
                                int chkstatus = Convert.ToInt32(rcmr.GetAll().Where(t => t.Id == rcmid && t.CompId == CommonMethod.CompId).FirstOrDefault().Status);
                                if (chkstatus == 1)
                                {
                                    RecieptChallanMasterRepository rcmr1 = new RecieptChallanMasterRepository();
                                    RecieptChallanMaster rcmdata = new RecieptChallanMaster();
                                    rcmdata = rcmr1.GetById(rcmid);
                                    rcmdata.Status = 0;
                                    rcmr1.Edit(rcmdata);
                                    rcmr1.Save();
                                    MessageBox.Show("Reciept challan Successfully Converted to Purchase bill.", "Success");
                                }
                            }
                            catch (Exception)
                            { }
                            this.ActiveControl = btnPrint;
                            chkcheque.Checked = false;
                            deleteid.Clear();
                            this.ActiveControl = txtporderno;
                        }
                    }
                    else
                    {
                        if (GvProductInfo.Rows.Count == 0)
                        {
                            MessageBox.Show("Please Add Record First.", "Warning");
                            this.ActiveControl = btnAdd;
                        }
                        else if (txtPaidAmt.Text == "")
                        {
                            MessageBox.Show("Please fill paid amount.", "Warning");
                            this.ActiveControl = txtPaidAmt;
                        }
                        else
                        {
                            MessageBox.Show("Add Product Information.", "Warning");
                            this.ActiveControl = txtpname;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Supplier Name.", "Warning");
                    this.ActiveControl = cmbsuppliername;
                }
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SavePurchaseBill();
            pnlIntegratedDtls.Visible = false;
        }

        private void txtpurchaseprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpurchaseprice.Text != "")
                    getGstCalculation();
                else
                {
                    // txtpurchaseprice.Text = "0";
                    this.ActiveControl = txtpurchaseprice;
                }
            }
            catch (Exception)
            { }
        }

        private void fmPurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                tooltipbtn.SetToolTip(this.rbtnaddproduct, "Click To Add Product");
                tooltipbtn.SetToolTip(this.btnSuppForm, "Click To Add Supplier");
                getProductList();
                lstprefixproduct.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void getProductList()
        {
            txtpname.Text = "";
            string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cnString1))
            {
                SqlCommand cmd = null;
                if (CommonMethod.commProduct == true)
                    cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation", con);
                else
                    cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    try
                    {
                        MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2));
                    }
                    catch (Exception)
                    { }
                }
                txtpname.AutoCompleteCustomSource = MyCollection;
                con.Close();
                reader.Dispose();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            pnlIntegratedDtls.Visible = false;
            try
            {
                int porderno = printId;
                RptPurchaseorder porderrpt = new RptPurchaseorder(porderno);
                porderrpt.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void ttvat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnUpdate.Enabled == false && btnSave.Enabled == false && ttvat.Enabled == true)
                { }
                else
                {
                    if (Convert.ToDecimal(ttvat.Text) <= 100)
                        CalculateNetAmt();
                    else
                    {
                        MessageBox.Show("Enter Vat Between 0 to 100 %.", "Warning");
                        ttvat.Text = "0";
                        this.ActiveControl = ttvat;
                    }
                }
            }
            catch (Exception)
            {
                ttvat.Text = "0";
                this.ActiveControl = ttvat;
            }
        }

        public void updatemaster()
        {
            try
            {
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaseMaster purchasedata = new PurchaseMaster();
                purchasedata.PorderNo = Convert.ToInt32(txtporderno.Text);
                purchasedata.PorderDate = dtpPorderdate.Value;
                purchasedata.SupplierpoNo = Convert.ToString(txtsupplierpono.Text);
                purchasedata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                purchasedata.Suppliername = cmbsuppliername.Text;
                purchasedata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                if (ttvat.Text == "")
                    purchasedata.Vat = Convert.ToDecimal(0);
                else
                    purchasedata.Vat = Convert.ToDecimal(ttvat.Text);
                if (txtDiscount.Text == "")
                    purchasedata.Discount = Convert.ToDecimal(0);
                else
                    purchasedata.Discount = Convert.ToDecimal(txtDiscount.Text);
                if (txtSpcDisc.Text == "")
                    purchasedata.SpcDiscount = Convert.ToDecimal(0);
                else
                    purchasedata.SpcDiscount = Convert.ToDecimal(txtSpcDisc.Text);
                if (txtPaidAmt.Text != "")
                    purchasedata.PaidAmount = Convert.ToDecimal(txtPaidAmt.Text);
                else
                    purchasedata.PaidAmount = 0;
                if (txtRemainingAmt.Text != "")
                    purchasedata.RemainingAmount = Convert.ToDecimal(txtRemainingAmt.Text);
                else
                    purchasedata.RemainingAmount = 0;
                if (txttranscharges.Text == "")
                    purchasedata.TransportCharges = 0;
                else
                    purchasedata.TransportCharges = Convert.ToDecimal(txttranscharges.Text);
                purchasedata.NetAmount = Convert.ToDecimal(ttnetamt.Text);
                if (chkTPurchase.Checked)
                    purchasedata.TAXType = "TaxPurchase";
                if (txtRoundOff.Text != "")
                    purchasedata.RoundOff = Convert.ToDecimal(txtRoundOff.Text);
                purchasedata.TotalGst = totalCGst + totalSGst + totalIGst;
                purchasedata.TransactionYear = CommonMethod.TransactionYear;
                purchasedata.CompId = CommonMethod.CompId;
                purchasedata.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                if (chkGstTransport.Checked == true)
                    purchasedata.TransportGst = Convert.ToDecimal(txtGstTransport.Text);
                else
                    purchasedata.TransportGst = 0;
                purchasedata.PackagingAndForwarding = Convert.ToDecimal(txtPackaging.Text);
                purchasedata.PackagingGstPercent = Convert.ToDecimal(txtGstPackpercent.Text);
                purchasedata.PackagingGstAmt = Convert.ToDecimal(lblpackGST.Text);
                if (txtfreightchrgs.Text == "") { txtfreightchrgs.Text = "0"; }
                if (txtexciseduty.Text == "") { txtexciseduty.Text = "0"; }
                purchasedata.Freightchrges = Convert.ToDecimal(txtfreightchrgs.Text);
                purchasedata.ExciseDuty = Convert.ToDecimal(txtexciseduty.Text);
                if (txtNarration.Text != "")
                    purchasedata.Narration = Convert.ToString(txtNarration.Text);
                else
                    purchasedata.Narration = "";
                purchasedata.Id = PurchaeId;
                purchasedata.ManualPurRefNo = txtMRefNo.Text.Trim();
                pno = Convert.ToInt32(purchasedata.PorderNo);
                pmasterrepo.Edit(purchasedata);
                pmasterrepo.Save();
                updateSupplierDataInRemainingPayment();
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void updateSupplierDataInRemainingPayment()
        {
            try
            {
                SupplierRemainingPaymentRepository payrepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPaymentRepository sremainingpaymentrepo = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> remainingdata = new List<SupplierRemainingPayment>();
                if (!string.IsNullOrEmpty(Convert.ToString(pno)))
                {
                    remainingdata = sremainingpaymentrepo.GetAll().Where(t => t.BillId == pno && t.Type != "Receipt" && t.Type != "OpeningBalance" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    int i = 0;
                    while (remainingdata.Count > i)
                    {
                        if (Convert.ToInt32(cmbsuppliername.SelectedValue) != remainingdata[i].SupplierId)
                        {
                            remainingdata[i].SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                            remainingdata[i].Suppliername = cmbsuppliername.Text;
                            payrepo.Edit(remainingdata[i]);
                            payrepo.Save();
                        }
                        i++;
                    }
                }
                payrepo.Dispose();
                sremainingpaymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void updateremainingpayment(string mode)
        {
            //Update Purchase billid with total calculation
            try
            {
                SupplierRemainingPaymentRepository sremainingpaymentrepo = new SupplierRemainingPaymentRepository();
                var remainingpay = sremainingpaymentrepo.GetAll().Where(t => t.BillId == Convert.ToInt32(pno) && t.Type == "Purchase" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                SupplierRemainingPayment paydata = new SupplierRemainingPayment();
                SupplierRemainingPaymentRepository remainingrepo = new SupplierRemainingPaymentRepository();

                if (mode == "Cheque" || mode == "Cash")
                {
                    if (remainingpay == null)
                    {
                        paydata = new SupplierRemainingPayment();
                        paydata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                        paydata.Suppliername = Convert.ToString(cmbsuppliername.Text);
                        if (ttnetamt.Text != "")
                            paydata.NetAmt = Convert.ToDecimal(ttnetamt.Text);
                        else
                            paydata.NetAmt = Convert.ToDecimal(txtTotalAmt.Text);
                        if (txtPaidAmt.Text != "")
                            paydata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                        else
                            paydata.PaidAmt = 0;
                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                        SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                        remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                        decimal decremamt = 0;
                        if (remainingdata1 != null)
                        {
                            decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                            if (txtRemainingAmt.Text != "")
                                paydata.RemainingAmt = decremamt + Convert.ToDecimal(txtRemainingAmt.Text);
                            else
                                paydata.RemainingAmt = decremamt;
                        }
                        else
                        {
                            if (txtRemainingAmt.Text != "")
                                paydata.RemainingAmt = Convert.ToDecimal(txtRemainingAmt.Text);
                            else
                                paydata.RemainingAmt = 0;
                        }
                        paydata.CreateDate = Convert.ToDateTime(dtpPorderdate.Value);
                        paydata.Type = "Purchase";
                        paydata.PaymentMode = mode;

                        if (mode == "Cheque")
                        {
                            paydata.Chequedate = dtpchequedate.Value;
                            paydata.Bankname = cmbbank.Text;
                            paydata.Chequeno = txtchequeno.Text;
                            try
                            {
                                BankStatementRepository BankStRepo = new BankStatementRepository();
                                SupplierRemainingPaymentRepository supprepo1 = new SupplierRemainingPaymentRepository();
                                var suppremdata = supprepo1.GetAll().Where(t => t.BillId == pno && t.Type == "Purchase" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                BankStatement bdata = new BankStatement();
                                if (suppremdata != null)
                                    bdata = BankStRepo.GetAll().Where(t => t.Bankname == suppremdata.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == suppremdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == suppremdata.Chequeno && t.Withdrawals == Convert.ToDecimal(suppremdata.PaidAmt)).FirstOrDefault();
                                else
                                    bdata = null;
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();

                                bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                bankstdata.TransactionDate = dtpPorderdate.Value;
                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbsuppliername.Text.Trim());
                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankstdata.TransactionType = "Purchase";
                                if (bdata != null)
                                {
                                    if (bdata.Id != 0)
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (bdata.Id == 0)
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                }
                                else if (bdata == null)
                                {
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                }
                                BankStRepo.Dispose();
                                bankrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                        }
                        paydata.BillId = Convert.ToInt32(txtporderno.Text);
                        paydata.BillDate = Convert.ToDateTime(dtpPorderdate.Value);
                        paydata.TransactionYear = CommonMethod.TransactionYear;
                        if (separateLedger == "Y")
                        {
                            DataTable dt = db.GetTable("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.PorderNo='" + txtporderno.Text + "' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");
                            if (dt.Rows.Count != 0)
                                paydata.Status = "Tax";
                            else
                                paydata.Status = "Estimate";
                        }
                        paydata.CompId = CommonMethod.CompId;
                        remainingrepo.Add(paydata);
                        remainingrepo.Save();
                    }
                    else
                    {
                        if (ttnetamt.Text != "")
                            remainingpay.NetAmt = Convert.ToDecimal(ttnetamt.Text);
                        if (txtPaidAmt.Text != "")
                            remainingpay.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                        SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                        remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                        decimal decremamt = 0;
                        if (remainingdata1 != null)
                        {
                            decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                            if (txtRemainingAmt.Text != "")
                            {
                                remainingpay.RemainingAmt = decremamt + Convert.ToDecimal(txtRemainingAmt.Text);
                                decremamt = decremamt + Convert.ToDecimal(txtRemainingAmt.Text);
                            }
                            else
                            {
                                remainingpay.RemainingAmt = decremamt;
                                decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                            }
                        }
                        else
                        {
                            if (txtRemainingAmt.Text != "")
                            {
                                remainingpay.RemainingAmt = Convert.ToDecimal(txtRemainingAmt.Text);
                                decremamt = Convert.ToDecimal(txtRemainingAmt.Text);
                            }
                            else
                            {
                                remainingpay.RemainingAmt = 0;
                                decremamt = 0;
                            }
                        }
                        SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                        List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                        remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id > remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        if (mode == "Cheque")
                        {
                            remainingpay.Chequedate = dtpchequedate.Value;
                            remainingpay.Bankname = cmbbank.Text;
                            remainingpay.Chequeno = txtchequeno.Text;
                            try
                            {
                                BankStatementRepository BankStRepo = new BankStatementRepository();
                                SupplierRemainingPaymentRepository supprepo1 = new SupplierRemainingPaymentRepository();
                                var suppremdata = supprepo1.GetAll().Where(t => t.BillId == pno && t.Type == "Purchase" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                BankStatement bdata = new BankStatement();
                                if (suppremdata != null)
                                    bdata = BankStRepo.GetAll().Where(t => t.Bankname == suppremdata.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == suppremdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == suppremdata.Chequeno && t.Withdrawals == Convert.ToDecimal(suppremdata.PaidAmt)).FirstOrDefault();
                                else
                                    bdata = null;
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();
                                bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                bankstdata.TransactionDate = dtpPorderdate.Value;
                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbsuppliername.Text.Trim());
                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankstdata.TransactionType = "Purchase";
                                if (bdata != null)
                                {
                                    if (bdata.Id != 0)
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (bdata.Id == 0)
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                }
                                else if (bdata == null)
                                {
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                }
                                BankStRepo.Dispose();
                                bankrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                        }
                        remainingpay.PaymentMode = mode;
                        remainingpay.BillDate = Convert.ToDateTime(dtpPorderdate.Value);
                        remainingpay.CreateDate = Convert.ToDateTime(dtpPorderdate.Value);
                        if (separateLedger == "Y")
                        {
                            DataTable dt = db.GetTable("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.PorderNo='" + txtporderno.Text + "' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'");
                            if (dt.Rows.Count != 0)
                                remainingpay.Status = "Tax";
                            else
                                remainingpay.Status = "Estimate";
                        }
                        sremainingpaymentrepo.Edit(remainingpay);
                        sremainingpaymentrepo.Save();

                        int i = 0;
                        foreach (var item in remainingdata2)
                        {
                            item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                            decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                            remainingpayrepo2.Edit(item);
                            remainingpayrepo2.Save();
                            i++;
                        }
                    }
                }
                sremainingpaymentrepo.Dispose();
                remainingrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcomanyname.Text != "" && prefixSelected == false)
                {
                    prefixSelected = true;
                    lstprefixproduct.Text = cmbcomanyname.Text;
                    lstprefixproduct.Visible = true;
                    getProductsbyPrefix(cmbcomanyname.Text);
                    this.ActiveControl = lstprefixproduct;
                }
                else
                    lstprefixproduct.Text = "";
            }
            catch (Exception)
            { }
        }

        private void txtpdiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpdiscount.Text != "")
                {
                    if (Convert.ToDecimal(txtpdiscount.Text) <= 100)
                        getGstCalculation();
                    else
                    {
                        MessageBox.Show("Enter Vat Between 0 to 100 %.", "Warning");
                        txtpdiscount.Text = "0";
                        this.ActiveControl = txtpdiscount;
                    }
                }
                else
                {
                    //txtpdiscount.Text = "0";
                    this.ActiveControl = txtpdiscount;
                }
            }
            catch (Exception)
            {
                txtpdiscount.Text = "0";
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    getGstCalculation();
                    if (glbproductdata != null)
                    {
                        if (glbproductdata.QuantitySubtitle == "Y")
                            txtQuantitySubtitle.Text = Convert.ToString(Convert.ToDecimal(txtQuantity.Text) * glbproductdata.QtySubCount);
                    }
                }
                else
                {
                    //txtQuantity.Text = "0";
                    this.ActiveControl = txtQuantity;
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

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnUpdate.Enabled == false && btnSave.Enabled == false)
                { }
                else
                {
                    if (Convert.ToDecimal(txtDiscount.Text) <= 100)
                        CalculateNetAmt();
                    else
                    {
                        MessageBox.Show("Enter Discount Between 0 to 100 %.", "Warning");
                        txtDiscount.Text = "0";
                        this.ActiveControl = txtDiscount;
                    }
                }
            }
            catch (Exception)
            {
                txtDiscount.Text = "0";
                this.ActiveControl = txtDiscount;
            }
        }

        private void txtSpcDisc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnUpdate.Enabled == false && btnSave.Enabled == false)
                { }
                else
                {
                    if (Convert.ToDecimal(txtSpcDisc.Text) <= 100)
                        CalculateNetAmt();
                    else
                    {
                        MessageBox.Show("Enter Special Discount Between 0 to 100 %.", "Warning");
                        txtSpcDisc.Text = "0";
                        this.ActiveControl = txtSpcDisc;
                    }
                }
            }
            catch (Exception)
            {
                txtSpcDisc.Text = "0";
                this.ActiveControl = txtSpcDisc;
            }
        }

        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPaidAmt.Text != "")
                {
                    decimal a = 0, b = 0;
                    txtRemainingAmt.Text = "0";
                    a = Convert.ToDecimal(ttnetamt.Text);
                    b = Convert.ToDecimal(txtPaidAmt.Text);
                    if (a >= b)
                        txtRemainingAmt.Text = Convert.ToString((a - b));
                    else
                    {
                        txtPaidAmt.Text = a.ToString();
                        txtRemainingAmt.Text = Convert.ToString((a - Convert.ToDecimal(txtPaidAmt.Text)));
                        MessageBox.Show("Paid Amount Less or equal to Net Amount.", "Warning");
                    }
                }
                else
                {
                    txtPaidAmt.Text = "0";
                    this.ActiveControl = txtPaidAmt;
                }
            }
            catch (Exception)
            {
                txtPaidAmt.Text = "0";
            }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                gvProductRemStock.Visible = false;
                refreshFlag = false;
                if (flag == 1)
                {
                    string proname = "", company = "", unit = "";
                    if ((txtpname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtpname.Text.Trim().Split(',')[0];
                            company = txtpname.Text.Trim().Split(',')[1];
                            unit = txtpname.Text.Trim().Split(',')[2];
                        }
                        catch (Exception)
                        { }
                        glbproductrepo = new ProductRepository();
                        glbproductdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            glbproductdata = glbproductrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        else
                            glbproductdata = glbproductrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (glbproductdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        else
                        {
                            try
                            {
                                DataTable sdt = new DataTable();
                                sdt = db.GetTable("Select st.StateId from StateMaster st inner join SupplierInformation s on s.StateId=st.Id where s.SupplierCode=" + Convert.ToInt32(cmbsuppliername.SelectedValue));
                                StateId = Convert.ToInt32(sdt.Rows[0]["StateId"]);
                            }
                            catch (Exception)
                            { }
                            getProductType(proname, company, unit);
                            if (productentrytype == 9)
                            {
                                txtQuantity.Text = "1";
                                txtQuantity.Enabled = false;
                            }
                            else
                                txtQuantity.Enabled = true;
                            cmbUnit.Items.Clear();
                            cmbUnit.DisplayMember = "Text";
                            cmbUnit.ValueMember = "Value";
                            cmbUnit.Items.Add(new { Text = glbproductdata.Uniti, Value = glbproductdata.Uniti });
                            if (glbproductdata.AlternateUnit != null && glbproductdata.AlternateUnit != "")
                                cmbUnit.Items.Add(new { Text = glbproductdata.AlternateUnit, Value = glbproductdata.AlternateUnit });
                            if (glbproductdata.TripleUnit != null && glbproductdata.TripleUnit != "")
                                cmbUnit.Items.Add(new { Text = glbproductdata.TripleUnit, Value = glbproductdata.TripleUnit });
                            txtMRP.Text = glbproductdata.Rate.ToString();
                            if (colIndex != -1)
                            {
                                if (proname != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value) && company != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Company"].Value) && unit != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Unit"].Value))
                                {
                                    cmbUnit.SelectedIndex = 0;
                                    txtpurchaseprice.Text = Convert.ToString(glbproductdata.PurchaseRate);
                                    if (glbproductdata.PurchaseDiscount != null && glbproductdata.PurchaseDiscount != 0)
                                        txtpdiscount.Text = glbproductdata.PurchaseDiscount.ToString();
                                    if (productentrytype == 9)
                                    {
                                        txtQuantity.Text = "1";
                                        txtQuantity.Enabled = false;
                                    }
                                    else
                                        txtQuantity.Enabled = true;
                                    if (glbproductdata.Pigstpercent != null && glbproductdata.Pigstpercent != 0)
                                    {
                                        if (StateId == 27)
                                        {
                                            cmbsgst.Text = Convert.ToString(Convert.ToDecimal(glbproductdata.Pigstpercent) / 2);
                                            cmbcgst.Text = Convert.ToString(Convert.ToDecimal(glbproductdata.Pigstpercent) / 2);
                                            cmbigst.Text = "0.00";
                                        }
                                        else
                                        {
                                            cmbsgst.Text = Convert.ToString(glbproductdata.Psgstpercent);
                                            cmbcgst.Text = Convert.ToString(glbproductdata.Pcgstpercent);
                                            cmbigst.Text = Convert.ToString(glbproductdata.Pigstpercent);
                                        }
                                    }
                                    else if (glbproductdata.Psgstpercent != null && glbproductdata.Psgstpercent != 0 && glbproductdata.Pcgstpercent != null && glbproductdata.Pcgstpercent != 0)
                                    {
                                        if (StateId == 27)
                                        {
                                            cmbsgst.Text = Convert.ToString(glbproductdata.Psgstpercent);
                                            cmbcgst.Text = Convert.ToString(glbproductdata.Pcgstpercent);
                                            cmbigst.Text = Convert.ToString(glbproductdata.Pigstpercent);
                                        }
                                        else
                                        {
                                            cmbsgst.Text = "0.00";
                                            cmbcgst.Text = "0.00";
                                            cmbigst.Text = Convert.ToString(Convert.ToDecimal(glbproductdata.Pcgstpercent) + Convert.ToDecimal(glbproductdata.Psgstpercent));
                                        }
                                    }
                                    else
                                    {
                                        cmbsgst.Text = Convert.ToString(glbproductdata.Psgstpercent);
                                        cmbcgst.Text = Convert.ToString(glbproductdata.Pcgstpercent);
                                        cmbigst.Text = Convert.ToString(glbproductdata.Pigstpercent);
                                    }
                                    if (glbproductdata.Cess != null)
                                        txtpCess.Text = Convert.ToString(glbproductdata.Cess);
                                    else
                                        txtpCess.Text = "0";
                                }
                                else
                                {
                                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["PurUnit"].Value);
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["Price"].Value.ToString()))
                                        txtpurchaseprice.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Price"].Value);
                                    else
                                        txtpurchaseprice.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["Amount"].Value.ToString()))
                                        txtamount.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Amount"].Value);
                                    else
                                        txtamount.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["Disc"].Value.ToString()))
                                        txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Disc"].Value);
                                    else
                                        txtpdiscount.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["SpDisc"].Value.ToString()))
                                        txtpSpcDisc.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["SpDisc"].Value);
                                    else
                                        txtpSpcDisc.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["SGST"].Value.ToString()))
                                        cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["SGST"].Value);
                                    else
                                        cmbsgst.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["SGSTAmt"].Value.ToString()))
                                        lblpvat.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["SGSTAmt"].Value);
                                    else
                                        lblpvat.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["CGST"].Value.ToString()))
                                        cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["CGST"].Value);
                                    else
                                        cmbcgst.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["CGSTAmt"].Value.ToString()))
                                        lblCGst.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["CGSTAmt"].Value);
                                    else
                                        lblCGst.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["IGST"].Value.ToString()))
                                        cmbigst.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["IGST"].Value);
                                    else
                                        cmbigst.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["IGSTAmt"].Value.ToString()))
                                        lblIGst.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["IGSTAmt"].Value);
                                    else
                                        lblIGst.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["Cess"].Value.ToString()))
                                        txtpCess.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Cess"].Value);
                                    else
                                        txtpCess.Text = "0";
                                    if (!String.IsNullOrEmpty(GvProductInfo.Rows[colIndex].Cells["TotalAmount"].Value.ToString()))
                                        txtpTotalAmt.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["TotalAmount"].Value);
                                    else
                                        txtpTotalAmt.Text = "0";
                                }
                            }
                            else
                            {
                                cmbUnit.SelectedIndex = 0;
                                txtpurchaseprice.Text = Convert.ToString(glbproductdata.PurchaseRate);
                                if (glbproductdata.PurchaseDiscount != null && glbproductdata.PurchaseDiscount != 0)
                                    txtpdiscount.Text = glbproductdata.PurchaseDiscount.ToString();
                                if (glbproductdata.Pigstpercent != null && glbproductdata.Pigstpercent != 0)
                                {
                                    if (StateId == 27)
                                    {
                                        cmbsgst.Text = Convert.ToString(Convert.ToDecimal(glbproductdata.Pigstpercent) / 2);
                                        cmbcgst.Text = Convert.ToString(Convert.ToDecimal(glbproductdata.Pigstpercent) / 2);
                                        cmbigst.Text = "0.00";
                                    }
                                    else
                                    {
                                        cmbsgst.Text = Convert.ToString(glbproductdata.Psgstpercent);
                                        cmbcgst.Text = Convert.ToString(glbproductdata.Pcgstpercent);
                                        cmbigst.Text = Convert.ToString(glbproductdata.Pigstpercent);
                                    }
                                }
                                else if (glbproductdata.Psgstpercent != null && glbproductdata.Psgstpercent != 0 && glbproductdata.Pcgstpercent != null && glbproductdata.Pcgstpercent != 0)
                                {
                                    if (StateId == 27)
                                    {
                                        cmbsgst.Text = Convert.ToString(glbproductdata.Psgstpercent);
                                        cmbcgst.Text = Convert.ToString(glbproductdata.Pcgstpercent);
                                        cmbigst.Text = Convert.ToString(glbproductdata.Pigstpercent);
                                    }
                                    else
                                    {
                                        cmbsgst.Text = "0.00";
                                        cmbcgst.Text = "0.00";
                                        cmbigst.Text = Convert.ToString(Convert.ToDecimal(glbproductdata.Pcgstpercent) + Convert.ToDecimal(glbproductdata.Psgstpercent));
                                    }
                                }
                                else
                                {
                                    cmbsgst.Text = Convert.ToString(glbproductdata.Psgstpercent);
                                    cmbcgst.Text = Convert.ToString(glbproductdata.Pcgstpercent);
                                    cmbigst.Text = Convert.ToString(glbproductdata.Pigstpercent);
                                }
                                if (glbproductdata.Cess != null)
                                    txtpCess.Text = Convert.ToString(glbproductdata.Cess);
                                else
                                    txtpCess.Text = "0";
                            }
                            ProductCategory = glbproductdata.Category;
                            txtHSN.Text = glbproductdata.HSNCode;
                            barcodestatus = glbproductdata.BarcodeStatus;
                            if (barcodestatus == "Static Barcode" || barcodestatus == "Our Barcode")
                            {
                                if (barcodestatus == "Static Barcode")
                                {
                                    txtstaticBarcode.Enabled = true;
                                    txtSaleRate.Enabled = true;
                                }
                                else
                                    txtSaleRate.Enabled = true;
                            }
                            else
                            {
                                txtstaticBarcode.Enabled = false;
                                txtstaticBarcode.Text = string.Empty;
                                txtSaleRate.Enabled = false;
                            }
                            getProductType(proname, company, unit);
                            AdjustPanelDetails(productentrytype);
                            //------------- check for batch used
                            if (pnlIntegratedDtls.Visible == true)
                            {
                                BatchRepository batchrepo = new BatchRepository();
                                var batch = batchrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.ExpiryDate.Value.ToString("MM/dd/yyyy") == dtpExpiry.Value.ToString("MM/dd/yyyy")).FirstOrDefault();
                                if (batch != null)
                                {
                                    if (batch.UsedQty > 0)
                                    {
                                        txtBatchNo.Enabled = false;
                                        dtpExpiry.Enabled = false;
                                    }
                                }
                                batchrepo.Dispose();
                                if (txtBatchNo.Enabled == true)
                                    this.ActiveControl = txtBatchNo;
                            }
                            else
                                this.ActiveControl = txtQuantity;
                        }
                    }
                    else
                    {
                        if (txtpname.Text != "")
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                    }
                    if (btnSave.Enabled == false)
                    {
                        decimal rate;
                        if (txtpurchaseprice.Text == "")
                            rate = 0;
                        else
                            rate = Convert.ToDecimal(txtpurchaseprice.Text);
                        decimal qty = Convert.ToDecimal(txtQuantity.Text);
                        txtamount.Text = Convert.ToString(rate * qty);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                frmPurchaseOrderList compinfo = new frmPurchaseOrderList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnorderlist_Click(object sender, EventArgs e)
        {
            try
            {
                pnlIntegratedDtls.Visible = false;
                clear();
                purorderinfo = this;
                frmPurchaseOrder p = new frmPurchaseOrder();
                p.Visible = false;
                frmPurchaseOrderList allpro = new frmPurchaseOrderList();
                allpro.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void chkcheque_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcheque.Checked == true)
                {
                    pnlBank.Visible = true;
                    if (txtPaidAmt.Text.Trim() != "0" && txtPaidAmt.Text.Trim() != "00" && txtPaidAmt.Text.Trim() != "000" && txtPaidAmt.Text.Trim() != "")
                    {
                        if (chkcheque.Checked == true)
                        {
                            label42.Enabled = true;
                            label43.Enabled = true;
                            label44.Enabled = true;
                            cmbbank.Enabled = true;
                            txtchequeno.Enabled = true;
                            dtpchequedate.Enabled = true;
                        }
                        else
                        {
                            label42.Enabled = false;
                            label43.Enabled = false;
                            label44.Enabled = false;
                            cmbbank.Enabled = false;
                            txtchequeno.Enabled = false;
                            dtpchequedate.Enabled = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("For Cheque Payment Paid Amount " + Environment.NewLine + " Greater than Zero.", "Warning");
                        chkcheque.CheckState = 0;
                        this.ActiveControl = txtPaidAmt;
                    }
                }
                else
                {
                    label42.Enabled = false;
                    label43.Enabled = false;
                    label44.Enabled = false;
                    cmbbank.Enabled = false;
                    txtchequeno.Enabled = false;
                    dtpchequedate.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        private void rbtnaddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                frmProductInformation pro = new frmProductInformation();
                pro.ShowDialog();
                fmPurchaseOrder_Load(sender, EventArgs.Empty);
                this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void txtpname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((txtpname.Text).Contains(","))
                {
                    flag = 1;
                    gvProductRemStock.Visible = true;
                    gvProductRemStock.Rows.Clear();
                    string proname = "", company = "", unit = "";
                    try
                    {
                        proname = txtpname.Text.Trim().Split(',')[0];
                        company = txtpname.Text.Trim().Split(',')[1];
                        unit = txtpname.Text.Trim().Split(',')[2];
                        getProductType(proname, company, unit);
                        if (productentrytype == 9)
                        {
                            gvProductRemStock.Visible = false;
                            gvProductRemStock.Rows.Clear();
                        }
                        else
                        {
                            try
                            {
                                getProductData(proname, company, unit);
                                if (productentrytype != 6)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = proname;
                                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                        command.CommandTimeout = 300;
                                        DataTable dt = new DataTable();
                                        dt.Load(command.ExecuteReader());
                                        if (dt.Rows.Count > 0)
                                        {
                                            gvProductRemStock.Rows.Add();
                                            gvProductRemStock.Rows[0].Cells[0].Value = dt.Rows[0]["ProductName"].ToString();
                                            if (dt.Rows.Count == 1)
                                                gvProductRemStock.Rows[0].Cells[1].Value = dt.Rows[0]["RemQty"].ToString();
                                            else
                                                gvProductRemStock.Rows[0].Cells[1].Value = dt.Compute("Sum(RemQty)", "RemQty>=0").ToString();
                                            gvProductRemStock.Rows[0].Cells[2].Value = dt.Rows[0]["Unit"].ToString();
                                        }
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                else
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPGetAssemblyProductRemQty", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@proname", SqlDbType.VarChar).Value = proname;
                                        command.Parameters.Add("@cname", SqlDbType.VarChar).Value = company;
                                        command.Parameters.Add("@uname", SqlDbType.VarChar).Value = unit;
                                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "P";
                                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = "a";
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);

                                        SqlDataReader reader = command.ExecuteReader();
                                        gvProductRemStock.Rows.Clear();
                                        while (reader.Read())
                                        {
                                            gvProductRemStock.Rows.Add();
                                            gvProductRemStock.Rows[0].Cells[0].Value = reader.GetString(1);
                                            gvProductRemStock.Rows[0].Cells[1].Value = reader.GetSqlDecimal(10).ToString();
                                            gvProductRemStock.Rows[0].Cells[2].Value = reader.GetString(3);
                                        }
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            { }
        }

        private void txtFreeQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFreeQty.Text != "")
                {
                    decimal pqty, fqty;
                    if (txtQuantity.Text != "")
                    {
                        pqty = Convert.ToDecimal(txtQuantity.Text);
                        fqty = Convert.ToDecimal(txtFreeQty.Text);
                        if (fqty > pqty)
                        {
                            MessageBox.Show("Free Quantity must be less than Quantity.", "Warning");
                            txtFreeQty.Text = "0";
                            this.ActiveControl = txtFreeQty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Quantity must be entered.", "Warning");
                        this.ActiveControl = txtQuantity;
                    }
                }
                else
                {
                    txtFreeQty.Text = "0";
                    this.ActiveControl = txtFreeQty;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (productentrytype == 1 || productentrytype == 2)
                    {
                        pnlIntegratedDtls.Visible = true;
                        this.ActiveControl = txtBatchNo;
                    }
                    else
                        this.ActiveControl = txtQuantity;
                }
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmProductInformation product = new frmProductInformation();
                        product.ShowDialog();
                        getProductList();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnreturn.Text == "Show Return")
                {
                    grppurretDetail.Visible = true;
                    gvpurreturnproducts.Rows.Clear();
                    PReturnDetailRepository PRDetailRepo = new PReturnDetailRepository();
                    List<PurchaseReturnDetail> PRDetailData = new List<PurchaseReturnDetail>();
                    PRDetailData = PRDetailRepo.GetAll().Where(t => t.PurchaseReturnMaster.PorderNo == Convert.ToInt32(txtporderno.Text) && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).ToList();
                    int i = 0;
                    foreach (var item in PRDetailData)
                    {
                        gvpurreturnproducts.Rows.Add();
                        gvpurreturnproducts.Rows[i].Cells["PRId"].Value = item.Id;
                        gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value = item.ProductName;
                        gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value = item.CompanyName;
                        gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value = item.Unit;
                        gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value = item.PurchaseUnit;
                        if (item.Unit != item.PurchaseUnit)
                        {
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                            gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                            gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value = Convert.ToDecimal(item.FreeQty) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                        }
                        else
                        {
                            gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value = item.Quantity;
                            gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value = item.FreeQty;
                        }
                        gvpurreturnproducts.Rows[i].Cells["PRCode"].Value = item.Code;
                        gvpurreturnproducts.Rows[i].Cells["PRPrice"].Value = item.PurchasePrice;
                        gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value = item.Amount;
                        gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value = item.Discount;
                        gvpurreturnproducts.Rows[i].Cells["PRVat"].Value = item.Vat;
                        gvpurreturnproducts.Rows[i].Cells["PRTotalAmt"].Value = item.TotalAmount;
                        gvpurreturnproducts.Rows[i].Cells["PRReason"].Value = item.Reason;
                        i++;
                    }
                    gvpurreturnproducts.ReadOnly = true;
                    btnreturn.Text = "Hide Return";
                    PRDetailRepo.Dispose();
                }
                else
                {
                    grppurretDetail.Visible = false;
                    btnreturn.Text = "Show Return";
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsuppliername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkTPurchase;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmSupplierInformation supplier = new frmSupplierInformation();
                        supplier.ShowDialog();
                        fillsupplier();
                        this.ActiveControl = cmbsuppliername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtFreeQty;
            }
            catch (Exception)
            { }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNetAmt();
            }
            catch (Exception)
            { }
        }

        private void ttnetamt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ttnetamt.Text) || !string.IsNullOrWhiteSpace(ttnetamt.Text))
                {
                    if (btnSave.Enabled == true)
                    {
                        if (string.IsNullOrEmpty(txtPaidAmt.Text) || string.IsNullOrWhiteSpace(txtPaidAmt.Text))
                            txtPaidAmt.Text = "0";
                        txtRemainingAmt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(ttnetamt.Text) - Convert.ToDecimal(txtPaidAmt.Text)), 2, MidpointRounding.AwayFromZero));
                    }
                    else if (btnSave.Enabled == false)
                    {
                        if (Convert.ToDecimal(ttnetamt.Text) != 0)
                        {
                            if (Convert.ToDecimal(ttnetamt.Text) >= Pamt)
                            {
                                txtPaidAmt.Text = "0";
                                if (remamt >= tempPaidamt)
                                    txtPaidAmt.Text = tempPaidamt.ToString();
                                txtRemainingAmt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(ttnetamt.Text) - Convert.ToDecimal(txtPaidAmt.Text)), 2, MidpointRounding.AwayFromZero));
                            }
                            else
                            {
                                MessageBox.Show("For This Purchase Order some Amount Paid " + Environment.NewLine + " So u can Decrease Amount less than that Amount.", "Warning");
                                btnSave.Enabled = false;
                                btnUpdate.Enabled = false;
                                btnNew.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    int Quantity_length = Convert.ToInt32(txtQuantity.Text);
                    flgBatchQty = 0;
                    string Proname = "", company = "", unit = "";
                    if (txtpname.Text != "")
                    {
                        Proname = txtpname.Text.Trim().Split(',')[0];
                        company = txtpname.Text.Trim().Split(',')[1];
                        unit = txtpname.Text.Trim().Split(',')[2];
                    }
                    getProductType(Proname, company, unit);

                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (productdata.QuantitySubtitle == "Y" && productentrytype == 0)
                        QtySubtitle = true;
                    else
                        QtySubtitle = false;
                    if (productentrytype == 3)
                    {
                        if (colIndex == -1)
                        {
                            showsize = true;
                            AdjustPanelDetails(productentrytype);
                        }
                    }
                    if (productentrytype == 1 || productentrytype == 2)
                    {
                        try
                        {
                            if (btnSave.Enabled == false && txtQuantity.Text != "" && txtBatchNo.Text != "")
                            {
                                BatchRepository batchrepo = new BatchRepository();
                                if (productdata != null)
                                {
                                    decimal remqty = 0, originalpqty = 0;
                                    if (colIndex != -1)
                                    {
                                        originalpqty = Convert.ToDecimal(GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value) + Convert.ToDecimal(GvProductInfo.Rows[colIndex].Cells["FrQty"].Value);
                                        PurchaseMasterRepository PurMRepo = new PurchaseMasterRepository();
                                        PurchaseMaster PurMData = new PurchaseMaster();
                                        PurMData = PurMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (PurMData != null)
                                        {
                                            BatchRepository BatchRepo = new BatchRepository();
                                            Batch BatchData = new Batch();
                                            decimal Usedqty = Convert.ToDecimal(BatchRepo.GetAll().Where(t => t.PorderNo == PurMData.Id && t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                            decimal srqty = 0;
                                            try
                                            {
                                                BatchRepository batchrpstr = new BatchRepository();
                                                List<Batch> BData = new List<Batch>();
                                                BData = batchrpstr.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.Status == "1" && t.PorderNo == PurMData.Id && t.CompId == CommonMethod.CompId).ToList();
                                                if (BData != null)
                                                {
                                                    foreach (var item1 in BData)
                                                    {
                                                        try
                                                        {
                                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                            GoodReturnDetail SRData = new GoodReturnDetail();
                                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo).FirstOrDefault();
                                                            if (SRData != null)
                                                                srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                            SRRepo.Dispose();
                                                        }
                                                        catch (Exception)
                                                        { }
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            remqty = originalpqty - (Usedqty - srqty);
                                            if ((Usedqty - srqty) == Convert.ToDecimal(txtQuantity.Text))
                                                flgBatchQty = 1;
                                            else
                                                flgBatchQty = 0;
                                            if ((Usedqty - srqty) > Convert.ToDecimal(txtQuantity.Text))
                                            {
                                                MessageBox.Show("You sold products from this batch.", "Warning");
                                                txtQuantity.Text = originalpqty.ToString();
                                                flgBatchQty = -1;
                                                this.ActiveControl = txtQuantity;
                                            }
                                        }
                                    }
                                }
                                productrepo.Dispose();
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    //------------------------------------------------

                    if (colIndex != -1 && flgBatchQty != -1)
                    {
                        decimal txtqty = 0;
                        PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                        PurchaseReturnMaster PRMData = new PurchaseReturnMaster();
                        PRMData = PRMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (PRMData != null)
                        {
                            var SRDData = PRMData.PurchaseReturnDetails;
                            foreach (var item in SRDData)
                            {
                                if (Convert.ToString(GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value) == item.ProductName && Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Company"].Value) == item.CompanyName && Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Unit"].Value) == item.Unit)
                                {
                                    if (Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Unit"].Value) != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["PurUnit"].Value))
                                    {
                                        if (CommonMethod.commProduct == true)
                                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                        else
                                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        txtqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                    }
                                    else
                                        txtqty = Convert.ToDecimal(txtQuantity.Text);
                                    if (txtqty >= Convert.ToDecimal(item.Quantity))
                                    {
                                        if (ProductCategory == "Motor" && ismandatory == false)
                                        {
                                            if (MessageBox.Show("Do you want to enter series", "Enter Series", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                flgmandatory = true;
                                                ismandatory = true;
                                                goto t;
                                            }
                                            else
                                            {
                                                flgmandatory = true;
                                                ismandatory = false;
                                                goto s;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        db.CloseConnection();
                                        MessageBox.Show("Some quantity is returned of this product so can't update to less quanity.", "Warning");
                                        txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value);
                                        this.ActiveControl = txtQuantity;
                                        goto s;
                                    }
                                }
                            }
                        }
                        else  //20012018
                        {
                            if (ProductCategory == "Motor")
                            {
                                if (Convert.ToDecimal(txtQuantity.Text) != 1)
                                {
                                    MessageBox.Show("Quantity must be 1 for update.", "Warning");
                                    txtQuantity.Text = "1";
                                }
                            }
                            else
                                goto s;
                        }
                        PRMRepo.Dispose();
                        goto s;
                    }
                    if (colIndex == -1 && flgBatchQty != -1)
                    {
                        if (ProductCategory == "Motor" && refreshFlag == false)
                        {
                            if (MessageBox.Show("Do you want to enter series", "Enter Series", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                flgmandatory = true;
                                ismandatory = true;
                                refreshFlag = true;  // this event calls more than one time, this flag is used to call message only one time
                                goto t;
                            }
                            else
                            {
                                flgmandatory = true;
                                ismandatory = false;
                                refreshFlag = true;
                                goto s;
                            }
                        }
                        else
                        {
                            if (refreshFlag == false)
                            {
                                dtgvcode.Rows.Clear();
                                goto s;
                            }
                        }
                    }
                    else
                        goto s;
                t:
                    {
                        dtgvcode.Rows.Clear();
                        for (int i = 0; i < Quantity_length; i++)
                        {
                            dtgvcode.Rows.Add();
                            if (colIndex != -1)
                                dtgvcode.Rows[i].Cells[0].Value = GvProductInfo.Rows[colIndex].Cells["Code"].Value.ToString();
                            else
                                dtgvcode.Rows[i].Cells[0].Value = "";
                        }
                        dtgvcode.Visible = true;
                        this.ActiveControl = dtgvcode;
                        dtgvcode.Height = dtgvcode.ColumnHeadersHeight + dtgvcode.Rows[0].Height * (dtgvcode.RowCount + 1);
                    }
                s:
                    { }
                    if (QtySubtitle == true)
                        pnlQtySubtitle.Visible = true;
                    else
                    {
                        pnlQtySubtitle.Visible = false;
                        txtQuantitySubtitle.Text = "0";
                    }
                }
                else
                    txtQuantity.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void txttranscharges_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txttranscharges.Text != "")
                {
                    if (btnUpdate.Enabled == false && btnSave.Enabled == false)
                    { }
                    else
                        CalculateNetAmt();
                }
                else
                {
                    txttranscharges.Text = "0";
                    this.ActiveControl = txttranscharges;
                }
            }
            catch (Exception)
            {
                txttranscharges.Text = "0";
            }
        }

        private void dtgvcode_Leave(object sender, EventArgs e)
        {
            try
            {
                bool findBlank = false;
                int currentRow_code = 0;
                if (dtgvcode.Visible == true)
                {
                    if (dtgvcode.RowCount > 0)
                    {
                        for (int i = 0; i < dtgvcode.RowCount - 1; i++)
                        {
                            if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                            {
                                findBlank = true;
                                currentRow_code = i;
                            }
                        }
                        if (findBlank == true)
                        {
                            MessageBox.Show("All Quantity must have Serial No or Code.", "Warning");
                            dtgvcode.Visible = true;
                            this.ActiveControl = dtgvcode;
                            dtgvcode.CurrentCell = dtgvcode[0, currentRow_code];
                            dtgvcode.CurrentCell.Selected = true;
                        }
                        else
                        {
                            dtgvcode.Visible = false;
                            this.ActiveControl = txtFreeQty;
                        }
                    }
                    else
                    {
                        dtgvcode.Visible = false;
                        this.ActiveControl = txtFreeQty;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (txtpname.Text != "")
                {
                    gvprepurchasedata.Visible = true;
                    string newpname = "";
                    if ((txtpname.Text).Contains("'"))
                        newpname = txtpname.Text.Replace("'", "''");
                    else
                        newpname = txtpname.Text.Trim();
                    string proname = newpname.Split(',')[0];
                    string company = newpname.Split(',')[1];
                    string unit = newpname.Split(',')[2];
                    SqlDataAdapter da = null;
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),m.PorderDate,103) as 'PO Date',cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then d.PurchasePrice else (d.PurchasePrice/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit )) end as Decimal(18,2)) as 'Price', d.Discount as 'Disc', m.Vat, cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then (d.TotalAmount/d.Quantity)+(d.TotalAmount/d.Quantity)*(m.Vat/100) else  ((d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit )/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit))))+ (d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit)/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit)))*(m.Vat/100)))/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ") end as decimal(18,2)) as 'TotalAmount' ,d.Unit FROM PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo  where  ProductName='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and m.Suppliername<>'First stock' and m.TransactionYear='" + CommonMethod.TransactionYear + "' ORDER BY d.Id DESC  ", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),m.PorderDate,103) as 'PO Date',cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then d.PurchasePrice else (d.PurchasePrice/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")) end as Decimal(18,2)) as 'Price', d.Discount as 'Disc', m.Vat, cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then (d.TotalAmount/d.Quantity)+(d.TotalAmount/d.Quantity)*(m.Vat/100) else  ((d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + "))))+ (d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")))*(m.Vat/100)))/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ") end as decimal(18,2)) as 'TotalAmount' ,d.Unit FROM PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo  where  ProductName='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and m.Suppliername<>'First stock' and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' ORDER BY d.Id DESC  ", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvprepurchasedata.DataSource = dt;
                    gvprepurchasedata.Refresh();
                    if (gvprepurchasedata.Rows.Count != 0)
                    {
                        gvprepurchasedata.Columns[0].Width = 70;
                        gvprepurchasedata.Columns[1].Width = 70;
                        gvprepurchasedata.Columns[2].Width = 45;
                        gvprepurchasedata.Columns[3].Width = 45;
                        gvprepurchasedata.Columns[4].Width = 80;
                        gvprepurchasedata.Columns[5].Width = 53;
                    }
                    da.Dispose();
                    dt.Dispose();
                    this.ActiveControl = gvprepurchasedata;
                }
                else
                {
                    MessageBox.Show("Select Product Name first.", "Warning");
                    this.ActiveControl = txtpname;
                }
            }
            catch (Exception)
            { gvprepurchasedata.Visible = false; }
        }

        private void gvprepurchasedata_Leave(object sender, EventArgs e)
        {
            try
            {
                gvprepurchasedata.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void gvprepurchasedata_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
                    this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void txtamount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtamount.Text != "")
                {
                    if (txtQuantity.Text != "")
                        txtpurchaseprice.Text = Convert.ToString(Math.Round((Convert.ToDecimal(txtamount.Text) / Convert.ToDecimal(txtQuantity.Text)), 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        public void RoundOff(decimal netamt)
        {
            try
            {
                decimal b = Math.Round(netamt);
                if (netamt > b)
                    txtRoundOff.Text = "-" + Convert.ToString(Math.Round((netamt - b), 2, MidpointRounding.AwayFromZero));
                else
                    txtRoundOff.Text = Convert.ToString(Math.Round((b - netamt), 2, MidpointRounding.AwayFromZero));
                ttnetamt.Text = Convert.ToString(Math.Round((Convert.ToDecimal(netamt) + Convert.ToDecimal(txtRoundOff.Text)), 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        public void CalculateNetAmt(bool minimizeAmt = false)
        {
            try
            {
                decimal d, v, t, total, damt = 0, vamt = 0, netamt, sdamt = 0, s, packaging_Amt = 0;
                total = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtDiscount.Text != "")//&& Convert.ToDecimal(txtDiscount.Text) != 0)
                {
                    d = Convert.ToDecimal(txtDiscount.Text);
                    damt = (total * d) / 100;
                    txtDiscAmt.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
                }
                s = Convert.ToDecimal(txtSpcDisc.Text);
                sdamt = ((total - damt) * s) / 100;
                lblSpcDisc.Text = Convert.ToString(Math.Round(sdamt, 5, MidpointRounding.AwayFromZero));
                if (vat5amt == 0 && vat12amt == 0)
                {
                    v = Convert.ToDecimal(ttvat.Text);
                    vamt = ((total - damt - sdamt) * v) / 100;
                    lblvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                }
                if (minimizeAmt == true)
                {
                    t = Convert.ToDecimal(txttranscharges.Text) - Convert.ToDecimal(txtGstTransport.Text);
                    packaging_Amt = Convert.ToDecimal(txtPackaging.Text) - Convert.ToDecimal(lblpackGST.Text);
                }
                else
                {
                    t = Convert.ToDecimal(txttranscharges.Text) + Convert.ToDecimal(txtGstTransport.Text);
                    packaging_Amt = Convert.ToDecimal(txtPackaging.Text) + Convert.ToDecimal(lblpackGST.Text);
                }
                decimal freightcharge = 0, ex_duty = 0;
                if (txtfreightchrgs.Text != "")
                    freightcharge = Convert.ToDecimal(txtfreightchrgs.Text);
                if (txtexciseduty.Text != "")
                    ex_duty = Convert.ToDecimal(txtexciseduty.Text);

                netamt = total - damt - sdamt + vamt + t + packaging_Amt - freightcharge + ex_duty;
                ttnetamt.Text = Convert.ToString(Math.Round(netamt, 2, MidpointRounding.AwayFromZero));
                if (rdoff == 1)
                    RoundOff(netamt);
            }
            catch (Exception)
            { }
        }

        private void txtDiscAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (btnUpdate.Enabled == false && btnSave.Enabled == false)
                { }
                else
                {
                    if (txtDiscAmt.Text != "" && txtTotalAmt.Text != "")
                    {
                        if (Convert.ToDecimal(txtDiscAmt.Text) <= Convert.ToDecimal(txtTotalAmt.Text))
                        {
                            try
                            {
                                decimal d, v, t, total, damt = 0, vamt = 0, netamt, sdamt = 0, s;
                                total = Convert.ToDecimal(txtTotalAmt.Text);
                                if (txtDiscAmt.Text != "")//&& Convert.ToDecimal(txtDiscAmt.Text) != 0)
                                {
                                    damt = Convert.ToDecimal(txtDiscAmt.Text);
                                    decimal disc = damt * 100 / total;
                                    txtDiscount.Text = Convert.ToString(Math.Round(disc, 5, MidpointRounding.AwayFromZero));
                                    d = Convert.ToDecimal(txtDiscount.Text);
                                }
                                s = Convert.ToDecimal(txtSpcDisc.Text);
                                sdamt = ((total - damt) * s) / 100;
                                lblSpcDisc.Text = Convert.ToString(Math.Round(sdamt, 5, MidpointRounding.AwayFromZero));
                                if (vat5amt == 0 && vat12amt == 0)
                                {
                                    v = Convert.ToDecimal(ttvat.Text);
                                    vamt = ((total - damt - sdamt) * v) / 100;
                                    lblvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                                }
                                t = Convert.ToDecimal(txttranscharges.Text);
                                netamt = total - damt - sdamt + vamt + t + Convert.ToDecimal(txtPackaging.Text) + Convert.ToDecimal(lblpackGST.Text);
                                ttnetamt.Text = Convert.ToString(Math.Round(netamt, 2, MidpointRounding.AwayFromZero));
                                if (rdoff == 1)
                                    RoundOff(netamt);
                            }
                            catch (Exception)
                            { }
                        }
                        else
                        {
                            MessageBox.Show("Enter Discount Less or Equal to Total Amount.", "Warning");
                            txtDiscAmt.Text = "0";
                            this.ActiveControl = txtDiscAmt;
                        }
                    }
                    else
                    {
                        txtDiscAmt.Text = "0";
                        this.ActiveControl = txtDiscAmt;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i >= 48 && i <= 57) || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F11))
            {
                if (txtpname.Text != "")
                {
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation pData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        pData = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    else
                        pData = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    if (pData != null)
                    {
                        if (pData.Image != null)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Height = 283;
                            pictureBox1.Width = 295;
                            byte[] @photoProduct;
                            @photoProduct = (byte[])pData.Image;
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(@photoProduct);
                            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                            this.ActiveControl = txtpname;
                        }
                        else
                        {
                            pictureBox1.Image = null;
                            MessageBox.Show("Image Not Available.", "Warning");
                            this.ActiveControl = txtpname;
                        }
                    }
                    return true;
                }
                else
                {
                    MessageBox.Show("Enter Product Name First.", "Warning");
                    this.ActiveControl = txtpname;
                }
            }
            else if (keyData == Keys.F10)
            {
                frmProductInformation pronew = new frmProductInformation();
                pronew.ShowDialog();
                getProductList();
                this.ActiveControl = txtpname;
                return true;
            }
            if (keyData == Keys.F3)
            {
                if (btnSave.Enabled == true)
                    SavePurchaseBill();
                else if (btnSave.Enabled == false)
                    UpdatePurchaseBill();
                return true;
            }
            if (keyData == Keys.Tab)
            {
                if (VisibleBatchPanel == "B" || VisibleBatchPanel == "BE")
                {
                    pnlIntegratedDtls.Visible = false;
                    VisibleBatchPanel = "";
                    this.ActiveControl = txtQuantity;
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.Text != "" || cmbUnit.Text != "Select")
                {
                    if (txtpname.Text != "")
                    {
                        string proname = "", company = "", unit = "";
                        if ((txtpname.Text).Contains(","))
                        {
                            try
                            {
                                proname = txtpname.Text.Trim().Split(',')[0];
                                company = txtpname.Text.Trim().Split(',')[1];
                                unit = txtpname.Text.Trim().Split(',')[2];
                            }
                            catch (Exception)
                            { }
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (productdata == null)
                            {
                                MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                                txtpname.Text = "";
                                this.ActiveControl = txtpname;
                            }
                            else
                            {
                                if (cmbUnit.Text == productdata.Uniti)
                                    txtpurchaseprice.Text = Convert.ToString(productdata.PurchaseRate);
                                else if (cmbUnit.Text == productdata.AlternateUnit)
                                    txtpurchaseprice.Text = Convert.ToString(productdata.AlternateUnitPurchaseRate);
                                ProductCategory = productdata.Category;
                            }
                            productrepo.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void getGstCalculation()
        {
            try
            {
                decimal Discamt = 0, SGstAmt = 0, CGstAmt = 0, IGstAmt = 0, SpcDiscAmt = 0, cessAmt = 0, disc3LevelAmt = 0, tcsamt = 0;

                if (txtQuantity.Text != "" && txtpurchaseprice.Text != "")
                {
                    glbTaxableTotal = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtpurchaseprice.Text);
                    txtamount.Text = Convert.ToString(Math.Round(glbTaxableTotal, 2, MidpointRounding.AwayFromZero));
                    if (txtpdiscount.Text != "")
                        Discamt = (glbTaxableTotal * Convert.ToDecimal(txtpdiscount.Text)) / 100;
                    txtpdisAmt.Text = Convert.ToString(Math.Round(Discamt, 2, MidpointRounding.AwayFromZero));
                    if (txtpSpcDisc.Text != "")
                        SpcDiscAmt = ((glbTaxableTotal - Discamt) * Convert.ToDecimal(txtpSpcDisc.Text)) / 100;
                    lblpSpcDis.Text = Convert.ToString(Math.Round(SpcDiscAmt, 5, MidpointRounding.AwayFromZero));
                    if (txt3LevelDisvPer.Text != "")
                        disc3LevelAmt = ((glbTaxableTotal - Discamt - SpcDiscAmt) * Convert.ToDecimal(txt3LevelDisvPer.Text)) / 100;
                    txt3LevelDiscAmt.Text = Convert.ToString(Math.Round(disc3LevelAmt, 2, MidpointRounding.AwayFromZero));
                    glbTaxableTotal = glbTaxableTotal - Discamt - SpcDiscAmt - disc3LevelAmt;
                    if (cmbsgst.Text != "")
                    {
                        SGstAmt = glbTaxableTotal * Convert.ToDecimal(cmbsgst.Text) / 100;
                        lblpvat.Text = Convert.ToString(Math.Round(SGstAmt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbcgst.Text != "")
                    {
                        CGstAmt = glbTaxableTotal * Convert.ToDecimal(cmbcgst.Text) / 100;
                        lblCGst.Text = Convert.ToString(Math.Round(CGstAmt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbigst.Text != "")
                    {
                        IGstAmt = glbTaxableTotal * Convert.ToDecimal(cmbigst.Text) / 100;
                        lblIGst.Text = Convert.ToString(Math.Round(IGstAmt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (txtpCess.Text != "")
                    {
                        cessAmt = glbTaxableTotal * Convert.ToDecimal(txtpCess.Text) / 100;
                        lblpCessAmt.Text = Convert.ToString(Math.Round(cessAmt, 2, MidpointRounding.AwayFromZero));
                    }
                    glbTaxableTotal = glbTaxableTotal + SGstAmt + CGstAmt + IGstAmt + cessAmt;
                    if (flgTCSchange == true)
                    {
                        decimal discount = 0;
                        discount = Convert.ToDecimal(txtTCSAmt.Text) / Convert.ToDecimal(glbTaxableTotal) * 100;
                        tcsamt = Convert.ToDecimal(txtTCSAmt.Text);
                        txtTCSPer.Text = Convert.ToString(Math.Round(discount, 5, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        if (txtTCSPer.Text != "")
                        {
                            tcsamt = (glbTaxableTotal * Convert.ToDecimal(txtTCSPer.Text) / 100);
                            txtTCSAmt.Text = Convert.ToString(Math.Round(tcsamt, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    glbTaxableTotal = glbTaxableTotal + tcsamt;
                    txtpTotalAmt.Text = Convert.ToString(Math.Round(glbTaxableTotal, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        private void fmPurchaseOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            flgmandatory = false;
            ismandatory = false;
        }

        private void txtpdisAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpdisAmt.Text != "" && txtamount.Text != "")
                {
                    if (Convert.ToDecimal(txtpdisAmt.Text) <= Convert.ToDecimal(txtamount.Text))
                    {
                        try
                        {
                            decimal damt = 0;
                            if (txtpdisAmt.Text != "")//&& Convert.ToDecimal(txtpdisAmt.Text) != 0)
                            {
                                damt = Convert.ToDecimal(txtpdisAmt.Text);
                                decimal disc = damt * 100 / Convert.ToDecimal(txtamount.Text);
                                txtpdiscount.Text = Convert.ToString(Math.Round(disc, 5, MidpointRounding.AwayFromZero));
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        txtpdisAmt.Text = "0";
                        txtpdiscount.Text = "0";
                        this.ActiveControl = txtpdisAmt;
                    }
                }
                else
                {
                    txtpdisAmt.Text = "0";
                    txtpdiscount.Text = "0";
                    this.ActiveControl = txtpdisAmt;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpSpcDisc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpSpcDisc.Text != "")
                {
                    if (Convert.ToDecimal(txtpSpcDisc.Text) <= 100)
                        getGstCalculation();
                    else
                    {
                        MessageBox.Show("Enter discount Between 0 to 100 %.", "Warning");
                        txtpSpcDisc.Text = "0";
                        this.ActiveControl = txtpSpcDisc;
                    }
                }
                else
                    this.ActiveControl = txtpSpcDisc;
            }
            catch (Exception)
            {
                txtpSpcDisc.Text = "0";
            }
        }

        private void txtpCess_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpCess.Text != "")
                    getGstCalculation();
                else
                    this.ActiveControl = txtpCess;
            }
            catch (Exception)
            {
                txtpCess.Text = "0";
            }
        }

        private void txtHSN_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "")
                {
                    ProductRepository prepo1 = new ProductRepository();
                    string proname = "", company = "", unit = "";
                    if ((txtpname.Text).Contains(","))
                    {
                        proname = txtpname.Text.Trim().Split(',')[0];
                        company = txtpname.Text.Trim().Split(',')[1];
                        unit = txtpname.Text.Trim().Split(',')[2];
                        ProductRepository prepo = new ProductRepository();
                        ProductInformation pdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            pdata = prepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.HSNCode == "").FirstOrDefault();
                        else
                            pdata = prepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.HSNCode == "" && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (pdata != null)
                        {
                            pdata.HSNCode = txtHSN.Text;
                            prepo1.Edit(pdata);
                            prepo1.Save();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    getGstCalculation();
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                }
                else
                {
                    lblpvat.Text = "0";
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                    getGstCalculation();
                    cmbigst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    getGstCalculation();
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                }
                else
                {
                    lblCGst.Text = "0";
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                    getGstCalculation();
                    cmbigst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbigst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbigst.SelectedIndex > 0)
                {
                    cmbsgst.SelectedIndex = 0;
                    cmbcgst.SelectedIndex = 0;
                    cmbsgst.Enabled = false;
                    cmbcgst.Enabled = false;
                    getGstCalculation();
                }
                else
                {
                    lblIGst.Text = "0";
                    getGstCalculation();
                    cmbsgst.Enabled = true;
                    cmbcgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void getProductsbyPrefix(string productprefix)
        {
            try
            {
                productprefix = productprefix.Trim();
                txtpname.Text = "";
                if (productprefix != "")
                {
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        List<string> lstproduct = new List<string>();
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where Prefix like '" + productprefix + "%'", con);
                        else
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where Prefix like '" + productprefix + "%' and compid=" + CommonMethod.CompId, con);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string strproduct = "";
                            strproduct = reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2);
                            lstproduct.Add(strproduct);
                        }
                        if (lstproduct.Count > 0)
                        {
                            lstprefixproduct.DataSource = lstproduct;  // assigned list to listbox
                            lstprefixproduct.Visible = true;
                        }
                    }
                }
                else
                    lstprefixproduct.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                prefixSelected = false;
                if (cmbcomanyname.Text != "" && cmbcomanyname.SelectedIndex != 0)
                {
                    getProductsbyPrefix(cmbcomanyname.Text);
                    this.ActiveControl = lstprefixproduct;
                }
                else
                    this.ActiveControl = txtpname;
            }
            if (Control.ModifierKeys == Keys.Alt)
            {
                if (e.KeyCode == Keys.N)
                {
                    frmProductInformation product = new frmProductInformation();
                    product.ShowDialog();
                    fillmfgcomp();
                    this.ActiveControl = cmbcomanyname;
                }
            }
        }

        private void lstprefixproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lstprefixproduct.Text != "")
            {
                prefixSelected = true;
                txtpname.Text = lstprefixproduct.Text;
                lstprefixproduct.Visible = false;
                this.ActiveControl = txtpname;
            }
        }

        private void cmbsuppliername_Leave(object sender, EventArgs e)
        {
            if (cmbsuppliername.Text != "" && cmbsuppliername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                int status = cm.ValidSupplier(cmbsuppliername.Text);
                if (status == 1)
                { }
                else
                {
                    if ((MessageBox.Show("This Supplier Name Not Saved. U want Save It?", "Preview", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        SupplierRepository suppRepo = new SupplierRepository();
                        SupplierInformation supplierData = new SupplierInformation();
                        string MAXId = "";
                        try
                        {
                            SupplierRepository suppRepo1 = new SupplierRepository();
                            var mid = suppRepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.SupplierCode);
                            if (mid == null)
                                MAXId = "1";
                            else
                                MAXId = Convert.ToString(Convert.ToInt32(mid) + 1);
                            suppRepo1.Dispose();
                            supplierData.SupplierCode = Convert.ToInt32(MAXId);
                            supplierData.SupplierName = cmbsuppliername.Text.Trim();
                            string name = cmbsuppliername.Text.Trim();
                            supplierData.CompId = CommonMethod.CompId;
                            CompanyInformationRepository cRepo = new CompanyInformationRepository();
                            var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                            if (cinfo != null)
                            {
                                StateMaster state = new StateMaster();
                                StateMasterRepository staterepo = new StateMasterRepository();
                                state = staterepo.GetAll().Where(t => t.StateId == cinfo.StateCode).FirstOrDefault();
                                supplierData.StateId = state.Id;
                            }
                            suppRepo.Add(supplierData);
                            suppRepo.Save();
                            MessageBox.Show("Supplier Name Save Successfully.", "Success");
                            fillsupplier();
                            cmbsuppliername.Text = name;
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        fillsupplier();
                        this.ActiveControl = cmbsuppliername;
                    }
                }
                this.txtpname_Leave(sender, e);
                //txtpname.Leave += new EventHandler(txtpname_Leave);
            }
        }

        private void getProductData(string proname, string company, string unit)
        {
            product_prefix = "";
            ProductRepository prodrepo = new ProductRepository();
            ProductInformation proddata = new ProductInformation();
            if (CommonMethod.commProduct == true)
                proddata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
            else
                proddata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

            if (proddata != null)
            {
                product_prefix = proddata.Prefix;
                cmbcomanyname.Text = product_prefix;
            }
            prodrepo.Dispose();
            proddata = null;
        }

        private void txtsupplierpono_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpPorderdate;
            }
            catch (Exception)
            { }
        }

        private void dtpPorderdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsuppliername;
            }
            catch (Exception)
            { }
        }

        public string GenerateandCheckNewBarcode()
        {
            string stringBarcode = "";
            try
            {
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                Random random = new Random();
                var byteArray = new byte[4];
                provider.GetBytes(byteArray);
                var randomInteger = BitConverter.ToUInt32(byteArray, 0);
                stringBarcode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(01, 99));
            a:
                {
                    if (stringBarcode.Length != 12)
                    {
                        if (stringBarcode.Length > 12)
                            stringBarcode = stringBarcode.Substring(0, 12);
                        else
                            stringBarcode = stringBarcode + Convert.ToString(random.Next(01, 99));
                    }
                }
                if (stringBarcode.Length != 12)
                    goto a;
                return stringBarcode;
            }
            catch (Exception)
            { return stringBarcode; }
        }

        private string generateBarcode()
        {
            string str_barCode = "";
            try
            {
                if (str_barCode == "" && barcodestatus == "Our Barcode")
                {
                    str_barCode = GenerateandCheckNewBarcode();
                a:
                    {
                        if (GvProductInfo.RowCount > 0)
                        {
                            for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                            {
                                if (GvProductInfo.Rows[i].Cells["Barcode"].Value.ToString() == str_barCode.ToString())
                                {
                                    str_barCode = string.Empty;
                                    str_barCode = GenerateandCheckNewBarcode();
                                }
                            }
                        }
                    }
                    PurchaseDetailRepository pDetailRepo = new PurchaseDetailRepository();
                    var data = pDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();

                    RepackDetailRepository RDetailRepo = new RepackDetailRepository();
                    var Rdata = RDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
                    pDetailRepo.Dispose();
                    RDetailRepo.Dispose();
                    if (data != null || Rdata != null)
                    {
                        goto a;
                    }
                }
                return str_barCode;
            }
            catch (Exception)
            { return str_barCode; }
        }

        //private string generateBarcode()
        //{
        //    string str_barCode = "";
        //    try
        //    {
        //        if (str_barCode == "" && barcodestatus == "Our Barcode")
        //        {
        //            string small_alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //            string numbers = "1234567890";
        //            string characters = numbers + small_alphabets;
        //            string otp = string.Empty;
        //            string otp1 = string.Empty;
        //            Random random = new Random();

        //            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        //            var byteArray = new byte[4];
        //            provider.GetBytes(byteArray);
        //            //convert 4 bytes to an integer
        //            var randomInteger = BitConverter.ToUInt32(byteArray, 0);
        //            if (randomInteger.ToString().Length == 10)
        //                str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(01, 99));
        //            else
        //            {
        //                str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(01, 99)) + Convert.ToString(random.Next(60, 86));
        //                if (str_barCode.Length != 12)
        //                {
        //                    str_barCode = str_barCode.Substring(0, 12);
        //                }
        //            }
        //            str_barCode = checkBarcodeLength(str_barCode);
        //            //MessageBox.Show("1st Attempt" + str_barCode, "Success");
        //            if (GvProductInfo.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < GvProductInfo.Rows.Count; i++)
        //                {
        //                    if (GvProductInfo.Rows[i].Cells["Barcode"].Value.ToString() == str_barCode.ToString())
        //                    {
        //                        byteArray = new byte[4];
        //                        provider.GetBytes(byteArray);
        //                        //convert 4 bytes to an integer
        //                        randomInteger = BitConverter.ToUInt32(byteArray, 0);
        //                        if (randomInteger.ToString().Length == 10)
        //                            str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(12, 89));
        //                        else
        //                        {
        //                            str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(12, 89)) + Convert.ToString(random.Next(40, 76));
        //                            if (str_barCode.Length > 12)
        //                            {
        //                                str_barCode = str_barCode.Substring(0, 12);
        //                            }
        //                        }
        //                        str_barCode = checkBarcodeLength(str_barCode);                       
        //                        //MessageBox.Show("2nd Attempt" + str_barCode, "Success");
        //                        break;
        //                    }
        //                }
        //            }
        //            PurchaseDetailRepository pDetailRepo = new PurchaseDetailRepository();
        //            var data = pDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();

        //            RepackDetailRepository RDetailRepo = new RepackDetailRepository();
        //            var Rdata = RDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();

        //            pDetailRepo.Dispose();
        //            RDetailRepo.Dispose();
        //            if (data != null || Rdata != null)
        //            {                       
        //                str_barCode = string.Empty;
        //                MessageBox.Show("This Barcode no are duplicate.", "Warning");
        //                byteArray = new byte[4];
        //                provider.GetBytes(byteArray);
        //                //convert 4 bytes to an integer
        //                randomInteger = BitConverter.ToUInt32(byteArray, 0);
        //                if (randomInteger.ToString().Length == 10)
        //                    str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(21, 76));
        //                else
        //                {
        //                    str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(21, 76)) + Convert.ToString(random.Next(50, 96));
        //                    if (str_barCode.Length > 12)
        //                    {
        //                        str_barCode = str_barCode.Substring(0, 12);
        //                    }
        //                }
        //                str_barCode = checkBarcodeLength(str_barCode);
        //                //MessageBox.Show("3rd Attempt" + str_barCode, "Success");
        //                if (GvProductInfo.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < GvProductInfo.Rows.Count; i++)
        //                    {
        //                        if (GvProductInfo.Rows[i].Cells["Barcode"].Value.ToString() == str_barCode.ToString())
        //                        {
        //                            byteArray = new byte[4];
        //                            provider.GetBytes(byteArray);
        //                            //convert 4 bytes to an integer
        //                            randomInteger = BitConverter.ToUInt32(byteArray, 0);
        //                            if (randomInteger.ToString().Length == 10)
        //                                str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(30, 79));
        //                            else
        //                            {
        //                                str_barCode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(30, 79)) + Convert.ToString(random.Next(70, 98));
        //                                if (str_barCode.Length > 12)
        //                                {
        //                                    str_barCode = str_barCode.Substring(0, 12);
        //                                }
        //                            }
        //                            str_barCode = checkBarcodeLength(str_barCode);
        //                            //MessageBox.Show("4th Attempt" + str_barCode, "Success");
        //                            break;
        //                        }
        //                    }
        //                }
        //                PurchaseDetailRepository pDetailReponew = new PurchaseDetailRepository();
        //                var purdata = pDetailReponew.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
        //                RepackDetailRepository RDetailReponew = new RepackDetailRepository();
        //                var Redata = RDetailReponew.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
        //                RDetailReponew.Dispose();
        //                pDetailReponew.Dispose();
        //                if (data != null || Redata != null)
        //                    str_barCode = "";
        //            }
        //        }
        //        return str_barCode;
        //    }
        //    catch (Exception)
        //    {
        //        return str_barCode;
        //    }
        //}

        private void txtpurchaseprice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtSaleRate.Enabled == true)
                        this.ActiveControl = txtSaleRate;
                    else
                    {
                        if (txtstaticBarcode.Enabled == true)
                            this.ActiveControl = txtstaticBarcode;
                        else
                            this.ActiveControl = btnAdd;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                SettingRepository settRepo = new SettingRepository();
                Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata.ShowBarcodePrintOn == "P")
                {
                    PrintBarcodeThroughPRN();
                }
                else
                {
                    string input = "", inputcopy = "";
                    int rownum = 0, copyno = 1, colno = 0;
                    input = Microsoft.VisualBasic.Interaction.InputBox("Enter row number from where you want to print " + Environment.NewLine + " from 1 to 12 ", "Page Setup", "" + rownum + "", 300, 250);
                    rownum = Convert.ToInt32(input);

                    input = Microsoft.VisualBasic.Interaction.InputBox("Enter column number from where you want to print " + Environment.NewLine + " from 1 to 4 ", "Page Setup", "" + colno + "", 300, 250);
                    colno = Convert.ToInt32(input);

                    inputcopy = Microsoft.VisualBasic.Interaction.InputBox("Enter number of copies you want to print", "Copies", "" + copyno + "", 300, 250);
                    copyno = Convert.ToInt32(inputcopy);

                    int lblavail = 0, totqty = 0, qtytoprint = 0;
                    if (colno > 1)
                    {
                        lblavail = 4 - (colno - 1);
                        totqty = Convert.ToInt32(totalqty);
                        qtytoprint = (totqty * copyno);
                    }
                    if (qtytoprint > lblavail)
                    {
                        MessageBox.Show("You have selected more labels to print than available labels,\n Please select another order.", "Printing Barcode");
                        this.ActiveControl = btnPrintBarcode;
                    }
                    else
                    {
                        decimal ponumber = 0;
                        if (purmasterid != 0)
                            ponumber = Convert.ToDecimal(purmasterid);
                        else if (PurchaeId != 0)
                            ponumber = Convert.ToDecimal(PurchaeId);
                        foreach (DataGridViewRow row in GvProductInfo.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["SelecttoPrint"];
                            int id = Convert.ToInt32(row.Cells["Id"].Value);
                            if (chk.Value != null)
                            {
                                if ((bool)chk.Value == true)
                                {
                                    if (DIdListBarcodePrint.Contains(id))
                                    { }
                                    else
                                        DIdListBarcodePrint.Add(id);
                                }
                            }
                        }
                        string flag = "Purchase";
                        if (DIdListBarcodePrint.Count != 0)
                        {
                            RptBarcode barrpt = new RptBarcode(DIdListBarcodePrint, ponumber, rownum, copyno, colno, flag);
                            barrpt.ShowDialog();
                        }
                        else
                        {
                            RptBarcode barrpt = new RptBarcode(ponumber, rownum, copyno, colno, flag);
                            barrpt.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
        public void PrintBarcodeThroughPRN()
        {

            try
            {
                string input = "", inputcopy = "";
                int rownum = 0, copyno = 1, colno = 0;
                input = Microsoft.VisualBasic.Interaction.InputBox("5-4(2 UP) Print Press 1,4-4(1 UP),5-2.5(2 UP)  " + Environment.NewLine + "", "", "" + rownum + "", 300, 250);
                rownum = Convert.ToInt32(input);

                if (rownum == 1)
                {
                    List<PurchaseDetail> lstbcp = null;
                    if (PurchaeId != 0)
                    {
                        PurchaseDetailRepository pDRepo = new PurchaseDetailRepository();
                        lstbcp = pDRepo.GetAll().Where(t => t.PorderNo == PurchaeId && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                    }
                    else
                    {
                        PurchaseDetailRepository pDRepo = new PurchaseDetailRepository();
                        lstbcp = pDRepo.GetAll().Where(t => t.PorderNo == purmasterid && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                    }

                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                    string companyName = cRepo.GetAll().Where(t => t.Id == CommonMethod.CompId).FirstOrDefault().CompanyName;
                    cRepo.Dispose();

                    for (int i = 0; i < lstbcp.Count(); i++)
                    {
                        lstbcp[i].ProductName.ToString();
                        // int id = Convert.ToInt32(lstbcp[i].Id.ToString());
                        decimal qty = Convert.ToDecimal(lstbcp[i].Quantity.ToString());
                        //List<PurchaseDetail> dList = pDRepo.GetAll().Where(t => t.Id == id).ToList();

                        decimal q = 0;
                        int roundValue = 0;
                        if (qty % 2 == 0)
                        {
                            q = Convert.ToDecimal(qty / 2);
                            roundValue = Convert.ToInt32(q);
                        }
                        else
                        {

                            q = Convert.ToDecimal(qty / 2);
                            q = q + Convert.ToDecimal(0.50);
                            roundValue = Convert.ToInt32(Math.Round(q));

                        }
                        for (int p = 0; p < roundValue; p++)
                        {

                            string pName = lstbcp[i].ProductName.ToString();
                            String path = @"E:\FInalSunny.PRN";
                            String path2 = @"E:\FInalSunnytxt.txt";

                            string batchfile = @"E:\sunny.bat";
                            if (!File.Exists(batchfile))
                            {
                                File.Create(batchfile).Dispose();
                            }
                            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                string a = Application.StartupPath + @"\FInalSunny.prn";
                                //string text = File.ReadAllLines(path2);
                                string[] dataSource = System.IO.File.ReadAllLines(path2);
                                string c = dataSource[0].ToString();
                                using (TextWriter tw = new StreamWriter(fs))
                                {

                                    tw.WriteLine("<xpml><page quantity='0' pitch='38.2 mm'></xpml>SIZE 105.5 mm, 38.2 mm");
                                    tw.WriteLine("DIRECTION 0,0");
                                    tw.WriteLine("REFERENCE 0,0");
                                    tw.WriteLine("OFFSET 0 mm");
                                    tw.WriteLine("SET PEEL OFF");
                                    tw.WriteLine("SET CUTTER OFF");
                                    tw.WriteLine("SET PARTIAL_CUTTER OFF");
                                    tw.WriteLine("OFFSET 0 mm");
                                    tw.WriteLine("SET PEEL OFF");
                                    tw.WriteLine("SET CUTTER OFF");
                                    tw.WriteLine("SET PARTIAL_CUTTER OFF");
                                    tw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.2 mm'></xpml>SET TEAR ON");
                                    tw.WriteLine("CLS");
                                    tw.WriteLine("BARCODE 796,264,\"128M\",38,0,180,3,6,\"!105" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("CODEPAGE 1252");

                                    tw.WriteLine("TEXT 746,221,\"0\",180,12,12,\"" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("TEXT 796,178,\"0\",180,16,9,\"" + lstbcp[i].ProductName.ToString() + "\"");
                                    tw.WriteLine("TEXT 804,141,\"0\",180,17,8,\"" + lstbcp[i].CompanyName.ToString() + "\"");
                                    tw.WriteLine("TEXT 796,92,\"0\",180,9,9,\"" + lstbcp[i].Size.ToString() + "\"");
                                    tw.WriteLine("TEXT 604,88,\"0\",180,5,6,\"MRP-\"");
                                    tw.WriteLine("TEXT 558,101,\"0\",180,11,12,\"" + lstbcp[i].SaleRate.ToString() + "/-\"");
                                    tw.WriteLine("TEXT 758,49,\"0\",180,12,12,\"" + companyName + "\"");
                                    tw.WriteLine("BARCODE 365,264,\"128M\",38,0,180,3,6,\"!105" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("TEXT 315,221,\"0\",180,12,12,\"" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("TEXT 365,178,\"0\",180,16,9,\"" + lstbcp[i].ProductName.ToString() + "\"");
                                    tw.WriteLine("TEXT 373,141,\"0\",180,17,8,\"" + lstbcp[i].CompanyName.ToString() + "\"");
                                    tw.WriteLine("TEXT 365,92,\"0\",180,9,9,\"" + lstbcp[i].Size.ToString() + "\"");
                                    tw.WriteLine("TEXT 173,88,\"0\",180,5,6,\"MRP-\"");

                                    tw.WriteLine("TEXT 127,101,\"0\",180,11,12,\"" + lstbcp[i].SaleRate.ToString() + "/-\"");
                                    tw.WriteLine("TEXT 327,49,\"0\",180,12,12,\"" + companyName + "\"");

                                    tw.WriteLine("PRINT 1,1");
                                    tw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");


                                }

                            }
                            Process proc = null;
                            String user = Environment.MachineName;

                            if (File.Exists(batchfile))
                            {
                                FileInfo fi = new FileInfo(batchfile);
                                File.Create(batchfile).Dispose();
                                using (StreamWriter sw = new StreamWriter(fi.Open(FileMode.Truncate)))
                                {
                                    sw.WriteLine("COPY E:\\FInalSunny.PRN \\\\" + user + "\\TSC");
                                    sw.Close();
                                }
                            }
                            string batDir = string.Format(@"E:\");
                            proc = new Process();
                            proc.StartInfo.WorkingDirectory = batDir;

                            proc.StartInfo.FileName = "sunny.bat";
                            proc.StartInfo.CreateNoWindow = false;
                            proc.StartInfo.Arguments = "E:\\FInalSunny.PRN \\\\" + user + "\\TSC";
                            proc.Start();
                            proc.WaitForExit();

                        }
                    }
                }
                else if (rownum == 2)
                {
                    List<PurchaseDetail> lstbcp = null;
                    if (PurchaeId != 0)
                    {
                        PurchaseDetailRepository pDRepo = new PurchaseDetailRepository();
                        lstbcp = pDRepo.GetAll().Where(t => t.PorderNo == PurchaeId && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                    }
                    else
                    {
                        PurchaseDetailRepository pDRepo = new PurchaseDetailRepository();
                        lstbcp = pDRepo.GetAll().Where(t => t.PorderNo == purmasterid && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                    }

                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                    string companyName = cRepo.GetAll().Where(t => t.Id == CommonMethod.CompId).FirstOrDefault().CompanyName;
                    cRepo.Dispose();

                    for (int i = 0; i < lstbcp.Count(); i++)
                    {
                        lstbcp[i].ProductName.ToString();
                        // int id = Convert.ToInt32(lstbcp[i].Id.ToString());
                        decimal qty = Convert.ToDecimal(lstbcp[i].Quantity.ToString());
                        //List<PurchaseDetail> dList = pDRepo.GetAll().Where(t => t.Id == id).ToList();

                        //decimal q = 0;
                        //int roundValue = 0;
                        //if (qty % 2 == 0)
                        //{
                        //    q = Convert.ToDecimal(qty / 2);
                        //    roundValue = Convert.ToInt32(q);
                        //}
                        //else
                        //{

                        //    q = Convert.ToDecimal(qty / 2);
                        //    q = q + Convert.ToDecimal(0.50);
                        //    roundValue = Convert.ToInt32(Math.Round(q));

                        //}
                        for (int p = 0; p < qty; p++)
                        {

                            string pName = lstbcp[i].ProductName.ToString();
                            String path = @"D:\FinalSunnyPRN4-4.PRN";
                            String path2 = @"D:\FinalSunnyPRN4-4.txt";

                            string batchfile = @"D:\sunny4-4.bat";
                            if (!File.Exists(batchfile))
                            {
                                File.Create(batchfile).Dispose();
                            }
                            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                string a = Application.StartupPath + @"\FinalSunnyPRN4-4.prn";
                                //string text = File.ReadAllLines(path2);
                                string[] dataSource = System.IO.File.ReadAllLines(path2);
                                string c = dataSource[0].ToString();
                                using (TextWriter tw = new StreamWriter(fs))
                                {

                                    tw.WriteLine("<xpml><page quantity='0' pitch='38.2 mm'></xpml>SIZE 38.8 mm, 38.2 mm");
                                    tw.WriteLine("DIRECTION 0,0");
                                    tw.WriteLine("REFERENCE 0,0");
                                    tw.WriteLine("OFFSET 0 mm");
                                    tw.WriteLine("SET PEEL OFF");
                                    tw.WriteLine("SET CUTTER OFF");
                                    tw.WriteLine("SET PARTIAL_CUTTER OFF");
                                    tw.WriteLine("OFFSET 0 mm");
                                    tw.WriteLine("SET PEEL OFF");
                                    tw.WriteLine("SET CUTTER OFF");
                                    tw.WriteLine("SET PARTIAL_CUTTER OFF");
                                    tw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='38.2 mm'></xpml>SET TEAR ON");
                                    tw.WriteLine("CLS");
                                    tw.WriteLine("BARCODE 260,264,\"128M\",48,0,180,2,4,\"!105" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("CODEPAGE 1252");

                                    tw.WriteLine("TEXT 243,210,\"0\",180,10,10,\"" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("TEXT 286,178,\"0\",180,12,8,\"" + lstbcp[i].ProductName.ToString() + "\"");
                                    tw.WriteLine("TEXT 276,141,\"0\",180,17,8,\"" + lstbcp[i].CompanyName.ToString() + "\"");
                                    
                                    tw.WriteLine("TEXT 71,99,\"0\",180,8,8,\"" + lstbcp[i].SaleRate.ToString() + "/-\"");
                                    tw.WriteLine("TEXT 121,100,\"0\",180,7,8,\"MRP-\"");
                                    tw.WriteLine("TEXT 263,100,\"0\",180,7,8,\"" + lstbcp[i].Size.ToString() + "\"");
                                    tw.WriteLine("TEXT 236,59,\"0\",180,8,8,\"" + companyName + "\"");
                               

                                    tw.WriteLine("PRINT 1,1");
                                    tw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");


                                }

                            }
                            Process proc = null;
                            String user = Environment.MachineName;

                            if (File.Exists(batchfile))
                            {
                                FileInfo fi = new FileInfo(batchfile);
                                File.Create(batchfile).Dispose();
                                using (StreamWriter sw = new StreamWriter(fi.Open(FileMode.Truncate)))
                                {
                                    sw.WriteLine("COPY D:\\FinalSunnyPRN4-4.PRN \\\\" + user + "\\TSC");
                                    sw.Close();
                                }
                            }
                            string batDir = string.Format(@"D:\");
                            proc = new Process();
                            proc.StartInfo.WorkingDirectory = batDir;

                            proc.StartInfo.FileName = "sunny4-4.bat";
                            proc.StartInfo.CreateNoWindow = false;
                            proc.StartInfo.Arguments = "D:\\FinalSunnyPRN4-4.PRN \\\\" + user + "\\TSC";
                            proc.Start();
                            proc.WaitForExit();


                       }
                    }
                }
                else if (rownum == 3)
                {
                    List<PurchaseDetail> lstbcp = null;
                    if (PurchaeId != 0)
                    {
                        PurchaseDetailRepository pDRepo = new PurchaseDetailRepository();
                        lstbcp = pDRepo.GetAll().Where(t => t.PorderNo == PurchaeId && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                    }
                    else
                    {
                        PurchaseDetailRepository pDRepo = new PurchaseDetailRepository();
                        lstbcp = pDRepo.GetAll().Where(t => t.PorderNo == purmasterid && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                    }

                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                    string companyName = cRepo.GetAll().Where(t => t.Id == CommonMethod.CompId).FirstOrDefault().CompanyName;
                    cRepo.Dispose();

                    for (int i = 0; i < lstbcp.Count(); i++)
                    {
                        lstbcp[i].ProductName.ToString();
                        // int id = Convert.ToInt32(lstbcp[i].Id.ToString());
                        decimal qty = Convert.ToDecimal(lstbcp[i].Quantity.ToString());
                        //List<PurchaseDetail> dList = pDRepo.GetAll().Where(t => t.Id == id).ToList();

                        decimal q = 0;
                        int roundValue = 0;
                        if (qty % 2 == 0)
                        {
                            q = Convert.ToDecimal(qty / 2);
                            roundValue = Convert.ToInt32(q);
                        }
                        else
                        {

                            q = Convert.ToDecimal(qty / 2);
                            q = q + Convert.ToDecimal(0.50);
                            roundValue = Convert.ToInt32(Math.Round(q));

                        }
                        for (int p = 0; p < roundValue; p++)
                        {

                            string pName = lstbcp[i].ProductName.ToString();
                            String path = @"E:\Barcode2x1inch.PRN";
                            String path2 = @"E:\Barcode2x1inch.txt";

                            string batchfile = @"E:\Barcode2x1inch.bat";
                            if (!File.Exists(batchfile))
                            {
                                File.Create(batchfile).Dispose();
                            }
                            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                            {
                                string a = Application.StartupPath + @"\Barcode2x1inch.prn";
                                //string text = File.ReadAllLines(path2);
                                string[] dataSource = System.IO.File.ReadAllLines(path2);
                                string c = dataSource[0].ToString();
                                using (TextWriter tw = new StreamWriter(fs))
                                {

                                    tw.WriteLine("<xpml><page quantity='0' pitch='25.4 mm'></xpml>SIZE 105.5 mm, 25.4 mm");
                                    tw.WriteLine("DIRECTION 0,0");
                                    tw.WriteLine("REFERENCE 0,0");
                                    tw.WriteLine("OFFSET 0 mm");
                                    tw.WriteLine("SET PEEL OFF");
                                    tw.WriteLine("SET CUTTER OFF");
                                    tw.WriteLine("SET PARTIAL_CUTTER OFF");
                                    tw.WriteLine("OFFSET 0 mm");
                                    tw.WriteLine("SET PEEL OFF");
                                    tw.WriteLine("SET CUTTER OFF");
                                    tw.WriteLine("SET PARTIAL_CUTTER OFF");
                                    tw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='25.4 mm'></xpml>SET TEAR ON");
                                    tw.WriteLine("CLS");
                                    tw.WriteLine("BARCODE 745,179,\"128M\",44,0,180,2,4,\"!105" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("CODEPAGE 1252");

                                    tw.WriteLine("TEXT 728,130,\"0\",180,9,9,\"" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("TEXT 768,99,\"0\",180,12,8,\"" + lstbcp[i].ProductName.ToString() + "\"");
                                    tw.WriteLine("TEXT 753,73,\"0\",180,17,8,\"" + lstbcp[i].CompanyName.ToString() + "\"");

                                    tw.WriteLine("TEXT 489,34,\"0\",180,8,8,\"" + lstbcp[i].SaleRate.ToString() + "/-\"");
                                    tw.WriteLine("TEXT 539,36,\"0\",180,17,8,\"MRP-\"");
                                    tw.WriteLine("TEXT 489,118,\"0\",180,7,8,\"" + lstbcp[i].Size.ToString() + "\"");
                                    tw.WriteLine("TEXT 777,36,\"0\",180,8,8,\"" + companyName + "\"");


                                    tw.WriteLine("BARCODE 314,179,\"128M\",44,0,180,2,4,\"!105" + lstbcp[i].Barcode.ToString() + "\"");

                                    tw.WriteLine("TEXT 285,130,\"0\",180,9,9,\"" + lstbcp[i].Barcode.ToString() + "\"");
                                    tw.WriteLine("TEXT 337,99,\"0\",180,12,8,\"" + lstbcp[i].ProductName.ToString() + "\"");
                                    tw.WriteLine("TEXT 322,73,\"0\",180,17,8,\"" + lstbcp[i].CompanyName.ToString() + "\"");

                                    tw.WriteLine("TEXT 58,34,\"0\",180,8,8,\"" + lstbcp[i].SaleRate.ToString() + "/-\"");
                                    tw.WriteLine("TEXT 108,36,\"0\",180,7,8,\"MRP-\"");
                                    tw.WriteLine("TEXT 58,118,\"0\",180,7,8,\"" + lstbcp[i].Size.ToString() + "\"");
                                    tw.WriteLine("TEXT 346,36,\"0\",180,8,8,\"" + companyName + "\"");

                                    tw.WriteLine("PRINT 1,1");
                                    tw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");


                                }

                            }
                            Process proc = null;
                            String user = Environment.MachineName;

                            if (File.Exists(batchfile))
                            {
                                FileInfo fi = new FileInfo(batchfile);
                                File.Create(batchfile).Dispose();
                                using (StreamWriter sw = new StreamWriter(fi.Open(FileMode.Truncate)))
                                {
                                    sw.WriteLine("COPY E:\\Barcode2x1inch.PRN \\\\" + user + "\\TSC");
                                    sw.Close();
                                }
                            }
                            string batDir = string.Format(@"E:\");
                            proc = new Process();
                            proc.StartInfo.WorkingDirectory = batDir;

                            proc.StartInfo.FileName = "Barcode2x1inch.bat";
                            proc.StartInfo.CreateNoWindow = false;
                            proc.StartInfo.Arguments = "E:\\Barcode2x1inch.PRN \\\\" + user + "\\TSC";
                            proc.Start();
                            proc.WaitForExit();


                        }
                    }
                }



            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString());
            }
        }

        private void chkGstTransport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGstTransport.Checked == true)
            {
                if (Convert.ToDecimal(txttranscharges.Text) != 0)
                {
                    if (Convert.ToDecimal(ttnetamt.Text) != 0)
                    {
                        decimal gstontransport = 0, gstpercent_OnTransport = 0;
                        gstpercent_OnTransport = (((totalCGst + totalIGst + totalSGst) * 100) / Convert.ToDecimal(TaxableTotal));
                        gstontransport = (Convert.ToDecimal(txttranscharges.Text) * gstpercent_OnTransport / 100);
                        gstontransport = Math.Round(gstontransport, 2, MidpointRounding.AwayFromZero);
                        txtGstTransport.Text = Convert.ToString(gstontransport);
                        CalculateNetAmt();
                    }
                }
            }
            else
            {
                txtGstTransport.Text = "0";
                CalculateNetAmt(true);
            }
        }

        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpSpcDisc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = lblpSpcDis;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsgst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcgst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbigst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpCess_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpTotalAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void txtstaticBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSaleRate.Text == "")
                {
                    //txtSaleRate.Text = "0";
                    this.ActiveControl = txtSaleRate;
                }
            }
            catch (Exception)
            { }
        }

        private void chkGstPack_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGstPack.Checked == true)
            {
                txtGstPackpercent.Enabled = true;
                if (lblpackGST.Text == "0" || lblpackGST.Text == "")
                    txtGstPackpercent.Text = "0";
            }
            else
            {
                txtGstPackpercent.Enabled = false;
                txtGstPackpercent.Text = "0";
                lblpackGST.Text = "0";
                CalculateNetAmt(true);
            }
        }

        private void txtGstPackpercent_Leave(object sender, EventArgs e)
        {
            if (txtGstPackpercent.Text != "")
            {
                decimal pack_GST = 0;
                pack_GST = (Convert.ToDecimal(txtPackaging.Text) * Convert.ToDecimal(txtGstPackpercent.Text)) / 100;
                lblpackGST.Text = pack_GST.ToString();
                CalculateNetAmt();
            }
        }

        private void txtPackaging_Leave(object sender, EventArgs e)
        {
            if (txtPackaging.Text == "")
                txtPackaging.Text = "0";
            if (txtPackaging.Text != "")
                CalculateNetAmt();
        }

        private void txttranscharges_Leave(object sender, EventArgs e)
        {
            if (txttranscharges.Text != "")
                CalculateNetAmt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pnlIntegratedDtls.Visible = false;
                this.ActiveControl = txtQuantity;
            }
            catch (Exception)
            { }
        }

        private void txtpcomm_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtpcommRs.Text == "" || txtpcommRs.Text == "0" || Convert.ToDecimal(txtpcommRs.Text) == 0)
                    txtpcomm.ReadOnly = false;
                else
                    txtpcomm.ReadOnly = true;
            }
            catch (Exception)
            { }
        }

        private void txtpcomm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpcomm.Text != "")
                {
                    if (Convert.ToDecimal(txtpcomm.Text) > 100)
                    {
                        MessageBox.Show("Enter commision between 0 to 100 %.", "Warning");
                        txtpcomm.Text = "0";
                        this.ActiveControl = txtpcomm;
                    }
                }
                else
                {
                    txtpcomm.Text = "0";
                    this.ActiveControl = txtpcomm;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpcommRs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpcommRs.Text != "")
                {
                    if (Convert.ToDecimal(txtpcommRs.Text) > Convert.ToDecimal(txtamount.Text))
                    {
                        MessageBox.Show("Enter Commision Amount Less or Equal to Amount.", "Warning");
                        txtpcommRs.Text = "0";
                        this.ActiveControl = txtpcommRs;
                    }
                }
                else
                {
                    txtpcommRs.Text = "0";
                    this.ActiveControl = txtpcommRs;
                }
            }
            catch (Exception)
            { }
        }

        private void txtsizefrom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsizefrom.Text == "")
                {
                    txtsizefrom.Text = "0";
                    this.ActiveControl = txtsizefrom;
                }
            }
            catch (Exception)
            { }
        }

        private void txtsizegap_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsizegap.Text == "")
                {
                    txtsizegap.Text = "0";
                    this.ActiveControl = txtsizegap;
                }
            }
            catch (Exception)
            { }
        }

        private void txtBachNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (productentrytype == 2)
                {
                    pnlIntegratedDtls.Visible = false;
                    this.ActiveControl = txtQuantity;
                }
                else
                {
                    VisibleBatchPanel = "";
                    this.ActiveControl = dtpExpiry;
                }
            }
        }

        private void AdjustPanelDetails(int product_entrytype)
        {
            pnlIntegratedDtls.Visible = false;
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData != null)
            {
                if (SetData.UpdateMRP == true)
                {
                    pnlIntegratedDtls.Visible = true;
                    txtMRP.Enabled = true;
                    txtBatchNo.Enabled = false;
                    dtpExpiry.Enabled = false;
                    txtpCess.Text = "0";
                    txtpCess.Enabled = false;
                    txtpcommRs.Enabled = false;
                    txtpcomm.Enabled = false;
                    dtgvSize.Enabled = false;
                    txtsizefrom.Enabled = false;
                    txtsizegap.Enabled = false;
                    txtsizeto.Enabled = false;
                    this.ActiveControl = txtMRP;
                }
            }
            if (product_entrytype == 1 || product_entrytype == 2)  //producttype == 0 && (WithBatchExp == "Y" || onlyBatch == "Y")
            {
                // 1 - krushi with batch and expiry
                txtBatchNo.Enabled = true;
                dtpExpiry.Enabled = true;
                txtpCess.Text = "0";
                txtpCess.Enabled = false;
                txtpcommRs.Enabled = false;
                txtpcomm.Enabled = false;
                dtgvSize.Enabled = false;
                txtsizefrom.Enabled = false;
                txtsizegap.Enabled = false;
                txtsizeto.Enabled = false;
                pnlIntegratedDtls.Visible = true;
                AdjustPanelString = "BE";
                if (product_entrytype == 2)
                {
                    dtpExpiry.Enabled = false;
                    AdjustPanelString = "EE";
                }
            }
            else if (product_entrytype == 4)// without size //matrix
            {
                txtBatchNo.Enabled = false;
                dtpExpiry.Enabled = false;
                txtsizefrom.Enabled = false;
                txtsizeto.Enabled = false;
                txtsizegap.Enabled = false;
                dtgvSize.Enabled = false;
                pnlIntegratedDtls.Visible = false;
            }
            else if (product_entrytype == 3 && showsize == true)  // With Size
            {
                txtBatchNo.Enabled = false;
                dtpExpiry.Enabled = false;
                txtBatchNo.Visible = false;
                dtpExpiry.Visible = false;
                txtsizefrom.Enabled = true;
                txtsizeto.Enabled = true;
                txtsizegap.Enabled = true;
                dtgvSize.Enabled = true;
                dtgvSize.Columns["SBarcodeg"].Visible = true;
                dtgvSize.Columns["SRateg"].Visible = true;
                if (barcodestatus == "Our Barcode" || barcodestatus == "Static Barcode")
                {
                    matrixwithbarcode = true;
                    if (barcodestatus == "Our Barcode")
                        dtgvSize.Columns["SBarcodeg"].Visible = false;
                    else
                        txtstaticBarcode.Enabled = false;
                }
                else
                {
                    matrixwithbarcode = true;
                    HideSaleRate = true;
                    dtgvSize.Columns["SBarcodeg"].Visible = false;
                    dtgvSize.Columns["SRateg"].Visible = false;
                }
                txtSaleRate.Enabled = false;
                pnlIntegratedDtls.Visible = true;
                txtsizefrom.Select();
            }
        }

        private void lstprefixproduct_Leave(object sender, EventArgs e)
        {
            try
            {
                lstprefixproduct.Visible = false;
                txtpname.Text = lstprefixproduct.Text;
                this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void txtfreightchrgs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtexciseduty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtchequeno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
            {
                if (txtchequeno.Text != "")
                    pnlBank.Visible = false;
            }
        }

        private void txtBatchNo_Enter(object sender, EventArgs e)
        {
            if (productentrytype == 2)
                VisibleBatchPanel = "B";
        }

        private void getProductType(string proname, string company, string unit)
        {
            if (glbproductdata == null)
            {
                glbproductrepo = new ProductRepository();
                glbproductdata = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    glbproductdata = glbproductrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    glbproductdata = glbproductrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
            }

            if (glbproductdata != null)
            {
                productentrytype = Convert.ToInt32(glbproductdata.ProductType);  // allinone standard
                if (glbproductdata.ProductType == 0 && glbproductdata.WithBatchAndExpiry == "N" && glbproductdata.WithBatch == "N")
                    productentrytype = 0;
                else if (glbproductdata.ProductType == 0 && glbproductdata.WithBatchAndExpiry == "Y")
                    productentrytype = 1;
                else if (glbproductdata.ProductType == 0 && glbproductdata.WithBatch == "Y") //krushi
                    productentrytype = 2;
                else if (glbproductdata.ProductType == 3 && glbproductdata.WithoutSize == "N")  //clothing
                    productentrytype = 3;
                else if (glbproductdata.ProductType == 3 && glbproductdata.WithoutSize == "Y")  //clothing
                    productentrytype = 4;
                else if (glbproductdata.ProductType == 1) // serialized
                    productentrytype = 5;
                else if (glbproductdata.ProductType == 2) // Assembly
                    productentrytype = 6;
                else if (glbproductdata.ProductType == 4) //kit
                    productentrytype = 7;
                else if (glbproductdata.ProductType == 5) //scrap
                    productentrytype = 8;
                else if (glbproductdata.ProductType == 6) //Work type
                    productentrytype = 9;
                else
                    productentrytype = 0;
            }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmUnitInformation unit = new frmUnitInformation();
                        unit.ShowDialog();
                    }
                }
            }
            catch (Exception) { }
        }

        private void chkTPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (cmbGodownName.Enabled == true)
                        this.ActiveControl = cmbGodownName;
                    else
                        this.ActiveControl = txtMRefNo;
                }
            }
            catch (Exception) { }
        }

        private void cmbGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
            }
            catch (Exception) { }
        }

        private void txtFreeQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpurchaseprice;
            }
            catch (Exception)
            { }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtDiscAmt;
            }
            catch (Exception)
            { }
        }

        private void txtDiscAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSpcDisc;
            }
            catch (Exception)
            { }
        }

        private void txtSpcDisc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtfreightchrgs;
            }
            catch (Exception)
            { }
        }

        private void txtfreightchrgs_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtexciseduty;
            }
            catch (Exception)
            { }
        }

        private void txtexciseduty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPackaging;
            }
            catch (Exception)
            { }
        }

        private void txtPackaging_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkGstPack;
            }
            catch (Exception)
            { }
        }

        private void chkGstPack_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtGstPackpercent;
            }
            catch (Exception)
            { }
        }

        private void txtGstPackpercent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txttranscharges;
            }
            catch (Exception)
            { }
        }

        private void txtGstTransport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPaidAmt;
            }
            catch (Exception)
            { }
        }

        private void txttranscharges_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkGstTransport;
            }
            catch (Exception)
            { }
        }

        private void chkGstTransport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtGstTransport;
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (btnSave.Enabled == true)
                        this.ActiveControl = btnSave;
                    else
                        this.ActiveControl = btnUpdate;
                }
            }
            catch (Exception)
            { }
        }

        private void txtBatchNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBatchNo.Text != "")
                {
                    Regex mRegxExpression;
                    mRegxExpression = new Regex(",");
                    if (mRegxExpression.IsMatch(txtBatchNo.Text.Trim()))
                    {
                        MessageBox.Show("Comma is not valid in this Batch No");
                        this.ActiveControl = txtBatchNo;
                    }
                }
            }
            catch (Exception) { }
        }

        public void updatebatchtable(int i)
        {
            try
            {
                CustomerBillMasterRepository custmrepo = new CustomerBillMasterRepository();
                PurchaseDetailRepository ppRepo = new PurchaseDetailRepository();
                BatchRepository batchRepo = new BatchRepository();

                List<decimal> nQty = new List<decimal>();
                string proname = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                string company = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                string batchno = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                string ExpiryDate = Convert.ToString(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                BatchRepository batchrepo = new BatchRepository();
                decimal purcahseQty = 0;
                List<Batch> batchdata2 = batchrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == batchno && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == ExpiryDate && t.PorderNo == PurchaeId && t.CompId == CommonMethod.CompId).ToList();
                if (batchdata2 != null)
                {
                    foreach (var item in batchdata2)
                    {
                        PurchaseDetail pData = new PurchaseDetail();
                        ppRepo = new PurchaseDetailRepository();
                        PurchaseDetail purdetail = new PurchaseDetail();
                        PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        PurchaseMaster PMasterData = new PurchaseMaster();
                        PMasterData = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (PMasterData != null)
                            purdetail = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.ExpireDate.Value.ToString("MM/dd/yyyy") == ExpiryDate && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        purcahseQty = Convert.ToDecimal(purdetail.Quantity) + Convert.ToDecimal(purdetail.FreeQty);
                        decimal prqty = 0;
                        try
                        {
                            PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                            var PurDData = PurRDRepo.GetAll().Where(t => t.POID == purdetail.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                            prqty = Convert.ToDecimal(PurDData.Quantity);
                        }
                        catch (Exception)
                        {
                            prqty = 0;
                        }
                        BatchRepository brepo = new BatchRepository();
                        decimal uQty = Convert.ToDecimal(brepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == batchno && t.ExpiryDate.Value.ToString("MM/dd/yyyy") == ExpiryDate && t.Status != "0" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                        decimal srqty = 0;
                        try
                        {
                            BatchRepository batchrpstr = new BatchRepository();
                            List<Batch> BData = new List<Batch>();
                            BData = batchrpstr.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == batchno && t.ExpiryDate.Value.ToString("MM/dd/yyyy") == ExpiryDate && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                            if (BData != null)
                            {
                                foreach (var item1 in BData)
                                {
                                    try
                                    {
                                        GoodDetailRepository SRRepo = new GoodDetailRepository();
                                        GoodReturnDetail SRData = new GoodReturnDetail();
                                        SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (SRData != null)
                                            srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                    }
                                    catch (Exception)
                                    { }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            srqty = 0;
                        }
                        if ((purcahseQty - prqty) == (uQty - srqty))
                        {
                            pMastRepo = new PurchaseMasterRepository();
                            PurchaseDetail purdetail11 = new PurchaseDetail();
                            PurchaseMaster PMasterData1 = new PurchaseMaster();
                            PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (PMasterData1 != null)
                                purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.ExpireDate.Value.ToString("MM/dd/yyyy") == ExpiryDate && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                            pData = ppRepo.GetById(purdetail11.Id);
                            pData.Status = "Inactive";
                            ppRepo.Edit(pData);
                            ppRepo.Save();
                        }
                        else
                        {
                            pMastRepo = new PurchaseMasterRepository();
                            PurchaseDetail purdetail11 = new PurchaseDetail();
                            PurchaseMaster PMasterData1 = new PurchaseMaster();
                            PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (PMasterData1 != null)
                                purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.ExpireDate.Value.ToString("MM/dd/yyyy") == ExpiryDate && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                            pData = ppRepo.GetById(purdetail11.Id);
                            pData.Status = "Active";
                            ppRepo.Edit(pData);
                            ppRepo.Save();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Excel();
            DataSet ds = new DataSet();              //                        0                   1                          2                          3                           4                            
            SqlDataAdapter da = new SqlDataAdapter("SELECT     PurchaseDetail.Id, PurchaseDetail.PorderNo, PurchaseDetail.ProductName, PurchaseDetail.CompanyName, PurchaseDetail.Quantity," +
                //                 5                        6                                                                    7                           8                                                                                           
                "PurchaseDetail.Amount, PurchaseDetail.ExpireDate, ((PurchaseDetail.Discount*PurchaseDetail.Amount)/100) as Pdiscount, PurchaseDetail.TotalAmount, " +
                //                 9                      10                   11                     12                                                                      
                "PurchaseDetail.FreeQty, PurchaseDetail.Status, PurchaseDetail.Code, PurchaseDetail.SGstAmt," +
                //                 13                     14                        15                                                                                                               
                "PurchaseDetail.CGstAmt, PurchaseDetail.IGstAmt, PurchaseDetail.HasMandatoryCodeSeries," +
                //                                                                                                                  16                       17                    18                      19                      20                                        
                "(((PurchaseDetail.Amount-(PurchaseDetail.Discount*PurchaseDetail.Amount)/100)*PurchaseDetail.SpcDisc)/100) as specialdisc, PurchaseDetail.Cess, PurchaseDetail.Barcode, PurchaseDetail.SaleRate, PurchaseDetail.StaticBarcode," +
                //                  21                     22                     23                         24                        25                                           
                "PurchaseDetail.ColorName, PurchaseDetail.Size, PurchaseDetail.DeptName, PurchaseDetail.CategoryName, PurchaseDetail.Commission," +
                //                26                          27                                                                                                                   
                "PurchaseDetail.BatchNo,PurchaseMaster.SupplierId," +
                //                  28                                      29                                             30                                                                                                                                
                "PurchaseMaster.Suppliername, PurchaseMaster.Discount AS MasterDiscount, PurchaseMaster.SpcDiscount as MasterSpcDiscount," +
                //                31                               32                                                                                                
                "PurchaseMaster.PaidAmount, PurchaseMaster.TransportCharges," +
                //                33                                                             
                "PurchaseMaster.TAXType," +
                //                   34                            35                                  36                                                                            
                "PurchaseMaster.TransportGst, PurchaseMaster.PackagingAndForwarding, PurchaseMaster.PackagingGstPercent," +
                //                     37                        38                                              39                           40                        41                                                                          
                "PurchaseMaster.Freightchrges, PurchaseMaster.ExciseDuty, PurchaseMaster.CategoryName AS MasterCategoryName, PurchaseMaster.CatId, PurchaseMaster.CreatedbyId," +
                //                    42                         43                              44                             45                           
                "PurchaseMaster.UpdatedbyId,ProductInformation.Category, ProductInformation.WithBatch, ProductInformation.WithBatchAndExpiry, " +
                //                     46                                 47                          48                       49                           50                         51                      52                             53                    
                "ProductInformation.BarcodeStatus,ProductInformation.ProductCode,ProductInformation.Prefix,GroupMaster.GroupMasterName,SupplierInformation.VatNo,PurchaseMaster.PorderDate,PurchaseMaster.SupplierpoNo,ProductInformation.HSNCode " +
                 "FROM  PurchaseDetail INNER JOIN " +
                      "PurchaseMaster ON PurchaseDetail.PorderNo = PurchaseMaster.Id INNER JOIN ProductInformation ON (PurchaseDetail.ProductName = ProductInformation.ProductName and PurchaseDetail.CompanyName = ProductInformation.ManufactureCompany and PurchaseDetail.Unit = ProductInformation.Uniti) left outer JOIN GroupMaster ON ProductInformation.GroupMasterId = GroupMaster.GroupMasterId Inner Join SupplierInformation on SupplierInformation.Id=PurchaseMaster.SupplierId where TransactionYear='" + CommonMethod.TransactionYear + "' order by PurchaseDetail.PorderNo ", db.Connection);
            da.Fill(ds, "PurchaseDetail");
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            { //consttructor code here
                try
                {
                    ExportToExcel(ds, saveFileDialog.FileName);
                }
                catch { }
            }
        }

        public void ExportToExcel(DataSet ds, string filename)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;

                object misValue = System.Reflection.Missing.Value;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                // excelSheet.Name = "Test work sheet";
                excelSheet.Application.ActiveWindow.ScrollRow = 1;
                excelSheet.Application.ActiveWindow.SplitRow = 1;
                excelSheet.Application.ActiveWindow.FreezePanes = true;
                string data = null;
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = ds.Tables["PurchaseDetail"].Rows.Count;
                progressBar1.Visible = true;
                int m = 1, k = 0, i = 0;

                k = i;
                excelSheet.get_Range("A1", "AS1").Font.Bold = true;
                excelSheet.get_Range("A1", "AS1").Font.Color = System.Drawing.Color.Red;
                excelSheet.Cells[m, 1] = "Bill No";
                excelSheet.Cells[m, 2] = "Document No";
                excelSheet.Cells[m, 3] = "Document Date";
                excelSheet.Cells[m, 4] = "Product Name";
                excelSheet.Cells[m, 5] = "Company Name";
                excelSheet.Cells[m, 6] = " Quantity";
                excelSheet.Cells[m, 7] = "Ammount";
                excelSheet.Cells[m, 8] = "Expiry Date";
                excelSheet.Cells[m, 9] = "Discount";
                excelSheet.Cells[m, 10] = "Total Amount";
                excelSheet.Cells[m, 11] = "free Quantity";
                excelSheet.Cells[m, 12] = "Status";
                excelSheet.Cells[m, 13] = "Code";
                excelSheet.Cells[m, 14] = "Sgst Amount";
                excelSheet.Cells[m, 15] = "Cgst Amount";
                excelSheet.Cells[m, 16] = "IGST Amount";
                excelSheet.Cells[m, 17] = "Has Mandatory Code Series";
                excelSheet.Cells[m, 18] = "Special Disc.";
                excelSheet.Cells[m, 19] = "cess";
                excelSheet.Cells[m, 20] = "Barcode";
                excelSheet.Cells[m, 21] = "Sale Rate";
                excelSheet.Cells[m, 22] = " Static Barcode";
                excelSheet.Cells[m, 23] = " Color Name";
                excelSheet.Cells[m, 24] = "Size";
                excelSheet.Cells[m, 25] = "Dept Name";
                excelSheet.Cells[m, 26] = "Category Name";
                excelSheet.Cells[m, 27] = "Commission";
                excelSheet.Cells[m, 28] = "Batch No";
                excelSheet.Cells[m, 29] = " Supplier Name";
                excelSheet.Cells[m, 30] = "Master Disc";
                excelSheet.Cells[m, 31] = "Master SpcDisc";
                excelSheet.Cells[m, 32] = "Paid Amount";
                excelSheet.Cells[m, 33] = "Transport Charges";
                excelSheet.Cells[m, 34] = "Tax Type";
                excelSheet.Cells[m, 35] = " Transport Gst";
                excelSheet.Cells[m, 36] = "PackagingAndForword";
                excelSheet.Cells[m, 37] = "Packaging Gst%";
                excelSheet.Cells[m, 38] = "Freightchrges";
                excelSheet.Cells[m, 39] = "Excise duty";
                excelSheet.Cells[m, 40] = "Pcode";
                excelSheet.Cells[m, 41] = "Group";
                excelSheet.Cells[m, 42] = "Supplier Vatno.";
                excelSheet.Cells[m, 43] = "HSNCode";
                m++;

                for (i = 0; i <= ds.Tables["PurchaseDetail"].Rows.Count - 1; i++)
                {
                    progressBar1.Value = k;
                    int l = 0;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[1].ToString();//bill no.
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[52].ToString();// documentno
                    excelSheet.Cells[m, ++l] = data;
                    data = Convert.ToDateTime(ds.Tables["PurchaseDetail"].Rows[i].ItemArray[51]).ToString();// ducdate
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[2].ToString();//productname
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[3].ToString();//companyname
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[4].ToString();//quantity
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[5].ToString();//Amount
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[6].ToString();//expiry date
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[7].ToString();//discount
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[8].ToString();//total amount
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[9].ToString();//free quantity
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[10].ToString();//status
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[11].ToString();//code
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[12].ToString();//sgst amount
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[13].ToString();//cgst amount
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[14].ToString();//Igst
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[15].ToString();//Has mandatory code serries
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[16].ToString();//spcl disc
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[17].ToString();//cess
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[18].ToString();//barcode
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[19].ToString();//sale rate
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[20].ToString();//static barcode
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[21].ToString();//colorname
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[22].ToString();//size
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[23].ToString();//dept name
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[24].ToString();//category name
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[25].ToString();//commision
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[26].ToString();//batch no
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[28].ToString();//sup name
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[29].ToString();//master disc
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[30].ToString();//master spcl disc
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[31].ToString();//paid amt
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[32].ToString();//transport charges
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[33].ToString();//tyax type
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[34].ToString();//transport gst
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[35].ToString();//packaging and forword
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[36].ToString();//packaging gst %
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[37].ToString();//freightchrges
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[38].ToString();//Excise duty
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[48].ToString();//pcode
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[49].ToString();//group
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[50].ToString();//sup vatno
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["PurchaseDetail"].Rows[i].ItemArray[53].ToString();//HSN code
                    excelSheet.Cells[m, ++l] = data;
                    m++;
                    k++;
                }
                m++;
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs(filename, Excel1.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel1.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file " + filename);
                progressBar1.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
                {
                    if (Path.GetExtension(file.FileName).CompareTo(".xls") == 0 || Path.GetExtension(file.FileName).CompareTo(".xlsx") == 0)
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcelMonthly(file.FileName, Path.GetExtension(file.FileName)); //read excel file  
                            maxpurchaseorderid();
                            MessageBox.Show("Import Excel file sucessfully.", "Success");
                            progressBar1.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else
                        MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
            catch (Exception)
            { }
        }

        public DataTable ReadExcelMonthly(string fileName, string fileExt) //{DataTable dt=new DataTable(); return dt; }
        {
            string masterNetAmt = "Total Amount";
            decimal sgst = 0, Igst = 0;
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                con.Open();
                var sheets = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                try
                {
                    int count = 0;
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  F10,F11,F14,F17,F18,F19,F20,F21
                    if (dtexcel.Rows[0][0].ToString() == "Bill No")
                    {
                        count = 1;
                        masterNetAmt = "F10";
                    }
                    int i = 1;
                    progressBar1.Visible = true;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dtexcel.Rows.Count;

                    DataTable dt = new DataTable();
                    dt = dtexcel.Clone();
                    dt.Columns[6].DataType = typeof(decimal);
                    dt.Columns[9].DataType = typeof(decimal);
                    string BarCStatus, Batch, BatchnExpiry;
                    int colno = 1;
                    string BillNum;
                    if (Convert.ToString(dtexcel.Rows[count][1].ToString()) == "")
                    {
                        BillNum = Convert.ToString(dtexcel.Rows[count][0].ToString());
                        colno = 0;
                    }
                    else
                    {
                        BillNum = Convert.ToString(dtexcel.Rows[count][1].ToString());
                        colno = 1;
                    }

                    for (i = count; i <= (dtexcel.Rows.Count); i++)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        progressBar1.Value = i;
                        try
                        {
                            if (i == dtexcel.Rows.Count)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    PurchaseMaster custMaster = new PurchaseMaster();
                                    PurchaseMasterRepository masterrepo = new PurchaseMasterRepository();
                                    SupplierInformation newcust = new SupplierInformation();
                                    SupplierRepository newcustRepo = new SupplierRepository();
                                    try
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            newcust = newcustRepo.GetAll().Where(t => t.SupplierName == dt.Rows[0][28].ToString()).FirstOrDefault();
                                            if (newcust == null)
                                            {
                                                SupplierInformation newcust1 = new SupplierInformation();
                                                SupplierRepository newcustRepo1 = new SupplierRepository();
                                                newcust1.SupplierName = dt.Rows[0][28].ToString();
                                                newcust1.SupplierCode = maxSupid();
                                                newcust1.CompId = CommonMethod.CompId;
                                                newcust1.VatNo = dt.Rows[0][41].ToString();
                                                newcustRepo1.Add(newcust1);
                                                newcustRepo1.Save();
                                                newcust = newcust1;
                                            }
                                            string billinvoiceseries, billseriesnext; ; ;
                                            billinvoiceseries = (dt.Rows[0][colno].ToString());//"SHRL1";
                                            billseriesnext = "";
                                            txtsupplierpono.Text = billinvoiceseries;
                                            string billn = Convert.ToString(dt.Rows[0][1].ToString());
                                            maxpurchaseorderid();
                                            custMaster.PorderDate = Convert.ToDateTime(dt.Rows[0][2].ToString());
                                            custMaster.SupplierpoNo = billinvoiceseries /*+ 'S'*/ + billseriesnext;
                                            custMaster.PorderNo = Convert.ToInt32(txtporderno.Text);
                                            custMaster.Suppliername = Convert.ToString(dt.Rows[0][28].ToString());
                                            custMaster.TAXType = Convert.ToString(dt.Rows[0][33].ToString());
                                            custMaster.TransactionYear = CommonMethod.TransactionYear;
                                            custMaster.CompId = CommonMethod.CompId;
                                            try
                                            {
                                                custMaster.TransportCharges = Convert.ToDecimal(dt.Rows[0][32].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.TransportCharges = 0;
                                            }
                                            try
                                            {
                                                custMaster.TransportGst = Convert.ToDecimal(dt.Rows[0][34].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.TransportGst = 0;
                                            }
                                            try
                                            {
                                                custMaster.PackagingAndForwarding = Convert.ToDecimal(dt.Rows[0][35].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.PackagingAndForwarding = 0;
                                            }
                                            try
                                            {
                                                custMaster.PackagingGstPercent = Convert.ToDecimal(dt.Rows[0][36].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.PackagingGstPercent = 0;
                                            }
                                            try
                                            {
                                                custMaster.Freightchrges = Convert.ToDecimal(dt.Rows[0][37].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.Freightchrges = 0;
                                            }
                                            try
                                            {
                                                custMaster.ExciseDuty = Convert.ToDecimal(dt.Rows[0][38].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.ExciseDuty = 0;
                                            }
                                            try
                                            {
                                                custMaster.Discount = Math.Round(Convert.ToDecimal((Convert.ToDecimal(dt.Rows[0][29].ToString()) * 100) / Convert.ToDecimal(dt.Compute("Sum([" + masterNetAmt + "])", string.Empty))), 2, MidpointRounding.AwayFromZero);
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.Discount = 0;
                                            }
                                            custMaster.SupplierId = newcust.SupplierCode;
                                            custMaster.TotalAmount = Convert.ToDecimal(dt.Compute("Sum([" + masterNetAmt + "])", string.Empty));//Convert.ToDecimal(dtexcel.Rows[8][0].ToString());
                                            decimal ntamt, tntamt;
                                            if ((dt.Rows[0][29].ToString()) != "" && (dt.Rows[0][29]) != null)
                                                ntamt = Convert.ToDecimal(custMaster.TotalAmount - Convert.ToDecimal(dt.Rows[0][29].ToString()));
                                            else
                                                ntamt = Convert.ToDecimal(custMaster.TotalAmount - 0);
                                            custMaster.NetAmount = ntamt;
                                            if ((dt.Rows[0][30].ToString()) != "" && (dt.Rows[0][30]) != null)
                                                tntamt = Convert.ToDecimal(custMaster.NetAmount - Convert.ToDecimal(dt.Rows[0][30].ToString()));
                                            else
                                                tntamt = Convert.ToDecimal(custMaster.NetAmount - 0);
                                            custMaster.NetAmount = tntamt;
                                            try
                                            {
                                                custMaster.PaidAmount = Convert.ToDecimal(dt.Rows[0][31].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.PaidAmount = 0;
                                            }
                                            try
                                            {
                                                custMaster.RemainingAmount = custMaster.NetAmount - custMaster.PaidAmount;
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.RemainingAmount = 0;
                                            }
                                            masterrepo.Add(custMaster);
                                            masterrepo.Save();
                                        }
                                    }
                                    catch (Exception) { }

                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        ProductRepository productrepo1 = new ProductRepository();
                                        ProductInformation pinfo1 = new ProductInformation();
                                        try
                                        {
                                            string p = Convert.ToString(dt.Rows[j][3].ToString());
                                            string u = "Pieces";
                                            string c = Convert.ToString(dt.Rows[j][4].ToString());
                                            if (string.IsNullOrEmpty(p) || string.IsNullOrEmpty(u) || string.IsNullOrEmpty(c))
                                            {
                                                MessageBox.Show("Please Fill all MasterInformation first Some MasterInformation is not found in database");
                                                goto A;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    if (CommonMethod.commProduct == true)
                                                        pinfo1 = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == p.Trim().ToLower() && t.ManufactureCompany.ToLower() == c.ToLower() && t.Uniti.ToLower() == u.ToLower()).FirstOrDefault();
                                                    else
                                                        pinfo1 = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == p.Trim().ToLower() && t.ManufactureCompany.ToLower() == c.ToLower() && t.Uniti.ToLower() == u.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                    decimal disc = 0;
                                                    if (dt.Rows[j][8].ToString() == "" || dt.Rows[j][8].ToString() == null)
                                                        disc = 0;
                                                    else
                                                        disc = Convert.ToDecimal(dt.Rows[j][8].ToString());
                                                    if (dt.Rows[j][13].ToString() != "" && dt.Rows[j][13].ToString() != null)
                                                    {
                                                        try
                                                        {
                                                            sgst = Math.Round((Convert.ToDecimal(dt.Rows[j][13].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                            Igst = 0;
                                                        }
                                                        catch
                                                        {
                                                            sgst = 0;
                                                            Igst = 0;
                                                        }
                                                    }
                                                    else if (dt.Rows[j][15].ToString() != "" && dt.Rows[j][15].ToString() != null)
                                                    {
                                                        try
                                                        {
                                                            Igst = Math.Round((Convert.ToDecimal(dt.Rows[j][15].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                            sgst = 0;
                                                        }
                                                        catch
                                                        {
                                                            sgst = 0;
                                                            Igst = 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Igst = 0;
                                                        sgst = 0;
                                                    }

                                                    if (pinfo1 == null)
                                                    {
                                                        pinfo1 = productrepo1.GetAll().Where(t => t.Prefix == Convert.ToString(dt.Rows[j][39].ToString()) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                        if (pinfo1 != null)
                                                        {
                                                            p = pinfo1.ProductName;
                                                            u = pinfo1.Uniti;
                                                            c = pinfo1.ManufactureCompany;
                                                            disc = 0;
                                                            if (dt.Rows[j][8].ToString() == "" || dt.Rows[j][8].ToString() == null)
                                                                disc = 0;
                                                            else
                                                                disc = Convert.ToDecimal(dt.Rows[j][8].ToString());
                                                            if (dt.Rows[j][13].ToString() != "" && dt.Rows[j][13].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    sgst = Math.Round((Convert.ToDecimal(dt.Rows[j][13].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    Igst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else if (dt.Rows[j][15].ToString() != "" && dt.Rows[j][15].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    Igst = Math.Round((Convert.ToDecimal(dt.Rows[j][15].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    sgst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Igst = 0;
                                                                sgst = 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            AIOInventorySystem.Data.Model.Unit un = new AIOInventorySystem.Data.Model.Unit();
                                                            UnitRepository unRepo = new UnitRepository();
                                                            ManuCompanyInfo newcom = new ManuCompanyInfo();
                                                            ManufactureCInfoRepository newcrepo = new ManufactureCInfoRepository();
                                                            GroupMaster grmaste = new GroupMaster();
                                                            GroupMasterRepository grmasterrepo = new GroupMasterRepository();
                                                            pinfo1 = new ProductInformation();
                                                            productrepo1 = new ProductRepository();
                                                            grmaste = grmasterrepo.GetAll().Where(t => t.GroupMasterName == Convert.ToString(dt.Rows[j][40].ToString())).FirstOrDefault();
                                                            un = unRepo.GetAll().Where(t => t.UnitName == u).FirstOrDefault();
                                                            if (dt.Rows[j][19].ToString() != "")
                                                                pinfo1.BarcodeStatus = "No Barcode";
                                                            else if (dt.Rows[j][21].ToString() != "")
                                                                pinfo1.BarcodeStatus = "Static Barcode";
                                                            else
                                                                pinfo1.BarcodeStatus = "No Barcode";
                                                            if (dt.Rows[j][27].ToString() != "" && dt.Rows[j][6].ToString() == "")
                                                                pinfo1.WithBatch = "Y";
                                                            else
                                                                pinfo1.WithBatch = "N";
                                                            if (dt.Rows[j][27].ToString() != "" && dt.Rows[j][7].ToString() != "")
                                                                pinfo1.WithBatchAndExpiry = "Y";
                                                            else
                                                                pinfo1.WithBatchAndExpiry = "N";
                                                            newcom = newcrepo.GetAll().Where(t => t.CompanyName == c).FirstOrDefault();
                                                            if (un == null)
                                                                SaveUnit(u);
                                                            if (newcom == null)
                                                                SaveCompany(c);
                                                            if (grmaste == null)
                                                                SaveGroup(dt.Rows[j][40].ToString());
                                                            pinfo1.ManufactureCompany = c;
                                                            pinfo1.Uniti = u;
                                                            pinfo1.ProductName = p;
                                                            grmaste = grmasterrepo.GetAll().Where(t => t.GroupMasterName == Convert.ToString(dt.Rows[j][40].ToString())).FirstOrDefault();
                                                            pinfo1.Prefix = Convert.ToString(dt.Rows[j][39].ToString());
                                                            pinfo1.GroupMasterId = grmaste.GroupMasterId;
                                                            pinfo1.Rate = 0;
                                                            pinfo1.DealerRate = 0;
                                                            pinfo1.Category = "N/A";
                                                            pinfo1.ProductCode = maxproductid();
                                                            pinfo1.PurchaseRate = 0;
                                                            pinfo1.ProductDiscount = 0;
                                                            pinfo1.TaxInclusive = "True";
                                                            pinfo1.HSNCode = Convert.ToString(dt.Rows[j][11].ToString());
                                                            disc = 0;
                                                            if (dt.Rows[j][8].ToString() == "" || dt.Rows[j][8].ToString() == null)
                                                                disc = 0;
                                                            else
                                                                disc = Convert.ToDecimal(dt.Rows[j][8].ToString());
                                                            if (dt.Rows[j][13].ToString() != "" && dt.Rows[j][13].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    sgst = Math.Round((Convert.ToDecimal(dt.Rows[j][13].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    Igst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else if (dt.Rows[j][15].ToString() != "" && dt.Rows[j][15].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    Igst = Math.Round((Convert.ToDecimal(dt.Rows[j][15].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    sgst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Igst = 0;
                                                                sgst = 0;
                                                            }
                                                            if (sgst != 0)
                                                            {
                                                                pinfo1.CGstPercent = sgst;
                                                                pinfo1.SGstPercent = sgst;
                                                                pinfo1.IGstPercent = 0;
                                                                pinfo1.Pigstpercent = 0;
                                                                pinfo1.Pcgstpercent = sgst;
                                                                pinfo1.Psgstpercent = sgst;
                                                            }
                                                            else if (Igst != 0)
                                                            {
                                                                pinfo1.CGstPercent = 0;
                                                                pinfo1.SGstPercent = 0;
                                                                pinfo1.IGstPercent = Igst;
                                                                pinfo1.Pigstpercent = Igst;
                                                                pinfo1.Pcgstpercent = 0;
                                                                pinfo1.Psgstpercent = 0;
                                                            }
                                                            else
                                                            {
                                                                pinfo1.CGstPercent = 0;
                                                                pinfo1.SGstPercent = 0;
                                                                pinfo1.IGstPercent = 0;
                                                                pinfo1.Pigstpercent = 0;
                                                                pinfo1.Pcgstpercent = 0;
                                                                pinfo1.Psgstpercent = 0;
                                                            }
                                                            pinfo1.ProductType = 0;
                                                            pinfo1.PurchaseRate = 0;
                                                            pinfo1.ProductDiscount = 0;
                                                            pinfo1.Usesubtitle = "N";
                                                            pinfo1.decription = "";
                                                            pinfo1.StockAlert = 0;
                                                            pinfo1.HasWarranty = "N";
                                                            pinfo1.TaxSlab = "N";
                                                            pinfo1.WithoutSize = "N";
                                                            pinfo1.CategoryId = 0;
                                                            pinfo1.SubCatId = 0;
                                                            pinfo1.SubCatName = "";
                                                            pinfo1.AlternateDealerRate = Convert.ToDecimal(0);
                                                            pinfo1.Warranty = "";
                                                            pinfo1.PPOnMRP = "False";
                                                            pinfo1.RackNO = "";
                                                            pinfo1.AlternateUnitPurchaseRate = Convert.ToDecimal(0);
                                                            pinfo1.Vat = Convert.ToDecimal(0.00);
                                                            pinfo1.DealerRate = Convert.ToDecimal(0.00);
                                                            pinfo1.CompId = CommonMethod.CompId;
                                                            productrepo1.Add(pinfo1);
                                                            productrepo1.Save();
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                            BarCStatus = pinfo1.BarcodeStatus;
                                            Batch = pinfo1.WithBatch;
                                            BatchnExpiry = pinfo1.WithBatchAndExpiry;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        try
                                        {
                                            decimal sinrate = 0, sindisc = 0, sindiscper = 0, sinspcldisc = 0, sinspcldiscper = 0, SaleRate = 0, SgstAmt = 0, CgstAmt = 0, Amount = 0, TaxableAmt = 0, IgstAmt = 0, TotalAmount = 0, Quantity = 0;
                                            try
                                            {
                                                Quantity = Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                Quantity = 0;
                                            }
                                            try
                                            {
                                                sinrate = Convert.ToDecimal(dt.Rows[j][6].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                sinrate = 0;
                                            }
                                            try
                                            {
                                                sindisc = Convert.ToDecimal(dt.Rows[j][8].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                sindisc = 0;
                                            }
                                            try
                                            {
                                                sindiscper = Math.Round(sindisc * 100 / sinrate, 2, MidpointRounding.AwayFromZero);
                                            }
                                            catch (Exception)
                                            {
                                                sindiscper = 0;
                                            }
                                            try
                                            {
                                                sinspcldisc = Convert.ToDecimal(dt.Rows[j][17].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                sinspcldisc = 0;
                                            }
                                            try
                                            {
                                                sinspcldiscper = Math.Round(sinspcldisc * 100 / (sinrate - sindisc), 2, MidpointRounding.AwayFromZero);
                                            }
                                            catch (Exception)
                                            {
                                                sinspcldiscper = 0;
                                            }
                                            try
                                            {
                                                Amount = Convert.ToDecimal(dt.Rows[j][6].ToString());
                                                TaxableAmt = Convert.ToDecimal(dt.Rows[j][6].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                Amount = 0;
                                                TaxableAmt = 0;
                                            }
                                            try
                                            {
                                                if (Convert.ToDecimal(dt.Rows[j][8].ToString()) != 0 && dt.Rows[j][8].ToString() != "")
                                                    TaxableAmt = TaxableAmt - (Convert.ToDecimal(dt.Rows[j][8].ToString()));
                                            }
                                            catch (Exception)
                                            {
                                                TaxableAmt = 0;
                                            }

                                            try
                                            {
                                                if (Convert.ToDecimal(dt.Rows[j][13].ToString()) != 0 && dt.Rows[j][13].ToString() != "")
                                                {
                                                    CgstAmt = Convert.ToDecimal(dt.Rows[j][13].ToString());//(TaxableAmt * Convert.ToDecimal(dt.Rows[j][29].ToString())) / 100;
                                                    SgstAmt = Convert.ToDecimal(dt.Rows[j][13].ToString());//(TaxableAmt * Convert.ToDecimal(dt.Rows[j][29].ToString())) / 100;
                                                    IgstAmt = 0;
                                                    TotalAmount = TaxableAmt + Convert.ToDecimal(dt.Rows[j][13].ToString()) + Convert.ToDecimal(dt.Rows[j][13].ToString());
                                                    SaleRate = TotalAmount / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                                }
                                                else if (Convert.ToDecimal(dt.Rows[j][15].ToString()) != 0 && dt.Rows[j][15].ToString() != "")
                                                {
                                                    CgstAmt = 0;
                                                    SgstAmt = 0;
                                                    IgstAmt = Convert.ToDecimal(dt.Rows[j][15].ToString());//(TaxableAmt * Convert.ToDecimal(dt.Rows[j][29].ToString())) / 100;
                                                    TotalAmount = TaxableAmt + IgstAmt;
                                                    SaleRate = TotalAmount / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                                }
                                                else
                                                {
                                                    CgstAmt = 0;
                                                    SgstAmt = 0;
                                                    IgstAmt = 0;
                                                    TotalAmount = TaxableAmt;
                                                    SaleRate = TotalAmount / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                CgstAmt = 0;
                                                SgstAmt = 0;
                                                IgstAmt = 0;
                                            }
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                            command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@BillNo", SqlDbType.Int).Value = custMaster.Id;
                                            command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = pinfo1.ProductName;//Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                            command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = pinfo1.ManufactureCompany;
                                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = pinfo1.Uniti;
                                            command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = pinfo1.Uniti;
                                            command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Quantity;
                                            command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                            command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "";
                                            command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = sinrate;//Convert.ToDecimal(dt.Rows[j][6].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;//Convert.ToDecimal(dt.Rows[j][6].ToString());
                                            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = sindiscper;
                                            command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                            command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = sgst;
                                            command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = SgstAmt;
                                            command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = sgst;
                                            command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = SgstAmt;
                                            command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Igst;
                                            command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = IgstAmt;
                                            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = TotalAmount;
                                            command.Parameters.Add("@HSNcode", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[j][42].ToString());
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@ProdSubtitle", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[j][3].ToString());
                                            command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = SaleRate;
                                            command.Parameters.Add("@SpcDisc", SqlDbType.Decimal).Value = sinspcldiscper;// Convert.ToString(dt.Rows[i][3].ToString());
                                            command.Parameters.Add("@Cess", SqlDbType.Decimal).Value = DBNull.Value; //Convert.ToString(dt.Rows[i][3].ToString());
                                            command.Parameters.Add("@CessAmt", SqlDbType.Decimal).Value = DBNull.Value;//Convert.ToString(dt.Rows[i][3].ToString());
                                            if (pinfo1.BarcodeStatus == "Static Barcode")
                                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = dt.Rows[j][21].ToString();// Convert.ToString(dt.Rows[i][3].ToString());                                            
                                            else if (pinfo1.BarcodeStatus == "Our Barcode")
                                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = dt.Rows[j][19].ToString();// Convert.ToString(dt.Rows[i][3].ToString());
                                            else
                                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = DBNull.Value;// Convert.ToString(dt.Rows[i][3].ToString());
                                            if (pinfo1.WithBatch == "Y")
                                            {
                                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = dt.Rows[j][27].ToString();//Convert.ToString(dt.Rows[i][3].ToString());
                                                command.Parameters.Add("@Expiry", SqlDbType.VarChar).Value = DBNull.Value;
                                            }
                                            else if (pinfo1.WithBatchAndExpiry == "Y")
                                            {
                                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = dt.Rows[j][27].ToString();//Convert.ToString(dt.Rows[i][3].ToString());
                                                command.Parameters.Add("@Expiry", SqlDbType.VarChar).Value = dt.Rows[j][7].ToString();//Convert.ToString(dt.Rows[i][3].ToString());
                                            }
                                            else
                                            {
                                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = DBNull.Value;//Convert.ToString(dt.Rows[i][3].ToString());
                                                command.Parameters.Add("@Expiry", SqlDbType.VarChar).Value = DBNull.Value;//Convert.ToString(dt.Rows[i][3].ToString());
                                            }
                                            command.Parameters.Add("@TCSPer", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSPer"].Value);
                                            command.Parameters.Add("@TCSAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TCSAmt"].Value);
                                            command.ExecuteNonQuery();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        {
                                            db.CloseConnection();
                                        }
                                    }
                                }
                            }
                            else
                                if (Convert.ToString(dtexcel.Rows[i][colno].ToString()) == BillNum)
                                {
                                    try
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr.ItemArray = dtexcel.Rows[i].ItemArray;
                                        dt.Rows.Add(dr);
                                    }
                                    catch { }
                                }
                                else
                                {
                                    PurchaseMaster custMaster = new PurchaseMaster();
                                    PurchaseMasterRepository masterrepo = new PurchaseMasterRepository();
                                    SupplierInformation newcust = new SupplierInformation();
                                    SupplierRepository newcustRepo = new SupplierRepository();
                                    try
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            newcust = newcustRepo.GetAll().Where(t => t.SupplierName == dt.Rows[0][28].ToString()).FirstOrDefault();
                                            if (newcust == null)
                                            {
                                                SupplierInformation newcust1 = new SupplierInformation();
                                                SupplierRepository newcustRepo1 = new SupplierRepository();
                                                newcust1.SupplierName = dt.Rows[0][28].ToString();
                                                newcust1.SupplierCode = maxSupid();
                                                newcust1.CompId = CommonMethod.CompId;
                                                newcust1.VatNo = dt.Rows[0][41].ToString();
                                                newcustRepo1.Add(newcust1);
                                                newcustRepo1.Save();
                                                newcust = newcust1;
                                            }
                                            string billinvoiceseries, billseriesnext; ; ;
                                            billinvoiceseries = (dt.Rows[0][colno].ToString());//"SHRL1";
                                            billseriesnext = "";//(dt.Rows[0][0].ToString()).Split('S')[2];
                                            txtsupplierpono.Text = billinvoiceseries;
                                            string billn = Convert.ToString(dt.Rows[0][1].ToString());
                                            maxpurchaseorderid();
                                            custMaster.PorderDate = Convert.ToDateTime(dt.Rows[0][2].ToString());
                                            custMaster.SupplierpoNo = billinvoiceseries /*+ 'S'*/ + billseriesnext;
                                            custMaster.PorderNo = Convert.ToInt32(txtporderno.Text);
                                            custMaster.Suppliername = Convert.ToString(dt.Rows[0][28].ToString());
                                            custMaster.TAXType = Convert.ToString(dt.Rows[0][33].ToString());
                                            custMaster.TransactionYear = CommonMethod.TransactionYear;
                                            custMaster.CompId = CommonMethod.CompId;
                                            try
                                            {
                                                custMaster.TransportCharges = Convert.ToDecimal(dt.Rows[0][32].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.TransportCharges = 0;
                                            }
                                            try
                                            {
                                                custMaster.TransportGst = Convert.ToDecimal(dt.Rows[0][34].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.TransportGst = 0;
                                            }
                                            try
                                            {
                                                custMaster.PackagingAndForwarding = Convert.ToDecimal(dt.Rows[0][35].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.PackagingAndForwarding = 0;
                                            }
                                            try
                                            {
                                                custMaster.PackagingGstPercent = Convert.ToDecimal(dt.Rows[0][36].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.PackagingGstPercent = 0;
                                            }
                                            try
                                            {
                                                custMaster.Freightchrges = Convert.ToDecimal(dt.Rows[0][37].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.Freightchrges = 0;
                                            }
                                            try
                                            {
                                                custMaster.ExciseDuty = Convert.ToDecimal(dt.Rows[0][38].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.ExciseDuty = 0;
                                            }
                                            try
                                            {
                                                custMaster.Discount = Math.Round(Convert.ToDecimal((Convert.ToDecimal(dt.Rows[0][29].ToString()) * 100) / Convert.ToDecimal(dt.Compute("Sum([" + masterNetAmt + "])", string.Empty))), 2, MidpointRounding.AwayFromZero);
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.Discount = 0;
                                            }
                                            custMaster.SupplierId = newcust.SupplierCode;
                                            custMaster.TotalAmount = Convert.ToDecimal(dt.Compute("Sum([" + masterNetAmt + "])", string.Empty));//Convert.ToDecimal(dtexcel.Rows[8][0].ToString());
                                            decimal ntamt, tntamt;
                                            if ((dt.Rows[0][29].ToString()) != "" && (dt.Rows[0][29]) != null)
                                                ntamt = Convert.ToDecimal(custMaster.TotalAmount - Convert.ToDecimal(dt.Rows[0][29].ToString()));
                                            else
                                                ntamt = Convert.ToDecimal(custMaster.TotalAmount - 0);
                                            custMaster.NetAmount = ntamt;
                                            if ((dt.Rows[0][30].ToString()) != "" && (dt.Rows[0][30]) != null)
                                                tntamt = Convert.ToDecimal(custMaster.NetAmount - Convert.ToDecimal(dt.Rows[0][30].ToString()));
                                            else
                                                tntamt = Convert.ToDecimal(custMaster.NetAmount - 0);
                                            custMaster.NetAmount = tntamt;
                                            try
                                            {
                                                custMaster.PaidAmount = Convert.ToDecimal(dt.Rows[0][31].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.PaidAmount = 0;
                                            }
                                            try
                                            {
                                                custMaster.RemainingAmount = custMaster.NetAmount - custMaster.PaidAmount;
                                            }
                                            catch (Exception)
                                            {
                                                custMaster.RemainingAmount = 0;
                                            }
                                            masterrepo.Add(custMaster);
                                            masterrepo.Save();
                                        }
                                    }
                                    catch (Exception) { }

                                    for (int j = 0; j < dt.Rows.Count; j++)
                                    {
                                        ProductRepository productrepo1 = new ProductRepository();
                                        ProductInformation pinfo1 = new ProductInformation();
                                        try
                                        {
                                            string p = Convert.ToString(dt.Rows[j][3].ToString());
                                            string u = "Pieces";
                                            string c = Convert.ToString(dt.Rows[j][4].ToString());
                                            if (string.IsNullOrEmpty(p) || string.IsNullOrEmpty(u) || string.IsNullOrEmpty(c))
                                            {
                                                MessageBox.Show("Please Fill all MasterInformation first Some MasterInformation is not found in database");
                                                goto A;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    if (CommonMethod.commProduct == true)
                                                        pinfo1 = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == p.Trim().ToLower() && t.ManufactureCompany.ToLower() == c.ToLower() && t.Uniti.ToLower() == u.ToLower()).FirstOrDefault();
                                                    else
                                                        pinfo1 = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == p.Trim().ToLower() && t.ManufactureCompany.ToLower() == c.ToLower() && t.Uniti.ToLower() == u.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();

                                                    decimal disc = 0;
                                                    if (dt.Rows[j][8].ToString() == "" || dt.Rows[j][8].ToString() == null)
                                                        disc = 0;
                                                    else
                                                        disc = Convert.ToDecimal(dt.Rows[j][8].ToString());
                                                    if (dt.Rows[j][13].ToString() != "" && dt.Rows[j][13].ToString() != null)
                                                    {
                                                        try
                                                        {
                                                            sgst = Math.Round((Convert.ToDecimal(dt.Rows[j][13].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                            Igst = 0;
                                                        }
                                                        catch
                                                        {
                                                            sgst = 0;
                                                            Igst = 0;
                                                        }
                                                    }
                                                    else if (dt.Rows[j][15].ToString() != "" && dt.Rows[j][15].ToString() != null)
                                                    {
                                                        try
                                                        {
                                                            Igst = Math.Round((Convert.ToDecimal(dt.Rows[j][15].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                            sgst = 0;
                                                        }
                                                        catch
                                                        {
                                                            sgst = 0;
                                                            Igst = 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Igst = 0;
                                                        sgst = 0;
                                                    }
                                                    if (pinfo1 == null)
                                                    {
                                                        pinfo1 = productrepo1.GetAll().Where(t => t.Prefix == Convert.ToString(dt.Rows[j][39].ToString()) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                        if (pinfo1 != null)
                                                        {
                                                            p = pinfo1.ProductName;
                                                            u = pinfo1.Uniti;
                                                            c = pinfo1.ManufactureCompany;
                                                            disc = 0;
                                                            if (dt.Rows[j][8].ToString() == "" || dt.Rows[j][8].ToString() == null)
                                                                disc = 0;
                                                            else
                                                                disc = Convert.ToDecimal(dt.Rows[j][8].ToString());
                                                            if (dt.Rows[j][13].ToString() != "" && dt.Rows[j][13].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    sgst = Math.Round((Convert.ToDecimal(dt.Rows[j][13].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    Igst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else if (dt.Rows[j][15].ToString() != "" && dt.Rows[j][15].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    Igst = Math.Round((Convert.ToDecimal(dt.Rows[j][15].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    sgst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Igst = 0;
                                                                sgst = 0;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            AIOInventorySystem.Data.Model.Unit un = new AIOInventorySystem.Data.Model.Unit();
                                                            UnitRepository unRepo = new UnitRepository();
                                                            ManuCompanyInfo newcom = new ManuCompanyInfo();
                                                            ManufactureCInfoRepository newcrepo = new ManufactureCInfoRepository();
                                                            GroupMaster grmaste = new GroupMaster();
                                                            GroupMasterRepository grmasterrepo = new GroupMasterRepository();
                                                            pinfo1 = new ProductInformation();
                                                            productrepo1 = new ProductRepository();
                                                            grmaste = grmasterrepo.GetAll().Where(t => t.GroupMasterName == Convert.ToString(dt.Rows[j][40].ToString())).FirstOrDefault();
                                                            un = unRepo.GetAll().Where(t => t.UnitName == u).FirstOrDefault();
                                                            if (dt.Rows[j][19].ToString() != "")
                                                                pinfo1.BarcodeStatus = "No Barcode";
                                                            else if (dt.Rows[j][21].ToString() != "")
                                                                pinfo1.BarcodeStatus = "Static Barcode";
                                                            else
                                                                pinfo1.BarcodeStatus = "No Barcode";
                                                            if (dt.Rows[j][27].ToString() != "" && dt.Rows[j][6].ToString() == "")
                                                                pinfo1.WithBatch = "Y";
                                                            else
                                                                pinfo1.WithBatch = "N";
                                                            if (dt.Rows[j][27].ToString() != "" && dt.Rows[j][7].ToString() != "")
                                                                pinfo1.WithBatchAndExpiry = "Y";
                                                            else
                                                                pinfo1.WithBatchAndExpiry = "N";
                                                            newcom = newcrepo.GetAll().Where(t => t.CompanyName == c).FirstOrDefault();
                                                            if (un == null)
                                                                SaveUnit(u);
                                                            if (newcom == null)
                                                                SaveCompany(c);
                                                            if (grmaste == null)
                                                                SaveGroup(dt.Rows[j][40].ToString());
                                                            pinfo1.ManufactureCompany = c;
                                                            pinfo1.Uniti = u;
                                                            pinfo1.ProductName = p;
                                                            grmaste = grmasterrepo.GetAll().Where(t => t.GroupMasterName == Convert.ToString(dt.Rows[j][40].ToString())).FirstOrDefault();
                                                            pinfo1.Prefix = Convert.ToString(dt.Rows[j][39].ToString());
                                                            pinfo1.GroupMasterId = grmaste.GroupMasterId;
                                                            pinfo1.Rate = 0;
                                                            pinfo1.DealerRate = 0;
                                                            pinfo1.Category = "N/A";
                                                            pinfo1.ProductCode = maxproductid();
                                                            pinfo1.PurchaseRate = 0;
                                                            pinfo1.ProductDiscount = 0;
                                                            pinfo1.TaxInclusive = "True";
                                                            pinfo1.HSNCode = Convert.ToString(dt.Rows[j][11].ToString());
                                                            disc = 0;
                                                            if (dt.Rows[j][8].ToString() == "" || dt.Rows[j][8].ToString() == null)
                                                                disc = 0;
                                                            else
                                                                disc = Convert.ToDecimal(dt.Rows[j][8].ToString());
                                                            if (dt.Rows[j][13].ToString() != "" && dt.Rows[j][13].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    sgst = Math.Round((Convert.ToDecimal(dt.Rows[j][13].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    Igst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else if (dt.Rows[j][15].ToString() != "" && dt.Rows[j][15].ToString() != null)
                                                            {
                                                                try
                                                                {
                                                                    Igst = Math.Round((Convert.ToDecimal(dt.Rows[j][15].ToString()) * 100) / (Convert.ToDecimal(dt.Rows[j][6].ToString()) - disc));
                                                                    sgst = 0;
                                                                }
                                                                catch
                                                                {
                                                                    sgst = 0;
                                                                    Igst = 0;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Igst = 0;
                                                                sgst = 0;
                                                            }
                                                            if (sgst != 0)
                                                            {
                                                                pinfo1.CGstPercent = sgst;
                                                                pinfo1.SGstPercent = sgst;
                                                                pinfo1.IGstPercent = 2 * sgst;
                                                                pinfo1.Pigstpercent = 2 * sgst;
                                                                pinfo1.Pcgstpercent = sgst;
                                                                pinfo1.Psgstpercent = sgst;
                                                            }
                                                            else if (Igst != 0)
                                                            {
                                                                pinfo1.CGstPercent = Igst / 2;
                                                                pinfo1.SGstPercent = Igst / 2;
                                                                pinfo1.IGstPercent = Igst;
                                                                pinfo1.Pigstpercent = Igst;
                                                                pinfo1.Pcgstpercent = Igst / 2;
                                                                pinfo1.Psgstpercent = Igst / 2;
                                                            }
                                                            else
                                                            {
                                                                pinfo1.CGstPercent = 0;
                                                                pinfo1.SGstPercent = 0;
                                                                pinfo1.IGstPercent = 0;
                                                                pinfo1.Pigstpercent = 0;
                                                                pinfo1.Pcgstpercent = 0;
                                                                pinfo1.Psgstpercent = 0;
                                                            }
                                                            pinfo1.ProductType = 0;
                                                            pinfo1.PurchaseRate = 0;
                                                            pinfo1.ProductDiscount = 0;
                                                            pinfo1.Usesubtitle = "N";
                                                            pinfo1.decription = "";
                                                            pinfo1.StockAlert = 0;
                                                            pinfo1.HasWarranty = "N";
                                                            pinfo1.TaxSlab = "N";
                                                            pinfo1.WithoutSize = "N";
                                                            pinfo1.CategoryId = 0;
                                                            pinfo1.SubCatId = 0;
                                                            pinfo1.SubCatName = "";
                                                            pinfo1.AlternateDealerRate = Convert.ToDecimal(0);
                                                            pinfo1.Warranty = "";
                                                            pinfo1.PPOnMRP = "False";
                                                            pinfo1.RackNO = "";
                                                            pinfo1.AlternateUnitPurchaseRate = Convert.ToDecimal(0);
                                                            pinfo1.Vat = Convert.ToDecimal(0.00);
                                                            pinfo1.DealerRate = Convert.ToDecimal(0.00);
                                                            pinfo1.CompId = CommonMethod.CompId;
                                                            productrepo1.Add(pinfo1);
                                                            productrepo1.Save();
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show(ex.Message);
                                                }
                                            }
                                            BarCStatus = pinfo1.BarcodeStatus;
                                            Batch = pinfo1.WithBatch;
                                            BatchnExpiry = pinfo1.WithBatchAndExpiry;
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        try
                                        {
                                            decimal sinrate = 0, sindisc = 0, sindiscper = 0, sinspcldisc = 0, sinspcldiscper = 0, SaleRate = 0, SgstAmt = 0, CgstAmt = 0, Amount = 0, TaxableAmt = 0, IgstAmt = 0, TotalAmount = 0, Quantity = 0;
                                            try
                                            {
                                                Quantity = Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                Quantity = 0;
                                            }
                                            try
                                            {
                                                sinrate = Convert.ToDecimal(dt.Rows[j][6].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                sinrate = 0;
                                            }
                                            try
                                            {
                                                sindisc = Convert.ToDecimal(dt.Rows[j][8].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                sindisc = 0;
                                            }
                                            try
                                            {
                                                sindiscper = Math.Round(sindisc * 100 / sinrate, 2, MidpointRounding.AwayFromZero);
                                            }
                                            catch (Exception)
                                            {
                                                sindiscper = 0;
                                            }
                                            try
                                            {
                                                sinspcldisc = Convert.ToDecimal(dt.Rows[j][17].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                sinspcldisc = 0;
                                            }
                                            try
                                            {
                                                sinspcldiscper = Math.Round(sinspcldisc * 100 / (sinrate - sindisc), 2, MidpointRounding.AwayFromZero);
                                            }
                                            catch (Exception)
                                            {
                                                sinspcldiscper = 0;
                                            }
                                            try
                                            {
                                                Amount = Convert.ToDecimal(dt.Rows[j][6].ToString());
                                                TaxableAmt = Convert.ToDecimal(dt.Rows[j][6].ToString());
                                            }
                                            catch (Exception)
                                            {
                                                Amount = 0;
                                                TaxableAmt = 0;
                                            }
                                            try
                                            {
                                                if (Convert.ToDecimal(dt.Rows[j][8].ToString()) != 0 && dt.Rows[j][8].ToString() != "")
                                                    TaxableAmt = TaxableAmt - (Convert.ToDecimal(dt.Rows[j][8].ToString()));
                                            }
                                            catch (Exception)
                                            {
                                                TaxableAmt = 0;
                                            }
                                            try
                                            {
                                                if (Convert.ToDecimal(dt.Rows[j][13].ToString()) != 0 && dt.Rows[j][13].ToString() != "")
                                                {
                                                    CgstAmt = Convert.ToDecimal(dt.Rows[j][13].ToString());//(TaxableAmt * Convert.ToDecimal(dt.Rows[j][29].ToString())) / 100;
                                                    SgstAmt = Convert.ToDecimal(dt.Rows[j][13].ToString());//(TaxableAmt * Convert.ToDecimal(dt.Rows[j][29].ToString())) / 100;
                                                    IgstAmt = 0;
                                                    TotalAmount = TaxableAmt + Convert.ToDecimal(dt.Rows[j][13].ToString()) + Convert.ToDecimal(dt.Rows[j][13].ToString());
                                                    SaleRate = TotalAmount / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                                }
                                                else if (Convert.ToDecimal(dt.Rows[j][15].ToString()) != 0 && dt.Rows[j][15].ToString() != "")
                                                {
                                                    CgstAmt = 0;
                                                    SgstAmt = 0;
                                                    IgstAmt = Convert.ToDecimal(dt.Rows[j][15].ToString());//(TaxableAmt * Convert.ToDecimal(dt.Rows[j][29].ToString())) / 100;
                                                    TotalAmount = TaxableAmt + IgstAmt;
                                                    SaleRate = TotalAmount / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                                }
                                                else
                                                {
                                                    CgstAmt = 0;
                                                    SgstAmt = 0;
                                                    IgstAmt = 0;
                                                    TotalAmount = TaxableAmt;
                                                    SaleRate = TotalAmount / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                CgstAmt = 0;
                                                SgstAmt = 0;
                                                IgstAmt = 0;
                                            }
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                            command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@BillNo", SqlDbType.Int).Value = custMaster.Id;
                                            command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = pinfo1.ProductName;//Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                            command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = pinfo1.ManufactureCompany;
                                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = pinfo1.Uniti;
                                            command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = pinfo1.Uniti;
                                            command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Quantity;
                                            command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                            command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "";
                                            command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = sinrate;//Convert.ToDecimal(dt.Rows[j][6].ToString()) / Convert.ToDecimal(dt.Rows[j][5].ToString());
                                            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;//Convert.ToDecimal(dt.Rows[j][6].ToString());
                                            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = sindiscper;
                                            command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                            command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = sgst;
                                            command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = SgstAmt;
                                            command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = sgst;
                                            command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = SgstAmt;
                                            command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Igst;
                                            command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = IgstAmt;
                                            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = TotalAmount;
                                            command.Parameters.Add("@HSNcode", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[j][42].ToString());
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = 0;
                                            command.Parameters.Add("@ProdSubtitle", SqlDbType.NVarChar).Value = Convert.ToString(dt.Rows[j][3].ToString());
                                            command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = SaleRate;
                                            command.Parameters.Add("@SpcDisc", SqlDbType.Decimal).Value = sinspcldiscper;// Convert.ToString(dt.Rows[i][3].ToString());
                                            command.Parameters.Add("@Cess", SqlDbType.Decimal).Value = DBNull.Value; //Convert.ToString(dt.Rows[i][3].ToString());
                                            command.Parameters.Add("@CessAmt", SqlDbType.Decimal).Value = DBNull.Value;//Convert.ToString(dt.Rows[i][3].ToString());
                                            if (pinfo1.BarcodeStatus == "Static Barcode")
                                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = dt.Rows[j][21].ToString();// Convert.ToString(dt.Rows[i][3].ToString());
                                            else if (pinfo1.BarcodeStatus == "Our Barcode")
                                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = dt.Rows[j][19].ToString();// Convert.ToString(dt.Rows[i][3].ToString());
                                            else
                                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = DBNull.Value;// Convert.ToString(dt.Rows[i][3].ToString());
                                            if (pinfo1.WithBatch == "Y")
                                            {
                                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = dt.Rows[j][27].ToString();//Convert.ToString(dt.Rows[i][3].ToString());
                                                command.Parameters.Add("@Expiry", SqlDbType.VarChar).Value = DBNull.Value;
                                            }
                                            else if (pinfo1.WithBatchAndExpiry == "Y")
                                            {
                                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = dt.Rows[j][27].ToString();//Convert.ToString(dt.Rows[i][3].ToString());
                                                command.Parameters.Add("@Expiry", SqlDbType.VarChar).Value = dt.Rows[j][7].ToString();//Convert.ToString(dt.Rows[i][3].ToString());
                                            }
                                            else
                                            {
                                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = DBNull.Value;//Convert.ToString(dt.Rows[i][3].ToString());
                                                command.Parameters.Add("@Expiry", SqlDbType.VarChar).Value = DBNull.Value;//Convert.ToString(dt.Rows[i][3].ToString());
                                            }
                                            command.ExecuteNonQuery();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        {
                                            db.CloseConnection();
                                        }
                                    }
                                    BillNum = Convert.ToString(dtexcel.Rows[i][colno].ToString());
                                    dt = new DataTable();
                                    dt = dtexcel.Clone();
                                    dt.Columns[6].DataType = typeof(decimal);
                                    dt.Columns[9].DataType = typeof(decimal);
                                    if (Convert.ToString(dtexcel.Rows[i][colno].ToString()) == BillNum)
                                    {
                                        try
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr.ItemArray = dtexcel.Rows[i].ItemArray;
                                            dt.Rows.Add(dr);
                                        }
                                        catch { }
                                    }
                                }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        A:
            { }
            return dtexcel;
        }

        public int maxSupid()
        {
            try
            {
                int n;
                SupplierRepository custrepo = new SupplierRepository();
                var mid = 0;

                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(custrepo.GetAll().Max(t => t.SupplierCode));
                else
                    mid = Convert.ToInt32(custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.SupplierCode));

                if (mid == null)
                    n = 1;
                else
                    n = Convert.ToInt32(mid) + 1;
                custrepo.Dispose();
                return n;
            }
            catch (Exception)
            { return 0; }
        }

        private void Excel()
        {
            try
            {
                saveFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog.Filter = "Execl files (*.xls or .xlsx)|.xls;*.xlsx";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "Export Excel File To";
            }
            catch (Exception)
            { MessageBox.Show("Error occurred while converting into Exel file."); }
        }

        public void SaveGroup(string gName)
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                var supp = unitrepo.GetAll().Where(t => t.GroupMasterName.ToLower() == gName.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (supp != null)
                { }
                else
                {
                    GroupMaster unitinfo = new GroupMaster();
                    unitinfo.GroupMasterName = gName;
                    unitinfo.Description = null;
                    unitinfo.CompId = CommonMethod.CompId;
                    unitrepo.Add(unitinfo);
                    unitrepo.Save();
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void SaveCompany(string companyName)
        {
            try
            {
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                ManuCompanyInfo BillId = new ManuCompanyInfo();
                if (CommonMethod.commProduct == true)
                    BillId = manufactureinforepo.GetAll().Where(t => t.CompanyName.Trim().ToLower() == companyName.Trim().ToLower()).FirstOrDefault();
                else
                    BillId = manufactureinforepo.GetAll().Where(t => t.CompanyName.Trim().ToLower() == companyName.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                { }
                else
                {
                    ManufactureCInfoRepository manufactureinforepo1 = new ManufactureCInfoRepository();
                    ManuCompanyInfo companyinfo = new ManuCompanyInfo();
                    int n = companymaxid();
                    companyinfo.CompanyId = n;
                    companyinfo.CompanyName = companyName.Trim();
                    companyinfo.CompanyAddress = null;
                    companyinfo.ContactNo = null;
                    companyinfo.StockAlert = 0;
                    companyinfo.CompId = CommonMethod.CompId;
                    manufactureinforepo1.Add(companyinfo);
                    manufactureinforepo1.Save();
                    manufactureinforepo1.Dispose();
                }
                manufactureinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public int companymaxid()
        {
            try
            {
                int n;
                var mid = 0;
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(manufactureinforepo.GetAll().Max(t => t.CompanyId));
                else
                    mid = Convert.ToInt32(manufactureinforepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.CompanyId));
                if (mid == null)
                    n = 1;
                else
                    n = Convert.ToInt32(mid) + 1;
                manufactureinforepo.Dispose();
                return n;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void SaveUnit(string unit)
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                AIOInventorySystem.Data.Model.Unit supp = new AIOInventorySystem.Data.Model.Unit();
                if (CommonMethod.commProduct == true)
                    supp = unitrepo.GetAll().Where(t => t.UnitName.ToLower() == unit.ToLower()).FirstOrDefault();
                else
                    supp = unitrepo.GetAll().Where(t => t.UnitName.ToLower() == unit.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (supp != null)
                { }
                else
                {
                    if (unit != "")
                    {
                        AIOInventorySystem.Data.Model.Unit unitinfo = new AIOInventorySystem.Data.Model.Unit();
                        int n = maxUnitid();
                        unitinfo.UnitID = n;
                        unitinfo.UnitName = unit;
                        unitinfo.Description = null;
                        unitinfo.UnitMarathi = null;
                        unitinfo.GSTUnitName = null;
                        unitinfo.CompId = CommonMethod.CompId;
                        unitinfo.StockAlertValue = 0;
                        unitrepo.Add(unitinfo);
                        unitrepo.Save();
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void UpdateMRP(string PName, string CName, string UName)
        {
            try
            {
                ProductRepository PrdtRepo = new ProductRepository();
                ProductInformation prdtData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    prdtData = PrdtRepo.GetAll().Where(t => t.ProductName == PName && t.ManufactureCompany == CName && t.Uniti == UName).FirstOrDefault();
                else
                    prdtData = PrdtRepo.GetAll().Where(t => t.ProductName == PName && t.ManufactureCompany == CName && t.Uniti == UName && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (prdtData != null)
                {
                    prdtData.Rate = Convert.ToDecimal(txtMRP.Text);
                    PrdtRepo.Edit(prdtData);
                    PrdtRepo.Save();
                }
                PrdtRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public int maxproductid()
        {
            try
            {
                ProductRepository productrepo = new ProductRepository();
                var mid = 0;
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(productrepo.GetAll().Max(t => t.ProductCode));
                else
                    mid = Convert.ToInt32(productrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.ProductCode));

                if (mid == null)
                    maxProductCode = 1;
                else
                    maxProductCode = Convert.ToInt32(mid) + 1;
                productrepo.Dispose();
            }
            catch (Exception)
            { }
            return maxProductCode;
        }

        public int maxUnitid()
        {
            try
            {
                int n;
                UnitRepository unitrepo = new UnitRepository();
                var mid = 0;

                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(unitrepo.GetAll().Max(t => t.UnitID));
                else
                    mid = Convert.ToInt32(unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.UnitID));

                if (mid == null)
                    n = 1;
                else
                    n = Convert.ToInt32(mid) + 1;
                unitrepo.Dispose();
                return n;
            }
            catch (Exception)
            { return 0; }
        }

        private void txt3LevelDisvPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt3LevelDisvPer.Text != "")
                {
                    if (Convert.ToDecimal(txt3LevelDisvPer.Text) <= 100)
                        getGstCalculation();
                    else
                    {
                        MessageBox.Show("Enter Vat Between 0 to 100 %.", "Warning");
                        txt3LevelDisvPer.Text = "0";
                        this.ActiveControl = txt3LevelDisvPer;
                    }
                }
                else
                {
                    txt3LevelDisvPer.Text = "0";
                    this.ActiveControl = txt3LevelDisvPer;
                }
            }
            catch (Exception)
            { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                pnl3LevelDisc.Visible = true;
                this.ActiveControl = txt3LevelDisvPer;
            }
            catch (Exception)
            { }
        }

        private void pnl3LevelDisc_Leave(object sender, EventArgs e)
        {
            try
            {
                pnl3LevelDisc.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void txt3LevelDiscAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt3LevelDiscAmt.Text != "" && txtamount.Text != "")
                {
                    if (Convert.ToDecimal(txt3LevelDiscAmt.Text) <= Convert.ToDecimal(txtamount.Text))
                    {
                        try
                        {
                            decimal total, damt = 0, Taxableamt = 0, Discamt = 0, SpcDiscAmt = 0;
                            Taxableamt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtpurchaseprice.Text);
                            if (txtpdiscount.Text != "")
                                Discamt = (Taxableamt * Convert.ToDecimal(txtpdiscount.Text)) / 100;
                            if (txtpSpcDisc.Text != "")
                                SpcDiscAmt = ((Taxableamt - Discamt) * Convert.ToDecimal(txtpSpcDisc.Text)) / 100;
                            total = Taxableamt - Discamt - SpcDiscAmt;
                            if (txt3LevelDiscAmt.Text != "")//&& Convert.ToDecimal(txtpdisAmt.Text) != 0)
                            {
                                damt = Convert.ToDecimal(txt3LevelDiscAmt.Text);
                                decimal disc = damt * 100 / total;
                                txt3LevelDisvPer.Text = Convert.ToString(Math.Round(disc, 5, MidpointRounding.AwayFromZero));
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Amount.", "Warning");
                        txt3LevelDiscAmt.Text = "0";
                        txt3LevelDiscAmt.Text = "0";
                        this.ActiveControl = txt3LevelDiscAmt;
                    }
                }
                else
                {
                    txt3LevelDiscAmt.Text = "0";
                    txt3LevelDiscAmt.Text = "0";
                    this.ActiveControl = txt3LevelDiscAmt;
                }
            }
            catch (Exception)
            { }
        }

        private void pnlQtySubtitle_Leave(object sender, EventArgs e)
        {
            try
            {
                pnlQtySubtitle.Visible = false;
                this.ActiveControl = txtFreeQty;
            }
            catch (Exception)
            { }
        }

        private void lblpSpcDis_Leave(object sender, EventArgs e)
        {
            try
            {
                if (lblpSpcDis.Text != "" && txtamount.Text != "")
                {
                    if (Convert.ToDecimal(lblpSpcDis.Text) <= Convert.ToDecimal(txtamount.Text))
                    {
                        try
                        {
                            decimal total, damt = 0, Taxableamt = 0, Discamt = 0;
                            Taxableamt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtpurchaseprice.Text);
                            if (txtpdiscount.Text != "")
                                Discamt = (Taxableamt * Convert.ToDecimal(txtpdiscount.Text)) / 100;
                            total = Taxableamt - Discamt;
                            if (lblpSpcDis.Text != "")//&& Convert.ToDecimal(txtpdisAmt.Text) != 0)
                            {
                                damt = Convert.ToDecimal(lblpSpcDis.Text);
                                decimal disc = damt * 100 / total;
                                txtpSpcDisc.Text = Convert.ToString(Math.Round(disc, 5, MidpointRounding.AwayFromZero));
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Amount.", "Warning");
                        txt3LevelDiscAmt.Text = "0";
                        txt3LevelDiscAmt.Text = "0";
                        this.ActiveControl = txt3LevelDiscAmt;
                    }
                }
                else
                {
                    txt3LevelDiscAmt.Text = "0";
                    txt3LevelDiscAmt.Text = "0";
                    this.ActiveControl = txt3LevelDiscAmt;
                }
            }
            catch (Exception)
            { }
        }

        private void lblpSpcDis_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = linkLabel2;
            }
            catch (Exception)
            { }
        }

        private void txtMRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
            }
            catch (Exception)
            { }
        }

        private void txt3LevelDisvPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = txt3LevelDiscAmt;
            }
            catch (Exception)
            { }
        }

        private void txt3LevelDiscAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = cmbsgst;
            }
            catch (Exception)
            { }
        }

        private void btnSuppForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplierInformation compinfo = new frmSupplierInformation();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                fillsupplier();
                this.ActiveControl = cmbsuppliername;
            }
            catch (Exception)
            { }
        }

        private void txtFreeQty_Enter(object sender, EventArgs e)
        {
            refreshFlag = false;
        }

        private void cmbcomanyname_MouseClick(object sender, MouseEventArgs e)
        {
            prefixSelected = false;
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                //if (e.ColumnIndex == 21)
                {
                    if (GvProductInfo.Rows.Count != 1 && btnSave.Enabled == false)
                    {
                        decimal srqty1 = 0, sqty1 = 0;
                        string pname1 = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                        string cname1 = GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value.ToString();
                        string uname1 = GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString();
                        decimal txtqty = 0;
                        if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value))
                        {
                            try
                            {
                                ProductRepository productrepo = new ProductRepository();
                                ProductInformation productdata = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == pname1 && t.ManufactureCompany == cname1 && t.Uniti == uname1).FirstOrDefault();
                                else
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == pname1 && t.ManufactureCompany == cname1 && t.Uniti == uname1 && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                txtqty = Convert.ToDecimal(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                            }
                            catch (Exception)
                            {
                                txtqty = 0;
                            }
                        }
                        else
                            txtqty = Convert.ToDecimal(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                        PReturnDetailRepository SaleRDRepo = new PReturnDetailRepository();
                        CustomerBillDetailRepository SaleRepo = new CustomerBillDetailRepository();
                        try
                        {
                            srqty1 = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.PurchaseReturnMaster.PurchaseReturnNo == Convert.ToInt32(txtporderno.Text) && t.ProductName == pname1 && t.CompanyName == cname1 && t.Unit == uname1 && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault().Quantity);
                        }
                        catch (Exception)
                        {
                            srqty1 = 0;
                        }
                        try
                        {
                            sqty1 = Convert.ToDecimal(SaleRepo.GetAll().Where(t => t.ProductId == pname1 && t.CompanyName == cname1 && t.Unit == uname1 && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault().Quantity);
                        }
                        catch (Exception)
                        {
                            sqty1 = 0;
                        }
                        if (srqty1 != 0)
                        {
                            if (srqty1 == txtqty)
                            {
                                MessageBox.Show("This Product is returned can't remove.", "Warning");
                                goto w;
                            }
                            else if (srqty1 != txtqty)
                            {
                                MessageBox.Show("Some quantity of this Product is returned can't remove.", "Warning");
                                goto w;
                            }
                        }
                        else if (sqty1 != 0)
                        {
                            decimal remqty = 0;
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = pname1;
                                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cname1;
                                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = uname1;
                                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                command.CommandTimeout = 300;
                                SqlDataReader reader = command.ExecuteReader();
                                while (reader.Read())
                                {
                                    remqty = Convert.ToDecimal(reader.GetSqlDecimal(11).ToString());
                                }
                                db.CloseConnection();
                                reader.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            if (sqty1 > remqty || remqty < txtqty)
                            {
                                MessageBox.Show("This Product is used can't remove.", "Warning");
                                goto w;
                            }
                        }
                        else
                            goto t;
                    t:
                        {
                            if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                            {
                                int index = e.RowIndex;
                                int qdid = Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value);
                                BatchRepository batchrepo = new BatchRepository();
                                var batch = batchrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Unit == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) && t.BatchNo == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value) && t.ExpiryDate.Value.ToString("dd/MM/yyyy") == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value) && t.PorderNo == qdid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                PReturnDetailRepository PRDRepo = new PReturnDetailRepository();
                                PurchaseReturnDetail PRDData = new PurchaseReturnDetail();
                                PRDData = PRDRepo.GetAll().Where(t => t.POID == qdid).FirstOrDefault();
                                if (batch == null && PRDData == null)
                                {
                                    deleteid.Add(qdid);
                                    GvProductInfo.Rows.Remove(GvProductInfo.Rows[index]);
                                    totalamt = 0;
                                    totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                                    totalSGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SGSTAmt"].Value));
                                    totalCGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CGSTAmt"].Value));
                                    totalIGst = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["IGSTAmt"].Value));
                                    totalGST = totalCGst + totalIGst + totalSGst;
                                    txtTotalGST.Text = Convert.ToString(totalGST);
                                    txtTotalAmt.Text = Convert.ToString(totalamt);
                                    getGstCalculation();
                                    totalamt = 0;
                                    colIndex = -1;
                                    txtpname.Text = "";
                                    txtQuantity.Text = "0";
                                    txtamount.Text = "";
                                    txtpurchaseprice.Text = "";
                                    txtpdiscount.Text = "0";
                                    txtpTotalAmt.Text = "";
                                    fillgst();
                                    lblIGst.Text = "0";
                                    lblCGst.Text = "0";
                                    lblpvat.Text = "0";
                                    txtSaleRate.Text = "0";
                                    txtstaticBarcode.Text = "";
                                    txtBatchNo.Text = "";
                                    dtpExpiry.Value = DateTime.Now.Date;
                                    txtSaleRate.Enabled = false;
                                    txtstaticBarcode.Enabled = false;
                                    CalculateNetAmt();
                                }
                                else if (batch != null && PRDData == null)
                                    MessageBox.Show("You can not Delete this Record." + Environment.NewLine + "You sold from this batch.", "Warning");
                                else if (PRDData != null && batch == null)
                                    MessageBox.Show("You can not Delete this Record." + Environment.NewLine + "You Returned some quantity from this batch.", "Warning");
                                else
                                    MessageBox.Show("You can not Delete this Record." + Environment.NewLine + "You Returned or sold this batch.", "Warning");
                                PRDRepo.Dispose();
                            }
                        }
                    w: { }
                    }
                    else if (btnSave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            int index = e.RowIndex;
                            int qdid = Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value);
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[index]);
                            totalamt = 0;
                            totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                            txtTotalAmt.Text = Convert.ToString(totalamt);
                            getGstCalculation();
                            totalamt = 0;
                            colIndex = -1;
                            txtpname.Text = "";
                            txtQuantity.Text = "0";
                            txtamount.Text = "";
                            txtpurchaseprice.Text = "";
                            txtpdiscount.Text = "0";
                            txtpTotalAmt.Text = "";
                            fillgst();
                            lblIGst.Text = "0";
                            lblCGst.Text = "0";
                            lblpvat.Text = "0";
                            txtSaleRate.Text = "0";
                            txtstaticBarcode.Text = "";
                            txtBatchNo.Text = "";
                            dtpExpiry.Value = DateTime.Now.Date;
                            txtSaleRate.Enabled = false;
                            txtstaticBarcode.Enabled = false;
                            CalculateNetAmt();
                        }
                    }
                    else
                        MessageBox.Show("This Invoice has only 1 product." + Environment.NewLine + "You have to delete full Order.", "Warning");
                }
                else if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Print Barcode")
                {
                    try
                    {
                        string input = "", inputcopy = "";
                        int rownum = 0, copyno = 1, colno = 0;
                        input = Microsoft.VisualBasic.Interaction.InputBox("Enter row number from where you want to print " + Environment.NewLine + " from 1 to 12 ", "Page Setup", "" + rownum + "", 300, 250);
                        rownum = Convert.ToInt32(input);

                        input = Microsoft.VisualBasic.Interaction.InputBox("Enter column number from where you want to print " + Environment.NewLine + " from 1 to 4 ", "Page Setup", "" + colno + "", 300, 250);
                        colno = Convert.ToInt32(input);

                        inputcopy = Microsoft.VisualBasic.Interaction.InputBox("Enter number of copies you want to print", "Copies", "" + copyno + "", 300, 250);
                        copyno = Convert.ToInt32(inputcopy);

                        int lblavail = 0, totqty = 0, qtytoprint = 0;
                        if (colno > 1)
                        {
                            lblavail = 4 - (colno - 1);
                            totqty = Convert.ToInt32(totalqty);
                            qtytoprint = (totqty * copyno);
                        }
                        if (qtytoprint > lblavail)
                        {
                            MessageBox.Show("You have selected more labels to print than available labels,\n Please select another order.", "Printing Barcode");
                            this.ActiveControl = btnPrintBarcode;
                        }
                        else
                        {
                            decimal ponumber = 0;
                            if (purmasterid != 0)
                                ponumber = Convert.ToDecimal(purmasterid);
                            else if (PurchaeId != 0)
                                ponumber = Convert.ToDecimal(PurchaeId);
                            string flag = "Purchase";
                            if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                            {
                                RptBarcode barrpt = new RptBarcode(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value), ponumber, rownum, copyno, colno, flag);
                                barrpt.ShowDialog();
                            }
                        }
                    }
                    catch (Exception)
                    { }
                }
                else if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Select to Print")
                {
                }
                else
                {
                    ProductRepository ProdRepo = new ProductRepository();
                    ProductInformation ProdData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        ProdData = ProdRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value)).FirstOrDefault();
                    else
                        ProdData = ProdRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    PReturnMasterRepository SaleRMRepo = new PReturnMasterRepository();
                    PurchaseReturnMaster SaleRMData = new PurchaseReturnMaster();
                    SaleRMData = SaleRMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SaleRMData == null)
                    {
                        colIndex = e.RowIndex;
                        fillgst();
                        txtpdiscount.Text = "0";
                        txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                        cmbUnit.Items.Clear();
                        cmbUnit.DisplayMember = "Text";
                        cmbUnit.ValueMember = "Value";
                        cmbUnit.Items.Add(new { Text = ProdData.Uniti, Value = ProdData.Uniti });
                        if (ProdData.AlternateUnit != null && ProdData.AlternateUnit != "")
                            cmbUnit.Items.Add(new { Text = ProdData.AlternateUnit, Value = ProdData.AlternateUnit });
                        if (ProdData.TripleUnit != null && ProdData.TripleUnit != "")
                            cmbUnit.Items.Add(new { Text = ProdData.TripleUnit, Value = ProdData.TripleUnit });
                        cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value);
                        txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                        txtFreeQty.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["FrQty"].Value);
                        txtpurchaseprice.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Price"].Value);
                        txtamount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Amount"].Value);
                        txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value);
                        txtpSpcDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SpDisc"].Value);
                        cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGST"].Value);
                        lblpvat.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGSTAmt"].Value);
                        cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGST"].Value);
                        lblCGst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGSTAmt"].Value);
                        cmbigst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                        lblIGst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGSTAmt"].Value);
                        txtpCess.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Cess"].Value);
                        txtTCSPer.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TCSPer"].Value);
                        txtTCSAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TCSAmt"].Value);
                        txtpTotalAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TotalAmount"].Value);
                        if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["MandatoryCodeSeries"].Value) == "Y")
                        {
                            flgmandatory = true;
                            ismandatory = true;
                            dtgvcode.Rows.Clear();
                            dtgvcode.Rows.Add();
                            dtgvcode.Rows[0].Cells[0].Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Code"].Value);
                        }
                        else
                        {
                            ismandatory = false;
                            flgmandatory = false;
                        }
                        updateBarcode = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Barcode"].Value);
                        txtSaleRate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleRate"].Value);
                        txtstaticBarcode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["StaticBarcode"].Value);
                        txtBatchNo.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value);
                        if (GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value == null || Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value) == "")
                            dtpExpiry.Value = DateTime.Now.Date;
                        else
                        {
                            DateTime date = DateTime.Parse(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value.ToString(), new CultureInfo("en-GB")); // returns 09/25/2011
                            string d2 = date.ToString("dd/MM/yyyy"); //should return 25/09/2011
                            dtpExpiry.Value = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //dtpExpiry.Value = DateTime.ParseExact(GvProductInfo.Rows[e.RowIndex].Cells[28].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        if (GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value == null || Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value) == "")
                            txt3LevelDisvPer.Text = "0";
                        else
                            txt3LevelDisvPer.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value);
                        if (GvProductInfo.Rows[e.RowIndex].Cells["QuantitySubtitle"].Value == null || Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["QuantitySubtitle"].Value) == "")
                            txtQuantitySubtitle.Text = "0";
                        else
                            txtQuantitySubtitle.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["QuantitySubtitle"].Value);
                        if (ProdData.BarcodeStatus == "Static Barcode" || ProdData.BarcodeStatus == "Our Barcode")
                            txtSaleRate.Enabled = true;
                    }
                    else
                    {
                        var SaleRDData = SaleRMData.PurchaseReturnDetails;
                        foreach (var item in SaleRDData)
                        {
                            decimal txtqty = 0;
                            if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value))
                            {
                                ProductRepository productrepo = new ProductRepository();
                                ProductInformation productdata = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString()).FirstOrDefault();
                                else
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                txtqty = Convert.ToDecimal(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                            }
                            else
                                txtqty = Convert.ToDecimal(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                            if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) == item.ProductName && Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) == item.CompanyName && Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) == item.Unit && txtqty == Convert.ToDecimal(item.Quantity))
                            {
                                colIndex = -1;
                                lblpvat.Text = "0";
                                txtQuantity.Text = "0";
                                txtpdiscount.Text = "0";
                                txtpTotalAmt.Text = "0";
                                fillgst();
                                lblCGst.Text = "0";
                                lblIGst.Text = "0";
                                goto s;
                            }
                        }
                    }
                    colIndex = e.RowIndex;
                    fillgst();
                    txtpdiscount.Text = "0";
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = ProdData.Uniti, Value = ProdData.Uniti });
                    if (ProdData.AlternateUnit != null && ProdData.AlternateUnit != "")
                        cmbUnit.Items.Add(new { Text = ProdData.AlternateUnit, Value = ProdData.AlternateUnit });
                    if (ProdData.TripleUnit != null && ProdData.TripleUnit != "")
                        cmbUnit.Items.Add(new { Text = ProdData.TripleUnit, Value = ProdData.TripleUnit });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value);
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                    txtFreeQty.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["FrQty"].Value);
                    txtpurchaseprice.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Price"].Value);
                    txtamount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Amount"].Value);
                    txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value);
                    txtpSpcDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SpDisc"].Value);
                    cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGST"].Value);
                    lblpvat.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGSTAmt"].Value);
                    cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGST"].Value);
                    lblCGst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGSTAmt"].Value);
                    cmbigst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                    lblIGst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGSTAmt"].Value);
                    txtpCess.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Cess"].Value);
                    txtTCSPer.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TCSPer"].Value);
                    txtTCSAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TCSAmt"].Value);
                    txtpTotalAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TotalAmount"].Value);
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["MandatoryCodeSeries"].Value) == "Y")
                    {
                        flgmandatory = true;
                        ismandatory = true;
                        dtgvcode.Rows.Clear();
                        dtgvcode.Rows.Add();
                        dtgvcode.Rows[0].Cells[0].Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Code"].Value);
                    }
                    else
                    {
                        ismandatory = false;
                        flgmandatory = false;
                    }
                    updateBarcode = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Barcode"].Value);
                    txtSaleRate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleRate"].Value);
                    txtstaticBarcode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["StaticBarcode"].Value);
                    txtBatchNo.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value);
                    if (GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value == null || Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value) == "")
                        dtpExpiry.Value = DateTime.Now.Date;
                    else
                    {
                        DateTime date = DateTime.Parse(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value.ToString(), new CultureInfo("en-GB")); // returns 09/25/2011
                        string d2 = date.ToString("dd/MM/yyyy"); //should return 25/09/2011
                        dtpExpiry.Value = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //dtpExpiry.Value = DateTime.ParseExact(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value == null || Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value) == "")
                        txt3LevelDisvPer.Text = "0";
                    else
                        txt3LevelDisvPer.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value);
                    if (GvProductInfo.Rows[e.RowIndex].Cells["QuantitySubtitle"].Value == null || Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["QuantitySubtitle"].Value) == "")
                        txtQuantitySubtitle.Text = "0";
                    else
                        txtQuantitySubtitle.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["QuantitySubtitle"].Value);
                    if (ProdData.BarcodeStatus == "Static Barcode" || ProdData.BarcodeStatus == "Our Barcode")
                    {
                        txtSaleRate.Enabled = true;
                    }
                    if (colIndex != -1) // added on integration 27032018
                        txtBatchNo.Enabled = false;
                    this.ActiveControl = txtpname;
                    SaleRMRepo.Dispose();
                }
            s: { }
            }
            catch (Exception)
            { }
        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {
            getGstCalculation();
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtstaticBarcode.Enabled == true)
                        this.ActiveControl = txtstaticBarcode;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePurchaseBill();
            pnlIntegratedDtls.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Delete this Bill ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", alternateunit = "", CDType = "", proname = "", unit1 = "", company1 = "", batchno = "",
                        expirydate = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, freeqty = 0,
                        remRawQty = 0;
                    int cntProduct = 0, rawqty = 0;

                    PurchaseMasterRepository purmastRepo = new PurchaseMasterRepository();
                    var purData = purmastRepo.GetAll().Where(t => t.Id == PurchaeId && t.PorderNo == pno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (purData != null)
                    {
                        SupplierRemainingPaymentRepository suppRemRepo = new SupplierRemainingPaymentRepository();
                        var suppremData = suppRemRepo.GetAll().Where(t => t.BillId == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == "Receipt" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                        var suppremData1 = suppRemRepo.GetAll().Where(t => t.BillId == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == "Purchase" && t.PaidAmt != 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && (t.PaymentMode == "Cash" || t.PaymentMode == "Cheque")).FirstOrDefault();

                        SuppCreditDebitNoteMasterRepository suppCDRepo = new SuppCreditDebitNoteMasterRepository();
                        var suppCDData = suppCDRepo.GetAll().Where(t => t.PorderNo == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.PorderDate.Value.ToShortDateString() == purData.PorderDate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                        PReturnMasterRepository PurRetRepo = new PReturnMasterRepository();
                        var PRData = PurRetRepo.GetAll().Where(t => t.PorderNo == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.PorderDate.Value.ToShortDateString() == purData.PorderDate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                        PurchaseDetailRepository detailRepo1 = new PurchaseDetailRepository();
                        List<PurchaseDetail> detailData1 = detailRepo1.GetAll().Where(t => t.PorderNo == PurchaeId && (t.BatchNo != null || t.ExpireDate != null) && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                        if (detailData1.Count != 0)
                        {

                            foreach (var pdata in detailData1)
                            {
                                proname = pdata.ProductName;
                                unit1 = pdata.Unit;
                                company1 = pdata.CompanyName;
                                batchno = pdata.BatchNo;

                                if (pdata.ExpireDate != null)
                                    expirydate = Convert.ToString(pdata.ExpireDate.Value.ToShortDateString());
                                else
                                    expirydate = "";
                                getProductType(proname, company1, unit1);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                    List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                    if (productentrytype == 1)
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno && t.Expiry == expirydate)).ToList();
                                    else
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno)).ToList();
                                    if (custdetailData.Count != 0)
                                    {
                                        foreach (var cdata in custdetailData)
                                        {
                                            cntProduct++;
                                        }
                                    }
                                }
                                else if (productentrytype == 6)
                                {
                                    CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                    List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                    if (productentrytype == 1)
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                    else
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                    if (custdetailData.Count != 0)
                                    {
                                        foreach (var cdata in custdetailData)
                                        {
                                            cntProduct++;
                                        }
                                    }
                                }
                                else
                                {
                                    DataTable dtAssembly = db.GetTable("Select * from tblAssemblyProductDetails where ProductName='" + proname + "' and CompanyName='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                    if (dtAssembly.Rows.Count > 0)
                                    {
                                        //                                                  0                   1                 2                     3                          4                   
                                        DataTable dtStock = db.GetTable("Select isnull(PurchaseQty,0), isnull(SaleQty,0),isnull(PurReturnQty,0),isnull(SaleReturnQty,0),isnull(OpeningQty,0) from Stock where ProductName='" + proname + "' and MfgCompany='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                        if (dtStock.Rows.Count > 0)
                                        {
                                            remRawQty = Convert.ToDecimal(dtStock.Rows[0][4].ToString()) + Convert.ToDecimal(dtStock.Rows[0][0].ToString()) - Convert.ToDecimal(dtStock.Rows[0][1].ToString()) - Convert.ToDecimal(dtStock.Rows[0][2].ToString()) + Convert.ToDecimal(dtStock.Rows[0][3].ToString()) - Convert.ToDecimal(pdata.Quantity);
                                            if (remRawQty < 0)
                                                rawqty = 1;
                                        }
                                    }
                                }
                            }
                        }

                        if (suppremData != null || suppremData1 != null || suppCDData != null || PRData != null || cntProduct != 0 || rawqty == 1)
                        {
                            if (suppremData != null || suppremData1 != null)
                            {
                                MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this bill was Paid.", "Warning");
                                this.ActiveControl = btnUpdate;
                            }
                            else if (suppCDData != null)
                            {
                                CDType = suppCDData.CDType;
                                MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this bill was " + CDType + ".", "Warning");
                                this.ActiveControl = btnUpdate;
                            }
                            else if (PRData != null)
                            {
                                MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this bill was purchase return.", "Warning");
                                this.ActiveControl = btnUpdate;
                            }
                            else if (cntProduct != 0)
                            {
                                MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this bill is used in batch / assembly product in sale bill.", "Warning");
                                this.ActiveControl = btnUpdate;
                            }
                            else if (rawqty == 1)
                            {
                                MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this raw products used for " + Environment.NewLine + "assembly product in sale bill.", "Warning");
                                this.ActiveControl = btnUpdate;
                            }
                        }
                        else
                        {
                            int purchaseId = purmastRepo.GetAll().Where(t => t.Id == PurchaeId && t.PorderNo == pno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                            // add details for history table
                            try
                            {
                                DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                DeleteHistoryTable historydata = new DeleteHistoryTable();
                                historydata.DeleteType = "Purchase Bill";
                                historydata.BillNo = Convert.ToString(purData.PorderNo);
                                historydata.SupplierOrCustomerName = Convert.ToString(purData.Suppliername);
                                historydata.BillDate = Convert.ToDateTime(purData.PorderDate);
                                historydata.CurrentDate = DateTime.Now.Date;
                                historydata.Amount = Convert.ToDecimal(purData.NetAmount);
                                historydata.CompId = CommonMethod.CompId;
                                historydata.TransactionYear = CommonMethod.TransactionYear;
                                historyRepo.Add(historydata);
                                historyRepo.Save();
                            }
                            catch (Exception) { }
                            RecieptChallanMasterRepository receiptRepo = new RecieptChallanMasterRepository();
                            var receiptData = receiptRepo.GetAll().Where(t => t.POrderNo == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (receiptData != null)
                            {
                                int receiptId = receiptData.Id;
                                // add details for history table
                                try
                                {
                                    DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                    DeleteHistoryTable historydata = new DeleteHistoryTable();
                                    historydata.DeleteType = "Receipt Challan";
                                    historydata.BillNo = Convert.ToString(receiptData.RecieptChallanNo);
                                    historydata.SupplierOrCustomerName = Convert.ToString(receiptData.SupplierName);
                                    historydata.Amount = Convert.ToDecimal(0);
                                    historydata.BillDate = Convert.ToDateTime(receiptData.RecieptChallanDate);
                                    historydata.CurrentDate = DateTime.Now.Date;
                                    historydata.CompId = CommonMethod.CompId;
                                    historydata.TransactionYear = CommonMethod.TransactionYear;
                                    historyRepo.Add(historydata);
                                    historyRepo.Save();
                                }
                                catch (Exception) { }

                                DataTable dtRCM = db.GetTable("delete from RecieptChalanDetail where RecieptChallanNo='" + receiptId + "' ");
                                DataTable dtRCD = db.GetTable("delete from RecieptChallanMaster where Id='" + receiptId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "' ");
                                dtRCM.Dispose();
                                dtRCD.Dispose();
                            }

                            SupplierRemainingPaymentRepository SuppRemaningRepo = new SupplierRemainingPaymentRepository();
                            var suppremaingData = SuppRemaningRepo.GetAll().Where(t => t.BillId == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == "Purchase" && t.PaidAmt == 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (suppremaingData != null)
                            {
                                decimal decremamt = 0;
                                int ReceiptId = SuppRemaningRepo.GetAll().Where(t => t.BillId == pno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == "Purchase" && t.PaidAmt == 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                                SupplierRemainingPaymentRepository SupplierRemRepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment SupplierRemdata = SupplierRemRepo.GetById(ReceiptId);
                                SupplierRemRepo.Remove(SupplierRemdata);
                                SupplierRemRepo.Save();

                                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remdata1 = SupplierRemRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id < ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                if (remdata1 != null)
                                    decremamt = Convert.ToDecimal(remdata1.RemainingAmt);
                                else
                                    decremamt = 0;
                                SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id > ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                remainingpayrepo1.Dispose();
                                remainingpayrepo2.Dispose();
                            }
                            PurchaseDetailRepository PRdetailRepo = new PurchaseDetailRepository();
                            List<PurchaseDetail> detailList = PRdetailRepo.GetAll().Where(t => t.PorderNo == purchaseId).ToList();
                            if (detailList.Count != 0)
                            {
                                foreach (var item1 in detailList)
                                {
                                    int detailId = 0, updateStockId = 0;
                                    decimal repackQty = 0, rate = 0;
                                    productname = item1.ProductName;
                                    company = item1.CompanyName;
                                    unit = item1.Unit;
                                    alternateunit = item1.PurchaseUnit;
                                    detailId = item1.Id;
                                    Quantity = Convert.ToDecimal(item1.Quantity);
                                    freeqty = Convert.ToDecimal(item1.FreeQty);
                                    ProductRepository productRepo = new ProductRepository();
                                    ProductInformation proddata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        proddata = productRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                    else
                                        proddata = productRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    RepackDetailRepository repackDRepo = new RepackDetailRepository();
                                    repackQty = Convert.ToDecimal(repackDRepo.GetAll().Where(t => t.ProductNo == proddata.ID && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Qty));
                                    DataTable dt = db.GetTable("Delete from PurchaseDetail where PorderNo='" + purchaseId + "' and Id='" + detailId + "'");
                                    if (productentrytype == 9)
                                    { }
                                    else if (productentrytype == 6)
                                    { }
                                    else
                                    {
                                        StockRepository stockRepo = new StockRepository();
                                        var stockdata = stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (stockdata != null)
                                        {
                                            newsqty = 0; sqty = 0; pqty = 0; prqty = 0; newrqty = 0; rqty = 0; srqty = 0; dqty = 0; oqty = 0;
                                            sqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleQty);
                                            pqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurchaseQty);
                                            rqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().RemQty);
                                            oqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().OpeningQty);
                                            dqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().DeadQty);
                                            prqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurReturnQty);
                                            srqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleReturnQty);
                                            newsqty = (pqty + repackQty) - Quantity - freeqty;
                                            newrqty = oqty + newsqty - prqty - sqty + srqty - dqty;
                                            updateStockId = stockdata.Id;
                                        }

                                        sdata = stockRepo.GetById(stockdata.Id);
                                        if (sdata != null)
                                        {
                                            sdata.ProductId = stockdata.ProductId;
                                            sdata.ProductName = productname;
                                            sdata.MfgCompany = company;
                                            sdata.Unit = unit;
                                            sdata.OpeningQty = stockdata.OpeningQty;
                                            sdata.DeadQty = stockdata.DeadQty;
                                            sdata.PurchaseQty = Convert.ToDecimal(newsqty);
                                            sdata.SaleQty = Convert.ToDecimal(sqty);
                                            sdata.RemQty = Convert.ToDecimal(newrqty);
                                            sdata.SaleReturnQty = stockdata.SaleReturnQty;
                                            sdata.PurReturnQty = stockdata.PurReturnQty;
                                            SettingRepository settRepo = new SettingRepository();
                                            Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            if (settData.StockOn == "Purchase Rate")
                                            {
                                                try
                                                {
                                                    db.connect();
                                                    SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                                    command.CommandType = CommandType.StoredProcedure;

                                                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productname;
                                                    command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                                    SqlDataReader reader = command.ExecuteReader();
                                                    DataTable dtRate = new DataTable();
                                                    dtRate.Load(reader);
                                                    rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                                    db.CloseConnection();
                                                    dtRate.Dispose();
                                                }
                                                catch (Exception)
                                                { db.CloseConnection(); }
                                            }
                                            if (settData.StockOn == "Sale Rate")
                                            {
                                                try
                                                {
                                                    db.connect();
                                                    SqlCommand command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                                                    command.CommandType = CommandType.StoredProcedure;

                                                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productname;
                                                    command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                                    SqlDataReader reader = command.ExecuteReader();
                                                    DataTable dtRate = new DataTable();
                                                    dtRate.Load(reader);
                                                    rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                                    db.CloseConnection();
                                                    dtRate.Dispose();
                                                }
                                                catch (Exception)
                                                { db.CloseConnection(); }
                                            }
                                            sdata.ProductRate = Convert.ToDecimal(rate);
                                            sdata.CompId = CommonMethod.CompId;
                                            sdata.TransactionYear = CommonMethod.TransactionYear;
                                            sdata.Id = updateStockId;
                                            stockRepo.Edit(sdata);
                                            stockRepo.Save();
                                        }
                                        stockRepo.Dispose();
                                        dt.Dispose();
                                    }
                                    PRdetailRepo.Dispose();
                                }
                            }
                            db.connect();
                            DataTable dt1 = db.GetTable("Delete from PurchaseMaster where Id='" + purchaseId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            dt1.Dispose();
                            db.CloseConnection();
                            GvProductInfo.Rows.Clear();
                            clear();
                            this.ActiveControl = btnNew;
                            btnDelete.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnBankPnlClose_Click(object sender, EventArgs e)
        {
            pnlBank.Visible = false;
        }

        private void dtgvcode_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (txtpname.Text != "" && dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    SqlDataAdapter da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int intflag = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (e.RowIndex != i)
                        {
                            if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != dt.Rows[i][0].ToString())
                                intflag = 1;
                            else
                            {
                                intflag = 0;
                                goto a;
                            }
                        }
                    }
                a: { }
                    if (intflag == 0)
                    {
                        MessageBox.Show("This Serial No is already Saved.", "Warning");
                        dtgvcode.Visible = true;
                        dtgvcode.ClearSelection();
                        dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        this.ActiveControl = dtgvcode;
                        dtgvcode.CurrentCell = dtgvcode[e.ColumnIndex, e.RowIndex];
                        dtgvcode.BeginEdit(true);
                        goto b;
                    }
                    if (dtgvcode.RowCount > 0)
                    {
                        for (int i = 0; i < dtgvcode.RowCount; i++)
                        {
                            if (e.RowIndex != i)
                            {
                                if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == dtgvcode.Rows[i].Cells[e.ColumnIndex].Value.ToString())
                                {
                                    MessageBox.Show("This Serial No is already Added to List.", "Warning");
                                    dtgvcode.Visible = true;
                                    dtgvcode.ClearSelection();
                                    dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                                    this.ActiveControl = dtgvcode;
                                    dtgvcode.CurrentCell = dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                    dtgvcode.CurrentCell.Selected = true;
                                    dtgvcode.BeginEdit(true);
                                    break;
                                }
                            }
                        }
                    }
                }
            b: { }
                try
                {
                    bool findBlank = false;//20/6/2018 by Dhanashri
                    int currentRow_code = 0;
                    if (dtgvcode.Visible == true)
                    {
                        if (dtgvcode.RowCount > 0)
                        {
                            for (int i = 0; i < dtgvcode.RowCount; i++)
                            {
                                if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                                {
                                    findBlank = true;
                                    currentRow_code = i;
                                }
                            }
                            if (findBlank == true)
                            { }
                            else
                            {
                                dtgvcode.Visible = false;
                                this.ActiveControl = txtFreeQty;
                            }
                        }
                        else
                        {
                            dtgvcode.Visible = false;
                            this.ActiveControl = txtFreeQty;
                        }
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        private void dtpExpiry_Enter(object sender, EventArgs e)
        {
            VisibleBatchPanel = "BE";
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                this.ActiveControl = txtQuantity;
                pnlIntegratedDtls.Visible = false;
            }
        }

        private void dtpExpiry_Leave(object sender, EventArgs e)
        {
            VisibleBatchPanel = "";
        }

        private void btnIntegratedPanelClose_Click(object sender, EventArgs e)
        {
            try
            {
                showsize = false;
                if (dtgvSize.Rows.Count > 0 && productentrytype == 3)
                {
                    decimal totalqty = dtgvSize.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells["Qtyg"].Value));
                    if (Convert.ToDecimal(txtQuantity.Text) != totalqty)
                    {
                        MessageBox.Show("Quantity is not distributed properly", "Warning");
                        this.ActiveControl = dtgvSize;
                        pnlIntegratedDtls.Visible = true;
                    }
                }
                else
                    pnlIntegratedDtls.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void txtsizeto_Leave(object sender, EventArgs e)
        {
            try
            {
                int gvcnt = 0;
                if (txtsizefrom.Text != "" && txtsizeto.Text != "")
                {
                    for (int cnt = 0; cnt < dtgvSize.Rows.Count; cnt++)
                    {
                        if (Convert.ToString(dtgvSize.Rows[cnt].Cells["Qtyg"].Value) != "0" || Convert.ToString(dtgvSize.Rows[cnt].Cells["SRateg"].Value) != "0")
                        {
                            gvcnt++;
                        }
                    }
                    if (gvcnt != 0)
                    { }
                    else
                    {
                        if (colIndex == -1)
                        {
                            string[] CharSizes = { "S", "M", "L", "XL", "XXL", "3XL", "4XL", "5XL", "6XL", "7XL", "8XL", "9XL", "10XL", "11XL", "12XL", "13XL", "14XL", "15XL" };
                            int[] intSizes = { 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70, 72, 74, 76, 78, 80, 82, 84, 86, 88, 90, 92, 94, 96, 98 };
                            if (CharSizes.Contains(txtsizefrom.Text.Trim().ToUpper()) && CharSizes.Contains(txtsizeto.Text.Trim().ToUpper()))
                            {
                                int size = 36, sizefrom = 0, sizeto = 0;
                                for (int i = 0; i < CharSizes.Length; i++)
                                {
                                    string comp1 = CharSizes[i];
                                    if (comp1 == txtsizefrom.Text.Trim().ToUpper())
                                        sizefrom = size;
                                    if (comp1 == txtsizeto.Text.Trim().ToUpper())
                                        sizeto = size;
                                    size = size + 2;
                                }
                                int sizecount = (sizeto - sizefrom) / 2;
                                List<string> s = new List<string>();
                                for (int i = sizefrom; i <= sizeto; i = i + 2)
                                {
                                    int j = Array.IndexOf(intSizes, i);
                                    if (i == intSizes[j])
                                        s.Add(CharSizes[j]);
                                }
                                dtgvSize.Visible = true;
                                dtgvSize.Rows.Clear();
                                int sizeGap = 0, k = 0, sizeGap1 = 0;
                                sizeGap = Convert.ToInt32(txtsizegap.Text);
                                sizeGap1 = Convert.ToInt32(txtsizegap.Text);
                                if (sizeGap != 0)
                                {
                                    for (int i = 0; i <= sizecount; i++)
                                    {
                                        if (s[i] == txtsizefrom.Text.Trim().ToUpper())
                                        {
                                            dtgvSize.Rows.Add();
                                            dtgvSize.Rows[k].Cells["Sizeg"].Value = s[i];
                                            dtgvSize.Rows[k].Cells["Qtyg"].Value = 0;
                                            dtgvSize.Rows[k].Cells["FrQtyg"].Value = 0;
                                            dtgvSize.Rows[k].Cells["SRateg"].Value = 0;
                                            dtgvSize.Rows[k].Cells["SBarcodeg"].Value = "";
                                            sizeGap = i + sizeGap;
                                            k++;
                                        }
                                        else
                                        {
                                            if (sizeGap == 0)
                                            {
                                                dtgvSize.Rows.Add();
                                                dtgvSize.Rows[k].Cells["Sizeg"].Value = s[i];
                                                dtgvSize.Rows[k].Cells["Qtyg"].Value = 0;
                                                dtgvSize.Rows[k].Cells["FrQtyg"].Value = 0;
                                                dtgvSize.Rows[k].Cells["SRateg"].Value = 0;
                                                dtgvSize.Rows[k].Cells["SBarcodeg"].Value = "";
                                                sizeGap = i + sizeGap1;
                                                k++;
                                            }
                                            else
                                            {
                                                if (sizeGap == i)
                                                    sizeGap = 0;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i <= sizecount; i++)
                                    {
                                        dtgvSize.Rows.Add();
                                        dtgvSize.Rows[i].Cells["Sizeg"].Value = s[i];
                                        dtgvSize.Rows[i].Cells["Qtyg"].Value = 0;
                                        dtgvSize.Rows[i].Cells["FrQtyg"].Value = 0;
                                        dtgvSize.Rows[i].Cells["SRateg"].Value = 0;
                                        dtgvSize.Rows[i].Cells["SBarcodeg"].Value = "";
                                    }
                                }
                                this.ActiveControl = dtgvSize;
                                dtgvSize.Height = dtgvSize.ColumnHeadersHeight + dtgvSize.Rows[0].Height * dtgvSize.RowCount;
                            }
                            else
                            {
                                int sizefrom = Convert.ToInt32(txtsizefrom.Text);
                                int sizeto = Convert.ToInt32(txtsizeto.Text);
                                int sizecount = sizeto - sizefrom;
                                List<int> s = new List<int>();
                                for (int i = sizefrom; i <= sizeto; i++)
                                {
                                    s.Add(i);
                                }
                                dtgvSize.Visible = true;
                                dtgvSize.Rows.Clear();
                                int sizeGap = 0, k = 0, sizeGap1 = 0;
                                sizeGap = Convert.ToInt32(txtsizegap.Text);
                                sizeGap1 = Convert.ToInt32(txtsizegap.Text);
                                if (sizeGap != 0)
                                {
                                    for (int i = 0; i <= sizecount; i++)
                                    {
                                        if (s[i] == sizefrom)
                                        {
                                            dtgvSize.Rows.Add();
                                            dtgvSize.Rows[k].Cells["Sizeg"].Value = s[i];
                                            dtgvSize.Rows[k].Cells["Qtyg"].Value = 0;
                                            dtgvSize.Rows[k].Cells["FrQtyg"].Value = 0;
                                            dtgvSize.Rows[k].Cells["SRateg"].Value = 0;
                                            dtgvSize.Rows[k].Cells["SBarcodeg"].Value = "";
                                            sizeGap = sizefrom + sizeGap;
                                            k++;
                                        }
                                        else
                                        {
                                            if (sizeGap == 0)
                                            {
                                                dtgvSize.Rows.Add();
                                                dtgvSize.Rows[k].Cells["Sizeg"].Value = s[i];
                                                dtgvSize.Rows[k].Cells["Qtyg"].Value = 0;
                                                dtgvSize.Rows[k].Cells["FrQtyg"].Value = 0;
                                                dtgvSize.Rows[k].Cells["SRateg"].Value = 0;
                                                dtgvSize.Rows[k].Cells["SBarcodeg"].Value = "";
                                                sizeGap = s[i] + sizeGap1;
                                                k++;
                                            }
                                            else
                                            {
                                                if (sizeGap == s[i])
                                                    sizeGap = 0;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i <= sizecount; i++)
                                    {
                                        dtgvSize.Rows.Add();
                                        dtgvSize.Rows[i].Cells["Sizeg"].Value = s[i];
                                        dtgvSize.Rows[i].Cells["Qtyg"].Value = 0;
                                        dtgvSize.Rows[i].Cells["FrQtyg"].Value = 0;
                                        dtgvSize.Rows[i].Cells["SRateg"].Value = 0;
                                        dtgvSize.Rows[i].Cells["SBarcodeg"].Value = "";
                                    }
                                }
                                this.ActiveControl = dtgvSize;
                                dtgvSize.Height = dtgvSize.ColumnHeadersHeight + dtgvSize.Rows[0].Height * dtgvSize.RowCount;
                            }
                        }
                    }
                    bindcmbingrid();
                }
            }
            catch (Exception)
            { }
        }

        public void bindcmbingrid()
        {
            try
            {
                for (int i = 0; i <= dtgvSize.RowCount; i++)
                {
                    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)(dtgvSize.Rows[i].Cells["Colorg"]);
                    try
                    {
                        DataTable dt = null;
                        dt = db.Gettable("Select Distinct Cast(Id as Varchar(50)) as Id,ColorName from Colorinfo where CompId=" + CommonMethod.CompId + "");
                        DataRow dr = dt.NewRow(); //Create New Row
                        dr["ColorName"] = "Select";
                        dr["Id"] = "0";
                        dt.Rows.InsertAt(dr, 0);
                        cmb.DataSource = dt;
                        cmb.ValueMember = "Id";
                        cmb.DisplayMember = "ColorName";
                        cmb.Value = "0";
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            { }
        }

        private void txtsizeto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsizeto.Text == "")
                {
                    txtsizeto.Text = "0";
                    this.ActiveControl = txtsizeto;
                }
                //else if (Convert.ToInt32(txtsizeto.Text) > 60)
                //{
                //    MessageBox.Show("Size must be less than 60","Warning");
                //    txtsizeto.Text = "0";
                //    this.ActiveControl = txtsizeto;
                //}
            }
            catch (Exception)
            { }
        }

        //private void txtTCSPer_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtTCSPer.Text != "")
        //        {
        //            if (Convert.ToDecimal(txtTCSPer.Text) <= 100)
        //                getGstCalculation();
        //            else
        //            {
        //                MessageBox.Show("Enter TCS Between 0 to 100 %.", "Warning");
        //                txtTCSPer.Text = "0";
        //                this.ActiveControl = txtTCSPer;
        //            }
        //        }
        //        else
        //        {
        //            txtTCSPer.Text = "0";
        //            this.ActiveControl = txtTCSPer;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        txtTCSPer.Text = "0";
        //    }
        //}

        private void txtTCSAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTCSAmt.Text != "")
                {
                    if (Convert.ToDecimal(txtTCSAmt.Text) != 0)
                        getGstCalculation();
                }
                else
                {
                    txtTCSAmt.Text = "0";
                    this.ActiveControl = txtTCSAmt;
                }
            }
            catch (Exception)
            {
                txtTCSAmt.Text = "0";
            }
        }

        private void txtTCSPer_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTCSPer.Text != "")
                {
                    if (Convert.ToDecimal(txtTCSPer.Text) <= 100)
                        getGstCalculation();
                    else
                    {
                        MessageBox.Show("Enter TCS Between 0 to 100 %.", "Warning");
                        txtTCSPer.Text = "0";
                        this.ActiveControl = txtTCSPer;
                    }
                }
                else
                {
                    txtTCSPer.Text = "0";
                    this.ActiveControl = txtTCSPer;
                }
            }
            catch (Exception)
            {
                txtTCSPer.Text = "0";
            }
        }

        private void txtTCSPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8)
                flgTCSchange = false;
        }

        private void txtTCSAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            flgTCSchange = false;
            if ((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8 || e.KeyChar == 46)
                flgTCSchange = true;
        }

        private void dtgvSize_Leave(object sender, EventArgs e)
        {
            try
            {
                if (dtgvSize.Visible == true)
                {
                    bool findBlank = false;
                    int currentRow_code = 0;
                    string messageStr = "";
                    if (dtgvSize.RowCount > 0)
                    {
                        decimal totalqty = dtgvSize.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells["Qtyg"].Value));
                        if (Convert.ToDecimal(txtQuantity.Text) == totalqty)
                        {
                            for (int i = 0; i < dtgvSize.RowCount; i++)
                            {
                                if (HideSaleRate == true && dtgvSize.Columns["SBarcodeg"].Visible == false && dtgvSize.Columns["SRateg"].Visible == false)
                                { }
                                else
                                {
                                    for (int j = i + 1; j < dtgvSize.RowCount; j++)
                                    {
                                        if (dtgvSize.Columns["SBarcodeg"].Visible == true)
                                        {
                                            if (dtgvSize.Rows[i].Cells["SBarcodeg"].Value.ToString() == dtgvSize.Rows[j].Cells["SBarcodeg"].Value.ToString())
                                            {
                                                findBlank = true;
                                                currentRow_code = i;
                                                messageStr = "All Quantity must have Unique Static Barcode";
                                                break;
                                            }
                                        }
                                    }
                                    if (((Convert.ToDecimal(dtgvSize.Rows[i].Cells["Qtyg"].Value) != 0 && Convert.ToString(dtgvSize.Rows[i].Cells["Qtyg"].Value) != "") && (Convert.ToString(dtgvSize.Rows[i].Cells["SRateg"].Value) == "" && Convert.ToString(dtgvSize.Rows[i].Cells["SRateg"].EditedFormattedValue) == "")) || ((Convert.ToDecimal(dtgvSize.Rows[i].Cells["Qtyg"].Value) != 0 && Convert.ToString(dtgvSize.Rows[i].Cells["Qtyg"].Value) != "") && (dtgvSize.Rows[i].Cells["SRateg"].Value.ToString() == "0" && Convert.ToString(dtgvSize.Rows[i].Cells["SRateg"].EditedFormattedValue) == "0")) || ((Convert.ToDecimal(dtgvSize.Rows[i].Cells["Qtyg"].Value) != 0 && Convert.ToString(dtgvSize.Rows[i].Cells["Qtyg"].Value) != "") && (dtgvSize.Rows[i].Cells["SRateg"].Value == null && Convert.ToString(dtgvSize.Rows[i].Cells["SRateg"].EditedFormattedValue) == null)))
                                    {
                                        findBlank = true;
                                        currentRow_code = i;
                                        messageStr = "All Quantity must have Sale Rate";
                                    }
                                    if (dtgvSize.Columns["SBarcodeg"].Visible == true)
                                    {
                                        if (dtgvSize.Rows[i].Cells["SBarcodeg"].Value.ToString() == "" || dtgvSize.Rows[i].Cells["SBarcodeg"].Value.ToString() == "0")
                                        {
                                            findBlank = true;
                                            currentRow_code = i;
                                            messageStr = "All Quantity must have Static Barcode";
                                        }
                                    }
                                }
                            }
                        }
                        else
                            goto a;
                        //MessageBox.Show("Quantity not distributed properly", "Warning");
                        if (findBlank == true)
                        {
                            MessageBox.Show(messageStr, "Warning");
                            dtgvSize.Visible = true;
                            dtgvSize.Focus();
                            dtgvSize.CurrentCell = dtgvSize[0, currentRow_code];
                            dtgvSize.CurrentCell.Selected = true;
                        }
                        else
                        {
                            pnlIntegratedDtls.Visible = false;
                            txtpurchaseprice.Select();
                        }
                    }
                    else
                    {
                        pnlIntegratedDtls.Visible = false;
                        txtpurchaseprice.Select();
                    }
                }
            a: { }
            }
            catch (Exception)
            { }
        }

        private void txtpurchaseprice_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpurchaseprice.Text == "")
                    txtpurchaseprice.Text = "0.00";
            }
            catch (Exception)
            { }
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSaleRate.Text == "")
                    txtSaleRate.Text = "0.00";
            }
            catch (Exception)
            { }
        }

        private void txtpdiscount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpdiscount.Text == "")
                    txtpdiscount.Text = "0.00";
            }
            catch (Exception)
            { }
        }

        private void txtpSpcDisc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpSpcDisc.Text == "")
                    txtpSpcDisc.Text = "0.00";
            }
            catch (Exception)
            { }
        }

        private void txtpCess_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpCess.Text == "")
                    txtpCess.Text = "0.00";
            }
            catch (Exception)
            { }
        }

        private void pnlIntegratedDtls_Leave(object sender, EventArgs e)
        {
            try
            {
                showsize = false;
                if (dtgvSize.Rows.Count > 0 && productentrytype == 3)
                {
                    decimal totalqty = dtgvSize.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToInt32(t.Cells["Qtyg"].Value));
                    if (Convert.ToDecimal(txtQuantity.Text) != totalqty)
                    {
                        MessageBox.Show("Quantity is not distributed properly", "Warning");
                        this.ActiveControl = dtgvSize;
                        pnlIntegratedDtls.Visible = true;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            try
            {
                showsize = false;
                if (dtgvSize.Rows.Count > 0)
                    dtgvSize.Rows.Clear();
            }
            catch (Exception)
            { }
        }

        private void dtgvcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dtgvcode.Rows.Count - 1 == dtgvcode.CurrentRow.Index)
                {
                    dtgvcode.Leave += new EventHandler(dtgvcode_Leave);
                }
            }
        }
    }
}