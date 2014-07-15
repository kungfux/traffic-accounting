/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) Fuks Alexander 2008-2014
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

using System.Windows.Forms;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace Traffic_Accounting
{
    public partial class TA : Form
    {
        public TA()
        {
            InitializeComponent();
        }

        private Traffic t = new Traffic();
        private TrafficFilter filter = new TrafficFilter();
        private ListView ListStat;
        private Label Label;
        private ListViewColumnSorter lvwColumnSorter;
        private Languages l = new Languages(ClientParams.Parameters.Language);
        /* WhatToRefresh
         * 0 - current week
         * 1 - Monday
         * 2 - Tuesday
         * ...
         * 8 - previous week
         */
        private int WhatToRefresh = 0;

        public event ConfChanged ConfigurationChanged;
        public delegate void ConfChanged();

        public void ConfigChanged()
        {
            if (ConfigurationChanged != null) ConfigurationChanged();
        }

        public event LangChanged LanguageChanged;
        public delegate void LangChanged();

        private void LanguageIsChanged()
        {
            if (LanguageChanged != null)
            {
                LanguageChanged();
            }
        }

        private void TA_Load(object sender, EventArgs e)
        {
            Translate();
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = lvwColumnSorter;

            ListStat = listView1;
            Label = label1;
        }

        // return monday of curent week
        private DateTime GetMonday()
        {
            DateTime dt = DateTime.Now;
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        // return first day of week of curent week
        private DateTime GetClientMonday()
        {
            DateTime dt = DateTime.Now;
            //while monday
            while (dt.DayOfWeek != DayOfWeek.Monday)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        // disable/enable existing days
        private void toolStripDay_DropDownOpening(object sender, EventArgs e)
        {
            for (int a = 0; a < 7; a++)
            {
                toolStripDropDownDay.DropDownItems[a].Enabled = false;
            }
            for (int a = (int)DateTime.Now.DayOfWeek; a > (int)DayOfWeek.Monday; a--)
            {
                toolStripDropDownDay.DropDownItems[a-2].Enabled = true;
            }
        }

        // fill statistic for selected day of week
        private void toolStripDay_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.UseWaitCursor = true;
            Label.Text = string.Format(l.GetMessage("TA001"), 
                l.GetMessage(((Day)toolStripDropDownDay.DropDownItems.IndexOf(e.ClickedItem)).ToString() + "R"));
            groupBox1.Controls.Clear();
            ListStat.Items.Clear();
            groupBox1.Controls.Add(ListStat);
            groupBox1.Controls.Add(Label);
            int i = toolStripDropDownDay.DropDownItems.IndexOf(e.ClickedItem) - ((int)DayOfWeek.Monday - 1);
            WhatToRefresh = i + 1;
            DateTime dt = GetClientMonday().AddDays(i);
            TrafficHistory h = t.getByDay(dt);
            if (h.IsLoaded)
            {
                long totalUnfiltered = 0;
                long totalFiltered = 0;
                for (int a = 0; a < h.WebSite.Count; a++)
                {
                    ListViewItem item = new ListViewItem(new string[] { (a+1).ToString(),
                    h.WebSite[a], t.getConvertedBytes(h.UsedTraffic[a]).ToString() });
                    if (ClientParams.Parameters.TrafficFilterEnabled &&
                        filter.isInList(h.WebSite[a]))
                    {
                        item.ForeColor = Color.Gray;
                        totalFiltered += h.UsedTraffic[a];
                    }
                    else
                    {
                        totalUnfiltered += h.UsedTraffic[a];
                    }
                    ListStat.Items.Add(item);
                }
                toolStripStatusLabel1.Text = string.Format(l.GetMessage("TA002"), t.getConvertedBytes(totalUnfiltered));
                if (ClientParams.Parameters.TrafficFilterEnabled)
                {
                    toolStripStatusLabel1.ToolTipText =
                        string.Format(
                        string.Concat(
                            l.GetMessage("TA010"),
                            Environment.NewLine,
                            l.GetMessage("TA012")),
                            t.getConvertedBytes(totalFiltered),
                            t.getConvertedBytes(totalFiltered + totalUnfiltered));
                }
                else
                {
                    toolStripStatusLabel1.ToolTipText = "";
                }
                if (ClientParams.Parameters.TOPenabled)
                {
                    toolStripStatusLabel1.Text += string.Format(" - " + l.GetMessage("TA003"),
                    h.TOP.Position, h.TOP.MaxPositions);
                }
            }
            else
            {
                MessageBox.Show(l.GetMessage("TA013"), l.GetMessage("PROGRAMNAME"),
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.UseWaitCursor = false;
        }

        // statistic for current week
        // based on daily stat
        private void currentWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            WhatToRefresh = 0;
            Label.Text = l.GetMessage("TA004");
            groupBox1.Controls.Clear();
            ListStat.Items.Clear();
            groupBox1.Controls.Add(ListStat);
            groupBox1.Controls.Add(Label);

            TrafficHistory h = new TrafficHistory();
            h = t.getByWeek(DateTime.Now);

            int a2 = 1;
            long totalUnfiltered = 0;
            long totalFiltered = 0;
            for (int a = 0; a < h.WebSite.Count; a++)
            {
                ListViewItem item = new ListViewItem(new string[] { (a2).ToString(), 
                    h.WebSite[a], t.getConvertedBytes(h.UsedTraffic[a]).ToString() });
                if (ClientParams.Parameters.TrafficFilterEnabled &&
                    filter.isInList(h.WebSite[a]))
                {
                    item.ForeColor = Color.Gray;
                    totalFiltered += h.UsedTraffic[a];
                }
                else
                {
                    totalUnfiltered += h.UsedTraffic[a];
                }
                ListStat.Items.Add(item);
                a2++;
            }
            toolStripStatusLabel1.Text = string.Format(l.GetMessage("TA002"), t.getConvertedBytes(totalUnfiltered));
            if (ClientParams.Parameters.TrafficFilterEnabled)
            {
                toolStripStatusLabel1.ToolTipText =
                    string.Format(
                    string.Concat(
                        l.GetMessage("TA010"),
                        Environment.NewLine,
                        l.GetMessage("TA012")),
                        t.getConvertedBytes(totalFiltered),
                        t.getConvertedBytes(totalFiltered + totalUnfiltered));
            }
            else
            {
                toolStripStatusLabel1.ToolTipText = "";
            }
            if (ClientParams.Parameters.TOPenabled)
            {
                int f = h.TOP.Position;
                if (f == 0)
                {
                    toolStripStatusLabel1.Text += " - " + l.GetMessage("TA005");
                }
                else
                {
                    toolStripStatusLabel1.Text += " - " + string.Format(l.GetMessage("TA006"),
                        f);
                }
            }
            if (!h.IsLoaded)
            {
                MessageBox.Show(l.GetMessage("TA013"), l.GetMessage("PROGRAMNAME"),
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.UseWaitCursor = false;
        }

        // statistic for previous week
        // based on weekly stat
        private void previousWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            WhatToRefresh = 8;
            Label.Text = l.GetMessage("TA007");
            groupBox1.Controls.Clear();
            ListStat.Items.Clear();
            groupBox1.Controls.Add(ListStat);
            groupBox1.Controls.Add(Label);
            TrafficHistory h = new TrafficHistory();
            DateTime prev = GetMonday().AddDays(-7);
            h = t.getByWeek(prev, false);
            if (h.IsLoaded)
            {
                int a2 = 1;
                long totalUnfiltered = 0;
                long totalFiltered = 0;
                for (int a = 0; a < h.WebSite.Count; a++)
                {
                    ListViewItem item = new ListViewItem(new string[] { (a2).ToString(),
                    h.WebSite[a], t.getConvertedBytes(h.UsedTraffic[a]).ToString() });
                    if (ClientParams.Parameters.TrafficFilterEnabled &&
                        filter.isInList(h.WebSite[a]))
                    {
                        item.ForeColor = Color.Gray;
                        totalFiltered += h.UsedTraffic[a];

                    }
                    else
                    {
                        totalUnfiltered += h.UsedTraffic[a];
                    }
                    ListStat.Items.Add(item);
                    a2++;
                }
                toolStripStatusLabel1.Text = string.Format(l.GetMessage("TA002"), t.getConvertedBytes(totalUnfiltered));
                if (ClientParams.Parameters.TrafficFilterEnabled)
                {
                    toolStripStatusLabel1.ToolTipText =
                        string.Format(
                        string.Concat(
                            l.GetMessage("TA010"),
                            Environment.NewLine,
                            l.GetMessage("TA012")),
                            t.getConvertedBytes(totalFiltered),
                            t.getConvertedBytes(totalFiltered + totalUnfiltered));
                }
                else
                {
                    toolStripStatusLabel1.ToolTipText = "";
                }
                if (ClientParams.Parameters.TOPenabled)
                {
                    toolStripStatusLabel1.Text += string.Format(" - " + l.GetMessage("TA003"),
                    h.TOP.Position, h.TOP.MaxPositions);
                }
            }
            else
            {
                MessageBox.Show(l.GetMessage("TA013"), l.GetMessage("PROGRAMNAME"),
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.UseWaitCursor = false;
        }

        public void callCurrentWeek()
        {
            currentWeekToolStripMenuItem.PerformClick();
        }

        public void callPrevWeek()
        {
            previousWeekToolStripMenuItem.PerformClick();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ListStat.SelectedItems.Count <= 0)
            {
                e.Cancel = true;
            }
            else
            {
                if (ListStat.SelectedItems[0].ForeColor != Color.Gray)
                {
                    addToFilterToolStripMenuItem.Text = l.GetMessage("TA009");
                }
                else
                {
                    addToFilterToolStripMenuItem.Text = l.GetMessage("TA011");
                }
                addToFilterToolStripMenuItem.Enabled = ClientParams.Parameters.TrafficFilterEnabled;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListStat.SelectedItems.Count > 0 && ListStat.SelectedItems[0].SubItems[1].Text.Length > 0)
            {
                Clipboard.SetText(ListStat.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(this, null);
        }

        private void addToFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListStat.SelectedItems.Count > 0)
            {
                if (ListStat.SelectedItems[0].ForeColor != Color.Gray)
                {
                    filter.addItem(ListStat.SelectedItems[0].SubItems[1].Text);
                }
                else
                {
                    filter.removeItem(ListStat.SelectedItems[0].SubItems[1].Text);
                }
                RefreshGrid();
            }
        }

        private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView1.Sort();
        }

        private void toolStripConfiguration_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";
            toolStripStatusLabel1.Text = "";
            groupBox1.Controls.Clear();
            Configuration c = new Configuration();
            c.ConfigurationChanged += new Configuration.ConfChanged(c_ConfigurationChanged);
            c.LanguageChanged += new Configuration.LangChanged(c_LanguageChanged);
            c.Dock = DockStyle.Fill;
            groupBox1.Controls.Add(c);
            c.Focus();
        }

        void c_LanguageChanged()
        {
            l = new Languages(ClientParams.Parameters.Language);
            Translate();
            t.LanguageChanged();
            LanguageIsChanged();
        }

        void c_ConfigurationChanged()
        {
            ConfigChanged();
        }

        private void Translate()
        {
            toolStripDropDownDay.Text = l.GetMessage("TAMENUDAY");
            toolStripDropDownWeek.Text = l.GetMessage("TAMENUWEEK");
            toolStripConfiguration.Text = l.GetMessage("TAMENUSETUP");
            toolStripHelp.Text = l.GetMessage("TAMENUHELP");
            mondayToolStripMenuItem.Text = l.GetMessage("Monday");
            tuesdayToolStripMenuItem.Text = l.GetMessage("Tuesday");
            wednesdayToolStripMenuItem.Text = l.GetMessage("Wednesday");
            thursdayToolStripMenuItem.Text = l.GetMessage("Thursday");
            fridayToolStripMenuItem.Text = l.GetMessage("Friday");
            saturdayToolStripMenuItem.Text = l.GetMessage("Saturday");
            sundayToolStripMenuItem.Text = l.GetMessage("Sunday");
            currentWeekToolStripMenuItem.Text = l.GetMessage("TASUBMENUCURRWEEK");
            previousWeekToolStripMenuItem.Text = l.GetMessage("TASUBMENUPREVWEEK");
            aboutToolStripMenuItem.Text = l.GetMessage("TAMENUABOUT");
            listView1.Columns[1].Text = l.GetMessage("TAColumn1");
            listView1.Columns[2].Text = l.GetMessage("TAColumn2");
            openToolStripMenuItem.Text = l.GetMessage("TA008");
            addToFilterToolStripMenuItem.Text = l.GetMessage("TA009");           
        }

        private void RefreshGrid()
        {
            switch (WhatToRefresh)
            {
                case 0:
                    currentWeekToolStripMenuItem_Click(this, null);
                    break;
                case 1:
                    mondayToolStripMenuItem.PerformClick();
                    break;
                case 2:
                    tuesdayToolStripMenuItem.PerformClick();
                    break;
                case 3:
                    wednesdayToolStripMenuItem.PerformClick();
                    break;
                case 4:
                    thursdayToolStripMenuItem.PerformClick();
                    break;
                case 5:
                    fridayToolStripMenuItem.PerformClick();
                    break;
                case 6:
                    saturdayToolStripMenuItem.PerformClick();
                    break;
                case 7:
                    sundayToolStripMenuItem.PerformClick();
                    break;
                case 8:
                    previousWeekToolStripMenuItem_Click(this, null);
                    break;
            }
            ConfigChanged();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";
            toolStripStatusLabel1.Text = "";
            AboutControl c = new AboutControl();
            c.Dock = DockStyle.Fill;
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(c);
        }

        public string getNotifyText()
        {
            Log.Trace.addTrace("Building notify text");

            string result = "";

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                result = l.GetMessage("TA007"); //Previous week's statistics
            }
            else
            {
                result = l.GetMessage("TA004"); //Current week's statistics
            }

            result += Environment.NewLine + Environment.NewLine;

            TrafficHistory h = new TrafficHistory();

            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
            {
                // get info for previous week
                DateTime prev = GetMonday().AddDays(-7);
                h = t.getByWeek(prev, false);
            }
            else
            {
                // get info for current week
                h = t.getByWeek(DateTime.Now);
            }

            if (!h.IsLoaded)
            {
                // Error occur during retrieving statistics. 
                // It can be network or server error. 
                // Statistics can be wrong or partially filled!
                result = l.GetMessage("TA013");
                return result;
            }

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

            result += string.Format(l.GetMessage("TA002"), //Total used traffic: {0}
                    t.getConvertedBytes(totalUnfiltered));

            if (ClientParams.Parameters.TrafficFilterEnabled)
            {
                result += Environment.NewLine;
                result +=
                    string.Format(
                    string.Concat(
                        l.GetMessage("TA010"), //Total filtered traffic: {0}
                        Environment.NewLine,
                        l.GetMessage("TA012")), //Total together: {1}
                        t.getConvertedBytes(totalFiltered),
                        t.getConvertedBytes(totalFiltered + totalUnfiltered));
            }

            if (ClientParams.Parameters.TOPenabled)
            {
                result += Environment.NewLine;
                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    result += string.Format(l.GetMessage("TA003"),
                        h.TOP.Position, h.TOP.MaxPositions);
                }
                else
                {
                    int f = h.TOP.Position;
                    if (f == 0)
                    {
                        result += l.GetMessage("TA005"); //You will not be in TOP 10 this week
                    }
                    else
                    {
                        result += string.Format(l.GetMessage("TA006"), f); //{0}% chance to be in TOP 10 this week
                    }
                }
            }

            return result;
        }

        private void TA_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}