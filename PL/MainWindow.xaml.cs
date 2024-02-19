using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Engineer;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonEngineer_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().Show();
        }

        private void ButtonInitialization_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to initialize?", "initialization", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                BlApi.IBl.s_bl.InitializeDB();

        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            BlApi.IBl.s_bl.ResetDB();
        }
    }
}