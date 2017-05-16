using System;
using System.Windows;
using System.Windows.Input;

namespace DemoWPF.ViewModel.Commands
{
    public class MenuCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            return;
        }
    }

    public class CloseCommand : MenuCommand
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }

    public class WWWCommand : MenuCommand
    {
        public override void Execute(object parameter)
        {
            System.Diagnostics.Process.Start("https://mkajzer.pl");
        }
    }

    public class AboutCommand : MenuCommand
    {
        public override void Execute(object parameter)
        {
            System.Diagnostics.Process.Start("https://github.com/mickaj/SheetMetalArranger");
        }
    }
}