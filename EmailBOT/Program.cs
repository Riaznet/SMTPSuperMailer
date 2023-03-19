using EmailBOT.Class;
using EmailBOT.Tasks;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Connection.OnlineConnection = new SqlConnectionStringBuilder { DataSource = "server5.hostever.com,14335", InitialCatalog = "artmailerdb", UserID = "artmailer", Password = "riaz.tahmid@123%rr", MultipleActiveResultSets = true, MinPoolSize = 1, MaxPoolSize = 2000 }.ToString();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Connection.DbSqliteConn = "Data Source=EmailTrackDB.db";
            Application.Run(new Login());
            //get local saved keys
            //string localFileSavedKeys = Properties.Settings.Default.Keys;
            //if (localFileSavedKeys == "")
            //{
            //    Application.Run(new ActiveLicense());
            //}
            //else
            //{
            //    string localKeyDecrypt = BaseClass.Decrypt(localFileSavedKeys, "Permsadasdas@!23$$%#$@$%^(*&*($%!$#@%");
            //    string keys = BaseClass.EncryptHash(localKeyDecrypt) + BaseClass.Encrypt(localKeyDecrypt, "PermissionKey123$$%#$@$%^(*&*($%!$#@%");
            //    var localData = BaseClass.DataReaderAdd("Select email,License from sys_table_dont_changeAnything");
            //    if (localData.Count > 0)
            //    {
            //        string localDatabaseKey = localData[1];
            //        if (localDatabaseKey != keys)// if local database key and local file saved key are not match then show active form
            //        {
            //            Application.Run(new ActiveLicense());
            //        }
            //        else
            //        {
            //            string email = localData[0];
            //            string onlineKey = BaseClass.GetData($"Select LicenseKey  from sys_license where email='{email}' and status=1", Connection.OnlineConnection);
            //            if (onlineKey == "")
            //                Application.Run(new ActiveLicense());
            //            else
            //            {
            //                BaseClass.license = onlineKey;
            //                Application.Run(new Login());
            //            }
            //        }
            //    }
            //    else
            //        Application.Run(new ActiveLicense());
            //}
        }
    }
}
