using System;
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
    public partial class frmDailySaleDetail : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmDailySaleDetail()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            try
            {
                dtpfromdate.Value = DateTime.Now.Date;
                fillcombo();
                getall();
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == null)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + " order by ManufactureCompany");
                cmbcomanyname.DataSource = dt;
                cmbcomanyname.ValueMember = "ManufactureCompany";
                cmbcomanyname.DisplayMember = "ManufactureCompany";
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDate.Checked)
                {
                    if (chkBillNo.Checked)
                    {
                        if (txtBillNo.Text != "")
                        {
                            try
                            {
                                DataTable dt = new DataTable();
                                lblsaleamount.Text = "0";
                                label3.Text = "0";
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                    command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = txtBillNo.Text;
                                    command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "FDB";
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    SqlDataReader reader = command.ExecuteReader();
                                    dt.Load(reader);
                                    reader.Dispose();
                                    command.Dispose();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                try
                                {
                                    GvCredit.DataSource = null;
                                    GvCredit.DataSource = dt;
                                    GvCredit.Refresh();
                                    if (GvCredit.Rows.Count != 0)
                                    {
                                        GvCredit.Columns["Id"].Visible = false;
                                        GvCredit.Columns["BillNo"].Width = 50;
                                        GvCredit.Columns["ProductName"].Width = 150;
                                        GvCredit.Columns["CompanyName"].Width = 120;
                                        GvCredit.Columns["Unit"].Width = 60;
                                        GvCredit.Columns["SaleUnit"].Width = 60;
                                        GvCredit.Columns["Quantity"].Width = 60;
                                        GvCredit.Columns["Rate"].Width = 70;
                                        GvCredit.Columns["Amount"].Width = 70;
                                        GvCredit.Columns["GSTAmt"].Width = 70;
                                        GvCredit.Columns["SAmt"].Width = 70;
                                        GvCredit.Columns["SRAmt"].Width = 70;
                                        GvCredit.Columns["Type"].Width = 50;
                                        GvCredit.Columns["PRate"].Width = 70;

                                        if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                                            lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                                        else
                                            lblsaleamount.Text = "0";
                                    }
                                    else
                                        this.ActiveControl = dtpfromdate;
                                    if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                                    {
                                        label1.Visible = true;
                                        label3.Visible = true;
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;
                                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@Profit", SqlDbType.Decimal);
                                            command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                                            command.Parameters["@Profit"].Precision = 18;
                                            command.Parameters["@Profit"].Scale = 2;
                                            command.ExecuteReader();
                                            decimal profit = Convert.ToDecimal(command.Parameters["@Profit"].Value.ToString());
                                            if (profit < 0)
                                                label3.Text = "0";
                                            else
                                                label3.Text = Convert.ToString(profit);
                                            command.Dispose();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            catch (Exception)
                            { }
                        }
                        else
                        {
                            MessageBox.Show("Please Enter BillNo.", "Warning");
                            this.ActiveControl = txtBillNo;
                        }
                    }
                    else if (chkproductname.Checked)
                    {
                        if (txtpname.Text != "")
                        {
                            string Proname = "", company = "", unit = "";
                            if ((txtpname.Text).Contains(","))
                            {
                                try
                                {
                                    Proname = txtpname.Text.Trim().Split(',')[0];
                                    company = txtpname.Text.Trim().Split(',')[1];
                                    unit = txtpname.Text.Trim().Split(',')[2];
                                }
                                catch (Exception)
                                { }

                                try
                                {
                                    DataTable dt = new DataTable();
                                    lblsaleamount.Text = "0";
                                    label3.Text = "0";
                                    try
                                    {
                                        db.connect();
                                        string sdate = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                        string fdate = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                        SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Proname;
                                        command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                        command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");// dtpfromdate.Value.Date.ToShortDateString();
                                        command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = '0';
                                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "FDP";
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        SqlDataReader reader = command.ExecuteReader();
                                        dt.Load(reader);
                                        reader.Dispose();
                                        command.Dispose();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                    try
                                    {
                                        GvCredit.DataSource = null;
                                        GvCredit.DataSource = dt;
                                        GvCredit.Refresh();
                                        if (GvCredit.Rows.Count != 0)
                                        {
                                            GvCredit.Columns["Id"].Visible = false;
                                            GvCredit.Columns["BillNo"].Width = 50;
                                            GvCredit.Columns["ProductName"].Width = 150;
                                            GvCredit.Columns["CompanyName"].Width = 120;
                                            GvCredit.Columns["Unit"].Width = 60;
                                            GvCredit.Columns["SaleUnit"].Width = 60;
                                            GvCredit.Columns["Quantity"].Width = 60;
                                            GvCredit.Columns["Rate"].Width = 70;
                                            GvCredit.Columns["Amount"].Width = 70;
                                            GvCredit.Columns["GSTAmt"].Width = 70;
                                            GvCredit.Columns["SAmt"].Width = 70;
                                            GvCredit.Columns["SRAmt"].Width = 70;
                                            GvCredit.Columns["Type"].Width = 50;
                                            GvCredit.Columns["PRate"].Width = 70;

                                            if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                                                lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                                            else
                                                lblsaleamount.Text = "0";
                                        }
                                        else
                                            this.ActiveControl = dtpfromdate;
                                        if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                                        {
                                            label1.Visible = true;
                                            label3.Visible = true;
                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 'D';
                                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                                command.Parameters.Add("@Profit", SqlDbType.Decimal);
                                                command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                                                command.Parameters["@Profit"].Precision = 18;
                                                command.Parameters["@Profit"].Scale = 2;
                                                command.ExecuteReader();
                                                decimal profit = Convert.ToDecimal(command.Parameters["@Profit"].Value.ToString());
                                                if (profit < 0)
                                                    label3.Text = "0";
                                                else
                                                    label3.Text = Convert.ToString(profit);
                                                command.Dispose();
                                                db.CloseConnection();
                                            }
                                            catch (Exception)
                                            { db.CloseConnection(); }
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter Product Name.", "Warning");
                            this.ActiveControl = txtpname;
                        }
                    }
                    else if (chkCompanyName.Checked)
                    {
                        if (cmbcomanyname.Text != "")
                        {
                            try
                            {
                                DataTable dt = new DataTable();
                                lblsaleamount.Text = "0";
                                label3.Text = "0";
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                    command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "FDC";
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    SqlDataReader reader = command.ExecuteReader();
                                    dt.Load(reader);
                                    reader.Dispose();
                                    command.Dispose();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                try
                                {
                                    GvCredit.DataSource = null;
                                    GvCredit.DataSource = dt;
                                    GvCredit.Refresh();
                                    if (GvCredit.Rows.Count != 0)
                                    {
                                        GvCredit.Columns["Id"].Visible = false;
                                        GvCredit.Columns["BillNo"].Width = 50;
                                        GvCredit.Columns["ProductName"].Width = 150;
                                        GvCredit.Columns["CompanyName"].Width = 120;
                                        GvCredit.Columns["Unit"].Width = 60;
                                        GvCredit.Columns["SaleUnit"].Width = 60;
                                        GvCredit.Columns["Quantity"].Width = 60;
                                        GvCredit.Columns["Rate"].Width = 70;
                                        GvCredit.Columns["Amount"].Width = 70;
                                        GvCredit.Columns["GSTAmt"].Width = 70;
                                        GvCredit.Columns["SAmt"].Width = 70;
                                        GvCredit.Columns["SRAmt"].Width = 70;
                                        GvCredit.Columns["Type"].Width = 50;
                                        GvCredit.Columns["PRate"].Width = 70;

                                        if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                                            lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                                        else
                                            lblsaleamount.Text = "0";
                                    }
                                    else
                                        this.ActiveControl = dtpfromdate;
                                    if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                                    {
                                        label1.Visible = true;
                                        label3.Visible = true;
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;
                                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 'D';
                                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@Profit", SqlDbType.Decimal);
                                            command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                                            command.Parameters["@Profit"].Precision = 18;
                                            command.Parameters["@Profit"].Scale = 2;
                                            command.ExecuteReader();
                                            decimal profit = Convert.ToDecimal(command.Parameters["@Profit"].Value.ToString());
                                            if (profit < 0)
                                                label3.Text = "0";
                                            else
                                                label3.Text = Convert.ToString(profit);
                                            command.Dispose();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            catch (Exception)
                            { }
                        }
                        else
                        {
                            MessageBox.Show("Please Select Company Name.", "Warning");
                            this.ActiveControl = cmbcomanyname;
                        }
                    }
                    else
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            lblsaleamount.Text = "0";
                            label3.Text = "0";
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "FD";
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                SqlDataReader reader = command.ExecuteReader();
                                dt.Load(reader);
                                reader.Dispose();
                                command.Dispose();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            try
                            {
                                GvCredit.DataSource = null;
                                GvCredit.DataSource = dt;
                                GvCredit.Refresh();
                                if (GvCredit.Rows.Count != 0)
                                {
                                    GvCredit.Columns["Id"].Visible = false;
                                    GvCredit.Columns["BillNo"].Width = 50;
                                    GvCredit.Columns["ProductName"].Width = 150;
                                    GvCredit.Columns["CompanyName"].Width = 120;
                                    GvCredit.Columns["Unit"].Width = 60;
                                    GvCredit.Columns["SaleUnit"].Width = 60;
                                    GvCredit.Columns["Quantity"].Width = 60;
                                    GvCredit.Columns["Rate"].Width = 70;
                                    GvCredit.Columns["Amount"].Width = 70;
                                    GvCredit.Columns["GSTAmt"].Width = 70;
                                    GvCredit.Columns["SAmt"].Width = 70;
                                    GvCredit.Columns["SRAmt"].Width = 70;
                                    GvCredit.Columns["Type"].Width = 50;
                                    GvCredit.Columns["PRate"].Width = 70;

                                    if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                                        lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                                    else
                                        lblsaleamount.Text = "0";
                                }
                                else
                                    this.ActiveControl = dtpfromdate;
                                if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                                {
                                    label1.Visible = true;
                                    label3.Visible = true;
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 'D';
                                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                        command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@Profit", SqlDbType.Decimal);
                                        command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                                        command.Parameters["@Profit"].Precision = 18;
                                        command.Parameters["@Profit"].Scale = 2;
                                        command.ExecuteReader();
                                        decimal profit = Convert.ToDecimal(command.Parameters["@Profit"].Value.ToString());
                                        if (profit < 0)
                                            label3.Text = "0";
                                        else
                                            label3.Text = Convert.ToString(profit);
                                        command.Dispose();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                            }
                            catch (Exception)
                            { }
                        }
                        catch (Exception)
                        { }
                    }
                }
                else if (chkBillNo.Checked)
                {
                    if (txtBillNo.Text != "")
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            lblsaleamount.Text = "0";
                            label3.Text = "0";
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = txtBillNo.Text;
                                command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 'B';
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                SqlDataReader reader = command.ExecuteReader();
                                dt.Load(reader);
                                reader.Dispose();
                                command.Dispose();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            try
                            {
                                GvCredit.DataSource = null;
                                GvCredit.DataSource = dt;
                                GvCredit.Refresh();
                                if (GvCredit.Rows.Count != 0)
                                {
                                    GvCredit.Columns["Id"].Visible = false;
                                    GvCredit.Columns["BillNo"].Width = 50;
                                    GvCredit.Columns["ProductName"].Width = 150;
                                    GvCredit.Columns["CompanyName"].Width = 120;
                                    GvCredit.Columns["Unit"].Width = 60;
                                    GvCredit.Columns["SaleUnit"].Width = 60;
                                    GvCredit.Columns["Quantity"].Width = 60;
                                    GvCredit.Columns["Rate"].Width = 70;
                                    GvCredit.Columns["Amount"].Width = 70;
                                    GvCredit.Columns["GSTAmt"].Width = 70;
                                    GvCredit.Columns["SAmt"].Width = 70;
                                    GvCredit.Columns["SRAmt"].Width = 70;
                                    GvCredit.Columns["Type"].Width = 50;
                                    GvCredit.Columns["PRate"].Width = 70;

                                    if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                                        lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                                    else
                                        lblsaleamount.Text = "0";
                                }
                                else
                                    this.ActiveControl = dtpfromdate;
                                if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                                {
                                    label1.Visible = true;
                                    label3.Visible = true;
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = '0';
                                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                                        command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@Profit", SqlDbType.Decimal);
                                        command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                                        command.Parameters["@Profit"].Precision = 18;
                                        command.Parameters["@Profit"].Scale = 2;
                                        command.ExecuteReader();
                                        decimal profit = Convert.ToDecimal(command.Parameters["@Profit"].Value.ToString());
                                        if (profit < 0)
                                            label3.Text = "0";
                                        else
                                            label3.Text = Convert.ToString(profit);
                                        command.Dispose();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                            }
                            catch (Exception)
                            { }
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter BillNo.", "Warning");
                        this.ActiveControl = txtBillNo;
                    }
                }
                else if (chkproductname.Checked == true)
                {
                    if (txtpname.Text != "")
                    {
                        if ((txtpname.Text).Contains(","))
                        {
                            string Proname = "", company = "", unit = "", flag = "";
                            try
                            {
                                Proname = txtpname.Text.Trim().Split(',')[0];
                                company = txtpname.Text.Trim().Split(',')[1];
                                unit = txtpname.Text.Trim().Split(',')[2];
                                ProductRepository pRepo = new ProductRepository();
                                var pData = pRepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (pData != null)
                                    flag = "P";
                            }
                            catch (Exception)
                            { }
                            try
                            {
                                DataTable dt = new DataTable();
                                lblsaleamount.Text = "0";
                                label3.Text = "0";
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Proname;
                                    command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0'; //dtpfromdate.Value.Date.ToShortDateString();
                                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0'; //dtpfromdate.Value.Date.ToShortDateString();
                                    command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = '0';
                                    command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = flag;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    SqlDataReader reader = command.ExecuteReader();
                                    dt.Load(reader);
                                    reader.Dispose();
                                    command.Dispose();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                try
                                {
                                    GvCredit.DataSource = null;
                                    GvCredit.DataSource = dt;
                                    GvCredit.Refresh();
                                    if (GvCredit.Rows.Count != 0)
                                    {
                                        GvCredit.Columns["Id"].Visible = false;
                                        GvCredit.Columns["BillNo"].Width = 50;
                                        GvCredit.Columns["ProductName"].Width = 150;
                                        GvCredit.Columns["CompanyName"].Width = 120;
                                        GvCredit.Columns["Unit"].Width = 60;
                                        GvCredit.Columns["SaleUnit"].Width = 60;
                                        GvCredit.Columns["Quantity"].Width = 60;
                                        GvCredit.Columns["Rate"].Width = 70;
                                        GvCredit.Columns["Amount"].Width = 70;
                                        GvCredit.Columns["GSTAmt"].Width = 70;
                                        GvCredit.Columns["SAmt"].Width = 70;
                                        GvCredit.Columns["SRAmt"].Width = 70;
                                        GvCredit.Columns["Type"].Width = 50;
                                        GvCredit.Columns["PRate"].Width = 70;

                                        if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                                            lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                                        else
                                            lblsaleamount.Text = "0";
                                    }
                                    else
                                        this.ActiveControl = dtpfromdate;
                                    if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                                    {
                                        label1.Visible = true;
                                        label3.Visible = true;
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;
                                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0'; //dtpfromdate.Value.Date.ToShortDateString();
                                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@Profit", SqlDbType.Decimal);
                                            command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                                            command.Parameters["@Profit"].Precision = 18;
                                            command.Parameters["@Profit"].Scale = 2;
                                            command.ExecuteReader();
                                            decimal profit = Convert.ToDecimal(command.Parameters["@Profit"].Value.ToString());
                                            if (profit < 0)
                                                label3.Text = "0";
                                            else
                                                label3.Text = Convert.ToString(profit);
                                            command.Dispose();
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter Product Name");
                        this.ActiveControl = txtpname;
                    }
                }
                else
                {
                    MessageBox.Show("Please Check Box.", "Warning");
                    this.ActiveControl = chkDate;
                }
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string productname = "", mfgcompany = "";
            try
            {
                if (chkproductname.Checked == true && txtpname.Text != "")
                    productname = txtpname.Text;
                if (chkCompanyName.Checked == true && cmbcomanyname.Text != "")
                    mfgcompany = cmbcomanyname.Text;
                RptDailySaleDetail dsrpt = new RptDailySaleDetail(productname, mfgcompany);
                dsrpt.ShowDialog();
                dsrpt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void getall()
        {
            try
            {
                DataTable dt = new DataTable();
                lblsaleamount.Text = "0";
                label3.Text = "0";
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPDailySaleDetails", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");//dtpfromdate.Value.Date.ToShortDateString();
                    command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                    command.Parameters.Add("@UBillNo", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "FD";
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Dispose();
                    command.Dispose();
                    db.CloseConnection();
                }
                catch (Exception)
                { db.CloseConnection(); }
                try
                {
                    GvCredit.DataSource = null;
                    GvCredit.DataSource = dt;
                    GvCredit.Refresh();
                    if (GvCredit.Rows.Count != 0)
                    {
                        GvCredit.Columns[0].Visible = false;
                        if (Convert.ToString(dt.Compute("Sum(SAmt)", "")) != "" && Convert.ToString(dt.Compute("Sum(SRAmt)", "")) != "")
                            lblsaleamount.Text = Convert.ToString(Convert.ToDecimal(dt.Compute("Sum(SAmt)", "")) - Convert.ToDecimal(dt.Compute("Sum(SRAmt)", "")));
                        else
                            lblsaleamount.Text = "0";
                    }
                    else
                        this.ActiveControl = dtpfromdate;
                    if (Convert.ToDecimal(lblsaleamount.Text) > 0)
                    {
                        label1.Visible = true;
                        label3.Visible = true;
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPDailySaleProfit", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 'D';
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");//dtpfromdate.Value.Date.ToShortDateString();
                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy"); //dtpfromdate.Value.Date.ToShortDateString();
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Profit", SqlDbType.Decimal);
                            command.Parameters["@Profit"].Direction = ParameterDirection.Output;
                            command.Parameters["@Profit"].Precision = 18;
                            command.Parameters["@Profit"].Scale = 2;
                            command.ExecuteReader();
                            label3.Text = command.Parameters["@Profit"].Value.ToString();
                            if (label3.Text == "-1")
                                label3.Text = "0";
                            command.Dispose();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            getall();
            try
            {
                chkproductname.Checked = false;
                chkCompanyName.Checked = false;
            }
            catch (Exception)
            { }
            txtpname.Text = "";
            txtBillNo.Text = "";
            fillcombo();
            dtpfromdate.Value = DateTime.Now.Date;
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

        private void frmDailySaleDetail_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtpname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation", con);
                    else
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
                    txtpname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }

                txtBillNo.Text = "";
                string cnString = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString))
                {
                    SqlCommand cmd = new SqlCommand("Select distinct BillNo from CustomerBillMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", con);
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
                    txtBillNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkproductname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkproductname.Checked)
                {
                    chkCompanyName.Checked = false;
                    chkBillNo.Checked = false;
                    fillcombo();
                    txtBillNo.Text = "";
                }
                else
                    txtpname.Text = "";
            }
            catch (Exception)
            { }
        }

        private void chkCompanyName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCompanyName.Checked)
                {
                    fillcombo();
                    chkproductname.Checked = false;
                    chkBillNo.Checked = false;
                }
                else
                    fillcombo();
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "")
                {
                    string Proname = "", company = "", unit = "";
                    if ((txtpname.Text).Contains(","))
                    {
                        try
                        {
                            Proname = txtpname.Text.Trim().Split(',')[0];
                            company = txtpname.Text.Trim().Split(',')[1];
                            unit = txtpname.Text.Trim().Split(',')[2];
                        }
                        catch (Exception)
                        { }

                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtpname.Text = "";
                        this.ActiveControl = txtpname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcomanyname.Text != "Select" && cmbcomanyname.Text != "Select")
                {
                    ManufactureCInfoRepository unitrepo = new ManufactureCInfoRepository();
                    ManuCompanyInfo CompData = new ManuCompanyInfo();
                    if (CommonMethod.commProduct == true)
                        CompData = unitrepo.GetAll().Where(t => t.CompanyName == cmbcomanyname.Text).FirstOrDefault();
                    else
                        CompData = unitrepo.GetAll().Where(t => t.CompanyName == cmbcomanyname.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (CompData == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillcombo();
                        this.ActiveControl = cmbcomanyname;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtBillNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBillNo.Text != "")
                {
                    CustomerBillMasterRepository CustRepo = new CustomerBillMasterRepository();
                    var CustData = CustRepo.GetAll().Where(t => t.BillNo == txtBillNo.Text.ToString() && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (CustData == null)
                    {
                        MessageBox.Show("This BillNo Is Not Valid.", "Warning");
                        txtBillNo.Text = "";
                        this.ActiveControl = txtBillNo;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void chkBillNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBillNo.Checked)
                {
                    chkproductname.Checked = false;
                    chkCompanyName.Checked = false;
                }
                else
                {
                    fillcombo();
                    txtpname.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
            }
            catch (Exception)
            { }
        }

        private void chkproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void chkDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        private void chkBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBillNo;
            }
            catch (Exception)
            { }
        }
    }
}