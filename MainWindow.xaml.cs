using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Serialization;
using Timer = System.Timers.Timer;

namespace PomodoroOnWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        enum TimerState {
            Stopped,
            WorkTime,
            RelaxTime
        }

        private static readonly SolidColorBrush StartButtonBrush = new SolidColorBrush(Color.FromRgb(34, 139, 34));
        private static readonly SolidColorBrush StopButtonBrush  = new SolidColorBrush(Color.FromRgb(128, 0, 0));
       

        private TimerState _curState = TimerState.Stopped;
        private Window _curAlarmWindow = null;
        private System.Windows.Threading.DispatcherTimer _curTimer;

        public MainWindow()
        {
            InitializeComponent();
            SetStartButton("Start", StartButtonBrush);
            _curTimer = new System.Windows.Threading.DispatcherTimer();
            _curTimer.Tick += new EventHandler(TimerClick);
        }

        private void TimerClick(object sender, EventArgs e) {
            this.TimerTB.Text = _curTimer.Interval.ToString();
        }

        private void StartStopBt_Click(object sender, RoutedEventArgs e)
        {
            if (_curState == TimerState.Stopped) 
            {
                _curState = TimerState.WorkTime;
                SetStartButton("Start", StartButtonBrush);
                _curTimer.Interval = new TimeSpan(0,10, 0);
                _curTimer.Start();

            }
            else 
            {
                _curState = TimerState.Stopped;
                SetStartButton("Stop", StopButtonBrush);
                _curTimer.Interval = new TimeSpan(0, 30, 0);
                _curTimer.Start();
            }
        }
        private void PauseResumeBt_Click(object sender, RoutedEventArgs e)
        {

            if (_curTimer.IsEnabled)
            {
                _curTimer.Stop();
                this.PauseResumeBt.Content = "Resume";
            }
            else
            {
                _curTimer.Start();
                this.PauseResumeBt.Content = "Pause";
            }


        }
       
        private void SetStartButton(string content, Brush brush) {
            this.StartStopBt.Content = content;
            this.StartStopBt.Background = brush;
        }

        private void ShowAlarm() {

            string alarm_window_title = "";
            string alarm_button_title = "";

            if (_curState == TimerState.RelaxTime) {
                alarm_window_title = "Time to relax!";
                alarm_button_title = "Start relaxing!";

            }
            else if (_curState == TimerState.WorkTime) {
                alarm_window_title = "Time to work!";
                alarm_button_title = "Start working!";
            }

            _curAlarmWindow = new AlarmWindow("Start", "Next stage", null, AlarmWindowClosed);
            _curAlarmWindow.Activate();
            _curAlarmWindow.Show();

        }

        private void AlarmWindowClosed(object resume){
            if (resume is bool) {
                NextStage();
            }
        }

        private void NextStage() {
            
            
            if (_curState == TimerState.WorkTime) {
                _curState = TimerState.RelaxTime;
            }
            else if(_curState == TimerState.RelaxTime) {
                _curState = TimerState.WorkTime;
            }

            _curTimer = new System.Windows.Threading.DispatcherTimer();

        }

        private static readonly Regex onlyNumbersRegex = new Regex("[^0-9]");
        private void TimerTB_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersRegex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowAlarm();
        }

    }
}
