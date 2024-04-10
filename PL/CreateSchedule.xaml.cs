using Microsoft.VisualBasic;
using PL.Task;
using System.Windows;
using System.Windows.Controls;


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

        static DateTime? StartProjectDate=null;

        public CreateSchedule()
        {
            InitializeComponent();
            
        }
        private void ButtonAddTask_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().Show();
        }

        private void ButtonFinishCreating_Click(object sender, RoutedEventArgs e)
        {
            if (StartProjectDate != null)
            {
                s_bl.Task.AddScheduledDates();
            }
            else
            {
                MessageBox.Show("You must enter start project date to continue");
            }

        }


        private void Button_Click_Start_Project(object sender, RoutedEventArgs e)
        {
            if(StartProjectDate.HasValue) 
            {
                MessageBox.Show("ERROR, The project already has a start date");
            }
            StartProjectDate = DateTime.Parse(Interaction.InputBox("Enter start project date", "Hi", ""));//entering the start project date
            s_bl.StartProjectDate=StartProjectDate;
        }

        private void Button_Click_End_Project(object sender, RoutedEventArgs e)
        {
            s_bl.EndProjectDate = DateTime.Now;
        }
    }
}
