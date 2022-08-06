using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSupplierReceipt : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int receiptid;

        public frmSupplierReceipt()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            this.ActiveControl = cmbcustomer;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public frmSupplierReceipt(int suppid)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            loaddata(suppid);
            this.ActiveControl = cmbcustomer;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public frmSupplierReceipt(DateTime from, DateTime to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            loaddata(from, to);
            this.ActiveControl = cmbcustomer;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public void loaddata(DateTime from, DateTime to)
        {
            try
            {
                CommonMethod com = new CommonMethod();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where CreateDate>='" + com.ValidFromDate(from.Date) + "' and CreateDate<='" + com.ValidToDate(to.Date) + "' and Type='Receipt' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                if (GvreceiptInfo.Rows.Count != 0)
                {
                    GvreceiptInfo.Columns["ID"].Visible = false;
                    GvreceiptInfo.Columns["CompId"].Visible = false;
                }
                this.ActiveControl = cmbsuppliername1;
                da.Dispose(); dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loaddata(int suppid)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where SupplierId='" + suppid + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                if (GvreceiptInfo.Rows.Count != 0)
                {
                    GvreceiptInfo.Columns["ID"].Visible = false;
                    GvreceiptInfo.Columns["CompId"].Visible = false;
                }
                else
                    MessageBox.Show("Record not Present.", "Warning");
                try
                {
                    db.connect();
                    //Stored Procedure code
                    SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRem";
                    command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = suppid;
                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    object sumtotAmount = new object();
                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                    db.CloseConnection();
                    label17.Visible = true;
                    label10.Visible = true;
                    label10.Text = Convert.ToString(sumtotAmount);
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
            }
            catch (Exception)
            { }

        }

        public void maxreceiptid()
        {
            try
            {
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                if (mid == null)
                    txtreceiptno.Text = "1";
                else
                    txtreceiptno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void customerdata()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "SupplierCode";
                cmbcustomer.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void supplierdata()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbsuppliername1.DataSource = customerdata;
                cmbsuppliername1.ValueMember = "SupplierCode";
                cmbsuppliername1.DisplayMember = "SupplierName";
                supplierrepo.Dispose();

                SqlDataAdapter da = new SqlDataAdapter("Select Distinct(Type) from SupplierRemainingPayment where CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    cmbtype.Items.Add(row["Type"]);
                }
                cmbtype.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void FillFirmNames()
        {
            try
            {
                CompanyInformationRepository CompRepo = new CompanyInformationRepository();
                List<CompanyInformation> CompData = new List<CompanyInformation>();
                CompData.Clear();
                CompData = CompRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).OrderByDescending(t => t.CompanyName).ToList();
                cmbFirmName.DataSource = CompData;
                cmbFirmName.ValueMember = "CompanyId";
                cmbFirmName.DisplayMember = "CompanyName";
                cmbFirmName.SelectedValue = CommonMethod.CompId;
                CompRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                BankRepository bankrepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank.DataSource = bankdata;
                cmbbank.ValueMember = "BankId";
                cmbbank.DisplayMember = "Bankname";
                bankrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillcombo1()
        {
            try
            {
                BankRepository bankrepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank2.DataSource = bankdata;
                cmbbank2.ValueMember = "BankId";
                cmbbank2.DisplayMember = "Bankname";
                bankrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void hidelabel()
        {
            try
            {
                label10.Visible = false;
                label17.Visible = false;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                chkAutoSort.Checked = false;
                txtTotalAmt.Text = "";
                txtPaidAmt.Text = "";
                txtRemainigAmt.Text = "";
                customerdata();
                txtchequeno.Text = "";
                maxreceiptid();
                try
                {
                    dtpchequedate.Value = DateTime.Now.Date;
                    dtpreceiptDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                cmbpaymentmode.SelectedText = "";
                cmbbank.Enabled = false;
                txtchequeno.Enabled = false;
                dtpchequedate.Enabled = false;
                FillFirmNames();
                fillcombo();
                fillcombo1();
                btnSave.Enabled = true;
                label15.Text = "Cheque No:";
                chkAdjustVoucher.Checked = false;
                txtAdjustAmount.Visible = false;
                if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                {
                    btnSave.Enabled = false;
                    btnnew.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.Text != "Select" && cmbpono.Text != "" && cmbpaymentmode.Text != "" && txtPaidAmt.Text != "")
                {
                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                    SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                    remainingdata.BillReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                    remainingdata.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                    remainingdata.Suppliername = cmbcustomer.Text;
                    remainingdata.CreateDate = dtpreceiptDate.Value;
                    remainingdata.BillId = Convert.ToInt32(cmbpono.Text);
                    remainingdata.Type = "Receipt";
                    remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainigAmt.Text);
                    remainingdata.NetAmt = Convert.ToDecimal(0);
                    remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                    remainingdata.PaymentMode = cmbpaymentmode.Text;
                    remainingdata.TransactionYear = CommonMethod.TransactionYear;
                    remainingdata.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                    remainingdata.ReceiptBy = CommonMethod.CompId;
                    if (cmbpaymentmode.Text == "Cash")
                    {
                        remainingdata.Bankname = "";
                        remainingdata.Chequeno = "";
                        remainingdata.Chequedate = null;
                        remainingpayrepo.Add(remainingdata);
                        remainingpayrepo.Save();
                        if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                        {
                            try
                            {
                                SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                remainingdataadj.Suppliername = cmbcustomer.Text;
                                remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                remainingdataadj.Type = "Adjustment";
                                remainingdataadj.BillId = Convert.ToInt32(cmbpono.Text);
                                remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                remainingdataadj.CompId = CommonMethod.CompId;
                                remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                remainingdataadj.Bankname = "";
                                remainingdataadj.Chequeno = "";
                                remainingdataadj.Chequedate = null;
                                remainingpayrepoadj.Add(remainingdataadj);
                                remainingpayrepoadj.Save();
                            }
                            catch (Exception)
                            { }
                        }
                        MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                        clear();
                        FillFirmNames();
                        fillcombo();
                        fillcombo1();
                        this.ActiveControl = btnnew;
                    }
                    else if (cmbpaymentmode.Text == "Cheque")
                    {
                        if (cmbbank.Text != "Select" && txtchequeno.Text != "")
                        {
                            remainingdata.Bankname = cmbbank.Text;
                            remainingdata.Chequeno = txtchequeno.Text;
                            remainingdata.Chequedate = dtpchequedate.Value;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            try
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();
                                bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                bankstdata.TransactionDate = dtpreceiptDate.Value;
                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Add(bankstdata);
                                bankrepo.Save();
                                bankrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                    remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                    remainingdataadj.Suppliername = cmbcustomer.Text;
                                    remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                    remainingdataadj.Type = "Adjustment";
                                    remainingdataadj.BillId = Convert.ToInt32(cmbpono.Text);
                                    remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                    remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                    remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                    remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                    remainingdataadj.CompId = CommonMethod.CompId;
                                    remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                    remainingdataadj.Bankname = cmbbank.Text;
                                    remainingdataadj.Chequeno = txtchequeno.Text;
                                    remainingdataadj.Chequedate = dtpchequedate.Value;
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                            FillFirmNames();
                            fillcombo();
                            fillcombo1();
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else
                        {
                            if (cmbbank.Text == "Select")
                            {
                                MessageBox.Show("Select Bank Name.", "Warning");
                                this.ActiveControl = cmbbank;
                            }
                            else
                            {
                                MessageBox.Show("Enter Cheque Number.", "Warning");
                                this.ActiveControl = txtchequeno;
                            }
                        }
                    }
                    else
                    {
                        if (cmbbank.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                        {
                            remainingdata.Bankname = cmbbank.Text;
                            remainingdata.Chequeno = txtchequeno.Text;
                            remainingdata.Chequedate = dtpchequedate.Value;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            try
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();
                                bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                bankstdata.TransactionDate = dtpreceiptDate.Value;
                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Add(bankstdata);
                                bankrepo.Save();
                                bankrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                            MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                            FillFirmNames();
                            fillcombo();
                            fillcombo1();
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else
                        {
                            if (cmbbank.Text.TrimStart() == "Select")
                            {
                                MessageBox.Show("Select Bank Name.", "Warning");
                                this.ActiveControl = cmbbank;
                            }
                            else
                            {
                                MessageBox.Show("Enter UTR Number.", "Warning");
                                this.ActiveControl = txtchequeno;
                            }
                        }
                    }
                    remainingpayrepo.Dispose();
                }
                else
                {
                    if (cmbcustomer.Text == "Select")
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomer;
                    }
                    else if (cmbpono.Text == "")
                    {
                        MessageBox.Show("Select Purchase Order Number.", "Warning");
                        this.ActiveControl = cmbpono;
                    }
                    else if (txtPaidAmt.Text == "")
                    {
                        MessageBox.Show("Enter Paid Amount.", "Warning");
                        this.ActiveControl = txtPaidAmt;
                    }
                    else
                    {
                        MessageBox.Show("Select Payment Mode.", "Warning");
                        this.ActiveControl = cmbpaymentmode;
                    }
                }
            }
            catch (Exception)
            { }

        }

        public void getcustomerdata()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'POrder No.',CONVERT(VARCHAR(24),BillDate,103) as 'POrder Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                GvreceiptInfo.Columns["ID"].Visible = false;
                GvreceiptInfo.Columns["CompId"].Visible = false;
                supplierdata();
                chksuppliername1.Checked = false;
                FillFirmNames();
                fillcombo();
                fillcombo1();
                chkbankname.Checked = false;
                clear();
                hidelabel();
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpaymentmode.Text == "Cheque")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label15.Text = "Cheque No:";
                    label13.Text = "Cheque Date:";
                }
                else if (cmbpaymentmode.Text == "NEFT" || cmbpaymentmode.Text == "RTGS")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label15.Text = "UTR No:";
                    if (cmbpaymentmode.Text == "NEFT")
                        label13.Text = "NEFT Date:";
                    else
                        label13.Text = "RTGS Date:";
                }
                else
                {
                    cmbbank.Enabled = false;
                    txtchequeno.Enabled = false;
                    dtpchequedate.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.SelectedIndex > 0)
                {
                    try
                    {
                        db.connect();
                        //Stored Procedure code
                        SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                        command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRem";
                        command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(cmbcustomer.SelectedValue);
                        DataTable dt1 = new DataTable();
                        dt1.Load(command.ExecuteReader());
                        object sumtotAmount = new object();
                        sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                        db.CloseConnection();
                        lbltotalremamt.Text = Convert.ToString(sumtotAmount);
                        dt1.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
                else
                    txtTotalAmt.Text = "0";
            }
            catch (Exception)
            { }

            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmbpono.DataSource = dt;
                if (dt.Rows.Count != 0)
                {
                    cmbpono.ValueMember = "BillNo";
                    cmbpono.DisplayMember = "BillNo";
                }
                else
                {
                    cmbpono.Text = "";
                    txtTotalAmt.Text = "0";
                }
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    decimal a = 0;
                    if (chkAutoSort.Checked)
                        a = Convert.ToDecimal(lbltotalremamt.Text);
                    else
                        a = Convert.ToDecimal(txtTotalAmt.Text);
                    decimal b = Convert.ToDecimal(txtPaidAmt.Text);
                    if (a >= b)
                    {
                        decimal c = (a - b);
                        txtRemainigAmt.Text = Convert.ToString(c);
                    }
                    else
                    {
                        txtPaidAmt.Text = "";
                        txtRemainigAmt.Text = "";
                        MessageBox.Show("Paid Amount Less or equal to Total Amount.", "Warning");
                        this.ActiveControl = txtPaidAmt;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            //try
            //{
            //        int customerId = Convert.ToInt32(cmbcustomer.SelectedValue);
            //    RptSupplierReceipt receiptrpt = new RptSupplierReceipt(customerId);
            //    receiptrpt.ShowDialog();
            //}
            //catch (Exception)
            //{
            //}
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

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                getcustomerdata();
                cmbsuppliername1.Text = "Select";
                chksuppliername1.Checked = false;
                clear();
                hidelabel();
                cmbcustomer.Text = "Select";
                txtTotalAmt.Text = "";
                chkbankname.Checked = false;
                cmbbank2.Text = "Select";
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                hidelabel();
                if (chksuppliername1.Checked == true)
                {
                    if (cmbsuppliername1.Text != "Select")
                    {
                        if (chktype.Checked == true)
                        {
                            if (cmbtype.Text != "Select")
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where SupplierId='" + cmbsuppliername1.SelectedValue + "'  and Type='" + cmbtype.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                GvreceiptInfo.DataSource = dt;
                                GvreceiptInfo.Refresh();
                                if (GvreceiptInfo.Rows.Count != 0)
                                {
                                    GvreceiptInfo.Columns["ID"].Visible = false;
                                    GvreceiptInfo.Columns["CompId"].Visible = false;
                                }
                                else
                                    MessageBox.Show("Record not Present.", "Warning");
                                try
                                {
                                    db.connect();
                                    //Stored Procedure code
                                    SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "TypeSuppRem";
                                    command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(cmbsuppliername1.SelectedValue);
                                    command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = cmbtype.Text.ToString();

                                    DataTable dt1 = new DataTable();
                                    dt1.Load(command.ExecuteReader());
                                    object sumtotAmount = new object();
                                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                                    db.CloseConnection();
                                    label17.Visible = true;
                                    label10.Visible = true;
                                    label10.Text = Convert.ToString(sumtotAmount);
                                    dt1.Dispose();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                this.ActiveControl = cmbsuppliername1;
                                da.Dispose(); dt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Please Select Type.", "Warning");
                                this.ActiveControl = cmbtype;
                            }
                        }
                        else if (chkbetdate.Checked)
                        {
                            if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                            {
                                CommonMethod com = new CommonMethod();
                                string toDate = com.ValidToDate(dtptodate.Value.Date);
                                string fromDate = com.ValidFromDate(dtpfromdate.Value.Date);
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where SupplierId='" + cmbsuppliername1.SelectedValue + "'  and CreateDate>='" + fromDate + "' and CreateDate<='" + toDate + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                GvreceiptInfo.DataSource = dt;
                                GvreceiptInfo.Refresh();
                                if (GvreceiptInfo.Rows.Count != 0)
                                {
                                    GvreceiptInfo.Columns["ID"].Visible = false;
                                    GvreceiptInfo.Columns["CompId"].Visible = false;
                                }
                                else
                                    MessageBox.Show("Record not Present.", "Warning");
                                this.ActiveControl = cmbsuppliername1;
                                da.Dispose(); dt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("To Date Should greater than from date.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }
                        }
                        else
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where SupplierId='" + cmbsuppliername1.SelectedValue + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvreceiptInfo.DataSource = dt;
                            GvreceiptInfo.Refresh();
                            if (GvreceiptInfo.Rows.Count != 0)
                            {
                                GvreceiptInfo.Columns["ID"].Visible = false;
                                GvreceiptInfo.Columns["CompId"].Visible = false;
                            }
                            else
                                MessageBox.Show("Record not Present.", "Warning");
                            try
                            {
                                db.connect();
                                //Stored Procedure code
                                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRem";
                                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(cmbsuppliername1.SelectedValue);
                                DataTable dt1 = new DataTable();
                                dt1.Load(command.ExecuteReader());
                                object sumtotAmount = new object();
                                sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                                db.CloseConnection();
                                label17.Visible = true;
                                label10.Visible = true;
                                label10.Text = Convert.ToString(sumtotAmount);
                                dt1.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            this.ActiveControl = cmbsuppliername1;
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsuppliername1;
                    }
                }
                else if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where Bankname='" + cmbbank2.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["CompId"].Visible = false;
                        }
                        else
                            MessageBox.Show("Record not Present.", "Warning");
                        this.ActiveControl = cmbbank2;
                        da.Dispose(); dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Bank Name.", "Warning");
                        this.ActiveControl = cmbbank2;
                    }
                }
                else if (chkdate.Checked == true)
                {
                    if (chktype.Checked == true)
                    {
                        if (cmbtype.Text != "Select")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where CONVERT(VARCHAR(24),CreateDate,103)='" + dtpdate.Value.ToString("dd/MM/yyyy") + "'  and Type='" + cmbtype.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvreceiptInfo.DataSource = dt;
                            GvreceiptInfo.Refresh();
                            if (GvreceiptInfo.Rows.Count != 0)
                            {
                                GvreceiptInfo.Columns["ID"].Visible = false;
                                GvreceiptInfo.Columns["CompId"].Visible = false;
                            }
                            else
                                MessageBox.Show("Record not Present.", "Warning");
                            try
                            {
                                db.connect();
                                //Stored Procedure code
                                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "TypeDate";
                                command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = cmbtype.Text.ToString();
                                CommonMethod com = new CommonMethod();
                                command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = com.ValidFromDate(dtpdate.Value.Date);
                                command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = com.ValidToDate(dtpdate.Value.Date);
                                DataTable dt1 = new DataTable();
                                dt1.Load(command.ExecuteReader());
                                object sumtotAmount = new object();
                                sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                                db.CloseConnection();
                                label17.Visible = true;
                                label10.Visible = true;
                                label10.Text = Convert.ToString(sumtotAmount);
                                dt1.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            this.ActiveControl = cmbtype;
                            da.Dispose(); dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Please Select Type.", "Warning");
                            this.ActiveControl = cmbtype;
                        }
                    }
                    else
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where CONVERT(VARCHAR(24),CreateDate,103)='" + dtpdate.Value.ToString("dd/MM/yyyy") + "'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["CompId"].Visible = false;
                        }
                        else
                            MessageBox.Show("Record not Present.", "Warning");
                        try
                        {
                            db.connect();
                            //Stored Procedure code
                            SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "Date";
                            CommonMethod com = new CommonMethod();
                            command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = com.ValidFromDate(dtpdate.Value.Date);
                            command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = com.ValidToDate(dtpdate.Value.Date);
                            DataTable dt1 = new DataTable();
                            dt1.Load(command.ExecuteReader());
                            object sumtotAmount = new object();
                            sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                            db.CloseConnection();
                            label17.Visible = true;
                            label10.Visible = true;
                            label10.Text = Convert.ToString(sumtotAmount);
                            dt1.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        this.ActiveControl = dtpdate;
                        da.Dispose(); dt.Dispose();
                    }
                }
                else if (chktype.Checked == true)
                {
                    if (cmbtype.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where  Type='" + cmbtype.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["CompId"].Visible = false;
                        }
                        else
                            MessageBox.Show("Record not Present.", "Warning");
                        try
                        {
                            db.connect();
                            //Stored Procedure code
                            SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "Type";
                            command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = cmbtype.Text.ToString();
                            DataTable dt1 = new DataTable();
                            dt1.Load(command.ExecuteReader());
                            object sumtotAmount = new object();
                            sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                            db.CloseConnection();
                            label17.Visible = true;
                            label10.Visible = true;
                            label10.Text = Convert.ToString(sumtotAmount);
                            dt1.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        this.ActiveControl = cmbtype;
                        da.Dispose(); dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Type.", "Warning");
                        this.ActiveControl = cmbtype;
                    }
                }
                else if (chkbetdate.Checked)
                {
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        string toDate = com.ValidToDate(dtptodate.Value.Date);
                        string fromDate = com.ValidFromDate(dtpfromdate.Value.Date);
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'Receipt Date',[Type],[Suppliername] as 'Supplier Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[PaymentMode] as 'Mode',[Bankname] as 'Bank Name',[Chequeno] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[Chequedate],103) as 'Chq./NEFT Date',CompId  FROM [SupplierRemainingPayment] where CreateDate>='" + fromDate + "' and CreateDate<='" + toDate + "' and TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["CompId"].Visible = false;
                        }
                        else
                            MessageBox.Show("Record not Present.", "Warning");
                        this.ActiveControl = cmbsuppliername1;
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpfromdate;
                    }
                }
                else
                {
                    MessageBox.Show("Please check checkbox first.", "Warning");
                    this.ActiveControl = chkbankname;
                }
            }
            catch (Exception)
            { }

        }

        private void btnprint1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chksuppliername1.Checked == true && cmbsuppliername1.Text != "Select")
                {
                    if (chkbetdate.Checked)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                        {
                            RptSupplierReceipt receiptrpt = new RptSupplierReceipt(Convert.ToInt32(cmbsuppliername1.SelectedValue), cmbsuppliername1.Text, dtpfromdate.Value.Date, dtptodate.Value.Date);
                            receiptrpt.ShowDialog();
                            receiptrpt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtpfromdate;
                        }
                    }
                    else
                    {
                        RptSupplierReceipt receiptrpt = new RptSupplierReceipt(Convert.ToInt32(cmbsuppliername1.SelectedValue), cmbsuppliername1.Text);
                        receiptrpt.ShowDialog();
                        receiptrpt.Dispose();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void chksuppliername1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chksuppliername1.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    fillcombo();
                    fillcombo1();
                }
            }
            catch (Exception)
            { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value) == "Purchase")
                {
                    if (Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[14].Value) == CommonMethod.CompId)
                    {
                        frmPurchaseOrder purord = new frmPurchaseOrder(Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[1].Value));
                        purord.ShowDialog();
                        btnsearch_Click(null, null);
                    }
                    else
                        MessageBox.Show("This Purchase Created By Another" + Environment.NewLine + "Person So U Can not Update it.", "Warning");
                }
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value) == "OpeningBalance")
                    MessageBox.Show("Please Update this Data in Supplier Information Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value) == "Receipt")
                {
                    SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                    var remdata = remRepo.GetAll().Where(t => t.Suppliername == Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[6].Value) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).LastOrDefault();
                    if (remdata.Type == "Receipt" && remdata.Id == Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[0].Value))
                    {
                        btndelete.Enabled = true;
                        btnSave.Enabled = false;
                        receiptid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[0].Value);
                        txtreceiptno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[3].Value);
                        dtpreceiptDate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells[4].Value);
                        cmbcustomer.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[6].Value);
                        cmbpono.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[1].Value);
                        txtTotalAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[7].Value);
                        txtPaidAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[8].Value);
                        txtRemainigAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[9].Value);
                        cmbpaymentmode.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[10].Value);
                        if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[10].Value) == "Cheque")
                        {
                            cmbbank.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[11].Value);
                            txtchequeno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[12].Value);
                            dtpchequedate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells[13].Value);
                        }
                        else
                        {
                            txtchequeno.Text = "";
                            cmbbank.Text = "Select";
                            dtpchequedate.Enabled = false;
                        }
                    }
                    else
                        MessageBox.Show("Invalid Choice.", "Warning");
                }
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_Leave_1(object sender, EventArgs e)
        {
            try
            {
                decimal a = 0;
                if (chkAutoSort.Checked)
                    a = Convert.ToDecimal(lbltotalremamt.Text);
                else
                    a = Convert.ToDecimal(txtTotalAmt.Text);
                decimal b = Convert.ToDecimal(txtPaidAmt.Text);
                if (a >= b)
                {
                    decimal c = (a - b);
                    txtRemainigAmt.Text = Convert.ToString(c);
                }
                else
                {
                    txtPaidAmt.Text = "";
                    txtRemainigAmt.Text = "";
                    MessageBox.Show("Paid Amount Less or equal to Total Amount.", "Warning");
                    this.ActiveControl = txtPaidAmt;
                }
            }
            catch (Exception)
            { }
        }

        private void chkbankname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbankname.Checked == true)
                {
                    chksuppliername1.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    chkbetdate.Checked = false;
                    supplierdata();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsuppliername1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbbank2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    chksuppliername1.Checked = false;
                    chkbankname.Checked = false;
                    chkbetdate.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chktype_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chktype.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkbetdate.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbpono_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTotalAmt.Text = "";
            txtPaidAmt.Text = "";
            txtRemainigAmt.Text = "";
            cmbpaymentmode.SelectedText = "";
            try
            {
                if (cmbpono.Text != "0")
                {
                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                    List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(cmbpono.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).ToList();
                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(cmbpono.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue) && (t.Type != "Receipt")).Sum(t => t.NetAmt));
                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(cmbpono.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).Sum(t => t.PaidAmt));
                    if (remainingamt != null)
                        txtTotalAmt.Text = Convert.ToString(netamt - Paidamt);
                    remainingpayrepo.Dispose();
                }
                else
                {
                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                    List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && (t.Type != "Receipt")).Sum(t => t.NetAmt));
                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                    if (remainingamt != null)
                        txtTotalAmt.Text = Convert.ToString(netamt - Paidamt);
                    remainingpayrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkbetdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbetdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    fillcombo();
                    fillcombo1();
                }
            }
            catch (Exception)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remdata = remRepo.GetById(receiptid);
                remRepo.Remove(remdata);
                remRepo.Save();
                if (remdata.PaymentMode != "Cash")
                {
                    BankStatementRepository Brepo = new BankStatementRepository();
                    var bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.TransactionDate == remdata.CreateDate && t.ChequeNo == remdata.Chequeno && t.Withdrawals == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (bdata != null)
                    {
                        BankStatement bsdata = Brepo.GetById(bdata.Id);
                        Brepo.Remove(bsdata);
                        Brepo.Save();
                    }
                }
                MessageBox.Show("Record Deleted SuccessFully.", "Success");
                clear();

                btnSave.Enabled = true;
                btndelete.Enabled = false;
                btnsearch_Click(null, null);
                this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void frmSupplierReceipt_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void chkAdjustVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAdjustVoucher.Checked && chkAutoSort.Checked == false)
                {
                    txtAdjustAmount.Visible = true;
                    txtAdjustAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text));
                }
                else
                {
                    txtAdjustAmount.Visible = false;
                    txtAdjustAmount.Text = Convert.ToString(0);
                }
            }
            catch (Exception)
            { }
        }

        private void txtAdjustAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    decimal a = Convert.ToDecimal(txtTotalAmt.Text);
                    decimal b = Convert.ToDecimal(txtPaidAmt.Text) + Convert.ToDecimal(txtAdjustAmount.Text);
                    if (a >= b)
                    {
                        decimal c = (a - b);
                        txtRemainigAmt.Text = Convert.ToString(c);
                    }
                    else
                    {
                        txtAdjustAmount.Text = "0";
                        MessageBox.Show("AdjustMent Amount Less or equal to Total Amount.", "Warning");
                        this.ActiveControl = txtAdjustAmount;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbFirmName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                customerdata();
            }
            catch (Exception)
            { }
        }

        private void cmbcustomer_Leave(object sender, EventArgs e)
        {
            if (cmbcustomer.Text != "" && cmbcustomer.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbcustomer.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomer;
                }
            }
        }

        private void cmbsuppliername1_Leave(object sender, EventArgs e)
        {
            if (cmbsuppliername1.Text != "" && cmbsuppliername1.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbsuppliername1.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    supplierdata();
                    this.ActiveControl = cmbsuppliername1;
                }
            }
        }
    }
}