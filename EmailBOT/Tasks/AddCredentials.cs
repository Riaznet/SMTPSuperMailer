using EmailBOT.Class;
using EmailBOT.Class.Access;
using EmailBOT.Class.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace EmailBOT.Tasks
{
    public partial class AddCredentials : Form
    {
        #region //Border shadow and drag by mouse


        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        private static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default: break;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }
        #endregion
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        Datas datas;
        int rowId = 0;
        Service service = new Service();
        SenderData model = new SenderData();
        public AddCredentials(string[] data, string action)
        {
            InitializeComponent();
            if (action == "Edit")
            {
                rowId = Convert.ToInt32(data[0]);
                txtEmail.Text = data[1];
                txtName.Text = data[2];
                txtSubject.Text = data[3];
                txtContent.Text = data[4];
                txtHost.Text = data[5];
                txtPort.Text = data[6];
                txtUserName.Text = data[7];
                txtPassword.Text = data[8];
                btnAdd.Text = "Update";
                lblTitle.Text = "Update SMTP";
            }
            txtEmail.Focus();
            //populate();
        }
        public AddCredentials()
        {
            InitializeComponent();
            txtEmail.Focus();
            //populate();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidate())
                    return;
                model.SenderId = txtUserName.Text;
                model.Name = txtName.Text;
                model.Content = txtContent.Text;
                model.Subject = txtSubject.Text;
                model.Date = dtpDate.Text;
                model.Host = txtHost.Text;
                model.Port = Convert.ToInt32(txtPort.Text==""?"0": txtPort.Text);
                model.UserName = txtUserName.Text;
                model.Password = txtPassword.Text;
                if (btnAdd.Text == "Add")
                {
                    model.Type = "Add";
                    bool res = service.AddUpdateSenderInfo(model);
                    if (res)
                        lblMessage.Text = "SMTP Addedd.";
                    else
                        lblMessage.Text = "Failed.";
                }
                else
                {
                    model.Id = rowId;
                    model.Type = "Update";
                    bool res = service.AddUpdateSenderInfo(model);
                    if (res)
                        lblMessage.Text = "SMTP Updated.";
                    else
                        lblMessage.Text = "Failed.";
                    BaseClass.AddToDataTable(dtpDate.Text);
                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool isValidate()
        {
            try
            {
                 if (txtHost.Text == "")
                {
                    MessageBox.Show("Host required.");
                    txtHost.Focus();
                    return false;
                } 
                else if (txtUserName.Text == "")
                {
                    MessageBox.Show("User Name required.");
                    txtUserName.Focus();
                    return false;
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Password required.");
                    txtPassword.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void clearForm()
        {
            rowId = 0;
            btnAdd.Text = "Add";
            lblTitle.Text = "Add New SMTP";
            txtEmail.Text = "";
            txtName.Text = "";
            txtContent.Text = "";
            txtHost.Text=txtPort.Text =txtUserName.Text=txtPassword.Text= "";
        }

      
        private void btnReset_Click(object sender, EventArgs e)
        {
            clearForm();
            txtEmail.Focus();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            BaseClass.AddToDataTable(dtpDate.Text);
            this.Close();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtUserName.Text = txtName.Text;
        }
    }
}
