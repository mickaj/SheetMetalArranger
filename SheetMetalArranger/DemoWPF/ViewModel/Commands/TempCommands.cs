using System;
using System.Windows;
using System.Windows.Input;

namespace DemoWPF.ViewModel.Commands
{
    public class AddItemCommand : ICommand
    {
        private readonly MainWindowViewModel vm;
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

        public AddItemCommand(MainWindowViewModel _vm)
        {
            if (_vm == null)
            {
                throw new ArgumentNullException("no viewModel defined");
            }
            this.vm = _vm;
        }

        public void Execute(object parameter)
        {
            //MessageBox.Show("adding being executed");
            vm.Items.Add(new ListedItem { Height = 99, Width = 99, Margin = 9, Rotation = true });
        }
    }

    public class DisplayItemsCommand : ICommand
    {
        private readonly MainWindowViewModel vm;
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

        public DisplayItemsCommand(MainWindowViewModel _vm)
        {
            if (_vm == null)
            {
                throw new ArgumentNullException("no viewModel defined");
            }
            this.vm = _vm;
        }

        public void Execute(object parameter)
        {
            string txt = "Items in collection:\n";
            foreach (ListedItem li in vm.Items)
            {
                txt += String.Format("H={0}; W={1}\n", li.Height, li.Width);
            }
            MessageBox.Show(txt);
        }
    }
}
