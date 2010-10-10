using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;

namespace SABSync
{
    public partial class FrmOptions : Form
    {
        private Config Config = new Config();

        /// <summary>
        /// Sets the form state change handlers.
        /// </summary>
        private void SetFormStateChangeHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Attach to text changed event
                EventInfo eventInfo = control.GetType().GetEvent("TextChanged",
                       BindingFlags.Instance | BindingFlags.Public);
                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(control, new EventHandler(ControlStateChanged));
                }

                // Attach to value changed event
                eventInfo = control.GetType().GetEvent("ValueChanged",
                        BindingFlags.Instance | BindingFlags.Public);
                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(control, new EventHandler(ControlStateChanged));
                }

                // Attach to checked changed event
                eventInfo = control.GetType().GetEvent("CheckedChanged",
                       BindingFlags.Instance | BindingFlags.Public);
                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(control, new EventHandler(ControlStateChanged));
                }

                // handle container controls which might have child controls
                if (control.Controls.Count > 0)
                {
                    SetFormStateChangeHandlers(control);
                }
            }
        }

        /// <summary>
        /// Controls the state changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ControlStateChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        public FrmOptions()
        {
            InitializeComponent();
            LoadConfig();
            SetFormStateChangeHandlers(this);
            btnApply.Enabled = false;
        }

        private void LoadConfig()
        {
            string[] sabInfoSplit = Config.GetValue("SabnzbdInfo").ToString().Split(':');

            string hostname = null;
            string port = null;
            if (sabInfoSplit.Length == 2)
            {
                hostname = sabInfoSplit[0];
                port = sabInfoSplit[1];
            }
            else
            {
                hostname = "127.0.0.1";
                port = "8080";
            }

            txtTvRoot.Text = Config.GetValue("TvRoot").ToString();
            txtTvTemplate.Text = Config.TvTemplate;
            txtTvDailyTemplate.Text = Config.TvDailyTemplate;
            txtVideoExt.Text = Config.GetValue("VideoExt").ToString();
            txtNzbDir.Text = Config.GetValue("NzbDir").ToString();
            txtSabInfoHost.Text = hostname;
            txtSabInfoPort.Text = port;
            txtUsername.Text = Config.GetValue("Username").ToString();
            txtPassword.Text = Config.GetValue("Password").ToString();
            txtApiKey.Text = Config.GetValue("ApiKey").ToString();
            txtPriority.Text = Config.GetValue("Priority").ToString();
            txtDeleteLogs.Text = Config.DeleteLogs.ToString();
            chkReplaceChars.Checked = Config.SabReplaceChars;
            chkVerboseLogging.Checked = Config.VerboseLogging;
            chkDownloadPropers.Checked = Config.DownloadPropers;
            chkSyncOnStart.Checked = Config.SyncOnStart;
            numMinutes.Value = Config.Interval;

            //Get Download Quality as a string
            comboBoxDefaultQuality.SelectedIndex = Convert.ToInt32(Config.GetValue("DownloadQuality"));

            //Get Priority as a String
            txtPriority.Text = GetPriorityAsString(Convert.ToInt32(Config.GetValue("Priority")));
        }

        private void SaveSettings()
        {
            Config.SaveValue("TvRoot", txtTvRoot.Text);
            Config.SaveValue("TvTemplate", txtTvTemplate.Text);
            Config.SaveValue("TvDailyTemplate", txtTvDailyTemplate.Text);
            Config.SaveValue("VideoExt", txtVideoExt.Text);
            Config.SaveValue("NzbDir", txtNzbDir.Text);
            Config.SaveValue("SabnzbdInfo", SabInfoJoin(txtSabInfoHost.Text, txtSabInfoPort.Text));
            Config.SaveValue("Username", txtUsername.Text);
            Config.SaveValue("Password", txtPassword.Text);
            Config.SaveValue("ApiKey", txtApiKey.Text);
            Config.SaveValue("SabReplaceChars", chkReplaceChars.Checked);
            Config.SaveValue("VerboseLogging", chkVerboseLogging.Checked);
            Config.SaveValue("DownloadPropers", chkDownloadPropers.Checked);
            Config.SaveValue("DeleteLogs", Convert.ToInt32(txtDeleteLogs.Text));
            Config.SaveValue("Interval", (int)numMinutes.Value);
            Config.SaveValue("SyncOnStart", chkSyncOnStart.Checked);

            //Save DownloadQuality
            Config.SaveValue("DownloadQuality", comboBoxDefaultQuality.SelectedIndex);

            //Save Priority
            Config.SaveValue("Priority", GetPriorityAsInt(txtPriority.Text));

            Config.SaveConfig(); //Save the Config
        }

        private int GetPriorityAsInt(string priority)
        {
            if (priority.ToLower() == "low")
                return -1;

            if (priority.ToLower() == "normal")
                return 0;

            if (priority.ToLower() == "high")
                return 1;

                return 0; //Default to Normal if not assigned
        }

        private string GetPriorityAsString(int priority)
        {

            if (priority == -1)
                return "Low";

            if (priority == 0)
                return "Normal";

            if (priority == 1)
                return "High";

            return "Normal";
        }

        public string TestConnection(string hostname, string port, string apiKey, string username, string password)
        {
            string versionRssUrl = "http://" + hostname + ":" + port + "/api?mode=queue&output=xml&apikey=" + apiKey + "&ma_username=" + username + "&ma_password=" + password;
            string sabVersion = null;
            try
            {
                HttpWebRequest versionRssRequest = (HttpWebRequest)WebRequest.Create(versionRssUrl);
                versionRssRequest.Timeout = 10000;
                HttpWebResponse versionRssResponse = (HttpWebResponse)versionRssRequest.GetResponse();
                Stream versionDoc = versionRssResponse.GetResponseStream();
                XmlTextReader versionReader = new XmlTextReader(versionDoc);
                XmlDocument versionRssDoc = new XmlDocument();
                versionRssDoc.Load(versionReader);

                var version = versionRssDoc.GetElementsByTagName(@"version");
                var error = versionRssDoc.GetElementsByTagName(@"error");

                if (error.Count != 0)
                {
                    string errorMsg = "Error connecting to SAB: " + error[0].InnerText;
                    return errorMsg;
                }

                if (version.Count != 0)
                {
                    sabVersion = version[0].InnerText;
                }
                versionRssResponse.Close();
            }

            catch
            {
                string errorMsg = "Timed out connecting to SABnzbd, check your settings";
                return errorMsg;
            }
            string result = "Successfully Connected to SABnzbd! Version: " + sabVersion;
            return result;
        }

        private string SabInfoJoin(string hostname, string port)
        {
            string sabInfo = hostname + ":" + port;
            return sabInfo;
        }

        private void treeViewOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Determine which was selected and put buttons on screen

            if (this.treeViewOptions.SelectedNode.Name == "NodeGeneral")
            {
                panelSab.Visible = false;
                panelShows.Visible = false;
                panelGeneral.Width = 449;
                panelGeneral.Height = 315;
                panelGeneral.Location = new Point(140, 13);
                panelGeneral.Visible = true;
            }

            else if (this.treeViewOptions.SelectedNode.Name == "NodeSab")
            {
                panelGeneral.Visible = false;
                panelShows.Visible = false;
                panelSab.Width = 449;
                panelSab.Height = 315;
                panelSab.Location = new Point(140, 13);
                panelSab.Visible = true;
            }

            else if (this.treeViewOptions.SelectedNode.Name == "NodeConfig")
            {
                panelGeneral.Visible = false;
                panelSab.Visible = false;
                panelShows.Visible = false;
            }

            else if (this.treeViewOptions.SelectedNode.Name == "NodeShows")
            {
                panelGeneral.Visible = false;
                panelSab.Visible = false;
                panelShows.Width = 449;
                panelShows.Height = 315;
                panelShows.Location = new Point(140, 13);
                panelShows.Visible = true;
            }
        }

        private void btnTestSab_Click(object sender, EventArgs e)
        {
            string response = TestConnection(txtSabInfoHost.Text, txtSabInfoPort.Text, txtApiKey.Text, txtUsername.Text, txtPassword.Text);
        }

        private void tvRootBrowse_Click(object sender, EventArgs e)
        {
            ChangeTvRootFolder();
        }

        public void nzbDirBrowse_Click(object sender, EventArgs e)
        {
            ChangeNzbFolder();
        }

        private bool ChangeTvRootFolder()
        {
            bool result = false;

            tvRootDialog.ShowDialog(this);

            if (!String.IsNullOrEmpty(tvRootDialog.SelectedPath))
            {
                if (!txtTvRoot.Text.Contains(tvRootDialog.SelectedPath))
                {
                    txtTvRoot.Text = txtTvRoot.Text + ";" + tvRootDialog.SelectedPath;
                    txtTvRoot.Text = txtTvRoot.Text.TrimStart(';', ' ').TrimEnd(';', ' ');
                    result = true;
                }
            }

            return result;
        }

        private bool ChangeNzbFolder()
        {
            bool result = false;

            nzbDirDialog.ShowDialog(this);

            if (!String.IsNullOrEmpty(nzbDirDialog.SelectedPath))
            {
                txtNzbDir.Text = nzbDirDialog.SelectedPath;
                result = true;
            }

            return result;
        }

        private void btnTvRootClear_Click(object sender, EventArgs e)
        {
            txtTvRoot.Text = null;
        }

        private void btnPriorityLow_Click(object sender, EventArgs e)
        {
            txtPriority.Text = "Low";
        }

        private void btnPriorityNormal_Click(object sender, EventArgs e)
        {
            txtPriority.Text = "Normal";
        }

        private void btnPriorityHigh_Click(object sender, EventArgs e)
        {
            txtPriority.Text = "High";
        }

        private void btn30Days_Click(object sender, EventArgs e)
        {
            txtDeleteLogs.Text = "30";
        }

        private void btn60Days_Click(object sender, EventArgs e)
        {
            txtDeleteLogs.Text = "60";
        }

        private void btn120Days_Click(object sender, EventArgs e)
        {
            txtDeleteLogs.Text = "120";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //Save and Close
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Close
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //Save but Leave Open
            SaveSettings(); //Save the Configuration
            btnApply.Enabled = false;
        }
    }
}
