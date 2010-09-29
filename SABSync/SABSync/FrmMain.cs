using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        FrmMain()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            
            Application.Run(new FrmMain());
        }

        private void Program_Load(object sender, EventArgs e)
        {
            
            
            this.Text = String.Format("{0} v{1}", App.Name, App.Version); //Set the GUI Task Bar Text

            CreateDatabase(); //Create the Database if needed
            SetSyncInterval(); //Set the Interval for Sync
            if (Config.SyncOnStart) //Run a Sync at the Start if Configured to
                StartSync();

            GetShows();
            GetHistory();
            GetFeeds();
            GetUpcoming();

            LoadGuiSettings(); //Load Previously saved settings for the gui
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
                ShowInTaskbar = false;
                notifyIconTray.Visible = true;
                notifyIconTray.BalloonTipText = "SABSync Minimized to Tray";
                notifyIconTray.ShowBalloonTip(1000, "SABSync", "Minimized to Tray", ToolTipIcon.None);
                Hide();
            }
        }

        private void notifyIconTray_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIconTray.Visible = false;
            ShowInTaskbar = true;
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Close();
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
                db.ProcessingShow +=new Database.ProcessingShowHandler(db_ProcessingShow);
                db.ShowsOnDiskToDatabase();

                Stopwatch sw = Stopwatch.StartNew();

                Logger.Log("=====================================================================");
                Logger.Log("Starting {0} v{1} - Build Date: {2:D}", App.Name, App.Version, App.BuildDate);
                Logger.Log("Current System Time: {0}", DateTime.Now);
                Logger.Log("=====================================================================");

                Logger.DeleteLogs();

                var job = new SyncJob();
                job.DbChanged +=new SyncJob.DatabaseChangedHandler(UpdateView);
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

            //Auto-Size the columns
            id.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_show_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_tvdb_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_aliases.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_air_day.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_air_time.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_status.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_imdb_id.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            shows_genre.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            objectListViewShows.Sort(shows_show_name); //Sort By The 'Show Name' Column
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
                                                       HistoryObject ho = (HistoryObject) rowObject;

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

                objectListViewHistory.SetObjects(history.ToList());

                history_show_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                history_episode_name.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                history_feed_title.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                history_date.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
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
                DateTime dateToday = DateTime.Now.Date;
                DateTime dateWeek = DateTime.Now.Date.AddDays(7);

                var shows = from s in sabSyncEntities.episodes.AsEnumerable()
                           where s.air_date != "" && Convert.ToDateTime(s.air_date) >= dateToday
                           && Convert.ToDateTime(s.air_date) < dateWeek
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

        private void btnScanNewShows_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(UpdateShows);
            thread.Start();
        }

        private void UpdateShows()
        {
            //Config.ReloadConfig();
            Database db = new Database();
            db.ProcessingShow +=new Database.ProcessingShowHandler(db_ProcessingShow);
            db.ShowsOnDiskToDatabase();
            GetShowsInvoke();
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
            cb.Font = ((ObjectListView) sender).Font;
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
                long id = ((shows) e.RowObject).id;
                long? quality = ((shows) e.RowObject).quality;

                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == id select s).FirstOrDefault();
                    show.quality = quality;
                    sabSyncEntities.shows.ApplyCurrentValues(show);
                    sabSyncEntities.SaveChanges();
                }

                // Stop listening for change events
                ((ComboBox) e.Control).SelectedIndexChanged -= new EventHandler(cb_SelectedIndexChanged);

                // Any updating will have been down in the SelectedIndexChanged event handler
                // Here we simply make the list redraw the involved ListViewItem
                ((ObjectListView) sender).RefreshItem(e.ListViewItem);

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
            SaveGuiSettings();
        }

        private void LoadGuiSettings()
        {
            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
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
    }
}