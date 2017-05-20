using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoWPF.ViewModel.Commands
{
    public class ResetResultsCommand : ICommand
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

        public ResetResultsCommand(MainWindowViewModel _vm)
        {
            if (_vm == null)
            {
                throw new ArgumentNullException("no viewModel defined");
            }
            this.vm = _vm;
        }

        public bool CanExecute(object parameter)
        {
            if (vm.Calculation.Calculated) { return true; }
            return false;
        }

        public void Execute(object parameter)
        {
            vm.Calculation.Calculated = false;
            vm.Calculation.BestPanel = 0;
            vm.Calculation.ItemsArranged = 0;
            vm.Calculation.ItemsLeft = 0;
            vm.Calculation.TotalItems = 0;
            vm.Calculation.TotalPanels = 0;
            vm.Calculation.Utilisation = 0;
            vm.Calculation.WorstPanel = 0;
            vm.Tabs.Clear();
        }
    }
}
