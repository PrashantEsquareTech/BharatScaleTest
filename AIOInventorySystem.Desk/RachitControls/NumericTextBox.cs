using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.RachitControls
{
    public partial class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            InitializeComponent();
            this.Width = 80;
            this.Height = 20;
            this.Name = "NumericTextBox";
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Leave += new EventHandler(NumericTextBox_Leave);
            this.TextChanged += new EventHandler(NumericTextBox_TextChanged);
            this.KeyPress += new KeyPressEventHandler(NumericTextBox_KeyPress);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i > 47 && i <= 57) || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void NumericTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void NumericTextBox_Leave(object sender, EventArgs e)
        {

        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i > 47 && i <= 57) || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }
    }
}
