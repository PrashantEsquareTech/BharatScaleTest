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
    public partial class frmDeptartement : Form
    {
        DepartemetRepository Deptrepo;
        public int deptid;
        DbClass db = new DbClass();

        public frmDeptartement()
        {
            InitializeComponent();
            maxdeptid();
            BindUnitinfo();
            btnDelete.Enabled = false;
        }

        public void maxdeptid()
        {
            try
            {
                Deptrepo = new DepartemetRepository();
                var mid = Deptrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.DeptNo);
                if (mid == null)
                    txtdeptid.Text = "1";
                else
                    txtdeptid.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtdeptname.Text = "";
                txtDescription.Text = "";
                maxdeptid();
                BindUnitinfo();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
                btnAdd.Text = "Add";
                this.ActiveControl = txtdeptname;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtdeptname.Text != "")
                {
                    DepartemetRepository deptdata = new DepartemetRepository();
                    if (btnAdd.Text == "Add")
                    {
                        var ddata = deptdata.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.DeptName.ToLower() == txtdeptname.Text.ToLower()).FirstOrDefault();
                        if (ddata == null)
                        {
                            Deptrepo = new DepartemetRepository();
                            Departement deptinfo = new Departement();
                            deptinfo.DeptNo = Convert.ToInt32(txtdeptid.Text);
                            deptinfo.DeptName = txtdeptname.Text;
                            deptinfo.DeptDesc = txtDescription.Text;
                            deptinfo.CompId = CommonMethod.CompId;
                            Deptrepo.Add(deptinfo);
                            Deptrepo.Save();
                            MessageBox.Show("Department Added Successfully.", "Success");
                        }
                        else
                        {
                            MessageBox.Show("This Department Name Already Entered.", "Warning");
                            this.ActiveControl = txtdeptname;
                        }
                    }
                    else
                    {
                        Deptrepo = new DepartemetRepository();
                        Departement deptinfo = new Departement();
                        deptinfo = Deptrepo.GetById(deptid);
                        string predeptname = deptinfo.DeptName;
                        deptinfo.DeptNo = Convert.ToInt32(txtdeptid.Text);
                        deptinfo.DeptName = txtdeptname.Text;
                        deptinfo.DeptDesc = txtDescription.Text;
                        deptinfo.CompId = CommonMethod.CompId;
                        Deptrepo.Edit(deptinfo);
                        Deptrepo.Save();
                        btnAdd.Text = "Add";

                        if (predeptname != txtdeptname.Text)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPUpdateDeptName", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@PreDName", SqlDbType.VarChar).Value = predeptname;
                                command.Parameters.Add("@NewDName", SqlDbType.VarChar).Value = txtdeptname.Text;
                                command.ExecuteNonQuery();
                                predeptname = "";
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { }
                        }
                        MessageBox.Show("Department Updated Successfully.", "Success");
                    }
                    clear();
                }
                else
                {
                    MessageBox.Show("Enter Department Name.", "Warning");
                    this.ActiveControl = txtdeptname;
                }
            }
            catch (Exception)
            { }
        }

        public void BindUnitinfo()
        {
            try
            {
                Deptrepo = new DepartemetRepository();
                dtgvList.Rows.Clear();
                List<Departement> ItemList = Deptrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        dtgvList.Rows[i].Cells["DepartmentId"].Value = item.DeptNo;
                        dtgvList.Rows[i].Cells["DepartmentName"].Value = item.DeptName;
                        dtgvList.Rows[i].Cells["Description"].Value = item.DeptDesc;
                        i++;
                    }
                    lblTotaluUnits.Text = ItemList.Count.ToString();
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
                    if (deptid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Department";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = deptid;
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
                            MessageBox.Show("This Department is Used. You Can Not Delete.", "Warning");
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

        private void txtdeptname_KeyDown(object sender, KeyEventArgs e)
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
                deptid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtdeptid.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["DepartmentId"].Value);
                txtdeptname.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["DepartmentName"].Value);
                txtDescription.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}