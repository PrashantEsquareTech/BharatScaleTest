using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;
using System.Data;
using System.Data.SqlClient;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmVehicleInformation : Form
    {
        public int Vehicleid = 0;
        DbClass db = new DbClass();

        public frmVehicleInformation()
        {
            InitializeComponent();
            maxVehicleid();
            BindVehicleinfo();
            btnDelete.Enabled = false;
            this.ActiveControl = txtVehicleNumber;
        }

        public void maxVehicleid()
        {
            try
            {
                VehicleInformationRepository Vehiclerepo = new VehicleInformationRepository();
                var mid = Convert.ToInt32(Vehiclerepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.VehicleId));
                if (mid == null)
                    txtVehicleID.Text = "1";
                else
                    txtVehicleID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                Vehiclerepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtVehicleNumber.Text = "";
                txtDescription.Text = "";
                Vehicleid = 0;
                maxVehicleid();
                BindVehicleinfo();
                this.ActiveControl = txtVehicleNumber;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                VehicleInformationRepository Vehiclerepo = new VehicleInformationRepository();
                VehicleInformation Vehicledata = new VehicleInformation();
                Vehicledata = Vehiclerepo.GetAll().Where(t => t.VehicleNumber == txtVehicleNumber.Text.Trim() && t.VehicleId != Convert.ToInt32(txtVehicleID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (Vehicledata != null)
                {
                    MessageBox.Show("This vehicle Name Already Present.", "Warning");
                    clear();
                    this.ActiveControl = txtVehicleNumber;
                }
                else
                {
                    if (txtVehicleNumber.Text.Trim() != "")
                    {
                        Vehiclerepo = new VehicleInformationRepository();
                        VehicleInformation supp = new VehicleInformation();
                        supp = Vehiclerepo.GetAll().Where(t => t.VehicleNumber.ToLower() == txtVehicleNumber.Text.Trim().ToLower() && t.VehicleId != Convert.ToInt32(txtVehicleID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtVehicleNumber.Text = "";
                            this.ActiveControl = txtVehicleNumber;
                        }
                        else
                        {
                            if (btnAdd.Text == "Add")
                            {
                                VehicleInformation unitinfo = new VehicleInformation();
                                maxVehicleid();
                                unitinfo.VehicleId = Convert.ToInt32(txtVehicleID.Text);
                                unitinfo.VehicleNumber = txtVehicleNumber.Text.Trim();
                                unitinfo.Description = txtDescription.Text.Trim();
                                unitinfo.CompId = CommonMethod.CompId;
                                Vehiclerepo.Add(unitinfo);
                                Vehiclerepo.Save();
                                MessageBox.Show("Vehicle Add Successfully.", "Success");
                                clear();
                            }
                            else
                            {
                                string vehicalno = "";
                                int dcNo = 0, billid = 0;
                                VehicleInformationRepository vehicleRepo = new VehicleInformationRepository();
                                var vehicleData = vehicleRepo.GetAll().Where(t => t.VehicleId == Vehicleid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (vehicleData != null)
                                    vehicalno = vehicleData.VehicleNumber;
                                VehicleInformation unitinfo = new VehicleInformation();
                                unitinfo = Vehiclerepo.GetById(Vehicleid);
                                unitinfo.VehicleId = Convert.ToInt32(txtVehicleID.Text);
                                unitinfo.VehicleNumber = txtVehicleNumber.Text.Trim();
                                unitinfo.Description = txtDescription.Text.Trim();
                                unitinfo.CompId = CommonMethod.CompId;
                                Vehiclerepo.Edit(unitinfo);
                                Vehiclerepo.Save();
                                if (vehicalno != null && vehicalno != "")
                                {
                                    DeliveryChallanRepository deliveryRepo = new DeliveryChallanRepository();
                                    List<DeliveryChallanMaster> listData = deliveryRepo.GetAll().Where(t => t.VehicalNo == vehicalno && t.CompId == CommonMethod.CompId).ToList();
                                    if (listData.Count != 0)
                                    {
                                        foreach (var item in listData)
                                        {
                                            dcNo = item.Id;
                                            DeliveryChallanRepository mastRepo = new DeliveryChallanRepository();
                                            var deliveryData = mastRepo.GetAll().Where(t => t.Id == dcNo).FirstOrDefault();
                                            if (deliveryData != null)
                                            {
                                                DeliveryChallanMaster masterdata = mastRepo.GetAll().Where(t => t.Id == dcNo).FirstOrDefault();
                                                masterdata.VehicalNo = txtVehicleNumber.Text;
                                                mastRepo.Edit(masterdata);
                                                mastRepo.Save();
                                            }
                                        }
                                    }

                                    CustomerBillMasterRepository billrepo = new CustomerBillMasterRepository();
                                    List<CustomerBillMaster> billlist = billrepo.GetAll().Where(t => t.VehicleNo == vehicalno && t.CompId == CommonMethod.CompId).ToList();
                                    if (billlist.Count != 0)
                                    {
                                        foreach (var item1 in billlist)
                                        {
                                            billid = item1.Id;
                                            CustomerBillMasterRepository billRepo = new CustomerBillMasterRepository();
                                            var billdata = billRepo.GetAll().Where(t => t.Id == billid).FirstOrDefault();
                                            if (billdata != null)
                                            {
                                                CustomerBillMaster masterData = billRepo.GetAll().Where(t => t.Id == billid).FirstOrDefault();
                                                masterData.VehicleNo = txtVehicleNumber.Text;
                                                billRepo.Edit(masterData);
                                                billRepo.Save();
                                            }
                                        }
                                    }
                                }
                                MessageBox.Show("Vehicle updated Successfully.", "Success");
                                clear();
                            }
                        }
                    }
                    else
                    {
                        if (txtVehicleNumber.Text == "")
                        {
                            MessageBox.Show("Enter Vehicle Number.", "Warning");
                            this.ActiveControl = txtVehicleNumber;
                        }
                    }
                }
                Vehiclerepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindVehicleinfo()
        {
            try
            {
                VehicleInformationRepository unitrepo = new VehicleInformationRepository();
                dtgvList.Rows.Clear();
                List<VehicleInformation> ItemList = unitrepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).ToList();
                lblTotaluUnits.Text = Convert.ToString(ItemList.Count);

                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["VehicleIdg"].Value = item.VehicleId;
                        dtgvList.Rows[i].Cells["VehicleNumber"].Value = item.VehicleNumber;
                        dtgvList.Rows[i].Cells["Description"].Value = item.Description;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void GvVehicleInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Vehicleid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtVehicleID.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["VehicleIdg"].Value);
                txtVehicleNumber.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["VehicleNumber"].Value);
                txtDescription.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (Vehicleid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Vehicle";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Vehicleid;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Check", SqlDbType.NVarChar);
                            command.Parameters["@Check"].Direction = ParameterDirection.Output;
                            command.Parameters["@Check"].Size = 5;
                            command.ExecuteReader();
                            strcheck = command.Parameters["@Check"].Value.ToString();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        if (strcheck == "T")
                            MessageBox.Show("This Vehicle No is Used. You Can Not Delete.", "Warning");
                        else
                        {
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clear();
                        }
                    }
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
            else if (keyData == Keys.F3)
            {
                btnAdd_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                btnAdd.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void txtVehicleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtDescription;
            }
            catch (Exception)
            { }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }
    }
}