using ArrangerLibrary.Abstractions;
using DemoWPF.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoWPF.ViewModel
{
    public class ProgressWindowViewModel : INotifyPropertyChanged
    {
        private MainWindowViewModel parent;

        private ICalculation calc;
        public ICalculation CalculationResults
        {
            get { return calc; }
            set { calc = value; }
        }

        public Action CloseAction { get; internal set; }

        private int totalItems;
        public int TotalItems
        {
            get { return totalItems; }
            set
            {
                totalItems = value;
                OnPropertyChanged("TotalItems");
            }
        }

        private int processedItems;
        public int ProcessedItems
        {
            get { return processedItems; }
            set
            {
                processedItems = value;
                OnPropertyChanged("ProcessedItems");
            }
        }

        private bool finished;
        public bool Finished
        {
            get { return finished; }
            set
            {
                finished = value;
                OnPropertyChanged("Finished");
            }
        }

        public ProgressWindowViewModel(MainWindowViewModel _parent)
        {
            parent = _parent;
        }

        public void UpdateProgres(int i)
        {
            ProcessedItems = i;
        }

        //implementation of INotifyPropertyChanged - BEGINS
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(params string[] propertiesChanged)
        {
            if (PropertyChanged != null)
            {
                foreach (string property in propertiesChanged)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }
        //implementation of INotifyPropertyChanged - ENDS

        private ICommand seeResultsCommand;
        public ICommand SeeResults
        {
            get
            {
                if (seeResultsCommand == null)
                {
                    seeResultsCommand = new SeeResultsCommand(this, parent);
                }
                return seeResultsCommand;
            }
        }

    }
}
