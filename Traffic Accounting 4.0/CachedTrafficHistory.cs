using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Traffic_Accounting
{
    internal class CachedTrafficHistory
    {
        // private items
        private List<TrafficHistory> TrafficHistoryCache = new List<TrafficHistory>();
        private readonly string CacheFileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Traffic Accounting\\cache.xml";
        // constructor
        // load cache
        public CachedTrafficHistory()
        {
            loadCache();
        }

        // search is date is present in cache
        // else search week
        public int searchDay(DateTime date, bool OnlySingleDays)
        {
            if (OnlySingleDays)
            {
                return TrafficHistoryCache.FindIndex(
                    delegate(TrafficHistory day)
                    {
                        return day.WeekNumber == -1 && day.DateTime.DayOfYear == date.DayOfYear;
                    }
                    );
            }
            else
            {
                int weekNumber = getWeekNumber(date);
                return TrafficHistoryCache.FindIndex(
                    delegate(TrafficHistory day)
                    {
                        return day.WeekNumber == weekNumber;
                    }
                    );
            }
        }

        // retrieve day item from cache
        public TrafficHistory getDay(DateTime date)
        {
            return TrafficHistoryCache[searchDay(date, true)];
        }

        // retrieve week item from cache
        public TrafficHistory getWeek(DateTime date)
        {
            return TrafficHistoryCache[searchDay(date, false)];
        }

        // add item to cache
        public void updateCache(TrafficHistory StatDay)
        {
            if (searchDay(StatDay.DateTime, true) == -1)
            {
                // add new cache item
                TrafficHistoryCache.Add(StatDay);
            }
            else
            {
                // update existing
                TrafficHistoryCache[searchDay(StatDay.DateTime, true)] = StatDay;
            }
            // sort stat by day
            TrafficHistoryCache.Sort(
                delegate(TrafficHistory day1, TrafficHistory day2)
                {
                    return day1.DateTime.CompareTo(day2.DateTime);
                });
            // save dump
            saveCache();
        }

        // load cache from fs
        public void loadCache()
        {
            List<TrafficHistory> loaded = DeserializeClass<List<TrafficHistory>>(CacheFileName);
            if (loaded != null)
            {
                TrafficHistoryCache = loaded;
            }
        }

        // save cache to fs
        public void saveCache()
        {
            ClearOutLimitedCache();
            SerializeClass<List<TrafficHistory>>(TrafficHistoryCache, CacheFileName);
        }

        // method for serializaion
        private bool SerializeClass<T>(T Class, string fullPathToFile)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StreamWriter sw = new StreamWriter(fullPathToFile);
                xs.Serialize(sw, Class);
                sw.Close();
                return true;
            }
            catch (IOException) 
            { 
                return false; 
            }
        }

        // method for deserialization
        public T DeserializeClass<T>(string fullPathToFile)
        {
            T result = default(T);
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StreamReader sr = new StreamReader(fullPathToFile);
                result = (T)xs.Deserialize(sr);
                sr.Close();
            }
            catch (IOException) 
            { 
            }
            catch (InvalidOperationException) 
            {
            }
            return result;
        }

        // remove old items from cache
        private void ClearOutLimitedCache()
        {
            TrafficHistoryCache.RemoveAll(
                delegate(TrafficHistory history)
                {
                    return 
                        (history.WeekNumber == -1 && history.DateTime.DayOfYear < DateTime.Now.AddDays(0 - ClientParams.Parameters.TrafficCacheSize).DayOfYear) ||
                        (history.WeekNumber != -1 && history.WeekNumber < getWeekNumber(DateTime.Now.AddDays(-7)));
                });
        }

        /// <summary>
        /// calculate week number
        /// </summary>
        private int getWeekNumber(DateTime date)
        {
            int weeks = 1;
            DateTime s = new DateTime(DateTime.Today.Year, 1, 1);
            while (s < date)
            {
                if (s.DayOfWeek == DayOfWeek.Sunday)
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
    }
}
