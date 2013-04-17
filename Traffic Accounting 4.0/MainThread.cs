/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) Fuks Alexander 2008-2013
 *  
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *  
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *  
 *  You should have received a copy of the GNU General Public License along
 *  with this program; if not, write to the Free Software Foundation, Inc.,
 *  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 *  
 *  Fuks Alexander, hereby disclaims all copyright
 *  interest in the program "Traffic Accounting"
 *  (which makes passes at compilers)
 *  written by Alexander Fuks.
 */

using System;
using System.Windows.Forms;
using Traffic_Accounting.Properties;
using Traffic_Accounting.GUI;
using Microsoft.Win32;

namespace Traffic_Accounting
{
    internal class MainThread : ApplicationContext
    {
        private NotifyIcon notifyIcon;

        private Traffic t = new Traffic();
        private int dtLastChecked = 0;
        private TA StatForm;
        private WebBrowserSetup WebBrowserSetup = new WebBrowserSetup();
        private bool forceRefresh = false;
        private ToolStripMenuItem menuImages = new ToolStripMenuItem("");
        private Languages l = new Languages(ClientParams.Parameters.Language);
        private ToolStripMenuItem menuOpen;
        private ToolStripMenuItem menuExit;
        private TrafficFilter filter = new TrafficFilter();
        // when notify form was displayed last time
        private int displayNotifyLastDay = 0;
        // last back color for icon
        // needed due bad code of displaying notify form
        private int lastBackColor = 2;
        // single instance of NotifyForm
        NotifyForm notifyForm = null;
        
        public MainThread()
        {
            // contextMenu
            ContextMenuStrip menu = new ContextMenuStrip();
            menuOpen = new ToolStripMenuItem(l.GetMessage("MT001"));
            menuExit = new ToolStripMenuItem(l.GetMessage("MT002"));
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
            notifyIcon.MouseDoubleClick += new MouseEventHandler(notifyIcon_MouseDoubleClick);
            // timerCheckElapsed
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 14400000; // 4 hours
            timer.Tick += new System.EventHandler(this.timerCheckElapsed_Tick);
            timerCheckElapsed_Tick(this, null);
            // registry system events
            if (ClientParams.Parameters.DisplayNotify)
            {
                SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
                DisplayNotify();
            }
        }

        void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                openToolStripMenuItem_Click(this, null);
            }
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            Log.Trace.addTrace(string.Format("Session switched. Argument: {1} DisplayNotifyLastDay: {0}",
                displayNotifyLastDay, e.Reason));

            // display notify form in case new day is active
            // and session has been unlocked
            if ((e.Reason == SessionSwitchReason.SessionUnlock ||
                e.Reason == SessionSwitchReason.ConsoleConnect ||
                e.Reason == SessionSwitchReason.RemoteConnect) &&
                displayNotifyLastDay != DateTime.Now.Day)
            {
                DisplayNotify();
            }
        }

        private void DisplayNotify()
        {
            Log.Trace.addTrace("Displaying notify window");
            if (notifyForm != null)
            {
                if (!notifyForm.IsDisposed)
                {
                    notifyForm.Close();
                }
            }
            notifyForm = new NotifyForm(getNotifyText(), lastBackColor);
            notifyForm.Show();
            displayNotifyLastDay = DateTime.Now.Day;
        }

        void updateImageItem()
        {
            switch (WebBrowserSetup.getImageStatus())
            {
                case WebBrowserSetup.ImageStatus.Show:
                    menuImages.Text = l.GetMessage("MT004");
                    break;
                case WebBrowserSetup.ImageStatus.Hide:
                    menuImages.Text = l.GetMessage("MT003");
                    break;
                case WebBrowserSetup.ImageStatus.Unknown:
                    menuImages.Text = l.GetMessage("MT005");
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
            Log.Trace.addTrace("Opening statistics window");

            if (StatForm == null || StatForm.IsDisposed)
            {
                StatForm = new TA();
                StatForm.FormClosed += new FormClosedEventHandler(StatForm_FormClosed);
                StatForm.ConfigurationChanged += new TA.ConfChanged(StatForm_ConfigurationChanged);
                StatForm.LanguageChanged += new TA.LangChanged(StatForm_LanguageChanged);
            }

            StatForm.Show();
            StatForm.Activate();

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                StatForm.callPrevWeek();
            }
            else
            {
                StatForm.callCurrentWeek();
            }
        }

        void StatForm_LanguageChanged()
        {
            Log.Trace.addTrace("Language changed. Translating");
            l = new Languages(ClientParams.Parameters.Language);
            Translate();
        }

        private void Translate()
        {
            menuOpen.Text = l.GetMessage("MT001");
            menuExit.Text = l.GetMessage("MT002");
            updateImageItem();
        }

        void StatForm_ConfigurationChanged()
        {
            Log.Trace.addTrace("Configuration changed.");
            forceRefresh = true;
            timerCheckElapsed_Tick(this, null);
            
            if (ClientParams.Parameters.DisplayNotify)
            {
                SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
                SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            }
        }

        void StatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StatForm = null;
        }

        // close app
        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Log.Trace.addTrace("Exit from application");
            notifyIcon.Dispose();
            Application.Exit();
        }

        // timer tick - auto check remaining traffic
        private void timerCheckElapsed_Tick(object sender, EventArgs e)
        {
            Log.Trace.addTrace("Check elapsed traffic started");
            Log.Trace.addTrace(string.Format("Force refresh: {0}, last checked: {1}, current: {2}",
                forceRefresh,
                dtLastChecked,
                DateTime.Now.DayOfYear));

            if (forceRefresh || dtLastChecked < DateTime.Now.DayOfYear)
            {
                forceRefresh = false;
                TrafficHistory h = t.getByWeek(DateTime.Now);

                Log.Trace.addTrace(string.Format("Is statistics loaded: {0}", h.IsLoaded));

                long total = 0;
                for (int a = 0; a < h.WebSite.Count; a++)
                {
                    if (!ClientParams.Parameters.TrafficFilterEnabled)
                    {
                        total += h.UsedTraffic[a];
                    }
                    else if (!filter.isInList(h.WebSite[a]))
                    {
                        total += h.UsedTraffic[a];
                    }
                }
                total = t.convertBytes(total, 4, 4)[0];

                SystemTray tray = new SystemTray();
                int trafficRemains = ClientParams.Parameters.TrafficLimitForWeek - Convert.ToInt32(total);
                notifyIcon.Icon = tray.getIcon(trafficRemains);
                lastBackColor = tray.getRangesColorRepsentation(trafficRemains);
                dtLastChecked = DateTime.Now.DayOfYear;

                if (!h.IsLoaded)
                {
                    MessageBox.Show(l.GetMessage("TA013"), l.GetMessage("PROGRAMNAME"),
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string getNotifyText()
        {
            if (StatForm == null)
            {
                StatForm = new TA();
            }
            string result = StatForm.getNotifyText();
            StatForm.Close();
            return result;
        }
    }
}
