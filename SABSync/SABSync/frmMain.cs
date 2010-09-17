using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using System.Data.Objects;


namespace SABSync
{
    public class frmMain : System.Windows.Forms.Form
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
        private DataGridView dataGridViewHistory;
        private BindingSource sABSyncEntitiesBindingSource1;
        private BindingSource sABSyncEntitiesBindingSource;
        private BindingSource historiesBindingSource;

        private int _interval;

        private SQLite Sql = new SQLite();
        private TabPage tabPageFeeds; //Create new Instance of SQLite
        private Logger Logger = new Logger();
        private Button btnScanNewShows;
        private StatusStrip statusMain;
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
        private DataGridViewTextBoxColumn episodesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn historiesDataGridViewTextBoxColumn;
        private ToolStripStatusLabel StatusStripLabel;
        private System.Windows.Forms.Timer timerUpdateCache;
        private Config Config = new Config();

        frmMain()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            
            Application.Run(new frmMain());
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.notifyIconTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerSync = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabShows = new System.Windows.Forms.TabPage();
            this.btnScanNewShows = new System.Windows.Forms.Button();
            this.dataGridViewShows = new System.Windows.Forms.DataGridView();
            this.showsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.tabPageFeeds = new System.Windows.Forms.TabPage();
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
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sABSyncEntitiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sABSyncEntitiesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.historiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusMain = new System.Windows.Forms.StatusStrip();
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
            this.episodesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.historiesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerUpdateCache = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripTray.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabShows.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showsBindingSource)).BeginInit();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiesBindingSource)).BeginInit();
            this.statusMain.SuspendLayout();
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
            this.tabControlMain.Location = new System.Drawing.Point(2, 27);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(980, 482);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabShows
            // 
            this.tabShows.Controls.Add(this.btnScanNewShows);
            this.tabShows.Controls.Add(this.dataGridViewShows);
            this.tabShows.Location = new System.Drawing.Point(4, 22);
            this.tabShows.Name = "tabShows";
            this.tabShows.Padding = new System.Windows.Forms.Padding(3);
            this.tabShows.Size = new System.Drawing.Size(972, 456);
            this.tabShows.TabIndex = 0;
            this.tabShows.Text = "Shows";
            this.tabShows.UseVisualStyleBackColor = true;
            // 
            // btnScanNewShows
            // 
            this.btnScanNewShows.Location = new System.Drawing.Point(891, 427);
            this.btnScanNewShows.Name = "btnScanNewShows";
            this.btnScanNewShows.Size = new System.Drawing.Size(75, 23);
            this.btnScanNewShows.TabIndex = 1;
            this.btnScanNewShows.Text = "Scan";
            this.btnScanNewShows.UseVisualStyleBackColor = true;
            this.btnScanNewShows.Click += new System.EventHandler(this.btnScanNewShows_Click);
            // 
            // dataGridViewShows
            // 
            this.dataGridViewShows.AutoGenerateColumns = false;
            this.dataGridViewShows.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewShows.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewShows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            this.overviewDataGridViewTextBoxColumn,
            this.episodesDataGridViewTextBoxColumn,
            this.historiesDataGridViewTextBoxColumn});
            this.dataGridViewShows.DataSource = this.showsBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewShows.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewShows.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewShows.Name = "dataGridViewShows";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewShows.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewShows.Size = new System.Drawing.Size(960, 415);
            this.dataGridViewShows.TabIndex = 0;
            this.dataGridViewShows.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewShows_CellEndEdit);
            this.dataGridViewShows.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewShows_CellFormatting);
            this.dataGridViewShows.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridViewShows_CellParsing);
            // 
            // showsBindingSource
            // 
            this.showsBindingSource.DataSource = typeof(SABSync.shows);
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.dataGridViewHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(972, 456);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.Size = new System.Drawing.Size(960, 415);
            this.dataGridViewHistory.TabIndex = 0;
            // 
            // tabPageFeeds
            // 
            this.tabPageFeeds.Location = new System.Drawing.Point(4, 22);
            this.tabPageFeeds.Name = "tabPageFeeds";
            this.tabPageFeeds.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFeeds.Size = new System.Drawing.Size(952, 447);
            this.tabPageFeeds.TabIndex = 2;
            this.tabPageFeeds.Text = "RSS Feeds";
            this.tabPageFeeds.UseVisualStyleBackColor = true;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolsToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(984, 24);
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
            this.toolStripMenuItemRun.Size = new System.Drawing.Size(95, 22);
            this.toolStripMenuItemRun.Text = "&Run";
            this.toolStripMenuItemRun.Click += new System.EventHandler(this.toolStripMenuItemRun_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(92, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(95, 22);
            this.exitToolStripMenuItem1.Text = "E&xit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1});
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
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.toolStripSeparator5,
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
            this.statusMain.Location = new System.Drawing.Point(0, 510);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(984, 22);
            this.statusMain.TabIndex = 3;
            this.statusMain.Text = "Status Strip...";
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
            // episodesDataGridViewTextBoxColumn
            // 
            this.episodesDataGridViewTextBoxColumn.DataPropertyName = "episodes";
            this.episodesDataGridViewTextBoxColumn.HeaderText = "episodes";
            this.episodesDataGridViewTextBoxColumn.Name = "episodesDataGridViewTextBoxColumn";
            // 
            // historiesDataGridViewTextBoxColumn
            // 
            this.historiesDataGridViewTextBoxColumn.DataPropertyName = "histories";
            this.historiesDataGridViewTextBoxColumn.HeaderText = "histories";
            this.historiesDataGridViewTextBoxColumn.Name = "historiesDataGridViewTextBoxColumn";
            // 
            // StatusStripLabel
            // 
            this.StatusStripLabel.Name = "StatusStripLabel";
            this.StatusStripLabel.Size = new System.Drawing.Size(53, 17);
            this.StatusStripLabel.Text = "SABSync";
            // 
            // timerUpdateCache
            // 
            this.timerUpdateCache.Interval = 3600000;
            this.timerUpdateCache.Tick += new System.EventHandler(this.timerUpdateCache_Tick);
            // 
            // frmMain
            // 
            this.ClientSize = new System.Drawing.Size(984, 532);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "frmMain";
            this.Text = "SABSync";
            this.Load += new System.EventHandler(this.Program_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.contextMenuStripTray.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabShows.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showsBindingSource)).EndInit();
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sABSyncEntitiesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.historiesBindingSource)).EndInit();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
            frmAbout frmAbout = new frmAbout();
            frmAbout.Visible = true;
        }

        private void toolStripMenuItemRun_Click(object sender, EventArgs e)
        {
            StartSync();
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmOptions frmOptions = new frmOptions();
            frmOptions.Visible = true;
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
                episodesDataGridViewTextBoxColumn.Visible = false;
                historiesDataGridViewTextBoxColumn.Visible = false;
                tvridDataGridViewTextBoxColumn.Visible = false;
                tvrnameDataGridViewTextBoxColumn.Visible = false;
                posterurlDataGridViewTextBoxColumn.Visible = false;
                bannerurlDataGridViewTextBoxColumn.Visible = false;
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

                dataGridViewHistory.DataSource = history.ToList();
                dataGridViewHistory.Columns[0].HeaderText = "Show Name";
                dataGridViewHistory.Columns[1].HeaderText = "Season Number";
                dataGridViewHistory.Columns[2].HeaderText = "Episode Number";
                dataGridViewHistory.Columns[3].HeaderText = "Episode Name";
                dataGridViewHistory.Columns[4].HeaderText = "Feed Title";
                dataGridViewHistory.Columns[5].HeaderText = "Quality";
                dataGridViewHistory.Columns[6].HeaderText = "Proper";
                dataGridViewHistory.Columns[7].HeaderText = "Provider";
                dataGridViewHistory.Columns[8].HeaderText = "Date";

                foreach (DataGridViewColumn col in dataGridViewHistory.Columns)
                    col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
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
    }
}