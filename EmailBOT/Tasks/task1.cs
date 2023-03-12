using GmailAPI.APIHelper;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Gmail.v1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Net.Mail;
using Thread = System.Threading.Thread;
using System.Data.SqlClient;
using System.Net.Mime;
using EmailBOT.Class;
using EmailBOT.Class.Access;
using EmailBOT.Class.Model;
using Message = Google.Apis.Gmail.v1.Data.Message;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Net;

namespace EmailBOT.Tasks
{
    public partial class task1 : Form
    {
        private static Random random = new Random();
        DataTable tempTable = new DataTable();

        // random variable use for unique file
        // identify
        string randomImageName = "", randomPdfName = "",
        randomAlphaNumeric = "", randomNumber = "", randomLetter = "", randomInvoice = "";
        string pdfPath = "";
        string taskName = "";

        int totalMail = 0;//count total mail
        Thread th;

        Panel panelBtn;
        Panel parent;
        TabControl tControl;

        static string tag = "";
        bool isMultipleSender = false;
        Service service = new Service();// add data by this class
        SentEmail model = new SentEmail();
        string copyToClipboard = "";

        //SMTP Information
        int port = 0;
        static string host = "", userName = "", password = "";
        public task1(Panel panel_, Panel parent_, TabControl tControl_, object tag_)
        {
            InitializeComponent();
            panelBtn = panel_;
            // get task button infomration . for status change
            taskName = panelBtn.Name.Substring(6, 5);
            //each task bind a panel which called parent
            parent = parent_;
            tControl = tControl_;
            //lblWelcome.Text = "Welcome To " + BaseClass.name;
            tempTable.Columns.Add("Email", typeof(string));
            tag = tag_.ToString();
            this.Tag = tag + panel_.Tag;
            //CredentialCheck(); 

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnImporttxtCsv.Focus();
        }
        string logFile = "";
        private void btnImporttxtCsv_Click(object sender, EventArgs e)
        {
            try
            {
                string today = DateTime.Now.ToString("dd_MM_yyyy");
                lblMsg.Text = "";
                string filename = "";
                var loadDialog = new OpenFileDialog { Filter = "File|*.txt", InitialDirectory = Application.StartupPath + "/output" };

                //loadDialog.AutoUpgradeEnabled = false;
                if (loadDialog.ShowDialog() == DialogResult.OK)
                {
                    filename = loadDialog.FileName;
                    logFile = "output/" + today + "_" + loadDialog.SafeFileName;
                    BaseClass.CopyFile(filename, logFile);

                    txtImportEmailListName.Text = loadDialog.SafeFileName;
                }
                else
                    return;
                tempTable.Rows.Clear();
                string[] lines = File.ReadAllLines(filename);
                string values;
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].ToString().Replace(" ", "");
                    // valid mail

                    if (BaseClass.IsValidEmail(values))
                        tempTable.Rows.Add(values);
                }
                changeFileName = BaseClass.RenameFile("Selected", loadDialog.FileName);
                dgvEmailList.DataSource = tempTable;
                lblTotalSentMail.Text = String.Format("Total mail sent 0 of {0}", dgvEmailList.Rows.Count);
                btnSelectSender.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CheckActivity()
        {
            BaseClass.isActive = true;
            string active = BaseClass.GetData($"Select id from Registration where id='{BaseClass.UserId}' and status=1", Connection.OnlineConnection);
            if (active == "")
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Invalid User Account.");
                    th.Abort();
                    this.Hide();
                    new Login().Show();
                }));

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
        private void InsertSenderInto()
        {
            try
            {

                string macAddress = MacAddress();
                int totalSend = dgvEmailList.Rows.Count;
                if (totalSend < 1)
                    return;
                Invoke(new Action(() =>
                {
                    if (txtAppName.Text == "" || txtFromMail.Text == "")
                        return;
                    // variable 
                    if (chkAttachment.Checked && attachment == "")
                    {
                        //lblMsg.Text = "Attachment required when selected html to pdf as attachment";
                        txtMessage.Focus();
                        return;
                    }
                    if (chkHtmlToImageToPdf.Checked && txtMessageHtml.Text == "")
                    {
                        //lblMsg.Text = "Html Resource required.";
                        txtMessageHtml.Focus();
                        return;
                    }
                    if (!File.Exists(credential_) && !isMultipleSender)
                    {
                        //MessageBox.Show("Credential folder is empty", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }));
                BaseClass.Execute($"Insert Into SentMail (SenderName,TotalSent,ExecutedAt,UserId) values ('{macAddress}','{totalSend}','{DateTime.Now}','{BaseClass.UserId}')", Connection.OnlineConnection);
            }
            catch
            {

            }
        }
        bool invalidHtml = true;
        private void btnSendEnail_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                if (th != null && th.IsAlive)
                    th.Abort();
                new Thread(new ThreadStart(CheckActivity)).Start();
                th = new Thread(new ThreadStart(SendBulkMessage));
                th.Start();
                new Thread(new ThreadStart(InsertSenderInto)).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClearGridStatus()// clear data gridview status
        {
            try
            {
                foreach (DataGridViewRow row in dgvEmailList.Rows)
                {
                    row.Cells["btn"].Value = "";
                }
            }
            catch
            {

            }
        }
        private void GenerateRandomData()
        {
            randomImageName = BaseClass.RandomString(15) + uniqueId;
            randomPdfName = BaseClass.RandomString(15) + uniqueId;
            randomAlphaNumeric = BaseClass.RandomString(15) + uniqueId;
            randomNumber = BaseClass.RandomString(12, "num") + uniqueId;
            randomLetter = BaseClass.RandomString(15, "alp");
            // this is random invoice generator like INV/WEP/123312
            randomInvoice = "INV/" + randomLetter.Substring(0, 3) + "/" + BaseClass.RandomString(6, "num");
        }
        private string ReplaceData(string str)
        {
            str = str.Replace("#NUMBER#", randomNumber);
            str = str.Replace("#LETTERS#", randomLetter);
            str = str.Replace("#RANDOM#", randomAlphaNumeric);
            str = str.Replace("#INVOICE#", randomInvoice);
            str = str.Replace("#EMAIL#", emailTo);
            return str;
        }
        private string ReplaceDataSub(string str)
        {
            try
            {
                str = str.Replace("#NUMBER#", randomNumber.Substring(0, 6));
                str = str.Replace("#LETTERS#", randomLetter.Substring(0, 6));
                str = str.Replace("#RANDOM#", randomAlphaNumeric.Substring(0, 6));
                str = str.Replace("#INVOICE#", randomInvoice);
                return str;
            }
            catch
            {
                return str;
            }

        }

        string emailTo = "", attachment = "", spamFilteringkeyMessage = "";
        int uniqueId = 0;
        string[] Scopes = { GmailService.Scope.GmailSend };
        string changeFileName = "";
        string senderId = "";
        string displayName = "", subjects = "";
        string credential_ = "";

        static List<EmailList> SenderList { get; set; }
        // change sender one by one when sent specific number 
        int changeSenderOneByOne = 0;
        int changeSenderId = 2;
        int sentFromPerSender = 0;
        string messages = "";
        int sendLimitFromPerSender = 0;//set sender limit from sender selection panel
        private void SendBulkMessage()
        {
            // clear data gridview status
            ClearGridStatus();
            //this are private variable
            string messagesHtml = "";
            bool randomContent = false, randomHtml = false;
            bool checkHtmlToPdf = false, checkAttachment = false;
            //this invoke used for thread. in thread process txtbox control does not work that's why use this step
            Invoke(new Action(() =>
            {
                messages = txtMessage.Text;
                messagesHtml = txtMessageHtml.Text;
                subjects = txtSubject.Text;
                displayName = txtAppName.Text;
                senderId = txtFromMail.Text;
                attachment = txtAttachment.Text;
                credential_ = txtCredentialPath.Text;
                checkHtmlToPdf = chkPdf.Checked;
                checkAttachment = chkAttachment.Checked;
                randomContent = chkRandomContent.Checked;
                randomHtml = chkRandomHtml.Checked;

            }));// variable
            int sentMail = 0, rowIndex = 0, delayCount = 0;
            totalMail = dgvEmailList.Rows.Count;

            #region Condition check
            if (totalMail == 0)
            {
                Invoke(new Action(() =>
                {
                    lblMsg.Text = "Import mail list.";
                    btnImporttxtCsv.Focus();
                }));
                return;
            }
            else
            {
                Invoke(new Action(() =>
                {
                    lblTotalSentMail.Text = "Total Sent 0 of " + totalMail;
                }));
            }
            if (senderId == "")
            {
                Invoke(new Action(() =>
                {
                    lblMsg.Text = "Sender Email is empty.";
                    txtFromMail.Focus();
                }));
                return;
            }
            if (checkAttachment && attachment == "")
            {
                Invoke(new Action(() =>
                {
                    lblMsg.Text = "Attachment required when selected html to pdf as attachment";
                    txtMessage.Focus();
                }));
                return;
            }
            if (checkHtmlToPdf && messagesHtml == "" && !randomHtml)
            {
                Invoke(new Action(() =>
                {
                    lblMsg.Text = "Html Resource required.";
                    txtMessageHtml.Focus();
                }));
                return;
            }
            if (!File.Exists(credential_) && !isMultipleSender)
            {
                MessageBox.Show("Credential folder is empty", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            Invoke(new Action(() =>
            {
                lblthreadAbord.Text = "✖";
                btnPauseRun.Visible = true;
            }));
            // when we generate random data and store in random variable this usinque also adding with those random variable data
            uniqueId = 0;

            // Enable and disable form control 
            EnableDisableControls(grpSendingOptions, false);

            string senderid = "";

            // Change task status
            StatusUpdate("Processing...");
             
            string multipleSender = "";
            string convertedMessage = "";

            //if (chkSentanceMaker.Checked)
            //    convertedMessage = ContentProcess(messages);
            //else
            //    convertedMessage = messages;
            // Change selected file name to processing. This is just to keep file tracking.Because of, working with bulk files
            // Such as if a txt file name is email1.txt then it will be email_Processing.txt
            changeFileName = BaseClass.RenameFile("Processing", changeFileName);

            //this variable for status of saving data . if false then dont save data else save data to database
            bool finalResult = false;
            //when send from multiple sender this variable store send qty if persender qty and this this are equal then change the sender.
            sentSenderEmail = 0;

            int removeIndex = 0;  //when select a file as import ,this selected file have copied to as log in application folder and when sending one by one ,remove this sent mail from copied log file . this variable store the index of email 
            int changeSenderDisplayName = 0;//change sender display name

            changeSenderOneByOne = 0;
            changeSenderId = (int)txtChangeSenderAfterSendThis.Value;
            sentFromPerSender = 0;
            string tempMail = "";
            copyToClipboard = "";
            string sentance = "";
            if (chkSentanceMaker.Checked)
                sentance = "<div style='margin-left: -9999px;display:none'>" + SentenceMaker.GenerateSentence() + "</div>";

            int randomChanger = Convert.ToInt16(txtRandomNameSubjectChanger.Text);
            int sentByRandomSubName = 0;

            string subjectRandom = BaseClass.GetRandomData(subjects);
            string emailDisplayNameRandom = BaseClass.GetRandomData(displayName);
            int changeRandomNameAfter10Sent = 0;
            sentSelectedMail = sendLimitFromPerSender;
            foreach (DataGridViewRow row in dgvEmailList.Rows)
            {
                while (pauseRun)// pause when press pause button
                {

                }
                if (chkGetRandomName.Checked)
                {
                    if (changeRandomNameAfter10Sent == 10)
                    {
                        changeRandomNameAfter10Sent = 0;
                        Invoke(new Action(() =>
                        {
                            BaseClass.Delay(1);
                            txtAppName.Text = BaseClass.GetFirstLastName();
                            emailDisplayNameRandom = txtAppName.Text;
                        }));

                    }
                    changeRandomNameAfter10Sent++;
                }
                else
                    emailDisplayNameRandom = txtAppName.Text;
                randomChanger = Convert.ToInt16(txtRandomNameSubjectChanger.Text);

                try
                {
                    Invoke(new Action(() =>
                    {
                        messagesHtml = txtMessageHtml.Text;
                    }));// variable 
                    //when mark this check control, content select randomly from selected folder 
                    if (sentByRandomSubName >= randomChanger && chkAllRandomChangeAfterSent.Checked)
                    {
                        if (chkRandomContent.Checked)
                        {
                            Invoke(new Action(() =>
                            {
                                string randomFilePath = BaseClass.RandomFileSelection(lblContentPath.Text);
                                messages =
                                txtMessage.Text =
                                File.ReadAllText(randomFilePath);
                                //if (chkSentanceMaker.Checked)
                                //    convertedMessage = ContentProcess(txtMessage.Text);
                            }));

                        }
                        //when mark this check control, html content select randomly from selected folder 
                        if (chkRandomHtml.Checked)
                        {
                            Invoke(new Action(() =>
                            {
                                string randomFilePath = BaseClass.RandomFileSelection(lblHtmlPath.Text);
                                messagesHtml =
                                txtMessageHtml.Text =
                                File.ReadAllText(randomFilePath);
                            }));
                        }
                    }
                    uniqueId++;
                    emailTo = row.Cells["Email"].Value.ToString();

                    Invoke(new Action(() =>
                    {

                        lblMsg.Text = "Sending to " + emailTo;
                    }));
                    //change tokern folder
                    senderid = BaseClass.SplitText(senderId, '@', 0);
                    //string tokenPath = @"token/" + rootMiddlePath + "/" + senderid;
                    string tokenPath = @"token/" + senderid;
                    // If directory does not exist, don't even try   
                    if (!Directory.Exists(tokenPath))
                        Directory.CreateDirectory(tokenPath);
                    // generate random number and set to all files rename  
                    GenerateRandomData();
                    // replace all tages by random data Such as #EMAIL# replace to receiver mail (xyz@gmail.com), #INVOICE# replace to INV/WEW/221234
                    Invoke(new Action(() =>
                    {
                        messages = ReplaceData(convertedMessage);
                    }));


                    // Set subject with commas for random selection
                    //Example: Subject1, Subject2, Subject3 
                    // get random auto generated name 
                    if (sentByRandomSubName >= randomChanger && chkAllRandomChangeAfterSent.Checked)
                    {
                        sentByRandomSubName = 0;
                        subjectRandom = BaseClass.GetRandomData(subjects);
                        emailDisplayNameRandom = BaseClass.GetRandomData(displayName);
                        BaseClass.Delay((int)txtdelayAfterRandomChange.Value);
                    }

                    sentByRandomSubName++;
                    subjectRandom = GetSubject(subjects);

                    // this statement use for proper inboxing. its hidden content
                    // spamFilteringkeyMessage = "<div style='margin-left: -9999px;display:none'>" + randomLetter +   randomAlphaNumeric + "</div>";
                    string script = @"<script>$(document).ready(function() {setTimeOut(function(){$('body').click(function() {$('div').empty(); });},1000) }); </ script > ";

                    using (var mail = new MailMessage())
                    {
                        string bodyMsg = messages;
                        if (chkPlain.Checked)
                            bodyMsg = "<pre>" + messages + "</pre>";//generate plain text  

                        spamFilteringkeyMessage = script + "<div style='margin-left: -9999px;display:none'> Subject :  " + subjectRandom + "</div>";//his statement use for proper inboxing
                        #region//now check condition that which options is active
                        if (chkPdf.Checked)
                        {
                            //convert html source to pdf 
                            BaseClass.ConvertHtmlToPdf(ReplaceData(messagesHtml), randomLetter);
                            if (!invalidHtml)
                                return;
                            string path = Path.Combine("PdfFile", randomLetter + ".pdf");
                            var attachment_ = new Attachment(path);// { Name = randomLetter + ".pdf" };
                            mail.Attachments.Add(attachment_);
                        }
                        else if (chkHtmlToImageToPdf.Checked)
                        {
                            BaseClass.ConvertHtmlToImage(ReplaceData(messagesHtml), randomImageName);// convert html to image
                            BaseClass.ConvertImageToPdf(randomLetter, randomImageName);//convert image to pdf 
                            string path = Path.Combine("PdfFile", randomLetter + ".pdf");
                            var attachment_ = new Attachment(path);// { Name = randomLetter + ".pdf" };
                            mail.Attachments.Add(attachment_);
                        }
                        else if (chkHtmlToImage.Checked)
                        {
                            BaseClass.ConvertHtmlToImage(ReplaceData(messagesHtml), randomImageName);// convert html to image
                            string messageWithImg = "<br/><img src=\"cid:id1\"></img>"; //image add to body
                            AlternateView view = AlternateView.CreateAlternateViewFromString(messageWithImg, null, MediaTypeNames.Text.Html);
                            LinkedResource resource = new LinkedResource("ImageFile/" + randomImageName + ".jpg");
                            resource.ContentId = "id1";
                            view.LinkedResources.Add(resource);
                            mail.Body = bodyMsg + spamFilteringkeyMessage;
                            mail.AlternateViews.Add(view);
                        }
                        else if (chkHtmltemplate.Checked)
                        {
                            mail.Body = bodyMsg + spamFilteringkeyMessage;
                            mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(bodyMsg + spamFilteringkeyMessage + messagesHtml, new ContentType("text/html")));
                        }
                        #endregion

                        mail.Subject = ReplaceDataSub(subjectRandom);
                        if ((!chkHtmltemplate.Checked) && (!chkHtmlToImage.Checked))
                        {
                            AlternateView alternate = AlternateView.CreateAlternateViewFromString(bodyMsg + sentance + spamFilteringkeyMessage, new ContentType("text/html"));
                            mail.AlternateViews.Add(alternate);
                        }
                        // set sender id and display name
                        mail.From = new MailAddress(emailDisplayNameRandom + "<" + senderId + ">");// sender email
                        mail.IsBodyHtml = true;
                        // if attachment is mark checked
                        if (chkAttachment.Checked)
                        {
                            if (attachment != "")
                            {
                                if (chkRandomAtthmentName.Checked)
                                    BaseClass.ChangeFileName(attachment);//change attachment name randomly
                                string[] filepathhs = Directory.GetFiles(attachment, "*");
                                foreach (var filepath in filepathhs)
                                {
                                    var attachment_ = new Attachment(filepath);
                                    mail.Attachments.Add(attachment_);
                                }
                            }
                        }

                        mail.To.Add(new MailAddress(emailTo));//client email

                        //MimeKit.MimeMessage mimeMessage = MimeKit.MimeMessage.CreateFromMailMessage(mail);
                        //Google.Apis.Gmail.v1.Data.Message message = new Google.Apis.Gmail.v1.Data.Message();
                        //message.Raw = BaseClass.Base64UrlEncode(mimeMessage.ToString());

                        // Authorize the API client using OAuth 2.0
                        //UserCredential credential;
                        //using (var stream =
                        //    new FileStream(credential_, FileMode.Open, FileAccess.Read, FileShare.Read))
                        //{
                        //    string credPath = tokenPath;
                        //    credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart2.json");
                        //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        //        GoogleClientSecrets.Load(stream).Secrets,
                        //        Scopes,
                        //        "user",
                        //        CancellationToken.None,
                        //        new FileDataStore(credPath, true)).Result;
                        //}
                        //string appName = Assembly.GetEntryAssembly().GetName().Name;
                        //// Create Gmail API service.
                        //var service = new GmailService(new BaseClientService.Initializer()
                        //{
                        //    HttpClientInitializer = credential,
                        //    ApplicationName= appName
                        //});

                        ////Send Email  
                        //var request = service.Users.Messages.Send(message, "me");
                        //var response = request.Execute();
                        bool result = false;
                        using (SmtpClient smtp = new SmtpClient())
                        {
                            smtp.Port = port;
                            smtp.Host = host;
                            //message.Headers.Add();
                            //smtp.Timeout = 30; 
                            if (chkEnableSSL.Checked)
                                smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = new NetworkCredential(userName, password);
                            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtp.Send(mail);

                            // The email was sent successfully, and the response is a Message object 
                            //update grid status
                            StatusUpdateToGrid(rowIndex, true);
                            changeSenderDisplayName++;// change sender display name after sent 50 mail
                            sentFromPerSender++;//change sender id after sent 5 mail

                            //update sender limit value
                            if (isMultipleSender)
                                UpdateSenderLimit(senderId);
                            else
                            {
                                //check limit and break if limit over
                                if (sentMail == sendLimitFromPerSender)
                                {
                                    break;
                                }
                            }
                        }
                        mail.Attachments.Dispose();
                    }
                    sentMail++;
                    Invoke(new Action(() =>
                    {
                        lblTotalSentMail.Text = "Total Sent " + sentMail + " of " + totalMail;
                    }));
                    BaseClass.RemoverSentEmail(logFile, removeIndex);//sent mail remove from temp log

                    rowIndex++;
                    finalResult = true;

                    if (txtDelay.Value > 0)
                    {
                        delayCount++;
                        if (rdoDelay20Mail.Checked)
                        {
                            if (delayCount == 20)
                            {
                                BaseClass.Delay((int)txtDelay.Value);
                                delayCount = 0;
                            }
                        }
                        else
                        {
                            BaseClass.Delay((int)txtDelay.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    removeIndex++;
                    StatusUpdate("Error");
                    Invoke(new Action(() =>
                    {
                        lblMsg.Text = "";
                        //lblthreadAbord.Text = "";
                    }));
                    //add all error into richtextbox
                    AddError("Email From:" + senderId + " To: " + emailTo + "\n" + ex.Message + "\nExecuted :" + DateTime.Now.ToString() + "\n\n");
                    //update grid status
                    StatusUpdateToGrid(rowIndex, false);
                    //EnableDisableControls(grpSendingOptions, true);
                    rowIndex++;
                }
            }

            //insert sent information
            #region // save information
            if (finalResult)
            {
                Invoke(new Action(() =>
                {
                    lblMsg.Text = "Saving data...";
                    lblthreadAbord.Text = "";

                    model.SenderId = txtFromMail.Text;
                    model.TotalMail = totalMail;
                    model.TotalSent = sentMail;
                    model.TotalFailed = totalMail - sentMail;
                }));
                bool result = service.AddSentInformation(model);
                StatusUpdate("Done");
            }
            #endregion
            // Change selected file name to processing. This is just to keep file tracking.Because of, working with bulk files
            // Such as if a txt file name is email1.txt then it will be email_Processing.txt
            changeFileName = BaseClass.RenameFile("Done", changeFileName);

            //delete file 
            BaseClass.DeleteFile("PdfFile");//delete all file from pdf folder which are converted from html 
            BaseClass.DeleteFile("ImageFile");//delete all image file from ImageFile folder
            EnableDisableControls(grpSendingOptions, true);
            Invoke(new Action(() =>
            {
                lblthreadAbord.Text = "";
                lblMsg.Text = "";
                btnPauseRun.Visible = false;
            }));
        }

        int senderIdCount = 0, sentSenderEmail = 0;
        private bool SetSender()
        {
            try
            {
                if (sentSenderEmail == senderIdCount)
                    return false;

                int totalSender = SenderList.Count;
                if (totalSender < sentSenderEmail)
                    sentSenderEmail = 0;
                senderId = SenderList[sentSenderEmail].Email;
                displayName = SenderList[sentSenderEmail].Name;
                subjects = SenderList[sentSenderEmail].Subject;
                if (!chkRandomContent.Checked)
                    messages = SenderList[sentSenderEmail].Content;
                string rowMsg = messages;
                if (chkSentanceMaker.Checked)
                    messages = ContentProcess(messages);
                string credential = SenderList[sentSenderEmail].Credential;
                //try
                //{
                //    if (!copyToClipboard.Contains(senderId))
                //    {
                //        copyToClipboard += "," + senderId;
                //        Clipboard.SetText(senderId);
                //    }
                //}
                //catch 
                //{

                //}

                Invoke(new Action(() =>
                {
                    txtFromMail.Text = senderId;
                    txtAppName.Text = displayName;
                    txtSubject.Text = subjects;
                    txtMessage.Text = rowMsg;
                    ///SaveCredentials(credential);// Save credential to application credential folder
                    sentSenderEmail++;
                }));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private string GetSubject(string subject)
        {
            try
            {
                if (subject.Contains(","))
                {
                    string[] sub = subject.Split(',');
                    int perlimit = 0, limit = 0;
                    // list email limit
                    if (SenderList == null || SenderList.Count == 0)
                    {
                        perlimit = sendLimitFromPerSender;
                        limit = sentSelectedMail;

                    }
                    else
                    {
                        perlimit = SenderList[sentSenderEmail].PerSenderLimit;
                        limit = SenderList[sentSenderEmail].Limit;//updated limit
                    }
                    int subjLen = sub.Length;
                    //int subchanger = (limit / subjLen);
                    // decimal val_=(perlimit / subjLen);
                    decimal findIndex = Convert.ToDecimal(limit) / (Convert.ToDecimal(perlimit) / subjLen);
                    int index = 0;
                    if (findIndex.ToString().Contains("."))
                    {
                        var val = findIndex.ToString("F2").Split('.');
                        if (Convert.ToInt16(val[1]) > 0)
                        {
                            index = (int)findIndex + 1;
                        }
                        else
                        {
                            index = (int)findIndex;
                        }
                        return sub[index - 1];
                    }
                    else
                    {
                        return sub[(int)findIndex - 1];
                    }

                }
                else
                {
                    return subjects;
                }
            }
            catch
            {
                return subject;
            }
        }
        private string ContentProcess(string str)
        {
            try
            {
                string loirstString = str.Substring(0, 100);
                string longString = str.Substring(100, str.Length - 100);
                string[] splitContent = longString.Split(' ');
                string content = loirstString;
                int i = 0;
                foreach (var item in splitContent)
                {
                    if (i % 2 == 0)
                    {
                        content += item + " <div style='margin-left: -9999px;display:none'>" + BaseClass.GetRandomSingleName() + " </div>";
                        i = 0;
                    }
                    i++;
                }
                return content;
            }
            catch
            {
                return str;
            }
        }
        private bool CheckSenderLimitAndChangeRandom()
        {
            try
            {
                int totalSender = SenderList.Count;
                if (sentFromPerSender == changeSenderId)// if sent mail and changeaftersent value is same
                {
                    sentFromPerSender = 0;
                    if (totalSender <= changeSenderOneByOne + 1)// now change sender by checking condition 
                    {
                        changeSenderOneByOne = 0;
                    }
                    else
                        ++changeSenderOneByOne;
                    string email = SenderList[changeSenderOneByOne].Email;
                    string name = SenderList[changeSenderOneByOne].Name;
                    string subject = SenderList[sentSenderEmail].Subject;
                    if (!chkRandomContent.Checked)
                        messages = SenderList[changeSenderOneByOne].Content;
                    string rowmsg = messages;
                    if (chkSentanceMaker.Checked)
                    {
                        messages = ContentProcess(messages);
                    }
                    string credential = SenderList[changeSenderOneByOne].Credential;

                    //try
                    //{
                    //    if (!copyToClipboard.Contains(email))
                    //    {
                    //        copyToClipboard += "," + email;
                    //        Clipboard.SetText(email);
                    //    }
                    //}
                    //catch 
                    //{

                    //}

                    senderId = email;
                    displayName = name;
                    subjects = subject;
                    Invoke(new Action(() =>
                    {
                        txtFromMail.Text = email;
                        txtAppName.Text = name;
                        txtSubject.Text = subjects;
                        txtMessage.Text = rowmsg;
                        //if (!CredentialCheck(email))// check credential if not in folder then execute below statement
                        //    SaveCredentials(credential);// Save credential to application credential folder 
                    }));
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CredentialCheck(string email)
        {
            string credential = "credential_" + email + ".json";
            // check credential is exist or not
            string path = Application.StartupPath + "/credentials/" + credential;

            if (File.Exists(path))
            {
                credential_ = path;
                return true;
            }
            else
                return false;
        }
        private bool UpdateSenderLimit(string email)
        {
            // find the person with Id = 1 and update their FirstName property
            EmailList emailToUpdate = SenderList.Find(p => p.Email == email);
            if (emailToUpdate != null)
            {
                int limit = emailToUpdate.Limit - 1;
                if (limit == 0)
                {
                    // remover sender from list
                    RemoveSenderFromList(email);
                    ++changeSenderOneByOne;
                    sentFromPerSender = changeSenderId;
                }
                else
                {
                    // update sender limit
                    emailToUpdate.Limit = limit;
                }
                return true;
            }
            return false;
        }
        private void RemoveSenderFromList(string email)
        {
            // find the person with Id = 1 and update their FirstName property
            EmailList emailToUpdate = SenderList.Find(p => p.Email == email);
            if (emailToUpdate != null)
            {
                SenderList.Remove(emailToUpdate);
            }
        }
        private void StatusUpdateToGrid(int i, bool status)
        {
            if (status)
            {
                UpdateSentValue();// update sent status
                dgvEmailList.Rows[i].Cells["btn"].Value = "✔";
                sentSelectedMail--;
            }
            else
                dgvEmailList.Rows[i].Cells["btn"].Value = "✕";
        }
        private void UpdateSentValue()
        {
            try
            {
                BaseClass.Execute($@"UPDATE senderInfo 
SET Sent = (SELECT IFNULL(sent, 0) + 1 FROM senderInfo WHERE SenderId = '{senderId}')
WHERE SenderId = '{senderId}'");
            }
            catch
            {

            }
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog open = new FolderBrowserDialog();
                open.SelectedPath = Application.StartupPath + "/atachment";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtAttachment.Text = open.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkAttachment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkAttachment.Checked)
                {
                    string attachmentPath = Path.Combine(Application.StartupPath, "Attachment", attachment);
                    txtAttachment.Text = attachmentPath;
                }
                else
                    txtAttachment.Text = "";
                pnlAttach.Enabled = chkAttachment.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblthreadAbord_Click(object sender, EventArgs e)
        {
            try
            {
                if (th != null && th.IsAlive)
                    th.Abort();
                EnableDisableControls(grpSendingOptions, true);
                lblthreadAbord.Text = "";
                lblMsg.Text = "";
            }
            catch (Exception ex)
            {
            }

        }
        private void importName_Click(object sender, EventArgs e)
        {
            txtAppName.Text = BaseClass.GetStringFromtxtFile();
            BaseClass.DisplayName = txtAppName.Text;
        }

        private void importSubject_Click(object sender, EventArgs e)
        {
            txtSubject.Text = BaseClass.GetStringFromtxtFile();
            BaseClass.SubjectName = txtSubject.Text;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            BaseTask.totalProcess--;
            panelBtn.Visible = false;
            parent.Controls.Clear();
            tControl.SelectTab(6);
        }
        int sentSelectedMail = 0;
        public void btnSelecSender_Click(object sender, EventArgs e)
        {
            try
            {
                if (BaseClass.SenderList != null)
                    BaseClass.SenderList.Clear();
                if (SenderList != null)
                    SenderList.Clear();
                if (BaseClass.SenderForm == null)
                    new SendersList().ShowDialog();
                else
                    BaseClass.SenderForm.ShowDialog();

                //new SendersList().ShowDialog();

                isMultipleSender = false;
                lnkViewEmailList.Visible = false;

                // check if multiple sender
                if (BaseClass.SenderList != null && BaseClass.SenderList.Count >= 1)
                {
                    chkRandomSender.Checked = true;
                    SenderList = BaseClass.SenderList;
                    isMultipleSender = true;
                    lnkViewEmailList.Visible = true;
                    txtAppName.Text = "Multiple Name";
                    txtFromMail.Text = "Multiple Sender";
                    txtSubject.Text = "Multiple Subject";
                    chkRandomSender.Checked = true;
                    return;
                }
                chkRandomSender.Checked = false;

                string[] data = BaseClass.MultiData;
                if (data == null || data[0] == null)
                {
                    txtFromMail.Text = txtAppName.Text = "";
                    return;
                }
                txtFromMail.Text = data[0].Replace(" ", "");
                BaseClass.SenderId = txtFromMail.Text;
                txtAppName.Text = data[1];
                // string credential = data[2];
                //SaveCredentials(credential);//save credential to credential folder
                string content = data[2];
                txtMessage.Text = content;
                string subject = data[3];
                txtSubject.Text = subject;
                string limit = data[4];
                sendLimitFromPerSender = Convert.ToInt16(limit);
                sentSelectedMail = Convert.ToInt16(data[5]);
                port = Convert.ToInt16(data[7]);
                host = data[6];
                userName = data[8];
                password = data[9];
                txtSubject.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EnableDisableControls(Control con, bool status)// enable and disable form control 
        {
            try
            {
                foreach (Control c in con.Controls)
                {
                    if (c.GetType().Name == "TextBox"
                        || c.GetType().Name == "CheckBox"
                        || c.GetType().Name == "Button"
                        || c.GetType().Name == "RadioButton"
                        || c.GetType().Name == "RichTextBox"
                        || c.GetType().Name == "PicturBox")
                        Invoke(new Action(() =>
                        {
                            c.Enabled = status;
                        }));

                }
                Invoke(new Action(() =>
                {
                    btnPauseRun.Enabled = true;
                    btnSelectSender.Enabled = status;
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtMessage_Leave(object sender, EventArgs e)
        {
            BaseClass.Message = txtMessage;
        }

        private void txtMessageHtml_Leave(object sender, EventArgs e)
        {
            BaseClass.MessageHtml = txtMessageHtml;
        }

        private void lblCopyContent_Click(object sender, EventArgs e)
        {
            chkRandomContent.Checked = false;
            if (BaseClass.Message != null)
                txtMessage.Text = BaseClass.Message.Text;
        }

        private void lblCopyHtml_Click(object sender, EventArgs e)
        {
            chkRandomHtml.Checked = false;
            if (BaseClass.MessageHtml != null)
                txtMessageHtml.Text = BaseClass.MessageHtml.Text;
        }

        private void btnImportContent_Click(object sender, EventArgs e)
        {
            chkRandomContent.Checked = false;
            txtMessage.Text = BaseClass.ReadText();
            if (BaseClass.Message != null)
                BaseClass.Message.Text = txtMessage.Text;
        }

        private void btnImportHtml_Click(object sender, EventArgs e)
        {
            chkRandomHtml.Checked = false;
            txtMessageHtml.Text = BaseClass.ReadText();
            if (BaseClass.MessageHtml != null)
                BaseClass.MessageHtml.Text = txtMessageHtml.Text;
        }
        private void AddError(string error)
        {
            foreach (MultiTask oForm1 in Application.OpenForms.OfType<MultiTask>())
            {
                Invoke(new Action(() => { oForm1.txtError.Text += "\n \t\t" + error; }));
            }
        }
        private void StatusUpdate(string status)
        {
            foreach (MultiTask oForm1 in Application.OpenForms.OfType<MultiTask>())
            {
                if (oForm1.Tag == tag)
                {
                    if (taskName == "Task1")
                        Invoke(new Action(() => { oForm1.lblStatus1.Text = status; }));
                    // Provided button1 is public.
                    else if (taskName == "Task2")
                        Invoke(new Action(() => { oForm1.lblStatus2.Text = status; }));
                    else if (taskName == "Task3")
                        Invoke(new Action(() => { oForm1.lblStatus3.Text = status; }));
                    else if (taskName == "Task4")
                        Invoke(new Action(() => { oForm1.lblStatus4.Text = status; }));
                    else if (taskName == "Task5")
                        Invoke(new Action(() => { oForm1.lblStatus5.Text = status; }));
                    break;
                }
            }
        }

        private void txtAppName_Leave(object sender, EventArgs e)
        {
            BaseClass.DisplayName = txtAppName.Text;
        }

        private void txtSubject_Leave(object sender, EventArgs e)
        {
            BaseClass.SubjectName = txtSubject.Text;
        }

        private void lblCopyName_Click(object sender, EventArgs e)
        {
            txtAppName.Text = BaseClass.DisplayName;
        }

        private void lblGetName_Click(object sender, EventArgs e)
        {
            txtAppName.Text = BaseClass.GetFirstLastName();
        }

        private void lnkViewEmailList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var str in SenderList)
            {
                builder.Append(str.Email.ToString()).AppendLine();
                builder.Append("-------------------------").AppendLine();
            }
            MessageBox.Show(builder.ToString(), "Listed Email");
            //BaseClass.SenderList = SenderList;
            // new SelectedSender().Show();
        }

        private void chkRandomSender_CheckedChanged(object sender, EventArgs e)
        {
            pnlSendValueFromPerSender.Visible = chkRandomSender.Checked;
        }

        private void lblRemoveSentData_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = 0;
                foreach (DataGridViewRow row in dgvEmailList.Rows)
                {
                    string status = row.Cells["btn"].Value.ToString();
                    if (status == "✔")
                    {
                        dgvEmailList.Rows.RemoveAt(ind);
                    }
                    else if (status == "✕")
                    { ind++; }
                    else
                        break;
                    //ind++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void deleteSendData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int ind = 0;
                foreach (DataGridViewRow row in dgvEmailList.Rows)
                {
                    string status = row.Cells["btn"].Value.ToString();
                    if (status == "✔")
                    {
                        dgvEmailList.Rows.RemoveAt(ind);
                    }
                    else
                    { ind++; }
                    //ind++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnView_Click(object sender, EventArgs e)
        {
            if (txtMessageHtml.Text != "")
                new HtmlViewer(txtMessageHtml.Text).Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // txtMessage.Text = Encrypt(txtMessageHtml.Text, "#@#@$@$@%%^$%#$%#$@$@$@1231231312312543524254543543"); 
        }
        public static string Encrypt(string text, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
        static string DecryptString(string encryptedString, string secretKey)
        {
            byte[] iv = new byte[16]; // Initialization vector
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            string decryptedString = srDecrypt.ReadToEnd();
                            return decryptedString;
                        }
                    }
                }
            }
        }
        bool pauseRun = false;
        private void btnPauseRun_Click(object sender, EventArgs e)
        {
            if (btnPauseRun.Text == "Pause")
            {
                btnPauseRun.Text = "Run";
                pauseRun = true;
            }
            else
            {
                btnPauseRun.Text = "Pause";
                pauseRun = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SelectedSender().ShowDialog();
        }

        private void chkAllRandomChangeAfterSent_CheckedChanged(object sender, EventArgs e)
        {
            pnlBottom.Enabled = chkAllRandomChangeAfterSent.Checked;
        }

        private void lblCopySubject_Click(object sender, EventArgs e)
        {
            txtSubject.Text = BaseClass.SubjectName;
        }

        private void dgvEmailList_Enter(object sender, EventArgs e)
        {
            btnImporttxtCsv.Focus();
        }
        private void txtSubject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblCopySubject_Click(sender, e);
            }
        }

        private void task1_Enter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void chkRandomContent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandomContent.Checked)
            {
                FolderBrowserDialog open = new FolderBrowserDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtMessage.Enabled = false;
                    lblContentPath.Text = open.SelectedPath;
                }
            }
            else
            {
                lblContentPath.Text = "";
                txtMessage.Enabled = true;
            }
        }

        private void chkRandomHtml_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRandomHtml.Checked)
            {
                FolderBrowserDialog open = new FolderBrowserDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtMessageHtml.Enabled = false;
                    lblHtmlPath.Text = open.SelectedPath;
                }
            }
            else
            {
                lblHtmlPath.Text = "";
                txtMessageHtml.Enabled = true;
            }
        }


        int flag = 0;
        private void chkRandomAtthmentName_CheckedChanged(object sender, EventArgs e)// this options work when attachment have any data
        {
            if (flag > 0)
            {
                flag = 0;
                return;
            }
            if (!chkAttachment.Checked)
            {
                flag++;
                MessageBox.Show("Check attachment");
                chkRandomAtthmentName.Checked = false;
            }
            else if (txtAttachment.Text == "")
            {
                flag++;
                MessageBox.Show("Attachment path required");
                chkRandomAtthmentName.Checked = false;
            }
        }
    }
}
