using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmServiceInvoice : Form
    {
        DbClass db = new DbClass();
        int colIndex = -1, ServiceMasterId;
        List<int> deleteid = new List<int>();

        public frmServiceInvoice()
        {
            InitializeComponent();
            maxId();
            // loadProductName();
            txtTotalServiceCharge.Text = "235";
            customerdata();
            CmbCustomer.Focus();
        }

        public frmServiceInvoice(int id)
        {
            InitializeComponent();
            maxId();
            // loadProductName();
            customerdata();
            btnSave.Enabled = false;
            btnUpdate.Enabled = true;
            BindData(id);
        }

        public void BindData(int id)//Bind Data to all controls fetched by specific Id
        {
            try
            {
                ServiceRepository ServiceMRepo = new ServiceRepository();
                var ServiceMData = ServiceMRepo.GetAll().Where(t => t.Id == Convert.ToInt32(id) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtServiceNo.Text = Convert.ToString(ServiceMData.ServiceNo);
                dtpServiceDate.Value = ServiceMData.ServiceDate.Value;
                try
                {
                    CmbCustomer.SelectedValue = ServiceMData.CustomerId;
                }
                catch (Exception)
                { }
                txtPavatiNo.Text = ServiceMData.PavatiNo;
                txtCLMNo.Text = ServiceMData.CLMNo;
                txtVaidhmapan.Text = ServiceMData.Vaidhmapan;
                txtTotalServiceCharge.Text = Convert.ToString(ServiceMData.TotalServiceCharge);
                txtTotalServiceChrageLateFee.Text = Convert.ToString(ServiceMData.ServiceLateFee);
                txtServiceChargeNetAmt.Text = Convert.ToString(ServiceMData.ServiceNetAmt);
                txtTotalVerficationFee.Text = Convert.ToString(ServiceMData.TotalVerficationFee);
                txtVerficationLateFee.Text = Convert.ToString(ServiceMData.VerficationLateFee);
                txtVerficationNetAmt.Text = Convert.ToString(ServiceMData.VerificationNetAmt);
                if (ServiceMData.GSTPercent!=0)
                {
                    cmbGST.Text = Convert.ToString(ServiceMData.GSTPercent);
                }
                
                txtNetAmount.Text = Convert.ToString(ServiceMData.ServiceGSTNetAmount);
                try
                {
                    dtpIssueDate.Value = ServiceMData.IssueDate.Value;
                    dtpRenewalDate.Value = ServiceMData.RenewalDate.Value;
                }
                catch (Exception)
                { }

                ServiceMasterId = ServiceMData.Id;
                var billDetailData = ServiceMData.ServiceInvoiceDetails;//get detail records of specified id of master
                gvAdd.Rows.Clear();
                int i = 0;
                foreach (var item in billDetailData)
                {
                    gvAdd.Rows.Add();
                    gvAdd.Rows[i].Cells["Id"].Value = item.Id;
                    gvAdd.Rows[i].Cells["Make"].Value = item.Make;
                    gvAdd.Rows[i].Cells["Model"].Value = item.Model;
                    gvAdd.Rows[i].Cells["Capacity"].Value = item.Capacity;
                    gvAdd.Rows[i].Cells["Min"].Value = item.Min;
                    gvAdd.Rows[i].Cells["e"].Value = item.e;
                    gvAdd.Rows[i].Cells["Class"].Value = item.Class;
                    gvAdd.Rows[i].Cells["MachineNo"].Value = item.MachineNo;
                    gvAdd.Rows[i].Cells["CertificateNo"].Value = item.CertificateNo;
                    gvAdd.Rows[i].Cells["ServiceCharge"].Value = item.ServiceCharge;
                    gvAdd.Rows[i].Cells["VerificationFees"].Value = item.VerficationFee;
                    i++;
                }
                ServiceMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxId()// get max ServiceId  from Service master
        {
            try
            {
                db.connect();
                ServiceRepository sRepo = new ServiceRepository();
                var maxid = sRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.ServiceNo);
                sRepo.Dispose();
                db.CloseConnection();
                if (maxid == null)
                    txtServiceNo.Text = "1";
                else
                    txtServiceNo.Text = Convert.ToString(Convert.ToInt32(maxid) + 1);
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void customerdata()// bind customer list to dropdownlist
        {
            try
            {
                CustomerRepository custrepo1 = new CustomerRepository();
                List<CustomerInformation> customerdata1 = new List<CustomerInformation>();
                customerdata1.Clear();
                customerdata1 = custrepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata1.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                customerdata1.Reverse();
                CmbCustomer.DataSource = customerdata1;
                CmbCustomer.ValueMember = "Id";
                CmbCustomer.DisplayMember = "CustomerName";
                custrepo1.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loadProductName()//autocomplete produtnames to txtProduct
        {
            try
            {
                TxtProduct.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;

                    cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2));
                        }
                        catch (Exception)
                        { }
                    }
                    TxtProduct.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception) { }
        }

        public void ClearDetail()//clear all controls of detail table
        {
            TxtProduct.Text = "";
            txtCompany.Text = "";
            txtCapacity.Text = "";
            txtMin.Text = "";
            txte.Text = "";
            txtClass.Text = "";
            txtMachineNo.Text = "";
            txtVerficationFee.Text = "0.00";
            txtServiceCHarge.Text = "0.00";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtProduct.Text.Trim() != "" && !string.IsNullOrEmpty(txtServiceCHarge.Text) && !string.IsNullOrEmpty(txtVerficationFee.Text))
                {
                    if (colIndex != -1)//update selected product or detail
                    {
                        gvAdd.Rows[colIndex].Cells["Make"].Value = TxtProduct.Text;
                        gvAdd.Rows[colIndex].Cells["Model"].Value = txtCompany.Text;
                        gvAdd.Rows[colIndex].Cells["Capacity"].Value = txtCapacity.Text;
                        gvAdd.Rows[colIndex].Cells["Min"].Value = txtMin.Text;
                        gvAdd.Rows[colIndex].Cells["e"].Value = txte.Text;
                        gvAdd.Rows[colIndex].Cells["Class"].Value = txtClass.Text;
                        gvAdd.Rows[colIndex].Cells["MachineNo"].Value = txtMachineNo.Text;
                        gvAdd.Rows[colIndex].Cells["CertificateNo"].Value = txtCertificateNo.Text;
                        gvAdd.Rows[colIndex].Cells["ServiceCharge"].Value = txtServiceCHarge.Text;
                        gvAdd.Rows[colIndex].Cells["VerificationFees"].Value = txtVerficationFee.Text;
                        gvAdd.Rows[colIndex].Cells["btnRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    }
                    else// add new detail to grid
                    {
                        int i = gvAdd.RowCount;
                        gvAdd.Rows.Add();
                        gvAdd.Rows[i].Cells["Make"].Value = TxtProduct.Text;
                        gvAdd.Rows[i].Cells["Model"].Value = txtCompany.Text;
                        gvAdd.Rows[i].Cells["Capacity"].Value = txtCapacity.Text;
                        gvAdd.Rows[i].Cells["Min"].Value = txtMin.Text;
                        gvAdd.Rows[i].Cells["e"].Value = txte.Text;
                        gvAdd.Rows[i].Cells["Class"].Value = txtClass.Text;
                        gvAdd.Rows[i].Cells["MachineNo"].Value = txtMachineNo.Text;
                        gvAdd.Rows[i].Cells["CertificateNo"].Value = txtCertificateNo.Text;
                        gvAdd.Rows[i].Cells["ServiceCharge"].Value = txtServiceCHarge.Text;
                        gvAdd.Rows[i].Cells["VerificationFees"].Value = txtVerficationFee.Text;
                        gvAdd.Rows[i].Cells["btnRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    }
                    Calculation();
                    colIndex = -1;
                    ClearDetail();
                    this.ActiveControl = TxtProduct;
                }
                else
                {
                    if (TxtProduct.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = TxtProduct;
                    }
                    else if (!string.IsNullOrEmpty(txtVerficationFee.Text))
                    {
                        MessageBox.Show("Enter Verification Fee.", "Warning");
                        this.ActiveControl = txtVerficationFee;
                    }
                    else if (!string.IsNullOrEmpty(txtServiceCHarge.Text))
                    {
                        MessageBox.Show("Enter Service Charge.", "Warning");
                        this.ActiveControl = txtServiceCHarge;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveServiceMaster();
                SaveServiceDetail();
                MessageBox.Show("Repacking Data saved.", "Success");
                gvAdd.Rows.Clear();
                ClearDetail();
                Clear();
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
            }
            catch (Exception)
            { }
        }

        public void Clear()// clear all control of master table
        {
            maxId();
            dtpServiceDate.Value = DateTime.Now;
            customerdata();
            txtCertificateNo.Text = "";
            txtPavatiNo.Text = "";
            txtTotalServiceCharge.Text = "0.00";
            txtTotalServiceChrageLateFee.Text = "0.00";
            txtServiceChargeNetAmt.Text = "0.00";
            txtTotalVerficationFee.Text = "0.00";
            txtVerficationLateFee.Text = "0.00";
            txtVerficationNetAmt.Text = "0.00";
            txtVaidhmapan.Text = "";
            txtCLMNo.Text = "";
            cmbGST.Text = "";
            txtNetAmount.Text = "0.00";
            btnUpdate.Enabled = false;
        }

        public void SaveServiceDetail()
        {
            try
            {
                for (int i = 0; i < gvAdd.Rows.Count; i++)
                {
                    try
                    {
                        ServiceDetailRepository sDetailRepo = new ServiceDetailRepository();
                        ServiceInvoiceDetail data = new ServiceInvoiceDetail();
                        data.ServiceId = ServiceMasterId;
                        data.Make = gvAdd.Rows[i].Cells["Make"].Value.ToString();
                        data.Model = gvAdd.Rows[i].Cells["Model"].Value.ToString();
                        data.Min = gvAdd.Rows[i].Cells["Min"].Value.ToString();
                        data.e = gvAdd.Rows[i].Cells["e"].Value.ToString();
                        data.Capacity = gvAdd.Rows[i].Cells["Capacity"].Value.ToString();
                        data.MachineNo = gvAdd.Rows[i].Cells["MachineNo"].Value.ToString();
                        data.CertificateNo = gvAdd.Rows[i].Cells["CertificateNo"].Value.ToString();
                        data.ServiceCharge = Convert.ToDecimal(gvAdd.Rows[i].Cells["ServiceCharge"].Value.ToString());
                        data.VerficationFee = Convert.ToDecimal(gvAdd.Rows[i].Cells["VerificationFees"].Value.ToString());
                        data.Class = gvAdd.Rows[i].Cells["Class"].Value.ToString();
                        sDetailRepo.Add(data);
                        sDetailRepo.Save();
                        sDetailRepo.Dispose();
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            { }
        }

        public void SaveServiceMaster()
        {
           
                ServiceRepository sRepo = new ServiceRepository();
                ServiceInvoiceMaster data = new ServiceInvoiceMaster();
                data.ServiceNo = Convert.ToInt32(txtServiceNo.Text);
                data.ServiceDate = dtpServiceDate.Value;
                data.CustomerId = Convert.ToInt32(CmbCustomer.SelectedValue);
                data.CLMNo = txtCLMNo.Text;
                data.PavatiNo = txtPavatiNo.Text;
                data.TotalServiceCharge = Convert.ToDecimal(txtTotalServiceCharge.Text);
                data.ServiceLateFee = Convert.ToDecimal(txtTotalServiceChrageLateFee.Text);
                data.ServiceNetAmt = Convert.ToDecimal(txtServiceChargeNetAmt.Text);
                data.TotalVerficationFee = Convert.ToDecimal(txtTotalVerficationFee.Text);
                data.VerficationLateFee = Convert.ToDecimal(txtVerficationLateFee.Text);
                data.VerificationNetAmt = Convert.ToDecimal(txtVerficationNetAmt.Text);
                data.IssueDate = dtpIssueDate.Value;
                data.RenewalDate = dtpRenewalDate.Value;
                data.CompId = CommonMethod.CompId;
                data.TransactionYear = CommonMethod.TransactionYear;
                data.Vaidhmapan = txtVaidhmapan.Text;
                try
                {
                    data.GSTPercent = Convert.ToInt32(cmbGST.Text);
                }
                catch (Exception)
                {
                    data.GSTPercent = 0;
                }
                data.ServiceGSTNetAmount=Convert.ToDecimal(txtNetAmount.Text);
                sRepo.Add(data);
                sRepo.Save();
                ServiceMasterId = data.Id;
                sRepo.Dispose();
           
        }

        private void txtTotalServiceChrageLateFee_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtVerficationLateFee_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void gvAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvAdd.Columns[e.ColumnIndex].HeaderText == "Remove")
                {
                    if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    {
                        if (Convert.ToInt32(gvAdd.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                        {
                            deleteid.Add(Convert.ToInt32(gvAdd.Rows[e.RowIndex].Cells["Id"].Value));
                            gvAdd.Rows.Remove(gvAdd.Rows[e.RowIndex]);
                        }
                        else
                            gvAdd.Rows.Remove(gvAdd.Rows[e.RowIndex]);
                    }
                }
                else
                {
                    colIndex = e.RowIndex;
                    TxtProduct.Text = gvAdd.Rows[e.RowIndex].Cells["Make"].ToString();
                    txtCompany.Text = gvAdd.Rows[e.RowIndex].Cells["Model"].Value.ToString();
                    txtCapacity.Text = gvAdd.Rows[e.RowIndex].Cells["Capacity"].Value.ToString();
                    txtMin.Text = gvAdd.Rows[e.RowIndex].Cells["Min"].Value.ToString();
                    txte.Text = gvAdd.Rows[e.RowIndex].Cells["e"].Value.ToString();
                    txtClass.Text = gvAdd.Rows[e.RowIndex].Cells["Class"].Value.ToString();
                    txtMachineNo.Text = gvAdd.Rows[e.RowIndex].Cells["MachineNo"].Value.ToString();
                    txtCertificateNo.Text = gvAdd.Rows[e.RowIndex].Cells["CertificateNo"].Value.ToString();
                    txtServiceCHarge.Text = gvAdd.Rows[e.RowIndex].Cells["ServiceCharge"].Value.ToString();
                    txtVerficationFee.Text = gvAdd.Rows[e.RowIndex].Cells["VerificationFees"].Value.ToString();
                }
            }
            catch (Exception)
            { }
        }

        private void TxtProduct_Leave(object sender, EventArgs e)//check for valid product and get company name
        {
            //try
            //{
            //    string proname, company, unit = "";

            //    proname = TxtProduct.Text.Trim().Split(',')[0];
            //    company = TxtProduct.Text.Trim().Split(',')[1];
            //    try
            //    {
            //        unit = TxtProduct.Text.Trim().Split(',')[2];
            //    }
            //    catch (Exception)
            //    { }

            //    ProductRepository productrepo = new ProductRepository();
            //    ProductInformation productdata = new ProductInformation();
            //    try
            //    {
            //        productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
            //    }
            //    catch (Exception)
            //    { productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.CompId == CommonMethod.CompId).FirstOrDefault(); }

            //    if (productdata == null)
            //    {
            //        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
            //        TxtProduct.Text = "";
            //        this.ActiveControl = TxtProduct;
            //    }
            //    txtCompany.Text = TxtProduct.Text.Trim().Split(',')[1];
            //}
            //catch (Exception)
            //{ }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateServiceMaster();
                SaveUpdateServiceDetail();
                MessageBox.Show("Repacking Data Updated.", "Success");
                gvAdd.Rows.Clear();
                ClearDetail();
                Clear();
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
            }
            catch (Exception)
            { }
        }

        private void btnServiceList_Click(object sender, EventArgs e)
        {
            try
            {
                frmServiceInvoiceList sList = new frmServiceInvoiceList();
                sList.WindowState = System.Windows.Forms.FormWindowState.Normal;
                sList.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                sList.ShowDialog();
            }
            catch (Exception)
            { }
        }

        public void UpdateServiceMaster()
        {
            try
            {
                ServiceRepository sRepo = new ServiceRepository();
                ServiceInvoiceMaster data = new ServiceInvoiceMaster();
                data = sRepo.GetById(ServiceMasterId);
                data.ServiceNo = Convert.ToInt32(txtServiceNo.Text);
                data.ServiceDate = dtpServiceDate.Value;
                data.CustomerId = Convert.ToInt32(CmbCustomer.SelectedValue);
                data.CLMNo = txtCLMNo.Text;
                data.PavatiNo = txtPavatiNo.Text;
                data.TotalServiceCharge = Convert.ToDecimal(txtTotalServiceCharge.Text);
                data.ServiceLateFee = Convert.ToDecimal(txtTotalServiceChrageLateFee.Text);
                data.ServiceNetAmt = Convert.ToDecimal(txtServiceChargeNetAmt.Text);
                data.TotalVerficationFee = Convert.ToDecimal(txtTotalVerficationFee.Text);
                data.VerficationLateFee = Convert.ToDecimal(txtVerficationLateFee.Text);
                data.VerificationNetAmt = Convert.ToDecimal(txtVerficationNetAmt.Text);
                data.IssueDate = dtpIssueDate.Value;
                data.RenewalDate = dtpRenewalDate.Value;
                data.CompId = CommonMethod.CompId;
                data.TransactionYear = CommonMethod.TransactionYear;
                data.Vaidhmapan = txtVaidhmapan.Text;
                try
                {
                    data.GSTPercent = Convert.ToInt32(cmbGST.Text);
                }
                catch (Exception)
                {
                    data.GSTPercent = 0;
                }
                
                data.ServiceGSTNetAmount =Convert.ToDecimal(txtNetAmount.Text);
                sRepo.Edit(data);
                sRepo.Save();
                sRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void SaveUpdateServiceDetail()
        {
            try
            {
                for (int i = 0; i < gvAdd.Rows.Count; i++)
                {
                    try
                    {
                        ServiceDetailRepository sDetailRepo = new ServiceDetailRepository();
                        ServiceInvoiceDetail data = new ServiceInvoiceDetail();
                        data.ServiceId = ServiceMasterId;
                        data.Make = gvAdd.Rows[i].Cells["Make"].Value.ToString(); ;
                        data.Model = gvAdd.Rows[i].Cells["Model"].Value.ToString();
                        data.Capacity = gvAdd.Rows[i].Cells["Capacity"].Value.ToString();
                        data.Min = gvAdd.Rows[i].Cells["Min"].Value.ToString();
                        data.e = gvAdd.Rows[i].Cells["e"].Value.ToString();
                        data.Class = gvAdd.Rows[i].Cells["Class"].Value.ToString();
                        data.MachineNo = gvAdd.Rows[i].Cells["MachineNo"].Value.ToString();
                        data.CertificateNo = gvAdd.Rows[i].Cells["CertificateNo"].Value.ToString();
                        data.ServiceCharge = Convert.ToDecimal(gvAdd.Rows[i].Cells["ServiceCharge"].Value.ToString());
                        data.VerficationFee = Convert.ToDecimal(gvAdd.Rows[i].Cells["VerificationFees"].Value.ToString());

                        try
                        {
                            if (string.IsNullOrEmpty(gvAdd.Rows[i].Cells["Id"].Value.ToString()))
                                sDetailRepo.Add(data);
                            else
                            {
                                data.Id = Convert.ToInt32(gvAdd.Rows[i].Cells["Id"].Value);
                                sDetailRepo.Edit(data);
                            }
                        }
                        catch (Exception)
                        {
                            sDetailRepo.Add(data);
                        }

                        sDetailRepo.Save();
                        sDetailRepo.Dispose();
                    }
                    catch (Exception)
                    { }
                }
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                ClearDetail();
                gvAdd.Rows.Clear();
                btnSave.Enabled = true;
                btnUpdate.Enabled = false;
                CmbCustomer.Focus();
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                RptServicePrint SerInvRpt = new RptServicePrint(ServiceMasterId, "Print");
                SerInvRpt.ShowDialog();
                SerInvRpt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void Calculation()
        {
            try
            {
                decimal totalServiceAmt = 0;
                totalServiceAmt = gvAdd.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["ServiceCharge"].Value));
                txtTotalServiceCharge.Text = Convert.ToString(Math.Round(totalServiceAmt, 2, MidpointRounding.AwayFromZero));
                decimal totalVerificationAmt = 0;
                totalVerificationAmt = gvAdd.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["VerificationFees"].Value));
                txtTotalVerficationFee.Text = Convert.ToString(Math.Round(totalVerificationAmt, 2, MidpointRounding.AwayFromZero));
                txtServiceChargeNetAmt.Text = Convert.ToString(Convert.ToDecimal(txtTotalServiceCharge.Text) + Convert.ToDecimal(txtTotalServiceChrageLateFee.Text));
                txtVerficationNetAmt.Text = Convert.ToString(Convert.ToDecimal(txtTotalVerficationFee.Text) + Convert.ToDecimal(txtVerficationLateFee.Text));
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Delete this Invoice?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    DataTable dt = db.GetTable("delete from ServiceInvoiceDetail where ServiceId='" + ServiceMasterId + "' ");
                    DataTable dt1 = db.GetTable("delete from ServiceInvoiceMaster where Id='" + ServiceMasterId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                    MessageBox.Show("Record Deleted Sucessfully.", "Success");
                    dt.Dispose();
                    dt1.Dispose();
                    db.CloseConnection();
                    btnNew.PerformClick();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbGST_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculateGST();
        }
        public void calculateGST()
        {
            try
            {
                decimal finalServiceAmount = Convert.ToDecimal(txtTotalServiceCharge.Text);
                int gst = Convert.ToInt32(cmbGST.Text);
                decimal gstAmount = finalServiceAmount * gst / 100;
                txtNetAmount.Text = Convert.ToString(finalServiceAmount + gstAmount);
            }
            catch (Exception)
            {
            }
        }

        private void txtVerficationLateFee_Leave(object sender, EventArgs e)
        {
            try
            {
                Calculation();
            }
            catch (Exception)
            { }
        }

        private void txtTotalServiceChrageLateFee_Leave(object sender, EventArgs e)
        {
            try
            {
                Calculation();
            }
            catch (Exception)
            { }
        }
    }
}
