using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DemoWPF.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace DemoWPF.View.UserControls
{
    /// <summary>
    /// Interaction logic for ResultsControl.xaml
    /// </summary>
    public partial class ResultsControl : UserControl
    {
        public ResultsControl()
        {
            InitializeComponent();
        }

        public Results Results
        {
            get { return (Results)GetValue(ResultsProperty); }
            set { SetValue(ResultsProperty, value); }
        }
        public static readonly DependencyProperty ResultsProperty =
            DependencyProperty.Register("Results", typeof(Results), typeof(ResultsControl));



        public ObservableCollection<ResultsTab> ResultsTabs
        {
            get { return (ObservableCollection<ResultsTab>)GetValue(ResultsTabsProperty); }
            set { SetValue(ResultsTabsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ResultsTabs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultsTabsProperty =
            DependencyProperty.Register("ResultsTabs", typeof(ObservableCollection<ResultsTab>), typeof(ResultsControl));



        public ICommand CalculateCommand
        {
            get { return (ICommand)GetValue(CalculateCommandProperty); }
            set { SetValue(CalculateCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalculateCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalculateCommandProperty =
            DependencyProperty.Register("CalculateCommand", typeof(ICommand), typeof(ResultsControl));



        public ICommand ResetCommand
        {
            get { return (ICommand)GetValue(ResetCommandProperty); }
            set { SetValue(ResetCommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ResetCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResetCommandProperty =
            DependencyProperty.Register("ResetCommand", typeof(ICommand), typeof(ResultsControl))   ;
    }
}
