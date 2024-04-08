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
        bool flagWorker=false;

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register(nameof(TaskList), typeof(IEnumerable<BO.Task>), typeof(TaskForListWindow));
        public IEnumerable<BO.Task?>? TaskList
        {
            get => (IEnumerable<BO.Task>)GetValue(TaskListProperty);
            set => SetValue(TaskListProperty, value);
        }
       
        public TaskForListWindow( BO.EngineerExperience? workerExperience=null)
        {
            InitializeComponent();
            try
            {
                if (workerExperience == null)
                {
                    TaskList = s_bl?.Task.ReadAll()!;
                }
                else
                {
                    flagWorker = true;
                    TaskList = s_bl?.Task.ReadAll(item => item.TaskStatus == BO.Status.Unscheduled);//myby not true
                    TaskList = s_bl?.Task.ReadAll(item => item.Complexity <= workerExperience);
                    TaskList = s_bl?.Task.ReadAll(item => item.Dependencies == null);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }



        }

        private void ComboBoxLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TaskList = (Experience != BO.EngineerExperience.Level) ?
                    s_bl?.Task.ReadAll(item => item.Complexity == Experience) : s_bl?.Task.ReadAll();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            

        }

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TaskList = (Status != BO.Status.Status) ?
                     s_bl?.Task.ReadAll(item => item.TaskStatus == Status) : s_bl?.Task.ReadAll();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ListView_OpenTaskWindow(object sender, RoutedEventArgs e)
        {
            BO.Task? tsk = (sender as ListView)?.SelectedItem as BO.Task;
            new TaskWindow(tsk.Id, flagWorker).Show();

        }

        private void Button_Click_Add_Task(object sender, RoutedEventArgs e)
        {
            if(flagWorker==false)
                new TaskWindow().ShowDialog();
            else
                MessageBox.Show("You do not have permission for this action \n");
        }
    }
}
