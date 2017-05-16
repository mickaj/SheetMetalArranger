using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DemoWPF.ViewModel
{
    public class ResultsTab : INotifyPropertyChanged
    {
        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public string Name
        {
            get
            {
                return String.Format("{0}: {1}x{2}", count, height, width);
            }
        }

        private int height;
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        private double utilisation;
        public double Utilisation
        {
            get { return utilisation; }
            set
            {
                utilisation = value;
                OnPropertyChanged("Utilisation");
            }
        }

        private BitmapImage drawing;
        public BitmapImage Drawing
        {
            get { return drawing; }
            set
            {
                drawing = value;
                OnPropertyChanged("Drawing");
            }
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
    }
}
