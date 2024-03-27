using PL.Engineer;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceWindow.xaml
    /// </summary>
    public partial class EntranceWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public readonly DependencyProperty CurrentTimeProperty= 
            DependencyProperty.Register(nameof(CurrentTime), typeof(IEnumerable<DateTime>), typeof(EntranceWindow));
        public IEnumerable<DateTime>? CurrentTime
        {
            get => (IEnumerable<DateTime>)GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }

        public EntranceWindow()
        {
            InitializeComponent();
        }

    private void ButtonManager_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
    }

    private void ButtonWorker_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Enter engineer Id:");//to fix

    }

        private void Button_ClickHour(object sender, RoutedEventArgs e)
        {

        }
        private void Button_ClickDay(object sender, RoutedEventArgs e)
        {

        }
        private void Button_ClickYear(object sender, RoutedEventArgs e)
        {
            CurrentTime = s_bl.Clock.AddYearClock();
        }
    }
}
