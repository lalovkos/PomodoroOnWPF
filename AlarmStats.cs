using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PomodoroOnWPF
{
    public class AlarmStats : INotifyPropertyChanged
    {
        #region EventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string Name { get; set; }

        private TimeSpan _interval;
        public TimeSpan Interval {
            get {
                return this._interval;
            }
            set {
                this._interval = value;
                this._timer.Interval = value;
                OnPropertyChanged();
            }
        }
        public string Description { get; set; }

        private int _pausesCount;
        public int PausesCount {
            get {
                return _pausesCount;
            } 
            set {
                this._pausesCount = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabled;
        public bool IsEnabled {
            get {
                return this._isEnabled;
            }
            private set {
                this._isEnabled = value;
                this._timer.IsEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        private string _timeLeft;
        public string TimeLeft {
            get {
                return _timeLeft;
            }
            set
            {
            if (value == _timeLeft)
                return;

            _timeLeft = value;
            OnPropertyChanged();
            }
        }

        private string _sumTime;
        public string SumTime {
            get {
                return _sumTime;
            }
            set {
                if (value == _sumTime)
                    return;

                _sumTime = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _fullWorkingTime;

        private readonly PausableDispatcherTimer _timer = new PausableDispatcherTimer();

        public AlarmStats(string name, TimeSpan interval, EventHandler on_timer_elapse = null, string description = "") {
            this.Name = name;
            this.Description = description;
            this.Interval = interval;
            this.PausesCount = 0;
            this._timer.Tick += on_timer_elapse;
            this._timer.Interval = interval;
            this.IsEnabled = this._timer.IsEnabled;
        }

        public void Reload() {
            this.TimeLeft = this._timer.TimeLeft().ToString(@"hh\:mm\:ss");
            this.SumTime = (_fullWorkingTime + this._timer.TimePassed()).ToString(@"hh\:mm\:ss");

        }

        public void Start() {
            _timer.Start();
            UpdateIsEnabled();
        }

        public void Stop() {
            _timer.Stop();
            UpdateIsEnabled();
            PausesCount++;
        }

        public void Reset(bool save_working_time = false) {
            PausesCount = 0;
            if (save_working_time) {
                _fullWorkingTime += this._timer.TimePassed();
            }
            else {
                _fullWorkingTime = new TimeSpan(0);
            }

            //Resetting timer
            _timer.Reset();

            UpdateIsEnabled();
            Reload();
        }

        private void UpdateIsEnabled() {
            IsEnabled = _timer.IsEnabled;
        }

    }
}
