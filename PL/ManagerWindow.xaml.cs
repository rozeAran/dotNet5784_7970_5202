using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Engineer;
using PL.Task;
using BO;
using PL.Gantt;

namespace PL;

/// <summary>
/// the window for the manager 
/// </summary>
/// <method name="ManagerWindow">: the constracture of the window</method>
/// <method name="ButtonEngineer_Click">: show the enginner window </method>
/// <method name="ButtonInitialization_Click">: button to initialize the data</method>
/// <method name="ButtonReset_Click">: button to reset the data</method>
/// <method name="ButtonTaskForList_Click">: button to show the list of tasks</method>    
/// <method name="ButtonGanttChart_Click">: button to show the grantt chart</method>
/// <method name="ButtonCreateSchedule_Click">: button to create the schedule </method>


public partial class ManagerWindow : Window
{
    static int CreatingSchedule = -1;// -1: before starting, 0: while building, 1: finished
    public ManagerWindow()
    {
        InitializeComponent();
    }

    private void ButtonEngineer_Click(object sender, RoutedEventArgs e)
    {
        new EngineerWindow().Show();
    }

    private void ButtonInitialization_Click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Do you want to initialize?", "initialization", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            BlApi.IBl.s_bl.InitializeDB();

    }

    private void ButtonReset_Click(object sender, RoutedEventArgs e)
    {
        BlApi.IBl.s_bl.ResetDB();
    }

    private void ButtonTaskForList_Click(object sender, RoutedEventArgs e)
    {
        new TaskForListWindow().Show();
    }

    private void ButtonGanttChart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (CreatingSchedule == 1)
                new GanttWindow().Show();

        }
        catch (BO.BlNotAPossabilityException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    private void ButtonCreateSchedule_Click(object sender, RoutedEventArgs e)
    {
        CreatingSchedule = 0;
        new CreateSchedule(CreatingSchedule).Show();

    }
}