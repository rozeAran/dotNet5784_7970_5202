using PL.Task;
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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for WorkerWindow.xaml
/// </summary>
public partial class WorkerWindow : Window
{
    int id = 0;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public WorkerWindow(int getid = 0)
    {
        int id = getid;
        InitializeComponent();
        BO.TaskInEngineer MyTask = s_bl?.Engineer.Read(id).Task;
        //צריך להגדיר פה משתנים לכל הפרטים של המשימה ולהציג אותם בלייבלים
    }

    private void ButtonTaskForList_Click(object sender, RoutedEventArgs e)
    {
        new TaskForListWindow().Show();//סינון המשימות
    }

}
