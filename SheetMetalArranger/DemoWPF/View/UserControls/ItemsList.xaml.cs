using DemoWPF.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ObservableCollection<ListedItem>), typeof(ItemsList));

        public ObservableCollection<ListedItem> Source
        {
            get { return (ObservableCollection<ListedItem>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            Source.Clear();
        }



        public ICommand RandomSetCommand
        {
            get { return (ICommand)GetValue(RandomSetCommandProperty); }
            set { SetValue(RandomSetCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RandomSetCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RandomSetCommandProperty = DependencyProperty.Register("RandomSetCommand", typeof(ICommand), typeof(ItemsList));
    }
}
