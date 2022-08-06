using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk
{
    public class CommonMethod
    {
        CustomerRepository custRepo;
        public static int? CompId;
        public static string TransactionYear, PreTransactionYear, NextTransactionYear, firstyear, lastyear, mac_Add;
        public static DateTime startdate, enddate, prestartdate, preenddate;
        public static long soft_serialkey;
        public static Boolean? commProduct = false;

        public void changedatetimepickerrange(Form form)
        {            
            foreach (Control control in form.Controls)
            {
                if (control is GroupBox)
                {
                    GroupBox grpbox = control as GroupBox;
                    foreach (Control control1 in grpbox.Controls)
                    {
                        if (control1 is DateTimePicker)
                        {
                            DateTimePicker dtpicker = control1 as DateTimePicker;
                            dtpicker.MinDate = CommonMethod.startdate;
                            dtpicker.MaxDate = CommonMethod.enddate;
                        }
                    }
                }
                //else if (control is grpBox)
                //{
                //    SgrpBox grpbox = control as SgrpBox;
                //    foreach (Control control1 in grpbox.Controls)
                //    {
                //        if (control1 is DateTimePicker)
                //        {
                //            DateTimePicker dtpicker1 = control1 as DateTimePicker;
                //            dtpicker1.MinDate = CommonMethod.startdate;
                //            dtpicker1.MaxDate = CommonMethod.enddate;
                //        }
                //    }
                //}
                else if (control is TableLayoutPanel)
                {
                    TableLayoutPanel tblpnl = control as TableLayoutPanel;
                    foreach (Control control1 in tblpnl.Controls)
                    {
                        if (control1 is DateTimePicker)
                        {
                            DateTimePicker dtpicker = control1 as DateTimePicker;
                            dtpicker.MinDate = CommonMethod.startdate;
                            dtpicker.MaxDate = CommonMethod.enddate;
                        }
                        else if (control1 is TableLayoutPanel)
                        {
                            TableLayoutPanel tblpnl1 = control1 as TableLayoutPanel;
                            foreach (Control control2 in tblpnl1.Controls)
                            {
                                if (control2 is DateTimePicker)
                                {
                                    DateTimePicker dtpicker = control2 as DateTimePicker;
                                    dtpicker.MinDate = CommonMethod.startdate;
                                    dtpicker.MaxDate = CommonMethod.enddate;
                                }
                                else if (control2 is TableLayoutPanel)
                                {
                                    TableLayoutPanel tblpnl3 = control2 as TableLayoutPanel;
                                    foreach (Control control4 in tblpnl3.Controls)
                                    {
                                        if (control4 is DateTimePicker)
                                        {
                                            DateTimePicker dtpicker = control4 as DateTimePicker;
                                            dtpicker.MinDate = CommonMethod.startdate;
                                            dtpicker.MaxDate = CommonMethod.enddate;
                                        }
                                        else if (control4 is TableLayoutPanel)
                                        {
                                            TableLayoutPanel tblpnl4 = control4 as TableLayoutPanel;
                                            foreach (Control control5 in tblpnl4.Controls)
                                            {
                                                if (control5 is DateTimePicker)
                                                {
                                                    DateTimePicker dtpicker = control5 as DateTimePicker;
                                                    dtpicker.MinDate = CommonMethod.startdate;
                                                    dtpicker.MaxDate = CommonMethod.enddate;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (control1 is GroupBox)
                        {
                            GroupBox grpbox = control1 as GroupBox;
                            foreach (Control control3 in grpbox.Controls)
                            {
                                if (control3 is TableLayoutPanel)
                                {
                                    TableLayoutPanel tblpnl1 = control3 as TableLayoutPanel;
                                    foreach (Control control4 in tblpnl1.Controls)
                                    {
                                        if (control4 is DateTimePicker)
                                        {
                                            DateTimePicker dtpicker = control4 as DateTimePicker;
                                            dtpicker.MinDate = CommonMethod.startdate;
                                            dtpicker.MaxDate = CommonMethod.enddate;
                                        }
                                        else if (control4 is TableLayoutPanel)
                                        {
                                            TableLayoutPanel tblpnl2 = control4 as TableLayoutPanel;
                                            foreach (Control control5 in tblpnl2.Controls)
                                            {
                                                if (control5 is DateTimePicker)
                                                {
                                                    DateTimePicker dtpicker = control5 as DateTimePicker;
                                                    dtpicker.MinDate = CommonMethod.startdate;
                                                    dtpicker.MaxDate = CommonMethod.enddate;
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (control3 is DateTimePicker)
                                {
                                    DateTimePicker dtpicker = control3 as DateTimePicker;
                                    dtpicker.MinDate = CommonMethod.startdate;
                                    dtpicker.MaxDate = CommonMethod.enddate;
                                }
                            }
                        }
                    }
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dtpicker2 = control as DateTimePicker;
                    dtpicker2.MinDate = CommonMethod.startdate;
                    dtpicker2.MaxDate = CommonMethod.enddate;
                }
            }
        }

        public int ValidCustomer(string custName)
        {
            custRepo = new CustomerRepository();
            CustomerInformation custData = custRepo.GetAll().Where(t => t.CustomerName.ToLower() == custName.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (custData == null)
                return 0;
            else
                return 1;
        }

        public int ValidSupplier(string suppName)
        {
            SupplierRepository suppRepo = new SupplierRepository();
            SupplierInformation suppData = suppRepo.GetAll().Where(t => t.SupplierName.ToLower() == suppName.ToLower()).FirstOrDefault();
            if (suppData == null)
                return 0;
            else
                return 1;
        }

        public int ValidExpence(string ExpName)
        {
            ExpenceMasterRepository ExpRepo = new ExpenceMasterRepository();
            ExpenceMaster ExpData = ExpRepo.GetAll().Where(t => t.ExpenceName.ToLower() == ExpName.ToLower()).FirstOrDefault();
            if (ExpData == null)
                return 0;
            else
                return 1;
        }

        public string ValidToDate(DateTime toDate)
        {
            try
            {
                string onlyDate = toDate.ToString("MM/dd/yyyy");
                string rStr = onlyDate.Split(' ')[0] + " 23:59:59.997";
                return rStr;
            }
            catch (Exception)
            {
                string rStr = toDate.ToString("MM/dd/yyyy");
                return rStr;
            }
        }

        public string ValidFromDate(DateTime fromdate)
        {
            try
            {
                string onlyDate = fromdate.ToString("MM/dd/yyyy");
                string rStr = onlyDate.Split(' ')[0] + " 00:00:00.000";
                return rStr;
            }
            catch (Exception)
            {
                string rStr = fromdate.ToString("MM/dd/yyyy");
                return rStr;
            }
        }

        public Boolean CheckForUpdation(string nextyr)
        {
            Boolean returnvalue;
            RemainingPaymentRepository CustLedgerRepo = new RemainingPaymentRepository();
            SupplierRemainingPaymentRepository SuppLedgerRepo = new SupplierRemainingPaymentRepository();
            StockRepository StockRepo = new StockRepository();
            StockOpeningRepository StockOpeningRepo = new StockOpeningRepository();

            RemainingPayment CustLedger = new RemainingPayment();
            SupplierRemainingPayment SuppLedger = new SupplierRemainingPayment();
            AIOInventorySystem.Data.Model.Stock StockData = new Data.Model.Stock();
            StockOpening StockOpeningDataVat = new StockOpening();
            StockOpening StockOpeningDataNonVat = new StockOpening();

            CustLedger = CustLedgerRepo.GetAll().Where(t => t.TransactionYear == nextyr).FirstOrDefault();
            SuppLedger = SuppLedgerRepo.GetAll().Where(t => t.TransactionYear == nextyr).FirstOrDefault();
            StockData = StockRepo.GetAll().Where(t => t.TransactionYear == nextyr).FirstOrDefault();
            StockOpeningDataVat = StockOpeningRepo.GetAll().Where(t => t.Vat == true && t.TransactionYear == nextyr).FirstOrDefault();
            StockOpeningDataNonVat = StockOpeningRepo.GetAll().Where(t => t.Vat == false && t.TransactionYear == nextyr).FirstOrDefault();

            if (CustLedger != null && SuppLedger != null && StockData != null && StockOpeningDataVat != null && StockOpeningDataNonVat != null)
                returnvalue = true;
            else
                returnvalue = false;
            if (StockData != null)
                returnvalue = true;
            else
                returnvalue = false;
            CustLedgerRepo.Dispose(); SuppLedgerRepo.Dispose(); StockRepo.Dispose(); StockOpeningRepo.Dispose();
            return returnvalue;
        }

        public string GetMACAddress()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration where IPEnabled=true");
            IEnumerable<ManagementObject> objects = searcher.Get().Cast<ManagementObject>();
            string mac = (from o in objects orderby o["IPConnectionMetric"] select o["MACAddress"].ToString()).FirstOrDefault();
            return mac;
        }

        public long GetSerial()
        {
            long Sr_sum = 0;
            int sumIndex = 1;
            long Serial_HD = 0;

            mac_Add = GetMACAddress();
            foreach (char ch in mac_Add)
            {
                if (char.IsDigit(ch))
                {
                    string chardg = ch.ToString();
                    int chnum = 0;
                    bool canParse = false;
                    canParse = int.TryParse(chardg, out chnum);
                    if (canParse == true)
                        Sr_sum += Sr_sum + chnum * (sumIndex * 2);
                }
                else if (char.IsLetter(ch))
                {
                    switch (ch.ToString().ToUpper())
                    {
                        case "A":
                            Sr_sum += Sr_sum + 14 * (sumIndex * 2);
                            break;
                        case "B":
                            Sr_sum += Sr_sum + 13 * (sumIndex * 2);
                            break;
                        case "C":
                            Sr_sum += Sr_sum + 12 * (sumIndex * 2);
                            break;
                        case "D":
                            Sr_sum += Sr_sum + 11 * (sumIndex * 2);
                            break;
                        case "E":
                            Sr_sum += Sr_sum + 10 * (sumIndex * 2);
                            break;
                    }
                }
                sumIndex++;
            }
            Serial_HD = getHDSerial();
            Sr_sum = Sr_sum + Serial_HD;
            return Sr_sum;
        }

        public bool chekKey(long serial_Key)
        {
            long x, y;
            x = GetSerial();
            y = x * x + 53 / x + 113 * (x / 4);
            string str_Serial = y.ToString();
            if (str_Serial.Length != 10)
            {
                int extlength = str_Serial.Length - 10;
                if (extlength < 0)
                {
                    int ext_j = extlength * -1;
                    for (int j = 0; j < ext_j; j++)
                    {
                        str_Serial = str_Serial + j;
                    }
                }
                else if (extlength > 0)
                    str_Serial = str_Serial.Substring(0, 10);
            }
            y = (long)Convert.ToDouble(str_Serial);
            if (y == serial_Key)
                return true;
            return false;
        }

        private long getHDSerial()
        {
            string hd_Serial1 = "";
            string hd_Serial2 = "";
            StringBuilder op_SERIAL = new StringBuilder();
            //  // for harddisk serial no
            ArrayList hdCollection = new ArrayList();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();
                hdCollection.Add(hd);
            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                try
                {
                    HardDrive hd = (HardDrive)hdCollection[i];
                    // get the hardware serial no.
                    if (i == 0)
                    {
                        if (wmi_HD["SerialNumber"] == null)
                            hd.SerialNo = "None";
                        else
                        {
                            hd.SerialNo = wmi_HD["SerialNumber"].ToString();

                            hd_Serial1 = hd.SerialNo.Trim();
                            hd_Serial1 = hd_Serial1.Substring(0, 5);
                            hd_Serial2 = hd.SerialNo.Trim();
                            hd_Serial2 = hd_Serial2.Substring(3, 8);
                        }
                    }
                    ++i;
                }
                catch (Exception)
                { }
            }

            //-----------------------------------------
            hd_Serial1 = hd_Serial1 + hd_Serial2;
            string op_ProductID = "";
            try
            {
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (ManagementObject wmi_OP in searcher.Get())
                {
                    object obj = wmi_OP["Manufacturer"];
                    obj = wmi_OP["SerialNumber"];
                    op_ProductID = obj.ToString();
                    if (op_ProductID.Contains('-'))
                        op_ProductID = op_ProductID.Replace('-', '0');
                    int ser_num = Convert.ToInt32(op_ProductID.Length);
                    for (int ii = ser_num - 1; ii >= 0; ii--)
                    {
                        op_SERIAL = op_SERIAL.Append(op_ProductID[ii]);
                    }
                    op_ProductID = Convert.ToString(op_SERIAL);
                    op_ProductID = op_ProductID.Substring(0, 5);
                }

                if (hd_Serial1 != "")
                    hd_Serial1 = hd_Serial1 + op_ProductID;
                else
                    hd_Serial1 = op_ProductID;
            }
            catch (Exception)
            { }

            long Sr_sum = 0;
            int sumIndex = 1;

            foreach (char ch in hd_Serial1)
            {
                if (char.IsDigit(ch))
                {
                    string chardg = ch.ToString();
                    int chnum = 0;
                    bool canParse = false;
                    canParse = int.TryParse(chardg, out chnum);
                    if (canParse == true)
                        Sr_sum += Sr_sum + chnum * (sumIndex * 2);
                }
                else if (char.IsLetter(ch))
                {
                    switch (ch.ToString().ToUpper())
                    {
                        case "A":
                            Sr_sum += Sr_sum + 14 * (sumIndex * 2);
                            break;
                        case "B":
                            Sr_sum += Sr_sum + 13 * (sumIndex * 2);
                            break;
                        case "C":
                            Sr_sum += Sr_sum + 12 * (sumIndex * 2);
                            break;
                        case "D":
                            Sr_sum += Sr_sum + 11 * (sumIndex * 2);
                            break;
                        case "E":
                            Sr_sum += Sr_sum + 10 * (sumIndex * 2);
                            break;
                        default:
                            Sr_sum += Sr_sum + 19 * (sumIndex * 2);
                            break;
                    }
                }
                sumIndex++;
            }
            return Sr_sum;
        }

        //HardDrive class
        class HardDrive
        {
            private string model = null;
            private string type = null;
            private string serialNo = null;
            public string Model
            {
                get { return model; }
                set { model = value; }
            }
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            public string SerialNo
            {
                get { return serialNo; }
                set { serialNo = value; }
            }
        }
    }
}