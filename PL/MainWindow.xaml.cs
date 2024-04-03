using PL.Engineer;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register(nameof(CurrentTime), typeof(IEnumerable<DateTime>), typeof(MainWindow));
        public IEnumerable<DateTime>? CurrentTime
        {
            get => (IEnumerable<DateTime>)GetValue(CurrentTimeProperty);
            set => SetValue(CurrentTimeProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonManager_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void ButtonWorker_Click(object sender, RoutedEventArgs e)
        {
            //fix

        }

        private void Button_ClickHour(object sender, RoutedEventArgs e)
        {
            CurrentTime = s_bl?.AddHourClock();
        }
        private void Button_ClickDay(object sender, RoutedEventArgs e)
        {
            CurrentTime = s_bl?.AddDayClock();
        }
        private void Button_ClickYear(object sender, RoutedEventArgs e)
        {
            CurrentTime = s_bl?.AddYearClock();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //fix
        }
    }
}
