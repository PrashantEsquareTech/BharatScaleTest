using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Data.Model;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmRouteInfo : Form
    {
        int routeId = 0, villlageId = 0;
        public frmRouteInfo()
        {
            InitializeComponent();
            routemaxid();
            villagemaxid();
            cmbRouteLoad();
            bindRouteDataToGrid();
            bindVillageDataToGrid();
        }
        public void bindVillageDataToGrid()
        {
            try
            {
                VillageRepository villageRepo = new VillageRepository();
                List<tblVillageInfo> villageList = villageRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                grdVillage.Rows.Clear();
                if (villageList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in villageList)
                    {
                        grdVillage.Rows.Add();
                        grdVillage.Rows[i].Cells["ID"].Value = item.Id;
                        grdVillage.Rows[i].Cells["VillageID"].Value = item.VillageId;
                        grdVillage.Rows[i].Cells["VillageName"].Value = item.VillageName;

                        RouteRepository rRepo = new RouteRepository();
                        string routeName = rRepo.GetAll().Where(t => t.RouteId == item.RouteId).FirstOrDefault().RouteName;
                        rRepo.Dispose();
                        grdVillage.Rows[i].Cells["RouteNameGrd"].Value = routeName;
                        i++;
                    }
                }
                villageRepo.Dispose();
            }
            catch (Exception)
            { }
        }
        public void bindRouteDataToGrid()
        {
            try
            {
                RouteRepository routeRepo = new RouteRepository();
                List<tblRouteInfo> routeList = routeRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                grdRoute.Rows.Clear();
                if (routeList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in routeList)
                    {
                        grdRoute.Rows.Add();
                        grdRoute.Rows[i].Cells["PID"].Value = item.Id;
                        grdRoute.Rows[i].Cells["PRouteID"].Value = item.RouteId;
                        grdRoute.Rows[i].Cells["RouteName"].Value = item.RouteName;

                        i++;
                    }
                }
                routeRepo.Dispose();
            }
            catch (Exception)
            { }
        }
        public void cmbRouteLoad()
        {
            try
            {
                RouteRepository ACGRepo = new RouteRepository();
                List<tblRouteInfo> ACGdata = new List<tblRouteInfo>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.RouteName).ToList();
                ACGdata.Add(new tblRouteInfo { RouteName = "Select", Id = 0 });
                ACGdata.Reverse();
                cmbRouteName.DataSource = ACGdata;
                cmbRouteName.ValueMember = "Id";
                cmbRouteName.DisplayMember = "RouteName";
                ACGRepo.Dispose();
            }
            catch (Exception)
            {
            }
        }
        public void clearRoute()
        {
            txtRouteName.Text = "";
            txtRouteName.Focus();
            routemaxid();
            btnRouteSave.Text = "Save";
        }
        public void clearVillage()
        {
            txtVillageName.Text = "";
            txtVillageName.Focus();
            cmbRouteLoad();
            villagemaxid();
            btnVillageSave.Text = "Save";
        }
        public void routemaxid()
        {
            try
            {
                RouteRepository rRepo = new RouteRepository();
                var mid = rRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.RouteId);
                if (mid == null)
                    txtRouteId.Text = "1";
                else
                    txtRouteId.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
            }
            catch (Exception)
            { }
        }
        public void villagemaxid()
        {
            try
            {
                VillageRepository rRepo = new VillageRepository();
                var mid = rRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.VillageId);
                if (mid == null)
                    txtVillageId.Text = "1";
                else
                    txtVillageId.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
            }
            catch (Exception)
            { }
        }
        private void btnRouteSave_Click(object sender, EventArgs e)
        {
            try
            {
                RouteRepository routeRepo = new RouteRepository();
                var routeData = routeRepo.GetAll().Where(t => t.RouteName.ToLower().Trim() == txtRouteName.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (routeData != null)
                {
                    MessageBox.Show("This Route Name Already Present.", "Warning");
                    clearRoute();
                    this.ActiveControl = txtRouteName;
                }
                else
                {
                    if (txtRouteName.Text.Trim() != "")
                    {
                        if (btnRouteSave.Text == "Update")
                        {
                            RouteRepository rRepo = new RouteRepository();
                            tblRouteInfo routeinfo = new tblRouteInfo();
                            routeinfo.RouteId = Convert.ToInt32(txtRouteId.Text);
                            routeinfo.RouteName = txtRouteName.Text;
                            routeinfo.Id = routeId;
                            routeinfo.CompId = CommonMethod.CompId;
                            rRepo.Edit(routeinfo);
                            rRepo.Save();

                            MessageBox.Show("Record Updated Sucessfully.", "Success");
                            clearRoute();
                            cmbRouteLoad();
                            bindRouteDataToGrid();
                            rRepo.Dispose();
                        }
                        else
                        {
                            RouteRepository rRepo = new RouteRepository();
                            tblRouteInfo routeinfo = new tblRouteInfo();
                            routemaxid();
                            routeinfo.RouteId = Convert.ToInt32(txtRouteId.Text);
                            routeinfo.RouteName = txtRouteName.Text;

                            routeinfo.CompId = CommonMethod.CompId;
                            rRepo.Add(routeinfo);
                            rRepo.Save();
                            MessageBox.Show("Record Saved Sucessfully.", "Success");
                            clearRoute();
                            cmbRouteLoad();
                            bindRouteDataToGrid();
                            rRepo.Dispose();
                        }
                    }
                    else
                    {
                        if (txtRouteName.Text == "")
                        {
                            MessageBox.Show("Enter Route Name.", "Warning");
                            this.ActiveControl = txtRouteName;
                        }
                    }
                }

            }
            catch (Exception)
            { }
        }

        private void btnVillageSave_Click(object sender, EventArgs e)
        {
            try
            {
                VillageRepository villageRepo = new VillageRepository();
                var villageData = villageRepo.GetAll().Where(t => t.VillageName.ToLower().Trim() == txtRouteName.Text.ToLower().Trim() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (villageData != null)
                {
                    MessageBox.Show("This Village Name Already Present.", "Warning");
                    clearRoute();
                    this.ActiveControl = txtVillageName;
                }
                else
                {
                    if (txtVillageName.Text.Trim() != "" && cmbRouteName.Text != "Select")
                    {
                        if (btnVillageSave.Text == "Update")
                        {
                            VillageRepository rRepo = new VillageRepository();
                            tblVillageInfo villageinfo = new tblVillageInfo();
                            villageinfo.VillageId = Convert.ToInt32(txtVillageId.Text);
                            villageinfo.VillageName = txtVillageName.Text;
                            villageinfo.RouteId = Convert.ToInt32(cmbRouteName.SelectedValue);
                            villageinfo.Id = villlageId;
                            villageinfo.CompId = CommonMethod.CompId;
                            rRepo.Edit(villageinfo);
                            rRepo.Save();

                            MessageBox.Show("Record Updated Sucessfully.", "Success");
                            clearVillage();
                            bindVillageDataToGrid();
                            rRepo.Dispose();
                        }
                        else
                        {
                            VillageRepository vRepo = new VillageRepository();
                            tblVillageInfo villageinfo = new tblVillageInfo();
                            routemaxid();
                            villageinfo.VillageId = Convert.ToInt32(txtVillageId.Text);
                            villageinfo.VillageName = txtVillageName.Text;
                            villageinfo.RouteId = Convert.ToInt32(cmbRouteName.SelectedValue);
                            villageinfo.CompId = CommonMethod.CompId;
                            vRepo.Add(villageinfo);
                            vRepo.Save();
                            MessageBox.Show("Record Saved Sucessfully.", "Success");
                            clearVillage();
                            bindVillageDataToGrid();
                            vRepo.Dispose();
                        }
                    }
                    else
                    {
                        if (txtVillageName.Text == "")
                        {
                            MessageBox.Show("Enter Village Name.", "Warning");
                            this.ActiveControl = txtVillageName;
                        }
                        else
                        {
                            MessageBox.Show("First Select Route", "Warning");
                            this.ActiveControl = cmbRouteName;
                        }
                    }
                }

            }
            catch (Exception)
            { }
        }

        private void grdRoute_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                routeId = Convert.ToInt32(grdRoute.Rows[e.RowIndex].Cells["PID"].Value);
                txtRouteId.Text = Convert.ToString(grdRoute.Rows[e.RowIndex].Cells["PRouteID"].Value);
                txtRouteName.Text = Convert.ToString(grdRoute.Rows[e.RowIndex].Cells["RouteName"].Value);
             
                btnRouteSave.Text = "Update";
                btnRouteDelete.Enabled = true;
                this.ActiveControl = txtRouteName;
            }
            catch (Exception)
            { }
        }

        private void grdVillage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                villlageId = Convert.ToInt32(grdVillage.Rows[e.RowIndex].Cells["ID"].Value);
                txtVillageId.Text = Convert.ToString(grdVillage.Rows[e.RowIndex].Cells["VillageID"].Value);
                txtVillageName.Text = Convert.ToString(grdVillage.Rows[e.RowIndex].Cells["VillageName"].Value);
                cmbRouteName.Text = Convert.ToString(grdVillage.Rows[e.RowIndex].Cells["RouteNameGrd"].Value);
               
                btnVillageSave.Text = "Update";
                btnVillageDelete.Enabled = true;
                this.ActiveControl = txtVillageName;
            }
            catch (Exception)
            { }
        }
    }
}
