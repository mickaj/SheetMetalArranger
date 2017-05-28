using ArrangerLibrary;
using ArrangerLibrary.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace DemoWPF.ViewModel.Commands
{
    public class SeeResultsCommand : ICommand
    {
        private ProgressWindowViewModel vm;
        private MainWindowViewModel mainVM;
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
            return vm.Finished;
        }

        public void Execute(object parameter)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            vm.CloseAction();
            PropagateResults(vm.CalculationResults);
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        public SeeResultsCommand(ProgressWindowViewModel _vm, MainWindowViewModel _mvm)
        {
            if ((_vm == null)||(_mvm==null))
            {
                throw new ArgumentNullException("no viewModel defined");
            }
            this.vm = _vm;
            mainVM = _mvm;
        }

        private void PropagateResults(ICalculation calc)
        {
            //get folder location for saving drawings
            string systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string complete = systemPath + @"\ArrangerDrawings\" + DateTime.Now.Ticks;
            //MessageBox.Show(complete);
            if (!Directory.Exists(complete))
            {
                Directory.CreateDirectory(complete);
            }
            //create instance of image drawer
            ImageDrawer drawer = new ImageDrawer();
            //clear tabs
            mainVM.Tabs.Clear();
            //assign results to view model
            IArrangement bestArr = calc.GetBestArrangement();
            List<IPanel> panels = bestArr.GetPanels();
            mainVM.Calculation.Utilisation = bestArr.Utilisation;
            mainVM.Calculation.TotalPanels = bestArr.GetPanels().Count;
            mainVM.Calculation.ItemsLeft = bestArr.GetLeftItems().Count;
            int i = 0;
            foreach (IPanel panel in bestArr.GetPanels())
            {
                ResultsTab tab = new ResultsTab();
                tab.Count = i;
                tab.Height = panel.Height;
                tab.Width = panel.Width;
                tab.Utilisation = panel.Utilisation;
                //get panel drawing
                string filename = complete + @"\" + i + ".png";
                drawer.Draw(panel).Save(filename);
                //MessageBox.Show(filename);
                Uri uriFilepath = new System.Uri(filename);
                //MessageBox.Show(uriFilepath.ToString());
                tab.Drawing = new BitmapImage(uriFilepath);
                mainVM.Tabs.Add(tab);
                i++;
            }
            mainVM.Calculation.BestPanel = bestArr.GetBestPanel().Utilisation;
            mainVM.Calculation.WorstPanel = bestArr.GetWorstPanel().Utilisation;
            mainVM.Calculation.TotalItems = bestArr.TotalItemsArea;
            mainVM.Calculation.TotalPanels = bestArr.TotalPanelsArea;
            mainVM.Calculation.ItemsArranged = bestArr.ItemsArranged;
            mainVM.Calculation.Calculated = true;
        }
    }
}
