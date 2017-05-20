using ArrangerLibrary;
using ArrangerLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            //get folder location for saving drawings
            string systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string complete = systemPath + @"\ArrangerDrawings\"+ DateTime.Now.Ticks;
            //MessageBox.Show(complete);
            if(!Directory.Exists(complete))
            {
                Directory.CreateDirectory(complete);
            }
            //create instance of image drawer
            ImageDrawer drawer = new ImageDrawer();
            //clear tabs
            vm.Tabs.Clear();
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
            calc.Calculate(ItemAreaComparer.Instance, ItemHeightComparer.Instance, ItemWidthComparer.Instance, HSector.Instance);
            //assign results to view model
            IArrangement bestArr = calc.GetBestArrangement();
            panels = bestArr.GetPanels();
            vm.Calculation.Utilisation = bestArr.Utilisation;
            vm.Calculation.TotalPanels = bestArr.GetPanels().Count;
            vm.Calculation.ItemsLeft = bestArr.GetLeftItems().Count;
            int i = 0;
            foreach(IPanel panel in bestArr.GetPanels())
            {
                ResultsTab tab = new ResultsTab();
                tab.Count = i;
                tab.Height = panel.Height;
                tab.Width = panel.Width;
                tab.Utilisation = panel.Utilisation;
                //get panel drawing
                string filename = complete + @"\"+i + ".png";
                drawer.Draw(panel).Save(filename);
                //MessageBox.Show(filename);
                Uri uriFilepath = new System.Uri(filename);
                //MessageBox.Show(uriFilepath.ToString());
                tab.Drawing = new BitmapImage(uriFilepath);
                vm.Tabs.Add(tab);
                i++;
            }
            vm.Calculation.BestPanel = bestArr.GetBestPanel().Utilisation;
            vm.Calculation.WorstPanel = bestArr.GetWorstPanel().Utilisation;
            vm.Calculation.TotalItems = bestArr.TotalItemsArea;
            vm.Calculation.TotalPanels = bestArr.TotalPanelsArea;
            vm.Calculation.ItemsArranged = bestArr.ItemsArranged;
            vm.Calculation.Calculated = true;
        }
    }
}
