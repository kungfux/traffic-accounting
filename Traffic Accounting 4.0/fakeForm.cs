using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Traffic_Accounting.Properties;

namespace Traffic_Accounting
{
    internal class fakeForm : Form
    {
        private NotifyIcon notifyIcon;

        private Traffic t = new Traffic();
        private DateTime dtLastChecked = DateTime.Now.AddDays(-1);
        private TA StatForm;
        bool forceRefresh = false;

        public fakeForm()
        {
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(DefaultTimer_Load);
            this.FormClosing += new FormClosingEventHandler(fakeForm_FormClosing);
            // contextMenu
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem menuOpen = new ToolStripMenuItem("Open");
            ToolStripMenuItem menuExit = new ToolStripMenuItem("Exit");
            menu.Font = new System.Drawing.Font("Tahoma", 8.25F);
            menuOpen.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            menuExit.Font = new System.Drawing.Font("Tahoma", 8.25F);
            menuOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            menuExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            menuOpen,
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
        }

        void fakeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon.Visible = false;
        }

        void DefaultTimer_Load(object sender, EventArgs e)
        {
            timerCheckElapsed_Tick(this, null);
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
            this.Close();
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
