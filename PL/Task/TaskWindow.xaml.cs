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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        static int CreatingSchedule=-1;
        private BO.Task? Tsk;
        public static readonly DependencyProperty DependencyListProperty =
            DependencyProperty.Register(nameof(DependencyList), typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow));
        public IEnumerable<BO.TaskInList>? DependencyList
        {
            get => (IEnumerable<BO.TaskInList>)GetValue(DependencyListProperty);
            set => SetValue(DependencyListProperty, value);
        }
        public TaskWindow(int id=0,int getCreatingSchedule=-1)
        {
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
                        Tsk = s_bl?.Task.Read(id)!;                       
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
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }

        }

        //info

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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_Add_Dependency(object sender, RoutedEventArgs e)
        {
            //fix
        }
    }
}
