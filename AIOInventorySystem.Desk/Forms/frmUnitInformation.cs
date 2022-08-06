using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmUnitInformation : Form
    {        
        DbClass db = new DbClass();
        public int Unitid = 0;
        private int preComp = 0;
        SettingRepository settRepo = new SettingRepository();
        Setting settData = new Setting();

        public frmUnitInformation()
        {
            InitializeComponent();
            settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settData != null)
            {
                if (settData.Applyonmarathifont == "Y")
                    txtUnitMarathi.Font = new Font("Shivaji01", 12);
                else
                    txtUnitMarathi.Font = new Font("Microsoft Sans Serif", 10);
            }
            maxUnitid();
            BindUnitinfo();
            btnDelete.Enabled = false;
            this.ActiveControl = txtUnitName;
        }

        public void maxUnitid()
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                var mid = 0;
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(unitrepo.GetAll().Max(t => t.UnitID));
                else
                    mid = Convert.ToInt32(unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.UnitID));
                if (mid == null)
                    txtUnitID.Text = "1";
                else
                    txtUnitID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtUnitName.Text = "";
                txtDescription.Text = "";
                txtstockalert.Text = "0";
                txtUnitMarathi.Text = "";
                Unitid = 0;
                maxUnitid();
                cmbGSTUnitName.SelectedIndex = -1;
                BindUnitinfo();
                btnAdd.Text = "Add";
                this.ActiveControl = txtUnitName;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                Unit unitdata = new Unit();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().Where(t => t.UnitName == txtUnitName.Text.Trim() && t.UnitID != Convert.ToInt32(txtUnitID.Text)).FirstOrDefault();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.UnitName == txtUnitName.Text.Trim() && t.UnitID != Convert.ToInt32(txtUnitID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (unitdata != null)
                {
                    MessageBox.Show("This Unit Name Already Present.", "Warning");
                    clear();
                }
                else
                {
                    if (txtUnitName.Text.Trim() != "" && txtstockalert.Text != "" && cmbGSTUnitName.Text != "")
                    {
                        unitrepo = new UnitRepository();
                        Unit supp = new Unit();
                        if (CommonMethod.commProduct == true)
                            supp = unitrepo.GetAll().Where(t => t.UnitName.ToLower() == txtUnitName.Text.Trim().ToLower() && t.UnitID != Convert.ToInt32(txtUnitID.Text)).FirstOrDefault();
                        else
                            supp = unitrepo.GetAll().Where(t => t.UnitName.ToLower() == txtUnitName.Text.Trim().ToLower() && t.UnitID != Convert.ToInt32(txtUnitID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtUnitName.Text = "";
                            this.ActiveControl = txtUnitName;
                        }
                        else
                        {
                            if (btnAdd.Text == "Add")
                            {
                                Unit unitinfo = new Unit();
                                maxUnitid();
                                unitinfo.UnitID = Convert.ToInt32(txtUnitID.Text);
                                unitinfo.UnitName = txtUnitName.Text.Trim();
                                unitinfo.Description = txtDescription.Text.Trim();
                                unitinfo.StockAlertValue = Convert.ToInt32(txtstockalert.Text);
                                unitinfo.UnitMarathi = txtUnitMarathi.Text;
                                unitinfo.GSTUnitName = cmbGSTUnitName.Text;
                                unitinfo.CompId = CommonMethod.CompId;
                                unitrepo.Add(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("Unit Add Successfully.", "Success");
                                clear();
                            }
                            else
                            {
                                string uname = "";
                                Unit unitinfo = new Unit();
                                unitinfo = unitrepo.GetById(Unitid);
                                uname = unitinfo.UnitName.ToString();
                                unitinfo.UnitID = Convert.ToInt32(txtUnitID.Text);
                                unitinfo.UnitName = txtUnitName.Text.Trim();
                                unitinfo.Description = txtDescription.Text.Trim();
                                unitinfo.StockAlertValue = Convert.ToInt32(txtstockalert.Text);
                                unitinfo.UnitMarathi = txtUnitMarathi.Text;
                                unitinfo.GSTUnitName = cmbGSTUnitName.Text;
                                unitinfo.CompId = preComp; //09062018
                                unitrepo.Edit(unitinfo);
                                unitrepo.Save();

                                if (uname != txtUnitName.Text.Trim().ToString())
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPUpdateUnit", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = uname;
                                        command.Parameters.Add("@NewUName", SqlDbType.VarChar).Value = txtUnitName.Text.Trim().ToString();
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.ExecuteNonQuery();
                                        uname = "";
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                MessageBox.Show("Unit updated Successfully.", "Success");
                                clear();
                            }
                        }
                    }
                    else
                    {
                        if (txtUnitName.Text == "")
                        {
                            MessageBox.Show("Enter Unit Name.", "Warning");
                            this.ActiveControl = txtUnitName;
                        }
                        else if (txtstockalert.Text == "")
                        {
                            MessageBox.Show("Enter Stock alert value.", "Warning");
                            this.ActiveControl = txtstockalert;
                        }
                        else
                        {
                            MessageBox.Show("Select GST Unit Name.", "Warning");
                            this.ActiveControl = cmbGSTUnitName;
                        }
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindUnitinfo()
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                dtgvList.Rows.Clear();
                List<Unit> ItemList = new List<Unit>();
                if (CommonMethod.commProduct == true)
                    ItemList = unitrepo.GetAll().OrderByDescending(t => t.UnitName).ToList();
                else
                    ItemList = unitrepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).OrderByDescending(t => t.UnitName).ToList();
                lblTotaluUnits.Text = Convert.ToString(ItemList.Count);
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["UnitIdg"].Value = item.UnitID;
                        dtgvList.Rows[i].Cells["UnitName"].Value = item.UnitName;
                        dtgvList.Rows[i].Cells["Description"].Value = item.Description;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        dtgvList.Rows[i].Cells["StockAlert"].Value = item.StockAlertValue;
                        dtgvList.Rows[i].Cells["UnitMarathi"].Value = item.UnitMarathi;
                        dtgvList.Rows[i].Cells["GSTUnitName"].Value = item.GSTUnitName;
                        dtgvList.Rows[i].Cells["CompId"].Value = item.CompId; // nilophar 09062018
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
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (Unitid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Unit";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Unitid;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Check", SqlDbType.NVarChar);
                            command.Parameters["@Check"].Direction = ParameterDirection.Output;
                            command.Parameters["@Check"].Size = 5;
                            command.ExecuteReader();
                            strcheck = command.Parameters["@Check"].Value.ToString();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        if (strcheck == "T")
                            MessageBox.Show("This Unit Company is Used. You Can Not Delete.", "Warning");
                        else
                        {
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clear();
                        }
                    }
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
                btnAdd_Click(null, null);
                return true;
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

        private void txtUnitName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtUnitMarathi;
            }
            catch (Exception)
            { }
        }

        private void txtstockalert_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
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
                    SqlCommand cmd = new SqlCommand();
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT UnitName FROM Unit", con);
                    else
                        cmd = new SqlCommand("SELECT UnitName FROM Unit where CompId='" + CommonMethod.CompId + "'", con);
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
                    txtUnitName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
                SettingRepository sRepo = new SettingRepository();
                Setting sdata = new Setting();
                sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata != null)
                {
                    if (sdata.Applyonmarathifont == "Y")
                        txtUnitMarathi.Font = new Font("Shivaji01", 12);
                    else
                        txtUnitMarathi.Font = new Font("Microsoft Sans Serif", 10);
                }
            }
            catch (Exception)
            { }
        }

        private void txtUnitMarathi_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                    frmUnitInformation_Load(null, null);
                }
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtDescription;
            }
            catch (Exception)
            { }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtstockalert;
            }
            catch (Exception)
            { }
        }

        private void txtstockalert_KeyDown_1(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbGSTUnitName;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnDelete;
            }
            catch (Exception)
            { }
        }

        private void btnDelete_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnClose;
            }
            catch (Exception)
            { }
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void btnnew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtUnitName;
            }
            catch (Exception)
            { }
        }

        private void txtUnitName_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtUnitMarathi;
            }
            catch (Exception)
            { }
        }

        private void cmbGSTUnitName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception) { }
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                preComp = 0;
                Unitid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtUnitID.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["UnitIdg"].Value);
                txtUnitName.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["UnitName"].Value);
                txtDescription.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                txtstockalert.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["StockAlert"].Value);
                txtUnitMarathi.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["UnitMarathi"].Value);
                cmbGSTUnitName.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["GSTUnitName"].Value);
                preComp = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["CompId"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}