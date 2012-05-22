using System.Collections.Generic;

namespace Traffic_Accounting
{
    public class TrafficFilter
    {
        // list of sites which should be skipped
        // from counting of total amount of traffic
        private List<string> FilterList = new List<string>();

        // add website to filter
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

        // remove website from filter
        public void removeItem(string site)
        {
            FilterList.RemoveAll(
                delegate(string item)
                {
                    return item == site;
                }
            );
        }

        // check is website in filter
        public bool isInList(string site)
        {
            return FilterList.FindIndex(
                delegate(string item)
                {
                    return site.EndsWith(item);
                }
            ) >= 0;
        }

        // return filter's list in separated list 
        // website1|website2|website3...
        public string getFormattedList()
        {
            string result = "";
            foreach (string site in FilterList)
            {
                result += site + "|";
            }
            return result;
        }

        // add websites from separated list
        // to filter
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
