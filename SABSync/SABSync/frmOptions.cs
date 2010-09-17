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
        private bool isFormChanged;

        /// <summary>
        /// Sets the form state change handlers.
        /// </summary>
        private void SetFormStateChangeHandlers(Control parent)
        {
            isFormChanged = false;

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
            isFormChanged = true;
            btnApply.Enabled = true;
        }

        public FrmOptions()
        {
            InitializeComponent();
            LoadConfig();
            SetFormStateChangeHandlers(this);
        }

        private void LoadConfig()
        {
            string[] sabInfoSplit = ConfigSettings.SabNzbdInfo.Split(':');

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

            txtTvRoot.Text = ConfigSettings.TvRootPath;
            txtTvTemplate.Text = ConfigSettings.TvTemplate;
            txtTvDailyTemplate.Text = ConfigSettings.TvDailyTemplate;
            txtVideoExt.Text = ConfigSettings.VideoExt;
            txtIgnoreSeasons.Text = ConfigSettings.IgnoreSeasons;
            txtNzbDir.Text = ConfigSettings.NzbDir;
            txtSabInfoHost.Text = hostname;
            txtSabInfoPort.Text = port;
            txtUsername.Text = ConfigSettings.Username;
            txtPassword.Text = ConfigSettings.Password;
            txtApiKey.Text = ConfigSettings.ApiKey;
            txtPriority.Text = ConfigSettings.Priority;
            txtRssConfig.Text = ConfigSettings.RssConfig;
            txtAliasConfig.Text = ConfigSettings.AliasConfig;
            txtQualityConfig.Text = ConfigSettings.QualityConfig;
            txtDownloadQuality.Text = ConfigSettings.DownloadQuality;
            txtDeleteLogs.Text = ConfigSettings.DeleteLogs;
            chkReplaceChars.Checked = Convert.ToBoolean(ConfigSettings.SabReplaceChars);
            chkVerboseLogging.Checked = Convert.ToBoolean(ConfigSettings.VerboseLogging);
            chkDownloadPropers.Checked = Convert.ToBoolean(ConfigSettings.DownloadPropers);
            chkSyncOnStart.Checked = Convert.ToBoolean(ConfigSettings.SyncOnStart);
            numMinutes.Value = Convert.ToInt32(ConfigSettings.Interval);

            //Get Priority as a String
            string priority = ConfigSettings.Priority;

            if (priority == "-1")
                txtPriority.Text = "Low";

            if (priority == "0")
                txtPriority.Text = "Normal";

            if (priority == "1")
                txtPriority.Text = "High";
        }

        private void SaveGeneralSettings()
        {
            ConfigSettings.TvRootPath = txtTvRoot.Text;
            ConfigSettings.TvTemplate = txtTvTemplate.Text;
            ConfigSettings.TvDailyTemplate = txtTvDailyTemplate.Text;
            ConfigSettings.VideoExt = txtVideoExt.Text;
            ConfigSettings.IgnoreSeasons = txtIgnoreSeasons.Text;
            ConfigSettings.NzbDir = txtNzbDir.Text;
            ConfigSettings.SabNzbdInfo = SabInfoJoin(txtSabInfoHost.Text, txtSabInfoPort.Text);
            ConfigSettings.Username = txtUsername.Text;
            ConfigSettings.Password = txtPassword.Text;
            ConfigSettings.ApiKey = txtApiKey.Text;
            ConfigSettings.RssConfig = txtRssConfig.Text;
            ConfigSettings.AliasConfig = txtAliasConfig.Text;
            ConfigSettings.QualityConfig = txtQualityConfig.Text;
            ConfigSettings.DownloadQuality = txtDownloadQuality.Text;
            ConfigSettings.SabReplaceChars = Convert.ToString(chkReplaceChars.Checked);
            ConfigSettings.VerboseLogging = Convert.ToString(chkVerboseLogging.Checked);
            ConfigSettings.DownloadPropers = Convert.ToString(chkDownloadPropers.Checked);
            ConfigSettings.DeleteLogs = txtDeleteLogs.Text;
            ConfigSettings.Interval = Convert.ToString(numMinutes.Value);
            ConfigSettings.SyncOnStart = Convert.ToString(chkSyncOnStart.Checked);

            //Save Priority
            string priority = txtPriority.Text;

            if (priority.ToLower() == "low")
                ConfigSettings.Priority = "-1";

            if (priority.ToLower() == "normal")
                ConfigSettings.Priority = "0";

            if (priority.ToLower() == "high")
                ConfigSettings.Priority = "1";

            else
                ConfigSettings.Priority = "0";
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
                panelConfig.Visible = false;
                panelShows.Visible = false;
                panelGeneral.Width = 449;
                panelGeneral.Height = 315;
                panelGeneral.Location = new Point(140, 13);
                panelGeneral.Visible = true;
            }

            else if (this.treeViewOptions.SelectedNode.Name == "NodeSab")
            {
                panelGeneral.Visible = false;
                panelConfig.Visible = false;
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
                panelConfig.Width = 449;
                panelConfig.Height = 315;
                panelConfig.Location = new Point(140, 13);
                panelConfig.Visible = true;
            }

            else if (this.treeViewOptions.SelectedNode.Name == "NodeShows")
            {
                panelGeneral.Visible = false;
                panelSab.Visible = false;
                panelConfig.Visible = false;
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

                //Settings.TvRootPath = tvRootText.Text;

                result = true;
            }

            return result;
        }

        private void btnTvRootClear_Click(object sender, EventArgs e)
        {
            txtTvRoot.Text = null;
        }

        private void btnRssConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtRssConfig.Text = fdlg.FileName;
            }
        }

        private void btnAliasConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtRssConfig.Text = fdlg.FileName;
            }
        }

        private void btnQualityConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtRssConfig.Text = fdlg.FileName;
            }
        }

        private void btnSd_Click(object sender, EventArgs e)
        {
            if (!txtDownloadQuality.Text.Contains("xvid"))
            {
                txtDownloadQuality.Text = txtDownloadQuality.Text + ";" + "xvid";
                txtDownloadQuality.Text = txtDownloadQuality.Text.TrimStart(';', ' ').TrimEnd(';', ' ');
            }
        }

        private void btnHd_Click(object sender, EventArgs e)
        {
            if (!txtDownloadQuality.Text.Contains("720p"))
            {
                txtDownloadQuality.Text = txtDownloadQuality.Text + ";" + "720p";
                txtDownloadQuality.Text = txtDownloadQuality.Text.TrimStart(';', ' ').TrimEnd(';', ' ');
            }
        }

        private void btnClearDQ_Click(object sender, EventArgs e)
        {
            txtDownloadQuality.Text = null;
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
            SaveGeneralSettings();
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
            SaveGeneralSettings();
        }
    }
}
