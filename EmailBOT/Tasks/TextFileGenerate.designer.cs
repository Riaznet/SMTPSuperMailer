using System.Windows.Forms;
namespace Inventory.Forms.Setup.Contact
{
    partial class TextFileGenerate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextFileGenerate));
            this.TxtFileName = new System.Windows.Forms.TextBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.Emails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotalRows = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtRows = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtEmailOwner = new System.Windows.Forms.TextBox();
            this.txtIndexVal = new System.Windows.Forms.NumericUpDown();
            this.chkInboxRate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndexVal)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtFileName
            // 
            this.TxtFileName.BackColor = System.Drawing.Color.Gainsboro;
            this.TxtFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtFileName.Enabled = false;
            this.TxtFileName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtFileName.Location = new System.Drawing.Point(109, 69);
            this.TxtFileName.Name = "TxtFileName";
            this.TxtFileName.Size = new System.Drawing.Size(331, 23);
            this.TxtFileName.TabIndex = 34;
            // 
            // btnChoose
            // 
            this.btnChoose.BackColor = System.Drawing.Color.Gray;
            this.btnChoose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoose.ForeColor = System.Drawing.Color.White;
            this.btnChoose.Location = new System.Drawing.Point(33, 68);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(79, 25);
            this.btnChoose.TabIndex = 32;
            this.btnChoose.Text = "Choose File";
            this.btnChoose.UseVisualStyleBackColor = false;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeight = 25;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Emails});
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.Color.Silver;
            this.dgvList.Location = new System.Drawing.Point(33, 133);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvList.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gainsboro;
            this.dgvList.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowTemplate.Height = 32;
            this.dgvList.RowTemplate.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(407, 323);
            this.dgvList.TabIndex = 108;
            this.dgvList.TabStop = false;
            // 
            // Emails
            // 
            this.Emails.DataPropertyName = "Emails";
            this.Emails.HeaderText = "Emails";
            this.Emails.Name = "Emails";
            this.Emails.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(130, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Create txt file with lines ";
            // 
            // lblTotalRows
            // 
            this.lblTotalRows.Location = new System.Drawing.Point(296, 93);
            this.lblTotalRows.Name = "lblTotalRows";
            this.lblTotalRows.Size = new System.Drawing.Size(144, 13);
            this.lblTotalRows.TabIndex = 109;
            this.lblTotalRows.Text = "0";
            this.lblTotalRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(317, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "per file";
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.Gray;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(361, 106);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(79, 25);
            this.btnGenerate.TabIndex = 32;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtRows
            // 
            this.txtRows.BackColor = System.Drawing.SystemColors.Control;
            this.txtRows.Location = new System.Drawing.Point(253, 109);
            this.txtRows.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(58, 20);
            this.txtRows.TabIndex = 111;
            this.txtRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEmailOwner
            // 
            this.txtEmailOwner.Font = new System.Drawing.Font("Tw Cen MT", 11F);
            this.txtEmailOwner.Location = new System.Drawing.Point(71, 4);
            this.txtEmailOwner.Multiline = true;
            this.txtEmailOwner.Name = "txtEmailOwner";
            this.txtEmailOwner.Size = new System.Drawing.Size(248, 51);
            this.txtEmailOwner.TabIndex = 112;
            this.toolTip1.SetToolTip(this.txtEmailOwner, "Must be separate by comma \r\nExample : abc@example.com,\r\nxyz@example.com,qwe@gmail" +
        ".com");
            this.txtEmailOwner.Leave += new System.EventHandler(this.txtEmailOwner_Leave);
            // 
            // txtIndexVal
            // 
            this.txtIndexVal.Location = new System.Drawing.Point(3, 23);
            this.txtIndexVal.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtIndexVal.Name = "txtIndexVal";
            this.txtIndexVal.Size = new System.Drawing.Size(63, 20);
            this.txtIndexVal.TabIndex = 113;
            this.toolTip1.SetToolTip(this.txtIndexVal, "Set value of index.\r\nYour mail set after this index");
            this.txtIndexVal.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // chkInboxRate
            // 
            this.chkInboxRate.AutoSize = true;
            this.chkInboxRate.Location = new System.Drawing.Point(362, 34);
            this.chkInboxRate.Name = "chkInboxRate";
            this.chkInboxRate.Size = new System.Drawing.Size(78, 30);
            this.chkInboxRate.TabIndex = 114;
            this.chkInboxRate.Text = "Check \r\nInbox Rate";
            this.toolTip1.SetToolTip(this.chkInboxRate, "Add own mail for checking inbox rate");
            this.chkInboxRate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtIndexVal);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtEmailOwner);
            this.panel1.Location = new System.Drawing.Point(33, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(322, 58);
            this.panel1.TabIndex = 115;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Tw Cen MT", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(77, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(229, 34);
            this.label4.TabIndex = 109;
            this.label4.Text = "Example : abc@example.com,\r\nxyz@example.com,qwe@gmail.com";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 109;
            this.label3.Text = "Set Of Index ";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(361, 14);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(76, 17);
            this.checkBox2.TabIndex = 114;
            this.checkBox2.Text = "Remember";
            this.checkBox2.UseVisualStyleBackColor = false;
            this.checkBox2.Visible = false;
            // 
            // TextFileGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 468);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.chkInboxRate);
            this.Controls.Add(this.txtRows);
            this.Controls.Add(this.lblTotalRows);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.TxtFileName);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextFileGenerate";
            this.Text = "File Generate";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndexVal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private TextBox TxtFileName;
        private System.Windows.Forms.Button btnChoose;
        private DataGridView dgvList;
        private Label label2;
        private Label lblTotalRows;
        private Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private NumericUpDown txtRows;
        private DataGridViewTextBoxColumn Emails;
        private ToolTip toolTip1;
        private TextBox txtEmailOwner;
        private NumericUpDown txtIndexVal;
        private CheckBox chkInboxRate;
        private Panel panel1;
        private Label label3;
        private CheckBox checkBox2;
        private Label label4;
    }
}