//====================================================================
// Copyright (C) 2006 Bernad Pakpahan. All rights reserved.
// Email me at bern4d@gmail.com
//====================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace XMLData.DAL
{
    public sealed class XMLCategory
    {
        private XMLCategory() { }
        static DataSet ds = new DataSet();
        static DataView dv = new DataView();
        public static DataSet allData = new DataSet();
        /// <summary>
        /// Inserts a record into the Category table.
        /// </summary>
        /// 
        public static void save()
        {
            ds.WriteXml(Application.StartupPath + "\\XML\\data.xml", XmlWriteMode.WriteSchema);
        }
        public static void Insert(string code,string settingsName, string email, string name, string credential)
        {
            DataRow dr = dv.Table.NewRow();
            dr[0] = code;
            dr[1] = settingsName;
            dr[2] = email;
            dr[3] = name;
            dr[4] = credential;
            dv.Table.Rows.Add(dr);
            save();
        }

        /// <summary>
        /// Updates a record in the Category table.
        /// </summary>
        public static void Update(string code, string settingsName, string email, string name, string credential)
        {
            DataRow dr = Select(code);
            if (dr == null)
                return;
            dr[1] = settingsName;
            dr[2] = email;
            dr[3] = name;
            dr[4] = credential;
            save();
        }

        /// <summary>
        /// Deletes a record from the Category table by a composite primary key.
        /// </summary>
        public static void Delete(string categoryID)
        {
            dv.RowFilter = "code='" + categoryID + "'";
            dv.Sort = "code";
            dv.Delete(0);
            dv.RowFilter = "";
            save();
        }
        public static void DeleteByEmail(string email)
        {
            dv.RowFilter = "email='" + email + "'";
            dv.Sort = "email";
            dv.Delete(0);
            dv.RowFilter = "";
            save();
        }

        /// <summary>
        /// Selects a single record from the Category table.
        /// </summary>
        public static DataRow Select(string categoryID)
        {
            //if(dv.Count==0)
            //    return null;
            dv.RowFilter = "code='" + categoryID + "'";
            dv.Sort = "code";
            DataRow dr = null;
            if (dv.Count > 0)
            {
                dr = dv[0].Row;
            }
            dv.RowFilter = "";
            return dr;
        }
        public static DataRow SelectByEmail(string email)
        {
            //if(dv.Count==0)
            //    return null;
            dv.RowFilter = "email='" + email + "'";
            dv.Sort = "email";
            DataRow dr = null;
            if (dv.Count > 0)
            {
                dr = dv[0].Row;
            }
            dv.RowFilter = "";
            return dr;
        }
        /// <summary>
        /// Selects all records from the Category table.
        /// </summary>
        public static DataView SelectAll()
        {
            ds.Clear();
            ds.ReadXml(Application.StartupPath + "\\XML\\data.xml", XmlReadMode.ReadSchema);
            allData.ReadXml(Application.StartupPath + "\\XML\\data.xml", XmlReadMode.ReadSchema);
            dv = ds.Tables[0].DefaultView;
            return dv;
        }
    }
}