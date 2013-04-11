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

using System;
using System.Text.RegularExpressions;

namespace Traffic_Accounting
{
    internal class Traffic
    {
        public bool LastOperationCompletedSuccessfully
        { 
            get; 
            private set; 
        }

        private HttpRequest HttpRequest = new HttpRequest();
        private CachedTrafficHistory StatCache = new CachedTrafficHistory();
        private TrafficFilter TrafficFilter = new TrafficFilter();
        private Languages l = new Languages(ClientParams.Parameters.Language);
        private FwServers fwservers = new FwServers();

        private enum TrafficEnumeration
        {
            B = 1,
            KB = 2,
            MB = 4,
            GB = 8
        }

        public TrafficHistory getByDay(DateTime date)
        {
            LastOperationCompletedSuccessfully = true;

            if (StatCache.searchDay(date, true) != -1)
            {
                // check is results already exists in cache
                return StatCache.getDay(date);
            }
            string url = prepareStatUrl(date,
                ClientParams.Parameters.TrafficStatDailyUrl);
            string html = HttpRequest.readUrl(url);
            if (HttpRequest.LastOperationCompletedSuccessfully)
            {
                // if there are no errors during reading statistic
                TrafficHistory stat = new TrafficHistory();
                if (ClientParams.Parameters.TOPenabled)
                {
                    stat.TOP = getTOP(html);
                }
                html = HttpRequest.cutHtml(html);
                stat.DateTime = date;
                Regex regex = new Regex(ClientParams.Parameters.TrafficStatPattern);
                Match m = regex.Match(html);
                while (m.Success)
                {
                    stat.WebSite.Add(m.Groups[1].Value);
                    stat.UsedTraffic.Add(Convert.ToInt32(m.Groups[2].Value));
                    m = m.NextMatch();
                }
                if (HttpRequest.LastOperationCompletedSuccessfully)
                {
                    StatCache.updateCache(stat);
                }
                stat.IsLoaded = HttpRequest.LastOperationCompletedSuccessfully;
                return stat;
            }
            else
            {
                LastOperationCompletedSuccessfully = false;
                return new TrafficHistory();
            }
        }

        // return traffic info for week
        // starting from first day of week
        // and for day specified in date value
        public TrafficHistory getByWeek(DateTime date)
        {
            LastOperationCompletedSuccessfully = true;
            bool setError = false; // set error when done in case error occur in the middle

            TrafficHistory stat = new TrafficHistory();
            for (int a = 0; a <= 7; a++)
            {
                // if requested date is Monday then nothing to do
                if (date.DayOfWeek == System.DayOfWeek.Monday)
                {
                    a = 7;
                }
                // skip request for today's date
                // 'coz there is no statistic for active day
                if (date.DayOfYear != DateTime.Now.DayOfYear)
                {
                    TrafficHistory day = getByDay(date);
                    if (!LastOperationCompletedSuccessfully)
                    {
                        // if error occur
                        setError = true;
                        //return new TrafficHistory();
                    }
                    for (int b = 0; b < day.WebSite.Count; b++)
                    {
                        int index = stat.WebSite.FindIndex(
                            delegate(string item)
                            {
                                return item.Equals(day.WebSite[b]);
                            }
                            );
                        if (index < 0)
                        {
                            // new element
                            stat.WebSite.Add(day.WebSite[b]);
                            stat.UsedTraffic.Add(day.UsedTraffic[b]);
                        }
                        else
                        {
                            // existing element
                            stat.UsedTraffic[index] += day.UsedTraffic[b];
                        }
                    }
                    // calculate % user will be in the top (old method getFortuneTelling())
                    // fortune telling will be stored in TOP.Position
                    // defined in percents
                    stat.TOP.MaxPositions = 100; // 100%
                    if (day.TOP.Position >= 1 && day.TOP.Position <= 10)
                    {
                        stat.TOP.Position = (stat.TOP.Position + 100) / 2;
                    }
                    else
                        if (day.TOP.Position > 10)
                        {
                            stat.TOP.Position = stat.TOP.Position / 2;
                        }
                    // end fortune
                }
                date = date.AddDays(-1);
            }
            stat.IsLoaded = !setError;
            return stat;
        }

