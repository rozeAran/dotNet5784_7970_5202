using PL.Engineer;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceWindow.xaml
    /// </summary>
    /// <parameter name="CurrentTime">: shows us the running hour </parameter>
    /// <parameter name="workerID">: in case we want to go to the worker window we need the workers id </parameter>
    /// <method name="MainWindow">: initilize the clock and every thing else </method>
    /// <method name="ButtonManager_Click">: in case we want to go the manager window </method>
    /// <method name="ButtonWorker_Click">: in case we want to go to the worker window </method>
    /// <method name="Button_ClickHour">: if we want to add a one hour to the time </method>
    /// <method name="Button_ClickDay">: if we want to add a one day to the time </method>
    /// <method name="Button_ClickYear">: if we want to add a one year to the time </method>
    /// <method name="TextBox_TextChanged">: findes the worker with this id </method>
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
        int workerID = 0;
       
        public MainWindow()
        {
            CurrentTime = s_bl.currentClock();
            InitializeComponent();
        }

        private void ButtonManager_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void ButtonWorker_Click(object sender, RoutedEventArgs e)
        {
            new WorkerWindow(workerID).Show();

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
            try
            {
                BO.Engineer eng = s_bl?.Engineer.Read(workerID)!;
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }


        }
    }
}
