using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for TaskForListWindow.xaml
    /// </summary>
    /// <method name="TaskForListWindow"> Initialize all Components and reads all the taskes to TaskList</method>
    /// <method name="ComboBoxLevel_SelectionChanged"> shows in the list only the tasks in a specific level</method>
    /// <method name="ComboBoxStatus_SelectionChanged"> shows in the list only the tasks in a specific status </method>
    /// <method name="ListView_OpenTaskWindow"> : if the user wants to update a task it opens the task window</method>
    /// <method name="Button_Click_Add_Task"> if the user wants to add a task it opens the task window</method>

    public partial class TaskForListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status Status { get; set; } = BO.Status.Status;
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.Level;

        bool flagWorker=false;//if we got to this window from a worker or the manager

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register(nameof(TaskList), typeof(IEnumerable<BO.Task>), typeof(TaskForListWindow));
        public IEnumerable<BO.Task?>? TaskList
        {
            get => (IEnumerable<BO.Task>)GetValue(TaskListProperty);
            set => SetValue(TaskListProperty, value);
        }
       
        public TaskForListWindow( int engineerID=0)
        {
            InitializeComponent();
            try
            {
                if (engineerID ==0)//we came from manager window
                {
                    TaskList = s_bl?.Task.ReadAll()!;
                }
                else//we came from worker window
                {
                    flagWorker = true;
                    TaskList = s_bl?.Task.ReadAllTasksEngineerCanBeAssigned(engineerID);
                }
            }
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

        private void ComboBoxLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)//to choose the level of the engineer
        {
            try
            {
                TaskList = (Experience != BO.EngineerExperience.Level) ?
                    s_bl?.Task.ReadAll(item => item.Complexity == Experience) : s_bl?.Task.ReadAll();
            }
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

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)//to choose the status of the task
        {
            try
            {
                TaskList = (Status != BO.Status.Status) ?
                     s_bl?.Task.ReadAll(item => item.TaskStatus == Status) : s_bl?.Task.ReadAll();
            }
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

        private void ListView_OpenTaskWindow(object sender, RoutedEventArgs e)//in case i want to update a task
        {
            BO.Task? tsk = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(tsk.Id, flagWorker).Show();
            if ( flagWorker == false)
                TaskList = s_bl?.Task.ReadAll();
            else//if it is a worker than he alredy choosed a task
                this.Close();

        }

        private void Button_Click_Add_Task(object sender, RoutedEventArgs e)//in case of adding a task
        {
            if(flagWorker==false)
            {
                new TaskWindow().ShowDialog();
                TaskList = s_bl?.Task.ReadAll();//reads all the tasks so the new task will show
            }
            else//a worker cant add a task
                MessageBox.Show("You do not have permission for this action \n");
        }
    }
}
