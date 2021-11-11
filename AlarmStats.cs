using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroOnWPF
{
    public class AlarmStats
    {
        public string Name { get; set; }
        public TimeSpan Interval { get; set; }
        public string Description { get; set; }
        public int PausesCount { get; set; }

        public TimeSpan TimeLeft { get; set; }
        public TimeSpan SumTime { get; set; }
        public bool Enabled { set; get; }

        private AlarmTimer _timer = new AlarmTimer();

        public AlarmStats(string name, TimeSpan interval, string description = "")
        {
            this.Name = name;
            this.Description = description;
            this.Interval = interval;
            this.PausesCount = 0;
            this._timer.Interval = interval;
            this.Enabled = false;
        }

        public void Reload() {
            this.TimeLeft = this._timer.TimeLeft();
            this.Enabled = this._timer.IsEnabled;
        }
    }
}
