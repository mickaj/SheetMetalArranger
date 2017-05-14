using System.ComponentModel;
using System.Windows.Input;
using DemoWPF.ViewModelCommands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace DemoWPF.ViewModel
{
    public class MainWindowViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<ListedItem> Items { get; set; }
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<ListedItem>();
            Items.Add(new ListedItem { Height = 20, Width = 30, Margin = 1, Rotation = true });
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

    public class ListedItem : INotifyPropertyChanged
    {
        private int height;
        public int Height
        {
            get { return height; }
            set
            {
                if(value<1) { height = 1; } else { height = value; }  
                OnPropertyChanged("Area", "Height");
            }
        }


        private int width;
        public int Width
        {
            get { return width; }
            set
            {
                if (value < 1) { width = 1; } else { width = value; }
                OnPropertyChanged("Area", "Width");
            }
        }

        public int Margin { get; set; }
        public bool Rotation { get; set; }
        public int Area { get { return Height * Width; } }

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

        public ListedItem()
        {
            Height = 1;
            Width = 1;
            Margin = 0;
            Rotation = false;
        }
    }
}
