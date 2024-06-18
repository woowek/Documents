using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fileMoniteringTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void execScan_Click(object sender, EventArgs e)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach(DriveInfo driveInfo in allDrives)
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = driveInfo.RootDirectory.FullName;

                watcher.NotifyFilter = NotifyFilters.FileName |
                                        NotifyFilters.DirectoryName |
                                        NotifyFilters.Size |
                                        NotifyFilters.LastAccess |
                                        NotifyFilters.CreationTime |
                                        NotifyFilters.LastWrite;

                watcher.Filter = "*.*";
                watcher.IncludeSubdirectories = true;

                // 4. 감시할 이벤트 설정 (생성, 변경..)
                watcher.Created += new FileSystemEventHandler(Changed);
                watcher.Changed += new FileSystemEventHandler(Changed);
                watcher.Renamed += new RenamedEventHandler(Renamed);
                watcher.EnableRaisingEvents = true;
            }
        }
        private void Changed(object source, FileSystemEventArgs e)
        {
            eventList.Items.Add("Changed : " + e.FullPath);
        }

        private void Renamed(object source, RenamedEventArgs e)
        {
            eventList.Items.Add("Renamed : " + e.FullPath);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // CloseReason.UserClosing : 사용자가 닫기 버튼을 누를 때
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // 폼 닫기 이벤트 취소
                e.Cancel = true;
                // 폼 숨기기
                this.Hide();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
