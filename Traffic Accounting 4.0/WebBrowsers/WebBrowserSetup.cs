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

using ItWorksTeam.IO;
namespace Traffic_Accounting
{
    internal class WebBrowserSetup
    {
        public enum ImageStatus
        {
            Show,
            Hide,
            Unknown
        }

        private Registry registry = new Registry();

        public ImageStatus getImageStatus()
        {
            switch(ClientParams.Parameters.UserWebBrowser)
            {
                case ClientParams.WebBrowser.Internet_Explorer:
                    return getImageStatusInInternetExplorer();
            }
            return ImageStatus.Unknown;
        }

        public void switchImagesStatus()
        {
            switch(ClientParams.Parameters.UserWebBrowser)
            {
                case ClientParams.WebBrowser.Internet_Explorer:
                    switchImageStatusInInternetExplorer();
                    break;
            }
        }

        private ImageStatus getImageStatusInInternetExplorer()
        {
            switch (registry.ReadKey<string>(Registry.BaseKeys.HKEY_CURRENT_USER,
                @"Software\Microsoft\Internet Explorer\Main", 
                "Display Inline Images", "unknown"))
            {
                case "yes":
                    return ImageStatus.Show;
                case "no":
                    return ImageStatus.Hide;
                default:
                    return ImageStatus.Unknown;
            }
        }

        private void switchImageStatusInInternetExplorer()
        {
            string value = "yes";
            switch (getImageStatus())
            {
                case ImageStatus.Show:
                    value = "no";
                    break;
                case ImageStatus.Hide:
                    value = "yes";
                    break;
            }
            registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                @"Software\Microsoft\Internet Explorer\Main",
                "Display Inline Images", value);
        }

        // this method returns unique
        // classes key name from registry
        // for Internet Explorer
        //private string GetUniqueClassesKey()
        //{
        //    string RegPath = "";
        //    foreach (string keyinusers in Microsoft.Win32.Registry.Users.GetSubKeyNames())
        //    {
        //        if (keyinusers.Contains("Classes"))
        //        {
        //            RegPath = keyinusers;
        //        }
        //    }
        //    if (RegPath != "")
        //    {
        //        return RegPath.Remove(RegPath.IndexOf("_Classes"), RegPath.Length - RegPath.IndexOf("_Classes"));
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }
}
