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

        Traffic t = new Traffic();

        private void TA_Load(object sender, System.EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            TrafficStatDay d = t.getByDay(dateTimePicker1.Value);
            string s = "";
            for (int a = 0; a != d.WebSites.Count; a++)
            {
                s += d.WebSites[a] + " " + d.SpentTraffic[a] + "\r\n";
            }
            s += d.TotalSpentTraffic;
            MessageBox.Show(s);
            //MessageBox.Show(t.getByWeek(dateTimePicker1.Value).TotalSpentTraffic.ToString());
            long b = t.convertBytes(t.getByWeek(dateTimePicker1.Value).TotalSpentTraffic)[0];
            int c = Convert.ToInt32(b);
            notifyIcon.Icon = new SystemTray().getIcon(c);
        }
    }
}
