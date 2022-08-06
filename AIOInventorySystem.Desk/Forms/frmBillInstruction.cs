using System;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBillInstruction : Form
    {
        public int insId;
        DbClass db = new DbClass();
        int maxid = 0;

        public frmBillInstruction()
        {
            InitializeComponent();
            MaxInstructId();
            BindInstructInfo();
        }

        public void MaxInstructId()
        {
            try
            {
                BillInstructionRepository insRepo = new BillInstructionRepository();
                var mid = insRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.Id);
                if (mid == null)
                    maxid = 1;
                insRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Clear()
        {
            try
            {
                txtStart1.Text = "";
                txtStart2.Text = "";
                txtStart3.Text = "";
                Instru1.Text = "";
                Instru2.Text = "";
                Instru3.Text = "";
                Note1.Text = "";
                Note2.Text = "";
                txtdeclaration.Text = string.Empty;
                txttermsconditions.Text = string.Empty;
                MaxInstructId();
                BindInstructInfo();
            }
            catch (Exception)
            { }
        }

        public void BindInstructInfo()
        {
            try
            {
                BillInstructionRepository insRepo = new BillInstructionRepository();
                BillInstruction ItemList = insRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (ItemList != null)
                {
                    maxid = ItemList.Id;
                    txtStart1.Text = ItemList.StartigText1.ToString();
                    txtStart2.Text = ItemList.StartigText2.ToString();
                    txtStart3.Text = ItemList.StartigText3.ToString();
                    Instru1.Text = ItemList.InstructionText1.ToString();
                    Instru2.Text = ItemList.InstructionText2.ToString();
                    Instru3.Text = ItemList.InstructionText3.ToString();
                    Note1.Text = ItemList.NoteText1.ToString();
                    Note2.Text = ItemList.NoteText2.ToString();
                    txtdeclaration.Text = ItemList.Declaration.ToString();
                    txttermsconditions.Text = ItemList.TermsConditions.ToString();
                    btnSave.Text = "Update";
                }
                insRepo.Dispose();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStart1.Text.Trim() == "" && txtStart2.Text.Trim() == "" && txtStart3.Text.Trim() == "" && Instru1.Text.Trim() == "" && Instru2.Text.Trim() == "" && Instru3.Text.Trim() == "" && Note1.Text.Trim() == "" && Note2.Text.Trim() == "" && txtdeclaration.Text.Trim() == "" && txttermsconditions.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Text.","Warning");
                    this.ActiveControl= txtStart1;
                }
                else
                {
                    BillInstructionRepository insRepo = new BillInstructionRepository();
                    BillInstruction instruction = new BillInstruction();
                    instruction.StartigText1 = txtStart1.Text.Trim();
                    instruction.StartigText2 = txtStart2.Text.Trim();
                    instruction.StartigText3 = txtStart3.Text.Trim();
                    instruction.InstructionText1 = Instru1.Text.Trim();
                    instruction.InstructionText2 = Instru2.Text.Trim();
                    instruction.InstructionText3 = Instru3.Text.Trim();
                    instruction.NoteText1 = Note1.Text.Trim();
                    instruction.NoteText2 = Note2.Text.Trim();
                    instruction.Declaration = txtdeclaration.Text.Trim();
                    instruction.TermsConditions = txttermsconditions.Text.Trim();
                    instruction.CompId = CommonMethod.CompId;
                    if (btnSave.Text == "Update")
                    {
                        instruction.Id = maxid;
                        insRepo.Edit(instruction);
                        insRepo.Save();
                        MessageBox.Show("Instruction Updated Successfully.","Success");
                        Clear();
                        this.ActiveControl = txtStart1;
                    }
                    else
                    {
                        insRepo.Add(instruction);
                        insRepo.Save();
                        MessageBox.Show("Instruction Saved Successfully.","Success");
                        Clear();
                        this.ActiveControl = txtStart1;
                    }
                    insRepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void frmBillInstruction_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }
    }
}
