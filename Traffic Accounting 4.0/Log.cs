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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Traffic_Accounting
{
    internal class Log
    {
        private static Log _log;
        private static Object _lock = new Object();

        private string TraceFile = Application.StartupPath + "\\trace.txt";
        private StreamWriter writer;

        public static Log Trace
        {
            get
            {
                if (_log == null)
                {
                    lock (_lock)
                    {
                        _log = new Log();
                    }
                }
                return _log;
            }
        }

        public Log()
        {
            if (!ClientParams.Parameters.TraceEnabled)
            {
                return;
            }
            try
            {
                writer = new StreamWriter(TraceFile);
            }
            catch (IOException)
            {
                return;
            }
            writer.AutoFlush = true;
            addTrace("Log started");
            addTrace(string.Format("User: {0}, Machine: {1}",
                Environment.UserName,
                Environment.MachineName));
        }

        ~Log()
        {
            if (writer != null && writer.BaseStream.CanWrite)
            {
                writer.Close();
            }
        }

        public void addTrace(string message)
        {
            if (!ClientParams.Parameters.TraceEnabled)
            {
                return;
            }
            if (writer == null)
            {
                return;
            }
            StackTrace stackTrace = new StackTrace();
            writer.WriteLine(
                string.Format(
                "{0} from {1}: {2}",
                DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff"),
                stackTrace.GetFrame(1).GetMethod().Name,
                message
                ));
        }
    }
}
