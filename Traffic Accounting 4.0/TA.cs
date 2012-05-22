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

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
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
            this.Visible = false;
            timerCheckElapsed_Tick(this, null);
        }
    }
}
