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
    public partial class frmUnpaidBonusList : Form
    {
        public static DataTable UnPaidBonusTable = new DataTable();
        DbClass db = new DbClass();

        public frmUnpaidBonusList()
        {
            InitializeComponent();
            customerdata();
            fillgrid();
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.BonusIn != null && t.BonusIn != "").OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                customerdata.Reverse();
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "Id";
                cmbcustomer.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomer.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillgrid()
        {
            try
            {
                db.connect();
                SqlCommand cmd = new SqlCommand("SP_UNPaidBonusList", db.Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@transactionYear", CommonMethod.TransactionYear);
                cmd.Parameters.AddWithValue("@compId", CommonMethod.CompId);
                cmd.ExecuteNonQuery();
                db.CloseConnection();
                UnPaidBonusTable = (new DbClass()).GetTable("Select * from TblunpaidBonusList");
                lblTBonus.Text = Convert.ToDecimal(UnPaidBonusTable.Compute("SUM(RemainingAmt)", string.Empty)).ToString();
                GvreceiptInfo.DataSource = UnPaidBonusTable;
                GvreceiptInfo.Columns["Id"].Visible = false;
                GvreceiptInfo.Columns["CustomerId"].Visible = false;
                GvreceiptInfo.Columns["BId"].Visible = false;
            }
            catch { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (new DbClass()).GetTable("SELECT TOP(1) RemainingAmt from dbo.CustomerBonus where  CustomerBonus.CustomerId='" + cmbcustomer.SelectedValue + "' and CustomerBonus.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBonus.compId=" + CommonMethod.CompId + " order by Id desc");
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        if (dt.Rows[0][0] != null && dt.Rows[0][0].ToString() != "")
                            lblTBonus.Text = dt.Rows[0][0].ToString();
                    }
                    catch (Exception)
                    { lblTBonus.Text = "0.00"; }
                }
                else
                    lblTBonus.Text = "0.00";
            }
            catch { }
            try
            {
                DataTable dt = new DataTable();
                try
                {
                    dt = UnPaidBonusTable.Select("CustomerId=" + cmbcustomer.SelectedValue).CopyToDataTable();
                }
                catch { MessageBox.Show("Record not found"); }
                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Columns["Id"].Visible = false;
                GvreceiptInfo.Columns["CustomerId"].Visible = false;
                GvreceiptInfo.Columns["BId"].Visible = false;
            }
            catch { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (frmBonus.newbonus == null)
                    {
                        frmBonus custbill = new frmBonus(Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["CustomerId"].Value), this);
                        custbill.ShowDialog();
                    }
                    else
                    {
                        this.Close();
                        frmBonus.newbonus.bonusdata(Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["CustomerId"].Value), this);
                        frmBonus.newbonus.Show();
                        frmBonus.newbonus = null;
                    }
                }
            }
            catch { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            fillgrid();
        }
    }
}