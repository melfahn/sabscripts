namespace SABSync
{
    partial class FrmMain
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
            this.updateCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnDeleteShows = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteShows, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnScanNewShows, 2, 0);
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
            this.objectListViewHistory.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
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
            this.tabPageFeeds.Size = new System.Drawing.Size(970, 457);
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
            this.tableLayoutPanelFeeds.Size = new System.Drawing.Size(964, 451);
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
            this.objectListViewFeeds.Size = new System.Drawing.Size(958, 418);
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
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 424);
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
            this.tabPageUpcoming.Size = new System.Drawing.Size(970, 457);
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
            this.tableLayoutPanelUpcoming.Size = new System.Drawing.Size(964, 451);
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
            this.objectListViewUpcoming.Size = new System.Drawing.Size(958, 418);
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 424);
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
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 19);
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
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateCacheToolStripMenuItem,
            this.viewLogsToolStripMenuItem,
            this.optionsToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 19);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // updateCacheToolStripMenuItem
            // 
            this.updateCacheToolStripMenuItem.Name = "updateCacheToolStripMenuItem";
            this.updateCacheToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.updateCacheToolStripMenuItem.Text = "&Update Cache";
            this.updateCacheToolStripMenuItem.Click += new System.EventHandler(this.updateCacheToolStripMenuItem_Click);
            // 
            // viewLogsToolStripMenuItem
            // 
            this.viewLogsToolStripMenuItem.Name = "viewLogsToolStripMenuItem";
            this.viewLogsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.viewLogsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.viewLogsToolStripMenuItem.Text = "&View Logs";
            this.viewLogsToolStripMenuItem.Click += new System.EventHandler(this.viewLogsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.optionsToolStripMenuItem1.Text = "&Options";
            this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
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
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 19);
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
            // btnDeleteShows
            // 
            this.btnDeleteShows.Location = new System.Drawing.Point(5, 1);
            this.btnDeleteShows.Margin = new System.Windows.Forms.Padding(5, 1, 1, 0);
            this.btnDeleteShows.Name = "btnDeleteShows";
            this.btnDeleteShows.Size = new System.Drawing.Size(71, 26);
            this.btnDeleteShows.TabIndex = 3;
            this.btnDeleteShows.Text = "Delete";
            this.btnDeleteShows.UseVisualStyleBackColor = true;
            this.btnDeleteShows.Click += new System.EventHandler(this.btnDeleteShows_Click);
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

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconTray;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.Timer timerSync;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabShows;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRun;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.TabPage tabPageFeeds;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Timer timerUpdateCache;
        private System.Windows.Forms.ToolStripMenuItem donateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFeeds;
        private BrightIdeasSoftware.ObjectListView objectListViewFeeds;
        private BrightIdeasSoftware.OLVColumn id;
        private BrightIdeasSoftware.OLVColumn name;
        private BrightIdeasSoftware.OLVColumn url;
        private BrightIdeasSoftware.ObjectListView objectListViewHistory;
        private BrightIdeasSoftware.OLVColumn history_show_name;
        private BrightIdeasSoftware.OLVColumn history_season_number;
        private BrightIdeasSoftware.OLVColumn history_episode_number;
        private BrightIdeasSoftware.OLVColumn history_episode_name;
        private BrightIdeasSoftware.OLVColumn history_feed_title;
        private BrightIdeasSoftware.OLVColumn history_quality;
        private BrightIdeasSoftware.OLVColumn history_proper;
        private BrightIdeasSoftware.OLVColumn history_provider;
        private BrightIdeasSoftware.OLVColumn history_date;
        private BrightIdeasSoftware.ObjectListView objectListViewShows;
        private BrightIdeasSoftware.OLVColumn shows_id;
        private BrightIdeasSoftware.OLVColumn shows_show_name;
        private BrightIdeasSoftware.OLVColumn shows_tvdb_id;
        private BrightIdeasSoftware.OLVColumn shows_tvdb_name;
        private BrightIdeasSoftware.OLVColumn shows_quality;
        private BrightIdeasSoftware.OLVColumn shows_ignore_season;
        private BrightIdeasSoftware.OLVColumn shows_aliases;
        private BrightIdeasSoftware.OLVColumn shows_air_day;
        private BrightIdeasSoftware.OLVColumn shows_air_time;
        private BrightIdeasSoftware.OLVColumn shows_run_time;
        private BrightIdeasSoftware.OLVColumn shows_status;
        private BrightIdeasSoftware.OLVColumn shows_poster_url;
        private BrightIdeasSoftware.OLVColumn shows_banner_url;
        private BrightIdeasSoftware.OLVColumn shows_imdb_id;
        private BrightIdeasSoftware.OLVColumn shows_genre;
        private BrightIdeasSoftware.OLVColumn shows_overview;
        private System.Windows.Forms.ImageList imageListProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button btnAddFeed;
        private System.Windows.Forms.Button btnDeleteFeeds;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnPurgeHistory;
        private System.Windows.Forms.Button btnDeleteHistory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnScanNewShows;
        private BrightIdeasSoftware.OLVColumn history_id;
        private System.Windows.Forms.TabPage tabPageUpcoming;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelUpcoming;
        private BrightIdeasSoftware.ObjectListView objectListViewUpcoming;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnRefreshUpcoming;
        private BrightIdeasSoftware.OLVColumn upcoming_show_name;
        private BrightIdeasSoftware.OLVColumn upcoming_season_number;
        private BrightIdeasSoftware.OLVColumn upcoming_episode_number;
        private BrightIdeasSoftware.OLVColumn upcoming_episode_name;
        private BrightIdeasSoftware.OLVColumn upcoming_overview;
        private BrightIdeasSoftware.OLVColumn upcoming_airs;
        private System.Windows.Forms.ToolStripMenuItem updateCacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogsToolStripMenuItem;
        private System.Windows.Forms.Button btnDeleteShows;




    }
}