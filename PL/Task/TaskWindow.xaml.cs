using Microsoft.VisualBasic;
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
using System.ComponentModel;

namespace PL.Task
{
    /// <summary>
    /// a window to add a task or update a task and to add a dependency
    /// </summary>
    /// <parameter name="Tsk">: a parameter that keeps the information about this task </parameter>
    /// <parameter name="depId">: a parameter that keeps the id of the task we want to add the dependency list </parameter>
    /// <method name="TaskWindow">: initialize Tsk and every thing else </method>
    /// <method name="Button_Click_Add">: in case we want to add a task </method>
    /// <method name="Button_Click_Update">: in case we want to update an existing task </method>
    /// <method name="Button_Click_Add_Dependency">: if we want to add a dependency </method>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        //static int CreatingSchedule=-1;


        public BO.Task Tsk
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Task.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("Tsk", typeof(BO.Task), typeof(TaskWindow));

        int engId = 0;
        DateTime? start;
        bool flagWorker= false;//if the flag=true the window was oppend from a worker, if the flag = false than from manager
     
        public TaskWindow(int id=0, bool fworker=false)//,int getCreatingSchedule=-1)
        {
           // if(getCreatingSchedule!=-1)
              //  CreatingSchedule=getCreatingSchedule;
            InitializeComponent();
            try
            {
                if (id == 0 )//in case of add
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
                        engId = Tsk.EngineerId;
                        start = Tsk.StartDate;
                    }
                        
                    
                }

            }
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
            catch (BO.ProjectStatusWrong ex) { MessageBox.Show(ex.Message); }
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



        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flagWorker)
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
                else
                {
                    {   //if not finished bulidlig schedule then add task
                       s_bl.Task.Create(Tsk);
                        MessageBox.Show("Task was successfully created");
                        this.Close();

                    }
                    this.Close();
                }
            }
            catch (BO.ProjectStatusWrong ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
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


        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flagWorker==false) 
                {
                    if(Tsk.EngineerId!=engId)//a manager cant choose a task for the worker
                    {
                        MessageBox.Show("A manager can't choose a task for the engineer \n");
                        this.Close();
                    }
                    if(Tsk.StartDate!=start)//a manager cant set the start date
                    {
                        MessageBox.Show("A manager can't choose the start date of the task\n");
                        this.Close();
                    }
                }
                s_bl.Task.Update(Tsk);//update the task
                MessageBox.Show("Task was successfully updated");
                this.Close();
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlCantBeUpdetedException ex) { MessageBox.Show(ex.Message); }
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


        private void Button_Click_Add_Dependency(object sender, RoutedEventArgs e)
        {
            try
            {
                if(flagWorker) //if this is a worker
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
                else
                {
                    int depId = int.Parse(Interaction.InputBox("Enter task Id", "Hi", "0"));//reciving the id of tha depend on task
                    s_bl.Task.AddDependency(Tsk, depId);//ading the dependency
                    MessageBox.Show("Dependency was successfully created \n");
                }

            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
            catch (BO.ProjectStatusWrong ex) { MessageBox.Show(ex.Message); }
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
       


        private void Button_Click_FinishTask(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flagWorker == true)//only a worker can say that he finished the task
                {
                    Tsk.CompleteDate = DateTime.Now;
                    MessageBox.Show("Task was successfully finished \n");
                }
                else//a manager cant say the task is finished
                {
                    MessageBox.Show("You do not have permission for this action \n");
                }
            }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
