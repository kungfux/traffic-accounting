using System;
using System.Windows.Forms;
using System.Threading;

namespace Traffic_Accounting
{
    public class Program
    {
        private const string AppMutexName = "Traffic Accounting 4.0";

        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, AppMutexName))
            {
                bool Running = !mutex.WaitOne(0, false);
                if (!Running)
                {
                    ClientParams p = new ClientParams();
                    p.LoadClientParams();

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new TA());
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
