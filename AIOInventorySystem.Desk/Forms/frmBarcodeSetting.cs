using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBarcodeSetting : Form
    {
        BarcodeSettingRepository BarcodeSetRepo;
        public int setid, updateId, i = 0, j = 0;
        public List<string> lstPrinterList = new List<string>();

        public frmBarcodeSetting()
        {
            InitializeComponent();

            btnUpdate.Enabled = false;
            string[] Property = { "Prefix", "Group Name", "Product Name", "Marathi Product Name", "Mfg Company Name", "Unit Name", "MRP", "Warranty", "HSN Code", "Shop Name", "Marathi Shop Name", "Purchase Rate", "Sale Rate" };
            chklstproperty1.Items.AddRange(Property);
            chklstproperty1.CheckOnClick = true;
            chklstproperty2.Items.AddRange(Property);
            chklstproperty2.CheckOnClick = true;
            if (chkUserDefined.Checked == false)
                txtUserDefined.Enabled = false;
            else
                txtUserDefined.Enabled = true;
            getsettingdata();
        }

        public void getsettingdata()
        {
            try
            {
                BarcodeSetRepo = new BarcodeSettingRepository();
                var data = BarcodeSetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (data != null)
                {
                    updateId = data.Id;
                    string c = data.Property3;
                    string[] arr1 = data.Property1.Split(','), arr2 = data.Property2.Split(',');
                    for (int k = 0; k < arr1.Length; k++)
                    {
                        try
                        {
                            foreach (Object item in chklstproperty1.Items)
                            {
                                if (item.ToString() == arr1[k].ToString())
                                {
                                    int index = chklstproperty1.Items.IndexOf(item);
                                    chklstproperty1.SetItemCheckState(index, CheckState.Checked);
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }

                    for (int m = 0; m < arr2.Length; m++)
                    {
                        try
                        {
                            foreach (Object item in chklstproperty2.Items)
                            {
                                if (item.ToString() == arr2[m].ToString())
                                {
                                    int index = chklstproperty2.Items.IndexOf(item);
                                    chklstproperty2.SetItemCheckState(index, CheckState.Checked);
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    if (c == "Prefix")
                        rdbtnprefix.Checked = true;
                    else if (c == "Group Name")
                        rdbtngroupname.Checked = true;
                    else if (c == "Product Name")
                        rdbtnproductname.Checked = true;
                    else if (c == "Marathi Product Name")
                        rdbtnMarathiproductname.Checked = true;
                    else if (c == "Mfg.Company Name")
                        rdbtnmfgcomname.Checked = true;
                    else if (c == "Unit Name")
                        rdbtnunitname.Checked = true;
                    else if (c == "MRP")
                        rdbtnmrp.Checked = true;
                    else if (c == "Warranty")
                        rdbtnwarranty.Checked = true;
                    else if (c == "HSN Code")
                        rdbtnhsncode.Checked = true;
                    else if (c == "Shop Name")
                        rdbtnshopname.Checked = true;
                    else if (c == "Marathi Shop Name")
                        rdbtnmarathishopname.Checked = true;
                    else if (c == "Purchase Rate")
                        rdbtnpurchaserate.Checked = true;
                    else if (c == "Sale Rate")
                        rdbtnsalerate.Checked = true;
                    else
                    {
                        chkUserDefined.Checked = true;
                        txtUserDefined.Text = c;
                        foreach (Control control in this.grpproperty3.Controls)
                        {
                            if (control is RadioButton)
                            {
                                RadioButton radio = control as RadioButton;
                                if (radio.Checked)
                                    radio.Checked = false;
                            }
                        }
                    }
                    txtPrinterName.Text = data.PrinterName;
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BarcodeSetRepo = new BarcodeSettingRepository();
                BarcodeSetting Barcodesetdata = new BarcodeSetting();
                string prop1 = "", prop2 = "";
                foreach (object itemChecked in chklstproperty1.CheckedItems)
                {
                    if (prop1 == "")
                        prop1 = itemChecked.ToString();
                    else
                        prop1 = prop1 + "," + itemChecked.ToString();
                    Barcodesetdata.Property1 = prop1.ToString();
                }
                foreach (object itemChecked in chklstproperty2.CheckedItems)
                {
                    if (prop2 == "")
                        prop2 = itemChecked.ToString();
                    else
                        prop2 = prop2 + "," + itemChecked.ToString();
                    Barcodesetdata.Property2 = prop2.ToString();
                }
                foreach (Control control in tblltProperty3.Controls)
                {
                    if (control is RadioButton)
                    {
                        RadioButton radio = control as RadioButton;
                        if (radio.Checked)
                            Barcodesetdata.Property3 = radio.Text;
                    }
                    else if (control is CheckBox)
                    {
                        CheckBox chk = control as CheckBox;
                        if (chk.Checked)
                            Barcodesetdata.Property3 = txtUserDefined.Text;
                    }
                }
                Barcodesetdata.CompId = Convert.ToInt32(CommonMethod.CompId);
                Barcodesetdata.PrinterName = txtPrinterName.Text;
                BarcodeSetRepo.Add(Barcodesetdata);
                BarcodeSetRepo.Save();
                MessageBox.Show("Barcode Settings are saved successfully.", "Success");
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BarcodeSetRepo = new BarcodeSettingRepository();
                BarcodeSetting Barcodesetdata = new BarcodeSetting();
                Barcodesetdata = BarcodeSetRepo.GetAll().LastOrDefault();
                string prop1 = "", prop2 = "";
                foreach (object itemChecked in chklstproperty1.CheckedItems)
                {
                    if (prop1 == "")
                        prop1 = itemChecked.ToString();
                    else
                        prop1 = prop1 + "," + itemChecked.ToString();
                    Barcodesetdata.Property1 = prop1.ToString();
                }
                foreach (object itemChecked in chklstproperty2.CheckedItems)
                {
                    if (prop2 == "")
                        prop2 = itemChecked.ToString();
                    else
                        prop2 = prop2 + "," + itemChecked.ToString();
                    Barcodesetdata.Property2 = prop2.ToString();
                }
                foreach (Control control in tblltProperty3.Controls)
                {
                    if (control is RadioButton)
                    {
                        RadioButton radio = control as RadioButton;
                        if (radio.Checked)
                            Barcodesetdata.Property3 = radio.Text;
                    }
                    else if (control is CheckBox)
                    {
                        CheckBox chk = control as CheckBox;
                        if (chk.Checked)
                            Barcodesetdata.Property3 = txtUserDefined.Text;
                    }
                }
                Barcodesetdata.CompId = Convert.ToInt32(CommonMethod.CompId);
                Barcodesetdata.PrinterName = txtPrinterName.Text;
                BarcodeSetRepo.Edit(Barcodesetdata);
                BarcodeSetRepo.Save();
                MessageBox.Show("Barcode Settings are uupdated successfully.", "Success");
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void chklstproperty_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                    i++;
                else if (e.NewValue == CheckState.Unchecked)
                    i--;
                if (i > 2)
                {
                    MessageBox.Show("Minimum 2 properties can merge.", "Warning");
                    e.NewValue = CheckState.Unchecked;
                    i = i - 1;
                }
            }
            catch (Exception)
            { }
        }

        private void chkcompname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUserDefined.Checked == true)
                {
                    txtUserDefined.Enabled = true;
                    foreach (Control control in this.grpproperty3.Controls)
                    {
                        if (control is RadioButton)
                        {
                            RadioButton radio = control as RadioButton;
                            if (radio.Checked)
                                radio.Checked = false;
                        }
                    }
                }
                else
                    txtUserDefined.Enabled = false;
            }
            catch (Exception)
            { }
        }

        public void radiobtncheck()
        {
            foreach (Control control in this.grpproperty3.Controls)
            {
                if (control is RadioButton)
                {
                    RadioButton radio = control as RadioButton;
                    if (radio.Checked)
                        radio.Checked = true;
                }
                else if (control is CheckBox)
                {
                    CheckBox chk = control as CheckBox;
                    if (chk.Checked)
                    {
                        chk.Checked = false;
                        txtUserDefined.Enabled = false;
                    }
                }
            }
        }

        private void rdbtn2articleno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void rdbtn2brand_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void rdbtn2dept_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void rdbtn2category_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void rdbtn2color_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                radiobtncheck();
            }
            catch (Exception)
            { }
        }

        private void chklstproperty2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                    j++;
                else if (e.NewValue == CheckState.Unchecked)
                    j--;
                if (j > 2)
                {
                    MessageBox.Show("Minimum 2 properties can merge.", "Warning");
                    e.NewValue = CheckState.Unchecked;
                    j = j - 1;
                }
            }
            catch (Exception)
            { }
        }

        private void lnkprintername_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                GetAllPrinterList();
                GVPrinterName.Visible = true;
            }
            catch (Exception)
            { }
        }

        private void GetAllPrinterList()
        {
            GVPrinterName.Rows.Clear();
            ManagementScope objScope = new ManagementScope(ManagementPath.DefaultPath); //For the local Access
            objScope.Connect();
            SelectQuery selectQuery = new SelectQuery();
            selectQuery.QueryString = "Select * from win32_Printer";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher(objScope, selectQuery);
            ManagementObjectCollection MOC = MOS.Get();
            foreach (ManagementObject mo in MOC)
            {
                lstPrinterList.Add(mo["Name"].ToString());
                GVPrinterName.Rows.Add(mo["Name"].ToString());
            }
        }

        private void GVPrinterName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtPrinterName.Text = Convert.ToString(GVPrinterName.Rows[e.RowIndex].Cells["PrinterNameg"].Value);
                GVPrinterName.Visible = false;
            }
            catch (Exception)
            { }
        }
    }
}