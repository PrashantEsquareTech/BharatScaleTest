using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class FrmProjectGlanceQuotation : Form
    {
        public int colIndex = -1, updateId = 0;
        List<int> deleteId = new List<int>();
        DbClass db = new DbClass();
        public static FrmProjectGlanceQuotation projectQut;
        CommonMethod cm = new CommonMethod();

        public FrmProjectGlanceQuotation()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            fillCustomercombo();
            fillsearchstaffcombo();
            cmbDescription.SelectedIndex = 0;
            fillGST();
        }

        public FrmProjectGlanceQuotation(int UpdatedID)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            fillCustomercombo();
            fillsearchstaffcombo();
            cmbDescription.SelectedIndex = 0;
            fillGST();
            LoadData(UpdatedID);
        }

        public void maxproductid()
        {
            try
            {
                ProjectGlanceMasterRepository productrepo = new ProjectGlanceMasterRepository();
                var mid = Convert.ToInt32(productrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.QutNo));
                if (mid == null)
                    txtQutNo.Text = "1";
                else
                    txtQutNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                productrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void LoadData(int UpdateID)
        {
            try
            {
                ProjectGlanceMasterRepository projectMRepo = new ProjectGlanceMasterRepository();
                var projectMData = projectMRepo.GetAll().Where(t => t.Id == UpdateID && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (projectMData != null)
                {
                    updateId = Convert.ToInt32(projectMData.Id);
                    txtQutNo.Text = Convert.ToString(projectMData.QutNo);
                    dtpQutDate.Value = Convert.ToDateTime(projectMData.Date);
                    txtRefChNo.Text = Convert.ToString(projectMData.RefernceNo);
                    txtShedSize.Text = Convert.ToString(projectMData.ShedSize);
                    cmbCustomer.SelectedValue = Convert.ToInt32(projectMData.CustomerId);
                    cmbEmployee.SelectedValue = Convert.ToInt32(projectMData.EmployeeId);
                    txtLengthInFt.Text = Convert.ToString(projectMData.LengthInFeet);
                    txtWidthINFt.Text = Convert.ToString(projectMData.WidthInFeet);
                    txtHeightInFt.Text = Convert.ToString(projectMData.HeightInFeet);
                    txtNOOfHouses.Text = Convert.ToString(projectMData.NoOfHouses);
                    txtSlopeInInch.Text = Convert.ToString(projectMData.SlopeInInch);
                    txtServiceRoom.Text = Convert.ToString(projectMData.ServiceRoom);
                    txtSideHeightTop1.Text = Convert.ToString(projectMData.SideHeightTop);
                    txtSideHeightTop2.Text = Convert.ToString(projectMData.SideHeightTop1);
                    txtSideHeightBottom1.Text = Convert.ToString(projectMData.SideHeightBottom);
                    txtSideHeightBottom2.Text = Convert.ToString(projectMData.SideHeightBottom1);
                    txtBirdsAreaSQFt.Text = Convert.ToString(projectMData.BirdsAreaSqFt);
                    txtBirdsAreaSQFt1.Text = Convert.ToString(projectMData.BirdsAreaSqFt1);
                    txtSHEDAreaSQFt.Text = Convert.ToString(projectMData.SHEDAreaSqFt);
                    txtSHEDAreaSQFt1.Text = Convert.ToString(projectMData.SHEDAreaSqFt1);
                    txtTypesOfBird.Text = Convert.ToString(projectMData.TypeOfBird);
                    txtNoOfBird.Text = Convert.ToString(projectMData.NoOfBirds);
                    txtNoOfBird1.Text = Convert.ToString(projectMData.NoOfBirds1);
                    txtSQFTBIRD.Text = Convert.ToString(projectMData.SQFTBIRD);
                    txtSQFTBIRD1.Text = Convert.ToString(projectMData.SQFTBIRD1);
                    txtType.Text = Convert.ToString(projectMData.Type);
                    txtBirdSQMtr.Text = Convert.ToString(projectMData.BIRDSPERSQMTR);
                    txtBirdSQMtr1.Text = Convert.ToString(projectMData.BIRDSPERSQMTR1);
                    txtBirdLiveWT.Text = Convert.ToString(projectMData.BIRDLIVEWT);
                    txtBirdLiveWT1.Text = Convert.ToString(projectMData.BIRDLIVEWT1);
                    txtEC.Text = Convert.ToString(projectMData.EC);
                    if (projectMData.Cages == "Y")
                        chkCages.Checked = true;
                    else
                        chkCages.Checked = false;
                    txtLiveWTKgSqMt.Text = Convert.ToString(projectMData.LiveWTKgSqMt);
                    txtTotalNipples.Text = Convert.ToString(projectMData.TotalNipples);
                    txtTotalEzypans.Text = Convert.ToString(projectMData.TotalEzypans);
                    txtAIRSPEEDFtMin.Text = Convert.ToString(projectMData.AirSpeedFtMin);
                    txtNOOfFANS.Text = Convert.ToString(projectMData.NoOfFANS);
                    txtBirdsNipples.Text = Convert.ToString(projectMData.BirdsPerNipples);
                    txtBirdsNipples1.Text = Convert.ToString(projectMData.BirdsPerNipples1);
                    txtBirdsPan.Text = Convert.ToString(projectMData.BirdsPerPan);
                    txtBirdsPan1.Text = Convert.ToString(projectMData.BirdsPerPan1);
                    txtRecLenOfLinesFt.Text = Convert.ToString(projectMData.RecLengthOfLinesft);
                    txtNipples10FT.Text = Convert.ToString(projectMData.NipplesIn10FTSegment);
                    txtNipples10FT1.Text = Convert.ToString(projectMData.NipplesIn10FTSegment1);
                    txtNoOfNipplesLines.Text = Convert.ToString(projectMData.NoOfNipplesLines);
                    txtPANS15SEGMENT.Text = Convert.ToString(projectMData.PansIn15FTSegment);
                    txtPANS15SEGMENT1.Text = Convert.ToString(projectMData.PansIn15FTSegment1);
                    txtNoOfFeedLinesLoops.Text = Convert.ToString(projectMData.NoOfFeedLinesLoops);
                    txtSuspensionPoints.Text = Convert.ToString(projectMData.SuspentionPointsAtEveryFt);
                    txtTotalAmt.Text = Convert.ToString(projectMData.TotalAmt);
                    txtGSTAmt.Text = Convert.ToString(projectMData.GSTAmt);
                    txtNetAmt.Text = Convert.ToString(projectMData.NetAmt);
                    txtTotalHP.Text = Convert.ToString(projectMData.TotalHP);
                    ProjectGlanceDetailRepository projectDetailRepo = new ProjectGlanceDetailRepository();
                    List<tblProjectGlanceDetail> projectDData = projectDetailRepo.GetAll().Where(t => t.MasterId == updateId && t.tblProjectAtGlanceMaster.CompId == CommonMethod.CompId).ToList();
                    if (projectDData.Count > 0)
                    {
                        int i = 0;
                        foreach (var item in projectDData)
                        {
                            GvProductInfo.Rows.Add();
                            GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                            GvProductInfo.Rows[i].Cells["Code"].Value = item.CodeNo;
                            GvProductInfo.Rows[i].Cells["DescriptionSystem"].Value = item.Description;
                            GvProductInfo.Rows[i].Cells["InvestBird"].Value = item.InvestBird;
                            GvProductInfo.Rows[i].Cells["InvPerBird"].Value = item.InvestmentPerHouse;
                            GvProductInfo.Rows[i].Cells["SGSTnCGST"].Value = item.SGSTCGSTPercent;
                            GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                            GvProductInfo.Rows[i].Cells["HP"].Value = item.HP;
                            GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            GvProductInfo.Rows[i].Cells["SCGSTAmt"].Value = Convert.ToDecimal(item.SGSTCGSTPercent) * Convert.ToDecimal(item.InvestmentPerHouse) / 100;
                            GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = Convert.ToDecimal(item.IGstPercent) * Convert.ToDecimal(item.InvestmentPerHouse) / 100;
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

        public void fillsearchstaffcombo()
        {
            try
            {
                StaffRepository staffrepo = new StaffRepository();
                List<StaffInfo> staffdata = new List<StaffInfo>();
                staffdata.Clear();
                staffdata = staffrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Name).ToList();
                staffdata.Add(new StaffInfo { Name = "Select", ID = 0 });
                staffdata.Reverse();
                cmbEmployee.DataSource = staffdata;
                cmbEmployee.ValueMember = "ID";
                cmbEmployee.DisplayMember = "Name";
                staffrepo.Dispose();
                cmbEmployee.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillCustomercombo()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> staffdata = new List<CustomerInformation>();
                staffdata.Clear();
                staffdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                staffdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                staffdata.Reverse();
                cmbCustomer.DataSource = staffdata;
                cmbCustomer.ValueMember = "Id";
                cmbCustomer.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbCustomer.SelectedIndex = 0;
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

        public void clear()
        {
            try
            {
                maxproductid(); fillGST();
                fillCustomercombo();
                fillsearchstaffcombo();
                txtAIRSPEEDFtMin.Text = "";
                txtBirdLiveWT.Text = "";
                txtBirdLiveWT1.Text = "";
                txtBirdsAreaSQFt.Text = "";
                txtBirdsAreaSQFt1.Text = "";
                txtBirdsNipples.Text = "";
                txtBirdsNipples1.Text = "";
                txtShedSize.Text = "";
                txtBirdsPan.Text = "";
                txtBirdsPan1.Text = "";
                txtBirdSQMtr.Text = "";
                txtBirdSQMtr1.Text = "";
                txtCode.Text = "";
                txtEC.Text = "";
                txtGSTAmt.Text = "0";
                txtHeightInFt.Text = "";
                txtHP.Text = "";
                txtInvestBirds.Text = "";
                txtINVPERHouse.Text = "";
                txtLengthInFt.Text = "";
                txtLiveWTKgSqMt.Text = "";
                txtNetAmt.Text = "0";
                txtNipples10FT.Text = "";
                txtNipples10FT1.Text = "";
                txtNoOfBird.Text = "";
                txtNoOfBird1.Text = "";
                txtNOOfFANS.Text = "";
                txtNoOfFeedLinesLoops.Text = "";
                txtNOOfHouses.Text = "";
                txtNoOfNipplesLines.Text = "";
                txtPANS15SEGMENT.Text = "";
                txtPANS15SEGMENT1.Text = "";
                txtRecLenOfLinesFt.Text = "";
                txtRefChNo.Text = "";
                txtServiceRoom.Text = "";
                txtSHEDAreaSQFt.Text = "";
                colIndex = -1;
                txtSHEDAreaSQFt1.Text = "";
                txtSideHeightBottom1.Text = "";
                txtSideHeightBottom2.Text = "";
                txtSideHeightTop1.Text = "";
                txtSideHeightTop2.Text = "";
                txtSlopeInInch.Text = "";
                txtSQFTBIRD.Text = "";
                txtSQFTBIRD1.Text = "";
                txtSuspensionPoints.Text = "";
                txtTotalAmt.Text = "0";
                txtTotalEzypans.Text = "";
                txtTotalHP.Text = "0";
                txtTotalNipples.Text = "";
                txtType.Text = "";
                txtTypesOfBird.Text = "";
                txtWidthINFt.Text = "";
                cmbDescription.SelectedIndex = 0;
                dtpQutDate.Value = DateTime.Now.Date;
                btnsave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                cmbCGST.Enabled = true;
                cmbIGST.Enabled = true;
                GvProductInfo.Rows.Clear();
                this.ActiveControl = btnprint;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text != "" && cmbDescription.Text != "")
                {
                    if (colIndex == -1)
                    {
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            if (cmbDescription.Text == Convert.ToString(GvProductInfo.Rows[i].Cells["DescriptionSystem"].Value))
                            {
                                MessageBox.Show("This description is already exists");
                                cmbDescription.SelectedIndex = 0;
                               this.ActiveControl= cmbDescription;
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
                            decimal totalAmt = 0, totSCGSTAmt = 0, totIGSTAmt = 0, totalHP = 0, totalGST = 0, totalNetAmt = 0;
                            GvProductInfo.Rows.Add();
                            int i = GvProductInfo.RowCount;
                            GvProductInfo.Rows[i - 1].Cells["Code"].Value = txtCode.Text;
                            GvProductInfo.Rows[i - 1].Cells["DescriptionSystem"].Value = cmbDescription.Text;
                            if (txtInvestBirds.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["InvestBird"].Value = "0";
                            else
                                GvProductInfo.Rows[i - 1].Cells["InvestBird"].Value = txtInvestBirds.Text;
                            if (txtINVPERHouse.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["InvPerBird"].Value = "0";
                            else
                                GvProductInfo.Rows[i - 1].Cells["InvPerBird"].Value = txtINVPERHouse.Text;
                            if (cmbCGST.Text == "")
                            {
                                GvProductInfo.Rows[i - 1].Cells["SGSTnCGST"].Value = "0";
                                GvProductInfo.Rows[i - 1].Cells["SCGSTAmt"].Value = "0";
                            }
                            else
                            {
                                GvProductInfo.Rows[i - 1].Cells["SGSTnCGST"].Value = cmbCGST.Text;
                                totSCGSTAmt = Convert.ToDecimal(GvProductInfo.Rows[i - 1].Cells["SGSTnCGST"].Value) * Convert.ToDecimal(GvProductInfo.Rows[i - 1].Cells["InvPerBird"].Value) / 100;
                                GvProductInfo.Rows[i - 1].Cells["SCGSTAmt"].Value = totSCGSTAmt;
                            }
                            if (cmbIGST.Text == "")
                            {
                                GvProductInfo.Rows[i - 1].Cells["IGST"].Value = "0";
                                GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = "0";
                            }
                            else
                            {
                                GvProductInfo.Rows[i - 1].Cells["IGST"].Value = cmbIGST.Text;
                                totSCGSTAmt = Convert.ToDecimal(GvProductInfo.Rows[i - 1].Cells["IGST"].Value) * Convert.ToDecimal(GvProductInfo.Rows[i - 1].Cells["InvPerBird"].Value) / 100;
                                GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = totSCGSTAmt;
                            }
                            if (txtHP.Text == "")
                                GvProductInfo.Rows[i - 1].Cells["HP"].Value = "0";
                            else
                                GvProductInfo.Rows[i - 1].Cells["HP"].Value = txtHP.Text;
                            GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;

                            totalAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["InvPerBird"].Value));
                            totSCGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["SCGSTAmt"].Value));
                            totIGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["IGSTAmt"].Value));
                            totalGST = totSCGSTAmt + totIGSTAmt;
                            totalHP = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["HP"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalAmt, 2, MidpointRounding.AwayFromZero));
                            txtGSTAmt.Text = Convert.ToString(Math.Round(totalGST, 2, MidpointRounding.AwayFromZero));
                            txtTotalHP.Text = Convert.ToString(Math.Round(totalHP, 2, MidpointRounding.AwayFromZero));
                            totalNetAmt = totalAmt + totalGST;
                            txtNetAmt.Text = Convert.ToString(Math.Round(totalNetAmt, 2, MidpointRounding.AwayFromZero));
                            txtHP.Text = "";
                            txtCode.Text = "";
                            cmbDescription.SelectedIndex = 0;
                            txtInvestBirds.Text = "";
                            txtINVPERHouse.Text = "";
                            colIndex = -1;
                            this.ActiveControl = txtCode; 
                            fillGST();
                            cmbCGST.Enabled = true;
                            cmbIGST.Enabled = true;
                        }
                        else
                        {
                            decimal totalAmt = 0, totSCGSTAmt = 0, totIGSTAmt = 0, totalHP = 0, totalGST = 0, totalNetAmt = 0;
                            GvProductInfo.Rows[colIndex].Cells["Code"].Value = txtCode.Text;
                            GvProductInfo.Rows[colIndex].Cells["DescriptionSystem"].Value = cmbDescription.Text;
                            if (txtInvestBirds.Text == "")
                                GvProductInfo.Rows[colIndex].Cells["InvestBird"].Value = "0";
                            else
                                GvProductInfo.Rows[colIndex].Cells["InvestBird"].Value = txtInvestBirds.Text;
                            if (txtINVPERHouse.Text == "")
                                GvProductInfo.Rows[colIndex].Cells["InvPerBird"].Value = "0";
                            else
                                GvProductInfo.Rows[colIndex].Cells["InvPerBird"].Value = txtINVPERHouse.Text;
                            if (cmbCGST.Text == "")
                            {
                                GvProductInfo.Rows[colIndex].Cells["SGSTnCGST"].Value = "0";
                                GvProductInfo.Rows[colIndex].Cells["SCGSTAmt"].Value = "0";
                            }
                            else
                            {
                                GvProductInfo.Rows[colIndex].Cells["SGSTnCGST"].Value = cmbCGST.Text;
                                totSCGSTAmt = Convert.ToDecimal(GvProductInfo.Rows[colIndex].Cells["SGSTnCGST"].Value) * Convert.ToDecimal(GvProductInfo.Rows[colIndex].Cells["InvPerBird"].Value) / 100;
                                GvProductInfo.Rows[colIndex].Cells["SCGSTAmt"].Value = totSCGSTAmt;
                            }
                            if (cmbIGST.Text == "")
                            {
                                GvProductInfo.Rows[colIndex].Cells["IGST"].Value = "0";
                                GvProductInfo.Rows[colIndex].Cells["IGSTAmt"].Value = "0";
                            }
                            else
                            {
                                GvProductInfo.Rows[colIndex].Cells["IGST"].Value = cmbIGST.Text;
                                totSCGSTAmt = Convert.ToDecimal(GvProductInfo.Rows[colIndex].Cells["IGST"].Value) * Convert.ToDecimal(GvProductInfo.Rows[colIndex].Cells["InvPerBird"].Value) / 100;
                                GvProductInfo.Rows[colIndex].Cells["IGSTAmt"].Value = totSCGSTAmt;
                            }
                            if (txtHP.Text == "")
                                GvProductInfo.Rows[colIndex].Cells["HP"].Value = "0";
                            else
                                GvProductInfo.Rows[colIndex].Cells["HP"].Value = txtHP.Text;
                            GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            totalAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["InvPerBird"].Value));
                            totSCGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["SCGSTAmt"].Value));
                            totIGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["IGSTAmt"].Value));
                            totalGST = totSCGSTAmt + totIGSTAmt;
                            totalHP = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["HP"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalAmt, 2, MidpointRounding.AwayFromZero));
                            txtGSTAmt.Text = Convert.ToString(Math.Round(totalGST, 2, MidpointRounding.AwayFromZero));
                            txtTotalHP.Text = Convert.ToString(Math.Round(totalHP, 2, MidpointRounding.AwayFromZero));
                            totalNetAmt = totalAmt + totalGST;
                            txtNetAmt.Text = Convert.ToString(Math.Round(totalNetAmt, 2, MidpointRounding.AwayFromZero));
                            txtHP.Text = "";
                            txtCode.Text = "";
                            cmbDescription.SelectedIndex = 0;
                            txtInvestBirds.Text = "";
                            txtINVPERHouse.Text = "";
                            colIndex = -1; fillGST();
                            this.ActiveControl = txtCode;
                            cmbCGST.Enabled = true;
                            cmbIGST.Enabled = true;
                        }
                    }
                a: { }
                }
                else
                {
                    if (txtCode.Text == "")
                    {
                        MessageBox.Show("Enter Code");
                        this.ActiveControl = txtCode;
                    }
                    else if (cmbDescription.Text == "")
                    {
                        MessageBox.Show("Seelct Description");
                        this.ActiveControl = cmbDescription;
                    }
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

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception)
            { }
        }

        public void fillGST()
        {
            try
            {
                DataTable dtigst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst.Rows.Count > 0)
                {
                    cmbIGST.ValueMember = "GSTNo";
                    cmbIGST.DisplayMember = "GSTPercent";
                    cmbIGST.DataSource = dtigst;
                }
                DataTable dtscgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtscgst.Rows.Count > 0)
                {
                    cmbCGST.ValueMember = "GSTNo";
                    cmbCGST.DisplayMember = "GSTPercent";
                    cmbCGST.DataSource = dtscgst;
                }
            }
            catch (Exception)
            { }
        }
      
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbCustomer.SelectedIndex > 0 && GvProductInfo.Rows.Count > 0)
                {
                    ProjectGlanceMasterRepository projectGMRepo = new ProjectGlanceMasterRepository();
                    var projectGMData = projectGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (projectGMData != null)
                    {
                        MessageBox.Show("This Record is already saved");
                        this.ActiveControl = txtRefChNo;
                    }
                    else
                    {
                        projectGMRepo = new ProjectGlanceMasterRepository();
                        tblProjectAtGlanceMaster projMData = new tblProjectAtGlanceMaster();
                        projMData.QutNo = Convert.ToInt32(txtQutNo.Text);
                        projMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                        projMData.RefernceNo = txtRefChNo.Text.Trim();
                        projMData.ShedSize = txtShedSize.Text.Trim();
                        if (cmbCustomer.SelectedIndex > 0)
                            projMData.CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                        else
                            projMData.CustomerId = null;
                        if (cmbEmployee.SelectedIndex > 0)
                            projMData.EmployeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
                        else
                            projMData.EmployeeId = null;
                        if (txtLengthInFt.Text != "")
                            projMData.LengthInFeet = Convert.ToDecimal(txtLengthInFt.Text);
                        else
                            projMData.LengthInFeet = 0;
                        if (txtWidthINFt.Text != "")
                            projMData.WidthInFeet = Convert.ToDecimal(txtWidthINFt.Text);
                        else
                            projMData.WidthInFeet = 0;
                        if (txtHeightInFt.Text != "")
                            projMData.HeightInFeet = Convert.ToDecimal(txtHeightInFt.Text);
                        else
                            projMData.HeightInFeet = 0;
                        if (txtNOOfHouses.Text != "")
                            projMData.NoOfHouses = Convert.ToDecimal(txtNOOfHouses.Text);
                        else
                            projMData.NoOfHouses = 0;
                        if (txtSlopeInInch.Text != "")
                            projMData.SlopeInInch = Convert.ToDecimal(txtSlopeInInch.Text);
                        else
                            projMData.SlopeInInch = 0;
                        if (txtServiceRoom.Text != "")
                            projMData.ServiceRoom = Convert.ToDecimal(txtServiceRoom.Text);
                        else
                            projMData.ServiceRoom = 0;
                        if (txtSideHeightTop1.Text != "")
                            projMData.SideHeightTop = Convert.ToDecimal(txtSideHeightTop1.Text);
                        else
                            projMData.SideHeightTop = 0;
                        if (txtSideHeightTop2.Text != "")
                            projMData.SideHeightTop1 = Convert.ToDecimal(txtSideHeightTop2.Text);
                        else
                            projMData.SideHeightTop1 = 0;
                        if (txtSideHeightBottom1.Text != "")
                            projMData.SideHeightBottom = Convert.ToDecimal(txtSideHeightBottom1.Text);
                        else
                            projMData.SideHeightBottom = 0;

                        if (txtSideHeightBottom2.Text != "")
                            projMData.SideHeightBottom1 = Convert.ToDecimal(txtSideHeightBottom2.Text);
                        else
                            projMData.SideHeightBottom1 = 0;
                        if (txtBirdsAreaSQFt.Text != "")
                            projMData.BirdsAreaSqFt = Convert.ToDecimal(txtBirdsAreaSQFt.Text);
                        else
                            projMData.BirdsAreaSqFt = 0;
                        if (txtBirdsAreaSQFt1.Text != "")
                            projMData.BirdsAreaSqFt1 = Convert.ToDecimal(txtBirdsAreaSQFt1.Text);
                        else
                            projMData.BirdsAreaSqFt1 = 0;
                        if (txtSHEDAreaSQFt.Text != "")
                            projMData.SHEDAreaSqFt = Convert.ToDecimal(txtSHEDAreaSQFt.Text);
                        else
                            projMData.SHEDAreaSqFt = 0;
                        if (txtSHEDAreaSQFt1.Text != "")
                            projMData.SHEDAreaSqFt1 = Convert.ToDecimal(txtSHEDAreaSQFt1.Text);
                        else
                            projMData.SHEDAreaSqFt1 = 0;
                        projMData.TypeOfBird = txtTypesOfBird.Text.Trim(); ;
                        projMData.Type = txtType.Text.Trim();
                        projMData.EC = txtEC.Text.Trim();
                        if (txtNoOfBird.Text != "")
                            projMData.NoOfBirds = Convert.ToDecimal(txtNoOfBird.Text);
                        else
                            projMData.NoOfBirds = 0;
                        if (txtNoOfBird1.Text != "")
                            projMData.NoOfBirds1 = Convert.ToDecimal(txtNoOfBird1.Text);
                        else
                            projMData.NoOfBirds1 = 0;
                        if (txtSQFTBIRD.Text != "")
                            projMData.SQFTBIRD = Convert.ToDecimal(txtSQFTBIRD.Text);
                        else
                            projMData.SQFTBIRD = 0;
                        if (txtSQFTBIRD1.Text != "")
                            projMData.SQFTBIRD1 = Convert.ToDecimal(txtSQFTBIRD1.Text);
                        else
                            projMData.SQFTBIRD1 = 0;
                        if (txtBirdSQMtr.Text != "")
                            projMData.BIRDSPERSQMTR = Convert.ToDecimal(txtBirdSQMtr.Text);
                        else
                            projMData.BIRDSPERSQMTR = 0;
                        if (txtBirdSQMtr1.Text != "")
                            projMData.BIRDSPERSQMTR1 = Convert.ToDecimal(txtBirdSQMtr1.Text);
                        else
                            projMData.BIRDSPERSQMTR1 = 0;

                        if (txtBirdLiveWT.Text != "")
                            projMData.BIRDLIVEWT = Convert.ToDecimal(txtBirdLiveWT.Text);
                        else
                            projMData.BIRDLIVEWT = 0;
                        if (txtBirdLiveWT1.Text != "")
                            projMData.BIRDLIVEWT1 = Convert.ToDecimal(txtBirdLiveWT1.Text);
                        else
                            projMData.BIRDLIVEWT1 = 0;
                        if (chkCages.Checked == true)
                            projMData.Cages = "Y";
                        else
                            projMData.Cages = "N";
                        if (txtLiveWTKgSqMt.Text != "")
                            projMData.LiveWTKgSqMt = Convert.ToDecimal(txtLiveWTKgSqMt.Text);
                        else
                            projMData.LiveWTKgSqMt = 0;
                        if (txtTotalNipples.Text != "")
                            projMData.TotalNipples = Convert.ToDecimal(txtTotalNipples.Text);
                        else
                            projMData.TotalNipples = 0;
                        if (txtTotalEzypans.Text != "")
                            projMData.TotalEzypans = Convert.ToDecimal(txtTotalEzypans.Text);
                        else
                            projMData.TotalEzypans = 0;
                        if (txtAIRSPEEDFtMin.Text != "")
                            projMData.AirSpeedFtMin = Convert.ToDecimal(txtAIRSPEEDFtMin.Text);
                        else
                            projMData.AirSpeedFtMin = 0;
                        if (txtNOOfFANS.Text != "")
                            projMData.NoOfFANS = Convert.ToDecimal(txtNOOfFANS.Text);
                        else
                            projMData.NoOfFANS = 0;
                        if (txtBirdsNipples.Text != "")
                            projMData.BirdsPerNipples = Convert.ToDecimal(txtBirdsNipples.Text);
                        else
                            projMData.BirdsPerNipples = 0;
                        if (txtBirdsNipples1.Text != "")
                            projMData.BirdsPerNipples1 = Convert.ToDecimal(txtBirdsNipples1.Text);
                        else
                            projMData.BirdsPerNipples1 = 0;
                        if (txtBirdsPan.Text != "")
                            projMData.BirdsPerPan = Convert.ToDecimal(txtBirdsPan.Text);
                        else
                            projMData.BirdsPerPan = 0;
                        if (txtBirdsPan1.Text != "")
                            projMData.BirdsPerPan1 = Convert.ToDecimal(txtBirdsPan1.Text);
                        else
                            projMData.BirdsPerPan1 = 0;
                        if (txtRecLenOfLinesFt.Text != "")
                            projMData.RecLengthOfLinesft = Convert.ToDecimal(txtRecLenOfLinesFt.Text);
                        else
                            projMData.RecLengthOfLinesft = 0;
                        if (txtNipples10FT.Text != "")
                            projMData.NipplesIn10FTSegment = Convert.ToDecimal(txtNipples10FT.Text);
                        else
                            projMData.NipplesIn10FTSegment = 0;
                        if (txtNipples10FT1.Text != "")
                            projMData.NipplesIn10FTSegment1 = Convert.ToDecimal(txtNipples10FT1.Text);
                        else
                            projMData.NipplesIn10FTSegment1 = 0;
                        if (txtNoOfNipplesLines.Text != "")
                            projMData.NoOfNipplesLines = Convert.ToDecimal(txtNoOfNipplesLines.Text);
                        else
                            projMData.NoOfNipplesLines = 0;
                        if (txtPANS15SEGMENT.Text != "")
                            projMData.PansIn15FTSegment = Convert.ToDecimal(txtPANS15SEGMENT.Text);
                        else
                            projMData.PansIn15FTSegment = 0;
                        if (txtPANS15SEGMENT1.Text != "")
                            projMData.PansIn15FTSegment1 = Convert.ToDecimal(txtPANS15SEGMENT1.Text);
                        else
                            projMData.PansIn15FTSegment1 = 0;
                        if (txtNoOfFeedLinesLoops.Text != "")
                            projMData.NoOfFeedLinesLoops = Convert.ToDecimal(txtNoOfFeedLinesLoops.Text);
                        else
                            projMData.NoOfFeedLinesLoops = 0;
                        if (txtSuspensionPoints.Text != "")
                            projMData.SuspentionPointsAtEveryFt = Convert.ToDecimal(txtSuspensionPoints.Text);
                        else
                            projMData.SuspentionPointsAtEveryFt = 0;
                        if (txtTotalAmt.Text != "")
                            projMData.TotalAmt = Convert.ToDecimal(txtTotalAmt.Text);
                        else
                            projMData.TotalAmt = 0;
                        if (txtGSTAmt.Text != "")
                            projMData.GSTAmt = Convert.ToDecimal(txtGSTAmt.Text);
                        else
                            projMData.GSTAmt = 0;
                        if (txtTotalHP.Text != "")
                            projMData.TotalHP = Convert.ToDecimal(txtTotalHP.Text);
                        else
                            projMData.TotalHP = 0;
                        if (txtNetAmt.Text != "")
                            projMData.NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                        else
                            projMData.NetAmt = 0;
                        projMData.CompId = CommonMethod.CompId;
                        projMData.TransactionYear = CommonMethod.TransactionYear;
                        projectGMRepo.Add(projMData);
                        projectGMRepo.Save();
                        updateId = Convert.ToInt32(projMData.Id);
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            ProjectGlanceDetailRepository projectDRepo = new ProjectGlanceDetailRepository();
                            tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                            projDData.CodeNo = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                            projDData.Description = Convert.ToString(GvProductInfo.Rows[i].Cells["DescriptionSystem"].Value);
                            projDData.InvestBird = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["InvestBird"].Value);
                            projDData.InvestmentPerHouse = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["InvPerBird"].Value);
                            projDData.HP = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["HP"].Value);
                            projDData.SGSTCGSTPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTnCGST"].Value);
                            projDData.IGstPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                            projDData.MasterId = updateId;
                            projectDRepo.Add(projDData);
                            projectDRepo.Save();
                        }
                        MessageBox.Show("Record Save Successfully");
                        clear();
                    }
                    projectGMRepo.Dispose();
                }
                else
                {
                    if (txtRefChNo.Text == "")
                    {
                        MessageBox.Show("Select Reference No");
                        this.ActiveControl = txtRefChNo;
                    }
                    else if (cmbCustomer.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbCustomer;
                    }
                    else
                    {
                        MessageBox.Show("Please add at least one Description System");
                        this.ActiveControl = txtCode;
                    }
                }
            }
            catch (Exception)
            { }
        }
            
        private void cmbCGST_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCGST.SelectedIndex > 0)
                    cmbIGST.Enabled = false;
                else
                    cmbIGST.Enabled = true;
            }
            catch (Exception)
            { }
        }

        public void CalFeetToMeter()
        {
            try
            {
                if (txtSideHeightTop1.Text != "")
                    txtSideHeightTop2.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtSideHeightTop1.Text) / Convert.ToDecimal(3.28084), 2, MidpointRounding.AwayFromZero));
                else
                {
                    txtSideHeightTop1.Text = "0";
                    txtSideHeightTop2.Text = "0";
                }

                if (txtSideHeightBottom1.Text != "")
                    txtSideHeightBottom2.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtSideHeightBottom1.Text) / Convert.ToDecimal(3.28084), 2, MidpointRounding.AwayFromZero));
                else
                {
                    txtSideHeightBottom1.Text = "0";
                    txtSideHeightBottom2.Text = "0";
                }
                if (txtBirdsAreaSQFt.Text != "")
                    txtBirdsAreaSQFt1.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtBirdsAreaSQFt.Text)/Convert.ToDecimal(10.764), 2, MidpointRounding.AwayFromZero));
                else
                {
                    txtBirdsAreaSQFt.Text = "0";
                    txtBirdsAreaSQFt1.Text = "0";
                }
                if (txtSHEDAreaSQFt.Text != "")
                    txtSHEDAreaSQFt1.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtSHEDAreaSQFt.Text)/Convert.ToDecimal(10.764), 2, MidpointRounding.AwayFromZero));
                else
                {
                    txtSHEDAreaSQFt.Text = "0";
                    txtSHEDAreaSQFt1.Text = "0";
                }
            }
            catch (Exception)
            { }
        }

        private void cmbIGST_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIGST.SelectedIndex > 0)
                    cmbCGST.Enabled = false;
                else
                    cmbCGST.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btndeliverychallenlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                projectQut = this;
                FrmProjectGlanceQuotation pq = new FrmProjectGlanceQuotation();
                pq.Visible = false;
                frmProjectAtGlanceQuotationList plist = new frmProjectAtGlanceQuotationList();
                plist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string input = "";
                input = Microsoft.VisualBasic.Interaction.InputBox("1 For Front Page Print and 2 For Project At Glance Page Print", "Project At Glance", "", 250, 250);
                if (input == "1" || input == "")
                {
                    RptProjectAtGlanceAndNippleFeederQuotation rpt = new RptProjectAtGlanceAndNippleFeederQuotation(updateId, "FrontPage");
                    rpt.ShowDialog();
                    rpt.Dispose();
                }
                else
                {
                    RptProjectAtGlanceAndNippleFeederQuotation rpt = new RptProjectAtGlanceAndNippleFeederQuotation(updateId, "GlanceQut");
                    rpt.ShowDialog();
                    rpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtSideHeightTop1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSideHeightTop1.Text != "")
                    CalFeetToMeter();
            }
            catch (Exception)
            { }
        }

        private void txtSideHeightBottom1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSideHeightBottom1.Text != "")
                    CalFeetToMeter();
            }
            catch (Exception)
            { }
        }

        private void txtBirdsAreaSQFt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBirdsAreaSQFt.Text != "")
                    CalFeetToMeter();
            }
            catch (Exception)
            { }
        }

        private void txtSHEDAreaSQFt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSHEDAreaSQFt.Text != "")
                    CalFeetToMeter();
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefChNo.Text != "" && cmbCustomer.SelectedIndex > 0 && GvProductInfo.Rows.Count > 0)
                {
                    ProjectGlanceMasterRepository projectGMRepo = new ProjectGlanceMasterRepository();
                    var projectGMData = projectGMRepo.GetAll().Where(t => t.RefernceNo.ToLower().Trim() == txtRefChNo.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Id != updateId).FirstOrDefault();
                    if (projectGMData != null)
                    {
                        MessageBox.Show("This Record is already saved");
                        this.ActiveControl = txtRefChNo;
                    }
                    else
                    {
                        projectGMRepo = new ProjectGlanceMasterRepository();
                        tblProjectAtGlanceMaster projMData = projectGMRepo.GetById(updateId);
                        projMData.QutNo = Convert.ToInt32(txtQutNo.Text);
                        projMData.Date = Convert.ToDateTime(dtpQutDate.Value);
                        projMData.RefernceNo = txtRefChNo.Text.Trim();
                        projMData.ShedSize = txtShedSize.Text.Trim();
                        if (cmbCustomer.SelectedIndex > 0)
                            projMData.CustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                        else
                            projMData.CustomerId = null;
                        if (cmbEmployee.SelectedIndex > 0)
                            projMData.EmployeeId = Convert.ToInt32(cmbEmployee.SelectedValue);
                        else
                            projMData.EmployeeId = null;
                        if (txtLengthInFt.Text != "")
                            projMData.LengthInFeet = Convert.ToDecimal(txtLengthInFt.Text);
                        else
                            projMData.LengthInFeet = 0;
                        if (txtWidthINFt.Text != "")
                            projMData.WidthInFeet = Convert.ToDecimal(txtWidthINFt.Text);
                        else
                            projMData.WidthInFeet = 0;
                        if (txtHeightInFt.Text != "")
                            projMData.HeightInFeet = Convert.ToDecimal(txtWidthINFt.Text);
                        else
                            projMData.HeightInFeet = 0;
                        if (txtNOOfHouses.Text != "")
                            projMData.NoOfHouses = Convert.ToDecimal(txtNOOfHouses.Text);
                        else
                            projMData.NoOfHouses = 0;
                        if (txtSlopeInInch.Text != "")
                            projMData.SlopeInInch = Convert.ToDecimal(txtSlopeInInch.Text);
                        else
                            projMData.SlopeInInch = 0;
                        if (txtServiceRoom.Text != "")
                            projMData.ServiceRoom = Convert.ToDecimal(txtServiceRoom.Text);
                        else
                            projMData.ServiceRoom = 0;
                        if (txtSideHeightTop1.Text != "")
                            projMData.SideHeightTop = Convert.ToDecimal(txtSideHeightTop1.Text);
                        else
                            projMData.SideHeightTop = 0;
                        if (txtSideHeightTop2.Text != "")
                            projMData.SideHeightTop1 = Convert.ToDecimal(txtSideHeightTop2.Text);
                        else
                            projMData.SideHeightTop1 = 0;
                        if (txtSideHeightBottom1.Text != "")
                            projMData.SideHeightBottom = Convert.ToDecimal(txtSideHeightBottom1.Text);
                        else
                            projMData.SideHeightBottom = 0;
                        if (txtSideHeightBottom2.Text != "")
                            projMData.SideHeightBottom1 = Convert.ToDecimal(txtSideHeightBottom2.Text);
                        else
                            projMData.SideHeightBottom1 = 0;
                        if (txtBirdsAreaSQFt.Text != "")
                            projMData.BirdsAreaSqFt = Convert.ToDecimal(txtBirdsAreaSQFt.Text);
                        else
                            projMData.BirdsAreaSqFt = 0;
                        if (txtBirdsAreaSQFt1.Text != "")
                            projMData.BirdsAreaSqFt1 = Convert.ToDecimal(txtBirdsAreaSQFt1.Text);
                        else
                            projMData.BirdsAreaSqFt1 = 0;
                        if (txtSHEDAreaSQFt.Text != "")
                            projMData.SHEDAreaSqFt = Convert.ToDecimal(txtSHEDAreaSQFt.Text);
                        else
                            projMData.SHEDAreaSqFt = 0;
                        if (txtSHEDAreaSQFt1.Text != "")
                            projMData.SHEDAreaSqFt1 = Convert.ToDecimal(txtSHEDAreaSQFt1.Text);
                        else
                            projMData.SHEDAreaSqFt1 = 0;
                        projMData.TypeOfBird = txtTypesOfBird.Text.Trim(); ;
                        projMData.Type = txtType.Text.Trim();
                        projMData.EC = txtEC.Text.Trim();
                        if (txtNoOfBird.Text != "")
                            projMData.NoOfBirds = Convert.ToDecimal(txtNoOfBird.Text);
                        else
                            projMData.NoOfBirds = 0;
                        if (txtNoOfBird1.Text != "")
                            projMData.NoOfBirds1 = Convert.ToDecimal(txtNoOfBird1.Text);
                        else
                            projMData.NoOfBirds1 = 0;
                        if (txtSQFTBIRD.Text != "")
                            projMData.SQFTBIRD = Convert.ToDecimal(txtSQFTBIRD.Text);
                        else
                            projMData.SQFTBIRD = 0;
                        if (txtSQFTBIRD1.Text != "")
                            projMData.SQFTBIRD1 = Convert.ToDecimal(txtSQFTBIRD1.Text);
                        else
                            projMData.SQFTBIRD1 = 0;
                        if (txtBirdSQMtr.Text != "")
                            projMData.BIRDSPERSQMTR = Convert.ToDecimal(txtBirdSQMtr.Text);
                        else
                            projMData.BIRDSPERSQMTR = 0;
                        if (txtBirdSQMtr1.Text != "")
                            projMData.BIRDSPERSQMTR1 = Convert.ToDecimal(txtBirdSQMtr1.Text);
                        else
                            projMData.BIRDSPERSQMTR1 = 0;

                        if (txtBirdLiveWT.Text != "")
                            projMData.BIRDLIVEWT = Convert.ToDecimal(txtBirdLiveWT.Text);
                        else
                            projMData.BIRDLIVEWT = 0;
                        if (txtBirdLiveWT1.Text != "")
                            projMData.BIRDLIVEWT1 = Convert.ToDecimal(txtBirdLiveWT1.Text);
                        else
                            projMData.BIRDLIVEWT1 = 0;
                        if (chkCages.Checked == true)
                            projMData.Cages = "Y";
                        else
                            projMData.Cages = "N";
                        if (txtLiveWTKgSqMt.Text != "")
                            projMData.LiveWTKgSqMt = Convert.ToDecimal(txtLiveWTKgSqMt.Text);
                        else
                            projMData.LiveWTKgSqMt = 0;
                        if (txtTotalNipples.Text != "")
                            projMData.TotalNipples = Convert.ToDecimal(txtTotalNipples.Text);
                        else
                            projMData.TotalNipples = 0;
                        if (txtTotalEzypans.Text != "")
                            projMData.TotalEzypans = Convert.ToDecimal(txtTotalEzypans.Text);
                        else
                            projMData.TotalEzypans = 0;
                        if (txtAIRSPEEDFtMin.Text != "")
                            projMData.AirSpeedFtMin = Convert.ToDecimal(txtAIRSPEEDFtMin.Text);
                        else
                            projMData.AirSpeedFtMin = 0;
                        if (txtNOOfFANS.Text != "")
                            projMData.NoOfFANS = Convert.ToDecimal(txtNOOfFANS.Text);
                        else
                            projMData.NoOfFANS = 0;
                        if (txtBirdsNipples.Text != "")
                            projMData.BirdsPerNipples = Convert.ToDecimal(txtBirdsNipples.Text);
                        else
                            projMData.BirdsPerNipples = 0;
                        if (txtBirdsNipples1.Text != "")
                            projMData.BirdsPerNipples1 = Convert.ToDecimal(txtBirdsNipples1.Text);
                        else
                            projMData.BirdsPerNipples1 = 0;
                        if (txtBirdsPan.Text != "")
                            projMData.BirdsPerPan = Convert.ToDecimal(txtBirdsPan.Text);
                        else
                            projMData.BirdsPerPan = 0;
                        if (txtBirdsPan1.Text != "")
                            projMData.BirdsPerPan1 = Convert.ToDecimal(txtBirdsPan1.Text);
                        else
                            projMData.BirdsPerPan1 = 0;
                        if (txtRecLenOfLinesFt.Text != "")
                            projMData.RecLengthOfLinesft = Convert.ToDecimal(txtRecLenOfLinesFt.Text);
                        else
                            projMData.RecLengthOfLinesft = 0;
                        if (txtNipples10FT.Text != "")
                            projMData.NipplesIn10FTSegment = Convert.ToDecimal(txtNipples10FT.Text);
                        else
                            projMData.NipplesIn10FTSegment = 0;
                        if (txtNipples10FT1.Text != "")
                            projMData.NipplesIn10FTSegment1 = Convert.ToDecimal(txtNipples10FT1.Text);
                        else
                            projMData.NipplesIn10FTSegment1 = 0;
                        if (txtNoOfNipplesLines.Text != "")
                            projMData.NoOfNipplesLines = Convert.ToDecimal(txtNoOfNipplesLines.Text);
                        else
                            projMData.NoOfNipplesLines = 0;
                        if (txtPANS15SEGMENT.Text != "")
                            projMData.PansIn15FTSegment = Convert.ToDecimal(txtPANS15SEGMENT.Text);
                        else
                            projMData.PansIn15FTSegment = 0;
                        if (txtPANS15SEGMENT1.Text != "")
                            projMData.PansIn15FTSegment1 = Convert.ToDecimal(txtPANS15SEGMENT1.Text);
                        else
                            projMData.PansIn15FTSegment1 = 0;
                        if (txtNoOfFeedLinesLoops.Text != "")
                            projMData.NoOfFeedLinesLoops = Convert.ToDecimal(txtNoOfFeedLinesLoops.Text);
                        else
                            projMData.NoOfFeedLinesLoops = 0;
                        if (txtSuspensionPoints.Text != "")
                            projMData.SuspentionPointsAtEveryFt = Convert.ToDecimal(txtSuspensionPoints.Text);
                        else
                            projMData.SuspentionPointsAtEveryFt = 0;
                        if (txtTotalAmt.Text != "")
                            projMData.TotalAmt = Convert.ToDecimal(txtTotalAmt.Text);
                        else
                            projMData.TotalAmt = 0;
                        if (txtGSTAmt.Text != "")
                            projMData.GSTAmt = Convert.ToDecimal(txtGSTAmt.Text);
                        else
                            projMData.GSTAmt = 0;
                        if (txtTotalHP.Text != "")
                            projMData.TotalHP = Convert.ToDecimal(txtTotalHP.Text);
                        else
                            projMData.TotalHP = 0;
                        if (txtNetAmt.Text != "")
                            projMData.NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                        else
                            projMData.NetAmt = 0;
                        projMData.CompId = CommonMethod.CompId;
                        projMData.TransactionYear = CommonMethod.TransactionYear;
                        projectGMRepo.Edit(projMData);
                        projectGMRepo.Save();
                        updateId = Convert.ToInt32(projMData.Id);
                        if (deleteId.Count > 0)
                        {
                            for (int x = 0; x < deleteId.Count; x++)
                            {
                                ProjectGlanceDetailRepository projectDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = projectDRepo.GetById(deleteId[x]);
                                projectDRepo.Remove(projDData);
                                projectDRepo.Save();
                            }
                        }
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value) != 0)
                            {
                                ProjectGlanceDetailRepository projectDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = projectDRepo.GetById(Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value));
                                projDData.CodeNo = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                projDData.Description = Convert.ToString(GvProductInfo.Rows[i].Cells["DescriptionSystem"].Value);
                                projDData.InvestBird = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["InvestBird"].Value);
                                projDData.InvestmentPerHouse = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["InvPerBird"].Value);
                                projDData.HP = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["HP"].Value);
                                projDData.SGSTCGSTPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTnCGST"].Value);
                                projDData.IGstPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                projDData.MasterId = updateId;
                                projectDRepo.Edit(projDData);
                                projectDRepo.Save();
                            }
                            else
                            {
                                ProjectGlanceDetailRepository projectDRepo = new ProjectGlanceDetailRepository();
                                tblProjectGlanceDetail projDData = new tblProjectGlanceDetail();
                                projDData.CodeNo = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                projDData.Description = Convert.ToString(GvProductInfo.Rows[i].Cells["DescriptionSystem"].Value);
                                projDData.InvestBird = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["InvestBird"].Value);
                                projDData.InvestmentPerHouse = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["InvPerBird"].Value);
                                projDData.HP = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["HP"].Value);
                                projDData.SGSTCGSTPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTnCGST"].Value);
                                projDData.IGstPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                projDData.MasterId = updateId;
                                projectDRepo.Add(projDData);
                                projectDRepo.Save();
                            }
                        }
                        MessageBox.Show("Record Update Successfully");
                        clear();
                    }
                    projectGMRepo.Dispose();
                }
                else
                {
                    if (txtRefChNo.Text == "")
                    {
                        MessageBox.Show("Select Reference No");
                        this.ActiveControl = txtRefChNo;
                    }
                    else if (cmbCustomer.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbCustomer;
                    }
                    else
                    {
                        MessageBox.Show("Please add at least one Description System");
                        this.ActiveControl = txtCode;
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
                if (e.ColumnIndex == 8)
                {
                    if (btnUpdate.Enabled == true && GvProductInfo.Rows.Count > 1)
                    {
                        decimal totalAmt = 0, totSCGSTAmt = 0, totIGSTAmt = 0, totalHP = 0, totalGST = 0, totalNetAmt = 0;
                        if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                            deleteId.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                        totalAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["InvPerBird"].Value));
                        totSCGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["SCGSTAmt"].Value));
                        totIGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["IGSTAmt"].Value));
                        totalGST = totSCGSTAmt + totIGSTAmt;
                        totalHP = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["HP"].Value));
                        txtTotalAmt.Text = Convert.ToString(Math.Round(totalAmt, 2, MidpointRounding.AwayFromZero));
                        txtGSTAmt.Text = Convert.ToString(Math.Round(totalGST, 2, MidpointRounding.AwayFromZero));
                        txtTotalHP.Text = Convert.ToString(Math.Round(totalHP, 2, MidpointRounding.AwayFromZero));
                        totalNetAmt = totalAmt + totalGST;
                        txtNetAmt.Text = Convert.ToString(Math.Round(totalNetAmt, 2, MidpointRounding.AwayFromZero));
                        this.ActiveControl = txtCode;
                    }
                    else if (btnsave.Enabled == true)
                    {
                        decimal totalAmt = 0, totSCGSTAmt = 0, totIGSTAmt = 0, totalHP = 0, totalGST = 0, totalNetAmt = 0;
                        GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                        totalAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["InvPerBird"].Value));
                        totSCGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["SCGSTAmt"].Value));
                        totIGSTAmt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["IGSTAmt"].Value));
                        totalGST = totSCGSTAmt + totIGSTAmt;
                        totalHP = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["HP"].Value));
                        txtTotalAmt.Text = Convert.ToString(Math.Round(totalAmt, 2, MidpointRounding.AwayFromZero));
                        txtGSTAmt.Text = Convert.ToString(Math.Round(totalGST, 2, MidpointRounding.AwayFromZero));
                        txtTotalHP.Text = Convert.ToString(Math.Round(totalHP, 2, MidpointRounding.AwayFromZero));
                        totalNetAmt = totalAmt + totalGST;
                        txtNetAmt.Text = Convert.ToString(Math.Round(totalNetAmt, 2, MidpointRounding.AwayFromZero));
                        this.ActiveControl = txtCode;
                    }
                    else
                        MessageBox.Show("This Quotation has only 1 product." + Environment.NewLine + "You have to delete full Order.", "Warning");
                }
                else
                {
                    txtCode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Code"].Value);
                    cmbDescription.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["DescriptionSystem"].Value);
                    txtInvestBirds.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["InvestBird"].Value);
                    txtINVPERHouse.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["InvPerBird"].Value);
                    txtHP.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["HP"].Value);
                    cmbCGST.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGSTnCGST"].Value);
                    cmbIGST.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                    colIndex = e.RowIndex;
                }
            }
            catch (Exception)
            { }
        }
    }
}
