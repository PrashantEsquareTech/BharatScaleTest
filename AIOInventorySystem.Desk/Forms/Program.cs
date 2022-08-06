using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Forms;
using Microsoft.Win32;


namespace AIOInventorySystem.Desk
{
    static class Program
    {
        private static readonly Mutex mutex = new Mutex(false, "e1a1edfa-7382-41e5-ab56-9b0841ed2071");
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DbClass db = new DbClass();
            int flag = 0;
            long Serial_key = 0;
            DateTime ExpiryDate = DateTime.Today, CurrentDate = DateTime.Today;
            DataTable dt = db.GetTable("Select top 1 * from TrailPeriod");
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0]["Version"].ToString().Trim() == "Full")
                    flag = 0;
                else
                    flag = 1;
            }
            else
                flag = 0;

            try
            {
                RegistryKey soft_key = Registry.CurrentUser.OpenSubKey("Software", true);
                RegistryKey newKey = soft_key.OpenSubKey("ESquare_SoftKey");
                Serial_key = (long)Convert.ToDouble(newKey.GetValue("ESquare_SoftKey"));
                if (flag == 1)
                {
                    RegistryKey ExpiryKey = soft_key.OpenSubKey("ESquare_SoftKeyExpiry");
                    ExpiryDate = Convert.ToDateTime(ExpiryKey.GetValue("ESquare_SoftKeyExpiry"));

                    try
                    {
                        RegistryKey CurrentKey = soft_key.OpenSubKey("ESquare_SoftKeyCurrent");
                        CurrentDate = Convert.ToDateTime(CurrentKey.GetValue("ESquare_SoftKeyCurrent"));
                    }
                    catch (Exception)
                    { }
                }
                if (Serial_key == 0)   // if serial key is not updated in repository then this code will work
                {                      // if in any case rachitsoftwarekey.xml file is delete then above code will work                     
                    if (Serial_key != 0)
                    {
                        CommonMethod cm = new CommonMethod();
                        if (cm.chekKey(Serial_key) == true)
                        {
                            CommonMethod.soft_serialkey = Serial_key;
                            if (flag == 1)
                            {
                                int DaysCnt = 0;
                                DaysCnt = (ExpiryDate - DateTime.Today).Days;
                                if (ExpiryDate < CurrentDate || DaysCnt < 0)
                                    MessageBox.Show("License is expired. Contact ESquare Technology for updation.", "Expired Warning");
                                else if (DaysCnt <= 15)
                                    MessageBox.Show("License will expire soon. Contact ESquare Technology for updation.", "Expire Alert");
                            }
                        }
                        else
                            MessageBox.Show("Serial Key file is corrupted!.", "Activation Warning");  // Message shown if wrong file found
                    }
                }
                else
                {
                    CommonMethod.soft_serialkey = Serial_key;
                    if (flag == 1)
                    {
                        int DaysCnt = 0;
                        DaysCnt = (ExpiryDate - DateTime.Today).Days;
                        if (ExpiryDate < CurrentDate || DaysCnt < 0)
                            MessageBox.Show("License is expired. Contact ESquare Technology for updation.", "Expired Warning");
                        else if (DaysCnt <= 15)
                            MessageBox.Show("License will expire soon. Contact ESquare Technology for updation.", "Expire Alert");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on checking key " + ex.Message);
                Serial_key = 0;
            }

            if (CommonMethod.soft_serialkey != 0)
            {
                if (flag == 1)
                {
                    if (ExpiryDate >= CurrentDate)
                    {
                        try
                        {
                            RegistryKey soft_key = Registry.CurrentUser.OpenSubKey("Software", true);
                            RegistryKey CurrentKey;
                            try
                            {
                                CurrentKey = soft_key.OpenSubKey("ESquare_SoftKeyCurrent");
                                CurrentKey.SetValue("ESquare_SoftKeyCurrent", Convert.ToString(DateTime.Today));
                            }
                            catch (Exception)
                            {
                                CurrentKey = soft_key.CreateSubKey("ESquare_SoftKeyCurrent");
                                CurrentKey.SetValue("ESquare_SoftKeyCurrent", Convert.ToString(DateTime.Today));
                            }
                        }
                        catch (Exception)
                        { }
                        Application.Run(new frmloginForm());
                    }
                    else
                    {
                        Application.Run(new frmloginForm("V"));
                    }
                }
                else
                    Application.Run(new frmloginForm());
            }
            else if (CommonMethod.soft_serialkey == 0)
                Application.Run(new frmloginForm());
            else
                Application.Exit();
        }
    }
}