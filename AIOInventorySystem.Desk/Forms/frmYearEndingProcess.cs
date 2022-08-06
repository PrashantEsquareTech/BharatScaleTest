using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmYearEndingProcess : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmYearEndingProcess()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
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

        private void btnCustomerOpeningBalance_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPCustomerOpeningBalance", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Customer Opening Balance saved successfully.", "Success");
                btnCustomerOpeningBalance.Enabled = false;
                btnSupplierOpeningBalance.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnSupplierOpeningBalance_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPSupplierOpeningBalance", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Supplier Opening Balance saved successfully.", "Success");
                btnSupplierOpeningBalance.Enabled = false;
                btnProprietorOpeningBalance.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnOpeningStock_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPOpeningStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = 'a';
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@flag", SqlDbType.Int).Value = 1;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Opening Stock saved successfully.", "Success");
                btnOpeningStock.Enabled = false;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnOpeningStockVat_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPOpeningStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = 'y';
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@flag", SqlDbType.Int).Value = 1;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Opening Stock of GST saved successfully.", "Success");
                btnOpeningStockVat.Enabled = false;
                btnOpeningStockNoVat.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnOpeningStockNoVat_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPOpeningStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = 'n';
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@flag", SqlDbType.Int).Value = 1;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Opening Stock of without GST saved successfully.", "Success");
                btnOpeningStockNoVat.Enabled = false;
                btnOpeningStock.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void frmYearEndingProcess_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;

                saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
                var connectionString = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                SqlConnection Connection = new SqlConnection(connectionString);

                var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);
                saveFileDialog1.FileName = sqlConStrBuilder.InitialCatalog;
                if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                            sqlConStrBuilder.InitialCatalog, saveFileDialog1.FileName);
                        using (var command = new SqlCommand(query, Connection))
                        {
                            Connection.Open();
                            command.ExecuteNonQuery();
                            MessageBox.Show("Backup Created Sucessfully.", "Success");
                            Connection.Close();
                            btnCustomerOpeningBalance.Enabled = true;
                            command.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Backup not Created" + Environment.NewLine + "Get backup from Sql Server.", "Warning");
                btnStartProcess.Visible = true;
            }
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            try
            {
                btnStartProcess.Enabled = false;
                btnCustomerOpeningBalance.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPAvgPurSaleRateSaveUpdate", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Average Rate Added Successfully.", "Success");
                btnAvg.Enabled = false;
                btnOpeningStockVat.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public decimal AvreRageSalePeice(string proName, string Company, string unit)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proName;
                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = Company;
                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                SqlDataReader reader = command.ExecuteReader();
                DataTable dtRate = new DataTable();
                dtRate.Load(reader);
                decimal avgprice = 0;
                avgprice = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                dtRate.Dispose();
                db.CloseConnection();
                return avgprice;                
            }
            catch (Exception)
            {
                db.CloseConnection();
                return 0;                
            }
        }

        public decimal AvreRagePurchasePeice(string proName, string Company, string unit)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proName;
                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = Company;
                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                SqlDataReader reader = command.ExecuteReader();
                DataTable dtRate = new DataTable();
                dtRate.Load(reader);
                decimal avgprice = 0;
                avgprice = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                dtRate.Dispose();
                db.CloseConnection();
                return avgprice;                
            }
            catch (Exception)
            {
                db.CloseConnection();
                return 0;                
            }
        }

        private void btnProprietorOpeningBalance_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPProprietorOpeningBalance", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Proprietor Opening Balance saved successfully.", "Success");
                btnProprietorOpeningBalance.Enabled = false;
                btnBankOpeningBalance.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnBankOpeningBalance_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPBankOpeningBalance", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.CommandTimeout = 1000;
                command.ExecuteNonQuery();
                db.CloseConnection();
                MessageBox.Show("Bank Opening Balance saved successfully.", "Success");
                btnBankOpeningBalance.Enabled = false;
                btnAvg.Enabled = true;
                command.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
    }
}