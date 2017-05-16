using System.ComponentModel;

namespace DemoWPF.ViewModel
{
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
