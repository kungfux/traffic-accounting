using System.Collections.Generic;

namespace Traffic_Accounting
{
    public class TrafficFilter
    {
        // list of sites which should be skipped
        // from counting of total amount of traffic
        private List<string> FilterList = new List<string>();

        public void addItem(string site)
        {
            if (FilterList.FindIndex(
                delegate(string item)
                {
                    return item == site;
                }
                ) == -1)
            {
                FilterList.Add(site);
            }
        }

        public bool isInList(string site)
        {
            return FilterList.FindIndex(
                delegate(string item)
                {
                    return site.EndsWith(item);
                }
            ) >= 0;
        }

        public string getFormattedList()
        {
            string result = "";
            foreach (string site in FilterList)
            {
                result += site + "|";
            }
            return result;
        }

        public void addFormattedList(string list)
        {
            if (list.Length > 0)
            {
                if (list.Contains("|"))
                {
                    foreach (string site in list.Split('|'))
                    {
                        if (site.Length > 0)
                        {
                            addItem(site);
                        }
                    }
                }
                else
                {
                    addItem(list);
                }
            }
        }
    }
}
