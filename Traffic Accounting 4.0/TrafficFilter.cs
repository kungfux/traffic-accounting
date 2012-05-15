using System.Collections.Generic;

namespace Traffic_Accounting
{
    public class TrafficFilter
    {
        private List<string> SitesList = new List<string>();

        public void addSite(string site)
        {
            if (SitesList.FindIndex(
                delegate(string item)
                {
                    return item == site;
                }
                ) == -1)
            {
                SitesList.Add(site);
            }
        }

        public bool isInList(string site)
        {
            return SitesList.FindIndex(
                delegate(string item)
                {
                    return site.EndsWith(item);
                }
            ) >= 0;
        }
    }
}
