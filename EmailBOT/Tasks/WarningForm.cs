using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT.Tasks
{
    public partial class WarningForm : Form
    {
        //private static extern IntPtr CreateRoundRectRgn(
        //    int nLeftRect,
        //    int nTopRect,
        //    int nRightRect,
        //    int nBottomRect,
        //    int nWidthEllipse,
        //    int nHeightEllipse
        //    );
        public WarningForm()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int sec = 120;
        private void timer1_Tick(object sender, EventArgs e)
        { 
            label1.Text = String.Format("Software will be closed after {0} second",sec.ToString());
            if(sec<=0)
            {
                Application.Exit();
            }
            sec--;
        }

        private void WarningForm_Load(object sender, EventArgs e)
        {
           // Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
    }
}
