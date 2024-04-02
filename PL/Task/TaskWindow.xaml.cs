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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static int CreatingSchedule=-1;
        public TaskWindow(int getCreatingSchedule=-1)
        {
            CreatingSchedule=getCreatingSchedule;
            InitializeComponent();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e)
        {
            new TaskForListWindow().ShowDialog();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (CreatingSchedule == 0)
            {
                //fix
            }
            //if not finished bulidlig schedule then add task
            }

        //info
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
    }
}
