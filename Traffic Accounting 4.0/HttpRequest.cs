﻿using System;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace Traffic_Accounting
{
    public class HttpRequest
    {
        public string Method = "POST";
        public Encoding CodePage = Encoding.GetEncoding("KOI8R");
        public string ContentType = "text/html";
        public string CutTop = "<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>";
        //public string CutTop = "<A NAME=10.98.58.43><H2><A HREF=#TOC>iofuks (10.98.58.43)</A></H2>";
        public string CutBottom = "</TABLE>";
        public bool LastOperationCompletedSuccessfully
        {
            get;
            private set;
        }


        public string readUrl(string url, bool performCutting)
        {
            LastOperationCompletedSuccessfully = true;
            string Response = "";

            try
            {
                byte[] buffer = CodePage.GetBytes(url);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = Method.ToString();
                myRequest.ContentType = ContentType;
                myRequest.ContentLength = buffer.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(buffer, 0, buffer.Length);
                newStream.Close();
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
                Stream streamResponse = myHttpWebResponse.GetResponseStream();
                StreamReader streamRead = new StreamReader(streamResponse, CodePage, true);
                Response = streamRead.ReadToEnd();
                streamRead.Close();
                streamResponse.Close();
                myHttpWebResponse.Close();
                if (performCutting)
                {
                    Response = cutHtml(Response);
                }
                return Response;
            }
            catch (WebException ex)
            {
                // TODO: Report about problem correcly
                LastOperationCompletedSuccessfully = false;
            }
            return Response;
        }

        private string cutHtml(string sourceHtml)
        {
            int a = 0;
            a = sourceHtml.IndexOf(prepareCut(CutTop));
            if (a < 0)
            {
                // TODO: alert for error
            }
            sourceHtml = sourceHtml.Remove(0, a);
            a = 0;
            a = sourceHtml.IndexOf(prepareCut(CutBottom));
            if (a < 0)
            {
                // TODO: alert for error
            }
            sourceHtml = sourceHtml.Remove(a, sourceHtml.Length - a);
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
