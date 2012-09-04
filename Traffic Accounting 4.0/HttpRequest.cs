/*   
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
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

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
            LastOperationCompletedSuccessfully = true;
            string Response = "";


            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(url);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
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
            catch (WebException)
            {
                // TODO: Report about problem correcly
                LastOperationCompletedSuccessfully = false;
            }
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
                return "NOT_FOUND";
            }
            else
            {
                sourceHtml = sourceHtml.Remove(a, sourceHtml.Length - a);
            }
            return sourceHtml;
        }

        /// <summary>
        /// parse string and insert ip/machine name into input string
        /// </summary>
        private string prepareCut(string cutPattern)
        {
            cutPattern = cutPattern.Replace("[IP]", getLocalIP());
            cutPattern = cutPattern.Replace("[MACHINE]", Environment.MachineName.ToLower());
            return cutPattern;
        }

        /// <summary>
        /// return current IP address for local machine
        /// </summary>
        private string getLocalIP()
        {
            string localHostName = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            if (localIPs.Length > 0)
            {
                return localIPs[localIPs.Length - 1].ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
