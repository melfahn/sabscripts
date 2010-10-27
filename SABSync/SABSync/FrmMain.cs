using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using System.Data.Objects;
using BrightIdeasSoftware;
using SABSync.Properties;

namespace SABSync
{
    partial class FrmMain : Form
    {
        public Config Config = new Config();

        private int _interval;
        private SQLite Sql = new SQLite();
        private Logger Logger = new Logger();
        //private CassiniDev.Server _server;
        private ComboBox comboBoxShows2_quality_multi =  new ComboBox();
        private Label labelShows2_quality_multi = new Label();

        private bool minimizedToTray;

        FrmMain()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            if (!SingleInstance.Start())
            {
                SingleInstance.ShowFirstInstance();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new FrmMain());
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            SingleInstance.Stop();

            //Application.EnableVisualStyles();
            //Application.Run(new FrmMain());
        }

        private void Program_Load(object sender, EventArgs e)
        {
            //_server = new CassiniDev.Server(8081, "/", @"C:\Users\Markus\Documents\Visual Studio 2010\Projects\sabscripts\SABSync\SABSync.Web", true);
            //this.Text = "SABSync";

            this.Text = String.Format("{0} v{1}", App.Name, App.Version); //Set the GUI Task Bar Text

            CreateDatabase(); //Create the Database if needed
            SetSyncInterval(); //Set the Interval for Sync
            if (Config.SyncOnStart) //Run a Sync at the Start if Configured to
                StartSync();

            GetBannersAndUpdates(); //Get Banners and Updates

            GetShows();
            GetShows2();
            GetHistory();
            GetFeeds();
            GetUpcoming();

            LoadGuiSettings(); //Load Previously saved settings for the gui
            shows_id.Width = 0;
            shows_id.IsVisible = false;

            //Used for multi-select in Shows2
            this.comboBoxShows2_quality_multi.Items.AddRange(new object[]
                                                                {
                                                                    "Best Possible",
                                                                    "xvid",
                                                                    "720p"
                                                                });
        }

        public void ShowWindow()
        {
            if (minimizedToTray)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                minimizedToTray = false;
                notifyIconTray.Visible = false;
                ShowInTaskbar = true;
            }

