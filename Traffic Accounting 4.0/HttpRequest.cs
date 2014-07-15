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
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Traffic_Accounting
{
    internal class HttpRequest
    {
        public bool LastOperationCompletedSuccessfully
        {
            get;
            private set;
        }

        public string readUrl(string url)
        {
            Log.Trace.addTrace(
                string.Format("Reading URL {0}",
                url));

            LastOperationCompletedSuccessfully = true;
            string Response = "";

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(url);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = ClientParams.Parameters.HttpTimeout;
                myRequest.Method = "POST";
                myRequest.ContentType = "text/html";
                myRequest.ContentLength = buffer.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(buffer, 0, buffer.Length);
                newStream.Close();
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse, Encoding.UTF8, true);
                Response = streamRead.ReadToEnd();
                streamRead.Close();
                streamResponse.Close();
                myHttpWebResponse.Close();
                return Response;
            }
            catch (WebException ex)
            {
                Log.Trace.addTrace("WebException: " + ex.Message);
                LastOperationCompletedSuccessfully = false;
            }

            Log.Trace.addTrace(
                string.Format("Done. Is success: {0}, responce length: {1}",
                LastOperationCompletedSuccessfully, Response.Length));

            return Response;
        }

        public string cutHtml(string sourceHtml)
        {
            int a = 0;
            a = sourceHtml.IndexOf(prepareCut(ClientParams.Parameters.HttpCut1));
            if (a < 0)
            {
                // TODO: alert for error
                // For now: if a < 0 then page does not contain 
                // any useful info and can be cleared
                Log.Trace.addTrace("Cut HTML failed. IndexOf return < 0");
                return "NOT_FOUND";
            }
            else
            {
                sourceHtml = sourceHtml.Remove(0, a);
            }
            a = 0;
            a = sourceHtml.IndexOf(prepareCut(ClientParams.Parameters.HttpCut2));
            if (a < 0)
            {
                // TODO: alert for error
                // For now: if a < 0 then page does not contain 
                // any useful info and can be cleared
                Log.Trace.addTrace("Cut HTML failed. IndexOf return < 0");
                return "NOT_FOUND";
            }
            else
            {
                sourceHtml = sourceHtml.Remove(a, sourceHtml.Length - a);
            }

            Log.Trace.addTrace("Cut HTML OK");

            return sourceHtml;

            //Regex regex = new Regex(prepareCut(@"<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>([\s\S]+?)</TABLE>"));
            //Match m = regex.Match(sourceHtml);
            //if (m.Groups.Count > 0)
            //{
            //    MessageBox.Show(m.Groups[0].Value, m.Groups.Count.ToString());
            //    return m.Groups[0].Value;
            //}
            //return "NOT_FOUND";
        }

        /// <summary>
        /// parse string and insert ip/machine name into input string
        /// </summary>
        private string prepareCut(string cutPattern)
        {
            cutPattern = cutPattern.Replace("[IP]", getLocalIP());
            cutPattern = cutPattern.Replace("[MACHINE]", Environment.MachineName.ToLower());

            Log.Trace.addTrace(string.Format("String will be {0}", cutPattern));

            return cutPattern;
        }

        /// <summary>
        /// return current IP address for local machine
        /// </summary>
        public string getLocalIP()
        {
            // prepare regexp which will qualify only ip addresses like *.*.*.*
            Regex regex = new Regex("([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})", 
                RegexOptions.Singleline);
            // get local ip's
            string localHostName = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            // check each ip until correct will be found
            foreach (IPAddress ip in localIPs)
            {
                // if qualify by mask *.*.*.*
                if (regex.Match(ip.ToString()).Success)
                {
                    Log.Trace.addTrace(string.Format("IP found: {0}", ip.ToString()));

                    return ip.ToString();
                }
            }
            // if nothing found
            Languages lang = new Languages(ClientParams.Parameters.Language);
            MessageBox.Show(lang.GetMessage("INTERNAL_IP_NOTFOUND"),
                lang.GetMessage("PROGRAMNAME"), MessageBoxButtons.OK,
                 MessageBoxIcon.Error);
            LastOperationCompletedSuccessfully = false;

            Log.Trace.addTrace(string.Format("Suitable IP not found. Host: {0}", 
                localHostName));

            return "";
        }
    }
}
