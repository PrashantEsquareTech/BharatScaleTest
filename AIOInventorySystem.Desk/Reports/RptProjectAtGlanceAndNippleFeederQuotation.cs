using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptProjectAtGlanceAndNippleFeederQuotation : Form
    {
        DbClass db = new DbClass();
        ProjectAtGlanceQuotation crpt = new ProjectAtGlanceQuotation();
        FrontPageProjectAtGlanceCrystalReport crpt2 = new FrontPageProjectAtGlanceCrystalReport();
        NippleFeederQuotationCrystalReport crpt1 = new NippleFeederQuotationCrystalReport();
        EzypanFeedingSystemCrystalReport crpt3 = new EzypanFeedingSystemCrystalReport();
        public string Instruction1 = "", Instruction2 = "";

        public RptProjectAtGlanceAndNippleFeederQuotation()
        {
            InitializeComponent();
        }

        public RptProjectAtGlanceAndNippleFeederQuotation(int PrintId, string type)
        {
            InitializeComponent();
            if (type == "GlanceQut")
                getPrint(PrintId);
            else if (type == "NippleFeederQut")
                getNippleFeederSyytemPrint(PrintId);
            else if (type == "FrontPage")
                getFrontPagePrint(PrintId);
        }

        public void getPrint(int printId)
        {
            try
            {
                DSProjectAtGlanceQutotaion ds = new DSProjectAtGlanceQutotaion();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from tblProjectAtGlanceMaster where Id=" + printId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectAtGlanceMaster");
                DataTable dtProjectMaster = new DataTable();
                da.Fill(dtProjectMaster);
                if (dtProjectMaster.Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select * From CustomerInformation where Id=" + Convert.ToInt32(dtProjectMaster.Rows[0]["CustomerId"].ToString()) + " and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerInformation");
                }

                da = new SqlDataAdapter("Select pd.* from tblProjectGlanceDetail as pd inner join tblProjectAtGlanceMaster as pm on pm.Id=pd.MasterId where pd.MasterId=" + printId + " and pm.CompId=" + CommonMethod.CompId + " and pm.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectGlanceDetail");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");                

                DataTable dtInstruct = db.GetTable("Select * from BillInstruction where CompId=" + CommonMethod.CompId + "");
                if (dtInstruct.Rows.Count > 0)
                {
                    if (dtInstruct.Rows[0]["InstructionText1"].ToString() == null || dtInstruct.Rows[0]["InstructionText1"].ToString() == "")
                        Instruction1 = "";
                    else
                        Instruction1 = Convert.ToString(dtInstruct.Rows[0]["InstructionText1"].ToString());
                    if (dtInstruct.Rows[0]["InstructionText2"].ToString() == null || dtInstruct.Rows[0]["InstructionText2"].ToString() == "")
                        Instruction2 = "";
                    else
                        Instruction2 = Convert.ToString(dtInstruct.Rows[0]["InstructionText2"].ToString());
                }
                else
                {
                    Instruction1 = "";
                    Instruction2 = "";
                }
                da = new SqlDataAdapter("Select * From tnlQuotationImges where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tnlQuotationImges");

                crpt = new ProjectAtGlanceQuotation();
                crpt.SetDataSource(ds);

                crpt.SetParameterValue("Instruction", Instruction1);
                crpt.SetParameterValue("Instruction1", Instruction2);
                crystalReportViewer1.ReportSource = crpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose(); ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getNippleFeederSyytemPrint(int printId)
        {
            try
            {
                int referenceId = 0;
                string SystemType = "";
                DSProjectAtGlanceQutotaion ds = new DSProjectAtGlanceQutotaion();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from tblNippleFeederSystemMaster where Id=" + printId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblNippleFeederSystemMaster");
                DataTable dtProject =new DataTable();
                da.Fill(dtProject);
                if (dtProject.Rows.Count > 0)
                {
                    referenceId = Convert.ToInt32(dtProject.Rows[0]["ReferenceId"].ToString());
                    SystemType = Convert.ToString(dtProject.Rows[0]["Type"].ToString());
                }

                da = new SqlDataAdapter("Select * from tblNippleFeederSystemDetail where MasterId=" + printId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblNippleFeederSystemDetail");                

                da = new SqlDataAdapter("Select * from tblProjectAtGlanceMaster where Id=" + referenceId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectAtGlanceMaster");
                DataTable dtProjectMaster = new DataTable();
                da.Fill(dtProjectMaster);

                da = new SqlDataAdapter("Select * from tblProjectGlanceDetail as pd inner join tblProjectAtGlanceMaster as pm on pm.Id=pd.MasterId where pd.MasterId=" + referenceId + " and pm.CompId=" + CommonMethod.CompId + " and pm.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectGlanceDetail");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (dtProjectMaster.Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select * From CustomerInformation where Id=" + Convert.ToInt32(dtProjectMaster.Rows[0]["CustomerId"].ToString()) + " and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerInformation");
                }
                DataTable dtInstruct = db.GetTable("Select * from BillInstruction where CompId=" + CommonMethod.CompId + "");
                if (dtInstruct.Rows.Count > 0)
                {
                    if (dtInstruct.Rows[0]["InstructionText3"].ToString() == null || dtInstruct.Rows[0]["InstructionText3"].ToString() == "")
                        Instruction1 = "";
                    else
                        Instruction1 = Convert.ToString(dtInstruct.Rows[0]["InstructionText3"].ToString());
                }
                else
                    Instruction1 = "";
                da = new SqlDataAdapter("Select * From tnlQuotationImges where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tnlQuotationImges");

                if (SystemType == "NIPPLE SYSTEM")
                {
                    crpt1 = new NippleFeederQuotationCrystalReport();
                    crpt1.SetDataSource(ds);
                    crpt1.SetParameterValue("Instruction", Instruction1);
                    crystalReportViewer1.ReportSource = crpt1;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose(); ds.Dispose();
                }
                else
                {
                    crpt3 = new EzypanFeedingSystemCrystalReport();
                    crpt3.SetDataSource(ds);
                    crpt3.SetParameterValue("Instruction", Instruction1);
                    crystalReportViewer1.ReportSource = crpt3;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose(); ds.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getFrontPagePrint(int printId)
        {
            try
            {
                DSFrontProjectAtGlance ds = new DSFrontProjectAtGlance();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from tblProjectAtGlanceMaster where Id=" + printId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectAtGlanceMaster");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                da = new SqlDataAdapter("Select * From tnlQuotationImges where CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tnlQuotationImges");

                DataTable dtProjectMaster = db.GetTable("Select * from tblProjectAtGlanceMaster where Id=" + printId + "");
                if (dtProjectMaster.Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select * From CustomerInformation where Id=" + Convert.ToInt32(dtProjectMaster.Rows[0][4].ToString()) + " and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CustomerInformation");

                    da = new SqlDataAdapter("Select * From StaffInfo where Id=" + Convert.ToInt32(dtProjectMaster.Rows[0][5].ToString()) + " and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "StaffInfo");
                }

                crpt2 = new FrontPageProjectAtGlanceCrystalReport();
                crpt2.SetDataSource(ds);
                crystalReportViewer1.ReportSource = crpt2;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose(); ds.Dispose();
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