using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using System.Data.SqlClient;
using System.Configuration;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmStateMaster : Form
    {
        public int StateId = 0;
        DbClass db = new DbClass();

        public frmStateMaster()
        {
            InitializeComponent();
            maxStateId();
            BindStateinfo();
            btnDelete.Enabled = false;
            this.ActiveControl = txtStateName;
        }

        public void maxStateId()
        {
            try
            {
                StateMasterRepository unitrepo = new StateMasterRepository();
                var mid = unitrepo.GetAll().Max(t => t.StateId);
                if (mid == null)
                    txtStateID.Text = "1";
                else
                    txtStateID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtStateName.Text = "";
                StateId = 0;
                maxStateId();
                BindStateinfo();
                btnDelete.Enabled = false;
                btnAdd.Text = "Add";
                this.ActiveControl = txtStateName;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                StateMasterRepository unitrepo = new StateMasterRepository();
                var unitdata = unitrepo.GetAll().Where(t => t.StateName == txtStateName.Text.Trim() && t.StateId != Convert.ToInt32(txtStateID.Text)).FirstOrDefault();
                if (unitdata != null)
                {
                    MessageBox.Show("This State Name Already Present.", "Warning");
                    clear();
                }
                else
                {
                    if (txtStateName.Text.Trim() != "")
                    {
                        unitrepo = new StateMasterRepository();
                        var supp = unitrepo.GetAll().Where(t => t.StateName.ToLower() == txtStateName.Text.Trim().ToLower() && t.StateId != Convert.ToInt32(txtStateID.Text)).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtStateName.Text = "";
                            this.ActiveControl = txtStateName;
                        }
                        else
                        {
                            if (btnAdd.Text == "Add")
                            {
                                StateMaster unitinfo = new StateMaster();
                                maxStateId();
                                unitinfo.StateId = Convert.ToInt32(txtStateID.Text);
                                unitinfo.StateName = txtStateName.Text.Trim();
                                unitrepo.Add(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("State Add Successfully.", "Success");
                                clear();
                            }
                            else
                            {
                                StateMaster unitinfo = new StateMaster();
                                unitinfo = unitrepo.GetById(StateId);
                                unitinfo.StateId = Convert.ToInt32(txtStateID.Text);
                                unitinfo.StateName = txtStateName.Text.Trim();
                                unitrepo.Edit(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("State updated Successfully.", "Success");
                                clear();
                            }
                        }
                    }
                    else
                    {
                        if (txtStateName.Text == "")
                        {
                            MessageBox.Show("Enter State Name.", "Warning");
                            this.ActiveControl = txtStateName;
                        }
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindStateinfo()
        {
            try
            {
                StateMasterRepository unitrepo = new StateMasterRepository();
                dtgvList.Rows.Clear();
                List<StateMaster> ItemList = unitrepo.GetAll();
                lblTotaluUnits.Text = Convert.ToString(ItemList.Count);
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["StateIDg"].Value = item.StateId;
                        dtgvList.Rows[i].Cells["StateName"].Value = item.StateName;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
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
                if (StateId != 0)
                {
                    CustomerRepository purdetailrepo = new CustomerRepository();
                    var purdata = purdetailrepo.GetAll().Where(t => t.State == txtStateName.Text.Trim()).FirstOrDefault();
                    if (purdata == null)
                    {
                        StateMasterRepository unitrepo = new StateMasterRepository();
                        StateMaster unitinfo = unitrepo.GetById(StateId);
                        if (unitinfo.StateName == txtStateName.Text.Trim().ToString())
                        {
                            unitrepo.Remove(unitinfo);
                            unitrepo.Save();
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clear();
                            btnAdd.Enabled = true;
                            btnDelete.Enabled = false;
                        }
                        else
                            MessageBox.Show("State name is not correct for delete.", "Warning");
                        this.ActiveControl = txtStateName;
                        unitrepo.Dispose();
                    }
                    else
                        MessageBox.Show("This state is Used. You Can Not Delete.", "Warning");
                    purdetailrepo.Dispose();
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtUnitName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (tb != null)
                {
                    int pos = tb.SelectionStart;
                    int length = tb.Text.Length;
                    tb.Text = tb.Text.Replace(",", "");
                    int diff = length - tb.Text.Length;
                    tb.SelectionStart = pos == 0 || diff == 0 ? pos : pos - diff;
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
                btnAdd.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void frmUnitInformation_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            try
            {
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT StateName FROM StateMaster", con);
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
                    txtStateName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                StateId = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtStateID.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["StateIDg"].Value);
                txtStateName.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["StateName"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}