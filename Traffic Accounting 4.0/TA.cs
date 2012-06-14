using System.Windows.Forms;
using System;

namespace Traffic_Accounting
{
    public partial class TA : Form
    {
        public TA()
        {
            InitializeComponent();
        }

        private Traffic t = new Traffic();
        private DateTime dtLastChecked = DateTime.Now.AddDays(-1);
        private bool cancelClosing = true;
        private ListView ListStat;

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            cancelClosing = false;
            this.Close();
        }

        private void timerCheckElapsed_Tick(object sender, EventArgs e)
        {
            if (dtLastChecked.DayOfYear < DateTime.Now.DayOfYear)
            {
                long b = t.convertBytes(t.getByWeek(DateTime.Now).TotalUsedTraffic, 4, 4)[0];
                if (t.LastOperationCompletedSuccessfully)
                {
                    notifyIcon.Icon = new SystemTray().getIcon(100 - Convert.ToInt32(b));
                    dtLastChecked = DateTime.Now;
                }
            }
        }

        private void TA_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            ListStat = listView1;
            timerCheckElapsed_Tick(this, null);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void TA_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClosing;
            this.Visible = true;
            this.Hide();
        }

        private void TA_Shown(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openToolStripMenuItem_Click(this, null);
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
            label1.Text = " Statistic for " + e.ClickedItem.Text + " ";
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(ListStat);
            int i = toolStripDropDownDay.DropDownItems.IndexOf(e.ClickedItem);
            DateTime dt = GetMonday().AddDays(i);
            TrafficHistory h = t.getByDay(dt);
            for (int a = 0; a < h.WebSite.Count; a++)
            {
                ListStat.Items.Add(new ListViewItem(new string[] { (a+1).ToString(),
                    h.WebSite[a], t.getConvertedBytes(h.UsedTraffic[a]).ToString() }));
            }
            toolStripStatusLabel1.Text = "Total used traffic: " + t.getConvertedBytes(h.TotalUsedTraffic);
            this.UseWaitCursor = false;
        }

        // fill statistic for weeks
        private void toolStripDropDownButton2_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.UseWaitCursor = true;
            label1.Text = " Statistic for " + e.ClickedItem.Text + " ";
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(ListStat);
            int i = toolStripDropDownWeek.DropDownItems.IndexOf(e.ClickedItem);
            MergedTrafficHistory h = new MergedTrafficHistory();
            if (i == 0)
            {
                h = t.getByWeek(DateTime.Now);
            }
            else
            {
                DateTime prev = GetMonday().AddDays(-1);
                h = t.getByWeek(prev);
            }
            int a2 = 1;
            foreach (TrafficHistory hd in h.TrafficHistory)
            {
                for (int a = 0; a < hd.WebSite.Count; a++)
                {
                    ListStat.Items.Add(new ListViewItem(new string[] { (a2).ToString(),
                    hd.WebSite[a], t.getConvertedBytes(hd.UsedTraffic[a]).ToString() }));
                    a2++;
                }
            }
            toolStripStatusLabel1.Text = "Total used traffic: " + t.getConvertedBytes(h.TotalUsedTraffic);
            this.UseWaitCursor = false;
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
    }
}
