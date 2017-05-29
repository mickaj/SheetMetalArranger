using ArrangerLibrary;
using ArrangerLibrary.Abstractions;
using DemoWPF.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DemoWPF.ViewModel.Commands
{
    public class CalculateCommand : ICommand
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
            if(vm.Items.Count == 0) { return false; }
            int panelsCount = vm.Panels.Sum<ListedPanel>(x => x.QTY);
            if((panelsCount < 1)&&(!vm.AllowNew)) { return false; }
            return true;
        }

        public CalculateCommand(MainWindowViewModel _vm)
        {
            if (_vm == null)
            {
                throw new ArgumentNullException("no viewModel defined");
            }
            this.vm = _vm;
        }

        public void Execute(object parameter)
        {
            ICalculation thisCalc = PrepareCalculation();
            vm.SetProgressViewModel();
            //creating new task for the Process method to keep the UI responsive
            Task processTask = new Task(() => thisCalc.Calculate(ItemAreaComparer.Instance, ItemHeightComparer.Instance, ItemWidthComparer.Instance, thisCalc.DefaultFactory.HSector, vm.ProgresWindowViewModel.UpdateProgres));
            processTask.Start();
            Task continueTask = processTask.ContinueWith((a) =>
            {
                vm.ProgressWindow.Dispatcher.Invoke(() =>
                {
                    vm.ProgresWindowViewModel.CalculationResults = thisCalc;
                    vm.ProgresWindowViewModel.Finished = true;
                    vm.ProgressWindow.Hide();
                    vm.ProgressWindow.Show();

                });
            });
        }


        private ICalculation PrepareCalculation()
        {
            //geterate items list
            List<IItem> items = new List<IItem>();
            foreach(ListedItem li in vm.Items)
            {
                items.Add(new Item(li.Height, li.Width, li.Margin, li.Rotation));
            }
            //generate ArrangerLibrary.Batch
            IBatch batch = new Batch(items);
            //generate panels
            List<IPanel> panels = new List<IPanel>();
            if(vm.Panels.Count == 0) { panels.Add(new Panel(vm.NewHeight, vm.NewWidth)); }
            else
            {
                foreach (ListedPanel lp in vm.Panels)
                {
                    for (int qty = 1; qty<= lp.QTY; qty++) { panels.Add(new Panel(lp.Height, lp.Width)); }
                }
            }
            //create calculation
            ICalculation calc;
            if (vm.AllowNew) { calc = new Calculation(batch, panels, vm.NewHeight, vm.NewWidth); }
            else { calc = new Calculation(batch, panels); }
            return calc;
        }
    }
}
