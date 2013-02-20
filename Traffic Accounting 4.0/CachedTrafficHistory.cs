﻿/*   
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

using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ItWorksTeam.IO;

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
            // check version compatibility
            Registry registry = new Registry();
            int assemblyBuild = Assembly.GetExecutingAssembly().GetName().Version.Revision;
            int lastusedBuild = registry.ReadKey<int>(Registry.BaseKeys.HKEY_CURRENT_USER,
                "Software\\ItWorksTeam\\Traffic Accounting\\Version 4.0", "LastBuildRun", 0);
            if (assemblyBuild != lastusedBuild)
            {
                if (File.Exists(CacheFileName))
                {
                    File.Delete(CacheFileName);
                    registry.SaveKey(Registry.BaseKeys.HKEY_CURRENT_USER,
                        "Software\\ItWorksTeam\\Traffic Accounting\\Version 4.0", "LastBuildRun",
                        assemblyBuild);
                }
            }

            loadCache();
        }

        // search is date present in cache?
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
            int index = searchDay(date, true);
            TrafficHistory h = TrafficHistoryCache[index];
            if (index >= 0)
            {
                h.IsLoaded = true;
            }
            return h;
        }

        // retrieve week item from cache
        public TrafficHistory getWeek(DateTime date)
        {
            int index = searchDay(date, false);
            TrafficHistory h = TrafficHistoryCache[index];
            if (index >= 0)
            {
                h.IsLoaded = true;
            }
            return h;
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
                        (history.WeekNumber != -1 && history.WeekNumber < getWeekNumber(DateTime.Now.AddDays(0 - ClientParams.Parameters.TrafficCacheSize)));
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
                if (s.DayOfWeek == System.DayOfWeek.Sunday)
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
