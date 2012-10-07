﻿/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2012
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
            while (dt.DayOfWeek != System.DayOfWeek.Monday)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        // return first day of week of curent week
        private DateTime GetClientMonday()
        {
            DateTime dt = DateTime.Now;
            //while (dt.DayOfWeek != ClientParams.Parameters.FirstDayOfTheWeek)
            while (DayOfWeek.Convert(dt.DayOfWeek) != ClientParams.Parameters.FirstDayOfTheWeek)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        // disable/enable existing days
        private void toolStripDay_DropDownOpening(object sender, EventArgs e)
        {
            //int b = GetDays();
            /*for (int a = 0; a < 7; a++)
            {
                if (b > a)
                {
                    toolStripDropDownDay.DropDownItems[a].Enabled = true;
                }
                else
                {
                    toolStripDropDownDay.DropDownItems[a].Enabled = false;
                }
            }*/
            for (int a = 0; a < 7; a++)
            {
                toolStripDropDownDay.DropDownItems[a].Enabled = false;
            }
            for (int a = (int)DayOfWeek.Convert(DateTime.Now.DayOfWeek) - 2; a >= (int)ClientParams.Parameters.FirstDayOfTheWeek - 1; a--)
            {
                toolStripDropDownDay.DropDownItems[a].Enabled = true;
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
            int i = toolStripDropDownDay.DropDownItems.IndexOf(e.ClickedItem) - ((int)ClientParams.Parameters.FirstDayOfTheWeek - 1);
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
            //int i = toolStripDropDownWeek.DropDownItems.IndexOf(e.ClickedItem);
            TrafficHistory h = new TrafficHistory();
            //if (i == 0)
            //{
            h = t.getByWeek(DateTime.Now);
            //}
            //else
            //{
            //    DateTime prev = GetMonday().AddDays(-1);
            //    h = t.getByWeek(prev);
            //}
            //
            //if (h.IsLoaded)
            //{
            int a2 = 1;
            long totalUnfiltered = 0;
            long totalFiltered = 0;
            for (int a = 0; a < h.WebSite.Count; a++)
            {
                //ListStat.Items.Add(new ListViewItem(new string[] { (a2).ToString(),
                //hd.WebSite[a], t.getConvertedBytes(hd.UsedTraffic[a]).ToString() }));
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
                //int f = t.getFortuneTelling();
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

        private void toolStripAboutButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "";
            toolStripStatusLabel1.Text = "";
            AboutControl c = new AboutControl();
            c.Dock = DockStyle.Fill;
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(c);
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
            if (ListStat.SelectedItems.Count > 0)
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
            toolStripAboutButton.Text = l.GetMessage("TAMENUABOUT");
            mondayToolStripMenuItem.Text = l.GetMessage("Monday");
            tuesdayToolStripMenuItem.Text = l.GetMessage("Tuesday");
            wednesdayToolStripMenuItem.Text = l.GetMessage("Wednesday");
            thursdayToolStripMenuItem.Text = l.GetMessage("Thursday");
            fridayToolStripMenuItem.Text = l.GetMessage("Friday");
            saturdayToolStripMenuItem.Text = l.GetMessage("Saturday");
            sundayToolStripMenuItem.Text = l.GetMessage("Sunday");
            currentWeekToolStripMenuItem.Text = l.GetMessage("TASUBMENUCURRWEEK");
            previousWeekToolStripMenuItem.Text = l.GetMessage("TASUBMENUPREVWEEK");
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
    }
}
