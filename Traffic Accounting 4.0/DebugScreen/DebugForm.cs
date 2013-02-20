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
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Traffic_Accounting.DebugScreen
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private string cacheFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Traffic Accounting\\cache.xml";
        private string cacheFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Traffic Accounting";
        private HttpRequest http = new HttpRequest();
        private Traffic t = new Traffic();

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(cacheFolder);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(cacheFile))
            {
                File.Delete(cacheFile);
            }
            DebugForm_Load(this, null);
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            long fileSize = 0;
            if (File.Exists(cacheFile))
            {
                fileSize = new FileInfo(cacheFile).Length;
            }
            label1.Text = string.Format("Local cache size is equals to {0} bytes", fileSize);

            cuttop.Text = ClientParams.Parameters.HttpCut1;
            cutbottom.Text = ClientParams.Parameters.HttpCut2;
            regex.Text = ClientParams.Parameters.TrafficStatPattern;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!checkBox1.Checked)
            {
                responce2.Text = http.readUrl(address2.Text);
            }
            else
            {
                responce2.Text = http.cutHtml(http.readUrl(address2.Text));
            }
            if (checkBox2.Checked)
            {
                responce2.Text += Environment.NewLine + "===== REGEX =====" + Environment.NewLine;
                // REGEX
                Regex r = new Regex(regex.Text);
                Match m = r.Match(responce2.Text);
                while (m.Success)
                {
                    responce2.Text += "Match found:" + Environment.NewLine +
                        " #1 " + m.Groups[1] +
                        " #2 " + m.Groups[2] + Environment.NewLine;
                    m = m.NextMatch();
                }
                //
            }
        }

        private void cutbottom_TextChanged(object sender, EventArgs e)
        {
            ClientParams.Parameters.HttpCut2 = cutbottom.Text;
        }

        private void cuttop_TextChanged(object sender, EventArgs e)
        {
            ClientParams.Parameters.HttpCut1 = cuttop.Text;
        }
    }
}
