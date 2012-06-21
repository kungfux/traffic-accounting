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
            switch (registry.ReadKey<string>(Registry.BaseKeys.HKEY_USERS,
                GetUniqueClassesKey() + @"\Software\Microsoft\Internet Explorer\Main", 
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
            registry.SaveKey(Registry.BaseKeys.HKEY_USERS,
                GetUniqueClassesKey() + @"\Software\Microsoft\Internet Explorer\Main",
                "Display Inline Images", value);
        }

        // this method returns unique
        // classes key name from registry
        // for Internet Explorer
        private string GetUniqueClassesKey()
        {
            string RegPath = "";
            foreach (string keyinusers in Microsoft.Win32.Registry.Users.GetSubKeyNames())
            {
                if (keyinusers.Contains("Classes"))
                {
                    RegPath = keyinusers;
                }
            }
            return RegPath.Remove(RegPath.IndexOf("_Classes"), RegPath.Length - RegPath.IndexOf("_Classes"));
        }
    }
}
