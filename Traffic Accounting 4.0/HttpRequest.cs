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
