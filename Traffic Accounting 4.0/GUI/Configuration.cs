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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Traffic_Accounting.GUI;

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

            cmbDigitsColor.Items.Clear();
            cmbDigitsColor.Items.AddRange(new string[] {
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
                cmbFont.Items.Add(ff.Name);
            }

            // display current setup
            chAutoStart.Checked = ClientParams.Parameters.AutoStart;
            chTrace.Checked = ClientParams.Parameters.TraceEnabled;
            cmbLanguage.SelectedItem = ClientParams.Parameters.Language;
            updTrafficLimit.Value = ClientParams.Parameters.TrafficLimitForWeek;
            chRoundUp.Checked = ClientParams.Parameters.TrafficRoundUp;
            chTop10.Checked = ClientParams.Parameters.TOPenabled;
            updTrafficExceed.Value = ClientParams.Parameters.TrayTrafficRanges[0];
            updTrafficCritical.Value = ClientParams.Parameters.TrayTrafficRanges[1];
            updTrafficWarning.Value = ClientParams.Parameters.TrayTrafficRanges[2];
            updTrafficCritical.Maximum = updTrafficWarning.Value;
            updTrafficExceed.Maximum = updTrafficCritical.Value;
            chDigits.Checked = ClientParams.Parameters.TrayDisplayDigits;
            cmbIconFashion.SelectedIndex = ClientParams.Parameters.IconFashion;
            chCache.Checked = ClientParams.Parameters.TrafficCacheEnabled;
            chFilter.Checked = ClientParams.Parameters.TrafficFilterEnabled;
            chNotify.Checked = ClientParams.Parameters.DisplayNotify;
            cmbDigitsSize.Text = ClientParams.Parameters.TrayFontSize.ToString();
            cmbFont.Text = ClientParams.Parameters.TrayFontName;

            cmbDigitsColor.SelectedItem = (object)ClientParams.Parameters.TrayIconFontColor;
            cmbDigitsColor.Enabled = chDigits.Checked;

            cmbLocation.SelectedIndex = (int)ClientParams.Parameters.Location - 1;
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
                cmbDigitsColor.Enabled = chDigits.Checked;
                ClientParams.Parameters.AutoStart = chAutoStart.Checked;
                ClientParams.Parameters.TrafficRoundUp = chRoundUp.Checked;
                ClientParams.Parameters.TOPenabled = chTop10.Checked;
                ClientParams.Parameters.TrayDisplayDigits = chDigits.Checked;
                ClientParams.Parameters.TrafficCacheEnabled = chCache.Checked;
                ClientParams.Parameters.TrafficFilterEnabled = chFilter.Checked;
                ClientParams.Parameters.DisplayNotify = chNotify.Checked;
                ClientParams.Parameters.TraceEnabled = chTrace.Checked;

                if (sender is CheckBox)
                {
                    CheckBox checkbox = (CheckBox)sender;
                    // check if user uncheck traffic cache
                    // then we need to clear cache
                    if (checkbox.Name == chCache.Name)
                    {
                        if (!checkbox.Checked)
                        {
                            CachedTrafficHistory cache = new CachedTrafficHistory();
                            cache.ClearCache();
                        }
                    }
                    // check if user uncheck log
                    // then we need to open/close writer
                    if (checkbox.Name == chTrace.Name)
                    {
                        if (!checkbox.Checked)
                        {
                            Log.Trace.closeTrace();
                        }
                        else
                        {
                            Log.Trace.resumeTrace();
                        }
                    }
                }

                ConfigChanged();
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ClientParams.Parameters.TrafficLimitForWeek = (int)updTrafficLimit.Value;
                ClientParams.Parameters.TrayTrafficRanges[0] = (byte)updTrafficExceed.Value;
                ClientParams.Parameters.TrayTrafficRanges[1] = (byte)updTrafficCritical.Value;
                ClientParams.Parameters.TrayTrafficRanges[2] = (byte)updTrafficWarning.Value;
                ConfigChanged();
            }
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (cmbIconFashion.SelectedIndex == 0)
                {
                    chDigits.Checked = true;
                    chDigits.Enabled = false;
                }
                else
                {
                    chDigits.Enabled = true;
                }
                ClientParams.Parameters.IconFashion = cmbIconFashion.SelectedIndex;
                if (cmbDigitsColor.SelectedItem != null) // just check for an error
                {
                    ClientParams.Parameters.TrayIconFontColor = cmbDigitsColor.SelectedItem.ToString();
                }
                int.TryParse(cmbDigitsSize.Text, out ClientParams.Parameters.TrayFontSize);
                ClientParams.Parameters.TrayFontName = cmbFont.Text;
                ClientParams.Parameters.Location = (FwServers.FwServer)cmbLocation.SelectedIndex + 1;

                // check if user change location
                // then we need to clear cache
                if (sender is ComboBox)
                {
                    ComboBox combobox = (ComboBox)sender;
                    if (combobox.Name == cmbLocation.Name)
                    {
                        CachedTrafficHistory cache = new CachedTrafficHistory();
                        cache.ClearCache();
                    }
                }

                ConfigChanged();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                ClientParams.Parameters.Language = (string)cmbLanguage.SelectedItem;
                LanguageIsChanged();
                ConfigChanged();
            }
        }

        private void numericUpDown5_Leave(object sender, EventArgs e)
        {
            updTrafficCritical.Maximum = updTrafficWarning.Value;
        }

        private void numericUpDown4_Leave(object sender, EventArgs e)
        {
            updTrafficExceed.Maximum = updTrafficCritical.Value;
        }

        private void Translate()
        {
            groupGeneral.Text = l.GetMessage("CONF002");
            chAutoStart.Text = l.GetMessage("CONF003");
            lLanguage.Text = l.GetMessage("CONF005");
            groupTraffic.Text = l.GetMessage("CONF006");
            lTrafficLimit.Text = l.GetMessage("CONF007");
            chRoundUp.Text = l.GetMessage("CONF008");
            chTop10.Text = l.GetMessage("CONF009");
            groupSystemTray.Text = l.GetMessage("CONF010");
            lTrafficRanges.Text = l.GetMessage("CONF011");
            chDigits.Text = l.GetMessage("CONF012");
            lIconFashion.Text = l.GetMessage("CONF013");
            chCache.Text = l.GetMessage("CONF015");
            lDigitsColor.Text = l.GetMessage("CONF022");
            cmbIconFashion.Items.Clear();
            cmbIconFashion.Items.AddRange(new string[] { 
                l.GetMessage("CONF021"), l.GetMessage("CONF017"), 
                l.GetMessage("CONF018"), l.GetMessage("CONF019")});
            chFilter.Text = l.GetMessage("CONF023");
            linkLabel1.Text = l.GetMessage("CONF024");
            chNotify.Text = l.GetMessage("CONF028");
            lDigitsSize.Text = l.GetMessage("CONF029");
            lFontName.Text = l.GetMessage("CONF030");
            lLocation.Text = l.GetMessage("CONF031");
            cmbLocation.Items.Clear();
            cmbLocation.Items.AddRange(l.GetMessage("CONF032").Split(','));
            cmbLocation.SelectedIndex = (int)ClientParams.Parameters.Location - 1;
            groupSystem.Text = l.GetMessage("CONF014");
            chTrace.Text = l.GetMessage("CONF033");
            btnAdditional.Text = l.GetMessage("CONF034");
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

        private void btnAdditional_Click(object sender, EventArgs e)
        {
            AdditionalConfiguration addConf = new AdditionalConfiguration();
            addConf.ConfigurationChanged += new AdditionalConfiguration.ConfChanged(addConf_ConfigurationChanged);
            addConf.Dock = DockStyle.Fill;
            this.Parent.Controls.Add(addConf);
            addConf.BringToFront();
        }

        void addConf_ConfigurationChanged()
        {
            ConfigChanged();
            CachedTrafficHistory cache = new CachedTrafficHistory();
            cache.ClearCache();
        }
    }
}
