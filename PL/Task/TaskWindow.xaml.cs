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
        private BO.Task? Tsk;
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
                    Tsk = new BO.Task()
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
                        Tsk = s_bl?.Task.Read(id)!;
                        DependencyList = Tsk.Dependencies.ToList();
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
                        s_bl.Task.Create(Tsk);
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
                s_bl.Task.Update(Tsk);
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
                    Tsk.Dependencies.Add(dep);
                }
                else
                {
                    MessageBox.Show("cant add a dependency after schedule was created \n");
                }
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }

        }
       

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string s = depndencyId.Text;

            depId = int.Parse(s);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flagWorker == true)
                {
                    Tsk.CompleteDate = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (flagWorker != true)
                MessageBox.Show("You do not have permission for this action \n");
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            if (flagWorker != true)
                MessageBox.Show("You do not have permission for this action \n");
        }
    }
}
