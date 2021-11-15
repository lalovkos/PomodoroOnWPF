using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;

namespace PomodoroOnWPF
{
    
    public class AlarmViewModel : INotifyPropertyChanged {

        public readonly TimeSpan STANDART_DATA_RELOAD_TIMESPAN = new TimeSpan(0, 0, 0, 0, 250);

        #region EventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        //For realtime clocks
        public string RealTimeClock { get; set; } = DateTime.Now.ToLongTimeString();

        //List of alarms
        private ObservableCollection<AlarmStats> _alarms;
        public ObservableCollection<AlarmStats> Alarms {
            get {
                return _alarms;
            }
            set {
                if (value == _alarms)
                    return;

                _alarms = value;
                OnPropertyChanged();
            }
        }
        public AlarmStats SelectedAlarm { get; set; }

        private AlarmStats _curTimer;

        #region Commands
        public ICommand AddRowCommand { get; set; }
        public ICommand DeleteRowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public ICommand StartTimerCommand { get; set; }
        public ICommand ResetTimerCommand { get; set; }
        public ICommand PauseTimerCommand { get; set; }
        public ICommand ResumeTimerCommand { get; set; }
        #endregion

        //TODO: make work only when Minimazed
        private readonly PausableDispatcherTimer _reloadTimer = new PausableDispatcherTimer();

        //ButtonsVisibility
        public Visibility StartBtVisible { get; set; } = Visibility.Visible;
        public Visibility ResetBtVisible { get; set; } = Visibility.Hidden;
        public Visibility PauseBtVisible { get; set; } = Visibility.Visible;
        public Visibility ResumeBtVisible { get; set; } = Visibility.Hidden;


        //Current alarm window
        private AlarmWindow _curAlarmWindow;

        public AlarmViewModel() {

            BindCommands();
            StartGlobalTimer();

            _alarms = new ObservableCollection<AlarmStats>();

            //Adding 2 starting standart rows
            AddRow();
            AddRow();
            SelectedAlarm = Alarms[0];
            _curTimer = SelectedAlarm;
        }

        private void StartGlobalTimer() {
            //Data reload timer
            _reloadTimer.Interval = STANDART_DATA_RELOAD_TIMESPAN;
            _reloadTimer.Tick += new EventHandler(ReloadData);
            _reloadTimer.Start();
        }

        private void BindCommands() {
            //Grid commands
            AddRowCommand = new RelayCommand(AddRow);
            DeleteRowCommand = new RelayCommand(DeleteRow);

            //Timer commands
            StartTimerCommand = new RelayCommand(StartTimer);
            ResetTimerCommand = new RelayCommand(ResetTimer);
            PauseTimerCommand = new RelayCommand(PauseTimer);
            ResumeTimerCommand = new RelayCommand(ResumeTimer);
        }

        private void AddRow() {
            if (_alarms.Count < 10) {
                _alarms.Add(new AlarmStats("Work", new TimeSpan(0, 20, 0)));
            }
        }

        private void DeleteRow() {
            if(SelectedAlarm != null && _alarms.Count > 0) Alarms.Remove(SelectedAlarm);
        }

        public void ReloadData(object sender = null, EventArgs e = null) {

            if (_curTimer != null) {
                _curTimer.Reload();
            }

            //Reloading real time clock
            RealTimeClock = DateTime.Now.ToLongTimeString();
            OnPropertyChanged("RealTimeClock");

        }

        private void NextTimer() {

        }

        public void ShowAlarm(object sender = null, EventArgs e = null) {
            _curAlarmWindow = new AlarmWindow("Start", "Next stage", null, AlarmWindowClosed);
            _curAlarmWindow.Activate();
            _curAlarmWindow.Show();
        }

        public void AlarmWindowClosed(object resume) {
            if (resume is bool && (bool) resume) { ResumeTimer();}
            else { PauseTimer(); }
        }

        private void StartTimer() {
            if (_curTimer == null) return;

            //Buttons visibility
            StartBtVisible = Visibility.Hidden;
            ResetBtVisible = Visibility.Visible;
            OnPropertyChanged("StartBtVisible");
            OnPropertyChanged("ResetBtVisible");

            //Start current timer
            _curTimer = SelectedAlarm;
            _curTimer.Start();
        }
        private void ResetTimer() {
            if (_curTimer == null) return;

            ResumeTimer();

            //Buttons visibility
            StartBtVisible = Visibility.Visible;
            ResetBtVisible = Visibility.Hidden;
            OnPropertyChanged("StartBtVisible");
            OnPropertyChanged("ResetBtVisible");

            //Reset current timer
            _curTimer.Reset(false);
        }
        private void PauseTimer() {
            if (_curTimer == null) return;

            //Buttons visibility
            PauseBtVisible = Visibility.Hidden;
            ResumeBtVisible = Visibility.Visible;
            OnPropertyChanged("PauseBtVisible");
            OnPropertyChanged("ResumeBtVisible");

            //Pause current timer
            _curTimer.Stop();
        }
        private void ResumeTimer() {
            if (_curTimer == null) return;

            //Buttons visibility
            PauseBtVisible = Visibility.Visible;
            ResumeBtVisible = Visibility.Hidden;
            OnPropertyChanged("PauseBtVisible");
            OnPropertyChanged("ResumeBtVisible");

            //Resume current timer
            _curTimer.Start();
        }

    }
}
