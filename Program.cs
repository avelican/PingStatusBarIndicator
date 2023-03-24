using System;
using System.Windows.Forms;

namespace PingStatusBarIndicator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var applicationContext = new PingStatusBarApplicationContext();
            Application.Run(applicationContext);
        }
    }
}
