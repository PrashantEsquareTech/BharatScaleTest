using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmGstMaster : Form
    {
        public int GSTid = 0;
        DbClass db = new DbClass();

        public frmGstMaster()
        {
            InitializeComponent();
            maxGSTId();
            BindGSTinfo();
            btnDelete.Enabled = false;
            this.ActiveControl = txtgstpercent;
        }

        public void maxGSTId()
        {
            try
            {
                GSTMasterRepository unitrepo = new GSTMasterRepository();
                var mid = unitrepo.GetAll().Max(t => t.GSTNo);
                if (mid == null)
                    txtGStID.Text = "1";
                else
                    txtGStID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtgstpercent.Text = "";
                GSTid = 0;
                maxGSTId();
                BindGSTinfo();
                btnAdd.Text = "Add";
                this.ActiveControl = txtgstpercent;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GSTMasterRepository unitrepo = new GSTMasterRepository();
                var unitdata = unitrepo.GetAll().Where(t => t.GSTPercent == Convert.ToDecimal(txtgstpercent.Text.Trim()) && t.GSTNo != Convert.ToInt32(txtGStID.Text)).FirstOrDefault();
                if (unitdata != null)
                {
                    MessageBox.Show("This GST Percent Already Present.", "Warning");
                    clear();
                    this.ActiveControl = txtgstpercent;
                }
                else
                {
                    if (txtgstpercent.Text.Trim() != "")
                    {
                        unitrepo = new GSTMasterRepository();
                        var supp = unitrepo.GetAll().Where(t => t.GSTPercent == Convert.ToDecimal(txtgstpercent.Text.Trim()) && t.GSTNo != Convert.ToInt32(txtGStID.Text)).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtgstpercent.Text = "";
                            this.ActiveControl = txtgstpercent;
                        }
                        else
                        {
                            if (btnAdd.Text == "Add")
                            {
                                GSTMaster unitinfo = new GSTMaster();
                                maxGSTId();
                                unitinfo.GSTNo = Convert.ToInt32(txtGStID.Text);
                                unitinfo.GSTPercent = Convert.ToDecimal(txtgstpercent.Text.Trim());
                                if (rbtcnsgst.Checked == true)
                                    unitinfo.ApplyforSnCGST = "Y";
                                else
                                    unitinfo.ApplyforSnCGST = "N";
                                if (rbtigst.Checked == true)
                                    unitinfo.ApplyforIGST = "Y";
                                else
                                    unitinfo.ApplyforIGST = "N";
                                unitrepo.Add(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("GST percent Add Successfully.", "Success");
                                clear();
                                this.ActiveControl = txtgstpercent;
                            }
                            else
                            {
                                GSTMaster unitinfo = new GSTMaster();
                                unitinfo = unitrepo.GetById(GSTid);
                                unitinfo.GSTNo = Convert.ToInt32(txtGStID.Text);
                                unitinfo.GSTPercent = Convert.ToDecimal(txtgstpercent.Text.Trim());
                                if (rbtcnsgst.Checked == true)
                                    unitinfo.ApplyforSnCGST = "Y";
                                else
                                    unitinfo.ApplyforSnCGST = "N";
                                if (rbtigst.Checked == true)
                                    unitinfo.ApplyforIGST = "Y";
                                else
                                    unitinfo.ApplyforIGST = "N";
                                unitrepo.Edit(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("GST percent updated Successfully.", "Success");
                                clear();
                                this.ActiveControl = txtgstpercent;
                            }
                        }
                    }
                    else
                    {
                        if (txtgstpercent.Text == "")
                        {
                            MessageBox.Show("Enter GST Percent.", "Warning");
                            this.ActiveControl = txtgstpercent;
                        }
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindGSTinfo()
        {
            try
            {
                GSTMasterRepository unitrepo = new GSTMasterRepository();
                GVList.Rows.Clear();
                List<GSTMaster> ItemList = unitrepo.GetAll();
                lblTotaluUnits.Text = Convert.ToString(ItemList.Count);

                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GVList.Rows.Add();
                        GVList.Rows[i].Cells["GSTIDgrid"].Value = item.GSTNo;
                        GVList.Rows[i].Cells["GSTPercent"].Value = item.GSTPercent;
                        GVList.Rows[i].Cells["Id"].Value = item.Id;
                        GVList.Rows[i].Cells["ApplyCSGST"].Value = item.ApplyforSnCGST;
                        GVList.Rows[i].Cells["ApplyIGST"].Value = item.ApplyforIGST;
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
                if (GSTid != 0)
                {
                    ProductRepository purdetailrepo = new ProductRepository();
                    var purdata = purdetailrepo.GetAll().Where(t => t.IGstPercent == Convert.ToDecimal(txtgstpercent.Text.Trim())).FirstOrDefault();
                    if (purdata == null)
                    {
                        GSTMasterRepository unitrepo = new GSTMasterRepository();
                        GSTMaster unitinfo = unitrepo.GetById(GSTid);
                        if (unitinfo.GSTPercent == Convert.ToDecimal(txtgstpercent.Text.Trim()))
                        {
                            unitrepo.Remove(unitinfo);
                            unitrepo.Save();
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clear();
                            btnAdd.Enabled = true;
                            btnDelete.Enabled = false;
                        }
                        else
                            MessageBox.Show("GST Percent is not correct for delete.", "Warning");
                        this.ActiveControl = txtgstpercent;
                        unitrepo.Dispose();
                    }
                    else
                        MessageBox.Show("This GST Percent is Used...You Can Not Delete.", "Warning");
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

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                btnAdd.Enabled = true;
                this.ActiveControl = txtgstpercent;
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
                    SqlCommand cmd = new SqlCommand("SELECT GSTPercent FROM GSTMaster", con);
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
                    txtgstpercent.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void GVList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GSTid = Convert.ToInt32(GVList.Rows[e.RowIndex].Cells["Id"].Value);
                txtGStID.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["GSTIDgrid"].Value);
                txtgstpercent.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["GSTPercent"].Value);
                if (Convert.ToString(GVList.Rows[e.RowIndex].Cells["ApplyCSGST"].Value) == "Y")
                    rbtcnsgst.Checked = true;
                else
                    rbtcnsgst.Checked = false;
                if (Convert.ToString(GVList.Rows[e.RowIndex].Cells["ApplyIGST"].Value) == "Y")
                    rbtigst.Checked = true;
                else
                    rbtigst.Checked = false;
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}