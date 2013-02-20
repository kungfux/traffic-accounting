/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2013
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
        Languages l = new Languages(ClientParams.Parameters.Language);

        public event ConfChanged ConfigurationChanged;
        public delegate void ConfChanged();

        private void ConfigChanged()
        {
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged();
            }
            cp.saveParams();
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

        private void Configuration_Load(object sender, EventArgs e)
        {
            this.LanguageChanged -= new LangChanged(Configuration_LanguageChanged);
            this.LanguageChanged += new LangChanged(Configuration_LanguageChanged);
            Translate();

            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(new string[] {
                KnownColor.AliceBlue.ToString(), 
                KnownColor.AntiqueWhite.ToString(), 
                KnownColor.Aqua.ToString(),
                KnownColor.Aquamarine.ToString(), 
                KnownColor.Azure.ToString(), 
                KnownColor.Beige.ToString(),
                KnownColor.Bisque.ToString(), 
                KnownColor.Black.ToString(), 
                KnownColor.BlanchedAlmond.ToString(),
                KnownColor.Blue.ToString(), 
                KnownColor.BlueViolet.ToString(), 
                KnownColor.Brown.ToString(),
                KnownColor.BurlyWood.ToString(), 
                KnownColor.CadetBlue.ToString(), 
                KnownColor.Chartreuse.ToString(),
                KnownColor.Chocolate.ToString(),
                KnownColor.Coral.ToString(), 
                KnownColor.CornflowerBlue.ToString(),
                KnownColor.Cornsilk.ToString(), 
                KnownColor.Crimson.ToString(), 
                KnownColor.Cyan.ToString(),
                KnownColor.DarkBlue.ToString(), 
                KnownColor.DarkCyan.ToString(), 
                KnownColor.DarkGoldenrod.ToString(),
                KnownColor.DarkGray.ToString(), 
                KnownColor.DarkGreen.ToString(), 
                KnownColor.DarkKhaki.ToString(),
                KnownColor.DarkMagenta.ToString(), 
                KnownColor.DarkOliveGreen.ToString(), 
                KnownColor.DarkOrange.ToString(),
                KnownColor.DarkOrchid.ToString(), 
                KnownColor.DarkRed.ToString(), 
                KnownColor.DarkSalmon.ToString(),
                KnownColor.DarkSeaGreen.ToString(), 
                KnownColor.DarkSlateBlue.ToString(), 
                KnownColor.DarkSlateGray.ToString(),
                KnownColor.DarkTurquoise.ToString(), 
                KnownColor.DarkViolet.ToString(), 
                KnownColor.DeepPink.ToString(),
                KnownColor.DeepSkyBlue.ToString(), 
                KnownColor.DimGray.ToString(), 
                KnownColor.DodgerBlue.ToString(),
                KnownColor.Firebrick.ToString(), 
                KnownColor.FloralWhite.ToString(), 
                KnownColor.ForestGreen.ToString(),
                KnownColor.Fuchsia.ToString(), 
                KnownColor.Gainsboro.ToString(), 
                KnownColor.GhostWhite.ToString(),
                KnownColor.Gold.ToString(), 
                KnownColor.Goldenrod.ToString(), 
                KnownColor.Gray.ToString(),
                KnownColor.Green.ToString(), 
                KnownColor.GreenYellow.ToString(), 
                KnownColor.Honeydew.ToString(),
                KnownColor.HotPink.ToString(), 
                KnownColor.IndianRed.ToString(), 
                KnownColor.Indigo.ToString(),
                KnownColor.Ivory.ToString(), 
                KnownColor.Khaki.ToString(), 
                KnownColor.Lavender.ToString(),
                KnownColor.LavenderBlush.ToString(), 
                KnownColor.LawnGreen.ToString(), 
                KnownColor.LemonChiffon.ToString(),
                KnownColor.LightBlue.ToString(), 
                KnownColor.LightCoral.ToString(), 
                KnownColor.LightCyan.ToString(),
                KnownColor.LightGoldenrodYellow.ToString(), 
                KnownColor.LightGray.ToString(), 
                KnownColor.LightGreen.ToString(),
                KnownColor.LightPink.ToString(), 
                KnownColor.LightSalmon.ToString(), 
                KnownColor.LightSeaGreen.ToString(),
                KnownColor.LightSkyBlue.ToString(), 
                KnownColor.LightSlateGray.ToString(), 
                KnownColor.LightSteelBlue.ToString(),
                KnownColor.LightYellow.ToString(), 
                KnownColor.Lime.ToString(), 
                KnownColor.LimeGreen.ToString(),
                KnownColor.Linen.ToString(), 
                KnownColor.Magenta.ToString(), 
                KnownColor.Maroon.ToString(), 
                KnownColor.MediumAquamarine.ToString(), 
                KnownColor.MediumBlue.ToString(), 
                KnownColor.MediumOrchid.ToString(), 
                KnownColor.MediumPurple.ToString(), 
                KnownColor.MediumSeaGreen.ToString(), 
                KnownColor.MediumSlateBlue.ToString(), 
                KnownColor.MediumSpringGreen.ToString(), 
                KnownColor.MediumTurquoise.ToString(), 
                KnownColor.MediumVioletRed.ToString(), 
                KnownColor.MintCream.ToString(), 
                KnownColor.MistyRose.ToString(), 
                KnownColor.Moccasin.ToString(), 
                KnownColor.NavajoWhite.ToString(), 
                KnownColor.Navy.ToString(), 
                KnownColor.OldLace.ToString(), 
                KnownColor.Olive.ToString(), 
                KnownColor.OliveDrab.ToString(), 
                KnownColor.Orange.ToString(), 
                KnownColor.OrangeRed.ToString(), 
                KnownColor.Orchid.ToString(), 
                KnownColor.PaleGoldenrod.ToString(), 
                KnownColor.PaleGreen.ToString(), 
                KnownColor.PaleTurquoise.ToString(), 
                KnownColor.PaleVioletRed.ToString(), 
                KnownColor.PapayaWhip.ToString(), 
                KnownColor.PeachPuff.ToString(), 
                KnownColor.Peru.ToString(), 
                KnownColor.Pink.ToString(), 
                KnownColor.Plum.ToString(), 
                KnownColor.PowderBlue.ToString(), 
                KnownColor.Purple.ToString(), 
                KnownColor.Red.ToString(), 
                KnownColor.RosyBrown.ToString(), 
                KnownColor.RoyalBlue.ToString(), 
                KnownColor.SaddleBrown.ToString(), 
                KnownColor.Salmon.ToString(), 
                KnownColor.SandyBrown.ToString(), 
                KnownColor.SeaGreen.ToString(), 
                KnownColor.SeaShell.ToString(), 
                KnownColor.Sienna.ToString(), 
                KnownColor.Silver.ToString(), 
                KnownColor.SkyBlue.ToString(), 
                KnownColor.SlateBlue.ToString(), 
                KnownColor.SlateGray.ToString(), 
                KnownColor.Snow.ToString(), 
                KnownColor.SpringGreen.ToString(), 
                KnownColor.SteelBlue.ToString(), 
                KnownColor.Tan.ToString(), 
                KnownColor.Teal.ToString(), 
                KnownColor.Thistle.ToString(), 
                KnownColor.Tomato.ToString(), 
                KnownColor.Turquoise.ToString(), 
                KnownColor.Violet.ToString(), 
                KnownColor.Wheat.ToString(), 
                KnownColor.White.ToString(), 
                KnownColor.WhiteSmoke.ToString(), 
                KnownColor.Yellow.ToString(), 
                KnownColor.YellowGreen.ToString()
            });

            System.Drawing.Text.InstalledFontCollection installedFonts =
                new System.Drawing.Text.InstalledFontCollection();
            foreach (FontFamily ff in installedFonts.Families)
            {
                comboBox5.Items.Add(ff.Name);
            }

            // display current setup
            checkBoxAutoStart.Checked = ClientParams.Parameters.AutoStart;
            comboBox2.SelectedItem = ClientParams.Parameters.Language;
            numericUpDown2.Value = ClientParams.Parameters.TrafficLimitForWeek;
            checkBox2.Checked = ClientParams.Parameters.TrafficRoundUp;
            checkBox4.Checked = ClientParams.Parameters.TOPenabled;
            numericUpDown3.Value = ClientParams.Parameters.TrayTrafficRanges[0];
            numericUpDown4.Value = ClientParams.Parameters.TrayTrafficRanges[1];
            numericUpDown5.Value = ClientParams.Parameters.TrayTrafficRanges[2];
            numericUpDown4.Maximum = numericUpDown5.Value;
            numericUpDown3.Maximum = numericUpDown4.Value;
            checkBox3.Checked = ClientParams.Parameters.TrayDisplayDigits;
            comboBox1.SelectedIndex = ClientParams.Parameters.IconFashion;
            checkBox1.Checked = ClientParams.Parameters.TrafficCacheEnabled;
            numericUpDown1.Value = ClientParams.Parameters.TrafficCacheSize;
            checkBox5.Checked = ClientParams.Parameters.TrafficFilterEnabled;
            checkBox6.Checked = ClientParams.Parameters.DisplayNotify;
            comboBox4.Text = ClientParams.Parameters.TrayFontSize.ToString();
            comboBox5.Text = ClientParams.Parameters.TrayFontName;

            comboBox3.SelectedItem = (object)ClientParams.Parameters.TrayIconFontColor;
            comboBox3.Enabled = checkBox3.Checked;
            loaded = true;
        }

        void Configuration_LanguageChanged()
        {
            l = new Languages(ClientParams.Parameters.Language);
            loaded = false;
            Configuration_Load(this, null);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                comboBox3.Enabled = checkBox3.Checked;
                ClientParams.Parameters.AutoStart = checkBoxAutoStart.Checked;
                ClientParams.Parameters.TrafficRoundUp = checkBox2.Checked;
                ClientParams.Parameters.TOPenabled = checkBox4.Checked;
                ClientParams.Parameters.TrayDisplayDigits = checkBox3.Checked;
                ClientParams.Parameters.TrafficCacheEnabled = checkBox1.Checked;
                ClientParams.Parameters.TrafficFilterEnabled = checkBox5.Checked;
                ClientParams.Parameters.DisplayNotify = checkBox6.Checked;
                ConfigChanged();
            }
        }

        //private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        //{
        //    int a = (e.OldValue - e.NewValue) * this.Height;
        //    int b = a / 100;
        //    foreach (Control c in this.Controls)
        //    {
        //        c.Top += b;
        //    }
        //}

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
                if (comboBox1.SelectedIndex == 0)
                {
                    checkBox3.Checked = true;
                    checkBox3.Enabled = false;
                }
                else
                {
                    checkBox3.Enabled = true;
                }
                ClientParams.Parameters.IconFashion = comboBox1.SelectedIndex;
                if (comboBox3.SelectedItem != null) // just check for an error
                {
                    ClientParams.Parameters.TrayIconFontColor = comboBox3.SelectedItem.ToString();
                }
                int.TryParse(comboBox4.Text, out ClientParams.Parameters.TrayFontSize);
                ClientParams.Parameters.TrayFontName = comboBox5.Text;
                ConfigChanged();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ClientParams.Parameters.Language = (string)comboBox2.SelectedItem;
                LanguageIsChanged();
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

        private void Translate()
        {
            groupBox1.Text = l.GetMessage("CONF002");
            checkBoxAutoStart.Text = l.GetMessage("CONF003");
            groupBox5.Text = l.GetMessage("CONF004");
            label5.Text = l.GetMessage("CONF005");
            groupBox3.Text = l.GetMessage("CONF006");
            label2.Text = l.GetMessage("CONF007");
            checkBox2.Text = l.GetMessage("CONF008");
            checkBox4.Text = l.GetMessage("CONF009");
            groupBox4.Text = l.GetMessage("CONF010");
            label3.Text = l.GetMessage("CONF011");
            checkBox3.Text = l.GetMessage("CONF012");
            label4.Text = l.GetMessage("CONF013");
            groupBox2.Text = l.GetMessage("CONF014");
            checkBox1.Text = l.GetMessage("CONF015");
            label1.Text = l.GetMessage("CONF016");
            label6.Text = l.GetMessage("CONF022");
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { 
                l.GetMessage("CONF021"), l.GetMessage("CONF017"), 
                l.GetMessage("CONF018"), l.GetMessage("CONF019"),
                /*l.GetMessage("CONF020")*/});
            checkBox5.Text = l.GetMessage("CONF023");
            linkLabel1.Text = l.GetMessage("CONF024");
            checkBox6.Text = l.GetMessage("CONF028");
            label7.Text = l.GetMessage("CONF029");
            label8.Text = l.GetMessage("CONF030");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // filter is enabled
            if (ClientParams.Parameters.TrafficSeparatedFilterList != "")
            {
                string websites = "";
                if (ClientParams.Parameters.TrafficSeparatedFilterList.IndexOf(';') < 0)
                {
                    websites = ClientParams.Parameters.TrafficSeparatedFilterList;
                }
                else
                {
                    foreach (string s in ClientParams.Parameters.TrafficSeparatedFilterList.Split(';'))
                    {
                        websites += string.Concat(s, Environment.NewLine);
                    }
                }
                MessageBox.Show(string.Format(l.GetMessage("CONF026"), websites),
                    l.GetMessage("CONF024"), MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(l.GetMessage("CONF025"), l.GetMessage("CONF024"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
