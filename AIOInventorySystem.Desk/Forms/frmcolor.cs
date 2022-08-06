using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmcolor : Form
    {
        ColorRepository colorRepo;
        public int colorid;
        DbClass db = new DbClass();

        public frmcolor()
        {
            InitializeComponent();
            maxColorid();
            BindColorinfo();
            btnAdd.Text = "ADD";
            btnDelete.Enabled = false;
            lblTotaluUnits.Text = Convert.ToString(colorRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count());
        }

        public void maxColorid()
        {
            try
            {
                colorRepo = new ColorRepository();
                var mid = colorRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.ColorId);
                if (mid == null)
                    txtcolorid.Text = "1";
                else
                    txtcolorid.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtColorName.Text = "";
                txtDescription.Text = "";
                maxColorid();
                BindColorinfo();
                btnAdd.Text = "ADD";
                lblTotaluUnits.Text = Convert.ToString(colorRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count());
                this.ActiveControl = txtColorName;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtColorName.Text != "")
                {
                    ColorRepository ColorRepo = new ColorRepository();
                    Colorinfo colordata = new Colorinfo();
                    colordata = ColorRepo.GetAll().Where(t => t.ColorId == Convert.ToInt32(txtcolorid.Text) && t.ColorName.ToLower() == txtColorName.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (colordata == null)
                    {
                        if (btnAdd.Text == "ADD")
                        {
                            colorRepo = new ColorRepository();
                            Colorinfo unitinfo = new Colorinfo();
                            unitinfo.ColorId = Convert.ToInt32(txtcolorid.Text);
                            unitinfo.ColorName = txtColorName.Text;
                            unitinfo.Description = txtDescription.Text;
                            unitinfo.CompId = CommonMethod.CompId;
                            colorRepo.Add(unitinfo);
                            colorRepo.Save();
                            MessageBox.Show("Color Added Successfully.", "Success");
                        }
                    }
                    else
                    {
                        if (btnAdd.Text == "Update")
                        {
                            colorRepo = new ColorRepository();
                            string precolorname = colordata.ColorName;
                            colordata.ColorId = Convert.ToInt32(txtcolorid.Text);
                            colordata.ColorName = txtColorName.Text;
                            colordata.Description = txtDescription.Text;
                            colordata.CompId = CommonMethod.CompId;
                            colorRepo.Edit(colordata);
                            colorRepo.Save();
                            if (precolorname != txtColorName.Text)
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPUpdateColorName", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@PreCLName", SqlDbType.VarChar).Value = precolorname;
                                    command.Parameters.Add("@NewCLName", SqlDbType.VarChar).Value = txtColorName.Text;
                                    command.ExecuteNonQuery();
                                    precolorname = "";
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Color Updated Successfully.", "Success");
                        }
                        else
                        {
                            MessageBox.Show("This Color Name Already Entered.", "Warning");
                            this.ActiveControl = txtColorName;
                        }
                    }
                    clear();
                }
                else
                {
                    MessageBox.Show("Enter Color Name.", "Warning");
                    this.ActiveControl = txtColorName;
                }
            }
            catch (Exception)
            { }
        }

        public void BindColorinfo()
        {
            try
            {
                colorRepo = new ColorRepository();
                dtgvList.Rows.Clear();
                List<Colorinfo> ItemList = colorRepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).OrderByDescending(t => t.ColorName).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["ClrId"].Value = item.ColorId;
                        dtgvList.Rows[i].Cells["ColorName"].Value = item.ColorName;
                        dtgvList.Rows[i].Cells["Description"].Value = item.Description;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
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
                    if (colorid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Color";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = colorid;
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
                            MessageBox.Show("This Color is Used. You Can Not Delete.", "Warning");
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
            if (keyData == Keys.F3)
            {
                btnAdd_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        private void txtColorName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                colorid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtcolorid.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["ClrId"].Value);
                txtColorName.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["ColorName"].Value);
                txtDescription.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}