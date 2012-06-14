using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Traffic_Accounting
{
    public partial class Configuration : UserControl
    {
        public Configuration()
        {
            InitializeComponent();
        }

        bool loaded = false;
        ClientParams cp = new ClientParams();

        public event ConfChanged ConfigurationChanged;
        public delegate void ConfChanged();

        public void ConfigChanged()
        {
            if (ConfigurationChanged != null) ConfigurationChanged();
            cp.saveParams();
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            // display current setup
            checkBoxAutoStart.Checked = ClientParams.Parameters.AutoStart;
            numericUpDown2.Value = ClientParams.Parameters.TrafficLimitForWeek;
            checkBox2.Checked = ClientParams.Parameters.TrafficRoundUp;
            checkBox4.Checked = ClientParams.Parameters.TOPenabled;
            numericUpDown3.Value = ClientParams.Parameters.TrayTrafficRanges[0];
            numericUpDown4.Value = ClientParams.Parameters.TrayTrafficRanges[1];
            numericUpDown5.Value = ClientParams.Parameters.TrayTrafficRanges[2];
            numericUpDown4.Maximum = numericUpDown5.Value;
            numericUpDown3.Maximum = numericUpDown4.Value;
            checkBox3.Checked = ClientParams.Parameters.TrayDisplayDigits;
            if (ClientParams.Parameters.TrayDrawCircleInsteadOfSquare)
            {
                comboBox1.SelectedIndex = 1;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
            checkBox1.Checked = ClientParams.Parameters.TrafficCacheEnabled;
            numericUpDown1.Value = ClientParams.Parameters.TrafficCacheSize;
            loaded = true;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ClientParams.Parameters.AutoStart = checkBoxAutoStart.Checked;
                ClientParams.Parameters.TrafficRoundUp = checkBox2.Checked;
                ClientParams.Parameters.TOPenabled = checkBox4.Checked;
                ClientParams.Parameters.TrayDisplayDigits = checkBox3.Checked;
                ClientParams.Parameters.TrafficCacheEnabled = checkBox1.Checked;
                ConfigChanged();
            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            int a = (e.OldValue - e.NewValue) * this.Height;
            int b = a / 100;
            foreach (Control c in this.Controls)
            {
                c.Top += b;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ClientParams.Parameters.TrafficLimitForWeek = (int)numericUpDown2.Value;
                ClientParams.Parameters.TrayTrafficRanges[0] = (byte)numericUpDown3.Value;
                ClientParams.Parameters.TrayTrafficRanges[1] = (byte)numericUpDown4.Value;
                ClientParams.Parameters.TrayTrafficRanges[2] = (byte)numericUpDown5.Value;
                ClientParams.Parameters.TrafficCacheSize = (int)numericUpDown1.Value;
                ConfigChanged();
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ClientParams.Parameters.TrayDrawCircleInsteadOfSquare = comboBox1.SelectedIndex == 1;
                ConfigChanged();
            }
        }

        private void numericUpDown5_Leave(object sender, EventArgs e)
        {
            numericUpDown4.Maximum = numericUpDown5.Value;
        }

        private void numericUpDown4_Leave(object sender, EventArgs e)
        {
            numericUpDown3.Maximum = numericUpDown4.Value;
        }
    }
}
