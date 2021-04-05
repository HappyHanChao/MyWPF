using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFTest.Common
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> _doExecute { get; set; }
        public Func<object, bool> _doCanExecute { get; set; }

        public CommandBase(Action<object> doExecute,Func<object,bool> doCanExecute)
        {
            _doExecute = doExecute;
            _doCanExecute = doCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _doCanExecute?.Invoke(parameter)==true;
        }

        public void Execute(object parameter)
        {
            _doExecute?.Invoke(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
