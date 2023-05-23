using EmailBOT.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;
using XMLData.DAL;
using CheckBox = System.Windows.Forms.CheckBox;

namespace EmailBOT.Tasks
{
    public partial class SendersList : Form
    {
        Datas datas;
        string credential = "";
        string _email = "";
        int rowIndex = 0;
        string dataid = "";
        public SendersList()
        {
            //XMLCategory.allData.Clear();
            InitializeComponent();
            //XMLCategory.SelectAll();
            //loadSenderData(); 
            //BaseClass.AddToDataTable(date);
            dgvList.DataSource = BaseClass.datatable;
            BaseClass.SenderForm = this;
            //Highlight();
            //dgvList.Columns["Check"].Visible = false;
            //btnOk.Visible = false;
        }

        public DataSet data { get; private set; }


        string[] credentialData = new string[12];
        private void lblClose_Click(object sender, EventArgs e)
        {
            BaseClass.MultiData = null;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            selected = 0;
            if (senderList.Count > 1)
            {
                BaseClass.SenderList = senderList;
                // MasrAsValid();
                this.Hide();
            }
            else if (credentialData != null)
            {
                BindSingleRowData();
                BaseClass.MultiData = credentialData;
                // MasrAsValid();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Select sender email.", "Sender Mail Required!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Highlight()
        {
            foreach (DataGridViewRow row in dgvList.Rows)
            {
                string status = row.Cells["status"].Value.ToString();
                if (status == "1")
                {
                    row.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                    //row.Cells["Check"].Value = true; 
                }
                else if (status == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.PeachPuff;
                    //row.Cells["Check"].Value = false;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.PeachPuff;
                    //row.Cells["Check"].Value = false;
                }
            }
        }


        private void lstSenderId_Click(object sender, EventArgs e)
        {
            // DataRow iDr = null;

            // string email = lstSenderId.SelectedItem.ToString();
            //iDr = XMLCategory.SelectByEmail(email);
            // if (iDr != null)
            // { 
            //     credentialData[0] = iDr[2] != DBNull.Value ? iDr[2].ToString() : string.Empty; 
            //     credentialData[1] = iDr[3] != DBNull.Value ? iDr[3].ToString() : string.Empty;
            //     credentialData[2] = iDr[4] != DBNull.Value ? iDr[4].ToString() : string.Empty;
            // }
        }

        private void lstSenderId_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //if (e.Button == MouseButtons.Right)
                //{
                //    //select the item under the mouse pointer
                //    lstSenderId.SelectedIndex = lstSenderId.IndexFromPoint(e.Location);
                //    if (lstSenderId.SelectedIndex != -1)
                //    {
                //        var email = lstSenderId.SelectedItems[0];
                //        _email = email.ToString();
                //        lstSenderId.Show();
                //    }
                //}
            }
            catch (Exception)
            {
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //XMLCategory.DeleteByEmail(_email);
            //XMLCategory.allData.Clear();
            //XMLCategory.SelectAll();
            //loadSenderData();

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
        private string DeleteById()
        {
            return BaseClass.Execute($"Delete from senderInfo where id='{dataid}'");
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BaseClass.MultiData = credentialData;
            this.Close();
        }

        private void dgvList_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    //dgvList.ClearSelection();
                    //dgvList.Rows[e.RowIndex].Selected = true;
                    //rowIndex = e.RowIndex;

                    //dataid = dgvList.Rows[e.RowIndex].Cells["ids"].Value.ToString();
                    //credentialData[0] = dgvList.Rows[rowIndex].Cells["SenderId"].Value.ToString();
                    //credentialData[1] = dgvList.Rows[rowIndex].Cells["Name"].Value.ToString(); 
                    //credentialData[2] = dgvList.Rows[rowIndex].Cells["Content"].Value.ToString();
                    //credentialData[3] = dgvList.Rows[rowIndex].Cells["Subject"].Value.ToString();
                    //credentialData[4] = txtSendMail.Text;
                    //credentialData[5] = txtSendMail.Text;
                    //credentialData[6] = dgvList.Rows[rowIndex].Cells["Host"].Value.ToString();
                    //credentialData[7] = dgvList.Rows[rowIndex].Cells["Port"].Value.ToString();
                    //credentialData[8] = dgvList.Rows[rowIndex].Cells["UserName"].Value.ToString();
                    //credentialData[9] = dgvList.Rows[rowIndex].Cells["Password"].Value.ToString();
                    //BaseClass.lastSelected = id == "" ? 0 : Convert.ToInt16(id);
                    //BaseClass.rowIndex = rowIndex;
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            selected = 0;
            if (senderList.Count > 1)
                return;
            rowIndex = e.RowIndex;
            BindSingleRowData();
            this.Close();
        }
        private void BindSingleRowData()
        {
            dataid = dgvList.Rows[rowIndex].Cells["ids"].Value.ToString();
            if (dataid != "")
            {
                credentialData[0] = dgvList.Rows[rowIndex].Cells["SenderId"].Value.ToString();
                credentialData[1] = dgvList.Rows[rowIndex].Cells["Name"].Value.ToString();
                credentialData[2] = dgvList.Rows[rowIndex].Cells["Content"].Value.ToString();
                credentialData[3] = dgvList.Rows[rowIndex].Cells["Subject"].Value.ToString();
                credentialData[4] = txtSendMail.Text;
                credentialData[5] = txtSendMail.Text;
                credentialData[6] = dgvList.Rows[rowIndex].Cells["Host"].Value.ToString();
                credentialData[7] = "0";
                credentialData[8] = dgvList.Rows[rowIndex].Cells["UserName"].Value.ToString();
                credentialData[9] = dgvList.Rows[rowIndex].Cells["Password"].Value.ToString();
                BaseClass.Execute($"Update senderinfo set status=1 where id={dataid}");

            }
            BaseClass.MultiData = credentialData;
        }
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //BaseClass.AddToDataTable(dtpDate.Value);
            //dgvList.DataSource = BaseClass.datatable;//
            //txtDate.Text = dtpDate.Text;
        }

