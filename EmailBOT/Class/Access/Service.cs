using EmailBOT.Class.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailBOT.Class.Access
{
    internal class Service
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        public Service()
        {
            conn = new SQLiteConnection(Connection.DbSqliteConn);
            cmd = new SQLiteCommand();
        }
        public bool AddUpdateSenderInfo(SenderData model)
        {
            bool result = false;
            SQLiteTransaction transaction = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                transaction = conn.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                if (model.Type == "Add")
                    cmd.CommandText = $@"Insert Into SenderInfo (SenderId,Name,Date,Content,Subject,Host,Port,UserName,Password) Values (@SenderId,@Name,@Date,@Content,@Subject,@Host,@Port,@UserName,@Password)";
                else
                    cmd.CommandText = $@"Update SenderInfo set SenderId=@SenderId,Name=@Name,Date=@Date,Content=@Content ,Subject=@Subject,Host=@Host,Port=@Port,UserName=@UserName,Password=@Password where Id=@Id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.Parameters.AddWithValue("@SenderId", model.SenderId);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Host", model.Host);
                cmd.Parameters.AddWithValue("@Port", model.Port);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.Parameters.AddWithValue("@Date", model.Date);
                cmd.Parameters.AddWithValue("@Content", model.Content);
                cmd.Parameters.AddWithValue("@Subject",model.Subject);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery(); 
                transaction.Commit();
                result = true;
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }
        public bool AddSentInformation(SentEmail model)
        {
            bool result = false;
            SQLiteTransaction transaction = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                transaction = conn.BeginTransaction();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $@"Insert Into SendEmail (SenderId,TotalMail,TotalSent,TotalFailed,Date) Values (@SenderId,@TotalMail,@TotalSent,@TotalFailed,@Date)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@SenderId", model.SenderId);
                cmd.Parameters.AddWithValue("@TotalMail", model.TotalMail);
                cmd.Parameters.AddWithValue("@TotalSent", model.TotalSent);
                cmd.Parameters.AddWithValue("@TotalFailed", model.TotalFailed);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = true;
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }
        public bool DeleteSenderInfo(int id)
        {
            bool result = false;
            SQLiteTransaction transaction = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                transaction = conn.BeginTransaction();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $@"Delete from SendEmail where id=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = true;
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return result;
        }
    }
}
