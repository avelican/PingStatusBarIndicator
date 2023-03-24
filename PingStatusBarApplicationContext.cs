using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace PingStatusBarIndicator
{
    class PingStatusBarApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon _trayIcon;
        private readonly System.Windows.Forms.Timer _pingTimer;
        private readonly Ping _ping;
        private readonly int _historyLength = 10;
        private readonly bool[] _pingResults;

        private int _pingIndex;

        public PingStatusBarApplicationContext()
        {
            _pingResults = new bool[_historyLength];
            _ping = new Ping();
            _ping.PingCompleted += OnPingCompleted;

            _pingTimer = new System.Windows.Forms.Timer
            {
                Interval = 1000,
            };
            _pingTimer.Tick += OnPingTimerTick;
            _pingTimer.Start();

            _trayIcon = new NotifyIcon
            {
                Icon = CreateIcon(Color.White),
                Visible = true,
                // ContextMenu = new ContextMenu(new[] { new MenuItem("Exit", Exit) }),
            };

            _trayIcon.ContextMenuStrip = new ContextMenuStrip();
            _trayIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Exit));
        }

        private void OnPingTimerTick(object sender, EventArgs e)
        {
            _ping.SendAsync("8.8.8.8", 1000);
        }

        private void OnPingCompleted(object sender, PingCompletedEventArgs e)
        {
            bool success = e.Reply?.Status == IPStatus.Success;
            _pingResults[_pingIndex] = success;
            _pingIndex = (_pingIndex + 1) % _historyLength;

            int successfulPings = 0;
            for (int i = 0; i < _historyLength; i++)
            {
                if (_pingResults[i]) successfulPings++;
            }

            double successRate = (double)successfulPings / _historyLength;
            _trayIcon.Icon = CreateIcon(GetColorBySuccessRate(successRate));
        }

        private Color GetColorBySuccessRate(double successRate)
        {
            if (successRate >= 0.9) return Color.Green;
            if (successRate >= 0.5) return Color.Yellow;
            return Color.Red;
        }

        private Icon CreateIcon(Color color)
        {
            var bitmap = new Bitmap(16, 16);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(color);
            }

            return Icon.FromHandle(bitmap.GetHicon());
        }

        private void Exit(object sender, EventArgs e)
        {
            _trayIcon.Visible = false;
            Application.Exit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _trayIcon.Dispose();
                _pingTimer.Dispose();
                _ping.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
