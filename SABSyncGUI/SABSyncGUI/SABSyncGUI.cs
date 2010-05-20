using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace SABSyncGUI
{
    public partial class SABSyncGUI : Form
    {
        public SABSyncGUI()
        {
            InitializeComponent();
            UpdateBindedUi();
        }

        private void UpdateBindedUi()
        {
            if (File.Exists("SABSync.exe") && File.Exists("SABSync.exe.config"))
            {
                string[] sabInfoSplit = Settings.SabnzbdInfo.Split(':');

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

                txtTvRoot.Text = Settings.TvRootPath;
                txtTvTemplate.Text = Settings.TvTemplate;
                txtTvDailyTemplate.Text = Settings.TvDailyTemplate;
                txtVideoExt.Text = Settings.VideoExt;
                txtIgnoreSeasons.Text = Settings.IgnoreSeasons;
                txtNzbDir.Text = Settings.NzbDir;
                txtSabInfoHost.Text = hostname;
                txtSabInfoPort.Text = port;
                txtUsername.Text = Settings.Username;
                txtPassword.Text = Settings.Password;
                txtApiKey.Text = Settings.ApiKey;
                txtPriority.Text = Settings.Priority;
                txtRssConfig.Text = Settings.RssConfig;
                txtAliasConfig.Text = Settings.AliasConfig;
                txtQualityConfig.Text = Settings.QualityConfig;
                txtDownloadQuality.Text = Settings.DownloadQuality;
                txtDeleteLogs.Text = Settings.DeleteLogs;
                chkReplaceChars.Checked = Convert.ToBoolean(Settings.SabReplaceChars);
                chkVerboseLogging.Checked = Convert.ToBoolean(Settings.VerboseLogging);
                chkDownloadPropers.Checked = Convert.ToBoolean(Settings.DownloadPropers);

                //Get Priority as a String
                ; ; string priority = Settings.Priority;

                if (priority == "-1")
                    txtPriority.Text = "Low";

                if (priority == "0")
                    txtPriority.Text = "Normal";

                if (priority == "1")
                    txtPriority.Text = "High";
            }

            else
            {
                //statusStripLabel.Text = "Missing SABSync.exe, must be run from the same Directory!";
                if (MessageBox.Show("Missing SABSync.exe, must be run from the same Directory as SABSync.exe", "Error Loading Config", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    statusStripLabel.Text = "Please close and copy to the folder containing SABSync.exe";
                }
            }
        }

        private void SaveConfigFiles()
        {
            try
            {
                File.WriteAllText(txtRssConfig.Text, txtRssDotConfig.Text);
                File.WriteAllText(txtAliasConfig.Text, txtAliasDotConfig.Text);
                File.WriteAllText(txtQualityConfig.Text, txtQualityDotConfig.Text);

                statusStripLabel.Text = "Config files have been saved!";
            }

            catch
            {
                statusStripLabel.Text = "Error saving config files...";
            }
        }

        private void SaveGeneralSettings()
        {
            Settings.TvRootPath = txtTvRoot.Text;
            Settings.TvTemplate = txtTvTemplate.Text;
            Settings.TvDailyTemplate = txtTvDailyTemplate.Text;
            Settings.VideoExt = txtVideoExt.Text;
            Settings.IgnoreSeasons = txtIgnoreSeasons.Text;
            Settings.NzbDir = txtNzbDir.Text;
            Settings.SabnzbdInfo = SabInfoJoin(txtSabInfoHost.Text, txtSabInfoPort.Text);
            Settings.Username = txtUsername.Text;
            Settings.Password = txtPassword.Text;
            Settings.ApiKey = txtApiKey.Text;
            Settings.RssConfig = txtRssConfig.Text;
            Settings.AliasConfig = txtAliasConfig.Text;
            Settings.QualityConfig = txtQualityConfig.Text;
            Settings.DownloadQuality = txtDownloadQuality.Text;
            Settings.SabReplaceChars = Convert.ToString(chkReplaceChars.Checked);
            Settings.VerboseLogging = Convert.ToString(chkVerboseLogging.Checked);
            Settings.DownloadPropers = Convert.ToString(chkDownloadPropers.Checked);
            Settings.DeleteLogs = txtDeleteLogs.Text;

            //Save Priority
            string priority = txtPriority.Text;

            if (priority.ToLower() == "low")
                Settings.Priority = "-1";

            if (priority.ToLower() == "normal")
                Settings.Priority = "0";

            if (priority.ToLower() == "high")
                Settings.Priority = "1";

            else
                Settings.Priority = "0";

            statusStripLabel.Text = "Settings have been saved!";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGeneralSettings();
        }

        private static string TestConnection(string hostname, string port, string apiKey, string username, string password)
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

        private void statusBarClear(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABSync GUI - Mouse over labels for more info!";
        }

        private void chkReplaceChars_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SAB is Replacing Illegal Characters = Checked";
        }

        private void chkVerboseLogging_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Check to enable Verbose Logging for SABSync";
        }

        private void chkDownloadPropers_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Check to enable SABSync to download PROPER releases";
        }

        private void tvRootBrowse_Click(object sender, EventArgs e)
        {
            ChangeTvRootFolder();
        }

        public void nzbDirBrowse_Click(object sender, EventArgs e)
        {
            ChangeNzbFolder();
        }

        private string SabInfoJoin(string hostname, string port)
        {
            string sabInfo = hostname + ":" + port;
            return sabInfo;
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

        private void btnTvRootBrowse_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for the TV Root Directory";
        }

        private void btnTvRootClear_Click(object sender, EventArgs e)
        {
            txtTvRoot.Text = null;
        }

        private void lblTvRoot_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Where are your TV Shows Located?";
        }

        private void btnNzbDir_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for NZB Directory";
        }

        private void lblNzbDir_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Imported NZB Directory";
        }

        private void lblTvTemplate_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Copy+Paste from SAB's TV Sorting Config";
        }

        private void lblTvDailyTemplate_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Copy+Paste from SAB's Daily TV Sorting Config";
        }

        private void lblVideoExt_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Extensions that should be considered Video Files";
        }

        private void lblSabInfoHostname_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Hostname/IP Address for SABnzbd";
        }

        private void lblSabInfoPort_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Port for SABnzbd";
        }

        private void lblUsername_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABnzbd Username";
        }

        private void lblPassword_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABnzbd Password";
        }

        private void lblApiKey_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "SABnzbd API Key (Config-General)";
        }

        private void lblPriority_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "The Priority NZBs should be added with";
        }

        private void lblIgnoreSeasons_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Shows & Seasons to Ignore";
        }

        private void btnRssConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for RSS Config";
        }

        private void lblRssConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "RSS Configuration File";
        }

        private void btnRssConfig_Click(object sender, EventArgs e)
        {
        OpenFileDialog fdlg = new OpenFileDialog();
        fdlg.Title = "C# Corner Open File Dialog" ;
        fdlg.InitialDirectory = @"c:\" ;
        fdlg.Filter = "Config Files (*.config)|*.config|All files (*.*)|*.*" ;
        fdlg.FilterIndex = 1 ;
        fdlg.RestoreDirectory = true ;

        if(fdlg.ShowDialog() == DialogResult.OK)
        {
            txtRssConfig.Text = fdlg.FileName ;
        }
        }

        private void btnTestSab_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Test current settings to connect to SABnzbd";
        }

        private void btnTestSab_Click(object sender, EventArgs e)
        {
            string response = TestConnection(txtSabInfoHost.Text, txtSabInfoPort.Text, txtApiKey.Text, txtUsername.Text, txtPassword.Text);
            statusStripLabel.Text = response;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            UpdateBindedUi();
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Reload settings from Config File";
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

        private void btnAliasConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for Alias onfig";
        }

        private void lblAliasConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Alias Configuration File";
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

        private void btnQualityConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Click to Browse for Quality Config";
        }

        private void lblQualityConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Quality Configuration File";
        }

        private void lblDownloadQuality_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Default Download Quality for Episodes - xvid;720p (one or both)";
        }

        private void txtAliasDotConfig_TextChanged(object sender, EventArgs e)
        {

        }

        private void TabControl1_Selected(Object sender, TabControlEventArgs e)
        {
            if (e.TabPageIndex != 1) //Save Config Files
                SaveConfigFiles();

            if (e.TabPageIndex != 0)  //Save General Settings & Create Config Files
            {
                SaveGeneralSettings();

                lblRssDotConfig.Text = txtRssConfig.Text + ":";
                lblAliasDotConfig.Text = txtAliasConfig.Text + ":";
                lblQualityDotConfig.Text = txtQualityConfig.Text + ":";

                if (File.Exists(txtRssConfig.Text))
                    txtRssDotConfig.Text = File.ReadAllText(txtRssConfig.Text);

                else
                {
                    StreamWriter SW;
                    SW = File.CreateText(txtRssConfig.Text);
                    SW.WriteLine("Name|URL");
                    SW.Close();
                    txtRssDotConfig.Text = File.ReadAllText(txtRssConfig.Text);
                }

                if (File.Exists(txtAliasConfig.Text))
                    txtAliasDotConfig.Text = File.ReadAllText(txtAliasConfig.Text);

                else
                {
                    StreamWriter SW;
                    SW = File.CreateText(txtAliasConfig.Text);
                    SW.WriteLine("Scene Name|TheTVDB Name");
                    SW.Close();
                    txtAliasDotConfig.Text = File.ReadAllText(txtAliasConfig.Text);
                }
                
                if (File.Exists(txtQualityConfig.Text))
                    txtQualityDotConfig.Text = File.ReadAllText(txtQualityConfig.Text);

                else
                {
                    StreamWriter SW;
                    SW = File.CreateText(txtQualityConfig.Text);
                    SW.WriteLine("Show Name|720p");
                    SW.WriteLine("Show Name|xvid");
                    SW.Close();
                    txtQualityDotConfig.Text = File.ReadAllText(txtQualityConfig.Text);
                }
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            SaveConfigFiles();
        }

        private void btnResetConfig_Click(object sender, EventArgs e)
        {
            txtRssDotConfig.Text = File.ReadAllText(txtRssConfig.Text);
            txtAliasDotConfig.Text = File.ReadAllText(txtAliasConfig.Text);
            txtQualityDotConfig.Text = File.ReadAllText(txtQualityConfig.Text);
        }

        private void lblRssDotConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Name and URL, separated by a PIPE \"|\"";
        }

        private void lblAliasDotConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Bad Name and Good Name, separated by a PIPE \"|\"";
        }

        private void lblQualityDotConfig_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Show Name and Wanted Quality, separated by a PIPE \"|\"";
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

        private void lblDeleteLogs_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Delete Logs after X days";
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

        private void btnCreateTask_Click(object sender, EventArgs e)
        {
            try
            {
                string user = null;

                string computerName = "localhost";
                computerName = Environment.MachineName;

                int time = Convert.ToInt32(numMinutes.Value);

                if (time < 15)
                    time = 15;

                if (time > 60)
                    time = 60;

                if (txtWinUsername.Text.Length == 0)
                    user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                else
                    user = txtWinUsername.Text;

                string password = txtWinPassword.Text;
                int majorVersion = Convert.ToInt32(Environment.OSVersion.Version.Major.ToString());
                string arguments = null;

                if (chkVisbile.Checked)
                {
                    FileInfo location = new FileInfo(new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName + "\\SABSync.exe");

                    if (majorVersion < 6)
                        arguments = String.Format("/create /tn SABSync /tr \"\\\" {0} \\\"\" /sc MINUTE /mo {1} /st 00:00:00 /f /s {2} /ru {3} /rp {4}", location, time, computerName, user, password);
                        
                    else
                        arguments = String.Format("/create /v1 /tn SABSync /tr \"\\\" {0} \\\"\" /sc MINUTE /mo {1} /st 00:00:00 /f /s {2} /ru {3} /rp {4}", location, time, computerName, user, password);
                        
                    txtResult.Text = CreateTask(arguments);
                }

                else
                {
                    FileInfo location = new FileInfo(new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory.FullName + "\\SABSyncHide.exe");

                    if (majorVersion < 6)
                        arguments = String.Format("/create /tn SABSync /tr \"\\\" {0} \\\"\" /sc MINUTE /mo {1} /st 00:00:00 /f /s {2} /ru {3} /rp {4}", location, time, computerName, user, password);

                    else
                        arguments = String.Format("/create /v1 /tn SABSync /tr \"\\\" {0} \\\"\" /sc MINUTE /mo {1} /st 00:00:00 /f /s {2} /ru {3} /rp {4}", location, time, computerName, user, password);

                    txtResult.Text = CreateTask(arguments);
                }
            }

            catch (Exception exc)
            {
                txtResult.Text = "An Error occurred creating the task: " + exc;
            }
        }

        private void btnCreateTask_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Press to Create a Scheduled Task for SABSync";
        }

        private void btnTestSabSync_Click(object sender, EventArgs e)
        {
            string arguments = "/Run /tn SABSync";

            // Start the child process.
            Process proc = new Process();
            // Redirect the output stream of the child process.
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.FileName = "schtasks.exe";
            proc.StartInfo.Arguments = arguments;
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            txtResult.Text = output + Environment.NewLine + error;
            proc.WaitForExit();
        }

        private void btnTestSabSync_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Press to test the SABSync Scheduled Task";
        }

        private static string CreateTask(string arguments)
        {
            // Start the child process.
            Process proc = new Process();
            // Redirect the output stream of the child process.
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.FileName = "schtasks.exe";
            proc.StartInfo.Arguments = arguments;
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            string result = output + Environment.NewLine + error;
            proc.WaitForExit();

            return result;
        }

        private void chkVisbile_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Should the Console Window be visible when the scheduled task runs?";
        }

        private void lblWinUsername_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Windows Username to run Scheduled Task under";
        }

        private void lblWinPassword_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "Password for Windows Username to run Scheduled Task under";
        }

        private void lblRepeatTask_MouseEnter(object sender, EventArgs e)
        {
            statusStripLabel.Text = "How often should the scheduled task run?";
        }

        private void btnRunSabSync_Click(object sender, EventArgs e)
        {
            // Start the child process.
            Process proc = new Process();
            // Redirect the output stream of the child process.
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.FileName = "SABSync.exe";
            proc.Start();
            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            txtOutput.Text = output + Environment.NewLine + error;
            proc.WaitForExit();
        }
    }
}
