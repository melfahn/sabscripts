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
    public partial class FrmAddFeed : Form
    {
        public FrmAddFeed()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Save to DB then close

            if (txtName.Text.Equals("") || txtUrl.Text.Equals(""))
                return;

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                providers feed = new providers
                                     {
                                         name = txtName.Text,
                                         url = txtUrl.Text
                                     };

                sabSyncEntities.AddToproviders(feed);
                sabSyncEntities.SaveChanges();
            }

            this.Close();
        }
    }
}
