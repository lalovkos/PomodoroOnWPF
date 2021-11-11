using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace PomodoroOnWPF {
    class AlarmTimer : DispatcherTimer {

        private DateTime _lastPauseDateTime;
        private TimeSpan _startedInterval;
        private TimeSpan _curInterval;

        private new TimeSpan Interval {
            get {
                return _curInterval;
            }

            set {
                _startedInterval = value;
                _curInterval = value;
                this.Interval = value;
            }
        }

        public AlarmTimer() : base() {
            _lastPauseDateTime = DateTime.Now;
        }

        public TimeSpan TimeLeft() {
            if (this.IsEnabled) {
                return (_lastPauseDateTime + _curInterval) - DateTime.Now;
            }
            else {
                return this.Interval;
            }

        }

        public new void Stop() {
            base.Stop();
            _curInterval = TimeLeft();
            this.Interval = _curInterval;
            _lastPauseDateTime = DateTime.Now;

        }

        public new void Start() {
            base.Start();
            _lastPauseDateTime = DateTime.Now;
        }

        public void Reset(bool stop = false) {
            this.Interval = _startedInterval;
            if (stop) {
                this.Stop();
            }
        }

    };
}