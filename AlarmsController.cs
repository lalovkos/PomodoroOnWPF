using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace PomodoroOnWPF
{
    public class AlarmStats
    {
        public string Name { get; set; }
        public string Description;
        public TimeSpan FullWorkingTime;
        public TimeSpan PassedTime;
        public int PausesCount;

        public bool Equals(AlarmStats other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }
    }

    public class AlarmsController {

        private List<AlarmStats> Alarms = new List<AlarmStats>();
        private AlarmTimer _curAlarm = new AlarmTimer();

        AlarmsController() {

        }

        public void AddAlarm(AlarmStats alarm) {
            Alarms.Add(alarm);
        }

        public void DeleteAlarm(string name) {
            Alarms.Remove(new AlarmStats() {Name = name});
        }

        public void Start() {
            _curAlarm.Start();
        }

        public void Stop() {
            _curAlarm.Stop();

        }

        public void Reset() {
            _curAlarm.Reset();
        }

    }
}
