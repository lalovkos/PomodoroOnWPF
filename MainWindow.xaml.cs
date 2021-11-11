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
using System.Windows.Threading;
using System.Windows.Xps.Serialization;
using Timer = System.Timers.Timer;

namespace PomodoroOnWPF
{
    
    public partial class MainWindow : Window
    {

        private static readonly SolidColorBrush StartButtonBrush = new SolidColorBrush(Color.FromRgb(34, 139, 34));
        private static readonly SolidColorBrush StopButtonBrush  = new SolidColorBrush(Color.FromRgb(128, 0, 0));
        private static AlarmViewModel _controller;
        private AlarmViewModel _alarmViewModel { get; } = new AlarmViewModel();

        public MainWindow()
        {
            InitializeComponent();
            StartDisplayTimer();
            DataContext = _alarmViewModel;
            
        }

        private void StartDisplayTimer() {
            //Setting Display timer update
            DispatcherTimer _displayTimer = new DispatcherTimer();
            _displayTimer.Tick += new EventHandler(DisplayTimerReload);
            _displayTimer.Interval = new TimeSpan(0, 0, 1);
            _displayTimer.Start();
        }

        private void DisplayTimerReload(object sender, EventArgs e) {
            //Reload clocks on screen
            this.TimerTB.Text = DateTime.Now.ToLongTimeString();

        }

        private void StartStopBt_Click(object sender, RoutedEventArgs e)
        {
        }
        
        private void SetStartButton(string content, Brush brush) {
            this.StartStopBt.Content = content;
            this.StartStopBt.Background = brush;
        }

        //Only numbers allowed
        private static readonly Regex onlyNumbersRegex = new Regex("[^0-9]");
        private void TimerTB_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = onlyNumbersRegex.IsMatch(e.Text);
        }

    }
}
