namespace EmailBOT.Tasks
{
    partial class SelectedSender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedSender));
            this.dgvSelectedSender = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credential = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedSender)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSelectedSender
            // 
            this.dgvSelectedSender.AllowUserToAddRows = false;
            this.dgvSelectedSender.AllowUserToDeleteRows = false;
            this.dgvSelectedSender.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectedSender.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Email,
            this.Name,
            this.Content,
            this.Credential,
            this.Limit});
            this.dgvSelectedSender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectedSender.Location = new System.Drawing.Point(0, 0);
            this.dgvSelectedSender.Name = "dgvSelectedSender";
            this.dgvSelectedSender.ReadOnly = true;
            this.dgvSelectedSender.RowHeadersVisible = false;
            this.dgvSelectedSender.RowTemplate.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvSelectedSender.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSelectedSender.Size = new System.Drawing.Size(532, 546);
            this.dgvSelectedSender.TabIndex = 0;
            this.dgvSelectedSender.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSelectedSender_CellMouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.BackColor = System.Drawing.Color.LightCoral;
            this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // Email
            // 
            this.Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // Name
            // 
            this.Name.DataPropertyName = "Name";
            this.Name.FillWeight = 90F;
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // Content
            // 
            this.Content.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Content.DataPropertyName = "Content";
            this.Content.FillWeight = 120F;
            this.Content.HeaderText = "Content";
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.Width = 130;
            // 
            // Credential
            // 
            this.Credential.DataPropertyName = "Credential";
            this.Credential.HeaderText = "Credential";
            this.Credential.Name = "Credential";
            this.Credential.ReadOnly = true;
            this.Credential.Visible = false;
            // 
            // Limit
            // 
            this.Limit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Limit.DataPropertyName = "Limit";
            this.Limit.FillWeight = 50F;
            this.Limit.HeaderText = "Limit";
            this.Limit.Name = "Limit";
            this.Limit.ReadOnly = true;
            this.Limit.Width = 50;
            // 
            // SelectedSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 546);
            this.Controls.Add(this.dgvSelectedSender);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            //this.Name = "SelectedSender";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selected Sender";
            this.Load += new System.EventHandler(this.SelectedSender_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectedSender)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSelectedSender;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credential;
        private System.Windows.Forms.DataGridViewTextBoxColumn Limit;
    }
}