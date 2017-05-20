using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoWPF.ViewModel.Commands
{
    public class RandomSetCommand : ICommand
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

        public RandomSetCommand(MainWindowViewModel _vm)
        {
            if (_vm == null)
            {
                throw new ArgumentNullException("no viewModel defined");
            }
            this.vm = _vm;
        }

        public void Execute(object parameter)
        {
            Random randGen = new Random(DateTime.Now.GetHashCode());
            Random randBool = new Random(DateTime.Now.GetHashCode());
                for (int i = 1; i <= 10; i++)
                {
                    int h = randGen.Next(1,1500);
                    int w = randGen.Next(1,3000);
                    int m = randGen.Next(0, 20);
                    bool rot = false;
                    if (randBool.Next(1, 1000) > 500) { rot = true; }
                    vm.Items.Add(new ListedItem { Height = h, Width = w, Margin = m, Rotation = rot });
                }
        }
    }
}
