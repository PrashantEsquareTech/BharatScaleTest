using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmTransactionYearDates : Form
    {
        public int updateId;

        public frmTransactionYearDates()
        {
            InitializeComponent();
            BindTransactionYears();
            clear();                    
        }

        #region Methods
        public void BindTransactionYears()
        {
            try
            {
                int SelectedindexYear = 0;
                tblTransactionYearDatesRepository TrDRepo = new tblTransactionYearDatesRepository();
                List<tblTransactionYearDate> TrDData = new List<tblTransactionYearDate>();
                TrDData = TrDRepo.GetAll().OrderByDescending(t => t.Id).Take(10).ToList();
                int i = 0;
                foreach (var item in TrDData)
                {
                    cmbTransactionYear.Items.Add(item.TransactionYear);
                    var dtstart = DateTime.ParseExact(item.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var dtend = DateTime.ParseExact(item.EndDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    var dtcurrent = DateTime.Today;
                    if (dtstart <= dtcurrent && dtend >= dtcurrent)
                        SelectedindexYear = i;
                    i++;
                }
                cmbTransactionYear.SelectedIndex = SelectedindexYear;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                cmbTransactionYear.SelectedIndex = 0;
                dtpStartDate.Value = DateTime.Now.Date;
                dtpEndDate.Value = DateTime.Now.Date;
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                BindGrid();
                this.ActiveControl = cmbTransactionYear;
            }
            catch (Exception)
            { }
        }

        public void BindGrid()
        {
            try
            {
                tblTransactionYearDatesRepository TransactionYearRepo = new tblTransactionYearDatesRepository();
                List<tblTransactionYearDate> TransactionYearData = new List<tblTransactionYearDate>();
                TransactionYearData = TransactionYearRepo.GetAll().ToList();
                dgvList.Rows.Clear();
                if (TransactionYearData.Count != 0)
                {
                    int i = 0;
                    foreach (var item in TransactionYearData)
                    {
                        dgvList.Rows.Add();
                        dgvList.Rows[i].Cells["Id"].Value = item.Id;
                        dgvList.Rows[i].Cells["TransactionYear"].Value = item.TransactionYear;
                        dgvList.Rows[i].Cells["StartDate"].Value = item.StartDate;
                        dgvList.Rows[i].Cells["EndDate"].Value = item.EndDate;
                        i++;
                    }
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region Events
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTransactionYear.SelectedIndex != 0)
                {
                    tblTransactionYearDatesRepository TransactionYearRepo1 = new tblTransactionYearDatesRepository();
                    var TYData = TransactionYearRepo1.GetAll().Where(t => t.TransactionYear.Trim() == cmbTransactionYear.Text.Trim()).FirstOrDefault();
                    if (TYData != null)
                    {
                        MessageBox.Show("Dates for this TransactionYear already added.");
                        this.ActiveControl = cmbTransactionYear;
                    }
                    else
                    {
                        tblTransactionYearDatesRepository TransactionYearRepo = new tblTransactionYearDatesRepository();
                        tblTransactionYearDate TransactionYearData = new tblTransactionYearDate();
                        TransactionYearData.TransactionYear = cmbTransactionYear.Text.Trim();
                        TransactionYearData.StartDate = dtpStartDate.Value.Date.ToString("dd/MM/yyyy");
                        TransactionYearData.EndDate = dtpEndDate.Value.Date.ToString("dd/MM/yyyy");
                        TransactionYearRepo.Add(TransactionYearData);
                        TransactionYearRepo.Save();
                        MessageBox.Show("Record Saved.","Successs");
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Select TransactionYear!","Warning");
                    clear();
                }                
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTransactionYear.SelectedIndex != 0)
                {
                    tblTransactionYearDatesRepository TransactionYearRepo = new tblTransactionYearDatesRepository();
                    tblTransactionYearDate TransactionYearData = new tblTransactionYearDate();
                    TransactionYearData = TransactionYearRepo.GetById(updateId);
                    if (TransactionYearData != null)
                    {
                        TransactionYearData.TransactionYear = cmbTransactionYear.Text.Trim();
                        TransactionYearData.StartDate = dtpStartDate.Value.Date.ToString("dd/MM/yyyy");
                        TransactionYearData.EndDate = dtpEndDate.Value.Date.ToString("dd/MM/yyyy");
                        TransactionYearRepo.Edit(TransactionYearData);
                        TransactionYearRepo.Save();
                        MessageBox.Show("Record Updated.", "Successs");
                        clear();
                    }
                }
                else
                {
                    MessageBox.Show("Select TransactionYear!", "Warning");
                    clear();
                }                
            }
            catch (Exception)
            { }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                updateId = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells["Id"].Value);
                cmbTransactionYear.Text = Convert.ToString(dgvList.Rows[e.RowIndex].Cells["TransactionYear"].Value);
                dtpStartDate.Value = Convert.ToDateTime(dgvList.Rows[e.RowIndex].Cells["StartDate"].Value);
                dtpEndDate.Value = Convert.ToDateTime(dgvList.Rows[e.RowIndex].Cells["EndDate"].Value);
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
            }
            catch (Exception)
            { }
        }      

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }        

        private void cmbTransactionYear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = dtpStartDate;
            }
            catch (Exception)
            { }
        }

        private void dtpStartDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = dtpEndDate;
            }
            catch (Exception)
            { }
        }

        private void dtpEndDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl= btnSave;
            }
            catch (Exception)
            { }
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception)
            { }
        }
        #endregion
    }
}
