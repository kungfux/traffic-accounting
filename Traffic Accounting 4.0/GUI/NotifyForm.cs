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
using Traffic_Accounting.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;

namespace Traffic_Accounting.GUI
{
    public partial class NotifyForm : Form
    {
        private int DayStarted = DateTime.Now.DayOfYear;

        public NotifyForm(string text, int imagestate)
        {
            InitializeComponent();
            label1.Text = text;

            switch (imagestate)
            {
                case 0:
                    pictureBox1.Image = Resources._1336866323_traffic_lights.ToBitmap();
                    break;
                case 1:
                    pictureBox1.Image = Resources._1336866125_traffic_lights_red.ToBitmap();
                    break;
                case 2:
                    pictureBox1.Image = Resources._1336866141_traffic_lights_yellow.ToBitmap();
                    break;
                case 3:
                    pictureBox1.Image = Resources._1336866150_traffic_lights_green.ToBitmap();
                    break;
            }

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,
            Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            Timer timerToDestroy = new Timer();
            timerToDestroy.Interval = 60000; // 1 minute
            timerToDestroy.Tick += new System.EventHandler(timerToDestroy_Tick);
        }

        void timerToDestroy_Tick(object sender, System.EventArgs e)
        {
            if (DateTime.Now.DayOfYear > DayStarted)
            {
                this.Close();
            }
        }

        private void NotifyForm_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void NotifyForm_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rc, Color.WhiteSmoke, Color.LightBlue, 90F))
            {
                e.Graphics.FillRectangle(brush, rc);
            }
            using (Pen pen = new Pen(Brushes.LightGray))
            {
                e.Graphics.DrawRectangle(pen, rc);
            }

            this.Height = label1.Top + 10 +
                (int)e.Graphics.MeasureString(label1.Text, label1.Font, label1.Width).Height;
            this.Width = label1.Left * 2 +
                (int)e.Graphics.MeasureString(label1.Text, label1.Font, label1.Width).Width;

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,
            Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }
    }
}
