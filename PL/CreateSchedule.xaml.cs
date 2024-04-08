using PL.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// a window for creating the schedule
    /// </summary>
    /// <parameter name="CreatingSchedule">  -1: before starting the schedule, 0: while building, 1: finished </parameter>
    /// <method name="CreateSchedule"> initialize everything</method>
    /// <method name="ButtonAddTask_Click">  if we want to add a task  </method>
    /// <method name="ButtonFinishCreating_Click"> : if the schedules building is finished</method>
    public partial class CreateSchedule : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        static int CreatingSchedule = -1;// -1: before starting, 0: while building, 1: finished

        static DateTime? CreatingDate=null;

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register(nameof(TaskList), typeof(IEnumerable<BO.Task>), typeof(CreateSchedule));
       
        public IEnumerable<BO.Task?>? TaskList
        {
            get => (IEnumerable<BO.Task>)GetValue(TaskListProperty);
            set => SetValue(TaskListProperty, value);
        }

        public CreateSchedule(int getCreatingSchedule=0)
        {
            InitializeComponent();
            CreatingSchedule = getCreatingSchedule;
            
        }
        private void ButtonAddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow(CreatingSchedule).Show();
        }

        private void ButtonFinishCreating_Click(object sender, RoutedEventArgs e)
        {
            CreatingSchedule=1;
            if (CreatingDate == null)
            {
                TaskList = s_bl?.Task.ReadAll()!;

            }
            else
            {
                MessageBox.Show("you must enter start project date to continue");
            }



            new ManagerWindow().Show();
        }
    }
}
