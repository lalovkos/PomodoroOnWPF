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

        public MainWindow() {
            InitializeComponent();
            DataContext = _alarmViewModel;
            
        }

        //Only numbers allowed
        private static readonly Regex onlyNumbersRegex = new Regex("[^0-9]");
        private void TimerTB_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
            e.Handled = onlyNumbersRegex.IsMatch(e.Text);
        }

    }
}
