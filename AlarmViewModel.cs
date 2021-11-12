using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;

namespace PomodoroOnWPF
{
    
    public class AlarmViewModel : INotifyPropertyChanged{

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //For realtime clocks
        public string RealTimeClock { get; set; } = DateTime.Now.ToLongTimeString();

        //List of alarms
        public BindingList<AlarmStats> Alarms { get; set; } = new BindingList<AlarmStats>();
        public AlarmStats SelectedAlarm { get; set; }

        //Commands
        public ICommand AddRowCommand { get; set; } 
        public ICommand DeleteRowCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public ICommand StartTimerCommand { get; set; }
        public ICommand ResetTimerCommand { get; set; }
        public ICommand PauseTimerCommand { get; set; }
        public ICommand ResumeTimerCommand { get; set; }


        //TODO: make work only when Minimazed
        private AlarmTimer _reloadTimer = new AlarmTimer();

        //ButtonsVisibility
        public Visibility StartBtVisible { get; set; } = Visibility.Visible;
        public Visibility ResetBtVisible { get; set; } = Visibility.Hidden;
        public Visibility PauseBtVisible { get; set; } = Visibility.Visible;
        public Visibility ResumeBtVisible { get; set; } = Visibility.Hidden;


        //Current alarm window
        private AlarmWindow _curAlarmWindow;

        public AlarmViewModel() {

            //Grid commands
            AddRowCommand = new RelayCommand(AddRow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);

            //Timer commands
            StartTimerCommand = new RelayCommand(StartTimer);
            ResetTimerCommand = new RelayCommand(ResetTimer);
            PauseTimerCommand = new RelayCommand(PauseTimer);
            ResumeTimerCommand = new RelayCommand(ResumeTimer);

            //Data reload timer
            _reloadTimer.Interval = new TimeSpan(0, 0, 1);
            _reloadTimer.Tick += new EventHandler(ReloadData);
            _reloadTimer.Start();

            //Adding 2 starting rows
            AddRow();
            AddRow();
        }

        private void SelectionChanged() {
            
        }

        private void AddRow() {
            if (Alarms.Count < 10) {
                Alarms.Add(new AlarmStats("Work", new TimeSpan(0, 20, 0)));
            }
        }

        private void DeleteRow() {
            if(SelectedAlarm != null) Alarms.Remove(SelectedAlarm);
        }

        public void ReloadData(object sender = null, EventArgs e = null) {

            if (SelectedAlarm != null) SelectedAlarm.Reload();

            //Reloading real time clock
            RealTimeClock = DateTime.Now.ToLongTimeString();
            OnPropertyChanged("RealTimeClock");
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
            //Buttons visibility
            StartBtVisible = Visibility.Hidden;
            ResetBtVisible = Visibility.Visible;
            OnPropertyChanged("StartBtVisible");
            OnPropertyChanged("ResetBtVisible");

        }
        private void ResetTimer() {
            //Buttons visibility
            StartBtVisible = Visibility.Visible;
            ResetBtVisible = Visibility.Hidden;
            OnPropertyChanged("StartBtVisible");
            OnPropertyChanged("ResetBtVisible");
        }
        private void PauseTimer() {
            //Buttons visibility
            PauseBtVisible = Visibility.Hidden;
            ResumeBtVisible = Visibility.Visible;
            OnPropertyChanged("PauseBtVisible");
            OnPropertyChanged("ResumeBtVisible");
        }
        private void ResumeTimer() {
            //Buttons visibility
            PauseBtVisible = Visibility.Visible;
            ResumeBtVisible = Visibility.Hidden;
            OnPropertyChanged("PauseBtVisible");
            OnPropertyChanged("ResumeBtVisible");
        }

    }
}
