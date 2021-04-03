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
        public Action<object> doExecute { get; set; }
        public Func<object, bool> doCanExecute { get; set; }

        public CommandBase(Action<object> _doExecute,Func<object,bool> _doCanExecute)
        {
            doExecute = _doExecute;
            doCanExecute = _doCanExecute;
        }

        public bool CanExecute(object parameter)
        {
            return doCanExecute?.Invoke(parameter)==true;
        }

        public void Execute(object parameter)
        {
            doExecute?.Invoke(parameter);
        }      
    }
}
