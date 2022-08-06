using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmRateSlab : Form
    {
        private int updateID = 0, dataCount = 0, rowIndex = -1, deleteID = 0;
        private string deletedId = "";

        public frmRateSlab()
        {
            InitializeComponent();
            clearFormControl();
            fillSlab();
            dtgRateSlab.Rows.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearFormControl();
            btnSave.Text = "Save";
            updateID = 0;
            deleteID = 0;
            deletedId = "";
            rowIndex = -1;
            dtgRateSlab.Rows.Clear();
            this.ActiveControl = txtSlab;
        }

        private void clearFormControl()
        {
            txtFrom.Text = "";
            txtTo.Text = "";
            txtID.Text = "";
            txtRate.Text = "";
            txtSlab.Text = "";
            dtgRateSlab.Rows.Clear();
            getMaxID();
        }

        private void getMaxID()
        {
            try
            {
                RateSlabRepository rateslabRepo = new RateSlabRepository();
                var mid = rateslabRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count();
                if (mid == null)
                    txtID.Text = "1";
                else
                    txtID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                rateslabRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtSlab.Text) && updateID == 0) || (updateID != 0))
                {
                    RateSlabRepository rateslabreponew = new RateSlabRepository();
                    RateSlabDetailRepository rateSlabDtlRepo = new RateSlabDetailRepository();
                    if (updateID != 0)
                    {
                        //------------- to delete from rateslab detail -----------//
                        if (deletedId != "")
                        {
                            if (deletedId.Contains(','))
                            {
                                string[] strdeleteID = deletedId.Split(',');
                                for (int i = 0; i < strdeleteID.Length; i++)
                                {
                                    RateSlabDetailRepository rateslabdtlREPO = new RateSlabDetailRepository();
                                    RateSlabDetail slabDetailData = rateslabdtlREPO.GetAll().Where(t => t.ID == Convert.ToInt32(strdeleteID[i])).FirstOrDefault();
                                    if (slabDetailData != null)
                                    {
                                        rateslabdtlREPO.Remove(slabDetailData);
                                        rateslabdtlREPO.Save();
                                    }
                                    rateslabdtlREPO.Dispose();
                                }
                            }
                            else
                            {
                                RateSlabDetailRepository rateslabdtlREPO = new RateSlabDetailRepository();
                                RateSlabDetail slabDetailData = rateslabdtlREPO.GetAll().Where(t => t.ID == Convert.ToInt32(deletedId)).FirstOrDefault();
                                if (slabDetailData != null)
                                {
                                    rateslabdtlREPO.Remove(slabDetailData);
                                    rateslabdtlREPO.Save();
                                }
                                rateslabdtlREPO.Dispose();
                            }
                        }
                        RateSlab rateslabinfo = rateslabreponew.GetAll().Where(t => t.ID == updateID).FirstOrDefault();
                        rateslabinfo.SlabDescription = cmbSlabDescription.Text;
                        rateslabreponew.Edit(rateslabinfo);
                        rateslabreponew.Save();

                        for (int i = 0; i < dtgRateSlab.Rows.Count; i++)
                        {
                            RateSlabDetail rateslabdtl = rateSlabDtlRepo.GetAll().Where(t => t.MasterID == updateID && t.ID == Convert.ToInt32(dtgRateSlab.Rows[i].Cells["DtlId"].Value)).FirstOrDefault();
                            if (rateslabdtl != null)
                            {
                                rateslabdtl.RangeFrom = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyFrom"].Value);
                                rateslabdtl.RangeTo = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyTo"].Value);
                                rateslabdtl.AppliedRate = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["Rate"].Value); ;
                                rateSlabDtlRepo.Edit(rateslabdtl);
                                rateSlabDtlRepo.Save();
                            }
                            else
                            {
                                RateSlabDetail rateslabdtlnew = new RateSlabDetail();
                                rateslabdtlnew.MasterID = updateID;
                                rateslabdtlnew.RangeFrom = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyFrom"].Value);
                                rateslabdtlnew.RangeTo = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyTo"].Value);
                                rateslabdtlnew.AppliedRate = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["Rate"].Value);
                                rateSlabDtlRepo.Add(rateslabdtlnew);
                                rateSlabDtlRepo.Save();
                            }
                        }
                        rateSlabDtlRepo.Dispose();
                        MessageBox.Show("Record Updated Successfully.");
                    }
                    else
                    {
                        RateSlab rateslabinfo = new RateSlab();
                        rateslabinfo.SlabDescription = txtSlab.Text;
                        rateslabinfo.SlabID = Convert.ToInt32(txtID.Text);
                        rateslabinfo.CompId = CommonMethod.CompId;
                        rateslabreponew.Add(rateslabinfo);
                        rateslabreponew.Save();
                        int slabmasterid = rateslabinfo.ID;

                        for (int i = 0; i < dtgRateSlab.Rows.Count; i++)
                        {
                            RateSlabDetail rateslabdtlnew = new RateSlabDetail();
                            rateslabdtlnew.MasterID = slabmasterid;
                            rateslabdtlnew.RangeFrom = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyFrom"].Value);
                            rateslabdtlnew.RangeTo = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyTo"].Value);
                            rateslabdtlnew.AppliedRate = Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["Rate"].Value);
                            rateSlabDtlRepo.Add(rateslabdtlnew);
                            rateSlabDtlRepo.Save();
                        }
                        MessageBox.Show("Record Saved Successfully.");
                    }
                    btnDelete.Enabled = true;
                    clearFormControl();
                    fillSlab();
                    btnSave.Text = "Save";
                    this.ActiveControl = btnNew;
                }
                else
                {
                    MessageBox.Show("Please Enter All Information.", "Warning");
                    this.ActiveControl = btnSave;
                }
            }
            catch (Exception)
            { }
        }

        private void fillSlab()
        {
            try
            {
                RateSlabRepository slabrepo = new RateSlabRepository();
                List<RateSlab> listdata = new List<RateSlab>();
                listdata.Clear();
                listdata = slabrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SlabDescription).ToList();
                dataCount = listdata.Count;
                listdata.Add(new RateSlab { SlabDescription = "Select", ID = 0 });
                listdata.Reverse();
                cmbSlabDescription.DataSource = listdata;
                cmbSlabDescription.ValueMember = "ID";
                cmbSlabDescription.DisplayMember = "SlabDescription";
                slabrepo.Dispose();
                cmbSlabDescription.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                RateSlabRepository rateslabrepo = new RateSlabRepository();
                RateSlabDetailRepository rateslabdtlrepo = new RateSlabDetailRepository();
                List<RateSlabDetail> slabList = new List<RateSlabDetail>();
                slabList = rateslabdtlrepo.GetAll().Where(t => t.MasterID == deleteID).ToList();
                if (slabList.Count > 0)
                {
                    foreach (var item in slabList)
                    {
                        RateSlabDetail rateslabDtl = rateslabdtlrepo.GetAll().Where(t => t.ID == item.ID).FirstOrDefault();
                        if (rateslabDtl != null)
                        {
                            rateslabdtlrepo.Remove(rateslabDtl);
                            rateslabdtlrepo.Save();
                        }
                    }
                }
                RateSlab rateslabdata = rateslabrepo.GetAll().Where(t => t.ID == deleteID && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (rateslabdata != null)
                {
                    rateslabrepo.Remove(rateslabdata);
                    rateslabrepo.Save();
                }
                rateslabdtlrepo.Dispose();
                rateslabrepo.Dispose();
                MessageBox.Show("Record Deleted Successfully.");
            }
            catch (Exception)
            { }
            clearFormControl();
            fillSlab();
            deleteID = 0;
            btnSave.Text = "Save";
        }

        private void dtgRateSlab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                updateID = Convert.ToInt32(cmbSlabDescription.SelectedValue);
                if (e.ColumnIndex == 5)  // remove button
                {
                    if (Convert.ToInt32(dtgRateSlab.Rows[e.RowIndex].Cells["DtlId"].Value) != 0)
                    {
                        if (deletedId.Trim() != "")
                            deletedId = deletedId + ',' + Convert.ToString(dtgRateSlab.Rows[e.RowIndex].Cells["DtlId"].Value);
                        else
                            deletedId = Convert.ToString(dtgRateSlab.Rows[e.RowIndex].Cells["DtlId"].Value);
                        dtgRateSlab.Rows.Remove(dtgRateSlab.Rows[e.RowIndex]);
                        btnSave.Text = "Update";
                    }
                }
                else
                {
                    txtFrom.Text = dtgRateSlab.Rows[e.RowIndex].Cells["QtyFrom"].Value.ToString();
                    txtTo.Text = dtgRateSlab.Rows[e.RowIndex].Cells["QtyTo"].Value.ToString();
                    txtRate.Text = dtgRateSlab.Rows[e.RowIndex].Cells["Rate"].Value.ToString();
                    btnSave.Text = "Update";
                    txtSlab.Text = cmbSlabDescription.Text;
                    rowIndex = e.RowIndex;
                }
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
            else if (keyData == Keys.F3)
            {
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtTo_Leave(object sender, EventArgs e)
        {
            if (txtFrom.Text == "") { txtFrom.Text = "0"; }
            if (txtTo.Text == "") { txtTo.Text = "0"; }
            if (Convert.ToDecimal(txtFrom.Text) >= Convert.ToDecimal(txtTo.Text))
            {
                MessageBox.Show("Please enter proper slab range.");
                txtTo.Text = "";
                this.ActiveControl = txtTo;
            }
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46) && (e.KeyChar != 13))
                e.Handled = true;
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46) && (e.KeyChar != 13))
                e.Handled = true;
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46) && (e.KeyChar != 13))
                e.Handled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text != "" && txtFrom.Text == "0")
            {
                MessageBox.Show("Please enter proper from slab range.");
                txtFrom.Text = "";
                this.ActiveControl = txtFrom;
            }
            else if (txtTo.Text != "" && txtTo.Text == "0")
            {
                MessageBox.Show("Please enter proper to slab range.");
                txtTo.Text = "";
                this.ActiveControl = txtTo;
            }
            else if (txtRate.Text == "0" && txtRate.Text == "")
            {
                MessageBox.Show("Please enter proper rate value.");
                txtRate.Text = "";
                this.ActiveControl = txtRate;
            }
            else
            {
                bool isValid = true;
                if (updateID == 0)
                {
                    for (int i = 0; i < dtgRateSlab.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(txtFrom.Text) >= Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyFrom"].Value) && Convert.ToDecimal(txtFrom.Text) <= Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyTo"].Value))
                            isValid = false;
                        if (Convert.ToDecimal(txtTo.Text) >= Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyFrom"].Value) && Convert.ToDecimal(txtTo.Text) <= Convert.ToDecimal(dtgRateSlab.Rows[i].Cells["QtyTo"].Value))
                            isValid = false;
                    }
                }
                if (isValid == true)
                {
                    if ((updateID == 0) || (updateID != 0 && rowIndex == -1))
                    {
                        dtgRateSlab.Rows.Add();
                        int rowcount = dtgRateSlab.Rows.Count;
                        dtgRateSlab.Rows[rowcount - 1].Cells["Id"].Value = rowcount;
                        dtgRateSlab.Rows[rowcount - 1].Cells["QtyFrom"].Value = txtFrom.Text;
                        dtgRateSlab.Rows[rowcount - 1].Cells["QtyTo"].Value = txtTo.Text;
                        dtgRateSlab.Rows[rowcount - 1].Cells["Rate"].Value = txtRate.Text;
                        dtgRateSlab.Rows[rowcount - 1].Cells["DtlId"].Value = "0";
                    }
                    else
                    {
                        if (rowIndex != -1)
                        {
                            dtgRateSlab.Rows[rowIndex].Cells["QtyFrom"].Value = txtFrom.Text;
                            dtgRateSlab.Rows[rowIndex].Cells["QtyTo"].Value = txtTo.Text;
                            dtgRateSlab.Rows[rowIndex].Cells["Rate"].Value = txtRate.Text;
                        }
                    }
                    txtFrom.Text = "";
                    txtTo.Text = "";
                    txtRate.Text = "";
                    this.ActiveControl = txtFrom;
                }
                else
                {
                    MessageBox.Show("Slab Already Exist!!");
                    txtFrom.Text = "";
                    txtTo.Text = "";
                    txtRate.Text = "";
                    this.ActiveControl = txtFrom;
                }
            }
        }

        private void cmbSlabDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbSlabDescription.Items.Count > 0)
                {
                    if (Convert.ToInt32(cmbSlabDescription.SelectedValue) != 0)
                    {
                        deleteID = Convert.ToInt32(cmbSlabDescription.SelectedValue);
                        updateID = Convert.ToInt32(cmbSlabDescription.SelectedValue);
                        txtSlab.Text = cmbSlabDescription.Text;
                        btnSave.Text = "Update";
                        fillSlabDetails(Convert.ToInt32(cmbSlabDescription.SelectedValue));
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void fillSlabDetails(int slabValue)
        {
            try
            {
                dtgRateSlab.Rows.Clear();
                RateSlabDetailRepository rateslbrepo = new RateSlabDetailRepository();
                RateSlabDetail rateslabinfo = new RateSlabDetail();
                List<RateSlabDetail> slabList = rateslbrepo.GetAll().Where(t => t.MasterID == slabValue).ToList();
                if (slabList.Count > 0)
                {
                    int i = 0;
                    foreach (var item in slabList)
                    {
                        dtgRateSlab.Rows.Add();
                        dtgRateSlab.Rows[i].Cells["Id"].Value = i + 1;
                        dtgRateSlab.Rows[i].Cells["QtyFrom"].Value = item.RangeFrom == null ? 0 : item.RangeFrom;
                        dtgRateSlab.Rows[i].Cells["QtyTo"].Value = item.RangeTo == null ? 0 : item.RangeTo;
                        dtgRateSlab.Rows[i].Cells["Rate"].Value = item.AppliedRate == null ? 0 : item.AppliedRate;
                        dtgRateSlab.Rows[i].Cells["DtlId"].Value = item.ID;
                        i++;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtSlab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ActiveControl = txtFrom;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clearFormControl();
            fillSlab();
            updateID = 0;
            deleteID = 0;
            btnSave.Text = "Save";
            if (dataCount > 0)
            {
                cmbSlabDescription.SelectedIndex = 0;
                updateID = Convert.ToInt32(cmbSlabDescription.SelectedValue);
                fillSlabDetails(updateID);
                deleteID = updateID;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSlabDescription.Text != "")
                {
                    dtgRateSlab.Rows.Clear();
                    RateSlabDetailRepository rateslbrepo = new RateSlabDetailRepository();
                    RateSlabDetail rateslabinfo = new RateSlabDetail();
                    List<RateSlabDetail> slabList = rateslbrepo.GetAll().Where(t => t.MasterID == Convert.ToInt32(cmbSlabDescription.SelectedValue)).ToList();
                    if (slabList.Count > 0)
                    {
                        int i = 0;
                        foreach (var item in slabList)
                        {
                            dtgRateSlab.Rows.Add();
                            dtgRateSlab.Rows[i].Cells["Id"].Value = i + 1;
                            dtgRateSlab.Rows[i].Cells["QtyFrom"].Value = item.RangeFrom == null ? 0 : item.RangeFrom;
                            dtgRateSlab.Rows[i].Cells["QtyTo"].Value = item.RangeTo == null ? 0 : item.RangeTo;
                            dtgRateSlab.Rows[i].Cells["Rate"].Value = item.AppliedRate == null ? 0 : item.AppliedRate;
                            dtgRateSlab.Rows[i].Cells["DtlId"].Value = item.ID;
                            i++;
                        }
                    }
                    rateslbrepo.Dispose();
                }
                else
                {
                    MessageBox.Show("Please select slab description.", "Warning");
                    this.ActiveControl = cmbSlabDescription;
                }
            }
            catch (Exception)
            { }
        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtTo;
            }
            catch (Exception) { }
        }

        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtRate;
            }
            catch (Exception) { }
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception) { }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
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