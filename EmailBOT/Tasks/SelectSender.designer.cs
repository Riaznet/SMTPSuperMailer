namespace EmailBOT.Tasks
{
    partial class SendersList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendersList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsInvalidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markAsValidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hIdeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSendMail = new System.Windows.Forms.NumericUpDown();
            this.pnlSendMail = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtShowCheckBox = new System.Windows.Forms.TextBox();
            this.btnGreenHide = new System.Windows.Forms.Button();
            this.btnRedHide = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalSelectedSender = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ids = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SenderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendMail)).BeginInit();
            this.pnlSendMail.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(72)))), ((int)(((byte)(114)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(889, 4);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(72)))), ((int)(((byte)(114)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 620);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(889, 4);
            this.panel2.TabIndex = 4;
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.AutoSize = true;
            this.lblClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblClose.Location = new System.Drawing.Point(836, 3);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(55, 39);
            this.lblClose.TabIndex = 5;
            this.lblClose.Text = "✖";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(72)))), ((int)(((byte)(114)))));
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Mongolian Baiti", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(830, 579);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(49, 30);
            this.btnOk.TabIndex = 18;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(72)))), ((int)(((byte)(114)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4, 616);
            this.panel4.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(72)))), ((int)(((byte)(114)))));
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(885, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(4, 616);
            this.panel5.TabIndex = 24;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.deleteAllToolStripMenuItem,
            this.markAsInvalidToolStripMenuItem,
            this.markAsValidToolStripMenuItem,
            this.hIdeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(154, 136);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.DarkRed;
            this.deleteToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Visible = false;
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.BackColor = System.Drawing.Color.SeaGreen;
            this.selectToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Visible = false;
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // deleteAllToolStripMenuItem
            // 
            this.deleteAllToolStripMenuItem.BackColor = System.Drawing.Color.LightSalmon;
            this.deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            this.deleteAllToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.deleteAllToolStripMenuItem.Text = "Delete All";
            this.deleteAllToolStripMenuItem.Visible = false;
            this.deleteAllToolStripMenuItem.Click += new System.EventHandler(this.deleteAllToolStripMenuItem_Click);
            // 
            // markAsInvalidToolStripMenuItem
            // 
            this.markAsInvalidToolStripMenuItem.BackColor = System.Drawing.Color.Tomato;
            this.markAsInvalidToolStripMenuItem.Name = "markAsInvalidToolStripMenuItem";
            this.markAsInvalidToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.markAsInvalidToolStripMenuItem.Text = "Mark as Invalid";
            this.markAsInvalidToolStripMenuItem.Click += new System.EventHandler(this.markAsInvalidToolStripMenuItem_Click);
            // 
            // markAsValidToolStripMenuItem
            // 
            this.markAsValidToolStripMenuItem.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.markAsValidToolStripMenuItem.Name = "markAsValidToolStripMenuItem";
            this.markAsValidToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.markAsValidToolStripMenuItem.Text = "Mark as valid";
            this.markAsValidToolStripMenuItem.Click += new System.EventHandler(this.markAsValidToolStripMenuItem_Click);
            // 
            // hIdeToolStripMenuItem
            // 
            this.hIdeToolStripMenuItem.Name = "hIdeToolStripMenuItem";
            this.hIdeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.hIdeToolStripMenuItem.Text = "HIde";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ids,
            this.SL,
            this.Date,
            this.SenderId,
            this.Name,
            this.Subject,
            this.Sent,
            this.Content,
            this.status,
            this.Host,
            this.UserName,
            this.Password,
            this.Check});
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvList.Location = new System.Drawing.Point(7, 50);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvList.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(872, 526);
            this.dgvList.TabIndex = 25;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
            this.dgvList.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseUp);
            this.dgvList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvList_DataBindingComplete);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MM-yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(19, 16);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(114, 20);
            this.dtpDate.TabIndex = 26;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Send ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(101, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "from per sender";
            // 
            // txtSendMail
            // 
            this.txtSendMail.Location = new System.Drawing.Point(46, 4);
            this.txtSendMail.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtSendMail.Name = "txtSendMail";
            this.txtSendMail.Size = new System.Drawing.Size(50, 20);
            this.txtSendMail.TabIndex = 29;
            this.txtSendMail.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // pnlSendMail
            // 
            this.pnlSendMail.Controls.Add(this.txtSendMail);
            this.pnlSendMail.Controls.Add(this.label1);
            this.pnlSendMail.Controls.Add(this.label2);
            this.pnlSendMail.Location = new System.Drawing.Point(570, 12);
            this.pnlSendMail.Name = "pnlSendMail";
            this.pnlSendMail.Size = new System.Drawing.Size(200, 26);
            this.pnlSendMail.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(139, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 22);
            this.label3.TabIndex = 27;
            this.label3.Text = "Search";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtShowCheckBox
            // 
            this.txtShowCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.txtShowCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtShowCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.txtShowCheckBox.Location = new System.Drawing.Point(478, 31);
            this.txtShowCheckBox.Name = "txtShowCheckBox";
            this.txtShowCheckBox.Size = new System.Drawing.Size(100, 13);
            this.txtShowCheckBox.TabIndex = 31;
            this.txtShowCheckBox.TextChanged += new System.EventHandler(this.txtShowCheckBox_TextChanged);
            // 
            // btnGreenHide
            // 
            this.btnGreenHide.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.btnGreenHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGreenHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGreenHide.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreenHide.ForeColor = System.Drawing.Color.Black;
            this.btnGreenHide.Location = new System.Drawing.Point(10, 579);
            this.btnGreenHide.Name = "btnGreenHide";
            this.btnGreenHide.Size = new System.Drawing.Size(123, 30);
            this.btnGreenHide.TabIndex = 18;
            this.btnGreenHide.Text = "Hide ";
            this.btnGreenHide.UseVisualStyleBackColor = false;
            this.btnGreenHide.Click += new System.EventHandler(this.btnGreenHide_Click);
            // 
            // btnRedHide
            // 
            this.btnRedHide.BackColor = System.Drawing.Color.PeachPuff;
            this.btnRedHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRedHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRedHide.Font = new System.Drawing.Font("Microsoft YaHei", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedHide.ForeColor = System.Drawing.Color.Black;
            this.btnRedHide.Location = new System.Drawing.Point(139, 579);
            this.btnRedHide.Name = "btnRedHide";
            this.btnRedHide.Size = new System.Drawing.Size(123, 30);
            this.btnRedHide.TabIndex = 18;
            this.btnRedHide.Text = "Hide ";
            this.btnRedHide.UseVisualStyleBackColor = false;
            this.btnRedHide.Click += new System.EventHandler(this.btnRedHide_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(231, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Search";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblTotalSelectedSender
            // 
            this.lblTotalSelectedSender.AutoSize = true;
            this.lblTotalSelectedSender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalSelectedSender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSelectedSender.ForeColor = System.Drawing.Color.White;
            this.lblTotalSelectedSender.Location = new System.Drawing.Point(750, 586);
            this.lblTotalSelectedSender.Name = "lblTotalSelectedSender";
            this.lblTotalSelectedSender.Size = new System.Drawing.Size(20, 22);
            this.lblTotalSelectedSender.TabIndex = 27;
            this.lblTotalSelectedSender.Text = "0";
            this.lblTotalSelectedSender.Visible = false;
            this.lblTotalSelectedSender.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(623, 589);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 15);
            this.label5.TabIndex = 27;
            this.label5.Text = "Total Selected Sender :";
            this.label5.Visible = false;
            this.label5.Click += new System.EventHandler(this.label3_Click);
            // 
            // ids
            // 
            this.ids.DataPropertyName = "Id";
            this.ids.FillWeight = 30F;
            this.ids.HeaderText = "Id";
            this.ids.Name = "ids";
            this.ids.ReadOnly = true;
            this.ids.Visible = false;
            this.ids.Width = 30;
            // 
            // SL
            // 
            this.SL.DataPropertyName = "SL";
            this.SL.FillWeight = 30F;
            this.SL.HeaderText = "SL";
            this.SL.Name = "SL";
            this.SL.ReadOnly = true;
            this.SL.Width = 30;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "Date";
            this.Date.FillWeight = 60F;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Visible = false;
            // 
            // SenderId
            // 
            this.SenderId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SenderId.DataPropertyName = "SenderId";
            this.SenderId.HeaderText = "SenderName";
            this.SenderId.Name = "SenderId";
            this.SenderId.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Name.DataPropertyName = "Name";
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Width = 130;
            // 
            // Subject
            // 
            this.Subject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Subject.DataPropertyName = "Subject";
            this.Subject.HeaderText = "Subject";
            this.Subject.Name = "Subject";
            this.Subject.ReadOnly = true;
            this.Subject.Width = 140;
            // 
            // Sent
            // 
            this.Sent.DataPropertyName = "Sent";
            this.Sent.FillWeight = 60F;
            this.Sent.HeaderText = "Sent";
            this.Sent.Name = "Sent";
            this.Sent.ReadOnly = true;
            this.Sent.Width = 60;
            // 
            // Content
            // 
            this.Content.DataPropertyName = "Content";
            this.Content.HeaderText = "Content";
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.Visible = false;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Visible = false;
            // 
            // Host
            // 
            this.Host.DataPropertyName = "Host";
            this.Host.FillWeight = 130F;
            this.Host.HeaderText = "Host";
            this.Host.Name = "Host";
            this.Host.ReadOnly = true;
            this.Host.Width = 130;
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "UserName";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            this.UserName.Visible = false;
            // 
            // Password
            // 
            this.Password.DataPropertyName = "Password";
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            this.Password.ReadOnly = true;
            this.Password.Visible = false;
            // 
            // Check
            // 
            this.Check.DataPropertyName = "Check";
            this.Check.FillWeight = 30F;
            this.Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Check.HeaderText = "";
            this.Check.Name = "Check";
            this.Check.ReadOnly = true;
            this.Check.Width = 30;
            // 
            // SendersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(889, 624);
            this.Controls.Add(this.txtShowCheckBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalSelectedSender);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlSendMail);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnRedHide);
            this.Controls.Add(this.btnGreenHide);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
           // this.Name = "SendersList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "s";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendMail)).EndInit();
            this.pnlSendMail.ResumeLayout(false);
            this.pnlSendMail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtSendMail;
        private System.Windows.Forms.Panel pnlSendMail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtShowCheckBox;
        private System.Windows.Forms.ToolStripMenuItem deleteAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsInvalidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markAsValidToolStripMenuItem;
        private System.Windows.Forms.Button btnGreenHide;
        private System.Windows.Forms.Button btnRedHide;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem hIdeToolStripMenuItem;
        private System.Windows.Forms.Label lblTotalSelectedSender;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ids;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn SenderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subject;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Host;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Check;
    }
}