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
        private ListView ListStat;
        private Label Label;
        private ListViewColumnSorter lvwColumnSorter;

        public event ConfChanged ConfigurationChanged;
        public delegate void ConfChanged();

        public void ConfigChanged()
        {
            if (ConfigurationChanged != null) ConfigurationChanged();
        }

        private void TA_Load(object sender, EventArgs e)
        {
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
            while (dt.DayOfWeek != ClientParams.Parameters.FirstDayOfTheWeek)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        private int GetDays()
        {
            int result = 0;
            DateTime dt = GetMonday();
            while (dt.DayOfYear != DateTime.Now.DayOfYear)
            {
                dt = dt.AddDays(1);
                result++;
            }
            return result;
        }

        // disable/enable existing days
        private void toolStripDay_DropDownOpening(object sender, EventArgs e)
        {
            int b = GetDays();
            for (int a = 0; a < 7; a++)
            {
                if (b > a)
                {
                    toolStripDropDownDay.DropDownItems[a].Enabled = true;
                }
                else
                {
                    toolStripDropDownDay.DropDownItems[a].Enabled = false;
                }
            }
        }

        // fill statistic for selected day of week
        private void toolStripDay_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.UseWaitCursor = true;
            Label.Text = "Traffic statistic for " + e.ClickedItem.Text + ".";
            groupBox1.Controls.Clear();
            ListStat.Items.Clear();
            groupBox1.Controls.Add(ListStat);
            groupBox1.Controls.Add(Label);
            int i = toolStripDropDownDay.DropDownItems.IndexOf(e.ClickedItem);
            DateTime dt = GetMonday().AddDays(i);
            TrafficHistory h = t.getByDay(dt);
            for (int a = 0; a < h.WebSite.Count; a++)
            {
                ListStat.Items.Add(new ListViewItem(new string[] { (a+1).ToString(),
                    h.WebSite[a], t.getConvertedBytes(h.UsedTraffic[a]).ToString() }));
            }
            toolStripStatusLabel1.Text = "Total used traffic: " + t.getConvertedBytes(h.TotalUsedTraffic);
            if (ClientParams.Parameters.TOPenabled)
            {
                toolStripStatusLabel1.Text += string.Format(" | Your position in top is {0} out of {1}",
                h.TOP.Position, h.TOP.MaxPositions);
            }
            this.UseWaitCursor = false;
        }

        // statistic for current week
        // based on daily stat
        private void currentWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            Label.Text = "Current week's statistics.";
            groupBox1.Controls.Clear();
            ListStat.Items.Clear();
            groupBox1.Controls.Add(ListStat);
            groupBox1.Controls.Add(Label);
            //int i = toolStripDropDownWeek.DropDownItems.IndexOf(e.ClickedItem);
            List<TrafficHistory> h = new List<TrafficHistory>();
            //if (i == 0)
            //{
                h = t.getByWeek(DateTime.Now);
            //}
            //else
            //{
            //    DateTime prev = GetMonday().AddDays(-1);
            //    h = t.getByWeek(prev);
            //}
            int a2 = 1;
            long total = 0;
            foreach (TrafficHistory hd in h)
            {
                for (int a = 0; a < hd.WebSite.Count; a++)
                {
                    ListStat.Items.Add(new ListViewItem(new string[] { (a2).ToString(),
                    hd.WebSite[a], t.getConvertedBytes(hd.UsedTraffic[a]).ToString() }));
                    a2++;
                }
                total += hd.TotalUsedTraffic;
            }
            toolStripStatusLabel1.Text = "Total used traffic: " + t.getConvertedBytes(total);
            int f = t.getFortuneTelling();
            if (f == 0)
            {
                toolStripStatusLabel1.Text += string.Format(" | You will not be in top this week",
                    f);
            }
            else
            {
                toolStripStatusLabel1.Text += string.Format(" | You {0}% will be in top this week",
                    f);
            }
            this.UseWaitCursor = false;
        }

        // statistic for previous week
        // based on weekly stat
        private void previousWeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            Label.Text = "Previous week's statistics.";
            groupBox1.Controls.Clear();
            ListStat.Items.Clear();
            groupBox1.Controls.Add(ListStat);
            groupBox1.Controls.Add(Label);
            TrafficHistory h = new TrafficHistory();
            DateTime prev = GetMonday().AddDays(-7);
            h = t.getByWeek(prev, false);
            int a2 = 1;
            for (int a = 0; a < h.WebSite.Count; a++)
            {
                ListStat.Items.Add(new ListViewItem(new string[] { (a2).ToString(),
                    h.WebSite[a], t.getConvertedBytes(h.UsedTraffic[a]).ToString() }));
                a2++;
            }
            toolStripStatusLabel1.Text = "Total used traffic: " + t.getConvertedBytes(h.TotalUsedTraffic);
            if (ClientParams.Parameters.TOPenabled)
            {
                toolStripStatusLabel1.Text += string.Format(" | Your position in top is {0} out of {1}",
                h.TOP.Position, h.TOP.MaxPositions);
            }
            this.UseWaitCursor = false;
        }

        public void callCurrentWeek()
        {
            currentWeekToolStripMenuItem.PerformClick();
        }

        // sort elements
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch(e.Column)
            {
                case 0:
                    // sort by id
                    break;
                case 1:
                    // sort by website
                    break;
                case 2:
                    // sort by traffic amount
                    break;
            }
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
                // TODO: Add site to list
                //TrafficFilter f = new TrafficFilter();
                //f.addItem(ListStat.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void banInHostsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            Configuration c = new Configuration();
            c.ConfigurationChanged += new Configuration.ConfChanged(c_ConfigurationChanged);
            c.Dock = DockStyle.Fill;
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(c);
        }

        void c_ConfigurationChanged()
        {
            ConfigChanged();
        }
    }
}
