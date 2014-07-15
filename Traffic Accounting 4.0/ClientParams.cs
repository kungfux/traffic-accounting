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
using ItWorksTeam.IO;
using System.Drawing;

namespace Traffic_Accounting
{
    // TODO
    // Optimize all methods
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
        public bool Welcome = true;
        //
        public bool TraceEnabled = false;
        //
        public string Language = "English";
        // OS Windows
        public bool AutoStart = false;
        // HttpRequest
        public string HttpCut1 = "<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>";
        public string HttpCut2 = "</TABLE>";
        public string MachineName = Environment.MachineName.ToLower();
        public short HttpTimeout = 5000;
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
        public bool DisplayNotify = true;
        // Traffic
        public int TrafficLimitForWeek = 100;
        public bool TrafficCacheEnabled = true;
        public bool TOPenabled = true;
        public bool TrafficFilterEnabled = false;
        public string TrafficSeparatedFilterList = "";
        public string TrafficStatDailyUrl = "[SERVER]/squid/daily/[yyyy_MM_dd].html";
        public string TrafficStatWeeklyUrl = "[SERVER]/squid/weekly/[yyyy_WW].html";
        public string TrafficStatPattern = @"<TR><TD ALIGN=LEFT>([\S.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>";
        public string TrafficTopPattern = @"<TR><TD ALIGN=LEFT><A HREF=#([\S.]*)>([\S.]*) (([\S.]*))</A>";
        public bool TrafficRoundUp = true;
        //
        public FwServers.FwServer Location = FwServers.FwServer.Berdyansk;
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
                string httpcut = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut", HttpCut1 + "|" + HttpCut2);
                if (httpcut.Contains("|"))
                {
                    string[] cutted = httpcut.Split('|');
                    if (cutted.Length == 2)
                    {
                        Parameters.HttpCut1 = cutted[0];
                        Parameters.HttpCut2 = cutted[1];
                    }
                }
                Parameters.MachineName = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "MachineName", MachineName);
                Parameters.HttpTimeout = Registry.ReadKey<short>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpTimeout", HttpTimeout);
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
                // Filter
                Parameters.TrafficFilterEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficFilterEnabled", TrafficFilterEnabled);
                Parameters.TrafficSeparatedFilterList = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficSeparatedFilterList", TrafficSeparatedFilterList);
                Parameters.TrafficStatDailyUrl = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatDailyUrl", TrafficStatDailyUrl);
                Parameters.TrafficStatWeeklyUrl = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatWeeklyUrl", TrafficStatWeeklyUrl);
                Parameters.TrafficStatPattern = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatPattern", TrafficStatPattern);
                Parameters.TrafficRoundUp = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                            RegPath, "TrafficRoundUp", TrafficRoundUp);
                Parameters.TrafficLimitForWeek = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficLimitForWeek", TrafficLimitForWeek);
                // TOP
                Parameters.TOPenabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TOPenabled", TOPenabled);
                Parameters.TrafficTopPattern = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficTopPattern", TrafficTopPattern);
                // Log
                Parameters.TraceEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TraceEnabled", TraceEnabled);
                // Location
                Parameters.Location = (FwServers.FwServer)Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Location", (int)Location);
                // Welcome
                Parameters.Welcome = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Welcome", Welcome);
            }
        }

        public void saveParams()
        {
            Log.Trace.addTrace("Client parameters are being saved");

            // Language
            Log.Trace.addTrace("Language is " + Parameters.Language);
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
            Log.Trace.addTrace("AutoStart is " + Parameters.AutoStart);
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
            Log.Trace.addTrace("TrafficLimitForWeek is " + Parameters.TrafficLimitForWeek);
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
            Log.Trace.addTrace("TrafficRoundUp is " + Parameters.TrafficRoundUp);
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
            Log.Trace.addTrace("TOPenabled is " + Parameters.TOPenabled);
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
            Log.Trace.addTrace("TrayTrafficRanges is " + 
                string.Concat(Parameters.TrayTrafficRanges[0],
                "|",
                Parameters.TrayTrafficRanges[1],
                "|",
                Parameters.TrayTrafficRanges[2]));
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
            Log.Trace.addTrace("TrayDisplayDigits is " + Parameters.TrayDisplayDigits);
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
            Log.Trace.addTrace("IconFashion is " + Parameters.IconFashion);
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
            Log.Trace.addTrace("TrayIconFontColor is " + Parameters.TrayIconFontColor);
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
            Log.Trace.addTrace("TrayFontSize is " + Parameters.TrayFontSize);
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
            Log.Trace.addTrace("TrayFontName is " + Parameters.TrayFontName);
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
            Log.Trace.addTrace("DisplayNotify is " + Parameters.DisplayNotify);
            if (!Parameters.DisplayNotify)
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
            Log.Trace.addTrace("TrafficCacheEnabled is " + Parameters.TrafficCacheEnabled);
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
            //
            Log.Trace.addTrace("TrafficSeparatedFilterList is " + Parameters.TrafficSeparatedFilterList);
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
            Log.Trace.addTrace("TrafficFilterEnabled is " + Parameters.TrafficFilterEnabled);
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
            //
            Log.Trace.addTrace("TraceEnabled is " + Parameters.TraceEnabled);
            if (Parameters.TraceEnabled != TraceEnabled)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TraceEnabled", Parameters.TraceEnabled);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TraceEnabled");
            }
            //
            Log.Trace.addTrace("Location is " + Parameters.Location);
            if (Parameters.Location != Location)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Location", (int)Parameters.Location);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Location");
            }
            //
            Log.Trace.addTrace("HttpCut is " + Parameters.HttpCut1 + "|" + Parameters.HttpCut2);
            if (Parameters.HttpCut1 != HttpCut1 || Parameters.HttpCut2 != HttpCut2)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut", Parameters.HttpCut1 + "|" + Parameters.HttpCut2);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut");
            }
            //
            Log.Trace.addTrace("HttpTimeout is " + Parameters.HttpTimeout);
            if (Parameters.HttpTimeout != HttpTimeout)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpTimeout", Parameters.HttpTimeout);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpTimeout");
            }
            //
            Log.Trace.addTrace("Welcome is " + Parameters.Welcome);
            if (Parameters.Welcome != Welcome)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Welcome", Parameters.Welcome);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "Welcome");
            }
            //
            Log.Trace.addTrace("TrafficStatDailyUrl is " + Parameters.TrafficStatDailyUrl);
            if (Parameters.TrafficStatDailyUrl != TrafficStatDailyUrl)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatDailyUrl", Parameters.TrafficStatDailyUrl);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatDailyUrl");
            }
            //
            Log.Trace.addTrace("TrafficStatWeeklyUrl is " + Parameters.TrafficStatWeeklyUrl);
            if (Parameters.TrafficStatWeeklyUrl != TrafficStatWeeklyUrl)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatWeeklyUrl", Parameters.TrafficStatWeeklyUrl);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatWeeklyUrl");
            }
            //
            Log.Trace.addTrace("TrafficStatPattern is " + Parameters.TrafficStatPattern);
            if (Parameters.TrafficStatPattern != TrafficStatPattern)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatPattern", Parameters.TrafficStatPattern);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatPattern");
            }
            //
            Log.Trace.addTrace("TrafficTopPattern is " + Parameters.TrafficTopPattern);
            if (Parameters.TrafficTopPattern != TrafficTopPattern)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficTopPattern", Parameters.TrafficTopPattern);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficTopPattern");
            }
            //
            Log.Trace.addTrace("MachineName is " + Parameters.MachineName);
            if (Parameters.MachineName != MachineName)
            {
                Registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "MachineName", Parameters.MachineName);
            }
            else
            {
                Registry.DeleteKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "MachineName");
            }
        }
    }
}
