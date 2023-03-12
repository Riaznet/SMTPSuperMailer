
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.Forms.Setup.Contact
{
    public partial class TextFileGenerate : Form
    {
        DataTable dt = new DataTable();
        public TextFileGenerate()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Workbook(*.xls;*.xlsx)|*.xls;*.xlsx"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TxtFileName.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            });
                            if (result.Tables[0] != null)
                            {
                                dgvList.DataSource = result.Tables[0];
                                lblTotalRows.Text = "Total Rows : " + dgvList.Rows.Count.ToString();
                            }
                        }
                    }
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtRows.Value==0)
                {
                    MessageBox.Show("Invalid value of lines");
                    txtRows.Focus();
                    return; 
                }
                string[] array = { };
                if (chkInboxRate.Checked && txtEmailOwner.Text != "")
                {
                    if (txtEmailOwner.Text.Contains(','))
                        array = txtEmailOwner.Text.Split(',');

                }
                string path = "";
                FolderBrowserDialog open = new FolderBrowserDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    path = open.SelectedPath.Replace("\\", "/")+"/";
                }

                int rows = (int)txtRows.Value;
                int rowCount = 0;
                int fileName = 1;
                string emails = "";
                int totalRows = dgvList.Rows.Count;
                int lastCount = 0;
                if (chkInboxRate.Checked && txtEmailOwner.Text != "")
                {
                    int indexValue = (int)txtIndexVal.Value;
                    foreach (DataGridViewRow Myrow in dgvList.Rows)
                    {
                        string data = Myrow.Cells[0].Value.ToString();
                        if (data == "")
                            continue;
                        rowCount++;
                        emails += data + "\n";
                        if(rowCount== indexValue)
                        {
                            foreach (string item in array)
                            {
                                rowCount++;
                                emails += item + "\n";
                            }
                        }
                        if (rowCount == rows)
                        {
                            lastCount = rowCount;
                            rowCount = 0;
                            File.WriteAllText(path + fileName + ".txt", emails.Replace(" ", ""));
                            emails = "";
                            fileName++;
                        } 
                    }
                    if (emails != "")
                    {
                        if (lastCount <= indexValue)
                        {
                            foreach (string item in array)
                            {
                                rowCount++;
                                emails += item + "\n";
                            }
                        }
                        File.WriteAllText(path + fileName++ + ".txt", emails.Replace(" ", ""));
                    }
                }
                else
                {
                    foreach (DataGridViewRow Myrow in dgvList.Rows)
                    {
                        string data = Myrow.Cells[0].Value.ToString();
                        if (data == "")
                            continue;
                        rowCount++;
                        emails += data + "\n";
                        if (rowCount == rows)
                        {
                            rowCount = 0;
                            File.WriteAllText(path + fileName + ".txt", emails.Replace(" ", ""));
                            emails = "";
                            fileName++;
                        }
                    }
                    if (emails != "")
                        File.WriteAllText(path + fileName++ + ".txt", emails.Replace(" ", ""));
                } 
               
                MessageBox.Show("File generated.", "Success");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            File.Move("Files/1.txt", "Files/1_selected.txt");
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            txtEmailOwner.Focus();
        }

        private void txtEmailOwner_Leave(object sender, EventArgs e)
        {
            txtEmailOwner.Text = txtEmailOwner.Text.Replace(" ", "");
            if (txtEmailOwner.Text == "")
                label4.Visible = true;
            else
                label4.Visible = false;
        }
    }
}
