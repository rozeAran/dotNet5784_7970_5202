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
        /*    public string? Alias { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAtDate { get; init; }
    public TimeSpan RequiredEffortTime { get; set; }
    public BO.EngineerExperience Complexity { get; set; }
    public string? Deliverables { get; set; }
    public int EngineerId { get; set; }
    public BO.Status TaskStatus { get; set; }
    public List<BO.TaskInList>? Dependencies { get; set; }
    public BO.EngineerInTask? Engineer { get; set; }
    public string? Remarks { get; set; }
    public DateTime? ScheduledDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;
    public DateTime? DeadLineDate { get; set; } = null;
    public DateTime? StartDate { get; init; } = null;*/
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
                    else//if he is tring to add a task and not dorin the schedule making
                        MessageBox.Show("Cant add a task not doring the schedule creation \n");
                }

            }
            catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message); }
            catch (BO.BlDataNotValidException ex) { MessageBox.Show(ex.Message); }
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
