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
        private const string CacheFileName = "cache.xml";
        // constructor
        // load cache
        public CachedTrafficHistory()
        {
            loadCache();
        }

        // search is date is present in cache
        public int searchDay(DateTime date)
        {
            return TrafficHistoryCache.FindIndex(
                delegate(TrafficHistory day)
                {
                    return day.DateTime.DayOfYear == date.DayOfYear;
                }
                );
        }

        // retrieve item from cache
        public TrafficHistory getDay(DateTime date)
        {
            return TrafficHistoryCache[searchDay(date)];
        }

        // add item to cache
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
                    return history.DateTime.DayOfYear < DateTime.Now.AddDays(0 - ClientParams.Parameters.TrafficCacheSize).DayOfYear;
                });
        }
    }
}
