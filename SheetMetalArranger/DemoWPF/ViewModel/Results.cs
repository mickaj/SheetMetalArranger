using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWPF.ViewModel
{
    public class Results : INotifyPropertyChanged
    {
        private bool calculated;
        public bool Calculated
        {
            get { return calculated; }
            set
            {
                calculated = value;
                OnPropertyChanged("Calculated");
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

        private double bestPanel;
        public double BestPanel
        {
            get { return bestPanel; }
            set
            {
                bestPanel = value;
                OnPropertyChanged("BestPanel");
            }
        }

        private double worstPanel;
        public double WorstPanel
        {
            get { return worstPanel; }
            set
            {
                worstPanel = value;
                OnPropertyChanged("WorstPanel");
            }
        }

        private int totalPanels;
        public int TotalPanels
        {
            get { return totalPanels; }
            set
            {
                totalPanels = value;
                OnPropertyChanged("TotalPanels");
            }
        }

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

        private int itemsArranged;
        public int ItemsArranged
        {
            get { return itemsArranged; }
            set
            {
                itemsArranged = value;
                OnPropertyChanged("ItemsArranged");
            }
        }

        private int itemsLeft;
        public int ItemsLeft
        {
            get { return itemsLeft; }
            set
            {
                itemsLeft = value;
                OnPropertyChanged("ItemsLeft");
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
