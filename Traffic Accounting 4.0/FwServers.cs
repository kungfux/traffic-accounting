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

namespace Traffic_Accounting
{
    public class FwServers
    {
        public enum FwServer
        {
            Unknown = 0,
            Berdyansk = 1,
            Zaporogie = 2,
            Lviv = 3,
            Sevastopol = 4,
            Yalta = 5            
        }

        // return url for local statistics server
        public string getHttpAddress(FwServer city)
        {
            switch(city)
            {
                case FwServer.Berdyansk:
                    return "http://fw-br.isd.dp.ua/";
                case FwServer.Yalta:
                    return "http://fw-ya.isd.dp.ua/";
                case FwServer.Sevastopol:
                    return "http://fw-sv.isd.dp.ua/";
                case FwServer.Zaporogie:
                    return "http://fw-zp.isd.dp.ua/";
                case FwServer.Lviv:
                    return "http://fw-lv.isd.dp.ua/";
            }
            // if unknown
            return "";
        }

        // trying to guess current location
        public FwServer getCurrentlocation()
        {
            string localIP = new HttpRequest().getLocalIP();
            if (!localIP.Contains(".") || localIP.Split('.').Length < 4)
            {
                // wrong ip
                return FwServer.Unknown;
            }
            int subLink = Convert.ToInt16(localIP.Split('.').GetValue(2).ToString());
            switch(subLink)
            {
                case 58:
                    return FwServer.Berdyansk;
                case 48:
                    return FwServer.Yalta;
                case 66:
                    return FwServer.Sevastopol;
                case 32:
                    return FwServer.Lviv;
                case 74:
                    return FwServer.Zaporogie;
            }
            return FwServer.Unknown;
        }
    }
}
