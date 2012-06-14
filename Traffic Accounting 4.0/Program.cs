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
                    bool Running = !mutex.WaitOne(0, false);
                    if (!Running)
                    {
                        string ta = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Traffic Accounting";
                        if (!Directory.Exists(ta))
                        {
                            Directory.CreateDirectory(ta);
                        }

                        ClientParams p = new ClientParams();
                        Assembly a = Assembly.GetExecutingAssembly();
                        ClientParams.Parameters.AssemblyFullName = a.Location;
                        p.LoadClientParams();

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new fakeForm());
                    }
                    else
                    {
                        // TODO: Initialize language parameters at startup and display correct messages
                        //       according to user's language selection
                        MessageBox.Show("Traffic Accounting 4.0 is already run...", "Traffic Accounting");
                    }
                }
            }
        }
    }
}
