using System.ComponentModel;

namespace DemoWPF.ViewModel
{
    public class ListedPanel : INotifyPropertyChanged
    {
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

        private int height;
        public int Height
        {
            get { return height; }
            set
            {
                if (value < 1) { height = 1; } else { height = value; }
                OnPropertyChanged("Height");
            }
        }


        private int width;
        public int Width
        {
            get { return width; }
            set
            {
                if (value < 1) { width = 1; } else { width = value; }
                OnPropertyChanged("Width");
            }
        }

        private int qty;
        public int QTY
        {
            get { return qty; }
            set
            {
                qty = value;
                OnPropertyChanged("QTY");
            }
        }
    }
}
