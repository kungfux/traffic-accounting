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

using System.Text;
using Traffic_Accounting.TAServer;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System;

namespace Traffic_Accounting
{
    internal class TAS
    {
        private string UpdateServerUrl = "http://iofuks:81";

        public UpdateInfo CheckUpdate()
        {
            string Response;

            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(UpdateServerUrl);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(UpdateServerUrl);
                myRequest.Method = "GET";
                myRequest.ContentType = "text/xml";
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
                return DeserializeClass<UpdateInfo>(Response);
            }
            catch (WebException)
            {
            }
            return null;
        }

        // method for deserialization
        public T DeserializeClass<T>(string data)
        {
            T result = default(T);
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StringReader ms = new StringReader(data);
                result = (T)xs.Deserialize(ms);
                ms.Close();
            }
            catch (IOException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            return result;
        }
    }
}
