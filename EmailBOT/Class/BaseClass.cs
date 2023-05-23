using Bogus;
using EmailBOT.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataSet = System.Data.DataSet;
using Image = iTextSharp.text.Image;

namespace EmailBOT.Class
{
    internal sealed class BaseClass : PublicProperty
    {
        public static List<EmailList> SenderList { get; set; }
        public static SQLiteConnection conn;
        public static SQLiteCommand cmd;
        public static DataTable datatable;

        public BaseClass()
        {
            conn = new SQLiteConnection("");
            cmd = new SQLiteCommand("", conn);
        }
        public static void AddToDataTable(string date)
        {
            datatable = BaseClass.DataTableData($@"Select id,row_number() over (order by Id) SL,  Date,SenderId,Name,Content,Subject, Status,Sent,Host,UserName,Password from senderInfo where Date = '{date}' order by id");
        }
        public static void GridViewBind(DataGridView ob, String sql)
        {
            DataTable table = new DataTable();
            try
            {
                SQLiteConnection conn = new SQLiteConnection(Connection.DbSqliteConn);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(table);
                ob.DataSource = table;
                //ob.DataBind();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch { }
            //return List;
        }

        public static string GetData(string str)
        {
            string Result = "";
            try
            {
                SQLiteConnection conn = new SQLiteConnection(Connection.DbSqliteConn);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd = new SQLiteCommand(str, conn);
                SQLiteDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                    Result = DR[0].ToString();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex) { }
            return Result;
        }
        public static string GetData(string str, string connection)
        {
            string Result = "";
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand cmd = new SqlCommand(str, conn);
                SqlDataReader DR = cmd.ExecuteReader();
                while (DR.Read())
                    Result = DR[0].ToString();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex) { }
            return Result;
        }
        public static List<string> DataReaderAdd(String sql)
        {
            var list = new List<string>();
            try
            {
                var conn = new SQLiteConnection(Connection.DbSqliteConn);
                if (conn.State != ConnectionState.Open) conn.Open();
                var cmd = new SQLiteCommand(sql, conn);
                SQLiteDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int count = rd.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(rd[i].ToString());
                    }
                }
                if (conn.State != ConnectionState.Closed) conn.Close();
                //rd.Close();
                return list;
            }
            catch
            {
                return list;
            }
        }
        public static List<string> DataReaderAdd(String sql, string connection)
        {
            var list = new List<string>();
            try
            {
                var conn = new SqlConnection(connection);
                if (conn.State != ConnectionState.Open) conn.Open();
                var cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    int count = rd.FieldCount;
                    for (int i = 0; i < count; i++)
                    {
                        list.Add(rd[i].ToString());
                    }
                }
                if (conn.State != ConnectionState.Closed) conn.Close();
                //rd.Close();
                return list;
            }
            catch
            {
                return list;
            }
        }

        public static string Execute(string str)
        {
            string s = "false";
            SQLiteTransaction tran = null;
            try
            {

                SQLiteConnection Conn = new SQLiteConnection(Connection.DbSqliteConn);
                if (Conn.State != ConnectionState.Open)
                    Conn.Open();
                tran = Conn.BeginTransaction();
                SQLiteCommand cmd = new SQLiteCommand(str, Conn);
                cmd.Transaction = tran;
                int count = cmd.ExecuteNonQuery();
                if (count == 0)
                    s = "null";
                else
                    s = "true";
                tran.Commit();
                if (Conn.State != ConnectionState.Closed)
                    Conn.Close();
            }
            catch (Exception ex) { tran.Rollback(); s = ex.Message; }
            return s;

        }
        public static string Execute(string str, string connection)
        {
            string s = "false";
            SqlTransaction tran = null;
            try
            {
                SqlConnection conn = new SqlConnection(connection);
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                tran = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.Transaction = tran;
                int count = cmd.ExecuteNonQuery();
                if (count <= 0)
                    s = "null";
                else
                    s = "true";
                tran.Commit();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex) { tran.Rollback(); s = ex.Message; }
            return s;

        }
        public static string EncryptHash(string clearText)
        {
            try
            {
                byte[] hashBytes = computeHash(clearText + "ArtMailer");
                byte[] hashWithSaltBytes = new byte[hashBytes.Length];
                for (int i = 0; i < hashBytes.Length; i++)
                    hashWithSaltBytes[i] = hashBytes[i];
                string hashValue = Convert.ToBase64String(hashWithSaltBytes);

                return hashValue;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static byte[] computeHash(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            HashAlgorithm hash = new SHA256Managed();
            return hash.ComputeHash(plainTextBytes);
        }
        public static void BindComboBox(ComboBox ddl, string root, string query)
        {
            SQLiteConnection conn = new SQLiteConnection(Connection.DbSqliteConn);
            DataTable dataTable = new DataTable();
            DataRow dataRow;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dataTable);
                dataRow = dataTable.NewRow();
                if (root != "")
                    dataRow.ItemArray = new object[] { 0, "" + root + "" };
                dataTable.Rows.InsertAt(dataRow, 0);
                ddl.DisplayMember = "Name";
                ddl.ValueMember = "Id";
                ddl.DataSource = dataTable;
                ddl.SelectedIndex = 0;
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        public static DataTable DataTableData(string Script)
        {
            try
            {
                DataTable ds = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Connection.DbSqliteConn))
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (cmd = new SQLiteCommand(Script, conn))
                    {
                        using (SQLiteDataAdapter sda = new SQLiteDataAdapter())
                        {
                            cmd.Connection = conn;
                            sda.SelectCommand = cmd;
                            sda.Fill(ds);
                            conn.Close();
                            return ds;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        public static DataSet DataSetData(string Script)
        {
            DataSet ds = new DataSet();
            SQLiteConnection conn = new SQLiteConnection(Connection.DbSqliteConn);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            using (cmd = new SQLiteCommand(Script, conn))
            {
                using (SQLiteDataAdapter sda = new SQLiteDataAdapter())
                {
                    cmd.Connection = conn;
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                    conn.Close();
                    return ds;
                }
            }

        }
        public static string RenameFile(string changeName, string ExistingPath)// change file name for status of txt file
        {
            string changeable = "";
            try
            {
                string exist = ExistingPath;
                changeable = ExistingPath.Replace("Selected_Selected", "Selected").Replace("Selected_Selected_Selected", "Selected");
                File.Move(exist, changeable);
                changeable = ExistingPath.Replace("_Done", "");
                File.Move(exist, changeable);

                string fileName = Path.GetFileName(ExistingPath);
                if (fileName.Contains("Selected") || fileName.Contains("Processing"))
                { 
                    changeable = ExistingPath.Replace("Selected", changeName).Replace("Processing", changeName);
                    File.Move(exist, changeable);
                    return changeable;
                }
                else
                {
                    string fileDirectory = Path.GetDirectoryName(ExistingPath);
                    string[] name = fileName.Split('.'); //0=1,1=txt
                    string changingName = name[0] + "_" + changeName + "." + name[1]; 
                    changeable = ExistingPath.Replace("Selected", changeName).Replace("Processing", changeName);
                    File.Move(exist, fileDirectory + "/" + changingName);
                    return fileDirectory + "/" + changingName;
                }
            }
            catch
            {
                return changeable;

            }
        }
        public static void RemoverSentEmail(string path, int line)
        {
            try
            {
                linesList = File.ReadAllLines(path).ToList();
                linesList.RemoveAt(line);
                File.WriteAllLines(path, linesList.ToArray());
            }
            catch
            {
            }

        }
        public static string RandomFileSelection(string path)
        {
            try
            {
                var rand = new Random();
                var files = Directory.GetFiles(path, "*.txt");
                return files[rand.Next(files.Length)];
            }
            catch
            {
                return "";
            }
        }
        public static void CopyFile(string sourceFile, string destinationFile)
        {
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch
            {
            }
        }
        public static string ReadText(string path)
        {
            try
            {
                string txt = File.ReadAllText(path);
                return txt;
            }
            catch (Exception)
            {
                return "";
            }

        }
        // convert html source to image 
        //public static void ConvertHtmlToImage(string htmlSource, string imageName)
        //{
        //    try
        //    {
        //        var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
        //        htmlToImageConv.Width = 595; // set width of A4 size in pixels at 72 dpi
        //        //htmlToImageConv.Height = 942; // set height of A4 size in pixels at 72 dpi
        //        var jpegBytes = htmlToImageConv.GenerateImage(htmlSource, NReco.ImageGenerator.ImageFormat.Jpeg);
        //        using (var ms = new MemoryStream(jpegBytes))
        //        {
        //            using (var fs = new FileStream("ImageFile/" + imageName + ".jpg", FileMode.Create))
        //            {
        //                ms.WriteTo(fs);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n Your html are not correct format.");
        //    }
        //}
        //public static string ConvertHtmlToPdf(string htmlSource, string pdfName)
        //{
        //    try
        //    {
        //        var htmlToPdf = new HtmlToPdfConverter();
               
        //        //htmlToPdf.Width = 595; // set width of A4 size in pixels at 72 dpi
        //        //htmlToPdf.Height = 842; // set height of A4 size in pixels at 72 dpi
        //        var pdfBytes = htmlToPdf.GeneratePdf(htmlSource);
        //        string path = Path.Combine("PdfFile", pdfName + ".pdf");
        //        using (var ms = new MemoryStream(pdfBytes))
        //        {
        //            using (var fs = new FileStream(path, FileMode.Create))
        //            {
        //                ms.WriteTo(fs);
        //            }
        //        }
        //        return path;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return "";
        //    }
        //}
        //public static void ConvertImageToPdf(string pdfName, string imageName)
        //{
        //    try
        //    {
        //        Document document = new Document(iTextSharp.text.PageSize.A4, 0f ,0f, 0f, 0f);
        //        string path = Path.Combine("PdfFile", pdfName + ".pdf");
        //        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
        //        {
        //            PdfWriter.GetInstance(document, stream);
        //            document.Open();
        //            using (var imageStream = new FileStream("ImageFile/" + imageName + ".jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        //            {
        //                var image = Image.GetInstance(imageStream);
        //                float maxWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        //                float maxHeight = document.PageSize.Height - document.TopMargin - document.BottomMargin;
        //                if (image.Height > maxHeight || image.Width > maxWidth)
        //                    image.ScaleToFit(maxWidth, maxHeight);
        //                document.Add(image);
        //            }
        //            document.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private static Random random = new Random();
        public static string RandomString(int length, string tp)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            if (tp == "num")
                chars = "0123456789";
            else if (tp == "alp")
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // wait processing as per val
        public static void Delay(int val)
        {
            val = (val) * 1000;
            var t = Task.Run(async delegate
            {
                await Task.Delay(val);
                return 60;
            });
            t.Wait();
        }
        public static void ChangeFileName(string attachmentPath)
        {
            string dir = attachmentPath;
            string fileDate, new_fileDate;
            DateTime dt;
            int i = 1;
            foreach (string original_filename in Directory.GetFiles(dir))
            {
                try
                {
                    fileDate = Path.GetFileName(original_filename);
                    string oldfileName = SplitText(fileDate, '.', 0);
                    string oldFileExtens = SplitText(fileDate, '.', 1);
                    new_fileDate = RandomString(12).ToString();
                    File.Move(original_filename, original_filename.Replace(fileDate, new_fileDate + i + "." + oldFileExtens));
                    i++;
                }
                catch
                {
                    i++;
                }
            }
        }
        public static string[] SplitText(string text, char sumbol)
        {
            string[] returnText = { };
            string searchPar = text;
            int splitter = searchPar.IndexOf(sumbol);
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split(sumbol);
                returnText = lineSplit;
            }
            else
                return returnText;
            return returnText;
        }
        public static string SplitText(string text, char sumbol, int indexNo)
        {
            string returnText = "";
            string searchPar = text;
            int splitter = searchPar.IndexOf(sumbol);
            if (splitter != -1)
            {
                string[] lineSplit = searchPar.Split(sumbol);
                returnText = lineSplit[indexNo];
            }
            else
                returnText = text;
            return returnText;
        }
        public static void DeleteFile(string path)
        {
            try
            {
                string[] filePaths = Directory.GetFiles(path, "*");
                foreach (string filePath in filePaths)
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static string Base64UrlEncode(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "");
        }
        static int selectRandomData = 0;
        public static string GetRandomData(string stringToarray)
        {
            if (!stringToarray.Contains(","))
                return stringToarray;
            string[] array = SplitText(stringToarray, ',');
            int len = array.Length;
            //Random random = new Random();
            //var randomIndex = random.Next(0, array.Length);
            string data = array[selectRandomData].ToString();
            selectRandomData++;
            if (len <= selectRandomData)
                selectRandomData = 0;
            return data;
        }
        public static string GetStringFromtxtFile()
        {
            try
            {
                string filename = "";
                var loadDialog = new OpenFileDialog { Filter = "File|*.txt" };
                if (loadDialog.ShowDialog() == DialogResult.OK)
                    filename = loadDialog.FileName;
                if (filename == "")
                    return "";
                //string ext = filename.Substring(filename.Length - 3, 3);
                string[] lines = File.ReadAllLines(filename);
                string values;
                string strings = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    values = lines[i].ToString();
                    if (values != "")
                    {
                        if (i == 0)
                            strings = values;
                        else
                            strings += "|" + values;
                    }
                }
                return strings;
            }
            catch
            {
                return "";
            }
        }
        public static string ReadText()
        {
            try
            {
                string filename = "";
                var loadDialog = new OpenFileDialog { Filter = "File|*.txt;*.html;*.htm" };
                if (loadDialog.ShowDialog() == DialogResult.OK)
                    filename = loadDialog.FileName;
                if (filename == "")
                    return "";
                //string ext = filename.Substring(filename.Length - 3, 3);
                string txt = File.ReadAllText(filename);
                return txt;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Encrypt(string encryptString, string encryptionKey)
        {
            //we can change the code converstion key as per our requirement    
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey,
                    new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText, string decryptiontKey)
        {
            //we can change the code converstion key as per our requirement, but the decryption key should be same as encryption key    
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {

                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(decryptiontKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        internal static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        internal static void CheckUserValidation()
        {
            //get local saved keys
            string localKeys = Properties.Settings.Default.Keys;
            //decrypt localsaved keys
            localKeys = BaseClass.Decrypt(localKeys, "Permsadasdas@!23$$%#$@$%^(*&*($%!$#@%");
            string keysCast = BaseClass.EncryptHash(localKeys) + BaseClass.Encrypt(localKeys, "PermissionKey123$$%#$@$%^(*&*($%!$#@%");
            //get local datbase saved keys
            string localDatabaseKeys = BaseClass.GetData("Select License from sys_table_dont_changeAnything");
            if (keysCast != localDatabaseKeys)
            {
                isActive = false;
                BaseClass.Execute("Delete from sys_table_dont_changeAnything");
                Properties.Settings.Default.Keys = null;
                Properties.Settings.Default.Save();
                return;
            }
            //get online key
            string onlineKey = BaseClass.GetData($"Select LicenseKey  from sys_license where LicenseKey='{localKeys}' and status=1", Connection.OnlineConnection);
            string onlinekeyGenerate = BaseClass.EncryptHash(onlineKey) + BaseClass.Encrypt(onlineKey, "PermissionKey123$$%#$@$%^(*&*($%!$#@%");
            string keysForLocal = BaseClass.Encrypt(onlinekeyGenerate, "Permsadasdas@!23$$%#$@$%^(*&*($%!$#@%");
            if (onlinekeyGenerate != keysCast)
            {
                isActive = false;
                BaseClass.Execute("Delete from sys_table_dont_changeAnything");
                Properties.Settings.Default.Keys = null;
                Properties.Settings.Default.Save();
            }
            else
                isActive = true;
        }
        static Faker faker = new Faker();



        internal static string GetFirstLastName()
        {
            try
            {
                var firstName = faker.Name.FirstName().Replace("\'", "");
                var lastName = faker.Name.LastName().Replace("\'", "");
                string name = firstName + " " + lastName;
                return name;
            }
            catch
            {
                return "";
            }
        }
        internal static string GetRandomSingleName()
        {
            try
            {
                var firstName = faker.Name.FirstName().Replace("\'", "");
                return firstName;
            }
            catch
            {
                return "";
            }
        }
        public static string GetUniqueId()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
                ManagementObject mo = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                return mo["UUID"].ToString();
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}
