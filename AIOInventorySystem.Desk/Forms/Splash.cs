using System;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Forms;

namespace AIOInventorySystem.Desk
{
    public partial class Splash : Form
    {
        int i = 0, j = 0;

        public Splash()
        {
            InitializeComponent();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            lblVersion.Text = AssemblyVersion;
            if (bwActivationCheck.IsBusy != true)
            {
                // Start the asynchronous operation.
                bwActivationCheck.RunWorkerAsync();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i+=2;
            DisplayText();
            DisplayDot();
            j++;

            if (i > 100) // Progress Completed
            {
                this.Hide();
                timer1.Stop();
                frmloginForm l = new frmloginForm();
                l.Show();                
            }
            else // Continue Progress
            {
                if (i <= 100)
                    prgbar.Value = i;
            }
        }

        public void DisplayText()
        {
            if (i < 10)
                lblDisplay.Text = "Initinalizing Software";
            else if (i > 11 && i < 50)
                lblDisplay.Text = "Checking Software Dependancies";
            else if (i > 51 && i < 60)
                lblDisplay.Text = "Checking Data Status";
            else if (i > 61 && i < 100)
                lblDisplay.Text = "Loading For Use";
            else if (i > 101)
                lblDisplay.Text = "Optimizing Display";
        }

        public void DisplayDot()
        {
            if (j == 0)
                lblDisplay.Text += ".";
            else if (j == 1)
                lblDisplay.Text += "..";
            else if (j == 2)
                lblDisplay.Text += "...";
            else if (j == 3)
                lblDisplay.Text += "....";
            else if (j == 4)
                lblDisplay.Text += ".....";
            else if (j == 5)
                lblDisplay.Text += "......";
            else if (j == 6)
                lblDisplay.Text += ".......";
            else if (j == 7)
                lblDisplay.Text += "........";
            else if (j == 8)
                lblDisplay.Text += ".........";
            else if (j == 9)
                lblDisplay.Text += "..........";
            else if (j == 10)
                lblDisplay.Text += "...........";
            else
                j = 0;
        }

        public string AssemblyVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version Deploy = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Version {0}.{1}.{2}.{3}", Deploy.Major, Deploy.Minor, Deploy.Build, Deploy.Revision);
                }
                else
                {
                    string versionDeploy = Application.ProductVersion;
                    //versionDeploy.Replace("1.0.0.0", "1.0.0.1");
                    //versionDeploy.Split('.')[3].Replace(
                    return string.Format("Version {0}", versionDeploy);
                }
            }
        }

        private void bwActivationCheck_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
    }
}
