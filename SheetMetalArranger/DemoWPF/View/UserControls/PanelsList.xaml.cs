using DemoWPF.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DemoWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for PanelsList.xaml
    /// </summary>
    public partial class PanelsList : UserControl
    {
        public PanelsList()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ObservableCollection<ListedPanel>), typeof(PanelsList));

        public ObservableCollection<ListedPanel> Source
        {
            get { return (ObservableCollection<ListedPanel>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty AllowNewProperty = DependencyProperty.Register("AllowNew", typeof(bool), typeof(PanelsList));

        public bool AllowNew
        {
            get { return (bool)GetValue(AllowNewProperty); }
            set { SetValue(AllowNewProperty, value); }
        }

        public static readonly DependencyProperty NewHeightProperty = DependencyProperty.Register("NewHeight", typeof(int), typeof(PanelsList), new PropertyMetadata(1500));

        public int NewHeight
        {
            get { return (int)GetValue(NewHeightProperty); }
            set { SetValue(NewHeightProperty, value); }
        }

        public static readonly DependencyProperty NewWidthProperty = DependencyProperty.Register("NewWidth", typeof(int), typeof(PanelsList), new PropertyMetadata(3000));

        public int NewWidth
        {
            get { return (int)GetValue(NewWidthProperty); }
            set { SetValue(NewWidthProperty, value); }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Source.Clear();
            AllowNew = true;
            NewHeight = 1500;
            NewWidth = 3000;
        }
    }
}
