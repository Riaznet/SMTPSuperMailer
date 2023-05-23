using EmailBOT.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailBOT.Tasks
{
    public partial class SelectedSender : Form
    {
        public SelectedSender()
        {
            InitializeComponent();
        }

        private void SelectedSender_Load(object sender, EventArgs e)
        { 
            DataTable dt = ToDataTable(BaseClass.SenderList);
            dgvSelectedSender.DataSource = dt;

            dgvSelectedSender.Columns["UserName"].Visible = false;
            dgvSelectedSender.Columns["Password"].Visible = false;
            dgvSelectedSender.Columns["Port"].Visible = false;
            dgvSelectedSender.Columns["Host"].Visible = false; 
        }
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            { 
                //string str= X
                string email = dgvSelectedSender.Rows[rowIndex].Cells["Email"].Value.ToString();
                EmailList personToDelete = BaseClass.SenderList.Find(p => p.Email == email);
                if (personToDelete != null)
                {
                    dgvSelectedSender.Rows.RemoveAt(rowIndex);
                    BaseClass.SenderList.Remove(personToDelete);
                    

                    //var bindingList = new BindingList<EmailList>(BaseClass.SenderList);
                    //var source = new BindingSource(bindingList, null);
                    //dgvSelectedSender.DataSource = source;
                }
            }
            catch (Exception)
            {

            }
        }
        int rowIndex = 0; 
        private void dgvSelectedSender_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    rowIndex = e.RowIndex;
                    dgvSelectedSender.Rows[e.RowIndex].Selected = true;
                }
            }
            catch
            {

            }
        }
    }
}