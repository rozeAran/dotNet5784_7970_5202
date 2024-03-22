using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceWindow.xaml
    /// </summary>
    public partial class EntranceWindow : Window
    {
        public DateTime CurrentTime { get; set; }= DateTime.Now;
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
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
            MessageBox.Show("Enter engineer Id:", "", MessageBoxButton.YesNo);//to fix
                
        }

    }
}
