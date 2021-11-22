using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PomodoroOnWPF
{
    class UserInteraction {

        private static readonly UserInteraction instance = new UserInteraction();

        public static UserInteraction getInstance() {
            return instance;
        }

        public void createAlarmWindow(string name, string decripton, string bt_text, Brush bt_brush = null, Action<object> action_on_close = null) {
            
            Brush button_back_brush = (bt_brush == null) ? new SolidColorBrush() : bt_brush;

            AlarmWindow new_alarm_window = new AlarmWindow(name, "Next timer", button_back_brush, action_on_close);
            new_alarm_window.Activate();
            new_alarm_window.Show();
        }

    }
}
