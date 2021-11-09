using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PomodoroOnWPF
{
    /// <summary>
    /// Interaction logic for AlarmWindow.xaml
    /// </summary>
    public partial class AlarmWindow : Window {
        
        private bool resume = false;
        private Action<object> _on_close_func;

        public AlarmWindow(string label = null, string Bt_label = null, Brush bt_brush = null, Action<object> _onCloseFunc = null) {
            InitializeComponent();

            if (label != null)
            {
                this.Title = label;
                this.MainLabelLbl.Content = label;
            }

            if (bt_brush != null)
            {
                this.NextBt.Background = bt_brush;
            }

            _on_close_func = _onCloseFunc;
        }

        private void AlarmWindow_OnClosed(object? sender, EventArgs e) {
            if (_on_close_func != null)
            {
                _on_close_func(resume);
            }
        }

        private void NextBt_OnClick(object sender, RoutedEventArgs e) {
            resume = true;
            this.Close();
        }
    }
}
