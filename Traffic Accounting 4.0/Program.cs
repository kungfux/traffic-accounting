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
using System.Threading;
using System.IO;
using System.Reflection;
using Traffic_Accounting.DebugScreen;
using System.Runtime.InteropServices;

namespace Traffic_Accounting
{
    public class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        // Application mutex
        private const string AppMutexName = "Traffic Accounting 4.0";

        [STAThread]
        static void Main(string[] args)
        {
            // Initialize configuration
            ClientParams p = new ClientParams();
            Assembly a = Assembly.GetExecutingAssembly();
            ClientParams.Parameters.AssemblyFullName = a.Location.ToLower();
            // Load client parameters
            p.LoadClientParams();
            // load language
            Languages l = new Languages(ClientParams.Parameters.Language);
            
            Log.Trace.addTrace("Starting application");

            if (args.Length > 0 && args[0].Length > 0)
            {
                switch(args[0])
                {
                    case "/debug":
                        // show debug form
                        Log.Trace.addTrace("Running debug form");
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new DebugForm());
                        break;
                }
            }
            else
            {
                Log.Trace.addTrace("Running main thread");

                using (Mutex mutex = new Mutex(false, AppMutexName))
                {
                    string ta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Traffic Accounting";
                    if (!Directory.Exists(ta))
                    {
                        Directory.CreateDirectory(ta);
                    }

                    bool Running = !mutex.WaitOne(0, false);
                    if (!Running)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainThread());
                    }
                    else
                    {
                        MessageBox.Show(l.GetMessage("PROGRAM001"), AppMutexName,
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            Log.Trace.addTrace("Program ended");
        }
    }
}
