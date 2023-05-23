using EmailBOT.Class;
using EmailBOT.Class.Access;
using EmailBOT.Class.Model;
using ExcelDataReader;
using Inventory.Forms.Setup.Contact;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmailBOT.Tasks
{
    public partial class MultiTask : Form
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
            //MouseX = Cursor.Position.X - this.Left;
            //MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                //this.Top = Cursor.Position.Y - MouseY;
                //this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }
        #endregion
        BaseTask task;
        private Button currentButton;
        private Form activeForm;
        string howToUse = "https://youtu.be/VSjE0C20Trc", createAPI = "https://youtu.be/VqI_hSqoQgE", manageAPI = "";
        string id_;
        int rowIndex = 0;
        int selectedSenderId = 0;
        Service service = new Service();
        SenderData model = new SenderData();
        DateTime _todayDashboard = DateTime.Now;
        public MultiTask()
        {
            InitializeComponent();

            task = new BaseTask();
            TabControl.SelectTab(tHome);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            //CredentialCheck();
            lblVersion.Text = "Version " + BaseClass.version;
            lblUser.Text = "Welcome To " + BaseClass.name;
            string today = DateTime.Now.ToString("dd-MM-yyyy");
            BaseClass.AddToDataTable(today);
            dgvList.DataSource = BaseClass.datatable;
            TabControl.Size = new Size(770, 629);
            BaseClass.LabelStatus1 = lblStatus1;
            lblDate.Text = today;
            GetDashboardData();
            GetChartValue();
            if (BaseClass.isDemo)
                lblIsDemo.Visible = true;
            else
                lblIsDemo.Visible = false;
            //DeletePreviousData();
            //BaseClass.CheckUserValidation();// check key is valid or not 
        }
        private void DeletePreviousData()
        {
            try
            {
                DateTime date = DateTime.Now.AddDays(-7);
                BaseClass.Execute($"delete from SenderInfo where Date<'{date.ToString("dd-MM-yyyy")}'");
                BaseClass.Execute($"delete from SendEmail where Date<'{date.ToString("dd-MM-yyyy")}'");
            }
            catch
            {
            }
        }
        private void ActiveButton(object btnSender)
        {
            if (btnSender != null)
            {
                DisableButton();
                currentButton = (Button)btnSender;
                currentButton.BackColor = Color.DarkGray;
            }
        }
        private void DisableButton()
        {
            Color color = Color.FromArgb(224, 224, 224);
            btnTask1.BackColor = color;
            btnTask2.BackColor = color;
            btnTask3.BackColor = color;
            btnTask4.BackColor = color;
            btnTask5.BackColor = color;
        }
        private void btnAddTask_Click(object sender, EventArgs e)
        {
            if (BaseTask.totalProcess > 0 && BaseClass.isDemo)
            {
                MessageBox.Show("You are using demo version");
                return;
            }
            if (!pnlbtnTask1.Visible)
            {
                pnlbtnTask1.Visible = true;
                BaseTask.totalProcess++;
            }
            else if (!BaseClass.isDemo)
            {
                if (!pnlbtnTask2.Visible)
                {
                    pnlbtnTask2.Visible = true;
                    BaseTask.totalProcess++;
                }
                else if (!pnlbtnTask3.Visible)
                {
                    pnlbtnTask3.Visible = true;
                    BaseTask.totalProcess++;
                }
                else if (!pnlbtnTask4.Visible)
                {
                    pnlbtnTask4.Visible = true;
                    BaseTask.totalProcess++;
                }
                else if (!pnlbtnTask5.Visible)
                {
                    pnlbtnTask5.Visible = true;
                    BaseTask.totalProcess++;
                }
                else
                {
                    // MessageBox.Show("Won't open anymore", "Limit Over");
                }
            }
        }

        public void btnTask1_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            TabControl.SelectTab(tOne);
            int count = pnlParentTask1.Controls.Count;
            if (count == 0)
                task.OpenChildForm(pnlParentTask1, new task1(pnlbtnTask1, pnlParentTask1, TabControl, this.Tag));
        }

        private void btnTask2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            TabControl.SelectTab(tTwo);
            int count = pnlParentTask2.Controls.Count;
            if (count == 0)
                task.OpenChildForm(pnlParentTask2, new task1(pnlbtnTask2, pnlParentTask2, TabControl, this.Tag));
        }

        private void btnTask3_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            TabControl.SelectTab(tThree);
            int count = pnlParentTask3.Controls.Count;
            if (count == 0)
                task.OpenChildForm(pnlParentTask3, new task1(pnlbtnTask3, pnlParentTask3, TabControl, this.Tag));
        }

        private void btnTask4_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            TabControl.SelectTab(tFour);
            int count = pnlParentTask4.Controls.Count;
            if (count == 0)
                task.OpenChildForm(pnlParentTask4, new task1(pnlbtnTask4, pnlParentTask4, TabControl, this.Tag));
        }

        private void btnTask5_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);
            TabControl.SelectTab(tFive);
            int count = pnlParentTask5.Controls.Count;
            if (count == 0)
                task.OpenChildForm(pnlParentTask5, new task1(pnlbtnTask5, pnlParentTask5, TabControl, this.Tag));
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit application ?", "Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            BaseClass.Execute($"Delete from LoginActivity where userid = '{BaseClass.UserId}'", Connection.OnlineConnection);
            Application.Exit();
        }

        private void viewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddCredentials(data, "Edit").ShowDialog();
            dgvList.DataSource = BaseClass.datatable;
        }

        private void closeTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteData();
            dgvList.DataSource = BaseClass.datatable;
            if (rowIndex == 0)
                return;
            dgvList.Rows[rowIndex - 1].Selected = true;
            dgvList.FirstDisplayedScrollingRowIndex = dgvList.SelectedRows[0].Index;
        }
        private string DeleteById()
        {
            return BaseClass.Execute($"Delete from senderInfo where id='{id_}'");
        }
        public void DeleteData()
        {
            try
            {
                DialogResult rslt = MessageBox.Show("Are sure want to delete record?", "Confirmation", MessageBoxButtons.YesNo);
                string result = DeleteById();
                if (result == "true")
                {
                    dgvList.Rows.RemoveAt(rowIndex);
                    BaseClass.AddToDataTable(dtpDate.Text);
                }
                else
                {
                    MessageBox.Show("Delete failed.");
                }
            }
            catch (Exception exp)
            {

            }
            //populate(); 
        }
        static string[] data = new string[9];
        private void dgvList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && e.RowIndex != -1)
                {
                    dgvList.ClearSelection();
                    dgvList.Rows[e.RowIndex].Selected = true;
                    rowIndex = e.RowIndex;
                    id_ = dgvList.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                    data[0] = id_;
                    data[1] = dgvList.Rows[e.RowIndex].Cells["SenderId"].Value.ToString();
                    data[2] = dgvList.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    data[3] = dgvList.Rows[e.RowIndex].Cells["Subjects"].Value.ToString();
                    data[4] = dgvList.Rows[e.RowIndex].Cells["Contents"].Value.ToString();
                    data[5] = dgvList.Rows[e.RowIndex].Cells["Hosts"].Value.ToString();
                    data[6] = dgvList.Rows[e.RowIndex].Cells["Ports"].Value.ToString();
                    data[7] = dgvList.Rows[e.RowIndex].Cells["UserNames"].Value.ToString();
                    data[8] = dgvList.Rows[e.RowIndex].Cells["Passwords"].Value.ToString();

                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(tOutPut);
        }

        private void btnAddNewCredential_Click(object sender, EventArgs e)
        {
            var sds = new AddCredentials().ShowDialog();
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangePassword().ShowDialog();
        }

        private void addCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddCredentials().ShowDialog();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            BaseClass.Execute($"Delete from LoginActivity where userid = '{BaseClass.UserId}'", Connection.OnlineConnection);
            new Login().Show();
            this.Close();
        }

        private void lblMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnReloadData_Click(object sender, EventArgs e)
        {
            BaseClass.AddToDataTable(dtpDate.Text);
            dgvList.DataSource = BaseClass.datatable;
        }

        private void txtGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TextFileGenerate().Show();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(tabPage1);
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "Excel Workbook(*.xls;*.xlsx)|*.xls;*.xlsx"
                })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        TxtFileName.Text = openFileDialog.FileName;
                        using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                        {
                            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                                });
                                if (result.Tables[0] != null)
                                {
                                    dgvUploadSender.DataSource = result.Tables[0];
                                    lblTotalRows.Text = "Total Rows : " + dgvUploadSender.Rows.Count.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblSelectCredential_Click(object sender, EventArgs e)
        {
            int countRow = dgvUploadSender.Rows.Count;
            if (countRow > 0)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        int i = 0;
                        string[] files = Directory.GetFiles(fbd.SelectedPath, "*.json");
                        foreach (var filepath in files)
                        {
                            string txt = File.ReadAllText(filepath);

                            dgvUploadSender.Rows[i].Cells["Credential"].Value = txt;
                            i++;
                            if (countRow == i)
                                break;
                        }
                    }
                }

            }
        }
        private void LoadingStart()
        {
            Invoke(new Action(() =>
            {
                lblUploadToServer.Text = "Uploading...";
            }));
        }
        private void LoadingStop()
        {
            Invoke(new Action(() =>
            {
                lblUploadToServer.Text = "Upload Data";
            }));
        }
        private void lblUploadToServer_Click(object sender, EventArgs e)
        {
            try
            {
                new Thread(new ThreadStart(LoadingStart)).Start();
                new Thread(new ThreadStart(AddSenderInformation)).Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddSenderInformation()
        {
            try
            {
                foreach (DataGridViewRow row in dgvUploadSender.Rows)
                {
                    string email = row.Cells["Email"].Value.ToString();
                    string Name = row.Cells["Names"].Value.ToString();
                    string Content = row.Cells["Content"].Value.ToString();
                    string Subject = row.Cells["Subject"].Value.ToString();
                    string host = row.Cells["Host"].Value.ToString();
                    string port = "0";
                    string userName = row.Cells["UserName"].Value.ToString();
                    string password = row.Cells["Password"].Value.ToString(); 
                    model.SenderId = email;
                    model.Name = Name;
                    model.Content = Content;
                    model.Subject = Subject;
                    model.Host = host;
                    model.Port = Convert.ToInt32(port);
                    model.UserName = userName;
                    model.Password = password;
                    model.Date = dtpDate.Text;
                    model.Type = "Add";
                    service.AddUpdateSenderInfo(model);
                }
                BaseClass.AddToDataTable(dateTimePicker1.Text);
                MessageBox.Show("Successfully Uploaded.");
                new Thread(new ThreadStart(LoadingStop)).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                new Thread(new ThreadStart(LoadingStop)).Start();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            BaseClass.AddToDataTable(dtpDate.Text);
            dgvList.DataSource = BaseClass.datatable;
        }

        private void errorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(tError);
        }
        private void Delay(int val)
        {
            val = (val) * 200;
            var t = Task.Run(async delegate
            {
                await Task.Delay(val);
                return 60;
            });
            t.Wait();
        }


        private void btnErrorClear_Click(object sender, EventArgs e)
        {
            txtError.Text = "";
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (howToUse != "")
                Process.Start(howToUse);
        }

        private void lblNextDay_Click(object sender, EventArgs e)
        {
            _todayDashboard = _todayDashboard.AddDays(1);
            lblDate.Text = _todayDashboard.ToString("dd-MM-yyyy");
            GetDashboardData();
        }

        private void lblPreviousDay_Click(object sender, EventArgs e)
        {
            _todayDashboard = _todayDashboard.AddDays(-1);
            lblDate.Text = _todayDashboard.ToString("dd-MM-yyyy");
            GetDashboardData();
        }

        private void howToCreateAPIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (createAPI != "")
                Process.Start(createAPI);
        }

        private void lblDownloadFormat_Click(object sender, EventArgs e)
        {
            CreateExcelFile();
        }

        private void GetDashboardData()
        {
            var data = BaseClass.DataReaderAdd($@"Select Sum(totalSender),sum(totalMail),sum(totalSent),sum(totalFailed) from (
Select count(*)totalSender,0 totalMail,0 totalSent,0 totalFailed from SenderInfo where date like '{lblDate.Text}%'
UNION
Select 0 totalSender,sum(totalmail) totalMail,sum(totalsent) totalSent,sum(totalfailed) totalFailed from SendEmail where date like '{lblDate.Text}%'
) A");
            if (data.Count > 0)
            {
                lblTotalSender.Text = data[0];
                lblTotalMail.Text = data[1];
                lblTotalSent.Text = data[2];
                lblTotalFailed.Text = data[3];
            }
        }
        private void btnSettings_MouseDown(object sender, MouseEventArgs e)
        {
            contextOptions.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void GetChartValue()
        {
            DateTime date = DateTime.Now.AddDays(-7);
            //chart.Invalidate(); 
            // Clear the existing data
            chart.Series.Clear();
            chart.Legends.Clear();
            // Add new data 
            chart.Series.Add("Total Mail");
            chart.Series.Add("Total Sent");
            chart.Series.Add("Total Failed");

            for (int i = 0; i < 7; i++)
            {
                date = date.AddDays(1);
                string dt = date.ToString("dd-MM-yyyy");
                //get date wise data
                var summary = BaseClass.DataReaderAdd($@"Select IFNULL(sum(totalmail),0) totalMail,IFNULL(sum(totalsent),0) totalSent,IFNULL(sum(totalfailed),0) totalFailed from SendEmail where date like '{dt}%'");
                if (summary.Count > 0)
                {
                    chart.Series["Total Mail"].Points.AddXY(dt, Convert.ToInt16(summary[0]));
                    chart.Series["Total Sent"].Points.AddXY(dt, Convert.ToInt16(summary[1]));
                    chart.Series["Total Failed"].Points.AddXY(dt, Convert.ToInt16(summary[2]));
                }
                else
                {
                    chart.Series[1].Points.AddXY(dt, 0);
                    chart.Series[2].Points.AddXY(dt, 0);
                    chart.Series[3].Points.AddXY(dt, 0);
                }
            }
        }
        private void lblDownloadFormat_Click_1(object sender, EventArgs e)
        {
            CreateExcelFile();
        }

        private void lblHelp_MouseDown(object sender, MouseEventArgs e)
        {
            contextHelp.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            GetDashboardData();
            GetChartValue();
            TabControl.SelectTab(tHome);
        }

        private void ValidKeyChecker_Tick(object sender, EventArgs e)
        {
            try
            {
                new Thread(new ThreadStart(CheckValidUser)).Start();
                
            }
            catch
            {

            }
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
        }

        private void btnAllUploadedDelete_Click(object sender, EventArgs e)
        {
            string result = BaseClass.Execute("Delete from SenderInfo");
            if (result == "true")
            {
                MessageBox.Show("Deleted all uploaded data", "Delete", MessageBoxButtons.OK, MessageBoxIcon.None);
                BaseClass.AddToDataTable(dtpDate.Text);
                dgvList.DataSource = BaseClass.datatable;
            }
        }

        // set excel uploaded data to this table
        DataTable dataTable = new DataTable();
        private void CreateExcelFile() // generate dynamic excel by using Microsoft.Office.Interop.Excel library
        {
            Invoke(new Action(() =>
            {
                lblDownloadFormat.Text = "Generating...";
            }));
            string path = "";
            using (var saveDialog = new SaveFileDialog())
            {
                string datetime = DateTime.Now.ToString("dd_MM_yyyy_hhmmtt");
                saveDialog.Filter = "Excel Files|*.xlsx;*.xls";
                saveDialog.FileName = $"SenderInfoExcel_Format_{datetime}.xlsx";
                DialogResult result = saveDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveDialog.FileName))
                {
                    path = saveDialog.FileName;
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        lblDownloadFormat.Text = "Generate Excel Format";
                    }));
                    return;
                }
            }

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Add(1);

            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int column = 2;
            int row = 1;
            
            xlWorksheet.Cells[1, 1] = "Name";
            xlWorksheet.Cells[2, 1] = "Display Name";

            xlWorksheet.Cells[1, 2] = "Subject";
            xlWorksheet.Cells[2, 2] = "Email Subject";

            xlWorksheet.Cells[1, 3] = "Content";
            xlWorksheet.Cells[2, 3] = "Email Body Message";

            xlWorksheet.Cells[1, 4] = "Host";
            xlWorksheet.Cells[2, 4] = "smtp.gmail.com";
              
            xlWorksheet.Cells[1, 5] = "UserName";
            xlWorksheet.Cells[2, 5] = "SenderMailId@gmail.com";

            xlWorksheet.Cells[1, 6] = "Password";
            xlWorksheet.Cells[2, 6] = "xasetthgfxyxasd";
             
            xlApp.ActiveWorkbook.SaveAs(path);
            xlWorkbook.Close();
            xlApp.Quit();
            Invoke(new Action(() =>
            {
                lblDownloadFormat.Text = "Generate Excel Format";
                MessageBox.Show("Excel File Generated");
            }));

        }
        int process = 0;
        int screenWidth = 2024;
        int screenHeight = 850;
        Thread th = null;
        int activeValue = 0;
        int incrementVal = 0;
        private void sssTimer_Tick(object sender, EventArgs e)
        {
            //incrementVal++;
            if (process == 0)
            {
                if (incrementVal % 2 == 0)
                {
                    th = new Thread(new ThreadStart(CaptureMyScreen));
                    th.Start();
                }

                if (IsSystemActive())
                {
                    activeValue = 0;
                }
                activeValue++;
                if (activeValue == 20)
                {
                    new WarningForm().ShowDialog();
                }
                if (incrementVal > 100)
                    incrementVal = 0;
                incrementVal++;
            }
        }
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }
        public static bool IsSystemActive()
        {
            int inactivityTime = 60000; // 1 minute
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTime = lastInputInfo.dwTime;
                uint currentTime = (uint)Environment.TickCount;
                uint idleTime = currentTime - lastInputTime;
                if (idleTime > inactivityTime)
                {
                    // user is inactive
                    // add your code here
                    return false;
                }
                else
                {
                    // user is active
                    // add your code here
                    return true;
                }
            }
            return false;
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
