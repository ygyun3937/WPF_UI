using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WPF_UI.ViewModel
{
    public class DelegateCommand :ICommand
    {
        private readonly Func<bool> canExecute;
        private readonly Action execute;

        public DelegateCommand(Action execute):this(execute,null)
        {

        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        /// <summary>
        /// can Executes event Handler
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// implement of icommand can execute method
        /// </summary>
        /// <param name="o">parameter by default of icommand interface</param>
        /// <returns>can execute or not</returns>
        public bool CanExecute(object o)
        {
            if(this.canExecute == null)
            {
                return true;
            }
            return this.canExecute();
        }

        /// <summary>
        /// implement of icommand interface execute method
        /// </summary>
        /// <param name="o">parameter by default of icommand interface</param>
        public void Execute(object parameter)
        {
            this.execute();
        }
        public void RaiseCanExecuteChanged()
        {
            if(this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }



    }
}
