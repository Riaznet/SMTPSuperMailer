using EmailBOT.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT
{
    public partial class ActiveLicense : Form
    {
        #region //Border shadow and moving by mouse


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
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
        protected override void WndProc(ref Message m)
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
        public ActiveLicense()
        {
            BaseClass.Execute($"Delete from sys_table_dont_changeAnything");
            Properties.Settings.Default.Keys = null;
            Properties.Settings.Default.Save();
            InitializeComponent();
            txtEmail.Focus();
        }
        int tryActive = 0;
        private void btnActive_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Email address required", "Email Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else if (!BaseClass.IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Invalid email address", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else if (txtKey.Text == "")
            {
                MessageBox.Show("Invalid Key", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtKey.Focus();
            }
            else
            {

                // check key from server
                string key = BaseClass.GetData($"Select LicenseKey  from sys_license where LicenseKey='{txtKey.Text}' and status=1 and isnull(email,'')=''", Connection.OnlineConnection);
                if (key == "")
                {
                    if (tryActive >= 5)
                    {
                        MessageBox.Show("User Blocked", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Application.Exit();
                    }
                    MessageBox.Show("Invalid Key", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtKey.Focus();
                    tryActive++;
                }
                else
                {
                    string keys = BaseClass.EncryptHash(key) + BaseClass.Encrypt(key, "PermissionKey123$$%#$@$%^(*&*($%!$#@%");
                    string result = BaseClass.Execute($"Insert into sys_table_dont_changeAnything (Email,License) values ('{txtEmail.Text}','{keys}')");
                    if (result == "true")
                    {
                        // save to local file
                        string keysForLocal = BaseClass.Encrypt(key, "Permsadasdas@!23$$%#$@$%^(*&*($%!$#@%");
                        Properties.Settings.Default.Keys = keysForLocal;
                        Properties.Settings.Default.Save();

                        //update email to online database
                        BaseClass.Execute($"update sys_license set email='{txtEmail.Text}' where LicenseKey='{txtKey.Text}' and status=1", Connection.OnlineConnection);

                        this.Hide();
                        new Login().Show();
                    }
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
