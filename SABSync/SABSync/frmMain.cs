using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO; 
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using System.Data.Objects;
using BrightIdeasSoftware;

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
        private DataGridView dataGridViewShows;
        private BindingSource showsBindingSource;
        private BindingSource sABSyncEntitiesBindingSource1;
        private BindingSource sABSyncEntitiesBindingSource;
        private BindingSource historiesBindingSource;

        private int _interval;

        private SQLite Sql = new SQLite();
        private TabPage tabPageFeeds; //Create new Instance of SQLite
        private Logger Logger = new Logger();
        private Button btnScanNewShows;
        private StatusStrip statusMain;
        private ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Timer timerUpdateCache;
        private ToolStripMenuItem donateToolStripMenuItem;
        private ToolStripMenuItem websiteToolStripMenuItem;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn shownameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tvdbidDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tvdbnameDataGridViewTextBoxColumn;
        private DataGridViewComboBoxColumn qualityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tvridDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tvrnameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ignoreseasonDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn aliasesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn airdayDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn airtimeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn runtimeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn posterurlDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn bannerurlDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn imdbidDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn genreDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn overviewDataGridViewTextBoxColumn;
        private TableLayoutPanel tableLayoutPanelShows;
        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelHistory;
        private TableLayoutPanel tableLayoutPanelFeeds;
        private BrightIdeasSoftware.ObjectListView objectListViewFeeds;
        private OLVColumn feed_id;
        private OLVColumn feed_name;
        private OLVColumn feed_url;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnAddFeed;
        private Button btnDelete;
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
        private Config Config = new Config();

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSync = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabShows = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelShows = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewShows = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shownameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvdbidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvdbnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qualityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tvridDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tvrnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ignoreseasonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aliasesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.airdayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.airtimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runtimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.posterurlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bannerurlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imdbidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overviewDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.showsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnScanNewShows = new System.Windows.Forms.Button();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelHistory = new System.Windows.Forms.TableLayoutPanel();
            this.tabPageFeeds = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelFeeds = new System.Windows.Forms.TableLayoutPanel();
            this.objectListViewFeeds = new BrightIdeasSoftware.ObjectListView();
            this.feed_id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.feed_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.feed_url = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddFeed = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.donateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sABSyncEntitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sABSyncEntitiesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.historiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerUpdateCache = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.objectListViewHistory = new BrightIdeasSoftware.ObjectListView();
            this.history_show_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_season_number = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_episode_number = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_episode_name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_feed_title = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_quality = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_proper = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_provider = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.history_date = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStripTray.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabShows.SuspendLayout();
            this.tableLayoutPanelShows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showsBindingSource)).BeginInit();
            this.tabHistory.SuspendLayout();
            this.tableLayoutPanelHistory.SuspendLayout();
            this.tabPageFeeds.SuspendLayout();
            this.tableLayoutPanelFeeds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewFeeds)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiesBindingSource)).BeginInit();
            this.statusMain.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewHistory)).BeginInit();
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
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(1, 23);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(982, 488);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabShows
            // 
            this.tabShows.Controls.Add(this.tableLayoutPanelShows);
            this.tabShows.Location = new System.Drawing.Point(4, 22);
            this.tabShows.Name = "tabShows";
            this.tabShows.Padding = new System.Windows.Forms.Padding(3);
            this.tabShows.Size = new System.Drawing.Size(974, 462);
            this.tabShows.TabIndex = 0;
            this.tabShows.Text = "Shows";
            this.tabShows.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelShows
            // 
            this.tableLayoutPanelShows.ColumnCount = 1;
            this.tableLayoutPanelShows.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelShows.Controls.Add(this.dataGridViewShows, 0, 0);
            this.tableLayoutPanelShows.Controls.Add(this.btnScanNewShows, 0, 1);
            this.tableLayoutPanelShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelShows.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelShows.Name = "tableLayoutPanelShows";
            this.tableLayoutPanelShows.RowCount = 2;
            this.tableLayoutPanelShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.50096F));
            this.tableLayoutPanelShows.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.499035F));
            this.tableLayoutPanelShows.Size = new System.Drawing.Size(968, 456);
            this.tableLayoutPanelShows.TabIndex = 2;
            // 
            // dataGridViewShows
            // 
            this.dataGridViewShows.AllowUserToAddRows = false;
            this.dataGridViewShows.AllowUserToDeleteRows = false;
            this.dataGridViewShows.AutoGenerateColumns = false;
            this.dataGridViewShows.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewShows.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewShows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewShows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewShows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.shownameDataGridViewTextBoxColumn,
            this.tvdbidDataGridViewTextBoxColumn,
            this.tvdbnameDataGridViewTextBoxColumn,
            this.qualityDataGridViewTextBoxColumn,
            this.tvridDataGridViewTextBoxColumn,
            this.tvrnameDataGridViewTextBoxColumn,
            this.ignoreseasonDataGridViewTextBoxColumn,
            this.aliasesDataGridViewTextBoxColumn,
            this.airdayDataGridViewTextBoxColumn,
            this.airtimeDataGridViewTextBoxColumn,
            this.runtimeDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.posterurlDataGridViewTextBoxColumn,
            this.bannerurlDataGridViewTextBoxColumn,
            this.imdbidDataGridViewTextBoxColumn,
            this.genreDataGridViewTextBoxColumn,
            this.overviewDataGridViewTextBoxColumn});
            this.dataGridViewShows.DataSource = this.showsBindingSource;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewShows.DefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridViewShows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewShows.Location = new System.Drawing.Point(1, 1);
            this.dataGridViewShows.Margin = new System.Windows.Forms.Padding(1);
            this.dataGridViewShows.Name = "dataGridViewShows";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewShows.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridViewShows.Size = new System.Drawing.Size(966, 428);
            this.dataGridViewShows.TabIndex = 0;
            this.dataGridViewShows.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewShows_CellEndEdit);
            this.dataGridViewShows.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewShows_CellFormatting);
            this.dataGridViewShows.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridViewShows_CellParsing);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Width = 40;
            // 
            // shownameDataGridViewTextBoxColumn
            // 
            this.shownameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.shownameDataGridViewTextBoxColumn.DataPropertyName = "show_name";
            this.shownameDataGridViewTextBoxColumn.HeaderText = "show_name";
            this.shownameDataGridViewTextBoxColumn.Name = "shownameDataGridViewTextBoxColumn";
            this.shownameDataGridViewTextBoxColumn.Width = 89;
            // 
            // tvdbidDataGridViewTextBoxColumn
            // 
            this.tvdbidDataGridViewTextBoxColumn.DataPropertyName = "tvdb_id";
            this.tvdbidDataGridViewTextBoxColumn.HeaderText = "tvdb_id";
            this.tvdbidDataGridViewTextBoxColumn.Name = "tvdbidDataGridViewTextBoxColumn";
            // 
            // tvdbnameDataGridViewTextBoxColumn
            // 
            this.tvdbnameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tvdbnameDataGridViewTextBoxColumn.DataPropertyName = "tvdb_name";
            this.tvdbnameDataGridViewTextBoxColumn.HeaderText = "tvdb_name";
            this.tvdbnameDataGridViewTextBoxColumn.Name = "tvdbnameDataGridViewTextBoxColumn";
            this.tvdbnameDataGridViewTextBoxColumn.Width = 85;
            // 
            // qualityDataGridViewTextBoxColumn
            // 
            this.qualityDataGridViewTextBoxColumn.DataPropertyName = "quality";
            this.qualityDataGridViewTextBoxColumn.HeaderText = "quality";
            this.qualityDataGridViewTextBoxColumn.Name = "qualityDataGridViewTextBoxColumn";
            this.qualityDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qualityDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tvridDataGridViewTextBoxColumn
            // 
            this.tvridDataGridViewTextBoxColumn.DataPropertyName = "tvr_id";
            this.tvridDataGridViewTextBoxColumn.HeaderText = "tvr_id";
            this.tvridDataGridViewTextBoxColumn.Name = "tvridDataGridViewTextBoxColumn";
            // 
            // tvrnameDataGridViewTextBoxColumn
            // 
            this.tvrnameDataGridViewTextBoxColumn.DataPropertyName = "tvr_name";
            this.tvrnameDataGridViewTextBoxColumn.HeaderText = "tvr_name";
            this.tvrnameDataGridViewTextBoxColumn.Name = "tvrnameDataGridViewTextBoxColumn";
            // 
            // ignoreseasonDataGridViewTextBoxColumn
            // 
            this.ignoreseasonDataGridViewTextBoxColumn.DataPropertyName = "ignore_season";
            this.ignoreseasonDataGridViewTextBoxColumn.HeaderText = "ignore_season";
            this.ignoreseasonDataGridViewTextBoxColumn.MinimumWidth = 80;
            this.ignoreseasonDataGridViewTextBoxColumn.Name = "ignoreseasonDataGridViewTextBoxColumn";
            // 
            // aliasesDataGridViewTextBoxColumn
            // 
            this.aliasesDataGridViewTextBoxColumn.DataPropertyName = "aliases";
            this.aliasesDataGridViewTextBoxColumn.HeaderText = "aliases";
            this.aliasesDataGridViewTextBoxColumn.Name = "aliasesDataGridViewTextBoxColumn";
            // 
            // airdayDataGridViewTextBoxColumn
            // 
            this.airdayDataGridViewTextBoxColumn.DataPropertyName = "air_day";
            this.airdayDataGridViewTextBoxColumn.HeaderText = "air_day";
            this.airdayDataGridViewTextBoxColumn.Name = "airdayDataGridViewTextBoxColumn";
            // 
            // airtimeDataGridViewTextBoxColumn
            // 
            this.airtimeDataGridViewTextBoxColumn.DataPropertyName = "air_time";
            this.airtimeDataGridViewTextBoxColumn.HeaderText = "air_time";
            this.airtimeDataGridViewTextBoxColumn.Name = "airtimeDataGridViewTextBoxColumn";
            // 
            // runtimeDataGridViewTextBoxColumn
            // 
            this.runtimeDataGridViewTextBoxColumn.DataPropertyName = "run_time";
            this.runtimeDataGridViewTextBoxColumn.HeaderText = "run_time";
            this.runtimeDataGridViewTextBoxColumn.Name = "runtimeDataGridViewTextBoxColumn";
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            // 
            // posterurlDataGridViewTextBoxColumn
            // 
            this.posterurlDataGridViewTextBoxColumn.DataPropertyName = "poster_url";
            this.posterurlDataGridViewTextBoxColumn.HeaderText = "poster_url";
            this.posterurlDataGridViewTextBoxColumn.Name = "posterurlDataGridViewTextBoxColumn";
            // 
            // bannerurlDataGridViewTextBoxColumn
            // 
            this.bannerurlDataGridViewTextBoxColumn.DataPropertyName = "banner_url";
            this.bannerurlDataGridViewTextBoxColumn.HeaderText = "banner_url";
            this.bannerurlDataGridViewTextBoxColumn.Name = "bannerurlDataGridViewTextBoxColumn";
            // 
            // imdbidDataGridViewTextBoxColumn
            // 
            this.imdbidDataGridViewTextBoxColumn.DataPropertyName = "imdb_id";
            this.imdbidDataGridViewTextBoxColumn.HeaderText = "imdb_id";
            this.imdbidDataGridViewTextBoxColumn.Name = "imdbidDataGridViewTextBoxColumn";
            // 
            // genreDataGridViewTextBoxColumn
            // 
            this.genreDataGridViewTextBoxColumn.DataPropertyName = "genre";
            this.genreDataGridViewTextBoxColumn.HeaderText = "genre";
            this.genreDataGridViewTextBoxColumn.Name = "genreDataGridViewTextBoxColumn";
            // 
            // overviewDataGridViewTextBoxColumn
            // 
            this.overviewDataGridViewTextBoxColumn.DataPropertyName = "overview";
            this.overviewDataGridViewTextBoxColumn.HeaderText = "overview";
            this.overviewDataGridViewTextBoxColumn.Name = "overviewDataGridViewTextBoxColumn";
            // 
            // showsBindingSource
            // 
            this.showsBindingSource.DataSource = typeof(SABSync.shows);
            // 
            // btnScanNewShows
            // 
            this.btnScanNewShows.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnScanNewShows.Location = new System.Drawing.Point(888, 431);
            this.btnScanNewShows.Margin = new System.Windows.Forms.Padding(1, 1, 5, 0);
            this.btnScanNewShows.Name = "btnScanNewShows";
            this.btnScanNewShows.Size = new System.Drawing.Size(75, 25);
            this.btnScanNewShows.TabIndex = 1;
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
            this.tabHistory.Size = new System.Drawing.Size(974, 462);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelHistory
            // 
            this.tableLayoutPanelHistory.ColumnCount = 1;
            this.tableLayoutPanelHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHistory.Controls.Add(this.objectListViewHistory, 0, 0);
            this.tableLayoutPanelHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelHistory.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelHistory.Name = "tableLayoutPanelHistory";
            this.tableLayoutPanelHistory.RowCount = 2;
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.28806F));
            this.tableLayoutPanelHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.711944F));
            this.tableLayoutPanelHistory.Size = new System.Drawing.Size(968, 456);
            this.tableLayoutPanelHistory.TabIndex = 1;
            // 
            // tabPageFeeds
            // 
            this.tabPageFeeds.Controls.Add(this.tableLayoutPanelFeeds);
            this.tabPageFeeds.Location = new System.Drawing.Point(4, 22);
            this.tabPageFeeds.Name = "tabPageFeeds";
            this.tabPageFeeds.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeeds.Size = new System.Drawing.Size(974, 462);
            this.tabPageFeeds.TabIndex = 2;
            this.tabPageFeeds.Text = "RSS Feeds";
            this.tabPageFeeds.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelFeeds
            // 
            this.tableLayoutPanelFeeds.ColumnCount = 1;
            this.tableLayoutPanelFeeds.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFeeds.Controls.Add(this.objectListViewFeeds, 0, 0);
            this.tableLayoutPanelFeeds.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanelFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFeeds.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelFeeds.Name = "tableLayoutPanelFeeds";
            this.tableLayoutPanelFeeds.RowCount = 2;
            this.tableLayoutPanelFeeds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.24626F));
            this.tableLayoutPanelFeeds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.753735F));
            this.tableLayoutPanelFeeds.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFeeds.Size = new System.Drawing.Size(968, 456);
            this.tableLayoutPanelFeeds.TabIndex = 1;
            // 
            // objectListViewFeeds
            // 
            this.objectListViewFeeds.AllColumns.Add(this.feed_id);
            this.objectListViewFeeds.AllColumns.Add(this.feed_name);
            this.objectListViewFeeds.AllColumns.Add(this.feed_url);
            this.objectListViewFeeds.AlternateRowBackColor = System.Drawing.SystemColors.ButtonFace;
            this.objectListViewFeeds.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.objectListViewFeeds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.feed_id,
            this.feed_name,
            this.feed_url});
            this.objectListViewFeeds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewFeeds.FullRowSelect = true;
            this.objectListViewFeeds.Location = new System.Drawing.Point(3, 3);
            this.objectListViewFeeds.MultiSelect = false;
            this.objectListViewFeeds.Name = "objectListViewFeeds";
            this.objectListViewFeeds.ShowGroups = false;
            this.objectListViewFeeds.Size = new System.Drawing.Size(962, 423);
            this.objectListViewFeeds.TabIndex = 0;
            this.objectListViewFeeds.UseAlternatingBackColors = true;
            this.objectListViewFeeds.UseCompatibleStateImageBehavior = false;
            this.objectListViewFeeds.View = System.Windows.Forms.View.Details;
            this.objectListViewFeeds.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.objectListViewFeeds_CellEditFinishing);
            // 
            // feed_id
            // 
            this.feed_id.AspectName = "id";
            this.feed_id.IsEditable = false;
            this.feed_id.IsVisible = false;
            this.feed_id.MaximumWidth = 0;
            this.feed_id.MinimumWidth = 0;
            this.feed_id.Text = "ID";
            this.feed_id.Width = 0;
            // 
            // feed_name
            // 
            this.feed_name.AspectName = "name";
            this.feed_name.MaximumWidth = 200;
            this.feed_name.MinimumWidth = 200;
            this.feed_name.Text = "Name";
            this.feed_name.Width = 200;
            // 
            // feed_url
            // 
            this.feed_url.AspectName = "url";
            this.feed_url.FillsFreeSpace = true;
            this.feed_url.Text = "URL";
            this.feed_url.Width = 400;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 362F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddFeed, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 429);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(968, 27);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnAddFeed
            // 
            this.btnAddFeed.Location = new System.Drawing.Point(882, 0);
            this.btnAddFeed.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddFeed.Name = "btnAddFeed";
            this.btnAddFeed.Size = new System.Drawing.Size(75, 25);
            this.btnAddFeed.TabIndex = 0;
            this.btnAddFeed.Text = "Add";
            this.btnAddFeed.UseVisualStyleBackColor = true;
            this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(984, 22);
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
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 18);
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
            this.optionsToolStripMenuItem1});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 18);
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
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator5,
            this.donateToolStripMenuItem,
            this.websiteToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 18);
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
            // sABSyncEntitiesBindingSource
            // 
            this.sABSyncEntitiesBindingSource.DataSource = typeof(SABSync.SABSyncEntities);
            // 
            // sABSyncEntitiesBindingSource1
            // 
            this.sABSyncEntitiesBindingSource1.DataSource = typeof(SABSync.SABSyncEntities);
            // 
            // historiesBindingSource
            // 
            this.historiesBindingSource.DataSource = typeof(SABSync.histories);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStripLabel});
            this.statusMain.Location = new System.Drawing.Point(0, 511);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(984, 21);
            this.statusMain.TabIndex = 3;
            this.statusMain.Text = "Status Strip...";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(53, 16);
            this.StatusStripLabel.Text = "SABSync";
            // 
            // timerUpdateCache
            // 
            this.timerUpdateCache.Interval = 3600000;
            this.timerUpdateCache.Tick += new System.EventHandler(this.timerUpdateCache_Tick);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMain.Controls.Add(this.menuStripMain, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.statusMain, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.tabControlMain, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.453441F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.54656F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(984, 532);
            this.tableLayoutPanelMain.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(798, 0);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 25);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // objectListViewHistory
            // 
            this.objectListViewHistory.AllColumns.Add(this.history_show_name);
            this.objectListViewHistory.AllColumns.Add(this.history_season_number);
            this.objectListViewHistory.AllColumns.Add(this.history_episode_number);
            this.objectListViewHistory.AllColumns.Add(this.history_episode_name);
            this.objectListViewHistory.AllColumns.Add(this.history_feed_title);
            this.objectListViewHistory.AllColumns.Add(this.history_quality);
            this.objectListViewHistory.AllColumns.Add(this.history_proper);
            this.objectListViewHistory.AllColumns.Add(this.history_provider);
            this.objectListViewHistory.AllColumns.Add(this.history_date);
            this.objectListViewHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.history_show_name,
            this.history_season_number,
            this.history_episode_number,
            this.history_episode_name,
            this.history_feed_title,
            this.history_quality,
            this.history_proper,
            this.history_provider,
            this.history_date});
            this.objectListViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListViewHistory.FullRowSelect = true;
            this.objectListViewHistory.Location = new System.Drawing.Point(3, 3);
            this.objectListViewHistory.Name = "objectListViewHistory";
            this.objectListViewHistory.ShowItemCountOnGroups = true;
            this.objectListViewHistory.Size = new System.Drawing.Size(962, 423);
            this.objectListViewHistory.TabIndex = 0;
            this.objectListViewHistory.UseCompatibleStateImageBehavior = false;
            this.objectListViewHistory.View = System.Windows.Forms.View.Details;
            this.objectListViewHistory.SelectedIndexChanged += new System.EventHandler(this.objectListViewHistory_SelectedIndexChanged);
            // 
            // history_show_name
            // 
            this.history_show_name.AspectName = "show_name";
            this.history_show_name.FillsFreeSpace = true;
            this.history_show_name.MinimumWidth = 80;
            this.history_show_name.Text = "Show Name";
            this.history_show_name.Width = 100;
            // 
            // history_season_number
            // 
            this.history_season_number.AspectName = "season_number";
            this.history_season_number.FillsFreeSpace = true;
            this.history_season_number.MaximumWidth = 90;
            this.history_season_number.MinimumWidth = 90;
            this.history_season_number.Text = "Season Number";
            this.history_season_number.Width = 90;
            // 
            // history_episode_number
            // 
            this.history_episode_number.AspectName = "episode_number";
            this.history_episode_number.FillsFreeSpace = true;
            this.history_episode_number.MaximumWidth = 90;
            this.history_episode_number.MinimumWidth = 90;
            this.history_episode_number.Text = "Episode Number";
            this.history_episode_number.Width = 90;
            // 
            // history_episode_name
            // 
            this.history_episode_name.AspectName = "episode_name";
            this.history_episode_name.MinimumWidth = 90;
            this.history_episode_name.Text = "Episode Name";
            this.history_episode_name.Width = 90;
            // 
            // history_feed_title
            // 
            this.history_feed_title.AspectName = "feed_title";
            this.history_feed_title.FillsFreeSpace = true;
            this.history_feed_title.MinimumWidth = 160;
            this.history_feed_title.Text = "Feed Title";
            this.history_feed_title.Width = 160;
            // 
            // history_quality
            // 
            this.history_quality.AspectName = "quality";
            this.history_quality.MaximumWidth = 50;
            this.history_quality.MinimumWidth = 50;
            this.history_quality.Text = "Quality";
            this.history_quality.Width = 50;
            // 
            // history_proper
            // 
            this.history_proper.AspectName = "proper";
            this.history_proper.MaximumWidth = 50;
            this.history_proper.MinimumWidth = 50;
            this.history_proper.Text = "Proper";
            this.history_proper.Width = 50;
            // 
            // history_provider
            // 
            this.history_provider.AspectName = "provider";
            this.history_provider.MaximumWidth = 120;
            this.history_provider.MinimumWidth = 60;
            this.history_provider.Text = "Provider";
            // 
            // history_date
            // 
            this.history_date.AspectName = "date";
            this.history_date.MaximumWidth = 150;
            this.history_date.MinimumWidth = 150;
            this.history_date.Text = "Date";
            this.history_date.Width = 150;
            // 
            // FrmMain
            // 
            this.ClientSize = new System.Drawing.Size(984, 532);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FrmMain";
            this.Text = "SABSync";
            this.Load += new System.EventHandler(this.Program_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.contextMenuStripTray.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabShows.ResumeLayout(false);
            this.tableLayoutPanelShows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showsBindingSource)).EndInit();
            this.tabHistory.ResumeLayout(false);
            this.tableLayoutPanelHistory.ResumeLayout(false);
            this.tabPageFeeds.ResumeLayout(false);
            this.tableLayoutPanelFeeds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewFeeds)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiesBindingSource)).EndInit();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListViewHistory)).EndInit();
            this.ResumeLayout(false);

        }

        private void Program_Load(object sender, EventArgs e)
        {
            this.Text = String.Format("{0} v{1}", App.Name, App.Version);
            //Create the Database if needed
            CreateDatabase();

            SetSyncInterval();
            //Run a Sync at the Start if Configured to
            if (Config.SyncOnStart)
                StartSync();

            GetShows();
            GetHistory();
            GetFeeds();
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
                notifyIconTray.ShowBalloonTip(500);
                Hide();
            }
        }

        private void notifyIconTray_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
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
            thread.Start();    
        }

        private void SyncThread()
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                Logger.Log("=====================================================================");
                Logger.Log("Starting {0} v{1} - Build Date: {2:D}", App.Name, App.Version, App.BuildDate);
                Logger.Log("Current System Time: {0}", DateTime.Now);
                Logger.Log("=====================================================================");

                Logger.DeleteLogs();

                var job = new SyncJob();
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
            frmOptions.ShowDialog();
            frmOptions.FormClosed +=new FormClosedEventHandler(frmOptions_FormClosed);
        }

        private void GetShows()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var shows = from s in sabSyncEntities.shows select s;
                dataGridViewShows.DataSource = shows;

                //Create the Datasource for the Qualities Drop Box
                DataTable tableSource = new DataTable("tableSource");
                tableSource.Columns.AddRange(new DataColumn[] {
                        new DataColumn("id"),
                        new DataColumn("quality") });
                tableSource.Columns["id"].DataType = System.Type.GetType("System.Int64");
                tableSource.Rows.Add(Convert.ToInt64(0), "Best Possible");
                tableSource.Rows.Add(Convert.ToInt64(1), "xvid");
                tableSource.Rows.Add(Convert.ToInt64(2), "720p");

                qualityDataGridViewTextBoxColumn.DataSource = tableSource;
                qualityDataGridViewTextBoxColumn.DisplayMember = "quality";
                qualityDataGridViewTextBoxColumn.ValueMember = "id";

                //Hide the Episodes amd Histories columns from the Show DGV
                tvridDataGridViewTextBoxColumn.Visible = false;
                tvrnameDataGridViewTextBoxColumn.Visible = false;
                posterurlDataGridViewTextBoxColumn.Visible = false;
                bannerurlDataGridViewTextBoxColumn.Visible = false;
                imdbidDataGridViewTextBoxColumn.Visible = false;
                genreDataGridViewTextBoxColumn.Visible = false;
                overviewDataGridViewTextBoxColumn.Visible = false;
            }
        }

        private void GetHistory()
        {
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var history = from h in sabSyncEntities.histories
                              select
                                  new
                                      {
                                          h.shows.show_name,
                                          h.episodes.season_number,
                                          h.episodes.episode_number,
                                          h.episodes.episode_name,
                                          h.feed_title,
                                          h.quality,
                                          h.proper,
                                          h.provider,
                                          h.date
                                      };

                objectListViewHistory.SetObjects(history.ToList());
                //dataGridViewHistory.Columns[0].HeaderText = "Show Name";
                //dataGridViewHistory.Columns[1].HeaderText = "Season Number";
                //dataGridViewHistory.Columns[2].HeaderText = "Episode Number";
                //dataGridViewHistory.Columns[3].HeaderText = "Episode Name";
                //dataGridViewHistory.Columns[4].HeaderText = "Feed Title";
                //dataGridViewHistory.Columns[5].HeaderText = "Quality";
                //dataGridViewHistory.Columns[6].HeaderText = "Proper";
                //dataGridViewHistory.Columns[7].HeaderText = "Provider";
                //dataGridViewHistory.Columns[8].HeaderText = "Date";
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

        private void dataGridViewShows_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // If the column is the Quality column, check the
            // value.
            if (this.dataGridViewShows.Columns[e.ColumnIndex].Name == "qualityDataGridViewTextBoxColumn")
            {
                if (e.Value != null)
                {
                    string stringValue = (string)e.Value.ToString();
                    stringValue = stringValue.ToLower();
                    if (stringValue == "0")
                        e.Value = "Best Possible";

                    else if (stringValue == "1")
                        e.Value = "xvid";

                    if (stringValue == "2")
                        e.Value = "720p";
                }
            }
        }

        private void dataGridViewShows_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (this.dataGridViewShows.Columns[e.ColumnIndex].Name == "qualityDataGridViewTextBoxColumn")
            {
                if (e.Value != null)
                {
                    string stringValue = (string)e.Value;
                    stringValue = stringValue.ToLower();
                    if (stringValue == "Best Possible")
                    {
                        e.Value = Convert.ToInt64(0);
                        //this.dataGridViewShows.CurrentCell.Value = Convert.ToInt64(0);
                        e.ParsingApplied = true;
                    }

                    else if (stringValue == "xvid")
                    {
                        e.Value = Convert.ToInt64(1);
                        //this.dataGridViewShows.CurrentCell.Value = Convert.ToInt64(1);
                        e.ParsingApplied = true;
                    }

                    if (stringValue == "720p")
                    {
                        //e.Value = Convert.ToInt64(2);
                        //this.dataGridViewShows.CurrentCell.Value = Convert.ToInt64(2);
                        e.ParsingApplied = true;
                    }
                }
            }
        }

        private void dataGridViewShows_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Save the DataGridView to DB with LINQ

            long showId = Convert.ToInt64(dataGridViewShows.Rows[e.RowIndex].Cells[0].Value);
            var columnName = dataGridViewShows.Columns[e.ColumnIndex].HeaderText;
            var change = dataGridViewShows[e.ColumnIndex, e.RowIndex].Value;

            if (columnName.Equals("quality", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == showId select s).First();
                    show.quality = Convert.ToInt64(change);
                    sabSyncEntities.shows.ApplyCurrentValues(show); //Apply the current values
                    sabSyncEntities.SaveChanges(); //Save them to the DB
                }
            }

            else if (columnName.Equals("show_name", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == showId select s).First();
                    show.show_name = Convert.ToString(change);
                    sabSyncEntities.shows.ApplyCurrentValues(show); //Apply the current values
                    sabSyncEntities.SaveChanges(); //Save them to the server
                }
            }

            else if (columnName.Equals("aliases", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == showId select s).First();
                    show.aliases = Convert.ToString(change);
                    sabSyncEntities.shows.ApplyCurrentValues(show); //Apply the current values
                    sabSyncEntities.SaveChanges(); //Save them to the server
                }
            }

            else if (columnName.Equals("ignore_season", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var show = (from s in sabSyncEntities.shows where s.id == showId select s).First();
                    show.ignore_season = Convert.ToInt32(change);
                    sabSyncEntities.shows.ApplyCurrentValues(show); //Apply the current values
                    sabSyncEntities.SaveChanges(); //Save them to the server
                }
            }
        }

        private void frmOptions_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Update the Timer if needed...
            Config.ReloadConfig();
            if (_interval != Config.Interval)
            {
                SetSyncInterval();
            }
        }

        private void btnScanNewShows_Click(object sender, EventArgs e)
        {
            Thread dbThread = new Thread(UpdateShows);
            dbThread.Start();
            
        }

        private void UpdateShows()
        {
            //Config.ReloadConfig();
            Database db = new Database(this);
            db.ShowsOnDiskToDatabase();
            GetShowsInvoke();
        }

        private void GetShowsInvoke()
        {
            if (this.dataGridViewShows.InvokeRequired)
            {
                this.dataGridViewShows.BeginInvoke(
                    new MethodInvoker(
                    delegate() { GetShows(); }));
                return;
            }

            GetShows();
        }

        public void UpdateStatusBar(string text)
        {
            if (this.statusMain.InvokeRequired)
            {
                this.statusMain.BeginInvoke(
                    new MethodInvoker(
                    delegate() { UpdateStatusBar(text); }));
            }
            else
            {
                this.statusMain.Items["StatusStripLabel"].Text = text;
            }
        }

        private void timerUpdateCache_Tick(object sender, EventArgs e)
        {
            Database db = new Database();
            db.GetTvDbUpdates();
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
                    var feed = (from f in sabSyncEntities.providers where f.url.Equals(oldValue) select f).First();

                    feed.url = newValue;
                    sabSyncEntities.providers.ApplyCurrentValues(feed);
                    sabSyncEntities.SaveChanges();
                }
            }

            if (columnName.Equals("name", StringComparison.InvariantCultureIgnoreCase))
            {
                using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
                {
                    var feed = (from f in sabSyncEntities.providers where f.name.Equals(oldValue) select f).First();

                    feed.name = newValue;
                    sabSyncEntities.providers.ApplyCurrentValues(feed);
                    sabSyncEntities.SaveChanges();
                }
            }
        }

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            FrmAddFeed frmAddFeed = new FrmAddFeed();
            frmAddFeed.ShowDialog();
            frmAddFeed.FormClosed +=new FormClosedEventHandler(frmAddFeed_FormClosed);
        }

        private void frmAddFeed_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Update objectListViewFeeds
            
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var feeds = from f in sabSyncEntities.providers select f;
                objectListViewFeeds.SetObjects(feeds.ToList());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get Selected Row and Delete it from DB and reload table

            if (MessageBox.Show("Are you sure you want to delete this feed?", "Confirm Delete", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            int id = Convert.ToInt32(objectListViewFeeds.SelectedItem.Text);

            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var delete = (from d in sabSyncEntities.providers where d.id == id select d).First();
                sabSyncEntities.providers.DeleteObject(delete);
                sabSyncEntities.SaveChanges();

                var feeds = from f in sabSyncEntities.providers select f;
                objectListViewFeeds.SetObjects(feeds.ToList());
            }
        }

        private void objectListViewHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}