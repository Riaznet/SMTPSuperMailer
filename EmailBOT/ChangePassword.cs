using EmailBOT.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string oldPass = BaseClass.GetData($"Select password from registration where id='{BaseClass.UserId}'",Connection.OnlineConnection);
            if (oldPass == txtOldPassword.Text)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(Connection.OnlineConnection))
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = $@"Update registration set password=@password where id='{BaseClass.UserId}'";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                            int x = cmd.ExecuteNonQuery();
                            if (x > 0)
                            {
                                MessageBox.Show("Pasword changed."); 
                                this.Close();
                            }
                        }
                        if (conn.State != ConnectionState.Closed)
                            conn.Close();
                    }
                }
                catch
                {

                } 
            }
        }
    }
}
