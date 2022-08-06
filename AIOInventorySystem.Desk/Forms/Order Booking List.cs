using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class Order_Booking_List : Form
    {
        public Order_Booking_List()
        {
            InitializeComponent();
            Bindgrid();
            customerdata();
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Bindgrid()
        {
            try
            {
                OrderbookingRepository orderbookrepo = new OrderbookingRepository();
                List<OrderBook> ItemList = orderbookrepo.GetAll();
                if (ItemList != null)
                {
                    dtgvList.Rows.Clear();
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells[0].Value = item.Orderbookno;
                        dtgvList.Rows[i].Cells[1].Value = item.Orderbookdate.Value.ToString("dd-MM-yyyy"); ;
                        dtgvList.Rows[i].Cells[2].Value = item.Customername;
                        i++;
                    }
                }
                orderbookrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkcustname_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcustname.Checked == true)
                chkorderno.Checked = false;
        }

        private void chkorderno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkorderno.Checked == true)
                chkcustname.Checked = false;
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

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void txtorderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void Order_Booking_List_Load_1(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            customerdata();
            Bindgrid();
            chkcustname.Checked = false;
            chkorderno.Checked = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (chkcustname.Checked == true)
            {
                try
                {
                    OrderbookingRepository orderbookrepo = new OrderbookingRepository();
                    List<OrderBook> ItemList = orderbookrepo.GetAll().Where(t => t.Customername == cmbcustomername.Text).ToList();
                    if (ItemList != null)
                    {
                        dtgvList.Rows.Clear();
                        int i = 0;

                        foreach (var item in ItemList)
                        {
                            dtgvList.Rows.Add();
                            dtgvList.Rows[i].Cells["OrderBookingNo"].Value = item.Orderbookno;
                            dtgvList.Rows[i].Cells["OrderBookingDate"].Value = item.Orderbookdate.Value.ToString("dd-MM-yyyy"); ;
                            dtgvList.Rows[i].Cells["CustomerName"].Value = item.Customername;
                            i++;
                        }
                        this.ActiveControl = cmbcustomername;
                    }
                    orderbookrepo.Dispose();
                }
                catch (Exception)
                { }
            }
            else
            {
                try
                {
                    OrderbookingRepository orderbookrepo = new OrderbookingRepository();
                    List<OrderBook> ItemList = orderbookrepo.GetAll().Where(t => t.Orderbookno == Convert.ToInt32(txtorderno.Text)).ToList();
                    if (ItemList != null)
                    {
                        dtgvList.Rows.Clear();
                        int i = 0;
                        foreach (var item in ItemList)
                        {
                            dtgvList.Rows.Add();
                            dtgvList.Rows[i].Cells["OrderBookingNo"].Value = item.Orderbookno;
                            dtgvList.Rows[i].Cells["OrderBookingDate"].Value = item.Orderbookdate.Value.ToString("dd-MM-yyyy"); ;
                            dtgvList.Rows[i].Cells["CustomerName"].Value = item.Customername;
                            i++;
                        }
                        this.ActiveControl = txtorderno;
                    }
                    orderbookrepo.Dispose();
                }
                catch (Exception)
                { }
            }
        }
    }
}
