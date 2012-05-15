using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;

namespace Traffic_Accounting
{
    public class TrafficStatCache
    {
        public int CacheSize = 7;

        private List<TrafficStatDay> TrafficCache = new List<TrafficStatDay>();
        private string CacheFileName = "cache.xml";

        public TrafficStatCache()
        {
            loadCache();
        }

        /// <summary>
        /// search is date is present in cache
        /// </summary>
        public int searchDay(DateTime date)
        {
            return TrafficCache.FindIndex(
                delegate(TrafficStatDay day)
                {
                    return day.Day.DayOfYear == date.DayOfYear;
                }
                );
        }

        /// <summary>
        /// retrieve item from cache
        /// </summary>
        public TrafficStatDay getDay(DateTime date)
        {
            return TrafficCache[searchDay(date)];
        }

        /// <summary>
        /// add item to cache
        /// </summary>
        public void updateCache(TrafficStatDay StatDay)
        {
            if (searchDay(StatDay.Day) == -1)
            {
                // add new cache item
                TrafficCache.Add(StatDay);
            }
            else
            {
                // update existing
                TrafficCache[searchDay(StatDay.Day)] = StatDay;
            }
            saveCache();
        }

        public void loadCache()
        {
            List<TrafficStatDay> loaded = DeserializeClass<List<TrafficStatDay>>(CacheFileName);
            if (loaded != null)
            {
                TrafficCache = loaded;
            }
        }

        public void saveCache()
        {
            SerializeClass<List<TrafficStatDay>>(TrafficCache, CacheFileName);
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
