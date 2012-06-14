using System;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Traffic_Accounting.Properties;

namespace Traffic_Accounting
{
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
        }

        private int image = 0;

        private void AboutControl_Load(object sender, EventArgs e)
        {
            pictureBox1_Click(this, null);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            switch(image)
            {
                case 0:
                    pictureBox1.Image = Resources._1336866125_traffic_lights_red.ToBitmap();
                    break;
                case 1:
                    pictureBox1.Image = Resources._1336866141_traffic_lights_yellow.ToBitmap();
                    break;
                case 2:
                    pictureBox1.Image = Resources._1336866150_traffic_lights_green.ToBitmap();
                    image = -1;
                    break;
            }
            image++;
        }
    }
}
