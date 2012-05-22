using System;
using System.Collections.Generic;
using System.Text;
using ItWorksTeam.IO;
using System.Windows.Forms;

namespace Traffic_Accounting
{
    public class ClientParams
    {
        private static ClientParams _clientParams;
        private static Object _lock = new Object();
        private Registry Registry = new Registry();
        private string RegPath = "Software\\ItWorksTeam\\Traffic Accounting\\Version 4.0";

        // list of parameters
        // 
        // OS Windows
        public bool AutoStart = false;
        // HttpRequest
        public string HttpMethod;
        public string HttpCodePage;
        public string HttpCut1;
        public string HttpCut2;
        // SystemTray
        public int TrayIconBackColor;
        public int TrayIconFontColor;
        public bool TrayDisplayDigits;
        public bool TrayDigitsColorRangesEnabled;
        public bool TrayBackColorRangesEnabled;
        public byte[] TrayTrafficRanges;
        public bool TrayDrawCircleInsteadOfSquare;
        // Traffic
        public bool TrafficCacheEnabled;
        public int TrafficCacheSize;
        public bool TrafficFilterEnabled;
        public string TrafficSeparatedFilterList;
        public string TrafficStatUrl;
        public string TrafficStatPattern;
        public DayOfWeek FirstDayOfTheWeek;
        public bool TrafficRoundUp;
        //

        static ClientParams()
        {
            lock (_lock)
            {
                if (_clientParams == null)
                {
                    _clientParams = new ClientParams();
                }
            }
        }

        public ClientParams Parameters
        {
            get {return _clientParams;}
        }

        public void LoadClientParams()
        {
            // Global
            _clientParams.AutoStart = (Registry.IsKeyExist(Registry.BaseKeys.HKEY_CURRENT_USER,
                "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0") &&
                Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0",
                "") == Application.StartupPath);
            // Http
            _clientParams.HttpMethod = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "HttpMethod", "POST");
            _clientParams.HttpCodePage = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "HttpCodePage", "utf-8");
            _clientParams.HttpCut1 = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "HttpCut1", "<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>");
            _clientParams.HttpCut2 = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "HttpCut2", "</TABLE>");
            // SystemTray
            _clientParams.TrayIconBackColor = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayIconBackColor", 16777215);
            _clientParams.TrayIconFontColor = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayIconFontColor", -1);
            _clientParams.TrayDisplayDigits = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayDisplayDigits", true);
            _clientParams.TrayDigitsColorRangesEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayDigitsColorRangesEnabled", false);
            _clientParams.TrayBackColorRangesEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayBackColorRangesEnabled", true);
            _clientParams.TrayTrafficRanges = Registry.ReadKey<byte[]>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayTrafficRanges", new byte[3] { 0, 20, 50 });
            _clientParams.TrayDrawCircleInsteadOfSquare = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrayDrawCircleInsteadOfSquare", true);
            // Cache
            _clientParams.TrafficCacheEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrafficCacheEnabled", true);
            _clientParams.TrafficCacheSize = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrafficCacheSize", 7);
            // Filter
            _clientParams.TrafficFilterEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrafficFilterEnabled", false);
            _clientParams.TrafficSeparatedFilterList = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrafficSeparatedFilterList", "");
            _clientParams.TrafficStatUrl = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrafficStatUrl", "http://fw-br/squid/daily/[yyyy_MM_dd].html");
            _clientParams.TrafficStatPattern = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                RegPath, "TrafficStatPattern", @"<TR><TD ALIGN=LEFT>([\S.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>");
            _clientParams.FirstDayOfTheWeek = (DayOfWeek)Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                            RegPath, "FirstDayOfTheWeek", 1);
            _clientParams.TrafficRoundUp = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                        RegPath, "TrafficRoundUp", false);
        
        }
    }
}
