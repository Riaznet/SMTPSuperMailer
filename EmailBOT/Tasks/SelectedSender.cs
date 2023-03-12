using EmailBOT.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            var bindingList = new BindingList<EmailList>(BaseClass.SenderList);
            var source = new BindingSource(bindingList, null);
            dgvSelectedSender.DataSource = source;
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