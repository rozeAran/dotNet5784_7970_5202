using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// a window to add a task or update a task and to add a dependency
    /// </summary>
    /// <parameter name="Tsk">: a parameter that keeps the information about this task </parameter>
    /// <parameter name="depId">: a parameter that keeps the id of the task we want to add the dependency list </parameter>
    /// <method name="TaskWindow">: initilize Tsk and every thing else </method>
    /// <method name="Button_Click_Add">: in case we want to add a task </method>
    /// <method name="Button_Click_Update">: in case we want to update an existing task </method>
    /// <method name="Button_Click_Add_Dependency">: if we want to add a dependency </method>
    /// <method name="TextBox_TextChanged">: takes the task id that we got from the user and keeps it in depId </method>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        static int CreatingSchedule=-1;


        public BO.Task Task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Task", typeof(BO.Task), typeof(TaskWindow));

        int depId = 0;
        bool flagWorker= false;//if the flag=true the window was oppend from a worker, if the flag = false than from manager
        public static readonly DependencyProperty DependencyListProperty =
            DependencyProperty.Register(nameof(DependencyList), typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow));
        public IEnumerable<BO.TaskInList>? DependencyList
        {
            get => (IEnumerable<BO.TaskInList>)GetValue(DependencyListProperty);
            set => SetValue(DependencyListProperty, value);
        }
        public TaskWindow(int id=0, bool fworker=false,int getCreatingSchedule=-1)
        {
            if(getCreatingSchedule!=-1)
                CreatingSchedule=getCreatingSchedule;
            InitializeComponent();
            try
            {
                if (id == 0 && getCreatingSchedule == 0)//in case of add only if the schedule is not finished yet
                    Task = new BO.Task()
                    {
                        Id = id,
                        Alias = "",
                        Description = "",
                        CreatedAtDate=DateTime.Now,
                        RequiredEffortTime=TimeSpan.FromSeconds(30),
                        Complexity = BO.EngineerExperience.Beginner,
                        Deliverables="",
                        EngineerId=0,
                        TaskStatus=BO.Status.Status,
                        Remarks="",
                        ScheduledDate=null,
                        CompleteDate=null,
                        DeadLineDate=null,
                        StartDate=null
                    };
                else
                {
                    if (id != 0)//in case of update
                    {
                        Task = s_bl?.Task.Read(id)!;
                        DependencyList = Task.Dependencies.ToList();
                    }
                        
                    
                }

            }
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
            

        }



        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if(flagWorker)
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
                else
                {
                    if (CreatingSchedule != 0)
                    {
                        MessageBox.Show("Cant add a task not during the schedule creation \n");
                    }
                    else
                    {   //if not finished bulidlig schedule then add task
                        s_bl.Task.Create(Task);
                        MessageBox.Show("Task was succsesfuly created");
                    }

                }

            }
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }

        }


        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                s_bl.Task.Update(Task);
                MessageBox.Show("Task was succsesfuly updated");
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlCantBeUpdetedException ex) { MessageBox.Show(ex.Message); }
        }


        private void Button_Click_Add_Dependency(object sender, RoutedEventArgs e)
        {
            try
            {
                if(flagWorker) //if this is a worker
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
                if (CreatingSchedule == -1)
                {
                    BO.Task? dTask = s_bl.Task.Read(depId);
                    if (dTask != null)
                    {
                        MessageBox.Show("Task was not found \n");
                    }
                    BO.TaskInList dep = new BO.TaskInList
                    {
                        Id = depId,
                        Alias = dTask.Alias,
                        Description = dTask.Description,
                        Status = dTask.TaskStatus
                    };
                    Task.Dependencies.Add(dep);
                }
                else
                {
                    MessageBox.Show("Cant add a dependency after schedule was created \n");
                }
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }

        }
       

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  e.Changes.Last()

            var text = (sender as TextBox)!.Text;

            depId = int.Parse(text);
        }


        private void Button_Click_FinishTask(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flagWorker == true)
                {
                    Task.CompleteDate = DateTime.Now;
                    MessageBox.Show("Task was secsesfully finished \n");
                }
                else
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }

        }

        private void TextBox_TextChanged_StartDate(object sender, TextChangedEventArgs e)
        {
            if (flagWorker != true)
                MessageBox.Show("You do not have permission for this action \n");
        }

        private void TextBox_TextChanged_EngId(object sender, TextChangedEventArgs e)
        {
            if (flagWorker != true)
                MessageBox.Show("You do not have permission for this action \n");
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
