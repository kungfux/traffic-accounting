/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) Fuks Alexander 2008-2013
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

using System;
using System.Windows.Forms;

namespace Traffic_Accounting.GUI
{
    public partial class Welcome : Form
    {
        private Languages lang = new Languages(ClientParams.Parameters.Language);
        
        public Welcome()
        {
            InitializeComponent();
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            Translate();

            comboBox1.SelectedItem = ClientParams.Parameters.Language;
            
            if (comboBox2.SelectedIndex == -1)
            {
                FwServers.FwServer server = new FwServers().getCurrentlocation();
                if (server != FwServers.FwServer.Unknown)
                {
                    comboBox2.SelectedIndex = (int)server - 1;
                }
            }
        }

        private void Translate()
        {
            this.Text = string.Format(lang.GetMessage("WELCOME"),
                lang.GetMessage("PROGRAMNAME"));
            label1.Text = lang.GetMessage("WELCOMETEXT");
            label2.Text = lang.GetMessage("CONF005");
            label3.Text = lang.GetMessage("CONF031");
            button1.Text = lang.GetMessage("WELCOMESTART");
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(lang.GetMessage("CONF032").Split(','));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lang = new Languages(comboBox1.Text);
            int selectedLocation = comboBox2.SelectedIndex;
            Translate();
            comboBox2.SelectedIndex = selectedLocation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                ClientParams.Parameters.Language = comboBox1.Text;
                ClientParams.Parameters.Location = (FwServers.FwServer)comboBox2.SelectedIndex + 1;
                ClientParams.Parameters.Welcome = false;
                new ClientParams().saveParams();
                Application.Restart();
            }
            else
            {
                comboBox2.DroppedDown = true;
            }
        }
    }
}
