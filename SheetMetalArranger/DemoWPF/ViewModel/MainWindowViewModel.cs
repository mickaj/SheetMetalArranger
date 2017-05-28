using System.ComponentModel;
using System.Windows.Input;
using DemoWPF.ViewModel.Commands;
using System.Collections.ObjectModel;
using DemoWPF.View;
using System.Windows;
using System;

namespace DemoWPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<ListedItem>();
            Panels = new ObservableCollection<ListedPanel>();
            Tabs = new ObservableCollection<ResultsTab>();
            calculation = new Results();
        }

        #region Items
        public ObservableCollection<ListedItem> Items { get; set; }
        #endregion

        #region Panels
        public ObservableCollection<ListedPanel> Panels { get; set; }

        internal ProgressWindowViewModel ProgresWindowViewModel;

        private int newHeight = 1500;
        public int NewHeight
        {
            get { return newHeight; }
            set
            {
                if (value < 1) { newHeight = 1; } else { newHeight = value; }
                OnPropertyChanged("NewHeight");
            }
        }

        private int newWidth = 3000;
        public int NewWidth
        {
            get { return newWidth; }
            set
            {
                if (value < 1) { newWidth = 1; } else { newWidth = value; }
                OnPropertyChanged("NewWidth");
            }
        }

        private bool allowNew;
        public bool AllowNew
        {
            get { return allowNew; }
            set
            {
                allowNew = value;
                OnPropertyChanged("AllowNew");
            }
        }
        #endregion

        #region Results
        private Results calculation;
        public Results Calculation
        {
            get { return calculation; }
            set
            {
                calculation = value;
                OnPropertyChanged("Calculation");
            }
        }

        public ObservableCollection<ResultsTab> Tabs { get; set; }
        #endregion

        internal ProgressWindow ProgressWindow;

        public void SetProgressViewModel()
        {
            ProgresWindowViewModel = new ProgressWindowViewModel(this);
            ProgresWindowViewModel.Finished = false;
            ProgresWindowViewModel.ProcessedItems = 0;
            ProgresWindowViewModel.TotalItems = Items.Count;
            ProgressWindow = new ProgressWindow();
            ProgressWindow.DataContext = ProgresWindowViewModel;
            ProgresWindowViewModel.CloseAction = new Action(ProgressWindow.Close);
            ProgressWindow.Owner = Application.Current.MainWindow;
            ProgressWindow.Show();
        }


        
        //CloseCommand definition
        private ICommand closeCommand;

        public ICommand Close
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new CloseCommand();
                }
                return closeCommand;
            }
        }
        //End of CloseCommand definition

        //WWWCommand definition
        private ICommand wwwCommand;

        public ICommand WWW
        {
            get
            {
                if (wwwCommand == null)
                {
                    wwwCommand = new WWWCommand();
                }
                return wwwCommand;
            }
        }
        //End of WWWCommand definition

        //AboutCommand definition
        private ICommand aboutCommand;

        public ICommand About
        {
            get
            {
                if (aboutCommand == null)
                {
                    aboutCommand = new AboutCommand();
                }
                return aboutCommand;
            }
        }
        //End of AboutCommand definition

        //CalculateCommand definition
        private ICommand calculateCommand;
        public ICommand CalculateCommand
        {
            get
            {
                if (calculateCommand == null) { calculateCommand = new CalculateCommand(this); }
                return calculateCommand;
            }
        }
        //End of CalculateCommand definition

        //ResetResultsCommand definition
        private ICommand resetResultsCommand;
        public ICommand ResetResultsCommand
        {
            get
            {
                if (resetResultsCommand == null) { resetResultsCommand = new ResetResultsCommand(this); }
                return resetResultsCommand;
            }
        }
        //End of ResetResultsCommand definition

        //RandomSet command definition
        private ICommand randomSetCommand;
        public ICommand RandomSetCommand
        {
            get
            {
                if (randomSetCommand == null) { randomSetCommand = new RandomSetCommand(this); }
                return randomSetCommand;
            }
        }
        //End of RandomSet command definition

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

        //addCommand definition
        private ICommand addCommand;

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new AddItemCommand(this);
                }
                return addCommand;
            }
        }
        //End of addCommand definition

        //displayCommand definition
        private ICommand displayCommand;

        public ICommand DisplayCommand
        {
            get
            {
                if (displayCommand == null)
                {
                   displayCommand = new DisplayItemsCommand(this);
                }
                return displayCommand;
            }
        }
        //End of displayCommand definition
    }
}
