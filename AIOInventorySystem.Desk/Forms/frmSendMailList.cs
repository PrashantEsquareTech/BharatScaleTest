using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSendMailList : Form
    {
        DbClass db = new DbClass();

        public frmSendMailList()
        {
            InitializeComponent();
            LoadData();
            fillCombo();
            this.ActiveControl = cmbsname;
        }

        public void MailCount()
        {
            try
            {
                SemdMailRepository sendRepo = new SemdMailRepository();
                lblTotalSendMail.Text = Convert.ToString(sendRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count());
                sendRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillCombo()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT MailBy FROM SendMail where CompId='" + CommonMethod.CompId + "' order by MailBy", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbsname.DataSource = dt;
                cmbsname.ValueMember = "MailBy";
                cmbsname.DisplayMember = "MailBy";
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void LoadData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MailBy, CONVERT(varchar(50), MailDate, 103) AS Mail_Date, MailFrom, MailTo, Attachement, MailSubject, MailBody FROM  dbo.SendMail where CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvSendMail.DataSource = dt;
                GvSendMail.Refresh();
                lblTotalSendMail.Text = Convert.ToString(GvSendMail.Rows.Count);
                GvSendMail.Columns[0].Width = 100;
                GvSendMail.Columns[1].Width = 65;
                GvSendMail.Columns[2].Width = 100;
                GvSendMail.Columns[3].Width = 100;
                GvSendMail.Columns[4].Width = 120;
                GvSendMail.Columns[5].Width = 130;
                GvSendMail.Columns[6].Width = 130;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MailBy, CONVERT(varchar(50), MailDate, 103) AS Mail_Date, MailFrom, MailTo, Attachement, MailSubject, MailBody FROM   dbo.SendMail Where MailBy='" + cmbsname.Text + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvSendMail.DataSource = dt;
                GvSendMail.Refresh();
                lblTotalSendMail.Text = Convert.ToString(GvSendMail.Rows.Count);
                GvSendMail.Columns[0].Width = 100;
                GvSendMail.Columns[1].Width = 65;
                GvSendMail.Columns[2].Width = 100;
                GvSendMail.Columns[3].Width = 100;
                GvSendMail.Columns[4].Width = 120;
                GvSendMail.Columns[5].Width = 130;
                GvSendMail.Columns[6].Width = 130;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MailBy, CONVERT(varchar(50), MailDate, 103) AS Mail_Date, MailFrom, MailTo, Attachement, MailSubject, MailBody FROM dbo.SendMail where CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvSendMail.DataSource = dt;
                GvSendMail.Refresh();
                GvSendMail.Columns[0].Width = 100;
                GvSendMail.Columns[1].Width = 65;
                GvSendMail.Columns[2].Width = 100;
                GvSendMail.Columns[3].Width = 100;
                GvSendMail.Columns[4].Width = 120;
                GvSendMail.Columns[5].Width = 130;
                GvSendMail.Columns[6].Width = 130;
                da.Dispose();
                dt.Dispose();
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

        private void frmSendMailList_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void cmbsname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception) { }
        }
    }
}