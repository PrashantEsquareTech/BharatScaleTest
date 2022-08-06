using System;
using System.Windows.Forms;
using AIOInventorySystem.Desk.Forms;

namespace AIOInventorySystem.Desk
{
    public class LoginAppContext : ApplicationContext
    {
        public string username;
        public LoginAppContext(Form loginForm)
            : base(loginForm)
        {
            //(SForm)mainForm m=new (SForm)mainForm(); 
        }

        protected override void OnMainFormClosed(object sender, EventArgs e)
        {
            if (sender is frmloginForm)
            {
                var frmLogin = (frmloginForm)sender;
                if (frmLogin.ApplicationFlag)
                {
                    MDINewForm app = new MDINewForm();
                    base.MainForm = app;
                    base.MainForm.Show();
                }
                else
                {
                    base.OnMainFormClosed(sender, e);
                }
            }
            else if (sender is frmloginForm)
            {
                base.OnMainFormClosed(sender, e);
            }
        }
    }
}
