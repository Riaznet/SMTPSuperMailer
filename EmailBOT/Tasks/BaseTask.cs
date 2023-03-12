using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT.Tasks
{
    internal class BaseTask
    {
        public static Button process = null;
        public static int totalProcess = 0;
        public void OpenChildForm(Panel Parent, Form childForm)
        {
            Parent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            Parent.Controls.Add(childForm);
            Parent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
