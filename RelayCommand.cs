using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PomodoroOnWPF {
    class RelayCommand : ICommand {

        private Action action;
        public RelayCommand(Action action) => this.action = action;
        public bool CanExecute(object parameter) => true;
        #pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
        #pragma warning restore CS0067
        public void Execute(object parameter) => action();
    }
}
