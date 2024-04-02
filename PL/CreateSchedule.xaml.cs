using PL.Task;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CreateSchedule.xaml
    /// </summary>
    public partial class CreateSchedule : Window
    {
        static int CreatingSchedule = -1;// -1: before starting, 0: while building, 1: finished
        public CreateSchedule(int getCreatingSchedule=-1)
        {
            CreatingSchedule = getCreatingSchedule;
            InitializeComponent();
        }
        private void ButtonAddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow(CreatingSchedule).Show();
        }

        private void ButtonFinishCreating_Click(object sender, RoutedEventArgs e)
        {
            CreatingSchedule=1;
        }
    }
}