        //List<string> senderList = new List<String> { };
        List<EmailList> senderList = new List<EmailList>();
        int selected = 0;
        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            BaseClass.AddToDataTable(dtpDate.Text);
            dgvList.DataSource = BaseClass.datatable;
        }

        private void txtShowCheckBox_TextChanged(object sender, EventArgs e)
        {
            if (txtShowCheckBox.Text == "riaz")
            {
                dgvList.Columns["Check"].Visible = true;
            }
            else
                dgvList.Columns["Check"].Visible = false;
        }

        private void deleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want delete all data? ?", "Confirmation !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            string res = BaseClass.Execute($"Delete from senderInfo where Dates='{dtpDate.Value}'");
            if (res == "true")
            {
                BaseClass.AddToDataTable(dtpDate.Text);
                dgvList.DataSource = BaseClass.datatable;
            }
        }

        private void markAsInvalidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BaseClass.Execute($"Update senderinfo set status=0 where id={dataid}");
                dgvList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.PeachPuff;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void markAsValidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BaseClass.Execute($"Update senderinfo set status=1 where id={dataid}");
                dgvList.Rows[rowIndex].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int process = 0;
        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (process == 1)
                return;
            foreach (DataGridViewRow Myrow in dgvList.Rows)
            {
                string status = Myrow.Cells["status"].Value.ToString();
                if (status == "1")
                {
                    Myrow.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                }
                else if (status == "0")
                {
                    Myrow.DefaultCellStyle.BackColor = Color.PeachPuff;
                }
                else
                {
                    Myrow.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnGreenHide_Click(object sender, EventArgs e)
        {
            HideRow(Color.DarkSeaGreen);
        }

        private void btnRedHide_Click(object sender, EventArgs e)
        {
            HideRow(Color.PeachPuff);
        }
        private void HideRow(Color color)
        {
            try
            {
                string selectedId = "";
                int ind = 0;
                process = 1;
                foreach (DataGridViewRow row in dgvList.Rows)
                {
                    if (color == row.DefaultCellStyle.BackColor)
                    {
                        dgvList.Rows.RemoveAt(ind);
                    }
                    ind++;
                }
                process = 0;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dgvList.Columns[e.ColumnIndex].HeaderText == "")
                {
                    DataGridViewCheckBoxCell chkchecking = dgvList.Rows[e.RowIndex].Cells["Check"] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chkchecking.Value) == false) // false is mean true
                    {
                        chkchecking.Value = true;
                        string email = dgvList.Rows[e.RowIndex].Cells["SenderId"].Value.ToString();
                        string name = dgvList.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        //string credential = dgvList.Rows[e.RowIndex].Cells["Credentials"].Value.ToString();
                        string content = dgvList.Rows[e.RowIndex].Cells["Content"].Value.ToString();
                        string subject = dgvList.Rows[e.RowIndex].Cells["Subject"].Value.ToString();
                        string host = dgvList.Rows[e.RowIndex].Cells["Host"].Value.ToString();
                        string port = "0";
                        string username = dgvList.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
                        string password = dgvList.Rows[e.RowIndex].Cells["Password"].Value.ToString();
                        //senderList.Add(email + "|" + name + "|" + credential + "|" + txtSendMail.Value,"");
                        senderList.Add(new EmailList { Email = email, Name = name, Content = content, Subject = subject, Host = host,Port = Convert.ToInt32(port),UserName=username,Password=password, Limit = (int)txtSendMail.Value, PerSenderLimit = (int)txtSendMail.Value });
                        string id = dgvList.Rows[e.RowIndex].Cells["ids"].Value.ToString();

                        //grid status
                        BaseClass.Execute($"Update senderinfo set status=1 where id={id}");
                        dgvList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        selected++;

                    }
                    else
                    {
                        chkchecking.Value = false;
                        string email = dgvList.Rows[e.RowIndex].Cells["SenderId"].Value.ToString();
                        senderList.Remove(senderList.Find(p => p.Email == email));
                        //grid status
                        dgvList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        selected--;
                    }
                    // lblMultipleEmail.Text = string.Join(",", senderList);
                    if (senderList.Count >= 1)
                        pnlSendMail.Enabled = false;
                    else
                        pnlSendMail.Enabled = true;

                }
                lblTotalSelectedSender.Text = selected.ToString();
            }
        }
    }
}
