using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSendMail : Form
    {
        MailMessage message;
        SmtpClient smtp;
        ToolTip tooltipbtn = new ToolTip();

        public frmSendMail()
        {
            InitializeComponent();
            SettingRepository setrepo = new SettingRepository();
            Setting setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            txtFrom.Text = setdata.EmailId;
            setrepo.Dispose();
        }

        public frmSendMail(string FileName)
        {
            InitializeComponent();
            SettingRepository setrepo = new SettingRepository();
            Setting setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            txtFrom.Text = setdata.EmailId;
            setrepo.Dispose();
            lblAttachment.Text = FileName;
            lblAttachment.Visible = true;
        }

        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
                MessageBox.Show("Email sending cancelled");
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message);
            else
            {
                try
                {
                    SemdMailRepository sendRepo = new SemdMailRepository();
                    SendMail sendData = new SendMail();
                    sendData.MailFrom = txtFrom.Text.Trim();
                    sendData.MailTo = txtTo.Text.Trim();
                    sendData.Attachement = lblAttachment.Text.Trim();
                    sendData.MailSubject = txtSubject.Text.Trim();
                    sendData.MailBody = txtBody.Text.Trim();
                    sendData.MailBy = txtMailSender.Text.Trim();
                    sendData.MailDate = DateTime.Now.Date;
                    sendData.CompId = CommonMethod.CompId;
                    sendRepo.Add(sendData);
                    sendRepo.Save();
                    sendRepo.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Record not saved");
                }
                MessageBox.Show("Email sent sucessfully");
                clear();
                this.ActiveControl = txtMailSender;
            }
            btnSend.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSend.Enabled == false)
                {
                    smtp.SendAsyncCancel();
                    MessageBox.Show("Email sending cancelled");
                }
                else
                {
                    if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        this.Close();
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

        private void lnkAttachFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    lblAttachment.Text = openFileDialog1.FileName;
                    lblAttachment.Visible = true;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtBody.Text = "";
                txtFrom.Text = "";
                txtMailSender.Text = "";
                txtSubject.Text = "";
                txtTo.Text = "";
            }
            catch (Exception)
            { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                lblAttachment.Visible = false;
                lblAttachment.Text = "";
            }
            catch (Exception)
            { }
        }

        private void frmSendMail_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                tooltipbtn.SetToolTip(this.lblAttachment, lblAttachment.Text);
                txtTo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT distinct MailTo FROM SendMail Where CompId='" + CommonMethod.CompId + "'", con);
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
                    txtTo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtMailSender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtTo;
            }
            catch (Exception) { }
        }

        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSubject;
            }
            catch (Exception) { }
        }

        private void txtSubject_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = lnkAttachFile;
            }
            catch (Exception) { }
        }

        private void txtBody_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSend;
            }
            catch (Exception) { }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                bool connection = NetworkInterface.GetIsNetworkAvailable();
                if (connection == true)
                {
                    if (txtMailSender.Text.Trim() != "" && txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "")
                    {
                        System.Text.RegularExpressions.Regex expr = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                        if (expr.IsMatch(txtTo.Text) && expr.IsMatch(txtFrom.Text))
                        {
                            btnSend.Enabled = false;
                            btnClose.Visible = true;
                            message = new MailMessage();
                            message.Subject = txtSubject.Text.Trim();
                            message.From = new MailAddress(txtFrom.Text.Trim());
                            message.Body = txtBody.Text;
                            message.To.Add(new MailAddress(txtTo.Text.Trim()));
                            if (lblAttachment.Text.Length > 0)
                            {
                                if (System.IO.File.Exists(lblAttachment.Text))
                                    message.Attachments.Add(new Attachment(lblAttachment.Text));
                            }
                            smtp = new SmtpClient("smtp.gmail.com");
                            smtp.Port = 25;
                            smtp.EnableSsl = true;
                            SettingRepository setrepo = new SettingRepository();
                            Setting setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (setdata != null)
                                smtp.Credentials = new NetworkCredential(setdata.EmailId, setdata.Password);
                            smtp.SendAsync(message, message.Subject);
                            smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
                            setrepo.Dispose();
                        }
                        else
                        {
                            if (expr.IsMatch(txtTo.Text))
                            { }
                            else
                            {
                                MessageBox.Show("Invalid To Email");
                                this.ActiveControl = txtTo;
                            }
                            if (expr.IsMatch(txtFrom.Text))
                            { }
                            else
                            {
                                MessageBox.Show("Invalid From Email");
                                this.ActiveControl = txtFrom;
                            }
                        }
                    }
                    else
                    {
                        if (txtMailSender.Text == "")
                        {
                            MessageBox.Show("Please add MAIL SENDER");
                            this.ActiveControl = txtMailSender;
                        }
                        else if (txtFrom.Text == "")
                        {
                            MessageBox.Show("Please add From Mail Id");
                            this.ActiveControl = txtFrom;
                        }
                        else
                        {
                            MessageBox.Show("Please add To Mail Id");
                            this.ActiveControl = txtTo;
                        }
                    }
                }
                else
                    MessageBox.Show("Internet connection in not available");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnClose.Visible = false;
                btnSend.Enabled = true;
            }
        }
    }
}