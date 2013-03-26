/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2013
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

using System.Collections.Generic;

namespace Traffic_Accounting
{
    internal class TrafficFilter
    {
        // list of sites which should be skipped
        // from counting of total amount of traffic
        private static List<string> FilterList = new List<string>();

        public TrafficFilter()
        {
            addFormattedList(ClientParams.Parameters.TrafficSeparatedFilterList);
        }

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
                updateClientParams();
            }
        }

        // add website to filter without updating
        private void addItemWOUpdate(string site)
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
            updateClientParams();
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
        // website1;website2;website3...
        public string getFormattedList()
        {
            string result = "";
            foreach (string site in FilterList)
            {
                if (result == "")
                {
                    result += site;
                }
                else
                {
                    result += ";" + site;
                }
                
            }
            return result;
        }

        // add websites from separated list
        // to filter
        private void addFormattedList(string list)
        {
            if (list.Length > 0)
            {
                if (list.Contains(";"))
                {
                    foreach (string site in list.Split(';'))
                    {
                        if (site.Length > 0)
                        {
                            addItemWOUpdate(site);
                        }
                    }
                }
                else
                {
                    addItemWOUpdate(list);
                }
            }
        }

        // apply new filter list and
        // save current filter list
        private void updateClientParams()
        {
            ClientParams.Parameters.TrafficSeparatedFilterList = getFormattedList();
            ClientParams p = new ClientParams();
            p.saveParams();
        }
    }
}
