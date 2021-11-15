using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace PomodoroOnWPF {
    class PausableDispatcherTimer : DispatcherTimer {

        private DateTime _lastPauseDateTime;
        private TimeSpan _startedInterval;

        public new TimeSpan Interval{
            
            get {
                return _startedInterval;
            }

            set {
                base.Interval = value;
                _startedInterval = value;
                base.Stop();
            }
            
        }

        public TimeSpan TimeLeft() {
            if (this.IsEnabled) {
                return (_lastPauseDateTime + base.Interval) - DateTime.Now;
            }
            else {
                return base.Interval;
            }
        }

        public TimeSpan TimePassed() {
            return this._startedInterval - TimeLeft();
        }

        public new void Stop() {
            base.Interval = TimeLeft();
            TimeLeft();
            base.Stop();
        }

        public new void Start() {
            _lastPauseDateTime = DateTime.Now;
            base.Start();
        }

        public void Reset() {
            base.Stop();
            base.Interval = _startedInterval;
        }

    };
}