//====================================================================
// Copyright (C) 2006 Bernad Pakpahan. All rights reserved.
// Email me at bern4d@gmail.com
//====================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections; 
using System.Data;
using XMLData.DAL;

namespace EmailBOT
{
    /// <summary>
    /// Summary description for Data.
    /// </summary>
    public class Datas
    {
        private string code;
        private string settingsName;
        private string email;
        private string name;
        private string credential;
        public Datas()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string SettingsName
        {
            get { return settingsName; }
            set { settingsName = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
    }
        public string Credential
        {
            get { return credential; }
            set { credential = value; }
        }
    }
    public class DataList
    {
        public static Datas GetData(string DataID)
        {
            DataRow iDr = null;
            iDr = XMLCategory.Select(DataID);
            Datas data = null;
            if (iDr != null)
            {
                data = new Datas();
                data.SettingsName = iDr[0] != DBNull.Value ? iDr[0].ToString() : string.Empty; ;
                data.Email = iDr[1] != DBNull.Value ? iDr[1].ToString() : string.Empty;
                data.Credential = iDr[2] != DBNull.Value ? iDr[2].ToString() : string.Empty;
            }
            return data;
        } 
        public static IList GetDataList()
        { 
            return XMLCategory.SelectAll(); 
        }

        public static void UpdateData(Datas data)
        {
            XMLCategory.Update(data.Code, data.SettingsName, data.Email, data.Name, data.Credential);
        }

        public static void InsertData(Datas data)
        {
            XMLCategory.Insert(data.Code,data.SettingsName, data.Email,data.Name, data.Credential);
        }

        public static void DeleteData(string DataID)
        {
            XMLCategory.Delete(DataID);
        }
    }

}