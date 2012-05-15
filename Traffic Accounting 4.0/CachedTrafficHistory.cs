using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;

namespace Traffic_Accounting
{
    public class CachedTrafficHistory
    {
        public int CacheSize = 7;

        private List<TrafficHistory> TrafficHistoryCache = new List<TrafficHistory>();
        private string CacheFileName = "cache.xml";

        public CachedTrafficHistory()
        {
            loadCache();
        }

        /// <summary>
        /// search is date is present in cache
        /// </summary>
        public int searchDay(DateTime date)
        {
            return TrafficHistoryCache.FindIndex(
                delegate(TrafficHistory day)
                {
                    return day.DateTime.DayOfYear == date.DayOfYear;
                }
                );
        }

        /// <summary>
        /// retrieve item from cache
        /// </summary>
        public TrafficHistory getDay(DateTime date)
        {
            return TrafficHistoryCache[searchDay(date)];
        }

        /// <summary>
        /// add item to cache
        /// </summary>
        public void updateCache(TrafficHistory StatDay)
        {
            if (searchDay(StatDay.DateTime) == -1)
            {
                // add new cache item
                TrafficHistoryCache.Add(StatDay);
            }
            else
            {
                // update existing
                TrafficHistoryCache[searchDay(StatDay.DateTime)] = StatDay;
            }
            saveCache();
        }

        public void loadCache()
        {
            List<TrafficHistory> loaded = DeserializeClass<List<TrafficHistory>>(CacheFileName);
            if (loaded != null)
            {
                TrafficHistoryCache = loaded;
            }
        }

        public void saveCache()
        {
            SerializeClass<List<TrafficHistory>>(TrafficHistoryCache, CacheFileName);
        }

        public bool SerializeClass<T>(T Class, string fullPathToFile)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                StreamWriter sw = new StreamWriter(fullPathToFile);
                xs.Serialize(sw, Class);
                sw.Close();
                return true;
            }
            catch (IOException ex) 
            { 
                return false; 
            }
        }

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
            catch (IOException ex) 
            { 
            }
            catch (InvalidOperationException ex) 
            {
            }
            return result;
        }
    }
}
