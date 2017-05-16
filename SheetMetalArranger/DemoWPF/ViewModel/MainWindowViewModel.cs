using System.ComponentModel;
using System.Windows.Input;
using DemoWPF.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Drawing;
using System;

namespace DemoWPF.ViewModel
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<ListedItem>();
            Panels = new ObservableCollection<ListedPanel>();
            Tabs = new ObservableCollection<ResultsTab>();
            BitmapImage pic = new BitmapImage(new Uri("file://d:/test.png"));
            Tabs.Add(new ResultsTab { Count=0, Height = 1500, Width=3000, Utilisation=0.80, Drawing=pic });
            Tabs.Add(new ResultsTab { Count = 1, Height = 1250, Width = 2500, Utilisation = 0.66, Drawing = pic });
            Calculation = new Results();
            Calculation.Calculated = true;
            Calculation.Utilisation = 0.34;
            Calculation.BestPanel = 0.99;
            Calculation.WorstPanel = 0.0001;
            Calculation.TotalPanels = 100;
            Calculation.TotalItems = 1999;
            Calculation.ItemsArranged = 996;
            Calculation.ItemsLeft = 9;
        }

        #region Items
        public ObservableCollection<ListedItem> Items { get; set; }
        #endregion

        #region Panels
        public ObservableCollection<ListedPanel> Panels { get; set; }

        private int newHeight=1500;
        public int NewHeight
        {
            get { return newHeight; }
            set
            {
                    if (value < 1) { newHeight = 1; } else { newHeight = value; }
                    OnPropertyChanged("NewHeight");
            }
        }

        private int newWidth=3000;
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
