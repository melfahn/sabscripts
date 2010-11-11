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
            this.tabShows2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelShows2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnShows2_scan = new System.Windows.Forms.Button();
            this.splitContainerShows = new System.Windows.Forms.SplitContainer();
            this.objectListViewShows2 = new BrightIdeasSoftware.ObjectListView();
            this.shows2_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows2_show_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStripShows2_list = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemShows2_list_update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShows2_list_update_selected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShows2_list_update_all = new System.Windows.Forms.ToolStripMenuItem();
            this.getBannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelShows2_details_full = new System.Windows.Forms.TableLayoutPanel();
            this.panelShows2Details = new System.Windows.Forms.Panel();
            this.tableLayoutPanelShows2_details = new System.Windows.Forms.TableLayoutPanel();
            this.labelShows2_tvdb_name = new System.Windows.Forms.Label();
            this.groupBoxShows2_details = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelShows2_show_details = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxShows2_details_editable = new System.Windows.Forms.GroupBox();
            this.textBoxShows2_aliases = new System.Windows.Forms.TextBox();
            this.numericUpDownShows2_ignore_seasons = new System.Windows.Forms.NumericUpDown();
            this.labelShows2_tvdb_id_value = new System.Windows.Forms.Label();
            this.comboBoxShows2_quality = new System.Windows.Forms.ComboBox();
            this.labelShows2_name = new System.Windows.Forms.Label();
            this.labelShows2_aliases = new System.Windows.Forms.Label();
            this.labelShows2_quality = new System.Windows.Forms.Label();
            this.labelShows2_ignore_season = new System.Windows.Forms.Label();
            this.labelShows2_tvdb_id = new System.Windows.Forms.Label();
            this.labelShows2_name_value = new System.Windows.Forms.Label();
            this.groupBoxShows2_airs_next = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelShows2_AirsLast = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxShows2_next_downloaded = new System.Windows.Forms.PictureBox();
            this.labelShows2_airs_next_date = new System.Windows.Forms.Label();
            this.labelShows2_airs_next_date_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_next_episode_number = new System.Windows.Forms.Label();
            this.labelShows2_airs_next_episode_number_value = new System.Windows.Forms.Label();
            this.labelShows_airs_next_title = new System.Windows.Forms.Label();
            this.labelShows2_airs_next_season_number_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_next_title_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_next_season_number = new System.Windows.Forms.Label();
            this.groupBoxShows2_airs_last = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxShows2_last_downloaded = new System.Windows.Forms.PictureBox();
            this.labelShows2_airs_last_episode_number = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_episode_number_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_date = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_title = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_season_number_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_date_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_title_value = new System.Windows.Forms.Label();
            this.labelShows2_airs_last_season_number = new System.Windows.Forms.Label();
            this.groupBoxShows2_details_air = new System.Windows.Forms.GroupBox();
            this.labelShows2_status_value = new System.Windows.Forms.Label();
            this.labelShows2_genre_value = new System.Windows.Forms.Label();
            this.labelShows2_air_time_value = new System.Windows.Forms.Label();
            this.labelShows2_air_day_value = new System.Windows.Forms.Label();
            this.labelShows2_air_time = new System.Windows.Forms.Label();
            this.labelShows2_air_day = new System.Windows.Forms.Label();
            this.labelShows2_status = new System.Windows.Forms.Label();
            this.labelShows2_genre = new System.Windows.Forms.Label();
            this.pictureBoxShows2_banner = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanelOverview = new System.Windows.Forms.TableLayoutPanel();
            this.labelShows2_overview = new System.Windows.Forms.Label();
            this.textBoxShows2_overview = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelShows2_details_buttons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonShows2_details_save = new System.Windows.Forms.Button();
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
            this.shows_run_time = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_poster_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_banner_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows_imdb_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.shows2id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
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
            this.contextMenuStripTray.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabShows2.SuspendLayout();
            this.tableLayoutPanelShows2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerShows)).BeginInit();
            this.splitContainerShows.Panel1.SuspendLayout();
            this.splitContainerShows.Panel2.SuspendLayout();
            this.splitContainerShows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewShows2)).BeginInit();
            this.contextMenuStripShows2_list.SuspendLayout();
            this.tableLayoutPanelShows2_details_full.SuspendLayout();
            this.panelShows2Details.SuspendLayout();
            this.tableLayoutPanelShows2_details.SuspendLayout();
            this.groupBoxShows2_details.SuspendLayout();
            this.tableLayoutPanelShows2_show_details.SuspendLayout();
            this.groupBoxShows2_details_editable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShows2_ignore_seasons)).BeginInit();
            this.groupBoxShows2_airs_next.SuspendLayout();
            this.tableLayoutPanelShows2_AirsLast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShows2_next_downloaded)).BeginInit();
            this.groupBoxShows2_airs_last.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShows2_last_downloaded)).BeginInit();
            this.groupBoxShows2_details_air.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShows2_banner)).BeginInit();
            this.tableLayoutPanelOverview.SuspendLayout();
            this.tableLayoutPanelShows2_details_buttons.SuspendLayout();
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
            this.tabControlMain.Controls.Add(this.tabShows2);
            this.tabControlMain.Controls.Add(this.tabHistory);
            this.tabControlMain.Controls.Add(this.tabPageFeeds);
            this.tabControlMain.Controls.Add(this.tabPageUpcoming);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(3, 26);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1078, 563);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabShows2
            // 
            this.tabShows2.Controls.Add(this.tableLayoutPanelShows2);
            this.tabShows2.Location = new System.Drawing.Point(4, 22);
            this.tabShows2.Name = "tabShows2";
            this.tabShows2.Padding = new System.Windows.Forms.Padding(3);
            this.tabShows2.Size = new System.Drawing.Size(1070, 537);
            this.tabShows2.TabIndex = 4;
            this.tabShows2.Text = "Shows";
            this.tabShows2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelShows2
            // 
            this.tableLayoutPanelShows2.ColumnCount = 1;
            this.tableLayoutPanelShows2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanelShows2.Controls.Add(this.splitContainerShows, 0, 0);
            this.tableLayoutPanelShows2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelShows2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows2.Name = "tableLayoutPanelShows2";
            this.tableLayoutPanelShows2.RowCount = 2;
            this.tableLayoutPanelShows2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelShows2.Size = new System.Drawing.Size(1064, 531);
            this.tableLayoutPanelShows2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 288F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel3.Controls.Add(this.btnShows2_scan, 5, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 504);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1064, 27);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // btnShows2_scan
            // 
            this.btnShows2_scan.Location = new System.Drawing.Point(988, 1);
            this.btnShows2_scan.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnShows2_scan.Name = "btnShows2_scan";
            this.btnShows2_scan.Size = new System.Drawing.Size(71, 26);
            this.btnShows2_scan.TabIndex = 2;
            this.btnShows2_scan.Text = "Scan";
            this.btnShows2_scan.UseVisualStyleBackColor = true;
            this.btnShows2_scan.Click += new System.EventHandler(this.btnShows2_scan_Click);
            // 
            // splitContainerShows
            // 
            this.splitContainerShows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerShows.Location = new System.Drawing.Point(3, 3);
            this.splitContainerShows.Name = "splitContainerShows";
            // 
            // splitContainerShows.Panel1
            // 
            this.splitContainerShows.Panel1.Controls.Add(this.objectListViewShows2);
            this.splitContainerShows.Panel1MinSize = 200;
            // 
            // splitContainerShows.Panel2
            // 
            this.splitContainerShows.Panel2.Controls.Add(this.tableLayoutPanelShows2_details_full);
            this.splitContainerShows.Size = new System.Drawing.Size(1058, 498);
            this.splitContainerShows.SplitterDistance = 276;
            this.splitContainerShows.TabIndex = 0;
            // 
            // objectListViewShows2
            // 
            this.objectListViewShows2.AllColumns.Add(this.shows2_id);
            this.objectListViewShows2.AllColumns.Add(this.shows2_show_name);
            this.objectListViewShows2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.shows2_id,
            this.shows2_show_name});
            this.objectListViewShows2.ContextMenuStrip = this.contextMenuStripShows2_list;
            this.objectListViewShows2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewShows2.FullRowSelect = true;
            this.objectListViewShows2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.objectListViewShows2.HideSelection = false;
            this.objectListViewShows2.Location = new System.Drawing.Point(0, 0);
            this.objectListViewShows2.Margin = new System.Windows.Forms.Padding(0);
            this.objectListViewShows2.Name = "objectListViewShows2";
            this.objectListViewShows2.ShowGroups = false;
            this.objectListViewShows2.Size = new System.Drawing.Size(274, 496);
            this.objectListViewShows2.TabIndex = 0;
            this.objectListViewShows2.UseCompatibleStateImageBehavior = false;
            this.objectListViewShows2.View = System.Windows.Forms.View.Details;
            this.objectListViewShows2.SelectionChanged += new System.EventHandler(this.objectListViewShows2_SelectionChanged);
            // 
            // shows2_id
            // 
            this.shows2_id.AspectName = "id";
            this.shows2_id.IsVisible = false;
            this.shows2_id.Text = "ID";
            this.shows2_id.Width = 0;
            // 
            // shows2_show_name
            // 
            this.shows2_show_name.AspectName = "show_name";
            this.shows2_show_name.FillsFreeSpace = true;
            this.shows2_show_name.Text = "Show Name";
            // 
            // contextMenuStripShows2_list
            // 
            this.contextMenuStripShows2_list.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShows2_list_update,
            this.getBannerToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.scanToolStripMenuItem});
            this.contextMenuStripShows2_list.Name = "contextMenuStripShows2_list";
            this.contextMenuStripShows2_list.Size = new System.Drawing.Size(145, 92);
            // 
            // toolStripMenuItemShows2_list_update
            // 
            this.toolStripMenuItemShows2_list_update.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemShows2_list_update_selected,
            this.toolStripMenuItemShows2_list_update_all});
            this.toolStripMenuItemShows2_list_update.Name = "toolStripMenuItemShows2_list_update";
            this.toolStripMenuItemShows2_list_update.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItemShows2_list_update.Text = "Force Update";
            // 
            // toolStripMenuItemShows2_list_update_selected
            // 
            this.toolStripMenuItemShows2_list_update_selected.Name = "toolStripMenuItemShows2_list_update_selected";
            this.toolStripMenuItemShows2_list_update_selected.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemShows2_list_update_selected.Text = "Selected";
            this.toolStripMenuItemShows2_list_update_selected.Click += new System.EventHandler(this.toolStripMenuItemShows2_list_update_selected_Click);
            // 
            // toolStripMenuItemShows2_list_update_all
            // 
            this.toolStripMenuItemShows2_list_update_all.Name = "toolStripMenuItemShows2_list_update_all";
            this.toolStripMenuItemShows2_list_update_all.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemShows2_list_update_all.Text = "All";
            this.toolStripMenuItemShows2_list_update_all.Click += new System.EventHandler(this.toolStripMenuItemShows2_list_update_all_Click);
            // 
            // getBannerToolStripMenuItem
            // 
            this.getBannerToolStripMenuItem.Name = "getBannerToolStripMenuItem";
            this.getBannerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.getBannerToolStripMenuItem.Text = "Get Banner";
            this.getBannerToolStripMenuItem.Click += new System.EventHandler(this.getBannerToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.scanToolStripMenuItem.Text = "Scan";
            this.scanToolStripMenuItem.Click += new System.EventHandler(this.scanToolStripMenuItem_Click);
            // 
            // tableLayoutPanelShows2_details_full
            // 
            this.tableLayoutPanelShows2_details_full.ColumnCount = 1;
            this.tableLayoutPanelShows2_details_full.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2_details_full.Controls.Add(this.panelShows2Details, 0, 1);
            this.tableLayoutPanelShows2_details_full.Controls.Add(this.pictureBoxShows2_banner, 0, 0);
            this.tableLayoutPanelShows2_details_full.Controls.Add(this.tableLayoutPanelOverview, 0, 2);
            this.tableLayoutPanelShows2_details_full.Controls.Add(this.tableLayoutPanelShows2_details_buttons, 0, 3);
            this.tableLayoutPanelShows2_details_full.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows2_details_full.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelShows2_details_full.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows2_details_full.Name = "tableLayoutPanelShows2_details_full";
            this.tableLayoutPanelShows2_details_full.RowCount = 4;
            this.tableLayoutPanelShows2_details_full.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanelShows2_details_full.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.72727F));
            this.tableLayoutPanelShows2_details_full.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanelShows2_details_full.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanelShows2_details_full.Size = new System.Drawing.Size(776, 496);
            this.tableLayoutPanelShows2_details_full.TabIndex = 0;
            // 
            // panelShows2Details
            // 
            this.panelShows2Details.Controls.Add(this.tableLayoutPanelShows2_details);
            this.panelShows2Details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelShows2Details.Location = new System.Drawing.Point(3, 108);
            this.panelShows2Details.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panelShows2Details.Name = "panelShows2Details";
            this.panelShows2Details.Size = new System.Drawing.Size(770, 290);
            this.panelShows2Details.TabIndex = 1;
            // 
            // tableLayoutPanelShows2_details
            // 
            this.tableLayoutPanelShows2_details.ColumnCount = 1;
            this.tableLayoutPanelShows2_details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2_details.Controls.Add(this.labelShows2_tvdb_name, 0, 0);
            this.tableLayoutPanelShows2_details.Controls.Add(this.groupBoxShows2_details, 0, 1);
            this.tableLayoutPanelShows2_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows2_details.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelShows2_details.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows2_details.Name = "tableLayoutPanelShows2_details";
            this.tableLayoutPanelShows2_details.RowCount = 2;
            this.tableLayoutPanelShows2_details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanelShows2_details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2_details.Size = new System.Drawing.Size(770, 290);
            this.tableLayoutPanelShows2_details.TabIndex = 12;
            // 
            // labelShows2_tvdb_name
            // 
            this.labelShows2_tvdb_name.AutoSize = true;
            this.labelShows2_tvdb_name.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_tvdb_name.Location = new System.Drawing.Point(3, 0);
            this.labelShows2_tvdb_name.Name = "labelShows2_tvdb_name";
            this.labelShows2_tvdb_name.Size = new System.Drawing.Size(204, 44);
            this.labelShows2_tvdb_name.TabIndex = 0;
            this.labelShows2_tvdb_name.Text = "Show Name";
            // 
            // groupBoxShows2_details
            // 
            this.groupBoxShows2_details.Controls.Add(this.tableLayoutPanelShows2_show_details);
            this.groupBoxShows2_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxShows2_details.Location = new System.Drawing.Point(3, 44);
            this.groupBoxShows2_details.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupBoxShows2_details.Name = "groupBoxShows2_details";
            this.groupBoxShows2_details.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupBoxShows2_details.Size = new System.Drawing.Size(764, 246);
            this.groupBoxShows2_details.TabIndex = 11;
            this.groupBoxShows2_details.TabStop = false;
            // 
            // tableLayoutPanelShows2_show_details
            // 
            this.tableLayoutPanelShows2_show_details.ColumnCount = 2;
            this.tableLayoutPanelShows2_show_details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.02339F));
            this.tableLayoutPanelShows2_show_details.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.97661F));
            this.tableLayoutPanelShows2_show_details.Controls.Add(this.groupBoxShows2_details_editable, 0, 0);
            this.tableLayoutPanelShows2_show_details.Controls.Add(this.groupBoxShows2_airs_next, 1, 1);
            this.tableLayoutPanelShows2_show_details.Controls.Add(this.groupBoxShows2_airs_last, 0, 1);
            this.tableLayoutPanelShows2_show_details.Controls.Add(this.groupBoxShows2_details_air, 1, 0);
            this.tableLayoutPanelShows2_show_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows2_show_details.Location = new System.Drawing.Point(3, 13);
            this.tableLayoutPanelShows2_show_details.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows2_show_details.Name = "tableLayoutPanelShows2_show_details";
            this.tableLayoutPanelShows2_show_details.RowCount = 2;
            this.tableLayoutPanelShows2_show_details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.9485F));
            this.tableLayoutPanelShows2_show_details.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.0515F));
            this.tableLayoutPanelShows2_show_details.Size = new System.Drawing.Size(758, 233);
            this.tableLayoutPanelShows2_show_details.TabIndex = 23;
            // 
            // groupBoxShows2_details_editable
            // 
            this.groupBoxShows2_details_editable.Controls.Add(this.textBoxShows2_aliases);
            this.groupBoxShows2_details_editable.Controls.Add(this.numericUpDownShows2_ignore_seasons);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_tvdb_id_value);
            this.groupBoxShows2_details_editable.Controls.Add(this.comboBoxShows2_quality);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_name);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_aliases);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_quality);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_ignore_season);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_tvdb_id);
            this.groupBoxShows2_details_editable.Controls.Add(this.labelShows2_name_value);
            this.groupBoxShows2_details_editable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxShows2_details_editable.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxShows2_details_editable.Location = new System.Drawing.Point(3, 0);
            this.groupBoxShows2_details_editable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxShows2_details_editable.Name = "groupBoxShows2_details_editable";
            this.groupBoxShows2_details_editable.Padding = new System.Windows.Forms.Padding(1);
            this.groupBoxShows2_details_editable.Size = new System.Drawing.Size(380, 146);
            this.groupBoxShows2_details_editable.TabIndex = 12;
            this.groupBoxShows2_details_editable.TabStop = false;
            // 
            // textBoxShows2_aliases
            // 
            this.textBoxShows2_aliases.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxShows2_aliases.Location = new System.Drawing.Point(130, 116);
            this.textBoxShows2_aliases.Name = "textBoxShows2_aliases";
            this.textBoxShows2_aliases.Size = new System.Drawing.Size(245, 20);
            this.textBoxShows2_aliases.TabIndex = 18;
            // 
            // numericUpDownShows2_ignore_seasons
            // 
            this.numericUpDownShows2_ignore_seasons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownShows2_ignore_seasons.Location = new System.Drawing.Point(130, 91);
            this.numericUpDownShows2_ignore_seasons.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownShows2_ignore_seasons.Name = "numericUpDownShows2_ignore_seasons";
            this.numericUpDownShows2_ignore_seasons.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownShows2_ignore_seasons.TabIndex = 16;
            // 
            // labelShows2_tvdb_id_value
            // 
            this.labelShows2_tvdb_id_value.AutoSize = true;
            this.labelShows2_tvdb_id_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_tvdb_id_value.Location = new System.Drawing.Point(125, 44);
            this.labelShows2_tvdb_id_value.Margin = new System.Windows.Forms.Padding(1, 7, 5, 5);
            this.labelShows2_tvdb_id_value.Name = "labelShows2_tvdb_id_value";
            this.labelShows2_tvdb_id_value.Size = new System.Drawing.Size(53, 13);
            this.labelShows2_tvdb_id_value.TabIndex = 12;
            this.labelShows2_tvdb_id_value.Text = "TVDB_ID";
            // 
            // comboBoxShows2_quality
            // 
            this.comboBoxShows2_quality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShows2_quality.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxShows2_quality.FormattingEnabled = true;
            this.comboBoxShows2_quality.Items.AddRange(new object[] {
            "Best Possible",
            "xvid",
            "720p"});
            this.comboBoxShows2_quality.Location = new System.Drawing.Point(130, 64);
            this.comboBoxShows2_quality.Name = "comboBoxShows2_quality";
            this.comboBoxShows2_quality.Size = new System.Drawing.Size(88, 21);
            this.comboBoxShows2_quality.TabIndex = 17;
            // 
            // labelShows2_name
            // 
            this.labelShows2_name.AutoSize = true;
            this.labelShows2_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_name.Location = new System.Drawing.Point(4, 19);
            this.labelShows2_name.Margin = new System.Windows.Forms.Padding(3, 5, 1, 5);
            this.labelShows2_name.Name = "labelShows2_name";
            this.labelShows2_name.Size = new System.Drawing.Size(103, 15);
            this.labelShows2_name.TabIndex = 1;
            this.labelShows2_name.Text = "Name On Disk:";
            // 
            // labelShows2_aliases
            // 
            this.labelShows2_aliases.AutoSize = true;
            this.labelShows2_aliases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_aliases.Location = new System.Drawing.Point(4, 119);
            this.labelShows2_aliases.Margin = new System.Windows.Forms.Padding(3, 5, 1, 5);
            this.labelShows2_aliases.Name = "labelShows2_aliases";
            this.labelShows2_aliases.Size = new System.Drawing.Size(57, 15);
            this.labelShows2_aliases.TabIndex = 4;
            this.labelShows2_aliases.Text = "Aliases:";
            // 
            // labelShows2_quality
            // 
            this.labelShows2_quality.AutoSize = true;
            this.labelShows2_quality.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_quality.Location = new System.Drawing.Point(4, 69);
            this.labelShows2_quality.Margin = new System.Windows.Forms.Padding(3, 5, 1, 5);
            this.labelShows2_quality.Name = "labelShows2_quality";
            this.labelShows2_quality.Size = new System.Drawing.Size(55, 15);
            this.labelShows2_quality.TabIndex = 2;
            this.labelShows2_quality.Text = "Quality:";
            // 
            // labelShows2_ignore_season
            // 
            this.labelShows2_ignore_season.AutoSize = true;
            this.labelShows2_ignore_season.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_ignore_season.Location = new System.Drawing.Point(4, 94);
            this.labelShows2_ignore_season.Margin = new System.Windows.Forms.Padding(3, 5, 1, 5);
            this.labelShows2_ignore_season.Name = "labelShows2_ignore_season";
            this.labelShows2_ignore_season.Size = new System.Drawing.Size(111, 15);
            this.labelShows2_ignore_season.TabIndex = 3;
            this.labelShows2_ignore_season.Text = "Ignore Seasons:";
            // 
            // labelShows2_tvdb_id
            // 
            this.labelShows2_tvdb_id.AutoSize = true;
            this.labelShows2_tvdb_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_tvdb_id.Location = new System.Drawing.Point(4, 44);
            this.labelShows2_tvdb_id.Margin = new System.Windows.Forms.Padding(3, 5, 1, 5);
            this.labelShows2_tvdb_id.Name = "labelShows2_tvdb_id";
            this.labelShows2_tvdb_id.Size = new System.Drawing.Size(64, 15);
            this.labelShows2_tvdb_id.TabIndex = 0;
            this.labelShows2_tvdb_id.Text = "TVDB ID:";
            // 
            // labelShows2_name_value
            // 
            this.labelShows2_name_value.AutoSize = true;
            this.labelShows2_name_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_name_value.Location = new System.Drawing.Point(125, 21);
            this.labelShows2_name_value.Margin = new System.Windows.Forms.Padding(1, 7, 5, 5);
            this.labelShows2_name_value.MinimumSize = new System.Drawing.Size(15, 10);
            this.labelShows2_name_value.Name = "labelShows2_name_value";
            this.labelShows2_name_value.Size = new System.Drawing.Size(82, 13);
            this.labelShows2_name_value.TabIndex = 11;
            this.labelShows2_name_value.Text = "Name_On_Disk";
            // 
            // groupBoxShows2_airs_next
            // 
            this.groupBoxShows2_airs_next.Controls.Add(this.tableLayoutPanelShows2_AirsLast);
            this.groupBoxShows2_airs_next.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxShows2_airs_next.Location = new System.Drawing.Point(389, 152);
            this.groupBoxShows2_airs_next.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.groupBoxShows2_airs_next.Name = "groupBoxShows2_airs_next";
            this.groupBoxShows2_airs_next.Size = new System.Drawing.Size(366, 74);
            this.groupBoxShows2_airs_next.TabIndex = 21;
            this.groupBoxShows2_airs_next.TabStop = false;
            this.groupBoxShows2_airs_next.Text = "Airs Next";
            // 
            // tableLayoutPanelShows2_AirsLast
            // 
            this.tableLayoutPanelShows2_AirsLast.ColumnCount = 5;
            this.tableLayoutPanelShows2_AirsLast.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanelShows2_AirsLast.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2_AirsLast.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanelShows2_AirsLast.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanelShows2_AirsLast.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.pictureBoxShows2_next_downloaded, 4, 0);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_date, 0, 0);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_date_value, 1, 0);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_episode_number, 2, 1);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_episode_number_value, 3, 1);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows_airs_next_title, 0, 1);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_season_number_value, 3, 0);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_title_value, 1, 1);
            this.tableLayoutPanelShows2_AirsLast.Controls.Add(this.labelShows2_airs_next_season_number, 2, 0);
            this.tableLayoutPanelShows2_AirsLast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows2_AirsLast.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelShows2_AirsLast.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows2_AirsLast.Name = "tableLayoutPanelShows2_AirsLast";
            this.tableLayoutPanelShows2_AirsLast.RowCount = 2;
            this.tableLayoutPanelShows2_AirsLast.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelShows2_AirsLast.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelShows2_AirsLast.Size = new System.Drawing.Size(360, 55);
            this.tableLayoutPanelShows2_AirsLast.TabIndex = 0;
            // 
            // pictureBoxShows2_next_downloaded
            // 
            this.pictureBoxShows2_next_downloaded.Image = global::SABSync.Images.check;
            this.pictureBoxShows2_next_downloaded.Location = new System.Drawing.Point(334, 0);
            this.pictureBoxShows2_next_downloaded.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxShows2_next_downloaded.Name = "pictureBoxShows2_next_downloaded";
            this.pictureBoxShows2_next_downloaded.Size = new System.Drawing.Size(26, 26);
            this.pictureBoxShows2_next_downloaded.TabIndex = 28;
            this.pictureBoxShows2_next_downloaded.TabStop = false;
            this.pictureBoxShows2_next_downloaded.Visible = false;
            // 
            // labelShows2_airs_next_date
            // 
            this.labelShows2_airs_next_date.AutoSize = true;
            this.labelShows2_airs_next_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_date.Location = new System.Drawing.Point(0, 5);
            this.labelShows2_airs_next_date.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelShows2_airs_next_date.Name = "labelShows2_airs_next_date";
            this.labelShows2_airs_next_date.Size = new System.Drawing.Size(41, 15);
            this.labelShows2_airs_next_date.TabIndex = 14;
            this.labelShows2_airs_next_date.Text = "Date:";
            // 
            // labelShows2_airs_next_date_value
            // 
            this.labelShows2_airs_next_date_value.AutoSize = true;
            this.labelShows2_airs_next_date_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_next_date_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_date_value.Location = new System.Drawing.Point(43, 7);
            this.labelShows2_airs_next_date_value.Margin = new System.Windows.Forms.Padding(1, 7, 5, 5);
            this.labelShows2_airs_next_date_value.Name = "labelShows2_airs_next_date_value";
            this.labelShows2_airs_next_date_value.Size = new System.Drawing.Size(195, 15);
            this.labelShows2_airs_next_date_value.TabIndex = 20;
            this.labelShows2_airs_next_date_value.Text = "N/A";
            // 
            // labelShows2_airs_next_episode_number
            // 
            this.labelShows2_airs_next_episode_number.AutoSize = true;
            this.labelShows2_airs_next_episode_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_episode_number.Location = new System.Drawing.Point(244, 32);
            this.labelShows2_airs_next_episode_number.Margin = new System.Windows.Forms.Padding(1, 5, 0, 5);
            this.labelShows2_airs_next_episode_number.Name = "labelShows2_airs_next_episode_number";
            this.labelShows2_airs_next_episode_number.Size = new System.Drawing.Size(59, 18);
            this.labelShows2_airs_next_episode_number.TabIndex = 25;
            this.labelShows2_airs_next_episode_number.Text = "Episode:";
            // 
            // labelShows2_airs_next_episode_number_value
            // 
            this.labelShows2_airs_next_episode_number_value.AutoSize = true;
            this.labelShows2_airs_next_episode_number_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_next_episode_number_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_episode_number_value.Location = new System.Drawing.Point(306, 34);
            this.labelShows2_airs_next_episode_number_value.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.labelShows2_airs_next_episode_number_value.Name = "labelShows2_airs_next_episode_number_value";
            this.labelShows2_airs_next_episode_number_value.Size = new System.Drawing.Size(28, 16);
            this.labelShows2_airs_next_episode_number_value.TabIndex = 26;
            this.labelShows2_airs_next_episode_number_value.Text = "N/A";
            // 
            // labelShows_airs_next_title
            // 
            this.labelShows_airs_next_title.AutoSize = true;
            this.labelShows_airs_next_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows_airs_next_title.Location = new System.Drawing.Point(0, 32);
            this.labelShows_airs_next_title.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelShows_airs_next_title.Name = "labelShows_airs_next_title";
            this.labelShows_airs_next_title.Size = new System.Drawing.Size(39, 15);
            this.labelShows_airs_next_title.TabIndex = 21;
            this.labelShows_airs_next_title.Text = "Title:";
            // 
            // labelShows2_airs_next_season_number_value
            // 
            this.labelShows2_airs_next_season_number_value.AutoSize = true;
            this.labelShows2_airs_next_season_number_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_next_season_number_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_season_number_value.Location = new System.Drawing.Point(306, 7);
            this.labelShows2_airs_next_season_number_value.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.labelShows2_airs_next_season_number_value.Name = "labelShows2_airs_next_season_number_value";
            this.labelShows2_airs_next_season_number_value.Size = new System.Drawing.Size(28, 15);
            this.labelShows2_airs_next_season_number_value.TabIndex = 24;
            this.labelShows2_airs_next_season_number_value.Text = "N/A";
            // 
            // labelShows2_airs_next_title_value
            // 
            this.labelShows2_airs_next_title_value.AutoSize = true;
            this.labelShows2_airs_next_title_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_next_title_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_title_value.Location = new System.Drawing.Point(43, 34);
            this.labelShows2_airs_next_title_value.Margin = new System.Windows.Forms.Padding(1, 7, 5, 5);
            this.labelShows2_airs_next_title_value.Name = "labelShows2_airs_next_title_value";
            this.labelShows2_airs_next_title_value.Size = new System.Drawing.Size(195, 16);
            this.labelShows2_airs_next_title_value.TabIndex = 22;
            this.labelShows2_airs_next_title_value.Text = "N/A";
            // 
            // labelShows2_airs_next_season_number
            // 
            this.labelShows2_airs_next_season_number.AutoSize = true;
            this.labelShows2_airs_next_season_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_next_season_number.Location = new System.Drawing.Point(244, 5);
            this.labelShows2_airs_next_season_number.Margin = new System.Windows.Forms.Padding(1, 5, 0, 5);
            this.labelShows2_airs_next_season_number.Name = "labelShows2_airs_next_season_number";
            this.labelShows2_airs_next_season_number.Size = new System.Drawing.Size(59, 15);
            this.labelShows2_airs_next_season_number.TabIndex = 23;
            this.labelShows2_airs_next_season_number.Text = "Season:";
            // 
            // groupBoxShows2_airs_last
            // 
            this.groupBoxShows2_airs_last.Controls.Add(this.tableLayoutPanel5);
            this.groupBoxShows2_airs_last.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxShows2_airs_last.Location = new System.Drawing.Point(3, 152);
            this.groupBoxShows2_airs_last.Margin = new System.Windows.Forms.Padding(3, 3, 3, 7);
            this.groupBoxShows2_airs_last.Name = "groupBoxShows2_airs_last";
            this.groupBoxShows2_airs_last.Size = new System.Drawing.Size(380, 74);
            this.groupBoxShows2_airs_last.TabIndex = 22;
            this.groupBoxShows2_airs_last.TabStop = false;
            this.groupBoxShows2_airs_last.Text = "Airs Last";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel5.Controls.Add(this.pictureBoxShows2_last_downloaded, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_episode_number, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_episode_number_value, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_date, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_title, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_season_number_value, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_date_value, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_title_value, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.labelShows2_airs_last_season_number, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(374, 55);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // pictureBoxShows2_last_downloaded
            // 
            this.pictureBoxShows2_last_downloaded.Image = global::SABSync.Images.check;
            this.pictureBoxShows2_last_downloaded.Location = new System.Drawing.Point(348, 0);
            this.pictureBoxShows2_last_downloaded.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxShows2_last_downloaded.Name = "pictureBoxShows2_last_downloaded";
            this.pictureBoxShows2_last_downloaded.Size = new System.Drawing.Size(26, 26);
            this.pictureBoxShows2_last_downloaded.TabIndex = 27;
            this.pictureBoxShows2_last_downloaded.TabStop = false;
            this.pictureBoxShows2_last_downloaded.Visible = false;
            // 
            // labelShows2_airs_last_episode_number
            // 
            this.labelShows2_airs_last_episode_number.AutoSize = true;
            this.labelShows2_airs_last_episode_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_episode_number.Location = new System.Drawing.Point(257, 32);
            this.labelShows2_airs_last_episode_number.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelShows2_airs_last_episode_number.Name = "labelShows2_airs_last_episode_number";
            this.labelShows2_airs_last_episode_number.Size = new System.Drawing.Size(63, 15);
            this.labelShows2_airs_last_episode_number.TabIndex = 25;
            this.labelShows2_airs_last_episode_number.Text = "Episode:";
            // 
            // labelShows2_airs_last_episode_number_value
            // 
            this.labelShows2_airs_last_episode_number_value.AutoSize = true;
            this.labelShows2_airs_last_episode_number_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_last_episode_number_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_episode_number_value.Location = new System.Drawing.Point(320, 34);
            this.labelShows2_airs_last_episode_number_value.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.labelShows2_airs_last_episode_number_value.Name = "labelShows2_airs_last_episode_number_value";
            this.labelShows2_airs_last_episode_number_value.Size = new System.Drawing.Size(28, 16);
            this.labelShows2_airs_last_episode_number_value.TabIndex = 26;
            this.labelShows2_airs_last_episode_number_value.Text = "N/A";
            // 
            // labelShows2_airs_last_date
            // 
            this.labelShows2_airs_last_date.AutoSize = true;
            this.labelShows2_airs_last_date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_date.Location = new System.Drawing.Point(0, 5);
            this.labelShows2_airs_last_date.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelShows2_airs_last_date.Name = "labelShows2_airs_last_date";
            this.labelShows2_airs_last_date.Size = new System.Drawing.Size(41, 15);
            this.labelShows2_airs_last_date.TabIndex = 14;
            this.labelShows2_airs_last_date.Text = "Date:";
            // 
            // labelShows2_airs_last_title
            // 
            this.labelShows2_airs_last_title.AutoSize = true;
            this.labelShows2_airs_last_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_title.Location = new System.Drawing.Point(0, 32);
            this.labelShows2_airs_last_title.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelShows2_airs_last_title.Name = "labelShows2_airs_last_title";
            this.labelShows2_airs_last_title.Size = new System.Drawing.Size(39, 15);
            this.labelShows2_airs_last_title.TabIndex = 21;
            this.labelShows2_airs_last_title.Text = "Title:";
            // 
            // labelShows2_airs_last_season_number_value
            // 
            this.labelShows2_airs_last_season_number_value.AutoSize = true;
            this.labelShows2_airs_last_season_number_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_last_season_number_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_season_number_value.Location = new System.Drawing.Point(320, 7);
            this.labelShows2_airs_last_season_number_value.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.labelShows2_airs_last_season_number_value.Name = "labelShows2_airs_last_season_number_value";
            this.labelShows2_airs_last_season_number_value.Size = new System.Drawing.Size(28, 15);
            this.labelShows2_airs_last_season_number_value.TabIndex = 24;
            this.labelShows2_airs_last_season_number_value.Text = "N/A";
            // 
            // labelShows2_airs_last_date_value
            // 
            this.labelShows2_airs_last_date_value.AutoSize = true;
            this.labelShows2_airs_last_date_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_last_date_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_date_value.Location = new System.Drawing.Point(42, 7);
            this.labelShows2_airs_last_date_value.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.labelShows2_airs_last_date_value.Name = "labelShows2_airs_last_date_value";
            this.labelShows2_airs_last_date_value.Size = new System.Drawing.Size(215, 15);
            this.labelShows2_airs_last_date_value.TabIndex = 20;
            this.labelShows2_airs_last_date_value.Text = "N/A";
            // 
            // labelShows2_airs_last_title_value
            // 
            this.labelShows2_airs_last_title_value.AutoSize = true;
            this.labelShows2_airs_last_title_value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelShows2_airs_last_title_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_title_value.Location = new System.Drawing.Point(42, 34);
            this.labelShows2_airs_last_title_value.Margin = new System.Windows.Forms.Padding(0, 7, 0, 5);
            this.labelShows2_airs_last_title_value.Name = "labelShows2_airs_last_title_value";
            this.labelShows2_airs_last_title_value.Size = new System.Drawing.Size(215, 16);
            this.labelShows2_airs_last_title_value.TabIndex = 22;
            this.labelShows2_airs_last_title_value.Text = "N/A";
            // 
            // labelShows2_airs_last_season_number
            // 
            this.labelShows2_airs_last_season_number.AutoSize = true;
            this.labelShows2_airs_last_season_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_airs_last_season_number.Location = new System.Drawing.Point(257, 5);
            this.labelShows2_airs_last_season_number.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelShows2_airs_last_season_number.Name = "labelShows2_airs_last_season_number";
            this.labelShows2_airs_last_season_number.Size = new System.Drawing.Size(59, 15);
            this.labelShows2_airs_last_season_number.TabIndex = 23;
            this.labelShows2_airs_last_season_number.Text = "Season:";
            // 
            // groupBoxShows2_details_air
            // 
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_status_value);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_genre_value);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_air_time_value);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_air_day_value);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_air_time);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_air_day);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_status);
            this.groupBoxShows2_details_air.Controls.Add(this.labelShows2_genre);
            this.groupBoxShows2_details_air.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxShows2_details_air.Font = new System.Drawing.Font("Microsoft Sans Serif", 1.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxShows2_details_air.Location = new System.Drawing.Point(389, 0);
            this.groupBoxShows2_details_air.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBoxShows2_details_air.Name = "groupBoxShows2_details_air";
            this.groupBoxShows2_details_air.Padding = new System.Windows.Forms.Padding(1);
            this.groupBoxShows2_details_air.Size = new System.Drawing.Size(366, 146);
            this.groupBoxShows2_details_air.TabIndex = 7;
            this.groupBoxShows2_details_air.TabStop = false;
            this.groupBoxShows2_details_air.UseCompatibleTextRendering = true;
            // 
            // labelShows2_status_value
            // 
            this.labelShows2_status_value.AutoSize = true;
            this.labelShows2_status_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_status_value.Location = new System.Drawing.Point(82, 86);
            this.labelShows2_status_value.Margin = new System.Windows.Forms.Padding(5, 7, 5, 5);
            this.labelShows2_status_value.Name = "labelShows2_status_value";
            this.labelShows2_status_value.Size = new System.Drawing.Size(37, 13);
            this.labelShows2_status_value.TabIndex = 15;
            this.labelShows2_status_value.Text = "Status";
            // 
            // labelShows2_genre_value
            // 
            this.labelShows2_genre_value.AutoSize = true;
            this.labelShows2_genre_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_genre_value.Location = new System.Drawing.Point(82, 111);
            this.labelShows2_genre_value.Margin = new System.Windows.Forms.Padding(5, 7, 5, 5);
            this.labelShows2_genre_value.Name = "labelShows2_genre_value";
            this.labelShows2_genre_value.Size = new System.Drawing.Size(36, 13);
            this.labelShows2_genre_value.TabIndex = 14;
            this.labelShows2_genre_value.Text = "Genre";
            // 
            // labelShows2_air_time_value
            // 
            this.labelShows2_air_time_value.AutoSize = true;
            this.labelShows2_air_time_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_air_time_value.Location = new System.Drawing.Point(82, 61);
            this.labelShows2_air_time_value.Margin = new System.Windows.Forms.Padding(5, 7, 5, 5);
            this.labelShows2_air_time_value.Name = "labelShows2_air_time_value";
            this.labelShows2_air_time_value.Size = new System.Drawing.Size(45, 13);
            this.labelShows2_air_time_value.TabIndex = 13;
            this.labelShows2_air_time_value.Text = "Air Time";
            // 
            // labelShows2_air_day_value
            // 
            this.labelShows2_air_day_value.AutoSize = true;
            this.labelShows2_air_day_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_air_day_value.Location = new System.Drawing.Point(82, 36);
            this.labelShows2_air_day_value.Margin = new System.Windows.Forms.Padding(5, 7, 5, 5);
            this.labelShows2_air_day_value.Name = "labelShows2_air_day_value";
            this.labelShows2_air_day_value.Size = new System.Drawing.Size(41, 13);
            this.labelShows2_air_day_value.TabIndex = 12;
            this.labelShows2_air_day_value.Text = "Air Day";
            // 
            // labelShows2_air_time
            // 
            this.labelShows2_air_time.AutoSize = true;
            this.labelShows2_air_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_air_time.Location = new System.Drawing.Point(14, 59);
            this.labelShows2_air_time.Margin = new System.Windows.Forms.Padding(5);
            this.labelShows2_air_time.Name = "labelShows2_air_time";
            this.labelShows2_air_time.Size = new System.Drawing.Size(64, 15);
            this.labelShows2_air_time.TabIndex = 6;
            this.labelShows2_air_time.Text = "Air Time:";
            // 
            // labelShows2_air_day
            // 
            this.labelShows2_air_day.AutoSize = true;
            this.labelShows2_air_day.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_air_day.Location = new System.Drawing.Point(14, 34);
            this.labelShows2_air_day.Margin = new System.Windows.Forms.Padding(5);
            this.labelShows2_air_day.Name = "labelShows2_air_day";
            this.labelShows2_air_day.Size = new System.Drawing.Size(56, 15);
            this.labelShows2_air_day.TabIndex = 5;
            this.labelShows2_air_day.Text = "Air Day:";
            // 
            // labelShows2_status
            // 
            this.labelShows2_status.AutoSize = true;
            this.labelShows2_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_status.Location = new System.Drawing.Point(14, 84);
            this.labelShows2_status.Margin = new System.Windows.Forms.Padding(5);
            this.labelShows2_status.Name = "labelShows2_status";
            this.labelShows2_status.Size = new System.Drawing.Size(51, 15);
            this.labelShows2_status.TabIndex = 7;
            this.labelShows2_status.Text = "Status:";
            // 
            // labelShows2_genre
            // 
            this.labelShows2_genre.AutoSize = true;
            this.labelShows2_genre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_genre.Location = new System.Drawing.Point(14, 109);
            this.labelShows2_genre.Margin = new System.Windows.Forms.Padding(5);
            this.labelShows2_genre.Name = "labelShows2_genre";
            this.labelShows2_genre.Size = new System.Drawing.Size(50, 15);
            this.labelShows2_genre.TabIndex = 8;
            this.labelShows2_genre.Text = "Genre:";
            // 
            // pictureBoxShows2_banner
            // 
            this.pictureBoxShows2_banner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxShows2_banner.Image = global::SABSync.Images.SABSync_Banner;
            this.pictureBoxShows2_banner.Location = new System.Drawing.Point(1, 1);
            this.pictureBoxShows2_banner.Margin = new System.Windows.Forms.Padding(1);
            this.pictureBoxShows2_banner.Name = "pictureBoxShows2_banner";
            this.pictureBoxShows2_banner.Size = new System.Drawing.Size(774, 106);
            this.pictureBoxShows2_banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxShows2_banner.TabIndex = 3;
            this.pictureBoxShows2_banner.TabStop = false;
            // 
            // tableLayoutPanelOverview
            // 
            this.tableLayoutPanelOverview.ColumnCount = 1;
            this.tableLayoutPanelOverview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelOverview.Controls.Add(this.labelShows2_overview, 0, 0);
            this.tableLayoutPanelOverview.Controls.Add(this.textBoxShows2_overview, 0, 1);
            this.tableLayoutPanelOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOverview.Location = new System.Drawing.Point(0, 398);
            this.tableLayoutPanelOverview.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelOverview.Name = "tableLayoutPanelOverview";
            this.tableLayoutPanelOverview.RowCount = 2;
            this.tableLayoutPanelOverview.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelOverview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanelOverview.Size = new System.Drawing.Size(776, 70);
            this.tableLayoutPanelOverview.TabIndex = 4;
            // 
            // labelShows2_overview
            // 
            this.labelShows2_overview.AutoSize = true;
            this.labelShows2_overview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelShows2_overview.Location = new System.Drawing.Point(0, 0);
            this.labelShows2_overview.Margin = new System.Windows.Forms.Padding(0);
            this.labelShows2_overview.Name = "labelShows2_overview";
            this.labelShows2_overview.Size = new System.Drawing.Size(68, 15);
            this.labelShows2_overview.TabIndex = 9;
            this.labelShows2_overview.Text = "Overview:";
            // 
            // textBoxShows2_overview
            // 
            this.textBoxShows2_overview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxShows2_overview.Location = new System.Drawing.Point(5, 17);
            this.textBoxShows2_overview.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.textBoxShows2_overview.Multiline = true;
            this.textBoxShows2_overview.Name = "textBoxShows2_overview";
            this.textBoxShows2_overview.ReadOnly = true;
            this.textBoxShows2_overview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxShows2_overview.Size = new System.Drawing.Size(766, 51);
            this.textBoxShows2_overview.TabIndex = 2;
            // 
            // tableLayoutPanelShows2_details_buttons
            // 
            this.tableLayoutPanelShows2_details_buttons.ColumnCount = 3;
            this.tableLayoutPanelShows2_details_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelShows2_details_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelShows2_details_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelShows2_details_buttons.Controls.Add(this.buttonShows2_details_save, 2, 0);
            this.tableLayoutPanelShows2_details_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows2_details_buttons.Location = new System.Drawing.Point(0, 468);
            this.tableLayoutPanelShows2_details_buttons.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelShows2_details_buttons.Name = "tableLayoutPanelShows2_details_buttons";
            this.tableLayoutPanelShows2_details_buttons.RowCount = 1;
            this.tableLayoutPanelShows2_details_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows2_details_buttons.Size = new System.Drawing.Size(776, 28);
            this.tableLayoutPanelShows2_details_buttons.TabIndex = 5;
            // 
            // buttonShows2_details_save
            // 
            this.buttonShows2_details_save.Location = new System.Drawing.Point(677, 1);
            this.buttonShows2_details_save.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.buttonShows2_details_save.Name = "buttonShows2_details_save";
            this.buttonShows2_details_save.Size = new System.Drawing.Size(71, 26);
            this.buttonShows2_details_save.TabIndex = 3;
            this.buttonShows2_details_save.Text = "Save";
            this.buttonShows2_details_save.UseVisualStyleBackColor = true;
            this.buttonShows2_details_save.Click += new System.EventHandler(this.buttonShows2_details_save_Click);
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.tableLayoutPanelHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(1070, 537);
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
            this.tableLayoutPanelHistory.Size = new System.Drawing.Size(1064, 531);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 504);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1064, 27);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnPurgeHistory
            // 
            this.btnPurgeHistory.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPurgeHistory.Location = new System.Drawing.Point(988, 1);
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
            this.btnDeleteHistory.Location = new System.Drawing.Point(911, 1);
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
            this.objectListViewHistory.Size = new System.Drawing.Size(1058, 498);
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
            this.tabPageFeeds.Size = new System.Drawing.Size(1070, 537);
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
            this.tableLayoutPanelFeeds.Size = new System.Drawing.Size(1064, 531);
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
            this.objectListViewFeeds.Size = new System.Drawing.Size(1058, 498);
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
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 504);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(1064, 27);
            this.tableLayoutPanelButtons.TabIndex = 1;
            // 
            // btnAddFeed
            // 
            this.btnAddFeed.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAddFeed.Location = new System.Drawing.Point(988, 1);
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
            this.btnDeleteFeeds.Location = new System.Drawing.Point(911, 1);
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
            this.tabPageUpcoming.Size = new System.Drawing.Size(1070, 537);
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
            this.tableLayoutPanelUpcoming.Size = new System.Drawing.Size(1064, 531);
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
            this.objectListViewUpcoming.Size = new System.Drawing.Size(1058, 498);
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
            this.upcoming_show_name.UseInitialLetterForGroup = true;
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
            this.upcoming_airs.UseInitialLetterForGroup = true;
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
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 504);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1064, 27);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btnRefreshUpcoming
            // 
            this.btnRefreshUpcoming.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshUpcoming.Location = new System.Drawing.Point(988, 1);
            this.btnRefreshUpcoming.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnRefreshUpcoming.Name = "btnRefreshUpcoming";
            this.btnRefreshUpcoming.Size = new System.Drawing.Size(71, 26);
            this.btnRefreshUpcoming.TabIndex = 2;
            this.btnRefreshUpcoming.Text = "Refresh";
            this.btnRefreshUpcoming.UseVisualStyleBackColor = true;
            this.btnRefreshUpcoming.Click += new System.EventHandler(this.btnRefreshUpcoming_Click);
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
            // shows2id
            // 
            this.shows2id.AspectName = "id";
            this.shows2id.IsVisible = false;
            this.shows2id.Text = "ID";
            this.shows2id.Width = 0;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1084, 23);
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
            this.toolStripMenuItemRun.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemRun.Text = "&Run Sync";
            this.toolStripMenuItemRun.Click += new System.EventHandler(this.toolStripMenuItemRun_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
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
            this.statusMain.Location = new System.Drawing.Point(0, 592);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(1084, 20);
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
            this.timerUpdateCache.Interval = 14400000;
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
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1084, 612);
            this.tableLayoutPanelMain.TabIndex = 4;
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(1084, 612);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(1100, 650);
            this.Name = "FrmMain";
            this.Text = "SABSync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.Program_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.contextMenuStripTray.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabShows2.ResumeLayout(false);
            this.tableLayoutPanelShows2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.splitContainerShows.Panel1.ResumeLayout(false);
            this.splitContainerShows.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerShows)).EndInit();
            this.splitContainerShows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewShows2)).EndInit();
            this.contextMenuStripShows2_list.ResumeLayout(false);
            this.tableLayoutPanelShows2_details_full.ResumeLayout(false);
            this.panelShows2Details.ResumeLayout(false);
            this.tableLayoutPanelShows2_details.ResumeLayout(false);
            this.tableLayoutPanelShows2_details.PerformLayout();
            this.groupBoxShows2_details.ResumeLayout(false);
            this.tableLayoutPanelShows2_show_details.ResumeLayout(false);
            this.groupBoxShows2_details_editable.ResumeLayout(false);
            this.groupBoxShows2_details_editable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownShows2_ignore_seasons)).EndInit();
            this.groupBoxShows2_airs_next.ResumeLayout(false);
            this.tableLayoutPanelShows2_AirsLast.ResumeLayout(false);
            this.tableLayoutPanelShows2_AirsLast.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShows2_next_downloaded)).EndInit();
            this.groupBoxShows2_airs_last.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShows2_last_downloaded)).EndInit();
            this.groupBoxShows2_details_air.ResumeLayout(false);
            this.groupBoxShows2_details_air.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShows2_banner)).EndInit();
            this.tableLayoutPanelOverview.ResumeLayout(false);
            this.tableLayoutPanelOverview.PerformLayout();
            this.tableLayoutPanelShows2_details_buttons.ResumeLayout(false);
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
        private BrightIdeasSoftware.OLVColumn shows_run_time;
        private BrightIdeasSoftware.OLVColumn shows_poster_url;
        private BrightIdeasSoftware.OLVColumn shows_banner_url;
        private BrightIdeasSoftware.OLVColumn shows_imdb_id;
        private System.Windows.Forms.ImageList imageListProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button btnAddFeed;
        private System.Windows.Forms.Button btnDeleteFeeds;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnPurgeHistory;
        private System.Windows.Forms.Button btnDeleteHistory;
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
        private System.Windows.Forms.TabPage tabShows2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows2;
        private System.Windows.Forms.SplitContainer splitContainerShows;
        private BrightIdeasSoftware.OLVColumn shows2id;
        private BrightIdeasSoftware.ObjectListView objectListViewShows2;
        private BrightIdeasSoftware.OLVColumn shows2_id;
        private BrightIdeasSoftware.OLVColumn shows2_show_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows2_details_full;
        private System.Windows.Forms.Panel panelShows2Details;
        private System.Windows.Forms.Label labelShows2_tvdb_name;
        private System.Windows.Forms.Label labelShows2_overview;
        private System.Windows.Forms.TextBox textBoxShows2_overview;
        private System.Windows.Forms.GroupBox groupBoxShows2_details;
        private System.Windows.Forms.Label labelShows2_tvdb_id_value;
        private System.Windows.Forms.Label labelShows2_name_value;
        private System.Windows.Forms.Label labelShows2_name;
        private System.Windows.Forms.Label labelShows2_tvdb_id;
        private System.Windows.Forms.Label labelShows2_quality;
        private System.Windows.Forms.Label labelShows2_aliases;
        private System.Windows.Forms.Label labelShows2_ignore_season;
        private System.Windows.Forms.NumericUpDown numericUpDownShows2_ignore_seasons;
        private System.Windows.Forms.ComboBox comboBoxShows2_quality;
        private System.Windows.Forms.TextBox textBoxShows2_aliases;
        private System.Windows.Forms.GroupBox groupBoxShows2_details_air;
        private System.Windows.Forms.Label labelShows2_status_value;
        private System.Windows.Forms.Label labelShows2_genre_value;
        private System.Windows.Forms.Label labelShows2_air_time_value;
        private System.Windows.Forms.Label labelShows2_air_day_value;
        private System.Windows.Forms.Label labelShows2_air_time;
        private System.Windows.Forms.Label labelShows2_air_day;
        private System.Windows.Forms.Label labelShows2_status;
        private System.Windows.Forms.Label labelShows2_genre;
        private System.Windows.Forms.GroupBox groupBoxShows2_details_editable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnShows2_scan;
        private System.Windows.Forms.PictureBox pictureBoxShows2_banner;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOverview;
        private System.Windows.Forms.Label labelShows2_airs_next_date_value;
        private System.Windows.Forms.Label labelShows2_airs_next_date;
        private System.Windows.Forms.GroupBox groupBoxShows2_airs_next;
        private System.Windows.Forms.Label labelShows2_airs_next_episode_number;
        private System.Windows.Forms.Label labelShows2_airs_next_episode_number_value;
        private System.Windows.Forms.Label labelShows2_airs_next_season_number;
        private System.Windows.Forms.Label labelShows2_airs_next_season_number_value;
        private System.Windows.Forms.Label labelShows_airs_next_title;
        private System.Windows.Forms.Label labelShows2_airs_next_title_value;
        private System.Windows.Forms.GroupBox groupBoxShows2_airs_last;
        private System.Windows.Forms.Label labelShows2_airs_last_episode_number;
        private System.Windows.Forms.Label labelShows2_airs_last_episode_number_value;
        private System.Windows.Forms.Label labelShows2_airs_last_season_number;
        private System.Windows.Forms.Label labelShows2_airs_last_season_number_value;
        private System.Windows.Forms.Label labelShows2_airs_last_title;
        private System.Windows.Forms.Label labelShows2_airs_last_title_value;
        private System.Windows.Forms.Label labelShows2_airs_last_date;
        private System.Windows.Forms.Label labelShows2_airs_last_date_value;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows2_details;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows2_show_details;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows2_details_buttons;
        private System.Windows.Forms.Button buttonShows2_details_save;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripShows2_list;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShows2_list_update;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShows2_list_update_all;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShows2_list_update_selected;
        private System.Windows.Forms.PictureBox pictureBoxShows2_last_downloaded;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBoxShows2_next_downloaded;
        private System.Windows.Forms.ToolStripMenuItem getBannerToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelShows2_AirsLast;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;




    }
}