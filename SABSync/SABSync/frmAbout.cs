using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SABSync
{
    public partial class frmAbout : Form
    {
        Logger Logger = new Logger();
        public frmAbout()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabelSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                linkLabelSite.LinkVisited = true;
                System.Diagnostics.Process.Start("http://code.google.com/p/sabscripts/");
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                throw;
            }
        }
    }
}
