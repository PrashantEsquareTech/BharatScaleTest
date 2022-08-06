using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmTaxSlab : Form
    {
        public frmTaxSlab()
        {
            InitializeComponent();
            fillGrid();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                this.ActiveControl = txtDescription;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void ClearAll()
        {
            txtDescription.Text = "";
            txtFrom.Text = "";
            txtTo.Text = "";
            try
            {
                TaxSlabRepository taxslabRepo = new TaxSlabRepository();
                var mid = taxslabRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => (int?)t.Id);
                if (mid == null)
                    txtID.Text = "1";
                else
                    txtID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                taxslabRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void fillGrid()
        {
            try
            {
                dtgTaxSlab.Rows.Clear();
                TaxSlabRepository taxslabrepo = new TaxSlabRepository();
                TaxSlabMaster taxslabinfo = new TaxSlabMaster();
                List<TaxSlabMaster> slabList = taxslabrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (slabList.Count > 0)
                {
                    int i = 0;
                    foreach (var item in slabList)
                    {
                        dtgTaxSlab.Rows.Add();
                        dtgTaxSlab.Rows[i].Cells["ID"].Value = item.Id;
                        dtgTaxSlab.Rows[i].Cells["Description"].Value = item.Description;
                        dtgTaxSlab.Rows[i].Cells["FromAmt"].Value = item.SlabFrom;
                        dtgTaxSlab.Rows[i].Cells["ToAmt"].Value = item.SlabTo;
                        switch (Convert.ToInt32(item.GstPercent))
                        {
                            case 0:
                                dtgTaxSlab.Rows[i].Cells["GST"].Value = 0;
                                break;
                            case 1:
                                dtgTaxSlab.Rows[i].Cells["GST"].Value = 5;
                                break;
                            case 2:
                                dtgTaxSlab.Rows[i].Cells["GST"].Value = 12;
                                break;
                            case 3:
                                dtgTaxSlab.Rows[i].Cells["GST"].Value = 18;
                                break;
                            case 4:
                                dtgTaxSlab.Rows[i].Cells["GST"].Value = 28;
                                break;
                        }
                        i++;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFrom.Text) && !string.IsNullOrEmpty(txtTo.Text) && !string.IsNullOrEmpty(txtDescription.Text))
                {
                    TaxSlabRepository taxslabrepo = new TaxSlabRepository();
                    TaxSlabMaster pretaxslab = new TaxSlabMaster();
                    pretaxslab = taxslabrepo.GetAll().Where(t => t.SlabTo == Convert.ToInt32(txtFrom.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pretaxslab != null)
                    {
                        MessageBox.Show("Slab Already Exist.", "Warning");
                        txtFrom.Text = "0";
                        this.ActiveControl = txtFrom;
                    }
                    else
                    {
                        TaxSlabMaster taxslabinfo = new TaxSlabMaster();
                        taxslabinfo = taxslabrepo.GetAll().Where(t => t.SlabFrom == Convert.ToInt32(txtFrom.Text) && t.SlabTo == Convert.ToInt32(txtTo.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (taxslabinfo == null)
                        {
                            TaxSlabMaster taxSlabData = new TaxSlabMaster();
                            taxSlabData.Description = txtDescription.Text;
                            taxSlabData.SlabFrom = Convert.ToInt32(txtFrom.Text);
                            taxSlabData.SlabTo = Convert.ToInt32(txtTo.Text);
                            taxSlabData.GstPercent = cmbGst.SelectedIndex;
                            taxSlabData.CompId = CommonMethod.CompId;
                            taxslabrepo.Add(taxSlabData);
                            taxslabrepo.Save();
                            taxslabrepo.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Slab Already Exist.", "Warning");
                            this.ActiveControl = txtFrom;
                        }
                        btnDelete.Enabled = false;
                        btnUpdate.Enabled = false;
                        fillGrid();
                    }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFrom.Text) && !string.IsNullOrEmpty(txtTo.Text) && !string.IsNullOrEmpty(txtDescription.Text) && cmbGst.Text.Trim() != "")
                {
                    TaxSlabRepository taxslabrepo = new TaxSlabRepository();
                    TaxSlabMaster taxslabinfo = new TaxSlabMaster();
                    taxslabinfo = taxslabrepo.GetAll().Where(t => t.SlabFrom == Convert.ToInt32(txtFrom.Text) && t.SlabTo == Convert.ToInt32(txtTo.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (taxslabinfo != null)
                    {
                        taxslabinfo.SlabFrom = Convert.ToInt32(txtFrom.Text);
                        taxslabinfo.SlabTo = Convert.ToInt32(txtTo.Text);
                        taxslabinfo.GstPercent = cmbGst.SelectedIndex;
                        taxslabrepo.Edit(taxslabinfo);
                        taxslabrepo.Save();
                        taxslabrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Slab Not Exist.", "Warning");
                        this.ActiveControl = txtFrom;
                    }
                    fillGrid();
                }
                else
                {
                    MessageBox.Show("Please Enter All Information.", "Warning");
                    this.ActiveControl = btnUpdate;
                }
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnNew.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFrom.Text) && !string.IsNullOrEmpty(txtTo.Text) && !string.IsNullOrEmpty(txtDescription.Text) && cmbGst.Text.Trim() != "")
                {
                    TaxSlabRepository taxslabrepo = new TaxSlabRepository();
                    TaxSlabMaster taxslabinfo = new TaxSlabMaster();
                    taxslabinfo = taxslabrepo.GetAll().Where(t => t.Id == Convert.ToInt32(txtID.Text) && t.CompId == Convert.ToInt32(CommonMethod.CompId)).FirstOrDefault();
                    if (taxslabinfo != null)
                    {
                        taxslabrepo.Remove(taxslabinfo);
                        taxslabrepo.Save();
                    }
                    btnDelete.Enabled = false;
                    fillGrid();
                }
            }
            catch (Exception)
            { }
        }

        private void dtgTaxSlab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgTaxSlab.Rows.Count > 0)
                {
                    txtID.Text = dtgTaxSlab.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    txtDescription.Text = dtgTaxSlab.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                    txtFrom.Text = dtgTaxSlab.Rows[e.RowIndex].Cells["FromAmt"].Value.ToString();
                    txtTo.Text = dtgTaxSlab.Rows[e.RowIndex].Cells["ToAmt"].Value.ToString();
                    switch (Convert.ToInt32(dtgTaxSlab.Rows[e.RowIndex].Cells["GST"].Value))
                    {
                        case 0:
                            cmbGst.SelectedIndex = 0;
                            break;
                        case 5:
                            cmbGst.SelectedIndex = 1;
                            break;
                        case 12:
                            cmbGst.SelectedIndex = 2;
                            break;
                        case 18:
                            cmbGst.SelectedIndex = 3;
                            break;
                        case 28:
                            cmbGst.SelectedIndex = 4;
                            break;
                    }
                    btnNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
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

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = txtFrom;
            }
            catch (Exception) { }
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
                    this.ActiveControl = cmbGst;
            }
            catch (Exception) { }
        }

        private void cmbGst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception)
            { }
        }
    }
}