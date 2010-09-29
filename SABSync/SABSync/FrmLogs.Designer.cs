namespace SABSync
{
    partial class FrmLogs
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
            this.tableLayoutPanelLogs = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelLogsButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.objectListViewLogs = new BrightIdeasSoftware.ObjectListView();
            this.logsName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.logsModified = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnPurge = new System.Windows.Forms.Button();
            this.tableLayoutPanelLogs.SuspendLayout();
            this.tableLayoutPanelLogsButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelLogs
            // 
            this.tableLayoutPanelLogs.ColumnCount = 1;
            this.tableLayoutPanelLogs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogs.Controls.Add(this.tableLayoutPanelLogsButtons, 0, 1);
            this.tableLayoutPanelLogs.Controls.Add(this.objectListViewLogs, 0, 0);
            this.tableLayoutPanelLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLogs.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelLogs.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelLogs.Name = "tableLayoutPanelLogs";
            this.tableLayoutPanelLogs.RowCount = 2;
            this.tableLayoutPanelLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelLogs.Size = new System.Drawing.Size(432, 620);
            this.tableLayoutPanelLogs.TabIndex = 0;
            // 
            // tableLayoutPanelLogsButtons
            // 
            this.tableLayoutPanelLogsButtons.ColumnCount = 4;
            this.tableLayoutPanelLogsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelLogsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanelLogsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogsButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanelLogsButtons.Controls.Add(this.btnOk, 3, 0);
            this.tableLayoutPanelLogsButtons.Controls.Add(this.btnPurge, 1, 0);
            this.tableLayoutPanelLogsButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelLogsButtons.Location = new System.Drawing.Point(0, 590);
            this.tableLayoutPanelLogsButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelLogsButtons.Name = "tableLayoutPanelLogsButtons";
            this.tableLayoutPanelLogsButtons.RowCount = 1;
            this.tableLayoutPanelLogsButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelLogsButtons.Size = new System.Drawing.Size(432, 30);
            this.tableLayoutPanelLogsButtons.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(356, 1);
            this.btnOk.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(71, 26);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // objectListViewLogs
            // 
            this.objectListViewLogs.AllColumns.Add(this.logsName);
            this.objectListViewLogs.AllColumns.Add(this.logsModified);
            this.objectListViewLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logsName,
            this.logsModified});
            this.objectListViewLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewLogs.EmptyListMsg = "No Log Files Found";
            this.objectListViewLogs.Location = new System.Drawing.Point(3, 3);
            this.objectListViewLogs.Name = "objectListViewLogs";
            this.objectListViewLogs.Size = new System.Drawing.Size(426, 584);
            this.objectListViewLogs.TabIndex = 1;
            this.objectListViewLogs.UseCompatibleStateImageBehavior = false;
            this.objectListViewLogs.View = System.Windows.Forms.View.Details;
            this.objectListViewLogs.DoubleClick += new System.EventHandler(this.objectListViewLogs_DoubleClick);
            // 
            // logsName
            // 
            this.logsName.AspectName = "Name";
            this.logsName.MinimumWidth = 150;
            this.logsName.Text = "Name";
            this.logsName.Width = 150;
            // 
            // logsModified
            // 
            this.logsModified.AspectName = "LastWriteTime";
            this.logsModified.FillsFreeSpace = true;
            this.logsModified.MinimumWidth = 100;
            this.logsModified.Text = "Modified";
            this.logsModified.Width = 100;
            // 
            // btnPurge
            // 
            this.btnPurge.Location = new System.Drawing.Point(21, 1);
            this.btnPurge.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnPurge.Name = "btnPurge";
            this.btnPurge.Size = new System.Drawing.Size(71, 26);
            this.btnPurge.TabIndex = 1;
            this.btnPurge.Text = "Purge";
            this.btnPurge.UseVisualStyleBackColor = true;
            this.btnPurge.Click += new System.EventHandler(this.btnPurge_Click);
            // 
            // FrmLogs
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(432, 620);
            this.Controls.Add(this.tableLayoutPanelLogs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogs";
            this.ShowIcon = false;
            this.Text = "Logs";
            this.tableLayoutPanelLogs.ResumeLayout(false);
            this.tableLayoutPanelLogsButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLogs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLogsButtons;
        private System.Windows.Forms.Button btnOk;
        private BrightIdeasSoftware.ObjectListView objectListViewLogs;
        private BrightIdeasSoftware.OLVColumn logsName;
        private BrightIdeasSoftware.OLVColumn logsModified;
        private System.Windows.Forms.Button btnPurge;
    }
}