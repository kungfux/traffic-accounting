using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Traffic_Accounting.Properties;

namespace Traffic_Accounting
{
    internal class MainThread : ApplicationContext
    {
        private NotifyIcon notifyIcon;

        private Traffic t = new Traffic();
        private DateTime dtLastChecked = DateTime.Now.AddDays(-1);
        private TA StatForm;
        private WebBrowserSetup WebBrowserSetup = new WebBrowserSetup();
        bool forceRefresh = false;
        ToolStripMenuItem menuImages = new ToolStripMenuItem("");

        public MainThread()
        {
            // contextMenu
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem menuOpen = new ToolStripMenuItem("Open");
            ToolStripMenuItem menuExit = new ToolStripMenuItem("Exit");
            updateImageItem();
            menu.Font = new System.Drawing.Font("Tahoma", 8.25F);
            menuOpen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            menuExit.Font = new System.Drawing.Font("Tahoma", 8.25F);
            menuImages.Font = new System.Drawing.Font("Tahoma", 8.25F);
            menuOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            menuExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            menuImages.Click += new EventHandler(menuImages_Click);
            menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            menuOpen,
            menuImages,
            menuExit});
            // notifyIcon
            notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = menu;
            notifyIcon.Icon = Resources._1336866323_traffic_lights;
            notifyIcon.Text = "Traffic Accounting 4.0";
            notifyIcon.Visible = true;
            notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.openToolStripMenuItem_Click);
            // timerCheckElapsed
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 3600000;
            timer.Tick += new System.EventHandler(this.timerCheckElapsed_Tick);
            timerCheckElapsed_Tick(this, null);
        }

        void updateImageItem()
        {
            switch (WebBrowserSetup.getImageStatus())
            {
                case WebBrowserSetup.ImageStatus.Show:
                    menuImages.Text = "Disable images";
                    break;
                case WebBrowserSetup.ImageStatus.Hide:
                    menuImages.Text = "Enable images";
                    break;
                case WebBrowserSetup.ImageStatus.Unknown:
                    menuImages.Text = "Images inaccessable";
                    break;
            }
        }

        void menuImages_Click(object sender, EventArgs e)
        {
            WebBrowserSetup.switchImagesStatus();
            updateImageItem();
        }

        // open statistic form
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StatForm == null)
            {
                StatForm = new TA();
                StatForm.FormClosed += new FormClosedEventHandler(StatForm_FormClosed);
                StatForm.ConfigurationChanged += new TA.ConfChanged(StatForm_ConfigurationChanged);
                StatForm.Show();
                StatForm.Activate();
                StatForm.callCurrentWeek();
            }
            else
            {
                StatForm.Activate();
            }
        }

        void StatForm_ConfigurationChanged()
        {
            forceRefresh = true;
            timerCheckElapsed_Tick(this, null);
        }

        void StatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StatForm = null;
        }

        // close app
        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            notifyIcon.Dispose();
            Application.Exit();
        }

        // timer tick - auto check remaining traffic
        private void timerCheckElapsed_Tick(object sender, EventArgs e)
        {
            if (forceRefresh || dtLastChecked.DayOfYear < DateTime.Now.DayOfYear)
            {
                forceRefresh = false;
                List<TrafficHistory> h = t.getByWeek(DateTime.Now);
                long total = 0;
                foreach (TrafficHistory th in h)
                {
                    total += th.TotalUsedTraffic;
                }
                total = t.convertBytes(total, 4, 4)[0];
                if (t.LastOperationCompletedSuccessfully)
                {
                    notifyIcon.Icon = new SystemTray().getIcon(ClientParams.Parameters.TrafficLimitForWeek - Convert.ToInt32(total));
                    dtLastChecked = DateTime.Now;
                }
            }
        }
    }
}
