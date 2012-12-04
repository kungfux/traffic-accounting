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
using System.Collections.Generic;
using System.Text;
using ItWorksTeam.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace Traffic_Accounting
{
    internal class ClientParams
    {
        private static ClientParams _clientParams;
        private static Object _lock = new Object();
        private Registry Registry = new Registry();
        private string RegPath = "Software\\ItWorksTeam\\Traffic Accounting\\Version 4.0";
        public string AssemblyFullName;

        public enum WebBrowser
        {
            Internet_Explorer
        }

        // list of parameters
        // 
        public string Language = "English";
        // OS Windows
        public bool AutoStart = false;
        // HttpRequest
        //public string HttpMethod = "POST";
        //public string HttpCodePage = "utf-8";
        public string HttpCut1 = "<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>";
        public string HttpCut2 = "</TABLE>";
        public string MachineName = Environment.MachineName.ToLower();
        // SystemTray
        public int TrayIconBackColor = Color.Transparent.ToArgb();
        public string TrayIconFontColor = "White";
        public int TrayFontSize = 17;
        public string TrayFontName = "Calibri";
        public bool TrayDisplayDigits = true;
        public bool TrayDigitsColorRangesEnabled = false;
        public bool TrayBackColorRangesEnabled = true;
        public byte[] TrayTrafficRanges = new byte[3] { 0, 20, 50 };
        public int IconFashion = 1;
        public bool DisplayNotify = false;
        // Traffic
        public int TrafficLimitForWeek = 100;
        public bool TrafficCacheEnabled = true;
        public int TrafficCacheSize = 8;
        public bool TOPenabled = true;
        public bool TrafficFilterEnabled = false;
        public string TrafficSeparatedFilterList = "";
        public string TrafficStatDailyUrl = "http://fw-br/squid/daily/[yyyy_MM_dd].html";
        public string TrafficStatWeeklyUrl = "http://fw-br/squid/weekly/[yyyy_WW].html";
        public string TrafficStatPattern = @"<TR><TD ALIGN=LEFT>([\S.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>";
        public string TrafficTopPattern = @"<TR><TD ALIGN=LEFT><A HREF=#([\S.]*)>([\S.]*) (([\S.]*))</A>"; // TODO: Add to registry
        public Traffic_Accounting.DayOfWeek.DayOfWeekValue FirstDayOfTheWeek = DayOfWeek.DayOfWeekValue.Monday;
        public bool TrafficRoundUp = true;
        //
        public WebBrowser UserWebBrowser = WebBrowser.Internet_Explorer;

        public static ClientParams Parameters
        {
            get 
            {
                if (_clientParams == null)
                {
                    lock (_lock)
                    {
                        _clientParams = new ClientParams();
                    }
                }
                return _clientParams;
            }
        }

        public void LoadClientParams()
        {
            // Global
            Parameters.AutoStart = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0",
                "").ToLower().CompareTo(ClientParams.Parameters.AssemblyFullName) == 0;

            if (Registry.IsBranchExist(Registry.BaseKeys.HKEY_CURRENT_USER, RegPath))
            {
                Parameters.Language = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Language", Language);
                // Http
                //Parameters.HttpMethod = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                //    RegPath, "HttpMethod", "POST");
                //Parameters.HttpCodePage = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                //    RegPath, "HttpCodePage", "utf-8");
                Parameters.HttpCut1 = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut1", HttpCut1);
                Parameters.HttpCut2 = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut2", HttpCut2);
                Parameters.MachineName = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "MachineName", MachineName);
                // SystemTray
                Parameters.TrayIconBackColor = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayIconBackColor", TrayIconBackColor);
                Parameters.TrayIconFontColor = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayIconFontColor", TrayIconFontColor);
                Parameters.TrayDisplayDigits = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDisplayDigits", TrayDisplayDigits);
                Parameters.TrayFontSize = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayFontSize", TrayFontSize);
                Parameters.TrayFontName = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayFontName", TrayFontName);
                Parameters.TrayDigitsColorRangesEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDigitsColorRangesEnabled", TrayDigitsColorRangesEnabled);
                Parameters.TrayBackColorRangesEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayBackColorRangesEnabled", TrayBackColorRangesEnabled);
                Parameters.TrayTrafficRanges = Registry.ReadKey<byte[]>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayTrafficRanges", TrayTrafficRanges);
                Parameters.IconFashion = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "IconFashion", IconFashion);
                Parameters.DisplayNotify = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "DisplayNotify", DisplayNotify);
                // Cache
                Parameters.TrafficCacheEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheEnabled", TrafficCacheEnabled);
                Parameters.TrafficCacheSize = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheSize", TrafficCacheSize);
                // Filter
                Parameters.TrafficFilterEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficFilterEnabled", TrafficFilterEnabled);
                Parameters.TrafficSeparatedFilterList = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficSeparatedFilterList", TrafficSeparatedFilterList);
                Parameters.TrafficStatDailyUrl = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatDailyUrl", TrafficStatDailyUrl);
                Parameters.TrafficStatWeeklyUrl = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatDailyUrl", TrafficStatWeeklyUrl);
                Parameters.TrafficStatPattern = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatPattern", TrafficStatPattern);
                Parameters.FirstDayOfTheWeek = (Traffic_Accounting.DayOfWeek.DayOfWeekValue)Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                                RegPath, "FirstDayOfTheWeek", (int)FirstDayOfTheWeek);
                Parameters.TrafficRoundUp = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                            RegPath, "TrafficRoundUp", TrafficRoundUp);
                Parameters.TrafficLimitForWeek = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficLimitForWeek", TrafficLimitForWeek);
                // TOP
                Parameters.TOPenabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TOPenabled", TOPenabled);
                Parameters.TrafficTopPattern = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficTopPattern", TrafficTopPattern);
            }
        }

        public void saveParams()
        {
            if (Parameters.Language != Language)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                        RegPath, "Language", Parameters.Language);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Language");
            }
            // auto start
            if (Parameters.AutoStart)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0",
                    Parameters.AssemblyFullName);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0");
            }
            // traffic limit
            if (Parameters.TrafficLimitForWeek != TrafficLimitForWeek)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficLimitForWeek", Parameters.TrafficLimitForWeek);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficLimitForWeek");
            }
            // traffic round up
            if (Parameters.TrafficRoundUp != TrafficRoundUp)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficRoundUp", Parameters.TrafficRoundUp);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficRoundUp");
            }
            // top enabled
            if (Parameters.TOPenabled != TOPenabled)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TOPenabled", Parameters.TOPenabled);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TOPenabled");
            }
            // traffic ranges
            if (!Parameters.TrayTrafficRanges[0].Equals(TrayTrafficRanges[0]) ||
                !Parameters.TrayTrafficRanges[1].Equals(TrayTrafficRanges[1]) ||
                !Parameters.TrayTrafficRanges[2].Equals(TrayTrafficRanges[2]))
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayTrafficRanges", Parameters.TrayTrafficRanges);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayTrafficRanges");
            }
            // tray display digits ?
            if (Parameters.TrayDisplayDigits != TrayDisplayDigits)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDisplayDigits", Parameters.TrayDisplayDigits);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDisplayDigits");
            }
            // display circle instead of square
            if (Parameters.IconFashion != IconFashion)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "IconFashion", Parameters.IconFashion);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "IconFashion");
            }
            // icon font color
            if (Parameters.TrayIconFontColor != TrayIconFontColor)
            {
               Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                        RegPath, "TrayIconFontColor", Parameters.TrayIconFontColor);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                   RegPath, "TrayIconFontColor");
            }
            if (Parameters.TrayFontSize != TrayFontSize)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                        RegPath, "TrayFontSize", Parameters.TrayFontSize);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                   RegPath, "TrayFontSize");
            }
            if (Parameters.TrayFontName != TrayFontName)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                        RegPath, "TrayFontName", Parameters.TrayFontName);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                   RegPath, "TrayFontName");
            }
            if (Parameters.DisplayNotify)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                        RegPath, "DisplayNotify", Parameters.DisplayNotify);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                   RegPath, "DisplayNotify");
            }
            // cache
            if (Parameters.TrafficCacheEnabled != TrafficCacheEnabled)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheEnabled", Parameters.TrafficCacheEnabled);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheEnabled");
            }
            if (Parameters.TrafficCacheSize != TrafficCacheSize && Parameters.TrafficCacheEnabled)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheSize", Parameters.TrafficCacheSize);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheSize");
            }
            //
            if (Parameters.TrafficSeparatedFilterList != TrafficSeparatedFilterList)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficSeparatedFilterList", Parameters.TrafficSeparatedFilterList);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficSeparatedFilterList");
            }
            //
            if (Parameters.TrafficFilterEnabled != TrafficFilterEnabled)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficFilterEnabled", Parameters.TrafficFilterEnabled);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficFilterEnabled");
            }
        }

        //private bool IsValidColor(KnownColor color)
        //{
        //    return Color.FromKnownColor(color).IsKnownColor;
        //}
    }
}
