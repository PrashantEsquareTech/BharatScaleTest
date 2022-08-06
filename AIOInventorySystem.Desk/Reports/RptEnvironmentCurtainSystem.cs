using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptEnvironmentCurtainSystem : Form
    {
        DbClass db = new DbClass();
        public int referenceId = 0;
        public string Instruction1 = "";
        EnvironmentControlSystemCrystalReport crpt = new EnvironmentControlSystemCrystalReport();
        CurtainCoolPadSystemCrystalReport crpt1 = new CurtainCoolPadSystemCrystalReport();
        VentilationCoolingSystemCrystalReport crpt2 = new VentilationCoolingSystemCrystalReport();

        public RptEnvironmentCurtainSystem()
        {
            InitializeComponent();
        }

        public RptEnvironmentCurtainSystem(int PrintId, string sType)
        {
            InitializeComponent();
            getPrint(PrintId, sType);
        }

        public RptEnvironmentCurtainSystem(int PrintId)
        {
            InitializeComponent();
            getVentilationPrint(PrintId);
        }

        public void getPrint(int printId, string sType)
        {
            try
            {
                DSEnvironmentAndCurtainCoolPadSystem ds = new DSEnvironmentAndCurtainCoolPadSystem();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from tblEnvirnmentAndCurtainCoolpadSystemMaster where Id=" + printId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblEnvirnmentAndCurtainCoolpadSystemMaster");
                DataTable dtProject = new DataTable();
                da.Fill(dtProject);
                if (dtProject.Rows.Count > 0)
                    referenceId = Convert.ToInt32(dtProject.Rows[0]["ReferenceId"].ToString());

                da = new SqlDataAdapter("Select * from tbEnvirnmentAndCurtainCoolpadSystemDetail where MasterId=" + printId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tbEnvirnmentAndCurtainCoolpadSystemDetail");
                
                da = new SqlDataAdapter("Select * from tblProjectAtGlanceMaster where Id=" + referenceId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectAtGlanceMaster");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dtProjectMaster = db.GetTable("Select * from tblProjectAtGlanceMaster where Id=" + referenceId + "");
                if (dtProjectMaster.Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select * From CustomerInformation where Id=" + Convert.ToInt32(dtProjectMaster.Rows[0][4].ToString()) + " and CompId=" + CommonMethod.CompId + "", db.Connection);
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
                da = new SqlDataAdapter("Select * from tnlQuotationImges where  CompId=" + CommonMethod.CompId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tnlQuotationImges");

                if (sType == "Environment Control System")
                {
                    crpt = new EnvironmentControlSystemCrystalReport();
                    crpt.SetDataSource(ds);
                    crpt.SetParameterValue("Instruction", Instruction1);
                    crystalReportViewer1.ReportSource = crpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose(); ds.Dispose();
                }
                else
                {
                    crpt1 = new CurtainCoolPadSystemCrystalReport();
                    crpt1.SetDataSource(ds);
                    crpt1.SetParameterValue("Instruction", Instruction1);
                    crystalReportViewer1.ReportSource = crpt1;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose(); ds.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getVentilationPrint(int printId)
        {
            try
            {
                DSEnvironmentAndCurtainCoolPadSystem ds = new DSEnvironmentAndCurtainCoolPadSystem();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from tblVentilationAndCoolingSystem where Id=" + printId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblVentilationAndCoolingSystem");
                DataTable dtProject = new DataTable();
                da.Fill(dtProject);
                if (dtProject.Rows.Count > 0)
                    referenceId = Convert.ToInt32(dtProject.Rows[0]["ReferenceId"].ToString());

                da = new SqlDataAdapter("Select * from tblVentilationAndCoolingSystemDetail where MasterId=" + printId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblVentilationAndCoolingSystemDetail");

                da = new SqlDataAdapter("Select * from tblProjectAtGlanceMaster where Id=" + referenceId + " and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblProjectAtGlanceMaster");

                da = new SqlDataAdapter("Select * From CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dtProjectMaster = db.GetTable("Select * from tblProjectAtGlanceMaster where Id=" + referenceId + "");
                if (dtProjectMaster.Rows.Count > 0)
                {
                    da = new SqlDataAdapter("Select * From CustomerInformation where Id=" + Convert.ToInt32(dtProjectMaster.Rows[0][4].ToString()) + " and CompId=" + CommonMethod.CompId + "", db.Connection);
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
                da = new SqlDataAdapter("Select * from tnlQuotationImges where  CompId=" + CommonMethod.CompId + " ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tnlQuotationImges");

                crpt2 = new VentilationCoolingSystemCrystalReport();
                crpt2.SetDataSource(ds);
                CrystalDecisions.Shared.PageMargins margins;
                margins = crpt2.PrintOptions.PageMargins;
                margins.bottomMargin = 100;
                margins.leftMargin = 100;
                margins.rightMargin = 100;
                margins.topMargin = 1500;
                crpt2.PrintOptions.ApplyPageMargins(margins);
                crpt2.SetParameterValue("Instruction", Instruction1);
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