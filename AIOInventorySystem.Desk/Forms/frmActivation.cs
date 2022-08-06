using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmActivation : Form
    {
        private static frmActivation activationform;
        CommonMethod cm = new CommonMethod();

        public frmActivation()
        {
            activationform = this;
            InitializeComponent();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            string txts1 = "", txts2 = "", txts3 = "", txts4 = "", txts5 = "", txts6 = "", txts7 = "", txts8 = "", txts9 = "", txts10 = "";
            try
            {
                txts1 = (txtS1.Text == "") ? "" : txtS1.Text.Substring(0, 1);
                txts2 = (txtS2.Text == "") ? "" : txtS2.Text.Substring(0, 1);
                txts3 = (txtS3.Text == "") ? "" : txtS3.Text.Substring(0, 1);
                txts4 = (txtS4.Text == "") ? "" : txtS4.Text.Substring(0, 1);
                txts5 = (txtS5.Text == "") ? "" : txtS5.Text.Substring(0, 1);
                txts6 = (txtS6.Text == "") ? "" : txtS6.Text.Substring(0, 1);
                txts7 = (txtS7.Text == "") ? "" : txtS7.Text.Substring(0, 1);
                txts8 = (txtS8.Text == "") ? "" : txtS8.Text.Substring(0, 1);
                txts9 = (txtS9.Text == "") ? "" : txtS9.Text.Substring(0, 1);
                txts10 = (txtS10.Text == "") ? "" : txtS10.Text.Substring(0, 1);

                string strSerialKey = txts1 + txts2 + txts3 + txts4 + txts5 + txts6 + txts7 + txts8 + txts9 + txts10;
                long Serial_key = (long)Convert.ToDouble(strSerialKey);
                if (cm.chekKey(Serial_key) == false)
                {
                    MessageBox.Show("Activation Failed. Please contact to ESquare Technology,Karad.", "Activation Warning");
                    this.ActiveControl = btnActivate;
                }
                else
                {
                    RegistryKey soft_key = Registry.CurrentUser.OpenSubKey("Software", true);
                    RegistryKey newKey = soft_key.CreateSubKey("ESquare_SoftKey");
                    newKey.SetValue("ESquare_SoftKey", Convert.ToString(Serial_key));

                    //DateTime startdate, expiredate;
                    //startdate = DateTime.Today;
                    //expiredate = DateTime.Today.AddYears(1);
                    //RegistryKey ExpiryDate = soft_key.CreateSubKey("ESquare_SoftKeyExpiry");
                    //ExpiryDate.SetValue("ESquare_SoftKeyExpiry", Convert.ToString(expiredate));

                    activationform.Hide();
                    Splash splashform = new Splash();
                    splashform.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on generating Key " + ex.Message);
            }
        }

        private void frmActivation_Load(object sender, EventArgs e)
        {
            long Serial_Number;
            Serial_Number = cm.GetSerial();
            MessageBox.Show("Please Contact to ESquare Technology with this serial number= " + Serial_Number + " for serial key.", "Activation Key");
            this.ActiveControl = txtS1;
        }

        private void TextBoxFocus_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Length == 1)
            { this.SelectNextControl(((TextBox)sender), true, true, true, true); }
        }
        
        //private void txtS1_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtS1.Text.Length == 1)
        //            this.ActiveControl = txtS2;
        //    }
        //    catch (Exception)
        //    { }
        //}
    }
}
