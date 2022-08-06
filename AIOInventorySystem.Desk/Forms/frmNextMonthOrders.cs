using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmNextMonthOrders : Form
    {
        OrderbookingRepository orderbookrepo;

        public frmNextMonthOrders()
        {
            InitializeComponent();
            Bindgrid();
        }

        public void Bindgrid()
        {
            try
            {
                orderbookrepo = new OrderbookingRepository();
                List<OrderBook> orderbook = new List<OrderBook>();
                List<OrderBook> allorders = orderbookrepo.GetAll().Where(t => t.status == "1" && t.CompId == CommonMethod.CompId).ToList();
                foreach (var item in allorders)
                {
                    var createdate = item.Delieverydate;
                    var futuredate = Convert.ToDateTime(DateTime.Now).AddMonths(1);
                    if (createdate <= futuredate && createdate >= DateTime.Now)
                        orderbook.Add(item);
                }
                foreach (var item in orderbook)
                {
                    GvorderInfo.Rows.Add();
                    int i = GvorderInfo.RowCount;
                    GvorderInfo.Rows[i - 1].Cells["OrderBookingNo"].Value = item.Orderbookno;
                    GvorderInfo.Rows[i - 1].Cells["OrderBookingDate"].Value = item.Orderbookdate.Value;
                    GvorderInfo.Rows[i - 1].Cells["CustomerName"].Value = item.Customername;
                    GvorderInfo.Rows[i - 1].Cells["DeliveryDate"].Value = item.Delieverydate.Value;
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

        private void frmNextMonthOrders_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
    }
}
