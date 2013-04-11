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

using System.Windows.Forms;

namespace Traffic_Accounting.GUI
{
    public partial class AdditionalConfiguration : UserControl
    {
        public AdditionalConfiguration()
        {
            InitializeComponent();
        }

        public event ConfChanged ConfigurationChanged;
        public delegate void ConfChanged();

        private void AdditionalConfiguration_Load(object sender, System.EventArgs e)
        {
            txtMachineName.Text = ClientParams.Parameters.MachineName;
            txtUrlDaily.Text = ClientParams.Parameters.TrafficStatDailyUrl;
            txtUrlWeekly.Text = ClientParams.Parameters.TrafficStatWeeklyUrl;
            txtHtmlCut.Text = string.Concat(ClientParams.Parameters.HttpCut1,
                "|", ClientParams.Parameters.HttpCut2);
            txtStatPattern.Text = ClientParams.Parameters.TrafficStatPattern;
            txtTopPattern.Text = ClientParams.Parameters.TrafficTopPattern;

            Translate();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            ClientParams.Parameters.MachineName = txtMachineName.Text;
            ClientParams.Parameters.TrafficStatDailyUrl = txtUrlDaily.Text;
            ClientParams.Parameters.TrafficStatWeeklyUrl = txtUrlWeekly.Text;
            if (txtHtmlCut.Text.Contains("|"))
            {
                string[] htmlCut = txtHtmlCut.Text.Split('|');
                if (htmlCut.Length == 2)
                {
                    ClientParams.Parameters.HttpCut1 = htmlCut[0];
                    ClientParams.Parameters.HttpCut2 = htmlCut[1];
                }
            }
            else
            {
                ClientParams.Parameters.HttpCut1 = txtHtmlCut.Text;
                ClientParams.Parameters.HttpCut2 = "";
            }
            ClientParams.Parameters.TrafficStatPattern = txtStatPattern.Text;
            ClientParams.Parameters.TrafficTopPattern = txtTopPattern.Text;

            if (ConfigurationChanged != null)
            {
                ConfigurationChanged();
            }
            btnCancel_Click(this, null);
        }

        private void Translate()
        {
            Languages l = new Languages(ClientParams.Parameters.Language);
            groupBox1.Text = l.GetMessage("CONF034");
            lblMachineName.Text = l.GetMessage("CONFA001");
            lblUrlDaily.Text = l.GetMessage("CONFA002");
            lblUrlWeekly.Text = l.GetMessage("CONFA003");
            lblHtmlCut.Text = l.GetMessage("CONFA004");
            lblStatPattern.Text = l.GetMessage("CONFA005");
            lblTopPattern.Text = l.GetMessage("CONFA006");
            btnApply.Text = l.GetMessage("CONFA007");
            btnCancel.Text = l.GetMessage("CONFA008");
        }
    }
}
