using DemoWPF.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for ItemsList.xaml
    /// </summary>
    public partial class ItemsList : UserControl
    {
        public ItemsList()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source",
                                                                                              typeof(ObservableCollection<ListedItem>),
                                                                                              typeof(ItemsList));
        public ObservableCollection<ListedItem> Source
        {
            get { return (ObservableCollection<ListedItem>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Source.Clear();
        }
    }
}
