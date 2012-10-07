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
        Languages l = new Languages(ClientParams.Parameters.Language);

        private void AboutControl_Load(object sender, EventArgs e)
        {
            label1.Text = l.GetMessage("ABOUT");
            //pictureBox1_Click(this, null);
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