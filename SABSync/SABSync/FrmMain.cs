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
    public class FrmMain : System.Windows.Forms.Form
    {
        private NotifyIcon notifyIconTray;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip contextMenuStripTray;
        private ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.Timer timerSync;
        private TabControl tabControlMain;
        private TabPage tabShows;
        private TabPage tabHistory;
        private MenuStrip menuStripMain;
        private ToolStripMenuItem toolStripMenuItemFile;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItemHelp;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem toolStripMenuItemRun;
        private ToolStripMenuItem contentsToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem1;
        private TabPage tabPageFeeds;
        private StatusStrip statusMain;
        private ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Timer timerUpdateCache;
        private ToolStripMenuItem donateToolStripMenuItem;
        private ToolStripMenuItem websiteToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanelShows;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelHistory;
        private TableLayoutPanel tableLayoutPanelFeeds;
        private BrightIdeasSoftware.ObjectListView objectListViewFeeds;
        private OLVColumn id;
        private OLVColumn name;
        private OLVColumn url;
        private ObjectListView objectListViewHistory;
        private OLVColumn history_show_name;
        private OLVColumn history_season_number;
        private OLVColumn history_episode_number;
        private OLVColumn history_episode_name;
        private OLVColumn history_feed_title;
        private OLVColumn history_quality;
        private OLVColumn history_proper;
        private OLVColumn history_provider;
        private OLVColumn history_date;
        private ObjectListView objectListViewShows;
        private OLVColumn shows_id;
        private OLVColumn shows_show_name;
        private OLVColumn shows_tvdb_id;
        private OLVColumn shows_tvdb_name;
        private OLVColumn shows_quality;
        private OLVColumn shows_ignore_season;
        private OLVColumn shows_aliases;
        private OLVColumn shows_air_day;
        private OLVColumn shows_air_time;
        private OLVColumn shows_run_time;
        private OLVColumn shows_status;
        private OLVColumn shows_poster_url;
        private OLVColumn shows_banner_url;
        private OLVColumn shows_imdb_id;
        private OLVColumn shows_genre;
        private OLVColumn shows_overview;
        private ImageList imageListProvider;
        private TableLayoutPanel tableLayoutPanelButtons;
        private Button btnAddFeed;
        private Button btnDeleteFeeds;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnPurgeHistory;
        private Button btnDeleteHistory;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnScanNewShows;
        private OLVColumn history_id;
        private TabPage tabPageUpcoming;
        private TableLayoutPanel tableLayoutPanelUpcoming;
        private ObjectListView objectListViewUpcoming;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btnRefreshUpcoming;
        private OLVColumn upcoming_show_name;
        private OLVColumn upcoming_season_number;
        private OLVColumn upcoming_episode_number;
        private OLVColumn upcoming_episode_name;
        private OLVColumn upcoming_overview;
        private OLVColumn upcoming_airs;
        private ToolStripMenuItem updateCacheToolStripMenuItem;

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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.notifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSync = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabShows = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelShows = new System.Windows.Forms.TableLayoutPanel();
            this.objectListViewShows = new BrightIdeasSoftware.ObjectListView();
            this.shows_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_show_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_tvdb_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_tvdb_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_quality = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_ignore_season = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_aliases = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_air_day = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_air_time = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_run_time = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_status = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_poster_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_banner_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_imdb_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_genre = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_overview = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnScanNewShows = new System.Windows.Forms.Button();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelHistory = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPurgeHistory = new System.Windows.Forms.Button();
            this.btnDeleteHistory = new System.Windows.Forms.Button();
            this.objectListViewHistory = new BrightIdeasSoftware.ObjectListView();
            this.history_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_show_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_season_number = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_episode_number = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_episode_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_feed_title = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_quality = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_proper = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_provider = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_date = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageListProvider = new System.Windows.Forms.ImageList(this.components);
            this.tabPageFeeds = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelFeeds = new System.Windows.Forms.TableLayoutPanel();
            this.objectListViewFeeds = new BrightIdeasSoftware.ObjectListView();
            this.id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddFeed = new System.Windows.Forms.Button();
            this.btnDeleteFeeds = new System.Windows.Forms.Button();
            this.tabPageUpcoming = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelUpcoming = new System.Windows.Forms.TableLayoutPanel();
            this.objectListViewUpcoming = new BrightIdeasSoftware.ObjectListView();
            this.upcoming_show_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.upcoming_season_number = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.upcoming_episode_number = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.upcoming_episode_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.upcoming_airs = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.upcoming_overview = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefreshUpcoming = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updateCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerUpdateCache = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStripTray.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabShows.SuspendLayout();
            this.tableLayoutPanelShows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewShows)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.tableLayoutPanelHistory.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewHistory)).BeginInit();
            this.tabPageFeeds.SuspendLayout();
            this.tableLayoutPanelFeeds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewFeeds)).BeginInit();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.tabPageUpcoming.SuspendLayout();
            this.tableLayoutPanelUpcoming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewUpcoming)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconTray
            // 
            this.notifyIconTray.ContextMenuStrip = this.contextMenuStripTray;
            this.notifyIconTray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconTray.Icon")));
            this.notifyIconTray.Text = "SABSync Notify";
            this.notifyIconTray.DoubleClick += new System.EventHandler(this.notifyIconTray_DoubleClick);
            // 
            // contextMenuStripTray
            // 
            this.contextMenuStripTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExit});
            this.contextMenuStripTray.Name = "contextMenuStrip1";
            this.contextMenuStripTray.Size = new System.Drawing.Size(93, 26);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // timerSync
            // 
            this.timerSync.Enabled = true;
            this.timerSync.Interval = 900000;
            this.timerSync.Tick += new System.EventHandler(this.timerSync_Tick);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabShows);
            this.tabControlMain.Controls.Add(this.tabHistory);
            this.tabControlMain.Controls.Add(this.tabPageFeeds);
            this.tabControlMain.Controls.Add(this.tabPageUpcoming);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(3, 26);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(978, 483);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabShows
            // 
            this.tabShows.Controls.Add(this.tableLayoutPanelShows);
            this.tabShows.Location = new System.Drawing.Point(4, 22);
            this.tabShows.Name = "tabShows";
            this.tabShows.Padding = new System.Windows.Forms.Padding(3);
            this.tabShows.Size = new System.Drawing.Size(970, 457);
            this.tabShows.TabIndex = 0;
            this.tabShows.Text = "Shows";
            this.tabShows.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelShows
            // 
            this.tableLayoutPanelShows.ColumnCount = 1;
            this.tableLayoutPanelShows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows.Controls.Add(this.objectListViewShows, 0, 0);
            this.tableLayoutPanelShows.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanelShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelShows.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows.Name = "tableLayoutPanelShows";
            this.tableLayoutPanelShows.RowCount = 2;
            this.tableLayoutPanelShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelShows.Size = new System.Drawing.Size(964, 451);
            this.tableLayoutPanelShows.TabIndex = 2;
            // 
            // objectListViewShows
            // 
            this.objectListViewShows.AllColumns.Add(this.shows_id);
            this.objectListViewShows.AllColumns.Add(this.shows_show_name);
            this.objectListViewShows.AllColumns.Add(this.shows_tvdb_id);
            this.objectListViewShows.AllColumns.Add(this.shows_tvdb_name);
            this.objectListViewShows.AllColumns.Add(this.shows_quality);
            this.objectListViewShows.AllColumns.Add(this.shows_ignore_season);
            this.objectListViewShows.AllColumns.Add(this.shows_aliases);
            this.objectListViewShows.AllColumns.Add(this.shows_air_day);
            this.objectListViewShows.AllColumns.Add(this.shows_air_time);
            this.objectListViewShows.AllColumns.Add(this.shows_run_time);
            this.objectListViewShows.AllColumns.Add(this.shows_status);
            this.objectListViewShows.AllColumns.Add(this.shows_poster_url);
            this.objectListViewShows.AllColumns.Add(this.shows_banner_url);
            this.objectListViewShows.AllColumns.Add(this.shows_imdb_id);
            this.objectListViewShows.AllColumns.Add(this.shows_genre);
            this.objectListViewShows.AllColumns.Add(this.shows_overview);
            this.objectListViewShows.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this.objectListViewShows.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.objectListViewShows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.shows_id,
            this.shows_show_name,
            this.shows_tvdb_id,
            this.shows_tvdb_name,
            this.shows_quality,
            this.shows_ignore_season,
            this.shows_aliases,
            this.shows_air_day,
            this.shows_air_time,
            this.shows_status,
            this.shows_genre,
            this.shows_overview});
            this.objectListViewShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewShows.FullRowSelect = true;
            this.objectListViewShows.Location = new System.Drawing.Point(3, 3);
            this.objectListViewShows.MultiSelect = false;
            this.objectListViewShows.Name = "objectListViewShows";
            this.objectListViewShows.ShowGroups = false;
            this.objectListViewShows.Size = new System.Drawing.Size(958, 418);
            this.objectListViewShows.TabIndex = 2;
            this.objectListViewShows.UseAlternatingBackColors = true;
            this.objectListViewShows.UseCompatibleStateImageBehavior = false;
            this.objectListViewShows.View = System.Windows.Forms.View.Details;
            this.objectListViewShows.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.objectListViewShows_CellEditFinishing);
            this.objectListViewShows.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.objectListViewShows_CellEditStarting);
            // 
            // shows_id
            // 
            this.shows_id.AspectName = "id";
            this.shows_id.IsVisible = false;
            this.shows_id.MinimumWidth = 0;
            this.shows_id.Text = "ID";
            this.shows_id.Width = 0;
            // 
            // shows_show_name
            // 
            this.shows_show_name.AspectName = "show_name";
            this.shows_show_name.IsEditable = false;
            this.shows_show_name.MinimumWidth = 70;
            this.shows_show_name.Text = "Show Name";
            this.shows_show_name.Width = 70;
            // 
            // shows_tvdb_id
            // 
            this.shows_tvdb_id.AspectName = "tvdb_id";
            this.shows_tvdb_id.IsEditable = false;
            this.shows_tvdb_id.MinimumWidth = 60;
            this.shows_tvdb_id.Text = "TVDB ID";
            // 
            // shows_tvdb_name
            // 
            this.shows_tvdb_name.AspectName = "tvdb_name";
            this.shows_tvdb_name.IsEditable = false;
            this.shows_tvdb_name.MinimumWidth = 75;
            this.shows_tvdb_name.Text = "TVDB Name";
            this.shows_tvdb_name.Width = 75;
            // 
            // shows_quality
            // 
            this.shows_quality.AspectName = "quality";
            this.shows_quality.MinimumWidth = 45;
            this.shows_quality.Text = "Quality";
            this.shows_quality.Width = 45;
            // 
            // shows_ignore_season
            // 
            this.shows_ignore_season.AspectName = "ignore_season";
            this.shows_ignore_season.MinimumWidth = 85;
            this.shows_ignore_season.Text = "Ignore Season";
            this.shows_ignore_season.Width = 85;
            // 
            // shows_aliases
            // 
            this.shows_aliases.AspectName = "aliases";
            this.shows_aliases.MinimumWidth = 45;
            this.shows_aliases.Text = "Aliases";
            this.shows_aliases.Width = 45;
            // 
            // shows_air_day
            // 
            this.shows_air_day.AspectName = "air_day";
            this.shows_air_day.IsEditable = false;
            this.shows_air_day.MinimumWidth = 50;
            this.shows_air_day.Text = "Air Day";
            this.shows_air_day.Width = 50;
            // 
            // shows_air_time
            // 
            this.shows_air_time.AspectName = "air_time";
            this.shows_air_time.IsEditable = false;
            this.shows_air_time.MinimumWidth = 50;
            this.shows_air_time.Text = "Air Time";
            this.shows_air_time.Width = 50;
            // 
            // shows_run_time
            // 
            this.shows_run_time.AspectName = "run_time";
            this.shows_run_time.DisplayIndex = 9;
            this.shows_run_time.IsEditable = false;
            this.shows_run_time.IsVisible = false;
            this.shows_run_time.MinimumWidth = 60;
            this.shows_run_time.Text = "Run Time";
            // 
            // shows_status
            // 
            this.shows_status.AspectName = "status";
            this.shows_status.IsEditable = false;
            this.shows_status.MinimumWidth = 40;
            this.shows_status.Text = "Status";
            this.shows_status.Width = 50;
            // 
            // shows_poster_url
            // 
            this.shows_poster_url.AspectName = "poster_url";
            this.shows_poster_url.DisplayIndex = 11;
            this.shows_poster_url.IsEditable = false;
            this.shows_poster_url.IsVisible = false;
            this.shows_poster_url.Text = "Poster URL";
            this.shows_poster_url.Width = 0;
            // 
            // shows_banner_url
            // 
            this.shows_banner_url.AspectName = "banner_url";
            this.shows_banner_url.DisplayIndex = 12;
            this.shows_banner_url.IsEditable = false;
            this.shows_banner_url.IsVisible = false;
            this.shows_banner_url.Text = "Banner URL";
            this.shows_banner_url.Width = 0;
            // 
            // shows_imdb_id
            // 
            this.shows_imdb_id.AspectName = "imdb_id";
            this.shows_imdb_id.DisplayIndex = 11;
            this.shows_imdb_id.IsEditable = false;
            this.shows_imdb_id.IsVisible = false;
            this.shows_imdb_id.Text = "IMDB ID";
            this.shows_imdb_id.Width = 0;
            // 
            // shows_genre
            // 
            this.shows_genre.AspectName = "genre";
            this.shows_genre.IsEditable = false;
            this.shows_genre.MinimumWidth = 45;
            this.shows_genre.Text = "Genre";
            this.shows_genre.Width = 45;
            // 
            // shows_overview
            // 
            this.shows_overview.AspectName = "overview";
            this.shows_overview.FillsFreeSpace = true;
            this.shows_overview.IsEditable = false;
            this.shows_overview.MinimumWidth = 80;
            this.shows_overview.Text = "Overview";
            this.shows_overview.Width = 80;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel2.Controls.Add(this.btnScanNewShows, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 424);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(964, 27);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // btnScanNewShows
            // 
            this.btnScanNewShows.Location = new System.Drawing.Point(888, 1);
            this.btnScanNewShows.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnScanNewShows.Name = "btnScanNewShows";
            this.btnScanNewShows.Size = new System.Drawing.Size(71, 26);
            this.btnScanNewShows.TabIndex = 2;
            this.btnScanNewShows.Text = "Scan";
            this.btnScanNewShows.UseVisualStyleBackColor = true;
            this.btnScanNewShows.Click += new System.EventHandler(this.btnScanNewShows_Click);
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.tableLayoutPanelHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(970, 457);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelHistory
            // 
            this.tableLayoutPanelHistory.ColumnCount = 1;
            this.tableLayoutPanelHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHistory.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanelHistory.Controls.Add(this.objectListViewHistory, 0, 0);
            this.tableLayoutPanelHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelHistory.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelHistory.Name = "tableLayoutPanelHistory";
            this.tableLayoutPanelHistory.RowCount = 2;
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelHistory.Size = new System.Drawing.Size(964, 451);
            this.tableLayoutPanelHistory.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.Controls.Add(this.btnPurgeHistory, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteHistory, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 424);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(964, 27);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnPurgeHistory
            // 
            this.btnPurgeHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPurgeHistory.Location = new System.Drawing.Point(888, 1);
            this.btnPurgeHistory.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnPurgeHistory.Name = "btnPurgeHistory";
            this.btnPurgeHistory.Size = new System.Drawing.Size(71, 26);
            this.btnPurgeHistory.TabIndex = 2;
            this.btnPurgeHistory.Text = "Purge";
            this.btnPurgeHistory.UseVisualStyleBackColor = true;
            this.btnPurgeHistory.Click += new System.EventHandler(this.btnPurgeHistory_Click);
            // 
            // btnDeleteHistory
            // 
            this.btnDeleteHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteHistory.Location = new System.Drawing.Point(811, 1);
            this.btnDeleteHistory.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnDeleteHistory.Name = "btnDeleteHistory";
            this.btnDeleteHistory.Size = new System.Drawing.Size(71, 26);
            this.btnDeleteHistory.TabIndex = 3;
            this.btnDeleteHistory.Text = "Delete";
            this.btnDeleteHistory.UseVisualStyleBackColor = true;
            this.btnDeleteHistory.Click += new System.EventHandler(this.btnDeleteHistory_Click);
            // 
            // objectListViewHistory
            // 
            this.objectListViewHistory.AllColumns.Add(this.history_id);
            this.objectListViewHistory.AllColumns.Add(this.history_show_name);
            this.objectListViewHistory.AllColumns.Add(this.history_season_number);
            this.objectListViewHistory.AllColumns.Add(this.history_episode_number);
            this.objectListViewHistory.AllColumns.Add(this.history_episode_name);
            this.objectListViewHistory.AllColumns.Add(this.history_feed_title);
            this.objectListViewHistory.AllColumns.Add(this.history_quality);
            this.objectListViewHistory.AllColumns.Add(this.history_proper);
            this.objectListViewHistory.AllColumns.Add(this.history_provider);
            this.objectListViewHistory.AllColumns.Add(this.history_date);
            this.objectListViewHistory.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this.objectListViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.history_id,
            this.history_show_name,
            this.history_season_number,
            this.history_episode_number,
            this.history_episode_name,
            this.history_feed_title,
            this.history_quality,
            this.history_proper,
            this.history_provider,
            this.history_date});
            this.objectListViewHistory.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewHistory.FullRowSelect = true;
            this.objectListViewHistory.LargeImageList = this.imageListProvider;
            this.objectListViewHistory.Location = new System.Drawing.Point(3, 3);
            this.objectListViewHistory.MultiSelect = false;
            this.objectListViewHistory.Name = "objectListViewHistory";
            this.objectListViewHistory.OwnerDraw = true;
            this.objectListViewHistory.ShowCommandMenuOnRightClick = true;
            this.objectListViewHistory.ShowGroups = false;
            this.objectListViewHistory.ShowImagesOnSubItems = true;
            this.objectListViewHistory.ShowItemCountOnGroups = true;
            this.objectListViewHistory.ShowItemToolTips = true;
            this.objectListViewHistory.Size = new System.Drawing.Size(958, 418);
            this.objectListViewHistory.SmallImageList = this.imageListProvider;
            this.objectListViewHistory.TabIndex = 0;
            this.objectListViewHistory.UseAlternatingBackColors = true;
            this.objectListViewHistory.UseCompatibleStateImageBehavior = false;
            this.objectListViewHistory.UseFiltering = true;
            this.objectListViewHistory.UseHotItem = true;
            this.objectListViewHistory.UseHyperlinks = true;
            this.objectListViewHistory.UseSubItemCheckBoxes = true;
            this.objectListViewHistory.View = System.Windows.Forms.View.Details;
            // 
            // history_id
            // 
            this.history_id.AspectName = "Id";
            this.history_id.IsVisible = false;
            this.history_id.Text = "ID";
            this.history_id.Width = 0;
            // 
            // history_show_name
            // 
            this.history_show_name.AspectName = "ShowName";
            this.history_show_name.MinimumWidth = 70;
            this.history_show_name.Text = "Show Name";
            this.history_show_name.Width = 70;
            // 
            // history_season_number
            // 
            this.history_season_number.AspectName = "SeasonNumber";
            this.history_season_number.MinimumWidth = 90;
            this.history_season_number.Text = "Season Number";
            this.history_season_number.Width = 90;
            // 
            // history_episode_number
            // 
            this.history_episode_number.AspectName = "EpisodeNumber";
            this.history_episode_number.MinimumWidth = 90;
            this.history_episode_number.Text = "Episode Number";
            this.history_episode_number.Width = 90;
            // 
            // history_episode_name
            // 
            this.history_episode_name.AspectName = "EpisodeName";
            this.history_episode_name.MinimumWidth = 85;
            this.history_episode_name.Text = "Episode Name";
            this.history_episode_name.Width = 85;
            // 
            // history_feed_title
            // 
            this.history_feed_title.AspectName = "FeedTitle";
            this.history_feed_title.MinimumWidth = 60;
            this.history_feed_title.Text = "Feed Title";
            // 
            // history_quality
            // 
            this.history_quality.AspectName = "Quality";
            this.history_quality.MinimumWidth = 45;
            this.history_quality.Text = "Quality";
            this.history_quality.Width = 45;
            // 
            // history_proper
            // 
            this.history_proper.AspectName = "Proper";
            this.history_proper.MinimumWidth = 45;
            this.history_proper.Text = "Proper";
            this.history_proper.Width = 45;
            // 
            // history_provider
            // 
            this.history_provider.AspectName = "Provider";
            this.history_provider.MinimumWidth = 70;
            this.history_provider.Text = "Provider";
            this.history_provider.Width = 70;
            // 
            // history_date
            // 
            this.history_date.AspectName = "Date";
            this.history_date.FillsFreeSpace = true;
            this.history_date.MinimumWidth = 130;
            this.history_date.Text = "Date";
            this.history_date.Width = 130;
            // 
            // imageListProvider
            // 
            this.imageListProvider.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProvider.ImageStream")));
            this.imageListProvider.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListProvider.Images.SetKeyName(0, "nzbmatrix");
            this.imageListProvider.Images.SetKeyName(1, "nzbs");
            this.imageListProvider.Images.SetKeyName(2, "nzbsrus.com.png");
            this.imageListProvider.Images.SetKeyName(3, "newzbin");
            this.imageListProvider.Images.SetKeyName(4, "favicon.ico");
            // 
            // tabPageFeeds
            // 
            this.tabPageFeeds.Controls.Add(this.tableLayoutPanelFeeds);
            this.tabPageFeeds.Location = new System.Drawing.Point(4, 22);
            this.tabPageFeeds.Name = "tabPageFeeds";
            this.tabPageFeeds.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeeds.Size = new System.Drawing.Size(970, 460);
            this.tabPageFeeds.TabIndex = 2;
            this.tabPageFeeds.Text = "RSS Feeds";
            this.tabPageFeeds.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelFeeds
            // 
            this.tableLayoutPanelFeeds.ColumnCount = 1;
            this.tableLayoutPanelFeeds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFeeds.Controls.Add(this.objectListViewFeeds, 0, 0);
            this.tableLayoutPanelFeeds.Controls.Add(this.tableLayoutPanelButtons, 0, 1);
            this.tableLayoutPanelFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFeeds.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelFeeds.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelFeeds.Name = "tableLayoutPanelFeeds";
            this.tableLayoutPanelFeeds.RowCount = 2;
            this.tableLayoutPanelFeeds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFeeds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelFeeds.Size = new System.Drawing.Size(964, 454);
            this.tableLayoutPanelFeeds.TabIndex = 1;
            // 
            // objectListViewFeeds
            // 
            this.objectListViewFeeds.AllColumns.Add(this.id);
            this.objectListViewFeeds.AllColumns.Add(this.name);
            this.objectListViewFeeds.AllColumns.Add(this.url);
            this.objectListViewFeeds.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this.objectListViewFeeds.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.objectListViewFeeds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.name,
            this.url});
            this.objectListViewFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewFeeds.FullRowSelect = true;
            this.objectListViewFeeds.Location = new System.Drawing.Point(3, 3);
            this.objectListViewFeeds.MultiSelect = false;
            this.objectListViewFeeds.Name = "objectListViewFeeds";
            this.objectListViewFeeds.ShowGroups = false;
            this.objectListViewFeeds.Size = new System.Drawing.Size(958, 421);
            this.objectListViewFeeds.TabIndex = 0;
            this.objectListViewFeeds.UseAlternatingBackColors = true;
            this.objectListViewFeeds.UseCompatibleStateImageBehavior = false;
            this.objectListViewFeeds.View = System.Windows.Forms.View.Details;
            this.objectListViewFeeds.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.objectListViewFeeds_CellEditFinishing);
            // 
            // id
            // 
            this.id.AspectName = "id";
            this.id.IsVisible = false;
            this.id.MaximumWidth = 0;
            this.id.MinimumWidth = 0;
            this.id.Text = "ID";
            this.id.Width = 0;
            // 
            // name
            // 
            this.name.AspectName = "name";
            this.name.MaximumWidth = 200;
            this.name.MinimumWidth = 200;
            this.name.Text = "Name";
            this.name.Width = 200;
            // 
            // url
            // 
            this.url.AspectName = "url";
            this.url.FillsFreeSpace = true;
            this.url.Text = "URL";
            this.url.Width = 400;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.ColumnCount = 3;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanelButtons.Controls.Add(this.btnAddFeed, 2, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnDeleteFeeds, 1, 0);
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 427);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(964, 27);
            this.tableLayoutPanelButtons.TabIndex = 1;
            // 
            // btnAddFeed
            // 
            this.btnAddFeed.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddFeed.Location = new System.Drawing.Point(888, 1);
            this.btnAddFeed.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnAddFeed.Name = "btnAddFeed";
            this.btnAddFeed.Size = new System.Drawing.Size(71, 26);
            this.btnAddFeed.TabIndex = 2;
            this.btnAddFeed.Text = "Add";
            this.btnAddFeed.UseVisualStyleBackColor = true;
            this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
            // 
            // btnDeleteFeeds
            // 
            this.btnDeleteFeeds.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteFeeds.Location = new System.Drawing.Point(811, 1);
            this.btnDeleteFeeds.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnDeleteFeeds.Name = "btnDeleteFeeds";
            this.btnDeleteFeeds.Size = new System.Drawing.Size(71, 26);
            this.btnDeleteFeeds.TabIndex = 3;
            this.btnDeleteFeeds.Text = "Delete";
            this.btnDeleteFeeds.UseVisualStyleBackColor = true;
            this.btnDeleteFeeds.Click += new System.EventHandler(this.btnDeleteFeeds_Click);
            // 
            // tabPageUpcoming
            // 
            this.tabPageUpcoming.Controls.Add(this.tableLayoutPanelUpcoming);
            this.tabPageUpcoming.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpcoming.Name = "tabPageUpcoming";
            this.tabPageUpcoming.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpcoming.Size = new System.Drawing.Size(970, 455);
            this.tabPageUpcoming.TabIndex = 3;
            this.tabPageUpcoming.Text = "Upcoming";
            this.tabPageUpcoming.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelUpcoming
            // 
            this.tableLayoutPanelUpcoming.ColumnCount = 1;
            this.tableLayoutPanelUpcoming.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUpcoming.Controls.Add(this.objectListViewUpcoming, 0, 0);
            this.tableLayoutPanelUpcoming.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanelUpcoming.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelUpcoming.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelUpcoming.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelUpcoming.Name = "tableLayoutPanelUpcoming";
            this.tableLayoutPanelUpcoming.RowCount = 2;
            this.tableLayoutPanelUpcoming.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelUpcoming.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelUpcoming.Size = new System.Drawing.Size(964, 449);
            this.tableLayoutPanelUpcoming.TabIndex = 2;
            // 
            // objectListViewUpcoming
            // 
            this.objectListViewUpcoming.AllColumns.Add(this.upcoming_show_name);
            this.objectListViewUpcoming.AllColumns.Add(this.upcoming_season_number);
            this.objectListViewUpcoming.AllColumns.Add(this.upcoming_episode_number);
            this.objectListViewUpcoming.AllColumns.Add(this.upcoming_episode_name);
            this.objectListViewUpcoming.AllColumns.Add(this.upcoming_airs);
            this.objectListViewUpcoming.AllColumns.Add(this.upcoming_overview);
            this.objectListViewUpcoming.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            this.objectListViewUpcoming.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.upcoming_show_name,
            this.upcoming_season_number,
            this.upcoming_episode_number,
            this.upcoming_episode_name,
            this.upcoming_airs,
            this.upcoming_overview});
            this.objectListViewUpcoming.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewUpcoming.FullRowSelect = true;
            this.objectListViewUpcoming.Location = new System.Drawing.Point(3, 3);
            this.objectListViewUpcoming.MultiSelect = false;
            this.objectListViewUpcoming.Name = "objectListViewUpcoming";
            this.objectListViewUpcoming.ShowGroups = false;
            this.objectListViewUpcoming.Size = new System.Drawing.Size(958, 416);
            this.objectListViewUpcoming.TabIndex = 0;
            this.objectListViewUpcoming.UseAlternatingBackColors = true;
            this.objectListViewUpcoming.UseCompatibleStateImageBehavior = false;
            this.objectListViewUpcoming.View = System.Windows.Forms.View.Details;
            // 
            // upcoming_show_name
            // 
            this.upcoming_show_name.AspectName = "ShowName";
            this.upcoming_show_name.MinimumWidth = 70;
            this.upcoming_show_name.Text = "Show Name";
            this.upcoming_show_name.Width = 70;
            // 
            // upcoming_season_number
            // 
            this.upcoming_season_number.AspectName = "SeasonNumber";
            this.upcoming_season_number.MinimumWidth = 90;
            this.upcoming_season_number.Text = "Season Number";
            this.upcoming_season_number.Width = 90;
            // 
            // upcoming_episode_number
            // 
            this.upcoming_episode_number.AspectName = "EpisodeNumber";
            this.upcoming_episode_number.MinimumWidth = 90;
            this.upcoming_episode_number.Text = "Episode Number";
            this.upcoming_episode_number.Width = 90;
            // 
            // upcoming_episode_name
            // 
            this.upcoming_episode_name.AspectName = "EpisodeName";
            this.upcoming_episode_name.MinimumWidth = 85;
            this.upcoming_episode_name.Text = "Episode Name";
            this.upcoming_episode_name.Width = 85;
            // 
            // upcoming_airs
            // 
            this.upcoming_airs.AspectName = "Airs";
            this.upcoming_airs.MinimumWidth = 85;
            this.upcoming_airs.Text = "Air Date & Time";
            this.upcoming_airs.Width = 85;
            // 
            // upcoming_overview
            // 
            this.upcoming_overview.AspectName = "Overview";
            this.upcoming_overview.FillsFreeSpace = true;
            this.upcoming_overview.Text = "Overview";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel4.Controls.Add(this.btnRefreshUpcoming, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 422);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(964, 27);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnRefreshUpcoming
            // 
            this.btnRefreshUpcoming.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshUpcoming.Location = new System.Drawing.Point(888, 1);
            this.btnRefreshUpcoming.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnRefreshUpcoming.Name = "btnRefreshUpcoming";
            this.btnRefreshUpcoming.Size = new System.Drawing.Size(71, 26);
            this.btnRefreshUpcoming.TabIndex = 2;
            this.btnRefreshUpcoming.Text = "Refresh";
            this.btnRefreshUpcoming.UseVisualStyleBackColor = true;
            this.btnRefreshUpcoming.Click += new System.EventHandler(this.btnRefreshUpcoming_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(984, 23);
            this.menuStripMain.TabIndex = 2;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRun,
            this.toolStripSeparator,
            this.exitToolStripMenuItem1});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemFile.Text = "&File";
            // 
            // toolStripMenuItemRun
            // 
            this.toolStripMenuItemRun.Name = "toolStripMenuItemRun";
            this.toolStripMenuItemRun.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuItemRun.Text = "&Run Sync";
            this.toolStripMenuItemRun.Click += new System.EventHandler(this.toolStripMenuItemRun_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(120, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem1.Text = "E&xit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1,
            this.updateCacheToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.optionsToolStripMenuItem1.Text = "&Options";
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
            // 
            // updateCacheToolStripMenuItem
            // 
            this.updateCacheToolStripMenuItem.Name = "updateCacheToolStripMenuItem";
            this.updateCacheToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.updateCacheToolStripMenuItem.Text = "&Update Cache";
            this.updateCacheToolStripMenuItem.Click += new System.EventHandler(this.updateCacheToolStripMenuItem_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator5,
            this.donateToolStripMenuItem,
            this.websiteToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItemHelp.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(119, 6);
            // 
            // donateToolStripMenuItem
            // 
            this.donateToolStripMenuItem.Name = "donateToolStripMenuItem";
            this.donateToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.donateToolStripMenuItem.Text = "Donate";
            this.donateToolStripMenuItem.Click += new System.EventHandler(this.donateToolStripMenuItem_Click);
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.websiteToolStripMenuItem.Text = "Website";
            this.websiteToolStripMenuItem.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusMain.Location = new System.Drawing.Point(0, 512);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(984, 20);
            this.statusMain.TabIndex = 3;
            this.statusMain.Text = "Status Strip...";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(53, 15);
            this.StatusStripLabel.Text = "SABSync";
            // 
            // timerUpdateCache
            // 
            this.timerUpdateCache.Interval = 21600000;
            this.timerUpdateCache.Tick += new System.EventHandler(this.timerUpdateCache_Tick);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.menuStripMain, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.statusMain, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.tabControlMain, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(984, 532);
            this.tableLayoutPanelMain.TabIndex = 4;
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(984, 532);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FrmMain";
            this.Text = "SABSync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.Program_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.contextMenuStripTray.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabShows.ResumeLayout(false);
            this.tableLayoutPanelShows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewShows)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.tableLayoutPanelHistory.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewHistory)).EndInit();
            this.tabPageFeeds.ResumeLayout(false);
            this.tableLayoutPanelFeeds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewFeeds)).EndInit();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.tabPageUpcoming.ResumeLayout(false);
            this.tableLayoutPanelUpcoming.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewUpcoming)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

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
    }
}