            else
            {
                WinApi.ShowToFront(this.Handle);
            }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
            {
                ShowWindow();
            }
            base.WndProc(ref message);
        }

        private void SetSyncInterval()
        {
            _interval = Config.Interval;
            this.timerSync.Interval = _interval * 60000;
        }

        private void CreateDatabase()
        {
            if (!File.Exists("SABSync.db"))
            {
                Logger.Log("Setting up SABSync Database for the first time");
                Sql.SetupDatabase();
            }
        }

        private void FrmMain_Resize(object sender, EventArgs ex)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                minimizedToTray = true;
                //ShowInTaskbar = false;
                notifyIconTray.Visible = true;
                notifyIconTray.Text = String.Format("{0} v{1}", App.Name, App.Version);
                notifyIconTray.BalloonTipText = "SABSync Minimized to Tray";
                notifyIconTray.ShowBalloonTip(1000, "SABSync", "Minimized to Tray", ToolTipIcon.None);
                Hide();
                //MinimizeToTray();
            }
        }

        private void notifyIconTray_DoubleClick(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            //this.ShowWindow();
            this.Close();
        }

        private void timerSync_Tick(object sender, EventArgs e)
        {
            //Run Sync
            StartSync();
        }

        private void StartSync()
        {
            Thread thread = new Thread(SyncThread);
            thread.Name = "Sync Thread";
            thread.Start();
        }

        private void SyncThread()
        {
            try
            {
                //First Populate the Shows Table
                Database db = new Database();
                db.ProcessingShow += new Database.ProcessingShowHandler(db_ProcessingShow);
                db.ShowsOnDiskToDatabase();

                Stopwatch sw = Stopwatch.StartNew();

                Logger.Log("=====================================================================");
                Logger.Log("Starting {0} v{1} - Build Date: {2:D}", App.Name, App.Version, App.BuildDate);
                Logger.Log("Current System Time: {0}", DateTime.Now);
                Logger.Log("=====================================================================");

                Logger.DeleteLogs();

                var job = new SyncJob();
                job.DbChanged += new SyncJob.DatabaseChangedHandler(UpdateView);
                job.Start();

                sw.Stop();
                Logger.Log("=====================================================================");
                Logger.Log("Process successfully completed. Duration {0:f1}s", sw.Elapsed.TotalSeconds);
                Logger.Log("{0}", DateTime.Now);
            }
            catch (Exception ex)
            {
                Logger.Log("Error: {0}", ex.Message);
                Logger.Log(ex.ToString());
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAbout frmAbout = new frmAbout();
            //frmAbout.Visible = true;
            AboutBox ab = new AboutBox();
            ab.Show();
        }

        private void toolStripMenuItemRun_Click(object sender, EventArgs e)
        {
            StartSync();
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmOptions frmOptions = new FrmOptions();
            frmOptions.StartPosition = FormStartPosition.CenterParent;
            frmOptions.ShowDialog();

            if (frmOptions.DialogResult != DialogResult.OK) //return if Cancel Button was presses
                return;

            Config.ReloadConfig();
            if (_interval != Config.Interval)
                SetSyncInterval();
        }

        private void GetShows()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var shows = from s in sabSyncEntities.shows select s;
                objectListViewShows.SetObjects(shows.ToList());
            }

            shows_quality.AspectToStringConverter = delegate(object obj)
            {
                Int64 quality = (Int64)obj;

                if (quality == 0)
                    return "Best Possible";

                if (quality == 1)
                    return "xvid";

                if (quality == 2)
                    return "720p";

                return "unknown"; //Default to unknown if well... unknown
            };

            //Auto-Size the columns
            //id.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_show_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_tvdb_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_quality.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_aliases.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_air_day.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_air_time.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_status.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_imdb_id.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_genre.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            objectListViewShows.Sort(shows_show_name); //Sort By The 'Show Name' Column
        }

        private void GetShows2()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var shows = from s in sabSyncEntities.shows select s;
                objectListViewShows2.SetObjects(shows.ToList());
            }
            objectListViewShows2.Sort(shows2_show_name); //Sort By The 'Show Name' Column
        }

        private void GetHistory()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var history = from h in sabSyncEntities.histories
                              select
                                  new HistoryObject
                                      {
                                          Id = h.id,
                                          ShowName = h.shows.show_name,
                                          SeasonNumber = h.episodes.season_number,
                                          EpisodeNumber = h.episodes.episode_number,
                                          EpisodeName = h.episodes.episode_name,
                                          FeedTitle = h.feed_title,
                                          Quality = h.quality,
                                          ProperLong = h.proper,
                                          Provider = h.provider,
                                          DateString = h.date
                                      };

                //Add an Image to the Provider Column
                history_provider.ImageGetter = delegate(object rowObject)
                                                   {
                                                       HistoryObject ho = (HistoryObject)rowObject;

                                                       if (ho.Provider == "nzbmatrix")
                                                           return 0;

                                                       if (ho.Provider == "nzbsDotOrg")
                                                           return 1;

                                                       if (ho.Provider == "nzbsrus")
                                                           return 2;

                                                       if (ho.Provider == "nzbmatrix")
                                                           return 3;

                                                       if (ho.Provider == "lilx")
                                                           return 4;

                                                       return -1; //No Image
                                                   };

                history_quality.AspectToStringConverter = delegate(object obj)
                {
                    Int32 quality = (Int32)obj;

                    if (quality == 0)
                        return "Best Possible";

                    if (quality == 1)
                        return "xvid";

                    if (quality == 2)
                        return "720p";

                    return "unknown"; //Default to unknown if well... unknown
                };

                objectListViewHistory.SetObjects(history.ToList());

                history_show_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                history_episode_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                history_feed_title.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                history_date.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                objectListViewFeeds.Sort(history_date, SortOrder.Descending);
            }
        }

        private void GetFeeds()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var feeds = from f in sabSyncEntities.providers select f;
                objectListViewFeeds.SetObjects(feeds.ToList());
            }
        }

        private void GetUpcoming()
        {
            //Get upcoming Shows
            //select from episodes where air_date at today or less than 7 days
            upcoming_airs.AspectToStringConverter = delegate(object obj)
            {
                DateTime dt = (DateTime)obj;
                return dt.ToString("dddd, MMMM dd, yyyy h:mm");
            };

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                DateTime dateYesterday = DateTime.Now.Date.AddDays(-1);
                DateTime dateWeek = DateTime.Now.Date.AddDays(7);


                var shows = from s in sabSyncEntities.episodes.AsEnumerable()
                            where s.air_date != "" && Convert.ToDateTime(s.air_date) >= dateYesterday
                                  && Convert.ToDateTime(s.air_date) < dateWeek &&
                                  s.shows.ignore_season < s.season_number
                            select new UpcomingObject()
                                       {
                                           ShowName = s.shows.show_name,
                                           SeasonNumber = s.season_number,
                                           EpisodeNumber = s.episode_number,
                                           EpisodeName = s.episode_name,
                                           AirDate = s.air_date,
                                           AirTime = s.shows.air_time,
                                           Overview = s.overview
                                       };

                // Group by month-year, rather than date
                this.upcoming_airs.GroupKeyGetter = delegate(object x)
                {
                    string airDate = ((UpcomingObject)x).AirDate;
                    DateTime dt = Convert.ToDateTime(airDate);
                    return new DateTime(dt.Year, dt.Month, dt.Day);
                };

                this.upcoming_airs.GroupKeyToTitleConverter = delegate(object x)
                {
                    DateTime dt = (DateTime)x;
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    DateTime today = DateTime.Now;
                    DateTime tomorrow = DateTime.Now.AddDays(1);

                    if (dt.ToShortDateString().Equals(yesterday.ToShortDateString()))
                        return String.Format("Yesterday ({0})", dt.ToString("MMMM dd, yyyy"));

                    if (dt.ToShortDateString().Equals(today.ToShortDateString()))
                        return String.Format("Today ({0})", dt.ToString("MMMM dd, yyyy"));

                    if (dt.ToShortDateString().Equals(tomorrow.ToShortDateString()))
                        return String.Format("Today ({0})", dt.ToString("MMMM dd, yyyy"));

                    return ((DateTime)x).ToString("MMMM dd, yyyy");
                };

                objectListViewUpcoming.SetObjects(shows.ToList());
            }

            //AutoSize the Columns
            upcoming_show_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            upcoming_season_number.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            upcoming_episode_number.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            upcoming_episode_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            upcoming_airs.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            objectListViewUpcoming.Sort(upcoming_airs); //Sort By The 'Airs' Column
        }

        private void GetBanners()
        {
            Database db = new Database();
            Thread thread = new Thread(db.GetBanners);
            thread.Name = "Get Banners Thread";
            thread.Start();
        }

        private void GetBannersAndUpdates()
        {
            Thread thread = new Thread(GetBannersAndUpdatesThread);
            thread.Name = "Banner/Update Thread";
            thread.Start();
        }

        private void GetBannersAndUpdatesThread()
        {
            Database db = new Database();
            db.GetBanners();
            db.GetTvDbUpdates();
        }

        private void btnScanNewShows_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateShows);
            thread.Start();
        }

        private void UpdateShows()
        {
            //Config.ReloadConfig();
            Database db = new Database();
            db.ProcessingShow += new Database.ProcessingShowHandler(db_ProcessingShow);
            db.ShowsOnDiskToDatabase();
            GetShowsInvoke();
        }

        private void UpdateShows2()
        {
            //Config.ReloadConfig();
            Database db = new Database();
            db.ProcessingShow += new Database.ProcessingShowHandler(db_ProcessingShow);
            db.ShowsOnDiskToDatabase();
            GetShowsInvoke2();
        }

        private void GetShowsInvoke()
        {
            if (this.objectListViewShows.InvokeRequired)
            {
                this.objectListViewShows.BeginInvoke(
                    new MethodInvoker(
                    delegate() { GetShows(); }));
                return;
            }

            GetShows();
        }

        private void GetShowsInvoke2()
        {
            if (this.objectListViewShows2.InvokeRequired)
            {
                this.objectListViewShows2.BeginInvoke(
                    new MethodInvoker(
                    delegate() { GetShows2(); }));
                return;
            }

            GetShows2();
        }

        private void GetHistoryInvoke()
        {
            if (this.objectListViewHistory.InvokeRequired)
            {
                this.objectListViewShows.BeginInvoke(
                    new MethodInvoker(
                    delegate() { GetHistory(); }));
                return;
            }

            GetHistory();
        }

        public void UpdateStatusBarInvoke(string text)
        {
            if (this.statusMain.InvokeRequired)
            {
                this.statusMain.BeginInvoke(
                    new MethodInvoker(
                    delegate() { UpdateStatusBarInvoke(text); }));
            }
            else
            {
                this.statusMain.Items["StatusStripLabel"].Text = text;
            }
        }

        private void timerUpdateCache_Tick(object sender, EventArgs e)
        {
            UpdateCache();
        }

        private void UpdateCache()
        {
            Database db = new Database();
            Thread thread = new Thread(db.GetTvDbUpdates);
            thread.Name = "Update Cache Thread";
            thread.Start();
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/ca/cgi-bin/webscr?cmd=_flow&SESSION=iS-YpSPr5fzHNnbWDLvRHT7aOymnWjYw-CvWj36bR_Thy4d4XTcDfATEqL0&dispatch=5885d80a13c0db1f8e263663d3faee8dc18bca4c6f47e633fcf61b288f5ebea2");
        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/sabscripts/");
        }

        private void objectListViewFeeds_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            string columnName = e.Column.Text.ToLower();
            string oldValue = e.Value.ToString();
            string newValue = e.NewValue.ToString();

            if (e.Cancel)
                return;

            if (columnName.Equals("url", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var feed = (from f in sabSyncEntities.providers where f.url.Equals(oldValue) select f).FirstOrDefault();

                    feed.url = newValue;
                    sabSyncEntities.providers.ApplyCurrentValues(feed);
                    sabSyncEntities.SaveChanges();
                }
            }

            if (columnName.Equals("name", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var feed = (from f in sabSyncEntities.providers where f.name.Equals(oldValue) select f).FirstOrDefault();

                    feed.name = newValue;
                    sabSyncEntities.providers.ApplyCurrentValues(feed);
                    sabSyncEntities.SaveChanges();
                }
            }
        }

        private void btnDeleteFeeds_Click(object sender, EventArgs e)
        {
            //Popup to Confirm, then delete selected row

            if (objectListViewFeeds.SelectedItems.Count != 1)
                return;

            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(objectListViewFeeds.SelectedItem.Text);

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var feed = (from f in sabSyncEntities.providers where f.id == id select f).FirstOrDefault();
                sabSyncEntities.DeleteObject(feed);
                sabSyncEntities.SaveChanges();
            }
            GetFeeds();
        }

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            //Add new feeds from + reload if one was added
            FrmAddFeed frmAddFeed = new FrmAddFeed();
            frmAddFeed.StartPosition = FormStartPosition.CenterParent;
            frmAddFeed.ShowDialog();

            if (frmAddFeed.DialogResult != DialogResult.OK)
                Console.WriteLine(frmAddFeed.DialogResult);

            GetFeeds();
        }

        private void btnDeleteHistory_Click(object sender, EventArgs e)
        {
            //Popup to Confirm, then delete selected row

            if (objectListViewHistory.SelectedItems.Count != 1)
                return;

            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(objectListViewHistory.SelectedItem.Text);

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var item = (from i in sabSyncEntities.histories where i.id == id select i).FirstOrDefault();
                sabSyncEntities.DeleteObject(item);
                sabSyncEntities.SaveChanges();
            }
            GetHistory();
        }

        private void btnPurgeHistory_Click(object sender, EventArgs e)
        {
            //Clear all items in the History

            if (MessageBox.Show("Are you sure?", "Confirm Purge", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var items = from i in sabSyncEntities.histories select i;

                foreach (var item in items)
                    sabSyncEntities.DeleteObject(item);

                sabSyncEntities.SaveChanges();
            }
            GetHistory();
        }

        private void objectListViewShows_CellEditStarting(object sender, CellEditEventArgs e)
        {
            //Return if not the Quality Column
            if (e.Column.Text != "Quality")
                return;

            ComboBox cb = new ComboBox();
            cb.Bounds = e.CellBounds;
            cb.Width = 70;
            cb.Font = ((ObjectListView)sender).Font;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Items.AddRange(new String[] { "Best Possible", "xvid", "720p" });
            cb.SelectedIndex = Convert.ToInt32(e.Value);
            cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
            cb.Tag = e.RowObject; // remember which person we are editing
            e.Control = cb;
        }

        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ((shows)cb.Tag).quality = cb.SelectedIndex;
        }

        private void objectListViewShows_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            //Only work on the Quality Column
            if (e.Column.Text == "Quality")
            {
                long id = ((shows)e.RowObject).id;
                long? quality = ((shows)e.RowObject).quality;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    show.quality = quality;
                    sabSyncEntities.shows.ApplyCurrentValues(show);
                    sabSyncEntities.SaveChanges();
                }

                // Stop listening for change events
                ((ComboBox)e.Control).SelectedIndexChanged -= new EventHandler(cb_SelectedIndexChanged);

                // Any updating will have been down in the SelectedIndexChanged event handler
                // Here we simply make the list redraw the involved ListViewItem
                ((ObjectListView)sender).RefreshItem(e.ListViewItem);

                // We have updated the model object, so we cancel the auto update
                e.Cancel = true;
            }

            if (e.Column.Text == "Ignore Season")
            {
                //Ignore Season - Update DB
                long id = ((shows)e.RowObject).id;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    show.ignore_season = Convert.ToInt32(e.NewValue);
                    sabSyncEntities.shows.ApplyCurrentValues(show);
                    sabSyncEntities.SaveChanges();
                }
                ((ObjectListView)sender).RefreshItem(e.ListViewItem);
            }

            if (e.Column.Text == "Aliases")
            {
                //Ignore Season - Update DB
                long id = ((shows)e.RowObject).id;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    show.aliases = Convert.ToString(e.NewValue);
                    sabSyncEntities.shows.ApplyCurrentValues(show);
                    sabSyncEntities.SaveChanges();
                }
                ((ObjectListView)sender).RefreshItem(e.ListViewItem);
                shows_aliases.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }

        private void btnRefreshUpcoming_Click(object sender, EventArgs e)
        {
            GetUpcoming();
        }

        private void updateCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Updates the Show/Episode Cache
            UpdateCache();
        }

        public void UpdateView(string dbName)
        {
            if (dbName == "history")
                GetHistoryInvoke();
        }

        public void db_ProcessingShow(string message)
        {
            UpdateStatusBarInvoke(message);
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_server.ShutDown();
            SaveGuiSettings();
        }

        private void LoadGuiSettings()
        {
            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();

                //Show the groups (was saved previously to hide them)
                objectListViewUpcoming.ShowGroups = true;
                objectListViewUpcoming.BuildList();
            }

            // Set window location
            if (Settings.Default.WindowLocation != null)
                this.Location = Settings.Default.WindowLocation;

            // Set window size
            if (Settings.Default.WindowSize != null)
                this.Size = Settings.Default.WindowSize;

            //Load OLV Data
            objectListViewShows.RestoreState(Encoding.Default.GetBytes(Settings.Default.olvShows));
            objectListViewHistory.RestoreState(Encoding.Default.GetBytes(Settings.Default.olvHistory));
            objectListViewFeeds.RestoreState(Encoding.Default.GetBytes(Settings.Default.olvFeeds));
            objectListViewUpcoming.RestoreState(Encoding.Default.GetBytes(Settings.Default.olvUpcoming));
        }

        private void SaveGuiSettings()
        {
            //Save the Window Settings
            Settings.Default.WindowLocation = this.Location;

            if (this.WindowState == FormWindowState.Normal) //Copy window size to app settings
                Settings.Default.WindowSize = this.Size;

            else
                Settings.Default.WindowSize = this.RestoreBounds.Size;

            //Save the OLVs
            Settings.Default.olvShows = Encoding.Default.GetString(objectListViewShows.SaveState());
            Settings.Default.olvHistory = Encoding.Default.GetString(objectListViewHistory.SaveState());
            Settings.Default.olvFeeds = Encoding.Default.GetString(objectListViewFeeds.SaveState());
            Settings.Default.olvUpcoming = Encoding.Default.GetString(objectListViewUpcoming.SaveState());

            Settings.Default.Save(); //Save settings to file
        }

        private void viewLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show Logs Window
            FrmLogs frmLogs = new FrmLogs();
            frmLogs.StartPosition = FormStartPosition.CenterParent;
            frmLogs.ShowDialog();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteShows_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                for (int i = 0; i < objectListViewShows.SelectedItems.Count; i++)
                {
                    int id = Convert.ToInt32(objectListViewShows.SelectedItems[i].Text);

                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    var episodes = from ep in sabSyncEntities.episodes where ep.show_id == id select ep;
                    var history = from h in sabSyncEntities.histories where h.show_id == id select h;

                    //Delete each item in history for the selected show
                    foreach (var h in history)
                        sabSyncEntities.DeleteObject(h);

                    //Delete each episode for the selected show
                    foreach (var episode in episodes)
                        sabSyncEntities.DeleteObject(episode);
                    sabSyncEntities.DeleteObject(show); //Delete the show
                }
                sabSyncEntities.SaveChanges(); //Save the changes
            }
            GetShows();
        }

        private void btnSetQualityShows_Click(object sender, EventArgs e)
        {
            if (comboBoxQualityShows.SelectedIndex < 0) //If quality combobox does not have a selected it, return
                return;

            if (objectListViewShows.SelectedItems.Count < 1) //If no shows are selected, return
                return;

            int quality = comboBoxQualityShows.SelectedIndex;

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                for (int i = 0; i < objectListViewShows.SelectedItems.Count; i++)
                {
                    int id = Convert.ToInt32(objectListViewShows.SelectedItems[i].Text);

                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    show.quality = quality;

                    sabSyncEntities.shows.ApplyCurrentValues(show); //Update the show
                }
                sabSyncEntities.SaveChanges(); //Save the changes
            }
            GetShows();

        }

        private void objectListViewShows2_SelectionChanged(object sender, EventArgs e)
        {
            //Create view on left when index is changed
            //if single item selected show more informaion

            if (objectListViewShows2.SelectedItems.Count == 1)
            {
                tableLayoutPanelOverview.Visible = true;
                tableLayoutPanelShows2_show_details.Visible = true;
                labelShows2_tvdb_name.Visible = true;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    //Get all information about the show, grab last and next episode information at some point

                    long id = Convert.ToInt64(objectListViewShows2.SelectedItem.Text);

                    var show =
                        (from s in sabSyncEntities.shows.AsEnumerable()
                         where s.id == id
                         select s).FirstOrDefault();

                    labelShows2_name_value.Text = show.show_name;
                    labelShows2_tvdb_id_value.Text = show.tvdb_id.ToString();
                    labelShows2_tvdb_name.Text = show.tvdb_name;
                    comboBoxShows2_quality.SelectedIndex = (int)show.quality;
                    numericUpDownShows2_ignore_seasons.Value = (int)show.ignore_season;
                    textBoxShows2_aliases.Text = show.aliases;

                    labelShows2_air_day_value.Text = show.air_day;
                    labelShows2_air_time_value.Text = show.air_time;
                    labelShows2_status_value.Text = show.status;
                    labelShows2_genre_value.Text = show.genre.Replace('|', '/');
                    textBoxShows2_overview.Text = show.overview;

                    Stopwatch swAll = new Stopwatch();
                    swAll.Start();

                    Stopwatch swDb = new Stopwatch();
                    swDb.Start();

                    var episodes = from n in sabSyncEntities.episodes
                                where n.shows.id == id select new EpisodeObject
                                {
                                    AirDateString = n.air_date,
                                    SeasonNumber = n.season_number,
                                    EpisodeNumber = n.episode_number,
                                    EpisodeName = n.episode_name,
                                    EpisodeId = n.id
                                };
                   
                    swDb.Stop();
                    Console.WriteLine("DB Query Time: " + swDb.Elapsed.TotalSeconds);

                    Stopwatch swLamba = new Stopwatch();
                    swLamba.Start();

                    List<EpisodeObject> episodeList = new List<EpisodeObject>();
                    episodeList = episodes.ToList();
                    episodeList.Sort();

                    var last = episodeList.FindLast(d => d.AirDate != new DateTime(1,1,1) && d.AirDate < DateTime.Today);
                    var next = episodeList.Find(d => d.AirDate != new DateTime(1, 1, 1) && d.AirDate >= DateTime.Today);

                    if (next != null)
                    {
                        labelShows2_airs_next_date_value.Text = next.AirDate.ToShortDateString();
                        labelShows2_airs_next_season_number_value.Text = next.SeasonNumber.ToString();
                        labelShows2_airs_next_episode_number_value.Text = next.EpisodeNumber.ToString();
                        labelShows2_airs_next_title_value.Text = next.EpisodeName;

                        //If SABSync downloaded this episode show a check mark!
                        if (sabSyncEntities.histories.Any(h => h.episode_id == next.EpisodeId))
                            pictureBoxShows2_next_downloaded.Visible = true;

                        else
                            pictureBoxShows2_next_downloaded.Visible = false;
                    }

                    else
                    {
                        labelShows2_airs_next_date_value.Text = "N/A";
                        labelShows2_airs_next_season_number_value.Text = "N/A";
                        labelShows2_airs_next_episode_number_value.Text = "N/A";
                        labelShows2_airs_next_title_value.Text = "N/A";
                    }

                    if (last != null)
                    {
                        labelShows2_airs_last_date_value.Text = last.AirDate.ToShortDateString();
                        labelShows2_airs_last_season_number_value.Text = last.SeasonNumber.ToString();
                        labelShows2_airs_last_episode_number_value.Text = last.EpisodeNumber.ToString();
                        labelShows2_airs_last_title_value.Text = last.EpisodeName;

                        //If SABSync downloaded this episode show a check mark!
                        if (sabSyncEntities.histories.Any(h => h.episode_id == last.EpisodeId))
                            pictureBoxShows2_last_downloaded.Visible = true;

                        else
                            pictureBoxShows2_last_downloaded.Visible = false;
                    }

                    else
                    {
                        labelShows2_airs_last_date_value.Text = "N/A";
                        labelShows2_airs_last_season_number_value.Text = "N/A";
                        labelShows2_airs_last_episode_number_value.Text = "N/A";
                        labelShows2_airs_last_title_value.Text = "N/A";
                    }

                    swLamba.Stop();
                    Console.WriteLine("Lambda: " + swLamba.Elapsed.TotalSeconds);

                    swAll.Stop();
                    Console.WriteLine("Total Elapsed: " + swAll.Elapsed.TotalSeconds);

                    //Show the Banner!
                    string image = String.Format("Images{0}Banners{1}{2}.jpg", Path.DirectorySeparatorChar,
                                                     Path.DirectorySeparatorChar, objectListViewShows2.SelectedItem.Text);

                    if (File.Exists(image))
                        pictureBoxShows2_banner.Image = Image.FromFile(image);

                    else
                        pictureBoxShows2_banner.Image = global::SABSync.Images.SABSync_Banner;
                }
            }

            if (objectListViewShows2.SelectedItems.Count > 1) //Multiple Selected
            {
                pictureBoxShows2_banner.Image = global::SABSync.Images.SABSync_Banner;

                //Display only Quality (and Ignore seasons options?)
                tableLayoutPanelOverview.Visible = false;
                tableLayoutPanelShows2_show_details.Visible = false;
                labelShows2_tvdb_name.Visible = false;
                
                //Create a new Label
                this.labelShows2_quality_multi.Text = "Quality:";
                this.labelShows2_quality_multi.AutoSize = true;
                this.labelShows2_quality_multi.Location = new Point(13, 50);
                this.labelShows2_quality_multi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //Create a new Combobox
                this.comboBoxShows2_quality_multi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.comboBoxShows2_quality_multi.SelectedIndex = -1;
                this.comboBoxShows2_quality_multi.Location = new Point(70, 45);
                this.comboBoxShows2_quality_multi.Size = new System.Drawing.Size(88, 21);
                this.comboBoxShows2_quality_multi.Visible = true;
                
                //Add the Controls to the Container
                this.groupBoxShows2_details.Controls.Add(labelShows2_quality_multi);
                this.groupBoxShows2_details.Controls.Add(comboBoxShows2_quality_multi);
            }

            else
            {
                //Display some default information
            }
        }

        private void btnShows2_scan_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateShows2);
            thread.Start();
        }

        private void btnShows2_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                for (int i = 0; i < objectListViewShows2.SelectedItems.Count; i++)
                {
                    int id = Convert.ToInt32(objectListViewShows2.SelectedItems[i].Text);

                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    var episodes = from ep in sabSyncEntities.episodes where ep.show_id == id select ep;
                    var history = from h in sabSyncEntities.histories where h.show_id == id select h;

                    //Delete each item in history for the selected show
                    foreach (var h in history)
                        sabSyncEntities.DeleteObject(h);

                    //Delete each episode for the selected show
                    foreach (var episode in episodes)
                        sabSyncEntities.DeleteObject(episode);
                    sabSyncEntities.DeleteObject(show); //Delete the show
                }
                sabSyncEntities.SaveChanges(); //Save the changes
            }
            GetShows2();
        }

        private void buttonShows2_details_save_Click(object sender, EventArgs e)
        {
            if (objectListViewShows2.SelectedItems.Count == 1)
            {
                //showId from selected show
                //Quality Dropbox Index
                //NumberSelect Ignore Season value
                //Aliases

                int showId = Convert.ToInt32(objectListViewShows2.SelectedItem.Text);
                int quality = comboBoxShows2_quality.SelectedIndex;
                int ignoreSeasons = Convert.ToInt32(numericUpDownShows2_ignore_seasons.Value);
                string aliases = textBoxShows2_aliases.Text;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == showId select s).First();

                    show.quality = quality;
                    show.ignore_season = ignoreSeasons;
                    show.aliases = aliases;

                    sabSyncEntities.shows.ApplyCurrentValues(show);
                    sabSyncEntities.SaveChanges();
                }
            }

            if (objectListViewShows2.SelectedItems.Count > 1)
            {
                //Save Quality (and Ignore Seasons?) for all selected shows

                //return if combobox for quality multi does not have a value selected
                if (comboBoxShows2_quality_multi.SelectedIndex < 0)
                    return;

                int quality = comboBoxShows2_quality_multi.SelectedIndex;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    for (int i = 0; i < objectListViewShows2.SelectedItems.Count; i++)
                    {
                        int id = Convert.ToInt32(objectListViewShows2.SelectedItems[i].Text);
                        var show = (from s in sabSyncEntities.shows where s.id == id select s).First();

                        show.quality = quality;
                        sabSyncEntities.shows.ApplyCurrentValues(show);
                    }
                    sabSyncEntities.SaveChanges();
                }
            }

            //Nothing is selected, return
            else
                return;
        }

        private void toolStripMenuItemShows2_list_update_all_Click(object sender, EventArgs e)
        {
            //Update all shows (Forced)
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var seriesIds= from s in sabSyncEntities.shows select s.tvdb_id;

                Database db = new Database();
                Thread dbThread = new Thread(new ThreadStart(delegate { db.UpdateFromTvDb(seriesIds.ToList()); }));
                dbThread.Name = "Update Cache Thread (Forced)";
                dbThread.Start();
            }
        }

        private void toolStripMenuItemShows2_list_update_selected_Click(object sender, EventArgs e)
        {
            //Update selected shows (1+)
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                List<long?> seriesIdList = new List<long?>();
                for (int i = 0 ; i < objectListViewShows2.SelectedItems.Count; i++)
                {
                    int id = Convert.ToInt32(objectListViewShows2.SelectedItems[i].Text);
                    var seriesId = (from s in sabSyncEntities.shows where s.id == id select s.tvdb_id).FirstOrDefault();
                    seriesIdList.Add(seriesId);
                }
                
                Database db = new Database();
                Thread dbThread = new Thread(new ThreadStart(delegate { db.UpdateFromTvDb(seriesIdList); }));
                dbThread.Name = "Update Cache Thread (Selected)";
                dbThread.Start();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Confirm Delete, then delete selected
            if (MessageBox.Show("Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                for (int i = 0; i < objectListViewShows2.SelectedItems.Count; i++)
                {
                    int id = Convert.ToInt32(objectListViewShows2.SelectedItems[i].Text);

                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    var episodes = from ep in sabSyncEntities.episodes where ep.show_id == id select ep;
                    var history = from h in sabSyncEntities.histories where h.show_id == id select h;

                    //Delete each item in history for the selected show
                    foreach (var h in history)
                        sabSyncEntities.DeleteObject(h);

                    //Delete each episode for the selected show
                    foreach (var episode in episodes)
                        sabSyncEntities.DeleteObject(episode);
                    sabSyncEntities.DeleteObject(show); //Delete the show
                }
                sabSyncEntities.SaveChanges(); //Save the changes
            }
            GetShows2();
        }

        private void getBannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectListViewShows2.SelectedItems.Count != 1)
                return;

            long id = Convert.ToInt64(objectListViewShows2.SelectedItem.Text);
            int index = objectListViewShows2.SelectedIndex;
            
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var showId = (from s in sabSyncEntities.shows where s.id == id select s.id).FirstOrDefault();

                Database db = new Database();
                db.GetBanner(showId);

                objectListViewShows2.SelectedIndex = 0;
                objectListViewShows2.SelectedIndex = index;
            }
        }
    }
}