using EmailBOT.Class;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms; 

namespace EmailBOT
{
    public partial class AddSenderCredential : Form
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
        string dataId = "";
        public AddSenderCredential()
        {
            InitializeComponent();
            populate();
        } 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveOrUpdateData();
        }
        private bool isValidate()
        {
            if (txtSettingsName.Text == string.Empty)
            {
                MessageBox.Show("Set a settings name.");
                return false;
            }
            else if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Sender id required.");
                return false;
            }
            else if (lblCredential.Text == string.Empty)
            {
                MessageBox.Show("Select credential.");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void IsAlreadyExist()
        {
            datas = DataList.GetData(txtSettingsName.Text);
            if (datas != null)
            {
                strAction = "update";
            }
            else
            {
                strAction = "insert";
            }

        }
        public void SaveOrUpdateData()
        {
            if (isValidate())
            {
                //IsAlreadyExist();
                SaveOrUpdateAction();
                if (!bResult)
                {
                    MessageBox.Show(strExp, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(strExp, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void SaveOrUpdateAction()
        {
            string code=Guid.NewGuid().ToString();
            datas = new Datas();
            datas.Code = code;
            datas.SettingsName = txtSettingsName.Text;
            datas.Email = txtEmail.Text;
            datas.Name = txtName.Text;
            datas.Credential =BaseClass.Encrypt(lblCredential.Text,"ArtMailerV2147");
            if (btnAdd.Text=="Add")
            {
                try
                {
                    DataList.InsertData(datas);
                    bResult = true;
                    strExp = "Record " + datas.Email.Trim() + " successfully insert to datasource";
                }
                catch (Exception exp)
                {
                    bResult = false;
                    strExp = "Record " + datas.Email.Trim() + " failed insert to datasource\n Message : " + exp.Message;
                }
            }
            else
            {
                try
                {
                    DataList.UpdateData(datas);
                    bResult = true;
                    strExp = "Record " + datas.Email.Trim() + " successfully update to datasource";
                }
                catch (Exception exp)
                {
                    bResult = false;
                    strExp = "Record " + datas.Email.Trim() + " failed update to datasource\n Message : " + exp.Message;
                }
                clearForm();
                txtEmail.Focus();
            }
            populate();
        }
        void populate()
        {
            XMLDataLoadToText();
            IList list = DataList.GetDataList();
            dgvList.DataSource = list;
            dgvList.Columns[0].Visible = false;
        }
        private static Random random = new Random();
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void XMLDataLoadToText()
        {
            try
            {
                AutoCompleteStringCollection coll1 = new AutoCompleteStringCollection(); 
                DataSet ds = new DataSet();
                string xmlPath= Application.StartupPath + "\\XML\\data.xml";
                bool checkPath = System.IO.File.Exists(xmlPath);
                if (!checkPath)
                    return;
                ds.ReadXml(xmlPath);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        coll1.Add(dt.Rows[i]["settingsName"].ToString()); 
                    } 
                    txtSettingsName.AutoCompleteCustomSource = coll1;
                    txtSettingsName.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtSettingsName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
            catch
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        public void DeleteData()
        {
            try
            {
                DialogResult rslt = MessageBox.Show("Are sure want to delete record no : " +
                    txtEmail.Text + " ?", "Confirmation", MessageBoxButtons.YesNo);
                if (rslt == DialogResult.Yes)
                { 
                    DataList.DeleteData(dataId);
                    bResult = true;
                    btnDelete.Visible = false;
                }
            }
            catch (Exception exp)
            {
                bResult = false;
                strExp = "Record " + txtEmail.Text.Trim() + " failed delete to datasource\n Message : " + exp.Message;
                MessageBox.Show(strExp, "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            populate();
            clearForm();
        }
        private void clearForm()
        {
            dataId = "";
            btnAdd.Text = "Add"; 
            txtEmail.Text = "";
            txtName.Text = "";
            lblCredential.Text = "";
            btnDelete.Visible = false; 
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        { 
            dataId= dgvList.Rows[e.RowIndex].Cells["code"].Value.ToString();
            txtSettingsName.Text = dgvList.Rows[e.RowIndex].Cells["settingsName"].Value.ToString();
            txtEmail.Text = dgvList.Rows[e.RowIndex].Cells["email"].Value.ToString();
            txtName.Text = dgvList.Rows[e.RowIndex].Cells["name"].Value.ToString();
            lblCredential.Text = BaseClass.Decrypt(dgvList.Rows[e.RowIndex].Cells["credential"].Value.ToString(), "ArtMailerV2147");
            btnAdd.Text = "Update";  
            //File.WriteAllText(@"credential_123.json", lblCredential.Text);
        } 
        private void SelectCredential_Click(object sender, EventArgs e)
        {
            string filename = "";
            var loadDialog = new OpenFileDialog { Filter = "Json File|*.json" };
            if (loadDialog.ShowDialog() == DialogResult.OK)
                filename = loadDialog.FileName;  

            String filetext = File.ReadAllText(filename);

            lblCredential.Text = filetext; 
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSettingsName.Text = "";
            clearForm();
            txtSettingsName.Focus();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            new multipleSenderSettings().ShowDialog();
        }
    }
}
