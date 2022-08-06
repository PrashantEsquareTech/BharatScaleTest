using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptServicePrint : Form
    {
        DbClass db = new DbClass();
        ServiceInvoiceCrystalReport ServiceInvoiceRpt = new ServiceInvoiceCrystalReport();
        SeviceRegister ServiceRegRpt = new SeviceRegister();

        public RptServicePrint()
        {
            InitializeComponent();
            GetRegisterPrint();
        }

        public RptServicePrint(int ServiceId, string flag)
        {
            InitializeComponent();
            if (flag == "Print")//for Single service print (Invoice)
                getdata(ServiceId);
        }

        public void getdata(int ServiceId)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSServiceInvoice ds = new DSServiceInvoice();
                da = new SqlDataAdapter("Select * from ServiceInvoiceMaster where Id=" + ServiceId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);                
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ServiceInvoiceMaster");

                da = new SqlDataAdapter("Select * from ServiceInvoiceDetail where ServiceId=" + ServiceId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ServiceInvoiceDetail");

                da = new SqlDataAdapter("Select * from CustomerInformation where Id=(Select CustomerId from ServiceInvoiceMaster where Id=" + ServiceId + " and CompId=" + CommonMethod.CompId + ") and CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");

                da = new SqlDataAdapter("Select * from ProductInformation where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from BillInstruction where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BillInstruction");

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                ServiceInvoiceRpt = new ServiceInvoiceCrystalReport();
                ServiceInvoiceRpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = ServiceInvoiceRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void GetRegisterPrint()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                DSServiceInvoice ds = new DSServiceInvoice();
                da = new SqlDataAdapter("Select * from ServiceInvoiceMaster where CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ServiceInvoiceMaster");

                da = new SqlDataAdapter("Select * from ServiceInvoiceDetail SD Inner Join ServiceInvoiceMaster SM ON SM.Id=SD.ServiceId where Sm.CompId=" + CommonMethod.CompId + " and Sm.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ServiceInvoiceDetail");

                da = new SqlDataAdapter("Select * from CustomerInformation where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CustomerInformation");

                da = new SqlDataAdapter("Select * from ProductInformation where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                da = new SqlDataAdapter("Select * from BillInstruction where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BillInstruction");

                da = new SqlDataAdapter("Select * from Settings where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Settings");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                ServiceRegRpt = new SeviceRegister();
                ServiceRegRpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = ServiceRegRpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
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
    }
}