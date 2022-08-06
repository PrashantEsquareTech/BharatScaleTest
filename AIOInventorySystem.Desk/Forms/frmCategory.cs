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
    public partial class frmCategory : Form
    {
        DbClass db = new DbClass();
        CategoryRepository catRepo;
        SubCategoryRepository subcatRepo;
        public int catID,subcatID;

        public frmCategory()
        {
            InitializeComponent();
            BindUnitinfo();
            BindSubCatinfo();            
        }
             
        public void clear()
        {
            //clear category panel
            try
            {
                txtCategoryName.Text = "";
                txtcatcode.Text = "";
                txtDescription.Text = "";
                BindUnitinfo();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnAdd.Text = "Add";
                this.ActiveControl = txtCategoryName;
            }
            catch (Exception)
            { }
        }

        public void clearsubcat()
        {
            //clear subcategory panel
            try
            {
                txtsubcatName.Text = "";
                txtsubcatcode.Text = "";
                txtsubDescription.Text = "";
                BindSubCatinfo();
                btnsadd.Enabled = true;
                btnsdelete.Enabled = false;
                this.ActiveControl = txtsubcatName;
            }
            catch (Exception)
            { }
        }
       
        public void BindUnitinfo()
        {
            //Bind Grid with unit info
            try
            {
                catRepo = new CategoryRepository();
                GvUnitInfo.Rows.Clear();
                List<Category> ItemList = catRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvUnitInfo.Rows.Add();
                        GvUnitInfo.Rows[i].Cells["CategoryID"].Value = item.CatID;
                        GvUnitInfo.Rows[i].Cells["CategoryName"].Value = item.CategoryName;
                        GvUnitInfo.Rows[i].Cells["Code"].Value = item.CatCode;
                        GvUnitInfo.Rows[i].Cells["Description"].Value = item.Description;
                        i++;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void BindSubCatinfo()
        {
            try
            {
                //Bind subcategory grid with subcategory info
                SqlDataAdapter da = new SqlDataAdapter("select s.SubCatId, s.SubCatName, s.SubCatCode, s.Description from SubCategory s where s.compid=" + CommonMethod.CompId, db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvSubCatInfo.DataSource = dt;
                GvSubCatInfo.Refresh();
                GvSubCatInfo.Columns["SubCatId"].Visible = false;
                GvSubCatInfo.Columns["Description"].Visible = false;
                if (GvSubCatInfo.Rows.Count != 0)
                {
                    GvSubCatInfo.DataSource = dt;
                    GvSubCatInfo.Refresh();
                    GvSubCatInfo.Columns["SubCatId"].Visible = false;
                    GvSubCatInfo.Columns["Description"].Visible = false;
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

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txtCategoryName.Text != "")
                {
                    catRepo = new CategoryRepository();
                    Category categorydata = new Category();
                    if (btnAdd.Text == "Add")
                    {
                        var catdata = catRepo.GetAll().Where(t => t.CategoryName.ToLower() == txtCategoryName.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (catdata == null)
                        {
                            catRepo = new CategoryRepository();
                            Category unitinfo = new Category();
                            unitinfo.CategoryName = txtCategoryName.Text;
                            unitinfo.CatCode = txtcatcode.Text;
                            unitinfo.Description = txtDescription.Text;
                            unitinfo.CompId = CommonMethod.CompId;
                            catRepo.Add(unitinfo);
                            catRepo.Save();
                            MessageBox.Show("Category Added Successfully.","Success");
                        }
                        else
                        {
                            if (catdata != null)
                            {
                                MessageBox.Show("This Category Name Already Saved.","Warning");
                                this.ActiveControl = txtCategoryName;
                            }
                        }
                    }
                    else
                    {
                        catRepo = new CategoryRepository();
                        Category unitinfo = new Category();
                        unitinfo = catRepo.GetById(catID);
                        string precategoryname = unitinfo.CategoryName;
                        unitinfo.CategoryName = txtCategoryName.Text;
                        unitinfo.CatCode = txtcatcode.Text;
                        unitinfo.Description = txtDescription.Text;
                        unitinfo.CompId = CommonMethod.CompId;
                        catRepo.Edit(unitinfo);
                        catRepo.Save();
                        if (precategoryname != txtCategoryName.Text)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPUpdateCategoryName", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@PreCTName", SqlDbType.VarChar).Value = precategoryname;
                                command.Parameters.Add("@NewCTName", SqlDbType.VarChar).Value = txtCategoryName.Text;
                                command.ExecuteNonQuery();
                                precategoryname = "";
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { }
                        }
                        MessageBox.Show("Category Updated Successfully.","Success");
                    }
                    clear();
                }
                else
                {
                    MessageBox.Show("Enter Category Name or Code.","Warning");
                    this.ActiveControl = txtCategoryName;
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (catID != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Category";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = catID;
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
                            MessageBox.Show("This Category is Used. You Can Not Delete.", "Warning");
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }     

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();            
        }

        private void btnsnew_Click(object sender, EventArgs e)
        {
            clearsubcat();
            this.ActiveControl = txtsubcatName;
        }

        private void btnsclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnsadd_Click(object sender, EventArgs e)
        {
            //save subcategory
            try
            {
                if (txtsubcatName.Text != "")
                {
                    subcatRepo = new SubCategoryRepository();
                    var subcatdata = subcatRepo.GetAll().Where(t => t.SubCatName.ToLower() == txtsubcatName.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (subcatdata == null)
                    {
                        subcatRepo = new SubCategoryRepository();
                        SubCategory subcatinfo = new SubCategory();
                        subcatinfo.SubCatName = txtsubcatName.Text;
                        subcatinfo.SubCatCode = txtsubcatcode.Text;
                        subcatinfo.Description = txtsubDescription.Text;
                        subcatinfo.CompId = CommonMethod.CompId;
                        subcatRepo.Add(subcatinfo);
                        subcatRepo.Save();
                        MessageBox.Show("SubCategory Add Successfully.","Success");
                        clearsubcat();
                    }
                    else
                    {
                        if (subcatdata != null)
                        {
                            MessageBox.Show("This Sub Category Name Already Saved.","Warning");
                            this.ActiveControl = txtCategoryName;
                        }
                        else
                        {
                            MessageBox.Show("This Sub Category Code Already Saved.","Warning");
                            this.ActiveControl = txtcatcode;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter SubCategory Name or Code.","Warning");
                    this.ActiveControl = txtsubcatName;
                }
            }
            catch (Exception)
            { }
        }

        private void btnsdelete_Click(object sender, EventArgs e)
        {
            //delete subcategory
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (catID != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "SubCategory";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = subcatID;
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
                            MessageBox.Show("This Sub Category is Used. You Can Not Delete.", "Warning");
                        else
                        {
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clearsubcat();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtsubcatName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsadd;
            }
            catch (Exception)
            { }
        }

        private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void GvUnitInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                catID = Convert.ToInt32(GvUnitInfo.Rows[e.RowIndex].Cells["CategoryID"].Value);
                txtCategoryName.Text = Convert.ToString(GvUnitInfo.Rows[e.RowIndex].Cells["CategoryName"].Value);
                txtcatcode.Text = Convert.ToString(GvUnitInfo.Rows[e.RowIndex].Cells["Code"].Value);
                txtDescription.Text = Convert.ToString(GvUnitInfo.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void GvSubCatInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            subcatID = Convert.ToInt32(GvSubCatInfo.Rows[e.RowIndex].Cells["SubCatId"].Value);
            txtsubcatName.Text = Convert.ToString(GvSubCatInfo.Rows[e.RowIndex].Cells["SubCatName"].Value);
            txtsubcatcode.Text = Convert.ToString(GvSubCatInfo.Rows[e.RowIndex].Cells["SubCatCode"].Value);
            txtsubDescription.Text = Convert.ToString(GvSubCatInfo.Rows[e.RowIndex].Cells["Description"].Value);
            btnsadd.Enabled = false;
            btnsdelete.Enabled = true;
        }      
    }
}
