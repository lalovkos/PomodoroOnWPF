using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Documents.DocumentStructures;
using System.Windows.Input;

namespace PomodoroOnWPF
{
    
    public class AlarmViewModel {

        //List of alarms
        public BindingList<AlarmStats> Alarms { get; set; } = new BindingList<AlarmStats>();
        public AlarmStats SelectedAlarm { get; set; }

        //Real time clocks
        public string RealTimeClock { get; set; }

        //Commands
        public ICommand AddRowCommand { get; set; } 
        public ICommand GetRowInfoCommand { get; set; }

        private AlarmTimer _reloadTimer = new AlarmTimer();

        //Current alarm window
        private AlarmWindow _curAlarmWindow;

        public AlarmViewModel() {
            AddRowCommand = new RelayCommand(AddRow);
            GetRowInfoCommand = new RelayCommand(GetRowInfo);

            _reloadTimer.Interval = new TimeSpan(0, 0, 1);
            _reloadTimer.Tick += new EventHandler(ReloadData);
            _reloadTimer.Start();
        }

        private void AddRow() {
            Alarms.Add(new AlarmStats("Work", new TimeSpan(0, 20, 0)));
        }

        private void GetRowInfo() {

        }

        //public void NextTimer() {
        //    if (_curAlarm.Current != null) {
        //        _curAlarm.MoveNext();
        //        _curTimer.Stop();
        //        _curTimer.Interval = _curAlarm.Current.Interval;
        //        _curTimer.Start();
        //    }
        //}

        public void ReloadData(object sender, EventArgs e) {
            if (SelectedAlarm != null) SelectedAlarm.Reload();
            RealTimeClock = DateTime.Now.ToLongTimeString();
        }

        public void ShowAlarm(object sender, EventArgs e) {
            _curAlarmWindow = new AlarmWindow("Start", "Next stage", null, AlarmWindowClosed);
            _curAlarmWindow.Activate();
            _curAlarmWindow.Show();

        }

        public void AlarmWindowClosed(object resume) {
            if (resume is bool && (bool)resume)
            {
                //NextTimer();
            }
        }

    }
}
