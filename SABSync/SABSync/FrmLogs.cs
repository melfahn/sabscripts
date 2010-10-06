using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SABSync
{
    public partial class FrmLogs : Form
    {
        public FrmLogs()
        {
            InitializeComponent();
            PopulateList();    
        }

        private void PopulateList()
        {
            //Directory is AppDir + log
            DirectoryInfo logInfo = new DirectoryInfo(App.ExecutablePath + Path.DirectorySeparatorChar + "log");
            if (!logInfo.Exists)
                return;

            // Group by month-year, rather than date
            this.logsModified.GroupKeyGetter = delegate(object x)
            {
                DateTime dt = ((FileSystemInfo)x).LastWriteTime;
                return new DateTime(dt.Year, dt.Month, dt.Day);
            };
            this.logsModified.GroupKeyToTitleConverter = delegate(object x)
            {
                return ((DateTime)x).ToString("MMMM dd, yyyy");
            };

            objectListViewLogs.SetObjects(logInfo.GetFileSystemInfos("*.txt"));
            objectListViewLogs.Sort(logsModified, SortOrder.Descending);
        }

        private void objectListViewLogs_DoubleClick(object sender, EventArgs e)
        {
            //Get Selected Index, Open that file
            if (objectListViewLogs.SelectedItems.Count != 1)
                return;

            string fileName = objectListViewLogs.SelectedItem.Text;

            //Open the file
            string filePath = App.ExecutablePath + Path.DirectorySeparatorChar + "log" + Path.DirectorySeparatorChar +
                              fileName;
            Process.Start(filePath);
        }

        private void btnPurge_Click(object sender, EventArgs e)
        {
            DirectoryInfo logInfo = new DirectoryInfo(App.ExecutablePath + Path.DirectorySeparatorChar + "log");
            if (!logInfo.Exists)
                return;

            foreach (var file in logInfo.GetFiles("*.txt"))
                file.Delete();

            objectListViewLogs.Refresh();
        }
    }
}
