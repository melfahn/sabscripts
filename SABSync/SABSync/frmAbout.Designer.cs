namespace SABSync
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.buttonClose = new System.Windows.Forms.Button();
            this.linkLabelSite = new System.Windows.Forms.LinkLabel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.linkLabelDonate = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(105, 131);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // linkLabelSite
            // 
            this.linkLabelSite.AutoSize = true;
            this.linkLabelSite.Location = new System.Drawing.Point(130, 97);
            this.linkLabelSite.Name = "linkLabelSite";
            this.linkLabelSite.Size = new System.Drawing.Size(60, 13);
            this.linkLabelSite.TabIndex = 1;
            this.linkLabelSite.TabStop = true;
            this.linkLabelSite.Text = "SABScripts";
            this.linkLabelSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSite_LinkClicked);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(28, 33);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(78, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "More Info Here";
            // 
            // linkLabelDonate
            // 
            this.linkLabelDonate.AutoSize = true;
            this.linkLabelDonate.Location = new System.Drawing.Point(151, 58);
            this.linkLabelDonate.Name = "linkLabelDonate";
            this.linkLabelDonate.Size = new System.Drawing.Size(42, 13);
            this.linkLabelDonate.TabIndex = 3;
            this.linkLabelDonate.TabStop = true;
            this.linkLabelDonate.Text = "Donate";
            // 
            // frmAbout
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 157);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabelDonate);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.linkLabelSite);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 500);
            this.Name = "frmAbout";
            this.ShowInTaskbar = false;
            this.Text = "About";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.LinkLabel linkLabelSite;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.LinkLabel linkLabelDonate;
    }
}