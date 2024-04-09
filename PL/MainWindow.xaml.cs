using Microsoft.VisualBasic;
using PL.Engineer;
using System.Windows;


namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceWindow.xaml
    /// </summary>
    /// <parameter name="CurrentTime">: shows us the running hour </parameter>
    /// <parameter name="workerID">: in case we want to go to the worker window we need the workers id </parameter>
    /// <method name="MainWindow">: initialize the clock and every thing else </method>
    /// <method name="ButtonManager_Click">: in case we want to go the manager window </method>
    /// <method name="ButtonWorker_Click">: in case we want to go to the worker window </method>
    /// <method name="Button_ClickHour">: if we want to add a one hour to the time </method>
    /// <method name="Button_ClickDay">: if we want to add a one day to the time </method>
    /// <method name="Button_ClickYear">: if we want to add a one year to the time </method>
    /// <method name="TextBox_TextChanged">: finds the worker with this id </method>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public DateTime? CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

     
        public static readonly DependencyProperty CurrentTimeProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow));
        public MainWindow()
        {
            CurrentTime = s_bl.Clock;
            InitializeComponent();
           
            
        }

        private void ButtonManager_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void ButtonWorker_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int workerID = int.Parse(Interaction.InputBox("Enter you Id", "Hi", "0"));//entering the worker id
                if (workerID == 0)
                {
                    MessageBox.Show("Error, enter the worker's Id \n");
                }
                else
                {
                    new WorkerWindow(workerID).Show();
                }
            }
            catch (BO.BlDoesNotExistException) { MessageBox.Show("The worker's Id doesn't match any worker"); }
            catch (Exception ex)
            {
                MessageBox.Show(
                         ex.Message,
                         "Error",
                         MessageBoxButton.OK,
                         MessageBoxImage.Hand,
                         MessageBoxResult.Cancel);
            }
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
    }
}
