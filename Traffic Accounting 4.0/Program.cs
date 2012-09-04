﻿/*   
 *  Traffic Accounting 4.0
 *  Traffic reporting system
 *  Copyright (C) IT WORKS TEAM 2008-2012
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
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;
using Traffic_Accounting.DebugScreen;

namespace Traffic_Accounting
{
    public class Program
    {
        private const string AppMutexName = "Traffic Accounting 4.0";

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "/debug")
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new DebugForm());
            }
            else
            {
                using (Mutex mutex = new Mutex(false, AppMutexName))
                {
                    ClientParams p = new ClientParams();
                    string ta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Traffic Accounting";
                    if (!Directory.Exists(ta))
                    {
                        Directory.CreateDirectory(ta);
                    }
                    Assembly a = Assembly.GetExecutingAssembly();
                    ClientParams.Parameters.AssemblyFullName = a.Location.ToLower();
                    // Load client parameters
                    p.LoadClientParams();

                    bool Running = !mutex.WaitOne(0, false);
                    if (!Running)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainThread());
                    }
                    else
                    {
                        // TODO: Initialize language parameters at startup and display correct messages
                        //       according to user's language selection
                        //MessageBox.Show("Traffic Accounting 4.0 is already run.", "Traffic Accounting");
                        Languages l = new Languages(ClientParams.Parameters.Language);
                        MessageBox.Show(l.GetMessage("PROGRAM001"), "Traffic Accounting");
                    }
                }
            }
        }
    }
}
