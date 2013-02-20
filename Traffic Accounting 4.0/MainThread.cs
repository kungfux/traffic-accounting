/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2013
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
 *  IT WORKS TEAM, hereby disclaims all copyright
 *  interest in the program ".NET Assemblies Collection"
 *  (which makes passes at compilers)
 *  written by Alexander Fuks.
 * 
 *  Alexander Fuks, 01 July 2010
 *  IT WORKS TEAM, Founder of the team.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Traffic_Accounting.Properties;
using System.Drawing;

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
        Languages l = new Languages(ClientParams.Parameters.Language);
        Configuration c = new Configuration();
        ToolStripMenuItem menuOpen;
        ToolStripMenuItem menuExit;
        TrafficFilter filter = new TrafficFilter();
        // timer DisplayNotify
        Timer timerDisplayNotify;
        Point mousePoint = new Point(0, 0);
        int displayNotifyLastDay = 0;

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
            notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.openToolStripMenuItem_Click);
            // timerCheckElapsed
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 14400000; // 4 hours
            timer.Tick += new System.EventHandler(this.timerCheckElapsed_Tick);
            timerCheckElapsed_Tick(this, null);
            // timerDisplayNotify
            initTimerDisplayNotify();
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
            if (StatForm == null)
            {
                StatForm = new TA();
                StatForm.FormClosed += new FormClosedEventHandler(StatForm_FormClosed);
                StatForm.ConfigurationChanged += new TA.ConfChanged(StatForm_ConfigurationChanged);
                StatForm.LanguageChanged += new TA.LangChanged(StatForm_LanguageChanged);
                StatForm.Show();
                StatForm.Activate();
                //if (DateTime.Now.DayOfWeek == ClientParams.Parameters.FirstDayOfTheWeek)
                if (DayOfWeek.Convert(DateTime.Now.DayOfWeek) == ClientParams.Parameters.FirstDayOfTheWeek)
                {
                    StatForm.callPrevWeek();
                }
                else
                {
                    StatForm.callCurrentWeek();
                }
            }
            else
            {
                StatForm.Activate();
            }
        }

        void StatForm_LanguageChanged()
        {
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
            forceRefresh = true;
            timerCheckElapsed_Tick(this, null);
            // TODO: add for DisplayNotify
            if (ClientParams.Parameters.DisplayNotify)
            {
                initTimerDisplayNotify();
            }
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
                TrafficHistory h = t.getByWeek(DateTime.Now);
                //if (h.IsLoaded)
                //{
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
                //if (t.LastOperationCompletedSuccessfully)
                //{
                    notifyIcon.Icon = new SystemTray().getIcon(ClientParams.Parameters.TrafficLimitForWeek - Convert.ToInt32(total));
                    dtLastChecked = DateTime.Now;
                //}
                if (!h.IsLoaded)
                {
                    MessageBox.Show(l.GetMessage("TA013"), l.GetMessage("PROGRAMNAME"),
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void initTimerDisplayNotify()
        {
            // timerDisplayNotify
            if (timerDisplayNotify != null)
            {
                timerDisplayNotify.Dispose();
            }
            if (ClientParams.Parameters.DisplayNotify)
            {
                mousePoint = Cursor.Position;

                timerDisplayNotify = new Timer();
                timerDisplayNotify.Interval = 5000;
                timerDisplayNotify.Tick += new EventHandler(timerDisplayNotify_Tick);
                timerDisplayNotify.Start();
            }
        }

        void timerDisplayNotify_Tick(object sender, EventArgs e)
        {
            if (mousePoint != Cursor.Position && 
                displayNotifyLastDay != DateTime.Now.Day)
            {
                notifyIcon.ShowBalloonTip(0, l.GetMessage("PROGRAMNAME"),
                    getNotifyText(), ToolTipIcon.Info);
                mousePoint = Cursor.Position;
                displayNotifyLastDay = DateTime.Now.Day;
            }
        }

        public string getNotifyText()
        {
            string result = l.GetMessage("TA004");
            result += Environment.NewLine + Environment.NewLine;

            TrafficHistory h = new TrafficHistory();
            h = t.getByWeek(DateTime.Now);
            int a2 = 1;
            long totalUnfiltered = 0;
            long totalFiltered = 0;
            for (int a = 0; a < h.WebSite.Count; a++)
            {
                if (ClientParams.Parameters.TrafficFilterEnabled &&
                    filter.isInList(h.WebSite[a]))
                {
                    totalFiltered += h.UsedTraffic[a];
                }
                else
                {
                    totalUnfiltered += h.UsedTraffic[a];
                }
                a2++;
            }

            result += string.Format(l.GetMessage("TA002"),
                    t.getConvertedBytes(totalUnfiltered));

            if (ClientParams.Parameters.TrafficFilterEnabled)
            {
                result += Environment.NewLine;
                result +=
                    string.Format(
                    string.Concat(
                        l.GetMessage("TA010"),
                        Environment.NewLine,
                        l.GetMessage("TA012")),
                        t.getConvertedBytes(totalFiltered),
                        t.getConvertedBytes(totalFiltered + totalUnfiltered));
            }

            if (ClientParams.Parameters.TOPenabled)
            {
                result += Environment.NewLine + Environment.NewLine;

                int f = h.TOP.Position;
                if (f == 0)
                {
                    result += l.GetMessage("TA005");
                }
                else
                {
                    result += string.Format(l.GetMessage("TA006"), f);
                }
            }
            
            if (!h.IsLoaded)
            {
                result = l.GetMessage("TA013");
            }

            return result;
        }
    }
}
