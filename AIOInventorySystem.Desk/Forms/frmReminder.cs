using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmReminder : Form
    {
        public int reminderid;

        public frmReminder()
        {
            InitializeComponent();
            maxRemid();
            BindReminfo();
            btndelete.Enabled = false;
        }

        public void maxRemid()
        {
            try
            {
                ReminderRepository remrepo = new ReminderRepository();
                var mid = remrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count();
                if (mid == null)
                    txtReminderID.Text = "1";
                else
                    txtReminderID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                remrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtremmessage.Text = "";
                maxRemid();
                dateTimePicker1.Value = DateTime.Now.Date;
                dateTimePicker2.Value = DateTime.Now.Date;
                BindReminfo();
            }
            catch (Exception)
            { }
        }

        public void BindReminfo()
        {
            try
            {
                ReminderRepository remrepo = new ReminderRepository();
                dtgvList.Rows.Clear();
                List<Reminder> ItemList = remrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList(); ;
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        dtgvList.Rows[i].Cells["RemID"].Value = item.ReminderId;
                        dtgvList.Rows[i].Cells["CreateDate"].Value = item.Createdate;
                        dtgvList.Rows[i].Cells["Date"].Value = item.Date;
                        dtgvList.Rows[i].Cells["Time"].Value = item.Time;
                        dtgvList.Rows[i].Cells["Message"].Value = item.RemMessage;
                        dtgvList.Rows[i].Cells["CompIdg"].Value = item.CompId;
                        i++;
                    }
                }
                remrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void frmReminder_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.CustomFormat = "HH:mm:ss"; // Only use hours and minutes
                dateTimePicker2.ShowUpDown = true;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ReminderRepository remrepo1 = new ReminderRepository();
                var Remdata = remrepo1.GetAll().Where(t => t.ReminderId == Convert.ToInt32(txtReminderID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (Remdata != null)
                {
                    MessageBox.Show("This Reminder Already Present.", "Warning");
                    clear();
                    this.ActiveControl = txtReminderID;
                }
                else
                {
                    if (txtremmessage.Text != "")
                    {
                        ReminderRepository remrepo = new ReminderRepository();
                        Reminder reminfo = new Reminder();
                        maxRemid();
                        reminfo.ReminderId = Convert.ToInt32(txtReminderID.Text);
                        reminfo.Date = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                        reminfo.Time = Convert.ToDateTime(dateTimePicker2.Value.ToString("HH:mm:ss"));
                        reminfo.RemMessage = txtremmessage.Text;
                        reminfo.Createdate = DateTime.Now.Date;
                        reminfo.CompId = CommonMethod.CompId;
                        remrepo.Add(reminfo);
                        remrepo.Save();
                        MessageBox.Show("Reminder Add Successfully.", "Success");
                        clear();
                        this.ActiveControl = txtReminderID;
                        remrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Reminder message.", "Warning");
                        this.ActiveControl = txtremmessage;
                    }
                }
                remrepo1.Dispose();
            }
            catch (Exception)
            { }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ReminderRepository remrepo = new ReminderRepository();
                Reminder reminfo = remrepo.GetById(reminderid);
                remrepo.Remove(reminfo);
                remrepo.Save();
                MessageBox.Show("Reminder Deleted Sucessfully.", "Success");
                clear();
                btnAdd.Enabled = true;
                btndelete.Enabled = false;
                this.ActiveControl = txtReminderID;
                remrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dateTimePicker2;
            }
            catch (Exception) { }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtremmessage;
            }
            catch (Exception) { }
        }

        private void txtremmessage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl= btnAdd;
            }
            catch (Exception) { }
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                reminderid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtReminderID.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["RemID"].Value);
                dateTimePicker1.Value = Convert.ToDateTime(dtgvList.Rows[e.RowIndex].Cells["Date"].Value);
                dateTimePicker2.Value = Convert.ToDateTime(dtgvList.Rows[e.RowIndex].Cells["Time"].Value);
                txtremmessage.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Message"].Value);
                btnAdd.Enabled = false;
                btndelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}