using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSlabMaster : Form
    {
        DbClass db = new DbClass();
        int updateSId;
        private bool newRecord = false;

        public frmSlabMaster()
        {
            InitializeComponent();
            newRecord = false;
            //cmbDescription.Visible = true;
            cmbDescription.Text = "";
            maxid();
            BindGrid();
            this.ActiveControl = btnNew;
        }

        public void maxid()
        {
            try
            {
                SlabMasterRepository masterRepo = new SlabMasterRepository();
                var maxSId = masterRepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).Max(t => t.SId);
                if (maxSId == null)
                    txtId.Text = "1";
                else
                {
                    int n = Convert.ToInt32(maxSId) + 1;
                    txtId.Text = Convert.ToString(n);
                }
            }
            catch (Exception) { }
        }

        public void clear()
        {
            try
            {
                maxid();
                txtDescription.Text = "";
                txtQtySlab.Text = "";
                cmbUnit.Text = "";
                dtgSizeRange.Rows.Clear();
                //cmbDescription.Visible = false;
                btnSave.Text = "Save";
            }
            catch (Exception) { }
        }

        public void BindGrid()
        {
            try
            {
                SlabMasterRepository slabrepo = new SlabMasterRepository();
                List<SlabMaster> listdata = slabrepo.GetAll().Where(t => t.SizeGroup == "Fixed Size" && t.CompId == CommonMethod.CompId).ToList();
                GVSlabmaster.Rows.Clear();
                if (listdata.Count != 0)
                {
                    int i = 0;
                    foreach (var item in listdata)
                    {
                        GVSlabmaster.Rows.Add();
                        GVSlabmaster.Rows[i].Cells[0].Value = item.Id;
                        GVSlabmaster.Rows[i].Cells[1].Value = item.Description;
                        GVSlabmaster.Rows[i].Cells[2].Value = item.QtySlab;
                        if (item.Unit == "In")
                            GVSlabmaster.Rows[i].Cells[3].Value = "Inch";
                        if (item.Unit == "Ft")
                            GVSlabmaster.Rows[i].Cells[3].Value = "Feet";
                        GVSlabmaster.Rows[i].Cells[4].Value = item.SId;
                        i++;
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveUpdate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtQtySlab.Text))
                {
                    SlabMasterRepository slabrepo = new SlabMasterRepository();
                    SlabMaster slabinfo = new SlabMaster();
                    slabinfo = slabrepo.GetAll().Where(t => t.Id == updateSId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (slabinfo != null)
                    {
                        slabrepo.Remove(slabinfo);
                        slabrepo.Save();
                    }
                    SlabDetailRepository slabdetailrepo = new SlabDetailRepository();
                    SlabDetail slabdtldata = new SlabDetail();
                    slabdtldata = slabdetailrepo.GetAll().Where(t => t.ID == updateSId).FirstOrDefault();
                    if (slabdtldata != null)
                    {
                        slabdetailrepo.Remove(slabdtldata);
                        slabdetailrepo.Save();
                    }
                    btnDelete.Enabled = true;
                    BindGrid();
                    newRecord = false;
                    //this.ActiveControl = cmbSlabGroup;
                }
            }
            catch (Exception) { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                newRecord = true;
                //this.ActiveControl = cmbSlabGroup;
            }
            catch (Exception) { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnDynamicSize.Checked==true && ((newRecord == true && txtDescription.Text != "") || (newRecord == false && cmbDescription.Text != "")) && txtFrom.Text != "" && txtTo.Text != "")
                {
                    dtgSizeRange.Rows.Add();
                    int rowcount = dtgSizeRange.Rows.Count - 1;
                    dtgSizeRange.Rows[rowcount].Cells[0].Value = txtFrom.Text;
                    dtgSizeRange.Rows[rowcount].Cells[1].Value = txtTo.Text;
                    txtFrom.Text = "";
                    txtTo.Text = "";
                    this.ActiveControl = txtFrom;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbSlabGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDescription.Text = "";
            if (cmbSlabGroup.Text == "Fixed Size")
            {
                cmbDescription.Visible = false;
                //pnlDynamic.Visible = false;
            }
            else if (cmbSlabGroup.Text == "Dynamic Size")
            {
                //pnlDynamic.Visible = true;
                if (newRecord == false)
                {
                    btnSave.Text = "Update";
                    cmbDescription.Visible = true;
                    fillcombo();
                }
                else
                {
                    btnSave.Text = "Save";
                    this.ActiveControl = txtDescription;
                }
            }
        }

        private void fillcombo()
        {
            try
            {
                SlabMasterRepository slabrepo = new SlabMasterRepository();
                List<SlabMaster> cdata = slabrepo.GetAll().Where(t => t.SizeGroup == "Dynamic Size" && t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Description).ToList();
                if (cdata != null && cdata.Count != 0)
                {
                    cdata.Reverse();
                    cmbDescription.DataSource = cdata;
                    cmbDescription.ValueMember = "Id";
                    cmbDescription.DisplayMember = "Description";
                    slabrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SlabDetailRepository slabrepo = new SlabDetailRepository();
                List<SlabDetail> listdata = slabrepo.GetAll().Where(t => t.SlabId == Convert.ToInt32(cmbDescription.SelectedValue)).ToList();
                dtgSizeRange.Rows.Clear();
                if (listdata.Count != 0)
                {
                    updateSId = Convert.ToInt32(cmbDescription.SelectedValue);
                    int i = 0;
                    foreach (var item in listdata)
                    {
                        dtgSizeRange.Rows.Add();
                        int rowcount = dtgSizeRange.Rows.Count - 1;
                        dtgSizeRange.Rows[rowcount].Cells[0].Value = item.FromRange;
                        dtgSizeRange.Rows[rowcount].Cells[1].Value = item.ToRange;
                        dtgSizeRange.Rows[rowcount].Cells[2].Value = item.ID;
                        i++;
                    }
                    btnSave.Text = "Update";
                }
            }
            catch (Exception) { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            newRecord = false;
            clear();
            if (rdbtnDynamicSize.Checked==true)
            {
                cmbDescription.Visible = true;
                fillcombo();
                this.ActiveControl = cmbDescription;
            }
        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 && e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 47)
                e.Handled = true;
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 && e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 47)
                e.Handled = true;
        }

        private void cmbDescription_Leave(object sender, EventArgs e)
        {
            if (cmbDescription.Text != "")
                this.ActiveControl = txtFrom;
        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && txtFrom.Text != "")
                this.ActiveControl = txtTo;
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            this.ActiveControl = txtFrom;
        }

        private void txtTo_Leave(object sender, EventArgs e)
        {
            //if (Convert.ToDecimal(txtTo.Text) <= Convert.ToDecimal(txtFrom.Text))
            //    MessageBox.Show("Please enter to range greater than from range.", "Warning");
        }

        private void cmbSlabGroup_Leave(object sender, EventArgs e)
        {
            //if (newRecord == false && cmbSlabGroup.Text == "Dynamic Size")
            //   this.ActiveControl= cmbDescription;
            //else if (newRecord == true)
            //  this.ActiveControl=  txtDescription;            
        }

        private void txtQtySlab_Leave(object sender, EventArgs e)
        {
            if (txtQtySlab.Text != "")
                this.ActiveControl = cmbUnit;
            else if (rdbtnDynamicSize.Checked==true)
                this.ActiveControl = txtQtySlab;
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            if (cmbUnit.Text == "")
                this.ActiveControl = cmbUnit;
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
                SaveUpdate();
                this.ActiveControl = txtDescription;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SaveUpdate()
        {
            try
            {
                //if (cmbSlabGroup.Text == "Fixed Size")
                if(rdbtnFixedSize.Checked==true)
                {
                    if (!string.IsNullOrEmpty(txtQtySlab.Text) && cmbUnit.Text != "")
                    {
                        if (btnSave.Text == "Save")
                        {
                            SlabMasterRepository slabRepo1 = new SlabMasterRepository();
                            var slabData1 = slabRepo1.GetAll().Where(t => t.SId == Convert.ToInt32(txtId.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (slabData1 != null)
                            {
                                MessageBox.Show("Already Slab Name Exist.", "Warning");
                                this.ActiveControl = txtDescription;
                            }
                            else
                            {
                                SlabMasterRepository slabrepo = new SlabMasterRepository();
                                SlabMaster slabdata = new SlabMaster();
                                slabdata.SId = Convert.ToInt32(txtId.Text);
                                slabdata.QtySlab = Convert.ToDecimal(txtQtySlab.Text);
                                slabdata.Description = Convert.ToString(txtDescription.Text);
                                if (cmbUnit.Text == "Inch")
                                    slabdata.Unit = "In";
                                if (cmbUnit.Text == "Feet")
                                    slabdata.Unit = "Ft";
                                slabdata.SizeGroup = rdbtnFixedSize.Text;
                                slabdata.CompId = CommonMethod.CompId;
                                slabrepo.Add(slabdata);
                                slabrepo.Save();

                                MessageBox.Show("Slab Saved Successfully.", "Success");
                                BindGrid();
                                maxid();
                                clear();
                                this.ActiveControl = txtDescription;
                                slabrepo.Dispose();
                            }
                        }
                        else
                        {
                            SlabMasterRepository slabrepo = new SlabMasterRepository();
                            SlabMaster slabdata = slabrepo.GetById(updateSId);
                            slabdata.SId = Convert.ToInt32(txtId.Text);
                            slabdata.QtySlab = Convert.ToDecimal(txtQtySlab.Text);
                            slabdata.Description = Convert.ToString(txtDescription.Text);

                            if (cmbUnit.Text == "Inch")
                                slabdata.Unit = "In";
                            if (cmbUnit.Text == "Feet")
                                slabdata.Unit = "Ft";
                            slabdata.SizeGroup = rdbtnFixedSize.Text;
                            slabdata.CompId = CommonMethod.CompId;
                            slabrepo.Edit(slabdata);
                            slabrepo.Save();

                            MessageBox.Show("Slab Updated Successfully.", "Success");
                            BindGrid();
                            clear();
                            maxid();
                            this.ActiveControl = txtDescription;
                            slabrepo.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Quantity Slab.", "Warning");
                        this.ActiveControl = txtQtySlab;
                    }
                }
                else // Dynamic Size
                {
                    //if (cmbSlabGroup.Text != "")
                    //{
                        if (btnSave.Text == "Save")
                        {
                            SlabMasterRepository slabRepo1 = new SlabMasterRepository();
                            var slabData1 = slabRepo1.GetAll().Where(t => t.SId == Convert.ToInt32(txtId.Text)).FirstOrDefault();
                            if (slabData1 != null)
                            {
                                MessageBox.Show("Slab Already Exist.", "Warning");
                                this.ActiveControl = txtDescription;
                            }
                            else
                            {
                                SlabMasterRepository slabrepo = new SlabMasterRepository();
                                SlabMaster slabdata = new SlabMaster();
                                slabdata.SId = Convert.ToInt32(txtId.Text);
                                slabdata.QtySlab = 0;
                                slabdata.Description = Convert.ToString(txtDescription.Text);
                                slabdata.ValueCount = 0;
                                slabdata.SizeGroup = rdbtnDynamicSize.Text;
                                slabdata.CompId = CommonMethod.CompId;
                                slabrepo.Add(slabdata);
                                slabrepo.Save();
                                int slab_Id = slabdata.Id;

                                SlabDetailRepository slabdtlrepo = new SlabDetailRepository();
                                for (int i = 0; i < dtgSizeRange.Rows.Count; i++)
                                {
                                    SlabDetail slabdtldata = new SlabDetail();
                                    slabdtldata.SlabId = slab_Id;
                                    slabdtldata.FromRange = Convert.ToDecimal(dtgSizeRange.Rows[i].Cells[0].Value);
                                    slabdtldata.ToRange = Convert.ToDecimal(dtgSizeRange.Rows[i].Cells[1].Value);
                                    slabdtlrepo.Add(slabdtldata);
                                    slabdtlrepo.Save();
                                }
                                slabdtlrepo.Dispose();
                                MessageBox.Show("Slab Saved Successfully.", "Success");
                                clear();
                                btnSave.Text = "Update";
                                slabrepo.Dispose();
                            }
                        }
                        else if (cmbDescription.Text != "")
                        {
                            SlabMasterRepository slabrepo = new SlabMasterRepository();
                            SlabMaster slabdata = slabrepo.GetById(updateSId);
                            if (slabdata != null)
                            {
                                slabdata.QtySlab = 0;
                                slabdata.Description = cmbDescription.Text;

                                if (cmbUnit.Text == "Inch")
                                    slabdata.Unit = "In";
                                if (cmbUnit.Text == "Feet")
                                    slabdata.Unit = "Ft";
                                slabdata.SizeGroup = rdbtnDynamicSize.Text;
                                slabdata.CompId = CommonMethod.CompId;
                                slabrepo.Edit(slabdata);
                                slabrepo.Save();

                                for (int i = 0; i < dtgSizeRange.Rows.Count; i++)
                                {
                                    SlabDetailRepository slabdtlrepo = new SlabDetailRepository();
                                    SlabDetail slabdtldatanew = slabdtlrepo.GetAll().Where(t => t.SlabId == updateSId && t.ID == Convert.ToInt32(dtgSizeRange.Rows[i].Cells[2].Value)).FirstOrDefault();
                                    if (slabdtldatanew != null)
                                    {
                                        slabdtldatanew.FromRange = Convert.ToDecimal(dtgSizeRange.Rows[i].Cells[0].Value);
                                        slabdtldatanew.ToRange = Convert.ToDecimal(dtgSizeRange.Rows[i].Cells[1].Value);
                                        slabdtlrepo.Edit(slabdtldatanew);
                                        slabdtlrepo.Save();
                                        slabdtlrepo.Dispose();
                                    }
                                    else
                                    {
                                        SlabDetail slabdtlData = new SlabDetail();
                                        slabdtlData.SlabId = updateSId;
                                        slabdtlData.FromRange = Convert.ToDecimal(dtgSizeRange.Rows[i].Cells[0].Value);
                                        slabdtlData.ToRange = Convert.ToDecimal(dtgSizeRange.Rows[i].Cells[1].Value);
                                        slabdtlrepo.Add(slabdtlData);
                                        slabdtlrepo.Save();
                                        slabdtlrepo.Dispose();
                                    }
                                }

                                MessageBox.Show("Slab Updated Successfully.", "Success");
                                BindGrid();
                                clear();
                                maxid();
                                this.ActiveControl = cmbDescription;
                                btnSave.Text = "Save";
                            }
                            slabrepo.Dispose();
                        }
                        newRecord = false;
                        cmbDescription.Visible = true;
                        fillcombo();
                        this.ActiveControl = cmbDescription;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Select Slab Group.", "Warning");
                    //    this.ActiveControl = txtQtySlab;
                    //}
                }
                newRecord = false;
            }
            catch (Exception) { }
        }

        private void cmbSlabGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtDescription;
            }
            catch (Exception) { }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    //if (cmbSlabGroup.Text == "Fixed Size")
                    this.ActiveControl = txtQtySlab;
                    //else
                    //    this.ActiveControl = txtFrom;
                }
            }
            catch (Exception) { }
        }

        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception) { }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void txtQtySlab_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbUnit;
            }
            catch (Exception) { }
        }

        private void GVSlabmaster_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                updateSId = Convert.ToInt32(GVSlabmaster.Rows[e.RowIndex].Cells[0].Value);
                txtId.Text = Convert.ToString(GVSlabmaster.Rows[e.RowIndex].Cells[4].Value);
                txtQtySlab.Text = Convert.ToString(GVSlabmaster.Rows[e.RowIndex].Cells[2].Value);
                txtDescription.Text = Convert.ToString(GVSlabmaster.Rows[e.RowIndex].Cells[1].Value);
                if (Convert.ToString(GVSlabmaster.Rows[e.RowIndex].Cells[3].Value) == "Inch")
                    cmbUnit.SelectedIndex = 0;
                if (Convert.ToString(GVSlabmaster.Rows[e.RowIndex].Cells[3].Value) == "Feet")
                    cmbUnit.SelectedIndex = 1;
                btnSave.Text = "Update";
            }
            catch (Exception) { }
        }

        private void rdbtnFixedSize_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnFixedSize.Checked == true)
                grpQtySlab.Enabled = true;
            else
                grpQtySlab.Enabled = false;
        }

        private void rdbtnDynamicSize_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnDynamicSize.Checked == true)
                grpDynamicAdd.Enabled = true;
            else
                grpDynamicAdd.Enabled = false;
        }
    }
}