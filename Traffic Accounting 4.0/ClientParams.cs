using System;
using System.Collections.Generic;
using System.Text;
using ItWorksTeam.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Traffic_Accounting
{
    internal class ClientParams
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
        //public string HttpMethod = "POST";
        //public string HttpCodePage = "utf-8";
        public string HttpCut1 = "<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>";
        public string HttpCut2 = "</TABLE>";
        // SystemTray
        public int TrayIconBackColor = Color.Transparent.ToArgb();
        public int TrayIconFontColor = Color.White.ToArgb();
        public bool TrayDisplayDigits = true;
        public bool TrayDigitsColorRangesEnabled = false;
        public bool TrayBackColorRangesEnabled = true;
        public byte[] TrayTrafficRanges = new byte[3] { 0, 20, 50 };
        public bool TrayDrawCircleInsteadOfSquare = true;
        // Traffic
        public bool TrafficCacheEnabled = true;
        public int TrafficCacheSize = 21;
        public bool TrafficFilterEnabled = false;
        public string TrafficSeparatedFilterList = "";
        public string TrafficStatUrl = "http://fw-br/squid/daily/[yyyy_MM_dd].html";
        public string TrafficStatPattern = @"<TR><TD ALIGN=LEFT>([\S.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>";
        public DayOfWeek FirstDayOfTheWeek = DayOfWeek.Monday;
        public bool TrafficRoundUp = true;
        //

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
            if (Registry.IsBranchExist(Registry.BaseKeys.HKEY_CURRENT_USER, RegPath))
            {
                // Global
                Parameters.AutoStart = (Registry.IsKeyExist(Registry.BaseKeys.HKEY_CURRENT_USER,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0") &&
                    Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    "Software\\Microsoft\\Windows\\CurrentVersion\\Run", "Traffic Accounting 4.0",
                    "") == Application.StartupPath);
                // Http
                //Parameters.HttpMethod = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                //    RegPath, "HttpMethod", "POST");
                //Parameters.HttpCodePage = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                //    RegPath, "HttpCodePage", "utf-8");
                Parameters.HttpCut1 = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut1", "<A NAME=[IP]><H2><A HREF=#TOC>[MACHINE] ([IP])</A></H2>");
                Parameters.HttpCut2 = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "HttpCut2", "</TABLE>");
                // SystemTray
                Parameters.TrayIconBackColor = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayIconBackColor", 16777215);
                Parameters.TrayIconFontColor = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayIconFontColor", -1);
                Parameters.TrayDisplayDigits = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDisplayDigits", true);
                Parameters.TrayDigitsColorRangesEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDigitsColorRangesEnabled", false);
                Parameters.TrayBackColorRangesEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayBackColorRangesEnabled", true);
                Parameters.TrayTrafficRanges = Registry.ReadKey<byte[]>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayTrafficRanges", new byte[3] { 0, 20, 50 });
                Parameters.TrayDrawCircleInsteadOfSquare = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrayDrawCircleInsteadOfSquare", true);
                // Cache
                Parameters.TrafficCacheEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheEnabled", true);
                Parameters.TrafficCacheSize = Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficCacheSize", 21);
                // Filter
                Parameters.TrafficFilterEnabled = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficFilterEnabled", false);
                Parameters.TrafficSeparatedFilterList = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficSeparatedFilterList", "");
                Parameters.TrafficStatUrl = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatUrl", "http://fw-br/squid/daily/[yyyy_MM_dd].html");
                Parameters.TrafficStatPattern = Registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                    RegPath, "TrafficStatPattern", @"<TR><TD ALIGN=LEFT>([\S.]*)</TD><TD ALIGN=RIGHT>([0-9]*)</TD>");
                Parameters.FirstDayOfTheWeek = (DayOfWeek)Registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                                RegPath, "FirstDayOfTheWeek", 1);
                Parameters.TrafficRoundUp = Registry.ReadKey<bool>(Registry.BaseKeys.HKEY_CURRENT_USER,
                            RegPath, "TrafficRoundUp", false);
            }
        
        }
    }
}