        // return weekly traffic info for specified week
        public TrafficHistory getByWeek(DateTime dayInWeek, bool fakeParam)
        {
            LastOperationCompletedSuccessfully = true;

            if (StatCache.searchDay(dayInWeek, false) != -1)
            {
                // check is results already exists in cache
                return StatCache.getWeek(dayInWeek);
            }
            string url = prepareStatUrl(dayInWeek,
                ClientParams.Parameters.TrafficStatWeeklyUrl);
            string html = HttpRequest.readUrl(url);
            if (HttpRequest.LastOperationCompletedSuccessfully)
            {
                // if there are no errors during reading statistic
                TrafficHistory stat = new TrafficHistory();
                stat.WeekNumber = getWeekNumber(dayInWeek);
                if (ClientParams.Parameters.TOPenabled)
                {
                    stat.TOP = getTOP(html);
                }
                html = HttpRequest.cutHtml(html);
                //stat.DateTime = date;
                Regex regex = new Regex(ClientParams.Parameters.TrafficStatPattern);
                Match m = regex.Match(html);
                while (m.Success)
                {
                    stat.WebSite.Add(m.Groups[1].Value);
                    stat.UsedTraffic.Add(Convert.ToInt32(m.Groups[2].Value));
                    //if (!ClientParams.Parameters.TrafficFilterEnabled ||
                    //    ClientParams.Parameters.TrafficFilterEnabled && !TrafficFilter.isInList(m.Groups[1].Value))
                    //{
                    // add value to total amount only in case
                    // filtering is disabled or site is not present in filter
                    //stat.TotalUsedTraffic += Convert.ToInt32(m.Groups[2].Value);
                    //}
                    m = m.NextMatch();
                }

                StatCache.updateCache(stat);

                stat.IsLoaded = true;
                return stat;
            }
            else
            {
                LastOperationCompletedSuccessfully = false;
                return new TrafficHistory();
            }
        }

        /// <summary>
        /// return traffic size and enum representation
        /// </summary>
        public long[] convertBytes(long bytes)
        {
            return convertBytes(bytes, 1, 8);
        }

        public long[] convertBytes(long bytes, int minEnum, int maxEnum)
        {
            int value = 1024;
            if (!ClientParams.Parameters.TrafficRoundUp) value = 10240; // 10 KB
            int enumeration = 1;
            double traffic = bytes;
            while ((traffic > value && enumeration < maxEnum) || enumeration < minEnum)
            {
                traffic = traffic / 1024;
                enumeration = enumeration * 2;
            }
            return new long[] { (long)Math.Round(traffic,0), enumeration };
        }

        /// <summary>
        /// return traffic with size representation
        /// </summary>
        public string getConvertedBytes(long bytes)
        {
            long[] result = convertBytes(bytes);
            return string.Format("{0} {1}", result[0].ToString(), l.GetMessage(((TrafficEnumeration)result[1]).ToString()));
        }

        /// <summary>
        /// parse string and insert date time to input string
        /// </summary>
        private string prepareStatUrl(DateTime date, string Url)
        {
            // insert server based on [SERVER]
            string result = Url.Replace("[SERVER]",
                fwservers.getHttpAddress(ClientParams.Parameters.Location));
            // insert date based on standard DateTime elements
            string dateInUrl = result.Substring(result.IndexOf('[') + 1, 
                result.LastIndexOf(']') - result.IndexOf('[') - 1);
            string dateToInsert = date.ToString(dateInUrl);
            // insert week number based on WW
            if (dateToInsert.Contains("WW"))
            {
                dateToInsert = dateToInsert.Replace("WW", getWeek(date));
            }
            result = result.Replace("[" + dateInUrl + "]", dateToInsert);
            return result;
        }

        /// <summary>
        /// retrieve week number in double digits format
        /// </summary>
        private string getWeek(DateTime date)
        {
            int week = getWeekNumber(date);
            if (week <= 9)
            {
                return "0" + week.ToString();
            }
            else
            {
                return week.ToString();
            }
            //return string.Format("{0:00}", week.ToString());
        }

        /// <summary>
        /// calculate week number
        /// </summary>
        private sbyte getWeekNumber(DateTime date)
        {
            sbyte weeks = 1;
            DateTime s = new DateTime(date.Year, 1, 1);
            while (s < date)
            {
                if (s.DayOfWeek == System.DayOfWeek.Monday)
                {
                    weeks++;
                    s = s.AddDays(7);
                }
                else
                {
                    s = s.AddDays(1);
                }
            }
            return weeks;
        }

        // return user position in TOP
        public TOP getTOP(string html)
        {
            TOP t = new TOP();
            t.MaxPositions = 0;
            t.Position = 0;
            Regex regex = new Regex(ClientParams.Parameters.TrafficTopPattern);
            Match m = regex.Match(html);
            while (m.Success)
            {
                // count positions available
                t.MaxPositions++;
                // check position of user
                if (m.Captures.Count > 0)
                {
                    if (m.Captures[0].Value.Contains(ClientParams.Parameters.MachineName))
                    {
                        t.Position = t.MaxPositions;
                    }
                }
                // next iteration
                m = m.NextMatch();
            }
            return t;
        }

        // calculate % user will be in the top
        //public int getFortuneTelling()
        //{
        //    int result = 0;
        //    int[] days = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
        //    List<TrafficHistory> history = getByWeek(DateTime.Now);
        //    foreach (TrafficHistory record in history)
        //    {
        //        days[history.IndexOf(record)] = record.TOP.Position;
        //    }
        //    foreach (int record in days)
        //    {
        //        if (record >= 1 && record <= 10)
        //        {
        //            result = (result + 100) / 2;
        //        }
        //        else
        //            if (record > 10)
        //            {
        //                result = result / 2;
        //            }
        //    }
        //    return result;
        //}

        public void LanguageChanged()
        {
            l = new Languages(ClientParams.Parameters.Language);
        }
    }
}
